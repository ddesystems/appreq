using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class CPUInfo {
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public UInt16? Datawidth { get; set; }
    public UInt32? Maxclockspeed { get; set; }
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(CPUInfo other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;

      if(Datawidth.HasValue && other.Datawidth.HasValue) {
        other.CheckPassed = (other.CheckPassed ?? true) && other.Datawidth >= Datawidth;
      }

      if (Maxclockspeed.HasValue && other.Datawidth.HasValue) {
        other.CheckPassed = (other.CheckPassed ?? true) && other.Maxclockspeed >= Maxclockspeed;
      }
      
      if(!string.IsNullOrEmpty(Manufacturer) && !string.IsNullOrEmpty(other.Manufacturer)) {
        other.CheckPassed = (other.CheckPassed ?? true) && Manufacturer == other.Manufacturer;
      }
    }
  }
}
