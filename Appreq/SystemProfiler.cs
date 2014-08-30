using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management;

namespace Appreq {
  public class SystemProfiler {
    public const string NA = "N/A";

    public List<Disk> GetDisks() {
      var ret = new List<Disk>();
      foreach (var drive in DriveInfo.GetDrives()) {
        var disk = new Disk();
        try {
          disk.Name = drive.Name;
          disk.VolumeLabel = drive.Name;
          disk.PercentFreeSpace = drive.TotalFreeSpace / drive.TotalSize * 100;
          disk.TotalSize = drive.TotalSize;
          disk.AvailableFreeSpace = drive.AvailableFreeSpace;
          disk.DriveType = drive.DriveType.ToString();
          ret.Add(disk);
        } catch {
          continue;
        }
      }
      return ret;
    }

    public List<OSInfo> GetOS() {
      var ret = new List<OSInfo>();
      var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
      var _os = new OSInfo();
      var release = new List<OsRelease>();
      _os.Release = new OsRelease[] {};
      foreach (var os in searcher.Get()) {
        _os.Name = (string) (os["Caption"] ?? NA);
        _os.Architecture = (string) (os["OSArchitecture"] ?? NA);
        var sp = os["ServicePackMajorVersion"];
        sp = sp ?? NA;
        release.Add(new OsRelease {
          Name = (string) (os["Version"] ?? NA),
          ServicePack = sp.ToString()
        });
      }
      _os.Release = release.ToArray();
      ret.Add(_os);
      return ret.Count > 0 ? ret : null;
    }

    public JavaInfo GetJava() {
      var java = new JavaInfo {
        JavaHome = GetJavaHome()
      };
      return java;
    }

    public string GetJavaHome() {
      var environmentPath = System.Environment.GetEnvironmentVariable("JAVA_HOME");
      if (!string.IsNullOrEmpty(environmentPath)) {
        return environmentPath;
      }
      var javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
      using (var rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey)) {
        var currentVersion = rk.GetValue("CurrentVersion").ToString();
        using (var key = rk.OpenSubKey(currentVersion)) {
          return key.GetValue("JavaHome").ToString();
        }
      }
    }

    public List<CPUInfo> GetCPU() {
      var searcher = new ManagementObjectSearcher("select maxclockspeed, datawidth, name, manufacturer FROM Win32_Processor");
      var ret = new List<CPUInfo>();
      foreach (var cpu in searcher.Get()) {
        ret.Add(new CPUInfo {
          Manufacturer = cpu["manufacturer"].ToString() ?? NA,
          Name = cpu["name"].ToString() ?? NA,
          Datawidth = cpu["datawidth"].ToString() ?? NA,
          Maxclockspeed = cpu["maxclockspeed"].ToString() ?? NA
        });        
      }
      return ret;
    }

    public RAMInfo GetRAM() {
      var connection = new ConnectionOptions();
      connection.Impersonation = ImpersonationLevel.Impersonate;
      var scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
      scope.Connect();
      var query = new ObjectQuery("select * from Win32_OperatingSystem");
      var searcher = new ManagementObjectSearcher(scope, query);
      var ret = new List<RAMInfo>();
      var col = searcher.Get().GetEnumerator();
      col.MoveNext();
      var ram = col.Current;
      return new RAMInfo {
        TotalVisibleMemorySize = int.Parse(ram["TotalVisibleMemorySize"].ToString() ?? "-1"),
        FreePhysicalMemory = int.Parse(ram["FreePhysicalMemory"].ToString() ?? "-1"),
        TotalVirtualMemorySize = int.Parse(ram["TotalVirtualMemorySize"].ToString() ?? "-1"),
        FreeVirtualMemory = int.Parse(ram["FreeVirtualMemory"].ToString() ?? "-1")
      };      
    }

    public Profile GetData() {
      var app = new Profile {
        Environment = new Env {
          Hardware = new Hardware(),
          Software = new Software()
        }
      };
      try {
        app.Environment.Hardware.CPU = GetCPU().ToArray();
      } catch (Exception e) {
        throw new Exception("Failed to retrieve CPU info", e);
      }
      try {
        app.Environment.Hardware.Disks = GetDisks().ToArray();
      } catch (Exception e) {
        throw new Exception("Failed to retrieve HDD info", e);
      }
      try {
        app.Environment.Hardware.RAM = GetRAM();
      } catch (Exception e) {
        throw new Exception("Failed to retrieve RAM info", e);
      }
      try {
        app.Environment.Software.OS = GetOS().ToArray();
      } catch (Exception e) {
        throw new Exception("Failed to retrieve OS info", e);
      }
      return app;
    }
  }
}
