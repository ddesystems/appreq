using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class OsRelease {
    [XmlElement(IsNullable = true)]
    public string Name { get; set; }
    [XmlElement(IsNullable = true)]
    public string ServicePack { get; set; }
  }
}
