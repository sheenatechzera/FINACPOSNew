namespace FinacPOS
{
    partial class frmPOSSettings
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
            this.cmbZeroQty = new System.Windows.Forms.ComboBox();
            this.lblZeroQty = new System.Windows.Forms.Label();
            this.chkBlockZeroPrice = new System.Windows.Forms.CheckBox();
            this.chkAlwaysEnableHoldBillView = new System.Windows.Forms.CheckBox();
            this.chkSessionMngmnt = new System.Windows.Forms.CheckBox();
            this.ChkStockView = new System.Windows.Forms.CheckBox();
            this.ChkActiveTable = new System.Windows.Forms.CheckBox();
            this.ChkShowProdtSummryInSessionclose = new System.Windows.Forms.CheckBox();
            this.ChkAddqtyinsameBarcode = new System.Windows.Forms.CheckBox();
            this.cmbCustBill = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowCustBill = new System.Windows.Forms.CheckBox();
            this.grpPrint = new System.Windows.Forms.GroupBox();
            this.chkCompanyArabic = new System.Windows.Forms.CheckBox();
            this.chkAddress = new System.Windows.Forms.CheckBox();
            this.chkPhone = new System.Windows.Forms.CheckBox();
            this.chkCompany = new System.Windows.Forms.CheckBox();
            this.txtPhoneH = new System.Windows.Forms.TextBox();
            this.txtAddressH = new System.Windows.Forms.TextBox();
            this.txtCompanyArabicH = new System.Windows.Forms.TextBox();
            this.txtCompanyH = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblCompanyNameArabic = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtCompanyArabic = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.txtExpiry = new System.Windows.Forms.TextBox();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkQtyChangeAuth = new System.Windows.Forms.CheckBox();
            this.chkCashBoxOpenAuth = new System.Windows.Forms.CheckBox();
            this.chkExchangeItemAuth = new System.Windows.Forms.CheckBox();
            this.chklastBillPrintAuth = new System.Windows.Forms.CheckBox();
            this.chkItemGrouping = new System.Windows.Forms.CheckBox();
            this.chkZeroStockAuth = new System.Windows.Forms.CheckBox();
            this.chkCreditSalesAuth = new System.Windows.Forms.CheckBox();
            this.chkPriceChangeAuth = new System.Windows.Forms.CheckBox();
            this.chkHoldBillAuth = new System.Windows.Forms.CheckBox();
            this.chkDiscountAuth = new System.Windows.Forms.CheckBox();
            this.chkBillClearAuth = new System.Windows.Forms.CheckBox();
            this.lblPOSSettingsId = new System.Windows.Forms.Label();
            this.lblDeleteMode = new System.Windows.Forms.Label();
            this.cmbDeleteMode = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.grpPrint.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbDeleteMode);
            this.panel1.Controls.Add(this.lblDeleteMode);
            this.panel1.Controls.Add(this.cmbZeroQty);
            this.panel1.Controls.Add(this.lblZeroQty);
            this.panel1.Controls.Add(this.chkBlockZeroPrice);
            this.panel1.Controls.Add(this.chkAlwaysEnableHoldBillView);
            this.panel1.Controls.Add(this.chkSessionMngmnt);
            this.panel1.Controls.Add(this.ChkStockView);
            this.panel1.Controls.Add(this.ChkActiveTable);
            this.panel1.Controls.Add(this.ChkShowProdtSummryInSessionclose);
            this.panel1.Controls.Add(this.ChkAddqtyinsameBarcode);
            this.panel1.Controls.Add(this.cmbCustBill);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chkShowCustBill);
            this.panel1.Controls.Add(this.grpPrint);
            this.panel1.Controls.Add(this.txtExpiry);
            this.panel1.Controls.Add(this.lblExpiry);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.chkQtyChangeAuth);
            this.panel1.Controls.Add(this.chkCashBoxOpenAuth);
            this.panel1.Controls.Add(this.chkExchangeItemAuth);
            this.panel1.Controls.Add(this.chklastBillPrintAuth);
            this.panel1.Controls.Add(this.chkItemGrouping);
            this.panel1.Controls.Add(this.chkZeroStockAuth);
            this.panel1.Controls.Add(this.chkCreditSalesAuth);
            this.panel1.Controls.Add(this.chkPriceChangeAuth);
            this.panel1.Controls.Add(this.chkHoldBillAuth);
            this.panel1.Controls.Add(this.chkDiscountAuth);
            this.panel1.Controls.Add(this.chkBillClearAuth);
            this.panel1.Controls.Add(this.lblPOSSettingsId);
            this.panel1.Font = new System.Drawing.Font("Verdana", 9F);
            this.panel1.Location = new System.Drawing.Point(-8, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(807, 441);
            this.panel1.TabIndex = 0;
            // 
            // cmbZeroQty
            // 
            this.cmbZeroQty.FormattingEnabled = true;
            this.cmbZeroQty.Items.AddRange(new object[] {
            "Warn",
            "Block",
            "Ignore"});
            this.cmbZeroQty.Location = new System.Drawing.Point(616, 156);
            this.cmbZeroQty.Name = "cmbZeroQty";
            this.cmbZeroQty.Size = new System.Drawing.Size(75, 22);
            this.cmbZeroQty.TabIndex = 45;
            // 
            // lblZeroQty
            // 
            this.lblZeroQty.AutoSize = true;
            this.lblZeroQty.Location = new System.Drawing.Point(437, 159);
            this.lblZeroQty.Name = "lblZeroQty";
            this.lblZeroQty.Size = new System.Drawing.Size(172, 14);
            this.lblZeroQty.TabIndex = 44;
            this.lblZeroQty.Text = "Zero Quantity Alert Status";
            // 
            // chkBlockZeroPrice
            // 
            this.chkBlockZeroPrice.AutoSize = true;
            this.chkBlockZeroPrice.Location = new System.Drawing.Point(433, 133);
            this.chkBlockZeroPrice.Name = "chkBlockZeroPrice";
            this.chkBlockZeroPrice.Size = new System.Drawing.Size(178, 18);
            this.chkBlockZeroPrice.TabIndex = 43;
            this.chkBlockZeroPrice.Text = "Block Zero Price in Sales";
            this.chkBlockZeroPrice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.chkBlockZeroPrice.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysEnableHoldBillView
            // 
            this.chkAlwaysEnableHoldBillView.AutoSize = true;
            this.chkAlwaysEnableHoldBillView.Location = new System.Drawing.Point(433, 108);
            this.chkAlwaysEnableHoldBillView.Name = "chkAlwaysEnableHoldBillView";
            this.chkAlwaysEnableHoldBillView.Size = new System.Drawing.Size(200, 18);
            this.chkAlwaysEnableHoldBillView.TabIndex = 42;
            this.chkAlwaysEnableHoldBillView.Text = "Always Enable HoldBill View";
            this.chkAlwaysEnableHoldBillView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.chkAlwaysEnableHoldBillView.UseVisualStyleBackColor = true;
            // 
            // chkSessionMngmnt
            // 
            this.chkSessionMngmnt.AutoSize = true;
            this.chkSessionMngmnt.Location = new System.Drawing.Point(433, 83);
            this.chkSessionMngmnt.Name = "chkSessionMngmnt";
            this.chkSessionMngmnt.Size = new System.Drawing.Size(214, 18);
            this.chkSessionMngmnt.TabIndex = 41;
            this.chkSessionMngmnt.Text = "Session Managment By Admin";
            this.chkSessionMngmnt.UseVisualStyleBackColor = true;
            // 
            // ChkStockView
            // 
            this.ChkStockView.AutoSize = true;
            this.ChkStockView.Location = new System.Drawing.Point(37, 168);
            this.ChkStockView.Name = "ChkStockView";
            this.ChkStockView.Size = new System.Drawing.Size(94, 18);
            this.ChkStockView.TabIndex = 40;
            this.ChkStockView.Text = "Stock View";
            this.ChkStockView.UseVisualStyleBackColor = true;
            // 
            // ChkActiveTable
            // 
            this.ChkActiveTable.AutoSize = true;
            this.ChkActiveTable.Location = new System.Drawing.Point(227, 180);
            this.ChkActiveTable.Name = "ChkActiveTable";
            this.ChkActiveTable.Size = new System.Drawing.Size(100, 18);
            this.ChkActiveTable.TabIndex = 39;
            this.ChkActiveTable.Text = "Active Table";
            this.ChkActiveTable.UseVisualStyleBackColor = true;
            this.ChkActiveTable.Enter += new System.EventHandler(this.ChkActiveTable_Enter);
            this.ChkActiveTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChkActiveTable_KeyDown);
            this.ChkActiveTable.Leave += new System.EventHandler(this.ChkActiveTable_Leave);
            // 
            // ChkShowProdtSummryInSessionclose
            // 
            this.ChkShowProdtSummryInSessionclose.AutoSize = true;
            this.ChkShowProdtSummryInSessionclose.Location = new System.Drawing.Point(433, 58);
            this.ChkShowProdtSummryInSessionclose.Name = "ChkShowProdtSummryInSessionclose";
            this.ChkShowProdtSummryInSessionclose.Size = new System.Drawing.Size(288, 18);
            this.ChkShowProdtSummryInSessionclose.TabIndex = 38;
            this.ChkShowProdtSummryInSessionclose.Text = "Show Product  Summary In Session Close";
            this.ChkShowProdtSummryInSessionclose.UseVisualStyleBackColor = true;
            this.ChkShowProdtSummryInSessionclose.Enter += new System.EventHandler(this.ChkShowProdtSummryInSessionclose_Enter);
            this.ChkShowProdtSummryInSessionclose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChkShowProdtSummryInSessionclose_KeyDown);
            this.ChkShowProdtSummryInSessionclose.Leave += new System.EventHandler(this.ChkShowProdtSummryInSessionclose_Leave);
            // 
            // ChkAddqtyinsameBarcode
            // 
            this.ChkAddqtyinsameBarcode.AutoSize = true;
            this.ChkAddqtyinsameBarcode.Location = new System.Drawing.Point(433, 33);
            this.ChkAddqtyinsameBarcode.Name = "ChkAddqtyinsameBarcode";
            this.ChkAddqtyinsameBarcode.Size = new System.Drawing.Size(234, 18);
            this.ChkAddqtyinsameBarcode.TabIndex = 37;
            this.ChkAddqtyinsameBarcode.Text = "Add Qty In Same Barcode To Grid";
            this.ChkAddqtyinsameBarcode.UseVisualStyleBackColor = true;
            this.ChkAddqtyinsameBarcode.Enter += new System.EventHandler(this.ChkAddqtyinsameBarcode_Enter);
            this.ChkAddqtyinsameBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChkAddqtyinsameBarcode_KeyDown);
            this.ChkAddqtyinsameBarcode.Leave += new System.EventHandler(this.ChkAddqtyinsameBarcode_Leave);
            // 
            // cmbCustBill
            // 
            this.cmbCustBill.FormattingEnabled = true;
            this.cmbCustBill.Items.AddRange(new object[] {
            "Summary",
            "Full Bill"});
            this.cmbCustBill.Location = new System.Drawing.Point(356, 156);
            this.cmbCustBill.Name = "cmbCustBill";
            this.cmbCustBill.Size = new System.Drawing.Size(72, 22);
            this.cmbCustBill.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 156);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 14);
            this.label1.TabIndex = 35;
            this.label1.Text = "Customer Bill Copy";
            // 
            // chkShowCustBill
            // 
            this.chkShowCustBill.AutoSize = true;
            this.chkShowCustBill.Location = new System.Drawing.Point(433, 9);
            this.chkShowCustBill.Name = "chkShowCustBill";
            this.chkShowCustBill.Size = new System.Drawing.Size(229, 18);
            this.chkShowCustBill.TabIndex = 34;
            this.chkShowCustBill.Text = "Show Customer Balance In Print";
            this.chkShowCustBill.UseVisualStyleBackColor = true;
            // 
            // grpPrint
            // 
            this.grpPrint.Controls.Add(this.chkCompanyArabic);
            this.grpPrint.Controls.Add(this.chkAddress);
            this.grpPrint.Controls.Add(this.chkPhone);
            this.grpPrint.Controls.Add(this.chkCompany);
            this.grpPrint.Controls.Add(this.txtPhoneH);
            this.grpPrint.Controls.Add(this.txtAddressH);
            this.grpPrint.Controls.Add(this.txtCompanyArabicH);
            this.grpPrint.Controls.Add(this.txtCompanyH);
            this.grpPrint.Controls.Add(this.lblPhone);
            this.grpPrint.Controls.Add(this.lblAddress);
            this.grpPrint.Controls.Add(this.lblCompanyNameArabic);
            this.grpPrint.Controls.Add(this.lblCompanyName);
            this.grpPrint.Controls.Add(this.txtPhone);
            this.grpPrint.Controls.Add(this.txtAddress);
            this.grpPrint.Controls.Add(this.txtCompanyArabic);
            this.grpPrint.Controls.Add(this.txtCompanyName);
            this.grpPrint.Location = new System.Drawing.Point(37, 209);
            this.grpPrint.Name = "grpPrint";
            this.grpPrint.Size = new System.Drawing.Size(546, 190);
            this.grpPrint.TabIndex = 33;
            this.grpPrint.TabStop = false;
            this.grpPrint.Text = "POS Bill Details";
            // 
            // chkCompanyArabic
            // 
            this.chkCompanyArabic.AutoSize = true;
            this.chkCompanyArabic.Location = new System.Drawing.Point(474, 44);
            this.chkCompanyArabic.Name = "chkCompanyArabic";
            this.chkCompanyArabic.Size = new System.Drawing.Size(66, 18);
            this.chkCompanyArabic.TabIndex = 46;
            this.chkCompanyArabic.Text = "Visible";
            this.chkCompanyArabic.UseVisualStyleBackColor = true;
            // 
            // chkAddress
            // 
            this.chkAddress.AutoSize = true;
            this.chkAddress.Location = new System.Drawing.Point(474, 76);
            this.chkAddress.Name = "chkAddress";
            this.chkAddress.Size = new System.Drawing.Size(66, 18);
            this.chkAddress.TabIndex = 45;
            this.chkAddress.Text = "Visible";
            this.chkAddress.UseVisualStyleBackColor = true;
            // 
            // chkPhone
            // 
            this.chkPhone.AutoSize = true;
            this.chkPhone.Location = new System.Drawing.Point(474, 151);
            this.chkPhone.Name = "chkPhone";
            this.chkPhone.Size = new System.Drawing.Size(66, 18);
            this.chkPhone.TabIndex = 44;
            this.chkPhone.Text = "Visible";
            this.chkPhone.UseVisualStyleBackColor = true;
            // 
            // chkCompany
            // 
            this.chkCompany.AutoSize = true;
            this.chkCompany.Location = new System.Drawing.Point(474, 20);
            this.chkCompany.Name = "chkCompany";
            this.chkCompany.Size = new System.Drawing.Size(66, 18);
            this.chkCompany.TabIndex = 42;
            this.chkCompany.Text = "Visible";
            this.chkCompany.UseVisualStyleBackColor = true;
            // 
            // txtPhoneH
            // 
            this.txtPhoneH.Location = new System.Drawing.Point(431, 149);
            this.txtPhoneH.Name = "txtPhoneH";
            this.txtPhoneH.Size = new System.Drawing.Size(37, 22);
            this.txtPhoneH.TabIndex = 41;
            this.txtPhoneH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhoneH_KeyPress);
            this.txtPhoneH.Leave += new System.EventHandler(this.txtPhoneH_Leave);
            // 
            // txtAddressH
            // 
            this.txtAddressH.Location = new System.Drawing.Point(431, 72);
            this.txtAddressH.Name = "txtAddressH";
            this.txtAddressH.Size = new System.Drawing.Size(37, 22);
            this.txtAddressH.TabIndex = 40;
            this.txtAddressH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAddressH_KeyPress);
            this.txtAddressH.Leave += new System.EventHandler(this.txtAddressH_Leave);
            // 
            // txtCompanyArabicH
            // 
            this.txtCompanyArabicH.Location = new System.Drawing.Point(431, 44);
            this.txtCompanyArabicH.Name = "txtCompanyArabicH";
            this.txtCompanyArabicH.Size = new System.Drawing.Size(37, 22);
            this.txtCompanyArabicH.TabIndex = 39;
            this.txtCompanyArabicH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompanyArabicH_KeyPress);
            this.txtCompanyArabicH.Leave += new System.EventHandler(this.txtCompanyArabicH_Leave);
            // 
            // txtCompanyH
            // 
            this.txtCompanyH.Location = new System.Drawing.Point(431, 18);
            this.txtCompanyH.Name = "txtCompanyH";
            this.txtCompanyH.Size = new System.Drawing.Size(37, 22);
            this.txtCompanyH.TabIndex = 38;
            this.txtCompanyH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCompanyH_KeyPress);
            this.txtCompanyH.Leave += new System.EventHandler(this.txtCompanyH_Leave);
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(9, 157);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(47, 14);
            this.lblPhone.TabIndex = 37;
            this.lblPhone.Text = "Phone";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(9, 72);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(58, 14);
            this.lblAddress.TabIndex = 36;
            this.lblAddress.Text = "Address";
            // 
            // lblCompanyNameArabic
            // 
            this.lblCompanyNameArabic.AutoSize = true;
            this.lblCompanyNameArabic.Location = new System.Drawing.Point(9, 44);
            this.lblCompanyNameArabic.Name = "lblCompanyNameArabic";
            this.lblCompanyNameArabic.Size = new System.Drawing.Size(45, 14);
            this.lblCompanyNameArabic.TabIndex = 35;
            this.lblCompanyNameArabic.Text = "Arabic";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(9, 21);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(106, 14);
            this.lblCompanyName.TabIndex = 34;
            this.lblCompanyName.Text = "Company Name";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(121, 149);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(304, 22);
            this.txtPhone.TabIndex = 3;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(121, 72);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(304, 71);
            this.txtAddress.TabIndex = 2;
            // 
            // txtCompanyArabic
            // 
            this.txtCompanyArabic.Location = new System.Drawing.Point(121, 44);
            this.txtCompanyArabic.Name = "txtCompanyArabic";
            this.txtCompanyArabic.Size = new System.Drawing.Size(304, 22);
            this.txtCompanyArabic.TabIndex = 1;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.Location = new System.Drawing.Point(121, 18);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(304, 22);
            this.txtCompanyName.TabIndex = 0;
            // 
            // txtExpiry
            // 
            this.txtExpiry.Location = new System.Drawing.Point(377, 130);
            this.txtExpiry.Name = "txtExpiry";
            this.txtExpiry.Size = new System.Drawing.Size(50, 22);
            this.txtExpiry.TabIndex = 12;
            this.txtExpiry.Text = "0";
            this.txtExpiry.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtExpiry_KeyDown);
            this.txtExpiry.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtExpiry_KeyPress);
            // 
            // lblExpiry
            // 
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Location = new System.Drawing.Point(224, 133);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(155, 14);
            this.lblExpiry.TabIndex = 31;
            this.lblExpiry.Text = "Credit Note expiry days\n";
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
            this.btnClose.Location = new System.Drawing.Point(477, 403);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 28);
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
            this.btnSave.Location = new System.Drawing.Point(387, 403);
            this.btnSave.Margin = new System.Windows.Forms.Padding(1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 28);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkQtyChangeAuth
            // 
            this.chkQtyChangeAuth.AutoSize = true;
            this.chkQtyChangeAuth.Location = new System.Drawing.Point(39, 144);
            this.chkQtyChangeAuth.Name = "chkQtyChangeAuth";
            this.chkQtyChangeAuth.Size = new System.Drawing.Size(134, 18);
            this.chkQtyChangeAuth.TabIndex = 10;
            this.chkQtyChangeAuth.Text = "Qty Change Auth";
            this.chkQtyChangeAuth.UseVisualStyleBackColor = true;
            // 
            // chkCashBoxOpenAuth
            // 
            this.chkCashBoxOpenAuth.AutoSize = true;
            this.chkCashBoxOpenAuth.Location = new System.Drawing.Point(228, 108);
            this.chkCashBoxOpenAuth.Name = "chkCashBoxOpenAuth";
            this.chkCashBoxOpenAuth.Size = new System.Drawing.Size(152, 18);
            this.chkCashBoxOpenAuth.TabIndex = 9;
            this.chkCashBoxOpenAuth.Text = "CashBox Open Auth";
            this.chkCashBoxOpenAuth.UseVisualStyleBackColor = true;
            // 
            // chkExchangeItemAuth
            // 
            this.chkExchangeItemAuth.AutoSize = true;
            this.chkExchangeItemAuth.Location = new System.Drawing.Point(228, 83);
            this.chkExchangeItemAuth.Name = "chkExchangeItemAuth";
            this.chkExchangeItemAuth.Size = new System.Drawing.Size(153, 18);
            this.chkExchangeItemAuth.TabIndex = 8;
            this.chkExchangeItemAuth.Text = "Exchange Item Auth";
            this.chkExchangeItemAuth.UseVisualStyleBackColor = true;
            // 
            // chklastBillPrintAuth
            // 
            this.chklastBillPrintAuth.AutoSize = true;
            this.chklastBillPrintAuth.Location = new System.Drawing.Point(228, 58);
            this.chklastBillPrintAuth.Name = "chklastBillPrintAuth";
            this.chklastBillPrintAuth.Size = new System.Drawing.Size(107, 18);
            this.chklastBillPrintAuth.TabIndex = 7;
            this.chklastBillPrintAuth.Text = "Last Bill Print";
            this.chklastBillPrintAuth.UseVisualStyleBackColor = true;
            // 
            // chkItemGrouping
            // 
            this.chkItemGrouping.AutoSize = true;
            this.chkItemGrouping.Location = new System.Drawing.Point(228, 33);
            this.chkItemGrouping.Name = "chkItemGrouping";
            this.chkItemGrouping.Size = new System.Drawing.Size(120, 18);
            this.chkItemGrouping.TabIndex = 6;
            this.chkItemGrouping.Text = "Item Grouping ";
            this.chkItemGrouping.UseVisualStyleBackColor = true;
            // 
            // chkZeroStockAuth
            // 
            this.chkZeroStockAuth.AutoSize = true;
            this.chkZeroStockAuth.Location = new System.Drawing.Point(228, 9);
            this.chkZeroStockAuth.Name = "chkZeroStockAuth";
            this.chkZeroStockAuth.Size = new System.Drawing.Size(126, 18);
            this.chkZeroStockAuth.TabIndex = 5;
            this.chkZeroStockAuth.Text = "Zero Stock Auth";
            this.chkZeroStockAuth.UseVisualStyleBackColor = true;
            // 
            // chkCreditSalesAuth
            // 
            this.chkCreditSalesAuth.AutoSize = true;
            this.chkCreditSalesAuth.Location = new System.Drawing.Point(39, 120);
            this.chkCreditSalesAuth.Name = "chkCreditSalesAuth";
            this.chkCreditSalesAuth.Size = new System.Drawing.Size(135, 18);
            this.chkCreditSalesAuth.TabIndex = 4;
            this.chkCreditSalesAuth.Text = "Credit Sales Auth";
            this.chkCreditSalesAuth.UseVisualStyleBackColor = true;
            // 
            // chkPriceChangeAuth
            // 
            this.chkPriceChangeAuth.AutoSize = true;
            this.chkPriceChangeAuth.Location = new System.Drawing.Point(39, 95);
            this.chkPriceChangeAuth.Name = "chkPriceChangeAuth";
            this.chkPriceChangeAuth.Size = new System.Drawing.Size(142, 18);
            this.chkPriceChangeAuth.TabIndex = 3;
            this.chkPriceChangeAuth.Text = "Price Change Auth";
            this.chkPriceChangeAuth.UseVisualStyleBackColor = true;
            // 
            // chkHoldBillAuth
            // 
            this.chkHoldBillAuth.AutoSize = true;
            this.chkHoldBillAuth.Location = new System.Drawing.Point(39, 70);
            this.chkHoldBillAuth.Name = "chkHoldBillAuth";
            this.chkHoldBillAuth.Size = new System.Drawing.Size(108, 18);
            this.chkHoldBillAuth.TabIndex = 2;
            this.chkHoldBillAuth.Text = "Hold Bill Auth";
            this.chkHoldBillAuth.UseVisualStyleBackColor = true;
            // 
            // chkDiscountAuth
            // 
            this.chkDiscountAuth.AutoSize = true;
            this.chkDiscountAuth.Location = new System.Drawing.Point(39, 45);
            this.chkDiscountAuth.Name = "chkDiscountAuth";
            this.chkDiscountAuth.Size = new System.Drawing.Size(113, 18);
            this.chkDiscountAuth.TabIndex = 1;
            this.chkDiscountAuth.Text = "Discount Auth";
            this.chkDiscountAuth.UseVisualStyleBackColor = true;
            // 
            // chkBillClearAuth
            // 
            this.chkBillClearAuth.AutoSize = true;
            this.chkBillClearAuth.Location = new System.Drawing.Point(39, 20);
            this.chkBillClearAuth.Name = "chkBillClearAuth";
            this.chkBillClearAuth.Size = new System.Drawing.Size(84, 18);
            this.chkBillClearAuth.TabIndex = 0;
            this.chkBillClearAuth.Text = "Bill Clear ";
            this.chkBillClearAuth.UseVisualStyleBackColor = true;
            // 
            // lblPOSSettingsId
            // 
            this.lblPOSSettingsId.AutoSize = true;
            this.lblPOSSettingsId.Location = new System.Drawing.Point(517, 410);
            this.lblPOSSettingsId.Name = "lblPOSSettingsId";
            this.lblPOSSettingsId.Size = new System.Drawing.Size(15, 14);
            this.lblPOSSettingsId.TabIndex = 32;
            this.lblPOSSettingsId.Text = "0";
            // 
            // lblDeleteMode
            // 
            this.lblDeleteMode.AutoSize = true;
            this.lblDeleteMode.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.lblDeleteMode.Location = new System.Drawing.Point(436, 188);
            this.lblDeleteMode.Name = "lblDeleteMode";
            this.lblDeleteMode.Size = new System.Drawing.Size(86, 14);
            this.lblDeleteMode.TabIndex = 46;
            this.lblDeleteMode.Text = "Delete Mode";
            // 
            // cmbDeleteMode
            // 
            this.cmbDeleteMode.FormattingEnabled = true;
            this.cmbDeleteMode.Items.AddRange(new object[] {
            "Delete By Button Click",
            "Delete By Keyboard Key",
            "Delete By Barcode"});
            this.cmbDeleteMode.Location = new System.Drawing.Point(528, 185);
            this.cmbDeleteMode.Name = "cmbDeleteMode";
            this.cmbDeleteMode.Size = new System.Drawing.Size(163, 22);
            this.cmbDeleteMode.TabIndex = 47;
            // 
            // frmPOSSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 441);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmPOSSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Settings";
            this.Load += new System.EventHandler(this.frmPOSSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grpPrint.ResumeLayout(false);
            this.grpPrint.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkBillClearAuth;
        private System.Windows.Forms.CheckBox chkHoldBillAuth;
        private System.Windows.Forms.CheckBox chkDiscountAuth;
        private System.Windows.Forms.CheckBox chkPriceChangeAuth;
        private System.Windows.Forms.CheckBox chkZeroStockAuth;
        private System.Windows.Forms.CheckBox chkCreditSalesAuth;
        private System.Windows.Forms.CheckBox chklastBillPrintAuth;
        private System.Windows.Forms.CheckBox chkItemGrouping;
        private System.Windows.Forms.CheckBox chkExchangeItemAuth;
        private System.Windows.Forms.CheckBox chkCashBoxOpenAuth;
        private System.Windows.Forms.CheckBox chkQtyChangeAuth;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtExpiry;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Label lblPOSSettingsId;
        private System.Windows.Forms.GroupBox grpPrint;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblCompanyNameArabic;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtCompanyArabic;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.TextBox txtPhoneH;
        private System.Windows.Forms.TextBox txtAddressH;
        private System.Windows.Forms.TextBox txtCompanyArabicH;
        private System.Windows.Forms.TextBox txtCompanyH;
        private System.Windows.Forms.CheckBox chkCompanyArabic;
        private System.Windows.Forms.CheckBox chkAddress;
        private System.Windows.Forms.CheckBox chkPhone;
        private System.Windows.Forms.CheckBox chkCompany;
        private System.Windows.Forms.CheckBox chkShowCustBill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustBill;
        private System.Windows.Forms.CheckBox ChkAddqtyinsameBarcode;
        private System.Windows.Forms.CheckBox ChkShowProdtSummryInSessionclose;
        private System.Windows.Forms.CheckBox ChkActiveTable;
        private System.Windows.Forms.CheckBox ChkStockView;
        private System.Windows.Forms.CheckBox chkSessionMngmnt;
        private System.Windows.Forms.CheckBox chkAlwaysEnableHoldBillView;
        private System.Windows.Forms.CheckBox chkBlockZeroPrice;
        private System.Windows.Forms.ComboBox cmbZeroQty;
        private System.Windows.Forms.Label lblZeroQty;
        private System.Windows.Forms.ComboBox cmbDeleteMode;
        private System.Windows.Forms.Label lblDeleteMode;
    }
}