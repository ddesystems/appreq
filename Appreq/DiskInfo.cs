using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  public class DiskInfo {
    [XmlElement("Disk")]
    public List<Disk> Disks { get; set; }
    
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeDisks() {
      return Disks != null && Disks.Count > 0;
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
        foreach (var d in Disks) {
          d.IsDiffMode = value;
        }
      }
    }

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
