using System;
using System.IO;
using System.Windows.Forms;
using PdfiumViewer;
using PdfiumDocument = PdfiumViewer.PdfDocument;

namespace Bindr
{
    public class PdfViewerManager
    {
        private readonly PdfViewer pdfViewer;
        private readonly DataGridView tab2DGV;
        private readonly DataGridView tab1DGV;
        private readonly Label statusLabel;
        private PdfiumDocument currentPdfDocument;

        public PdfViewerManager(PdfViewer pdfViewer, DataGridView tab2DGV, DataGridView tab1DGV, Label statusLabel)
        {
            this.pdfViewer = pdfViewer ?? throw new ArgumentNullException(nameof(pdfViewer));
            this.tab2DGV = tab2DGV ?? throw new ArgumentNullException(nameof(tab2DGV));
            this.tab1DGV = tab1DGV ?? throw new ArgumentNullException(nameof(tab1DGV));
            this.statusLabel = statusLabel ?? throw new ArgumentNullException(nameof(statusLabel));
        }

        public void Dispose()
        {
            currentPdfDocument?.Dispose();
            currentPdfDocument = null;
        }

        public void LoadPlanPdf()
        {
            if (tab2DGV.SelectedRows.Count > 0)
            {
                var selectedRow = tab2DGV.SelectedRows[0];
                var planName = selectedRow.Cells["PlanId"].Value?.ToString();
                if (!string.IsNullOrEmpty(planName))
                {
                    try
                    {
                        string pdfPath = Path.Combine("Y:\\PDF Files", planName + ".pdf");
                        if (File.Exists(pdfPath))
                        {
                            LoadPdf(pdfPath);
                            statusLabel.Text = $"Status: Loaded Plan PDF: {pdfPath}";
                        }
                        else
                        {
                            statusLabel.Text = $"Status: Plan PDF not found: {pdfPath}";
                        }
                    }
                    catch (Exception ex)
                    {
                        statusLabel.Text = $"Status: Error loading Plan PDF: {ex.Message}";
                    }
                }
            }
        }

        public void LoadSupportDetailPdf()
        {
            if (tab2DGV.SelectedRows.Count > 0)
            {
                var selectedRow = tab2DGV.SelectedRows[0];
                var partInfo = selectedRow.Cells["PartInfo"].Value?.ToString();
                if (!string.IsNullOrEmpty(partInfo))
                {
                    var partInfoParts = partInfo.Split('-');
                    if (partInfoParts.Length > 0)
                    {
                        string partInfoWoNumber = partInfoParts[0];
                        foreach (DataGridViewRow row in tab1DGV.Rows)
                        {
                            var woNumberFull = row.Cells["WO#"].Value?.ToString();
                            if (!string.IsNullOrEmpty(woNumberFull))
                            {
                                var woNumberParts = woNumberFull.Split(' ');
                                if (woNumberParts.Length > 0)
                                {
                                    string woNumber = woNumberParts[0];
                                    if (woNumber == partInfoWoNumber)
                                    {
                                        string folderPath = row.Cells["FolderPath"].Value?.ToString();
                                        string pcmk = row.Cells["PCMK"].Value?.ToString();
                                        if (!string.IsNullOrEmpty(folderPath) && !string.IsNullOrEmpty(pcmk))
                                        {
                                            try
                                            {
                                                string pdfPath = Path.Combine(folderPath, pcmk + ".pdf");
                                                if (File.Exists(pdfPath))
                                                {
                                                    LoadPdf(pdfPath);
                                                    statusLabel.Text = $"Status: Loaded Support Detail PDF: {pdfPath}";
                                                }
                                                else
                                                {
                                                    statusLabel.Text = $"Status: PDF not found: {pdfPath}";
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                statusLabel.Text = $"Status: Error loading PDF: {ex.Message}";
                                            }
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        statusLabel.Text = "Status: No matching WO# found in tab1DGV.";
                    }
                    else
                    {
                        statusLabel.Text = "Status: Invalid PartInfo format.";
                    }
                }
                else
                {
                    statusLabel.Text = "Status: PartInfo is empty.";
                }
            }
            else
            {
                statusLabel.Text = "Status: No row selected.";
            }
        }

        public void LoadTestPdf()
        {
            try
            {
                string pdfPath = @"Y:\PDF Files\NestPlan007724.pdf";
                if (File.Exists(pdfPath))
                {
                    LoadPdf(pdfPath);
                    statusLabel.Text = $"Status: Loaded Test PDF: {pdfPath}";
                }
                else
                {
                    statusLabel.Text = $"Status: Test PDF not found: {pdfPath}";
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Status: Error loading Test PDF: {ex.Message}";
            }
        }

        private void LoadPdf(string pdfPath)
        {
            // Dispose previous document
            if (currentPdfDocument != null)
            {
                currentPdfDocument.Dispose();
                currentPdfDocument = null;
            }
            // Load new PdfiumViewer document
            currentPdfDocument = PdfiumDocument.Load(pdfPath);
            pdfViewer.Document = currentPdfDocument;
        }
    }
}