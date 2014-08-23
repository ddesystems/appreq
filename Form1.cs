using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Xml.XPath;

namespace Appreq
{
    public partial class Form1 : Form
    {
        private XDocument doc;

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            populateTreeView();
            //generatedoc();
        }

        private void populateTreeView() {
          var profiler = new SystemProfiler();
          var app = profiler.GetData();
          var xs = new XmlSerializer(typeof(Appl));
          var sw = new StringWriter();          
          xs.Serialize(sw, app);          
        }

        private void generatedoc()
        {
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

            // -- Study this sample
            // XDocument doc = XDocument.Load(new StreamReader(Application.GetResourceStream(new Uri(@"\Data\XmlData.xml", UriKind.Relative)).Stream));
            // this.treeView.DataContext = doc;

            // ------------------------------------------
            // Test Routine to insert in LOGIC session
            // ------------------------------------------
            // XElement prova = Build_Element_OS();  // OK
            // prova.Save(Console.Out);

            // int pino = Get_SPVersion_OS();

            // string aaa = Get_Release_OS();
            // -------------------------------------------

            // -------------------------
            //  DEMO with prebuild XML
            // -------------------------
            // XmlGen();

            // --------------
            //  LOGIC
            // --------------
            XmlGather();

        }
      //TODO: mock, remove after downgrade to .net 2.0
      class XElement {
        private string p;
        private XElement xElement;
        private IEnumerable<Disk> iEnumerable;
        private XElement xElement_2;

        public XElement(string s, params XElement[] e) { }
        public XElement(string s, string s1) { }
        public XElement(string s, object o) { }        
        public XElement(params XElement[] e) { }


        public XElement(string p, XElement xElement, IEnumerable<Disk> iEnumerable, XElement xElement_2) {
          // TODO: Complete member initialization
          this.p = p;
          this.xElement = xElement;
          this.iEnumerable = iEnumerable;
          this.xElement_2 = xElement_2;
        }
      }

      class XDocument {
        private XElement xElement;

        public XDocument(XDeclaration d, params XElement[] e) { }
        public XDocument(XDocument d) { }

        public XDocument(XElement xElement) {
          // TODO: Complete member initialization
          this.xElement = xElement;
        }

        public XmlReader CreateReader() {
          throw new NotImplementedException();
        }
        public XDocument Load(XmlReader r) {
          throw new NotImplementedException();
        }
      }
      class XDeclaration {
        public XDeclaration(string s1, string s2, string s3) { }
      }


