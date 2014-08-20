using System.Xml.Serialization;

namespace Appreq
{
    public class Hardware
    {
        [XmlArray("Disks")]
        public Disk[] Disks { get; set; }
    }
}
