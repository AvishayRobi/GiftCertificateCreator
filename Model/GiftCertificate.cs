using System;
using System.Xml.Serialization;

namespace GiftCertificateCreator.Model
{
  [XmlRoot(ElementName = "gift-certificate")]
  public class GiftCertificate
  {
    [XmlAttribute("gc-id")]
    public string GcID { get; set; }

    [XmlElement("create-date")]
    public DateTime CreationDate { get; set; }   
   
    [XmlElement("order-no")]
    public string OrderNo { get; set; }

    [XmlElement("enabled-flag")]
    public bool Enabled { get; set; }

    [XmlElement("status")]
    public string Status { get; set; }

    [XmlElement("currency")]
    public string Currency { get; set; }

    [XmlElement("amount")]
    public int Amount { get; set; }

    [XmlElement("custom-attributes")]
    public GiftCustomAttributs CustomAttributs { get; set; }

    [XmlElement("recipient-name")]
    public string RecipinetName { get; set; }

    [XmlElement("recipient-email")]
    public string RecipientEmail { get; set; }

    [XmlElement("sender-name")]
    public string SenderName { get; set; }

    [XmlElement("message")]
    public string Message { get; set; }

    [XmlElement("email-subject")]
    public string EmailSubject { get; set; }
  }
}
