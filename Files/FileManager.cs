using System.IO;

namespace GiftCertificateCreator.Files
{
  public class FileManager
  {
    public static string GetTempPath()
      =>
      Path.GetTempPath();

    public void SaveData(string data, string fileFullPath)
      =>
      File.WriteAllText(fileFullPath, data);      
  }
}
