using System;

namespace Appreq {
  [Serializable]
  public class RAMInfo {
    public int TotalVisibleMemorySize { get; set; }
    public int FreePhysicalMemory { get; set; }
    public int TotalVirtualMemorySize { get; set; }
    public int FreeVirtualMemory { get; set; }
  }
}
