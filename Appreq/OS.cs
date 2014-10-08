using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  public class OS {
    [XmlArray("OSs")]
    [XmlArrayItem("OS", typeof(OSInfo))]
    public OSInfo[] Versions { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeVersions() {
      return Versions != null && Versions.Length > 0;
    }

    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(OS other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = null;
      if (null != Versions && null != other.Versions) {
        foreach (var ver in Versions) {
          foreach (var verOther in other.Versions) {
            if (!verOther.CheckPassed.GetValueOrDefault()) {
              ver.Diff(verOther);
              if (verOther.CheckPassed.GetValueOrDefault()) {
                other.CheckPassed = true;
              }
            }
          }
        }
      }
    }
  }
}
