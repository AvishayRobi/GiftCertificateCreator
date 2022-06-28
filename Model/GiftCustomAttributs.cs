using System.Xml.Serialization;

namespace GiftCertificateCreator.Model
{
  public class GiftCustomAttributs
  {
    [XmlElement(ElementName = "custom-attribute", Order = 1)]
    public CustomAttribute ValidFromDate { get; set; }

    [XmlElement(ElementName = "custom-attribute", Order = 2)]
    public CustomAttribute ValidToDate { get; set; }

    [XmlElement(ElementName = "custom-attribute", Order = 3)]
    public CustomAttribute WsSaleID { get; set; }
  }  
}
