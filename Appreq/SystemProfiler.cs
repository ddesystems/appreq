using System;
using System.Collections.Generic;
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
      var searcher = new ManagementObjectSearcher(
        @"select
            Caption, 
            OSArchitecture, 
            ServicePackMajorVersion,
            Version 
          from 
            Win32_OperatingSystem");
      var os = new OSInfo();
      var release = new List<OsRelease>();
      os.Release = new OsRelease[] {};
      foreach (var o in searcher.Get()) {
        var osRelease = new OsRelease();
        foreach (var p in o.Properties) {
               if(p.Name == "Caption")                 { os.Name = p.Value.ToString(); } 
          else if(p.Name == "OSArchitecture")          { os.Architecture = p.Value.ToString(); }
          else if(p.Name == "ServicePackMajorVersion") { osRelease.ServicePack = p.Value.ToString(); }
          else if(p.Name == "Version")                 { osRelease.Name = p.Value.ToString(); }
        }
        release.Add(osRelease);
        os.Release = release.ToArray();
      }
      ret.Add(os);
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
      var searcher = new ManagementObjectSearcher(
        @"select
            MaxClockSpeed,
            DataWidth,
            Name,
            Manufacturer
          from
            Win32_Processor");
      var ret = new List<CPUInfo>();
      foreach (var cpu in searcher.Get()) {
        var cpuInfo = new CPUInfo();
        foreach (var p in cpu.Properties) {
               if (p.Name == "Manufacturer")  { cpuInfo.Manufacturer = p.Value.ToString(); }
          else if (p.Name == "Name")          { cpuInfo.Name = p.Value.ToString(); }
          else if (p.Name == "DataWidth")     { cpuInfo.Datawidth = p.Value.ToString(); }
          else if (p.Name == "MaxClockSpeed") { cpuInfo.Maxclockspeed = p.Value.ToString(); }
        }
        ret.Add(cpuInfo);
      }
      return ret;
    }

    public RAMInfo GetRAM() {
      var connection = new ConnectionOptions();
      connection.Impersonation = ImpersonationLevel.Impersonate;
      var scope = new ManagementScope(@"\\.\root\CIMV2", connection);
      scope.Connect();
      var query = new ObjectQuery(
        @"select 
            TotalVisibleMemorySize,
            FreePhysicalMemory,
            TotalVirtualMemorySize,
            FreeVirtualMemory
          from
            Win32_OperatingSystem");
      var searcher = new ManagementObjectSearcher(scope, query);
      var ret = new RAMInfo();
      foreach (var ram in searcher.Get()) {
        foreach(var p in ram.Properties) {
               if(p.Name == "TotalVisibleMemorySize") { ret.TotalVisibleMemorySize = (UInt64) p.Value; }
          else if(p.Name == "FreePhysicalMemory")     { ret.FreePhysicalMemory = (UInt64) p.Value; }
          else if(p.Name == "TotalVirtualMemorySize") { ret.TotalVirtualMemorySize = (UInt64) p.Value; }
          else if(p.Name == "FreeVirtualMemory")      { ret.FreeVirtualMemory = (UInt64) p.Value; }
        }
      }
      return ret;
    }

    public App[] GetApps() {
      //TODO: pull the info from the registry
      return new[] { 
        new App {
          Id = NA,
          IdDesc = NA,
          LongDesc = NA,
          Version = new AppVersion {
            DLLVersion = NA,
            FrameworkVersion = NA,
            PatchVersion = NA,
            Version = NA
          }
        }
      };
    }

    public Profile GetData() {
      var app = new Profile {
        Apps = GetApps(),
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
