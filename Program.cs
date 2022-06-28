using GiftCertificateCreator.BL;

namespace GiftCertificateCreator
{
  public class Program
  {
    public static void Main(string[] args)
    {
      GiftCertificateCreatorHandler handler = new GiftCertificateCreatorHandler();

      handler.Exec();
    }
  }
}
