using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Commons.Utils;

namespace Bindr_new
{
    public partial class settingsForm : Form
    {

        public settingsForm()
        {
            InitializeComponent();

            tbCoords1.Text = Properties.Settings.Default.Coords1;
            tbCoords2.Text = Properties.Settings.Default.Coords2;
            tbPath.Text = Properties.Settings.Default.Path;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Coords1 = tbCoords1.Text;
            Properties.Settings.Default.Coords2 = tbCoords2.Text;
            Properties.Settings.Default.Path = tbPath.Text;
            Properties.Settings.Default.Save();  // Save settings

            // Close the settingsForm
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
