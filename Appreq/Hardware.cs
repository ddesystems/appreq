using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  public class Hardware {
    [XmlArray("Disks")]
    public Disk[] Disks { get; set; }
    [XmlArray("CPUs")]
    [XmlArrayItem("CPU", typeof(CPUInfo))]
    public CPUInfo[] CPU { get; set; }
    public RAMInfo RAM { get; set; }
    public bool CheckPassed { get; set; }
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
            other.CheckPassed = other.CheckPassed && cpuOther.CheckPassed;
          }
        }
      }
      if (null != Disks && null != other.Disks) {
        foreach (var disk in Disks) {
          foreach (var diskOther in other.Disks) {
            disk.Diff(diskOther);
            other.CheckPassed = other.CheckPassed && diskOther.CheckPassed;
          }
        }
      }
      if (null != RAM && null != other.RAM) {
        RAM.Diff(other.RAM);
        other.CheckPassed = other.CheckPassed && other.RAM.CheckPassed;
      }
    }
  }
}