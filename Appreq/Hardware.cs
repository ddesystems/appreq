using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Hardware {
    public CPU CPU { get; set; }
    [XmlElement("Disks")]
    public DiskInfo Disk { get; set; }
    public RAMInfo RAM { get; set; }

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
        CPU.IsDiffMode = value;
        Disk.IsDiffMode = value;
        RAM.IsDiffMode = value;
      }
    }

    public void Diff(Hardware other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (null != CPU && null != other.CPU) {
        CPU.Diff(other.CPU);
        other.CheckPassed = (other.CheckPassed ?? true) && other.CPU.CheckPassed.GetValueOrDefault();
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