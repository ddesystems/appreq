using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class NetFramework: IDiff<NetFramework> {
    [XmlArray("Versions")]
    [XmlArrayItem("Version", typeof(NetFrameworkVersion))]
    public NetFrameworkVersion[] Versions { get; set; }
    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeVersions() {
      return Versions != null && Versions.Length > 0;
    }

    public NetFramework Diff(NetFramework other) {
      return null;
    }
  }
}
