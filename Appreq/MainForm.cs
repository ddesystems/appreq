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
  public partial class MainForm : Form {
    private Profile _currentProfile;
    private Profile _appProfile;
    public const string FILE_DIALOG_FILTER = "XML Document | *.xml";
    public delegate void ProfileModeChangedHandler(ProfileMode newMode);
    public event ProfileModeChangedHandler ProfileModeChanged;
    private ProfileMode _mode;    
    public ProfileMode Mode {
      get {
        return _mode;
      }
      private set {
        _mode = value;
        ProfileModeChanged(value);
        if (ProfileMode.External == value) {
          grpLocal.Text = "System Profile [OFFLINE]";
          grpLocal.ForeColor = Color.Red;
          _currentProfile = null;
          profileTreeView.Nodes.Clear();
          exportButton.Enabled = false;
          appComboBox.Enabled = false;
        } else if (ProfileMode.System == value) {
          grpLocal.Text = "System Profile";
          grpLocal.ForeColor = Color.Black;
          RefreshForm();
        }
      }
    }

    private string _extProfile;    

    public MainForm() {
      InitializeComponent();
      this.ProfileModeChanged += (newMode) => {
        if (ProfileMode.External == newMode) {
          grpLocal.Text = "System Profile [OFFLINE]";
        } else if (ProfileMode.System == newMode) {
          grpLocal.Text = "System Profile";
        }
      };
    }

    private void RefreshForm() {
      var worker = new BackgroundWorker();

      worker.DoWork += new DoWorkEventHandler((o, e) => {
        toolStripStatusLabel1.Text = "Loading, please wait...";
        if (ProfileMode.External == Mode) {
          // Load from file
          if (String.IsNullOrEmpty(_extProfile)) {
            toolStripStatusLabel1.Text = "Please import a profile first";
          } else {
            e.Result = ProfileSerializer.Deserialize(_extProfile);
          }
        } else { 
          // Get system profile
          var profiler = new SystemProfiler();
          e.Result = profiler.GetData();
        }
      });

      worker.RunWorkerCompleted += (o, e) => {
        var msg = "";
        if (e.Cancelled) {
          msg = "Cancelled";
        } else if (null != e.Error) {
          msg = e.Error.Message;
        } else {
          _currentProfile = (Profile)e.Result;
          if (null != _appProfile && null != _currentProfile) {
            _currentProfile.IsDiffMode = true;
            _appProfile.Diff(_currentProfile);
          }
          TreeViewHelper.populateTreeView(_currentProfile, profileTreeView);
          TreeViewHelper.populateTreeView(_currentProfile, reportTreeView, true);
          openButton.Enabled = ProfileMode.External == Mode;
          openMenuItem.Enabled = ProfileMode.External == Mode;
          exportMenuItem.Enabled = null != _currentProfile;
          exportButton.Enabled = null != _currentProfile;
          exportReportButton.Enabled = null != _currentProfile;
          exportReportMenuItem.Enabled = null != _currentProfile;
          refreshMenuItem.Enabled = true;
          refreshButton.Enabled = true;
          appComboBox.Enabled = null != _currentProfile;
          msg = "Done";
        }
        toolStripStatusLabel1.Text = msg;
      };

      openButton.Enabled = false;
      openMenuItem.Enabled = false;
      exportMenuItem.Enabled = false;
      exportButton.Enabled = false;
      refreshMenuItem.Enabled = false;
      refreshButton.Enabled = false;
      appComboBox.Enabled = false;

      worker.RunWorkerAsync();
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

    private void ImportProfile_Command(object sender, EventArgs e) {
      openFileDialog1.Filter = FILE_DIALOG_FILTER;
      var result = openFileDialog1.ShowDialog();
      if (DialogResult.OK == result) {
        try {
          _currentProfile = ProfileSerializer.Deserialize(openFileDialog1.FileName);
          _extProfile = openFileDialog1.FileName;
          exportButton.Enabled = false;
          appComboBox.Enabled = false;
          RefreshForm();
        } catch (Exception ex) {
          _extProfile = null;
          _currentProfile = null;
          toolStripStatusLabel1.Text = ex.Message;
        }
      }
    }

    private void ExportSystemProfile_Command(object sender, EventArgs e) {
      if (null == _currentProfile) return;
      saveFileDialog1.Filter = FILE_DIALOG_FILTER;
      saveFileDialog1.FileName = string.Format("system.profile_{0}.xml", System.Environment.MachineName);
      var result = saveFileDialog1.ShowDialog();
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

    /// <summary>
    /// Load app profile from resource
    /// </summary>
    /// <param name="profile">app profile name</param>
    /// <returns>Stream, containing an app profile xml</returns>
    private Stream GetProfileResourceStream(string profile) {
      var assembly = Assembly.GetExecutingAssembly();
      var resourceName = string.Format(
        "{0}.{1}.xml",
        assembly.GetName().Name,
        profile);
      return assembly.GetManifestResourceStream(resourceName);
    }

    private void appComboBox_SelectedIndexChanged(object sender, EventArgs e) {
      try {
        // populate the application treeview
        _appProfile = null;
        var resourceStream = GetProfileResourceStream((string)((ComboBox)sender).SelectedItem);
        using (Stream stream = resourceStream) {
          var xs = new XmlSerializer(typeof(Profile));
          using (StreamReader reader = new StreamReader(stream)) {
            var appProfile = (Profile)xs.Deserialize(reader);
            _currentProfile.IsDiffMode = true;
            appProfile.Diff(_currentProfile);
            _appProfile = appProfile;
            TreeViewHelper.populateTreeView(appProfile, appTreeView);
            TreeViewHelper.populateTreeView(_currentProfile, profileTreeView);
            TreeViewHelper.populateTreeView(_currentProfile, reportTreeView, true);
            toolStripStatusLabel1.Text = "Ready";
            profileTreeView.Focus();
            profileTreeView.SelectedNode = null;
            exportReportButton.Enabled = null != _appProfile;
            exportReportMenuItem.Enabled = null != _appProfile;
          }
        }
      } catch (Exception) {
        exportReportButton.Enabled = false;
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

    private void profileTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
      var path = ((TreeView)sender).SelectedNode.FullPath;
      SelectNodes(appTreeView, path);
    }

    private void profileTreeView_MouseUp(object sender, MouseEventArgs e) {
      var tv = ((TreeView)sender);
      if (null != tv.SelectedNode) {
        tv.SelectedNode.BackColor = Color.White;
      }
      if (null != appTreeView.SelectedNode) {
        appTreeView.SelectedNode.BackColor = Color.White;
      }
    }

    private void profileTreeView_KeyDown(object sender, KeyEventArgs e) {
      var tv = ((TreeView)sender);
      if (null != tv.SelectedNode) {
        tv.SelectedNode.BackColor = Color.White;
      }
      if (null != profileTreeView.SelectedNode) {
        appTreeView.SelectedNode.BackColor = Color.White;
      }
    }

    private void ExportReport_Command(object sender, EventArgs e) {
      if (null == _appProfile) return;
      saveFileDialog1.Filter = "XML Document | *.xml";
      saveFileDialog1.FileName = string.Format("{0}.report_{1}.xml", (string)appComboBox.SelectedItem, System.Environment.MachineName);
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

    private void externalModeCheck_CheckedChanged(object sender, EventArgs e) {
      var cb = (CheckBox)sender;
      openButton.Enabled = cb.Checked;
      openMenuItem.Enabled = cb.Checked;
      Mode = cb.Checked ? ProfileMode.External : ProfileMode.System;
    }

    private void reportTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e) {

    }
  }
}
