namespace FinacPOS
{
    partial class FrmPOSTable
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
            this.lblTblNo = new System.Windows.Forms.Label();
            this.lblNoOfSeats = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.ChkActive = new System.Windows.Forms.CheckBox();
            this.txtTblNo = new System.Windows.Forms.TextBox();
            this.txtNoOfSeats = new System.Windows.Forms.TextBox();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTblNo
            // 
            this.lblTblNo.AutoSize = true;
            this.lblTblNo.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblTblNo.Location = new System.Drawing.Point(25, 24);
            this.lblTblNo.Name = "lblTblNo";
            this.lblTblNo.Size = new System.Drawing.Size(61, 14);
            this.lblTblNo.TabIndex = 8;
            this.lblTblNo.Text = "Table No";
            // 
            // lblNoOfSeats
            // 
            this.lblNoOfSeats.AutoSize = true;
            this.lblNoOfSeats.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblNoOfSeats.Location = new System.Drawing.Point(26, 58);
            this.lblNoOfSeats.Name = "lblNoOfSeats";
            this.lblNoOfSeats.Size = new System.Drawing.Size(114, 14);
            this.lblNoOfSeats.TabIndex = 9;
            this.lblNoOfSeats.Text = "Number Of Seats";
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Font = new System.Drawing.Font("Verdana", 9F);
            this.lblGroup.Location = new System.Drawing.Point(26, 96);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(45, 14);
            this.lblGroup.TabIndex = 10;
            this.lblGroup.Text = "Group";
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F);
            this.ChkActive.Location = new System.Drawing.Point(29, 127);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.Size = new System.Drawing.Size(63, 18);
            this.ChkActive.TabIndex = 11;
            this.ChkActive.Text = "Active";
            this.ChkActive.UseVisualStyleBackColor = true;
            this.ChkActive.Enter += new System.EventHandler(this.ChkActive_Enter);
            this.ChkActive.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChkActive_KeyDown);
            this.ChkActive.Leave += new System.EventHandler(this.ChkActive_Leave);
            // 
            // txtTblNo
            // 
            this.txtTblNo.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtTblNo.Location = new System.Drawing.Point(162, 24);
            this.txtTblNo.Name = "txtTblNo";
            this.txtTblNo.Size = new System.Drawing.Size(158, 22);
            this.txtTblNo.TabIndex = 0;
            this.txtTblNo.Enter += new System.EventHandler(this.txtTblNo_Enter);
            this.txtTblNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTblNo_KeyDown);
            this.txtTblNo.Leave += new System.EventHandler(this.txtTblNo_Leave);
            // 
            // txtNoOfSeats
            // 
            this.txtNoOfSeats.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtNoOfSeats.Location = new System.Drawing.Point(162, 58);
            this.txtNoOfSeats.Name = "txtNoOfSeats";
            this.txtNoOfSeats.Size = new System.Drawing.Size(158, 22);
            this.txtNoOfSeats.TabIndex = 1;
            this.txtNoOfSeats.Enter += new System.EventHandler(this.txtNoOfSeats_Enter);
            this.txtNoOfSeats.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNoOfSeats_KeyDown);
            this.txtNoOfSeats.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoOfSeats_KeyPress);
            this.txtNoOfSeats.Leave += new System.EventHandler(this.txtNoOfSeats_Leave);
            // 
            // txtGroup
            // 
            this.txtGroup.Font = new System.Drawing.Font("Verdana", 9F);
            this.txtGroup.Location = new System.Drawing.Point(162, 96);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(158, 22);
            this.txtGroup.TabIndex = 2;
            this.txtGroup.Enter += new System.EventHandler(this.txtGroup_Enter);
            this.txtGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGroup_KeyDown);
            this.txtGroup.Leave += new System.EventHandler(this.txtGroup_Leave);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Location = new System.Drawing.Point(268, 188);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 28);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnSave.FlatAppearance.BorderSize = 0;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Location = new System.Drawing.Point(88, 188);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(1);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(88, 28);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = false;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
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
            this.btnClose.Location = new System.Drawing.Point(358, 188);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 28);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(178, 188);
            this.btnClear.Margin = new System.Windows.Forms.Padding(1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 28);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnSearch.BackgroundImage = global::FinacPOS.Properties.Resources.searchicon;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(339, 24);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmPOSTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 231);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtGroup);
            this.Controls.Add(this.txtNoOfSeats);
            this.Controls.Add(this.txtTblNo);
            this.Controls.Add(this.ChkActive);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblNoOfSeats);
            this.Controls.Add(this.lblTblNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FrmPOSTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Table";
            this.Load += new System.EventHandler(this.FrmPOSTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTblNo;
        private System.Windows.Forms.Label lblNoOfSeats;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.CheckBox ChkActive;
        private System.Windows.Forms.TextBox txtTblNo;
        private System.Windows.Forms.TextBox txtNoOfSeats;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
    }
}