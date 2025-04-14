using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Geom;
using System;
using System.Text;
using System.Windows.Forms;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using System.Security.Cryptography.X509Certificates;
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
using PdfiumViewer;

namespace Bindr
{
    public partial class Main : Form
    {
        private string sourcePdfPath = "";
        private string selectedFolderPath = "";
        private string suggestedFolderPath = "";
        NestPlanProcessor nestPlanProcessor = new NestPlanProcessor();
        private BindingSource tab2BindingSource = new BindingSource();
        private DataTable tab2DataTable = new DataTable();
        private PdfViewerManager pdfViewerManager;
        private LoadingAnimation loadingAnimation; // Add animation control

        public Main()
        {
            InitializeComponent();
            SetupDataGridView();
            pdfViewerManager = new PdfViewerManager(tab2PDFView, tab2DGV, tab1DGV, tab2StatusLabel);

            // Initialize loading animation
            loadingAnimation = new LoadingAnimation
            {
                Visible = false
            };
            tab1DGV.Controls.Add(loadingAnimation);
            UpdateLoadingAnimationPosition();

            btntab1SelectFolder.Enabled = false;
            btntab1Process.Enabled = false;
            tab2DGV.FilterAndSortEnabled = true;
            tab2DGV.SortStringChanged += Tab2DGV_SortStringChanged;
            tab2DGV.FilterStringChanged += Tab2DGV_FilterStringChanged;
            this.tab2DGV.CellMouseDown += tab2DGV_CellMouseDown;
            this.tab2DGV.SizeChanged += (s, e) => UpdateLoadingAnimationPosition(); // Adjust position on resize
            this.Refresh();
        }

        private void UpdateLoadingAnimationPosition()
        {
            if (loadingAnimation != null && tab1DGV != null)
            {
                loadingAnimation.Location = new System.Drawing.Point(
                    (tab1DGV.Width - loadingAnimation.Width) / 2,
                    (tab1DGV.Height - loadingAnimation.Height) / 2
                );
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                tab2PDFView.Visible = true;
                tab2PDFView.Enabled = true;
                tab2StatusLabel.Text = "Status: PdfiumViewer initialized.";
            }
            catch (Exception ex)
            {
                tab2StatusLabel.Text = $"Status: PdfiumViewer failed: {ex.Message}";
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            pdfViewerManager?.Dispose();
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

        // TAB 1
        private void btntab1LoadPdf_Click(object sender, EventArgs e)
        {
            ResetAppState();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tab1StatusLabel.Text = "Status: Processing PDF";
                loadingAnimation.Visible = true; // Show animation
                Application.DoEvents(); // Ensure UI updates

                try
                {
                    sourcePdfPath = openFileDialog.FileName;
                    PdfProcessor.LoadPdf(sourcePdfPath, tab1DGV, ref selectedFolderPath, ref suggestedFolderPath);
                    tab1StatusLabel.Text = "Status: PDF loaded successfully.";
                }
                catch (Exception ex)
                {
                    tab1StatusLabel.Text = $"Status: Error loading PDF: {ex.Message}";
                }
                finally
                {
                    loadingAnimation.Visible = false; // Hide animation
                }
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
            tab1StatusLabel.Text = "Status: Processing...";
            loadingAnimation.Visible = true; // Show animation
            Application.DoEvents(); // Ensure UI updates

            try
            {
                PdfProcessor.ProcessPdfAndSaveResults(sourcePdfPath, selectedFolderPath, tab1DGV);
                tab1StatusLabel.Text = "Status: Split and merge complete!";
            }
            catch (Exception ex)
            {
                tab1StatusLabel.Text = $"Status: Error processing PDF: {ex.Message}";
            }
            finally
            {
                loadingAnimation.Visible = false; // Hide animation
            }
        }

        private void ResetAppState()
        {
            sourcePdfPath = "";
            selectedFolderPath = "";
            suggestedFolderPath = "";
            tab1DGV.Rows.Clear();
            tab1StatusLabel.Text = "Status: Ready, please select a PDF";
        }

        // TAB 2
        private void tab2btnLoadNestPlans_Click(object sender, EventArgs e)
        {
            tab2DGV.Rows.Clear();
            tab2StatusLabel.Text = "Status: Select File(s)";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    tab2DataTable = new DataTable();
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
                tab2DGV.ClearSelection();
                tab2DGV.Rows[e.RowIndex].Selected = true;
                tab2DGV.CurrentCell = tab2DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                tab2RightClick.Show(Cursor.Position);
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
                    string pdfPath = System.IO.Path.Combine("Y:\\PDF Files", $"{planId}.pdf");
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

        private void loadPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pdfViewerManager.LoadPlanPdf();
        }

        private void loadSupportDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pdfViewerManager.LoadSupportDetailPdf();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pdfViewerManager.LoadTestPdf();
        }
    }
}