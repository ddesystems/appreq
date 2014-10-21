using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  public class CPU {
    [XmlElement("CPU")]
    public List<CPUInfo> CPUs { get; set; }
    
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCPUs() {
      return CPUs != null && CPUs.Count > 0;
    }

    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    private bool _isDiffMode;
    [XmlIgnore]
    public bool IsDiffMode {
      get {
        return _isDiffMode;
      }
      set {
        _isDiffMode = value;
        foreach (var cpu in CPUs) {
          cpu.IsDiffMode = value;
        }
      }
    }

    public void Diff(CPU other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (null != CPUs && null != other.CPUs) {
        foreach (var cpu in CPUs) {
          foreach (var cpuOther in other.CPUs) {
            if (!cpuOther.CheckPassed.GetValueOrDefault()) {
              cpu.Diff(cpuOther);
              if (cpuOther.CheckPassed.GetValueOrDefault()) {
                other.CheckPassed = true;
              }
            }
          }
        }
      }
    }
  }
}
