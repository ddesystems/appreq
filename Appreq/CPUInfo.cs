using System;

namespace Appreq {
  [Serializable]
  public class CPUInfo {
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public string Datawidth { get; set; }
    public string Maxclockspeed { get; set; }
  }
}
