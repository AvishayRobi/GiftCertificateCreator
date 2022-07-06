using System;
using System.Collections.Generic;
using GiftCertificateCreator.Files;
using GiftCertificateCreator.Model;
using WallaShops.Common.Logs.BL;
using WallaShops.Utils;

namespace GiftCertificateCreator.BL
{
  public class GiftCertificateCreatorHandler
  {
    #region Data Members
    private GiftCertificateDalManager dalManager { get; }
    private FileManager fileManager { get; }
    private FileInfo xmlFileInfo { get; }
    #endregion

    #region Ctor
    public GiftCertificateCreatorHandler()
    {
      this.xmlFileInfo = getFileInfo();
      this.dalManager = new GiftCertificateDalManager();
      this.fileManager = new FileManager(xmlFileInfo.FullPath);
    }
    #endregion

    public void Exec()
    {
      try
      {
        IEnumerable<GiftCertificate> gifts = this.dalManager.GetGiftCertificates();
        handleGiftCertificates(gifts);
      }
      catch (Exception ex)
      {
        LogHandler.WriteToConsoleIfDebugMode($"Exception: {ex.Message} \nStackTrace: {ex.StackTrace}");
      }
    }

    private void handleGiftCertificates(IEnumerable<GiftCertificate> gifts)
    {      
      string sftpUploadError = processXml(gifts);
      deleteLocalFile();

      updateProcessStatus(gifts, sftpUploadError);
      attachXmlFileIdToGiftCertificates(gifts);
    }

    private string processXml(IEnumerable<GiftCertificate> gifts)
    {
      string xml = convertGiftsToXml(gifts);

      saveDataLocalFile(xml);
      this.xmlFileInfo.VirtualPath = uploadXmlToBlob(xml);

      return uploadXmlToSftp();
    }

    private void updateProcessStatus(IEnumerable<GiftCertificate> gifts, string sftpUploadError)
    {
      eProcessStatus status = getUploadStatus(sftpUploadError);

      this.dalManager.UpdateProcessStatus(
        creationStatus: status.ToString(),
        failureReason: sftpUploadError,
        fileInfo: this.xmlFileInfo);
    }

    private eProcessStatus getUploadStatus(string sftpUploadError)
    {
      bool isSftpUploadSuccess = string.IsNullOrEmpty(sftpUploadError);

      return isSftpUploadSuccess ? eProcessStatus.Ok : eProcessStatus.Error;
    }

    private void attachXmlFileIdToGiftCertificates(IEnumerable<GiftCertificate> gifts)
      =>
      this.dalManager.AttachXmlFileIdToGc(gifts);

    private string convertGiftsToXml(IEnumerable<GiftCertificate> gifts)
      =>
      new GiftCertificateXmlManager()
      .ConvertGiftCertToXml(gifts)
      .SetHeaders()
      .GetXml();

    private void saveDataLocalFile(string data)
      =>
      this.fileManager
      .SaveData(data);

    private void deleteLocalFile()
      =>
      this.fileManager
      .DeleteSavedFile();

    private string uploadXmlToSftp()
      =>
      new SFTPUploader()
      .SetFileInfo(this.xmlFileInfo)
      .UploadFile();

    private string uploadXmlToBlob(string xml)
      =>
      new BlobUploader()
      .GenerateFullPath(this.xmlFileInfo.Name)
      .ConvertFileContent(xml)
      .Upload();

    private FileInfo getFileInfo()
      =>
      new FileInfo()
      {
        Name = generateFileName(),
        Path = FileManager.GetTempPath()
      }
      .SetFullPath();

    private string generateFileName()
      =>
      WSGeneralUtils.GetAppSettings("GiftCertificateFilePrefix")
      + DateTime.Now.ToString(format: "ddmmyyyy_HHmm")
      + ".xml";
  }
}
