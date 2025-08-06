using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using DevExpress.Utils.About;
using System.IO;
//using FinacPOS.Classes.Info;

namespace FinacPOS
{
    public partial class frmPOSCounter : Form
    {
        public frmPOSCounter()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);
        }
        public void setLanguage(String language)
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            if (language == "ARB")
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;
            }
            //this.Controls.Clear();
        }
        bool isInEditMode;
        string strCounterIdToEdit;
        int inFooterDetailsCount = 0; // to hold count of enter keys presses

        POSCounterInfo counterInfo = new POSCounterInfo();
        POSCounterSP counterSP = new POSCounterSP();
        ComboValidation objComboValidation = new ComboValidation();
        frmCountersReport objfrmcounter = null;
        PosCounterPrinterDetailsInfo counterprinterDetailsInfo = new PosCounterPrinterDetailsInfo();
        private void txtCounterId_Enter(object sender, EventArgs e)
        {
            lblCounterId.ForeColor = System.Drawing.Color.Red;
        }

        private void txtCounterId_Leave(object sender, EventArgs e)
        {
            lblCounterId.ForeColor = System.Drawing.Color.Black;
        }

        private void frmPOSCounter_Load(object sender, EventArgs e)
        {
            clsGeneral objGeneral = new clsGeneral();
            objGeneral.formSettings(this);

            lblCounterId.ForeColor = System.Drawing.Color.Red;

            FillLedgerUnderGroupCombo(cmbSalesAccount, "25"); // added  on  29/03/2025
            FillPaymentModeCombo(cmbCashAccount);
            FillPaymentModeCombo(cmbBankAccount);
            FillPaymentModeCombo(cmbUpiAccount);
            cmbSalesType.SelectedItem = "Type1";
            UpdateCounterPrinterGroupBoxStatus();
            FillCounterPrinterComboBoxes();
            Clear();
        }

        private void txtCounterName_Enter(object sender, EventArgs e)
        {
            lblCounterName.ForeColor = System.Drawing.Color.Red;
        }

        private void txtCounterName_Leave(object sender, EventArgs e)
        {
            lblCounterName.ForeColor = System.Drawing.Color.Black;
        }

        private void txtSystName_Leave(object sender, EventArgs e)
        {
            lblSystName.ForeColor = System.Drawing.Color.Black;
        }

        private void txtSystName_Enter(object sender, EventArgs e)
        {
            lblSystName.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbSaleBillType_Leave(object sender, EventArgs e)
        {
            lblSaleBillType.ForeColor = System.Drawing.Color.Black;
        }

        private void cmbSaleBillType_Enter(object sender, EventArgs e)
        {
            lblSaleBillType.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbRefundBillType_Leave(object sender, EventArgs e)
        {
            lblRefundBillType.ForeColor = System.Drawing.Color.Black;
        }

        private void cmbRefundBillType_Enter(object sender, EventArgs e)
        {
            lblRefundBillType.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbDisplayPort_Leave(object sender, EventArgs e)
        {
            lblDisplayPort.ForeColor = System.Drawing.Color.Black;
        }

        private void cmbDisplayPort_Enter(object sender, EventArgs e)
        {
            lblDisplayPort.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbDefaltPRinter_Leave(object sender, EventArgs e)
        {
            lblDefaultPrinter.ForeColor = System.Drawing.Color.Black;
        }

        private void cmbDefaltPRinter_Enter(object sender, EventArgs e)
        {
            lblDefaultPrinter.ForeColor = System.Drawing.Color.Red;
        }

        private void txtFootrDetails_Leave(object sender, EventArgs e)
        {
            lblFooterDetails.ForeColor = System.Drawing.Color.Black;
        }

        private void txtFootrDetails_Enter(object sender, EventArgs e)
        {
            lblFooterDetails.ForeColor = System.Drawing.Color.Red;
        }

        private void chkDisplayStatus_Leave(object sender, EventArgs e)
        {
            chkDisplayStatus.ForeColor = System.Drawing.Color.Black;
        }

        private void chkDisplayStatus_Enter(object sender, EventArgs e)
        {
            chkDisplayStatus.ForeColor = System.Drawing.Color.Red;
        }

        private void chkCashDrStatus_Enter(object sender, EventArgs e)
        {
            chkCashDrStatus.ForeColor = System.Drawing.Color.Red;
        }

        private void chkCashDrStatus_Leave(object sender, EventArgs e)
        {
            chkCashDrStatus.ForeColor = System.Drawing.Color.Black;
        }

        private void txtCounterId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtCounterName.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCounterName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtCounterName.Text.Trim() == "" || txtCounterName.SelectionStart == 0)
                    {

                        txtCounterId.Focus();
                        txtCounterId.SelectionStart = txtCounterId.Text.Trim().Length;
                        txtCounterId.SelectionLength = 0;

                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtSystName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtSystName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtSystName.Text.Trim() == "" || txtSystName.SelectionStart == 0)
                    {

                        txtCounterName.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    cmbSaleBillType.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbSaleBillType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    txtSystName.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    cmbRefundBillType.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbRefundBillType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    cmbSaleBillType.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    chkDisplayStatus.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkDisplayStatus_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    cmbRefundBillType.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    cmbDisplayPort.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbDisplayPort_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    chkDisplayStatus.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    ChkshowProductInSalesinvoice.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkCashDrStatus_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    chkStatus.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    chkDisplayStatus.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbDefaltPRinter_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    cmbRefundBillType.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    cmbSalesType.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtFootrDetails_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    TxtReturnprintcopy.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    chkStatus.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    chkStatus.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillDefaultPrinters()
        {
            cmbDefaltPRinter.Items.Clear();
            PrintDocument prtdoc = new PrintDocument();
            string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;

            foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cmbDefaltPRinter.Items.Add(strPrinter);
                if (strPrinter == strDefaultPrinter)
                {
                    cmbDefaltPRinter.SelectedIndex = cmbDefaltPRinter.Items.IndexOf(strPrinter);
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveOrEdit();
        }

        public void Clear()
        {
            txtCounterId.ReadOnly = false;
            txtCounterId.Enabled = true;
            txtCounterId.Clear();
            txtCounterName.Clear();
            txtSystName.Clear();
            txtFootrDetails.Clear();
            txtFooterH.Text = "0";
            chkCashDrStatus.Checked = false;
            chkDisplayStatus.Checked = false;
            chkStatus.Checked = true;
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
            isInEditMode = false;
            strCounterIdToEdit = "";
            btnDelete.Enabled = false;
            cmbDefaltPRinter.SelectedIndex = -1;
            cmbSaleBillType.SelectedIndex = -1;
            cmbRefundBillType.SelectedIndex = -1;
            cmbDisplayPort.SelectedIndex = -1;
            cmbSalesAccount.SelectedValue = 7;
            cmbCashAccount.SelectedValue = 1;
            cmbBankAccount.SelectedValue = 1;
            cmbUpiAccount.SelectedValue = 1;
            txtSalesPrintcopy.Text = "1";
            TxtReturnprintcopy.Text = "1";
            //  cmbSalesType.SelectedIndex = -1;
            FillDefaultPrinters();
            txtCounterId.Focus();
            foreach (DataGridViewRow row in dgvPosCounterPrinterDetails.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.Cells["ProductCategory"].Value = null;
                    row.Cells["DefaultPrinters"].Value = null;
                }
            }


            dgvPosCounterPrinterDetails.Rows.Clear();
        }

        private void chkStatus_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    txtFootrDetails.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    chkCashDrStatus.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkStatus_Enter(object sender, EventArgs e)
        {
            chkStatus.ForeColor = System.Drawing.Color.Red;
        }

        private void chkStatus_Leave(object sender, EventArgs e)
        {
            chkStatus.ForeColor = System.Drawing.Color.Black;
        }

        public void SaveOrEdit()
        {
            // Doing Save Or Edit
            txtCounterId.Text = txtCounterId.Text.Trim();
            txtCounterName.Text = txtCounterName.Text.Trim();
            if (txtCounterId.Text == "")
            {
                MessageBox.Show("Enter Counter Id", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCounterId.Focus();
            }
            else if (txtCounterName.Text == "")
            {
                MessageBox.Show("Enter Counter Name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCounterName.Focus();
            }
            else if (cmbSalesAccount.SelectedValue == null)
            {
                MessageBox.Show(" Select Sales Account", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSalesAccount.Focus();
            }
            else if (cmbCashAccount.SelectedValue == null)
            {
                MessageBox.Show("Select Cash Account", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbCashAccount.Focus();
            }
            else if (cmbBankAccount.SelectedValue == null)
            {
                MessageBox.Show("Select Bank Account", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbBankAccount.Focus();
            }
            else if (cmbUpiAccount.SelectedValue == null)
            {
                MessageBox.Show("Select Upi Account", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbUpiAccount.Focus();
            }

            else if (txtSalesPrintcopy.Text == "")
            {
                MessageBox.Show("Select Counter Id", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCounterId.Focus();
            }
            else
            {
                bool isSave = true;
                // Asking confirmation if settings is true
                //if (SettingsInfo._messageBoxAddEdit)
                //{
                if (isInEditMode)
                {
                    if (MessageBox.Show("Do you want to update?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        isSave = false;
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        isSave = false;
                    }
                }
                if (isSave)
                {
                    counterInfo = new POSCounterInfo();
                    counterInfo.CounterId = txtCounterId.Text;
                    counterInfo.CounterName = txtCounterName.Text;
                    counterInfo.SystemName = txtSystName.Text;
                    counterInfo.SaleBillType = cmbSaleBillType.Text.ToString();
                    counterInfo.RefundBillType = cmbRefundBillType.Text.ToString();
                    counterInfo.DisplayStatus = chkDisplayStatus.Checked;
                    counterInfo.DisplayPort = cmbDisplayPort.Text;
                    counterInfo.CashDrawerStatus = chkCashDrStatus.Checked;
                    counterInfo.DefaultPrinter = cmbDefaltPRinter.Text.ToString();
                    counterInfo.FooterDetails = txtFootrDetails.Text;
                    counterInfo.FooterH = Convert.ToInt32(txtFooterH.Text);
                    counterInfo.Status = chkStatus.Checked;
                    counterInfo.BranchId = PublicVariables._branchId;
                    counterInfo.ShowProductInSalesInvoice = ChkshowProductInSalesinvoice.Checked;
                    counterInfo.Directprint = Chkdirectprint.Checked;
                    counterInfo.SalesAccountLedgerId = cmbSalesAccount.SelectedValue.ToString();
                    counterInfo.CashAccountLedgerId = cmbCashAccount.SelectedValue.ToString();
                    counterInfo.BankAccountLedgerId = cmbBankAccount.SelectedValue.ToString();
                    counterInfo.UPIAccountLedgerId = cmbUpiAccount.SelectedValue.ToString();
                    counterInfo.SalesPrintCopy = Convert.ToInt32(txtSalesPrintcopy.Text);
                    counterInfo.SalesReturnPrintCopy = Convert.ToInt32(TxtReturnprintcopy.Text);
                    counterInfo.SalesType = cmbSalesType.Text.ToString();
                    counterInfo.ProductSearchWithImage = ChkProductSearchWithImage.Checked;
                    counterInfo.ShowPrefixInBillNo = ChkShowPrefixInBillNo.Checked;
                    counterInfo.CategoryWaysPrint = chkCategoryWaysPrint.Checked;
                    counterInfo.KOTPrint = chkkotPrint.Checked;
                    counterInfo.ShowPreview = chkPreview.Checked;

                }

                if (!isInEditMode)
                {
                    if (counterSP.CheckExistanceOfCounterID(txtCounterId.Text))
                    {
                        MessageBox.Show("Counter Id already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCounterId.Focus();
                        txtCounterId.SelectAll();
                    }
                    else
                    {
                        // Save 
                        counterSP.POSCounterAdd(counterInfo);
                        if(dgvPosCounterPrinterDetails.Rows.Count >0 )
                        {
                            foreach (DataGridViewRow row in dgvPosCounterPrinterDetails.Rows)
                            {
                                if (row.IsNewRow) continue;

                                string productCategory = row.Cells["ProductCategory"].Value?.ToString()?.Trim();
                                string defaultPrinter = row.Cells["DefaultPrinters"].Value?.ToString()?.Trim();


                                if (string.IsNullOrWhiteSpace(productCategory) || string.IsNullOrWhiteSpace(defaultPrinter))
                                    continue;
                                PosCounterPrinterDetailsInfo counterprinterDetailsInfo = new PosCounterPrinterDetailsInfo();
                                counterprinterDetailsInfo.CounterId = txtCounterId.Text;
                                counterprinterDetailsInfo.ProductGroupCategory = productCategory;
                                counterprinterDetailsInfo.DefaultPrinter = defaultPrinter;
                                counterprinterDetailsInfo.extra1 = "";
                                counterprinterDetailsInfo.extra2 = "";

                                counterSP.POSCounterPrinterDetailsAdd(counterprinterDetailsInfo);

                            }
                        }
                       
                        MessageBox.Show("Counter created successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Added on 04/May/2025
                        POSCounterInfo Info = new POSCounterInfo();
                        Info = counterSP.POSCounterViewBySystemName();
                        if (Info.CounterId != null)
                        {
                            PublicVariables._counterId = Info.CounterId;
                            PublicVariables._counterName = Info.CounterName;
                            PublicVariables._SalesScreenType = Info.SalesType;

                        }
                        //

                        Clear();
                    }
                }
                else
                {
                    counterSP.POSCounterEdit(counterInfo);
                    counterSP.POSCounterPrinterDetailsDelete(strCounterIdToEdit);
                    foreach (DataGridViewRow row in dgvPosCounterPrinterDetails.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string productCategory = row.Cells["ProductCategory"].Value?.ToString()?.Trim();
                        string defaultPrinter = row.Cells["DefaultPrinters"].Value?.ToString()?.Trim();


                        if (string.IsNullOrWhiteSpace(productCategory) || string.IsNullOrWhiteSpace(defaultPrinter))
                            continue;
                        PosCounterPrinterDetailsInfo counterprinterDetailsInfo = new PosCounterPrinterDetailsInfo();
                        counterprinterDetailsInfo.CounterId = txtCounterId.Text;
                        counterprinterDetailsInfo.ProductGroupCategory = productCategory;
                        counterprinterDetailsInfo.DefaultPrinter = defaultPrinter;
                        counterprinterDetailsInfo.extra1 = "";
                        counterprinterDetailsInfo.extra2 = "";

                        counterSP.POSCounterPrinterDetailsAdd(counterprinterDetailsInfo);

                    }
                    MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Added on 04/May/2025
                    POSCounterInfo Info = new POSCounterInfo();
                    Info = counterSP.POSCounterViewBySystemName();
                    if (Info.CounterId != null)
                    {
                        PublicVariables._counterId = Info.CounterId;
                        PublicVariables._counterName = Info.CounterName;
                        PublicVariables._SalesScreenType = Info.SalesType;

                    }
                    //
                    Clear();
                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                frmCountersReport frmobj = new frmCountersReport();
                frmobj.MdiParent = MDIFinacPOS.ActiveForm;
                frmCountersReport open = Application.OpenForms["frmCountersReport"] as frmCountersReport;
                if (open == null)
                {
                    frmobj.WindowState = FormWindowState.Normal;
                    frmobj.MdiParent = MDIFinacPOS.ActiveForm;
                    frmobj.CallFromPOSCounter(this);
                }
                else
                {
                    open.MdiParent = MDIFinacPOS.ActiveForm;
                    open.CallFromPOSCounter(this);
                    open.BringToFront();
                    if (open.WindowState == FormWindowState.Minimized)
                    {
                        open.WindowState = FormWindowState.Normal;
                    }
                }
                this.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PU76:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void CallFromPOSCounterReport(frmCountersReport frmcounterReport, string StrCounterId)
        {
            try
            {
                isInEditMode = true;
                this.objfrmcounter = frmcounterReport;
                base.Show();
                this.Enabled = true;
                this.BringToFront();
                FillCounterForEdit(StrCounterId);

                txtCounterName.Focus();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                objfrmcounter.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("FM12:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void FillPaymentModeCombo(ComboBox cmb)
        {
            try
            {
                cmb.DataSource = null;
                AccountLedgerSP spledger = new AccountLedgerSP();
                DataTable ledgerTable = spledger.AccountLedgerGetCashBankAndODWithBalance(PublicVariables._branchId);
                cmb.DataSource = ledgerTable;
                cmb.DisplayMember = "ledgerName";
                cmb.ValueMember = "ledgerId";
                cmb.SelectedValue = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void FillCounterForEdit(string StrCounterId)
        {
            isInEditMode = true;
            strCounterIdToEdit = txtCounterId.Text = StrCounterId;
            txtCounterId.ReadOnly = true;
            POSCounterInfo InfoPOSCounter = new POSCounterInfo();
            InfoPOSCounter = counterSP.POSCounterViewbyCounterId(txtCounterId.Text);
            if (InfoPOSCounter.CounterId != null)
            {
                btnSave.Text = "Update";
                btnClear.Text = "New";
                btnDelete.Enabled = true;
                txtCounterName.Text = InfoPOSCounter.CounterName;
                txtSystName.Text = InfoPOSCounter.SystemName;
                cmbSaleBillType.Text = InfoPOSCounter.SaleBillType;
                cmbRefundBillType.Text = InfoPOSCounter.RefundBillType;
                chkDisplayStatus.Checked = InfoPOSCounter.DisplayStatus;
                cmbDisplayPort.Text = InfoPOSCounter.DisplayPort;
                chkCashDrStatus.Checked = InfoPOSCounter.CashDrawerStatus;
                cmbDefaltPRinter.Text = InfoPOSCounter.DefaultPrinter.ToString();
                txtFootrDetails.Text = InfoPOSCounter.FooterDetails.ToString();
                txtFooterH.Text = InfoPOSCounter.FooterH.ToString();
                chkStatus.Checked = InfoPOSCounter.Status;
                ChkshowProductInSalesinvoice.Checked = InfoPOSCounter.ShowProductInSalesInvoice;
                Chkdirectprint.Checked = InfoPOSCounter.Directprint;
                cmbSalesAccount.SelectedValue = InfoPOSCounter.SalesAccountLedgerId;
                cmbCashAccount.SelectedValue = InfoPOSCounter.CashAccountLedgerId;
                cmbBankAccount.SelectedValue = InfoPOSCounter.BankAccountLedgerId;
                cmbUpiAccount.SelectedValue = InfoPOSCounter.UPIAccountLedgerId;
                txtSalesPrintcopy.Text = InfoPOSCounter.SalesPrintCopy.ToString();
                TxtReturnprintcopy.Text = InfoPOSCounter.SalesReturnPrintCopy.ToString();
                cmbSalesType.Text = InfoPOSCounter.SalesType;
                ChkProductSearchWithImage.Checked = InfoPOSCounter.ProductSearchWithImage;
                chkCategoryWaysPrint.Checked = InfoPOSCounter.CategoryWaysPrint;
                chkkotPrint.Checked = InfoPOSCounter.KOTPrint;
                chkPreview.Checked = InfoPOSCounter.ShowPreview;

                dgvPosCounterPrinterDetails.Rows.Clear();
                DataTable dt = counterSP.POSCounterDetailsViewByCounterId(strCounterIdToEdit);
                foreach (DataRow row in dt.Rows)
                {
                    dgvPosCounterPrinterDetails.Rows.Add(
                        row["ProductGroupCategory"].ToString(),
                        row["DefaultPrinter"].ToString()
                    );
                }
            }

        }
        public void FillLedgerUnderGroupCombo(ComboBox cmbCurr, string strGroupId)
        {
            try
            {

                cmbCurr.DataSource = null;
                AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
                DataTable dtblCashOrParty = SpAccountLedger.AccountLedgerGetWithBalanceByGroupId(strGroupId, PublicVariables._branchId);
                cmbCurr.DataSource = dtblCashOrParty;

                if (dtblCashOrParty.Rows.Count > 0)
                {
                    cmbCurr.DisplayMember = "ledgerName";
                    cmbCurr.ValueMember = "ledgerId";

                    cmbSalesAccount.SelectedValue = "7";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DeleteCounter()
        {
            try
            {
                counterSP.POSCounterPrinterDetailsDelete(strCounterIdToEdit);
                counterSP.POSCounterDelete(txtCounterId.Text);

                MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (objfrmcounter != null)
                {
                    this.Close();
                }
                else
                {
                    strCounterIdToEdit = "";
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (isInEditMode)
            {
                if (MessageBox.Show("Do you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    DeleteCounter();

                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("AG7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtFooterH_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtFooterH.Text);
            }
            catch
            {
                txtFooterH.Text = "0";
            }
        }

        private void txtFooterH_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtFootrDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                inFooterDetailsCount++;
                if (inFooterDetailsCount == 2)
                {
                    inFooterDetailsCount = 0;
                    btnSave.Focus();
                }
            }
            else
            {
                inFooterDetailsCount = 0;
            }
        }

        private void ChkshowProductInSalesinvoice_Enter(object sender, EventArgs e)
        {
            ChkshowProductInSalesinvoice.ForeColor = System.Drawing.Color.Red;
        }

        private void ChkshowProductInSalesinvoice_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    txtFootrDetails.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    Chkdirectprint.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ChkshowProductInSalesinvoice_Leave(object sender, EventArgs e)
        {
            ChkshowProductInSalesinvoice.ForeColor = System.Drawing.Color.Black;
        }

        private void Chkdirectprint_Enter(object sender, EventArgs e)
        {
            Chkdirectprint.ForeColor = System.Drawing.Color.Red;
        }

        private void Chkdirectprint_Leave(object sender, EventArgs e)
        {
            Chkdirectprint.ForeColor = System.Drawing.Color.Black;
        }

        private void Chkdirectprint_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    txtFootrDetails.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    btnSave.Focus();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbSalesAccount_Enter(object sender, EventArgs e)
        {
            lblSalesAccount.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbSalesAccount_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Back)
            {

                cmbDefaltPRinter.Focus();

            }
            else if (e.KeyCode == Keys.Enter)
            {

                cmbCashAccount.Focus();

            }

        }

        private void cmbSalesAccount_Leave(object sender, EventArgs e)
        {
            lblSalesAccount.ForeColor = System.Drawing.Color.Black;
        }

        private void cmbCashAccount_Enter(object sender, EventArgs e)
        {
            lblCashAccount.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbCashAccount_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Back)
            {

                cmbSalesAccount.Focus();

            }
            else if (e.KeyCode == Keys.Enter)
            {

                cmbBankAccount.Focus();

            }
        }

        private void cmbCashAccount_Leave(object sender, EventArgs e)
        {
            lblCashAccount.ForeColor = System.Drawing.Color.Black;
        }

        private void cmbBankAccount_Enter(object sender, EventArgs e)
        {
            lblBankAccount.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbBankAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {

                cmbCashAccount.Focus();

            }
            else if (e.KeyCode == Keys.Enter)
            {

                cmbUpiAccount.Focus();

            }
        }

        private void cmbBankAccount_Leave(object sender, EventArgs e)
        {
            lblBankAccount.ForeColor = System.Drawing.Color.Black;
        }

        private void cmbUpiAccount_Enter(object sender, EventArgs e)
        {
            lblUPIAccount.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbUpiAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {

                cmbBankAccount.Focus();

            }
            else if (e.KeyCode == Keys.Enter)
            {

                txtSalesPrintcopy.Focus();

            }
        }

        private void cmbUpiAccount_Leave(object sender, EventArgs e)
        {
            lblUPIAccount.ForeColor = System.Drawing.Color.Black;
        }

        private void txtSalesPrintcopy_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.IntigerFieldKeypress(sender, e);

        }

        private void txtSalesPrintcopy_Enter(object sender, EventArgs e)
        {
            lblSalesPrintCopy.ForeColor = System.Drawing.Color.Red;
        }

        private void txtSalesPrintcopy_Leave(object sender, EventArgs e)
        {
            lblSalesPrintCopy.ForeColor = System.Drawing.Color.Black;
        }

        private void TxtReturnprintcopy_Enter(object sender, EventArgs e)
        {
            lblSalesReturnPrintcopy.ForeColor = System.Drawing.Color.Red;
        }

        private void TxtReturnprintcopy_Leave(object sender, EventArgs e)
        {
            lblSalesReturnPrintcopy.ForeColor = System.Drawing.Color.Black;
        }

        private void TxtReturnprintcopy_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.IntigerFieldKeypress(sender, e);
        }

        private void txtSalesPrintcopy_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                TxtReturnprintcopy.Focus();

            }
        }

        private void TxtReturnprintcopy_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                txtFootrDetails.Focus();

            }
        }

        private void cmbSalesType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {

                cmbDefaltPRinter.Focus();

            }
            else if (e.KeyCode == Keys.Enter)
            {

                cmbSalesAccount.Focus();

            }
        }

        private void cmbSalesType_Enter(object sender, EventArgs e)
        {
            lblSalesType.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbSalesType_Leave(object sender, EventArgs e)
        {
            lblSalesType.ForeColor = System.Drawing.Color.Black;
        }

        private void ChkProductSearchWithImage_Enter(object sender, EventArgs e)
        {

            ChkProductSearchWithImage.ForeColor = System.Drawing.Color.Red;
        }

        private void ChkProductSearchWithImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {

                TxtReturnprintcopy.Focus();

            }
            else if (e.KeyCode == Keys.Enter)
            {

                txtFootrDetails.Focus();

            }
        }

        private void ChkProductSearchWithImage_Leave(object sender, EventArgs e)
        {
            ChkProductSearchWithImage.ForeColor = System.Drawing.Color.Black;
        }

        private void FillCounterPrinterComboBoxes()
        {
            try
            {

                ProductSP objProductSp = new ProductSP();

                DataTable PdtCtgryDtble = new DataTable();
                PdtCtgryDtble = objProductSp.ProductGroupViewAllByCategorys("Category 4");
                DataGridViewComboBoxColumn comboxCol = dgvPosCounterPrinterDetails.Columns["ProductCategory"] as DataGridViewComboBoxColumn;

                if (comboxCol != null)
                {
                    comboxCol.DataSource = PdtCtgryDtble;
                    comboxCol.DisplayMember = "groupName";
                    comboxCol.ValueMember = "groupId";
                }


                DataGridViewComboBoxColumn printerCol = dgvPosCounterPrinterDetails.Columns["DefaultPrinters"] as DataGridViewComboBoxColumn;
                if (printerCol != null)
                {
                    printerCol.Items.Clear();
                    foreach (string strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                    {
                        printerCol.Items.Add(strPrinter);
                    }
                }

            }

            catch { }

        }
        private void UpdateCounterPrinterGroupBoxStatus()
        {
            groupBoxCountrprinter.Enabled = chkCategoryWaysPrint.Checked;
        }
        private void chkCategoryWaysPrint_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCounterPrinterGroupBoxStatus();


        }

    
      
        private void dgvPosCounterPrinterDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
         
            if (e.ColumnIndex == dgvPosCounterPrinterDetails.Columns["ProductCategory"].Index)
            {
                var currentCell = dgvPosCounterPrinterDetails.Rows[e.RowIndex].Cells[e.ColumnIndex];
                string newValue = currentCell.Value?.ToString()?.Trim();

                if (string.IsNullOrWhiteSpace(newValue)) return;

                for (int i = 0; i < dgvPosCounterPrinterDetails.Rows.Count; i++)
                {
                    if (i == e.RowIndex) continue;

                    var otherCell = dgvPosCounterPrinterDetails.Rows[i].Cells["ProductCategory"];
                    string existingValue = otherCell.Value?.ToString()?.Trim();

                    if (string.Equals(existingValue, newValue, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("This Product Category is already selected in another row.",
                                        "Duplicate Entry",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);

                        // Clear the cell
                        dgvPosCounterPrinterDetails.Rows[e.RowIndex].Cells["ProductCategory"].Value = null;
                        break;
                    }
                }
            }
        }

        private void lnklblUnitRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                if (dgvPosCounterPrinterDetails.SelectedCells.Count > 0)
                {
                    int selectedRowIndex = dgvPosCounterPrinterDetails.SelectedCells[0].RowIndex;

                    DataGridViewRow selectedRow = dgvPosCounterPrinterDetails.Rows[selectedRowIndex];

                    if (!selectedRow.IsNewRow)
                    {
                        DialogResult result = MessageBox.Show(
                            "Do you want to remove the selected row?",  "Remove Row", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            dgvPosCounterPrinterDetails.Rows.RemoveAt(selectedRowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing row: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
     

}
