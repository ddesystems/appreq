using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  [Serializable]
  public class Software {
    [XmlArray("OSs")]
    [XmlArrayItem("OS", typeof(OSInfo))]
    public OSInfo[] OS { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeOS() {
      return OS != null && OS.Length > 0;
    }
    public BrowserInfo Browsers { get; set; }
    public NetFramework NetFramework { get; set; }
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
        foreach (var os in OS) {
          var found = false;
          foreach (var osOther in other.OS) {
            os.Diff(osOther);
            found = osOther.CheckPassed.GetValueOrDefault();
            if (found) { break; }
          }
          other.CheckPassed = found;
          if (found) { break; }
        }
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
