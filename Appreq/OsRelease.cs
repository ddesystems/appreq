using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class OsRelease: IDiff<OsRelease> {
    [XmlElement(IsNullable = true)]
    public string Name { get; set; }
    [XmlElement(IsNullable = true)]
    public string ServicePack { get; set; }

    public OsRelease Diff(OsRelease other) {
      //TODO: implement
      return new OsRelease {
      };
    }
  }
}
