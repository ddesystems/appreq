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
          foreach (var osOther in other.OS) {
            os.Diff(osOther);
            other.CheckPassed = osOther.CheckPassed;
          }
        }
      }
      if (null != Browser && null != other.Browser) {
        var checkPassed = false;
        foreach (var browser in Browser) {
          foreach (var browserOther in other.Browser) {
            browser.Diff(browserOther);
            checkPassed = browserOther.CheckPassed.GetValueOrDefault();
          }
        }
        if (other.CheckPassed.HasValue) {
          other.CheckPassed = other.CheckPassed.Value && checkPassed;
        } else {
          other.CheckPassed = checkPassed;
        }
      }
      if(null != NetFramework && null != other.NetFramework) {
        NetFramework.Diff(other.NetFramework);
        if (other.CheckPassed.HasValue) {
          other.CheckPassed = other.CheckPassed.Value && other.NetFramework.CheckPassed.GetValueOrDefault();
        } else {
          other.CheckPassed = other.NetFramework.CheckPassed.GetValueOrDefault();
        }
      }
      if (null != Java && null != other.Java) {
        var checkPassed = false;
        foreach (var javaOther in other.Java) {
          foreach (var java in Java) {
            java.Diff(javaOther);
            if (!checkPassed) {
              checkPassed = javaOther.CheckPassed.GetValueOrDefault();
              if (checkPassed) {
                break;
              }
            }
            if (other.CheckPassed.HasValue) {
              other.CheckPassed = other.CheckPassed.Value && checkPassed;
            } else {
              other.CheckPassed = checkPassed;
            }
          }
        }
      }
    }
  }
}
