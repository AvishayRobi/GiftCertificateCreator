using WallaShops.Common.SalesForce;
using WallaShops.Utils;

namespace GiftCertificateCreator.Files
{
  public class SFTPUploader
  {
    #region Data Members
    private string fileFullPath { get; set; }
    private SFSftpClient sftpClient { get; }
    private string remotePath { get; set; }
    private string fileName { get; set; }
    #endregion

    #region Ctor
    public SFTPUploader()
    {
      this.remotePath = WSGeneralUtils.GetAppSettings("---");

      this.sftpClient = new SFSftpClient(
        WSGeneralUtils.GetAppSettings("---"),
        WSGeneralUtils.GetAppSettings("---"),
        WSGeneralUtils.GetAppSettings("---"),
        WSGeneralUtils.GetAppSettingsInt("---"));
    }
    #endregion

    public SFTPUploader SetFileFullPath(string fileFullPath)
    {
      this.fileFullPath = fileFullPath;

      return this;
    }

    public SFTPUploader SetFileName(string fileName)
    {
      this.fileName = fileName;

      return this;
    }

    public string UploadFile()
     =>
      this.sftpClient
      .UploadFile(
        this.fileFullPath,
        this.remotePath,
        this.fileName);
  }
}
