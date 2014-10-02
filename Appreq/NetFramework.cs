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
          var found = false;
          foreach (var verOther in other.Versions) {
            if (found) { break; }
            ver.Diff(verOther);
            found = verOther.CheckPassed.GetValueOrDefault();
          }
          other.CheckPassed = found;
        }
      }
    }
  }
}
