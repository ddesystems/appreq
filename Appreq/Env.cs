using System;
using System.Xml.Serialization;
namespace Appreq {
  [Serializable]
  public class Env: IDiff<Env> {
    [XmlElement("SW")]
    public Software Software { get; set; }
    [XmlElement("HW")]
    public Hardware Hardware { get; set; }

    public Env Diff(Env other) {
      return new Env {
        Hardware = Hardware.Diff(other.Hardware),
        Software = Software.Diff(other.Software)
      };
    }
  }
}
