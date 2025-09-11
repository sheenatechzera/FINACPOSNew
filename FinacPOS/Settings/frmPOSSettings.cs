using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.CodeParser;
using DevExpress.Xpo;

namespace FinacPOS
{
    public partial class frmPOSSettings : Form
    {
        public frmPOSSettings()
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
        POSSettingsInfo infoPOS = new POSSettingsInfo();
        public static MDIFinacPOS MDIObj;
        ComboValidation objComboValidation = new ComboValidation();
        POSSettingsSP spPOSSettings = new POSSettingsSP();
       POSSalesMasterInfo objsalesmasterinfo = new POSSalesMasterInfo();
        private void btnSave_Click(object sender, EventArgs e)
        {

            POSSettingsInfo infoPOS = new POSSettingsInfo();
            infoPOS.POSSettingsId = lblPOSSettingsId.Text.ToString();
            infoPOS.BillClearAuth = chkBillClearAuth.Checked;
            infoPOS.DiscountAuth = chkDiscountAuth.Checked;
            infoPOS.HoldBillAuth = chkHoldBillAuth.Checked;
            infoPOS.PriceChangeAuth = chkPriceChangeAuth.Checked;
            infoPOS.CreditSalesAuth = chkCreditSalesAuth.Checked;
            infoPOS.ZeroStockAuth = chkZeroStockAuth.Checked;
            infoPOS.ItemGroupinginPrint = chkItemGrouping.Checked;
            infoPOS.lastBillPrintAuth = chklastBillPrintAuth.Checked;
            infoPOS.ExchangeItemAuth = chkExchangeItemAuth.Checked;
            infoPOS.CashBoxOpenAuth = chkCashBoxOpenAuth.Checked;
            infoPOS.QtyChangeAuth = chkQtyChangeAuth.Checked;
            infoPOS.CNDays = int.Parse(txtExpiry.Text);
            infoPOS.POSCompanyName = txtCompanyName.Text.ToString().Trim();
            infoPOS.POSCompanyNameArabic = txtCompanyArabic.Text.ToString().Trim();
            infoPOS.POSAddress = txtAddress.Text.ToString().Trim();
            infoPOS.POSPhone = txtPhone.Text.ToString().Trim();
            infoPOS.CompanyH = int.Parse(txtCompanyH.Text);
            infoPOS.CompanyArabicH = int.Parse(txtCompanyArabicH.Text);
            infoPOS.AddressH = int.Parse(txtAddressH.Text);
            infoPOS.PhoneH = int.Parse(txtPhoneH.Text);
            infoPOS.CompanyVisible = chkCompany.Checked;
            infoPOS.CompanyArabicVisible = chkCompanyArabic.Checked;
            infoPOS.AddressVisible = chkAddress.Checked;
            infoPOS.PhoneVisible = chkPhone.Checked;
            infoPOS.ShowCustBalinPrint = chkShowCustBill.Checked;
            infoPOS.CustBillCopy = cmbCustBill.Text;
            infoPOS.AddQtyInSameBarcodeToGrid = ChkAddqtyinsameBarcode.Checked;
            infoPOS.ShowProductSummaryInSessionClose = ChkShowProdtSummryInSessionclose.Checked;
            infoPOS.ActiveTableManage = ChkActiveTable.Checked;
            infoPOS.StockView = ChkStockView.Checked;
            infoPOS.SessionManagmentByAdmin = chkSessionMngmnt.Checked;
            infoPOS.AlwaysEnableHoldBillView = chkAlwaysEnableHoldBillView.Checked;
            infoPOS.BlockZeroPriceInSales = chkBlockZeroPrice.Checked;
            infoPOS.ZeroQtyAlert= cmbZeroQty.Text;
            infoPOS.DeleteMode = cmbDeleteMode.Text;
            infoPOS.StartingTokenNo = txtStartingTokenNo.Text;
            infoPOS.IsHoldBillPrint = chkIsHoldBillNeeded.Checked;
            spPOSSettings.POSSettingsEdit(infoPOS);
            MessageBox.Show("Settings saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmPOSSettings_Load(object sender, EventArgs e)
        {
            clsGeneral objGeneral = new clsGeneral();
            objGeneral.formSettings(this);
            FillCheckBox();
           
        }
        private void FillCheckBox()
        {
            POSSettingsInfo infoPOS = new POSSettingsInfo();
            infoPOS = spPOSSettings.POSSettingsViewByBranchId(PublicVariables._branchId);
            if (infoPOS != null)
            {
                lblPOSSettingsId.Text = infoPOS.POSSettingsId;
                chkBillClearAuth.Checked = infoPOS.BillClearAuth;
                chkDiscountAuth.Checked = infoPOS.DiscountAuth;
                chkHoldBillAuth.Checked = infoPOS.HoldBillAuth;
                chkPriceChangeAuth.Checked = infoPOS.PriceChangeAuth;
                chkCreditSalesAuth.Checked = infoPOS.CreditSalesAuth;
                chkZeroStockAuth.Checked = infoPOS.ZeroStockAuth;
                chkItemGrouping.Checked = infoPOS.ItemGroupinginPrint;
                chklastBillPrintAuth.Checked = infoPOS.lastBillPrintAuth;
                chkExchangeItemAuth.Checked = infoPOS.ExchangeItemAuth;
                chkCashBoxOpenAuth.Checked = infoPOS.CashBoxOpenAuth;
                chkQtyChangeAuth.Checked = infoPOS.QtyChangeAuth;
                txtExpiry.Text = infoPOS.CNDays.ToString();

                txtCompanyName.Text = infoPOS.POSCompanyName;
                txtCompanyArabic.Text = infoPOS.POSCompanyNameArabic;
                txtAddress.Text = infoPOS.POSAddress;
                txtPhone.Text = infoPOS.POSPhone;
                txtCompanyH.Text = infoPOS.CompanyH.ToString();
                txtCompanyArabicH.Text = infoPOS.CompanyArabicH.ToString();
                txtAddressH.Text = infoPOS.AddressH.ToString();
                txtPhoneH.Text = infoPOS.PhoneH.ToString();
                chkCompany.Checked = infoPOS.CompanyVisible;
                chkCompanyArabic.Checked = infoPOS.CompanyArabicVisible;
                chkAddress.Checked = infoPOS.AddressVisible;
                chkPhone.Checked = infoPOS.PhoneVisible;
                chkShowCustBill.Checked = infoPOS.ShowCustBalinPrint;
                cmbCustBill.Text = infoPOS.CustBillCopy;
                ChkAddqtyinsameBarcode.Checked = infoPOS.AddQtyInSameBarcodeToGrid;
                ChkActiveTable.Checked = infoPOS.ActiveTableManage;
                ChkStockView.Checked = infoPOS.StockView;
                chkSessionMngmnt.Checked = infoPOS.SessionManagmentByAdmin;
                chkAlwaysEnableHoldBillView.Checked = infoPOS.AlwaysEnableHoldBillView;
                chkBlockZeroPrice.Checked = infoPOS.BlockZeroPriceInSales;
                cmbZeroQty.Text= infoPOS.ZeroQtyAlert;
                cmbDeleteMode.Text= infoPOS.DeleteMode;
                txtStartingTokenNo.Text = infoPOS.StartingTokenNo;
                chkIsHoldBillNeeded.Checked = infoPOS.IsHoldBillPrint;
            }
        }
      
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtExpiry_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objComboValidation.IntigerFieldKeypress(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show("C24:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();

            }
        }

        private void txtCompanyH_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtCompanyArabicH_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtAddressH_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtPhoneH_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtCompanyH_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtCompanyH.Text);
            }
            catch
            {
                txtCompanyH.Text = "0";
            }
        }

