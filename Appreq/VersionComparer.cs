using System;
using System.Collections.Generic;
using System.Text;

namespace Appreq {
  public class VersionComparer {
    public static bool CompareMinorMajor(string version1, string version2) {
      // extract minor and major versions
        var split1 = version1.Split('.');
        var split2 = version2.Split('.');
        if (split1.Length >= 2 && split2.Length >= 2) {
          // compare maj/min versions
          return split1[0] == split2[0] && split1[1] == split2[1];
        } else {
          return false;
        }
    }
  }
}
