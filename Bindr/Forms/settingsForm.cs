﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

//todo
//View  current mapping button to show whats currently mapped, and auto show it after the image is loaded.

namespace Bindr
{
    public partial class settingsForm : Form
    {
        private enum RegionMode
        { None, Pcmk, JobPo, WO, ItemNo }

        private enum DrawMode
        { None, Pcmk, JobPo, WO, ItemNo }

        private DrawMode currentDrawMode = DrawMode.None;
        private bool isDrawing = false;
        private Point startPoint; // Pixel coordinates for drawing
        private Rectangle currentRect; // Pixel coordinates for drawing
        private RectangleF pdfRect; // PDF coordinates (points) for output
        private const int dpi = 206; // From RenderPdfPageToBitmap
        private string pdfPath; // Store loaded PDF path

        private List<Rectangle> displayedRectangles = new List<Rectangle>();
        private Dictionary<Rectangle, RegionMode> rectangleTypes = new Dictionary<Rectangle, RegionMode>();

        private Dictionary<RegionMode, Color> regionColors = new Dictionary<RegionMode, Color>
        {
            { RegionMode.Pcmk, Color.Red },
            { RegionMode.JobPo, Color.Blue },
            { RegionMode.WO, Color.Green },
            { RegionMode.ItemNo, Color.Orange }
        };

        public settingsForm()
        {
            InitializeComponent();

            // Wire up Paint event explicitly
            pdfPreviewBox.Paint += pdfPreviewBox_Paint;

            txtPcmkRect.Text = Properties.Settings.Default.Coords1;
            txtJobPoRect.Text = Properties.Settings.Default.Coords2;
            txtItemNoRect.Text = Properties.Settings.Default.Coords3;
            txtWORect.Text = Properties.Settings.Default.Coords4;
            tbPath.Text = Properties.Settings.Default.Path;

            // Load PDF if path exists
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Coords1 = txtPcmkRect.Text;
            Properties.Settings.Default.Coords2 = txtJobPoRect.Text;
            Properties.Settings.Default.Coords3 = txtItemNoRect.Text;
            Properties.Settings.Default.Coords4 = txtWORect.Text;
            Properties.Settings.Default.Path = tbPath.Text;
            Properties.Settings.Default.Save(); // Save settings

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void settingsbtnSetPCMK_Click(object sender, EventArgs e)
        {
            currentDrawMode = DrawMode.Pcmk;
            FlashTextBox(txtPcmkRect, 3, 2000);
            ClearDisplayedRegionsForMode(RegionMode.Pcmk);
        }

        private void settingsbtnSetWO_Click(object sender, EventArgs e)
        {
            currentDrawMode = DrawMode.WO;
            FlashTextBox(txtWORect, 3, 2000);
            ClearDisplayedRegionsForMode(RegionMode.WO);
        }

        private void settingsbtnItemNo_Click(object sender, EventArgs e)
        {
            currentDrawMode = DrawMode.ItemNo;
            FlashTextBox(txtItemNoRect, 3, 2000);
            ClearDisplayedRegionsForMode(RegionMode.ItemNo);
        }

        private void settingsbtnSetJobPO_Click(object sender, EventArgs e)
        {
            currentDrawMode = DrawMode.JobPo;
            FlashTextBox(txtJobPoRect, 3, 2000);
            ClearDisplayedRegionsForMode(RegionMode.JobPo);
        }

        private void pdfPreviewBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentDrawMode != DrawMode.None)
            {
                isDrawing = true;
                startPoint = e.Location; // Store pixel coordinates (top-left)
                currentRect = new Rectangle();
                pdfRect = new RectangleF();
            }
        }

        private void pdfPreviewBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                // Calculate rectangle in pixel coordinates (top-left)
                currentRect = new Rectangle(
                    Math.Min(startPoint.X, e.X),
                    Math.Min(startPoint.Y, e.Y),
                    Math.Abs(startPoint.X - e.X),
                    Math.Abs(startPoint.Y - e.Y)
                );

