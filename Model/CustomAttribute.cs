using System.Xml.Serialization;

namespace GiftCertificateCreator.Model
{
  public class CustomAttribute
  {
    [XmlText]
    public string Value;

    [XmlAttribute("attribute-id")]
    public string AttributeID;
  }
}
