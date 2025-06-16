namespace FinacPOS
{
    partial class frmPOSControlSettings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDGV = new System.Windows.Forms.CheckBox();
            this.dgvLabels = new System.Windows.Forms.DataGridView();
            this.colWordId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFormId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colControlType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWordEnglish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWordArabic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGetFormNames = new System.Windows.Forms.Button();
            this.cmbFormName = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblFormName = new System.Windows.Forms.Label();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTranslate);
            this.panel1.Controls.Add(this.chkDGV);
            this.panel1.Controls.Add(this.dgvLabels);
            this.panel1.Controls.Add(this.btnGetFormNames);
            this.panel1.Controls.Add(this.cmbFormName);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.lblFormName);
            this.panel1.Controls.Add(this.txtFormName);
            this.panel1.Font = new System.Drawing.Font("Verdana", 9F);
            this.panel1.Location = new System.Drawing.Point(-5, -2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1211, 652);
            this.panel1.TabIndex = 1;
            // 
            // chkDGV
            // 
            this.chkDGV.AutoSize = true;
            this.chkDGV.Location = new System.Drawing.Point(874, 48);
            this.chkDGV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDGV.Name = "chkDGV";
            this.chkDGV.Size = new System.Drawing.Size(100, 22);
            this.chkDGV.TabIndex = 38;
            this.chkDGV.Text = "Grid View";
            this.chkDGV.UseVisualStyleBackColor = true;
            this.chkDGV.CheckedChanged += new System.EventHandler(this.chkDGV_CheckedChanged);
            // 
            // dgvLabels
            // 
            this.dgvLabels.AllowUserToAddRows = false;
            this.dgvLabels.AllowUserToDeleteRows = false;
            this.dgvLabels.AllowUserToResizeColumns = false;
            this.dgvLabels.AllowUserToResizeRows = false;
            this.dgvLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colWordId,
            this.colFormId,
            this.colControlName,
            this.colControlType,
            this.colWordEnglish,
            this.colWordArabic});
            this.dgvLabels.Location = new System.Drawing.Point(15, 86);
            this.dgvLabels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvLabels.Name = "dgvLabels";
            this.dgvLabels.RowHeadersVisible = false;
            this.dgvLabels.RowHeadersWidth = 51;
            this.dgvLabels.Size = new System.Drawing.Size(1191, 506);
            this.dgvLabels.TabIndex = 37;
            // 
            // colWordId
            // 
            this.colWordId.HeaderText = "WordId";
            this.colWordId.MinimumWidth = 6;
            this.colWordId.Name = "colWordId";
            this.colWordId.ReadOnly = true;
            this.colWordId.Visible = false;
            this.colWordId.Width = 125;
            // 
            // colFormId
            // 
            this.colFormId.HeaderText = "FormId";
            this.colFormId.MinimumWidth = 6;
            this.colFormId.Name = "colFormId";
            this.colFormId.ReadOnly = true;
            this.colFormId.Visible = false;
            this.colFormId.Width = 125;
            // 
            // colControlName
            // 
            this.colControlName.HeaderText = "ControlName";
            this.colControlName.MinimumWidth = 6;
            this.colControlName.Name = "colControlName";
            this.colControlName.ReadOnly = true;
            this.colControlName.Visible = false;
            this.colControlName.Width = 125;
            // 
            // colControlType
            // 
            this.colControlType.HeaderText = "ControlType";
            this.colControlType.MinimumWidth = 6;
            this.colControlType.Name = "colControlType";
            this.colControlType.ReadOnly = true;
            this.colControlType.Visible = false;
            this.colControlType.Width = 125;
            // 
            // colWordEnglish
            // 
            this.colWordEnglish.HeaderText = "English";
            this.colWordEnglish.MinimumWidth = 6;
            this.colWordEnglish.Name = "colWordEnglish";
            this.colWordEnglish.Width = 500;
            // 
            // colWordArabic
            // 
            this.colWordArabic.HeaderText = "Arabic";
            this.colWordArabic.MinimumWidth = 6;
            this.colWordArabic.Name = "colWordArabic";
            this.colWordArabic.Width = 515;
            // 
            // btnGetFormNames
            // 
            this.btnGetFormNames.Location = new System.Drawing.Point(1009, 17);
            this.btnGetFormNames.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGetFormNames.Name = "btnGetFormNames";
            this.btnGetFormNames.Size = new System.Drawing.Size(176, 27);
            this.btnGetFormNames.TabIndex = 36;
            this.btnGetFormNames.Text = "Get Form Names";
            this.btnGetFormNames.UseVisualStyleBackColor = true;
            this.btnGetFormNames.Click += new System.EventHandler(this.btnGetFormNames_Click);
            // 
            // cmbFormName
            // 
            this.cmbFormName.FormattingEnabled = true;
            this.cmbFormName.Location = new System.Drawing.Point(140, 17);
            this.cmbFormName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFormName.Name = "cmbFormName";
            this.cmbFormName.Size = new System.Drawing.Size(689, 26);
            this.cmbFormName.TabIndex = 35;
            this.cmbFormName.SelectionChangeCommitted += new System.EventHandler(this.cmbFormName_SelectionChangeCommitted);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(105)))), ((int)(((byte)(84)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(561, 604);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 34);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(451, 604);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 34);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.Location = new System.Drawing.Point(23, 22);
            this.lblFormName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(109, 18);
            this.lblFormName.TabIndex = 34;
            this.lblFormName.Text = "Form Name :";
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(140, 52);
            this.txtFormName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(689, 26);
            this.txtFormName.TabIndex = 0;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(861, 17);
            this.btnTranslate.Margin = new System.Windows.Forms.Padding(4);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(137, 27);
            this.btnTranslate.TabIndex = 39;
            this.btnTranslate.Text = "Translate";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // frmPOSControlSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 649);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmPOSControlSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Control Settings";
            this.Load += new System.EventHandler(this.frmLanguageEntry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDGV;
        private System.Windows.Forms.DataGridView dgvLabels;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWordId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colControlType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWordEnglish;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWordArabic;
        private System.Windows.Forms.Button btnGetFormNames;
        private System.Windows.Forms.ComboBox cmbFormName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblFormName;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.Button btnTranslate;
    }
}