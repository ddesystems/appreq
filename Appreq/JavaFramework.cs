using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  public class JavaFramework {
    [XmlArray("Versions")]
    [XmlArrayItem("Version", typeof(JavaInfo))]
    public JavaInfo[] Versions { get; set; }

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeVersions() {
      return Versions != null && Versions.Length > 0;
    }

    public bool? CheckPassed { get; set; }
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
