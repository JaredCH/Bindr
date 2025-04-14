using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Bindr
{
    public class PdfProcessor
    {
        // Define a data structure for rows
        public struct PdfRow
        {
            public string Pcmk { get; set; }
            public string JobPo { get; set; }
            public string FgCode { get; set; }
            public string WoNumber { get; set; }
            public string FolderPath { get; set; }
            public string Status { get; set; }
            public int PageNumber { get; set; }
        }

        public static (List<PdfRow> rows, string selectedFolderPath, string suggestedFolderPath) LoadPdf(
            string sourcePdfPath, ref string selectedFolderPath, ref string suggestedFolderPath)
        {
            List<PdfRow> rows = new List<PdfRow>();

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

                        // Add to rows list
                        rows.Add(new PdfRow
                        {
                            Pcmk = pcmk,
                            JobPo = jobPo,
                            FgCode = coords3Text,
                            WoNumber = coords4Text,
                            FolderPath = folderPath,
                            Status = "",
                            PageNumber = i
                        });

                        // Set label to the first folder path encountered
                        if (string.IsNullOrEmpty(suggestedFolderPath) && !string.IsNullOrEmpty(folderPath))
                        {
                            selectedFolderPath = folderPath;
                        }
                    }
                }
            }

            return (rows, selectedFolderPath, suggestedFolderPath);
        }

        public static void SelectFolder(ref string selectedFolderPath)
        {
            // Unchanged, runs on UI thread
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolderPath = folderDialog.SelectedPath;
                }
            }
        }

        public struct RowUpdate
        {
            public int RowIndex { get; set; }
            public string FileFound { get; set; }
            public string Status { get; set; }
        }


        public static List<RowUpdate> ProcessPdfAndSaveResults(string sourcePdfPath, string selectedFolderPath, DataGridView dataGridView)
        {
            if (string.IsNullOrEmpty(sourcePdfPath) || string.IsNullOrEmpty(selectedFolderPath))
            {
                return null; // Let caller handle error
            }

            // Collect row updates instead of modifying DataGridView
            List<RowUpdate> rowUpdates = new List<RowUpdate>();

            string resultsFolder = Path.Combine(selectedFolderPath, "results");
            Directory.CreateDirectory(resultsFolder); // Ensure it exists

            using (var reader = new PdfReader(sourcePdfPath))
            using (var pdfDoc = new PdfDocument(reader))
            {
                int rowIndex = 0;
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (row.IsNewRow) continue;

                    string pcmk = row.Cells[0].Value?.ToString()?.Trim();
                    string jobPo = row.Cells[1].Value?.ToString()?.Trim();

                    // Safely parse PageNumber
                    string pageNumberStr = row.Cells[6]?.Value?.ToString();
                    if (string.IsNullOrWhiteSpace(pageNumberStr) || !int.TryParse(pageNumberStr, out int pageNumber))
                    {
                        rowUpdates.Add(new RowUpdate
                        {
                            RowIndex = rowIndex,
                            FileFound = "",
                            Status = "Invalid Page Number"
                        });
                        rowIndex++;
                        continue;
                    }

                    string singlePagePath = Path.Combine(resultsFolder, $"{pcmk}.pdf");

                    // Save single page as {pcmk}.pdf
                    using (var writer = new PdfWriter(singlePagePath))
                    using (var newDoc = new PdfDocument(writer))
                    {
                        pdfDoc.CopyPagesTo(pageNumber, pageNumber, newDoc);
                    }

                    // Look for matching file in selected folder
                    string matchPath = Directory.GetFiles(selectedFolderPath, "*.pdf")
                                                .FirstOrDefault(f =>
                                                    Path.GetFileNameWithoutExtension(f).Equals(pcmk, StringComparison.OrdinalIgnoreCase));

                    string status = "No Match Found";
                    string fileFound = "";

                    if (matchPath != null && matchPath != singlePagePath)
                    {
                        string mergedPath = Path.Combine(resultsFolder, $"{pcmk}_merged.pdf");

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

                        File.Delete(singlePagePath); // Optional

                        fileFound = Path.GetFileName(matchPath);
                        status = "Matched Successfully";
                    }

                    // Collect update
                    rowUpdates.Add(new RowUpdate
                    {
                        RowIndex = rowIndex,
                        FileFound = fileFound,
                        Status = status
                    });

                    rowIndex++;
                }
            }

            return rowUpdates;
        }

        public static string MergeAllFilesInResultsFolder(string selectedFolderPath, DataGridView dataGridView)
        {
            string resultsFolder = Path.Combine(selectedFolderPath, "results");

            // Get the JobPo from the first row, column 1
            string jobPo = dataGridView.Rows[0].Cells[1].Value?.ToString()?.Trim();

            if (string.IsNullOrEmpty(jobPo))
            {
                return "JobPo from DataGridView is empty.";
            }

            // Get all PDF files in the results folder
            string[] pdfFiles = Directory.GetFiles(resultsFolder, "*.pdf");
            if (pdfFiles.Length == 0)
            {
                return "No PDF files found in the results folder.";
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
                        pdfToMerge.CopyPagesTo(1, pdfToMerge.GetNumberOfPages(), mergedDoc);
                    }
                }
            }

            return $"Merged PDF saved to: {mergedFilePath}";
        }
    }
}