namespace FinacPOS
{
    partial class frmSessionManagement
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
            this.pnlSession = new System.Windows.Forms.Panel();
            this.txtOpenBal = new System.Windows.Forms.TextBox();
            this.dtpSessionDate = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblOpenBal = new System.Windows.Forms.Label();
            this.lblSessionDate = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlSession.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSession
            // 
            this.pnlSession.Controls.Add(this.txtOpenBal);
            this.pnlSession.Controls.Add(this.dtpSessionDate);
            this.pnlSession.Controls.Add(this.btnSave);
            this.pnlSession.Controls.Add(this.btnClose);
            this.pnlSession.Controls.Add(this.lblOpenBal);
            this.pnlSession.Controls.Add(this.lblSessionDate);
            this.pnlSession.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.pnlSession.Location = new System.Drawing.Point(-5, -6);
            this.pnlSession.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlSession.Name = "pnlSession";
            this.pnlSession.Size = new System.Drawing.Size(421, 230);
            this.pnlSession.TabIndex = 0;
            // 
            // txtOpenBal
            // 
            this.txtOpenBal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpenBal.Location = new System.Drawing.Point(188, 101);
            this.txtOpenBal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOpenBal.Name = "txtOpenBal";
            this.txtOpenBal.Size = new System.Drawing.Size(172, 26);
            this.txtOpenBal.TabIndex = 5;
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
            this.dtpSessionDate.Location = new System.Drawing.Point(189, 66);
            this.dtpSessionDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpSessionDate.Name = "dtpSessionDate";
            this.dtpSessionDate.Size = new System.Drawing.Size(171, 26);
            this.dtpSessionDate.TabIndex = 2;
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
            this.btnSave.Location = new System.Drawing.Point(89, 145);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 62);
            this.btnSave.TabIndex = 6;
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
            this.btnClose.Location = new System.Drawing.Point(233, 145);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 62);
            this.btnClose.TabIndex = 36456498;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblOpenBal
            // 
            this.lblOpenBal.AutoSize = true;
            this.lblOpenBal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenBal.Location = new System.Drawing.Point(50, 105);
            this.lblOpenBal.Name = "lblOpenBal";
            this.lblOpenBal.Size = new System.Drawing.Size(130, 18);
            this.lblOpenBal.TabIndex = 4;
            this.lblOpenBal.Text = "Opening Balance";
            // 
            // lblSessionDate
            // 
            this.lblSessionDate.AutoSize = true;
            this.lblSessionDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionDate.Location = new System.Drawing.Point(50, 66);
            this.lblSessionDate.Name = "lblSessionDate";
            this.lblSessionDate.Size = new System.Drawing.Size(105, 18);
            this.lblSessionDate.TabIndex = 1;
            this.lblSessionDate.Text = "Session Date";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.Location = new System.Drawing.Point(296, 16);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(58, 25);
            this.lblUserId.TabIndex = 3;
            this.lblUserId.Text = "User";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounter.Location = new System.Drawing.Point(65, 16);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(93, 25);
            this.lblCounter.TabIndex = 2;
            this.lblCounter.Text = "Counter";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCounter);
            this.panel1.Controls.Add(this.lblUserId);
            this.panel1.Location = new System.Drawing.Point(-19, -6);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(436, 59);
            this.panel1.TabIndex = 1;
            // 
            // frmSessionManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(413, 213);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlSession);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSessionManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Session Management";
            this.Load += new System.EventHandler(this.frmSessionManagement_Load);
            this.pnlSession.ResumeLayout(false);
            this.pnlSession.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSession;
        private System.Windows.Forms.Label lblSessionDate;
        private System.Windows.Forms.Label lblOpenBal;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtOpenBal;
        private System.Windows.Forms.DateTimePicker dtpSessionDate;
        private System.Windows.Forms.Panel panel1;
    }
}