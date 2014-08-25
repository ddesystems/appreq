using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class OSInfo {
    public string Name { get; set; }    
    public string Architecture { get; set; }
    [XmlArray("Releases", IsNullable = true)]
    [XmlArrayItem("Release", typeof(OsRelease))]
    public OsRelease[] Release { get; set; }
  }
}
