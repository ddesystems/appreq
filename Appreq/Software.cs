using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Software {
    [XmlArray("OSs", IsNullable = true)]
    [XmlArrayItem("OS", typeof(OSInfo))]
    public OSInfo[] OS { get; set; }
  }
}
