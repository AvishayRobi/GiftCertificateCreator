namespace GiftCertificateCreator.Model
{
  public enum eProcessStatus
  {
    /// <summary>
    /// GiftCertificate hasnt been processed yet
    /// </summary>
    Unprocessed = 0,

    /// <summary>
    /// GiftCertificate process succeed
    /// </summary>
    Ok = 1,

    /// <summary>
    /// GiftCertificate process failed
    /// </summary>
    Error = 2
  }
}
