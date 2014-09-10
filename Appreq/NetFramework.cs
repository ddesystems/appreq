using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class NetFramework {
    [XmlArray("Versions")]
    [XmlArrayItem("Version", typeof(NetFrameworkVersion))]
    public NetFrameworkVersion[] Versions { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeVersions() {
      return Versions != null && Versions.Length > 0;
    }
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(NetFramework other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (null != Versions && null != other.Versions) {
        foreach (var ver in Versions) {
          foreach (var verOther in other.Versions) {
            ver.Diff(verOther);
            if (other.CheckPassed.HasValue) {
              other.CheckPassed = other.CheckPassed.Value && verOther.CheckPassed.GetValueOrDefault();
            } else {
              other.CheckPassed = verOther.CheckPassed.GetValueOrDefault();
            }
          }
        }
      }
    }
  }
}
