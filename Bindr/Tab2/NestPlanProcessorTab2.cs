using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bindr.Tab2
{
    public class NestPlanProcessorTab2
    {
        private readonly NestPlanProcessor nestPlanProcessor;

        public NestPlanProcessorTab2()
        {
            nestPlanProcessor = new NestPlanProcessor();
        }

        public async Task LoadNestPlansAsync(DataGridView tab2DGV, Label statusLabel, BindingSource tab2BindingSource, DataTable tab2DataTable)
        {
            tab2DGV.Rows.Clear();
            statusLabel.Text = "Status: Select File(s)";
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
                    statusLabel.Text = "Status: Processing NestPlans";
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
                    statusLabel.Text = "Status: NestPlan Processing Completed";
                }
            }

            await CheckForPdfFilesAsync(tab2DGV);
        }

        private async Task CheckForPdfFilesAsync(DataGridView tab2DGV)
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
    }
}