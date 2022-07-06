using System.IO;

namespace GiftCertificateCreator.Files
{
  public class FileManager
  {
    #region Data Members
    private string fileFullPath { get; }
    #endregion

    #region Ctor
    public FileManager(string fullPath)
    {
      this.fileFullPath = fullPath;
    }
    #endregion

    public static string GetTempPath()
      =>
      Path.GetTempPath();

    public void SaveData(string data)
      =>
      File.WriteAllText(this.fileFullPath, data);

    public void DeleteSavedFile()
      =>
      File.Delete(this.fileFullPath);
  }
}
