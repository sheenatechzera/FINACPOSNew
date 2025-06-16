namespace FinacPOS
{
    partial class frmPOSSalesReturn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPOSSalesReturn));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPriceChange = new System.Windows.Forms.Button();
            this.btnBarcode = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnQty = new System.Windows.Forms.Button();
            this.btnSeven = new System.Windows.Forms.Button();
            this.btnClosePOS = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalQty = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtTaxable = new System.Windows.Forms.TextBox();
            this.btnOne = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.btnDot = new System.Windows.Forms.Button();
            this.btnFindProduct = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.label15 = new System.Windows.Forms.Label();
            this.btnTwo = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnThree = new System.Windows.Forms.Button();
            this.btnNine = new System.Windows.Forms.Button();
            this.btnFour = new System.Windows.Forms.Button();
            this.btnEight = new System.Windows.Forms.Button();
            this.btnSix = new System.Windows.Forms.Button();
            this.btnFive = new System.Windows.Forms.Button();
            this.txtTaxAmt = new System.Windows.Forms.TextBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.btnFindCustomer = new System.Windows.Forms.Button();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelBarcode = new System.Windows.Forms.Panel();
            this.lblBarcodeScanningType = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.panelBillDetails = new System.Windows.Forms.Panel();
            this.lblBillNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBillDate = new System.Windows.Forms.Label();
            this.lblCounter = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblSessionNO = new System.Windows.Forms.Label();
            this.lblBillTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSessionDate = new System.Windows.Forms.Label();
            this.dgvProduct = new System.Windows.Forms.DataGridView();
            this.SLNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaseUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitConversion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExcludeRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GrossValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NetValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillDiscIndProductAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountBeforeDisc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rateDiscAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.offerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMainButton = new System.Windows.Forms.Panel();
            this.btnReturnFullBill = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtCredit = new System.Windows.Forms.RadioButton();
            this.rbtCash = new System.Windows.Forms.RadioButton();
            this.rbtCreditNote = new System.Windows.Forms.RadioButton();
            this.btnNewSale = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblLedgerId = new System.Windows.Forms.Label();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDocumentCreditNote = new System.Drawing.Printing.PrintDocument();
            this.printDocumentThermal3 = new System.Drawing.Printing.PrintDocument();
            this.panelMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelBarcode.SuspendLayout();
            this.panelBillDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).BeginInit();
            this.panelMainButton.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(78)))), ((int)(((byte)(92)))));
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(15, 517);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(228, 75);
            this.btnSave.TabIndex = 43;
            this.btnSave.Text = "SAVE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(123)))), ((int)(((byte)(149)))));
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(176, 332);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 65);
            this.btnDelete.TabIndex = 42;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPriceChange
            // 
            this.btnPriceChange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(123)))), ((int)(((byte)(149)))));
            this.btnPriceChange.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPriceChange.FlatAppearance.BorderSize = 0;
            this.btnPriceChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPriceChange.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPriceChange.ForeColor = System.Drawing.Color.White;
            this.btnPriceChange.Location = new System.Drawing.Point(12, 332);
            this.btnPriceChange.Name = "btnPriceChange";
            this.btnPriceChange.Size = new System.Drawing.Size(75, 65);
            this.btnPriceChange.TabIndex = 40;
            this.btnPriceChange.Text = "Price Change\r\n(F7)";
            this.btnPriceChange.UseVisualStyleBackColor = false;
            this.btnPriceChange.Click += new System.EventHandler(this.btnPriceChange_Click);
            // 
            // btnBarcode
            // 
            this.btnBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(152)))), ((int)(((byte)(29)))));
            this.btnBarcode.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBarcode.FlatAppearance.BorderSize = 0;
            this.btnBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBarcode.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarcode.ForeColor = System.Drawing.Color.White;
            this.btnBarcode.Location = new System.Drawing.Point(176, 261);
            this.btnBarcode.Name = "btnBarcode";
            this.btnBarcode.Size = new System.Drawing.Size(75, 65);
            this.btnBarcode.TabIndex = 39;
            this.btnBarcode.Text = "Barcode";
            this.btnBarcode.UseVisualStyleBackColor = false;
            this.btnBarcode.Click += new System.EventHandler(this.btnBarcode_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(140)))), ((int)(((byte)(68)))));
            this.btnEnter.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEnter.FlatAppearance.BorderSize = 0;
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnter.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.ForeColor = System.Drawing.Color.White;
            this.btnEnter.Location = new System.Drawing.Point(94, 261);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 65);
            this.btnEnter.TabIndex = 38;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnQty
            // 
            this.btnQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(88)))), ((int)(((byte)(39)))));
            this.btnQty.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnQty.FlatAppearance.BorderSize = 0;
            this.btnQty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQty.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQty.ForeColor = System.Drawing.Color.White;
            this.btnQty.Location = new System.Drawing.Point(12, 261);
            this.btnQty.Name = "btnQty";
            this.btnQty.Size = new System.Drawing.Size(75, 65);
            this.btnQty.TabIndex = 27;
            this.btnQty.Text = "Qty\r\n(F4)";
            this.btnQty.UseVisualStyleBackColor = false;
            this.btnQty.Click += new System.EventHandler(this.btnQty_Click);
            // 
            // btnSeven
            // 
            this.btnSeven.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnSeven.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSeven.FlatAppearance.BorderSize = 0;
            this.btnSeven.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeven.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeven.ForeColor = System.Drawing.Color.White;
            this.btnSeven.Location = new System.Drawing.Point(15, 131);
            this.btnSeven.Name = "btnSeven";
            this.btnSeven.Size = new System.Drawing.Size(70, 50);
            this.btnSeven.TabIndex = 32;
            this.btnSeven.Text = "7";
            this.btnSeven.UseVisualStyleBackColor = false;
            this.btnSeven.Click += new System.EventHandler(this.btnSeven_Click);
            // 
            // btnClosePOS
            // 
            this.btnClosePOS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnClosePOS.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClosePOS.FlatAppearance.BorderSize = 0;
            this.btnClosePOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClosePOS.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClosePOS.ForeColor = System.Drawing.Color.White;
            this.btnClosePOS.Location = new System.Drawing.Point(15, 644);
            this.btnClosePOS.Name = "btnClosePOS";
            this.btnClosePOS.Size = new System.Drawing.Size(90, 60);
            this.btnClosePOS.TabIndex = 42;
            this.btnClosePOS.Text = "Close POS";
            this.btnClosePOS.UseVisualStyleBackColor = false;
            this.btnClosePOS.Click += new System.EventHandler(this.btnClosePOS_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(657, 569);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 41;
            this.label1.Text = "Total Qty:";
            // 
            // lblTotalQty
            // 
            this.lblTotalQty.AutoSize = true;
            this.lblTotalQty.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQty.Location = new System.Drawing.Point(729, 568);
            this.lblTotalQty.Name = "lblTotalQty";
            this.lblTotalQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTotalQty.Size = new System.Drawing.Size(15, 16);
            this.lblTotalQty.TabIndex = 40;
            this.lblTotalQty.Text = "0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(547, 664);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(54, 23);
            this.label19.TabIndex = 38;
            this.label19.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.White;
            this.txtTotal.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.Red;
            this.txtTotal.Location = new System.Drawing.Point(609, 657);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(158, 37);
            this.txtTotal.TabIndex = 35;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(179, 192);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 50);
            this.btnClear.TabIndex = 37;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtTaxable
            // 
            this.txtTaxable.BackColor = System.Drawing.Color.White;
            this.txtTaxable.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxable.Location = new System.Drawing.Point(609, 587);
            this.txtTaxable.Name = "txtTaxable";
            this.txtTaxable.ReadOnly = true;
            this.txtTaxable.Size = new System.Drawing.Size(158, 33);
            this.txtTaxable.TabIndex = 33;
            this.txtTaxable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOne
            // 
            this.btnOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnOne.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOne.FlatAppearance.BorderSize = 0;
            this.btnOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOne.ForeColor = System.Drawing.Color.White;
            this.btnOne.Location = new System.Drawing.Point(15, 9);
            this.btnOne.Name = "btnOne";
            this.btnOne.Size = new System.Drawing.Size(70, 50);
            this.btnOne.TabIndex = 26;
            this.btnOne.Text = "1";
            this.btnOne.UseVisualStyleBackColor = false;
            this.btnOne.Click += new System.EventHandler(this.btnOne_Click);
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(707, 12);
            this.txtQty.Name = "txtQty";
            this.txtQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtQty.Size = new System.Drawing.Size(65, 23);
            this.txtQty.TabIndex = 26;
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            // 
            // btnDot
            // 
            this.btnDot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnDot.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDot.FlatAppearance.BorderSize = 0;
            this.btnDot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDot.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDot.ForeColor = System.Drawing.Color.White;
            this.btnDot.Location = new System.Drawing.Point(97, 192);
            this.btnDot.Name = "btnDot";
            this.btnDot.Size = new System.Drawing.Size(70, 50);
            this.btnDot.TabIndex = 36;
            this.btnDot.Text = ".";
            this.btnDot.UseVisualStyleBackColor = false;
            this.btnDot.Click += new System.EventHandler(this.btnDot_Click);
            // 
            // btnFindProduct
            // 
            this.btnFindProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnFindProduct.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnFindProduct.FlatAppearance.BorderSize = 0;
            this.btnFindProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindProduct.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFindProduct.ForeColor = System.Drawing.Color.White;
            this.btnFindProduct.Location = new System.Drawing.Point(122, 3);
            this.btnFindProduct.Name = "btnFindProduct";
            this.btnFindProduct.Size = new System.Drawing.Size(105, 70);
            this.btnFindProduct.TabIndex = 19;
            this.btnFindProduct.Text = "Find Product\r\n(F10)";
            this.btnFindProduct.UseVisualStyleBackColor = false;
            this.btnFindProduct.Click += new System.EventHandler(this.btnFindProduct_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(9, 13);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 16);
            this.label15.TabIndex = 25;
            this.label15.Text = "Scan";
            // 
            // btnTwo
            // 
            this.btnTwo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnTwo.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnTwo.FlatAppearance.BorderSize = 0;
            this.btnTwo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTwo.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTwo.ForeColor = System.Drawing.Color.White;
            this.btnTwo.Location = new System.Drawing.Point(97, 9);
            this.btnTwo.Name = "btnTwo";
            this.btnTwo.Size = new System.Drawing.Size(70, 50);
            this.btnTwo.TabIndex = 27;
            this.btnTwo.Text = "2";
            this.btnTwo.UseVisualStyleBackColor = false;
            this.btnTwo.Click += new System.EventHandler(this.btnTwo_Click);
            // 
            // btnZero
            // 
            this.btnZero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnZero.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnZero.FlatAppearance.BorderSize = 0;
            this.btnZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZero.ForeColor = System.Drawing.Color.White;
            this.btnZero.Location = new System.Drawing.Point(15, 192);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(70, 50);
            this.btnZero.TabIndex = 35;
            this.btnZero.Text = "0";
            this.btnZero.UseVisualStyleBackColor = false;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // btnThree
            // 
            this.btnThree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnThree.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnThree.FlatAppearance.BorderSize = 0;
            this.btnThree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThree.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThree.ForeColor = System.Drawing.Color.White;
            this.btnThree.Location = new System.Drawing.Point(179, 9);
            this.btnThree.Name = "btnThree";
            this.btnThree.Size = new System.Drawing.Size(70, 50);
            this.btnThree.TabIndex = 28;
            this.btnThree.Text = "3";
            this.btnThree.UseVisualStyleBackColor = false;
            this.btnThree.Click += new System.EventHandler(this.btnThree_Click);
            // 
            // btnNine
            // 
            this.btnNine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnNine.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNine.FlatAppearance.BorderSize = 0;
            this.btnNine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNine.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNine.ForeColor = System.Drawing.Color.White;
            this.btnNine.Location = new System.Drawing.Point(179, 131);
            this.btnNine.Name = "btnNine";
            this.btnNine.Size = new System.Drawing.Size(70, 50);
            this.btnNine.TabIndex = 34;
            this.btnNine.Text = "9";
            this.btnNine.UseVisualStyleBackColor = false;
            this.btnNine.Click += new System.EventHandler(this.btnNine_Click);
            // 
            // btnFour
            // 
            this.btnFour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnFour.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnFour.FlatAppearance.BorderSize = 0;
            this.btnFour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFour.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFour.ForeColor = System.Drawing.Color.White;
            this.btnFour.Location = new System.Drawing.Point(15, 70);
            this.btnFour.Name = "btnFour";
            this.btnFour.Size = new System.Drawing.Size(70, 50);
            this.btnFour.TabIndex = 29;
            this.btnFour.Text = "4";
            this.btnFour.UseVisualStyleBackColor = false;
            this.btnFour.Click += new System.EventHandler(this.btnFour_Click);
            // 
            // btnEight
            // 
            this.btnEight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnEight.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnEight.FlatAppearance.BorderSize = 0;
            this.btnEight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEight.ForeColor = System.Drawing.Color.White;
            this.btnEight.Location = new System.Drawing.Point(97, 131);
            this.btnEight.Name = "btnEight";
            this.btnEight.Size = new System.Drawing.Size(70, 50);
            this.btnEight.TabIndex = 33;
            this.btnEight.Text = "8";
            this.btnEight.UseVisualStyleBackColor = false;
            this.btnEight.Click += new System.EventHandler(this.btnEight_Click);
            // 
            // btnSix
            // 
            this.btnSix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnSix.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSix.FlatAppearance.BorderSize = 0;
            this.btnSix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSix.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSix.ForeColor = System.Drawing.Color.White;
            this.btnSix.Location = new System.Drawing.Point(179, 70);
            this.btnSix.Name = "btnSix";
            this.btnSix.Size = new System.Drawing.Size(70, 50);
            this.btnSix.TabIndex = 31;
            this.btnSix.Text = "6";
            this.btnSix.UseVisualStyleBackColor = false;
            this.btnSix.Click += new System.EventHandler(this.btnSix_Click);
            // 
            // btnFive
            // 
            this.btnFive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(115)))), ((int)(((byte)(178)))));
            this.btnFive.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnFive.FlatAppearance.BorderSize = 0;
            this.btnFive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFive.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFive.ForeColor = System.Drawing.Color.White;
            this.btnFive.Location = new System.Drawing.Point(97, 70);
            this.btnFive.Name = "btnFive";
            this.btnFive.Size = new System.Drawing.Size(70, 50);
            this.btnFive.TabIndex = 30;
            this.btnFive.Text = "5";
            this.btnFive.UseVisualStyleBackColor = false;
            this.btnFive.Click += new System.EventHandler(this.btnFive_Click);
            // 
            // txtTaxAmt
            // 
            this.txtTaxAmt.BackColor = System.Drawing.Color.White;
            this.txtTaxAmt.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxAmt.Location = new System.Drawing.Point(609, 621);
            this.txtTaxAmt.Name = "txtTaxAmt";
            this.txtTaxAmt.ReadOnly = true;
            this.txtTaxAmt.Size = new System.Drawing.Size(158, 33);
            this.txtTaxAmt.TabIndex = 34;
            this.txtTaxAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.txtCustName);
            this.panelMain.Controls.Add(this.lblCustomerName);
            this.panelMain.Controls.Add(this.lblCustomerCode);
            this.panelMain.Controls.Add(this.txtCustomerId);
            this.panelMain.Controls.Add(this.btnClosePOS);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.lblTotalQty);
            this.panelMain.Controls.Add(this.label19);
            this.panelMain.Controls.Add(this.txtTotal);
            this.panelMain.Controls.Add(this.txtTaxAmt);
            this.panelMain.Controls.Add(this.txtTaxable);
            this.panelMain.Controls.Add(this.btnFindCustomer);
            this.panelMain.Controls.Add(this.txtSubTotal);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Controls.Add(this.panelBarcode);
            this.panelMain.Controls.Add(this.panelBillDetails);
            this.panelMain.Controls.Add(this.dgvProduct);
            this.panelMain.Controls.Add(this.panelMainButton);
            this.panelMain.Controls.Add(this.label16);
            this.panelMain.Controls.Add(this.label20);
            this.panelMain.Controls.Add(this.label21);
            this.panelMain.Controls.Add(this.lblLedgerId);
            this.panelMain.Location = new System.Drawing.Point(-12, 1);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1065, 707);
            this.panelMain.TabIndex = 18;
            // 
            // txtCustName
            // 
            this.txtCustName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustName.Location = new System.Drawing.Point(162, 614);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(137, 23);
            this.txtCustName.TabIndex = 49;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.ForeColor = System.Drawing.Color.Black;
            this.lblCustomerName.Location = new System.Drawing.Point(64, 619);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(82, 13);
            this.lblCustomerName.TabIndex = 48;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AutoSize = true;
            this.lblCustomerCode.ForeColor = System.Drawing.Color.Black;
            this.lblCustomerCode.Location = new System.Drawing.Point(64, 595);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(79, 13);
            this.lblCustomerCode.TabIndex = 46;
            this.lblCustomerCode.Text = "Customer Code";
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerId.Location = new System.Drawing.Point(162, 587);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.Size = new System.Drawing.Size(137, 23);
            this.txtCustomerId.TabIndex = 47;
            this.txtCustomerId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerId_KeyPress);
            // 
            // btnFindCustomer
            // 
            this.btnFindCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(161)))), ((int)(((byte)(45)))));
            this.btnFindCustomer.FlatAppearance.BorderSize = 0;
            this.btnFindCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindCustomer.Location = new System.Drawing.Point(15, 594);
            this.btnFindCustomer.Name = "btnFindCustomer";
            this.btnFindCustomer.Size = new System.Drawing.Size(41, 40);
            this.btnFindCustomer.TabIndex = 16;
            this.btnFindCustomer.Text = "Find Customer";
            this.btnFindCustomer.UseVisualStyleBackColor = false;
            this.btnFindCustomer.Click += new System.EventHandler(this.btnFindCustomer_Click);
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.BackColor = System.Drawing.Color.White;
            this.txtSubTotal.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubTotal.Location = new System.Drawing.Point(431, 587);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.ReadOnly = true;
            this.txtSubTotal.Size = new System.Drawing.Size(114, 33);
            this.txtSubTotal.TabIndex = 27;
            this.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnPriceChange);
            this.panel1.Controls.Add(this.btnBarcode);
            this.panel1.Controls.Add(this.btnEnter);
            this.panel1.Controls.Add(this.btnQty);
            this.panel1.Controls.Add(this.btnSeven);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnOne);
            this.panel1.Controls.Add(this.btnDot);
            this.panel1.Controls.Add(this.btnTwo);
            this.panel1.Controls.Add(this.btnZero);
            this.panel1.Controls.Add(this.btnThree);
            this.panel1.Controls.Add(this.btnNine);
            this.panel1.Controls.Add(this.btnFour);
            this.panel1.Controls.Add(this.btnEight);
            this.panel1.Controls.Add(this.btnFive);
            this.panel1.Controls.Add(this.btnSix);
            this.panel1.Location = new System.Drawing.Point(784, 102);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 605);
            this.panel1.TabIndex = 26;
            // 
            // panelBarcode
            // 
            this.panelBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(244)))), ((int)(((byte)(233)))));
            this.panelBarcode.Controls.Add(this.lblBarcodeScanningType);
            this.panelBarcode.Controls.Add(this.txtQty);
            this.panelBarcode.Controls.Add(this.label15);
            this.panelBarcode.Controls.Add(this.txtBarcode);
            this.panelBarcode.Location = new System.Drawing.Point(9, 102);
            this.panelBarcode.Name = "panelBarcode";
            this.panelBarcode.Size = new System.Drawing.Size(776, 40);
            this.panelBarcode.TabIndex = 24;
            // 
            // lblBarcodeScanningType
            // 
            this.lblBarcodeScanningType.AutoSize = true;
            this.lblBarcodeScanningType.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcodeScanningType.ForeColor = System.Drawing.Color.Red;
            this.lblBarcodeScanningType.Location = new System.Drawing.Point(261, 12);
            this.lblBarcodeScanningType.Name = "lblBarcodeScanningType";
            this.lblBarcodeScanningType.Size = new System.Drawing.Size(54, 18);
            this.lblBarcodeScanningType.TabIndex = 28;
            this.lblBarcodeScanningType.Text = "Bill No";
            this.lblBarcodeScanningType.Visible = false;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(58, 8);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(187, 27);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            this.txtBarcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarcode_KeyPress);
            // 
            // panelBillDetails
            // 
            this.panelBillDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(42)))));
            this.panelBillDetails.Controls.Add(this.lblBillNo);
            this.panelBillDetails.Controls.Add(this.label3);
            this.panelBillDetails.Controls.Add(this.lblBillDate);
            this.panelBillDetails.Controls.Add(this.lblCounter);
            this.panelBillDetails.Controls.Add(this.label2);
            this.panelBillDetails.Controls.Add(this.label6);
            this.panelBillDetails.Controls.Add(this.lblUser);
            this.panelBillDetails.Controls.Add(this.lblSessionNO);
            this.panelBillDetails.Controls.Add(this.lblBillTime);
            this.panelBillDetails.Controls.Add(this.label10);
            this.panelBillDetails.Controls.Add(this.label9);
            this.panelBillDetails.Controls.Add(this.lblSessionDate);
            this.panelBillDetails.Location = new System.Drawing.Point(7, 0);
            this.panelBillDetails.Name = "panelBillDetails";
            this.panelBillDetails.Size = new System.Drawing.Size(1040, 26);
            this.panelBillDetails.TabIndex = 17;
            // 
            // lblBillNo
            // 
            this.lblBillNo.AutoSize = true;
            this.lblBillNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillNo.ForeColor = System.Drawing.Color.White;
            this.lblBillNo.Location = new System.Drawing.Point(9, 5);
            this.lblBillNo.Name = "lblBillNo";
            this.lblBillNo.Size = new System.Drawing.Size(108, 16);
            this.lblBillNo.TabIndex = 14;
            this.lblBillNo.Text = "C1202300001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(159, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Bill Date :";
            // 
            // lblBillDate
            // 
            this.lblBillDate.AutoSize = true;
            this.lblBillDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblBillDate.ForeColor = System.Drawing.Color.White;
            this.lblBillDate.Location = new System.Drawing.Point(218, 5);
            this.lblBillDate.Name = "lblBillDate";
            this.lblBillDate.Size = new System.Drawing.Size(88, 16);
            this.lblBillDate.TabIndex = 6;
            this.lblBillDate.Text = "31/May/2023";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblCounter.ForeColor = System.Drawing.Color.White;
            this.lblCounter.Location = new System.Drawing.Point(961, 5);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(64, 16);
            this.lblCounter.TabIndex = 9;
            this.lblCounter.Text = "Counter 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(808, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "User :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(896, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Counter :";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(851, 5);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(46, 16);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Admin";
            // 
            // lblSessionNO
            // 
            this.lblSessionNO.AutoSize = true;
            this.lblSessionNO.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblSessionNO.ForeColor = System.Drawing.Color.White;
            this.lblSessionNO.Location = new System.Drawing.Point(784, 5);
            this.lblSessionNO.Name = "lblSessionNO";
            this.lblSessionNO.Size = new System.Drawing.Size(15, 16);
            this.lblSessionNO.TabIndex = 13;
            this.lblSessionNO.Text = "1";
            // 
            // lblBillTime
            // 
            this.lblBillTime.AutoSize = true;
            this.lblBillTime.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblBillTime.ForeColor = System.Drawing.Color.White;
            this.lblBillTime.Location = new System.Drawing.Point(306, 5);
            this.lblBillTime.Name = "lblBillTime";
            this.lblBillTime.Size = new System.Drawing.Size(62, 16);
            this.lblBillTime.TabIndex = 7;
            this.lblBillTime.Text = "10:00 AM";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(703, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Session No :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(507, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Session Date :";
            // 
            // lblSessionDate
            // 
            this.lblSessionDate.AutoSize = true;
            this.lblSessionDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold);
            this.lblSessionDate.ForeColor = System.Drawing.Color.White;
            this.lblSessionDate.Location = new System.Drawing.Point(595, 5);
            this.lblSessionDate.Name = "lblSessionDate";
            this.lblSessionDate.Size = new System.Drawing.Size(88, 16);
            this.lblSessionDate.TabIndex = 11;
            this.lblSessionDate.Text = "31/May/2023";
            // 
            // dgvProduct
            // 
            this.dgvProduct.AllowUserToResizeColumns = false;
            this.dgvProduct.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgvProduct.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProduct.BackgroundColor = System.Drawing.Color.White;
            this.dgvProduct.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SLNo,
            this.ProductCode,
            this.Barcode,
            this.ItemName,
            this.Qty,
            this.UnitId,
            this.Unit,
            this.BaseUnitId,
            this.UnitConversion,
            this.SalesRate,
            this.ExcludeRate,
            this.PurchaseRate,
            this.GrossValue,
            this.DiscAmt,
            this.NetValue,
            this.TaxId,
            this.TaxPerc,
            this.TaxAmt,
            this.Total,
            this.BillDiscIndProductAmt,
            this.amountBeforeDisc,
            this.rateDiscAmount,
            this.offerId});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.SandyBrown;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProduct.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvProduct.EnableHeadersVisualStyles = false;
            this.dgvProduct.Location = new System.Drawing.Point(11, 142);
            this.dgvProduct.MultiSelect = false;
            this.dgvProduct.Name = "dgvProduct";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduct.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvProduct.RowHeadersVisible = false;
            this.dgvProduct.Size = new System.Drawing.Size(774, 424);
            this.dgvProduct.TabIndex = 4;
            this.dgvProduct.TabStop = false;
            this.dgvProduct.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProduct_CellEndEdit);
            this.dgvProduct.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvProduct_EditingControlShowing);
            this.dgvProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvProduct_KeyDown);
            // 
            // SLNo
            // 
            this.SLNo.HeaderText = "SN";
            this.SLNo.Name = "SLNo";
            this.SLNo.ReadOnly = true;
            this.SLNo.Width = 30;
            // 
            // ProductCode
            // 
            this.ProductCode.HeaderText = "ProductCode";
            this.ProductCode.Name = "ProductCode";
            this.ProductCode.Visible = false;
            // 
            // Barcode
            // 
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            this.Barcode.Width = 70;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 210;
            // 
            // Qty
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle3;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.Width = 50;
            // 
            // UnitId
            // 
            this.UnitId.HeaderText = "UnitId";
            this.UnitId.Name = "UnitId";
            this.UnitId.Visible = false;
            // 
            // Unit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle4;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 40;
            // 
            // BaseUnitId
            // 
            this.BaseUnitId.HeaderText = "BaseUnitId";
            this.BaseUnitId.Name = "BaseUnitId";
            this.BaseUnitId.Visible = false;
            // 
            // UnitConversion
            // 
            this.UnitConversion.HeaderText = "UnitConversion";
            this.UnitConversion.Name = "UnitConversion";
            this.UnitConversion.Visible = false;
            // 
            // SalesRate
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SalesRate.DefaultCellStyle = dataGridViewCellStyle5;
            this.SalesRate.HeaderText = "Rate";
            this.SalesRate.Name = "SalesRate";
            this.SalesRate.Width = 60;
            // 
            // ExcludeRate
            // 
            this.ExcludeRate.HeaderText = "ExcludeRate";
            this.ExcludeRate.Name = "ExcludeRate";
            this.ExcludeRate.Visible = false;
            // 
            // PurchaseRate
            // 
            this.PurchaseRate.HeaderText = "PurchaseRate";
            this.PurchaseRate.Name = "PurchaseRate";
            this.PurchaseRate.Visible = false;
            // 
            // GrossValue
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GrossValue.DefaultCellStyle = dataGridViewCellStyle6;
            this.GrossValue.HeaderText = "G.V";
            this.GrossValue.Name = "GrossValue";
            this.GrossValue.ReadOnly = true;
            this.GrossValue.Width = 50;
            // 
            // DiscAmt
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscAmt.DefaultCellStyle = dataGridViewCellStyle7;
            this.DiscAmt.HeaderText = "Disc";
            this.DiscAmt.Name = "DiscAmt";
            this.DiscAmt.Visible = false;
            this.DiscAmt.Width = 40;
            // 
            // NetValue
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.NetValue.DefaultCellStyle = dataGridViewCellStyle8;
            this.NetValue.HeaderText = "NetValue";
            this.NetValue.Name = "NetValue";
            this.NetValue.ReadOnly = true;
            this.NetValue.Width = 70;
            // 
            // TaxId
            // 
            this.TaxId.HeaderText = "TaxId";
            this.TaxId.Name = "TaxId";
            this.TaxId.Visible = false;
            // 
            // TaxPerc
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TaxPerc.DefaultCellStyle = dataGridViewCellStyle9;
            this.TaxPerc.HeaderText = "Tax%";
            this.TaxPerc.Name = "TaxPerc";
            this.TaxPerc.ReadOnly = true;
            this.TaxPerc.Width = 45;
            // 
            // TaxAmt
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TaxAmt.DefaultCellStyle = dataGridViewCellStyle10;
            this.TaxAmt.HeaderText = "TaxAmt";
            this.TaxAmt.Name = "TaxAmt";
            this.TaxAmt.ReadOnly = true;
            this.TaxAmt.Width = 55;
            // 
            // Total
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Total.DefaultCellStyle = dataGridViewCellStyle11;
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Width = 60;
            // 
            // BillDiscIndProductAmt
            // 
            this.BillDiscIndProductAmt.HeaderText = "BillDiscIndProductAmt";
            this.BillDiscIndProductAmt.Name = "BillDiscIndProductAmt";
            this.BillDiscIndProductAmt.Visible = false;
            // 
            // amountBeforeDisc
            // 
            this.amountBeforeDisc.HeaderText = "AmountBeforeDisc";
            this.amountBeforeDisc.Name = "amountBeforeDisc";
            this.amountBeforeDisc.Visible = false;
            // 
            // rateDiscAmount
            // 
            this.rateDiscAmount.HeaderText = "RateDiscAmount";
            this.rateDiscAmount.Name = "rateDiscAmount";
            this.rateDiscAmount.Visible = false;
            // 
            // offerId
            // 
            this.offerId.HeaderText = "offerId";
            this.offerId.Name = "offerId";
            this.offerId.Visible = false;
            // 
            // panelMainButton
            // 
            this.panelMainButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(71)))), ((int)(((byte)(83)))));
            this.panelMainButton.Controls.Add(this.btnReturnFullBill);
            this.panelMainButton.Controls.Add(this.groupBox1);
            this.panelMainButton.Controls.Add(this.btnFindProduct);
            this.panelMainButton.Controls.Add(this.btnNewSale);
            this.panelMainButton.Location = new System.Drawing.Point(7, 24);
            this.panelMainButton.Name = "panelMainButton";
            this.panelMainButton.Size = new System.Drawing.Size(1032, 78);
            this.panelMainButton.TabIndex = 18;
            // 
            // btnReturnFullBill
            // 
            this.btnReturnFullBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnReturnFullBill.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnReturnFullBill.FlatAppearance.BorderSize = 0;
            this.btnReturnFullBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnFullBill.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnFullBill.ForeColor = System.Drawing.Color.White;
            this.btnReturnFullBill.Location = new System.Drawing.Point(233, 3);
            this.btnReturnFullBill.Name = "btnReturnFullBill";
            this.btnReturnFullBill.Size = new System.Drawing.Size(105, 70);
            this.btnReturnFullBill.TabIndex = 21;
            this.btnReturnFullBill.Text = "Return Full Bill";
            this.btnReturnFullBill.UseVisualStyleBackColor = false;
            this.btnReturnFullBill.Click += new System.EventHandler(this.btnReturnFullBill_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtCredit);
            this.groupBox1.Controls.Add(this.rbtCash);
            this.groupBox1.Controls.Add(this.rbtCreditNote);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox1.Location = new System.Drawing.Point(670, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 53);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Return Method";
            // 
            // rbtCredit
            // 
            this.rbtCredit.AutoSize = true;
            this.rbtCredit.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCredit.ForeColor = System.Drawing.Color.White;
            this.rbtCredit.Location = new System.Drawing.Point(244, 23);
            this.rbtCredit.Name = "rbtCredit";
            this.rbtCredit.Size = new System.Drawing.Size(71, 22);
            this.rbtCredit.TabIndex = 2;
            this.rbtCredit.TabStop = true;
            this.rbtCredit.Text = "Credit";
            this.rbtCredit.UseVisualStyleBackColor = true;
            this.rbtCredit.CheckedChanged += new System.EventHandler(this.rbtCredit_CheckedChanged);
            // 
            // rbtCash
            // 
            this.rbtCash.AutoSize = true;
            this.rbtCash.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCash.ForeColor = System.Drawing.Color.White;
            this.rbtCash.Location = new System.Drawing.Point(152, 23);
            this.rbtCash.Name = "rbtCash";
            this.rbtCash.Size = new System.Drawing.Size(63, 22);
            this.rbtCash.TabIndex = 1;
            this.rbtCash.TabStop = true;
            this.rbtCash.Text = "Cash";
            this.rbtCash.UseVisualStyleBackColor = true;
            this.rbtCash.CheckedChanged += new System.EventHandler(this.rbtCash_CheckedChanged);
            // 
            // rbtCreditNote
            // 
            this.rbtCreditNote.AutoSize = true;
            this.rbtCreditNote.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCreditNote.ForeColor = System.Drawing.Color.White;
            this.rbtCreditNote.Location = new System.Drawing.Point(13, 23);
            this.rbtCreditNote.Name = "rbtCreditNote";
            this.rbtCreditNote.Size = new System.Drawing.Size(110, 22);
            this.rbtCreditNote.TabIndex = 0;
            this.rbtCreditNote.TabStop = true;
            this.rbtCreditNote.Text = "Credit Note";
            this.rbtCreditNote.UseVisualStyleBackColor = true;
            this.rbtCreditNote.CheckedChanged += new System.EventHandler(this.rbtCreditNote_CheckedChanged);
            // 
            // btnNewSale
            // 
            this.btnNewSale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btnNewSale.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnNewSale.FlatAppearance.BorderSize = 0;
            this.btnNewSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSale.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewSale.ForeColor = System.Drawing.Color.White;
            this.btnNewSale.Location = new System.Drawing.Point(11, 3);
            this.btnNewSale.Name = "btnNewSale";
            this.btnNewSale.Size = new System.Drawing.Size(105, 70);
            this.btnNewSale.TabIndex = 15;
            this.btnNewSale.Text = "New Refund\r\n(F2)";
            this.btnNewSale.UseVisualStyleBackColor = false;
            this.btnNewSale.Click += new System.EventHandler(this.btnNewSale_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(360, 596);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 18);
            this.label16.TabIndex = 30;
            this.label16.Text = "Sub Total";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(547, 628);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 18);
            this.label20.TabIndex = 37;
            this.label20.Text = "Tax Amt";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(547, 595);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(66, 18);
            this.label21.TabIndex = 36;
            this.label21.Text = "Taxable";
            // 
            // lblLedgerId
            // 
            this.lblLedgerId.AutoSize = true;
            this.lblLedgerId.ForeColor = System.Drawing.Color.Black;
            this.lblLedgerId.Location = new System.Drawing.Point(186, 624);
            this.lblLedgerId.Name = "lblLedgerId";
            this.lblLedgerId.Size = new System.Drawing.Size(0, 13);
            this.lblLedgerId.TabIndex = 50;
            // 
            // printDocumentCreditNote
            // 
            this.printDocumentCreditNote.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentCreditNote_PrintPage);
            // 
            // printDocumentThermal3
            // 
            this.printDocumentThermal3.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentThermal3_PrintPage);
            // 
            // frmPOSSalesReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 707);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmPOSSalesReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Return";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPOSSalesReturn_FormClosing);
            this.Load += new System.EventHandler(this.frmPOSSalesReturn_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panelBarcode.ResumeLayout(false);
            this.panelBarcode.PerformLayout();
            this.panelBillDetails.ResumeLayout(false);
            this.panelBillDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduct)).EndInit();
            this.panelMainButton.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnPriceChange;
        private System.Windows.Forms.Button btnBarcode;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Button btnQty;
        private System.Windows.Forms.Button btnSeven;
        private System.Windows.Forms.Button btnClosePOS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalQty;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtTaxable;
        private System.Windows.Forms.Button btnOne;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Button btnDot;
        private System.Windows.Forms.Button btnFindProduct;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnTwo;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnThree;
        private System.Windows.Forms.Button btnNine;
        private System.Windows.Forms.Button btnFour;
        private System.Windows.Forms.Button btnEight;
        private System.Windows.Forms.Button btnSix;
        private System.Windows.Forms.Button btnFive;
        private System.Windows.Forms.TextBox txtTaxAmt;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnFindCustomer;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Panel panelBillDetails;
        private System.Windows.Forms.Label lblBillNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBillDate;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblSessionNO;
        private System.Windows.Forms.Label lblBillTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblSessionDate;
        private System.Windows.Forms.DataGridView dgvProduct;
        private System.Windows.Forms.Panel panelMainButton;
        private System.Windows.Forms.Button btnNewSale;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtCredit;
        private System.Windows.Forms.RadioButton rbtCash;
        private System.Windows.Forms.RadioButton rbtCreditNote;
        private System.Drawing.Printing.PrintDocument printDocumentCreditNote;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.Label lblLedgerId;
        private System.Drawing.Printing.PrintDocument printDocumentThermal3;
        private System.Windows.Forms.Button btnReturnFullBill;
        private System.Windows.Forms.Label lblBarcodeScanningType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaseUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitConversion;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExcludeRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GrossValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn NetValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxId;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillDiscIndProductAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountBeforeDisc;
        private System.Windows.Forms.DataGridViewTextBoxColumn rateDiscAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn offerId;
    }
}