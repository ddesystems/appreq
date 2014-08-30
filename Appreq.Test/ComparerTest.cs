using System;
using NUnit.Framework;

namespace Appreq.Test {
  [TestFixture]
  public class ComparerTest {
    [Test]
    public void CompareRamTest1() {
      var actual = new RAMInfo {
        FreePhysicalMemory = 10,
        FreeVirtualMemory = 11,
        TotalVirtualMemorySize = 12,
        TotalVisibleMemorySize = 13
      };
      var expected = new RAMInfo {
        FreePhysicalMemory = 9,
        FreeVirtualMemory = 10,
        TotalVirtualMemorySize = 12,
        TotalVisibleMemorySize = 14
      };
      var diff = expected.Diff(actual);
      Console.WriteLine(string.Format("expected={0},actual={1}", expected.TotalVisibleMemorySize, diff.TotalVisibleMemorySize));
      Assert.Greater(expected.TotalVisibleMemorySize, diff.TotalVisibleMemorySize);
    }
  }
}
