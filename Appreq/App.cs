using System;
using System.Xml.Serialization;

namespace Appreq {
  [Serializable]
  public class App {
    public string Id { get; set; }
    public string IdDesc { get; set; }
    public string LongDesc { get; set; }
    public AppVersion Version { get; set; }
  }
}
