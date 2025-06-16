namespace FinacPOS
{
    partial class frmPOSUserSettings
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
            this.btnClose = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbUSerGroup = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lstbSelsected = new System.Windows.Forms.ListBox();
            this.lstbNonSelsected = new System.Windows.Forms.ListBox();
            this.btnNonSelect = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(105)))), ((int)(((byte)(84)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(481, 250);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 29);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.cmbUSerGroup);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.lstbSelsected);
            this.panel4.Controls.Add(this.lstbNonSelsected);
            this.panel4.Controls.Add(this.btnNonSelect);
            this.panel4.Controls.Add(this.btnSelect);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Controls.Add(this.btnClose);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.ForeColor = System.Drawing.Color.Black;
            this.panel4.Location = new System.Drawing.Point(0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(15);
            this.panel4.Size = new System.Drawing.Size(590, 307);
            this.panel4.TabIndex = 218;
            // 
            // cmbUSerGroup
            // 
            this.cmbUSerGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUSerGroup.FormattingEnabled = true;
            this.cmbUSerGroup.Location = new System.Drawing.Point(20, 10);
            this.cmbUSerGroup.Name = "cmbUSerGroup";
            this.cmbUSerGroup.Size = new System.Drawing.Size(212, 21);
            this.cmbUSerGroup.TabIndex = 222;
            this.cmbUSerGroup.SelectedIndexChanged += new System.EventHandler(this.cmbUSerGroup_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(357, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 16);
            this.label10.TabIndex = 221;
            this.label10.Text = "Current Items";
            // 
            // lstbSelsected
            // 
            this.lstbSelsected.BackColor = System.Drawing.Color.White;
            this.lstbSelsected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbSelsected.ForeColor = System.Drawing.Color.Black;
            this.lstbSelsected.FormattingEnabled = true;
            this.lstbSelsected.ItemHeight = 16;
            this.lstbSelsected.Location = new System.Drawing.Point(360, 41);
            this.lstbSelsected.Name = "lstbSelsected";
            this.lstbSelsected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstbSelsected.Size = new System.Drawing.Size(214, 196);
            this.lstbSelsected.TabIndex = 0;
            this.lstbSelsected.Click += new System.EventHandler(this.lstbSelsected_Click);
            this.lstbSelsected.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstbSelsected_KeyUp);
            this.lstbSelsected.Leave += new System.EventHandler(this.lstbSelsected_Leave);
            // 
            // lstbNonSelsected
            // 
            this.lstbNonSelsected.BackColor = System.Drawing.Color.White;
            this.lstbNonSelsected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbNonSelsected.FormattingEnabled = true;
            this.lstbNonSelsected.ItemHeight = 16;
            this.lstbNonSelsected.Location = new System.Drawing.Point(20, 41);
            this.lstbNonSelsected.Name = "lstbNonSelsected";
            this.lstbNonSelsected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstbNonSelsected.Size = new System.Drawing.Size(214, 196);
            this.lstbNonSelsected.TabIndex = 2;
            this.lstbNonSelsected.Click += new System.EventHandler(this.lstbNonSelsected_Click);
            this.lstbNonSelsected.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstbNonSelsected_KeyUp);
            this.lstbNonSelsected.Leave += new System.EventHandler(this.lstbNonSelsected_Leave);
            // 
            // btnNonSelect
            // 
            this.btnNonSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNonSelect.Location = new System.Drawing.Point(258, 137);
            this.btnNonSelect.Name = "btnNonSelect";
            this.btnNonSelect.Size = new System.Drawing.Size(75, 23);
            this.btnNonSelect.TabIndex = 1;
            this.btnNonSelect.Text = "<<";
            this.btnNonSelect.UseVisualStyleBackColor = true;
            this.btnNonSelect.Click += new System.EventHandler(this.btnNonSelect_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(258, 108);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = ">>";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Location = new System.Drawing.Point(386, 250);
            this.btnClear.Margin = new System.Windows.Forms.Padding(1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 29);
            this.btnClear.TabIndex = 6;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(188)))), ((int)(((byte)(232)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(291, 250);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 29);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPOSUserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(590, 309);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmPOSUserSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Launch Customization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPOSUserSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmPOSUserSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPOSUserSettings_KeyDown);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnNonSelect;
        private System.Windows.Forms.ListBox lstbNonSelsected;
        private System.Windows.Forms.ListBox lstbSelsected;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbUSerGroup;
    }
}