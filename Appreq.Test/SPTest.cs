using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.Win32;
using System;

namespace Appreq.Test {
  [TestFixture]
  public class SPTest {
    [Test]
    public void SPTest1() {
      var sp = new SystemProfiler();
      var os = sp.GetOS();
      Assert.IsNotNull(os);
      Assert.Greater(os.Count, 0);
      foreach (var o in os) {
        Assert.IsNotNullOrEmpty(o.Name);
        Assert.IsNotEmpty(o.Release);
      }
    }

    [Test]
    public void SerializeTest() {
      var profiler = new SystemProfiler();
      var profile = profiler.GetData();
      var xs = new XmlSerializer(typeof(Profile));
      var sw = new StringWriter();
      xs.Serialize(sw, profile);
      using(var sr = new StringReader(sw.ToString())) {
        var profile2 = (Profile) xs.Deserialize(sr);
        Assert.IsNotNull(profile2);
        Assert.IsNotNull(profile2.Environment);
        Assert.IsNotNull(profile2.Environment.Software);
        Assert.IsNotNull(profile2.Environment.Hardware);
        Assert.IsNotEmpty(profile2.Environment.Hardware.Disk.Disks);
        Assert.IsNotEmpty(profile2.Environment.Software.OS.Versions);
        Assert.AreEqual(profile.Environment.Software.OS.Versions.Length, profile2.Environment.Software.OS.Versions.Length);
        Assert.AreEqual(profile.Environment.Software.OS.Versions[0].Name, profile2.Environment.Software.OS.Versions[0].Name);
        Assert.AreEqual(profile.Environment.Hardware.Disk.Disks.Length, profile2.Environment.Hardware.Disk.Disks.Length);
      }
    }    
  }
}
