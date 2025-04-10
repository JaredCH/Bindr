// PdfProcessor.cs

using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Bindr_new
{
    public class PdfProcessor
    {
        public static void LoadPdf(string sourcePdfPath, DataGridView dataGridView, ref string selectedFolderPath, ref string suggestedFolderPath)
        {
            dataGridView.Rows.Clear();

            using (PdfReader reader = new PdfReader(sourcePdfPath))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    var page = pdfDoc.GetPage(i);
                    string coords1 = Properties.Settings.Default.Coords1;
                    string coords2 = Properties.Settings.Default.Coords2;

                    string[] coordsa = coords1.Split(',');
                    int ax = int.Parse(coordsa[0].Trim());
                    int ay = int.Parse(coordsa[1].Trim());
                    int awidth = int.Parse(coordsa[2].Trim());
                    int aheight = int.Parse(coordsa[3].Trim());

                    string[] coordsb = coords2.Split(',');
                    int bx = int.Parse(coordsb[0].Trim());
                    int by = int.Parse(coordsb[1].Trim());
                    int bwidth = int.Parse(coordsb[2].Trim());
                    int bheight = int.Parse(coordsb[3].Trim());

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

                        // Add to DataGridView: PCMK, Job_PO, Folder Path, (empty status), Page Number
                        dataGridView.Rows.Add(pcmk, jobPo, folderPath, "", i);

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
                    if (!int.TryParse(row.Cells[4]?.Value?.ToString(), out int pageNumber)) continue;

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
                    row.Cells[2].Value = fileFound;
                    row.Cells[3].Value = status;
                    row.Cells[3].Style.ForeColor = (status == "Matched Successfully") ? Color.Green : Color.Red;
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
