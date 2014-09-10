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
      other.IsDiffMode = true;
      other.CheckPassed =
        !string.IsNullOrEmpty(Name) &&
        !string.IsNullOrEmpty(other.Name) &&
        Name == other.Name &&
        !string.IsNullOrEmpty(Install) &&
        !string.IsNullOrEmpty(other.Install) &&
        Install == other.Install &&
        !string.IsNullOrEmpty(SP) &&
        !string.IsNullOrEmpty(other.SP) &&
        SP == other.SP;
    }
  }
}
