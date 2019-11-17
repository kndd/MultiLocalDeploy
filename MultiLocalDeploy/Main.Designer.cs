namespace MultiLocalDeploy
{
    partial class Main
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.fbdTargetFolders = new System.Windows.Forms.FolderBrowserDialog();
            this.fbdSourceFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnTarget = new System.Windows.Forms.Button();
            this.btnSource = new System.Windows.Forms.Button();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.lbFolders = new System.Windows.Forms.ListBox();
            this.cbSkipConfigFiles = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pbDeploy = new System.Windows.Forms.ToolStripProgressBar();
            this.tvSourceFolder = new System.Windows.Forms.TreeView();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fbdSourceFolder
            // 
            this.fbdSourceFolder.ShowNewFolderButton = false;
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(159, 13);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(75, 23);
            this.btnTarget.TabIndex = 1;
            this.btnTarget.Text = "Add target...";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.BtnTarget_Click);
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(13, 13);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(75, 23);
            this.btnSource.TabIndex = 2;
            this.btnSource.Text = "Źródło...";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.BtnSource_Click);
            // 
            // btnDeploy
            // 
            this.btnDeploy.BackColor = System.Drawing.Color.Red;
            this.btnDeploy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnDeploy.Location = new System.Drawing.Point(388, 358);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(334, 79);
            this.btnDeploy.TabIndex = 4;
            this.btnDeploy.Text = "DEPLOY";
            this.btnDeploy.UseVisualStyleBackColor = false;
            this.btnDeploy.Click += new System.EventHandler(this.BtnDeploy_Click);
            // 
            // tbLog
            // 
            this.tbLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbLog.Location = new System.Drawing.Point(389, 43);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(333, 263);
            this.tbLog.TabIndex = 6;
            // 
            // lbFolders
            // 
            this.lbFolders.FormattingEnabled = true;
            this.lbFolders.Location = new System.Drawing.Point(159, 43);
            this.lbFolders.Name = "lbFolders";
            this.lbFolders.Size = new System.Drawing.Size(223, 394);
            this.lbFolders.TabIndex = 8;
            this.lbFolders.SelectedIndexChanged += new System.EventHandler(this.lbFolders_SelectedIndexChanged);
            this.lbFolders.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lbFolders_KeyUp);
            // 
            // cbSkipConfigFiles
            // 
            this.cbSkipConfigFiles.AutoSize = true;
            this.cbSkipConfigFiles.Checked = true;
            this.cbSkipConfigFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSkipConfigFiles.Location = new System.Drawing.Point(389, 312);
            this.cbSkipConfigFiles.Name = "cbSkipConfigFiles";
            this.cbSkipConfigFiles.Size = new System.Drawing.Size(107, 17);
            this.cbSkipConfigFiles.TabIndex = 9;
            this.cbSkipConfigFiles.Text = "Skip *.config files";
            this.cbSkipConfigFiles.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbDeploy});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(740, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pbDeploy
            // 
            this.pbDeploy.Name = "pbDeploy";
            this.pbDeploy.Size = new System.Drawing.Size(100, 16);
            // 
            // tvSourceFolder
            // 
            this.tvSourceFolder.Location = new System.Drawing.Point(13, 43);
            this.tvSourceFolder.Name = "tvSourceFolder";
            this.tvSourceFolder.Size = new System.Drawing.Size(140, 394);
            this.tvSourceFolder.TabIndex = 12;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 470);
            this.Controls.Add(this.tvSourceFolder);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cbSkipConfigFiles);
            this.Controls.Add(this.lbFolders);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btnDeploy);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.btnTarget);
            this.Name = "Main";
            this.Text = "Multi Local Deploy";
            this.Load += new System.EventHandler(this.Main_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog fbdTargetFolders;
        private System.Windows.Forms.FolderBrowserDialog fbdSourceFolder;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.ListBox lbFolders;
        private System.Windows.Forms.CheckBox cbSkipConfigFiles;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pbDeploy;
        private System.Windows.Forms.TreeView tvSourceFolder;
    }
}

