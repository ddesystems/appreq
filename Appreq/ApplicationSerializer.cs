using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  public class ProfileSerializer {
    public static void SerializeToFile(Profile profile, string FileName) {
      var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
      var xs = new XmlSerializer(typeof(Profile));
      var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
      using (var fs = new FileStream(FileName, FileMode.Create)) {
        using (var xw = XmlWriter.Create(fs, settings)) {
          xs.Serialize(xw, profile, emptyNs);
        }
      }
    }
  }
}
