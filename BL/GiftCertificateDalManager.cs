using System.Collections.Generic;
using System.Data;
using System.Linq;
using GiftCertificateCreator.DAL;
using GiftCertificateCreator.Model;
using WallaShops.Utils;

namespace GiftCertificateCreator.BL
{
  public class GiftCertificateDalManager
  {
    #region Data Members
    private GiftCertificateDal dal { get; }
    #endregion

    #region Ctor
    public GiftCertificateDalManager()
    {
      dal = new GiftCertificateDal();
    }
    #endregion

    public IEnumerable<GiftCertificate> GetGiftCertificates()
    {
      DataTable dt = this.dal.GetGiftCertificatesData();

      return from DataRow dr
             in dt.Rows
             select new GiftCertificate()
             {
               CreationDate = WSStringUtils.ToDateTime(dr["create_date"]),
               Enabled = WSStringUtils.ToBoolean(dr["enabled_flag"]),
               RecipientEmail = dr["recipient_email"] as string,
               RecipinetName = dr["recipient_name"] as string,
               EmailSubject = dr["email_subject"] as string,
               Amount = WSStringUtils.ToInt(dr["amount"]),
               SenderName = dr["sender_name"] as string,
               Currency = dr["currency"] as string,
               OrderNo = dr["order_no"] as string,
               Message = dr["message"] as string,
               Status = dr["status"] as string,
               GcID = dr["gc_id"] as string,
               CustomAttributs = new GiftCustomAttributs()
               {
                 ValidFromDate = new CustomAttribute()
                 {
                   Value = WSStringUtils.ToString(dr["valid_from_date"]),
                   AttributeID = "valid_from_date"
                 },
                 ValidToDate = new CustomAttribute()
                 {
                   Value = WSStringUtils.ToString(dr["valid_to_date"]),
                   AttributeID = "valid_to_date"
                 },
                 WsSaleID = new CustomAttribute()
                 {
                   Value = WSStringUtils.ToString(dr["ws_sale_id"]),
                   AttributeID = "ws_sale_id"
                 }
               }
             };
    }

    public void UpdateProcessStatus(FileInfo fileInfo, string creationStatus, string failureReason = "")
      =>
      this.dal.UpdateProcessStatus(fileInfo, creationStatus, failureReason);
    
    public void AttachXmlFileIdToGc(IEnumerable<GiftCertificate> giftCertificates)
      =>      
      giftCertificates
        .Select(getGcId)
        .ApplyEach(attachXmlFileIdToGc);    

    private string getGcId(GiftCertificate giftCertificate)
      =>
      giftCertificate.GcID;

    private void attachXmlFileIdToGc(string gcId)
      =>
      this.dal.AttachXmlFileIdToGc(gcId);
  }
}
