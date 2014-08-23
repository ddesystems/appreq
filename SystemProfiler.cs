using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management;

namespace Appreq {
  public class SystemProfiler {
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

    public List<OS> GetOS() {
      var ret = new List<OS>();
      //try {
        var searcher = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
        var _os = new OS();
        var release = new List<OsRelease>();
        _os.Release = new OsRelease[] {};
        foreach (var os in searcher.Get()) {
          // debug
          //foreach (var p in os.Properties) {
          //  Console.WriteLine(string.Format("{0}={1}", p.Name, p.Value));
          //}
          // debug.end
          _os.Name = (string) (os["Caption"] ?? "N/A");
          _os.Architecture = (string) (os["OSArchitecture"] ?? "N/A");
          var sp = os["ServicePackMajorVersion"];
          sp = sp ?? "N/A";
          release.Add(new OsRelease {
            Name = (string) (os["Version"] ?? "N/A"),
            ServicePack = sp.ToString()
          });
        }
        _os.Release = release.ToArray();
        ret.Add(_os);
      //} catch (Exception e) {
      //  //TODO: log error
      //}
      return ret.Count > 0 ? ret : null;
    }

    public Appl GetData() {
      return new Appl() {
        Environment = new Environment {
          Hardware = new Hardware {
            Disks = GetDisks().ToArray()
          },
          Software = new Software {
            OS = GetOS().ToArray()
          }
        }
      };
    }
  }
}
