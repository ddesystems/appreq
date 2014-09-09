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
    public bool CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Disk other) {
      other.IsDiffMode = true;
      other.CheckPassed = DriveType == other.DriveType;
      if (!other.CheckPassed) {
        return;
      }
      if(other.AvailableFreeSpace.HasValue && AvailableFreeSpace.HasValue) {
        other.CheckPassed = other.CheckPassed && AvailableFreeSpace.Value >= other.AvailableFreeSpace.Value;
      }
      if (!other.CheckPassed) {
        return;
      }
      if(other.PercentFreeSpace.HasValue && PercentFreeSpace.HasValue) {
        other.CheckPassed = other.CheckPassed && PercentFreeSpace.Value >= other.PercentFreeSpace.Value;
      }
    }
  }
}
