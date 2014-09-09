﻿using System;
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
    public bool CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Software other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (OS != null && other.OS != null) {
        foreach (var os in OS) {
          foreach (var osOther in other.OS) {
            os.Diff(osOther);
            other.CheckPassed = other.CheckPassed && osOther.CheckPassed;
          }
        }
      }
      if (other.Browser != null) {
        foreach (var browser in Browser) {
          foreach (var browserOther in other.Browser) {
            browser.Diff(browserOther);
            other.CheckPassed = other.CheckPassed && browserOther.CheckPassed;
          }
        }
      }
    }
  }
}
