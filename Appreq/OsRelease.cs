using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class OsRelease: IDiff<OsRelease> {
    public string Name { get; set; }
    public string ServicePack { get; set; }

    public OsRelease Diff(OsRelease other) {
      if (null == other) return null;
      var release = new OsRelease {
        Name = Name != other.Name ? other.Name : null,
        ServicePack = ServicePack != other.ServicePack ? other.ServicePack : null
      };
      if (null == release.Name && null == release.ServicePack) {
        return null;
      } else {
        return release;
      }
    }
  }
}
