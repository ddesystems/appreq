using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class App {
    [XmlElement("Environment")]
    public Env Environment { get; set; }
  }
}
