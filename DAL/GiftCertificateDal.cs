using System.Data;
using WallaShops.Data;
using WallaShops.Objects;

namespace GiftCertificateCreator.DAL
{
  public class GiftCertificateDal : WSSqlHelper
  {
    #region Ctor
    public GiftCertificateDal() : base(WSPlatforms.WallaShops)
    {
    }
    #endregion    

    public void UpdateProcessStatus(string filePath, string creationStatus, string failureReason = "")
    {
      WSSqlParameters spParams = new WSSqlParameters();
      spParams.AddInputParameter("@file_path", filePath);
      spParams.AddInputParameter("@creation_status", creationStatus);
      spParams.AddInputParameter("@failure_reason", failureReason);

      this.ExecuteNonQuery("gift_certificates_update_status", ref spParams);
    }

    public void AttachXmlFileIdToGc(string gcId)
    {
      WSSqlParameters spParams = new WSSqlParameters();
      spParams.AddInputParameter("@gc_id", gcId);

      this.ExecuteNonQuery("gift_certificates_attach_xmlfileid_to_gc", ref spParams);
    }

    public DataTable GetGiftCertificatesData()
      =>
      this.GetDataTable("gift_certificates_get_all");    
  }
}
