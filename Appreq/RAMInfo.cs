using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class RAMInfo {
    public UInt64? TotalVisibleMemorySize { get; set; }
    public bool ShouldSerializeTotalVisibleMemorySize() { return TotalVisibleMemorySize.HasValue; }
    public UInt64? FreePhysicalMemory { get; set; }
    public bool ShouldSerializeFreePhysicalMemory() { return FreePhysicalMemory.HasValue; }
    public UInt64? TotalVirtualMemorySize { get; set; }
    public bool ShouldSerializeTotalVirtualMemorySize() { return TotalVirtualMemorySize.HasValue; }
    public UInt64? FreeVirtualMemory { get; set; }
    public bool ShouldSerializeFreeVirtualMemory() { return FreeVirtualMemory.HasValue; }
    public bool CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(RAMInfo other) {
      other.IsDiffMode = true;
      if(TotalVirtualMemorySize.HasValue && other.TotalVirtualMemorySize.HasValue) {
        other.CheckPassed = TotalVirtualMemorySize.Value >= other.TotalVirtualMemorySize;
      }
      if(TotalVisibleMemorySize.HasValue && other.TotalVisibleMemorySize.HasValue) {
        other.CheckPassed = other.CheckPassed && TotalVisibleMemorySize >= other.TotalVisibleMemorySize;
      }
    }
  }
}
