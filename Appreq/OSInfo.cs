using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Appreq {
  [Serializable]
  public class OSInfo: IDiff<OSInfo> {
    public string Name { get; set; }
    public string Architecture { get; set; }
    [XmlArray("Releases")]
    [XmlArrayItem("Release", typeof(OsRelease))]    
    public OsRelease[] Release { get; set; }
    public bool ShouldSerializeRelease() {
      return Release != null && Release.Length > 0;
    }

    public OSInfo Diff(OSInfo other) {
      var os = new OSInfo {
        Name = Name != other.Name ? other.Name : null,
        Architecture = Architecture != other.Architecture ? other.Architecture : null
      };
      if (Release != null && other.Release != null) {
        var relDiff = new List<OsRelease>();
        var releaseFound = false;
        foreach (var rel in Release) {
          if (releaseFound) break;
          foreach (var relOther in other.Release) {
            var diff = rel.Diff(relOther);
            if (diff != null) {
              relDiff.Add(diff);
            } else {
              releaseFound = true;
              break;
            }
          }
        }
        os.Release = releaseFound ? null : os.Release = relDiff.ToArray();
      }
      if (os.Name == null && os.Architecture == null && (os.Release == null || os.Release.Length == 0)) {
        return null;
      } else {
        return os;
      }
    }
  }
}
