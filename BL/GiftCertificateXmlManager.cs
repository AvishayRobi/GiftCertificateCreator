using System;
using System.Collections.Generic;
using System.Linq;
using GiftCertificateCreator.Model;
using GiftCertificateCreator.Parsers;

namespace GiftCertificateCreator.BL
{
  public class GiftCertificateXmlManager
  {
    #region Data Members
    private string xmlDocument { get; set; }
    #endregion

    #region Ctor
    public GiftCertificateXmlManager()
    {
    }
    #endregion

    public GiftCertificateXmlManager ConvertGiftCertToXml(IEnumerable<GiftCertificate> gifts)
    {
      this.xmlDocument =
        gifts
        .Select(parseXml)
        .Select(removeDeclaration)
        .Aggregate(separateByNewLine);

      return this;
    }

    public GiftCertificateXmlManager SetHeaders()
    {
      this.xmlDocument =
        ""; // WS Logic

      return this;
    }

    public string GetXml()
      =>
      this.xmlDocument;

    private string parseXml(GiftCertificate gift)
      =>
      XmlParser<GiftCertificate>
      .Serialize(gift);

    private string separateByNewLine(string a, string b)
      =>
      a + Environment.NewLine + b;

    private string removeDeclaration(string xml)
      =>
      ""; // WS Logic    
  }
}
