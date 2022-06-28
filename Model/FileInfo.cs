namespace GiftCertificateCreator.Model
{
  public class FileInfo
  {
    public string Name { get; set; }

    public string Path { get; set; }

    public string FullPath { get; set; }

    public FileInfo SetFullPath()
    {
      this.FullPath = this.Path + this.Name;

      return this;
    }
  }
}
