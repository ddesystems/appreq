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
      other.CheckPassed = !string.IsNullOrEmpty(CurrentVersion) &&
         !string.IsNullOrEmpty(other.CurrentVersion) &&
         CurrentVersion == other.CurrentVersion &&
         !string.IsNullOrEmpty(Security) &&
         !string.IsNullOrEmpty(other.Security) &&
         Security == other.Security;
    }
  }  
}
