using System;
using System.Collections.Generic;
using GiftCertificateCreator.Files;
using GiftCertificateCreator.Logs;
using GiftCertificateCreator.Model;
using WallaShops.Utils;

namespace GiftCertificateCreator.BL
{
  public class GiftCertificateCreatorHandler
  {
    #region Data Members
    private GiftCertificateDalManager dalManager { get; }
    #endregion

    #region Ctor
    public GiftCertificateCreatorHandler()
    {
      dalManager = new GiftCertificateDalManager();
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
      FileInfo fileInfo = getFileInfo();
      string sftpUploadError = processXml(gifts, fileInfo);

      updateProcessStatus(gifts, sftpUploadError, fileInfo.FullPath);
      attachXmlFileIdToGiftCertificates(gifts);
    }

    private string processXml(IEnumerable<GiftCertificate> gifts, FileInfo fileInfo)
    {
      string xml = convertGiftsToXml(gifts);

      saveDataLocalFile(xml, fileInfo.FullPath);

      return uploadToSftp(fileInfo.Name, fileInfo.FullPath);
    }

    private void updateProcessStatus(IEnumerable<GiftCertificate> gifts, string sftpUploadError, string filePath)
    {
      eProcessStatus status = getUploadStatus(sftpUploadError);

      this.dalManager.UpdateProcessStatus(
        creationStatus: status.ToString(),
        failureReason: sftpUploadError,
        filePath: filePath);
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

    private void saveDataLocalFile(string data, string fileFullPath)
      =>
      new FileManager()
      .SaveData(data, fileFullPath);

    private string uploadToSftp(string fileName, string fileFullPath)
      =>
      new SFTPUploader()
      .SetFileFullPath(fileFullPath)
      .SetFileName(fileName)
      .UploadFile();

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
