﻿using Q3LogAnalyzer.Controls;

namespace Q3LogAnalyzer.Forms
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            Q3LogAnalyzer.Controls.GraphLib.ScaledViewerBase.GridSettings gridSettings1 = new Q3LogAnalyzer.Controls.GraphLib.ScaledViewerBase.GridSettings();
            Q3LogAnalyzer.Controls.GraphLib.ScaledViewerBase.GridSettings gridSettings2 = new Q3LogAnalyzer.Controls.GraphLib.ScaledViewerBase.GridSettings();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnLogOpen = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFilterLastGame = new System.Windows.Forms.ToolStripButton();
            this.btnFilterLast50Games = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFilterEmptyRecords = new System.Windows.Forms.ToolStripButton();
            this.ofdQ3Log = new System.Windows.Forms.OpenFileDialog();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpStatistic = new System.Windows.Forms.TabPage();
            this.scStatistic = new System.Windows.Forms.SplitContainer();
            this.lvStatistic = new Q3LogAnalyzer.Controls.ListViewEx();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pGraph = new System.Windows.Forms.Panel();
            this.legendControl = new Q3LogAnalyzer.Controls.GraphLib.LegendControl();
            this.graphViewer = new Q3LogAnalyzer.Controls.GraphLib.InteractiveGraphViewer();
            this.tpDetailed = new System.Windows.Forms.TabPage();
            this.lvDetailed = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpSummary = new System.Windows.Forms.TabPage();
            this.lvSummary = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpEfficiency = new System.Windows.Forms.TabPage();
            this.lvEfficiency = new Q3LogAnalyzer.Controls.ListViewEx();
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.slFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.slLoadingTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnColorizeStatistics = new Q3LogAnalyzer.Controls.ControlCheckBox();
            this.btnCollapsAll = new Q3LogAnalyzer.Controls.ControlButton();
            this.btnExpandAll = new Q3LogAnalyzer.Controls.ControlButton();
            this.tsMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpStatistic.SuspendLayout();
            this.scStatistic.Panel1.SuspendLayout();
            this.scStatistic.Panel2.SuspendLayout();
            this.scStatistic.SuspendLayout();
            this.pGraph.SuspendLayout();
            this.tpDetailed.SuspendLayout();
            this.tpSummary.SuspendLayout();
            this.tpEfficiency.SuspendLayout();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLogOpen,
            this.btnExport,
            this.toolStripSeparator1,
            this.btnFilterLastGame,
            this.btnFilterLast50Games,
            this.toolStripSeparator2,
            this.btnFilterEmptyRecords});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tsMain.Size = new System.Drawing.Size(762, 38);
            this.tsMain.TabIndex = 1;
            // 
            // btnLogOpen
            // 
            this.btnLogOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnLogOpen.Image")));
            this.btnLogOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogOpen.Name = "btnLogOpen";
            this.btnLogOpen.Size = new System.Drawing.Size(79, 35);
            this.btnLogOpen.Text = "Open log file";
            this.btnLogOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLogOpen.ToolTipText = "Open log file... (Ctrl+O, F3)";
            this.btnLogOpen.Click += new System.EventHandler(this.BtnLogOpenClick);
            // 
            // btnExport
            // 
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(71, 35);
            this.btnExport.Text = "Save to XLS";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // btnFilterLastGame
            // 
            this.btnFilterLastGame.CheckOnClick = true;
            this.btnFilterLastGame.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterLastGame.Image")));
            this.btnFilterLastGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilterLastGame.Name = "btnFilterLastGame";
            this.btnFilterLastGame.Size = new System.Drawing.Size(65, 35);
            this.btnFilterLastGame.Text = "Last game";
            this.btnFilterLastGame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilterLastGame.Click += new System.EventHandler(this.BtnFilterLastGamesClick);
            // 
            // btnFilterLast50Games
            // 
            this.btnFilterLast50Games.Checked = true;
            this.btnFilterLast50Games.CheckOnClick = true;
            this.btnFilterLast50Games.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnFilterLast50Games.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterLast50Games.Image")));
            this.btnFilterLast50Games.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilterLast50Games.Name = "btnFilterLast50Games";
            this.btnFilterLast50Games.Size = new System.Drawing.Size(70, 35);
            this.btnFilterLast50Games.Text = "Last games";
            this.btnFilterLast50Games.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilterLast50Games.Click += new System.EventHandler(this.BtnFilterLastGamesClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // btnFilterEmptyRecords
            // 
            this.btnFilterEmptyRecords.Checked = true;
            this.btnFilterEmptyRecords.CheckOnClick = true;
            this.btnFilterEmptyRecords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnFilterEmptyRecords.Image = ((System.Drawing.Image)(resources.GetObject("btnFilterEmptyRecords.Image")));
            this.btnFilterEmptyRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFilterEmptyRecords.Name = "btnFilterEmptyRecords";
            this.btnFilterEmptyRecords.Size = new System.Drawing.Size(74, 35);
            this.btnFilterEmptyRecords.Text = "Filter empty";
            this.btnFilterEmptyRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFilterEmptyRecords.Click += new System.EventHandler(this.BtnFilterEmptyRecordsClick);
            // 
            // ofdQ3Log
            // 
            this.ofdQ3Log.DefaultExt = "*.log";
            this.ofdQ3Log.Filter = "Q3 log file|*.log";
            this.ofdQ3Log.ShowReadOnly = true;
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpStatistic);
            this.tcMain.Controls.Add(this.tpDetailed);
            this.tcMain.Controls.Add(this.tpSummary);
            this.tcMain.Controls.Add(this.tpEfficiency);
            this.tcMain.Location = new System.Drawing.Point(12, 41);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(738, 593);
            this.tcMain.TabIndex = 2;
            // 
            // tpStatistic
            // 
            this.tpStatistic.Controls.Add(this.scStatistic);
            this.tpStatistic.Location = new System.Drawing.Point(4, 24);
            this.tpStatistic.Name = "tpStatistic";
            this.tpStatistic.Padding = new System.Windows.Forms.Padding(3);
            this.tpStatistic.Size = new System.Drawing.Size(730, 565);
            this.tpStatistic.TabIndex = 1;
            this.tpStatistic.Text = "Statistic";
            this.tpStatistic.UseVisualStyleBackColor = true;
            // 
            // scStatistic
            // 
            this.scStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scStatistic.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scStatistic.Location = new System.Drawing.Point(3, 3);
            this.scStatistic.Name = "scStatistic";
            this.scStatistic.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scStatistic.Panel1
            // 
            this.scStatistic.Panel1.Controls.Add(this.lvStatistic);
            // 
            // scStatistic.Panel2
            // 
            this.scStatistic.Panel2.Controls.Add(this.pGraph);
            this.scStatistic.Size = new System.Drawing.Size(724, 559);
            this.scStatistic.SplitterDistance = 357;
            this.scStatistic.SplitterWidth = 5;
            this.scStatistic.TabIndex = 3;
            this.scStatistic.TabStop = false;
            // 
            // lvStatistic
            // 
            this.lvStatistic.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader8,
            this.columnHeader11,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader4,
            this.columnHeader10,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader9});
            this.lvStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvStatistic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvStatistic.FullRowSelect = true;
            this.lvStatistic.HideSelection = false;
            this.lvStatistic.Location = new System.Drawing.Point(0, 0);
            this.lvStatistic.MultiSelect = false;
            this.lvStatistic.Name = "lvStatistic";
            this.lvStatistic.OwnerDraw = true;
            this.lvStatistic.Size = new System.Drawing.Size(724, 357);
            this.lvStatistic.TabIndex = 3;
            this.lvStatistic.UseCompatibleStateImageBehavior = false;
            this.lvStatistic.View = System.Windows.Forms.View.Details;
            this.lvStatistic.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            this.lvStatistic.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.LvStatisticDrawColumnHeader);
            this.lvStatistic.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.LvStatisticDrawSubItem);
            this.lvStatistic.SelectedIndexChanged += new System.EventHandler(this.LvStatisticSelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Player";
            this.columnHeader2.Width = 96;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Team";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Scores";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "1";
            this.columnHeader3.Text = "Frags";
            this.columnHeader3.Width = 58;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Tag = "1";
            this.columnHeader7.Text = "Team kills";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Tag = "1";
            this.columnHeader4.Text = "Total deaths";
            this.columnHeader4.Width = 107;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Tag = "1";
            this.columnHeader10.Text = "Backstabs";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Tag = "1";
            this.columnHeader5.Text = "Suicides";
            this.columnHeader5.Width = 82;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Tag = "1";
            this.columnHeader6.Text = "Swimmings";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Tag = "2";
            this.columnHeader9.Text = "Efficiency";
            // 
            // pGraph
            // 
            this.pGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pGraph.Controls.Add(this.legendControl);
            this.pGraph.Controls.Add(this.graphViewer);
            this.pGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pGraph.Location = new System.Drawing.Point(0, 0);
            this.pGraph.Name = "pGraph";
            this.pGraph.Size = new System.Drawing.Size(724, 197);
            this.pGraph.TabIndex = 0;
            // 
            // legendControl
            // 
            this.legendControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.legendControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.legendControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendControl.HighlightedColor = System.Drawing.Color.LightGray;
            this.legendControl.Location = new System.Drawing.Point(621, 146);
            this.legendControl.Name = "legendControl";
            this.legendControl.NoGraphsLabel = "";
            this.legendControl.Size = new System.Drawing.Size(102, 50);
            this.legendControl.TabIndex = 9;
            this.legendControl.Visible = false;
            // 
            // graphViewer
            // 
            this.graphViewer.AdditionalPadding = new System.Windows.Forms.Padding(0);
            this.graphViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphViewer.EmbeddedLegend = false;
            this.graphViewer.ForceCustomBounds = false;
            this.graphViewer.Legend = this.legendControl;
            this.graphViewer.Location = new System.Drawing.Point(0, 0);
            this.graphViewer.Name = "graphViewer";
            this.graphViewer.Size = new System.Drawing.Size(722, 195);
            this.graphViewer.TabIndex = 0;
            gridSettings1.LineColor = System.Drawing.Color.LightGray;
            gridSettings1.ProportionalToTransformedScale = true;
            gridSettings1.ShowGridLines = true;
            gridSettings1.ShowLabels = true;
            gridSettings1.TransformLabelValues = true;
            this.graphViewer.XGrid = gridSettings1;
            gridSettings2.LineColor = System.Drawing.Color.LightGray;
            gridSettings2.ProportionalToTransformedScale = true;
            gridSettings2.ShowGridLines = true;
            gridSettings2.ShowLabels = true;
            gridSettings2.TransformLabelValues = true;
            this.graphViewer.YGrid = gridSettings2;
            this.graphViewer.MouseMove += new Q3LogAnalyzer.Controls.GraphLib.GraphMouseEventHandler(this.GraphViewerMouseMove);
            this.graphViewer.FormatXValue += new Q3LogAnalyzer.Controls.GraphLib.ScaledViewerBase.LabelFormatter(this.GraphViewerFormatXValue);
            this.graphViewer.FormatYValue += new Q3LogAnalyzer.Controls.GraphLib.ScaledViewerBase.LabelFormatter(this.GraphViewerFormatYValue);
            this.graphViewer.MouseLeave += new System.EventHandler(this.GraphViewerMouseLeave);
            // 
            // tpDetailed
            // 
            this.tpDetailed.Controls.Add(this.lvDetailed);
            this.tpDetailed.Location = new System.Drawing.Point(4, 24);
            this.tpDetailed.Name = "tpDetailed";
            this.tpDetailed.Padding = new System.Windows.Forms.Padding(3);
            this.tpDetailed.Size = new System.Drawing.Size(730, 565);
            this.tpDetailed.TabIndex = 0;
            this.tpDetailed.Text = "Detailed";
            this.tpDetailed.UseVisualStyleBackColor = true;
            // 
            // lvDetailed
            // 
            this.lvDetailed.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvDetailed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDetailed.FullRowSelect = true;
            this.lvDetailed.Location = new System.Drawing.Point(3, 3);
            this.lvDetailed.MultiSelect = false;
            this.lvDetailed.Name = "lvDetailed";
            this.lvDetailed.Size = new System.Drawing.Size(724, 559);
            this.lvDetailed.TabIndex = 1;
            this.lvDetailed.UseCompatibleStateImageBehavior = false;
            this.lvDetailed.View = System.Windows.Forms.View.Details;
            this.lvDetailed.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Detailed statistic";
            this.columnHeader1.Width = 492;
            // 
            // tpSummary
            // 
            this.tpSummary.Controls.Add(this.lvSummary);
            this.tpSummary.Location = new System.Drawing.Point(4, 24);
            this.tpSummary.Name = "tpSummary";
            this.tpSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tpSummary.Size = new System.Drawing.Size(730, 565);
            this.tpSummary.TabIndex = 2;
            this.tpSummary.Text = "Summary";
            this.tpSummary.UseVisualStyleBackColor = true;
            // 
            // lvSummary
            // 
            this.lvSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.lvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSummary.FullRowSelect = true;
            this.lvSummary.Location = new System.Drawing.Point(3, 3);
            this.lvSummary.MultiSelect = false;
            this.lvSummary.Name = "lvSummary";
            this.lvSummary.Size = new System.Drawing.Size(724, 559);
            this.lvSummary.TabIndex = 2;
            this.lvSummary.UseCompatibleStateImageBehavior = false;
            this.lvSummary.View = System.Windows.Forms.View.Details;
            this.lvSummary.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewColumnClick);
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Map";
            this.columnHeader12.Width = 183;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "RED team wins";
            this.columnHeader13.Width = 118;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "BLUE team wins";
            this.columnHeader14.Width = 116;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Winner";
            this.columnHeader15.Width = 71;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Rate";
            // 
            // tpEfficiency
            // 
            this.tpEfficiency.Controls.Add(this.lvEfficiency);
            this.tpEfficiency.Location = new System.Drawing.Point(4, 24);
            this.tpEfficiency.Name = "tpEfficiency";
            this.tpEfficiency.Padding = new System.Windows.Forms.Padding(3);
            this.tpEfficiency.Size = new System.Drawing.Size(730, 565);
            this.tpEfficiency.TabIndex = 3;
            this.tpEfficiency.Text = "Efficiency";
            this.tpEfficiency.UseVisualStyleBackColor = true;
            // 
            // lvEfficiency
            // 
            this.lvEfficiency.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader17,
            this.columnHeader26,
            this.columnHeader18});
            this.lvEfficiency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEfficiency.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvEfficiency.FullRowSelect = true;
            this.lvEfficiency.HideSelection = false;
            this.lvEfficiency.Location = new System.Drawing.Point(3, 3);
            this.lvEfficiency.MultiSelect = false;
            this.lvEfficiency.Name = "lvEfficiency";
            this.lvEfficiency.Size = new System.Drawing.Size(724, 559);
            this.lvEfficiency.TabIndex = 4;
            this.lvEfficiency.UseCompatibleStateImageBehavior = false;
            this.lvEfficiency.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Player";
            this.columnHeader17.Width = 96;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Tag = "2";
            this.columnHeader26.Text = "Efficiency";
            this.columnHeader26.Width = 107;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Weapons rating";
            this.columnHeader18.Width = 340;
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slFileName,
            this.slLoadingTime});
            this.ssMain.Location = new System.Drawing.Point(0, 645);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(762, 22);
            this.ssMain.TabIndex = 7;
            // 
            // slFileName
            // 
            this.slFileName.Name = "slFileName";
            this.slFileName.Size = new System.Drawing.Size(0, 17);
            // 
            // slLoadingTime
            // 
            this.slLoadingTime.Name = "slLoadingTime";
            this.slLoadingTime.Size = new System.Drawing.Size(0, 17);
            // 
            // btnColorizeStatistics
            // 
            this.btnColorizeStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorizeStatistics.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnColorizeStatistics.Image = ((System.Drawing.Image)(resources.GetObject("btnColorizeStatistics.Image")));
            this.btnColorizeStatistics.Location = new System.Drawing.Point(686, 41);
            this.btnColorizeStatistics.Name = "btnColorizeStatistics";
            this.btnColorizeStatistics.Size = new System.Drawing.Size(21, 21);
            this.btnColorizeStatistics.TabIndex = 8;
            this.toolTip.SetToolTip(this.btnColorizeStatistics, "Colorize statistics");
            this.btnColorizeStatistics.UseVisualStyleBackColor = true;
            this.btnColorizeStatistics.CheckedChanged += new System.EventHandler(this.BtnColorizeStatisticCheckedChanged);
            // 
            // btnCollapsAll
            // 
            this.btnCollapsAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCollapsAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapsAll.Image")));
            this.btnCollapsAll.Location = new System.Drawing.Point(728, 41);
            this.btnCollapsAll.Name = "btnCollapsAll";
            this.btnCollapsAll.Size = new System.Drawing.Size(21, 21);
            this.btnCollapsAll.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnCollapsAll, "Collapse all");
            this.btnCollapsAll.UseVisualStyleBackColor = true;
            this.btnCollapsAll.Click += new System.EventHandler(this.BtnCollapsAllClick);
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandAll.Image")));
            this.btnExpandAll.Location = new System.Drawing.Point(707, 41);
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(21, 21);
            this.btnExpandAll.TabIndex = 6;
            this.toolTip.SetToolTip(this.btnExpandAll, "Expand all");
            this.btnExpandAll.UseVisualStyleBackColor = true;
            this.btnExpandAll.Click += new System.EventHandler(this.BtnExpandAllClick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 667);
            this.Controls.Add(this.btnColorizeStatistics);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.btnCollapsAll);
            this.Controls.Add(this.btnExpandAll);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.tsMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Q3 Log Analyzer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMainKeyDown);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.tpStatistic.ResumeLayout(false);
            this.scStatistic.Panel1.ResumeLayout(false);
            this.scStatistic.Panel2.ResumeLayout(false);
            this.scStatistic.ResumeLayout(false);
            this.pGraph.ResumeLayout(false);
            this.tpDetailed.ResumeLayout(false);
            this.tpSummary.ResumeLayout(false);
            this.tpEfficiency.ResumeLayout(false);
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton btnLogOpen;
        private System.Windows.Forms.OpenFileDialog ofdQ3Log;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpDetailed;
        private System.Windows.Forms.ListView lvDetailed;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabPage tpStatistic;
        private System.Windows.Forms.ToolStripButton btnExport;
        private Controls.ControlButton btnCollapsAll;
        private Controls.ControlButton btnExpandAll;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel slFileName;
        private System.Windows.Forms.ToolStripStatusLabel slLoadingTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnFilterLast50Games;
        private System.Windows.Forms.SplitContainer scStatistic;
        private ListViewEx lvStatistic;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Panel pGraph;
        private Controls.GraphLib.InteractiveGraphViewer graphViewer;
        private Controls.GraphLib.LegendControl legendControl;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ToolStripButton btnFilterEmptyRecords;
        private ControlCheckBox btnColorizeStatistics;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tpSummary;
        private System.Windows.Forms.ListView lvSummary;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.TabPage tpEfficiency;
        private ListViewEx lvEfficiency;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ToolStripButton btnFilterLastGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

