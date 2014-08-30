using System;
using System.Xml;
using System.Xml.Serialization;

namespace Appreq {
  public class Hardware {
    [XmlArray("Disks", IsNullable=true)]
    public Disk[] Disks { get; set; }
    [XmlArray("CPU", IsNullable = true)]
    public CPUInfo[] CPU { get; set; }
    [XmlElement(IsNullable=true)]
    public RAMInfo RAM { get; set; }
  }
}