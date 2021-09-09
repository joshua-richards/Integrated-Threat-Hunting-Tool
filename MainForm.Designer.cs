
namespace Integrated_Threat_Hunting_Tool
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsEventViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsPerformanceMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openNewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.maximiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.versionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sysmonEventIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.windowsEventViewerButton = new System.Windows.Forms.ToolStripButton();
            this.windowsPerformanceMonitorButton = new System.Windows.Forms.ToolStripButton();
            this.filterInstanceIDToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.instanceIDToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.filterSourceToolStripTextBox = new System.Windows.Forms.ToolStripComboBox();
            this.sourceToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.filterTypeToolStripTextBox = new System.Windows.Forms.ToolStripComboBox();
            this.typeToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.filterToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripClearResultsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.datetimeToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.systemInfoMachineLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.systemInfoUserNameLabel = new System.Windows.Forms.Label();
            this.systemInfoOSLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gridViewInstructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.newToolStripMenuItem.Text = "New";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(125, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsEventViewerToolStripMenuItem,
            this.windowsPerformanceMonitorToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // windowsEventViewerToolStripMenuItem
            // 
            this.windowsEventViewerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("windowsEventViewerToolStripMenuItem.Image")));
            this.windowsEventViewerToolStripMenuItem.Name = "windowsEventViewerToolStripMenuItem";
            this.windowsEventViewerToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.windowsEventViewerToolStripMenuItem.Text = "Windows Event Viewer";
            this.windowsEventViewerToolStripMenuItem.Click += new System.EventHandler(this.windowsEventViewerToolStripMenuItem_Click);
            // 
            // windowsPerformanceMonitorToolStripMenuItem
            // 
            this.windowsPerformanceMonitorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("windowsPerformanceMonitorToolStripMenuItem.Image")));
            this.windowsPerformanceMonitorToolStripMenuItem.Name = "windowsPerformanceMonitorToolStripMenuItem";
            this.windowsPerformanceMonitorToolStripMenuItem.Size = new System.Drawing.Size(297, 26);
            this.windowsPerformanceMonitorToolStripMenuItem.Text = "Windows Performance Monitor";
            this.windowsPerformanceMonitorToolStripMenuItem.Click += new System.EventHandler(this.windowsPerformanceMonitorToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.openNewWindowToolStripMenuItem,
            this.toolStripSeparator4,
            this.clearAllParametersToolStripMenuItem,
            this.toolStripSeparator1,
            this.maximiseToolStripMenuItem,
            this.minimiseToolStripMenuItem,
            this.windowedToolStripMenuItem});
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(78, 24);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.homeToolStripMenuItem.Text = "Home";
            // 
            // openNewWindowToolStripMenuItem
            // 
            this.openNewWindowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panel1ToolStripMenuItem});
            this.openNewWindowToolStripMenuItem.Name = "openNewWindowToolStripMenuItem";
            this.openNewWindowToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.openNewWindowToolStripMenuItem.Text = "Open New Window";
            // 
            // panel1ToolStripMenuItem
            // 
            this.panel1ToolStripMenuItem.Name = "panel1ToolStripMenuItem";
            this.panel1ToolStripMenuItem.Size = new System.Drawing.Size(209, 26);
            this.panel1ToolStripMenuItem.Text = "Machine Learning";
            this.panel1ToolStripMenuItem.Click += new System.EventHandler(this.panel1ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(222, 6);
            // 
            // clearAllParametersToolStripMenuItem
            // 
            this.clearAllParametersToolStripMenuItem.Name = "clearAllParametersToolStripMenuItem";
            this.clearAllParametersToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.clearAllParametersToolStripMenuItem.Text = "Clear All Parameters";
            this.clearAllParametersToolStripMenuItem.Click += new System.EventHandler(this.clearAllParametersToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // maximiseToolStripMenuItem
            // 
            this.maximiseToolStripMenuItem.Name = "maximiseToolStripMenuItem";
            this.maximiseToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.maximiseToolStripMenuItem.Text = "Maximise";
            this.maximiseToolStripMenuItem.Click += new System.EventHandler(this.maximiseToolStripMenuItem_Click);
            // 
            // minimiseToolStripMenuItem
            // 
            this.minimiseToolStripMenuItem.Name = "minimiseToolStripMenuItem";
            this.minimiseToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.minimiseToolStripMenuItem.Text = "Minimise";
            this.minimiseToolStripMenuItem.Click += new System.EventHandler(this.minimiseToolStripMenuItem_Click);
            // 
            // windowedToolStripMenuItem
            // 
            this.windowedToolStripMenuItem.Name = "windowedToolStripMenuItem";
            this.windowedToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.windowedToolStripMenuItem.Text = "Windowed";
            this.windowedToolStripMenuItem.Click += new System.EventHandler(this.windowedToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.versionToolStripMenuItem,
            this.guideToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // versionToolStripMenuItem
            // 
            this.versionToolStripMenuItem.Name = "versionToolStripMenuItem";
            this.versionToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.versionToolStripMenuItem.Text = "Version";
            this.versionToolStripMenuItem.Click += new System.EventHandler(this.versionToolStripMenuItem_Click);
            // 
            // guideToolStripMenuItem
            // 
            this.guideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sysmonEventIDsToolStripMenuItem,
            this.gridViewInstructionsToolStripMenuItem});
            this.guideToolStripMenuItem.Name = "guideToolStripMenuItem";
            this.guideToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.guideToolStripMenuItem.Text = "How to Use Guide";
            // 
            // sysmonEventIDsToolStripMenuItem
            // 
            this.sysmonEventIDsToolStripMenuItem.Name = "sysmonEventIDsToolStripMenuItem";
            this.sysmonEventIDsToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.sysmonEventIDsToolStripMenuItem.Text = "Sysmon Event IDs";
            this.sysmonEventIDsToolStripMenuItem.Click += new System.EventHandler(this.sysmonEventIDsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressLabel,
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 680);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1024, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressLabel
            // 
            this.toolStripProgressLabel.Name = "toolStripProgressLabel";
            this.toolStripProgressLabel.Size = new System.Drawing.Size(68, 20);
            this.toolStripProgressLabel.Text = "Progress:";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(49, 20);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 18);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Integrated Threat Hunting Tool is running";
            this.notifyIcon1.Visible = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsEventViewerButton,
            this.windowsPerformanceMonitorButton,
            this.filterInstanceIDToolStripTextBox,
            this.instanceIDToolStripLabel,
            this.filterSourceToolStripTextBox,
            this.sourceToolStripLabel,
            this.filterTypeToolStripTextBox,
            this.typeToolStripLabel,
            this.filterToolStripButton,
            this.toolStripClearResultsButton,
            this.toolStripSeparator3,
            this.datetimeToolStripLabel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1024, 47);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // windowsEventViewerButton
            // 
            this.windowsEventViewerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.windowsEventViewerButton.Image = ((System.Drawing.Image)(resources.GetObject("windowsEventViewerButton.Image")));
            this.windowsEventViewerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.windowsEventViewerButton.Name = "windowsEventViewerButton";
            this.windowsEventViewerButton.Size = new System.Drawing.Size(44, 44);
            this.windowsEventViewerButton.Text = "Windows Event Viewer";
            this.windowsEventViewerButton.Click += new System.EventHandler(this.windowsEventViewerButton_Click);
            // 
            // windowsPerformanceMonitorButton
            // 
            this.windowsPerformanceMonitorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.windowsPerformanceMonitorButton.Image = ((System.Drawing.Image)(resources.GetObject("windowsPerformanceMonitorButton.Image")));
            this.windowsPerformanceMonitorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.windowsPerformanceMonitorButton.Name = "windowsPerformanceMonitorButton";
            this.windowsPerformanceMonitorButton.Size = new System.Drawing.Size(44, 44);
            this.windowsPerformanceMonitorButton.Text = "Windows Performance Monitor";
            this.windowsPerformanceMonitorButton.Click += new System.EventHandler(this.windowsPerformanceMonitorButton_Click);
            // 
            // filterInstanceIDToolStripTextBox
            // 
            this.filterInstanceIDToolStripTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.filterInstanceIDToolStripTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.filterInstanceIDToolStripTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.filterInstanceIDToolStripTextBox.Name = "filterInstanceIDToolStripTextBox";
            this.filterInstanceIDToolStripTextBox.Size = new System.Drawing.Size(125, 47);
            this.filterInstanceIDToolStripTextBox.ToolTipText = "Enter an Instance/Event ID (Not Required)";
            // 
            // instanceIDToolStripLabel
            // 
            this.instanceIDToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.instanceIDToolStripLabel.Name = "instanceIDToolStripLabel";
            this.instanceIDToolStripLabel.Size = new System.Drawing.Size(85, 44);
            this.instanceIDToolStripLabel.Text = "Instance ID:";
            // 
            // filterSourceToolStripTextBox
            // 
            this.filterSourceToolStripTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.filterSourceToolStripTextBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterSourceToolStripTextBox.DropDownWidth = 300;
            this.filterSourceToolStripTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.filterSourceToolStripTextBox.Name = "filterSourceToolStripTextBox";
            this.filterSourceToolStripTextBox.Size = new System.Drawing.Size(200, 47);
            this.filterSourceToolStripTextBox.Sorted = true;
            this.filterSourceToolStripTextBox.ToolTipText = "Select the log sources that you want to view";
            // 
            // sourceToolStripLabel
            // 
            this.sourceToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.sourceToolStripLabel.Name = "sourceToolStripLabel";
            this.sourceToolStripLabel.Size = new System.Drawing.Size(57, 44);
            this.sourceToolStripLabel.Text = "Source:";
            // 
            // filterTypeToolStripTextBox
            // 
            this.filterTypeToolStripTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.filterTypeToolStripTextBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterTypeToolStripTextBox.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.filterTypeToolStripTextBox.Items.AddRange(new object[] {
            "Application",
            "Security",
            "System",
            "Sysmon"});
            this.filterTypeToolStripTextBox.Name = "filterTypeToolStripTextBox";
            this.filterTypeToolStripTextBox.Size = new System.Drawing.Size(200, 47);
            this.filterTypeToolStripTextBox.ToolTipText = "Select the log types that you want to view";
            this.filterTypeToolStripTextBox.DropDownClosed += new System.EventHandler(this.filterTypeToolStripTextBox_DropDownClosed);
            // 
            // typeToolStripLabel
            // 
            this.typeToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.typeToolStripLabel.Name = "typeToolStripLabel";
            this.typeToolStripLabel.Size = new System.Drawing.Size(43, 44);
            this.typeToolStripLabel.Text = "Type:";
            // 
            // filterToolStripButton
            // 
            this.filterToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.filterToolStripButton.AutoSize = false;
            this.filterToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.filterToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("filterToolStripButton.Image")));
            this.filterToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.filterToolStripButton.Name = "filterToolStripButton";
            this.filterToolStripButton.Size = new System.Drawing.Size(30, 25);
            this.filterToolStripButton.Text = "Filter";
            this.filterToolStripButton.ToolTipText = "Filter (This may take a few moments)";
            this.filterToolStripButton.Click += new System.EventHandler(this.filterToolStripButton_Click);
            // 
            // toolStripClearResultsButton
            // 
            this.toolStripClearResultsButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripClearResultsButton.AutoSize = false;
            this.toolStripClearResultsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripClearResultsButton.Image = ((System.Drawing.Image)(resources.GetObject("toolStripClearResultsButton.Image")));
            this.toolStripClearResultsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripClearResultsButton.Name = "toolStripClearResultsButton";
            this.toolStripClearResultsButton.Size = new System.Drawing.Size(30, 25);
            this.toolStripClearResultsButton.Text = "Clear Results";
            this.toolStripClearResultsButton.Click += new System.EventHandler(this.toolStripClearResultsButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 47);
            // 
            // datetimeToolStripLabel
            // 
            this.datetimeToolStripLabel.Name = "datetimeToolStripLabel";
            this.datetimeToolStripLabel.Size = new System.Drawing.Size(74, 44);
            this.datetimeToolStripLabel.Text = "DateTime";
            // 
            // systemInfoMachineLabel
            // 
            this.systemInfoMachineLabel.AutoSize = true;
            this.systemInfoMachineLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemInfoMachineLabel.Location = new System.Drawing.Point(6, 29);
            this.systemInfoMachineLabel.Name = "systemInfoMachineLabel";
            this.systemInfoMachineLabel.Size = new System.Drawing.Size(171, 18);
            this.systemInfoMachineLabel.TabIndex = 3;
            this.systemInfoMachineLabel.Text = "systemInfoMachineLabel";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.systemInfoUserNameLabel);
            this.groupBox1.Controls.Add(this.systemInfoOSLabel);
            this.groupBox1.Controls.Add(this.systemInfoMachineLabel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1000, 113);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System Information";
            // 
            // systemInfoUserNameLabel
            // 
            this.systemInfoUserNameLabel.AutoSize = true;
            this.systemInfoUserNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemInfoUserNameLabel.Location = new System.Drawing.Point(6, 84);
            this.systemInfoUserNameLabel.Name = "systemInfoUserNameLabel";
            this.systemInfoUserNameLabel.Size = new System.Drawing.Size(187, 18);
            this.systemInfoUserNameLabel.TabIndex = 5;
            this.systemInfoUserNameLabel.Text = "systemInfoUserNameLabel";
            // 
            // systemInfoOSLabel
            // 
            this.systemInfoOSLabel.AutoSize = true;
            this.systemInfoOSLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemInfoOSLabel.Location = new System.Drawing.Point(6, 57);
            this.systemInfoOSLabel.Name = "systemInfoOSLabel";
            this.systemInfoOSLabel.Size = new System.Drawing.Size(137, 18);
            this.systemInfoOSLabel.TabIndex = 4;
            this.systemInfoOSLabel.Text = "systemInfoOSLabel";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 208);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(999, 469);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // gridViewInstructionsToolStripMenuItem
            // 
            this.gridViewInstructionsToolStripMenuItem.Name = "gridViewInstructionsToolStripMenuItem";
            this.gridViewInstructionsToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.gridViewInstructionsToolStripMenuItem.Text = "Grid View Instructions";
            this.gridViewInstructionsToolStripMenuItem.Click += new System.EventHandler(this.gridViewInstructionsToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 706);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1042, 753);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Integrated Threat Hunting Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openNewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panel1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maximiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowedToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripProgressLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton windowsEventViewerButton;
        private System.Windows.Forms.Label systemInfoMachineLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label systemInfoUserNameLabel;
        private System.Windows.Forms.Label systemInfoOSLabel;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsEventViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsPerformanceMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem guideToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem versionToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton filterToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox filterTypeToolStripTextBox;
        private System.Windows.Forms.ToolStripButton toolStripClearResultsButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripTextBox filterInstanceIDToolStripTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripLabel instanceIDToolStripLabel;
        private System.Windows.Forms.ToolStripLabel typeToolStripLabel;
        private System.Windows.Forms.ToolStripButton windowsPerformanceMonitorButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem clearAllParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox filterSourceToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel sourceToolStripLabel;
        private System.Windows.Forms.ToolStripLabel datetimeToolStripLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem sysmonEventIDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridViewInstructionsToolStripMenuItem;
    }
}

