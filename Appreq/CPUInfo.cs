using System;

namespace Appreq {
  [Serializable]
  public class CPUInfo: IDiff<CPUInfo> {
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public UInt64? Datawidth { get; set; }
    public string Maxclockspeed { get; set; }

    public CPUInfo Diff(CPUInfo other) {
      return new CPUInfo {
        Datawidth = Datawidth > other.Datawidth ? other.Datawidth : null,
        Manufacturer = Manufacturer != other.Manufacturer ? other.Manufacturer : null
        //Maxclockspeed
        //Name
      };
    }
  }
}
