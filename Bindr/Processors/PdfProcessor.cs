using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Bindr
{
    public class PdfProcessor
    {
        public static void LoadPdf(string sourcePdfPath, DataGridView dataGridView, ref string selectedFolderPath, ref string suggestedFolderPath)
        {
            dataGridView.Rows.Clear();

            // Ensure DataGridView has the correct columns
            if (dataGridView.Columns.Count != 7)
            {
                dataGridView.Columns.Clear();
                dataGridView.Columns.Add("PCMK", "PCMK");
                dataGridView.Columns.Add("Job_PO", "Job_PO");
                dataGridView.Columns.Add("FG Code", "FG Code");
                dataGridView.Columns.Add("WO#", "WO#");
                dataGridView.Columns.Add("FolderPath", "Folder Path");
                dataGridView.Columns.Add("Status", "Status");
                dataGridView.Columns.Add("PageNumber", "Page Number");
            }

            using (PdfReader reader = new PdfReader(sourcePdfPath))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    var page = pdfDoc.GetPage(i);
                    string coords1 = Properties.Settings.Default.Coords1;
                    string coords2 = Properties.Settings.Default.Coords2;
                    string coords3 = Properties.Settings.Default.Coords3;
                    string coords4 = Properties.Settings.Default.Coords4;

                    // Parse Coords1 (PCMK)
                    string[] coordsa = coords1.Split(',');
                    int ax = int.Parse(coordsa[0].Trim());
                    int ay = int.Parse(coordsa[1].Trim());
                    int awidth = int.Parse(coordsa[2].Trim());
                    int aheight = int.Parse(coordsa[3].Trim());

                    // Parse Coords2 (Job_PO)
                    string[] coordsb = coords2.Split(',');
                    int bx = int.Parse(coordsb[0].Trim());
                    int by = int.Parse(coordsb[1].Trim());
                    int bwidth = int.Parse(coordsb[2].Trim());
                    int bheight = int.Parse(coordsb[3].Trim());

                    // Parse Coords3
                    int cx = 0, cy = 0, cwidth = 0, cheight = 0;
                    string coords3Text = "";
                    if (!string.IsNullOrEmpty(coords3))
                    {
                        string[] coordsc = coords3.Split(',');
                        cx = int.Parse(coordsc[0].Trim());
                        cy = int.Parse(coordsc[1].Trim());
                        cwidth = int.Parse(coordsc[2].Trim());
                        cheight = int.Parse(coordsc[3].Trim());
                    }

                    // Parse Coords4
                    int dx = 0, dy = 0, dwidth = 0, dheight = 0;
                    string coords4Text = "";
                    if (!string.IsNullOrEmpty(coords4))
                    {
                        string[] coordsd = coords4.Split(',');
                        dx = int.Parse(coordsd[0].Trim());
                        dy = int.Parse(coordsd[1].Trim());
                        dwidth = int.Parse(coordsd[2].Trim());
                        dheight = int.Parse(coordsd[3].Trim());
                    }

                    // PCMK region
                    var rect1 = new iText.Kernel.Geom.Rectangle(ax, ay, awidth, aheight);
                    var strategy1 = new FilteredTextEventListener(
                        new LocationTextExtractionStrategy(),
                        new TextRegionEventFilter(rect1)
                    );
                    string pcmk = PdfTextExtractor.GetTextFromPage(page, strategy1).Trim();

                    // Job_PO region
                    var rect2 = new iText.Kernel.Geom.Rectangle(bx, by, bwidth, bheight);
                    var strategy2 = new FilteredTextEventListener(
                        new LocationTextExtractionStrategy(),
                        new TextRegionEventFilter(rect2)
                    );
                    string jobPo = PdfTextExtractor.GetTextFromPage(page, strategy2).Trim();

                    // Coords3 region
                    if (!string.IsNullOrEmpty(coords3))
                    {
                        var rect3 = new iText.Kernel.Geom.Rectangle(cx, cy, cwidth, cheight);
                        var strategy3 = new FilteredTextEventListener(
                            new LocationTextExtractionStrategy(),
                            new TextRegionEventFilter(rect3)
                        );
                        coords3Text = PdfTextExtractor.GetTextFromPage(page, strategy3).Trim();
                    }

                    // Coords4 region
                    if (!string.IsNullOrEmpty(coords4))
                    {
                        var rect4 = new iText.Kernel.Geom.Rectangle(dx, dy, dwidth, dheight);
                        var strategy4 = new FilteredTextEventListener(
                            new LocationTextExtractionStrategy(),
                            new TextRegionEventFilter(rect4)
                        );
                        coords4Text = PdfTextExtractor.GetTextFromPage(page, strategy4).Trim();
                    }

                    if (!string.IsNullOrWhiteSpace(pcmk))
                    {
                        string folderPath = "";
                        string[] parts = jobPo.Split('_');
                        if (parts.Length == 2)
                        {
                            string jobNoRaw = parts[0];
                            string jobNoPadded = jobNoRaw.PadLeft(5, '0');
                            string po = parts[1];
                            string path = Properties.Settings.Default.Path;
                            folderPath = $@"{path}{jobNoPadded}\{jobNoRaw}-{po}";
                        }

                        // Add to DataGridView: PCMK, Job_PO, Coords3, Coords4, Folder Path, Status, Page Number
                        dataGridView.Rows.Add(pcmk, jobPo, coords3Text, coords4Text, folderPath, "", i);

                        // Optional: Set label to the first folder path encountered
                        if (string.IsNullOrEmpty(suggestedFolderPath) && !string.IsNullOrEmpty(folderPath))
                        {
                            selectedFolderPath = folderPath;
                        }
                    }
                }
            }
        }

        public static void SelectFolder(ref string selectedFolderPath)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderDialog.SelectedPath;
                }
            }
        }

        public static void ProcessPdfAndSaveResults(string sourcePdfPath, string selectedFolderPath, DataGridView dataGridView)
        {
            if (string.IsNullOrEmpty(sourcePdfPath) || string.IsNullOrEmpty(selectedFolderPath))
            {
                MessageBox.Show("Please select both the PDF file and the folder first.");
                return;
            }

            // Ensure DataGridView has the correct columns
            if (dataGridView.Columns.Count != 7)
            {
                MessageBox.Show("DataGridView column configuration is incorrect. Please reload the PDF.");
                return;
            }

            string resultsFolder = System.IO.Path.Combine(selectedFolderPath, "results");
            Directory.CreateDirectory(resultsFolder); // ensure it exists

            using (var reader = new PdfReader(sourcePdfPath))
            using (var pdfDoc = new PdfDocument(reader))
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;

                    string pcmk = row.Cells[0].Value?.ToString()?.Trim();
                    string jobPo = row.Cells[1].Value?.ToString()?.Trim();

                    // Safely parse PageNumber
                    string pageNumberStr = row.Cells[6]?.Value?.ToString();
                    if (string.IsNullOrWhiteSpace(pageNumberStr) || !int.TryParse(pageNumberStr, out int pageNumber))
                    {
                        row.Cells[5].Value = "Invalid Page Number";
                        row.Cells[5].Style.ForeColor = Color.Red;
                        continue;
                    }

                    string singlePagePath = System.IO.Path.Combine(resultsFolder, $"{pcmk}.pdf");

                    // Save single page as {pcmk}.pdf
                    using (var writer = new PdfWriter(singlePagePath))
                    using (var newDoc = new PdfDocument(writer))
                    {
                        pdfDoc.CopyPagesTo(pageNumber, pageNumber, newDoc);
                    }

                    // Look for matching file in selected folder
                    string matchPath = Directory.GetFiles(selectedFolderPath, "*.pdf")
                                                .FirstOrDefault(f =>
                                                    System.IO.Path.GetFileNameWithoutExtension(f).Equals(pcmk, StringComparison.OrdinalIgnoreCase));

                    string status = "No Match Found";
                    string fileFound = "";

                    if (matchPath != null && matchPath != singlePagePath)
                    {
                        string mergedPath = System.IO.Path.Combine(resultsFolder, $"{pcmk}_merged.pdf");

                        using (var writer = new PdfWriter(mergedPath))
                        using (var mergedDoc = new PdfDocument(writer))
                        {
                            using (var doc1 = new PdfDocument(new PdfReader(singlePagePath)))
                            {
                                doc1.CopyPagesTo(1, doc1.GetNumberOfPages(), mergedDoc);
                            }
                            using (var doc2 = new PdfDocument(new PdfReader(matchPath)))
                            {
                                doc2.CopyPagesTo(1, doc2.GetNumberOfPages(), mergedDoc);
                            }
                        }

                        File.Delete(singlePagePath); // optional

                        fileFound = System.IO.Path.GetFileName(matchPath);
                        status = "Matched Successfully";
                    }

                    // Update DataGridView
                    row.Cells[4].Value = fileFound; // Folder Path at index 4
                    row.Cells[5].Value = status;    // Status at index 5
                    row.Cells[5].Style.ForeColor = (status == "Matched Successfully") ? Color.Green : Color.Red;
                }
            }

            // Call MergeAllFilesInResultsFolder if needed
            MergeAllFilesInResultsFolder(resultsFolder, dataGridView);
        }

        public static void MergeAllFilesInResultsFolder(string resultsFolder, DataGridView dataGridView)
        {
            // Ensure that the DataGridView has at least one row
            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("DataGridView is empty, unable to retrieve JobPo.");
                return;
            }

            // Get the JobPo from the first row, column 1
            string jobPo = dataGridView.Rows[0].Cells[1].Value?.ToString()?.Trim();

            if (string.IsNullOrEmpty(jobPo))
            {
                MessageBox.Show("JobPo from DataGridView is empty.");
                return;
            }

            // Get all PDF files in the results folder
            string[] pdfFiles = Directory.GetFiles(resultsFolder, "*.pdf");
            if (pdfFiles.Length == 0)
            {
                MessageBox.Show("No PDF files found in the results folder.");
                return;
            }

            // Define the output path for the merged PDF
            string mergedFilePath = Path.Combine(resultsFolder, $"{jobPo}_Merged.pdf");

            using (var writer = new PdfWriter(mergedFilePath))
            using (var mergedDoc = new PdfDocument(writer))
            {
                foreach (var pdfFile in pdfFiles)
                {
                    using (var pdfReader = new PdfReader(pdfFile))
                    using (var pdfToMerge = new PdfDocument(pdfReader))
                    {
                        // Copy all pages from the current PDF to the merged document
                        pdfToMerge.CopyPagesTo(1, pdfToMerge.GetNumberOfPages(), mergedDoc);
                    }
                }
            }

            MessageBox.Show($"Merged PDF saved to: {mergedFilePath}");
        }
    }
}