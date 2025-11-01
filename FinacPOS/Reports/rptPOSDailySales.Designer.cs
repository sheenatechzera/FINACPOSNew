namespace FinacPOS
{
    partial class rptPOSDailySales
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lblBranch = new System.Windows.Forms.Label();
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.chkListFilter = new System.Windows.Forms.CheckedListBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.cmbExport = new System.Windows.Forms.ComboBox();
            this.cmbReportType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtnBillDate = new System.Windows.Forms.RadioButton();
            this.rbtnSessionDate = new System.Windows.Forms.RadioButton();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAdress = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bwrkControlSettings = new System.ComponentModel.BackgroundWorker();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.bwrk1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(489, 82);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(108, 28);
            this.btnRefresh.TabIndex = 156;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "Reset";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(16, 423);
            this.chkSelectAll.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(83, 20);
            this.chkSelectAll.TabIndex = 155;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lblBranch
            // 
            this.lblBranch.AutoSize = true;
            this.lblBranch.Location = new System.Drawing.Point(19, 469);
            this.lblBranch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBranch.Name = "lblBranch";
            this.lblBranch.Size = new System.Drawing.Size(50, 16);
            this.lblBranch.TabIndex = 153;
            this.lblBranch.Text = "Branch";
            // 
            // cmbBranch
            // 
            this.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Location = new System.Drawing.Point(101, 466);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(212, 24);
            this.cmbBranch.TabIndex = 151;
            this.cmbBranch.TabStop = false;
            // 
            // chkListFilter
            // 
            this.chkListFilter.FormattingEnabled = true;
            this.chkListFilter.Location = new System.Drawing.Point(13, 122);
            this.chkListFilter.Margin = new System.Windows.Forms.Padding(4);
            this.chkListFilter.Name = "chkListFilter";
            this.chkListFilter.Size = new System.Drawing.Size(584, 293);
            this.chkListFilter.TabIndex = 147;
            // 
            // btnGo
            // 
            this.btnGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(128)))));
            this.btnGo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnGo.FlatAppearance.BorderSize = 0;
            this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.ForeColor = System.Drawing.Color.White;
            this.btnGo.Location = new System.Drawing.Point(493, 463);
            this.btnGo.Margin = new System.Windows.Forms.Padding(1);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(108, 28);
            this.btnGo.TabIndex = 146;
            this.btnGo.TabStop = false;
            this.btnGo.Text = "Show";
            this.btnGo.UseVisualStyleBackColor = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // cmbExport
            // 
            this.cmbExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExport.FormattingEnabled = true;
            this.cmbExport.Items.AddRange(new object[] {
            "Print",
            "Export to Excel",
            "Export to Html"});
            this.cmbExport.Location = new System.Drawing.Point(355, 465);
            this.cmbExport.Margin = new System.Windows.Forms.Padding(4);
            this.cmbExport.Name = "cmbExport";
            this.cmbExport.Size = new System.Drawing.Size(132, 24);
            this.cmbExport.TabIndex = 145;
            // 
            // cmbReportType
            // 
            this.cmbReportType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbReportType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbReportType.BackColor = System.Drawing.Color.White;
            this.cmbReportType.FormattingEnabled = true;
            this.cmbReportType.Items.AddRange(new object[] {
            "Cashier wise Sales Summery",
            "Counter wise Sales Summery",
            "Tender Type Summery",
            "Daily Sales Summery",
            "Daily wise Bill Count",
            "Cashier wise bill summery"});
            this.cmbReportType.Location = new System.Drawing.Point(112, 84);
            this.cmbReportType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbReportType.Name = "cmbReportType";
            this.cmbReportType.Size = new System.Drawing.Size(369, 24);
            this.cmbReportType.TabIndex = 143;
            this.cmbReportType.TabStop = false;
            this.cmbReportType.SelectedIndexChanged += new System.EventHandler(this.cmbReportType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 144;
            this.label1.Text = "Report Type";
            // 
            // rbtnBillDate
            // 
            this.rbtnBillDate.AutoSize = true;
            this.rbtnBillDate.Location = new System.Drawing.Point(13, 17);
            this.rbtnBillDate.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnBillDate.Name = "rbtnBillDate";
            this.rbtnBillDate.Size = new System.Drawing.Size(76, 20);
            this.rbtnBillDate.TabIndex = 141;
            this.rbtnBillDate.Text = "Bill Date";
            this.rbtnBillDate.UseVisualStyleBackColor = true;
            // 
            // rbtnSessionDate
            // 
            this.rbtnSessionDate.AutoSize = true;
            this.rbtnSessionDate.Location = new System.Drawing.Point(425, 17);
            this.rbtnSessionDate.Margin = new System.Windows.Forms.Padding(4);
            this.rbtnSessionDate.Name = "rbtnSessionDate";
            this.rbtnSessionDate.Size = new System.Drawing.Size(107, 20);
            this.rbtnSessionDate.TabIndex = 142;
            this.rbtnSessionDate.Text = "Session Date";
            this.rbtnSessionDate.UseVisualStyleBackColor = true;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(340, 49);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(4);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(141, 22);
            this.dtpTo.TabIndex = 119;
            this.dtpTo.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 53);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 16);
            this.label4.TabIndex = 121;
            this.label4.Text = "To Date";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(112, 47);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(129, 22);
            this.dtpFrom.TabIndex = 118;
            this.dtpFrom.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 49);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 120;
            this.label6.Text = "From Date";
            // 
            // lblPhone
            // 
            this.lblPhone.BackColor = System.Drawing.Color.Transparent;
            this.lblPhone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.ForeColor = System.Drawing.Color.Black;
            this.lblPhone.Location = new System.Drawing.Point(82, 280);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(348, 22);
            this.lblPhone.TabIndex = 149;
            this.lblPhone.Text = "Phone ";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAdress
            // 
            this.lblAdress.BackColor = System.Drawing.Color.Transparent;
            this.lblAdress.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdress.ForeColor = System.Drawing.Color.Black;
            this.lblAdress.Location = new System.Drawing.Point(82, 256);
            this.lblAdress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdress.Name = "lblAdress";
            this.lblAdress.Size = new System.Drawing.Size(348, 22);
            this.lblAdress.TabIndex = 150;
            this.lblAdress.Text = "Address";
            this.lblAdress.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.DimGray;
            this.lblName.Location = new System.Drawing.Point(82, 221);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(348, 37);
            this.lblName.TabIndex = 148;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Location = new System.Drawing.Point(289, 388);
            this.cmbCurrency.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(67, 24);
            this.cmbCurrency.TabIndex = 152;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 397);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 154;
            this.label2.Text = "Currency :";
            // 
            // bwrkControlSettings
            // 
            this.bwrkControlSettings.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrkControlSettings_DoWork);
            this.bwrkControlSettings.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrkControlSettings_RunWorkerCompleted);
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // bwrk1
            // 
            this.bwrk1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwrk1_DoWork);
            this.bwrk1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwrk1_RunWorkerCompleted);
            // 
            // rptPOSDailySales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(240)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(610, 516);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cmbReportType);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkListFilter);
            this.Controls.Add(this.rbtnBillDate);
            this.Controls.Add(this.lblBranch);
            this.Controls.Add(this.rbtnSessionDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.cmbBranch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbCurrency);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lblAdress);
            this.Controls.Add(this.cmbExport);
            this.Controls.Add(this.lblPhone);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "rptPOSDailySales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Daily Sales Report";
            this.Load += new System.EventHandler(this.rptPOSDailySales_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rptPOSDailySales_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rbtnBillDate;
        private System.Windows.Forms.RadioButton rbtnSessionDate;
        private System.Windows.Forms.ComboBox cmbReportType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox chkListFilter;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ComboBox cmbExport;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAdress;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBranch;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnRefresh;
        private System.ComponentModel.BackgroundWorker bwrkControlSettings;
        private System.Drawing.Printing.PrintDocument printDoc;
        private System.ComponentModel.BackgroundWorker bwrk1;
    }
}