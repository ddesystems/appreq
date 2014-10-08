using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Appreq {
  public class NetFrameworkVersion {
    public string Name { get; set; }
    public string Install { get; set; }
    public string SP { get; set; }
    public string WCFEnable { get; set; }
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(NetFrameworkVersion other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = null;
      if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(other.Name)) {
        other.CheckPassed = VersionComparer.CompareMinorMajor(Name, other.Name);
      }
    }
  }
}
