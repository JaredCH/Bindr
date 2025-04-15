using System;
using System.IO;
using System.Windows.Forms;

namespace Bindr.Tab1
{
    public class FolderSelectionForm : Form
    {
        private TextBox txtFolderPath;
        private Button btnBrowse;
        private Button btnOK;
        private Button btnCancel;
        public string SelectedPath { get; private set; }

        public FolderSelectionForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Select PS Job Folder";
            this.Size = new System.Drawing.Size(400, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label
            {
                Text = "Enter or browse to a folder with 'PS Job' (e.g., Z:\\Jobs\\PS Job 01555):",
                Location = new System.Drawing.Point(10, 10),
                Size = new System.Drawing.Size(360, 20)
            };

            txtFolderPath = new TextBox
            {
                Location = new System.Drawing.Point(10, 30),
                Size = new System.Drawing.Size(300, 20),
                Text = Properties.Settings.Default.LastJobFolder ?? ""
            };

            btnBrowse = new Button
            {
                Text = "Browse...",
                Location = new System.Drawing.Point(315, 30),
                Size = new System.Drawing.Size(60, 23)
            };
            btnBrowse.Click += BtnBrowse_Click;

            btnOK = new Button
            {
                Text = "OK",
                Location = new System.Drawing.Point(200, 60),
                Size = new System.Drawing.Size(75, 23),
                DialogResult = DialogResult.OK
            };
            btnOK.Click += BtnOK_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new System.Drawing.Point(285, 60),
                Size = new System.Drawing.Size(75, 23),
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lbl, txtFolderPath, btnBrowse, btnOK, btnCancel });
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder containing 'PS Job' (e.g., Z:\\Jobs\\PS Job 01555)";
                dialog.SelectedPath = txtFolderPath.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = dialog.SelectedPath;
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            SelectedPath = txtFolderPath.Text.Trim();
            if (string.IsNullOrEmpty(SelectedPath) || !Directory.Exists(SelectedPath))
            {
                MessageBox.Show("Please enter or select a valid folder path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}