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
            this.Refresh();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            settingsForm settings = new settingsForm();
            settings.Show();
        }



        private void SetupDataGridView()
        {
            tab1DGV.Columns.Clear();  // Clear any existing columns
            tab1DGV.Columns.Add("PCMK", "PCMK");
            tab1DGV.Columns.Add("JobPO", "Job_PO");
            tab1DGV.Columns.Add("FolderPath", "Folder Path");
            tab1DGV.Columns.Add("Status", "Status");
            tab1DGV.Columns.Add("PageNumber", "Page Number");
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
                sourcePdfPath = openFileDialog.FileName;
                PdfProcessor.LoadPdf(sourcePdfPath, tab1DGV, ref selectedFolderPath, ref suggestedFolderPath);
                statusLabel.Text = "Status: PDF loaded successfully.";
            }
            btntab1SelectFolder.Enabled = true;
            btntab1Process.Enabled = true;
        }

        private void btntab1SelectFolder_Click(object sender, EventArgs e)
        {
            PdfProcessor.SelectFolder(ref selectedFolderPath);
            statusLabel.Text = "Status: Selected Folder: " + selectedFolderPath;
        }

        private void btntab1Process_Click(object sender, EventArgs e)
        {
            PdfProcessor.ProcessPdfAndSaveResults(sourcePdfPath, selectedFolderPath, tab1DGV);
            statusLabel.Text = "Status: Split and merge complete!";
        }

        private void ResetAppState()
        {
            sourcePdfPath = "";
            selectedFolderPath = "";
            suggestedFolderPath = "";
            tab1DGV.Rows.Clear();
            statusLabel.Text = "Status: Ready";
        }



        //TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___
        //TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___
        //TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___TAB 2___
        private void tab2btnLoadNestPlans_Click(object sender, EventArgs e)
        {
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
                }
            }
        }

        private void Tab2DGV_SortStringChanged(object sender, EventArgs e)
        {
            tab2BindingSource.Sort = tab2DGV.SortString;
        }

        private void Tab2DGV_FilterStringChanged(object sender, EventArgs e)
        {
            tab2BindingSource.Filter = tab2DGV.FilterString;
        }


    }


    //PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___
    //PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___
    //PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___PLAY SPACE___



 }

