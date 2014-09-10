using System;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Appreq {
  [Serializable]
  public class OsRelease {
    public string Name { get; set; }
    public string ServicePack { get; set; }
    public bool CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(OsRelease other) {
      if (null == other) {
        return;
      } 
      other.IsDiffMode = true;
      other.CheckPassed = !string.IsNullOrEmpty(Name) && 
         !string.IsNullOrEmpty(other.Name) && 
         Name == other.Name &&
         !string.IsNullOrEmpty(ServicePack) && 
         !string.IsNullOrEmpty(other.ServicePack) &&
         ServicePack == other.ServicePack;
    }
  }
}
