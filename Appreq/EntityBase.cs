using System.Xml.Serialization;
using System;

namespace Appreq {
  [Serializable]
  public abstract class EntityBase {
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }
  }
}
