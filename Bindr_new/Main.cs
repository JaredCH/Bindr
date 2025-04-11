using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Geom;
using System;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Emit;
using System.IO;
using Zuby.ADGV;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using WindowsInput;
using WindowsInput.Native;
using System.Threading;
using System.Runtime.InteropServices;
using System.Linq;

////////////////////////////////////////////////////////////////////////////////////
//TODO//////////////////////////////////////////////////////////////////////////////
//Update Status label to per tab
//Add button to send list to clipboard
//export reports to csv,excel
//add more scraping to pdf processor FG code, WO, Qty
//connect nestplan processor to nestplans and support detail pdf files.
////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////


namespace Bindr_new
{
    // Main.cs

    public partial class Main : Form
    {
        private string sourcePdfPath = "";
        private string selectedFolderPath = "";
        private string suggestedFolderPath = "";
        NestPlanProcessor nestPlanProcessor = new NestPlanProcessor();
        private BindingSource tab2BindingSource = new BindingSource();
        private DataTable tab2DataTable = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Main_Load(object sender, EventArgs e)
        {

        }


        public Main()
        {
            InitializeComponent();
            SetupDataGridView();
            btntab1SelectFolder.Enabled = false;
            btntab1Process.Enabled = false;
            tab2DGV.FilterAndSortEnabled = true;
            tab2DGV.SortStringChanged += Tab2DGV_SortStringChanged;
            tab2DGV.FilterStringChanged += Tab2DGV_FilterStringChanged;
            this.tab2DGV.CellMouseDown += tab2DGV_CellMouseDown;

            this.Refresh();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            settingsForm settings = new settingsForm();
            settings.Show();
        }



        private void SetupDataGridView()
        {
            tab1DGV.Rows.Clear();
        }

        //TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___
        //TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___
        //TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___TAB 1___
        private void btntab1LoadPdf_Click(object sender, EventArgs e)
        {
            ResetAppState();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tab1StatusLabel.Text = "Status: Processing PDF";
                sourcePdfPath = openFileDialog.FileName;
                PdfProcessor.LoadPdf(sourcePdfPath, tab1DGV, ref selectedFolderPath, ref suggestedFolderPath);
                tab1StatusLabel.Text = "Status: PDF loaded successfully.";
            }
            btntab1SelectFolder.Enabled = true;
            btntab1Process.Enabled = true;
        }

        private void btntab1SelectFolder_Click(object sender, EventArgs e)
        {
            PdfProcessor.SelectFolder(ref selectedFolderPath);
            tab1StatusLabel.Text = "Status: Selected Folder: " + selectedFolderPath;
        }

        private void btntab1Process_Click(object sender, EventArgs e)
        {
            PdfProcessor.ProcessPdfAndSaveResults(sourcePdfPath, selectedFolderPath, tab1DGV);
            tab1StatusLabel.Text = "Status: Split and merge complete!";
        }

        private void ResetAppState()
        {
            sourcePdfPath = "";
            selectedFolderPath = "";
            suggestedFolderPath = "";
            tab1DGV.Rows.Clear();
            tab1StatusLabel.Text = "Status: Ready, please select a PDF";
        }



        //TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___
        //TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___
        //TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___
        private void tab2btnLoadNestPlans_Click(object sender, EventArgs e)
        {
            tab1DGV.Rows.Clear();
            tab2StatusLabel.Text = "Status: Select File(s)";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tab2DataTable = new DataTable(); // Reset
                    tab2DataTable.Columns.Add("FileName");
                    tab2DataTable.Columns.Add("PlanId");
                    tab2DataTable.Columns.Add("PartInfo");
                    tab2DataTable.Columns.Add("Qty");
                    tab2DataTable.Columns.Add("Date Created");
                    var fileList = openFileDialog.FileNames;
                    var allRows = new List<List<string>>();
                    tab2StatusLabel.Text = "Status: Processing NestPlans";
                    Parallel.ForEach(fileList, file =>
                    {
                        var rows = nestPlanProcessor.ParseNestPlanFileFast(file);
                        lock (allRows)
                        {
                            allRows.AddRange(rows);
                        }
                    });

                    foreach (var row in allRows)
                    {
                        tab2DataTable.Rows.Add(row.ToArray());
                    }

                    tab2BindingSource.DataSource = tab2DataTable;
                    tab2DGV.DataSource = tab2BindingSource;

                    tab2DGV.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    tab2DGV.RowHeadersWidth = 55;
                    tab2StatusLabel.Text = "Status: NestPlan Processing Completed";
                }
            }
            CheckForPdfFilesAsync();
        }

        private void Tab2DGV_SortStringChanged(object sender, EventArgs e)
        {
            tab2BindingSource.Sort = tab2DGV.SortString;
        }

        private void Tab2DGV_FilterStringChanged(object sender, EventArgs e)
        {
            tab2BindingSource.Filter = tab2DGV.FilterString;
        }

        private void tab2DGV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Select the row under the cursor
                tab2DGV.ClearSelection();
                tab2DGV.Rows[e.RowIndex].Selected = true;
                tab2DGV.CurrentCell = tab2DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Show the context menu at mouse position
                tab2RightClick.Show(Cursor.Position);
            }
        }


        private void loadPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tab2DGV.SelectedRows.Count > 0)
            {
                var selectedRow = tab2DGV.SelectedRows[0];
                var planName = selectedRow.Cells["PlanId"].Value?.ToString();

                if (!string.IsNullOrEmpty(planName))
                {
                    tab2PDFView.LoadFile("Y:\\PDF Files\\" + planName + ".pdf");
                }
            }
        }

        private async void CheckForPdfFilesAsync()
        {
            var planIdToExistsMap = new Dictionary<string, bool>();
            var planIds = tab2DGV.Rows
                .Cast<DataGridViewRow>()
                .Select(r => r.Cells["PlanId"].Value?.ToString())
                .Where(id => !string.IsNullOrEmpty(id))
                .Distinct()
                .ToList();

            await Task.Run(() =>
            {
                foreach (var planId in planIds)
                {
                    string pdfPath = System.IO.Path.Combine("Y:\\PDF Files\\", $"{planId}.pdf");
                    bool exists = File.Exists(pdfPath);
                    lock (planIdToExistsMap)
                    {
                        planIdToExistsMap[planId] = exists;
                    }
                }
            });

            foreach (DataGridViewRow row in tab2DGV.Rows)
            {
                string planId = row.Cells["PlanId"].Value?.ToString();
                if (!string.IsNullOrEmpty(planId) && planIdToExistsMap.ContainsKey(planId))
                {
                    bool exists = planIdToExistsMap[planId];
                    row.HeaderCell.Style.ForeColor = exists ? Color.DarkOliveGreen : Color.DarkRed;
                    row.HeaderCell.Value = exists ? "📄" : "❌";
                    row.HeaderCell.ToolTipText = exists ? "PDF exists for this drawing." : "No PDF found.";
                }
            }
        }

        //PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___
        //PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___
        //PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___

    }

 }

