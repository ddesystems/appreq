using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Appreq
{ 
    [XmlElement("Appl")]
    public class Appl
    {         
        [XmlAttribute("Environment")]
        public Environment Environment { get; set; }
    }

    [XmlElement("Environment")]
    public class Environment
    {
        [XmlAttribute("SW")]
        public Software Software { get; set; }

        [XmlAttribute("HW")]
        public Hardware Hardware { get; set; }
    }

    [XmlElement("SW")]
    public class Software
    {
        [XmlArray("OSs")]
        public OS OS { get; set; }        
    }

    [XmlElement("OS")]
    public class OS
    {
        public string Name { get; set; }
        [XmlArray("Releases")]
        public OsRelease[] Release { get; set; }
    }

    [XmlElement("Release")]
    public class OsRelease
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("ServicePack")]
        public string ServicePack { get; set; }
    }
}