        private void XmlGather()
        {
            // SqlTestInfo(); --> See it can work better to gather SQL SERVER information... (lack of SP info..mmm)

            // Create Document

            doc = new XDocument(new XDeclaration("1.0", "utf-8", ""),
              // Add Root document (Don't complete at moment)
              new XElement("Appl",
                new XElement("Id", "01"),
                new XElement("IdDesc", "SM"),
                new XElement("LongDesc", "Saldi e Movimenti"),
                new XElement("Versions",
                     new XElement("Version",
                         new XElement("ApplVersion", "V. 3.0.1"),
                         new XElement("PatchVersion", "V. 1.0.0.1"),
                         new XElement("FrameworkVersion", "V. 7.3.2.0"),
                         new XElement("DLLVersion", "V. 4")),
                     new XElement("Version",
                         new XElement("ApplVersion", "V. 3.0.1"),
                         new XElement("PatchVersion", "V. 1.0.0.1"),
                         new XElement("FrameworkVersion", "V. 7.3.2.0"),
                         new XElement("DLLVersion", "V. 4"))),

                new XElement("Environment",
                     new XElement("SW",
                         new XElement("OSs",                                    
                             Get_OS(),
                             Get_NetFramework(),
                             Get_IIS(),
                             Get_current_javaJVM(),
                             Get_Browsers(),
                             Get_DataBases())),
                     new XElement("HW",
                          Get_Processor(),
                          //TODO: deserialize disks
                          Get_Disks(),
                          Get_RAM()),
                     new XElement("Network",                          
                          Get_Network(),
                          Get_Ports()
                ))));



            // Load in a TreeView (before Load a XmlDataDocument object from Xdocument Object)
            XmlDataDocument xmldoc = new XmlDataDocument();
            TreeNode tNode;
            XmlNode xmlnode;

            using (var xmlReader = doc.CreateReader())
            {
                xmldoc.Load(xmlReader);
            }

            xmlnode = xmldoc.ChildNodes[0];

            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));           

            tNode = treeView1.Nodes[0];
            AddNode(xmlnode, tNode);

            // Expand Treeview
            treeView1.Nodes[0].Expand();

            // Update Status Bar
            ststatus.Text = "Caricamento Effettuato";
        }

        private XElement Get_OS()
        {
            // --- Example

            //new XElement("OS",
            //   new XElement("name", "Windows 2008"),
            //   new XElement("Releases",              // Loop                        
            //      new XElement("Release",
            //          new XElement("name", "R1"),
            //          new XElement("ServicePack", "SP 1"),             // Loop                      
            //          new XElement("ServicePack", "SP 2"),
            //          new XElement("ServicePack", "SP 3")),

            XElement xelem =
              new XElement("OS",
                  new XElement("name", inquiry_OS()),
                  new XElement("Releases",
                      new XElement("Release",
                          new XElement("name", Get_Release_OS()),
                          new XElement("ServicePack", Get_SPVersion_OS()))));

            return xelem;

        }

        private string inquiry_OS()
        {
          /*
            var name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
           */
          throw new NotImplementedException();
        }

        private string Get_Release_OS() {
            // Environment.OSVersion.ToString();  // Alternative don't like ... I prefere WMI interface as follow

            string Version;
            var wmi = new ManagementObjectSearcher("select * from Win32_OperatingSystem")
                .Get().GetEnumerator().Current;
                //.Cast<ManagementObject>()
                //.First();
          
            Version = (string)wmi["Version"];

            //OS.Name = ((string)wmi["Caption"]).Trim();
            //OS.Version = (string)wmi["Version"];
            //OS.MaxProcessCount = (uint)wmi["MaxNumberOfProcesses"];
            //OS.MaxProcessRAM = (ulong)wmi["MaxProcessMemorySize"];
            //OS.Architecture = (string)wmi["OSArchitecture"];
            //OS.SerialNumber = (string)wmi["SerialNumber"];
            //OS.Build = ((string)wmi["BuildNumber"]).ToUint();

            //var cpu =
            //    new ManagementObjectSearcher("select * from Win32_Processor")
            //    .Get()
            //    .Cast<ManagementObject>()
            //    .First();

            //CPU.ID = (string)cpu["ProcessorId"];
            //CPU.Socket = (string)cpu["SocketDesignation"];
            //CPU.Name = (string)cpu["Name"];
            //CPU.Description = (string)cpu["Caption"];
            //CPU.AddressWidth = (ushort)cpu["AddressWidth"];
            //CPU.DataWidth = (ushort)cpu["DataWidth"];
            //CPU.Architecture = (CPU.CpuArchitecture)(ushort)cpu["Architecture"];
            //CPU.SpeedMHz = (uint)cpu["MaxClockSpeed"];
            //CPU.BusSpeedMHz = (uint)cpu["ExtClock"];
            //CPU.L2Cache = (uint)cpu["L2CacheSize"] * (ulong)1024;
            //CPU.L3Cache = (uint)cpu["L3CacheSize"] * (ulong)1024;
            //CPU.Cores = (uint)cpu["NumberOfCores"];
            //CPU.Threads = (uint)cpu["NumberOfLogicalProcessors"];

            //CPU.Name =
            //   CPU.Name
            //   .Replace("(TM)", "™")
            //   .Replace("(tm)", "™")
            //   .Replace("(R)", "®")
            //   .Replace("(r)", "®")
            //   .Replace("(C)", "©")
            //   .Replace("(c)", "©")
            //   .Replace("    ", " ")
            //   .Replace("  ", " ");

            return Version;
        }

        private int Get_SPVersion_OS()
        {
            // Get last Service Pack Number (Use WMI)
            int iSPVersionMajor=0;

            SelectQuery query = new SelectQuery("Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get()) {
               iSPVersionMajor = int.Parse(mo["ServicePackMajorVersion"].ToString());
            }

            return iSPVersionMajor;
        }

        private XElement Get_NetFramework()
        {
            // What to obtain:             
                //new XElement("NetFramework",                                    
                //    new XElement("Versions",                          
                //         new XElement("Version", 
                //             new XElement("Name", "3.5"),
                //             new XElement("WCFEnable", "yes")),
                //         new XElement("Version", 
                //             new XElement("Name", "4.0"),
                //             new XElement("WCFEnable", "yes")),
                //         new XElement("Version", 
                //             new XElement("Name", "4.5"),
                //             new XElement("WCFEnable", "yes"))
            
            // --------->  First sample code (Using registry)  
            //string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            //using(Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            //{
            //   foreach(string subkey_name in key.GetSubKeyNames())
            //   {
            //       using(RegistryKey subkey = key.OpenSubKey(subkey_name))
            //         {
            //            Console.WriteLine(subkey.GetValue("DisplayName"));
            //         }
            //    }
            //}

            // ----
            // --------->  Second sample code (using registry)  (The choice)
            // ----
   
            XElement xelem =
                    new XElement("NetFramework",
                        new XElement("Versions"));

               //using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
            using (RegistryKey ndpKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))
               {
                foreach (string versionKeyName in ndpKey.GetSubKeyNames())
                {
                    if (versionKeyName.StartsWith("v"))
                    {

                        RegistryKey versionKey = ndpKey.OpenSubKey(versionKeyName);
                        string name = (string)versionKey.GetValue("Version", "");
                        string sp = versionKey.GetValue("SP", "").ToString();
                        string install = versionKey.GetValue("Install", "").ToString();
                        if (install == "") //no install info, ust be later
                            Console.WriteLine(versionKeyName + "  " + name);
                        else
                        {
                            if (sp != "" && install == "1")
                            {
                              /*
                                xelem.Element("Versions").Add(new XElement("Version", 
                                      new XElement("Name", name),
                                      new XElement("versionKeyName", versionKeyName),
                                      new XElement("sp", sp),
                                      new XElement("WCFEnable", "yes")));
                               */
                            }

                        }
                        if (name != "")
                        {
                            continue;
                        }
                        foreach (string subKeyName in versionKey.GetSubKeyNames())
                        {
                            RegistryKey subKey = versionKey.OpenSubKey(subKeyName);
                            name = (string)subKey.GetValue("Version", "");
                            if (name != "")
                                sp = subKey.GetValue("SP", "").ToString();
                            install = subKey.GetValue("Install", "").ToString();
                            if (install == "")                                   //no install info, must be later
                                Console.WriteLine(versionKeyName + "  " + name);
                            else
                            {
                              /*
                                if (sp != "" && install == "1")
                                {                                  
                                    xelem.Element("Versions").Add(new XElement("Version",
                                           new XElement("Name", name),
                                           new XElement("versionKeyName", versionKeyName),
                                           new XElement("sp", sp),
                                           new XElement("WCFEnable", "yes")));
                                   
                                }
                                else if (install == "1")
                                {
                                    xelem.Element("Versions").Add(new XElement("Version",
                                           new XElement("Name", name),
                                           new XElement("versionKeyName", subKeyName),
                                           new XElement("sp", "null"),
                                           new XElement("WCFEnable", "yes")));
                                }
                              */
                            }

                        }

                    }
                }
            }

            // ---------> Third sample code (using WMI)  

            //string[] NetFramework;

            //ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT name, version FROM Win32_Product where name like 'Microsoft .Net%'");
            
            //foreach (ManagementObject mo in mos.Get())
            //{
            //    NetFramework (index);
            //}

            //return NetFramework;

 
            return xelem;
        }

        private XElement Get_IIS()
        {
            int majorVersion;
            int minorVersion;
            XElement xelem;

            // What to obtain:             
                 //    new XElement("IIS",                                    
                 //        new XElement("Version",                          
                 //            new XElement("majorVersion", "6"),
                 //            new XElement("minorVersion", "7"),

            // Correct XML doc: <<ONLY one VErsion on Systems.. no many>>

            xelem = new XElement("IIS", new XElement("Version"));

            using (RegistryKey componentsKey =
                    Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\InetStp", false))
            {
                if (componentsKey != null)
                {
                    majorVersion = (int)componentsKey.GetValue("MajorVersion", -1);
                    minorVersion = (int)componentsKey.GetValue("MinorVersion", -1);
                    if (majorVersion != -1 && minorVersion != -1)
                    {
                      /*
                        xelem.Element("Version").Add(new XElement("MajorVersion", majorVersion),
                          new XElement("MinorVersion", minorVersion));
                       */ 
                    }
                }
                else
                {
                  /*
                   xelem.Element("Version").Add(new XElement("MajorVersion", "null"),
                      new XElement("MinorVersion", "null"));
                   */
                }
            }

            return xelem;
        }

        private XElement Get_current_javaJVM()
        {

            XElement xelem;

            // What to obtain:             
            //new XElement("JavaFramework",                                    
            //    new XElement("Versions",                          
            //         new XElement("Version",
            //             new XElement("Name", "6"),
            //             new XElement("Security", "yes")),
            //         new XElement("Version",
            //             new XElement("Name", "7"),
            //             new XElement("Security", "yes")),
            //    ................ 

            // Correct XML doc: <<ONLY current VErsion on Systems.. no other installed>>

            xelem = new XElement("JavaFramework", new XElement("Version"));

            RegistryKey rk = Registry.LocalMachine;
            RegistryKey subKey = rk.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment");

            if (subKey != null)
            {
               string currentVersion = subKey.GetValue("CurrentVersion").ToString();
               //xelem.Element("Version").Add(new XElement("CurrentVersion", currentVersion), new XElement("Security", "yes"));
            }
            else
            {
               //xelem.Element("Version").Add(new XElement("CurrentVersion", "null"), new XElement("Security", "null"));
            }

            return xelem;
        }

        private XElement Get_Browsers()
        {

            XElement xelem;

            // What to obtain:             
            //    new XElement("Browsers",                                    
            //        new XElement("Browser",                          
            //            new XElement("Name", "Internet Explorer"),
            //            new XElement("Version", "11"),
            //            new XElement("CompatibilityMode", "yes"),
            //        new XElement("Browser",                          
            //            new XElement("Name", "Chrome"),
            //            new XElement("Version", "7"),
            //            new XElement("CompatibilityMode", "yes"),

            xelem = new XElement("Browsers", "");

            //Check if your using x64 system first if return is null your on a x86 system.
            RegistryKey browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Clients\StartMenuInternet");
            if (browserKeys == null)
                browserKeys = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");

            // Lets get our keys!
            string[] browserNames = browserKeys.GetSubKeyNames();
        
            // Loop through all the subkeys for the information you want then display it on the console.
            for (int i = 0; i < browserNames.Length; i++)
            {
                RegistryKey browserKey = browserKeys.OpenSubKey(browserNames[i]);
                //xelem.Add(new XElement("Browser", new XElement("Name", (string)browserKey.GetValue(null)), new XElement("Version", "null"), new XElement("CompatibilityMode", "null")));

                // RegistryKey browserKeyPath = browserKey.OpenSubKey(@"shell\open\command");
                // browser.Path = (string)browserKeyPath.GetValue(null);
            }


            return xelem;
        }


        private XElement Get_DataBases()
        {

            XElement xelem;

            // What to obtain:             

            //   new XElement("Databases",
            //       new XElement("OnAppServer", "Yes"),    <---- TODO: Shift on the <app> as <DBOnAppServer>                                 
            //       new XElement("Database",
            //           new XElement("Name", "SQLSERVER 2008"),                         
            //           new XElement("Release", "R1"),
            //           new XElement("ServicePack", "SP 1"),
            //       new XElement("Database",
            //           new XElement("Name", "SQLSERVER 2012"),                         
            //           new XElement("Release", "R2"),
            //           new XElement("ServicePack", "SP 3")))),

            xelem = new XElement("Database", "");

            //SqlDataSourceEnumerator sqldatasourceenumerator1 = SqlDataSourceEnumerator.Instance;
            //DataTable datatable1 = sqldatasourceenumerator1.GetDataSources();
            
            //foreach (DataRow row in datatable1.Rows)
            //{
            //    xelem.Add(new XElement("Name", row["ServerName"]), 
            //              new XElement("ServerName",row["ServerName"]),          // Shift at top level of XML
            //              new XElement("InstanceName", row["InstanceName"]),
            //              new XElement("Version", row["Version"]));
            //}


            // Run a WQL query to return information about SKUNAME and SPLEVEL about installed instances
            // of the SQL Engine.
            
            // ServiceName

            IEnumerable<string> aaa = EnumCorrectWmiNameSpace();

            if (aaa == null)
            {
              /*
                xelem.Add(new XElement("Name", "null"),
                          new XElement("Release", "null"),
                          new XElement("ServicePack", "null"));
               * */
            }
            else
            {
                // Esempio
                // string serviceName, string wmiNamespace, string propertyName
                // string propertyValue = string.Empty;
                // string query = String.Format("select * from SqlServiceAdvancedProperty where SQLServiceType = 1 and PropertyName = '{0}' and ServiceName = '{1}'", propertyName, serviceName);
                // ManagementObjectSearcher getSql = new ManagementObjectSearcher(wmiNamespace, query);

                ManagementObjectSearcher getSql =
                    new ManagementObjectSearcher("root\\Microsoft\\SqlServer\\ComputerManagement",
                    "select * from SqlServiceAdvancedProperty  where SQLServiceType = 1");

                // If nothing is returned, SQL SERVER isn't installed.
                if (getSql.Get().Count == 0)
                {
                  /*
                    xelem.Add(new XElement("Name","null"),
                              new XElement("Release", "null"),         
                              new XElement("ServicePack", "null"));
                   */
                }
                else
                {
                    // If something is returned, verify it is the correct edition and SP level.
                    foreach (ManagementObject sqlEngine in getSql.Get())
                    {
                        switch (sqlEngine["PropertyName"].ToString())
                        {
                             case "SKUNAME":
                    //             xelem.Add(new XElement("Release", sqlEngine["PropertyStrValue"].ToString()));
                                 break;
                             
                             case "SPLEVEL":
                      //           xelem.Add(new XElement("ServicePack", sqlEngine["PropertyNumValue"].ToString()));
                                 break;
                         }
                     }
                }
            }
            return xelem;
        }

        public static IEnumerable<string> EnumCorrectWmiNameSpace()
        {
          /*
            const string WMI_NAMESPACE_TO_USE = @"root\Microsoft\sqlserver";
            try
            {
                ManagementClass nsClass =
                    new ManagementClass(
                        new ManagementScope(WMI_NAMESPACE_TO_USE),
                        new ManagementPath("__namespace"),
                        null);

                return nsClass
                    .GetInstances()
                    .Cast<ManagementObject>()
                    .Where(m => m["Name"].ToString().StartsWith("ComputerManagement"))
                    .Select(ns => WMI_NAMESPACE_TO_USE + "\\" + ns["Name"].ToString());
            }
            catch (ManagementException e)
            {
                // Console.WriteLine("Exception = " + e.Message);
            }

            return null;
           */
          throw new NotImplementedException();
        }
        
        private XElement Get_Processor()
        {

            XElement xelem;

            // What to obtain:             
            //        new XElement("Processor",                                      
            //            new XElement("Manufacturer", "Intel"),                       
            //            new XElement("Name", "Xeon"),                       
            //            new XElement("Datawidth", "xxxx"),                       
            //            new XElement("Maxclockspeed", "3.22")),     

            xelem = new XElement("Processor", "");

            ManagementObjectSearcher searcher =  new ManagementObjectSearcher("SELECT  maxclockspeed, datawidth, name, manufacturer FROM  Win32_Processor");
            ManagementObjectCollection objCol = searcher.Get();
            
            foreach (ManagementObject mgtObject in objCol)
            {

              /*
                xelem.Add(new XElement("Manufacturer", mgtObject["manufacturer"].ToString()),
                          new XElement("Name", mgtObject["name"].ToString()),
                          new XElement("Datawidth", mgtObject["datawidth"].ToString()),
                          new XElement("Maxclockspeed", mgtObject["maxclockspeed"].ToString()));
               */
            } 
         
            return xelem;
        }

        private IEnumerable<Disk> Get_Disks()
        {
            // loop through each drive in the system
            foreach (var drive in DriveInfo.GetDrives())
            {
                var disk = new Disk();
                try
                {
                    disk.Name = drive.Name;
                    disk.VolumeLabel = drive.Name;
                    disk.PercentFreeSpace = drive.TotalFreeSpace / drive.TotalSize * 100;
                    disk.TotalSize = drive.TotalSize;
                    disk.AvailableFreeSpace = drive.AvailableFreeSpace;
                    disk.DriveType = drive.DriveType.ToString();
                }
                catch
                {
                    continue;
                }
                yield return disk;
            }
        }

        private XElement Get_RAM()
        {

            XElement xelem;

            //    new XElement("Appl",                             <---- sheeft in appl tag                                      
            //    new XElement("DataBase",                         <---- sheeft in appl tag                                    
            //        new XElement("MinQuantityMB", "2000"),       <---- sheeft in appl tag                     
            //        new XElement("AdvQuantityMB", "4000")),      <---- sheeft in appl tag                          

            // What to obtain:             
            //   new XElement("RAM",                                      
            //       new XElement("TotalVisibleMemorySize", "8000")))),        
            //       new XElement("FreePhysicalMemory", "8000")))),        
            //       new XElement("TotalVirtualMemorySize", "8000")))),        
            //       new XElement("FreeVirtualMemory", "8000")))),        

            xelem = new XElement("RAM", "");

            ConnectionOptions connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Impersonate;

            ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
            scope.Connect();

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject queryObj in searcher.Get())
            {
              /*
                xelem.Add(new XElement("TotalVisibleMemorySize", queryObj["TotalVisibleMemorySize"].ToString()),
                          new XElement("FreePhysicalMemory", queryObj["FreePhysicalMemory"].ToString()),
                          new XElement("TotalVirtualMemorySize", queryObj["TotalVirtualMemorySize"].ToString()),
                          new XElement("FreeVirtualMemory", queryObj["FreeVirtualMemory"].ToString()));
               */
            } 

            return xelem;

            // Briefly with no 'connection' object
            //ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            //ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(wql);
            //ManagementObjectCollection results = searcher1.Get();

            // foreach (ManagementObject result in results)
            // ................ 
        
        }
        
        private XElement Get_Network()
        {

            XElement xelem;

            //
            // What to obtain:             
            //   new XElement("NICs",
            //       new XElement("NIC", "name of NIC"), 
            //           new XElement("name of attribute",                                   
            //               new XElement("Value", "value of property"                                    
            //           new XElement("name of attribute",                                   
            //               new XElement("Value", "value of property"                                    
            //           ..........................................                                   
            //               ..........................................                                    
            //   new XElement("Ports",                                   
            //       new XElement("Port", 
            //           new XElement("Number", "80")),
            //           new XElement("Status", "Open"),
            //       new XElement("Port", 
            //           new XElement("Number", "443")),
            //           new XElement("Status", "Open"))

            xelem = new XElement("NICs", "");

            ManagementObjectSearcher mos = null;
            
            // Filtering on the Manufacturer and PNPDeviceID not starting with "ROOT\"
            // Physical devices have PNPDeviceID starting with "PCI\" or something else besides "ROOT\"

            mos = new ManagementObjectSearcher(@"SELECT * 
                                     FROM   Win32_NetworkAdapter 
                                     WHERE  Manufacturer != 'Microsoft' 
                                            AND NOT PNPDeviceID LIKE 'ROOT\\%'");

//            mos = new ManagementObjectSearcher(@"SELECT * 
//                                     FROM   Win32_NetworkAdapter");
            
            // Get the physical adapters and sort them by their index. 
            // This is needed because they're not sorted by default
          /*
            IList<ManagementObject> managementObjectList = mos.Get()
                                                              .Cast<ManagementObject>()
                                                              .OrderBy(p => Convert.ToUInt32(p.Properties["Index"].Value))
                                                              .ToList();

            // Let's just show all the properties for all physical adapters.
            foreach (ManagementObject mo in managementObjectList)
            {
                xelem.Add(new XElement("NIC", mo.Properties["Description"].Value));

                foreach (PropertyData pd in mo.Properties)
                {
                    xelem.Element("NIC").Add(new XElement(pd.Name, new XElement("Value", pd.Value)));
                }
            }
            */
            return xelem;

        }

        private XElement Get_Ports()
        {

            XElement xelem;

            // What to obtain:             
            //   new XElement("NICs",
            //       new XElement("NIC", "name of NIC"), 
            //           new XElement("name of attribute",                                   
            //               new XElement("Value", "value of property"                                    
            //           new XElement("name of attribute",                                   
            //               new XElement("Value", "value of property"                                    
            //           ..........................................                                   
            //               ..........................................                                    
            //   new XElement("Ports",                                   
            //       new XElement("Port", 
            //           new XElement("Number", "80")),
            //           new XElement("Status", "Open"),
            //       new XElement("Port", 
            //           new XElement("Number", "443")),
            //           new XElement("Status", "Open"))

            xelem = new XElement("Ports");

            // Evaluate current system tcp connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.

            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
               
                switch (tcpi.LocalEndPoint.Port)
                {
                    case 80:
                       //xelem.Add(new XElement("Port"), new XElement("Number", "80"), new XElement("Status", "Occupied"));
                       break;  

                    case 443:
                       //xelem.Add(new XElement("Port"), new XElement("Number", "443"), new XElement("Status", "Occupied"));
                       break;

                    default:
                       //xelem.Add(new XElement("Port"), new XElement("Number", tcpi.LocalEndPoint.Port.ToString()), new XElement("Status", "Ready"));
                       break;
                }
                // tcpi.LocalEndPoint.Port.GetType

             }

            return xelem;

        }

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode ;
            TreeNode tNode ;
            XmlNodeList nodeList ;
            int i = 0;
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                inTreeNode.Text = inXmlNode.InnerText.ToString();
            }
        }
   
        private void Search_element() 
        {
            // Customers is a List<Customer>
            // List<string> list = new List<string>() { "Data1", "Data2", "Data3" };

            /* XElement customersElement = new XElement("customers",
            customers.Select(c => new XElement("customer",
                new XAttribute("name", c.Name),
                new XAttribute("lastSeen", c.LastOrder)
                new XElement("address",
                    new XAttribute("town", c.Town),
                    new XAttribute("firstline", c.Address1),
                    // etc
            )); */
        }        

        private void Add_element()
        {
            // Example to Add element to existent XML doc

            // Create XDocument doc2
            XDocument doc2 =
              new XDocument(
                new XElement("file")
              );

            // -- Create a element + Add attribute to this element
            // -- XElement e1 = new XElement("Address");
            // e1.Add(new XAttribute("id",44),new XAttribute("value", "label")) ;

            // -- Create a element + Add sub element with related attribute
            // XElement e1 = new XElement("Address");
            // e1.Element("Address").Add(new XElement("pippo",
            //    new XAttribute("id", 233),
            //    new XAttribute("value", "label23"),
            //    new XAttribute("desc", "mydesc")));
            // doc2.Root.Element("file").Add(e1);

            // -- Create a doc + Add element "name" + Add sub element "Address" with related attribute
            // doc2.Element("file").Element("name").Add(new XElement("Address",
            //        new XAttribute("id", 2),
            //        new XAttribute("value", "label"),
            //        new XAttribute("desc", "")));
            // doc2.Root.Element("file").Add(e1);

            // doc2.Save(Console.Out);    // Print to console
            // doc2.Save("doc2.xml");     // Save to file 

        }

        private string GetJavaInstallationPath()
        {
            string environmentPath = System.Environment.GetEnvironmentVariable("JAVA_HOME");
            if (!string.IsNullOrEmpty(environmentPath))
            {
                return environmentPath;
            }

            string javaKey = "SOFTWARE\\JavaSoft\\Java Runtime Environment\\";
            using (Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(javaKey))
            {
                string currentVersion = rk.GetValue("CurrentVersion").ToString();
                using (Microsoft.Win32.RegistryKey key = rk.OpenSubKey(currentVersion))
                {
                    return key.GetValue("JavaHome").ToString();
                }
            }
        }

        // ---------------------------------------------
        // Interesting to understand SQL SERVER Status
        // ---------------------------------------------

        //public void StartSqlBrowserService(List<String> activeMachines)
        //{
        //    ServiceController myService = new ServiceController();
        //    myService.ServiceName = "SQLBrowser";

        //    foreach (var machine in activeMachines)
        //    {
        //        try
        //        {
        //            myService.MachineName = machine;
        //            string svcStatus = myService.Status.ToString();
        //            switch (svcStatus)
        //            {
        //                case "ContinuePending":
        //                    Console.WriteLine("Service is attempting to continue.");
        //                    break;

        //                case "Paused":
        //                    Console.WriteLine("Service is paused.");
        //                    Console.WriteLine("Attempting to continue the service.");
        //                    myService.Continue();
        //                    break;

        //                case "PausePending":
        //                    Console.WriteLine("Service is pausing.");
        //                    Thread.Sleep(5000);
        //                    try
        //                    {
        //                        Console.WriteLine("Attempting to continue the service.");
        //                        myService.Start();
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e.Message);
        //                    }
        //                    break;

        //                case "Running":
        //                    Console.WriteLine("Service is already running.");
        //                    break;

        //                case "StartPending":
        //                    Console.WriteLine("Service is starting.");
        //                    break;

        //                case "Stopped":
        //                    Console.WriteLine("Service is stopped.");
        //                    Console.WriteLine("Attempting to start service.");
        //                    myService.Start();
        //                    break;

        //                case "StopPending":
        //                    Console.WriteLine("Service is stopping.");
        //                    Thread.Sleep(5000);
        //                    try
        //                    {
        //                        Console.WriteLine("Attempting to restart service.");
        //                        myService.Start();
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        Console.WriteLine(e.Message);
        //                    }
        //                    break;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //}

        //public static void SqlTestInfo()
        //{
        //    SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
        //    DataTable table = instance.GetDataSources();
        //    DisplayData(table);
        //}

        //private static void DisplayData(DataTable table)
        //{
        //    foreach (DataRow row in table.Rows)
        //    {
        //        foreach (DataColumn dataColumn in table.Columns)
        //        {
        //            Console.WriteLine("{0} = {1}", dataColumn.ColumnName, row[dataColumn]);
        //        }
        //        Console.WriteLine();
        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            saveFileDialog1.Filter = "Document Xml | *.xml";
            saveFileDialog1.FileName = "rilevazione.xml";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)              // Test result.
            {
                //doc.Save(saveFileDialog1.FileName);     // Save to file 
            }

        }        

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
      }
}
