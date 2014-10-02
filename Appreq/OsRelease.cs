using System;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  [Serializable]
  public class OsRelease {
    public string Name { get; set; }
    public string ServicePack { get; set; }
    public bool? CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(OsRelease other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = null;
      if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(other.Name)) {
        other.CheckPassed = (other.CheckPassed ?? true) && VersionComparer.CompareMinorMajor(Name, other.Name);
      }
      if (!string.IsNullOrEmpty(ServicePack) && !string.IsNullOrEmpty(other.ServicePack)) {
        other.CheckPassed = (other.CheckPassed ?? true) && ServicePack == other.ServicePack;
      }
    }
  }
}