                // Convert to PDF coordinates (bottom-left, points)
                if (!string.IsNullOrEmpty(pdfPath))
                {
                    pdfRect = PixelsToPdfPoints(currentRect);
                }

                pdfPreviewBox.Invalidate();
            }
        }

        private void pdfPreviewBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                isDrawing = false;

                // Output PDF coordinates (in points)
                string rectStr = string.Empty;
                if (!pdfRect.IsEmpty)
                {
                    rectStr = $"{(int)Math.Round(pdfRect.X)}, {(int)Math.Round(pdfRect.Y)}, {(int)Math.Round(pdfRect.Width)}, {(int)Math.Round(pdfRect.Height)}";
                }
                else
                {
                    // Fallback to pixel coordinates if PDF conversion failed
                    rectStr = $"{currentRect.X}, {currentRect.Y}, {currentRect.Width}, {currentRect.Height}";
                }

                if (currentDrawMode == DrawMode.Pcmk)
                    txtPcmkRect.Text = rectStr;
                else if (currentDrawMode == DrawMode.JobPo)
                    txtJobPoRect.Text = rectStr;
                if (currentDrawMode == DrawMode.ItemNo)
                    txtItemNoRect.Text = rectStr;
                else if (currentDrawMode == DrawMode.WO)
                    txtWORect.Text = rectStr;

                currentDrawMode = DrawMode.None;
                pdfPreviewBox.Invalidate(); // Refresh box
            }
            ShowSavedRegions();
        }

        private void pdfPreviewBox_Paint(object sender, PaintEventArgs e)
        {
            // Draw the currently-being-drawn rectangle
            if (isDrawing && !currentRect.IsEmpty)
            {
                using (var pen = new Pen(Color.Red, 2))
                {
                    e.Graphics.DrawRectangle(pen, currentRect);
                }
            }

            // Draw all saved rectangles
            foreach (var rect in displayedRectangles)
            {
                RegionMode mode = rectangleTypes[rect];
                Color color = regionColors.ContainsKey(mode) ? regionColors[mode] : Color.Gray;

                using (var pen = new Pen(color, 2))
                {
                    e.Graphics.DrawRectangle(pen, rect);

                    // Add a small label to identify the rectangle type
                    string label = mode.ToString();
                    SizeF textSize = e.Graphics.MeasureString(label, this.Font);

                    using (var brush = new SolidBrush(Color.FromArgb(200, color)))
                    {
                        // Draw label background
                        e.Graphics.FillRectangle(brush,
                            rect.X, rect.Y - textSize.Height,
                            textSize.Width, textSize.Height);

                        // Draw text
                        e.Graphics.DrawString(label, this.Font, Brushes.White,
                            rect.X, rect.Y - textSize.Height);
                    }
                }
            }
        }

        private RectangleF PixelsToPdfPoints(Rectangle pixelRect)
        {
            if (string.IsNullOrEmpty(pdfPath) || !System.IO.File.Exists(pdfPath))
            {
                return RectangleF.Empty;
            }

            try
            {
                using (var document = PdfiumViewer.PdfDocument.Load(pdfPath))
                {
                    // Get page size in points (page 0 for simplicity)
                    var pageSize = document.PageSizes[0];
                    float pageWidthPoints = pageSize.Width;
                    float pageHeightPoints = pageSize.Height;

                    // Calculate scaling: pixels to points
                    float scale = 72f / dpi; // 1 pixel = scale points

                    // Map pixel coordinates to PDF points
                    float pdfX = pixelRect.X * scale;
                    float pdfWidth = pixelRect.Width * scale;

                    // Flip Y-axis: convert top-left pixel Y to bottom-left PDF Y
                    float pixelYFromBottom = pdfPreviewBox.Height - (pixelRect.Y + pixelRect.Height);
                    float pdfY = pixelYFromBottom * scale;
                    float pdfHeight = pixelRect.Height * scale;

                    // Adjust for control vs. page scaling
                    float controlAspect = (float)pdfPreviewBox.Width / pdfPreviewBox.Height;
                    float pageAspect = pageWidthPoints / pageHeightPoints;
                    if (Math.Abs(controlAspect - pageAspect) > 0.01)
                    {
                        // Assume PDF is scaled to fit control
                        float scaleX = pageWidthPoints / pdfPreviewBox.Width;
                        float scaleY = pageHeightPoints / pdfPreviewBox.Height;
                        pdfX *= scaleX;
                        pdfWidth *= scaleX;
                        pdfY *= scaleY;
                        pdfHeight *= scaleY;
                    }

                    return new RectangleF(pdfX, pdfY, pdfWidth, pdfHeight);
                }
            }
            catch (Exception)
            {
                return RectangleF.Empty;
            }
        }

        public Bitmap RenderPdfPageToBitmap(string pdfPath, int pageNumber = 0, int dpi = 206)
        {
            using (var document = PdfiumViewer.PdfDocument.Load(pdfPath))
            {
                // Get page size in POINTS (1 point = 1/72 inch)
                var size = document.PageSizes[pageNumber];

                // Calculate scaling factor
                float scale = dpi / 72f;
                int width = (int)Math.Round(size.Width * scale);
                int height = (int)Math.Round(size.Height * scale);

                // Create bitmap with proper pixel format
                var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                bitmap.SetResolution(dpi, dpi);

                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Set graphics quality
                    graphics.Clear(Color.White);
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;

                    // Define rendering bounds
                    var bounds = new Rectangle(0, 0, width, height);

                    // Render flags
                    var renderFlags = PdfiumViewer.PdfRenderFlags.ForPrinting | PdfiumViewer.PdfRenderFlags.Annotations;

                    // Render the page
                    document.Render(pageNumber, graphics, dpi, dpi, bounds, renderFlags);
                }

                return bitmap;
            }
        }

        private void LoadPdfAndDisplayImage(string path)
        {
            try
            {
                var bmp = RenderPdfPageToBitmap(path);
                pdfPreviewBox.Image?.Dispose();
                pdfPreviewBox.Image = bmp;
                pdfPreviewBox.Size = bmp.Size; // Set PictureBox size to image size
                pdfPath = path;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void settingsLoadPDF_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadPdfAndDisplayImage(openFileDialog.FileName); // Load PDF
                    ShowSavedRegions();
                }
            }
        }

        private void FlashTextBox(TextBox textBox, int flashCount, int totalDurationMs)
        {
            if (textBox == null) return;

            Color defaultColor = textBox.BackColor; // Save default color
            Color flashColor = Color.Yellow; // Flash color
            int toggleCount = flashCount * 2; // Each flash is an on-off cycle
            int intervalMs = totalDurationMs / toggleCount; // Time per toggle

            Timer flashTimer = new Timer
            {
                Interval = intervalMs
            };
            int togglesRemaining = toggleCount;

            flashTimer.Tick += (s, args) =>
            {
                if (togglesRemaining <= 0)
                {
                    flashTimer.Stop();
                    textBox.BackColor = defaultColor; // Restore default color
                    flashTimer.Dispose();
                    return;
                }

                textBox.BackColor = (togglesRemaining % 2 == 0) ? flashColor : defaultColor;
                togglesRemaining--;
            };

            flashTimer.Start();
        }

        private void settingsForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("Z:\\Pipe Supports Documentation\\Review\\PS-012_Bindr_R0.docx");
        }

        public void ShowSavedRegions()
        {
            displayedRectangles.Clear();

            // Process each saved rectangle
            if (!string.IsNullOrEmpty(txtPcmkRect.Text))
                AddSavedRectangle(txtPcmkRect.Text, RegionMode.Pcmk);

            if (!string.IsNullOrEmpty(txtJobPoRect.Text))
                AddSavedRectangle(txtJobPoRect.Text, RegionMode.JobPo);

            if (!string.IsNullOrEmpty(txtWORect.Text))
                AddSavedRectangle(txtWORect.Text, RegionMode.WO);

            if (!string.IsNullOrEmpty(txtItemNoRect.Text))
                AddSavedRectangle(txtItemNoRect.Text, RegionMode.ItemNo);

            // Force redraw
            pdfPreviewBox.Invalidate();
        }

        // Convert saved PDF points back to screen pixels
        private void AddSavedRectangle(string coordsText, RegionMode mode)
        {
            try
            {
                // Parse coordinates (x, y, width, height)
                string[] parts = coordsText.Split(',');
                if (parts.Length != 4) return;

                float x = float.Parse(parts[0].Trim());
                float y = float.Parse(parts[1].Trim());
                float width = float.Parse(parts[2].Trim());
                float height = float.Parse(parts[3].Trim());

                RectangleF pdfRectF = new RectangleF(x, y, width, height);
                Rectangle screenRect = PdfPointsToPixels(pdfRectF);

                // Store rectangle with its type in the dictionary
                displayedRectangles.Add(screenRect);
                rectangleTypes[screenRect] = mode;
            }
            catch (Exception ex)
            {
                // Silently handle parsing errors
                Debug.WriteLine($"Error parsing coordinates: {ex.Message}");
            }
        }

        // Convert PDF points to screen pixels (inverse of PixelsToPdfPoints)
        private Rectangle PdfPointsToPixels(RectangleF pdfRect)
        {
            if (string.IsNullOrEmpty(pdfPath) || !System.IO.File.Exists(pdfPath) || pdfPreviewBox.Image == null)
            {
                return Rectangle.Empty;
            }

            try
            {
                using (var document = PdfiumViewer.PdfDocument.Load(pdfPath))
                {
                    // Get page size in points
                    var pageSize = document.PageSizes[0];
                    float pageWidthPoints = pageSize.Width;
                    float pageHeightPoints = pageSize.Height;

                    // Calculate scaling: points to pixels
                    float scale = dpi / 72f; // 1 point = scale pixels

                    // Adjust for control vs. page scaling
                    float controlAspect = (float)pdfPreviewBox.Width / pdfPreviewBox.Height;
                    float pageAspect = pageWidthPoints / pageHeightPoints;

                    float scaleX = 1.0f;
                    float scaleY = 1.0f;

                    if (Math.Abs(controlAspect - pageAspect) > 0.01)
                    {
                        // Assume PDF is scaled to fit control
                        scaleX = pdfPreviewBox.Width / pageWidthPoints;
                        scaleY = pdfPreviewBox.Height / pageHeightPoints;
                    }

                    // Convert PDF points to pixel coordinates
                    int pixelX = (int)Math.Round(pdfRect.X * scale * scaleX);
                    float pdfYFromBottom = pdfRect.Y;

                    // Flip Y-axis: convert bottom-left PDF Y to top-left pixel Y
                    int pixelY = pdfPreviewBox.Height - (int)Math.Round((pdfYFromBottom + pdfRect.Height) * scale * scaleY);
                    int pixelWidth = (int)Math.Round(pdfRect.Width * scale * scaleX);
                    int pixelHeight = (int)Math.Round(pdfRect.Height * scale * scaleY);

                    return new Rectangle(pixelX, pixelY, pixelWidth, pixelHeight);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error converting PDF to pixels: {ex.Message}");
                return Rectangle.Empty;
            }
        }

        // Clear displayed rectangles
        public void ClearDisplayedRegions()
        {
            displayedRectangles.Clear();
            pdfPreviewBox.Invalidate();
        }

        // Call this from each "Set" button click handler
        private void ClearDisplayedRegionsForMode(RegionMode mode)
        {
            // Find rectangles of the specified mode
            List<Rectangle> toRemove = new List<Rectangle>();
            foreach (var kvp in rectangleTypes)
            {
                if (kvp.Value == mode)
                {
                    toRemove.Add(kvp.Key);
                }
            }

            // Remove them from both collections
            foreach (var rect in toRemove)
            {
                displayedRectangles.Remove(rect);
                rectangleTypes.Remove(rect);
            }

            pdfPreviewBox.Invalidate();
        }
    }
}