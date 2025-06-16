namespace FinacPOS
{
    partial class frmPOSReceiptVoucher
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtNarration = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblNarration = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblCashOrBank = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.lblSessionDt = new System.Windows.Forms.Label();
            this.lblSessionDate = new System.Windows.Forms.Label();
            this.cmbCashOrBank = new System.Windows.Forms.ComboBox();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.btnFindCustomer = new System.Windows.Forms.Button();
            this.lblLedgerId = new System.Windows.Forms.Label();
            this.dgvPartyBalance = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.voucherType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MasterId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.voucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferanceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountToPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewButtonColumn();
            this.isSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currency = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.crOrDr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InterestParameter = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IsFromDb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblChequeDate = new System.Windows.Forms.Label();
            this.lblChequeNO = new System.Windows.Forms.Label();
            this.txtChequeNo = new System.Windows.Forms.TextBox();
            this.timerSessionDate = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtChequeDate = new System.Windows.Forms.DateTimePicker();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnLoadPending = new System.Windows.Forms.Button();
            this.lnklblRemove = new System.Windows.Forms.LinkLabel();
            this.btnClear = new System.Windows.Forms.Button();
            this.RadioBtnCash = new System.Windows.Forms.RadioButton();
            this.RadioBtnUPI = new System.Windows.Forms.RadioButton();
            this.RadioBtnCreditCard = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabelRePrint = new System.Windows.Forms.LinkLabel();
            this.panelBillDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyBalance)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNarration
            // 
            this.txtNarration.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNarration.Location = new System.Drawing.Point(658, 99);
            this.txtNarration.Multiline = true;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Size = new System.Drawing.Size(149, 49);
            this.txtNarration.TabIndex = 7;
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.Wheat;
            this.txtTotalAmount.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(658, 63);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(149, 33);
            this.txtTotalAmount.TabIndex = 6;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmount.Enter += new System.EventHandler(this.txtCash_Enter);
            this.txtTotalAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalAmount_KeyDown);
            this.txtTotalAmount.Leave += new System.EventHandler(this.txtCash_Leave);
            // 
            // lblNarration
            // 
            this.lblNarration.AutoSize = true;
            this.lblNarration.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNarration.Location = new System.Drawing.Point(560, 100);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Size = new System.Drawing.Size(69, 16);
            this.lblNarration.TabIndex = 55;
            this.lblNarration.Text = "Narration";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(560, 66);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(57, 16);
            this.lblTotalAmount.TabIndex = 56;
            this.lblTotalAmount.Text = "Amount";
            // 
            // lblCashOrBank
            // 
            this.lblCashOrBank.AutoSize = true;
            this.lblCashOrBank.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCashOrBank.Location = new System.Drawing.Point(407, 248);
            this.lblCashOrBank.Name = "lblCashOrBank";
            this.lblCashOrBank.Size = new System.Drawing.Size(97, 16);
            this.lblCashOrBank.TabIndex = 58;
            this.lblCashOrBank.Text = "Cash Or Bank";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.OrangeRed;
            this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(817, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 80);
            this.btnSave.TabIndex = 62;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(817, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 80);
            this.btnCancel.TabIndex = 63;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.panelBillDetails.Controls.Add(this.lblSessionDt);
            this.panelBillDetails.Controls.Add(this.lblSessionDate);
            this.panelBillDetails.Location = new System.Drawing.Point(2, 0);
            this.panelBillDetails.Name = "panelBillDetails";
            this.panelBillDetails.Size = new System.Drawing.Size(1091, 30);
            this.panelBillDetails.TabIndex = 65;
            // 
            // lblBillNo
            // 
            this.lblBillNo.AutoSize = true;
            this.lblBillNo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillNo.ForeColor = System.Drawing.Color.White;
            this.lblBillNo.Location = new System.Drawing.Point(4, 6);
            this.lblBillNo.Name = "lblBillNo";
            this.lblBillNo.Size = new System.Drawing.Size(111, 19);
            this.lblBillNo.TabIndex = 15;
            this.lblBillNo.Text = "C1202300001";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(139, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Bill Date :";
            // 
            // lblBillDate
            // 
            this.lblBillDate.AutoSize = true;
            this.lblBillDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillDate.ForeColor = System.Drawing.Color.White;
            this.lblBillDate.Location = new System.Drawing.Point(200, 9);
            this.lblBillDate.Name = "lblBillDate";
            this.lblBillDate.Size = new System.Drawing.Size(87, 16);
            this.lblBillDate.TabIndex = 6;
            this.lblBillDate.Text = "31/May/2023";
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounter.ForeColor = System.Drawing.Color.White;
            this.lblCounter.Location = new System.Drawing.Point(853, 6);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(63, 16);
            this.lblCounter.TabIndex = 9;
            this.lblCounter.Text = "Counter 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(673, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "User :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(776, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Counter :";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(713, 6);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(45, 16);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Admin";
            // 
            // lblSessionNO
            // 
            this.lblSessionNO.AutoSize = true;
            this.lblSessionNO.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionNO.ForeColor = System.Drawing.Color.White;
            this.lblSessionNO.Location = new System.Drawing.Point(635, 6);
            this.lblSessionNO.Name = "lblSessionNO";
            this.lblSessionNO.Size = new System.Drawing.Size(14, 16);
            this.lblSessionNO.TabIndex = 13;
            this.lblSessionNO.Text = "1";
            // 
            // lblBillTime
            // 
            this.lblBillTime.AutoSize = true;
            this.lblBillTime.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillTime.ForeColor = System.Drawing.Color.White;
            this.lblBillTime.Location = new System.Drawing.Point(287, 9);
            this.lblBillTime.Name = "lblBillTime";
            this.lblBillTime.Size = new System.Drawing.Size(61, 16);
            this.lblBillTime.TabIndex = 7;
            this.lblBillTime.Text = "10:00 AM";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(559, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Session No :";
            // 
            // lblSessionDt
            // 
            this.lblSessionDt.AutoSize = true;
            this.lblSessionDt.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionDt.ForeColor = System.Drawing.Color.White;
            this.lblSessionDt.Location = new System.Drawing.Point(371, 9);
            this.lblSessionDt.Name = "lblSessionDt";
            this.lblSessionDt.Size = new System.Drawing.Size(85, 16);
            this.lblSessionDt.TabIndex = 10;
            this.lblSessionDt.Text = "Session Date :";
            // 
            // lblSessionDate
            // 
            this.lblSessionDate.AutoSize = true;
            this.lblSessionDate.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionDate.ForeColor = System.Drawing.Color.White;
            this.lblSessionDate.Location = new System.Drawing.Point(458, 9);
            this.lblSessionDate.Name = "lblSessionDate";
            this.lblSessionDate.Size = new System.Drawing.Size(87, 16);
            this.lblSessionDate.TabIndex = 11;
            this.lblSessionDate.Text = "31/May/2023";
            // 
            // cmbCashOrBank
            // 
            this.cmbCashOrBank.FormattingEnabled = true;
            this.cmbCashOrBank.Location = new System.Drawing.Point(537, 233);
            this.cmbCashOrBank.Name = "cmbCashOrBank";
            this.cmbCashOrBank.Size = new System.Drawing.Size(149, 21);
            this.cmbCashOrBank.TabIndex = 5;
            this.cmbCashOrBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCashOrBank_KeyDown);
            // 
            // txtCustName
            // 
            this.txtCustName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustName.Location = new System.Drawing.Point(130, 63);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(137, 23);
            this.txtCustName.TabIndex = 1;
            this.txtCustName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustName_KeyDown);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.Black;
            this.lblCustomerName.Location = new System.Drawing.Point(12, 68);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(111, 16);
            this.lblCustomerName.TabIndex = 72;
            this.lblCustomerName.Text = "Customer Name";
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AutoSize = true;
            this.lblCustomerCode.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCustomerCode.ForeColor = System.Drawing.Color.Black;
            this.lblCustomerCode.Location = new System.Drawing.Point(12, 41);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(107, 16);
            this.lblCustomerCode.TabIndex = 69;
            this.lblCustomerCode.Text = "Customer Code";
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerId.Location = new System.Drawing.Point(130, 36);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.Size = new System.Drawing.Size(137, 23);
            this.txtCustomerId.TabIndex = 0;
            this.txtCustomerId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerId_KeyDown);
            // 
            // btnFindCustomer
            // 
            this.btnFindCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(161)))), ((int)(((byte)(45)))));
            this.btnFindCustomer.FlatAppearance.BorderSize = 0;
            this.btnFindCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFindCustomer.Location = new System.Drawing.Point(273, 38);
            this.btnFindCustomer.Name = "btnFindCustomer";
            this.btnFindCustomer.Size = new System.Drawing.Size(41, 40);
            this.btnFindCustomer.TabIndex = 70;
            this.btnFindCustomer.Text = "Find Customer";
            this.btnFindCustomer.UseVisualStyleBackColor = false;
            this.btnFindCustomer.Click += new System.EventHandler(this.btnFindCustomer_Click);
            // 
            // lblLedgerId
            // 
            this.lblLedgerId.AutoSize = true;
            this.lblLedgerId.ForeColor = System.Drawing.Color.Black;
            this.lblLedgerId.Location = new System.Drawing.Point(550, 106);
            this.lblLedgerId.Name = "lblLedgerId";
            this.lblLedgerId.Size = new System.Drawing.Size(0, 13);
            this.lblLedgerId.TabIndex = 75;
            this.lblLedgerId.Visible = false;
            // 
            // dgvPartyBalance
            // 
            this.dgvPartyBalance.AllowUserToDeleteRows = false;
            this.dgvPartyBalance.AllowUserToResizeColumns = false;
            this.dgvPartyBalance.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgvPartyBalance.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPartyBalance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPartyBalance.BackgroundColor = System.Drawing.Color.White;
            this.dgvPartyBalance.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPartyBalance.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPartyBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartyBalance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check,
            this.type,
            this.voucherType,
            this.MasterId,
            this.voucherNo,
            this.VoucherDate,
            this.ReferanceNo,
            this.BillAmount,
            this.amountToPay,
            this.Select,
            this.isSelect,
            this.amount,
            this.currency,
            this.crOrDr,
            this.InterestParameter,
            this.IsFromDb});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SandyBrown;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPartyBalance.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPartyBalance.EnableHeadersVisualStyles = false;
            this.dgvPartyBalance.Location = new System.Drawing.Point(10, 152);
            this.dgvPartyBalance.MultiSelect = false;
            this.dgvPartyBalance.Name = "dgvPartyBalance";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPartyBalance.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPartyBalance.RowHeadersVisible = false;
            this.dgvPartyBalance.Size = new System.Drawing.Size(799, 299);
            this.dgvPartyBalance.TabIndex = 74;
            this.dgvPartyBalance.TabStop = false;
            this.dgvPartyBalance.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartyBalance_CellContentClick);
            this.dgvPartyBalance.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPartyBalance_CellValueChanged);
            // 
            // check
            // 
            this.check.FillWeight = 1F;
            this.check.HeaderText = "";
            this.check.Name = "check";
            // 
            // type
            // 
            this.type.FillWeight = 2F;
            this.type.HeaderText = "Reference";
            this.type.Name = "type";
            this.type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // voucherType
            // 
            this.voucherType.FillWeight = 2F;
            this.voucherType.HeaderText = "voucherType";
            this.voucherType.Name = "voucherType";
            this.voucherType.Visible = false;
            // 
            // MasterId
            // 
            this.MasterId.FillWeight = 3F;
            this.MasterId.HeaderText = "MasterId";
            this.MasterId.Name = "MasterId";
            this.MasterId.Visible = false;
            // 
            // voucherNo
            // 
            this.voucherNo.FillWeight = 2F;
            this.voucherNo.HeaderText = "voucherNo";
            this.voucherNo.Name = "voucherNo";
            // 
            // VoucherDate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VoucherDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.VoucherDate.FillWeight = 2F;
            this.VoucherDate.HeaderText = "Date";
            this.VoucherDate.Name = "VoucherDate";
            // 
            // ReferanceNo
            // 
            this.ReferanceNo.FillWeight = 2F;
            this.ReferanceNo.HeaderText = "Referance No";
            this.ReferanceNo.Name = "ReferanceNo";
            // 
            // BillAmount
            // 
            this.BillAmount.FillWeight = 2F;
            this.BillAmount.HeaderText = "BillAmount";
            this.BillAmount.Name = "BillAmount";
            // 
            // amountToPay
            // 
            this.amountToPay.FillWeight = 2F;
            this.amountToPay.HeaderText = "Pending";
            this.amountToPay.Name = "amountToPay";
            // 
            // Select
            // 
            this.Select.FillWeight = 2F;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Text = "Select";
            // 
            // isSelect
            // 
            this.isSelect.FillWeight = 2F;
            this.isSelect.HeaderText = "";
            this.isSelect.Name = "isSelect";
            this.isSelect.ReadOnly = true;
            // 
            // amount
            // 
            this.amount.FillWeight = 2F;
            this.amount.HeaderText = "Amount";
            this.amount.Name = "amount";
            this.amount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // currency
            // 
            this.currency.FillWeight = 2F;
            this.currency.HeaderText = "Currency";
            this.currency.Name = "currency";
            // 
            // crOrDr
            // 
            this.crOrDr.FillWeight = 2F;
            this.crOrDr.HeaderText = "Cr/Dr";
            this.crOrDr.Name = "crOrDr";
            // 
            // InterestParameter
            // 
            this.InterestParameter.FillWeight = 2F;
            this.InterestParameter.HeaderText = "InterestParameter";
            this.InterestParameter.Name = "InterestParameter";
            this.InterestParameter.Visible = false;
            // 
            // IsFromDb
            // 
            this.IsFromDb.FillWeight = 1F;
            this.IsFromDb.HeaderText = "IsFromDb";
            this.IsFromDb.Name = "IsFromDb";
            this.IsFromDb.Visible = false;
            // 
            // lblChequeDate
            // 
            this.lblChequeDate.AutoSize = true;
            this.lblChequeDate.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblChequeDate.ForeColor = System.Drawing.Color.Black;
            this.lblChequeDate.Location = new System.Drawing.Point(318, 68);
            this.lblChequeDate.Name = "lblChequeDate";
            this.lblChequeDate.Size = new System.Drawing.Size(92, 16);
            this.lblChequeDate.TabIndex = 78;
            this.lblChequeDate.Text = "Cheque Date";
            // 
            // lblChequeNO
            // 
            this.lblChequeNO.AutoSize = true;
            this.lblChequeNO.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblChequeNO.ForeColor = System.Drawing.Color.Black;
            this.lblChequeNO.Location = new System.Drawing.Point(318, 41);
            this.lblChequeNO.Name = "lblChequeNO";
            this.lblChequeNO.Size = new System.Drawing.Size(80, 16);
            this.lblChequeNO.TabIndex = 76;
            this.lblChequeNO.Text = "Cheque No";
            // 
            // txtChequeNo
            // 
            this.txtChequeNo.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChequeNo.Location = new System.Drawing.Point(410, 36);
            this.txtChequeNo.Name = "txtChequeNo";
            this.txtChequeNo.Size = new System.Drawing.Size(137, 23);
            this.txtChequeNo.TabIndex = 2;
            this.txtChequeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeNo_KeyDown);
            // 
            // timerSessionDate
            // 
            this.timerSessionDate.Interval = 500;
            this.timerSessionDate.Tick += new System.EventHandler(this.timerSessionDate_Tick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(140)))), ((int)(((byte)(68)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(817, 122);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 85);
            this.btnSearch.TabIndex = 80;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtChequeDate
            // 
            this.txtChequeDate.Location = new System.Drawing.Point(410, 65);
            this.txtChequeDate.Name = "txtChequeDate";
            this.txtChequeDate.Size = new System.Drawing.Size(137, 20);
            this.txtChequeDate.TabIndex = 3;
            this.txtChequeDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChequeDate_KeyDown);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(123)))), ((int)(((byte)(149)))));
            this.btnDelete.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(817, 214);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 80);
            this.btnDelete.TabIndex = 82;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnLoadPending
            // 
            this.btnLoadPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(123)))), ((int)(((byte)(149)))));
            this.btnLoadPending.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnLoadPending.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnLoadPending.FlatAppearance.BorderSize = 0;
            this.btnLoadPending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadPending.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadPending.ForeColor = System.Drawing.Color.White;
            this.btnLoadPending.Location = new System.Drawing.Point(15, 98);
            this.btnLoadPending.Name = "btnLoadPending";
            this.btnLoadPending.Size = new System.Drawing.Size(170, 39);
            this.btnLoadPending.TabIndex = 84;
            this.btnLoadPending.Text = "Load Pending Docs";
            this.btnLoadPending.UseVisualStyleBackColor = false;
            this.btnLoadPending.Click += new System.EventHandler(this.btnLoadPending_Click);
            // 
            // lnklblRemove
            // 
            this.lnklblRemove.AutoSize = true;
            this.lnklblRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnklblRemove.Location = new System.Drawing.Point(746, 454);
            this.lnklblRemove.Name = "lnklblRemove";
            this.lnklblRemove.Size = new System.Drawing.Size(64, 18);
            this.lnklblRemove.TabIndex = 102;
            this.lnklblRemove.TabStop = true;
            this.lnklblRemove.Text = "Remove";
            this.lnklblRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblRemove_LinkClicked);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DimGray;
            this.btnClear.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(817, 299);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 80);
            this.btnClear.TabIndex = 103;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click_1);
            // 
            // RadioBtnCash
            // 
            this.RadioBtnCash.AutoSize = true;
            this.RadioBtnCash.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.RadioBtnCash.Location = new System.Drawing.Point(25, 17);
            this.RadioBtnCash.Name = "RadioBtnCash";
            this.RadioBtnCash.Size = new System.Drawing.Size(58, 20);
            this.RadioBtnCash.TabIndex = 104;
            this.RadioBtnCash.TabStop = true;
            this.RadioBtnCash.Text = "Cash";
            this.RadioBtnCash.UseVisualStyleBackColor = true;
            // 
            // RadioBtnUPI
            // 
            this.RadioBtnUPI.AutoSize = true;
            this.RadioBtnUPI.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.RadioBtnUPI.Location = new System.Drawing.Point(112, 18);
            this.RadioBtnUPI.Name = "RadioBtnUPI";
            this.RadioBtnUPI.Size = new System.Drawing.Size(44, 20);
            this.RadioBtnUPI.TabIndex = 105;
            this.RadioBtnUPI.TabStop = true;
            this.RadioBtnUPI.Text = "UPI";
            this.RadioBtnUPI.UseVisualStyleBackColor = true;
            // 
            // RadioBtnCreditCard
            // 
            this.RadioBtnCreditCard.AutoSize = true;
            this.RadioBtnCreditCard.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold);
            this.RadioBtnCreditCard.Location = new System.Drawing.Point(184, 19);
            this.RadioBtnCreditCard.Name = "RadioBtnCreditCard";
            this.RadioBtnCreditCard.Size = new System.Drawing.Size(98, 20);
            this.RadioBtnCreditCard.TabIndex = 106;
            this.RadioBtnCreditCard.TabStop = true;
            this.RadioBtnCreditCard.Text = "CreditCard";
            this.RadioBtnCreditCard.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RadioBtnCreditCard);
            this.groupBox1.Controls.Add(this.RadioBtnUPI);
            this.groupBox1.Controls.Add(this.RadioBtnCash);
            this.groupBox1.Location = new System.Drawing.Point(222, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 46);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PaymentMode";
            // 
            // linkLabelRePrint
            // 
            this.linkLabelRePrint.AutoSize = true;
            this.linkLabelRePrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelRePrint.Location = new System.Drawing.Point(675, 454);
            this.linkLabelRePrint.Name = "linkLabelRePrint";
            this.linkLabelRePrint.Size = new System.Drawing.Size(57, 18);
            this.linkLabelRePrint.TabIndex = 108;
            this.linkLabelRePrint.TabStop = true;
            this.linkLabelRePrint.Text = "RePrint";
            this.linkLabelRePrint.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelRePrint_LinkClicked);
            // 
            // frmPOSReceiptVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(930, 482);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabelRePrint);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lnklblRemove);
            this.Controls.Add(this.btnLoadPending);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.txtChequeDate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblChequeDate);
            this.Controls.Add(this.lblChequeNO);
            this.Controls.Add(this.txtChequeNo);
            this.Controls.Add(this.lblLedgerId);
            this.Controls.Add(this.dgvPartyBalance);
            this.Controls.Add(this.txtCustName);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.lblCustomerCode);
            this.Controls.Add(this.txtCustomerId);
            this.Controls.Add(this.btnFindCustomer);
            this.Controls.Add(this.cmbCashOrBank);
            this.Controls.Add(this.panelBillDetails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblCashOrBank);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblNarration);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.txtNarration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPOSReceiptVoucher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Receipt Voucher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPOSReceiptVoucher_FormClosing);
            this.Load += new System.EventHandler(this.frmPOSReceiptVoucher_Load);
            this.panelBillDetails.ResumeLayout(false);
            this.panelBillDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartyBalance)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNarration;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label lblNarration;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblCashOrBank;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelBillDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBillDate;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblSessionNO;
        private System.Windows.Forms.Label lblBillTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSessionDt;
        private System.Windows.Forms.Label lblSessionDate;
        private System.Windows.Forms.ComboBox cmbCashOrBank;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label lblCustomerCode;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.Button btnFindCustomer;
        private System.Windows.Forms.Label lblBillNo;
        private System.Windows.Forms.Label lblLedgerId;
        private System.Windows.Forms.DataGridView dgvPartyBalance;
        private System.Windows.Forms.Label lblChequeDate;
        private System.Windows.Forms.Label lblChequeNO;
        private System.Windows.Forms.TextBox txtChequeNo;
        private System.Windows.Forms.Timer timerSessionDate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker txtChequeDate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnLoadPending;
        private System.Windows.Forms.LinkLabel lnklblRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn check;
        private System.Windows.Forms.DataGridViewComboBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherType;
        private System.Windows.Forms.DataGridViewTextBoxColumn MasterId;
        private System.Windows.Forms.DataGridViewTextBoxColumn voucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferanceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountToPay;
        private System.Windows.Forms.DataGridViewButtonColumn Select;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount;
        private System.Windows.Forms.DataGridViewComboBoxColumn currency;
        private System.Windows.Forms.DataGridViewTextBoxColumn crOrDr;
        private System.Windows.Forms.DataGridViewButtonColumn InterestParameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsFromDb;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton RadioBtnCash;
        private System.Windows.Forms.RadioButton RadioBtnUPI;
        private System.Windows.Forms.RadioButton RadioBtnCreditCard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabelRePrint;
    }
}