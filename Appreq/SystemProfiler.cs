using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using Microsoft.Win32;

namespace Appreq {
  // Get Installed Application information
  // Id and name
  // version
  // Get Software requirement
  // Os
  // .Net framework
  // IIS
  // java Framework     
  // Browser
  // database
  // Get Hardware requirement
  // Processor (speed ?)     
  // Disk
  // RAM Memory
  // Get Network requirement
  // Protocol
  // Which port is opened ?     
  // Application Dependency
  // Id and name
  // version
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

    public List<JavaInfo> GetJava() {
      var ret = new List<JavaInfo>();
      var hklm = Registry.LocalMachine;
      var javaKey = hklm.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment");
      foreach (var key in javaKey.GetSubKeyNames()) {
        var sk = javaKey.OpenSubKey(key);
        var ji = new JavaInfo();
        ji.CurrentVersion = key;
        ji.Security = "yes";
        ji.JavaHome = (string) sk.GetValue("JavaHome") ?? sk.GetValue("JavaHome").ToString();
        ret.Add(ji);
      }
      
      return ret.Count > 0 ? ret : null;
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
          else if (p.Name == "DataWidth")     { cpuInfo.Datawidth = (UInt16) p.Value; }
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

    private static string GetIEVersion() {
      var key = Registry.LocalMachine;
      var ieKey = key.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer");
      if (null == ieKey) {
        return string.Empty;
      }
      return (string) (ieKey.GetValue("svcVersion") ?? ieKey.GetValue("Version"));
    }

    private static string GetFirefoxVersion() {
      string wowNode = string.Empty;
      if (Is64BitOperatingSystem()) wowNode = @"Wow6432Node\";
      RegistryKey regKey = Registry.LocalMachine;
      var ffKey = regKey.OpenSubKey(string.Format(@"Software\{0}Mozilla\Mozilla Firefox", wowNode));
      if(null == ffKey) {
        return string.Empty;
      }
      return (string) ffKey.GetValue("CurrentVersion");
    }

    public static bool Is64BitOperatingSystem() {
      return !Environment.GetFolderPath(Environment.SpecialFolder.System).ToUpper().Contains("SYSTEM32");       
    }

    private static string GetChromeVersion() {
      var wowNode = string.Empty;
      if(Is64BitOperatingSystem()) wowNode = @"Wow6432Node\";

      var regKey = Registry.LocalMachine;
      var keyPath = regKey.OpenSubKey(@"Software\" + wowNode + @"Google\Update\Clients");

      if (keyPath == null) {
        regKey = Registry.CurrentUser;
        keyPath = regKey.OpenSubKey(@"Software\" + wowNode + @"Google\Update\Clients");
      }

      if (keyPath == null) {
        regKey = Registry.LocalMachine;
        keyPath = regKey.OpenSubKey(@"Software\Google\Update\Clients");
      }

      if (keyPath == null) {
        regKey = Registry.CurrentUser;
        keyPath = regKey.OpenSubKey(@"Software\Google\Update\Clients");
      }

      if (keyPath != null) {
        var subKeys = keyPath.GetSubKeyNames();
        foreach (string subKey in subKeys) {
          object value = keyPath.OpenSubKey(subKey).GetValue("name");
          bool found = false;
          if (value != null)
            found = value.ToString().Equals("Google Chrome", StringComparison.InvariantCultureIgnoreCase);
          if (found) {
            return (string) keyPath.OpenSubKey(subKey).GetValue("pv");
          }
        }
      }
      return string.Empty;
    }

    public List<Browser> GetBrowser() {
      // Check if your using x64 system first if return is null your on a x86 system.
      var wowNode = string.Empty;
      if (Is64BitOperatingSystem()) wowNode = @"Wow6432Node\";
      var browserKeys = Registry.LocalMachine.OpenSubKey(string.Format(@"SOFTWARE\{0}Clients\StartMenuInternet", wowNode));

      // Loop through all the subkeys for the information you want then display it on the console.
      var ret = new List<Browser>();
      foreach (var browser in browserKeys.GetSubKeyNames()) {
        var b = new Browser {
          Name = (string) browserKeys.OpenSubKey(browser).GetValue(null)          
        };
        if (b.Name.Contains("Internet Explorer")) {
          b.Version = GetIEVersion();
        } else if (b.Name.Contains("Firefox")) {
          b.Version = GetFirefoxVersion();
        } else if (b.Name.Contains("Chrome")) {
          b.Version = GetChromeVersion();
        }
        ret.Add(b);
      }
      return ret;
    }

    public List<NetFrameworkVersion> GetNetFramework() {
      RegistryKey ndpKey = null;
      List<NetFrameworkVersion> ret = null;
      try {
        ret = new List<NetFrameworkVersion>();
        ndpKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\");
        foreach (var versionKeyName in ndpKey.GetSubKeyNames()) {
          if (!versionKeyName.StartsWith("v")) continue;
          var versionKey = ndpKey.OpenSubKey(versionKeyName);
          if(versionKey.ValueCount <= 1) {
            versionKey = versionKey.OpenSubKey("Client");
          }
          System.Diagnostics.Debug.WriteLine(versionKey.ToString());
          ret.Add(new NetFrameworkVersion {
            Name = versionKey.GetValue("Version", "").ToString(),
            SP = versionKey.GetValue("SP", "").ToString(),
            Install = versionKey.GetValue("Install", "").ToString()
          });
          versionKey.Close();
        }
      } finally {
        if (null != ndpKey) {
          ndpKey.Close();
        }
      }
      return ret.Count > 0 ? ret : null;
    }
    public Profile GetData() {
      var app = new Profile {
        App = GetApps()[0],
        Dependencies = GetApps(),
        Environment = new Env {
          Hardware = new Hardware(),
          Software = new Software(),          
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
      try {
        app.Environment.Software.Browsers = new BrowserInfo();
        app.Environment.Software.Browsers.Browser = GetBrowser().ToArray();
      } catch (Exception e) {
        throw new Exception("Failed to retrieve Browser info", e);
      }
      try {
        app.Environment.Software.NetFramework = new NetFramework();
        app.Environment.Software.NetFramework.Versions = GetNetFramework().ToArray();
      } catch (Exception e) {
        throw new Exception("Failed to retrieve NetFramework info", e);
      }
      try {
        app.Environment.Software.JavaFramework = new JavaFramework();
        app.Environment.Software.JavaFramework.Versions = GetJava().ToArray();
      } catch (Exception e) {
        throw new Exception("Failed to retrieve Java info", e);
      }
      return app;
    }
  }
}
