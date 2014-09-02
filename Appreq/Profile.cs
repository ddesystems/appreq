using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class Profile: IDiff<Profile> {    
    public App App { get; set; }    
    public Env Environment { get; set; }
    [XmlArray("Dependencies")]
    public App[] Dependencies { get; set; }

    public Profile Diff(Profile other) {
      return new Profile {
        App = new App(),
        Environment = Environment.Diff(other.Environment),
        Dependencies = new App[] { }
      };
    }
  }
}
