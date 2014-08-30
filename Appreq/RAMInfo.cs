using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class RAMInfo {
    [XmlElement(IsNullable = true)]
    public int? TotalVisibleMemorySize { get; set; }
    [XmlElement(IsNullable = true)]
    public int? FreePhysicalMemory { get; set; }
    [XmlElement(IsNullable = true)]
    public int? TotalVirtualMemorySize { get; set; }
    [XmlElement(IsNullable = true)]
    public int? FreeVirtualMemory { get; set; }

    public RAMInfo Diff(RAMInfo other) {
      return new RAMInfo {
        TotalVirtualMemorySize = TotalVirtualMemorySize >= other.TotalVirtualMemorySize ? other.TotalVirtualMemorySize : null,
        TotalVisibleMemorySize = TotalVisibleMemorySize >= other.TotalVisibleMemorySize ? other.TotalVisibleMemorySize : null
      };
    }
  }
}
