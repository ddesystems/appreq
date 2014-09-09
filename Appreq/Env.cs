using System;
using System.Xml.Serialization;
namespace Appreq {
  [Serializable]
  public class Env {
    [XmlElement("SW")]
    public Software Software { get; set; }
    [XmlElement("HW")]
    public Hardware Hardware { get; set; }
    public bool CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Env other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (null != Hardware && null != other.Hardware) {
        Hardware.Diff(other.Hardware);
        other.CheckPassed = other.Hardware.CheckPassed;
      }
      if (null != Software && null != other.Software) {
        Software.Diff(other.Software);
        other.CheckPassed = other.CheckPassed && other.Software.CheckPassed;
      }
    }
  }
}