        private void txtCompanyArabicH_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtCompanyArabicH.Text);
            }
            catch
            {
                txtCompanyArabicH.Text = "0";
            }
        }

        private void txtAddressH_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtAddressH.Text);
            }
            catch
            {
                txtAddressH.Text = "0";
            }
        }

        private void txtPhoneH_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtPhoneH.Text);
            }
            catch
            {
                txtPhoneH.Text = "0";
            }
        }

        private void ChkAddqtyinsameBarcode_Enter(object sender, EventArgs e)
        {
            ChkAddqtyinsameBarcode.ForeColor = System.Drawing.Color.Red;
        }

        private void ChkAddqtyinsameBarcode_Leave(object sender, EventArgs e)
        {
            ChkAddqtyinsameBarcode.ForeColor = System.Drawing.Color.Black;
        }

        private void ChkAddqtyinsameBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChkShowProdtSummryInSessionclose.Focus();

            }

        }

        private void ChkShowProdtSummryInSessionclose_Enter(object sender, EventArgs e)
        {
            ChkShowProdtSummryInSessionclose.ForeColor = System.Drawing.Color.Red;
        }

        private void ChkShowProdtSummryInSessionclose_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();

            }
        }

        private void ChkShowProdtSummryInSessionclose_Leave(object sender, EventArgs e)
        {
            ChkShowProdtSummryInSessionclose.ForeColor = System.Drawing.Color.Black;
        }

        private void ChkActiveTable_Enter(object sender, EventArgs e)
        {
            ChkActiveTable.ForeColor = System.Drawing.Color.Red;
        }

        private void ChkActiveTable_Leave(object sender, EventArgs e)
        {
            ChkActiveTable.ForeColor = System.Drawing.Color.Black;
        }

        private void ChkActiveTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();

            }

        }
        public void sessionClose()
        {
            if (infoPOS.SessionManagmentByAdmin == false)
            {
                // Check Sales and Return screen is opened
                frmPOSSales _isOpen = Application.OpenForms["frmPOSSales"] as frmPOSSales;
                if (_isOpen != null)
                {
                    MessageBox.Show("Please Close Sales Screen!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmPOSSalesReturn _isOpen1 = Application.OpenForms["frmPOSSalesReturn"] as frmPOSSalesReturn;
                if (_isOpen1 != null)
                {
                    MessageBox.Show("Please Close Sales Return Screen!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                SessionManagementSP sessionSp = new SessionManagementSP();
                DataTable dtbl = new DataTable();
                dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                if (dtbl.Rows.Count > 0)
                {
                    if (MessageBox.Show("This is SESSION CLOSING Option. Do you want to continue?", "Session Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        frmSessionClosing frmObj = new frmSessionClosing();
                        frmObj.DoWhenComingFromPOSMDI(dtbl.Rows[0]["sessionNo"].ToString(), Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()));

                        SessionManagementSP SPSessionManagement = new SessionManagementSP();
                        SPSessionManagement.UpdateSessionClose(PublicVariables._counterId);
                        MessageBox.Show("Session Closed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Session is Already Closed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("You have no permission to close the session", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        public void openSalesInvoice()
        {
            if (infoPOS.SessionManagmentByAdmin == true)
            {

                SessionManagementSP sessionSp = new SessionManagementSP();
                DataTable dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                if (dtbl.Rows.Count > 0) // If a session is active, allow opening the sales invoice
                {
                    if (PublicVariables._SalesScreenType == "Type1")
                    {
                        frmPOSSales frm = new frmPOSSales();
                        frmPOSSales _isOpen = Application.OpenForms["frmPOSSales"] as frmPOSSales;
                        if (_isOpen == null)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.MdiParent = MDIObj;
                            frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
                            frm.Show();
                        }
                        else
                        {
                            _isOpen.MdiParent = MDIObj;
                            _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                            if (_isOpen.WindowState == FormWindowState.Minimized)
                            {
                                _isOpen.WindowState = FormWindowState.Normal;
                            }
                            if (_isOpen.Enabled)
                            {
                                _isOpen.Activate();
                                _isOpen.BringToFront();
                            }
                        }
                    }
                    else if (PublicVariables._SalesScreenType == "Type2")
                    {
                        frmPOSSales2 frm = new frmPOSSales2();
                        frmPOSSales2 _isOpen = Application.OpenForms["frmPOSSales2"] as frmPOSSales2;

                        if (_isOpen == null)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            frm.MdiParent = MDIObj;
                            frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
                            frm.Show();
                        }
                        else
                        {
                            _isOpen.MdiParent = MDIObj;
                            _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                            if (_isOpen.WindowState == FormWindowState.Minimized)
                            {
                                _isOpen.WindowState = FormWindowState.Normal;
                            }
                            if (_isOpen.Enabled)
                            {
                                _isOpen.Activate();
                                _isOpen.BringToFront();
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No sales are permitted. Please start a session first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                if (PublicVariables._counterId == "")
                {
                    MessageBox.Show("This System is not configure as Counter!");
                }
                else
                {
                    SessionManagementSP sessionSp = new SessionManagementSP();
                    DataTable dtbl = new DataTable();
                    dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                    if (dtbl.Rows.Count > 0)
                    {
                        if (PublicVariables._SalesScreenType == "Type1")
                        {
                            frmPOSSales frm = new frmPOSSales();
                            frmPOSSales _isOpen = Application.OpenForms["frmPOSSales"] as frmPOSSales;
                            if (_isOpen == null)
                            {
                                frm.WindowState = FormWindowState.Normal;
                                frm.MdiParent = MDIObj;
                                frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                                frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                                frm.Show();
                            }
                            else
                            {
                                _isOpen.MdiParent = MDIObj;
                                _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                                _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
                                if (_isOpen.WindowState == FormWindowState.Minimized)
                                {
                                    _isOpen.WindowState = FormWindowState.Normal;
                                }
                                if (_isOpen.Enabled)
                                {
                                    _isOpen.Activate();
                                    _isOpen.BringToFront();
                                }

                            }
                        }
                        else if (PublicVariables._SalesScreenType == "Type2") //Added on 01/Apr/2025 Varis
                        {
                            frmPOSSales2 frm = new frmPOSSales2();
                            frmPOSSales2 _isOpen = Application.OpenForms["frmPOSSales2"] as frmPOSSales2;
                            if (_isOpen == null)
                            {
                                frm.WindowState = FormWindowState.Normal;
                                frm.MdiParent = MDIObj;
                                frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                                frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                                frm.Show();
                            }
                            else
                            {
                                _isOpen.MdiParent = MDIObj;
                                _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                                _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
                                if (_isOpen.WindowState == FormWindowState.Minimized)
                                {
                                    _isOpen.WindowState = FormWindowState.Normal;
                                }
                                if (_isOpen.Enabled)
                                {
                                    _isOpen.Activate();
                                    _isOpen.BringToFront();
                                }

                            }
                        }


                    }
                    else
                    {
                        string strOpenedUserId = sessionSp.CheckAnySessionIsOpenedInCounter(PublicVariables._counterId);
                        if (strOpenedUserId != "")
                        {
                            MessageBox.Show(strOpenedUserId + " session is kept Open, Please Close it first");
                            Application.Exit();
                        }

                        frmSessionManagement frmsessionManagement = new frmSessionManagement();
                        frmSessionManagement _isOpen = Application.OpenForms["frmSessionManagement"] as frmSessionManagement;
                        if (_isOpen == null)
                        {
                            frmsessionManagement.WindowState = FormWindowState.Normal;
                            frmsessionManagement.MdiParent = MDIObj;
                            frmsessionManagement.Show();
                        }
                        else
                        {
                            _isOpen.MdiParent = MDIObj;
                            if (_isOpen.WindowState == FormWindowState.Minimized)
                            {
                                _isOpen.WindowState = FormWindowState.Normal;
                            }
                            if (_isOpen.Enabled)
                            {
                                _isOpen.Activate();
                                _isOpen.BringToFront();
                            }

                        }
                    }

                }
            }
        }
        public void openSalesReturn()
        {
            if (infoPOS.SessionManagmentByAdmin == false)
            {
                if (PublicVariables._counterId == "")
                {
                    MessageBox.Show("This System is not configure as Counter!");
                }
                else
                {
                    SessionManagementSP sessionSp = new SessionManagementSP();
                    DataTable dtbl = new DataTable();
                    dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                    if (dtbl.Rows.Count > 0)
                    {
                        frmPOSSalesReturn frm = new frmPOSSalesReturn();
                        frmPOSSalesReturn _isOpen = Application.OpenForms["frmPOSSalesReturn"] as frmPOSSalesReturn;
                        if (_isOpen == null)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            //frm.MdiParent = MDIObj;
                            frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                            frm.Show();
                        }
                        else
                        {
                            //_isOpen.MdiParent = MDIObj;
                            _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
                            if (_isOpen.WindowState == FormWindowState.Minimized)
                            {
                                _isOpen.WindowState = FormWindowState.Normal;
                            }
                            if (_isOpen.Enabled)
                            {
                                _isOpen.Activate();
                                _isOpen.BringToFront();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Session not Opened! Please Open session");
                    }
                }
            }
        }

       
    }

}    



