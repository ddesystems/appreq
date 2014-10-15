using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  [Serializable]
  public class Software {
    public OS OS { get; set; }
    public BrowserInfo Browsers { get; set; }
    [XmlElement("NetFrameworks")]
    public NetFramework NetFramework { get; set; }
    [XmlElement("JavaFrameworks")]
    public JavaFramework JavaFramework { get; set; }
        
    public bool? CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Software other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (null != OS && null != other.OS) {
        OS.Diff(other.OS);
        other.CheckPassed = (other.CheckPassed ?? true) && other.OS.CheckPassed.GetValueOrDefault();
      }
      if (null != Browsers && null != other.Browsers) {
        Browsers.Diff(other.Browsers);
        other.CheckPassed = (other.CheckPassed ?? true) && other.Browsers.CheckPassed.GetValueOrDefault();
      }
      if(null != NetFramework && null != other.NetFramework) {
        NetFramework.Diff(other.NetFramework);
        other.CheckPassed = (other.CheckPassed ?? true) && other.NetFramework.CheckPassed.GetValueOrDefault();
      }
      if (null != JavaFramework && null != other.JavaFramework) {
        JavaFramework.Diff(other.JavaFramework);
        other.CheckPassed = (other.CheckPassed ?? true) && other.JavaFramework.CheckPassed.GetValueOrDefault();
      }
    }
  }
}
