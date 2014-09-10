using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Reflection;
using System.Drawing;

namespace Appreq { 
  public partial class Form1 : Form {
    private XDocument doc;
    private Profile _currentProfile;
    private Profile _appProfile;

    public Form1() {
      InitializeComponent();
    }

    private void RefreshForm() {
      var worker = new BackgroundWorker();
      worker.RunWorkerCompleted += (o, e) => {
        var msg = "";
        if (e.Cancelled) {
          msg = "Cancelled";
        } else if (null != e.Error) {
          msg = e.Error.Message;
        } else {
          _currentProfile = (Profile)e.Result;
          populateTreeView(_currentProfile, profileTreeView);
          exportMenuItem.Enabled = true;
          exportButton.Enabled = true;
          refreshMenuItem.Enabled = true;
          refreshButton.Enabled = true;
          msg = "Done";
        }
        toolStripStatusLabel1.Text = msg;
      };
      worker.DoWork += new DoWorkEventHandler((o, e) => {
        toolStripStatusLabel1.Text = "Loading, please wait...";
        var profiler = new SystemProfiler();
        e.Result = profiler.GetData();
      });
      exportMenuItem.Enabled = false;
      exportButton.Enabled = false;
      refreshMenuItem.Enabled = false;
      refreshButton.Enabled = false;
      worker.RunWorkerAsync();
    }

