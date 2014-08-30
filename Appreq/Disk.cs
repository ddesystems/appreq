using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Disk {
    public string Name { get; set; }
    public string VolumeLabel { get; set; }
    [XmlElement(IsNullable=true)]
    public double? TotalSize { get; set; }
    [XmlElement(IsNullable = true)]
    public double? AvailableFreeSpace { get; set; }
    [XmlElement(IsNullable = true)]
    public float? PercentFreeSpace { get; set; }
    public string DriveType { get; set; }
  }
}
