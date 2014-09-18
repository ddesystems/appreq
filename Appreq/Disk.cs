using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Disk {
    public string Name { get; set; }
    public string VolumeLabel { get; set; }
    public double? TotalSize { get; set; }
    public bool ShouldSerializeTotalSize() { return TotalSize.HasValue; }
    public double? AvailableFreeSpace { get; set; }
    public bool ShouldSerializeAvailableFreeSpace() { return AvailableFreeSpace.HasValue; }
    public float? PercentFreeSpace { get; set; }
    public bool ShouldSerializePercentFreeSpace() { return PercentFreeSpace.HasValue; }
    public string DriveType { get; set; }
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Disk other) {
      other.IsDiffMode = true;
      other.CheckPassed = DriveType == other.DriveType;
      if (!other.CheckPassed.GetValueOrDefault()) {
        return;
      }
      if(other.AvailableFreeSpace.HasValue && AvailableFreeSpace.HasValue) {
        if (other.CheckPassed.HasValue) {
          other.CheckPassed = other.CheckPassed.Value && other.AvailableFreeSpace.Value >= AvailableFreeSpace.Value;
        } else {
          other.CheckPassed = other.AvailableFreeSpace.Value >= AvailableFreeSpace.Value;
        }
      }
      if (!other.CheckPassed.GetValueOrDefault()) {
        return;
      }
      if(other.PercentFreeSpace.HasValue && PercentFreeSpace.HasValue) {
        if (other.CheckPassed.HasValue) {
          other.CheckPassed = other.CheckPassed.Value && other.PercentFreeSpace.Value >= PercentFreeSpace.Value;
        } else {
          other.CheckPassed = other.PercentFreeSpace.Value >= PercentFreeSpace.Value;
        }
      }
    }
  }
}
