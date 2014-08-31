using System;
using System.Collections.Generic;
using System.Text;

namespace Appreq {
  [Serializable]
  public class Browser: IDiff<Browser> {
    public string Name { get; set; }
    public string Version { get; set; }
    //public string CompatibilityMode { get; set; }

    public Browser Diff(Browser other) {
      if(null == other) {
        return null;
      }
      return new Browser {
        Name = !string.IsNullOrEmpty(other.Name) && Name != other.Name ? other.Name : null,
        //CompatibilityMode = !string.IsNullOrEmpty(other.CompatibilityMode) && CompatibilityMode != other.CompatibilityMode ? other.CompatibilityMode : null,
        Version = !string.IsNullOrEmpty(other.Version) && Version != other.Version ? other.Version : null
      };
    }
  }
}
