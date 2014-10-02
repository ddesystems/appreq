using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Hardware {
    [XmlArray("Disks")]
    public Disk[] Disks { get; set; }
    [XmlArray("CPUs")]
    [XmlArrayItem("CPU", typeof(CPUInfo))]
    public CPUInfo[] CPU { get; set; }
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
      if (null != Disks && null != other.Disks) {
        foreach (var disk in Disks) {
          var found = false;
          foreach (var diskOther in other.Disks) {
            disk.Diff(diskOther);
            found = diskOther.CheckPassed.GetValueOrDefault();
            if (found) { break; }
          }
          other.CheckPassed = (other.CheckPassed ?? true) && found;
          if (found) { break; }
        }
      }
      if (null != RAM && null != other.RAM) {
        RAM.Diff(other.RAM);
        other.CheckPassed = (other.CheckPassed ?? true) && other.RAM.CheckPassed.GetValueOrDefault();
      }
    }
  }
}