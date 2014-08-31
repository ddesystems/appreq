using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Disk: IDiff<Disk> {
    public string Name { get; set; }
    public string VolumeLabel { get; set; }
    public double? TotalSize { get; set; }
    public bool ShouldSerializeTotalSize() { return TotalSize.HasValue; }
    public double? AvailableFreeSpace { get; set; }
    public bool ShouldSerializeAvailableFreeSpace() { return AvailableFreeSpace.HasValue; }
    public float? PercentFreeSpace { get; set; }
    public bool ShouldSerializePercentFreeSpace() { return PercentFreeSpace.HasValue; }
    public string DriveType { get; set; }

    public Disk Diff(Disk other) {
      return new Disk {
        DriveType = DriveType != other.DriveType ? other.DriveType : null,
        AvailableFreeSpace = AvailableFreeSpace.GetValueOrDefault() > other.AvailableFreeSpace.GetValueOrDefault() ? other.AvailableFreeSpace : null,
        PercentFreeSpace = PercentFreeSpace.GetValueOrDefault() > other.PercentFreeSpace.GetValueOrDefault() ? other.PercentFreeSpace : null
      };
    }
  }
}
