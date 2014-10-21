using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  [XmlInclude(typeof(OSInfo))]
  public class OS {
    [XmlElement("OS")]
    public List<OSInfo> Versions { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeVersions() {
      return Versions != null && Versions.Count > 0;
    }

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
        foreach (var v in Versions) {
          v.IsDiffMode = value;
        }
      }
    }

    public void Diff(OS other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = null;
      if (null != Versions && null != other.Versions) {
        foreach (var ver in this.Versions) {
          foreach (var verOther in other.Versions) {
            if (!verOther.CheckPassed.GetValueOrDefault()) {
              ver.Diff(verOther);
              if (verOther.CheckPassed.GetValueOrDefault()) {
                other.CheckPassed = true;
              }
            }
          }
        }
      }
    }    
  }
}
