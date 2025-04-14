using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Bindr
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
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pdfPreviewBox = new System.Windows.Forms.PictureBox();
            this.settingsbtnSetPCMK = new System.Windows.Forms.Button();
            this.settingsbtnSetJobPO = new System.Windows.Forms.Button();
            this.txtPcmkRect = new System.Windows.Forms.TextBox();
            this.txtJobPoRect = new System.Windows.Forms.TextBox();
            this.settingsLoadPDF = new System.Windows.Forms.Button();
            this.settingsbtnSetWO = new System.Windows.Forms.Button();
            this.settingsbtnItemNo = new System.Windows.Forms.Button();
            this.txtWORect = new System.Windows.Forms.TextBox();
            this.txtItemNoRect = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pdfPreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 216);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(160, 20);
            this.tbPath.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(87, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 21);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save All";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "X, Y, Width, Height (from bottom left)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "X, Y, Width, Height (from bottom left)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(284, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Directory path plus partial folder name (dont enter numbers)";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pdfPreviewBox);
            this.panel1.Location = new System.Drawing.Point(464, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(637, 600);
            this.panel1.TabIndex = 7;
            // 
            // pdfPreviewBox
            // 
            this.pdfPreviewBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pdfPreviewBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("pdfPreviewBox.InitialImage")));
            this.pdfPreviewBox.Location = new System.Drawing.Point(3, 3);
            this.pdfPreviewBox.Name = "pdfPreviewBox";
            this.pdfPreviewBox.Size = new System.Drawing.Size(631, 594);
            this.pdfPreviewBox.TabIndex = 0;
            this.pdfPreviewBox.TabStop = false;
            this.pdfPreviewBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pdfPreviewBox_MouseDown);
            this.pdfPreviewBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pdfPreviewBox_MouseMove);
            this.pdfPreviewBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pdfPreviewBox_MouseUp);
            // 
            // settingsbtnSetPCMK
            // 
            this.settingsbtnSetPCMK.Location = new System.Drawing.Point(363, 20);
            this.settingsbtnSetPCMK.Name = "settingsbtnSetPCMK";
            this.settingsbtnSetPCMK.Size = new System.Drawing.Size(93, 21);
            this.settingsbtnSetPCMK.TabIndex = 8;
            this.settingsbtnSetPCMK.Text = "Set PCMK";
            this.settingsbtnSetPCMK.UseVisualStyleBackColor = true;
            this.settingsbtnSetPCMK.Click += new System.EventHandler(this.settingsbtnSetPCMK_Click);
            // 
            // settingsbtnSetJobPO
            // 
            this.settingsbtnSetJobPO.Location = new System.Drawing.Point(363, 61);
            this.settingsbtnSetJobPO.Name = "settingsbtnSetJobPO";
            this.settingsbtnSetJobPO.Size = new System.Drawing.Size(91, 20);
            this.settingsbtnSetJobPO.TabIndex = 9;
            this.settingsbtnSetJobPO.Text = "Set JobPo";
            this.settingsbtnSetJobPO.UseVisualStyleBackColor = true;
            this.settingsbtnSetJobPO.Click += new System.EventHandler(this.settingsbtnSetJobPO_Click);
            // 
            // txtPcmkRect
            // 
            this.txtPcmkRect.Location = new System.Drawing.Point(12, 21);
            this.txtPcmkRect.Name = "txtPcmkRect";
            this.txtPcmkRect.Size = new System.Drawing.Size(160, 20);
            this.txtPcmkRect.TabIndex = 10;
            // 
            // txtJobPoRect
            // 
            this.txtJobPoRect.Location = new System.Drawing.Point(12, 61);
            this.txtJobPoRect.Name = "txtJobPoRect";
            this.txtJobPoRect.Size = new System.Drawing.Size(159, 20);
            this.txtJobPoRect.TabIndex = 11;
            // 
            // settingsLoadPDF
            // 
            this.settingsLoadPDF.Location = new System.Drawing.Point(383, 588);
            this.settingsLoadPDF.Name = "settingsLoadPDF";
            this.settingsLoadPDF.Size = new System.Drawing.Size(81, 20);
            this.settingsLoadPDF.TabIndex = 12;
            this.settingsLoadPDF.Text = "Load PDF";
            this.settingsLoadPDF.UseVisualStyleBackColor = true;
            this.settingsLoadPDF.Click += new System.EventHandler(this.settingsLoadPDF_Click);
            // 
            // settingsbtnSetWO
            // 
            this.settingsbtnSetWO.Location = new System.Drawing.Point(364, 106);
            this.settingsbtnSetWO.Name = "settingsbtnSetWO";
            this.settingsbtnSetWO.Size = new System.Drawing.Size(91, 21);
            this.settingsbtnSetWO.TabIndex = 13;
            this.settingsbtnSetWO.Text = "Set WO#";
            this.settingsbtnSetWO.UseVisualStyleBackColor = true;
            this.settingsbtnSetWO.Click += new System.EventHandler(this.settingsbtnSetWO_Click);
            // 
            // settingsbtnItemNo
            // 
            this.settingsbtnItemNo.Location = new System.Drawing.Point(363, 149);
            this.settingsbtnItemNo.Name = "settingsbtnItemNo";
            this.settingsbtnItemNo.Size = new System.Drawing.Size(91, 20);
            this.settingsbtnItemNo.TabIndex = 14;
            this.settingsbtnItemNo.Text = "Set ItemNo";
            this.settingsbtnItemNo.UseVisualStyleBackColor = true;
            this.settingsbtnItemNo.Click += new System.EventHandler(this.settingsbtnItemNo_Click);
            // 
            // txtWORect
            // 
            this.txtWORect.Location = new System.Drawing.Point(12, 106);
            this.txtWORect.Name = "txtWORect";
            this.txtWORect.Size = new System.Drawing.Size(160, 20);
            this.txtWORect.TabIndex = 15;
            // 
            // txtItemNoRect
            // 
            this.txtItemNoRect.Location = new System.Drawing.Point(12, 149);
            this.txtItemNoRect.Name = "txtItemNoRect";
            this.txtItemNoRect.Size = new System.Drawing.Size(160, 20);
            this.txtItemNoRect.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(179, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "X, Y, Width, Height (from bottom left)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(179, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "X, Y, Width, Height (from bottom left)";
            // 
            // settingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 624);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtItemNoRect);
            this.Controls.Add(this.txtWORect);
            this.Controls.Add(this.settingsbtnItemNo);
            this.Controls.Add(this.settingsbtnSetWO);
            this.Controls.Add(this.settingsLoadPDF);
            this.Controls.Add(this.txtJobPoRect);
            this.Controls.Add(this.txtPcmkRect);
            this.Controls.Add(this.settingsbtnSetJobPO);
            this.Controls.Add(this.settingsbtnSetPCMK);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "settingsForm";
            this.Text = "Bindr Settings";
            this.Load += new System.EventHandler(this.settingsForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pdfPreviewBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox tbPath;
        private Button btnSave;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
        private PictureBox pdfPreviewBox;
        private Button settingsbtnSetJobPO;
        private TextBox txtPcmkRect;
        private TextBox txtJobPoRect;
        private Button settingsLoadPDF;
        private Button settingsbtnSetPCMK;
        private Button settingsbtnSetWO;
        private Button settingsbtnItemNo;
        private TextBox txtWORect;
        private TextBox txtItemNoRect;
        private Label label4;
        private Label label5;
    }
}