namespace Appreq
{
    partial class Form1
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
          this.profileTreeView = new System.Windows.Forms.TreeView();
          this.appTreeView = new System.Windows.Forms.TreeView();
          this.grpLocal = new System.Windows.Forms.GroupBox();
          this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
          this.ststatus = new System.Windows.Forms.StatusStrip();
          this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
          this.grpmatch = new System.Windows.Forms.GroupBox();
          this.diffTreeView = new System.Windows.Forms.TreeView();
          this.grpAppl = new System.Windows.Forms.GroupBox();
          this.appComboBox = new System.Windows.Forms.ComboBox();
          this.menuMain = new System.Windows.Forms.MenuStrip();
          this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStrip1 = new System.Windows.Forms.ToolStrip();
          this.openButton = new System.Windows.Forms.ToolStripButton();
          this.exportButton = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
          this.refreshButton = new System.Windows.Forms.ToolStripButton();
          this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
          this.exitButton = new System.Windows.Forms.ToolStripButton();
          this.grpLocal.SuspendLayout();
          this.ststatus.SuspendLayout();
          this.grpmatch.SuspendLayout();
          this.grpAppl.SuspendLayout();
          this.menuMain.SuspendLayout();
          this.toolStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // profileTreeView
          // 
          this.profileTreeView.Location = new System.Drawing.Point(6, 21);
          this.profileTreeView.Name = "profileTreeView";
          this.profileTreeView.Size = new System.Drawing.Size(293, 386);
          this.profileTreeView.TabIndex = 1;
          // 
          // appTreeView
          // 
          this.appTreeView.Location = new System.Drawing.Point(679, 161);
          this.appTreeView.Name = "appTreeView";
          this.appTreeView.Size = new System.Drawing.Size(293, 343);
          this.appTreeView.TabIndex = 3;
          // 
          // grpLocal
          // 
          this.grpLocal.Controls.Add(this.profileTreeView);
          this.grpLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.grpLocal.Location = new System.Drawing.Point(9, 91);
          this.grpLocal.Name = "grpLocal";
          this.grpLocal.Size = new System.Drawing.Size(305, 413);
          this.grpLocal.TabIndex = 4;
          this.grpLocal.TabStop = false;
          this.grpLocal.Text = "Risorse locali rilevate";
          // 
          // ststatus
          // 
          this.ststatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
          this.ststatus.Location = new System.Drawing.Point(0, 518);
          this.ststatus.Name = "ststatus";
          this.ststatus.Size = new System.Drawing.Size(999, 22);
          this.ststatus.TabIndex = 6;
          // 
          // toolStripStatusLabel1
          // 
          this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
          this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
          // 
          // grpmatch
          // 
          this.grpmatch.Controls.Add(this.diffTreeView);
          this.grpmatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.grpmatch.Location = new System.Drawing.Point(338, 91);
          this.grpmatch.Name = "grpmatch";
          this.grpmatch.Size = new System.Drawing.Size(306, 413);
          this.grpmatch.TabIndex = 7;
          this.grpmatch.TabStop = false;
          this.grpmatch.Text = "Raffronto";
          // 
          // diffTreeView
          // 
          this.diffTreeView.Location = new System.Drawing.Point(7, 22);
          this.diffTreeView.Name = "diffTreeView";
          this.diffTreeView.Size = new System.Drawing.Size(293, 385);
          this.diffTreeView.TabIndex = 0;
          // 
          // grpAppl
          // 
          this.grpAppl.Controls.Add(this.appComboBox);
          this.grpAppl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.grpAppl.Location = new System.Drawing.Point(673, 91);
          this.grpAppl.Name = "grpAppl";
          this.grpAppl.Size = new System.Drawing.Size(305, 413);
          this.grpAppl.TabIndex = 8;
          this.grpAppl.TabStop = false;
          this.grpAppl.Text = "Applicazioni";
          // 
          // appComboBox
          // 
          this.appComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
          this.appComboBox.FormattingEnabled = true;
          this.appComboBox.Items.AddRange(new object[] {
            "test-app",
            "Gant",
            "Saldi e Movimenti",
            "Checkin",
            "Comma"});
          this.appComboBox.Location = new System.Drawing.Point(6, 31);
          this.appComboBox.Name = "appComboBox";
          this.appComboBox.Size = new System.Drawing.Size(293, 24);
          this.appComboBox.TabIndex = 9;
          this.appComboBox.SelectedIndexChanged += new System.EventHandler(this.appComboBox_SelectedIndexChanged);
          // 
          // menuMain
          // 
          this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.optionsMenuItem,
            this.helpToolStripMenuItem});
          this.menuMain.Location = new System.Drawing.Point(0, 0);
          this.menuMain.Name = "menuMain";
          this.menuMain.Size = new System.Drawing.Size(999, 24);
          this.menuMain.TabIndex = 9;
          // 
          // fileMenuItem
          // 
          this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuItem,
            this.exportMenuItem,
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
          this.openMenuItem.Size = new System.Drawing.Size(116, 22);
          this.openMenuItem.Text = "Open...";
          this.openMenuItem.Click += new System.EventHandler(this.OpenFile_Command);
          // 
          // exportMenuItem
          // 
          this.exportMenuItem.Enabled = false;
          this.exportMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exportMenuItem.Image")));
          this.exportMenuItem.Name = "exportMenuItem";
          this.exportMenuItem.Size = new System.Drawing.Size(116, 22);
          this.exportMenuItem.Text = "Export...";
          this.exportMenuItem.Click += new System.EventHandler(this.ExportFile_Command);
          // 
          // exitMenuItem
          // 
          this.exitMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitMenuItem.Image")));
          this.exitMenuItem.Name = "exitMenuItem";
          this.exitMenuItem.Size = new System.Drawing.Size(116, 22);
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
            this.refreshButton,
            this.toolStripSeparator1,
            this.exitButton});
          this.toolStrip1.Location = new System.Drawing.Point(0, 24);
          this.toolStrip1.Name = "toolStrip1";
          this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
          this.toolStrip1.Size = new System.Drawing.Size(999, 39);
          this.toolStrip1.TabIndex = 10;
          this.toolStrip1.Text = "toolStrip1";
          // 
          // openButton
          // 
          this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.openButton.Enabled = false;
          this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
          this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.openButton.Name = "openButton";
          this.openButton.Size = new System.Drawing.Size(36, 36);
          this.openButton.Text = "&Open";
          this.openButton.Click += new System.EventHandler(this.OpenFile_Command);
          // 
          // exportButton
          // 
          this.exportButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
          this.exportButton.Enabled = false;
          this.exportButton.Image = ((System.Drawing.Image)(resources.GetObject("exportButton.Image")));
          this.exportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.exportButton.Name = "exportButton";
          this.exportButton.Size = new System.Drawing.Size(36, 36);
          this.exportButton.Text = "&Save";
          this.exportButton.Click += new System.EventHandler(this.ExportFile_Command);
          // 
          // toolStripSeparator
          // 
          this.toolStripSeparator.Name = "toolStripSeparator";
          this.toolStripSeparator.Size = new System.Drawing.Size(6, 39);
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
          // Form1
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(999, 540);
          this.Controls.Add(this.toolStrip1);
          this.Controls.Add(this.ststatus);
          this.Controls.Add(this.menuMain);
          this.Controls.Add(this.grpLocal);
          this.Controls.Add(this.appTreeView);
          this.Controls.Add(this.grpmatch);
          this.Controls.Add(this.grpAppl);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MainMenuStrip = this.menuMain;
          this.Name = "Form1";
          this.Text = "System Profiler";
          this.Load += new System.EventHandler(this.Refresh_Command);
          this.grpLocal.ResumeLayout(false);
          this.ststatus.ResumeLayout(false);
          this.ststatus.PerformLayout();
          this.grpmatch.ResumeLayout(false);
          this.grpAppl.ResumeLayout(false);
          this.menuMain.ResumeLayout(false);
          this.menuMain.PerformLayout();
          this.toolStrip1.ResumeLayout(false);
          this.toolStrip1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView profileTreeView;
        private System.Windows.Forms.TreeView appTreeView;
        private System.Windows.Forms.GroupBox grpLocal;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.StatusStrip ststatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox grpmatch;
        private System.Windows.Forms.GroupBox grpAppl;
        private System.Windows.Forms.ComboBox appComboBox;
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
        private System.Windows.Forms.TreeView diffTreeView;
    }
}

