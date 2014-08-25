using System;
using System.Xml.Serialization;
namespace Appreq {
  [Serializable]
  public class Env {
    [XmlElement("SW")]
    public Software Software { get; set; }
    [XmlElement("HW")]
    public Hardware Hardware { get; set; }
  }
}
