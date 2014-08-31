using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Software: IDiff<Software> {
    [XmlArray("OSs", IsNullable = true)]
    [XmlArrayItem("OS", typeof(OSInfo))]
    public OSInfo[] OS { get; set; }

    public Software Diff(Software other) {
      var sw = new Software();
      var osDiff = new List<OSInfo>();
      foreach (var os in OS) {
        foreach (var osOther in other.OS) {
          osDiff.Add(os.Diff(osOther));
        }
      }      
      return new Software {
        OS = osDiff.ToArray()
      };
    }
  }
}
