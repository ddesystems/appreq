using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  public class ProfileSerializer {
    public static void SerializeToFile(Profile profile, string fileName) {
      var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
      var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
      using (var fs = new FileStream(fileName, FileMode.Create)) {
        using (var xw = XmlWriter.Create(fs, settings)) {
          var xs = new XmlSerializer(typeof(Profile));
          xs.Serialize(xw, profile, emptyNs);
        }
      }
    }

    public static Profile Deserialize(string fileName) {
      using(var fs = new FileStream(fileName, FileMode.Open)) {
        using(var xr = XmlReader.Create(fs)) {
          var xs = new XmlSerializer(typeof(Profile));
          return (Profile) xs.Deserialize(xr);
        }
      }
    }
  }
}
