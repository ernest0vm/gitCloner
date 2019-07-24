namespace gitCloner
{
    partial class MainForm
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
            this.chkCompress = new System.Windows.Forms.CheckBox();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnClone = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SourceList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.btnMultiClone = new System.Windows.Forms.Button();
            this.lblItems = new System.Windows.Forms.Label();
            this.chkMirror = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkCompress
            // 
            this.chkCompress.AutoSize = true;
            this.chkCompress.Location = new System.Drawing.Point(474, 253);
            this.chkCompress.Name = "chkCompress";
            this.chkCompress.Size = new System.Drawing.Size(138, 17);
            this.chkCompress.TabIndex = 6;
            this.chkCompress.Text = "Compress after clone all";
            this.chkCompress.UseVisualStyleBackColor = true;
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.Location = new System.Drawing.Point(474, 276);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(129, 17);
            this.chkDelete.TabIndex = 7;
            this.chkDelete.Text = "Delete after compress";
            this.chkDelete.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(89, 429);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 36);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "Open List";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(463, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(165, 36);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(463, 50);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(165, 36);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(463, 92);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(165, 36);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Location = new System.Drawing.Point(8, 346);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(449, 20);
            this.txtPath.TabIndex = 12;
            // 
            // btnClone
            // 
            this.btnClone.Location = new System.Drawing.Point(463, 309);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(165, 48);
            this.btnClone.TabIndex = 13;
            this.btnClone.Text = "Single Clone";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.BtnClone_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(8, 429);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 36);
            this.btnNew.TabIndex = 14;
            this.btnNew.Text = "New List";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(251, 421);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(377, 22);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(463, 134);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(165, 36);
            this.btnAbout.TabIndex = 16;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 470);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(640, 10);
            this.progressBar1.TabIndex = 17;
            // 
            // SourceList
            // 
            this.SourceList.FormattingEnabled = true;
            this.SourceList.Location = new System.Drawing.Point(8, 8);
            this.SourceList.Name = "SourceList";
            this.SourceList.Size = new System.Drawing.Size(449, 316);
            this.SourceList.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 330);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Source list path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 375);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Path to save repositories:";
            // 
            // txtSavePath
            // 
            this.txtSavePath.Location = new System.Drawing.Point(8, 391);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(449, 20);
            this.txtSavePath.TabIndex = 21;
            // 
            // btnSavePath
            // 
            this.btnSavePath.Location = new System.Drawing.Point(170, 429);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(75, 36);
            this.btnSavePath.TabIndex = 22;
            this.btnSavePath.Text = "Select Save Folder";
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.BtnSavePath_Click);
            // 
            // btnMultiClone
            // 
            this.btnMultiClone.Location = new System.Drawing.Point(463, 363);
            this.btnMultiClone.Name = "btnMultiClone";
            this.btnMultiClone.Size = new System.Drawing.Size(165, 48);
            this.btnMultiClone.TabIndex = 23;
            this.btnMultiClone.Text = "Multi Clone";
            this.btnMultiClone.UseVisualStyleBackColor = true;
            this.btnMultiClone.Click += new System.EventHandler(this.BtnMultiClone_Click);
            // 
            // lblItems
            // 
            this.lblItems.Location = new System.Drawing.Point(251, 444);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(377, 22);
            this.lblItems.TabIndex = 24;
            this.lblItems.Text = "Items";
            this.lblItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMirror
            // 
            this.chkMirror.AutoSize = true;
            this.chkMirror.Location = new System.Drawing.Point(474, 230);
            this.chkMirror.Name = "chkMirror";
            this.chkMirror.Size = new System.Drawing.Size(93, 17);
            this.chkMirror.TabIndex = 25;
            this.chkMirror.Text = "Mirrored clone";
            this.chkMirror.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.chkMirror);
            this.Controls.Add(this.lblItems);
            this.Controls.Add(this.btnMultiClone);
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.txtSavePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SourceList);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnClone);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.chkDelete);
            this.Controls.Add(this.chkCompress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gitCloner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chkCompress;
        private System.Windows.Forms.CheckBox chkDelete;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnClone;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox SourceList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.Button btnMultiClone;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.CheckBox chkMirror;
    }
}

