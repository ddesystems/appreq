using System;

namespace Appreq {
  [Serializable]
  public class Disk {
    public string Name { get; set; }
    public string VolumeLabel { get; set; }
    public double TotalSize { get; set; }
    public double AvailableFreeSpace { get; set; }
    public float PercentFreeSpace { get; set; }
    public string DriveType { get; set; }
  }
}
