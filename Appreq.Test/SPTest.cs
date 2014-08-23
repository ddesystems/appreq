using System;
using System.Collections.Generic;
using System.Text;
using Appreq;
using NUnit.Framework;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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
      var app = profiler.GetData();
      var xs = new XmlSerializer(typeof(Appl));
      var sw = new StringWriter();
      xs.Serialize(sw, app);
      using(var sr = new StringReader(sw.ToString())) {
        var app2 = (Appl) xs.Deserialize(sr);
        Assert.IsNotNull(app2);
        Assert.IsNotNull(app2.Environment);
        Assert.IsNotNull(app2.Environment.Software);
        Assert.IsNotNull(app2.Environment.Hardware);
        Assert.IsNotEmpty(app2.Environment.Hardware.Disks);
        Assert.IsNotEmpty(app2.Environment.Software.OS);
        Assert.AreEqual(app.Environment.Software.OS.Length, app2.Environment.Software.OS.Length);
        Assert.AreEqual(app.Environment.Software.OS[0].Name, app2.Environment.Software.OS[0].Name);
        Assert.AreEqual(app.Environment.Hardware.Disks.Length, app2.Environment.Hardware.Disks.Length);
      }
    }
  }
}
