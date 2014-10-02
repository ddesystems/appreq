﻿namespace Appreq
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          this.components = new System.ComponentModel.Container();
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
          this.profileTreeView = new System.Windows.Forms.TreeView();
          this.imageList1 = new System.Windows.Forms.ImageList(this.components);
          this.grpLocal = new System.Windows.Forms.GroupBox();
          this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
          this.ststatus = new System.Windows.Forms.StatusStrip();
          this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
          this.menuMain = new System.Windows.Forms.MenuStrip();
          this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.exportReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStrip1 = new System.Windows.Forms.ToolStrip();
          this.openButton = new System.Windows.Forms.ToolStripButton();
          this.exportButton = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
          this.exportReportButton = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
          this.refreshButton = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
          this.exitButton = new System.Windows.Forms.ToolStripButton();
          this.splitContainer1 = new System.Windows.Forms.SplitContainer();
          this.grpAppl = new System.Windows.Forms.GroupBox();
          this.appComboBox = new System.Windows.Forms.ComboBox();
          this.appTreeView = new System.Windows.Forms.TreeView();
          this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
          this.tabMain = new System.Windows.Forms.TabControl();
          this.tabProfile = new System.Windows.Forms.TabPage();
          this.tabReport = new System.Windows.Forms.TabPage();
          this.reportTreeView = new System.Windows.Forms.TreeView();
          this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
          this.externalModeCheck = new System.Windows.Forms.CheckBox();
          this.grpLocal.SuspendLayout();
          this.ststatus.SuspendLayout();
          this.menuMain.SuspendLayout();
          this.toolStrip1.SuspendLayout();
          this.splitContainer1.Panel1.SuspendLayout();
          this.splitContainer1.Panel2.SuspendLayout();
          this.splitContainer1.SuspendLayout();
          this.grpAppl.SuspendLayout();
          this.tabMain.SuspendLayout();
          this.tabProfile.SuspendLayout();
          this.tabReport.SuspendLayout();
          this.SuspendLayout();
          // 
          // profileTreeView
          // 
          this.profileTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.profileTreeView.ImageKey = "wheel";
          this.profileTreeView.ImageList = this.imageList1;
          this.profileTreeView.Location = new System.Drawing.Point(6, 21);
          this.profileTreeView.Name = "profileTreeView";
          this.profileTreeView.SelectedImageIndex = 0;
          this.profileTreeView.Size = new System.Drawing.Size(485, 549);
          this.profileTreeView.TabIndex = 1;
          this.profileTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.profileTreeView_AfterSelect);
          this.profileTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.profileTreeView_KeyDown);
          this.profileTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.profileTreeView_MouseUp);
          // 
          // imageList1
          // 
          this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
          this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
          this.imageList1.Images.SetKeyName(0, "wheel");
          this.imageList1.Images.SetKeyName(1, "alert");
          this.imageList1.Images.SetKeyName(2, "accept");
          this.imageList1.Images.SetKeyName(3, "save");
          // 
          // grpLocal
          // 
          this.grpLocal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.grpLocal.Controls.Add(this.profileTreeView);
          this.grpLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.grpLocal.Location = new System.Drawing.Point(3, 3);
          this.grpLocal.Name = "grpLocal";
          this.grpLocal.Size = new System.Drawing.Size(497, 576);
          this.grpLocal.TabIndex = 4;
          this.grpLocal.TabStop = false;
          this.grpLocal.Text = "System Profile";
          // 
          // ststatus
          // 
          this.ststatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
          this.ststatus.Location = new System.Drawing.Point(0, 689);
          this.ststatus.Name = "ststatus";
          this.ststatus.Size = new System.Drawing.Size(1072, 22);
          this.ststatus.TabIndex = 6;
          // 
          // toolStripStatusLabel1
          // 
          this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
          this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
          // 
          // menuMain
          // 
          this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.optionsMenuItem,
            this.helpToolStripMenuItem});
          this.menuMain.Location = new System.Drawing.Point(0, 0);
          this.menuMain.Name = "menuMain";
          this.menuMain.Size = new System.Drawing.Size(1072, 24);
          this.menuMain.TabIndex = 9;
          // 
          // fileMenuItem
          // 
          this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.exportMenuItem,
            this.exportReportMenuItem,
            this.exitMenuItem});
          this.fileMenuItem.Name = "fileMenuItem";
          this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
          this.fileMenuItem.Text = "File";
          // 
          // openMenuItem
          // 
          this.openMenuItem.Enabled = false;
          this.openMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openMenuItem.Image")));
          this.openMenuItem.Name = "openMenuItem";
          this.openMenuItem.Size = new System.Drawing.Size(197, 22);
          this.openMenuItem.Text = "Import System Profile...";
          // 
          // exportMenuItem
          // 
          this.exportMenuItem.Enabled = false;
          this.exportMenuItem.Image = global::Appreq.Properties.Resources.Custom_Icon_Design_Mini_Save;
          this.exportMenuItem.Name = "exportMenuItem";
          this.exportMenuItem.Size = new System.Drawing.Size(197, 22);
          this.exportMenuItem.Text = "Export System Profile...";
          this.exportMenuItem.Click += new System.EventHandler(this.ExportSystemProfile_Command);
          // 
          // exportReportMenuItem
          // 
          this.exportReportMenuItem.Enabled = false;
          this.exportReportMenuItem.Image = global::Appreq.Properties.Resources.Custom_Icon_Design_Mini_Save;
          this.exportReportMenuItem.Name = "exportReportMenuItem";
          this.exportReportMenuItem.Size = new System.Drawing.Size(197, 22);
          this.exportReportMenuItem.Text = "Export Report...";
          this.exportReportMenuItem.Click += new System.EventHandler(this.ExportReport_Command);
          // 
          // exitMenuItem
          // 
          this.exitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitMenuItem.Image")));
          this.exitMenuItem.Name = "exitMenuItem";
          this.exitMenuItem.Size = new System.Drawing.Size(197, 22);
          this.exitMenuItem.Text = "Exit";
          this.exitMenuItem.Click += new System.EventHandler(this.Exit_Command);
          // 
          // optionsMenuItem
          // 
          this.optionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshMenuItem});
          this.optionsMenuItem.Name = "optionsMenuItem";
          this.optionsMenuItem.Size = new System.Drawing.Size(48, 20);
          this.optionsMenuItem.Text = "Tools";
          // 
          // refreshMenuItem
          // 
          this.refreshMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("refreshMenuItem.Image")));
          this.refreshMenuItem.Name = "refreshMenuItem";
          this.refreshMenuItem.Size = new System.Drawing.Size(113, 22);
          this.refreshMenuItem.Text = "Refresh";
          this.refreshMenuItem.Click += new System.EventHandler(this.Refresh_Command);
          // 
          // helpToolStripMenuItem
          // 
          this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
          this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
          this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
          this.helpToolStripMenuItem.Text = "Help";
          // 
          // aboutToolStripMenuItem
          // 
          this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
          this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
          this.aboutToolStripMenuItem.Text = "About";
          // 
          // toolStrip1
          // 
          this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
          this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
          this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openButton,
            this.exportButton,
            this.toolStripSeparator,
            this.exportReportButton,
            this.toolStripSeparator2,
            this.refreshButton,
            this.toolStripSeparator1,
            this.exitButton});
          this.toolStrip1.Location = new System.Drawing.Point(0, 24);
          this.toolStrip1.Name = "toolStrip1";
          this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
          this.toolStrip1.Size = new System.Drawing.Size(1072, 39);
          this.toolStrip1.TabIndex = 10;
          this.toolStrip1.Text = "toolStrip1";
          // 
          // openButton
          // 
          this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
          this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.openButton.Name = "openButton";
          this.openButton.Size = new System.Drawing.Size(36, 36);
          this.openButton.Text = "&Open";
          this.openButton.ToolTipText = "Open...";
          this.openButton.Click += new System.EventHandler(this.ImportProfile_Command);
          // 
          // exportButton
          // 
          this.exportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.exportButton.Enabled = false;
          this.exportButton.Image = ((System.Drawing.Image)(resources.GetObject("exportButton.Image")));
          this.exportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.exportButton.Name = "exportButton";
          this.exportButton.Size = new System.Drawing.Size(36, 36);
          this.exportButton.Text = "&Export System Profile...";
          this.exportButton.ToolTipText = "Save...";
          this.exportButton.Click += new System.EventHandler(this.ExportSystemProfile_Command);
          // 
          // toolStripSeparator
          // 
          this.toolStripSeparator.Name = "toolStripSeparator";
          this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
          // 
          // exportReportButton
          // 
          this.exportReportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.exportReportButton.Enabled = false;
          this.exportReportButton.Image = global::Appreq.Properties.Resources.Custom_Icon_Design_Mini_Save;
          this.exportReportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.exportReportButton.Name = "exportReportButton";
          this.exportReportButton.Size = new System.Drawing.Size(36, 36);
          this.exportReportButton.Text = "Export Report...";
          this.exportReportButton.Click += new System.EventHandler(this.ExportReport_Command);
          // 
          // toolStripSeparator2
          // 
          this.toolStripSeparator2.Name = "toolStripSeparator2";
          this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
          // 
          // refreshButton
          // 
          this.refreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
          this.refreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.refreshButton.Name = "refreshButton";
          this.refreshButton.Size = new System.Drawing.Size(36, 36);
          this.refreshButton.Text = "Refresh";
          this.refreshButton.Click += new System.EventHandler(this.Refresh_Command);
          // 
          // toolStripSeparator1
          // 
          this.toolStripSeparator1.Name = "toolStripSeparator1";
          this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
          // 
          // exitButton
          // 
          this.exitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.exitButton.Image = ((System.Drawing.Image)(resources.GetObject("exitButton.Image")));
          this.exitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.exitButton.Name = "exitButton";
          this.exitButton.Size = new System.Drawing.Size(36, 36);
          this.exitButton.Text = "Exit";
          this.exitButton.Click += new System.EventHandler(this.Exit_Command);
          // 
          // splitContainer1
          // 
          this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.splitContainer1.Location = new System.Drawing.Point(6, 6);
          this.splitContainer1.Name = "splitContainer1";
          // 
          // splitContainer1.Panel1
          // 
          this.splitContainer1.Panel1.Controls.Add(this.grpLocal);
          // 
          // splitContainer1.Panel2
          // 
          this.splitContainer1.Panel2.Controls.Add(this.grpAppl);
          this.splitContainer1.Size = new System.Drawing.Size(1028, 582);
          this.splitContainer1.SplitterDistance = 503;
          this.splitContainer1.TabIndex = 11;
          // 
          // grpAppl
          // 
          this.grpAppl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.grpAppl.Controls.Add(this.appComboBox);
          this.grpAppl.Controls.Add(this.appTreeView);
          this.grpAppl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.grpAppl.Location = new System.Drawing.Point(3, 6);
          this.grpAppl.Name = "grpAppl";
          this.grpAppl.Size = new System.Drawing.Size(515, 573);
          this.grpAppl.TabIndex = 8;
          this.grpAppl.TabStop = false;
          this.grpAppl.Text = "Application Profile";
          // 
          // appComboBox
          // 
          this.appComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.appComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.appComboBox.FormattingEnabled = true;
          this.appComboBox.Items.AddRange(new object[] {
            "test-app",
            "Saldi e Movimenti",
            "Gant",
            "Checkin",
            "Comma"});
          this.appComboBox.Location = new System.Drawing.Point(6, 21);
          this.appComboBox.Name = "appComboBox";
          this.appComboBox.Size = new System.Drawing.Size(503, 24);
          this.appComboBox.TabIndex = 9;
          this.appComboBox.SelectedIndexChanged += new System.EventHandler(this.appComboBox_SelectedIndexChanged);
          // 
          // appTreeView
          // 
          this.appTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.appTreeView.ImageIndex = 0;
          this.appTreeView.ImageList = this.imageList1;
          this.appTreeView.Location = new System.Drawing.Point(6, 51);
          this.appTreeView.Name = "appTreeView";
          this.appTreeView.SelectedImageIndex = 0;
          this.appTreeView.Size = new System.Drawing.Size(503, 516);
          this.appTreeView.TabIndex = 3;
          this.appTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.profileTreeView_MouseUp);
          // 
          // tabMain
          // 
          this.tabMain.Controls.Add(this.tabProfile);
          this.tabMain.Controls.Add(this.tabReport);
          this.tabMain.Location = new System.Drawing.Point(12, 66);
          this.tabMain.Name = "tabMain";
          this.tabMain.SelectedIndex = 0;
          this.tabMain.Size = new System.Drawing.Size(1048, 620);
          this.tabMain.TabIndex = 12;
          // 
          // tabProfile
          // 
          this.tabProfile.Controls.Add(this.splitContainer1);
          this.tabProfile.Location = new System.Drawing.Point(4, 22);
          this.tabProfile.Name = "tabProfile";
          this.tabProfile.Padding = new System.Windows.Forms.Padding(3);
          this.tabProfile.Size = new System.Drawing.Size(1040, 594);
          this.tabProfile.TabIndex = 0;
          this.tabProfile.Text = "Profile";
          this.tabProfile.UseVisualStyleBackColor = true;
          // 
          // tabReport
          // 
          this.tabReport.Controls.Add(this.reportTreeView);
          this.tabReport.Location = new System.Drawing.Point(4, 22);
          this.tabReport.Name = "tabReport";
          this.tabReport.Padding = new System.Windows.Forms.Padding(3);
          this.tabReport.Size = new System.Drawing.Size(1040, 594);
          this.tabReport.TabIndex = 1;
          this.tabReport.Text = "Report";
          this.tabReport.UseVisualStyleBackColor = true;
          // 
          // reportTreeView
          // 
          this.reportTreeView.ImageIndex = 0;
          this.reportTreeView.ImageList = this.imageList1;
          this.reportTreeView.Location = new System.Drawing.Point(6, 6);
          this.reportTreeView.Name = "reportTreeView";
          this.reportTreeView.SelectedImageIndex = 0;
          this.reportTreeView.Size = new System.Drawing.Size(1028, 582);
          this.reportTreeView.TabIndex = 0;
          // 
          // openFileDialog1
          // 
          this.openFileDialog1.FileName = "openFileDialog1";
          // 
          // externalModeCheck
          // 
          this.externalModeCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)));
          this.externalModeCheck.AutoSize = true;
          this.externalModeCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.externalModeCheck.Location = new System.Drawing.Point(207, 43);
          this.externalModeCheck.Name = "externalModeCheck";
          this.externalModeCheck.Size = new System.Drawing.Size(93, 17);
          this.externalModeCheck.TabIndex = 13;
          this.externalModeCheck.Text = "External Profile";
          this.externalModeCheck.UseVisualStyleBackColor = true;
          this.externalModeCheck.CheckedChanged += new System.EventHandler(this.externalModeCheck_CheckedChanged);
          // 
          // MainForm
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(1072, 711);
          this.Controls.Add(this.externalModeCheck);
          this.Controls.Add(this.tabMain);
          this.Controls.Add(this.toolStrip1);
          this.Controls.Add(this.ststatus);
          this.Controls.Add(this.menuMain);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MainMenuStrip = this.menuMain;
          this.Name = "MainForm";
          this.Text = "System Profiler";
          this.Load += new System.EventHandler(this.Refresh_Command);
          this.grpLocal.ResumeLayout(false);
          this.ststatus.ResumeLayout(false);
          this.ststatus.PerformLayout();
          this.menuMain.ResumeLayout(false);
          this.menuMain.PerformLayout();
          this.toolStrip1.ResumeLayout(false);
          this.toolStrip1.PerformLayout();
          this.splitContainer1.Panel1.ResumeLayout(false);
          this.splitContainer1.Panel2.ResumeLayout(false);
          this.splitContainer1.ResumeLayout(false);
          this.grpAppl.ResumeLayout(false);
          this.tabMain.ResumeLayout(false);
          this.tabProfile.ResumeLayout(false);
          this.tabReport.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView profileTreeView;
        private System.Windows.Forms.GroupBox grpLocal;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip ststatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openButton;
        private System.Windows.Forms.ToolStripButton exportButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton refreshButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton exitButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpAppl;
        private System.Windows.Forms.ComboBox appComboBox;
        private System.Windows.Forms.TreeView appTreeView;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripMenuItem exportReportMenuItem;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabProfile;
        private System.Windows.Forms.TabPage tabReport;
        private System.Windows.Forms.TreeView reportTreeView;
        private System.Windows.Forms.ToolStripButton exportReportButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox externalModeCheck;
    }
}
