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
      actual.Diff(expected);
      Assert.IsTrue(expected.IsDiffMode);
      Assert.IsFalse(actual.CheckPassed.GetValueOrDefault());
    }
  }
}
