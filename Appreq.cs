using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Appreq
{ 
    [XmlType("Appl")]
    public class Appl
    {
        [XmlElement("Environment")]
        public Environment Environment { get; set; }
    }

    [XmlType("Environment")]
    public class Environment
    {
        [XmlElement("SW")]
        public Software Software { get; set; }

        [XmlElement("HW")]
        public Hardware Hardware { get; set; }
    }

    [XmlType("SW")]
    public class Software {
      [XmlArray("OSs", IsNullable = true)]
      [XmlArrayItem("OS", typeof(OS))]
      public OS[] OS { get; set; }        
    }

    [XmlType("OS")]
    public class OS
    {
      [XmlElement("Name", IsNullable = true)]
      public string Name { get; set; }
      [XmlElement("Architecture", IsNullable = true)]
      public string Architecture { get; set; }
      [XmlArray("Releases", IsNullable = true)]
      [XmlArrayItem("Release", typeof(OsRelease))]
      public OsRelease[] Release { get; set; }
    }

    [XmlType("Release")]
    public class OsRelease
    {
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("ServicePack")]
        public string ServicePack { get; set; }
    }

    [XmlType("Disk")]
    public class Disk
    {
        public string Name { get; set; }
        public string VolumeLabel { get; set; }
        public double TotalSize { get; set; }
        public double AvailableFreeSpace { get; set; }
        public float PercentFreeSpace { get; set; }
        public string DriveType { get; set; }
    }
}
