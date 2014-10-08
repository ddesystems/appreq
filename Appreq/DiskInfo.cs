using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  public class DiskInfo {
    [XmlArray("Disks")]
    [XmlArrayItem("Disk", typeof(Disk))]
    public Disk[] Disks { get; set; }
    
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeDisks() {
      return Disks != null && Disks.Length > 0;
    }

    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(DiskInfo other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if (null != Disks && null != other.Disks) {
        foreach (var disk in Disks) {
          foreach (var diskOther in other.Disks) {
            if (!diskOther.CheckPassed.GetValueOrDefault()) {
              disk.Diff(diskOther);
              if (diskOther.CheckPassed.GetValueOrDefault()) {
                other.CheckPassed = true;
              }
            }
          }
        }
      }
    }
  }
}
