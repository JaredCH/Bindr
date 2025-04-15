using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bindr.Processors;

namespace Bindr.Tab1
{
    public class BomProcessor
    {
        public async Task ProcessBomAsync(Form owner, Label statusLabel, Control loadingAnimation)
        {
            try
            {
                string targetFolder = await Task.Run(() =>
                {
                    string selectedPath = null;
                    owner.Invoke((Action)(() =>
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
                    await owner.InvokeAsync(() => MessageBox.Show("No valid folder with 'PS Job' selected. Please choose a path like 'Z:\\Jobs\\PS Job 01555'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                    return;
                }

                Properties.Settings.Default.LastJobFolder = targetFolder;
                Properties.Settings.Default.Save();

                string bomCsvFile = await PoProcessor.FindBomCsvFileAsync(targetFolder);
                if (string.IsNullOrEmpty(bomCsvFile))
                {
                    await owner.InvokeAsync(() => MessageBox.Show($"No CSV file with 'BOM' found in {targetFolder}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                    return;
                }

                string processedContent = await PoProcessor.ProcessCsvFileAsync(bomCsvFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    await owner.InvokeAsync(() => Clipboard.SetText(processedContent));
                    await owner.InvokeAsync(() => MessageBox.Show("Processed content copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await owner.InvokeAsync(() => MessageBox.Show("No valid content to copy from the CSV file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                await owner.InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
        }

        public async Task LoadSalesOrderAsync(Form owner, Label statusLabel, Control loadingAnimation, bool manualSelect = false)
        {
            try
            {
                string targetFolder = Properties.Settings.Default.LastJobFolder ?? @"Z:\Jobs";
                string salesOrderFile = manualSelect ? null : await PoProcessor.FindExcelFileAsync(targetFolder, "sales order");

                if (string.IsNullOrEmpty(salesOrderFile))
                {
                    salesOrderFile = await PoProcessor.PromptForExcelFileAsync("Select Sales Order Excel File", targetFolder, owner);
                    if (string.IsNullOrEmpty(salesOrderFile))
                    {
                        await owner.InvokeAsync(() => MessageBox.Show("No Sales Order file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        return;
                    }
                }

                Properties.Settings.Default.LastJobFolder = System.IO.Path.GetDirectoryName(salesOrderFile);
                Properties.Settings.Default.Save();

                await owner.InvokeAsync(() => loadingAnimation.Visible = true);

                string processedContent = await PoProcessor.ProcessSalesOrderExcelAsync(salesOrderFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    await owner.InvokeAsync(() => Clipboard.SetText(processedContent));
                    await owner.InvokeAsync(() => MessageBox.Show("Sales Order content (A:Q, row 2+) copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await owner.InvokeAsync(() => MessageBox.Show("No valid content to copy from the Sales Order file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                await owner.InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                await owner.InvokeAsync(() => loadingAnimation.Visible = false);
            }
        }

        public async Task LoadBillOfMaterialAsync(Form owner, Label statusLabel, Control loadingAnimation, bool manualSelect = false)
        {
            try
            {
                string targetFolder = Properties.Settings.Default.LastJobFolder ?? @"Z:\Jobs";
                string bomFile = manualSelect ? null : await PoProcessor.FindExcelFileAsync(targetFolder, "bill of material");

                if (string.IsNullOrEmpty(bomFile))
                {
                    bomFile = await PoProcessor.PromptForExcelFileAsync("Select Bill of Material Excel File", targetFolder, owner);
                    if (string.IsNullOrEmpty(bomFile))
                    {
                        await owner.InvokeAsync(() => MessageBox.Show("No Bill of Material file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                        return;
                    }
                }

                Properties.Settings.Default.LastJobFolder = System.IO.Path.GetDirectoryName(bomFile);
                Properties.Settings.Default.Save();

                await owner.InvokeAsync(() => loadingAnimation.Visible = true);

                string processedContent = await PoProcessor.ProcessBomExcelAsync(bomFile);
                if (!string.IsNullOrEmpty(processedContent))
                {
                    await owner.InvokeAsync(() => Clipboard.SetText(processedContent));
                    await owner.InvokeAsync(() => MessageBox.Show("Bill of Material content (A:I, row 2+) copied to clipboard as tab-delimited text.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information));
                }
                else
                {
                    await owner.InvokeAsync(() => MessageBox.Show("No valid content to copy from the Bill of Material file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning));
                }
            }
            catch (Exception ex)
            {
                await owner.InvokeAsync(() => MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
            finally
            {
                await owner.InvokeAsync(() => loadingAnimation.Visible = false);
            }
        }
    }
}