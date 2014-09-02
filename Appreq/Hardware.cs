using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  public class Hardware: IDiff<Hardware> {
    [XmlArray("Disks")]
    public Disk[] Disks { get; set; }
    [XmlArray("CPUs")]
    [XmlArrayItem("CPU", typeof(CPUInfo))]
    public CPUInfo[] CPU { get; set; }
    public RAMInfo RAM { get; set; }

    public Hardware Diff(Hardware other) {
      if (null == other) {
        return null;
      }
      var hw = new Hardware();
      if (null != CPU && null != other.CPU) {
        var cpuDiff = new List<CPUInfo>();
        foreach (var cpu in CPU) {
          foreach (var cpuOther in other.CPU) {
            cpuDiff.Add(cpu.Diff(cpuOther));
          }
        }
        hw.CPU = cpuDiff.ToArray();
      }
      if (null != Disks && null != other.Disks) {
        var diskDiff = new List<Disk>();
        foreach (var disk in Disks) {
          foreach (var diskOther in other.Disks) {
            diskDiff.Add(disk.Diff(diskOther));
          }
        }
        hw.Disks = diskDiff.ToArray();
      }
      if (null != other.RAM) {
        hw.RAM = RAM.Diff(other.RAM);
      }
      return hw;
    }
  }
}