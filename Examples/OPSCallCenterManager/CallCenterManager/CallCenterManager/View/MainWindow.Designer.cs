using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CallCenterManager.Properties;
using CallCenterManager.View.Control;

namespace CallCenterManager.View
{
	partial class MainWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Internal", 8, 8);
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("External", 9, 9);
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Inbound", 10, 10);
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Outbound", 11, 11);
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Calls", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Phonebook");
			this.il_UserImages = new System.Windows.Forms.ImageList(this.components);
			this.il_CallImages = new System.Windows.Forms.ImageList(this.components);
			this.cms_CallMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmi_Listen = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_StopListening = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmi_Join = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_Leave = new System.Windows.Forms.ToolStripMenuItem();
			this.t_Timer = new System.Windows.Forms.Timer(this.components);
			this.sfd_SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.p_Main = new System.Windows.Forms.Panel();
			this.p_Base = new System.Windows.Forms.Panel();
			this.p_Upper = new System.Windows.Forms.Panel();
			this.p_Right = new System.Windows.Forms.Panel();
			this.tc_Statistics = new System.Windows.Forms.TabControl();
			this.tp_Summary = new System.Windows.Forms.TabPage();
			this.c_Statistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.tp_CallsInProgress = new System.Windows.Forms.TabPage();
			this.c_CallsInProgressStatistics = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.s_Splitter_2 = new System.Windows.Forms.Splitter();
			this.p_Left = new System.Windows.Forms.Panel();
			this.tc_Tabs = new System.Windows.Forms.TabControl();
			this.tp_Sessions = new System.Windows.Forms.TabPage();
			this.p_Session = new System.Windows.Forms.Panel();
			this.tv_Sessions = new CallCenterManager.View.Control.FlickerFreeTreeView();
			this.s_Splitter_3 = new System.Windows.Forms.Splitter();
			this.c_UserChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.p_SessionUpper = new System.Windows.Forms.Panel();
			this.btn_Join = new System.Windows.Forms.Button();
			this.btn_Listen = new System.Windows.Forms.Button();
			this.tp_PhoneBook = new System.Windows.Forms.TabPage();
			this.tv_PhoneBook = new CallCenterManager.View.Control.FlickerFreeTreeView();
			this.s_Splitter_1 = new System.Windows.Forms.Splitter();
			this.p_Lower = new System.Windows.Forms.Panel();
			this.ss_StatusStrip = new System.Windows.Forms.StatusStrip();
			this.ms_Menu = new System.Windows.Forms.MenuStrip();
			this.tsmi_File = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_ExportPhonebook = new System.Windows.Forms.ToolStripMenuItem();
			this.tss_Separator_1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmi_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_Manage = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_ConnectToServer = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_Disconnect = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_OpenOnlineDocumentation = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi_About = new System.Windows.Forms.ToolStripMenuItem();
			this.cms_CallMenu.SuspendLayout();
			this.p_Main.SuspendLayout();
			this.p_Base.SuspendLayout();
			this.p_Upper.SuspendLayout();
			this.p_Right.SuspendLayout();
			this.tc_Statistics.SuspendLayout();
			this.tp_Summary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_Statistics)).BeginInit();
			this.tp_CallsInProgress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_CallsInProgressStatistics)).BeginInit();
			this.p_Left.SuspendLayout();
			this.tc_Tabs.SuspendLayout();
			this.tp_Sessions.SuspendLayout();
			this.p_Session.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.c_UserChart)).BeginInit();
			this.p_SessionUpper.SuspendLayout();
			this.tp_PhoneBook.SuspendLayout();
			this.ms_Menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// il_UserImages
			// 
			this.il_UserImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il_UserImages.ImageStream")));
			this.il_UserImages.TransparentColor = System.Drawing.Color.Transparent;
			this.il_UserImages.Images.SetKeyName(0, "phonebook_icon.png");
			this.il_UserImages.Images.SetKeyName(1, "user_icon.png");
			this.il_UserImages.Images.SetKeyName(2, "tag_blue.png");
			// 
			// il_CallImages
			// 
			this.il_CallImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il_CallImages.ImageStream")));
			this.il_CallImages.TransparentColor = System.Drawing.Color.Transparent;
			this.il_CallImages.Images.SetKeyName(0, "mobile_phone.png");
			this.il_CallImages.Images.SetKeyName(1, "busy_btn.png");
			this.il_CallImages.Images.SetKeyName(2, "completed_btn.png");
			this.il_CallImages.Images.SetKeyName(3, "error_btn.png");
			this.il_CallImages.Images.SetKeyName(4, "hold_btn.png");
			this.il_CallImages.Images.SetKeyName(5, "incall_btn.png");
			this.il_CallImages.Images.SetKeyName(6, "ringing_btn.png");
			this.il_CallImages.Images.SetKeyName(7, "transfer_btn.png");
			this.il_CallImages.Images.SetKeyName(8, "arrow_right.png");
			this.il_CallImages.Images.SetKeyName(9, "red_left.ico");
			this.il_CallImages.Images.SetKeyName(10, "blue_right.ico");
			this.il_CallImages.Images.SetKeyName(11, "purple_right.ico");
			// 
			// cms_CallMenu
			// 
			this.cms_CallMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Listen,
            this.tsmi_StopListening,
            this.toolStripSeparator2,
            this.tsmi_Join,
            this.tsmi_Leave});
			this.cms_CallMenu.Name = "cms_CallMenu";
			this.cms_CallMenu.Size = new System.Drawing.Size(139, 98);
			// 
			// tsmi_Listen
			// 
			this.tsmi_Listen.Name = "tsmi_Listen";
			this.tsmi_Listen.Size = new System.Drawing.Size(138, 22);
			this.tsmi_Listen.Text = "Listen";
			this.tsmi_Listen.Click += new System.EventHandler(this.tsmi_Listen_Click);
			// 
			// tsmi_StopListening
			// 
			this.tsmi_StopListening.Name = "tsmi_StopListening";
			this.tsmi_StopListening.Size = new System.Drawing.Size(138, 22);
			this.tsmi_StopListening.Text = "Stop listening";
			this.tsmi_StopListening.Click += new System.EventHandler(this.tsmi_StopListening_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(135, 6);
			// 
			// tsmi_Join
			// 
			this.tsmi_Join.Name = "tsmi_Join";
			this.tsmi_Join.Size = new System.Drawing.Size(138, 22);
			this.tsmi_Join.Text = "Join";
			this.tsmi_Join.Click += new System.EventHandler(this.tsmi_Join_Click);
			// 
			// tsmi_Leave
			// 
			this.tsmi_Leave.Name = "tsmi_Leave";
			this.tsmi_Leave.Size = new System.Drawing.Size(138, 22);
			this.tsmi_Leave.Text = "Leave";
			this.tsmi_Leave.Click += new System.EventHandler(this.tsmi_Leave_Click);
			// 
			// t_Timer
			// 
			this.t_Timer.Interval = 1000;
			this.t_Timer.Tick += new System.EventHandler(this.t_Timer_Tick);
			// 
			// sfd_SaveFileDialog
			// 
			this.sfd_SaveFileDialog.Filter = "CSV files|*.csv|All files|*.*";
			// 
			// p_Main
			// 
			this.p_Main.Controls.Add(this.p_Base);
			this.p_Main.Controls.Add(this.ss_StatusStrip);
			this.p_Main.Controls.Add(this.ms_Menu);
			this.p_Main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Main.Location = new System.Drawing.Point(0, 0);
			this.p_Main.Name = "p_Main";
			this.p_Main.Size = new System.Drawing.Size(1098, 633);
			this.p_Main.TabIndex = 0;
			// 
			// p_Base
			// 
			this.p_Base.Controls.Add(this.p_Upper);
			this.p_Base.Controls.Add(this.s_Splitter_1);
			this.p_Base.Controls.Add(this.p_Lower);
			this.p_Base.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Base.Location = new System.Drawing.Point(0, 24);
			this.p_Base.Name = "p_Base";
			this.p_Base.Size = new System.Drawing.Size(1098, 587);
			this.p_Base.TabIndex = 2;
			// 
			// p_Upper
			// 
			this.p_Upper.Controls.Add(this.p_Right);
			this.p_Upper.Controls.Add(this.s_Splitter_2);
			this.p_Upper.Controls.Add(this.p_Left);
			this.p_Upper.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Upper.Location = new System.Drawing.Point(0, 0);
			this.p_Upper.Name = "p_Upper";
			this.p_Upper.Size = new System.Drawing.Size(1098, 557);
			this.p_Upper.TabIndex = 2;
			// 
			// p_Right
			// 
			this.p_Right.Controls.Add(this.tc_Statistics);
			this.p_Right.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Right.Location = new System.Drawing.Point(234, 0);
			this.p_Right.Name = "p_Right";
			this.p_Right.Size = new System.Drawing.Size(864, 557);
			this.p_Right.TabIndex = 2;
			// 
			// tc_Statistics
			// 
			this.tc_Statistics.Controls.Add(this.tp_Summary);
			this.tc_Statistics.Controls.Add(this.tp_CallsInProgress);
			this.tc_Statistics.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tc_Statistics.Location = new System.Drawing.Point(0, 0);
			this.tc_Statistics.Name = "tc_Statistics";
			this.tc_Statistics.SelectedIndex = 0;
			this.tc_Statistics.Size = new System.Drawing.Size(864, 557);
			this.tc_Statistics.TabIndex = 0;
			// 
			// tp_Summary
			// 
			this.tp_Summary.Controls.Add(this.c_Statistics);
			this.tp_Summary.Location = new System.Drawing.Point(4, 22);
			this.tp_Summary.Name = "tp_Summary";
			this.tp_Summary.Padding = new System.Windows.Forms.Padding(3);
			this.tp_Summary.Size = new System.Drawing.Size(856, 531);
			this.tp_Summary.TabIndex = 0;
			this.tp_Summary.Text = "Summary";
			this.tp_Summary.UseVisualStyleBackColor = true;
			// 
			// c_Statistics
			// 
			chartArea5.AxisX.Minimum = 0D;
			chartArea5.AxisX.Title = "sec";
			chartArea5.CursorX.IsUserEnabled = true;
			chartArea5.CursorX.IsUserSelectionEnabled = true;
			chartArea5.CursorY.IsUserEnabled = true;
			chartArea5.CursorY.IsUserSelectionEnabled = true;
			chartArea5.Name = "ca_ChartArea";
			this.c_Statistics.ChartAreas.Add(chartArea5);
			this.c_Statistics.Dock = System.Windows.Forms.DockStyle.Fill;
			legend5.Name = "l_Legend";
			this.c_Statistics.Legends.Add(legend5);
			this.c_Statistics.Location = new System.Drawing.Point(3, 3);
			this.c_Statistics.Name = "c_Statistics";
			series4.BorderWidth = 3;
			series4.ChartArea = "ca_ChartArea";
			series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
			series4.Legend = "l_Legend";
			series4.Name = "Number of calls";
			series5.BorderWidth = 3;
			series5.ChartArea = "ca_ChartArea";
			series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
			series5.Legend = "l_Legend";
			series5.Name = "Number of dropped calls";
			series5.YValuesPerPoint = 2;
			this.c_Statistics.Series.Add(series4);
			this.c_Statistics.Series.Add(series5);
			this.c_Statistics.Size = new System.Drawing.Size(850, 525);
			this.c_Statistics.TabIndex = 0;
			this.c_Statistics.Text = "Summary";
			// 
			// tp_CallsInProgress
			// 
			this.tp_CallsInProgress.Controls.Add(this.c_CallsInProgressStatistics);
			this.tp_CallsInProgress.Location = new System.Drawing.Point(4, 22);
			this.tp_CallsInProgress.Name = "tp_CallsInProgress";
			this.tp_CallsInProgress.Padding = new System.Windows.Forms.Padding(3);
			this.tp_CallsInProgress.Size = new System.Drawing.Size(856, 531);
			this.tp_CallsInProgress.TabIndex = 1;
			this.tp_CallsInProgress.Text = "Calls in progress";
			this.tp_CallsInProgress.UseVisualStyleBackColor = true;
			// 
			// c_CallsInProgressStatistics
			// 
			chartArea4.AxisX.Minimum = 0D;
			chartArea4.AxisX.Title = "sec";
			chartArea4.CursorX.IsUserEnabled = true;
			chartArea4.CursorX.IsUserSelectionEnabled = true;
			chartArea4.CursorY.IsUserEnabled = true;
			chartArea4.CursorY.IsUserSelectionEnabled = true;
			chartArea4.Name = "ca_ChartArea";
			this.c_CallsInProgressStatistics.ChartAreas.Add(chartArea4);
			this.c_CallsInProgressStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
			legend4.Name = "l_Legend";
			this.c_CallsInProgressStatistics.Legends.Add(legend4);
			this.c_CallsInProgressStatistics.Location = new System.Drawing.Point(3, 3);
			this.c_CallsInProgressStatistics.Name = "c_CallsInProgressStatistics";
			this.c_CallsInProgressStatistics.Size = new System.Drawing.Size(850, 525);
			this.c_CallsInProgressStatistics.TabIndex = 1;
			this.c_CallsInProgressStatistics.Text = "CallsInProgress";
			// 
			// s_Splitter_2
			// 
			this.s_Splitter_2.Location = new System.Drawing.Point(231, 0);
			this.s_Splitter_2.Name = "s_Splitter_2";
			this.s_Splitter_2.Size = new System.Drawing.Size(3, 557);
			this.s_Splitter_2.TabIndex = 1;
			this.s_Splitter_2.TabStop = false;
			// 
			// p_Left
			// 
			this.p_Left.Controls.Add(this.tc_Tabs);
			this.p_Left.Dock = System.Windows.Forms.DockStyle.Left;
			this.p_Left.Location = new System.Drawing.Point(0, 0);
			this.p_Left.Name = "p_Left";
			this.p_Left.Size = new System.Drawing.Size(231, 557);
			this.p_Left.TabIndex = 0;
			// 
			// tc_Tabs
			// 
			this.tc_Tabs.Controls.Add(this.tp_Sessions);
			this.tc_Tabs.Controls.Add(this.tp_PhoneBook);
			this.tc_Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tc_Tabs.Location = new System.Drawing.Point(0, 0);
			this.tc_Tabs.Name = "tc_Tabs";
			this.tc_Tabs.SelectedIndex = 0;
			this.tc_Tabs.Size = new System.Drawing.Size(231, 557);
			this.tc_Tabs.TabIndex = 0;
			// 
			// tp_Sessions
			// 
			this.tp_Sessions.Controls.Add(this.p_Session);
			this.tp_Sessions.Controls.Add(this.p_SessionUpper);
			this.tp_Sessions.Location = new System.Drawing.Point(4, 22);
			this.tp_Sessions.Name = "tp_Sessions";
			this.tp_Sessions.Padding = new System.Windows.Forms.Padding(3);
			this.tp_Sessions.Size = new System.Drawing.Size(223, 531);
			this.tp_Sessions.TabIndex = 1;
			this.tp_Sessions.Text = "Calls";
			this.tp_Sessions.UseVisualStyleBackColor = true;
			// 
			// p_Session
			// 
			this.p_Session.Controls.Add(this.tv_Sessions);
			this.p_Session.Controls.Add(this.s_Splitter_3);
			this.p_Session.Controls.Add(this.c_UserChart);
			this.p_Session.Dock = System.Windows.Forms.DockStyle.Fill;
			this.p_Session.Location = new System.Drawing.Point(3, 37);
			this.p_Session.Name = "p_Session";
			this.p_Session.Size = new System.Drawing.Size(217, 491);
			this.p_Session.TabIndex = 1;
			// 
			// tv_Sessions
			// 
			this.tv_Sessions.ContextMenuStrip = this.cms_CallMenu;
			this.tv_Sessions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv_Sessions.ImageIndex = 0;
			this.tv_Sessions.ImageList = this.il_CallImages;
			this.tv_Sessions.Location = new System.Drawing.Point(0, 0);
			this.tv_Sessions.Name = "tv_Sessions";
			treeNode7.ImageIndex = 8;
			treeNode7.Name = "internal_root_node";
			treeNode7.SelectedImageIndex = 8;
			treeNode7.Text = "Internal";
			treeNode8.ImageIndex = 9;
			treeNode8.Name = "external_root_node";
			treeNode8.SelectedImageIndex = 9;
			treeNode8.Text = "External";
			treeNode9.ImageIndex = 10;
			treeNode9.Name = "inbound_root_node";
			treeNode9.SelectedImageIndex = 10;
			treeNode9.Text = "Inbound";
			treeNode10.ImageIndex = 11;
			treeNode10.Name = "outbound_root_node";
			treeNode10.SelectedImageIndex = 11;
			treeNode10.Text = "Outbound";
			treeNode11.Name = "sessions_root_node";
			treeNode11.SelectedImageIndex = 0;
			treeNode11.StateImageKey = "(none)";
			treeNode11.Text = "Calls";
			this.tv_Sessions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11});
			this.tv_Sessions.SelectedImageIndex = 0;
			this.tv_Sessions.ShowRootLines = false;
			this.tv_Sessions.Size = new System.Drawing.Size(217, 371);
			this.tv_Sessions.TabIndex = 5;
			this.tv_Sessions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_Sessions_NodeMouseClick);
			// 
			// s_Splitter_3
			// 
			this.s_Splitter_3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.s_Splitter_3.Location = new System.Drawing.Point(0, 371);
			this.s_Splitter_3.Name = "s_Splitter_3";
			this.s_Splitter_3.Size = new System.Drawing.Size(217, 8);
			this.s_Splitter_3.TabIndex = 4;
			this.s_Splitter_3.TabStop = false;
			// 
			// c_UserChart
			// 
			chartArea6.CursorX.IsUserEnabled = true;
			chartArea6.CursorX.IsUserSelectionEnabled = true;
			chartArea6.CursorY.IsUserEnabled = true;
			chartArea6.CursorY.IsUserSelectionEnabled = true;
			chartArea6.Name = "ca_ChartArea";
			this.c_UserChart.ChartAreas.Add(chartArea6);
			this.c_UserChart.Dock = System.Windows.Forms.DockStyle.Bottom;
			legend6.Name = "l_Legend";
			this.c_UserChart.Legends.Add(legend6);
			this.c_UserChart.Location = new System.Drawing.Point(0, 379);
			this.c_UserChart.Name = "c_UserChart";
			series6.BorderWidth = 3;
			series6.ChartArea = "ca_ChartArea";
			series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
			series6.IsValueShownAsLabel = true;
			series6.Legend = "l_Legend";
			series6.LegendText = "#PERCENT{P1}";
			series6.Name = "User states";
			this.c_UserChart.Series.Add(series6);
			this.c_UserChart.Size = new System.Drawing.Size(217, 112);
			this.c_UserChart.TabIndex = 3;
			this.c_UserChart.Text = "UserStates";
			// 
			// p_SessionUpper
			// 
			this.p_SessionUpper.Controls.Add(this.btn_Join);
			this.p_SessionUpper.Controls.Add(this.btn_Listen);
			this.p_SessionUpper.Dock = System.Windows.Forms.DockStyle.Top;
			this.p_SessionUpper.Location = new System.Drawing.Point(3, 3);
			this.p_SessionUpper.Name = "p_SessionUpper";
			this.p_SessionUpper.Size = new System.Drawing.Size(217, 34);
			this.p_SessionUpper.TabIndex = 0;
			// 
			// btn_Join
			// 
			this.btn_Join.Enabled = false;
			this.btn_Join.Location = new System.Drawing.Point(89, 5);
			this.btn_Join.Name = "btn_Join";
			this.btn_Join.Size = new System.Drawing.Size(78, 23);
			this.btn_Join.TabIndex = 1;
			this.btn_Join.Text = "Join";
			this.btn_Join.UseVisualStyleBackColor = true;
			this.btn_Join.Click += new System.EventHandler(this.btn_Join_Click);
			// 
			// btn_Listen
			// 
			this.btn_Listen.Enabled = false;
			this.btn_Listen.Location = new System.Drawing.Point(5, 5);
			this.btn_Listen.Name = "btn_Listen";
			this.btn_Listen.Size = new System.Drawing.Size(78, 23);
			this.btn_Listen.TabIndex = 0;
			this.btn_Listen.Text = "Listen";
			this.btn_Listen.UseVisualStyleBackColor = true;
			this.btn_Listen.Click += new System.EventHandler(this.btn_Listen_Click);
			// 
			// tp_PhoneBook
			// 
			this.tp_PhoneBook.Controls.Add(this.tv_PhoneBook);
			this.tp_PhoneBook.Location = new System.Drawing.Point(4, 22);
			this.tp_PhoneBook.Name = "tp_PhoneBook";
			this.tp_PhoneBook.Padding = new System.Windows.Forms.Padding(3);
			this.tp_PhoneBook.Size = new System.Drawing.Size(223, 531);
			this.tp_PhoneBook.TabIndex = 0;
			this.tp_PhoneBook.Text = "Phonebook";
			this.tp_PhoneBook.UseVisualStyleBackColor = true;
			// 
			// tv_PhoneBook
			// 
			this.tv_PhoneBook.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tv_PhoneBook.ImageIndex = 0;
			this.tv_PhoneBook.ImageList = this.il_UserImages;
			this.tv_PhoneBook.Location = new System.Drawing.Point(3, 3);
			this.tv_PhoneBook.Name = "tv_PhoneBook";
			treeNode1.Name = "phone_book_root_node";
			treeNode1.Text = "Phonebook";
			this.tv_PhoneBook.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.tv_PhoneBook.SelectedImageIndex = 0;
			this.tv_PhoneBook.ShowRootLines = false;
			this.tv_PhoneBook.Size = new System.Drawing.Size(217, 525);
			this.tv_PhoneBook.TabIndex = 3;
			this.tv_PhoneBook.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_PhoneBook_NodeMouseClick);
			// 
			// s_Splitter_1
			// 
			this.s_Splitter_1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.s_Splitter_1.Location = new System.Drawing.Point(0, 557);
			this.s_Splitter_1.Name = "s_Splitter_1";
			this.s_Splitter_1.Size = new System.Drawing.Size(1098, 3);
			this.s_Splitter_1.TabIndex = 2;
			this.s_Splitter_1.TabStop = false;
			// 
			// p_Lower
			// 
			this.p_Lower.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.p_Lower.Location = new System.Drawing.Point(0, 560);
			this.p_Lower.Name = "p_Lower";
			this.p_Lower.Size = new System.Drawing.Size(1098, 27);
			this.p_Lower.TabIndex = 0;
			this.p_Lower.Visible = false;
			// 
			// ss_StatusStrip
			// 
			this.ss_StatusStrip.Location = new System.Drawing.Point(0, 611);
			this.ss_StatusStrip.Name = "ss_StatusStrip";
			this.ss_StatusStrip.Size = new System.Drawing.Size(1098, 22);
			this.ss_StatusStrip.TabIndex = 1;
			this.ss_StatusStrip.Text = "statusStrip1";
			// 
			// ms_Menu
			// 
			this.ms_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_File,
            this.tsmi_Manage,
            this.tsmi_Help});
			this.ms_Menu.Location = new System.Drawing.Point(0, 0);
			this.ms_Menu.Name = "ms_Menu";
			this.ms_Menu.Size = new System.Drawing.Size(1098, 24);
			this.ms_Menu.TabIndex = 0;
			this.ms_Menu.Text = "menuStrip1";
			// 
			// tsmi_File
			// 
			this.tsmi_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ExportPhonebook,
            this.tss_Separator_1,
            this.tsmi_Exit});
			this.tsmi_File.Image = global::CallCenterManager.Properties.Resources.page_2;
			this.tsmi_File.Name = "tsmi_File";
			this.tsmi_File.Size = new System.Drawing.Size(51, 20);
			this.tsmi_File.Text = "File";
			// 
			// tsmi_ExportPhonebook
			// 
			this.tsmi_ExportPhonebook.Image = global::CallCenterManager.Properties.Resources.export_phonebook_icon;
			this.tsmi_ExportPhonebook.Name = "tsmi_ExportPhonebook";
			this.tsmi_ExportPhonebook.Size = new System.Drawing.Size(162, 22);
			this.tsmi_ExportPhonebook.Text = "Export phonebook";
			this.tsmi_ExportPhonebook.Click += new System.EventHandler(this.tsmi_tsmi_ExportPhonebook_Click);
			// 
			// tss_Separator_1
			// 
			this.tss_Separator_1.Name = "tss_Separator_1";
			this.tss_Separator_1.Size = new System.Drawing.Size(159, 6);
			// 
			// tsmi_Exit
			// 
			this.tsmi_Exit.Image = global::CallCenterManager.Properties.Resources.door_in;
			this.tsmi_Exit.Name = "tsmi_Exit";
			this.tsmi_Exit.Size = new System.Drawing.Size(162, 22);
			this.tsmi_Exit.Text = "Exit";
			this.tsmi_Exit.Click += new System.EventHandler(this.tsmi_Exit_Clicked);
			// 
			// tsmi_Manage
			// 
			this.tsmi_Manage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_ConnectToServer,
            this.tsmi_Disconnect});
			this.tsmi_Manage.Image = global::CallCenterManager.Properties.Resources.computer;
			this.tsmi_Manage.Name = "tsmi_Manage";
			this.tsmi_Manage.Size = new System.Drawing.Size(73, 20);
			this.tsmi_Manage.Text = "Manage";
			// 
			// tsmi_ConnectToServer
			// 
			this.tsmi_ConnectToServer.Image = global::CallCenterManager.Properties.Resources.connect;
			this.tsmi_ConnectToServer.Name = "tsmi_ConnectToServer";
			this.tsmi_ConnectToServer.Size = new System.Drawing.Size(161, 22);
			this.tsmi_ConnectToServer.Text = "Connect to server";
			this.tsmi_ConnectToServer.Click += new System.EventHandler(this.tsmi_ConnectToServerServer_Clicked);
			// 
			// tsmi_Disconnect
			// 
			this.tsmi_Disconnect.Image = global::CallCenterManager.Properties.Resources.disconnect;
			this.tsmi_Disconnect.Name = "tsmi_Disconnect";
			this.tsmi_Disconnect.Size = new System.Drawing.Size(161, 22);
			this.tsmi_Disconnect.Text = "Disconnect";
			this.tsmi_Disconnect.Click += new System.EventHandler(this.tsmi_Disconnect_Clicked);
			// 
			// tsmi_Help
			// 
			this.tsmi_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_OpenOnlineDocumentation,
            this.tsmi_About});
			this.tsmi_Help.Image = global::CallCenterManager.Properties.Resources.help;
			this.tsmi_Help.Name = "tsmi_Help";
			this.tsmi_Help.Size = new System.Drawing.Size(56, 20);
			this.tsmi_Help.Text = "Help";
			// 
			// tsmi_OpenOnlineDocumentation
			// 
			this.tsmi_OpenOnlineDocumentation.Image = global::CallCenterManager.Properties.Resources.page_white_text;
			this.tsmi_OpenOnlineDocumentation.Name = "tsmi_OpenOnlineDocumentation";
			this.tsmi_OpenOnlineDocumentation.Size = new System.Drawing.Size(205, 22);
			this.tsmi_OpenOnlineDocumentation.Text = "Open online documentation";
			this.tsmi_OpenOnlineDocumentation.Click += new System.EventHandler(this.tsmi_OpenOnlineDocumentation_Clicked);
			// 
			// tsmi_About
			// 
			this.tsmi_About.Image = global::CallCenterManager.Properties.Resources.exclamation;
			this.tsmi_About.Name = "tsmi_About";
			this.tsmi_About.Size = new System.Drawing.Size(205, 22);
			this.tsmi_About.Text = "About";
			this.tsmi_About.Click += new System.EventHandler(this.tsmi_About_Clicked);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(1098, 633);
			this.Controls.Add(this.p_Main);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.ms_Menu;
			this.Name = "MainWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Call Center Manager";
			this.cms_CallMenu.ResumeLayout(false);
			this.p_Main.ResumeLayout(false);
			this.p_Main.PerformLayout();
			this.p_Base.ResumeLayout(false);
			this.p_Upper.ResumeLayout(false);
			this.p_Right.ResumeLayout(false);
			this.tc_Statistics.ResumeLayout(false);
			this.tp_Summary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c_Statistics)).EndInit();
			this.tp_CallsInProgress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c_CallsInProgressStatistics)).EndInit();
			this.p_Left.ResumeLayout(false);
			this.tc_Tabs.ResumeLayout(false);
			this.tp_Sessions.ResumeLayout(false);
			this.p_Session.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.c_UserChart)).EndInit();
			this.p_SessionUpper.ResumeLayout(false);
			this.tp_PhoneBook.ResumeLayout(false);
			this.ms_Menu.ResumeLayout(false);
			this.ms_Menu.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Panel p_Main;
		private MenuStrip ms_Menu;
		private ToolStripMenuItem tsmi_Help;
		private ToolStripMenuItem tsmi_About;
		private Panel p_Base;
		private Panel p_Upper;
		private Panel p_Right;
		private Splitter s_Splitter_2;
		private Panel p_Left;
		private TabControl tc_Tabs;
		private TabPage tp_PhoneBook;
		private TabPage tp_Sessions;
		private Splitter s_Splitter_1;
		private Panel p_Lower;
		private StatusStrip ss_StatusStrip;
		private ToolStripMenuItem tsmi_Manage;
		private ToolStripMenuItem tsmi_OpenOnlineDocumentation;
		private ToolStripMenuItem tsmi_ConnectToServer;
		private ToolStripMenuItem tsmi_Disconnect;
		private ImageList il_UserImages;
		private ImageList il_CallImages;
        private ContextMenuStrip cms_CallMenu;
        private ToolStripMenuItem tsmi_Listen;
		private ToolStripMenuItem tsmi_Join;
		private ToolStripMenuItem tsmi_StopListening;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem tsmi_Leave;
		private Timer t_Timer;
		private FlickerFreeTreeView tv_PhoneBook;
		private ToolStripMenuItem tsmi_File;
		private ToolStripMenuItem tsmi_ExportPhonebook;
		private SaveFileDialog sfd_SaveFileDialog;
		private ToolStripSeparator tss_Separator_1;
		private ToolStripMenuItem tsmi_Exit;
		private TabControl tc_Statistics;
		private TabPage tp_Summary;
		private Chart c_Statistics;
		private Panel p_Session;
		private Panel p_SessionUpper;
		private Button btn_Join;
		private Button btn_Listen;
		private FlickerFreeTreeView tv_Sessions;
		private Splitter s_Splitter_3;
		private Chart c_UserChart;
		private TabPage tp_CallsInProgress;
		private Chart c_CallsInProgressStatistics;
	}
}

