using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class RAMInfo: IDiff<RAMInfo> {
    public UInt64? TotalVisibleMemorySize { get; set; }
    public bool ShouldSerializeTotalVisibleMemorySize() { return TotalVisibleMemorySize.HasValue; }
    public UInt64? FreePhysicalMemory { get; set; }
    public bool ShouldSerializeFreePhysicalMemory() { return FreePhysicalMemory.HasValue; }
    public UInt64? TotalVirtualMemorySize { get; set; }
    public bool ShouldSerializeTotalVirtualMemorySize() { return TotalVirtualMemorySize.HasValue; }
    public UInt64? FreeVirtualMemory { get; set; }
    public bool ShouldSerializeFreeVirtualMemory() { return FreeVirtualMemory.HasValue; }

    public RAMInfo Diff(RAMInfo other) {
      return new RAMInfo {
        TotalVirtualMemorySize = TotalVirtualMemorySize > other.TotalVirtualMemorySize ? other.TotalVirtualMemorySize : null,
        TotalVisibleMemorySize = TotalVisibleMemorySize > other.TotalVisibleMemorySize ? other.TotalVisibleMemorySize : null
      };
    }
  }
}
