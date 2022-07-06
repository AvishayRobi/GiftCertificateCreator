using System.IO;
using System.Text;
using WallaShops.Data;
using WallaShops.Utils;

namespace GiftCertificateCreator.Files
{
  public class BlobUploader
  {
    #region Data Members
    private AzureBlobClient azureBlobClient { get; }
    private Stream fileContent { get; set; }
    private string fullPath { get; set; }
    private string azurePath { get; }
    #endregion

    #region Ctor
    public BlobUploader()
    {
      this.azurePath = WSGeneralUtils.GetAppSettings("AzureUploadPath");
      this.azureBlobClient = new AzureBlobClient();
    }
    #endregion

    public BlobUploader ConvertFileContent(string content)
    {
      byte[] byteArray = Encoding.UTF8.GetBytes(content);
      this.fileContent = new MemoryStream(byteArray);

      return this;
    }    

    public BlobUploader GenerateFullPath(string fileName)
    {
      this.fullPath = this.azurePath + fileName;

      return this;
    }

    public string Upload()
      =>
      this.azureBlobClient.SaveFile("upload", this.fullPath, this.fileContent, "text/xml");   
  }
}
