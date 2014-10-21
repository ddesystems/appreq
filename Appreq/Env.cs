using System;
using System.Xml.Serialization;
using System.ComponentModel;
namespace Appreq {
  [Serializable]
  public class Env {
    [XmlElement("SW")]
    public Software Software { get; set; }
    [XmlElement("HW")]
    public Hardware Hardware { get; set; }

    public bool? CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    private bool _isDiffMode;
    [XmlIgnore]
    public bool IsDiffMode {
      get {
        return _isDiffMode;
      }
      set {
        _isDiffMode = value;
        Software.IsDiffMode = value;
        Hardware.IsDiffMode = value;
      }
    }

    public void Diff(Env other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = null;
      if (null != Hardware && null != other.Hardware) {
        Hardware.Diff(other.Hardware);
        other.CheckPassed = (other.CheckPassed ?? true) && other.Hardware.CheckPassed.GetValueOrDefault();
      }
      if (null != Software && null != other.Software) {
        Software.Diff(other.Software);
        other.CheckPassed = (other.CheckPassed ?? true) && other.Software.CheckPassed.GetValueOrDefault();        
      }
    }
  }
}
