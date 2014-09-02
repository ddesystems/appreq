using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  [Serializable]
  public class Software: IDiff<Software> {
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

    public Software Diff(Software other) {
      if (null == other) return null;
      var sw = new Software();
      if (OS != null && other.OS != null) {
        var osDiff = new List<OSInfo>();
        var osMatchFound = false;
        foreach (var os in OS) {
          if (osMatchFound) break;
          foreach (var osOther in other.OS) {
            var diff = os.Diff(osOther);
            if (null != diff) {
              osDiff.Add(diff);
            } else {
              osMatchFound = true;
              break;
            }
          }
        }
        if(osMatchFound) {
          sw.OS = null;
        } else {
          sw.OS = osDiff.ToArray();
        }
      }
      if (other.Browser != null) {
        var brwsrDiff = new List<Browser>();
        foreach (var brwsr in Browser) {
          foreach (var brwsrOther in other.Browser) {
            var diff = brwsr.Diff(brwsrOther);
            if (null != diff) { brwsrDiff.Add(diff); }
          }
        }
        sw.Browser = brwsrDiff.ToArray();
      }
      return sw;
    }
  }
}
