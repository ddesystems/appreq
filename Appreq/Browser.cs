using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Browser {
    public string Name { get; set; }
    public string Version { get; set; }
    //public string CompatibilityMode { get; set; }
    public bool? CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(Browser other) {
      if(null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = 
        !string.IsNullOrEmpty(Name) && 
        !string.IsNullOrEmpty(other.Name) &&
        Name == other.Name;        
      if (!other.CheckPassed.GetValueOrDefault()) {
        return;
      }
      if(!string.IsNullOrEmpty(Version) && !string.IsNullOrEmpty(other.Version)) {
        var otherSplit = other.Version.Split(new[] {'.'}, 4);
        var split = Version.Split('.');
        int ver;
        int verOther;
        if (int.TryParse(split[0], out ver) && int.TryParse(otherSplit[0], out verOther)) {
          if (other.CheckPassed.HasValue) {
            other.CheckPassed = other.CheckPassed.Value && verOther >= ver;
          } else {
            other.CheckPassed = verOther >= ver;
          }
        }
      }
    }
  }
}
