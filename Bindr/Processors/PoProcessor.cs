using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using CsvHelper;

namespace Bindr.Processors
{
    public static class PoProcessor
    {
        // Helper: Find CSV file with "BOM" in the name
        public static async Task<string> FindBomCsvFileAsync(string folderPath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Directory.GetFiles(folderPath, "*.csv")
                        .FirstOrDefault(file => Path.GetFileName(file).ToLower().Contains("bom"));
                }
                catch
                {
                    return null;
                }
            });
        }

        // Helper: Process the CSV file and return tab-delimited content for clipboard
        public static async Task<string> ProcessCsvFileAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var reader = new StreamReader(filePath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        // Skip header (row 1)
                        csv.Read();
                        csv.ReadHeader();
                        if (csv.HeaderRecord.Length < 12) // Ensure at least 12 columns
                            return null;

                        // Process rows 2+
                        StringBuilder processedContent = new StringBuilder();
                        while (csv.Read())
                        {
                            var row = new string[12];
                            for (int j = 0; j < 12; j++)
                            {
                                string cell = csv[j];
                                if (string.IsNullOrWhiteSpace(cell))
                                {
                                    row[j] = "0"; // Blanks to "0"
                                }
                                else
                                {
                                    // Remove "%" and replace "#" with "0"
                                    cell = cell.Replace("%", "").Replace("#", "0").Trim();
                                    row[j] = cell;
                                }
                            }

                            // Join the row with tabs
                            processedContent.AppendLine(string.Join("\t", row));
                        }

                        return processedContent.ToString().TrimEnd(); // Remove trailing newline
                    }
                }
                catch
                {
                    return null;
                }
            });
        }

        // Helper: Find .xlsx file with specific keywords in the name
        public static async Task<string> FindExcelFileAsync(string folderPath, string keyword)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Directory.GetFiles(folderPath, "*.xlsx")
                        .FirstOrDefault(file => Path.GetFileName(file).ToLower().Contains(keyword.ToLower()));
                }
                catch
                {
                    return null;
                }
            });
        }

        // Helper: Prompt user to select an .xlsx file
        public static async Task<string> PromptForExcelFileAsync(string title, string initialFolder, Form owner)
        {
            return await Task.Run(() =>
            {
                string selectedFile = null;
                owner.Invoke((Action)(() =>
                {
                    using (OpenFileDialog openFileDialog = new OpenFileDialog())
                    {
                        openFileDialog.Title = title;
                        openFileDialog.InitialDirectory = initialFolder;
                        openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        if (openFileDialog.ShowDialog(owner) == DialogResult.OK)
                        {
                            selectedFile = openFileDialog.FileName;
                        }
                    }
                }));
                return selectedFile;
            });
        }

        // Helper: Process Sales Order Excel (A:Q, row 2+)
        public static async Task<string> ProcessSalesOrderExcelAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1); // First sheet
                        var rows = worksheet.RowsUsed().Skip(1); // Skip header (row 1)

                        StringBuilder processedContent = new StringBuilder();
                        foreach (var row in rows)
                        {
                            var cells = new string[17]; // Columns A:Q (1-17)
                            for (int col = 1; col <= 17; col++)
                            {
                                string cell = row.Cell(col).GetString()?.Trim() ?? "0"; // Blanks to "0"
                                cell = Regex.Replace(cell, "#+", "0"); // Replace #, ##, ###, etc. with "0"
                                cells[col - 1] = cell;
                            }
                            processedContent.AppendLine(string.Join("\t", cells));
                        }
                        return processedContent.ToString().TrimEnd(); // Remove trailing newline
                    }
                }
                catch
                {
                    return null;
                }
            });
        }

        // Helper: Process BOM Excel (A:I, row 2+)
        public static async Task<string> ProcessBomExcelAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var workbook = new XLWorkbook(filePath))
                    {
                        var worksheet = workbook.Worksheet(1); // First sheet
                        var rows = worksheet.RowsUsed().Skip(1); // Skip header (row 1)

                        StringBuilder processedContent = new StringBuilder();
                        foreach (var row in rows)
                        {
                            var cells = new string[9]; // Columns A:I (1-9)
                            for (int col = 1; col <= 9; col++)
                            {
                                string cell = row.Cell(col).GetString()?.Trim() ?? "0"; // Blanks to "0"
                                cell = Regex.Replace(cell, "#+", "0"); // Replace #, ##, ###, etc. with "0"
                                cells[col - 1] = cell;
                            }
                            processedContent.AppendLine(string.Join("\t", cells));
                        }
                        return processedContent.ToString().TrimEnd(); // Remove trailing newline
                    }
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}