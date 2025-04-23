using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bindr
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btntab1LoadPdf = new System.Windows.Forms.Button();
            this.btntab1SelectFolder = new System.Windows.Forms.Button();
            this.btntab1Process = new System.Windows.Forms.Button();
            this.tab1StatusLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.PropMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainTab = new System.Windows.Forms.TabControl();
            this.tabPDFMerge = new System.Windows.Forms.TabPage();
            this.btntab1LoadBOM = new System.Windows.Forms.Button();
            this.btntab1LoadSO = new System.Windows.Forms.Button();
            this.btntab1ProcessBOM = new System.Windows.Forms.Button();
            this.tab1DGV = new Zuby.ADGV.AdvancedDataGridView();
            this.tabNestPlanProcessor = new System.Windows.Forms.TabPage();
            this.tab2PDFView = new PdfiumViewer.PdfViewer();
            this.tab2StatusLabel = new System.Windows.Forms.Label();
            this.tab2DGV = new Zuby.ADGV.AdvancedDataGridView();
            this.tab2btnLoadNestPlans = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tab2btnShowRollUp = new System.Windows.Forms.Button();
            this.tabWOReport = new System.Windows.Forms.TabPage();
            this.btntab3FGSort = new System.Windows.Forms.Button();
            this.btntab3reset = new System.Windows.Forms.Button();
            this.btntab3summarize = new System.Windows.Forms.Button();
            this.btntab3LoadReport = new System.Windows.Forms.Button();
            this.tab3DGV = new Zuby.ADGV.AdvancedDataGridView();
            this.tab3contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabView = new System.Windows.Forms.TabPage();
            this.advancedDataGridView1 = new Zuby.ADGV.AdvancedDataGridView();
            this.tab4PDFView = new PdfiumViewer.PdfViewer();
            this.tabCutLog = new System.Windows.Forms.TabPage();
            this.tabGatesLog = new System.Windows.Forms.TabPage();
            this.tabShipping = new System.Windows.Forms.TabPage();
            this.tabDigitalCubby = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label43 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabModernReports = new System.Windows.Forms.TabPage();
            this.tabReleaseEval = new System.Windows.Forms.TabPage();
            this.tabMetalTrace = new System.Windows.Forms.TabPage();
            this.tab2RightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSupportDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.tabPDFMerge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab1DGV)).BeginInit();
            this.tabNestPlanProcessor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab2DGV)).BeginInit();
            this.tabWOReport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab3DGV)).BeginInit();
            this.tab3contextMenuStrip1.SuspendLayout();
            this.tabView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).BeginInit();
            this.tabDigitalCubby.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tab2RightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // btntab1LoadPdf
            // 
            this.btntab1LoadPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1LoadPdf.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntab1LoadPdf.Location = new System.Drawing.Point(867, 346);
            this.btntab1LoadPdf.Margin = new System.Windows.Forms.Padding(10);
            this.btntab1LoadPdf.Name = "btntab1LoadPdf";
            this.btntab1LoadPdf.Size = new System.Drawing.Size(174, 35);
            this.btntab1LoadPdf.TabIndex = 0;
            this.btntab1LoadPdf.Text = "Load JDE PDF";
            this.btntab1LoadPdf.UseVisualStyleBackColor = true;
            this.btntab1LoadPdf.Click += new System.EventHandler(this.btntab1LoadPdf_Click);
            // 
            // btntab1SelectFolder
            // 
            this.btntab1SelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1SelectFolder.BackColor = System.Drawing.Color.Transparent;
            this.btntab1SelectFolder.Enabled = false;
            this.btntab1SelectFolder.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btntab1SelectFolder.FlatAppearance.BorderSize = 10;
            this.btntab1SelectFolder.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntab1SelectFolder.Location = new System.Drawing.Point(865, 394);
            this.btntab1SelectFolder.Name = "btntab1SelectFolder";
            this.btntab1SelectFolder.Size = new System.Drawing.Size(176, 35);
            this.btntab1SelectFolder.TabIndex = 1;
            this.btntab1SelectFolder.Text = "Select Folder";
            this.btntab1SelectFolder.UseVisualStyleBackColor = false;
            this.btntab1SelectFolder.Click += new System.EventHandler(this.btntab1SelectFolder_Click);
            // 
            // btntab1Process
            // 
            this.btntab1Process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1Process.Enabled = false;
            this.btntab1Process.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntab1Process.Location = new System.Drawing.Point(865, 445);
            this.btntab1Process.Name = "btntab1Process";
            this.btntab1Process.Size = new System.Drawing.Size(176, 34);
            this.btntab1Process.TabIndex = 2;
            this.btntab1Process.Text = "Merge PDFs";
            this.btntab1Process.UseVisualStyleBackColor = true;
            this.btntab1Process.Click += new System.EventHandler(this.btntab1Process_Click);
            // 
            // tab1StatusLabel
            // 
            this.tab1StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tab1StatusLabel.AutoSize = true;
            this.tab1StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tab1StatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tab1StatusLabel.Location = new System.Drawing.Point(6, 487);
            this.tab1StatusLabel.Name = "tab1StatusLabel";
            this.tab1StatusLabel.Size = new System.Drawing.Size(58, 18);
            this.tab1StatusLabel.TabIndex = 4;
            this.tab1StatusLabel.Text = "Status: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1057, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // PropMenuItem1
            // 
            this.PropMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.PropMenuItem1.Name = "PropMenuItem1";
            this.PropMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.PropMenuItem1.Text = "Settings";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(127, 22);
            this.toolStripMenuItem1.Text = "Properties";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // MainTab
            // 
            this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTab.Controls.Add(this.tabPDFMerge);
            this.MainTab.Controls.Add(this.tabNestPlanProcessor);
            this.MainTab.Controls.Add(this.tabWOReport);
            this.MainTab.Controls.Add(this.tabView);
            this.MainTab.Controls.Add(this.tabCutLog);
            this.MainTab.Controls.Add(this.tabGatesLog);
            this.MainTab.Controls.Add(this.tabShipping);
            this.MainTab.Controls.Add(this.tabDigitalCubby);
            this.MainTab.Controls.Add(this.tabModernReports);
            this.MainTab.Controls.Add(this.tabReleaseEval);
            this.MainTab.Controls.Add(this.tabMetalTrace);
            this.MainTab.Location = new System.Drawing.Point(0, 23);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(1057, 534);
            this.MainTab.TabIndex = 6;
            // 
            // tabPDFMerge
            // 
            this.tabPDFMerge.BackColor = System.Drawing.Color.Transparent;
            this.tabPDFMerge.Controls.Add(this.btntab1LoadBOM);
            this.tabPDFMerge.Controls.Add(this.btntab1LoadSO);
            this.tabPDFMerge.Controls.Add(this.btntab1ProcessBOM);
            this.tabPDFMerge.Controls.Add(this.tab1StatusLabel);
            this.tabPDFMerge.Controls.Add(this.tab1DGV);
            this.tabPDFMerge.Controls.Add(this.btntab1LoadPdf);
            this.tabPDFMerge.Controls.Add(this.btntab1Process);
            this.tabPDFMerge.Controls.Add(this.btntab1SelectFolder);
            this.tabPDFMerge.Location = new System.Drawing.Point(4, 22);
            this.tabPDFMerge.Name = "tabPDFMerge";
            this.tabPDFMerge.Padding = new System.Windows.Forms.Padding(3);
            this.tabPDFMerge.Size = new System.Drawing.Size(1049, 508);
            this.tabPDFMerge.TabIndex = 0;
            this.tabPDFMerge.Text = "PDF Merge";
            // 
            // btntab1LoadBOM
            // 
            this.btntab1LoadBOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1LoadBOM.Location = new System.Drawing.Point(867, 86);
            this.btntab1LoadBOM.Name = "btntab1LoadBOM";
            this.btntab1LoadBOM.Size = new System.Drawing.Size(174, 34);
            this.btntab1LoadBOM.TabIndex = 7;
            this.btntab1LoadBOM.Text = "Load BOM";
            this.btntab1LoadBOM.UseVisualStyleBackColor = true;
            this.btntab1LoadBOM.Click += new System.EventHandler(this.btntab1LoadBOM_Click);
            // 
            // btntab1LoadSO
            // 
            this.btntab1LoadSO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1LoadSO.Location = new System.Drawing.Point(867, 46);
            this.btntab1LoadSO.Name = "btntab1LoadSO";
            this.btntab1LoadSO.Size = new System.Drawing.Size(174, 34);
            this.btntab1LoadSO.TabIndex = 6;
            this.btntab1LoadSO.Text = "Load Sales Order";
            this.btntab1LoadSO.UseVisualStyleBackColor = true;
            this.btntab1LoadSO.Click += new System.EventHandler(this.btntab1LoadSO_Click);
            // 
            // btntab1ProcessBOM
            // 
            this.btntab1ProcessBOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1ProcessBOM.Location = new System.Drawing.Point(867, 6);
            this.btntab1ProcessBOM.Name = "btntab1ProcessBOM";
            this.btntab1ProcessBOM.Size = new System.Drawing.Size(174, 34);
            this.btntab1ProcessBOM.TabIndex = 5;
            this.btntab1ProcessBOM.Text = "Process BOM";
            this.btntab1ProcessBOM.UseVisualStyleBackColor = true;
            this.btntab1ProcessBOM.Click += new System.EventHandler(this.btntab1ProcessBOM_Click);
            // 
            // tab1DGV
            // 
            this.tab1DGV.AllowUserToAddRows = false;
            this.tab1DGV.AllowUserToDeleteRows = false;
            this.tab1DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab1DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tab1DGV.FilterAndSortEnabled = true;
            this.tab1DGV.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab1DGV.Location = new System.Drawing.Point(6, 6);
            this.tab1DGV.MaxFilterButtonImageHeight = 23;
            this.tab1DGV.Name = "tab1DGV";
            this.tab1DGV.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tab1DGV.Size = new System.Drawing.Size(853, 473);
            this.tab1DGV.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab1DGV.TabIndex = 4;
            // 
            // tabNestPlanProcessor
            // 
            this.tabNestPlanProcessor.Controls.Add(this.tab2PDFView);
            this.tabNestPlanProcessor.Controls.Add(this.tab2StatusLabel);
            this.tabNestPlanProcessor.Controls.Add(this.tab2DGV);
            this.tabNestPlanProcessor.Controls.Add(this.tab2btnLoadNestPlans);
            this.tabNestPlanProcessor.Controls.Add(this.button2);
            this.tabNestPlanProcessor.Controls.Add(this.tab2btnShowRollUp);
            this.tabNestPlanProcessor.Location = new System.Drawing.Point(4, 22);
            this.tabNestPlanProcessor.Name = "tabNestPlanProcessor";
            this.tabNestPlanProcessor.Padding = new System.Windows.Forms.Padding(3);
            this.tabNestPlanProcessor.Size = new System.Drawing.Size(1049, 508);
            this.tabNestPlanProcessor.TabIndex = 1;
            this.tabNestPlanProcessor.Text = "NestPlan Processor";
            this.tabNestPlanProcessor.UseVisualStyleBackColor = true;
            // 
            // tab2PDFView
            // 
            this.tab2PDFView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab2PDFView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tab2PDFView.Location = new System.Drawing.Point(567, 6);
            this.tab2PDFView.Name = "tab2PDFView";
            this.tab2PDFView.ShowBookmarks = false;
            this.tab2PDFView.ShowToolbar = false;
            this.tab2PDFView.Size = new System.Drawing.Size(368, 471);
            this.tab2PDFView.TabIndex = 10;
            // 
            // tab2StatusLabel
            // 
            this.tab2StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tab2StatusLabel.AutoSize = true;
            this.tab2StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tab2StatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tab2StatusLabel.Location = new System.Drawing.Point(8, 487);
            this.tab2StatusLabel.Name = "tab2StatusLabel";
            this.tab2StatusLabel.Size = new System.Drawing.Size(58, 18);
            this.tab2StatusLabel.TabIndex = 9;
            this.tab2StatusLabel.Text = "Status: ";
            // 
            // tab2DGV
            // 
            this.tab2DGV.AllowUserToAddRows = false;
            this.tab2DGV.AllowUserToDeleteRows = false;
            this.tab2DGV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tab2DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tab2DGV.FilterAndSortEnabled = true;
            this.tab2DGV.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab2DGV.Location = new System.Drawing.Point(6, 6);
            this.tab2DGV.MaxFilterButtonImageHeight = 23;
            this.tab2DGV.Name = "tab2DGV";
            this.tab2DGV.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tab2DGV.Size = new System.Drawing.Size(553, 472);
            this.tab2DGV.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab2DGV.TabIndex = 8;
            this.tab2DGV.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tab2DGV_CellMouseDown);
            // 
            // tab2btnLoadNestPlans
            // 
            this.tab2btnLoadNestPlans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tab2btnLoadNestPlans.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab2btnLoadNestPlans.Location = new System.Drawing.Point(946, 6);
            this.tab2btnLoadNestPlans.Margin = new System.Windows.Forms.Padding(10);
            this.tab2btnLoadNestPlans.Name = "tab2btnLoadNestPlans";
            this.tab2btnLoadNestPlans.Size = new System.Drawing.Size(97, 35);
            this.tab2btnLoadNestPlans.TabIndex = 5;
            this.tab2btnLoadNestPlans.Text = "Load Nest Plans";
            this.tab2btnLoadNestPlans.UseVisualStyleBackColor = true;
            this.tab2btnLoadNestPlans.Click += new System.EventHandler(this.tab2btnLoadNestPlans_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(944, 444);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 34);
            this.button2.TabIndex = 7;
            this.button2.Text = "empty";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tab2btnShowRollUp
            // 
            this.tab2btnShowRollUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tab2btnShowRollUp.BackColor = System.Drawing.Color.Transparent;
            this.tab2btnShowRollUp.Enabled = false;
            this.tab2btnShowRollUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.tab2btnShowRollUp.FlatAppearance.BorderSize = 10;
            this.tab2btnShowRollUp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab2btnShowRollUp.Location = new System.Drawing.Point(944, 68);
            this.tab2btnShowRollUp.Name = "tab2btnShowRollUp";
            this.tab2btnShowRollUp.Size = new System.Drawing.Size(99, 35);
            this.tab2btnShowRollUp.TabIndex = 6;
            this.tab2btnShowRollUp.Text = "empty";
            this.tab2btnShowRollUp.UseVisualStyleBackColor = false;
            this.tab2btnShowRollUp.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabWOReport
            // 
            this.tabWOReport.Controls.Add(this.btntab3FGSort);
            this.tabWOReport.Controls.Add(this.btntab3reset);
            this.tabWOReport.Controls.Add(this.btntab3summarize);
            this.tabWOReport.Controls.Add(this.btntab3LoadReport);
            this.tabWOReport.Controls.Add(this.tab3DGV);
            this.tabWOReport.Location = new System.Drawing.Point(4, 22);
            this.tabWOReport.Name = "tabWOReport";
            this.tabWOReport.Size = new System.Drawing.Size(1049, 508);
            this.tabWOReport.TabIndex = 2;
            this.tabWOReport.Text = "WO Report";
            this.tabWOReport.UseVisualStyleBackColor = true;
            // 
            // btntab3FGSort
            // 
            this.btntab3FGSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab3FGSort.Location = new System.Drawing.Point(575, 467);
            this.btntab3FGSort.Name = "btntab3FGSort";
            this.btntab3FGSort.Size = new System.Drawing.Size(112, 30);
            this.btntab3FGSort.TabIndex = 4;
            this.btntab3FGSort.Text = "FG Sort";
            this.btntab3FGSort.UseVisualStyleBackColor = true;
            this.btntab3FGSort.Click += new System.EventHandler(this.btntab3FGSort_Click);
            // 
            // btntab3reset
            // 
            this.btntab3reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab3reset.Location = new System.Drawing.Point(693, 468);
            this.btntab3reset.Name = "btntab3reset";
            this.btntab3reset.Size = new System.Drawing.Size(112, 30);
            this.btntab3reset.TabIndex = 3;
            this.btntab3reset.Text = "reset";
            this.btntab3reset.UseVisualStyleBackColor = true;
            this.btntab3reset.Click += new System.EventHandler(this.btntab3reset_Click);
            // 
            // btntab3summarize
            // 
            this.btntab3summarize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab3summarize.Location = new System.Drawing.Point(811, 468);
            this.btntab3summarize.Name = "btntab3summarize";
            this.btntab3summarize.Size = new System.Drawing.Size(112, 30);
            this.btntab3summarize.TabIndex = 2;
            this.btntab3summarize.Text = "summarize";
            this.btntab3summarize.UseVisualStyleBackColor = true;
            this.btntab3summarize.Click += new System.EventHandler(this.btntab3summarize_Click);
            // 
            // btntab3LoadReport
            // 
            this.btntab3LoadReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab3LoadReport.Location = new System.Drawing.Point(929, 468);
            this.btntab3LoadReport.Name = "btntab3LoadReport";
            this.btntab3LoadReport.Size = new System.Drawing.Size(112, 30);
            this.btntab3LoadReport.TabIndex = 1;
            this.btntab3LoadReport.Text = "load report";
            this.btntab3LoadReport.UseVisualStyleBackColor = true;
            this.btntab3LoadReport.Click += new System.EventHandler(this.btntab3LoadReport_Click);
            // 
            // tab3DGV
            // 
            this.tab3DGV.AllowUserToAddRows = false;
            this.tab3DGV.AllowUserToDeleteRows = false;
            this.tab3DGV.AllowUserToOrderColumns = true;
            this.tab3DGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab3DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tab3DGV.ContextMenuStrip = this.tab3contextMenuStrip1;
            this.tab3DGV.FilterAndSortEnabled = true;
            this.tab3DGV.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab3DGV.Location = new System.Drawing.Point(6, 7);
            this.tab3DGV.MaxFilterButtonImageHeight = 23;
            this.tab3DGV.Name = "tab3DGV";
            this.tab3DGV.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tab3DGV.Size = new System.Drawing.Size(1040, 455);
            this.tab3DGV.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab3DGV.TabIndex = 0;
            this.tab3DGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.tab3DGV_CellDoubleClick);
            // 
            // tab3contextMenuStrip1
            // 
            this.tab3contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDetailToolStripMenuItem,
            this.openWOToolStripMenuItem});
            this.tab3contextMenuStrip1.Name = "tab3contextMenuStrip1";
            this.tab3contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            // 
            // openDetailToolStripMenuItem
            // 
            this.openDetailToolStripMenuItem.Name = "openDetailToolStripMenuItem";
            this.openDetailToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openDetailToolStripMenuItem.Text = "Open Detail";
            this.openDetailToolStripMenuItem.Click += new System.EventHandler(this.openDetailToolStripMenuItem_Click);
            // 
            // openWOToolStripMenuItem
            // 
            this.openWOToolStripMenuItem.Name = "openWOToolStripMenuItem";
            this.openWOToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openWOToolStripMenuItem.Text = "Open WO";
            this.openWOToolStripMenuItem.Click += new System.EventHandler(this.openWOToolStripMenuItem_Click);
            // 
            // tabView
            // 
            this.tabView.Controls.Add(this.advancedDataGridView1);
            this.tabView.Controls.Add(this.tab4PDFView);
            this.tabView.Location = new System.Drawing.Point(4, 22);
            this.tabView.Name = "tabView";
            this.tabView.Size = new System.Drawing.Size(1049, 508);
            this.tabView.TabIndex = 3;
            this.tabView.Text = "View";
            this.tabView.UseVisualStyleBackColor = true;
            // 
            // advancedDataGridView1
            // 
            this.advancedDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedDataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.advancedDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView1.FilterAndSortEnabled = true;
            this.advancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advancedDataGridView1.Location = new System.Drawing.Point(726, 6);
            this.advancedDataGridView1.MaxFilterButtonImageHeight = 23;
            this.advancedDataGridView1.Name = "advancedDataGridView1";
            this.advancedDataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.advancedDataGridView1.Size = new System.Drawing.Size(322, 500);
            this.advancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.advancedDataGridView1.TabIndex = 1;
            // 
            // tab4PDFView
            // 
            this.tab4PDFView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab4PDFView.Location = new System.Drawing.Point(7, 4);
            this.tab4PDFView.Name = "tab4PDFView";
            this.tab4PDFView.ShowBookmarks = false;
            this.tab4PDFView.ShowToolbar = false;
            this.tab4PDFView.Size = new System.Drawing.Size(713, 503);
            this.tab4PDFView.TabIndex = 0;
            // 
            // tabCutLog
            // 
            this.tabCutLog.Location = new System.Drawing.Point(4, 22);
            this.tabCutLog.Name = "tabCutLog";
            this.tabCutLog.Size = new System.Drawing.Size(1049, 508);
            this.tabCutLog.TabIndex = 4;
            this.tabCutLog.Text = "Cut Log (Plate/Pipe)";
            this.tabCutLog.UseVisualStyleBackColor = true;
            // 
            // tabGatesLog
            // 
            this.tabGatesLog.Location = new System.Drawing.Point(4, 22);
            this.tabGatesLog.Name = "tabGatesLog";
            this.tabGatesLog.Size = new System.Drawing.Size(1049, 508);
            this.tabGatesLog.TabIndex = 5;
            this.tabGatesLog.Text = "Gates Pad Log";
            this.tabGatesLog.UseVisualStyleBackColor = true;
            // 
            // tabShipping
            // 
            this.tabShipping.Location = new System.Drawing.Point(4, 22);
            this.tabShipping.Name = "tabShipping";
            this.tabShipping.Size = new System.Drawing.Size(1049, 508);
            this.tabShipping.TabIndex = 6;
            this.tabShipping.Text = "Shipping / PL\'s";
            this.tabShipping.UseVisualStyleBackColor = true;
            // 
            // tabDigitalCubby
            // 
            this.tabDigitalCubby.Controls.Add(this.tableLayoutPanel1);
            this.tabDigitalCubby.Location = new System.Drawing.Point(4, 22);
            this.tabDigitalCubby.Name = "tabDigitalCubby";
            this.tabDigitalCubby.Size = new System.Drawing.Size(1049, 508);
            this.tabDigitalCubby.TabIndex = 7;
            this.tabDigitalCubby.Text = "Digital Cubby";
            this.tabDigitalCubby.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.label43, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label37, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label31, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label19, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label13, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1038, 502);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(6, 437);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(13, 13);
            this.label43.TabIndex = 42;
            this.label43.Text = "1";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(6, 375);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(24, 13);
            this.label37.TabIndex = 36;
            this.label37.Text = "3/4";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 313);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(24, 13);
            this.label31.TabIndex = 30;
            this.label31.Text = "5/8";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 251);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(24, 13);
            this.label25.TabIndex = 24;
            this.label25.Text = "1/2";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 189);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(24, 13);
            this.label19.TabIndex = 18;
            this.label19.Text = "3/8";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 127);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "1/4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "3/16";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(866, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "A36";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(694, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "A516";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(522, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "CHROME";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(350, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "304";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "316";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Duplex";
            // 
            // tabModernReports
            // 
            this.tabModernReports.Location = new System.Drawing.Point(4, 22);
            this.tabModernReports.Name = "tabModernReports";
            this.tabModernReports.Size = new System.Drawing.Size(1049, 508);
            this.tabModernReports.TabIndex = 8;
            this.tabModernReports.Text = "Modern Reports";
            this.tabModernReports.UseVisualStyleBackColor = true;
            // 
            // tabReleaseEval
            // 
            this.tabReleaseEval.Location = new System.Drawing.Point(4, 22);
            this.tabReleaseEval.Name = "tabReleaseEval";
            this.tabReleaseEval.Size = new System.Drawing.Size(1049, 508);
            this.tabReleaseEval.TabIndex = 9;
            this.tabReleaseEval.Text = "Release Eval";
            this.tabReleaseEval.UseVisualStyleBackColor = true;
            // 
            // tabMetalTrace
            // 
            this.tabMetalTrace.Location = new System.Drawing.Point(4, 22);
            this.tabMetalTrace.Name = "tabMetalTrace";
            this.tabMetalTrace.Size = new System.Drawing.Size(1049, 508);
            this.tabMetalTrace.TabIndex = 10;
            this.tabMetalTrace.Text = "Metal Trace";
            this.tabMetalTrace.UseVisualStyleBackColor = true;
            // 
            // tab2RightClick
            // 
            this.tab2RightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPDFToolStripMenuItem,
            this.loadSupportDetailToolStripMenuItem});
            this.tab2RightClick.Name = "tab2RightClick";
            this.tab2RightClick.Size = new System.Drawing.Size(179, 48);
            // 
            // loadPDFToolStripMenuItem
            // 
            this.loadPDFToolStripMenuItem.Name = "loadPDFToolStripMenuItem";
            this.loadPDFToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.loadPDFToolStripMenuItem.Text = "Load NestPlan";
            this.loadPDFToolStripMenuItem.Click += new System.EventHandler(this.loadPDFToolStripMenuItem_Click);
            // 
            // loadSupportDetailToolStripMenuItem
            // 
            this.loadSupportDetailToolStripMenuItem.Name = "loadSupportDetailToolStripMenuItem";
            this.loadSupportDetailToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.loadSupportDetailToolStripMenuItem.Text = "Load Support Detail";
            this.loadSupportDetailToolStripMenuItem.Click += new System.EventHandler(this.loadSupportDetailToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 554);
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Support Bindr";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.tabPDFMerge.ResumeLayout(false);
            this.tabPDFMerge.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab1DGV)).EndInit();
            this.tabNestPlanProcessor.ResumeLayout(false);
            this.tabNestPlanProcessor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab2DGV)).EndInit();
            this.tabWOReport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tab3DGV)).EndInit();
            this.tab3contextMenuStrip1.ResumeLayout(false);
            this.tabView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView1)).EndInit();
            this.tabDigitalCubby.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tab2RightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem PropMenuItem1;
        private ToolStripMenuItem toolStripMenuItem1;
        private TabPage tabPDFMerge;
        private TabPage tabNestPlanProcessor;
        private Zuby.ADGV.AdvancedDataGridView tab1DGV;
        private Button btntab1LoadPdf;
        private Button btntab1SelectFolder;
        private Button btntab1Process;
        private Label tab1StatusLabel;
        public TabControl MainTab;
        private Zuby.ADGV.AdvancedDataGridView tab2DGV;
        private Button tab2btnLoadNestPlans;
        private Button button2;
        private Button tab2btnShowRollUp;
        private TabPage tabWOReport;
        private Label tab2StatusLabel;
        private ContextMenuStrip tab2RightClick;
        private ToolStripMenuItem loadPDFToolStripMenuItem;
        private ToolStripMenuItem loadSupportDetailToolStripMenuItem;
        private PdfiumViewer.PdfViewer tab2PDFView;
        private Button btntab1ProcessBOM;
        private Button btntab1LoadBOM;
        private Button btntab1LoadSO;
        private Button btntab3LoadReport;
        private Zuby.ADGV.AdvancedDataGridView tab3DGV;
        private ContextMenuStrip tab3contextMenuStrip1;
        private ToolStripMenuItem openDetailToolStripMenuItem;
        private Button btntab3summarize;
        private Button btntab3reset;
        private ToolStripMenuItem openWOToolStripMenuItem;
        private Button btntab3FGSort;
        private TabPage tabView;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView1;
        private PdfiumViewer.PdfViewer tab4PDFView;
        private TabPage tabCutLog;
        private TabPage tabGatesLog;
        private TabPage tabShipping;
        private TabPage tabDigitalCubby;
        private TabPage tabModernReports;
        private TabPage tabReleaseEval;
        private TabPage tabMetalTrace;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label43;
        private Label label37;
        private Label label31;
        private Label label25;
        private Label label19;
        private Label label13;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
