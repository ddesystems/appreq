using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class CPUInfo {
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public UInt64? Datawidth { get; set; }
    public string Maxclockspeed { get; set; }
    public bool CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(CPUInfo other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      if(Datawidth.HasValue && other.Datawidth.HasValue) {
        other.CheckPassed = other.CheckPassed && Datawidth >= other.Datawidth;
      }
      if (!other.CheckPassed) {
        return;
      }
      if(!string.IsNullOrEmpty(Manufacturer) && !string.IsNullOrEmpty(other.Manufacturer)) {
        other.CheckPassed = other.CheckPassed && Manufacturer == other.Manufacturer;
      }
    }
  }
}
