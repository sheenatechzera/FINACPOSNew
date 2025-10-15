namespace FinacPOS.Masters
{
    partial class frmSessionControl
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
            this.txtOpenBal = new System.Windows.Forms.TextBox();
            this.dtpSessionDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblOpenBal = new System.Windows.Forms.Label();
            this.lblSessionDate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cmbCounter = new System.Windows.Forms.ComboBox();
            this.lblCounter = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSessionClose = new System.Windows.Forms.RadioButton();
            this.rbSessionOpening = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOpenBal
            // 
            this.txtOpenBal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpenBal.Location = new System.Drawing.Point(153, 187);
            this.txtOpenBal.Name = "txtOpenBal";
            this.txtOpenBal.Size = new System.Drawing.Size(148, 22);
            this.txtOpenBal.TabIndex = 36456502;
            this.txtOpenBal.Text = "0";
            this.txtOpenBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOpenBal.Enter += new System.EventHandler(this.txtOpenBal_Enter);
            this.txtOpenBal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpenBal_KeyDown);
            this.txtOpenBal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpenBal_KeyPress);
            this.txtOpenBal.Leave += new System.EventHandler(this.txtOpenBal_Leave);
            // 
            // dtpSessionDate
            // 
            this.dtpSessionDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpSessionDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSessionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSessionDate.Location = new System.Drawing.Point(154, 159);
            this.dtpSessionDate.Name = "dtpSessionDate";
            this.dtpSessionDate.Size = new System.Drawing.Size(147, 22);
            this.dtpSessionDate.TabIndex = 36456500;
            this.dtpSessionDate.Enter += new System.EventHandler(this.dtpSessionDate_Enter);
            this.dtpSessionDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpSessionDate_KeyDown);
            this.dtpSessionDate.Leave += new System.EventHandler(this.dtpSessionDate_Leave);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(68, 223);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 36);
            this.btnSave.TabIndex = 36456503;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(105)))), ((int)(((byte)(84)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(192, 223);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.TabIndex = 36456504;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblOpenBal
            // 
            this.lblOpenBal.AutoSize = true;
            this.lblOpenBal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenBal.Location = new System.Drawing.Point(35, 190);
            this.lblOpenBal.Name = "lblOpenBal";
            this.lblOpenBal.Size = new System.Drawing.Size(113, 14);
            this.lblOpenBal.TabIndex = 36456501;
            this.lblOpenBal.Text = "Opening Balance";
            // 
            // lblSessionDate
            // 
            this.lblSessionDate.AutoSize = true;
            this.lblSessionDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionDate.Location = new System.Drawing.Point(35, 161);
            this.lblSessionDate.Name = "lblSessionDate";
            this.lblSessionDate.Size = new System.Drawing.Size(90, 14);
            this.lblSessionDate.TabIndex = 36456499;
            this.lblSessionDate.Text = "Session Date";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmbUser);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Controls.Add(this.cmbCounter);
            this.panel1.Controls.Add(this.lblCounter);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(21, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 133);
            this.panel1.TabIndex = 36456505;
            // 
            // cmbUser
            // 
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(96, 104);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(174, 21);
            this.cmbUser.TabIndex = 36456510;
          //  this.cmbUser.SelectedIndexChanged += new System.EventHandler(this.cmbUser_SelectedIndexChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(12, 104);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(38, 14);
            this.lblUser.TabIndex = 36456509;
            this.lblUser.Text = "User";
            // 
            // cmbCounter
            // 
            this.cmbCounter.FormattingEnabled = true;
            this.cmbCounter.Location = new System.Drawing.Point(96, 75);
            this.cmbCounter.Name = "cmbCounter";
            this.cmbCounter.Size = new System.Drawing.Size(174, 21);
            this.cmbCounter.TabIndex = 36456509;
            this.cmbCounter.SelectedIndexChanged += new System.EventHandler(this.cmbCounter_SelectedIndexChanged);
     //       this.cmbCounter.SelectedValueChanged += new System.EventHandler(this.cmbCounter_SelectedValueChanged);
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounter.Location = new System.Drawing.Point(9, 74);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(59, 14);
            this.lblCounter.TabIndex = 36456508;
            this.lblCounter.Text = "Counter";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSessionClose);
            this.groupBox1.Controls.Add(this.rbSessionOpening);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 46);
            this.groupBox1.TabIndex = 36456507;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SessionControl";
            // 
            // rbSessionClose
            // 
            this.rbSessionClose.AutoSize = true;
            this.rbSessionClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSessionClose.Location = new System.Drawing.Point(130, 17);
            this.rbSessionClose.Name = "rbSessionClose";
            this.rbSessionClose.Size = new System.Drawing.Size(110, 17);
            this.rbSessionClose.TabIndex = 36456507;
            this.rbSessionClose.TabStop = true;
            this.rbSessionClose.Text = "SessionClose";
            this.rbSessionClose.UseVisualStyleBackColor = true;
            this.rbSessionClose.CheckedChanged += new System.EventHandler(this.rbSessionClose_CheckedChanged);
            // 
            // rbSessionOpening
            // 
            this.rbSessionOpening.AutoSize = true;
            this.rbSessionOpening.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSessionOpening.Location = new System.Drawing.Point(8, 17);
            this.rbSessionOpening.Name = "rbSessionOpening";
            this.rbSessionOpening.Size = new System.Drawing.Size(108, 17);
            this.rbSessionOpening.TabIndex = 36456506;
            this.rbSessionOpening.TabStop = true;
            this.rbSessionOpening.Text = "SessionOpen";
            this.rbSessionOpening.UseVisualStyleBackColor = true;
            this.rbSessionOpening.CheckedChanged += new System.EventHandler(this.rbSessionOpening_CheckedChanged);
            // 
            // frmSessionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 281);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtOpenBal);
            this.Controls.Add(this.dtpSessionDate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblOpenBal);
            this.Controls.Add(this.lblSessionDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSessionControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session Control";
            this.Load += new System.EventHandler(this.SessionManagmentByAdmin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOpenBal;
        private System.Windows.Forms.DateTimePicker dtpSessionDate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblOpenBal;
        private System.Windows.Forms.Label lblSessionDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbSessionOpening;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbSessionClose;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cmbCounter;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.ComboBox cmbUser;
    }
}