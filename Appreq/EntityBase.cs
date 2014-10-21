using System.Xml.Serialization;

namespace Appreq {
  public abstract class EntityBase {
    public bool? CheckPassed { get; set; }
    public bool ShouldSerializeCheckPassed() { return IsDiffMode; }
    [XmlIgnore]
    public bool IsDiffMode { get; set; }
  }
}
