using System;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  public class Hardware {
    [XmlArray("Disks")]
    public Disk[] Disks { get; set; }
    [XmlArray("CPU")]
    public CPUInfo[] CPU { get; set; }
    public RAMInfo RAM { get; set; }
  }
}