using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class CPUInfo {
    public string Manufacturer { get; set; }
    public string Name { get; set; }
    public UInt64? Datawidth { get; set; }
    public string Maxclockspeed { get; set; }
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
        if (other.CheckPassed.HasValue) {
          other.CheckPassed = other.CheckPassed.Value && Datawidth >= other.Datawidth;
        } else {
          other.CheckPassed = Datawidth >= other.Datawidth;
        }
      }
      if (!other.CheckPassed.GetValueOrDefault()) {
        return;
      }
      if(!string.IsNullOrEmpty(Manufacturer) && !string.IsNullOrEmpty(other.Manufacturer)) {
        if (other.CheckPassed.HasValue) {
          other.CheckPassed = other.CheckPassed.Value && Manufacturer == other.Manufacturer;
        } else {
          other.CheckPassed = Manufacturer == other.Manufacturer;
        }
      }
    }
  }
}
