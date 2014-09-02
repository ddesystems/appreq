using System;
using System.Collections.Generic;
using System.Text;

namespace Appreq {
  public class NetFrameworkVersion: IDiff<NetFrameworkVersion> {
    public string Name { get; set; }
    public string Install { get; set; }
    public string SP { get; set; }
    public string WCFEnable { get; set; }

    public NetFrameworkVersion Diff(NetFrameworkVersion other) {
      return null;
    }
  }
}
