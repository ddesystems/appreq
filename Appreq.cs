using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Appreq
{ 
    [XmlType("Appl")]
    public class Appl
    {         
        [XmlAttribute("Environment")]
        public Environment Environment { get; set; }
    }

    [XmlType("Environment")]
    public class Environment
    {
        [XmlAttribute("SW")]
        public Software Software { get; set; }

        [XmlAttribute("HW")]
        public Hardware Hardware { get; set; }
    }

    [XmlType("SW")]
    public class Software
    {
        [XmlArray("OSs")]
        public OS OS { get; set; }        
    }

    [XmlType("OS")]
    public class OS
    {
        public string Name { get; set; }
        [XmlArray("Releases")]
        public OsRelease[] Release { get; set; }
    }

    [XmlType("Release")]
    public class OsRelease
    {
        [XmlElement("name")]
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
