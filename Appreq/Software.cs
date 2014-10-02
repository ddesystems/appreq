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
    public NetFramework NetFramework { get; set; }
    [XmlArray("Browsers")]
    [XmlArrayItem("Browser", typeof(Browser))]
    public Browser[] Browser { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeBrowser() {
      return Browser != null && Browser.Length > 0;
    }
    [XmlArray("JavaFramework")]
    [XmlArrayItem("Version", typeof(JavaInfo))]
    public JavaInfo[] Java { get; set; }
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
      if (null != Browser && null != other.Browser) {
        foreach (var browser in Browser) {
          var found = false;
          foreach (var browserOther in other.Browser) {
            browser.Diff(browserOther);
            found = browserOther.CheckPassed.GetValueOrDefault();
            if(found) { break; }
          }
          other.CheckPassed = (other.CheckPassed ?? true) && found;
        }
      }
      if(null != NetFramework && null != other.NetFramework) {
        NetFramework.Diff(other.NetFramework);
        other.CheckPassed = (other.CheckPassed ?? true) && other.NetFramework.CheckPassed.GetValueOrDefault();
      }
      if (null != Java && null != other.Java) {
        foreach (var java in Java) {
          var found = false;
          foreach (var javaOther in other.Java) {
            java.Diff(javaOther);
            found = javaOther.CheckPassed.GetValueOrDefault();
            if (found) { break; }
          }
          other.CheckPassed = (other.CheckPassed ?? true) && found;
        }
      }
    }
  }
}
