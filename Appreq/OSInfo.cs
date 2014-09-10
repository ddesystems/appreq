using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;

namespace Appreq {
  [Serializable]
  public class OSInfo {
    public string Name { get; set; }
    public string Architecture { get; set; }
    [XmlArray("Releases")]
    [XmlArrayItem("Release", typeof(OsRelease))]    
    public OsRelease[] Release { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeRelease() {
      return Release != null && Release.Length > 0;
    }
    public bool? CheckPassed { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }

    public void Diff(OSInfo other) {
      if (null == other) {
        return;
      }
      other.IsDiffMode = true;
      other.CheckPassed = !string.IsNullOrEmpty(Name) && 
        !string.IsNullOrEmpty(other.Name) && 
        Name == other.Name && 
        !string.IsNullOrEmpty(Architecture) && 
        !string.IsNullOrEmpty(other.Architecture) && 
        Architecture == other.Architecture;

      if (Release != null && other.Release != null) {
        foreach (var rel in Release) {
          foreach (var relOther in other.Release) {
            rel.Diff(relOther);
            if (other.CheckPassed.HasValue) {
              other.CheckPassed = other.CheckPassed.Value && relOther.CheckPassed;
            } else {
              other.CheckPassed = relOther.CheckPassed;
            }
          }
        }
      }
    }    
  }
}
