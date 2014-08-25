﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace Appreq {
  public partial class Form1 : Form {
    private XDocument doc;
    private App _currentProfile;

    public Form1() {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) {
      populateTreeView();
    }

    private void populateTreeView() {
      var profiler = new SystemProfiler();
      _currentProfile = profiler.GetData();
      var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
      var xs = new XmlSerializer(typeof(App));
      //var settings = new XmlWriterSettings { OmitXmlDeclaration = true };
      var settings = new XmlWriterSettings { OmitXmlDeclaration = false, Indent = true };
      using (var ms = new MemoryStream()) {
        using (var xw = XmlWriter.Create(ms, settings)) {
          xs.Serialize(xw, _currentProfile, emptyNs);
          var xmldoc = new XmlDocument();
          ms.Seek(0, SeekOrigin.Begin);
          xmldoc.Load(ms);
          var xmlnode = xmldoc.ChildNodes[0];
          treeView1.Nodes.Clear();
          treeView1.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));
          var tNode = treeView1.Nodes[0];
          AddNode(xmldoc, tNode);
        }
      }
      saveAsToolStripMenuItem.Enabled = true;
    }

    [Obsolete]
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


    [Obsolete]
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
                             //Get_OS(),
                             Get_NetFramework(),
                             Get_IIS(),
                             Get_current_javaJVM(),
                             Get_Browsers(),
                             Get_DataBases())),
                     //new XElement("HW",
                          //Get_Processor(),
                          //Get_Disks(),
                          //Get_RAM()),
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

    private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
      if (null == _currentProfile) return;
      saveFileDialog1.Filter = "XML Document | *.xml";
      saveFileDialog1.FileName = string.Format("system.profile_{0}.xml", System.Environment.MachineName);
      DialogResult result = saveFileDialog1.ShowDialog();
      if (result == DialogResult.OK) {
        try {
          ApplicationSerializer.SerializeToFile(_currentProfile, saveFileDialog1.FileName);
          toolStripStatusLabel1.Text = string.Format("Saved to: {0}", saveFileDialog1.FileName);
        } catch (Exception ex) {
          toolStripStatusLabel1.Text = ex.Message;
        }
      }
    }

    
  }
}
