using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class OsRelease {
    public string Name { get; set; }
    public string ServicePack { get; set; }
  }
}
