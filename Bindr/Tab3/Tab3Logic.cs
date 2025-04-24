using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using PdfiumDocument = PdfiumViewer.PdfDocument;

namespace Bindr.Tab3
{
    public class Tab3Logic
    {
        private DataGridView tab3DGV;
        private DataTable tab3DataTableOriginal = new DataTable();
        private DataTable tab3dataTable = new DataTable();
        private DataTable tab3SummaryTable = null;
        private string tab3DataHash = null;
        private bool isSummaryView = false;
        private ContextMenuStrip contextMenu;
        private PdfiumDocument currentPdfDocument;
        private Main mainForm;

        // In your Tab3Logic constructor:
        public Tab3Logic(Main mainForm, DataGridView dataGridView)
        {
            tab3DGV = dataGridView;
            this.mainForm = mainForm;
            // Hook up events directly
            tab3DGV.CellMouseDown += tab3DGV_CellMouseDown;
            tab3DGV.CellFormatting += tab3DGV_CellFormatting;

            // Allow the Main form to hook its events to our methods
            tab3DGV.CellDoubleClick += tab3DGV_CellDoubleClick;

            // Initialize context menu without hooking it directly
            contextMenu = new ContextMenuStrip();
            var detailItem = contextMenu.Items.Add("Open Detail");
            detailItem.Click += openDetailToolStripMenuItem_Click;

            var woItem = contextMenu.Items.Add("Open WO");
            woItem.Click += openWOToolStripMenuItem_Click;
        }

