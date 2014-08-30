using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class App {
    [XmlElement(IsNullable=true)]
    public string Id { get; set; }
    [XmlElement(IsNullable=true)]
    public string IdDesc { get; set; }
    [XmlElement(IsNullable=true)]
    public string LongDesc { get; set; }
    [XmlElement(IsNullable=true)]
    public AppVersion Version { get; set; }
  }
}
