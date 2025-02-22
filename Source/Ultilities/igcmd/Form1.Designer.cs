﻿namespace igcmd
{
    partial class frmCheckForUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckForUpdate));
            this.lblStatus = new System.Windows.Forms.Label();
            this.lnkUpdateReadMore = new System.Windows.Forms.LinkLabel();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picStoreApp = new System.Windows.Forms.PictureBox();
            this.txtUpdates = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStoreApp)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(208)))));
            this.lblStatus.Location = new System.Drawing.Point(47, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(309, 41);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Checking for update...";
            // 
            // lnkUpdateReadMore
            // 
            this.lnkUpdateReadMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkUpdateReadMore.AutoSize = true;
            this.lnkUpdateReadMore.BackColor = System.Drawing.Color.Transparent;
            this.lnkUpdateReadMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(185)))));
            this.lnkUpdateReadMore.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkUpdateReadMore.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(185)))));
            this.lnkUpdateReadMore.Location = new System.Drawing.Point(12, 371);
            this.lnkUpdateReadMore.Name = "lnkUpdateReadMore";
            this.lnkUpdateReadMore.Size = new System.Drawing.Size(105, 23);
            this.lnkUpdateReadMore.TabIndex = 14;
            this.lnkUpdateReadMore.TabStop = true;
            this.lnkUpdateReadMore.Text = "Read more...";
            this.lnkUpdateReadMore.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(111)))), ((int)(((byte)(185)))));
            this.lnkUpdateReadMore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUpdateReadMore_LinkClicked);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.AutoSize = true;
            this.btnDownload.Location = new System.Drawing.Point(251, 16);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(115, 42);
            this.btnDownload.TabIndex = 1;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSize = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(372, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 42);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // picStatus
            // 
            this.picStatus.Image = global::igcmd.Properties.Resources.loading;
            this.picStatus.Location = new System.Drawing.Point(9, 14);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(34, 42);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picStatus.TabIndex = 16;
            this.picStatus.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(168)))));
            this.panel1.Controls.Add(this.picStoreApp);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnDownload);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 408);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 74);
            this.panel1.TabIndex = 4;
            // 
            // picStoreApp
            // 
            this.picStoreApp.BackColor = System.Drawing.Color.Black;
            this.picStoreApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picStoreApp.Dock = System.Windows.Forms.DockStyle.Left;
            this.picStoreApp.Image = ((System.Drawing.Image)(resources.GetObject("picStoreApp.Image")));
            this.picStoreApp.Location = new System.Drawing.Point(0, 0);
            this.picStoreApp.Name = "picStoreApp";
            this.picStoreApp.Size = new System.Drawing.Size(215, 74);
            this.picStoreApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStoreApp.TabIndex = 24;
            this.picStoreApp.TabStop = false;
            this.picStoreApp.Click += new System.EventHandler(this.picStoreApp_Click);
            // 
            // txtUpdates
            // 
            this.txtUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpdates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(203)))), ((int)(((byte)(204)))));
            this.txtUpdates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUpdates.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUpdates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(1)))), ((int)(((byte)(1)))));
            this.txtUpdates.Location = new System.Drawing.Point(16, 77);
            this.txtUpdates.Margin = new System.Windows.Forms.Padding(4);
            this.txtUpdates.Name = "txtUpdates";
            this.txtUpdates.ReadOnly = true;
            this.txtUpdates.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtUpdates.ShortcutsEnabled = false;
            this.txtUpdates.Size = new System.Drawing.Size(432, 278);
            this.txtUpdates.TabIndex = 17;
            this.txtUpdates.Text = "List of components here...";
            // 
            // frmCheckForUpdate
            // 
            this.AcceptButton = this.btnDownload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(134F, 134F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(203)))), ((int)(((byte)(204)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(475, 482);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtUpdates);
            this.Controls.Add(this.picStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lnkUpdateReadMore);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(495, 238);
            this.Name = "frmCheckForUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check for update";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCheckForUpdate_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStoreApp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.LinkLabel lnkUpdateReadMore;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.RichTextBox txtUpdates;
        private System.Windows.Forms.PictureBox picStoreApp;
    }
}

