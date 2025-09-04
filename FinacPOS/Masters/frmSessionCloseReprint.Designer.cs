namespace FinacPOS.Masters
{
    partial class frmSessionCloseReprint
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
            this.lbldate = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmbPOSCounter = new System.Windows.Forms.ComboBox();
            this.cmbSessionNo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.Font = new System.Drawing.Font("Verdana", 9F);
            this.lbldate.Location = new System.Drawing.Point(18, 23);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(37, 14);
            this.lbldate.TabIndex = 20;
            this.lbldate.Text = "Date";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblCounter.Location = new System.Drawing.Point(18, 57);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(58, 14);
            this.lblCounter.TabIndex = 21;
            this.lblCounter.Text = "Counter";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblGroup.Location = new System.Drawing.Point(18, 95);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(60, 14);
            this.lblGroup.TabIndex = 22;
            this.lblGroup.Text = "Session ";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(105)))), ((int)(((byte)(84)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(190, 131);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 28);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReprint
            // 
            this.btnReprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnReprint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnReprint.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnReprint.FlatAppearance.BorderSize = 0;
            this.btnReprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprint.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReprint.ForeColor = System.Drawing.Color.Black;
            this.btnReprint.Location = new System.Drawing.Point(91, 131);
            this.btnReprint.Margin = new System.Windows.Forms.Padding(1);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(88, 28);
            this.btnReprint.TabIndex = 18;
            this.btnReprint.Text = "Reprint";
            this.btnReprint.UseVisualStyleBackColor = false;
            this.btnReprint.Click += new System.EventHandler(this.btnReprint_Click_1);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(91, 23);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(187, 22);
            this.dtpDate.TabIndex = 24;
            // 
            // cmbPOSCounter
            // 
            this.cmbPOSCounter.FormattingEnabled = true;
            this.cmbPOSCounter.Location = new System.Drawing.Point(91, 57);
            this.cmbPOSCounter.Name = "cmbPOSCounter";
            this.cmbPOSCounter.Size = new System.Drawing.Size(187, 21);
            this.cmbPOSCounter.TabIndex = 25;
            this.cmbPOSCounter.SelectedIndexChanged += new System.EventHandler(this.cmbPOSCounter_SelectedIndexChanged_1);
            // 
            // cmbSessionNo
            // 
            this.cmbSessionNo.FormattingEnabled = true;
            this.cmbSessionNo.Location = new System.Drawing.Point(91, 95);
            this.cmbSessionNo.Name = "cmbSessionNo";
            this.cmbSessionNo.Size = new System.Drawing.Size(187, 21);
            this.cmbSessionNo.TabIndex = 26;
            // 
            // frmSessionCloseReprint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 178);
            this.Controls.Add(this.cmbSessionNo);
            this.Controls.Add(this.cmbPOSCounter);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.btnReprint);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.lbldate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSessionCloseReprint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session Close Reprint";
            this.Load += new System.EventHandler(this.frmSessionCloseReprint_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbldate;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ComboBox cmbPOSCounter;
        private System.Windows.Forms.ComboBox cmbSessionNo;
    }
}