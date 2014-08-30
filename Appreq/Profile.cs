using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Profile {
    [XmlArray("Apps", IsNullable=true)]
    public App[] Apps { get; set; }
    [XmlElement("Environment", IsNullable=true)]
    public Env Environment { get; set; }
  }
}
