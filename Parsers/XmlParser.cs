using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GiftCertificateCreator.Parsers
{
  public class XmlParser<T> where T : class
  {
    public static string Serialize(T obj)
    {
      XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
      var emptyNamespace = getEmptyNamesapce();

      using (var sw = new StringWriter())
      {
        using (XmlTextWriter writer = new XmlTextWriter(sw) { Formatting = Formatting.Indented })
        {
          xsSubmit.Serialize(writer, obj, emptyNamespace);

          return sw.ToString();
        }
      }
    }

    private static XmlSerializerNamespaces getEmptyNamesapce()
      =>
      new XmlSerializerNamespaces(
        new[]
        {
          XmlQualifiedName.Empty
        });
  }
}
