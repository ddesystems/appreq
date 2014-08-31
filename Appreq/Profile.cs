using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Profile: IDiff<Profile> {
    [XmlArray("Apps", IsNullable=true)]
    public App[] Apps { get; set; }
    [XmlElement("Environment", IsNullable=true)]
    public Env Environment { get; set; }

    public Profile Diff(Profile other) {
      return new Profile {
        Apps = new App[] {},
        Environment = Environment.Diff(other.Environment)
      };
    }
  }
}