    private void populateTreeView(Profile profile, TreeView treeView) {
      if (null == profile || null == treeView) {
        return;
      }
      var emptyNs = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
      var xs = new XmlSerializer(typeof(Profile));
      var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true };
      using (var ms = new MemoryStream()) {
        using (var xw = XmlWriter.Create(ms, settings)) {
          xs.Serialize(xw, profile, emptyNs);
          var xmldoc = new XmlDocument();
          ms.Seek(0, SeekOrigin.Begin);
          xmldoc.Load(ms);
          var xmlnode = xmldoc.ChildNodes[0];
          treeView.Nodes.Clear();
          treeView.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));
          var tNode = treeView.Nodes[0];
          AddNode(xmldoc, tNode);
        }
      }
      treeView.ExpandAll();
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
          new XElement("Appl",/*
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
            */
            new XElement("Environment",
                  new XElement("SW",
                      new XElement("OSs",                                    
                          //Get_OS(),
                          Get_NetFramework(),
                          Get_IIS(),
                          Get_current_javaJVM(),
                          //Get_Browsers(),
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

        profileTreeView.Nodes.Clear();
        profileTreeView.Nodes.Add(new TreeNode(xmldoc.DocumentElement.Name));           

        tNode = profileTreeView.Nodes[0];
        AddNode(xmlnode, tNode);

        // Expand Treeview
        profileTreeView.Nodes[0].Expand();

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

        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode) {
          XmlNode xNode ;
          TreeNode tNode ;
          XmlNodeList nodeList ;
          var i = 0;
          if (inXmlNode.HasChildNodes) {
            nodeList = inXmlNode.ChildNodes;
            for (i = 0; i <= nodeList.Count - 1; i++) {
              xNode = inXmlNode.ChildNodes[i];
              var newTreeNode = new TreeNode(xNode.Name);
              // Set icon
              var check = xNode["CheckPassed"];
              if (null != check) {
                if(check.InnerText == "true") {
                  newTreeNode.ImageKey = "accept";
                  newTreeNode.SelectedImageKey = "accept";
                }
                if (check.InnerText == "false") {
                  newTreeNode.ImageKey = "alert";
                  newTreeNode.SelectedImageKey = "alert";
                }
              }
              if (xNode.Name == "CheckPassed") {
                continue;
              }
              inTreeNode.Nodes.Add(newTreeNode);
              tNode = inTreeNode.Nodes[i];
              AddNode(xNode, tNode);
            }
          } else {
            inTreeNode.Text = inXmlNode.Value;
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

    private void OpenFile_Command(object sender, EventArgs e) {
      //TODO: implement
      toolStripStatusLabel1.Text = "Open XML is not implemented";
    }

    private void ExportSystemProfile_Command(object sender, EventArgs e) {
      if (null == _currentProfile) return;
      saveFileDialog1.Filter = "XML Document | *.xml";
      saveFileDialog1.FileName = string.Format("system.profile_{0}.xml", System.Environment.MachineName);
      DialogResult result = saveFileDialog1.ShowDialog();
      if (result == DialogResult.OK) {
        try {
          ProfileSerializer.SerializeToFile(_currentProfile, saveFileDialog1.FileName);
          toolStripStatusLabel1.Text = string.Format("Saved to: {0}", saveFileDialog1.FileName);
        } catch (Exception ex) {
          toolStripStatusLabel1.Text = ex.Message;
        }
      }
    }

    private void Refresh_Command(object sender, EventArgs e) {
      //TODO: refresh compare and app info windows
      RefreshForm();
    }

    private void Exit_Command(object sender, EventArgs e) {
      Application.Exit();
    }

    private void appComboBox_SelectedIndexChanged(object sender, EventArgs e) {
      try {
        appTreeView.Nodes.Clear();
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = string.Format("{0}.{1}.xml", Assembly.GetExecutingAssembly().GetName().Name, (string)((ComboBox)sender).SelectedItem);
        using (Stream stream = assembly.GetManifestResourceStream(resourceName)) {
          var xs = new XmlSerializer(typeof(Profile));
          using (StreamReader reader = new StreamReader(stream)) {
            var app = (Profile)xs.Deserialize(reader);
            app.IsDiffMode = true;
            _currentProfile.Diff(app);
            _appProfile = app;
            populateTreeView(app, appTreeView);
            toolStripStatusLabel1.Text = "Ready";
            appTreeView.Focus();        
            appTreeView.SelectedNode = null;
            exportCheckButton.Enabled = null != _appProfile;
            exportReportMenuItem.Enabled = null != _appProfile;
          }
        }
      } catch (Exception) {
        exportCheckButton.Enabled = false;
        exportReportMenuItem.Enabled = false;
        toolStripStatusLabel1.Text = "Unable to load embedded resource";
      }
    }

    private TreeNode FindNode(TreeNode tnc, string path) {
      if (path == tnc.FullPath) {
        return tnc;
      }
      foreach (TreeNode tn in tnc.Nodes) {
        var find = FindNode(tn, path);
        if (null != find) {
          return find;
        }
      }
      return null;
    }

    private void SelectNodes(TreeView tv, string path) {
      foreach (TreeNode tn in tv.Nodes) {
        var findNode = FindNode(tn, path);
        if (null != findNode) {
          tv.CollapseAll();
          tv.SelectedNode = findNode;
          tv.SelectedNode.BackColor = Color.Yellow;
          tv.SelectedNode.Toggle();
          break;
        }
      }
    }

    private void appTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
      var path = ((TreeView)sender).SelectedNode.FullPath;
      SelectNodes(profileTreeView, path);
    }

    private void appTreeView_MouseUp(object sender, MouseEventArgs e) {
      var tv = ((TreeView)sender);
      if (null != tv.SelectedNode) {
        tv.SelectedNode.BackColor = Color.White;
      }
      if (null != profileTreeView.SelectedNode) {
        profileTreeView.SelectedNode.BackColor = Color.White;
      }
    }

    private void appTreeView_KeyDown(object sender, KeyEventArgs e) {
      var tv = ((TreeView)sender);
      if (null != tv.SelectedNode) {
        tv.SelectedNode.BackColor = Color.White;
      }
      if (null != profileTreeView.SelectedNode) {
        profileTreeView.SelectedNode.BackColor = Color.White;
      }
    }

    private void ExportReport_Command(object sender, EventArgs e) {
      if (null == _appProfile) return;
      saveFileDialog1.Filter = "XML Document | *.xml";
      saveFileDialog1.FileName = string.Format("{0}.report_{1}.xml", (string)appComboBox.SelectedItem, System.Environment.MachineName);
      DialogResult result = saveFileDialog1.ShowDialog();
      if (result == DialogResult.OK) {
        try {
          ProfileSerializer.SerializeToFile(_appProfile, saveFileDialog1.FileName);
          toolStripStatusLabel1.Text = string.Format("Saved to: {0}", saveFileDialog1.FileName);
        } catch (Exception ex) {
          toolStripStatusLabel1.Text = ex.Message;
        }
      }
    }
  }
}
