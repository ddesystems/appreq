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
          foreach (var cpuOther in other.CPU) {
            cpu.Diff(cpuOther);
            if (other.CheckPassed.HasValue) {
              other.CheckPassed = other.CheckPassed.Value && cpuOther.CheckPassed.GetValueOrDefault();
            } else {
              other.CheckPassed = cpuOther.CheckPassed.GetValueOrDefault();
            }
          }
        }
      }
      if (null != Disks && null != other.Disks) {
        foreach (var disk in Disks) {
          foreach (var diskOther in other.Disks) {
            disk.Diff(diskOther);
            if (other.CheckPassed.HasValue) {
              other.CheckPassed = other.CheckPassed.Value && diskOther.CheckPassed.GetValueOrDefault();
            } else {
              other.CheckPassed = diskOther.CheckPassed.GetValueOrDefault();
            }
          }
        }
      }
      if (null != RAM && null != other.RAM) {
        RAM.Diff(other.RAM);
        if (other.CheckPassed.HasValue) {
          other.CheckPassed = other.CheckPassed.Value && other.RAM.CheckPassed.GetValueOrDefault();
        } else {
          other.CheckPassed = other.RAM.CheckPassed.GetValueOrDefault();
        }
      }
    }
  }
}