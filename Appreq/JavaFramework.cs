using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class JavaFramework {
    [XmlElement("JavaFramework")]
    public List<JavaInfo> Versions { get; set; }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeVersions() {
      return Versions != null && Versions.Count > 0;
    }

    public bool? CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(JavaFramework other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
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
