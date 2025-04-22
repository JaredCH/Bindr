using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bindr.Processors; // Added for PoProcessor
using Bindr.Tab3;
using ClosedXML.Excel;


//TODO
//(look for open email window, list out attatchments, determine which one has the info we need based on name, extract a date from that pdf
//digital cubby system to auto sort, then auto print, so i dont have to sort anymore.
//((get highest grade plate, and thinnest plate thickness, build out a merged pdf of results, print,
//do that until all pdfs are consumed
//work out a reporting tab so its out of excel.
//
//
//

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
        private DataTable tab3DataTableOriginal = new DataTable();
        private DataTable tab3dataTable = new DataTable();
        private PdfViewerManager pdfViewerManager;
        private LoadingAnimation loadingAnimation; // Add animation control
        private ContextMenuStrip loadSOContextMenu; // Context menu for Load SO button
        private ContextMenuStrip loadBOMContextMenu; // Context menu for Load BOM button
        private DataTable tab3SummaryTable = null;
        private string tab3DataHash = null;
        private bool isSummaryView = false;
        private Tab3Logic tab3Logic;



        public Main()
        {
            InitializeComponent();
            tab3Logic = new Tab3Logic(this, tab3DGV);
            // Add Paint event handlers for down arrows
            btntab1LoadSO.Paint += (s, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    // Triangle points: 8px wide, 6px tall, 5px from right edge
                    PointF[] points = new PointF[]
                    {
            new PointF(btntab1LoadSO.Width - 20, btntab1LoadSO.Height / 2 - 3), // Left
            new PointF(btntab1LoadSO.Width - 12, btntab1LoadSO.Height / 2 - 3),  // Right
            new PointF(btntab1LoadSO.Width - 16, btntab1LoadSO.Height / 2 + 3)   // Bottom
                    };
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillPolygon(brush, points);
                }
            };
            btntab1LoadBOM.Paint += (s, e) =>
            {
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    // Triangle points: 8px wide, 6px tall, 5px from right edge
                    PointF[] points = new PointF[]
                    {
            new PointF(btntab1LoadBOM.Width - 20, btntab1LoadBOM.Height / 2 - 3), // Left
            new PointF(btntab1LoadBOM.Width - 12, btntab1LoadBOM.Height / 2 - 3),  // Right
            new PointF(btntab1LoadBOM.Width - 16, btntab1LoadBOM.Height / 2 + 3)   // Bottom
                    };
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.FillPolygon(brush, points);
                }
            };

            tab1DGV.GetType()
               .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
               .SetValue(tab1DGV, true);
            tab2DGV.GetType()
               .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
               .SetValue(tab1DGV, true);

            SetupDataGridView();
            pdfViewerManager = new PdfViewerManager(tab2PDFView, tab2DGV, tab1DGV, tab2StatusLabel);

            // Initialize loading animation
            loadingAnimation = new LoadingAnimation
            {
                Visible = false
            };
            tab1DGV.Controls.Add(loadingAnimation);
            UpdateLoadingAnimationPosition();

            // Initialize context menus for Load SO and Load BOM buttons
            loadSOContextMenu = new ContextMenuStrip();
            loadSOContextMenu.Items.Add("Manually Select File", null, async (s, e) => await ManuallySelectSOFile());
            btntab1LoadSO.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                    loadSOContextMenu.Show(btntab1LoadSO, e.Location);
            };

            loadBOMContextMenu = new ContextMenuStrip();
            loadBOMContextMenu.Items.Add("Manually Select File", null, async (s, e) => await ManuallySelectBOMFile());
            btntab1LoadBOM.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                    loadBOMContextMenu.Show(btntab1LoadBOM, e.Location);
            };

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

        public void UpdateLoadingAnimationPositionForTab3()
        {
            if (loadingAnimation != null && tab3DGV != null)
            {
                loadingAnimation.Location = new System.Drawing.Point(
                    (tab3DGV.Width - loadingAnimation.Width) / 2,
                    (tab3DGV.Height - loadingAnimation.Height) / 2
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
        private async void btntab1LoadPdf_Click(object sender, EventArgs e)
        {
            ResetAppState();
            try
            {
                // Open file dialog with last saved folder
                string selectedFile = null;
                await Task.Run(() =>
                {
                    Invoke((Action)(() =>
                    {
                        using (var openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

                            // Set initial directory from LastJobFolder
                            string lastFolder = Properties.Settings.Default.LastJobFolder;
                            if (!string.IsNullOrEmpty(lastFolder) && Directory.Exists(lastFolder))
                            {
                                openFileDialog.InitialDirectory = lastFolder;
                            }
                            else
                            {
                                // Fallback to a default
                                openFileDialog.InitialDirectory = @"Z:\Jobs"; // Customize if needed
                            }

                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                selectedFile = openFileDialog.FileName;
                            }
                        }
                    }));
                });

                if (selectedFile != null)
                {
                    tab1StatusLabel.Text = "Status: Processing PDF";
                    loadingAnimation.Visible = true; // Show animation

                    try
                    {
                        sourcePdfPath = selectedFile;

                        // Run PDF processing in a background thread and get results
                        var (rows, selectedFolder, suggestedFolder) = await Task.Run(() =>
                            PdfProcessor.LoadPdf(sourcePdfPath, ref selectedFolderPath, ref suggestedFolderPath)
                        );

                        // Update DataGridView on UI thread
                        await InvokeAsync(() =>
                        {
                            tab1DGV.Rows.Clear();
                            if (tab1DGV.Columns.Count != 7)
                            {
                                tab1DGV.Columns.Clear();
                                tab1DGV.Columns.Add("PCMK", "PCMK");
                                tab1DGV.Columns.Add("Job_PO", "Job_PO");
                                tab1DGV.Columns.Add("FG Code", "FG Code");
                                tab1DGV.Columns.Add("WO#", "WO#");
                                tab1DGV.Columns.Add("FolderPath", "Folder Path");
                                tab1DGV.Columns.Add("Status", "Status");
                                tab1DGV.Columns.Add("PageNumber", "Page Number");
                            }
                            foreach (var row in rows)
                            {
                                tab1DGV.Rows.Add(row.Pcmk, row.JobPo, row.FgCode, row.WoNumber, row.FolderPath, row.Status, row.PageNumber);
                            }

                            // Update folder paths
                            selectedFolderPath = selectedFolder;
                            suggestedFolderPath = suggestedFolder;

                            tab1StatusLabel.Text = "Status: PDF loaded successfully.";
                        });
                    }
                    catch (Exception ex)
                    {
                        await InvokeAsync(() =>
                        {
                            tab1StatusLabel.Text = $"Status: Error loading PDF: {ex.Message}";
                        });
                    }
                    finally
                    {
                        await InvokeAsync(() =>
                        {
                            loadingAnimation.Visible = false; // Hide animation
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await InvokeAsync(() =>
                {
                    tab1StatusLabel.Text = $"Status: Error: {ex.Message}";
                    loadingAnimation.Visible = false;
                    MessageBox.Show($"Error opening dialog: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                });
            }

            await InvokeAsync(() =>
            {
                btntab1SelectFolder.Enabled = true;
                btntab1Process.Enabled = true;
            });
        }

        private void btntab1SelectFolder_Click(object sender, EventArgs e)
        {
            PdfProcessor.SelectFolder(ref selectedFolderPath);
            tab1StatusLabel.Text = "Status: Selected Folder: " + selectedFolderPath;
        }

        private async void btntab1Process_Click(object sender, EventArgs e)
        {
            tab1StatusLabel.Text = "Status: Processing...";
            loadingAnimation.Visible = true; // Show animation

            try
            {
                // Run PDF processing and merging in a background thread
                var (rowUpdates, mergeMessage) = await Task.Run(() =>
                {
                    var updates = PdfProcessor.ProcessPdfAndSaveResults(sourcePdfPath, selectedFolderPath, tab1DGV);
                    string message = "";
                    if (updates != null) // Only merge if processing succeeded
                    {
                        message = PdfProcessor.MergeAllFilesInResultsFolder(selectedFolderPath, tab1DGV);
                    }
                    return (updates, message);
                });

                if (rowUpdates == null)
                {
                    tab1StatusLabel.Text = "Status: Processing failed. Check inputs.";
                }
                else
                {
                    // Apply DataGridView updates on UI thread
                    foreach (var update in rowUpdates)
                    {
                        var row = tab1DGV.Rows[update.RowIndex];
                        
                        row.Cells[5].Value = update.Status;    // Status
                        row.Cells[5].Style.ForeColor = update.Status == "Matched Successfully" ? Color.Green : Color.Red;
                    }

                    tab1StatusLabel.Text = "Status: Split and merge complete!";
                    if (!string.IsNullOrEmpty(mergeMessage))
                    {
                        MessageBox.Show(mergeMessage);
                    }
                }
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

        private async void btntab1ProcessBOM_Click(object sender, EventArgs e)
        {
            try
            {
                // Show folder selection form
                string targetFolder = await Task.Run(() =>
                {
                    string selectedPath = null;
                    Invoke((Action)(() =>
                    {
                        using (var form = new FolderSelectionForm())
                        {
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                selectedPath = form.SelectedPath;
                            }
                        }
                    }));
                    return selectedPath;
                });

                if (string.IsNullOrEmpty(targetFolder) ||
                    targetFolder.IndexOf("PS Job", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    await InvokeAsync(() => MessageBox.Show("No valid folder with 'PS Job' selected. Please choose a path like 'Z:\\Jobs\\PS Job 01555'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                    return;
                }

                // Save the selected folder for next time
                Properties.Settings.Default.LastJobFolder = targetFolder;
                Properties.Settings.Default.Save();

                // Step 1: Find CSV file with "BOM" in the name
                string bomCsvFile = await PoProcessor.FindBomCsvFileAsync(targetFolder);
                if (string.IsNullOrEmpty(bomCsvFile))
                {
                    await InvokeAsync(() => MessageBox.Show($"No CSV file with 'BOM' found in {targetFolder}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                    return;
                }

                // Step 2: Process the CSV file and copy to clipboard
                string processedContent = await PoProcessor.ProcessCsvFileAsync(bomCsvFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    // Copy to clipboard on the UI thread
                    await InvokeAsync(() => Clipboard.SetText(processedContent));
                    await InvokeAsync(() => MessageBox.Show("Processed content copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await InvokeAsync(() => MessageBox.Show("No valid content to copy from the CSV file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                // Show error on the UI thread
                await InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
        }

        private async void btntab1LoadSO_Click(object sender, EventArgs e)
        {
            try
            {
                string targetFolder = Properties.Settings.Default.LastJobFolder ?? @"Z:\Jobs";

                // Step 1: Find .xlsx file with "sales order" in the name
                string salesOrderFile = await PoProcessor.FindExcelFileAsync(targetFolder, "sales order");
                if (string.IsNullOrEmpty(salesOrderFile))
                {
                    // Prompt user to select file manually
                    salesOrderFile = await PoProcessor.PromptForExcelFileAsync("Select Sales Order Excel File", targetFolder, this);
                    if (string.IsNullOrEmpty(salesOrderFile))
                    {
                        await InvokeAsync(() => MessageBox.Show("No Sales Order file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        return;
                    }
                }

                // Save the selected folder for next time
                Properties.Settings.Default.LastJobFolder = System.IO.Path.GetDirectoryName(salesOrderFile);
                Properties.Settings.Default.Save();

                // Show loading animation
                await InvokeAsync(() => loadingAnimation.Visible = true);

                // Step 2: Process the Excel file and copy to clipboard
                string processedContent = await PoProcessor.ProcessSalesOrderExcelAsync(salesOrderFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    await InvokeAsync(() => Clipboard.SetText(processedContent));
                    await InvokeAsync(() => MessageBox.Show("Sales Order content (A:Q, row 2+) copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await InvokeAsync(() => MessageBox.Show("No valid content to copy from the Sales Order file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                await InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                await InvokeAsync(() => loadingAnimation.Visible = false);
            }
        }

        private async void btntab1LoadBOM_Click(object sender, EventArgs e)
        {
            try
            {
                string targetFolder = Properties.Settings.Default.LastJobFolder ?? @"Z:\Jobs";

                // Step 1: Find .xlsx file with "bill of material" in the name
                string bomFile = await PoProcessor.FindExcelFileAsync(targetFolder, "bill of material");
                if (string.IsNullOrEmpty(bomFile))
                {
                    // Prompt user to select file manually
                    bomFile = await PoProcessor.PromptForExcelFileAsync("Select Bill of Material Excel File", targetFolder, this);
                    if (string.IsNullOrEmpty(bomFile))
                    {
                        await InvokeAsync(() => MessageBox.Show("No Bill of Material file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        return;
                    }
                }

                // Save the selected folder for next time
                Properties.Settings.Default.LastJobFolder = System.IO.Path.GetDirectoryName(bomFile);
                Properties.Settings.Default.Save();

                // Show loading animation
                await InvokeAsync(() => loadingAnimation.Visible = true);

                // Step 2: Process the Excel file and copy to clipboard
                string processedContent = await PoProcessor.ProcessBomExcelAsync(bomFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    await InvokeAsync(() => Clipboard.SetText(processedContent));
                    await InvokeAsync(() => MessageBox.Show("Bill of Material content (A:I, row 2+) copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await InvokeAsync(() => MessageBox.Show("No valid content to copy from the Bill of Material file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                await InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                await InvokeAsync(() => loadingAnimation.Visible = false);
            }
        }

        // Helper: Manually select Sales Order file (right-click)
        private async Task ManuallySelectSOFile()
        {
            try
            {
                string targetFolder = Properties.Settings.Default.LastJobFolder ?? @"Z:\Jobs";

                // Prompt user to select file
                string salesOrderFile = await PoProcessor.PromptForExcelFileAsync("Select Sales Order Excel File", targetFolder, this);
                if (string.IsNullOrEmpty(salesOrderFile))
                {
                    await InvokeAsync(() => MessageBox.Show("No Sales Order file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                    return;
                }

                // Save the selected folder for next time
                Properties.Settings.Default.LastJobFolder = System.IO.Path.GetDirectoryName(salesOrderFile);
                Properties.Settings.Default.Save();

                // Show loading animation
                await InvokeAsync(() => loadingAnimation.Visible = true);

                // Process the Excel file and copy to clipboard
                string processedContent = await PoProcessor.ProcessSalesOrderExcelAsync(salesOrderFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    await InvokeAsync(() => Clipboard.SetText(processedContent));
                    await InvokeAsync(() => MessageBox.Show("Sales Order content (A:Q, row 2+) copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await InvokeAsync(() => MessageBox.Show("No valid content to copy from the Sales Order file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                await InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                await InvokeAsync(() => loadingAnimation.Visible = false);
            }
        }

        // Helper: Manually select BOM file (right-click)
        private async Task ManuallySelectBOMFile()
        {
            try
            {
                string targetFolder = Properties.Settings.Default.LastJobFolder ?? @"Z:\Jobs";

                // Prompt user to select file
                string bomFile = await PoProcessor.PromptForExcelFileAsync("Select Bill of Material Excel File", targetFolder, this);
                if (string.IsNullOrEmpty(bomFile))
                {
                    await InvokeAsync(() => MessageBox.Show("No Bill of Material file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                    return;
                }

                // Save the selected folder for next time
                Properties.Settings.Default.LastJobFolder = System.IO.Path.GetDirectoryName(bomFile);
                Properties.Settings.Default.Save();

                // Show loading animation
                await InvokeAsync(() => loadingAnimation.Visible = true);

                // Process the Excel file and copy to clipboard
                string processedContent = await PoProcessor.ProcessBomExcelAsync(bomFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    await InvokeAsync(() => Clipboard.SetText(processedContent));
                    await InvokeAsync(() => MessageBox.Show("Bill of Material content (A:I, row 2+) copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await InvokeAsync(() => MessageBox.Show("No valid content to copy from the Bill of Material file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                await InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                await InvokeAsync(() => loadingAnimation.Visible = false);
            }
        }

        // Helper: Run UI-related actions on the UI thread
        private async Task InvokeAsync(Action action)
        {
            if (InvokeRequired)
            {
                await Task.Run(() => Invoke(action));
            }
            else
            {
                action();
            }
        }

        // Form to select or paste the folder path
        public class FolderSelectionForm : Form
        {
            private TextBox txtFolderPath;
            private Button btnBrowse;
            private Button btnOK;
            private Button btnCancel;
            public string SelectedPath { get; private set; }

            public FolderSelectionForm()
            {
                InitializeComponents();
            }

            private void InitializeComponents()
            {
                // Form setup
                this.Text = "Select PS Job Folder";
                this.Size = new System.Drawing.Size(400, 150);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.StartPosition = FormStartPosition.CenterParent;

                // Label
                var lbl = new Label
                {
                    Text = "Enter or browse to a folder with 'PS Job' (e.g., Z:\\Jobs\\PS Job 01555):",
                    Location = new System.Drawing.Point(10, 10),
                    Size = new System.Drawing.Size(360, 20)
                };

                // Textbox for path
                txtFolderPath = new TextBox
                {
                    Location = new System.Drawing.Point(10, 30),
                    Size = new System.Drawing.Size(300, 20),
                    Text = Properties.Settings.Default.LastJobFolder ?? ""
                };

                // Browse button
                btnBrowse = new Button
                {
                    Text = "Browse...",
                    Location = new System.Drawing.Point(315, 30),
                    Size = new System.Drawing.Size(60, 23)
                };
                btnBrowse.Click += BtnBrowse_Click;

                // OK button
                btnOK = new Button
                {
                    Text = "OK",
                    Location = new System.Drawing.Point(200, 60),
                    Size = new System.Drawing.Size(75, 23),
                    DialogResult = DialogResult.OK
                };
                btnOK.Click += BtnOK_Click;

                // Cancel button
                btnCancel = new Button
                {
                    Text = "Cancel",
                    Location = new System.Drawing.Point(285, 60),
                    Size = new System.Drawing.Size(75, 23),
                    DialogResult = DialogResult.Cancel
                };

                // Add controls
                this.Controls.AddRange(new Control[] { lbl, txtFolderPath, btnBrowse, btnOK, btnCancel });
                this.AcceptButton = btnOK;
                this.CancelButton = btnCancel;
            }

            private void BtnBrowse_Click(object sender, EventArgs e)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "Select a folder containing 'PS Job' (e.g., Z:\\Jobs\\PS Job 01555)";
                    dialog.SelectedPath = txtFolderPath.Text;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        txtFolderPath.Text = dialog.SelectedPath;
                    }
                }
            }

            private void BtnOK_Click(object sender, EventArgs e)
            {
                SelectedPath = txtFolderPath.Text.Trim();
                if (string.IsNullOrEmpty(SelectedPath) || !Directory.Exists(SelectedPath))
                {
                    MessageBox.Show("Please enter or select a valid folder path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btntab3LoadReport_Click(object sender, EventArgs e)
        {
            tab3Logic.LoadExcelFile();

        }

        public PdfiumViewer.PdfViewer GetTab4PdfViewer()
        {
            // Return the PdfViewer control from the 4th tab (index 3)
            // Adjust the index or control name based on your setup
            if (MainTab.TabPages.Count > 3)
            {
                var tab4 = MainTab.TabPages[3]; // 4th tab
                return tab4.Controls.OfType<PdfiumViewer.PdfViewer>().FirstOrDefault();
            }
            return null;
        }

        public void SwitchToTab4()
        {
            // Switch to the 4th tab (index 3)
            if (MainTab.TabPages.Count > 3)
            {
                MainTab.SelectedIndex = 3;
            }
        }



        private void btntab3summarize_Click(object sender, EventArgs e)
        {tab3Logic.GenerateSummary();}
        private void btntab3reset_Click(object sender, EventArgs e)
        {tab3Logic.ResetView();}
        private void btntab3FGSort_Click(object sender, EventArgs e)
        {tab3Logic.GenerateFGSummary();}
        private void tab3DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {/*Forward the event to Tab3Logic*/if (tab3Logic != null){tab3Logic.HandleCellDoubleClick(sender, e);}}
        private void openDetailToolStripMenuItem_Click(object sender, EventArgs e)
        { /*Forward the event to Tab3Logic*/if (tab3Logic != null){tab3Logic.HandleOpenDetailClick(sender, e);}}
        private void openWOToolStripMenuItem_Click(object sender, EventArgs e)
        {/*Forward the event to Tab3Logic*/if (tab3Logic != null){tab3Logic.HandleOpenWOClick(sender, e);}}

    }
}