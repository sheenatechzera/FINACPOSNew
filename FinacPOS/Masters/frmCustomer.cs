using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace FinacPOS
{
    partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);
        }

        #region PUBLIC VARIABLES

        public bool isCustomerCreation = false;
        UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();//changed on 19/10/2023 sheena    
  
        string strOldGroupId = "";         // To keep current account ledger id while going to create new one
        string strOldArea = "";                    // To keep current area id while going to create new one
        string strOldMarket = ""; // To keep current market id while going to create new one
        string strOldCurrency = ""; // To keep current currency id while going to create new one
        string strOldPricingLevel = ""; // To keep current pricing level id while going to create new one
        string strLedgerIdForEdit = "";     // to keep ledger id while editing
        string strLedgerIdForOtherForms = ""; // To keep ledger id of added ledger to return to other forms
        string strLedgerName = "";            // To keep ledger name before editing
        string strHeaderTExt = "";            // To keep headertext of dgvRegister
        string strOldRoute= ""; // To keep current route id while going to create new one


        string strAreaText = "";
        string strCurrencyText = "";
        string strMarketText = "";
        string strPLText = "";
        string strLedgerText = "";

        string strFormType = ""; 

        bool isInEditMode = false; // To indicate whether the form is  in edit mode
        bool isFromOtherForm = false;      // To indicate whetehr this form is calle dform other forms
        bool isFormload = false;              // To prevent filling of register many time in form load

        bool isFromPurchaseInvoice = false;
        bool isFromSalesInvoice = false;
        bool isFromSalesOrder = false;
        bool isFromProformaInvoice = false;
        bool isFromSalesQuotation = false;
        bool isFromPayableVoucher = false;
        bool isFromReceivableVoucher = false;
        bool isFromRejectionIn = false;
        bool isFromRejectionOut = false;
        bool isFromDeliveryNote = false;
        bool isFromPurchaseInvoicePOS = false;
        bool isFromPurchaseOrder = false;
        bool isFromMaterialreciept = false;
        bool isFromPurchasereturn = false;
        bool isFromSalesreturn = false;


        //frmPurchaseInvoice frmPurchaseInvoice;
        //frmSalesInvoice frmSalesInvoice;
        //frmPayableVoucher frmPayable;
        //frmReceivableVoucher frmReceivable;
        //frmSalesOrder frmSalesOrder;
        //frmProformaInvoice frmProformaInvoice;
        //frmSalesQuotation frmSalesQuotation;
        //frmProgress frmCompanyProgress = new frmProgress();
        //frmRejectionIn frmRejectionIn;
        //frmRejectionOut frmRejectionout;
        //frmDeliveryNote frmDeliveryNote;
        //frmPurchaseInvoicePOS frmPurchaseInvoicePOS;
        //frmPurchaseOrder frmPurchOrder;
        //frmMaterialReceipt frmMatrialreceipt;
        //frmPurchaseReturn frmPurchasereturn;
        //frmSalesReturn frmsalesreturn;
        bool isSundryDebtorOrCreditor = false;    // To indicate whether the selected accont group is under sundry debtor or creditor
        //  to indicate whetehr the selected ledger is uilt in ledger 

        //int inNarrationCount = 0;
        //int inAdressCount = 0;

        decimal oldBlnc = 0;   // to hold opening blanace of ledger inorder to check the financial year status while editing or deleting

        ComboValidation objComboValidation = new ComboValidation();
        TransactionsGeneralFill objGeneral = new TransactionsGeneralFill();
        AccountLedgerSP SpLedger = new AccountLedgerSP();
        DataTable dtblCostCentre;
        int inKeyPrsCou = 0;
        string _mainMenuItem = "";

        #endregion

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                _mainMenuItem = "Masters"; //PublicVariables._mainMenuItem;
                bwrkControlSettings.RunWorkerAsync();
                isCustomerCreation = true;
                //frmCompanyProgress.ShowInTaskbar = false;
                //frmCompanyProgress.ShowFromReport();

                ClearFunction();

            }
            catch (Exception ex)
            {
                MessageBox.Show("AL15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #region FUNCTIONS
        //--------------------------------------------------------------------------------------------------------------------
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
        public void CreateDataTableForCostCentre()
        {
            try
            {
                // creating columns for data table
                dtblCostCentre = new DataTable();
                DataColumn c = new DataColumn("costCentreId");
                dtblCostCentre.Columns.Add(c);
                //c = new DataColumn("ledgerId");
                //dtblCostCentre.Columns.Add(c);
                c = new DataColumn("amount", typeof(decimal));
                dtblCostCentre.Columns.Add(c);
                c = new DataColumn("DrCr");
                dtblCostCentre.Columns.Add(c);
                //c = new DataColumn("ledgerAmount", typeof(decimal));
                //dtblCostCentre.Columns.Add(c);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PI6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DowhenReturningFromMarketCreationForm(string strId)
        {
            // Function to call from account group creation form after creating new group
            this.Enabled = true;


            if (strId != "")
            {
                FillAreaCombo();
                MarketInfo InfoMarket = new MarketInfo();
                MarketSP SpMarkte = new MarketSP();
                InfoMarket = SpMarkte.MarketView(strId);
                cmbArea.SelectedValue = InfoMarket.AreaId;

            }
            //-----------------------------------------------
            FillMarket();
            if (strId != "")
            {
                // Assign newly created id
                cmbMarket.SelectedValue = strId;
            }
            else if (strOldMarket != "")
            {
                // Assign old id as new one is not created
                cmbMarket.SelectedValue = strOldMarket;
            }
            else
            {
                // Assign  "" as new one is not created and nothing selected while going to account ledger form
                cmbMarket.SelectedIndex = -1;
            }
            txtLedgerName.Text = txtLedgerName.Text.Trim();
            cmbMarket.Focus();


            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }
        public void DowhenReturningFromRouteCreationForm(string strId)
        {
            // Function to call from account group creation form after creating new group
            this.Enabled = true;


            if (strId != "")
            {
                FillMarket();
                RouteInfo InfoRoute = new RouteInfo();
                RouteSP SpRoute = new RouteSP();
                InfoRoute = SpRoute.RouteView(strId);
                cmbMarket.SelectedValue = InfoRoute.MarketId;

            }
            //-----------------------------------------------
            FillRoute();
            if (strId != "")
            {
                // Assign newly created id
                cmbRoute.SelectedValue = strId;
            }
            else if (strOldRoute != "")
            {
                // Assign old id as new one is not created
                cmbRoute.SelectedValue = strOldRoute;
            }
            else
            {
                // Assign  "" as new one is not created and nothing selected while going to account ledger form
                cmbRoute.SelectedIndex = -1;
            }
            txtLedgerName.Text = txtLedgerName.Text.Trim();
            cmbRoute.Focus();


            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void DowhenReturningFromMasterCreationForm(string strId, string strType)
        {
            // Function to call from account group creation form after creating new group
            this.Enabled = true;
            if (strType == "Area")
            {
                FillAreaCombo();
                if (strId != "")
                {
                    // Assign newly created id
                    cmbArea.SelectedValue = strId;
                }
                else if (strOldArea != "")
                {
                    // Assign old id as new one is not created
                    cmbArea.SelectedValue = strOldArea;
                }
                else
                {
                    // Assign  "" as new one is not created and nothing selected while going to account ledger form
                    cmbArea.SelectedIndex = -1;
                }
                txtLedgerName.Text = txtLedgerName.Text.Trim();

                cmbArea.Focus();

            }
            else
            {
                FillPricingLevel();
                if (strId != "")
                {
                    // Assign newly created id
                    cmbPricingLevel.SelectedValue = strId;
                }
                else if (strOldPricingLevel != "")
                {
                    // Assign old id as new one is not created
                    cmbPricingLevel.SelectedValue = strOldPricingLevel;
                }
                else
                {
                    // Assign  "" as new one is not created and nothing selected while going to account ledger form
                    cmbPricingLevel.SelectedIndex = -1;
                }
                txtLedgerName.Text = txtLedgerName.Text.Trim();

                cmbPricingLevel.Focus();

            }
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void DowhenReturningFromCurrencyCreationForm(string strCurrencyId)
        {
            // Function to call from account group creation form after creating new group
            this.Enabled = true;
            FillCurrency();
            if (strCurrencyId != "")
            {
                // Assign newly created id
                cmbCurrency.SelectedValue = strCurrencyId;
            }
            else if (strOldCurrency != "")
            {
                // Assign old id as new one is not created
                cmbCurrency.SelectedValue = strOldCurrency;
            }
            else
            {
                // Assign  "" as new one is not created and nothing selected while going to account ledger form
                cmbCurrency.SelectedIndex = -1;
            }
            txtLedgerName.Text = txtLedgerName.Text.Trim();

            cmbCurrency.Focus();

            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }
        public void ClearFunction()
        {
            // To reset controls fo form
            isFormload = true;

            if (isCustomerCreation == true)
            {
                strFormType = "Customer";
                cmbLedgerType.Text = "Customer";//24-04-2024 sheena
            }
            else
            {
                strFormType = "Vendor";
                cmbLedgerType.Text = "Vendor";//24-04-2024 sheena
            }

            this.Text = strFormType + " Creation";
            if(PublicVariables._ModuleLanguage=="ARB")
            {
                if(strFormType == "Customer")
                {
                    lblLedgerCode.Text = "رمز العميل";
                    lblLedgerName.Text = "اسم الزبون";
                }
                else if (strFormType == "Vendor")
                {
                    lblLedgerCode.Text = "رمز البائع";
                    lblLedgerName.Text = "اسم البائع";
                }
            }
            else
            {
                lblLedgerCode.Text = strFormType + " Code";
                lblLedgerName.Text = strFormType + " Name";
            }
            

            cmbGrpUnder.Enabled = false;

            oldBlnc = 0;
            isSundryDebtorOrCreditor = false;
            FillGroupUnderCombo(strFormType);
            FillAccountGroupCombo();
            isFormload = true;

            FillPricingLevel();
            FillCurrency();
            FillAreaCombo();
            FillMarket();
            FillRoute();

            txtLedgerCode.Text = "";
            txtLedgerName.Text = "";
            //cmbIntreast.Text = "No";
            cmbBill.Text = "No";
            cmbDebit.Text = "Dr";
            txtNarration.Text = "";
            txtMailName.Text = "";
            txtAccount.Text = "";
            txtAddress.Text = "";
            txtAddressArabic.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtTin.Text = "";
            txtCst.Text = "";
           // txtPan.Text = "";
            txtCreditPeriod.Text = "0";
            txtCreditLimit.Text = "0.00";
            txtBlnc.Text = "0.00";

            //
            txtBuildingNo.Text = "";
            txtAdditionalNo.Text = "";
            txtStreetName.Text = "";
            txtdistrict.Text = "";
            txtPostBoxNo.Text = "";
            txtCity.Text = "";
            txtCountry.Text = "";
            //
            txtBuildingNoARB.Text = "";
            txtAdditionalNoARB.Text = "";
            txtStreetARB.Text = "";
            txtDistrictARB.Text = "";
            txtPostBoxNoARB.Text = "";
            txtCityNameARB.Text = "";
            txtCountryARB.Text = "";
            txtUserdef1.Text = "";
            txtUserdef2.Text = "";
            txtUserdef3.Text = "";
            txtUserdef4.Text = "";
            if (!string.IsNullOrEmpty(FinanceSettingsInfo._Userdef1))
            {
                lblUserdef1.Text = FinanceSettingsInfo._Userdef1;
            }
            if (!string.IsNullOrEmpty(FinanceSettingsInfo._Userdef2))
            {
                lblUserdef2.Text = FinanceSettingsInfo._Userdef2;
            }
            if (!string.IsNullOrEmpty(FinanceSettingsInfo._Userdef3))
            {
                lblUserdef3.Text = FinanceSettingsInfo._Userdef3;
            }
            if (!string.IsNullOrEmpty(FinanceSettingsInfo._Userdef4))
            {
                lblUserdef4.Text = FinanceSettingsInfo._Userdef4;
            }
            // Settings
            if (FinanceSettingsInfo._BillByBill)
            {
                cmbBill.Enabled = true;
                cmbBill.SelectedIndex = 0;
            }
            else
            {
                cmbBill.Enabled = false;
            }

            if (FinanceSettingsInfo._UseMultiCurrency)
            {
                cmbCurrency.Enabled = true;
                btnNewCurrency.Enabled = true;
            }
            else
            {
                cmbCurrency.Enabled = false;
                btnNewCurrency.Enabled = false;
            }
            FinacMessage.SaveButtonText(btnSave, "New");
            FinacMessage.ClearButtonText(btnClear, "New"); 
            isInEditMode = false;
            btnDelete.Enabled = false;
            //cmbGroupName.Enabled = true;
            txtLedgerName.ReadOnly = false;
            txtLedgerName.BackColor = Color.White;

            cmbCreditLimitStatus.SelectedIndex = 0;

            GenerateLedgerCode(cmbGroupName.SelectedValue.ToString());   

            isFormload = false;
            txtLedgerCode.Focus();
        //added 02/11/2023   sheena       
            //if (SettingsInfo._costCentre)
            //{
            //    btnCostCentre.Visible = true;
            //}
            //else
            //    btnCostCentre.Visible = false;
            CreateDataTableForCostCentre();


        }
        public void GenerateLedgerCode(string strGroupId)
        {
            int IntNextNo = 0;
            string strGroupCode = "";

            AccountGroupInfo InfoAccountGroup = new AccountGroupInfo();
            AccountGroupSP SpGroupSp = new AccountGroupSP();
            InfoAccountGroup = SpGroupSp.AccountGroupView(strGroupId);
            strGroupCode = InfoAccountGroup.AccountGroupCode;
            IntNextNo = InfoAccountGroup.LedgerNextNo;

            string strLedgerCode = strGroupCode + "" + IntNextNo.ToString("000");
            txtLedgerCode.Text = strLedgerCode;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void FillControlsForEdit()
        {
            // Fill the details of account ledger when one row of grid is selected
            try
            {
                AccountLedgerInfo InfoLedger = new AccountLedgerInfo();
                InfoLedger = SpLedger.AccountLedgerView(strLedgerIdForEdit);
                // Filling details 
                txtLedgerCode.Text = InfoLedger.LedgerCode;//by sheena on 06-08-2023
                txtLedgerName.Text = InfoLedger.LedgerName;
                strLedgerName = InfoLedger.LedgerName;
                cmbGroupName.SelectedValue = InfoLedger.GroupId;
                //if (InfoLedger.InterestOrNot)
                //{
                //    cmbIntreast.Text = "Yes";
                //}
                //else
                //{
                //    cmbIntreast.Text = "No";
                //}
                if (InfoLedger.BillByBill)
                {
                    cmbBill.Text = "Yes";
                }
                else
                {
                    cmbBill.Text = "No";
                }
                oldBlnc = InfoLedger.OpeningBalance;
                txtBlnc.Text = InfoLedger.OpeningBalance.ToString(FinanceSettingsInfo._roundDecimalPart);
                cmbDebit.Text = InfoLedger.CrOrDr;
                txtNarration.Text = InfoLedger.Narration;
                txtMailName.Text = InfoLedger.Name;
                txtAccount.Text = InfoLedger.AccountNo;
                txtAddress.Text = InfoLedger.Address;

                txtAddressArabic.Text = InfoLedger.AddressArabic;


                txtPhone.Text = InfoLedger.PhoneNo;
                txtFax.Text = InfoLedger.FaxNo;
                txtEmail.Text = InfoLedger.Email;
                txtCreditPeriod.Text = InfoLedger.CreditPeriod.ToString();
                txtCreditLimit.Text = InfoLedger.CreditLimit.ToString(FinanceSettingsInfo._roundDecimalPart);
                cmbPricingLevel.SelectedValue = InfoLedger.PricingLevelId;
                cmbCurrency.SelectedValue = InfoLedger.CurrencyId;

                // Getting area id of market
                try
                {
                    cmbArea.SelectedValue = InfoLedger.AreaId;
                    cmbMarket.SelectedValue = InfoLedger.MarketId;
                }
                catch { }
                txtTin.Text = InfoLedger.TinNumber;
                txtCst.Text = InfoLedger.CstNumber;
               // txtPan.Text = InfoLedger.PanNumber;

                txtBuildingNo.Text = InfoLedger.BuildingNo;
                txtAdditionalNo.Text = InfoLedger.AdditionalNo;
                txtStreetName.Text = InfoLedger.StreetName;
                txtdistrict.Text = InfoLedger.District;
                txtPostBoxNo.Text = InfoLedger.PostboxNo;
                txtCity.Text = InfoLedger.CityName;
                txtCountry.Text = InfoLedger.Country;

                txtBuildingNoARB.Text = InfoLedger.BuildingNoArb;
                txtAdditionalNoARB.Text = InfoLedger.AdditionalNoArb;
                txtStreetARB.Text = InfoLedger.StreetNameArb;
                txtDistrictARB.Text = InfoLedger.DistrictArb;
                txtPostBoxNoARB.Text = InfoLedger.PostboxNoArb;
                txtCityNameARB.Text = InfoLedger.CityNameArb;
                txtCountryARB.Text = InfoLedger.CountryArb;

                cmbCreditLimitStatus.Text = InfoLedger.CreditLimitStatus;

                txtLedgerName.ReadOnly = false;
                txtLedgerName.BackColor = Color.White;
                btnNewCurrency.Enabled = true;
                cmbLedgerType.Text = InfoLedger.LedgerType;//24-04-2024 sheena
                try
                {
                    cmbRoute.SelectedValue = InfoLedger.routeId;
                }
                catch { }
                txtUserdef1.Text = InfoLedger.Userdef1;
                txtUserdef2.Text = InfoLedger.Userdef2;
                txtUserdef3.Text = InfoLedger.Userdef3;
                txtUserdef4.Text = InfoLedger.Userdef4;

                if (InfoLedger.Default == true)
                {

                    cmbGroupName.Enabled = false;
                    txtLedgerName.ReadOnly = true;
                    txtLedgerName.BackColor = Color.WhiteSmoke;
                }

                btnDelete.Enabled = true;
                FinacMessage.SaveButtonText(btnSave, "Edit");
                FinacMessage.ClearButtonText(btnClear, "Edit"); 
                isInEditMode = true;
                //Fill to costCenteropening datatable added 02/11/2023 by sheena
                CostCentreOpeningSP Spcost = new CostCentreOpeningSP();
                DataTable dtbl = new DataTable();
                dtbl = Spcost.CostCentreOpeningViewAllByLedgerID(strLedgerIdForEdit);
                CreateDataTableForCostCentre();

                foreach (DataRow dr in dtbl.Rows)
                {
                    //debit
                    DataRow Dr1 = dtblCostCentre.NewRow();
                    Dr1["costCentreId"] = dr["costCentreId"];
                    // Dr1["ledgerId"] = dr["ledgerId"];

                    if (dr["costCentreId"].ToString() == "1")
                    {
                        Dr1["DrCr"] = cmbDebit.Text;
                        if (decimal.Parse(dr["crAmount"].ToString()) > 0)
                        {
                            Dr1["amount"] = dr["crAmount"];
                        }
                        else if (decimal.Parse(dr["drAmount"].ToString()) > 0)
                        {
                            Dr1["amount"] = dr["drAmount"];
                        }
                        else
                            Dr1["amount"] = 0;
                    }
                    else
                    {
                        if (decimal.Parse(dr["drAmount"].ToString()) > 0)
                        {
                            Dr1["amount"] = dr["drAmount"];
                            Dr1["DrCr"] = "Dr";
                        }
                        else if (decimal.Parse(dr["crAmount"].ToString()) > 0)
                        {
                            Dr1["amount"] = dr["crAmount"];
                            Dr1["DrCr"] = "Cr";
                        }
                    }

                    dtblCostCentre.Rows.Add(Dr1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillGroupUnderCombo(string strFormType)
        {
            // To fill all account group in combo box
            try
            {
                AccountGroupSP SpAccountGroup = new AccountGroupSP();
                DataTable dtbl = new DataTable();
                dtbl = SpAccountGroup.AccountGroupViewAll();
                DataRow[] dr = dtbl.Select("groupId <>'" + "0" + "' AND groupId <> '" + "1" + "' AND groupId <> '" + "2" + "' AND groupId <> '" + "3" + "' AND groupId <> '" + "4" + "'");
                DataSet dset = new DataSet();
                dset.Merge(dr);
                dtbl = dset.Tables[0];

                cmbGrpUnder.DisplayMember = dtbl.Columns[1].ToString();
                cmbGrpUnder.ValueMember = dtbl.Columns[0].ToString();
                cmbGrpUnder.DataSource = dtbl;
                isFormload = false;
                if (isCustomerCreation == true)
                {
                    cmbGrpUnder.Text = "Sundry Debtors";
                }
                else
                {
                    cmbGrpUnder.Text = "Sundry Creditors";
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("AL2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
               
        }


        //--------------------------------------------------------------------------------------------------------------------
        public void FillAccountGroupCombo()
        {
            // To fill all account group in combo box
            try
            {
                AccountGroupSP SpAccountGroup = new AccountGroupSP();
                DataTable dtbl = new DataTable();
                if (isCustomerCreation == true)
                {
                    dtbl = SpAccountGroup.AccountGroupGetCustomer();
                    DataRow[] dr = dtbl.Select("groupId <>'" + "29" + "' ");
                    DataSet dset = new DataSet();
                    dset.Merge(dr);
                    dtbl = dset.Tables[0];
                }
                else
                {
                    dtbl = SpAccountGroup.AccountGroupGetVendor();
                    DataRow[] dr = dtbl.Select("groupId <>'" + "28" + "' ");
                    DataSet dset = new DataSet();
                    dset.Merge(dr);
                    dtbl = dset.Tables[0];
                }
                
              

                cmbGroupName.DisplayMember = dtbl.Columns[1].ToString();
                cmbGroupName.ValueMember = dtbl.Columns[0].ToString();
                cmbGroupName.DataSource = dtbl;
                isFormload = false;
                if (isCustomerCreation == true)
                {
                    cmbGroupName.Text = "Customer";
                }
                else
                {
                    cmbGroupName.Text = "Vendor";
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("AL2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void FillCurrency()
        {
            objGeneral.FillCurrencyCombo(cmbCurrency);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void FillPricingLevel()
        {
            objGeneral.FillPricingLevelCombo(cmbPricingLevel);
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void FillAreaCombo()
        {
            // Filling area combo
            AreaSP SpArea = new AreaSP();
            DataTable dtbl = new DataTable();
            dtbl = SpArea.AreaViewAllWithNA();
            cmbArea.DataSource = dtbl;
            cmbArea.DisplayMember = "areaName";
            cmbArea.ValueMember = "areaId";
            cmbArea.SelectedValue = "1";
        }
        //--------------------------------------------------------------------------------------------------------------------
        public void FillMarket()
        {
            // fillig market combo
            string strAreaId = "1";
            MarketSP SpMarket = new MarketSP();
            DataTable dtbl = new DataTable();
            if (cmbArea.SelectedValue != null && cmbArea.Text != "")
            {
                strAreaId = cmbArea.SelectedValue.ToString();
            }
            dtbl = SpMarket.MarketViewAllByArea(strAreaId, false);
            cmbMarket.DataSource = dtbl;
            cmbMarket.ValueMember = "marketId";
            cmbMarket.DisplayMember = "marketName";

        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void FillRoute()
        {
            // fillig market combo
            string strMarketId = "1";
            RouteSP SpRoute = new RouteSP();
            DataTable dtbl = new DataTable();
            if (cmbMarket.SelectedValue != null && cmbMarket.Text != "")
            {
                strMarketId = cmbMarket.SelectedValue.ToString();
            }
            dtbl = SpRoute.RouteViewAllByMarket(strMarketId);
            cmbRoute.DataSource = dtbl;
            cmbRoute.ValueMember = "routeId";
            cmbRoute.DisplayMember = "routeName";

        }
        public bool ValidatePhoneNo()
        {
            // Check whether the enterd phone number is valid or not
            bool isValidContactPhone = true;

            return isValidContactPhone;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        public bool ValidateFax()
        {
            // Check whether the enterd phone number is valid or not
            bool isValidFax = true;

            return isValidFax;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        public bool ValidateEmail()
        {
            //Check whether the enterd email id is valid or not
            bool isValidEmail = true;
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");//^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$
            if (txtEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmail.Text))
                {
                    MessageBox.Show("Invalid Email", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isValidEmail = false;
                    txtEmail.Focus();
                    txtEmail.SelectAll();
                }
            }
            return isValidEmail;
        }
        //--------------------------------------------------------------------------------------------------------------------
        public bool CheckExistanceOfLedgerName()
        {
            //Check whether a group name already exist in DB
            bool isExist = false;
            isExist = SpLedger.AccountLedgerCheckExistanceOfName(txtLedgerName.Text);
            if (isExist && isInEditMode)
            {
                if (txtLedgerName.Text.ToLower() == strLedgerName.ToLower())
                {
                    isExist = false;
                }
            }
            return isExist;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void DoLedgerPosting()
        {
            // Do ledger posting
            // Delete exisintg posting in the case of edit ,then add new
            decimal dcOpeningBlnc = decimal.Parse(txtBlnc.Text);
            CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
            LedgerPostingSP SpPosting = new LedgerPostingSP();
            LedgerPostingInfo InfoPosting = new LedgerPostingInfo();
            InfoPosting.VoucherType = "Opening Balance";
            InfoPosting.postingType = "Opening";
            if (cmbDebit.Text == "Dr")
            {
                InfoPosting.Debit = dcOpeningBlnc;
            }
            else
            {
                InfoPosting.Credit = dcOpeningBlnc;
            }
            InfoPosting.Extra1 = "";
            InfoPosting.Extra2 = "";
            InfoPosting.InvoiceNo = txtLedgerCode.Text;
            InfoPosting.Narration = txtNarration.Text;
            InfoPosting.costCentreId = "";

            string CurrencyConversionId = SPCurrencyConversion.CurrencyConversionRateIdViewByCurrencyId(PublicVariables._currencyId, DateTime.Parse(PublicVariables._fromDate.ToString("dd-MMM-yyyy")), PublicVariables._branchId);
            CurrencyConversionInfo InfoCurrencyConversion = SPCurrencyConversion.CurrencyConversionView(CurrencyConversionId);
            InfoPosting.exchangeRate = InfoCurrencyConversion.Rate;
            InfoPosting.exchangeDate = InfoCurrencyConversion.Date;

            if (!isInEditMode)
            {
                // Post new balance
                if (dcOpeningBlnc > 0)
                {
                    InfoPosting.VoucherNo = strLedgerIdForOtherForms;
                    InfoPosting.LedgerId = strLedgerIdForOtherForms;
                    InfoPosting.Date = PublicVariables._fromDate;
                    SpPosting.LedgerPostingAdd(InfoPosting);
                }
            }
            else
            {
                InfoPosting.Date = PublicVariables._fromDate;
                InfoPosting.VoucherNo = strLedgerIdForEdit;
                InfoPosting.LedgerId = strLedgerIdForEdit;
                DataTable dtbl = SpPosting.LedgerPostingViewByVoucherTypeAndVoucherNumber(InfoPosting.VoucherNo, "Opening Balance");
                if (dtbl.Rows.Count > 0)
                {
                    // Edit exisitng row
                    if (dcOpeningBlnc > 0)
                    {
                        InfoPosting.LedgerPostingId = dtbl.Rows[0][0].ToString();
                        SpPosting.LedgerPostingEdit(InfoPosting);
                    }
                    else
                    {
                        // Ledger posting done in save ,but no opening balance in editing,so delete that row
                        SpPosting.LedgerPostingDeleteByVoucherTypeAndVoucherNo(InfoPosting.VoucherNo, "Opening Balance");
                    }
                }
                else
                {
                    // Add new row
                    SpPosting.LedgerPostingAdd(InfoPosting);
                }
            }

            DoPartyBalance(!isInEditMode ? strLedgerIdForOtherForms : strLedgerIdForEdit, dcOpeningBlnc);
        }
        public void DoPartyBalance(string strLedgerId, decimal dcOpeningBalance)
        {

            // To add to party balance table
            PartyBalanceInfo InfoPatryBalance = new PartyBalanceInfo();
            PartyBalanceSP SPPartyBalance = new PartyBalanceSP();
            CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
            AccountLedgerSP SPLedger = new AccountLedgerSP();
            //------------------------------------------------------------------------------
            SPPartyBalance.PartyBalanceDeleteByVoucherTypeVoucherNoAndReferenceType(strLedgerId, "Opening Balance");
            //------------------------------------------------------------------------------
            if (dcOpeningBalance > 0)
            {
                if (cmbBill.Text == "Yes")
                {
                    InfoPatryBalance.Date = DateTime.Parse(PublicVariables._fromDate.ToString("dd-MMM-yyyy"));
                    InfoPatryBalance.BranchId = PublicVariables._branchId;
                    InfoPatryBalance.Extra1 = "";
                    InfoPatryBalance.Extra2 = "";
                    InfoPatryBalance.Optional = false;
                    InfoPatryBalance.LedgerId = strLedgerId;
                    InfoPatryBalance.VoucherType = "Opening Balance";
                    InfoPatryBalance.VoucherNo = strLedgerId;
                    InfoPatryBalance.AgainstVoucherType = "NA";
                    InfoPatryBalance.AgainstvoucherNo = "NA";
                    InfoPatryBalance.ReferenceType = "New";
                    AccountLedgerInfo InfoLedger = SPLedger.AccountLedgerView(strLedgerId);
                    InfoPatryBalance.CreditPeriod = InfoLedger.CreditPeriod;
                    InfoPatryBalance.CurrencyConversionId = SPCurrencyConversion.CurrencyConversionRateIdViewByCurrencyId(PublicVariables._currencyId, DateTime.Parse(PublicVariables._fromDate.ToString("dd-MMM-yyyy")), PublicVariables._branchId);
                    if (cmbDebit.Text == "Dr")
                    {
                        InfoPatryBalance.Debit = dcOpeningBalance;
                        InfoPatryBalance.Credit = 0;
                    }
                    else
                    {
                        InfoPatryBalance.Debit = 0;
                        InfoPatryBalance.Credit = dcOpeningBalance;
                    }
                    InfoPatryBalance.invoiceNo = strLedgerId;
                    InfoPatryBalance.referenceNo = txtNarration.Text;
                    InfoPatryBalance.BillAmount = dcOpeningBalance;
                    InfoPatryBalance.invoiceDate = DateTime.Parse(PublicVariables._fromDate.ToString("dd-MMM-yyyy"));
                    InfoPatryBalance.costCentreId = "";

                    string CurrencyConversionId = SPCurrencyConversion.CurrencyConversionRateIdViewByCurrencyId(PublicVariables._currencyId, DateTime.Parse(PublicVariables._fromDate.ToString("dd-MMM-yyyy")), PublicVariables._branchId);
                    CurrencyConversionInfo InfoCurrencyConversion = SPCurrencyConversion.CurrencyConversionView(CurrencyConversionId);
                    InfoPatryBalance.exchangeRate = InfoCurrencyConversion.Rate;
                    InfoPatryBalance.exchangeDate = InfoCurrencyConversion.Date;

                    SPPartyBalance.PartyBalanceAdd(InfoPatryBalance);
                }
            }
            //------------------------------------------------------------------------------

        }
        //--------------------------------------------------------------------------------------------------------------------
        public void SaveOrEdit()
        {
            // doing save or edit
            try
            {
                bool isOk = true;
                EventArgs ex = new EventArgs();
                //if (txtMailName.Text == "")
                //    txtMailName.Text = txtLedgerName.Text;
                cmbGroupName_Leave(cmbGroupName, ex);
                try
                {
                    txtBlnc.Text = txtBlnc.Text.Trim();
                    decimal.Parse(txtBlnc.Text);
                }
                catch
                {
                    txtBlnc.Text = "0.00";
                }
                try
                {
                    txtCreditLimit.Text = txtCreditLimit.Text.Trim();
                    decimal.Parse(txtCreditLimit.Text);
                }
                catch
                {
                    txtCreditLimit.Text = "0.00";
                }
                try
                {
                    txtCreditPeriod.Text = txtCreditPeriod.Text.Trim();
                    int.Parse(txtCreditPeriod.Text);
                }
                catch
                {
                    txtCreditPeriod.Text = "0";
                }
                if (PublicVariables._closed && (decimal.Parse(txtBlnc.Text) > 0 || oldBlnc > 0))
                {
                    if (MessageBox.Show("Selected financial year has been closed. Do you want to open it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        FinancialYearSp SpFinance = new FinancialYearSp();
                        SpFinance.FinancialYearchangeStatus(false);
                        isOk = true;
                    }
                    else
                    {
                        isOk = false;
                    }
                }
                else if (isInEditMode && decimal.Parse(txtBlnc.Text) == 0)
                {
                    PartyBalanceSP SPPartyBalnce = new PartyBalanceSP();
                    if (SPPartyBalnce.PartyBalanceAgainstReferenceCheck(strLedgerIdForEdit, "Opening Balance"))
                    {
                        MessageBox.Show("Opening balance cannot be zero, against reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtBlnc.Focus();
                        isOk = false;
                    }
                }
                if (isOk && isInEditMode)
                {
                    PartyBalanceSP SPPartyBalnce = new PartyBalanceSP();

                    AccountLedgerInfo InfoLedger = new AccountLedgerInfo();
                    InfoLedger = new AccountLedgerSP().AccountLedgerView(strLedgerIdForEdit);
                    if (SPPartyBalnce.PartyBalanceAgainstReferenceCheck(strLedgerIdForEdit, "Opening Balance"))
                    {
                        if (InfoLedger.CrOrDr != cmbDebit.Text)
                        {
                            MessageBox.Show("Cant change debit or credit, against reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbDebit.Focus();
                            isOk = false;
                        }
                    }
                }
                if (isOk)
                {
                    AccountLedgerInfo InfoLedger = new AccountLedgerInfo();
                    // All trimming 
                    txtLedgerCode.Text = txtLedgerCode.Text.Trim();//By Sheena on 06-08-2023
                    txtLedgerName.Text = txtLedgerName.Text.Trim();
                    txtBlnc.Text = txtBlnc.Text.Trim();
                    txtNarration.Text = txtNarration.Text.Trim();
                    txtMailName.Text = txtMailName.Text.Trim();
                    txtAccount.Text = txtAccount.Text.Trim();
                    txtAddress.Text = txtAddress.Text.Trim();
                   
                   
                    txtAddressArabic.Text = txtAddressArabic.Text.Trim();
                    txtPhone.Text = txtPhone.Text.Trim();
                    txtFax.Text = txtFax.Text.Trim();
                    txtEmail.Text = txtEmail.Text.Trim();
                    txtCreditPeriod.Text = txtCreditPeriod.Text.Trim();
                    txtCreditLimit.Text = txtCreditLimit.Text.Trim();
                    txtTin.Text = txtTin.Text.Trim();
                    txtCst.Text = txtCst.Text.Trim();
                   // txtPan.Text = txtPan.Text.Trim();

                    txtBuildingNo.Text = txtBuildingNo.Text.Trim();
                    txtAdditionalNo.Text = txtAdditionalNo.Text.Trim();
                    txtStreetName.Text = txtStreetName.Text.Trim();
                    txtdistrict.Text = txtdistrict.Text.Trim();
                    txtPostBoxNo.Text = txtPostBoxNo.Text.Trim();
                    txtCity.Text = txtCity.Text.Trim();
                    txtCountry.Text = txtCountry.Text.Trim();
                    txtUserdef1.Text=txtUserdef1.Text.Trim();
                    txtUserdef2.Text=txtUserdef2.Text.Trim();
                    txtUserdef3.Text=txtUserdef3.Text.Trim();
                    txtUserdef4.Text=txtUserdef4.Text.Trim();

                    InfoLedger = SpLedger.AccountLedgerView(strLedgerIdForEdit);

                    if (txtLedgerName.Text == "")
                    {
                        MessageBox.Show("Enter " + strFormType + " name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLedgerName.Focus();
                    }
                    else
                    {
                        if (cmbGroupName.SelectedValue == null || cmbGroupName.Text.Trim() == "")
                        {
                            MessageBox.Show("Select account group", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbGroupName.Focus();
                        }
                        else
                        {
                            if (ValidatePhoneNo() == true && ValidateFax() == true && ValidateEmail() == true)
                            {
                                bool isSave = true;

                                if (InventorySettingsInfo._messageBoxAddEdit)
                                {
                                    if (!isInEditMode)
                                    {
                                        if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                        {
                                            isSave = false;
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("Do you want to update?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                        {
                                            isSave = false;
                                        }
                                    }
                                }
                                if (isSave)
                                {
                                    if (CheckExistanceOfLedgerName() == false)
                                    {
                                        ////
                                        // assigning all values to info
                                        //////////////////////
                                        InfoLedger.GroupId = cmbGroupName.SelectedValue.ToString();

                                        InfoLedger.LedgerName = txtLedgerName.Text;
                                        InfoLedger.Narration = txtNarration.Text;
                                        InfoLedger.OpeningBalance = decimal.Parse(txtBlnc.Text);
                                        InfoLedger.CrOrDr = cmbDebit.Text;
                                        InfoLedger.Default = false;
                                        //if (cmbIntreast.Text == "Yes")
                                        //{
                                        //    InfoLedger.InterestOrNot = true;
                                        //}
                                        //else
                                        //{
                                        //    InfoLedger.InterestOrNot = false;
                                        //}
                                        InfoLedger.InterestOrNot = true;
                                        if (isSundryDebtorOrCreditor)
                                        {
                                            if (cmbBill.Text == "Yes")
                                            {
                                                InfoLedger.BillByBill = true;
                                            }
                                            else
                                            {
                                                InfoLedger.BillByBill = false;
                                            }
                                            InfoLedger.Name = txtMailName.Text;
                                            InfoLedger.AccountNo = txtAccount.Text;
                                            InfoLedger.Address = txtAddress.Text;
                                            InfoLedger.AddressArabic = txtAddressArabic.Text;
                                            InfoLedger.PhoneNo = txtPhone.Text;
                                            InfoLedger.FaxNo = txtFax.Text;
                                            InfoLedger.Email = txtEmail.Text;
                                            InfoLedger.CreditPeriod = int.Parse(txtCreditPeriod.Text);
                                            InfoLedger.CreditLimit = decimal.Parse(txtCreditLimit.Text);
                                            InfoLedger.PricingLevelId = cmbPricingLevel.SelectedValue.ToString();
                                            InfoLedger.CurrencyId = cmbCurrency.SelectedValue.ToString();
                                            InfoLedger.MarketId = cmbMarket.SelectedValue.ToString();
                                            try
                                            {
                                                InfoLedger.AreaId = cmbArea.SelectedValue.ToString();// by sumana on 07.01.2011             
                                            }
                                            catch { InfoLedger.AreaId = "1"; }
                                            try
                                            {
                                                InfoLedger.routeId = cmbRoute.SelectedValue.ToString();// 22-06-2024 Sheena          

                                            }
                                            catch
                                            {
                                                InfoLedger.routeId = "1";
                                            }
                                            InfoLedger.TinNumber = txtTin.Text;
                                            InfoLedger.CstNumber = txtCst.Text;
                                           // InfoLedger.PanNumber = txtPan.Text;
                                            InfoLedger.PanNumber = "";

                                            InfoLedger.BuildingNo = txtBuildingNo.Text;
                                            InfoLedger.AdditionalNo = txtAdditionalNo.Text;
                                            InfoLedger.StreetName = txtStreetName.Text;
                                            InfoLedger.District = txtdistrict.Text;
                                            InfoLedger.PostboxNo = txtPostBoxNo.Text;
                                            InfoLedger.CityName = txtCity.Text;
                                            InfoLedger.Country = txtCountry.Text;

                                            InfoLedger.BuildingNoArb = txtBuildingNoARB.Text;
                                            InfoLedger.AdditionalNoArb = txtAdditionalNoARB.Text;
                                            InfoLedger.StreetNameArb = txtStreetARB.Text;
                                            InfoLedger.DistrictArb = txtDistrictARB.Text;
                                            InfoLedger.PostboxNoArb = txtPostBoxNoARB.Text;
                                            InfoLedger.CityNameArb = txtCityNameARB.Text;
                                            InfoLedger.CountryArb = txtCountryARB.Text;

                                            InfoLedger.CreditLimitStatus = cmbCreditLimitStatus.Text;

                                            //InfoLedger.AccountNo = "";
                                            InfoLedger.bankname = "";
                                            InfoLedger.ibanno = "";
                                            InfoLedger.bankaccname = "";

                                            InfoLedger.BankSwiftCode = "";
                                            InfoLedger.BankBranchName = "";
                                            InfoLedger.LedgerType = cmbLedgerType.Text;
                                        }
                                        else
                                        {

                                            InfoLedger.BillByBill = false;
                                            InfoLedger.Name = "";
                                            InfoLedger.AccountNo = "";
                                            InfoLedger.Address = "";
                                            InfoLedger.PhoneNo = "";
                                            InfoLedger.FaxNo = "";
                                            InfoLedger.Email = "";
                                            InfoLedger.CreditPeriod = 0;
                                            InfoLedger.CreditLimit = 0;
                                            InfoLedger.PricingLevelId = "1";
                                            InfoLedger.CurrencyId = PublicVariables._currencyId;
                                            InfoLedger.MarketId = "1";
                                            InfoLedger.AreaId = "1";
                                            InfoLedger.routeId = "1";//22-06-2024 sheena
                                            InfoLedger.TinNumber = "";
                                            InfoLedger.CstNumber = "";
                                            InfoLedger.PanNumber = "";

                                            InfoLedger.BuildingNo = "";
                                            InfoLedger.AdditionalNo = "";
                                            InfoLedger.StreetName = "";
                                            InfoLedger.District = "";
                                            InfoLedger.PostboxNo = "";
                                            InfoLedger.CityName = "";
                                            InfoLedger.Country = "";

                                            InfoLedger.BuildingNoArb = "";
                                            InfoLedger.AdditionalNoArb = "";
                                            InfoLedger.StreetNameArb = "";
                                            InfoLedger.DistrictArb = "";
                                            InfoLedger.PostboxNoArb = "";
                                            InfoLedger.CityNameArb = "";
                                            InfoLedger.CountryArb = "";

                                            InfoLedger.AccountNo = "";
                                            InfoLedger.bankname = "";
                                            InfoLedger.ibanno = "";
                                            InfoLedger.bankaccname = "";

                                            //Added on 18/Mar/2024 Varis
                                            InfoLedger.BankSwiftCode = "";
                                            InfoLedger.BankBranchName = "";
                                            InfoLedger.AddressArabic = "";

                                            InfoLedger.CreditLimitStatus = "Ignore";
                                            InfoLedger.LedgerType = cmbLedgerType.Text;
                                        }
                                        InfoLedger.LedgerCode = txtLedgerCode.Text;//By Sheena on 06-08-2023
                                        InfoLedger.BranchId = PublicVariables._branchId;
                                        InfoLedger.Extra1 = "";
                                        InfoLedger.Extra2 = "";
                                        InfoLedger.Userdef1 = txtUserdef1.Text;
                                        InfoLedger.Userdef2 = txtUserdef2.Text;
                                        InfoLedger.Userdef3 = txtUserdef3.Text;
                                        InfoLedger.Userdef4 = txtUserdef4.Text;

                                        if (!isInEditMode)
                                        {
                                            string filePath = Path.Combine(Application.StartupPath, "AddresArea.txt");
                                            // Check if the file exists
                                            if (File.Exists(filePath))
                                            {
                                                InfoLedger.Address = txtAddress.Text.Trim() + " - " + cmbArea.Text.Trim();
                                            }
                                           

                                            // Do saving

                                            strLedgerIdForOtherForms = SpLedger.AccountLedgerAdd(InfoLedger);
                                            decimal dcOpeningBlnc = decimal.Parse(txtBlnc.Text);
                                            if (dcOpeningBlnc > 0)
                                                DoLedgerPosting();
                                            AddtoCostCentreOpening(strLedgerIdForOtherForms);//save cost centre opneing added 02/11/2023
                                            MessageBox.Show("Saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            if (isFromOtherForm)
                                            {
                                                this.Close();
                                            }
                                            else
                                            {
                                                ClearFunction();

                                            }
                                        }
                                        else
                                        {
                                            // Do editing
                                            InfoLedger.LedgerId = strLedgerIdForEdit;
                                            SpLedger.AccountLedgerEdit(InfoLedger);
                                            decimal dcOpeningBlnc = decimal.Parse(txtBlnc.Text);
                                            if (dcOpeningBlnc > 0)
                                                DoLedgerPosting();
                                      
                                            CostCentreOpeningSP costSp = new CostCentreOpeningSP();
                                            costSp.CostCentreOpeningDelete(InfoLedger.LedgerId); //Delete Cost center opng   added 02/11/2023 sheena
                                            AddtoCostCentreOpening(strLedgerIdForEdit);//save cost centre opneing  added 02/11/2023
                                            MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            
                                            ClearFunction();

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show(strFormType + " already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        
                                        txtLedgerName.SelectAll();
                                        txtLedgerName.Focus();
                                    }
                                }

                            }
                        }
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------
        public bool CheckExistanceOfLedgerIdInLedgerPosting()
        {
            // Check existance of ledger id in any other tables
            DataTable dtblLedger = new DataTable();
            LedgerPostingSP SpLedgerPosting = new LedgerPostingSP();
            return SpLedgerPosting.LedgerPostingCheckExistanceOfLedgerId(strLedgerIdForEdit);
        }
        //--------------------------------------------------------------------------------------------------------------------
        //public void CallThisFormFromPurchaseInvoice(frmPurchaseInvoice frm)
        //{
        //    // Function to call from purchase invoice for creating new account ledger
        //    isFromPurchaseInvoice = true;
        //    this.frmPurchaseInvoice = frm;

        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromPurchaseOrder(frmPurchaseOrder frm)
        //{
        //    // Function to call from purchase invoice for creating new account ledger
        //    isFromPurchaseOrder = true;
        //    this.frmPurchOrder = frm;

        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromPurchaseInvoicePOS(frmPurchaseInvoicePOS frm)
        //{
        //    // Function to call from purchase invoice for creating new account ledger
        //    isFromPurchaseInvoicePOS = true;
        //    this.frmPurchaseInvoicePOS = frm;

        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromMaterialReciept(frmMaterialReceipt frm)
        //{
        //    // Function to call from purchase invoice for creating new account ledger
        //    isFromMaterialreciept = true;
        //    this.frmMatrialreceipt = frm;

        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromPurchaseReturn(frmPurchaseReturn frm)
        //{
        //    // Function to call from purchase invoice for creating new account ledger
        //    isFromPurchasereturn = true;
        //    this.frmPurchasereturn = frm;

        //    isFromOtherForm = true;
        //    base.Show();
        //}
       
        //public void CallThisFormFromRejectionOut(frmRejectionOut frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromRejectionOut = true;
        //    this.frmRejectionout = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromRejectionIn(frmRejectionIn frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromRejectionIn = true;
        //    this.frmRejectionIn = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromPayableVoucher(frmPayableVoucher frm)
        //{
        //    // Function to call from purchase invoice for creating new account ledger
        //    isFromPayableVoucher = true;
        //    this.frmPayable = frm;

        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromReceivableVoucher(frmReceivableVoucher frm)
        //{
        //    // Function to call from purchase invoice for creating new account ledger
        //    isFromReceivableVoucher = true;
        //    this.frmReceivable = frm;

        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromSalesInvoice(frmSalesInvoice frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromSalesInvoice = true;
        //    this.frmSalesInvoice = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //bool isFromSalesInvoiceSimple = false;
        //frmSalesInvoiceSimple frmSalesInvoiceSimp;
        //public void CallThisFormFromSalesInvoiceSimple(frmSalesInvoiceSimple frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromSalesInvoiceSimple = true;
        //    this.frmSalesInvoiceSimp = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromDeliveryNote(frmDeliveryNote frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromDeliveryNote = true;
        //    this.frmDeliveryNote = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromSalesOrder(frmSalesOrder frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromSalesOrder = true;
        //    this.frmSalesOrder = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromProformaInvoice(frmProformaInvoice frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromProformaInvoice = true;
        //    this.frmProformaInvoice = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromSalesQuotation(frmSalesQuotation frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromSalesQuotation = true;
        //    this.frmSalesQuotation = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        //public void CallThisFormFromSalesReturn(frmSalesReturn frm)
        //{
        //    // Function to call from sales invoice for creating new account ledger
        //    isFromSalesreturn = true;
        //    this.frmsalesreturn = frm;
        //    isFromOtherForm = true;
        //    base.Show();
        //}
        public void DeleteLedger()
        {
            // To delete an exsitng account group
            bool isOk = true;
            if (PublicVariables._closed && oldBlnc > 0)
            {
                if (MessageBox.Show("Selected financial year has been closed. Do you want to open it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    FinancialYearSp SpFinance = new FinancialYearSp();
                    SpFinance.FinancialYearchangeStatus(false);
                    isOk = true;
                }
                else
                {
                    isOk = false;
                }
            }
            if (isOk)
            {
                AccountLedgerInfo InfoLedger = new AccountLedgerInfo();
                LedgerPostingSP SpPosting = new LedgerPostingSP();
                InfoLedger = SpLedger.AccountLedgerView(strLedgerIdForEdit);
                if (InfoLedger.Default == false)
                {

                    if (InventorySettingsInfo._messageBoxDelete)
                    {
                        if (MessageBox.Show("Do you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            //if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, "Delete", _mainMenuItem) == true)//added on 19/10/2023 sheena
                            //{
                                if (CheckExistanceOfLedgerIdInLedgerPosting() == false)
                                {
                                    SpLedger.AccountLedgerDelete(strLedgerIdForEdit);

                                    SpPosting.LedgerPostingDeleteByVoucherTypeAndVoucherNo(strLedgerIdForEdit, "Opening Balance");
                                    PartyBalanceSP SPPartyBalance = new PartyBalanceSP();

                                    SPPartyBalance.PartyBalanceDeleteByVoucherTypeVoucherNoAndReferenceType(strLedgerIdForEdit, "Opening Balance");
                                    MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearFunction();

                                }
                                else
                                {
                                    MessageBox.Show("Can't delete,reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            //}
                            //else
                            //{
                            //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                        }
                    }
                    else
                    {
                        //if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, "Delete", _mainMenuItem) == true)//added on 19/10/2023 sheena
                        //{
                            if (CheckExistanceOfLedgerIdInLedgerPosting() == false)
                            {
                                SpLedger.AccountLedgerDelete(strLedgerIdForEdit);

                                SpPosting.LedgerPostingDeleteByVoucherTypeAndVoucherNo(strLedgerIdForEdit, "Opening Balance");
                                PartyBalanceSP SPPartyBalance = new PartyBalanceSP();
                                SPPartyBalance.PartyBalanceDeleteByVoucherTypeVoucherNoAndReferenceType(strLedgerIdForEdit, "Opening Balance");
                                MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearFunction();

                            }
                            else
                            {
                                MessageBox.Show("Can't delete,reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }

                }
                else
                {
                    MessageBox.Show("Can't delete built in account ledger", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        private void frmCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Short cut keys
                if (e.Control && e.KeyCode == Keys.S)
                {
                    SaveOrEdit();
                }
                else if ((e.Control && e.KeyCode == Keys.D) && isInEditMode)
                {
                    DeleteLedger();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL19:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOk = false;
                //if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, (strLedgerIdForEdit == "" ? "Add" : "Edit"), _mainMenuItem) == true)//added on 19/10/2023 sheena
                //{
                    if (txtLedgerCode.Text == "")
                    {
                        DataTable dtGroup = new DataTable();
                        dtGroup = SpLedger.AccountLedgrGetGroupIdForLedgerCodeChecking();
                        if (cmbGroupName.SelectedValue.ToString() != "")
                        {
                            string groupid = cmbGroupName.SelectedValue.ToString();
                            DataRow[] dr = dtGroup.Select("groupId = " + groupid + "");
                            if (dr.Length > 0 && txtLedgerCode.Text == "")
                            {
                                MessageBox.Show("Please enter " + strFormType + " code", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                                isOk = true;

                        }
                    }
                    else
                    {
                        if (SpLedger.AccountLedgerExistenceOfLedgerCode(txtLedgerCode.Text.Trim(), strLedgerIdForEdit))
                        {
                            MessageBox.Show(strFormType + " Code already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            isOk = true;
                    }
                    if (isOk)
                    {
                        if (string.IsNullOrWhiteSpace(txtLedgerName.Text))
                        {
                            MessageBox.Show("Ledger name cannot be empty.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (FinanceSettingsInfo._ZatcaType == "Phase 2")
                        {
                            if (txtLedgerName.Text.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
                            {
                                MessageBox.Show("Ledger name cannot contain special characters.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        SaveOrEdit();
                    }
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbGroupName_Leave(object sender, EventArgs e)
        {
            try
            {
                ComboBox cmb = (ComboBox)sender;
                ComboValidation objGeneral = new ComboValidation();
                if (cmb.SelectedIndex == -1)
                    cmb.Text = strLedgerText;
                objGeneral.CheckCollection(cmb);

                lblGroup.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL21:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteLedger();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (InventorySettingsInfo._messageBoxClear)
                {
                    if (MessageBox.Show("Do you want to clear?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ClearFunction();
                    }
                }
                else
                {
                    ClearFunction();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL8:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (InventorySettingsInfo._messageBoxClose == true)
                {
                    if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNewArea_Click(object sender, EventArgs e)
        {
            // To open new account ledger creation form after checking privilege of current user for that form
            try
            {
                //CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
                //if (checkuserprivilege.CheckPrivilage("Area", "") == true)
                //{

                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Area", "", "Masters") == true)//added on 19/10/2023 sheena
                //{
                    // Save current area Id
                    if (cmbArea.SelectedValue != null)
                    {
                        strOldArea = cmbArea.SelectedValue.ToString();
                    }
                    else
                    {
                        strOldArea = "";
                    }
                    frmMasterCreation frmMaster = new frmMasterCreation();
                    frmMaster.MdiParent = MDIFinacPOS.MDIObj;
                    frmMaster.DoWhenComingFromCustomerForm(this, "Area");
                    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("AL70:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNewLevel_Click(object sender, EventArgs e)
        {
            // To open new account ledger creation form after checking privilege of current user for that form
            try
            {
                //CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
                //if (checkuserprivilege.CheckPrivilage("Pricing Level", "") == true)
                //{
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Pricing Level", "", "Masters") == true)//added on 19/10/2023 sheena
                //{
                //    // Save current area Id
                //    if (cmbPricingLevel.SelectedValue != null)
                //    {
                //        strOldPricingLevel = cmbPricingLevel.SelectedValue.ToString();
                //    }
                //    else
                //    {
                //        strOldPricingLevel = "";
                //    }
                //    frmMasterCreation frmMaster = new frmMasterCreation();
                //    frmMaster.MdiParent = MDIFinacAcount.MDIObj;
                //    frmMaster.DoWhenComingFromCustomerForm(this, "Pricing Level");
                //    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("AL72:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnNewCurrency_Click(object sender, EventArgs e)
        {
            // To open new account ledger creation form after checking privilege of current user for that form
            try
            {
                //CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
                //if (checkuserprivilege.CheckPrivilage("New Currency", "") == true)
                //{
                if (spUsergroupSettings.CheckUSerGroupPrivilage("New Currency", "", "Masters") == true)//added on 19/10/2023 sheena
                {
                    // Save current area Id
                    if (cmbCurrency.SelectedValue != null)
                    {
                        strOldCurrency = cmbCurrency.SelectedValue.ToString();
                    }
                    else
                    {
                        strOldCurrency = "";
                    }
                    //frmCurrency frmcurrency = new frmCurrency();
                    //frmcurrency.MdiParent = MDIFinacAcount.MDIObj;
                    //frmcurrency.DoWhenComingFromCustomerForm(this);
                    //this.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("AL74:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNewMarket_Click(object sender, EventArgs e)
        {
            // To open new market creation form after checking privilege of current user for that form
            try
            {
                //CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
                //if (checkuserprivilege.CheckPrivilage("Market", "") == true)
                //{

                if (spUsergroupSettings.CheckUSerGroupPrivilage("Market", "", "Masters") == true)//added on 19/10/2023 sheena
                {
                    // Save current Group Id
                    if (cmbMarket.SelectedValue != null)
                    {
                        strOldMarket = cmbMarket.SelectedValue.ToString();
                    }
                    else
                    {
                        strOldMarket = "";
                    }
                    //frmMarket frmmarket = new frmMarket();
                    //frmmarket.MdiParent = MDIFinacAcount.MDIObj;
                    //frmmarket.DoWhenComingFromCustomerForm(this, cmbArea.SelectedValue.ToString());
                    //this.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("AL73:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbArea_Enter(object sender, EventArgs e)
        {
            strAreaText = cmbArea.Text;
            lblArea.ForeColor = Color.Red;
        }

        private void cmbArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            } 
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                SendKeys.Send("{F10}");
                btnNewArea_Click(sender, e);
            }
        }

        private void cmbArea_KeyUp(object sender, KeyEventArgs e)
        {
            strAreaText = cmbArea.Text;
        }

        private void cmbArea_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbArea.SelectedIndex == -1)
                    cmbArea.Text = strAreaText;
                if (cmbArea.SelectedValue == null)
                {
                    cmbArea.SelectedValue = "1";
                }

                lblArea.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL25:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                strAreaText = cmbArea.Text;
                FillMarket();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL20:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbBill_Leave(object sender, EventArgs e)
        {
            //interest can be done only if bill wise details are kept
            try
            {
                //if (cmbBill.Text == "No")
                //    cmbIntreast.Text = "No";

                lblBillByBill.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL79:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbCurrency_Enter(object sender, EventArgs e)
        {
            strCurrencyText = cmbCurrency.Text;
            lblCurrency.ForeColor = Color.Red;
        }

        private void cmbCurrency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            } 
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                SendKeys.Send("{F10}");
                btnNewCurrency_Click(sender, e);
            }
        }

        private void cmbCurrency_KeyUp(object sender, KeyEventArgs e)
        {
            strCurrencyText = cmbCurrency.Text;
        }

        private void cmbCurrency_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbCurrency.SelectedValue == null)
                {
                    cmbCurrency.SelectedValue = PublicVariables._currencyId;

                }
                if (cmbCurrency.SelectedIndex == -1)
                    cmbCurrency.Text = strCurrencyText;

                lblCurrency.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL24:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            strCurrencyText = cmbCurrency.Text;
        }

        private void cmbGroupName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroupName.SelectedValue != null && !isFormload)
                {
                    DataTable dtbl = new DataTable();
                    AccountGroupSP SpGroup = new AccountGroupSP();
                   // cmbIntreast.Enabled = false;
                   // cmbIntreast.Text = "No";
                    isSundryDebtorOrCreditor = false;
                 //   cmbIntreast.Enabled = false;
                    GenerateLedgerCode(cmbGroupName.SelectedValue.ToString());
                    //------------------------------------------------------------------------------
                    dtbl = SpGroup.AccountLedgerGetGroupUnder(cmbGroupName.SelectedValue.ToString());
                    if (cmbGroupName.SelectedValue.ToString() == "1" || cmbGroupName.SelectedValue.ToString() == "4")
                    {
                        cmbDebit.Text = "Dr";
                    }
                    else if (cmbGroupName.SelectedValue.ToString() == "2" || cmbGroupName.SelectedValue.ToString() == "3")
                    {
                        cmbDebit.Text = "Cr";
                    }
                    else
                    {
                        if (dtbl != null)
                        {
                            if (dtbl.Rows.Count > 0)
                            {
                                DataRow[] drToMrg = dtbl.Select("groupUnder ='1' OR groupUnder ='4' OR groupUnder ='12' OR groupUnder ='16' OR groupUnder ='23' OR groupUnder ='9' OR groupUnder ='15' OR groupUnder ='18' OR groupUnder ='19' OR groupUnder ='21' OR groupUnder ='30'");
                                if (drToMrg.Length > 0)
                                {
                                    cmbDebit.Text = "Dr";
                                }
                                else
                                {
                                    cmbDebit.Text = "Cr";
                                }
                            }
                        }
                    }
                    //------------------------------------------------------------------------------
                    if (cmbGroupName.SelectedValue.ToString() != "28" && cmbGroupName.SelectedValue.ToString() != "29" && cmbGroupName.SelectedValue.ToString() != "19" && cmbGroupName.SelectedValue.ToString() != "20" && cmbGroupName.SelectedValue.ToString() != "26" && cmbGroupName.SelectedValue.ToString() != "32" && cmbGroupName.SelectedValue.ToString() != "39" && cmbGroupName.SelectedValue.ToString() != "40")
                    {
                        // Checking whether the selected legder is under sundry debtor or creditor




                        // Checking whether the selected legder is under sundry debtor or creditor
                        foreach (DataRow row in dtbl.Rows)
                        {
                            if (row.ItemArray[0].ToString() == "28" || row.ItemArray[0].ToString() == "29")
                            {
                                isSundryDebtorOrCreditor = true;
                                //if (SettingsInfo._interestCalculation)
                                //{
                                //    cmbIntreast.Enabled = true;
                                //}
                                //else
                                //{
                                //    cmbIntreast.Enabled = false;
                                //}
                                break;
                            }

                        }
                        if (!isSundryDebtorOrCreditor)
                        {
                            // Checkign whetehr under group for which interest calculaiton is true
                            foreach (DataRow row in dtbl.Rows)
                            {
                                if (row.ItemArray[0].ToString() == "19" || row.ItemArray[0].ToString() == "20" || row.ItemArray[0].ToString() == "26" || row.ItemArray[0].ToString() == "32" || row.ItemArray[0].ToString() == "39" || row.ItemArray[0].ToString() == "40")
                                {
                                    //if (SettingsInfo._interestCalculation)
                                    //{
                                    //    cmbIntreast.Enabled = true;
                                    //}
                                    //else
                                    //{
                                    //    cmbIntreast.Enabled = false;
                                    //}
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        // selcted one is under sundry debtor or creditor or any other for whihc intrest calculation is true
                        if (cmbGroupName.SelectedValue.ToString() == "28" || cmbGroupName.SelectedValue.ToString() == "29")
                        {
                            isSundryDebtorOrCreditor = true;
                            //if (SettingsInfo._interestCalculation)
                            //{
                            //    cmbIntreast.Enabled = true;
                            //}
                            //else
                            //{
                            //    cmbIntreast.Enabled = false;
                            //}

                        }
                        else
                        {
                            if (cmbGroupName.SelectedValue.ToString() == "19" || cmbGroupName.SelectedValue.ToString() == "20" || cmbGroupName.SelectedValue.ToString() == "26" || cmbGroupName.SelectedValue.ToString() == "32" || cmbGroupName.SelectedValue.ToString() == "39" || cmbGroupName.SelectedValue.ToString() == "40")
                            {
                                //if (SettingsInfo._interestCalculation)
                                //{
                                //    cmbIntreast.Enabled = true;
                                //}
                                //else
                                //{
                                //    cmbIntreast.Enabled = false;
                                //}
                            }
                        }
                    }
                }
                DataTable dtblYear = new FinancialYearSp().FinancialYearGetFirst();
                if (dtblYear != null)
                {
                    if (dtblYear.Rows.Count > 0)
                    {
                        if (DateTime.Parse(dtblYear.Rows[0]["fromDate"].ToString()) != PublicVariables._fromDate)
                        {
                            txtBlnc.ReadOnly = true;
                            cmbDebit.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL17:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbIntreast_Leave(object sender, EventArgs e)
        {
            //interest can be done only if bill wise details are kept
            //try
            //{
            //    if (cmbIntreast.Text == "Yes")
            //        cmbBill.Text = "Yes";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("AL79:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void cmbMarket_Enter(object sender, EventArgs e)
        {
            strMarketText = cmbMarket.Text;
            lblMarket.ForeColor = Color.Red;
        }

        private void cmbMarket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            } 
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                SendKeys.Send("{F10}");
                btnNewMarket_Click(sender, e);
            }
        }

        private void cmbMarket_KeyUp(object sender, KeyEventArgs e)
        {
            strMarketText = cmbMarket.Text;
        }

        private void cmbMarket_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbMarket.SelectedIndex == -1)
                    cmbMarket.Text = strMarketText;
                if (cmbMarket.SelectedValue == null)
                {
                    cmbMarket.SelectedValue = "1";
                }
                lblMarket.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL22:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbMarket_SelectedIndexChanged(object sender, EventArgs e)
        {
            strMarketText = cmbMarket.Text;
            FillRoute();
        }

        private void cmbPricingLevel_Enter(object sender, EventArgs e)
        {
            strPLText = cmbPricingLevel.Text;
            lblPricingLevel.ForeColor = Color.Red;
        }

        private void cmbPricingLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            else if (e.Alt && e.KeyCode == Keys.C)
            {
                SendKeys.Send("{F10}");
                btnNewLevel_Click(sender, e);
            }
        }

        private void cmbPricingLevel_KeyUp(object sender, KeyEventArgs e)
        {
            strPLText = cmbPricingLevel.Text;
        }

        private void cmbPricingLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbPricingLevel.SelectedIndex == -1)
                    cmbPricingLevel.Text = strPLText;
                if (cmbPricingLevel.SelectedValue == null)
                {
                    cmbPricingLevel.SelectedValue = "1";
                }

                lblPricingLevel.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL23:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbPricingLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            strPLText = cmbPricingLevel.Text;
        }

        private void txtBlnc_Enter(object sender, EventArgs e)
        {
            // To make text box content as "" when amount is zero
            try
            {
                lblOpeningBalance.ForeColor = Color.Red;

                if (decimal.Parse(txtBlnc.Text) == 0)
                {
                    txtBlnc.Text = "";
                }
            }
            catch (Exception)
            {
                // Message box not needed in catch
                txtBlnc.Text = "";
            }
        }

        private void txtBlnc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    
                }
                else
                {
                    objComboValidation.DecimalValidation(sender, e, false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("AL30:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtBlnc_Leave(object sender, EventArgs e)
        {
            // To assign 0.00 to amount when amount is null
            try
            {
                try
                {
                    decimal.Parse(txtBlnc.Text);
                }
                catch (Exception)
                {
                    txtBlnc.Text = "0";
                }
                lblOpeningBalance.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL69:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCreditLimit_Enter(object sender, EventArgs e)
        {
            // To make text box content as "" when amount is zero
            try
            {
                lblCreditLimit.ForeColor = Color.Red;

                if (decimal.Parse(txtCreditLimit.Text) == 0)
                {
                    txtCreditLimit.Text = "";
                }
            }
            catch (Exception)
            {
                // Message box not needed in catch
                txtCreditLimit.Text = "";
            }
        }

        private void txtCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    
                }
                else
                {
                    objComboValidation.DecimalValidation(sender, e, false);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("AL40:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCreditLimit_Leave(object sender, EventArgs e)
        {
            // To assign 0.00 to amount when amount is null
            try
            {
                try
                {
                    decimal.Parse(txtCreditLimit.Text);
                }
                catch (Exception)
                {
                    txtCreditLimit.Text = "0.00";
                }
                lblCreditLimit.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL68:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCreditPeriod_Enter(object sender, EventArgs e)
        {
            // To make text box content as "" when amount is zero
            try
            {
                lblCreditPeriod.ForeColor = Color.Red;

                if (int.Parse(txtCreditPeriod.Text) == 0)
                {
                    txtCreditPeriod.Text = "";
                }
            }
            catch (Exception)
            {
                // Message box not needed in catch
                txtCreditPeriod.Text = "";
            }
        }

        private void txtCreditPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    
                }
                else
                {
                    objComboValidation.IntigerFieldKeypress(sender, e);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("AL39:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtCreditPeriod_Leave(object sender, EventArgs e)
        {
            // To assign 0.00 to amount when amount is null
            try
            {
                try
                {
                    int.Parse(txtCreditPeriod.Text);
                }
                catch (Exception)
                {
                    txtCreditPeriod.Text = "0";
                }
                lblCreditPeriod.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL67:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            lblLedgerName.ForeColor = Color.Black;
            
        }
        public string GoogleTranslate(string inputtext, string fromlangid, string tolangid)
        {
            string step4 = "";
            try
            {
                inputtext = HttpUtility.HtmlAttributeEncode(inputtext);
                using (WebClient step1 = new WebClient())
                {
                    step1.Encoding = Encoding.UTF8;
                    string step2 = step1.DownloadString("https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=" + tolangid + "&hl=" + fromlangid + "&dt=t&dt=bd&dj=1&source=icon&q=" + inputtext);
                    Newtonsoft.Json.Linq.JObject step3 = Newtonsoft.Json.Linq.JObject.Parse(step2);
                    step4 = step3.SelectToken("sentences[0]").SelectToken("trans").ToString();
                    
                }
            }
            catch
            {
                
                
            }
            return step4;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmLookupMaster frmlookup = new frmLookupMaster();
            //frmlookup.MdiParent = MDIFinacAcount.MDIObj;

            
            if (isCustomerCreation == true)
            {
              //  frmlookup.strSearchCondition = " groupId='44' ";
                frmlookup.strSearchCondition = "  EXISTS (    SELECT groupId    FROM tbl_AccountGroup    WHERE groupUnder = '29' AND groupId = tbl_AccountLedger.groupId) ";
                frmlookup.strSearchingName = "Customer";
                frmlookup.strFromFormName = "Customer";
            }
            else
            {
               // frmlookup.strSearchCondition = " groupId='43' ";
                frmlookup.strSearchCondition = "  EXISTS (    SELECT groupId    FROM tbl_AccountGroup    WHERE groupUnder = '28' AND groupId = tbl_AccountLedger.groupId) ";
                frmlookup.strSearchingName = "Vendor";
                frmlookup.strFromFormName = "Vendor";
            }

            frmlookup.strSearchColumn = "LedgerName";
            frmlookup.strSearchOrder = " LedgerName ";
            frmlookup.strSearchQry = " ledgerId,LedgerCode,LedgerName ";
            frmlookup.strSearchTable = " tbl_AccountLedger(NOLOCK) ";
            frmlookup.strMasterIdColumnName = "ledgerId";
            frmlookup.IntSearchFiledCount = 3;

            frmlookup.DoWhenComingFromCustomerForm(this);
            //this.Enabled = false;
        }
        public void DowhenReturningFromSearchForm(string strId)
        {
            try
            {
                this.Enabled = true; 
                if (strId != "")
                {
                    strLedgerIdForEdit = strId.ToString();
                    FillControlsForEdit();
                }
                
                txtLedgerCode.Focus();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //DoWhenQuitingForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL27:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public void DoWhenQuitingForm()
        //{
        //    // Function to execute at the time of closing the form
        //    // To return to the form rom which this form is called
        //    if (isFromPurchaseInvoice)
        //    {
        //        frmPurchaseInvoice.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromSalesInvoice)
        //    {
        //        frmSalesInvoice.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromSalesInvoiceSimple)
        //    {
        //        frmSalesInvoiceSimp.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromPayableVoucher)
        //    {
        //        frmPayable.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromReceivableVoucher)
        //    {
        //        frmReceivable.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromSalesOrder)
        //    {
        //        frmSalesOrder.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromSalesQuotation)
        //    {
        //        frmSalesQuotation.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromDeliveryNote)
        //    {
        //        frmDeliveryNote.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromRejectionIn)
        //    {
        //        frmRejectionIn.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromPurchaseInvoicePOS)
        //    {
        //        frmPurchaseInvoicePOS.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromPurchaseOrder)
        //    {
        //        frmPurchOrder.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromMaterialreciept)
        //    {
        //        frmMatrialreceipt.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromRejectionOut)
        //    {
        //        frmRejectionout.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromPurchasereturn)
        //    {
        //        frmPurchasereturn.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //    else if (isFromSalesreturn)
        //    {
        //        frmsalesreturn.DoWhenReturningFromAccountLedgerForm(strLedgerIdForOtherForms);
        //    }
        //}

        private void btnCostCentre_Click(object sender, EventArgs e)
        {
            try
            {

                decimal dctotal = decimal.Parse(txtBlnc.Text.Replace(cmbCurrency.Text, "").Trim());
                if (dctotal != 0m)
                {
                    //this.Enabled = false;
                    //frmCostCentreOpening frmCost = new frmCostCentreOpening();
                    //frmCost.MdiParent = MDIFinacAcount.MDIObj;
                    //frmCost.WindowState = FormWindowState.Normal;
                    //frmCost.Show();

                    //frmCost.CallFromCustomerForm(this, dtblCostCentre, dctotal, cmbCurrency.Text, cmbDebit.Text);

                    //frmCost.Activate();
                    //frmCost.BringToFront();
                }

            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("PI83:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public void CallFromCostCentre(DataTable dtbl)
        {
            // function to call from costcentre opening form
            try
            {
                dtblCostCentre = dtbl;
                decimal dcTotalCostcentre = 0;
                if (dtblCostCentre.Rows.Count > 0)
                {
                    dcTotalCostcentre = decimal.Parse(dtblCostCentre.Compute("Sum(amount)", string.Empty).ToString());
                }  
                this.Enabled = true;
                this.Activate();
                this.BringToFront();              
            }
            catch (Exception ex)
            {
                MessageBox.Show("PI45:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void AddtoCostCentreOpening(string ledgerid)
        {
            // saving additional cost details to corresponding table
            try
            {               
                string drcr = "";
                if (dtblCostCentre.Rows.Count == 0)
                {
                    DataRow Dr1 = dtblCostCentre.NewRow();
                    Dr1["costCentreId"] = "1";
                    Dr1["amount"] = 0;
                    Dr1["DrCr"] = "Dr";

                    dtblCostCentre.Rows.Add(Dr1);
                }
                if (dtblCostCentre != null && dtblCostCentre.Rows.Count != 0)
                {
                    // save to CostCentreOpening table
                    CostCentreOpeningSP SPCost = new CostCentreOpeningSP();
                    CostCentreOpeningInfo InfoCost = new CostCentreOpeningInfo();

                    InfoCost.Extra1 = "";
                    InfoCost.Extra2 = "";

                    foreach (DataRow dr in dtblCostCentre.Rows)
                    {
                        if (dr["costCentreId"] != null && dr["costCentreId"].ToString().Trim() != "")
                        {
                            InfoCost.CostCentreId = dr["costCentreId"].ToString();
                            drcr = dr["DrCr"].ToString();
                            try
                            {
                                if (drcr == "Dr")
                                {
                                    InfoCost.DrAmount = decimal.Parse(dr["amount"].ToString());
                                    InfoCost.CrAmount = 0;
                                }
                                else if (drcr == "Cr")
                                {
                                    InfoCost.CrAmount = decimal.Parse(dr["amount"].ToString());
                                    InfoCost.DrAmount = 0;
                                }
                            }
                            catch { }

                            //------------ add details to cost centre opening cost-----------------------
                            InfoCost.LedgerId = ledgerid;
                            InfoCost.BranchId = PublicVariables._branchId;
                            SPCost.CostCentreOpeningAdd(InfoCost);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PI22:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void bwrkControlSettings_DoWork(object sender, DoWorkEventArgs e)
        {
            //FinacFormControl objGeneral = new FinacFormControl();
            //objGeneral.formSettings(this);
        }

        private void bwrkControlSettings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (frmCompanyProgress != null && frmCompanyProgress.Visible)
            //    frmCompanyProgress.Close();
        }

        private void txtLedgerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtMailName.Text))
                {
                    txtMailName.Text = GoogleTranslate(txtLedgerName.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtMailName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtBlnc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbDebit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtBuildingNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtBuildingNoARB.Text))
                {
                    txtBuildingNoARB.Text = GoogleTranslate(txtBuildingNo.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtBuildingNoARB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtAdditionalNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtAdditionalNoARB.Text))
                {
                    txtAdditionalNoARB.Text = GoogleTranslate(txtAdditionalNo.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtAdditionalNoARB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtStreetName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtStreetARB.Text))
                {
                    txtStreetARB.Text = GoogleTranslate(txtStreetName.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtStreetARB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtdistrict_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtDistrictARB.Text))
                {
                    txtDistrictARB.Text = GoogleTranslate(txtdistrict.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtDistrictARB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtPostBoxNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtPostBoxNoARB.Text))
                {
                    txtPostBoxNoARB.Text = GoogleTranslate(txtPostBoxNo.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtPostBoxNoARB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtCityNameARB.Text))
                {
                    txtCityNameARB.Text = GoogleTranslate(txtCity.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCityNameARB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtCountryARB.Text))
                {
                    txtCountryARB.Text = GoogleTranslate(txtCountry.Text, "en", "ar");
                }
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCountryARB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCreditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCreditLimit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbCreditLimitStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtTin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtLedgerCode_Enter(object sender, EventArgs e)
        {
            lblLedgerCode.ForeColor = Color.Red;
        }

        private void txtLedgerCode_Leave(object sender, EventArgs e)
        {
            lblLedgerCode.ForeColor = Color.Black;
        }

        private void txtLedgerName_Enter(object sender, EventArgs e)
        {
            lblLedgerName.ForeColor = Color.Red;
        }

        private void txtMailName_Enter(object sender, EventArgs e)
        {
            lblLedgerNameArabic.ForeColor = Color.Red;
        }

        private void txtMailName_Leave(object sender, EventArgs e)
        {
            lblLedgerNameArabic.ForeColor = Color.Black;
        }

        private void cmbGroupName_Enter(object sender, EventArgs e)
        {
            lblGroup.ForeColor = Color.Red;
        }

        private void cmbDebit_Enter(object sender, EventArgs e)
        {
            lblOpeningBalance.ForeColor = Color.Red;
        }

        private void cmbDebit_Leave(object sender, EventArgs e)
        {
            lblOpeningBalance.ForeColor = Color.Black;
        }

        private void txtBuildingNo_Enter(object sender, EventArgs e)
        {
            lblBuildingNo.ForeColor = Color.Red;
        }

        private void txtBuildingNo_Leave(object sender, EventArgs e)
        {
            lblBuildingNo.ForeColor = Color.Black;
            
        }

        private void txtAdditionalNo_Enter(object sender, EventArgs e)
        {
            lblAdditionalNo.ForeColor = Color.Red;
        }

        private void txtAdditionalNo_Leave(object sender, EventArgs e)
        {
            lblAdditionalNo.ForeColor = Color.Black;
            
        }

        private void txtStreetName_Enter(object sender, EventArgs e)
        {
            lblStreetName.ForeColor = Color.Red;
        }

        private void txtStreetName_Leave(object sender, EventArgs e)
        {
            lblStreetName.ForeColor = Color.Black;
            
        }

        private void txtdistrict_Enter(object sender, EventArgs e)
        {
            lbldistrict.ForeColor = Color.Red;
        }

        private void txtdistrict_Leave(object sender, EventArgs e)
        {
            lbldistrict.ForeColor = Color.Black;
            
        }

        private void txtPostBoxNo_Enter(object sender, EventArgs e)
        {
            lblPostBoxNo.ForeColor = Color.Red;
        }

        private void txtPostBoxNo_Leave(object sender, EventArgs e)
        {
            lblPostBoxNo.ForeColor = Color.Black;
            
        }

        private void txtCity_Enter(object sender, EventArgs e)
        {
            lblCity.ForeColor = Color.Red;
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            lblCity.ForeColor = Color.Black;
            
        }

        private void txtCountry_Enter(object sender, EventArgs e)
        {
            lblCountry.ForeColor = Color.Red;
        }

        private void txtBuildingNoARB_Enter(object sender, EventArgs e)
        {
            lblBuildingNoArabic.ForeColor = Color.Red;
        }

        private void txtBuildingNoARB_Leave(object sender, EventArgs e)
        {
            lblBuildingNoArabic.ForeColor = Color.Black;
        }

        private void txtAdditionalNoARB_Enter(object sender, EventArgs e)
        {
            lblAdditionalNoArabic.ForeColor = Color.Red;
        }

        private void txtAdditionalNoARB_Leave(object sender, EventArgs e)
        {
            lblAdditionalNoArabic.ForeColor = Color.Black;
        }

        private void txtStreetARB_Enter(object sender, EventArgs e)
        {
            lblStreetNameArabic.ForeColor = Color.Red;
        }

        private void txtStreetARB_Leave(object sender, EventArgs e)
        {
            lblStreetNameArabic.ForeColor = Color.Black;
        }

        private void txtDistrictARB_Enter(object sender, EventArgs e)
        {
            lbldistrictArabic.ForeColor = Color.Red;
        }

        private void txtDistrictARB_Leave(object sender, EventArgs e)
        {
            lbldistrictArabic.ForeColor = Color.Black;
        }

        private void txtPostBoxNoARB_Enter(object sender, EventArgs e)
        {
            lblPostBoxNoArabic.ForeColor = Color.Red;
        }

        private void txtCityNameARB_Leave(object sender, EventArgs e)
        {
            lblCityArabic.ForeColor = Color.Black;
        }

        private void txtPostBoxNoARB_Leave(object sender, EventArgs e)
        {
            lblPostBoxNoArabic.ForeColor = Color.Black;
        }

        private void txtCityNameARB_Enter(object sender, EventArgs e)
        {
            lblCityArabic.ForeColor = Color.Red;
        }

        private void txtCountryARB_Enter(object sender, EventArgs e)
        {
            lblCountryArabic.ForeColor = Color.Red;
        }

        private void txtCountryARB_Leave(object sender, EventArgs e)
        {
            lblCountryArabic.ForeColor = Color.Black;
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            lblAddress.ForeColor = Color.Red;

            inKeyPrsCou = 0;
            txtAddress.Text = txtAddress.Text.Trim();
            if (txtAddress.Text == "")
            {
                txtAddress.SelectionStart = 0;
                txtAddress.Focus();
            }
            else
            {
                txtAddress.SelectionStart = txtAddress.Text.Length;
                txtAddress.Focus();
            }
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            lblAddress.ForeColor = Color.Black;
            
        }

        private void txtAccount_Enter(object sender, EventArgs e)
        {
            lblAccountNo.ForeColor = Color.Red;
        }

        private void txtAccount_Leave(object sender, EventArgs e)
        {
            lblAccountNo.ForeColor = Color.Black;
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            lblPhone.ForeColor = Color.Red;
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            lblPhone.ForeColor = Color.Black;
        }

        private void txtFax_Enter(object sender, EventArgs e)
        {
            lblFaxNo.ForeColor = Color.Red;
        }

        private void txtFax_Leave(object sender, EventArgs e)
        {
            lblFaxNo.ForeColor = Color.Black;
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            lblEmail.ForeColor = Color.Red;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            lblEmail.ForeColor = Color.Black;
        }

        private void cmbCreditLimitStatus_Enter(object sender, EventArgs e)
        {
            lblCreditLimitAlert.ForeColor = Color.Red;
        }

        private void cmbCreditLimitStatus_Leave(object sender, EventArgs e)
        {
            lblCreditLimitAlert.ForeColor = Color.Black;
        }

        private void cmbBill_Enter(object sender, EventArgs e)
        {
            lblBillByBill.ForeColor = Color.Red;
        }

        private void txtTin_Enter(object sender, EventArgs e)
        {
            lblVatNo.ForeColor = Color.Red;
        }

        private void txtTin_Leave(object sender, EventArgs e)
        {
            lblVatNo.ForeColor = Color.Black;
        }

        private void txtCst_Enter(object sender, EventArgs e)
        {
            lblCRNo.ForeColor = Color.Red;
        }

        private void txtCst_Leave(object sender, EventArgs e)
        {
            lblCRNo.ForeColor = Color.Black;
        }

        private void txtNarration_Enter(object sender, EventArgs e)
        {
            lblNarration.ForeColor = Color.Red;

            inKeyPrsCou = 0;
            txtNarration.Text = txtNarration.Text.Trim();
            if (txtNarration.Text == "")
            {
                txtNarration.SelectionStart = 0;
                txtNarration.Focus();
            }
            else
            {
                txtNarration.SelectionStart = txtNarration.Text.Length;
                txtNarration.Focus();
            }
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            lblNarration.ForeColor = Color.Black;
        }

        private void txtCountry_Leave(object sender, EventArgs e)
        {
            lblCountry.ForeColor = Color.Black;
            
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    inKeyPrsCou++;
                    if (inKeyPrsCou == 2)
                    {
                        inKeyPrsCou = 0;
                        if (string.IsNullOrWhiteSpace(txtAddressArabic.Text))
                        {
                            txtAddressArabic.Text = GoogleTranslate(txtAddress.Text, "en", "ar");
                        }
                        txtAddressArabic.Focus();
                    }
                    
                }
                else
                {
                    inKeyPrsCou = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PO38:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    inKeyPrsCou++;
                    if (inKeyPrsCou == 2)
                    {
                        inKeyPrsCou = 0;
                        txtUserdef1.Focus();
                    }
                }
                else
                {
                    inKeyPrsCou = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PO38:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            // To open new account ledger creation form after checking privilege of current user for that form
            try
            {
                CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
                //if (checkuserprivilege.CheckPrivilage("Account Group","") == true)
                //{
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Account Group", "", "Masters") == true)//added on 19/10/2023 sheena
                //{
                //    // Save current Group Id
                //    if (cmbGroupName.SelectedValue != null)
                //    {
                //        strOldGroupId = cmbGroupName.SelectedValue.ToString();
                //    }
                //    else
                //    {
                //        strOldGroupId = "";
                //    }
                //    frmAccountGroup frmaccountgroup = new frmAccountGroup();
                //    frmaccountgroup.MdiParent = MDIFinacPOS.MDIObj;
                //    frmaccountgroup.DoWhenComingFromCustomerForm(this);
                //    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("AL26:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DowhenReturningFromAccountGroupCreationForm(string strAccountGroupId)
        {
            // Function to call from account group creation form after creating new group
            this.Enabled = true;
            isFormload = true;
            FillAccountGroupCombo();
         
            if (strAccountGroupId != "")
            {
                // Assign newly created id
                cmbGroupName.SelectedValue = strAccountGroupId;
            }
            else if (strOldGroupId != "")
            {
                // Assign old id as new one is not created
                cmbGroupName.SelectedValue = strOldGroupId;
            }
            else
            {
                // Assign  "" as new one is not created and nothing selected while going to account ledger form
                cmbGroupName.SelectedIndex = -1;
            }
            txtLedgerName.Text = txtLedgerName.Text.Trim();

            cmbGroupName.Focus();

            isFormload = false;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }

        private void txtAddressArabic_Enter(object sender, EventArgs e)
        {
            lblAddressArabic.ForeColor = Color.Red;

            inKeyPrsCou = 0;
            txtAddressArabic.Text = txtAddressArabic.Text.Trim();
            if (txtAddressArabic.Text == "")
            {
                txtAddressArabic.SelectionStart = 0;
                txtAddressArabic.Focus();
            }
            else
            {
                txtAddressArabic.SelectionStart = txtAddressArabic.Text.Length;
                txtAddressArabic.Focus();
            }
        }

        private void txtAddressArabic_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    inKeyPrsCou++;
                    if (inKeyPrsCou == 2)
                    {
                        inKeyPrsCou = 0;
                        txtAccount.Focus();
                    }
                }
                else
                {
                    inKeyPrsCou = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PO38:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtAddressArabic_Leave(object sender, EventArgs e)
        {
            lblAddressArabic.ForeColor = Color.Black;
        }

        private void cmbLedgerType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbLedgerType_Leave(object sender, EventArgs e)
        {
            lblLedgerType.ForeColor = Color.Black;
        }

        private void cmbLedgerType_Enter(object sender, EventArgs e)
        {
            lblLedgerType.ForeColor = Color.Red;
        }

        private void btnNewRoute_Click(object sender, EventArgs e)
        {
            // To open new route creation form after checking privilege of current user for that form
            try
            {
                //CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
                //if (checkuserprivilege.CheckPrivilage("Market", "") == true)
                //{

                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Route", "", "Masters") == true)//added on 19/10/2023 sheena
                //{
                //    // Save current Group Id
                //    if (cmbRoute.SelectedValue != null)
                //    {
                //        strOldRoute = cmbRoute.SelectedValue.ToString();
                //    }
                //    else
                //    {
                //        strOldRoute = "";
                //    }
                //    frmRoute frmRoute = new frmRoute();
                //    frmRoute.MdiParent = MDIFinacAcount.MDIObj;
                //    frmRoute.DoWhenComingFromCustomerForm(this, cmbArea.SelectedValue.ToString());
                //    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("AL73:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbRoute_Enter(object sender, EventArgs e)
        {
            lblRoute.ForeColor = Color.Red;
        }

        private void cmbRoute_Leave(object sender, EventArgs e)
        {
            lblRoute.ForeColor = Color.Black;
        }

        private void cmbRoute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtUserdef1_Enter(object sender, EventArgs e)
        {
            lblUserdef1.ForeColor = Color.Red;
        }

        private void txtUserdef1_Leave(object sender, EventArgs e)
        {
            lblUserdef1.ForeColor = Color.Black;
        }

        private void txtUserdef1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtUserdef2_Enter(object sender, EventArgs e)
        {
            lblUserdef2.ForeColor = Color.Red;
        }

        private void txtUserdef2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtUserdef2_Leave(object sender, EventArgs e)
        {
            lblUserdef2.ForeColor = Color.Black;
        }

        private void txtUserdef3_Enter(object sender, EventArgs e)
        {
            lblUserdef3.ForeColor = Color.Red;
        }

        private void txtUserdef3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtUserdef3_Leave(object sender, EventArgs e)
        {
            lblUserdef3.ForeColor = Color.Black;
        }

        private void txtUserdef4_Enter(object sender, EventArgs e)
        {
            lblUserdef4.ForeColor = Color.Red;
        }

        private void txtUserdef4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtUserdef4_Leave(object sender, EventArgs e)
        {
            lblUserdef4.ForeColor = Color.Black;
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCst_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys like Backspace, Delete, etc.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void txtTin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys like Backspace, Delete, etc.
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ignore the input
            }
        }
    }
}