        public void LoadExcelFile()
        {
            // Open File Dialog to pick an Excel file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    // Show loading indicator
                    mainForm.Cursor = Cursors.WaitCursor;

                    // Use Task.Run to load file in background
                    Task.Run(() => LoadExcelFileAsync(filePath))
                        .ContinueWith(t =>
                        {
                            // Return to UI thread
                            mainForm.BeginInvoke(new Action(() =>
                            {
                                mainForm.Cursor = Cursors.Default;
                                if (t.Exception != null)
                                    MessageBox.Show($"Error loading Excel file: {t.Exception.InnerException?.Message}");
                            }));
                        });
                }
            }
        }

        private DataTable LoadExcelFileAsync(string filePath)
        {
            try
            {
                DataTable newDataTable = new DataTable();

                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1);
                    var range = worksheet.RangeUsed();

                    // Get headers at once
                    var headerRow = range.FirstRow();
                    foreach (var cell in headerRow.Cells())
                    {
                        newDataTable.Columns.Add(cell.Value.ToString());
                    }

                    // Pre-calculate dimensions
                    int rowCount = range.RowCount() - 1; // Exclude header
                    int colCount = range.ColumnCount();

                    // Process data rows in bulk
                    object[,] data = new object[rowCount, colCount];

                    int rowIndex = 0;
                    foreach (var row in range.RowsUsed().Skip(1)) // Skip header
                    {
                        int colIndex = 0;
                        foreach (var cell in row.CellsUsed())
                        {
                            data[rowIndex, colIndex] = cell.Value;
                            colIndex++;
                        }
                        rowIndex++;
                    }

                    // Add all rows at once
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow newRow = newDataTable.NewRow();
                        for (int j = 0; j < colCount; j++)
                        {
                            newRow[j] = data[i, j] ?? DBNull.Value;
                        }
                        newDataTable.Rows.Add(newRow);
                    }
                }

                // Update UI on the UI thread
                mainForm.BeginInvoke(new Action(() =>
                {
                    tab3dataTable = newDataTable;
                    tab3DGV.DataSource = tab3dataTable;
                    tab3DataTableOriginal = tab3dataTable.Copy();

                    // Debug column names
                    foreach (DataColumn col in tab3dataTable.Columns)
                    {
                        Debug.WriteLine($"Column: '{col.ColumnName}'");
                    }
                }));

                return newDataTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void openDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tab3DGV.SelectedRows.Count == 0) return;

            var selectedRow = tab3DGV.SelectedRows[0];

            // Extract values from the appropriate columns
            string po_wo = selectedRow.Cells["PO_WO#"].Value?.ToString() ?? "";
            string tag = selectedRow.Cells["Description/Supt Tag"].Value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(po_wo) || string.IsNullOrWhiteSpace(tag))
            {
                MessageBox.Show("Missing required data.");
                return;
            }

            string[] parts = po_wo.Split('_');
            if (parts.Length != 2)
            {
                MessageBox.Show("Invalid PO_WO# format. Expected format: JOB_PO.");
                return;
            }

            string job = parts[0];
            string po = parts[1];

            // Pad job to 5 characters
            string jobLong = job.PadLeft(5, '0');

            // Add .pdf to the tag
            string tagFile = $"{tag}.pdf";

            // Construct the path
            string filePath = $@"Z:\Jobs\PS Job {jobLong}\{job}-{po}\{tagFile}";

            try
            {
                if (File.Exists(filePath))
                {
                    // Load the PDF into the PdfViewer control in the 4th tab
                    var pdfViewer = mainForm.GetTab4PdfViewer(); // Custom method to get PdfViewer
                    if (pdfViewer != null)
                    {
                        // Dispose of any existing document
                        if (currentPdfDocument != null)
                        {
                            currentPdfDocument.Dispose();
                            currentPdfDocument = null;
                        }

                        // Load the new PDF
                        currentPdfDocument = PdfiumViewer.PdfDocument.Load(filePath);
                        pdfViewer.Document = currentPdfDocument;

                        // Switch to the 4th tab
                        mainForm.SwitchToTab4(); // Custom method to switch tabs
                    }
                    else
                    {
                        MessageBox.Show("PDF Viewer control not found in the 4th tab.");
                    }
                }
                else
                {
                    MessageBox.Show($"File not found:\n{filePath}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file:\n{ex.Message}");
            }
        }

        private void tab3DGV_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                tab3DGV.ClearSelection();
                tab3DGV.Rows[e.RowIndex].Selected = true;
                tab3DGV.CurrentCell = tab3DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];

                // Show context menu
                contextMenu.Show(tab3DGV, e.Location);
            }
        }

        public void GenerateSummary()
        {
            if (tab3dataTable == null)
            {
                MessageBox.Show("Load data first.");
                return;
            }

            string currentHash = GetTableHash(tab3dataTable);

            if (tab3SummaryTable != null && tab3DataHash == currentHash)
            {
                tab3DGV.DataSource = tab3SummaryTable;
                return;
            }

            var summaryDict = new Dictionary<string, Dictionary<string, decimal>>();

            foreach (DataRow row in tab3dataTable.Rows)
            {
                string po_wo = row["PO_WO#"]?.ToString() ?? "";
                string status = row["WO Status"]?.ToString() ?? "";
                string qtyOrderedStr = row["Quantity Ordered"]?.ToString() ?? "0";

                if (string.IsNullOrWhiteSpace(po_wo) || string.IsNullOrWhiteSpace(status) || !decimal.TryParse(qtyOrderedStr, out decimal qtyOrdered))
                    continue;

                if (int.TryParse(status, out int statusInt) && statusInt >= 92)
                    continue;

                string job = po_wo.Split('_')[0];

                if (!summaryDict.ContainsKey(job))
                    summaryDict[job] = new Dictionary<string, decimal>();

                if (!summaryDict[job].ContainsKey(status))
                    summaryDict[job][status] = 0;

                summaryDict[job][status] += qtyOrdered;
            }

            var allStatuses = summaryDict
                .SelectMany(kvp => kvp.Value.Keys)
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            tab3SummaryTable = new DataTable();
            tab3SummaryTable.Columns.Add("Job");

            foreach (string status in allStatuses)
                tab3SummaryTable.Columns.Add(status);

            tab3SummaryTable.Columns.Add("TOTALS");

            Dictionary<string, decimal> totalsDict = allStatuses.ToDictionary(s => s, s => 0m);
            decimal grandTotal = 0;

            foreach (var jobEntry in summaryDict)
            {
                DataRow newRow = tab3SummaryTable.NewRow();
                newRow["Job"] = jobEntry.Key;

                decimal rowTotal = 0;

                foreach (string status in allStatuses)
                {
                    decimal qtySum = jobEntry.Value.ContainsKey(status) ? jobEntry.Value[status] : 0;
                    newRow[status] = qtySum;
                    totalsDict[status] += qtySum;
                    rowTotal += qtySum;
                }

                newRow["TOTALS"] = rowTotal;
                grandTotal += rowTotal;

                tab3SummaryTable.Rows.Add(newRow);
            }

            DataRow totalsRow = tab3SummaryTable.NewRow();
            totalsRow["Job"] = "TOTALS";

            foreach (string status in allStatuses)
            {
                totalsRow[status] = totalsDict[status];
            }

            totalsRow["TOTALS"] = grandTotal;
            tab3SummaryTable.Rows.Add(totalsRow);

            tab3DataHash = currentHash;
            tab3DGV.DataSource = tab3SummaryTable;
            isSummaryView = true;
        }

        private void tab3DGV_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv.Rows[e.RowIndex].IsNewRow)
                return;

            DataGridViewRow row = dgv.Rows[e.RowIndex];
            DataGridViewColumn column = dgv.Columns[e.ColumnIndex];

            bool isTotalsRow = row.Cells[0].Value?.ToString() == "TOTALS";
            bool isTotalsCol = column.HeaderText == "TOTALS";

            if (isTotalsRow || isTotalsCol)
            {
                e.CellStyle.Font = new Font(dgv.Font, FontStyle.Bold);
                e.CellStyle.BackColor = Color.LightSteelBlue; // Soft, subtle color
            }
        }

        public void ResetView()
        {
            tab3DGV.DataSource = tab3DataTableOriginal;
            isSummaryView = false;
        }

        private string GetTableHash(DataTable table)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var sb = new System.Text.StringBuilder();

                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                        sb.Append(item?.ToString() ?? "");
                }

                byte[] hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sb.ToString()));
                return Convert.ToBase64String(hash);
            }
        }

        public void tab3DGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header row/column clicks
            if (e.RowIndex < 0 || e.ColumnIndex < 1)
                return;

            // Check if we're in the summary view
            if (!isSummaryView)
                return;

            try
            {
                // Get job and status
                string job = tab3DGV.Rows[e.RowIndex].Cells[0].Value?.ToString(); // Job column is always first
                string status = tab3DGV.Columns[e.ColumnIndex].HeaderText;

                // Check for valid column names
                string poColumn = tab3DataTableOriginal.Columns
                    .Cast<DataColumn>()
                    .FirstOrDefault(c => c.ColumnName.Trim() == "PO_WO#")?.ColumnName;

                string statusColumn = tab3DataTableOriginal.Columns
                    .Cast<DataColumn>()
                    .FirstOrDefault(c => c.ColumnName.Trim() == "WO Status")?.ColumnName;

                if (poColumn == null || statusColumn == null)
                {
                    MessageBox.Show("Expected columns not found.");
                    return;
                }

                // Filter rows
                var filtered = tab3DataTableOriginal.AsEnumerable()
                    .Where(row =>
                    {
                        string po_wo = row[poColumn]?.ToString() ?? "";
                        string rowStatus = row[statusColumn]?.ToString() ?? "";
                        string rowJob = po_wo.Split('_')[0];

                        return rowJob == job && rowStatus == status;
                    });

                if (!filtered.Any())
                {
                    MessageBox.Show("No matching data found.");
                    return;
                }

                // Create filtered table
                DataTable filteredTable = filtered.CopyToDataTable();

                // Display filtered results
                tab3DGV.DataSource = filteredTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void openWOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tab3DGV.SelectedRows.Count == 0) return;

            var selectedRow = tab3DGV.SelectedRows[0];
            string po_wo = selectedRow.Cells["PO_WO#"].Value?.ToString() ?? "";
            string tag = selectedRow.Cells["Description/Supt Tag"].Value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(po_wo) || string.IsNullOrWhiteSpace(tag))
            {
                MessageBox.Show("PO_WO# and Description/Supt Tag are required.");
                return;
            }

            string[] parts = po_wo.Split('_');
            if (parts.Length != 2)
            {
                MessageBox.Show("Invalid PO_WO# format. Expected format: JOB_PO.");
                return;
            }

            string job = parts[0];
            string po = parts[1];

            string jobLong = job.PadLeft(5, '0');
            string jobFolder = $@"Z:\Jobs\PS Job {jobLong}\{job}-{po}";
            string searchTerm = po_wo.Replace('_', '-');
            string tagMergedFileName = $"{tag}_merged.pdf";
            string targetFile = null;
            try
            {
                if (!Directory.Exists(jobFolder))
                {
                    MessageBox.Show($"Folder not found:\n{jobFolder}");
                    return;
                }

                // 1. Search all subfolders for tag_merged.pdf
                var subDirs = Directory.GetDirectories(jobFolder);
                foreach (var dir in subDirs)
                {
                    var files = Directory.GetFiles(dir);
                    targetFile = files.FirstOrDefault(f =>
                        Path.GetFileName(f).Equals(tagMergedFileName, StringComparison.OrdinalIgnoreCase));

                    if (targetFile != null)
                        break;
                }

                // 2. Search in main job folder
                if (targetFile == null)
                {
                    var rootFiles = Directory.GetFiles(jobFolder);
                    targetFile = rootFiles.FirstOrDefault(f =>
                        Path.GetFileName(f).IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                // 3. Search in subfolders for PO-WO match
                if (targetFile == null)
                {
                    foreach (var dir in subDirs)
                    {
                        var files = Directory.GetFiles(dir);
                        targetFile = files.FirstOrDefault(f =>
                            Path.GetFileName(f).IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);

                        if (targetFile != null)
                            break;
                    }
                }

                // 4. Load the file in PdfViewer or prompt user
                var pdfViewer = mainForm.GetTab4PdfViewer();
                if (pdfViewer == null)
                {
                    MessageBox.Show("PDF Viewer control not found in the 4th tab.");
                    return;
                }

                if (targetFile != null)
                {
                    if (!File.Exists(targetFile))
                    {
                        MessageBox.Show($"File does not exist:\n{targetFile}");
                        return;
                    }

                    // Load PDF into PdfViewer
                    if (currentPdfDocument != null)
                    {
                        currentPdfDocument.Dispose();
                        currentPdfDocument = null;
                    }

                    currentPdfDocument = PdfiumViewer.PdfDocument.Load(targetFile);
                    pdfViewer.Document = currentPdfDocument;
                    mainForm.SwitchToTab4();
                }
                else
                {
                    MessageBox.Show("File not found automatically. Please select it manually.");

                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.InitialDirectory = jobFolder;
                        openFileDialog.Title = "Select Work Order File";
                        openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            targetFile = openFileDialog.FileName;
                            if (!File.Exists(targetFile))
                            {
                                MessageBox.Show($"Selected file does not exist:\n{targetFile}");
                                return;
                            }

                            // Load selected PDF into PdfViewer
                            if (currentPdfDocument != null)
                            {
                                currentPdfDocument.Dispose();
                                currentPdfDocument = null;
                            }

                            currentPdfDocument = PdfiumViewer.PdfDocument.Load(targetFile);
                            pdfViewer.Document = currentPdfDocument;
                            mainForm.SwitchToTab4();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file:\n{(targetFile ?? jobFolder)}\n{ex.Message}\n{ex.InnerException?.Message}");
            }
        }

        public void GenerateFGSummary()
        {
            if (tab3dataTable == null)
            {
                MessageBox.Show("Load data first.");
                return;
            }

            // Dictionary to hold FG prefix and summed quantity
            Dictionary<string, int> fgSummary = new Dictionary<string, int>();

            foreach (DataRow row in tab3dataTable.Rows)
            {
                string itemCode = row["Item Number/FG Code"]?.ToString() ?? "";
                string qtyStr = row["Quantity Ordered"]?.ToString() ?? "0";
                string statusStr = row["WO Status"]?.ToString() ?? "";

                // Skip rows with status >= 92
                if (!int.TryParse(statusStr, out int statusVal) || statusVal >= 92)
                    continue;

                if (itemCode.Length < 5)
                    continue;

                string fgPrefix = itemCode.Substring(0, 5);
                if (!int.TryParse(qtyStr, out int qty))
                    qty = 0;

                if (!fgSummary.ContainsKey(fgPrefix))
                    fgSummary[fgPrefix] = 0;

                fgSummary[fgPrefix] += qty;
            }

            // Build the summary DataTable
            DataTable fgSummaryTable = new DataTable();
            fgSummaryTable.Columns.Add("FG Code");
            fgSummaryTable.Columns.Add("Count", typeof(int));

            foreach (var entry in fgSummary.OrderBy(c => c.Key))
            {
                var row = fgSummaryTable.NewRow();
                row["FG Code"] = entry.Key;
                row["Count"] = entry.Value;
                fgSummaryTable.Rows.Add(row);
            }

            // Display the result in the grid
            tab3DGV.DataSource = fgSummaryTable;
        }

        public void HandleCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tab3DGV_CellDoubleClick(sender, e);
        }

        public void HandleOpenDetailClick(object sender, EventArgs e)
        {
            openDetailToolStripMenuItem_Click(sender, e);
        }

        public void HandleOpenWOClick(object sender, EventArgs e)
        {
            openWOToolStripMenuItem_Click(sender, e);
        }
    }
}