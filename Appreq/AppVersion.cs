using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class AppVersion {
    [XmlElement(IsNullable=true)]
    public string Version { get; set; }
    [XmlElement(IsNullable = true)]
    public string PatchVersion  { get; set; }
    [XmlElement(IsNullable = true)]
    public string FrameworkVersion { get; set; }
    [XmlElement(IsNullable = true)]
    public string DLLVersion { get; set; }
  }
}
