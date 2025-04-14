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
            this.tabpdfmerge = new System.Windows.Forms.TabPage();
            this.tab1DGV = new Zuby.ADGV.AdvancedDataGridView();
            this.tabreport = new System.Windows.Forms.TabPage();
            this.tab2PDFView = new PdfiumViewer.PdfViewer();
            this.tab2StatusLabel = new System.Windows.Forms.Label();
            this.tab2DGV = new Zuby.ADGV.AdvancedDataGridView();
            this.tab2btnLoadNestPlans = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tab2RightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSupportDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.tabpdfmerge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab1DGV)).BeginInit();
            this.tabreport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab2DGV)).BeginInit();
            this.tab2RightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // btntab1LoadPdf
            // 
            this.btntab1LoadPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1LoadPdf.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntab1LoadPdf.Location = new System.Drawing.Point(800, 6);
            this.btntab1LoadPdf.Margin = new System.Windows.Forms.Padding(10);
            this.btntab1LoadPdf.Name = "btntab1LoadPdf";
            this.btntab1LoadPdf.Size = new System.Drawing.Size(97, 35);
            this.btntab1LoadPdf.TabIndex = 0;
            this.btntab1LoadPdf.Text = "Load JDE PDF";
            this.btntab1LoadPdf.UseVisualStyleBackColor = true;
            this.btntab1LoadPdf.Click += new System.EventHandler(this.btntab1LoadPdf_Click);
            // 
            // btntab1SelectFolder
            // 
            this.btntab1SelectFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btntab1SelectFolder.BackColor = System.Drawing.Color.Transparent;
            this.btntab1SelectFolder.Enabled = false;
            this.btntab1SelectFolder.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btntab1SelectFolder.FlatAppearance.BorderSize = 10;
            this.btntab1SelectFolder.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntab1SelectFolder.Location = new System.Drawing.Point(798, 68);
            this.btntab1SelectFolder.Name = "btntab1SelectFolder";
            this.btntab1SelectFolder.Size = new System.Drawing.Size(99, 35);
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
            this.btntab1Process.Location = new System.Drawing.Point(790, 281);
            this.btntab1Process.Name = "btntab1Process";
            this.btntab1Process.Size = new System.Drawing.Size(105, 34);
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
            this.tab1StatusLabel.Location = new System.Drawing.Point(6, 323);
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
            this.menuStrip1.Size = new System.Drawing.Size(982, 24);
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
            this.MainTab.Controls.Add(this.tabpdfmerge);
            this.MainTab.Controls.Add(this.tabreport);
            this.MainTab.Controls.Add(this.tabPage1);
            this.MainTab.Location = new System.Drawing.Point(0, 23);
            this.MainTab.Name = "MainTab";
            this.MainTab.SelectedIndex = 0;
            this.MainTab.Size = new System.Drawing.Size(982, 370);
            this.MainTab.TabIndex = 6;
            // 
            // tabpdfmerge
            // 
            this.tabpdfmerge.BackColor = System.Drawing.Color.Transparent;
            this.tabpdfmerge.Controls.Add(this.tab1StatusLabel);
            this.tabpdfmerge.Controls.Add(this.tab1DGV);
            this.tabpdfmerge.Controls.Add(this.btntab1LoadPdf);
            this.tabpdfmerge.Controls.Add(this.btntab1Process);
            this.tabpdfmerge.Controls.Add(this.btntab1SelectFolder);
            this.tabpdfmerge.Location = new System.Drawing.Point(4, 22);
            this.tabpdfmerge.Name = "tabpdfmerge";
            this.tabpdfmerge.Padding = new System.Windows.Forms.Padding(3);
            this.tabpdfmerge.Size = new System.Drawing.Size(974, 344);
            this.tabpdfmerge.TabIndex = 0;
            this.tabpdfmerge.Text = "PDF Merge";
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
            this.tab1DGV.Size = new System.Drawing.Size(778, 309);
            this.tab1DGV.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab1DGV.TabIndex = 4;
            // 
            // tabreport
            // 
            this.tabreport.Controls.Add(this.tab2PDFView);
            this.tabreport.Controls.Add(this.tab2StatusLabel);
            this.tabreport.Controls.Add(this.tab2DGV);
            this.tabreport.Controls.Add(this.tab2btnLoadNestPlans);
            this.tabreport.Controls.Add(this.button2);
            this.tabreport.Controls.Add(this.button3);
            this.tabreport.Location = new System.Drawing.Point(4, 22);
            this.tabreport.Name = "tabreport";
            this.tabreport.Padding = new System.Windows.Forms.Padding(3);
            this.tabreport.Size = new System.Drawing.Size(974, 344);
            this.tabreport.TabIndex = 1;
            this.tabreport.Text = "Report";
            this.tabreport.UseVisualStyleBackColor = true;
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
            this.tab2PDFView.Size = new System.Drawing.Size(293, 307);
            this.tab2PDFView.TabIndex = 10;
            // 
            // tab2StatusLabel
            // 
            this.tab2StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tab2StatusLabel.AutoSize = true;
            this.tab2StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.tab2StatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tab2StatusLabel.Location = new System.Drawing.Point(8, 323);
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
            this.tab2DGV.Size = new System.Drawing.Size(553, 308);
            this.tab2DGV.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.tab2DGV.TabIndex = 8;
            this.tab2DGV.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tab2DGV_CellMouseDown);
            // 
            // tab2btnLoadNestPlans
            // 
            this.tab2btnLoadNestPlans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tab2btnLoadNestPlans.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab2btnLoadNestPlans.Location = new System.Drawing.Point(871, 6);
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
            this.button2.Location = new System.Drawing.Point(869, 280);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 34);
            this.button2.TabIndex = 7;
            this.button2.Text = "empty";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.Enabled = false;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderSize = 10;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(869, 68);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 35);
            this.button3.TabIndex = 6;
            this.button3.Text = "empty";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(974, 344);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "JDE";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.ClientSize = new System.Drawing.Size(982, 390);
            this.Controls.Add(this.MainTab);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Bindr";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MainTab.ResumeLayout(false);
            this.tabpdfmerge.ResumeLayout(false);
            this.tabpdfmerge.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab1DGV)).EndInit();
            this.tabreport.ResumeLayout(false);
            this.tabreport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tab2DGV)).EndInit();
            this.tab2RightClick.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem PropMenuItem1;
        private ToolStripMenuItem toolStripMenuItem1;
        private TabPage tabpdfmerge;
        private TabPage tabreport;
        private Zuby.ADGV.AdvancedDataGridView tab1DGV;
        private Button btntab1LoadPdf;
        private Button btntab1SelectFolder;
        private Button btntab1Process;
        private Label tab1StatusLabel;
        private TabControl MainTab;
        private Zuby.ADGV.AdvancedDataGridView tab2DGV;
        private Button tab2btnLoadNestPlans;
        private Button button2;
        private Button button3;
        private TabPage tabPage1;
        private Label tab2StatusLabel;
        private ContextMenuStrip tab2RightClick;
        private ToolStripMenuItem loadPDFToolStripMenuItem;
        private ToolStripMenuItem loadSupportDetailToolStripMenuItem;
        private PdfiumViewer.PdfViewer tab2PDFView;
    }
}
