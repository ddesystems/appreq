using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class AppVersion {    
    public string Version { get; set; }
    public string PatchVersion  { get; set; }
    public string FrameworkVersion { get; set; }
    public string DLLVersion { get; set; }
  }
}
