using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bindr_new
{
    partial class settingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(settingsForm));
            this.tbCoords1 = new System.Windows.Forms.TextBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbCoords2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbCoords1
            // 
            this.tbCoords1.Location = new System.Drawing.Point(27, 21);
            this.tbCoords1.Name = "tbCoords1";
            this.tbCoords1.Size = new System.Drawing.Size(307, 20);
            this.tbCoords1.TabIndex = 0;
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(28, 101);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(307, 20);
            this.tbPath.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(249, 136);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 21);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbCoords2
            // 
            this.tbCoords2.Location = new System.Drawing.Point(27, 60);
            this.tbCoords2.Name = "tbCoords2";
            this.tbCoords2.Size = new System.Drawing.Size(307, 20);
            this.tbCoords2.TabIndex = 3;
            // 
            // settingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.tbCoords2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.tbCoords1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "settingsForm";
            this.Text = "settingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbCoords1;
        private TextBox tbPath;
        private Button btnSave;
        private TextBox tbCoords2;
    }
}