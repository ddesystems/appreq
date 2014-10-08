using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Hardware {
    [XmlArray("CPUs")]
    [XmlArrayItem("CPU", typeof(CPUInfo))]
    public CPUInfo[] CPU { get; set; }

    public DiskInfo Disk { get; set; }
    public RAMInfo RAM { get; set; }
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Hardware other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (null != CPU && null != other.CPU) {
        foreach (var cpu in CPU) {
          var found = false;
          foreach (var cpuOther in other.CPU) {
            cpu.Diff(cpuOther);
            found = cpuOther.CheckPassed.GetValueOrDefault();
            if (found) { break; }
          }
          other.CheckPassed = found;
        }
      }
      if (null != Disk && null != other.Disk) {
        Disk.Diff(other.Disk);
        other.CheckPassed = (other.CheckPassed ?? true) && other.Disk.CheckPassed.GetValueOrDefault();
      }
      if (null != RAM && null != other.RAM) {
        RAM.Diff(other.RAM);
        other.CheckPassed = (other.CheckPassed ?? true) && other.RAM.CheckPassed.GetValueOrDefault();
      }
    }
  }
}