using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    public partial class frmPOSReceiptVoucher : Form
    {
        public frmPOSReceiptVoucher()
        {
            InitializeComponent();
        }

        bool formCancel = false;
        frmPOSSales frmPOSSales;
        string strFocusedControl = "";
        string strFromPaymentMethod = "";
        TextBox txtTouchTextBox = new TextBox();
        public string strSessionNo = "";
        public string strSessionDate = "";

  

        POSSalesMasterSP POSSalesMasterSP = new POSSalesMasterSP();
        POSReceiptMasterSP objPOSReceiptMasterSP = new POSReceiptMasterSP();
        ComboValidation objComboValidation = new ComboValidation();
        POSCounterInfo counterInfo = new POSCounterInfo();
        GeneralSP SPGeneral = new GeneralSP();
        ProductSP spProduct = new ProductSP();
        POSReceiptMasterSP POSreceiptMasterSP = new POSReceiptMasterSP();
        POSReceiptDetailsSP POSreceiptDetailsSP = new POSReceiptDetailsSP(); 
        public void FillCombo()
        {
            // To fill bank or cash combo
            try
            {
                cmbCashOrBank.DataSource = null;
                AccountLedgerSP spledger = new AccountLedgerSP();
                DataTable dtbl = new DataTable();
                dtbl = spledger.AccountLedgerGetCashBankAndODWithBalance(PublicVariables._branchId);
                cmbCashOrBank.DataSource = dtbl;
                cmbCashOrBank.DisplayMember = "ledgerName";
                cmbCashOrBank.ValueMember = "ledgerId";
                cmbCashOrBank.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string POSBillNumberMax()
        {
            string PartBillNo = "";

            try
            {
                DataTable dtbl = new DataTable();
                dtbl = SPGeneral.GetPOSLastBillNo(PublicVariables._counterId, "Receipt");


                if (dtbl.Rows.Count > 0)
                {
                    if (!dtbl.Rows[0].IsNull(0))
                        PartBillNo = dtbl.Rows[0]["LastBillNo"].ToString();
                    else
                        PartBillNo = "1";
                }
                else
                    PartBillNo = "1";

                PartBillNo = (PartBillNo.ToString()).PadLeft(7, '0');

                PartBillNo = "RV" + PublicVariables._counterId + "" + DateTime.Now.Date.ToString("yy") + "" + PartBillNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return PartBillNo;
        }
        public void ClearFunction()
        {
            isInEditMode = false;
            btnDelete.Enabled = false;
            strDetailsIdForEdit = "";
            strMasterIdForEdit = "";
            btnSave.Text = "Save";
            btnCancel.Text = "Cancel";
            txtNarration.Clear();
            txtCustName.Clear();
            txtCustomerId.Clear();
            lblLedgerId.Text = "";
            txtChequeDate.Value = DateTime.Now;
            txtChequeNo.Clear();
            cmbCashOrBank.SelectedIndex = -1;
            lblBillNo.Text = POSBillNumberMax();
            RadioBtnCash.Checked = true;
            txtTotalAmount.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            dgvPartyBalance.Rows.Clear();
            txtCustomerId.Focus();
        }

        public void FormLoadFunction()
        {
            timerSessionDate.Start();
            timer1.Start();
            lblSessionNO.Text = strSessionNo;
            lblSessionDate.Text = strSessionDate;
            lblCounter.Text = PublicVariables._counterId;
            //lblCounter.Tag = PublicVariables._counterId;
            lblUser.Text = PublicVariables._EmpName;
            if (DateTime.Compare(Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(strSessionDate)) > 0)
            {
                MessageBox.Show("Opened Session Date is not Today's Date");
            }
            // Set the custom format of the DateTimePicker
            txtChequeDate.Format = DateTimePickerFormat.Custom;
            txtChequeDate.CustomFormat = PublicVariables._DateFormat;

            // Set the value of the DateTimePicker to today's date
            txtChequeDate.Value = DateTime.Today;
            //txtChequeDate.Text = DateTime.Today.ToString(PublicVariables._DateFormat);   
            
            POSCounterSP counterSP = new POSCounterSP();
            counterInfo = counterSP.POSCounterViewbyCounterId(PublicVariables._counterId);

            //POS Settings
            POSSettingsInfo InfoPOSSettings = new POSSettingsInfo();
            POSSettingsSP SpPOSSettings = new POSSettingsSP();
            InfoPOSSettings = SpPOSSettings.POSSettingsViewByBranchId(PublicVariables._branchId);
            FillCombo();
            FilltypeCombo();
            FillCurrencyCombo();

        }
       
      
        private void frmPOSReceiptVoucher_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

    

    
        private void txtCash_Enter(object sender, EventArgs e)
        {
            txtTouchTextBox = sender as TextBox;
            txtTouchTextBox.Select();
              
            lblTotalAmount.ForeColor = Color.Red; 
        }

       

        private void btnCash_Click(object sender, EventArgs e)
        {
            //txtCash.Text = "";
            strFocusedControl = "txtCash";
            txtTouchTextBox = txtTotalAmount;
            //txtCash.Text = CalculateBalanceTenderedAmt().ToString(SettingsInfo._roundDecimalPart);  
            txtTotalAmount.Focus();
            txtTotalAmount.SelectAll();
        }

      

      
       
        private void txtUPI_Enter(object sender, EventArgs e)
        {
            lblCashOrBank.ForeColor = Color.Red; 
        }

        private void txtCredit_Enter(object sender, EventArgs e)
        {
            lblNarration.ForeColor = Color.Red; 
        }  
        private void txtUPI_Leave(object sender, EventArgs e)
        {
            lblCashOrBank.ForeColor = Color.Black; 
        }

        private void txtCredit_Leave(object sender, EventArgs e)
        {
            lblNarration.ForeColor = Color.Black; 
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            lblTotalAmount.ForeColor = Color.Black; 
        }  
        private void btnSelect_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Focus(); 
            txtTouchTextBox.SelectAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Clear();
           
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
          
            if (CheckGrid())
            {
                if (!isInEditMode)
                {
                    if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        FindTotal();
                        SaveFunction();
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you want to update?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        FindTotal();
                        SaveFunction();
                    }
                }

               
            }
        }
        public bool CheckGrid()
        {
            // To check all the conditions for saving 
            bool isOk = false;
            decimal amntTotal = 0;
            bool isTicked = false;
            try
            {
                 if (txtCustomerId.Text == "")
                {
                    isOk = false;
                    MessageBox.Show("Select Ledger", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCustomerId.Focus();
                }
               //blocked on 29/mar/2025 for ledger load from counter master
                //else if (cmbCashOrBank.SelectedValue == null)
                //{
                //    isOk = false;
                //    MessageBox.Show("Select bank or cash account", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    cmbCashOrBank.Focus();
                //}
                 else if (txtTotalAmount.Text == "0" || txtTotalAmount.Text == "0.00" || txtTotalAmount.Text == "")
                 {
                     isOk = false;
                     MessageBox.Show("Amount should not be zero", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     txtTotalAmount.Focus();
                 }
                else
                {
                    int inCk = 0;
                    if (dgvPartyBalance.Rows.Count > 1)
                    {
                        foreach (DataGridViewRow dgvrowCurChk in dgvPartyBalance.Rows)
                        {
                            if (dgvrowCurChk.Cells["check"].Value != null)
                            {
                                if (dgvrowCurChk.Cells["check"].Value.ToString() != "x")
                                {
                                    if (!dgvrowCurChk.IsNewRow)
                                    {
                                        inCk++;
                                    }
                                }
                            }
                        }

                        if (inCk > 0)
                        {
                            string strErrorMessage = "Rows ";
                            int inC = 0, inForFirst = 0;
                            // To check any incomplete row and to give message according to that
                            foreach (DataGridViewRow dgvrowCur in dgvPartyBalance.Rows)
                            {
                                if (dgvrowCur.Cells["check"].Value != null)
                                    if (dgvrowCur.Cells["check"].Value.ToString() == "x")
                                    {
                                        isOk = false;
                                        if (inC == 0)
                                        {
                                            strErrorMessage = strErrorMessage + Convert.ToString(dgvrowCur.Index + 1);
                                            inForFirst = dgvrowCur.Index;
                                            inC++;
                                        }
                                        else
                                        {
                                            strErrorMessage = strErrorMessage + ", " + Convert.ToString(dgvrowCur.Index + 1);
                                        }
                                    }
                            }
                            if (strErrorMessage != "Rows ")
                            {
                                strErrorMessage = strErrorMessage + " contains invalid entries";
                                //if (MessageBox.Show(strErrorMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                //{
                                //    //isOk = true;
                                //    //DeleteRow();
                                //}
                                //else
                                //{
                                isOk = false;
                                dgvPartyBalance.Focus();
                                if (dgvPartyBalance.Rows[inForFirst].Cells["type"].Value == null || dgvPartyBalance.Rows[inForFirst].Cells["type"].Value.ToString().Trim() == "")
                                    dgvPartyBalance.Rows[inForFirst].Cells["type"].Selected = true;
                                else if (dgvPartyBalance.Rows[inForFirst].Cells["type"].Value.ToString() == "Against" && (dgvPartyBalance.Rows[inForFirst].Cells["voucherType"].Value == null ? "" : dgvPartyBalance.Rows[inForFirst].Cells["voucherType"].Value.ToString()) == "")
                                    dgvPartyBalance.Rows[inForFirst].Cells["voucherType"].Selected = true;
                                else if (dgvPartyBalance.Rows[inForFirst].Cells["amount"].Value == null || decimal.Parse(dgvPartyBalance.Rows[inForFirst].Cells["amount"].Value.ToString()) == 0m)
                                    dgvPartyBalance.Rows[inForFirst].Cells["amount"].Selected = true;
                                else if (dgvPartyBalance.Rows[inForFirst].Cells["currency"].Value == null || dgvPartyBalance.Rows[inForFirst].Cells["currency"].Value.ToString().Trim() == "")
                                    dgvPartyBalance.Rows[inForFirst].Cells["currency"].Selected = true;

                            }
                            else
                            {
                                isOk = true;
                            }
                            // To check any Repeated entry in grid
                            //foreach (DataGridViewRow dgvrowFirst in dgvPartyBalance.Rows)
                            //{
                            //    foreach (DataGridViewRow dgvrowSecond in dgvPartyBalance.Rows)
                            //    {
                            //        if (dgvrowFirst.Index != dgvrowSecond.Index)
                            //        {
                            //            if (dgvrowFirst.Cells["voucherType"].Value != null && dgvrowSecond.Cells["voucherType"].Value != null)
                            //                if (dgvrowFirst.Cells["voucherType"].Value.ToString() != "" && dgvrowSecond.Cells["voucherType"].Value.ToString() != "")
                            //                    if (dgvrowFirst.Cells["voucherType"].Value.ToString() == dgvrowSecond.Cells["voucherType"].Value.ToString())
                            //                    {
                            //                        isOk = false;
                            //                        MessageBox.Show("Repeated entry of same voucher type exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //                        dgvPartyBalance.Focus();
                            //                        dgvrowSecond.Cells["voucherType"].Selected = true;
                            //                        break;
                            //                    }
                            //        }
                            //    }
                            //    if (!isOk)
                            //    {
                            //        break;
                            //    }
                            //}
                        }
                        else
                        {
                            MessageBox.Show("Can't save party balance without atleast row with complete details", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isOk = false;
                            dgvPartyBalance.Focus();
                            dgvPartyBalance.Rows[0].Cells["VoucherType"].Selected = true;
                        }

                        amntTotal = 0;
                        if (isOk)
                        {
                            foreach (DataGridViewRow dgvRow in dgvPartyBalance.Rows)
                            {
                                if (!dgvRow.IsNewRow)
                                {
                                    if (dgvRow.Cells["isSelect"].Value != null && dgvRow.Cells["isSelect"].Value.ToString() != "")
                                    {
                                        if (bool.Parse(dgvRow.Cells["isSelect"].Value.ToString()) == true)
                                        {
                                            isTicked = true;
                                            if (dgvRow.Cells["amountToPay"].Value != null && dgvRow.Cells["amountToPay"].Value.ToString() != "")
                                            {
                                                if (dgvRow.Cells["amount"].Value != null && dgvRow.Cells["amount"].Value.ToString() != "")
                                                {
                                                    if (dgvRow.Cells["type"].Value.ToString() == "Against")
                                                    {
                                                        string[] strAmountToPay = dgvRow.Cells["amountToPay"].Value.ToString().Split(' ');
                                                        amntTotal = amntTotal + decimal.Parse(dgvRow.Cells["amount"].Value.ToString());
                                                        if (decimal.Parse(dgvRow.Cells["amountToPay"].Value.ToString()) < 0)// for salesReturn
                                                        {
                                                            if (decimal.Parse(dgvRow.Cells["amount"].Value.ToString()) < decimal.Parse(strAmountToPay[0]))
                                                            {
                                                                isOk = false;
                                                                MessageBox.Show("Amount exceeds the amount to pay", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                dgvPartyBalance.Focus();
                                                                dgvRow.Cells["amount"].Selected = true;
                                                                break;
                                                            }
                                                        }
                                                        else//others
                                                        {
                                                            if (decimal.Parse(dgvRow.Cells["amount"].Value.ToString()) > decimal.Parse(strAmountToPay[0]))
                                                            {
                                                                isOk = false;
                                                                MessageBox.Show("Amount exceeds the amount to pay", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                dgvPartyBalance.Focus();
                                                                dgvRow.Cells["amount"].Selected = true;
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    else
                                                        amntTotal = amntTotal + decimal.Parse(dgvRow.Cells["amount"].Value.ToString());
                                                }
                                            }
                                            else
                                                amntTotal = amntTotal + decimal.Parse(dgvRow.Cells["amount"].Value.ToString());
                                        }
                                    }
                                }
                            }
                        }
                        if (isTicked)
                            if (amntTotal == 0)
                                isOk = false;
                        //if (isfromJournal)
                        //{
                        //    if (isOk)
                        //    {
                        //        if (amntTotal != this.journalAmount)
                        //        {
                        //            isOk = false;
                        //            MessageBox.Show("Amount should be equal to Journal amount", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //            dgvPartyBalance.Focus();
                        //            dgvPartyBalance.Rows[0].Cells["amount"].Selected = true;
                        //        }

                        //    }
                        //}
                    }
                    else
                        isOk = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PB19:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return isOk;
        }
        public void  SaveFunction()
        {
            string strMasterId = "";
            try
            {
                if (!isInEditMode)
                {
                    lblBillNo.Text = POSBillNumberMax();
                }
                POSReceiptMasterInfo InfoPOSReceiptMaster = new POSReceiptMasterInfo();
                InfoPOSReceiptMaster.receiptNo = lblBillNo.Text;
                InfoPOSReceiptMaster.billDate = Convert.ToDateTime(lblBillDate.Text);
                InfoPOSReceiptMaster.billTime = lblBillTime.Text;
                InfoPOSReceiptMaster.sessionDate = Convert.ToDateTime(lblSessionDate.Text);
                InfoPOSReceiptMaster.counterId = PublicVariables._counterId;
                InfoPOSReceiptMaster.sessionNo = lblSessionNO.Text;

                if (RadioBtnCash.Checked == true) //ADDED on 29-03-2025 by Nishana
                {
                    InfoPOSReceiptMaster.CashOrBankledgerId = counterInfo.CashAccountLedgerId;
                    InfoPOSReceiptMaster.ReceiptType = RadioBtnCash.Text ;
                    
                }
                else if (RadioBtnUPI.Checked == true)
                {
                    InfoPOSReceiptMaster.CashOrBankledgerId = counterInfo.UPIAccountLedgerId;
                    InfoPOSReceiptMaster.ReceiptType = RadioBtnUPI.Text;

                }
                else if (RadioBtnCreditCard.Checked == true)
                {
                    InfoPOSReceiptMaster.CashOrBankledgerId = counterInfo.BankAccountLedgerId;
                    InfoPOSReceiptMaster.ReceiptType = RadioBtnCreditCard.Text;

                }

                //cashledgerid
               // InfoPOSReceiptMaster.CashOrBankledgerId = cmbCashOrBank.SelectedValue.ToString(); //blocked on 29-03-2025

                InfoPOSReceiptMaster.narration = txtNarration.Text;
                InfoPOSReceiptMaster.userId = PublicVariables._currentUserId;
                InfoPOSReceiptMaster.totalAmount = decimal.Parse(txtTotalAmount.Text);
                InfoPOSReceiptMaster.extra1 = "";
                if (!isInEditMode)
                {                   
                    strMasterId = POSreceiptMasterSP.POSReceiptMasterAdd(InfoPOSReceiptMaster);
                }
                else
                {
                    InfoPOSReceiptMaster.POSReceiptMasterId = strMasterIdForEdit;
                    POSreceiptMasterSP.POSReceiptMasterEdit(InfoPOSReceiptMaster);
                }
                //if (strMasterId != "")
                //{
                    POSReceiptDetailsInfo InfoPOSSalesDetails1 = new POSReceiptDetailsInfo();
                    if (isInEditMode)
                    {
                        InfoPOSSalesDetails1.POSReceiptMasterId = strMasterIdForEdit;
                    }
                    else
                        InfoPOSSalesDetails1.POSReceiptMasterId = strMasterId;
                    InfoPOSSalesDetails1.ledgerId = lblLedgerId.Text;
                    InfoPOSSalesDetails1.amount = Convert.ToDecimal(txtTotalAmount.Text);
                    InfoPOSSalesDetails1.chequeNo = txtChequeNo.Text;
                    InfoPOSSalesDetails1.chequeDate = DateTime.Now;
                    InfoPOSSalesDetails1.extra1 = "";
                    string strPODetailsId = "";
                    if (!isInEditMode)
                    {
                        strPODetailsId = POSreceiptDetailsSP.POSReceiptDetailsAdd(InfoPOSSalesDetails1);
                    }
                    else
                    {
                        InfoPOSSalesDetails1.POSReceiptDetailsId = strDetailsIdForEdit;
                        POSreceiptDetailsSP.POSReceiptDetailsEdit(InfoPOSSalesDetails1);
                    }
                    if (isInEditMode)
                    {
                        LedgerPostingSP SpPosting = new LedgerPostingSP();
                        PartyBalanceSP SpPartyBalance = new PartyBalanceSP();
                      //  IntrestParameterSettingsSP SpInterest = new IntrestParameterSettingsSP();
                        SpPosting.LedgerPostingDeleteByVoucherTypeAndVoucherNo(InfoPOSSalesDetails1.POSReceiptMasterId, "POSReceipt");
                        SpPartyBalance.PartyBalanceDeleteByVoucherTypeVoucherNoAndReferenceType(InfoPOSSalesDetails1.POSReceiptMasterId, "POSReceipt");

                    }
                    // Add details in grid to ledger posting 
                    DoLedgerPosting(InfoPOSSalesDetails1.ledgerId, InfoPOSSalesDetails1.amount, InfoPOSSalesDetails1.POSReceiptMasterId, true);

                    AccountLedgerSP SpLedger = new AccountLedgerSP();
                    AccountLedgerInfo Info = new AccountLedgerInfo();
                    Info = SpLedger.AccountLedgerView(lblLedgerId.Text);
                    CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
                    bool BillByBill = SPCurrencyConversion.SettingsViewByBranchId(PublicVariables._branchId);
                    if (Info.BillByBill && BillByBill)
                    {
                        // Add deails to party balance table
                        DoPartyBalance(InfoPOSSalesDetails1.ledgerId, InfoPOSSalesDetails1.POSReceiptMasterId);
                    }
                    // Add interest parameter details to InterestparameterSettings and interest parameter details table
                   // DoInterestparameterSettings(InfoPOSSalesDetails1.ledgerId, InfoPOSSalesDetails1.POSReceiptMasterId);


                    // Adding Master details to Ledger posting table
                    DoLedgerPosting(InfoPOSReceiptMaster.CashOrBankledgerId, InfoPOSReceiptMaster.totalAmount, InfoPOSSalesDetails1.POSReceiptMasterId, false);
               // }
                if (!isInEditMode)
                {
                    spGeneral.UserActivityAdd("POSReceipt", "Save", InfoPOSSalesDetails1.POSReceiptMasterId, lblBillNo.Text, Convert.ToDateTime(lblBillDate.Text), decimal.Parse(txtTotalAmount.Text), decimal.Parse(txtTotalAmount.Text));
                    // Checking voucher number
                    //if (dtblMaster.Rows[0].ItemArray[1].ToString() != txtVoucherNo.Text)
                    //{
                    //    MessageBox.Show("Voucher number has been changed from " + txtVoucherNo.Text + " to " + dtblMaster.Rows[0].ItemArray[1].ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtVoucherNo.Text = dtblMaster.Rows[0].ItemArray[1].ToString();
                    //}
                   
                        MessageBox.Show("Saved Successfully");
                        SPGeneral.POSBillUpdate(PublicVariables._counterId, PublicVariables._currentUserId, "Receipt");
                        FillDatatatablesforDevPrint(false, "");

                        ClearFunction();
                    
                }
                else
                {
                    spGeneral.UserActivityAdd("POSReceipt", "Edit", InfoPOSSalesDetails1.POSReceiptMasterId, lblBillNo.Text, Convert.ToDateTime(lblBillDate.Text), decOldAmount, decimal.Parse(txtTotalAmount.Text));
                    MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillDatatatablesforDevPrint(false, "");

                    ClearFunction();
                }
            }
            catch (Exception ex)
            { }
            
        }
        public void DoLedgerPosting(string strLedgerId, decimal dcAmount, string voucherNo, bool isMaster)
        {
            try
            {
                //Ledger posting for payment voucher is credit when master ledger Debit when Details ledger
                // isMaster bit true for Master and false for  Details 
                LedgerPostingSP SpPosting = new LedgerPostingSP();
                LedgerPostingInfo InfoPosting = new LedgerPostingInfo();
                InfoPosting.VoucherType = "POSReceipt";
                InfoPosting.postingType = "POSReceipt";
                InfoPosting.InvoiceNo = lblBillNo.Text;
                InfoPosting.Narration = txtNarration.Text;
                InfoPosting.exchangeDate = PublicVariables._fromDate; //added on 24/03/2025 by Nishana
                InfoPosting.exchangeRate = 1;//added on 24/03/2025 

              //  InfoPosting.costCentreId = cmbCostCentre.SelectedValue.ToString();

                if (isMaster)
                {
                    InfoPosting.Debit = 0;
                    InfoPosting.Credit = dcAmount;
                }
                else
                {
                    InfoPosting.Debit = dcAmount;
                    InfoPosting.Credit = 0;

                }
                InfoPosting.Extra1 = "";
                InfoPosting.Extra2 = "";
               
              
                // InfoPosting.Optional = cbxOptional.Checked;

                // Post new balance
                InfoPosting.VoucherNo = voucherNo;
                InfoPosting.LedgerId = strLedgerId;
                InfoPosting.Date = DateTime.Parse(lblBillDate.Text);
                SpPosting.LedgerPostingAdd(InfoPosting);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV18:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DoPartyBalance(string strLedgerId, string voucherNo)
        {
            try
            {
                // To add to party balance table
                bool isSelect = false;
                PartyBalanceInfo InfoPatryBalance = new PartyBalanceInfo();
                PartyBalanceSP SPPartyBalance = new PartyBalanceSP();
                CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
                AccountLedgerSP SPLedger = new AccountLedgerSP();
                if (dgvPartyBalance.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dRow in dgvPartyBalance.Rows)
                    {
                        if (dRow.Cells["check"].Value != null)
                            if (dRow.Cells["check"].Value.ToString() != "x")
                            {
                                if (!dRow.IsNewRow)
                                {
                                    if (dRow.Cells["isSelect"].Value != null)
                                        isSelect = bool.Parse(dRow.Cells["isSelect"].Value.ToString());
                                    else
                                        isSelect = false;

                                    if (isSelect)
                                    {
                                        if (lblLedgerId.Text == strLedgerId)
                                        {                                           
                                            InfoPatryBalance.Date = DateTime.Parse(lblBillDate.Text);
                                            InfoPatryBalance.BranchId = PublicVariables._branchId;
                                            InfoPatryBalance.Extra1 = "";
                                            InfoPatryBalance.Extra2 = "";
                                            InfoPatryBalance.Optional = false;
                                            InfoPatryBalance.LedgerId = lblLedgerId.Text;

                                            if (dRow.Cells["type"].Value.ToString() == "Against")
                                            {
                                                InfoPatryBalance.VoucherType = dRow.Cells["voucherType"].Value.ToString();
                                                InfoPatryBalance.VoucherNo = dRow.Cells["MasterId"].Value.ToString();
                                                InfoPatryBalance.AgainstVoucherType = "POSReceipt";
                                                InfoPatryBalance.AgainstvoucherNo = voucherNo;
                                            }
                                            if (dRow.Cells["type"].Value.ToString() == "New" || dRow.Cells["type"].Value.ToString() == "OnAccount")
                                            {
                                                InfoPatryBalance.VoucherType = "POSReceipt";
                                                InfoPatryBalance.VoucherNo = voucherNo;
                                                InfoPatryBalance.AgainstVoucherType = "NA";
                                                InfoPatryBalance.AgainstvoucherNo = "NA";
                                            }
                                            InfoPatryBalance.ReferenceType = dRow.Cells["type"].Value.ToString();
                                            AccountLedgerInfo InfoLedger = SPLedger.AccountLedgerView(strLedgerId);
                                            InfoPatryBalance.CreditPeriod = InfoLedger.CreditPeriod;
                                            InfoPatryBalance.CurrencyConversionId = SPCurrencyConversion.CurrencyConversionRateIdViewByCurrencyId(dRow.Cells[12].Value.ToString(), DateTime.Parse(lblBillDate.Text), PublicVariables._branchId);
                                            CurrencyConversionInfo InfoCurrencyConversion = SPCurrencyConversion.CurrencyConversionView(InfoPatryBalance.CurrencyConversionId);
                                            InfoPatryBalance.Debit = 0;
                                            InfoPatryBalance.Credit = decimal.Parse(dRow.Cells["amount"].Value.ToString());
                                            InfoPatryBalance.invoiceNo = lblBillNo.Text; //added on 24/03/2025  by  Nishana
                                            InfoPatryBalance.referenceNo = "";  //24/03/2025
                                            InfoPatryBalance.BillAmount = decimal.Parse(txtTotalAmount.Text); //24/03/2025
                                            InfoPatryBalance.invoiceDate = DateTime.Parse(lblSessionDate.Text); //24/03/2025
                                            InfoPatryBalance.costCentreId = "1"; //24/03/2025
                                            InfoPatryBalance.exchangeDate = PublicVariables._fromDate; //24/03/2025
                                            InfoPatryBalance.exchangeRate = 1; //24/03/2025
                                            SPPartyBalance.PartyBalanceAdd(InfoPatryBalance);
                                        }
                                    }
                                }
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV19:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

      
        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                frmProductPopUpGrid frm = new frmProductPopUpGrid();
                frm.CallFromReceiptVoucher(this, "customer");
                if (lblLedgerId.Text.ToString() != "")
                {
                    DataTable dtbl = new DataTable();
                    dtbl = spProduct.GetCustomerDetailsbyLedgerId(lblLedgerId.Text.ToString());
                    if (dtbl.Rows.Count > 0)
                    {
                        txtCustomerId.Text = dtbl.Rows[0]["ledgerCode"].ToString();
                        txtCustName.Text = dtbl.Rows[0]["ledgerName"].ToString();
                        dgvPartyBalance.Rows.Clear();
                        txtTotalAmount.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);

                        AccountLedgerSP SpLedger = new AccountLedgerSP();
                        AccountLedgerInfo Info = new AccountLedgerInfo();
                        Info = SpLedger.AccountLedgerView(lblLedgerId.Text);
                        CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
                        bool BillByBill = SPCurrencyConversion.SettingsViewByBranchId(PublicVariables._branchId);
                        if (Info.BillByBill && BillByBill)
                        {
                            txtTotalAmount.Enabled = false;
                            FillGrid(false);
                            //isReadOnlyAgainst = false;
                        }
                        else
                        {
                            txtTotalAmount.Enabled = true;
                        }
                        linkLabelRePrint.Visible = true;
                    }
                }
                else
                {
                    lblLedgerId.Text = "";
                    txtCustomerId.Text = "";
                    txtCustName.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR34:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillrowAfterPickingCustomer(string ledgerId)
        {
            lblLedgerId.Text = ledgerId;
        }
        public void FilltypeCombo()
        {
            // To fill bank or cash combo
            try
            {
                DataTable dtbl = new DataTable();
                DataColumn clmn = new DataColumn("Type");
                dtbl.Columns.Add(clmn);
                DataRow nrow = dtbl.NewRow();
                dtbl.Rows.Add(nrow);
                nrow["Type"] = "Against";
                DataRow nrow1 = dtbl.NewRow();
                dtbl.Rows.Add(nrow1);
                nrow1["Type"] = "New";
                DataRow nrow2 = dtbl.NewRow();
                dtbl.Rows.Add(nrow2);
                nrow2["Type"] = "OnAccount";


                if (dtbl.Rows.Count > 0)
                {
                    type.DataSource = dtbl;
                    type.DisplayMember = "Type";
                    type.ValueMember = "Type";
                    //if (dtbl.Select("currencyId ='" + PublicVariables._currencyId + "'").Length > 0)
                    //{
                    //    type.SelectedValue = "New";
                    //}

                }

                //TransactionsGeneralFill objGeneral = new TransactionsGeneralFill();
                //ComboBox cmbCurrency = new ComboBox();
                //objGeneral.FillCurrencyCombo(cmbCurrency);
                //currency.DataSource = (DataTable)cmbCurrency.DataSource;
                //currency.DisplayMember = "currencySymbol";
                //currency.ValueMember = "currencyId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR14:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillCurrencyCombo()
        {
            // To fill currency combo
            try
            {

                CurrencyConversionSP objGeneral = new CurrencyConversionSP();
                ComboBox cmbCurrency = new ComboBox();
                objGeneral.FillCurrencyCombo(cmbCurrency);
                currency.DataSource = (DataTable)cmbCurrency.DataSource;
                currency.DisplayMember = "currencySymbol";
                currency.ValueMember = "currencyId";
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmPOSReceiptVoucher_Load(object sender, EventArgs e)
        {
           
            FormLoadFunction();
            ClearFunction();
            CreatingPartyBalanceDatatable();
            linkLabelRePrint.Visible = false;
        }
        DataTable dtblParty = new DataTable();
        PartyBalanceSP spPartyBalance = new PartyBalanceSP();
        string strcurrency = "";
        private void FillGrid(bool IsFromLoadPEnding)
        {
            try
            {
               
                DataTable dtblPartyBalance = new DataTable();
                decimal dcAmount = 0;
                DataTable dtblFilter = new DataTable();
                string strLedgerId = "";
                strLedgerId = lblLedgerId.Text;
                if (dtblParty.Rows.Count > 0)
                {
                    dtblPartyBalance = dtblParty.Copy();
                }
                else
                {
                    dtblPartyBalance = spPartyBalance.PartyBalanceViewByLedgerIdNew(strLedgerId, "Cr");
                }

                CurrencyConversionSP SpCurrencyConversion = new CurrencyConversionSP();
                DataTable dtblAgainst = new DataTable();
                if (!IsFromLoadPEnding)
                    dgvPartyBalance.Rows.Clear();
                else
                {

                    if (dtblPartyBalance.Rows.Count > 0 && dgvPartyBalance.Rows.Count > 1)
                    {
                        string strCondition = "";
                        for (int rowIndex = 0; rowIndex < dtblPartyBalance.Rows.Count; rowIndex++)
                        {
                            foreach (DataGridViewRow gridRow in dgvPartyBalance.Rows)
                            {
                                if ((gridRow.Cells["MasterId"].Value != null && gridRow.Cells["MasterId"].Value.ToString() != ""))
                                {
                                    if (strCondition == "")
                                        strCondition = "(voucherType <>'" + gridRow.Cells["voucherType"].Value.ToString() + "'OR MasterId <>'" + gridRow.Cells["MasterId"].Value.ToString() + "')";
                                    else
                                    {
                                        strCondition += "AND( voucherType <>'" + gridRow.Cells["voucherType"].Value.ToString() + "'OR MasterId <>'" + gridRow.Cells["MasterId"].Value.ToString() + "')";
                                    }

                                }

                            }
                        }
                        if (strCondition != "")
                        {

                            strCondition = "(" + strCondition + ")";
                            strCondition += "OR(  voucherType ='" + "''" + "')";
                            dtblPartyBalance.DefaultView.RowFilter = strCondition;
                            dtblPartyBalance = dtblPartyBalance.DefaultView.ToTable();

                        }
                    }
                }


                if (dtblPartyBalance.Rows.Count > 0)
                {
                   // txtTotalAmount.ReadOnly = true;
                    for (int i = 0; i < dtblPartyBalance.Rows.Count; ++i)
                    {
                        dgvPartyBalance.Rows.Add();
                        // FillCombo(dgvPartyBalance.Rows.Count - 2);
                       // dgvPartyBalance.CurrentCell = dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["voucherType"];
                        CurrencyConversionInfo InfoCurrencyConversion = SpCurrencyConversion.CurrencyConversionView(dtblPartyBalance.Rows[i]["currecyConversionId"].ToString());


                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["type"].Value = dtblPartyBalance.Rows[i]["type"].ToString();//Against
                        if (dtblPartyBalance.Rows[i]["type"].ToString() == "Against")
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["type"].ReadOnly = true;
                        else
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["type"].ReadOnly = false;

                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["ReferanceNo"].Value = dtblPartyBalance.Rows[i]["ReferanceNo"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["MasterId"].Value = dtblPartyBalance.Rows[i]["MasterId"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["voucherNo"].Value = dtblPartyBalance.Rows[i]["VoucherNo"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["voucherType"].Value = dtblPartyBalance.Rows[i]["voucherType"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["VoucherDate"].Value = dtblPartyBalance.Rows[i]["VoucherDate"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["BillAmount"].Value = Math.Round((decimal.Parse(dtblPartyBalance.Rows[i]["BillAmount"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);
                        if (strMasterIdForEdit != "" && !IsFromLoadPEnding)
                        {
                            dtblAgainst = new DataTable();
                            dtblAgainst = spPartyBalance.PartyBalanceGetByAgainstNew("POSReceipt", strMasterIdForEdit, strLedgerId, "Cr");
                            if (dtblAgainst.Rows.Count > 0)
                            {
                                dcAmount = 0;
                                try
                                {
                                    DataRow[] drArr = dtblAgainst.Select("MastervoucherNo = '" + dtblPartyBalance.Rows[i]["VoucherNo"].ToString() + "' AND voucherType = '" + dtblPartyBalance.Rows[i]["voucherType"].ToString() + "'");//check mastervoucherNo of each datatable
                                    if (drArr.Length > 0)
                                    {

                                        dcAmount = decimal.Parse(drArr[0]["amount"].ToString()) + decimal.Parse(dtblPartyBalance.Rows[i]["amountToPay"].ToString());

                                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = Math.Round((dcAmount / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);

                                    }
                                    else
                                    {
                                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = Math.Round((decimal.Parse(dtblPartyBalance.Rows[i]["amountToPay"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);
                                        //if (bool.Parse(dtblPartyBalance.Rows[i]["IsFromDb"].ToString().ToString()) == false)
                                        //{
                                        //    dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = Math.Round((decimal.Parse(dtblPartyBalance.Rows[i]["amountToPay"].ToString()) / InfoCurrencyConversion.Rate), 2);
                                        //}
                                        //else
                                        //    dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = "";
                                    }
                                    ////string am = dtblAgainst.Rows[i]["amount"].ToString();
                                    ////string amTopay = dtblPartyBalance.Rows[i]["amountToPay"].ToString();

                                }
                                catch
                                {
                                    dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = "";
                                }
                            }
                            else
                            {
                                dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = Math.Round((decimal.Parse(dtblPartyBalance.Rows[i]["amountToPay"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);
                            }
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["isSelect"].Value = true;
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["Select"].Value = "UnSelect";
                        }
                        else
                        {
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = Math.Round((decimal.Parse(dtblPartyBalance.Rows[i]["amountToPay"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["Select"].Value = "Select";
                        }

                        //  dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amountToPay"].Value = Math.Round((decimal.Parse(dtblPartyBalance.Rows[i]["amountToPay"].ToString()) / InfoCurrencyConversion.Rate), 2);
                      
                        strcurrency = dtblPartyBalance.Rows[i]["currency"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["currency"].Value = dtblPartyBalance.Rows[i]["currency"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["crOrDr"].Value = "Cr";// Dr for payment and Cr for Receipt
                        isFormLoad = true;
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["amount"].Value = dtblPartyBalance.Rows[i]["amount"].ToString();

                        isFormLoad = false;
                        // dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["IsFromDb"].Value = dtblPartyBalance.Rows[i]["IsFromDb"].ToString();
                        // dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["InVoucherType"].Value = dtblFillGrid.Rows[i]["voucherType"].ToString();
                        //if (dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["type"].Value.ToString() == "Against")
                        //{
                        //    dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 2].Cells["InterestParameter"].ReadOnly = true;
                        //   // isReadOnly = true;
                        //}
                    }

                }
                //else
                //    txtTotalAmount.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR70:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        bool isWorkValueChanged = true;
        bool isFormLoad = false;
        private void dgvPartyBalance_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    if (isWorkValueChanged)
                    {
                        // Check if the changed cell is a checkbox cell
                        if (e.ColumnIndex == dgvPartyBalance.Columns["isSelect"].Index && e.RowIndex >= 0)
                        {
                            //  string strType = dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString();
                            if (dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "Against")
                            {
                                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"];
                                bool isChecked = (bool)cell.Value;
                                // Do something based on checkbox state
                                // MessageBox.Show("Checkbox changed: " + isChecked);
                                if (!isFormLoad)
                                {
                                    isWorkValueChanged = false;
                                    if (isChecked)
                                    {
                                        decimal balance = dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString() == "" ? 0 : decimal.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString());
                                        dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value = Math.Round(balance, SettingsInfo._roundDecimal).ToString();
                                    }
                                    //else
                                    //{

                                    //    dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value = "0";
                                    //}
                                    isWorkValueChanged = true;
                                }
                            }
                          
                        }
                        else if (e.ColumnIndex == dgvPartyBalance.Columns["amount"].Index && e.RowIndex >= 0)
                        {
                            //  string a=dgvPartyBalance.Columns["type"].ToString();
                            string stramount = dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value.ToString() == null ? "" : dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value.ToString();
                            isWorkValueChanged = false;
                            dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value = stramount == "" ? "0" : Math.Round(decimal.Parse(stramount), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                            isWorkValueChanged = true;
                            if (dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "New" || dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "OnAccount")
                            {
                                isWorkValueChanged = false;
                                if (decimal.Parse(stramount) > 0)
                                {
                                    dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value = true;
                                    ToggleSelectUnselect(e.RowIndex, "UnSelect", true, stramount);
                                }
                                else
                                {
                                    dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value = false;
                                    ToggleSelectUnselect(e.RowIndex, "Select", false, stramount);
                                }
                                isWorkValueChanged = true;
                            }
                            else
                            {
                                //decimal decamountToPay = decimal.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString()) == null ? 0 : decimal.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString());
                                decimal decamount = dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value.ToString() == null ? 0 : decimal.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value.ToString());
                                if (decamount != 0)
                                    dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value = true;

                                //if (decamountToPay > 0)
                                //{
                                //    isWorkValueChanged = false;
                                //    if (decimal.Parse(stramount) > 0)
                                //        dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value = true;
                                //    else
                                //        dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value = false;
                                //    isWorkValueChanged = true;
                                //}
                                //else//for sales return
                                //{
                                //    isWorkValueChanged = false;
                                //    if (decimal.Parse(stramount) < 0)
                                //        dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value = true;
                                //    else
                                //        dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value = false;
                                //    isWorkValueChanged = true;
                                //}
                            }

                        }
                        else if (e.ColumnIndex == dgvPartyBalance.Columns["type"].Index && e.RowIndex >= 0)
                        {
                            if (dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "New" || dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "OnAccount")
                            {
                                isWorkValueChanged = false;
                                dgvPartyBalance.Rows[e.RowIndex].Cells["crOrDr"].Value = "Cr";

                                //if (FinanceSettingsInfo._UseMultiCurrency)
                                //    dgvPartyBalance.Rows[e.RowIndex].Cells["currency"].Value = "";
                                //else
                                dgvPartyBalance.Rows[e.RowIndex].Cells["currency"].Value = PublicVariables._currencyId;
                                isWorkValueChanged = true;
                            }
                        }
                        bool isOk = true;
                        if (dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value != null)
                            if (bool.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["isSelect"].Value.ToString()) == true)
                            {


                                try
                                {
                                    string strType = dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString();
                                    if (strType == "")
                                    {
                                        dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                        dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        isOk = false;
                                    }
                                }
                                catch
                                {
                                    dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                    dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    isOk = false;
                                }
                                if (dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value != null && dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() != "")
                                {
                                    if (dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "Against")
                                    {
                                        try
                                        {
                                            // Voucher Type
                                            string voucherType = dgvPartyBalance.Rows[e.RowIndex].Cells["voucherType"].Value.ToString();
                                            if (voucherType == "")
                                            {
                                                dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                                dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                                isOk = false;
                                            }
                                        }
                                        catch
                                        {
                                            dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                            dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                            isOk = false;
                                        }
                                    }


                                }

                                try
                                {
                                    //  Amount
                                    decimal Paidamount = decimal.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["amount"].Value.ToString());

                                    if (Paidamount == 0)
                                    {
                                        dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                        dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        isOk = false;
                                    }

                                    if (dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "Against")//amounttoPay only applicable for Against
                                    {
                                        decimal amountToPay = dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString() == "" ? 0 : decimal.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString());
                                        if (amountToPay < 0)//for sales return only -ve values allowd 
                                        {
                                            if (Paidamount >= 0) //check if amount is +ve or zero(not allowed)
                                            {
                                                dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                                dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                                isOk = false;
                                            }
                                        }
                                        else//others
                                        {
                                            if (dgvPartyBalance.Rows[e.RowIndex].Cells["voucherType"].Value.ToString() != "Opening Balance")
                                            {
                                                if (Paidamount > amountToPay) // over amount not allowed
                                                {
                                                    dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                                    dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                                    isOk = false;
                                                }
                                                else if (Paidamount <= 0) // -ve values  not allowed
                                                {
                                                    dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                                    dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                                    isOk = false;
                                                }
                                            }
                                        }
                                    }
                                    else// no checking with amounttoPay for "New and OnAccount"
                                    {
                                        if (Paidamount < 0)//-ve values not allowed
                                        {
                                            dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                            dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                            isOk = false;
                                        }
                                    }
                                }
                                catch
                                {
                                    dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                    dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    isOk = false;
                                }
                                try
                                {
                                    string strLedger = dgvPartyBalance.Rows[e.RowIndex].Cells["currency"].Value.ToString();
                                    if (strLedger == "")
                                    {
                                        dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                        dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        isOk = false;
                                    }
                                }
                                catch
                                {
                                    dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "x";
                                    dgvPartyBalance["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    isOk = false;
                                }

                            }
                        if (isOk)
                            dgvPartyBalance.Rows[e.RowIndex].Cells["check"].Value = "";
                    }
                    FindTotal();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR73:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FindTotal()
        {
            try
            {
                bool isSelect = false;
                decimal dTotal = 0;
                CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
                if (dgvPartyBalance.Rows.Count > 1)
                {
                    foreach (DataGridViewRow dgvrow in dgvPartyBalance.Rows)
                    {
                        if (dgvrow.Cells["amount"].Value != null)
                            if (dgvrow.Cells["amount"].Value.ToString() != "")
                            {
                                if (dgvrow.Cells["check"].Value != null)
                                    if (dgvrow.Cells["check"].Value.ToString() != "x")
                                    {
                                        if (dgvrow.Cells["isSelect"].Value != null)
                                            isSelect = bool.Parse(dgvrow.Cells["isSelect"].Value.ToString());

                                        if (isSelect)
                                        {
                                            try
                                            {
                                                string strCurrencyId = dgvrow.Cells["Currency"].Value.ToString();
                                                string strCurrencyConversionId = SPCurrencyConversion.CurrencyConversionRateIdViewByCurrencyId(strCurrencyId, Convert.ToDateTime(lblBillDate.Text), PublicVariables._branchId);
                                                CurrencyConversionInfo InfoCurrencyConversion = SPCurrencyConversion.CurrencyConversionView(strCurrencyConversionId);
                                                dTotal = dTotal + decimal.Parse(dgvrow.Cells["amount"].Value.ToString()) * InfoCurrencyConversion.Rate;


                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                            }
                    }
                    CurrencySP currencysp = new CurrencySP();
                    CurrencyInfo currencyinfo = new CurrencyInfo();
                    currencyinfo = currencysp.CurrencyView(PublicVariables._currencyId);
                    lblTotalAmount.Text = Math.Round(dTotal, currencyinfo.NoOfDecimalPlace).ToString() + " " + currencyinfo.CurrencySymbol;
                    txtTotalAmount.Text = dTotal.ToString();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR77:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void FillDatatatablesforDevPrint(bool isDuplicatePrint, string strDuplicateBillNo)
        {
            //--------Company Details Datatable--------------
            DataTable dtblCompanyDetails = new DataTable();
            BranchSP SpBranch = new BranchSP();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
            POSSalesMasterSP salesmaster = new POSSalesMasterSP();

            dtblCompanyDetails.Columns.Add("companyheader_logo");
            //dtblCompanyDetails.Columns.Add("companyfooter_logo");

            string image_path = "";

            if (!Convert.IsDBNull(dtblCompanyDetails.Rows[0]["logo"]))
            {
                byte[] bytes = (byte[])dtblCompanyDetails.Rows[0]["logo"];
                try
                {
                    Image img = Image.FromStream(new MemoryStream(bytes));
                    string folderPath = @"Images";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fullpath = Path.GetFullPath(folderPath);
                    string filename = PublicVariables._branchId + ".bmp";
                    image_path = Path.Combine(fullpath, filename);
                    img.Save(image_path, System.Drawing.Imaging.ImageFormat.Bmp);

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            //byte[] imageBytes = File.ReadAllBytes(image_path);

            dtblCompanyDetails.Rows[0]["companyheader_logo"] = image_path;



            //--------- Other Details Datatable---------------
            DataTable dtblPaymentVoucherDetails = new DataTable();

            dtblPaymentVoucherDetails.Columns.Add("ReceiptNo");
            dtblPaymentVoucherDetails.Columns.Add("Code");
            dtblPaymentVoucherDetails.Columns.Add("name");
            dtblPaymentVoucherDetails.Columns.Add("ChequeNo");
            dtblPaymentVoucherDetails.Columns.Add("ChequeDate");
            dtblPaymentVoucherDetails.Columns.Add("ReceiptMode");
            dtblPaymentVoucherDetails.Columns.Add("BillDate");
            dtblPaymentVoucherDetails.Columns.Add("BillTime");
            dtblPaymentVoucherDetails.Columns.Add("CounterId");
            dtblPaymentVoucherDetails.Columns.Add("User");
            dtblPaymentVoucherDetails.Columns.Add("PaidAmount");
            dtblPaymentVoucherDetails.Columns.Add("Narration");
            dtblPaymentVoucherDetails.Columns.Add("AmountInWords");
            dtblPaymentVoucherDetails.Columns.Add("BillName");
            // Add DataRow for dtblPaymentvoucher details
            if (isDuplicatePrint == false)
            {
                DataRow dRowDetails = dtblPaymentVoucherDetails.NewRow();
                dRowDetails["ReceiptNo"] = lblBillNo.Text;
                dRowDetails["Code"] = txtCustomerId.Text;
                dRowDetails["name"] = txtCustName.Text;
                dRowDetails["ChequeNo"] = txtChequeNo.Text;
                dRowDetails["ChequeDate"] = txtChequeDate.Text;
                string paymentMode = "";

                if (RadioBtnCash.Checked)
                {
                    paymentMode = RadioBtnCash.Text;
                }
                else if (RadioBtnUPI.Checked)
                {
                    paymentMode = RadioBtnUPI.Text;
                }
                else if (RadioBtnCreditCard.Checked)
                {
                    paymentMode = RadioBtnCreditCard.Text;
                }
                dRowDetails["ReceiptMode"] = paymentMode;
                dRowDetails["BillDate"] = lblBillDate.Text;
                dRowDetails["BillTime"] = lblBillTime.Text;
                dRowDetails["CounterId"] = lblCounter.Text;
                dRowDetails["User"] = lblUser.Text;
                dRowDetails["PaidAmount"] = txtTotalAmount.Text;
                decimal paidAmount = Convert.ToDecimal(txtTotalAmount.Text);
                dRowDetails["Narration"] = txtNarration.Text;
                dRowDetails["AmountInWords"] = new NumToText().ConvertAmountToWordsForPrint(paidAmount, "");
                dRowDetails["BillName"] = "Receipt Voucher";
                dtblPaymentVoucherDetails.Rows.Add(dRowDetails);

            }
            else
            {

                DataTable dtbl = objPOSReceiptMasterSP.GetPOSReceiptBillDetails(strDuplicateBillNo);

                if (dtbl.Rows.Count > 0)
                {

                    DataRow dRowDetails = dtblPaymentVoucherDetails.NewRow();


                    dRowDetails["ReceiptNo"] = dtbl.Rows[0]["ReceiptNo"].ToString();
                    dRowDetails["Code"] = dtbl.Rows[0]["Code"].ToString();
                    dRowDetails["name"] = dtbl.Rows[0]["Name"].ToString();
                    dRowDetails["ChequeNo"] = dtbl.Rows[0]["ChequeNo"].ToString();
                    dRowDetails["ChequeDate"] = Convert.ToDateTime(dtbl.Rows[0]["ChequeDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["ReceiptMode"] = dtbl.Rows[0]["ReceiptMode"].ToString();
                    dRowDetails["BillDate"] = Convert.ToDateTime(dtbl.Rows[0]["BillDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["BillTime"] = dtbl.Rows[0]["BillTime"].ToString();
                    dRowDetails["CounterId"] = dtbl.Rows[0]["CounterId"].ToString();
                    dRowDetails["User"] = dtbl.Rows[0]["User"].ToString();

                    decimal Amount = Convert.ToDecimal(dtbl.Rows[0]["PaidAmount"].ToString());
                    dRowDetails["PaidAmount"] = Amount.ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["Narration"] = dtbl.Rows[0]["Narration"].ToString();
                    dRowDetails["AmountInWords"] = new NumToText().ConvertAmountToWordsForPrint(Amount, "");
                    dRowDetails["BillName"] = "Receipt Voucher";

                    // Add populated DataRow to Payment Voucher DataTable
                    dtblPaymentVoucherDetails.Rows.Add(dRowDetails);


                }


            }

            DevPrint spPrint = new DevPrint();
            spPrint.PrintReceiptVoucher(dtblCompanyDetails, dtblPaymentVoucherDetails, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);





        }
        private void dgvPartyBalance_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Ensure the click is on a button cell
                if (e.ColumnIndex == dgvPartyBalance.Columns["Select"].Index && e.RowIndex >= 0)
                {
                    if (dgvPartyBalance.Rows[e.RowIndex].Cells["type"].Value.ToString() == "Against")
                    {
                        // Get the current value of the button cell
                        var currentValue = dgvPartyBalance.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                        // Toggle logic based on current button value
                        if (currentValue == "UnSelect")
                        {
                            ToggleSelectUnselect(e.RowIndex, "Select", false, "0");
                        }
                        else if (currentValue == "Select")
                        {
                            decimal balance = dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString() == "" ? 0 : decimal.Parse(dgvPartyBalance.Rows[e.RowIndex].Cells["amountToPay"].Value.ToString());
                            string roundedBalance = Math.Round(balance, SettingsInfo._roundDecimal).ToString();
                            ToggleSelectUnselect(e.RowIndex, "UnSelect", true, roundedBalance);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR38:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ToggleSelectUnselect(int rowIndex, string buttonText, bool checkBoxValue, string amountValue)
        {
            isWorkValueChanged = false;

            // Change the button text
            dgvPartyBalance.Rows[rowIndex].Cells["Select"].Value = buttonText;

            // Ensure the DataGridView commits the edit
            dgvPartyBalance.CommitEdit(DataGridViewDataErrorContexts.Commit);

            // Update the checkbox cell value
            DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)dgvPartyBalance.Rows[rowIndex].Cells["isSelect"];
            checkBoxCell.Value = checkBoxValue;

            // Update the amount cell value
            dgvPartyBalance.Rows[rowIndex].Cells["amount"].Value = amountValue;

            isWorkValueChanged = true;

            // Refresh the DataGridView to ensure the changes are displayed
            dgvPartyBalance.Refresh();
        }

        private void timerSessionDate_Tick(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(strSessionDate)) > 0)
            {
                if (lblSessionDate.ForeColor == Color.Red)
                {
                    lblSessionDate.ForeColor = Color.White;
                }
                else
                {
                    lblSessionDate.ForeColor = Color.Red;
                }

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblBillDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            lblBillTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmLookup frmlookup = new frmLookup();

            frmlookup.strSearchingName = "ReceiptVoucher";
            frmlookup.strFromFormName = "ReceiptVoucher";
            frmlookup.strSearchColumn = "receiptNo";
            frmlookup.strSearchOrder = " POSReceiptMasterId DESC ";
            frmlookup.strSearchQry = " POSReceiptMasterId,receiptNo,Date,BankCash,Narration,totalAmount ";
            string strdate = "";
            //if (InventorySettingsInfo._ShowAllTransctn)
            //{
            //    strdate = "";
            //}
            //else
            //    strdate = " AND date BETWEEN '" + PublicVariables._fromDate + "' AND '" + PublicVariables._toDate + "'";
            //frmlookup.strSearchTable = " (SELECT CONVERT(INT,R.POSReceiptMasterId) POSReceiptMasterId,R.receiptNo AS 'ReceiptNo',REPLACE(CONVERT(VARCHAR(11), R.billDate, 106), ' ', '-') AS Date,A.ledgerName AS BankCash,R.Narration,CONVERT(DECIMAL(18," + SettingsInfo._roundDecimal + "),R.totalAmount) AS totalAmount FROM dbo.tbl_POSReceiptMaster(NOLOCK) AS R,tbl_AccountLedger(NOLOCK) A WHERE A.CashOrBankledgerId=R.ledgerId  " + strdate + ") AS A ";
            frmlookup.strSearchTable = " (SELECT CONVERT(INT,R.POSReceiptMasterId) POSReceiptMasterId,R.receiptNo AS 'ReceiptNo',REPLACE(CONVERT(VARCHAR(11), R.billDate, 106), ' ', '-') AS Date,A.ledgerName AS BankCash,R.Narration,CONVERT(DECIMAL(18," + SettingsInfo._roundDecimal + "),R.totalAmount) AS totalAmount FROM dbo.tbl_POSReceiptMaster(NOLOCK) AS R,tbl_AccountLedger(NOLOCK) A WHERE A.ledgerId=R.CashOrBankledgerId  ) AS A ";
            frmlookup.strSearchCondition = "";
            frmlookup.strMasterIdColumnName = "POSReceiptMasterId";
            frmlookup.IntSearchFiledCount = 5;

            frmlookup.DoWhenComingFromReceiptForm(this);
        }
        string strMasterIdForEdit = "";
        bool isOrderOrReceiptFil = false;
        public void DowhenReturningFromSearchForm(string strId)
        {
            try
            {
                this.Enabled = true;
                if (strId != "")
                {
                    dgvPartyBalance.Rows.Clear();
                    strMasterIdForEdit = strId;
                    //isInEditMode = true;
                    btnCancel.Text = "New";
                    btnSave.Text = "Update";
                    btnDelete.Enabled = true;
                    isOrderOrReceiptFil = true;
                    FillReceipt();
                    isOrderOrReceiptFil = false;
                }

                //txtVoucherNo.Focus();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("POSR13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        bool isInEditMode = false;
        string strDetailsIdForEdit = "";
        decimal decOldAmount = 0;
        public void FillReceipt()
        {
            try
            {
                isInEditMode = true;
                // To fill all details of selected Payment voucher
                POSReceiptMasterSP SpMaster = new POSReceiptMasterSP();
                POSReceiptDetailsSP SpDetails = new POSReceiptDetailsSP();
                POSReceiptMasterInfo InfoMaster = new POSReceiptMasterInfo();
                
                PartyBalanceSP SpBalance = new PartyBalanceSP();
                CurrencyConversionSP SpCurrencyConversion = new CurrencyConversionSP();
                DataTable dtMaster = SpMaster.POSReceiptMasterViewByMasterId(strMasterIdForEdit);
                if(dtMaster.Rows.Count>0)
                {
                    strMasterIdForEdit = dtMaster.Rows[0]["POSReceiptMasterId"].ToString();
                    lblBillNo.Text = dtMaster.Rows[0]["receiptNo"].ToString();
                    lblBillDate.Text = dtMaster.Rows[0]["billDate"].ToString();
                    lblSessionDate.Text = dtMaster.Rows[0]["sessionDate"].ToString();
                    lblSessionNO.Text = dtMaster.Rows[0]["sessionNo"].ToString();
                    lblCounter.Text = dtMaster.Rows[0]["counterId"].ToString();
                    cmbCashOrBank.SelectedValue = dtMaster.Rows[0]["CashOrBankledgerId"].ToString();
                    txtNarration.Text = dtMaster.Rows[0]["narration"].ToString();
                    lblUser.Text = dtMaster.Rows[0]["userId"].ToString();
                    txtTotalAmount.Text = Math.Round((decimal.Parse(dtMaster.Rows[0]["totalAmount"].ToString().ToString())), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    decOldAmount = Math.Round((decimal.Parse(dtMaster.Rows[0]["totalAmount"].ToString().ToString())), SettingsInfo._roundDecimal);

                    DataTable dtDetails = SpDetails.POSReceiptDetailsViewByMasterId(strMasterIdForEdit);
                    if (dtDetails.Rows.Count > 0)
                    {
                        strDetailsIdForEdit = dtDetails.Rows[0]["POSReceiptDetailsId"].ToString();
                        txtCustomerId.Text = dtDetails.Rows[0]["ledgerId"].ToString();
                        lblLedgerId.Text = dtDetails.Rows[0]["ledgerId"].ToString();
                        txtCustName.Text = dtDetails.Rows[0]["ledgerName"].ToString();
                        txtChequeNo.Text = dtDetails.Rows[0]["chequeNo"].ToString();
                        txtChequeDate.Text = dtDetails.Rows[0]["chequeDate"].ToString();
                    }
                   
                    FillPartyBalanceDatatable();
                    FindTotal();
                }
              
                
            }
            catch (Exception ex) { MessageBox.Show("POSR5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
        public void CreatingPartyBalanceDatatable()
        {
            try
            {
                //// Creating a table to keep values from party balance form
                //DataColumn clmn = new DataColumn("LedgerId");
                //dtblPartyBalance.Columns.Add(clmn);
                //DataColumn clmn1 = new DataColumn("type");
                //dtblPartyBalance.Columns.Add(clmn1);
                //DataColumn clmn2 = new DataColumn("voucherType");
                //dtblPartyBalance.Columns.Add(clmn2);
                //DataColumn clmn3 = new DataColumn("voucherNo");
                //dtblPartyBalance.Columns.Add(clmn3);
                //DataColumn clmn4 = new DataColumn("amount");
                //dtblPartyBalance.Columns.Add(clmn4);
                //DataColumn clmn5 = new DataColumn("currency");
                //dtblPartyBalance.Columns.Add(clmn5);
                //DataColumn clmn6 = new DataColumn("crOrDr");
                //dtblPartyBalance.Columns.Add(clmn6);
                //DataColumn clmn7 = new DataColumn("currecyConversionId");
                //dtblPartyBalance.Columns.Add(clmn7);

                // Creating a table to keep values from party balance form
           
                DataColumn clmn1 = new DataColumn("type");
                dtblParty.Columns.Add(clmn1);
                DataColumn clmn2 = new DataColumn("voucherType");
                dtblParty.Columns.Add(clmn2);
                DataColumn clmn3 = new DataColumn("voucherNo");
                dtblParty.Columns.Add(clmn3);
                DataColumn clmn4 = new DataColumn("amount");
                dtblParty.Columns.Add(clmn4);
                DataColumn clmn5 = new DataColumn("currency");
                dtblParty.Columns.Add(clmn5);
                DataColumn clmn6 = new DataColumn("crOrDr");
                dtblParty.Columns.Add(clmn6);
                DataColumn clmn7 = new DataColumn("currecyConversionId");
                dtblParty.Columns.Add(clmn7);
                DataColumn clmn8 = new DataColumn("MasterId");
                dtblParty.Columns.Add(clmn8);
                DataColumn clmn9 = new DataColumn("VoucherDate");
                dtblParty.Columns.Add(clmn9);
                DataColumn clmn10 = new DataColumn("BillAmount");
                dtblParty.Columns.Add(clmn10);
                DataColumn clmn11 = new DataColumn("amountToPay");
                dtblParty.Columns.Add(clmn11);
                DataColumn clmn12 = new DataColumn("ReferanceNo");
                dtblParty.Columns.Add(clmn12);

            }
            catch (Exception ex)
            {
                MessageBox.Show("PB23:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillPartyBalanceDatatable()
        {
            try
            {
                // To fill party balace table details of this payment when coming from register
                PartyBalanceSP SpPartyBanace = new PartyBalanceSP();
                DataTable dtbl = SpPartyBanace.PartyBalanceByVoucherTypeAndVoucherNoNew("POSReceipt", strMasterIdForEdit, "Cr");
                dtblParty.Rows.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        DataRow nrow = dtblParty.NewRow();
                        dtblParty.Rows.Add(nrow);
                        CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
                        CurrencyConversionInfo InfoCurrencyConversion = SPCurrencyConversion.CurrencyConversionView(dtbl.Rows[i]["currecyConversionId"].ToString());
                       // nrow["LedgerId"] = dtbl.Rows[i]["ledgerId"].ToString();
                        nrow["type"] = dtbl.Rows[i]["referenceType"].ToString();
                        if (dtbl.Rows[i]["referenceType"].ToString() == "New" || dtbl.Rows[i]["referenceType"].ToString() == "OnAccount")
                        {
                            nrow["voucherType"] = "";
                            nrow["voucherNo"] = "";
                        }
                        if (dtbl.Rows[i]["referenceType"].ToString() == "Against")
                        {
                            nrow["voucherType"] = dtbl.Rows[i]["voucherType"].ToString();
                            nrow["voucherNo"] = dtbl.Rows[i]["mastervoucherNo"].ToString();

                        }
                        nrow["amount"] = Math.Round((decimal.Parse(dtbl.Rows[i]["credit"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);
                        nrow["currency"] = dtbl.Rows[i]["currencyId"].ToString();
                        nrow["crOrDr"] = "Cr";
                        nrow["currecyConversionId"] = dtbl.Rows[i]["currecyConversionId"].ToString();

                        nrow["VoucherDate"] = dtbl.Rows[i]["VoucherDate"].ToString();
                        nrow["MasterId"] = dtbl.Rows[i]["MasterId"].ToString();
                        nrow["ReferanceNo"] = dtbl.Rows[i]["ReferanceNo"].ToString();
                        nrow["BillAmount"] = Math.Round((decimal.Parse(dtbl.Rows[i]["BillAmount"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);
                        nrow["amountToPay"] = Math.Round((decimal.Parse(dtbl.Rows[i]["balance"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);

                    }
                    FillGrid(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV14:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillPartyBalanceGrid()
        {
            try
            {
                // To fill party balace table details of this payment when coming from register
                PartyBalanceSP SpPartyBanace = new PartyBalanceSP();
                DataTable dtbl = SpPartyBanace.PartyBalanceByVoucherTypeAndVoucherNoNew("POSReceipt", strMasterIdForEdit, "Cr");
                dgvPartyBalance.Rows.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                       // DataRow nrow = dtblPartyBalance.NewRow();
                        dgvPartyBalance.Rows.Add();
                        CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
                        CurrencyConversionInfo InfoCurrencyConversion = SPCurrencyConversion.CurrencyConversionView(dtbl.Rows[i]["currecyConversionId"].ToString());
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["type"].Value = dtbl.Rows[i]["referenceType"].ToString();

                      


                        if (dtbl.Rows[i]["referenceType"].ToString() == "New" || dtbl.Rows[i]["referenceType"].ToString() == "OnAccount")
                        {
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["type"].ReadOnly = false;
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["voucherType"].Value = "";
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["voucherNo"].Value = "";

                        }
                        if (dtbl.Rows[i]["referenceType"].ToString() == "Against")
                        {
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["type"].ReadOnly = true;
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["voucherType"].Value = dtbl.Rows[i]["voucherType"].ToString();
                            dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["voucherNo"].Value = dtbl.Rows[i]["mastervoucherNo"].ToString();

                        }




                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["ReferanceNo"].Value = dtbl.Rows[i]["ReferanceNo"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["MasterId"].Value = dtbl.Rows[i]["MasterId"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["VoucherDate"].Value = dtbl.Rows[i]["VoucherDate"].ToString();
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["BillAmount"].Value = Math.Round((decimal.Parse(dtbl.Rows[i]["BillAmount"].ToString()) / InfoCurrencyConversion.Rate), SettingsInfo._roundDecimal);
                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["currencyId"].Value = dtbl.Rows[i]["currencyId"].ToString();

                        dgvPartyBalance.Rows[dgvPartyBalance.Rows.Count - 1].Cells["crOrDr"].Value = "Cr";
                    


                        //nrow["amount"] = Math.Round((decimal.Parse(dtbl.Rows[i]["credit"].ToString()) / InfoCurrencyConversion.Rate), 2);
                        //nrow["currency"] = dtbl.Rows[i]["currencyId"].ToString();
                        //nrow["crOrDr"] = "Cr";
                        //nrow["currecyConversionId"] = dtbl.Rows[i]["currecyConversionId"].ToString();

                        //nrow["VoucherDate"] = dtbl.Rows[i]["VoucherDate"].ToString();
                        //nrow["MasterId"] = dtbl.Rows[i]["MasterId"].ToString();
                        //nrow["ReferanceNo"] = dtbl.Rows[i]["ReferanceNo"].ToString();
                        //nrow["BillAmount"] = Math.Round((decimal.Parse(dtbl.Rows[i]["BillAmount"].ToString()) / InfoCurrencyConversion.Rate), 2);
                        //nrow["amountToPay"] = Math.Round((decimal.Parse(dtbl.Rows[i]["balance"].ToString()) / InfoCurrencyConversion.Rate), 2);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV14:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLoadPending_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblLedgerId.Text != "")
                {
                    dtblParty = spPartyBalance.PartyBalanceViewByLedgerIdNew(lblLedgerId.Text, "Cr");
                    if (dtblParty.Rows.Count > 0)
                        FillGrid(true);
                    else
                        MessageBox.Show("No Pending Documents Found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Please select a ledger", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("PB75:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Calling delete function
                DeleteFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV45:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        clsGeneral spGeneral = new clsGeneral();

        public void DeleteFunction()
        {
            try
            {
                // To delete the current payment voucher
                bool isOk = true;

                if (isOk)
                {
                    // Doing deleting of Payment
                    bool isExistInterest = false;
                    bool isExistParty = false;
                    POSReceiptMasterSP SpMaster = new POSReceiptMasterSP();
                    if (strMasterIdForEdit != "")
                    {
                        if (MessageBox.Show("Do you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            //CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
                            //if (checkuserprivilege.CheckPrivilage("Receipt Voucher", "Delete") == true)
                            //UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();//changed on 19/10/2023 sheena
                            //if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, "Delete", PublicVariables._mainMenuItem) == true)
                            //{
                            //    if (isInEditMode && SpMaster.ReceiptVoucherCheckRreference(strMasterIdForEdit) == true)
                            //    {
                            //        MessageBox.Show("Can't delete,reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    }
                            //    else
                            //    {

                            if (dtblParty.Rows.Count > 0)
                            {
                                // Checking whether the party balance have any reference or not
                                PartyBalanceSP SpBalance = new PartyBalanceSP();
                                foreach (DataRow dRow in dtblParty.Rows)
                                {
                                    if (dRow.ItemArray[1].ToString() == "New")
                                    {
                                        isExistParty = SpBalance.PartyBalanceReferenceCheck(strMasterIdForEdit, "POSReceipt");
                                        if (isExistParty)
                                            break;
                                    }
                                }
                            }
                            if (!isExistInterest && !isExistParty)
                            {
                                // Delete receipt voucher if no reference exist
                                SpMaster.POSReceiptVoucherDeleteDetails(strMasterIdForEdit);
                                spGeneral.UserActivityAdd("POSReceipt", "Delete", strMasterIdForEdit, lblBillNo.Text, Convert.ToDateTime(lblBillDate.Text), decimal.Parse(txtTotalAmount.Text), decimal.Parse(txtTotalAmount.Text));
                                MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //if (isInEditMode)                                           
                                ClearFunction();


                            }
                            else
                            {
                                MessageBox.Show("Can't delete, reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //}

                    }
                    else
                        MessageBox.Show("Please select a document", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("PB27:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lnklblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // To delete selected row from the dgvPriceList grid
                if (dgvPartyBalance.SelectedCells.Count > 0 && dgvPartyBalance.CurrentRow != null)
                {
                    int inRowIndex = dgvPartyBalance.CurrentRow.Index;
                    if (dgvPartyBalance.RowCount > 1)
                    {
                        if (SettingsInfo._messageBoxRowRemove)
                        {
                            if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                RemoveRow();
                            }
                        }
                        else
                        {
                            RemoveRow();
                        }
                    }
                    else
                    {
                        if (dgvPartyBalance.CurrentRow.Cells["voucherType"].Value != null)
                        {
                            if (SettingsInfo._messageBoxRowRemove)
                            {
                                if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    dgvPartyBalance.Rows.Clear();
                                }
                            }
                            else
                            {
                                dgvPartyBalance.Rows.Clear();
                            }
                        }
                    }
                    FindTotal();
                    dgvPartyBalance.Focus();
                    dgvPartyBalance.CurrentCell = dgvPartyBalance.Rows[inRowIndex].Cells["type"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PB34:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void RemoveRow()
        {
            try
            {
                // To remove the the selected row
                if (!dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].IsNewRow)
                {
                    // In the case of new row
                    dgvPartyBalance.Rows.RemoveAt(dgvPartyBalance.CurrentRow.Index);
                }
                else
                {
                    // In the case of values in cells of the row
                    isWorkValueChanged = false;
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["check"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["type"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["voucherType"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["voucherNo"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["amountToPay"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["amount"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["crOrDr"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["MasterId"].Value = "";
                    dgvPartyBalance.Rows[dgvPartyBalance.CurrentRow.Index].Cells["BillAmount"].Value = "";

                    isWorkValueChanged = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PB21:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text == "Cancel")
            {
                formCancel = true;
                this.Close();
            }else if(btnCancel.Text=="New")
            {
                ClearFunction();
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ClearFunction();
        }

        private void txtCustomerId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCustName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtChequeNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtChequeDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbCashOrBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTotalAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        e.Handled = true;
            //        this.SelectNextControl((Control)sender, true, true, true, true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("RV50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void linkLabelRePrint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string duplicateBillNo = lblBillNo.Text;


            FillDatatatablesforDevPrint(true, duplicateBillNo);
        }
    }
}
