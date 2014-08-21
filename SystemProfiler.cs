using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

    public Appl GetData() {
      return new Appl() {
        Environment = new Environment {
          Hardware = new Hardware {
            Disks = GetDisks().ToArray()
          },
          Software = new Software {
            OS = new OS[] {
              new OS {
                Name = "test",
                Release = new[] {new OsRelease {
                  Name = "test-release",
                  ServicePack = "1"
                }
                }
              }
            }
          }
        }
      };
    }
  }
}
