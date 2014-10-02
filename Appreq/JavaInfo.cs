using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class JavaInfo {
    public string JavaHome { get; set; }
    public string CurrentVersion { get; set; }
    public string Security { get; set; }
    public bool? CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(JavaInfo other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = null;
      if (!string.IsNullOrEmpty(CurrentVersion) && !string.IsNullOrEmpty(other.CurrentVersion)) {
        other.CheckPassed = VersionComparer.CompareMinorMajor(CurrentVersion, other.CurrentVersion);
      }

      if (!string.IsNullOrEmpty(Security) && !string.IsNullOrEmpty(other.Security)) {
        other.CheckPassed = (other.CheckPassed ?? true) && Security == other.Security;
      }
    }
  }  
}
