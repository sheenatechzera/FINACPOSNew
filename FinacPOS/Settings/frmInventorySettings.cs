using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace FinacPOS
{
    partial class frmInventorySettings : Form
    {
        public frmInventorySettings()
        {
            InitializeComponent();
        }
        /******************************************************************************************************************
         *                                       PUBLIC VARIABLES
         *****************************************************************************************************************/
        #region PUBLIC VARIABLES
        UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();//changed on 19/10/2023 sheena    
  
        ComboValidation objComboValidation = new ComboValidation();
        TransactionsGeneralFill objGeneral = new TransactionsGeneralFill();
        AccountLedgerSP SpLedger = new AccountLedgerSP();
    

        frmProgress frmCompanyProgress = new frmProgress();
        byte[] CompanyHeaderImage = null;
        byte[] CompanyFooterImage = null;

        #endregion
        /******************************************************************************************************************
        *                                    FUNCTIONS
        *****************************************************************************************************************/


        /******************************************************************************************************************
        *                                      EVENTS
        *****************************************************************************************************************/
        #region EVENTS

        //--------------------------------------------------------------------------------------------------------------------

        private bool IsValidDateFormat(string format)
        {
            if (string.IsNullOrWhiteSpace(format))
                return false;

           // Must contain day, month, year
            if (!(format.Contains("d") && format.Contains("M") && format.Contains("y")))
                return false;

            // Allow only valid characters (d, M, y, /, -, space, etc.)
            // You can expand allowed symbols if needed
            string allowedChars = "dMy/-. :";
            foreach (char c in format)
            {
                if (!allowedChars.Contains(c))
                    return false;
            }

            //  Try to format a test date
            try
            {
                string test = new DateTime(2025, 9, 25).ToString(format);
                if (string.IsNullOrWhiteSpace(test))
                    return false;
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    int.Parse(txtExpiry.Text);

                }
                catch { txtExpiry.Text = "0"; }

                if (cmbPrinter.Text == "Dot Matrix")
                    cbDirectPrinting.Checked = true;


                InventorySettingsSP SpSettings = new InventorySettingsSP();

                InventorySettingsInfo InfoSEttings = new InventorySettingsInfo();
                InfoSEttings.InventorySettingsId = settingsId;
                InfoSEttings.ShowAllTransctn = chkShowTrnsctn.Checked;
                InfoSEttings.GridColumnHeight = int.Parse(txtRowHeight.Text);
                InfoSEttings.ShowSize = chkShowSize.Checked;
                InfoSEttings.SalePriceUpdateByCostPerc = chkSalesPriceUpdate.Checked;
                if (cbExpiry.Checked)
                {
                    InfoSEttings.ExpiryReminder = txtExpiry.Text + "+" + cmbExpiry.Text + "+Y";
                }
                else
                {
                    InfoSEttings.ExpiryReminder = txtExpiry.Text + "+" + cmbExpiry.Text + "+N";
                }
                InfoSEttings.LowStockReminderPopUp = cbLowStockReminder.Checked;
                InfoSEttings.StockCalculatingMethod = cmbStockValue.Text;

                InfoSEttings.NegativeStockStatus = cmbNegative.Text;
                InfoSEttings.ExpiryStatus = cmbExpiryStatus.Text;
                InfoSEttings.RoundOff = cbroundOff.Checked;
                InfoSEttings.maintainGodown = chkMaintainGodown.Checked;
                InfoSEttings.maintainRack = chkMaintainRack.Checked;
                InfoSEttings.MessageBoxAddEdit = cbSave.Checked;
                InfoSEttings.MessageBoxClose = cbClose.Checked;
                InfoSEttings.MessageBoxRowRemove = cbRemove.Checked;
                InfoSEttings.MessageBoxDelete = cbDelete.Checked;
                InfoSEttings.MessageBoxPrint = chkPrint.Checked;
                InfoSEttings.MessageBoxClear = chkClear.Checked;
                InfoSEttings.ShowBalanceLabel = cbxBalanceLabel.Checked;
                InfoSEttings.CurrencySuffix = cbxCurrencyPrefix.Checked;
                InfoSEttings.Printer = cmbPrinter.Text;              
                InfoSEttings.Vehicle = cbxVehicle.Checked;
                InfoSEttings.ShowSalesmanInPrint = cbSalesMan.Checked;
                InfoSEttings.ShowCompanyForEstimatePrint = cbShowCompanyDetForEstimatePrint.Checked;
                InfoSEttings.PaperOut = txtPaperOut.Text == "" ? 0 : int.Parse(txtPaperOut.Text.Trim());
                InfoSEttings.showPartNoInLookup = chkShowPartNoinLookup.Checked;
                InfoSEttings.showLocationInLookUp = chkShowLocationinlookup.Checked;
                InfoSEttings.showAlternateCodeInLookup = chkShowAltCodeInLookup.Checked;
                InfoSEttings.BackupPath1 = txtPath1.Text == null ? "" : txtPath1.Text;
                InfoSEttings.BackupPath2 = txtPath2.Text == null ? "" : txtPath2.Text;
                InfoSEttings.ShowGodownWiseStock= chkShowGodownWiseStock.Checked;

                SpSettings.InventorySettingsEdit(InfoSEttings);

                SalesSettingsInfo SalesInfoSEttings = new SalesSettingsInfo();
                SalesInfoSEttings.SaleSettingsId = settingsId;
                SalesInfoSEttings.showSize = chkSalesShowSize.Checked;
                SalesInfoSEttings.showProductCode = chkshowProductCode.Checked;
                SalesInfoSEttings.showProductDescription = chkshowProductDescription.Checked;
                SalesInfoSEttings.showPaymntInSalesOrder = chkShowPayInOrder.Checked;
                SalesInfoSEttings.QuotationValidity = txtQuotationValidity.Text == null ? "" : txtQuotationValidity.Text;
                SalesInfoSEttings.QuotationDeliveredWithIn = txtDeliveredWithIn.Text == null ? "" : txtDeliveredWithIn.Text;
                SalesInfoSEttings.QuotationPaymntTerms = txtQuotationPaymntTerms.Text == null ? "" : txtQuotationPaymntTerms.Text;
                SalesInfoSEttings.GridRowHeight = txtGridRowHeight.Text == "" ? 0 : int.Parse(txtGridRowHeight.Text);
                SalesInfoSEttings.ShowFreeQtyColumns = chkShowFreeQtyColumns.Checked;
                SalesInfoSEttings.ActivateRetention = chkActivateRetention.Checked;

                //Add YBN ON 04-03-2024
                SalesInfoSEttings.ProductNameColumnWidth = Convert.ToInt32(txtProductNameColumnWidth.Text);
                SalesInfoSEttings.ProductDiscriptionColumnWidth = Convert.ToInt32(txtProductDiscriptionColumnWidth.Text);
                SalesInfoSEttings.SalesOrderTaxType = cmbSalesOrderTaxType.Text;
                SalesInfoSEttings.TickPrintAfterSave = cbTickPrintAfterSAve.Checked;
                SalesInfoSEttings.DirectPrint = cbDirectPrinting.Checked;
                if (cmbBank.SelectedValue != null)
                {
                    if (cmbBank.SelectedValue.ToString() != "0" )
                        SalesInfoSEttings.DefaultBank = cmbBank.SelectedValue.ToString();
                }
                else
                    SalesInfoSEttings.DefaultBank = "0";
                SalesInfoSEttings.ShowPaymentInReport = chkShowPaymnt.Checked;
                SalesInfoSEttings.ApplyDiscountLimit = chkApplyDiscLimit.Checked;
                SalesInfoSEttings.ShowBillProfit = chkShowBillProfit.Checked;
                SalesInfoSEttings.SalesInvoiceType= cmbSalesInvoiceType.Text;
                SalesInfoSEttings.ShowVehicleDetails = chkShowVehicle.Checked;
                SalesInfoSEttings.ShowLineDiscount = chkShowLineDiscount.Checked;
                if (cmbRoundOff.Text == "")
                    SalesInfoSEttings.AutomaticRoundOff = "NA";
                else
                    SalesInfoSEttings.AutomaticRoundOff = cmbRoundOff.Text.ToString();
                SalesInfoSEttings.AdvancePerc = chkAdvPerc.Checked;
                SalesInfoSEttings.BillDiscountPer = cbBilldicountPer.Checked;
                SalesInfoSEttings.ShowPurchaseRate = cbPurchaseRate.Checked;
                SalesInfoSEttings.ShowStockinLookup = chkShowStock.Checked;
                SalesInfoSEttings.ActivatIntermediateSearch = chkActivateIntermediateSearch.Checked;
                SalesInfoSEttings.ShowPartNo = chkPartNo.Checked;//sheena 26-06-2025
                SalesInfoSEttings.ShowLookupForBarcode = chkShowLookupBarcode.Checked;//sheena 29-07-2025
               // SalesInfoSEttings.ShowAlternateCodeInLookup = chkShowAltCodeInLookup.Checked;//sheena 29-07-2025
                SpSettings.SalesSettingsEdit(SalesInfoSEttings);

                //Add YBN ON 04-03-2024
                PurchaseSettingsInfo infoPurchaseSettingsInfo = new PurchaseSettingsInfo();
                infoPurchaseSettingsInfo.PurchaseSettingsId = PurchasesettingsId;
                infoPurchaseSettingsInfo.showSize = cbShowSizePurchase.Checked;
                infoPurchaseSettingsInfo.showProductCode = cbShowProductCodePurchase.Checked;
                infoPurchaseSettingsInfo.showProductDescription = cbShowProductDescriptionPurchase.Checked;
                infoPurchaseSettingsInfo.ActivateRetention = cbActivateRetentionPurchase.Checked;
                infoPurchaseSettingsInfo.GridRowHeight = txtGridRowHeightPurchase.Text == "" ? 0 : int.Parse(txtGridRowHeightPurchase.Text);
                infoPurchaseSettingsInfo.ShowFreeQtyColumns = cbShowFreeQtyColumnsPurchase.Checked;
                infoPurchaseSettingsInfo.ProductNameColumnWidth = Convert.ToInt32(txtProductNameColumnWidthPurchase.Text);
                infoPurchaseSettingsInfo.ProductDiscriptionColumnWidth = Convert.ToInt32(txtProductDiscriptionColumnWidthPurchase.Text);
                infoPurchaseSettingsInfo.VendorInvoiceChecking = cbVendorInvoiceChecking.Checked;
                infoPurchaseSettingsInfo.TickPrintAfterSave = cbPrintAfterSave.Checked;
                infoPurchaseSettingsInfo.DirectPrint = cbPurchDirectPrinting.Checked;
                infoPurchaseSettingsInfo.ChangeSalesPricInPurcchase = chkChangeSalesPrice.Checked;

                infoPurchaseSettingsInfo.ShowLookupForBarcode = chkPurShowLookupBarcode.Checked;//sheena 30-07-2025
             //   infoPurchaseSettingsInfo.ShowAlternateCodeInLookup = chkPurShowAlterNo.Checked;//sheena 30-07-2025
                infoPurchaseSettingsInfo.ShowPartNo = chkPurShowPartNo.Checked;//sheena30-07-2025
                SpSettings.PurchaseSettingsEdit(infoPurchaseSettingsInfo);


                FinanceSettingsInfo infoFinanceSettings = new FinanceSettingsInfo();
                infoFinanceSettings.FinanceSettingsId = int.Parse(FinanceSettingsId);
                infoFinanceSettings.ActivateTax = cbTax.Checked;
                infoFinanceSettings.BillByBill = cbBillByBill.Checked;
                infoFinanceSettings.ActivateInterstCalc = cbInterest.Checked;
                infoFinanceSettings.UseMultiCurrency = cbMultiCurrency.Checked;
                infoFinanceSettings.SufPrefixVoucherNoGen = cbSuffix.Checked;
                infoFinanceSettings.ActivateCosteCentre = chkCostCetre.Checked;
                infoFinanceSettings.NegativeCashTransaction = cmbCash.Text;
                infoFinanceSettings.AccountsCalcMethod = cmbAccCalcMethod.Text;
                infoFinanceSettings.TaxType = cmbTax.Text;
                infoFinanceSettings.FormType = cmbForm.Text;
                infoFinanceSettings.VatIncluded = cbVat.Checked;
                infoFinanceSettings.VatandCessIncluded = cbCess.Checked;
                infoFinanceSettings.extra1 = "";
                infoFinanceSettings.AccountsPosting = chkPosting.Checked;
                infoFinanceSettings.ZatcaType = cmbZatcaType.Text;
                infoFinanceSettings.ShowCompanyHeader = chkCompanyHeader.Checked;
                infoFinanceSettings.ShowCompanyFooter = chkCompanyFooter.Checked;
                infoFinanceSettings.CompanyHeader = CompanyHeaderImage;
                infoFinanceSettings.CompanyFooter = CompanyFooterImage;
                infoFinanceSettings.RoundDecimal = int.Parse(cmbRoundDecimal.Text.Trim());
                infoFinanceSettings.EnablePOS = chkEnablePOS.Checked;
                infoFinanceSettings.Userdef1 = tUserdef1.Text;
                infoFinanceSettings.Userdef2 = tUserdef2.Text;
                infoFinanceSettings.Userdef3 = tUserdef3.Text;
                infoFinanceSettings.Userdef4 = tUserdef4.Text;
                infoFinanceSettings.DateFormat = cmbDateFormat.Text;
                String strDateFormat = cmbDateFormat.Text;
                try
                {
                    if (IsValidDateFormat(strDateFormat))
                    {
                        infoFinanceSettings.DateFormat = strDateFormat;
                        if (!string.IsNullOrWhiteSpace(txtPaymentMode.Text))
                        {
                            // Split multiline text to array
                            string[] modes = txtPaymentMode.Text
        .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Trim())
        .Distinct()
        .ToArray();

                            string modeString = string.Join(",", modes);
                            infoFinanceSettings.PaymentMode = modeString;
                        }
                        else
                            infoFinanceSettings.PaymentMode = "";
                        infoFinanceSettings.ShowReminder = chkShowReminder.Checked;
                        SpSettings.FinanceSettingsEdit(infoFinanceSettings);
                    }
                    else
                    {
                        MessageBox.Show("Invalid date format. Please use a valid Date format ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbDateFormat.Focus();
                        return;
                    }

                }
                catch { }
                infoFinanceSettings.ValidateVatNumber = chkValidateVAT.Checked;

                SpSettings.FinanceSettingsEdit(infoFinanceSettings);



                VanSaleSettingsInfo infoVansaleSettings = new VanSaleSettingsInfo();
                infoVansaleSettings.VanSaleSettingsId = VanSaleSettingsId;
                infoVansaleSettings.DirectSalesPosting = chkDirectPosting.Checked;
                infoVansaleSettings.extra1 = "";
                infoVansaleSettings.extra2 = "";
                infoVansaleSettings.BranchId = PublicVariables._branchId;
                infoVansaleSettings.SalesPostingDuration = cmbDuration.Text.ToString();
                infoVansaleSettings.BillDiscountIncludeVat = chkBillDiscountInVat.Checked;
                infoVansaleSettings.EnableVanSale= chkEnableVANSale.Checked;
                infoVansaleSettings.ShowPurchaseCostInProduct = chkShowPurchaseCost.Checked;

                SpSettings.VanSaleSettingsEdit(infoVansaleSettings);

                MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Better to close all windows to get fulll effect of settings", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MDIFinacPOS.MDIObj.CheckInventorySettings();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       private void frmInventorySettings_Load(object sender, EventArgs e)
        {
            //this.Dock = DockStyle.Fill; 
            try
            {
                bwrk1.RunWorkerAsync();
                frmCompanyProgress.ShowInTaskbar = false;
                frmCompanyProgress.ShowFromReport();
                FillBankCombo();
               // cmbBank.SelectedIndex = -1;
                FillInventorySettings();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("AL15:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       public void FillBankCombo()
       {
           // To fill bank or cash combo
           try
           {
               cmbBank.DataSource = null;
               InventorySettingsSP SpSEttings = new InventorySettingsSP();
              // DataTable dtbl = new DataTable();
                System.Data.DataTable dtbl = SpSEttings.AccountLedgerGetBankForSettings(PublicVariables._branchId);
                dtbl = SpSEttings.AccountLedgerGetBankForSettings(PublicVariables._branchId);
               cmbBank.DataSource = dtbl;
               cmbBank.DisplayMember = "ledgerName";
               cmbBank.ValueMember = "ledgerId";
              
           }
           catch (Exception ex)
           {
               MessageBox.Show("RV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
       }
       string settingsId = "", PurchasesettingsId="",FinanceSettingsId="",VanSaleSettingsId="";
        public void FillInventorySettings()
        {
            try
            {
                cmbStockValue.Text = "Average Cost";
                InventorySettingsSP SpSEttings = new InventorySettingsSP();
                // To fill currenct settings of current branch

                InventorySettingsInfo InfoSEttings = new InventorySettingsInfo();

                InfoSEttings = SpSEttings.InventorySettingsViewAll();
                if (InfoSEttings != null)
                {
                    settingsId = InfoSEttings.InventorySettingsId.ToString();
                    chkShowTrnsctn.Checked = InfoSEttings.ShowAllTransctn;
                    txtRowHeight.Text = InfoSEttings.GridColumnHeight.ToString();
                    chkShowSize.Checked = InfoSEttings.ShowSize;
                    chkSalesPriceUpdate.Checked = InfoSEttings.SalePriceUpdateByCostPerc;
                    string str = InfoSEttings.ExpiryReminder;
                    gpbLimit.Enabled = true;
                    string[] words = str.Split('+');
                    txtExpiry.Text = words[0].ToString();
                    cmbExpiry.Text = words[1].ToString();
                    string expiryStatus = words[2].ToString();
                    if (expiryStatus == "Y")
                    {
                        cbExpiry.Checked = true;
                    }
                    else
                    {
                        cbExpiry.Checked = false;
                    }
                    cbLowStockReminder.Checked = InfoSEttings.LowStockReminderPopUp;
                    cmbStockValue.Text = InfoSEttings.StockCalculatingMethod;
                    cmbNegative.Text = InfoSEttings.NegativeStockStatus;
                    cmbExpiryStatus.Text = InfoSEttings.ExpiryStatus;
                    cbroundOff.Checked = InfoSEttings.RoundOff;
                    chkMaintainRack.Checked = InfoSEttings.maintainRack;
                    chkMaintainGodown.Checked = InfoSEttings.maintainGodown;
                    cbSave.Checked = InfoSEttings.MessageBoxAddEdit;
                    cbDelete.Checked = InfoSEttings.MessageBoxDelete;
                    cbClose.Checked = InfoSEttings.MessageBoxClose;
                    cbRemove.Checked = InfoSEttings.MessageBoxRowRemove;
                    chkPrint.Checked = InfoSEttings.MessageBoxPrint;
                    chkClear.Checked = InfoSEttings.MessageBoxClear;
                    cbxBalanceLabel.Checked = InfoSEttings.ShowBalanceLabel;
                    cbxCurrencyPrefix.Checked = InfoSEttings.CurrencySuffix;
                    cbSalesMan.Checked = InfoSEttings.ShowSalesmanInPrint;
                    cbPaperOut.Checked = InfoSEttings.PaperOut == 0 ? false : true;
                    if (cbPaperOut.Checked)
                    {
                        txtPaperOut.ReadOnly = false;
                    }
                    else
                    {
                        txtPaperOut.ReadOnly = true;
                    }
                    txtPaperOut.Text = InfoSEttings.PaperOut.ToString();
                    cbShowCompanyDetForEstimatePrint.Checked = InfoSEttings.ShowCompanyForEstimatePrint;
                    cbxVehicle.Checked = InfoSEttings.Vehicle;
                    cmbPrinter.Text = InfoSEttings.Printer;
                    chkShowPartNoinLookup.Checked = InfoSEttings.showPartNoInLookup;
                    chkShowAltCodeInLookup.Checked = InfoSEttings.showAlternateCodeInLookup;
                    chkShowLocationinlookup.Checked = InfoSEttings.showLocationInLookUp;
                    txtPath1.Text = InfoSEttings.BackupPath1;
                    txtPath2.Text = InfoSEttings.BackupPath2;
                    chkShowGodownWiseStock.Checked = InfoSEttings.ShowGodownWiseStock;

                }

                SalesSettingsInfo SalesInfoSEttings = new SalesSettingsInfo();
                SalesInfoSEttings = SpSEttings.SalesSettingsViewAll();
                if (SalesInfoSEttings != null)
                {
                    settingsId = SalesInfoSEttings.SaleSettingsId.ToString();
                    chkSalesShowSize.Checked = SalesInfoSEttings.showSize;
                    chkshowProductCode.Checked = SalesInfoSEttings.showProductCode;
                    chkshowProductDescription.Checked = SalesInfoSEttings.showProductDescription;
                    chkShowPayInOrder.Checked = SalesInfoSEttings.showPaymntInSalesOrder;
                    txtQuotationValidity.Text = SalesInfoSEttings.QuotationValidity == null ? "" : SalesInfoSEttings.QuotationValidity;
                    txtDeliveredWithIn.Text = SalesInfoSEttings.QuotationDeliveredWithIn == null ? "" : SalesInfoSEttings.QuotationDeliveredWithIn;
                    txtQuotationPaymntTerms.Text = SalesInfoSEttings.QuotationPaymntTerms == null ? "" : SalesInfoSEttings.QuotationPaymntTerms;
                    txtGridRowHeight.Text = SalesInfoSEttings.GridRowHeight.ToString() == null ? "0" : SalesInfoSEttings.GridRowHeight.ToString();
                    chkActivateRetention.Checked = SalesInfoSEttings.ActivateRetention;

                    //Add YBN ON 04-03-2024
                    txtProductNameColumnWidth.Text = SalesInfoSEttings.ProductNameColumnWidth.ToString() == null ? "0" : SalesInfoSEttings.ProductNameColumnWidth.ToString();
                    txtProductDiscriptionColumnWidth.Text = SalesInfoSEttings.ProductDiscriptionColumnWidth.ToString() == null ? "0" : SalesInfoSEttings.ProductDiscriptionColumnWidth.ToString();
                    cmbSalesOrderTaxType.Text = SalesInfoSEttings.SalesOrderTaxType.ToString();

                    cbTickPrintAfterSAve.Checked = SalesInfoSEttings.TickPrintAfterSave;
                    cbDirectPrinting.Checked = SalesInfoSEttings.DirectPrint;
                    if (SalesInfoSEttings.DefaultBank != null)
                        cmbBank.SelectedValue = SalesInfoSEttings.DefaultBank;
                    else
                        cmbBank.SelectedIndex = -1;
                    chkShowPaymnt.Checked = SalesInfoSEttings.ShowPaymentInReport;
                    chkApplyDiscLimit.Checked = SalesInfoSEttings.ApplyDiscountLimit;
                    chkShowBillProfit.Checked = SalesInfoSEttings.ShowBillProfit;
                    cmbSalesInvoiceType.Text=SalesInfoSEttings.SalesInvoiceType.ToString();
                    chkShowVehicle.Checked = SalesInfoSEttings.ShowVehicleDetails;
                    chkShowLineDiscount.Checked = SalesInfoSEttings.ShowLineDiscount;
                    cmbRoundOff.Text = SalesInfoSEttings.AutomaticRoundOff.ToString();
                    chkAdvPerc.Checked = SalesInfoSEttings.AdvancePerc;
                    cbPurchaseRate.Checked = SalesInfoSEttings.ShowPurchaseRate;
                    cbBilldicountPer.Checked = SalesInfoSEttings.BillDiscountPer;
                    chkShowStock.Checked = SalesInfoSEttings.ShowStockinLookup;
                    chkActivateIntermediateSearch.Checked = SalesInfoSEttings.ActivatIntermediateSearch;
                    chkShowFreeQtyColumns.Checked = SalesInfoSEttings.ShowFreeQtyColumns;
                    chkPartNo.Checked = SalesInfoSEttings.ShowPartNo;
                    chkShowLookupBarcode.Checked = SalesInfoSEttings.ShowLookupForBarcode;
                   // chkShowAltCodeInLookup.Checked = SalesInfoSEttings.ShowAlternateCodeInLookup;
                }

                //Add YBN ON 04-03-2024
                PurchaseSettingsInfo infoPurchaseSettings = new PurchaseSettingsInfo();
                infoPurchaseSettings = SpSEttings.PurchaseSettingsViewAll();
                if (infoPurchaseSettings != null)
                {
                    PurchasesettingsId = infoPurchaseSettings.PurchaseSettingsId.ToString();
                    cbShowSizePurchase.Checked = infoPurchaseSettings.showSize;
                    cbShowProductCodePurchase.Checked = infoPurchaseSettings.showProductCode;
                    cbShowProductDescriptionPurchase.Checked = infoPurchaseSettings.showProductDescription;
                    cbActivateRetentionPurchase.Checked = infoPurchaseSettings.ActivateRetention;
                    txtGridRowHeightPurchase.Text = infoPurchaseSettings.GridRowHeight.ToString() == null ? "0" : infoPurchaseSettings.GridRowHeight.ToString();
                    cbShowFreeQtyColumnsPurchase.Checked = infoPurchaseSettings.ShowFreeQtyColumns;
                    txtProductNameColumnWidthPurchase.Text = infoPurchaseSettings.ProductNameColumnWidth.ToString() == null ? "0" : infoPurchaseSettings.ProductNameColumnWidth.ToString();
                    txtProductDiscriptionColumnWidthPurchase.Text = infoPurchaseSettings.ProductDiscriptionColumnWidth.ToString() == null ? "0" : infoPurchaseSettings.ProductDiscriptionColumnWidth.ToString();
                    cbVendorInvoiceChecking.Checked = infoPurchaseSettings.VendorInvoiceChecking;
                    cbPrintAfterSave.Checked = infoPurchaseSettings.TickPrintAfterSave;
                    cbPurchDirectPrinting.Checked = infoPurchaseSettings.DirectPrint;
                    chkChangeSalesPrice.Checked = infoPurchaseSettings.ChangeSalesPricInPurcchase;                  
                    chkPurShowLookupBarcode.Checked = infoPurchaseSettings.ShowLookupForBarcode;
                   // chkPurShowAlterNo.Checked = infoPurchaseSettings.ShowAlternateCodeInLookup;
                    chkPurShowPartNo.Checked = infoPurchaseSettings.ShowPartNo;
                }


                FinanceSettingsInfo infoFinanceSettings = new FinanceSettingsInfo();
                infoFinanceSettings = SpSEttings.FinanceSettingsViewAll(PublicVariables._branchId);
                if (infoFinanceSettings != null)
                {
                    FinanceSettingsId = infoFinanceSettings.FinanceSettingsId.ToString();
                    cbTax.Checked = infoFinanceSettings.ActivateTax;
                    cbBillByBill.Checked = infoFinanceSettings.BillByBill;
                    cbInterest.Checked = infoFinanceSettings.ActivateInterstCalc;
                    cbMultiCurrency.Checked = infoFinanceSettings.UseMultiCurrency;
                    cbSuffix.Checked = infoFinanceSettings.SufPrefixVoucherNoGen;
                    chkCostCetre.Checked = infoFinanceSettings.ActivateCosteCentre;
                    cmbCash.Text = infoFinanceSettings.NegativeCashTransaction.ToString();
                    cmbAccCalcMethod.Text = infoFinanceSettings.AccountsCalcMethod.ToString();
                    cmbTax.Text = infoFinanceSettings.TaxType;
                    cmbForm.Text = infoFinanceSettings.FormType;
                    cbVat.Checked = infoFinanceSettings.VatIncluded;
                    cbCess.Checked = infoFinanceSettings.VatandCessIncluded;
                    chkPosting.Checked = infoFinanceSettings.AccountsPosting;
                    cmbZatcaType.Text = infoFinanceSettings.ZatcaType;

                    chkCompanyHeader.Checked = infoFinanceSettings.ShowCompanyHeader;
                    chkCompanyFooter.Checked = infoFinanceSettings.ShowCompanyFooter;
                    if (infoFinanceSettings.CompanyHeader == null)
                    {
                        GetDefaultImageHeader();
                    }
                    else
                    {
                        CompanyHeaderImage = (byte[])infoFinanceSettings.CompanyHeader;
                    }
                    MemoryStream ms = new MemoryStream(CompanyHeaderImage);
                    Image im = Image.FromStream(ms);
                    pbCompanyHeader.Image = im;

                    if (infoFinanceSettings.CompanyFooter == null)
                    {
                        GetDefaultImageFooter();
                    }
                    else
                    {
                        CompanyFooterImage = (byte[])infoFinanceSettings.CompanyFooter;
                    }
                    ms = new MemoryStream(CompanyFooterImage);
                    im = Image.FromStream(ms);
                    pblCompanyFooter.Image = im;

                    cmbRoundDecimal.Text = infoFinanceSettings.RoundDecimal.ToString();
                    chkEnablePOS.Checked = infoFinanceSettings.EnablePOS;
                    tUserdef1.Text = infoFinanceSettings.Userdef1;
                    tUserdef2.Text = infoFinanceSettings.Userdef2;
                    tUserdef3.Text = infoFinanceSettings.Userdef3;
                    tUserdef4.Text = infoFinanceSettings.Userdef4;
                    cmbDateFormat.Text = infoFinanceSettings.DateFormat;
                    if (infoFinanceSettings.PaymentMode != null && !string.IsNullOrWhiteSpace(infoFinanceSettings.PaymentMode.ToString()))
                    {
                        string modeString = infoFinanceSettings.PaymentMode.ToString();

                        // Split by commas and show each on a new line
                        txtPaymentMode.Text = string.Join(Environment.NewLine, modeString.Split(','));
                    }
                    else
                    {
                        // Nothing saved yet → keep empty
                        txtPaymentMode.Clear();
                    }
                    chkShowReminder.Checked= infoFinanceSettings.ShowReminder;
                    chkValidateVAT.Checked = infoFinanceSettings.ValidateVatNumber;
                }

                VanSaleSettingsInfo infoVansaleSettings = new VanSaleSettingsInfo();
                infoVansaleSettings = SpSEttings.VanSaleSettingsViewAll(PublicVariables._branchId);
                if (infoVansaleSettings != null)
                {
                    VanSaleSettingsId = infoVansaleSettings.VanSaleSettingsId.ToString();
                    chkDirectPosting.Checked = infoVansaleSettings.DirectSalesPosting;
                    cmbDuration.Text = infoVansaleSettings.SalesPostingDuration;
                    chkBillDiscountInVat.Checked = infoVansaleSettings.BillDiscountIncludeVat;
                    chkEnableVANSale.Checked = infoVansaleSettings.EnableVanSale;
                    chkShowPurchaseCost.Checked = infoVansaleSettings.ShowPurchaseCostInProduct;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected override void OnShown(EventArgs e)
        {
            
        }
        //--------------------------------------------------------------------------------------------------------------------
       
      
       
        //--------------------------------------------------------------------------------------------------------------------
        private void frmInventorySettings_KeyDown(object sender, KeyEventArgs e)
        
        
        {
          
        }
        //--------------------------------------------------------------------------------------------------------------------
      
      
        private void frmInventorySettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            //try
            //{
            //    DoWhenQuitingForm();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("AL27:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
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
                MessageBox.Show("CS05:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void chkShowTrnsctn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtRowHeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void chkShowSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtRowHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtQuotationValidity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtDeliveredWithIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtQuotationPaymntTerms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void bwrk1_DoWork(object sender, DoWorkEventArgs e)
        {
            FinacFormControl objGeneral = new FinacFormControl();
            objGeneral.formSettings(this);
        }

        private void txtExpiry_Enter(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtExpiry.Text) == 0)
                    txtExpiry.Text = "";
            }
            catch { txtExpiry.Text = ""; }
        }

        private void txtExpiry_Leave(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtExpiry.Text) == 0)
                    txtExpiry.Text = "0";
            }
            catch { txtExpiry.Text = "0"; }
        }

        private void txtExpiry_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {

                }
                else
                    if (e.KeyChar > 47 && e.KeyChar < 58 || e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("CS08:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbExpiry_CheckedChanged(object sender, EventArgs e)
        {
            if (cbExpiry.Checked == true)
                gpbLimit.Enabled = true;
            else
                gpbLimit.Enabled = false;
        }

        private void tpInventory_Click(object sender, EventArgs e)
        {

        }
        bool isExecute = true;
        private void cbMessageBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isExecute)
                {
                    if (cbMessageBox.Checked)
                    {
                        isExecute = false;
                        cbSave.Checked = true;
                        isExecute = false;
                        cbRemove.Checked = true;
                        isExecute = false;
                        cbDelete.Checked = true;
                        isExecute = false;
                        cbClose.Checked = true;
                        isExecute = false;
                        chkPrint.Checked = true;
                        isExecute = false;
                        chkClear.Checked = true;
                    }
                    else
                    {
                        isExecute = false;
                        cbSave.Checked = false;
                        isExecute = false;
                        cbRemove.Checked = false;
                        isExecute = false;
                        cbDelete.Checked = false;
                        isExecute = false;
                        cbClose.Checked = false;
                        isExecute = false;
                        chkPrint.Checked = false;
                        isExecute = false;
                        chkClear.Checked = false;
                    }
                }
                else
                {
                    isExecute = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CS15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbSave_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.CheckBox cb = (System.Windows.Forms.CheckBox)sender;
                // CheckBox cb = (CheckBox)cbClose;
                if (isExecute)
                {

                    if (cb.Checked)
                    {
                        if (cbSave.Checked && cbDelete.Checked && cbClose.Checked && cbRemove.Checked)
                        {
                            isExecute = false;
                            cbMessageBox.Checked = true;
                        }
                        else
                        {
                            isExecute = false;
                            cbMessageBox.Checked = false;
                        }
                    }
                    else
                    {
                        isExecute = false;
                        cbMessageBox.Checked = false;
                    }
                }
                isExecute = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("CS15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPrinter.Text == "Dot Matrix" || cmbPrinter.Text == "Thermal")
            {
                cbDirectPrinting.Checked = true;
            }
            if (cmbPrinter.Text == "Other" || cmbPrinter.Text == "Thermal")
            {
                cbSalesMan.Visible = true;
            }
            else
            {
                cbSalesMan.Visible = false;
            }
            cbShowCompanyDetForEstimatePrint.Visible = false;
            cbPaperOut.Visible = false;
            txtPaperOut.Visible = false;
            if (cmbPrinter.Text == "Dot Matrix")
            {
                if (cmbPrinter.Text == "Thermal")
                {
                    cbShowCompanyDetForEstimatePrint.Visible = true;
                    cbPaperOut.Visible = true;
                    txtPaperOut.Visible = true;
                }
            }

        }

        private void cbPaperOut_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPaperOut.Checked)
            {
                txtPaperOut.ReadOnly = false;
                txtPaperOut.Text = txtPaperOut.Text.Trim() == "" ? "0" : txtPaperOut.Text.Trim();
            }
            else
            {
                txtPaperOut.ReadOnly = true;
                txtPaperOut.Text = "0";
            }

        }

        private void btnBrowseFooter_Click(object sender, EventArgs e)
        {
            try
            {
                OFDImage.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG";
                OFDImage.FileName = "";
                if (DialogResult.OK == OFDImage.ShowDialog())
                {
                    if (OFDImage.FileName != "")
                    {
                        try
                        {
                            CompanyFooterImage = ReadFile(OFDImage.FileName);
                            MemoryStream ms = new MemoryStream(CompanyFooterImage);
                            Image newImage = Image.FromStream(ms);
                            pblCompanyFooter.Image = newImage;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BC9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBrowseHeader_Click(object sender, EventArgs e)
        {
            try
            {
                OFDImage.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG";
                OFDImage.FileName = "";
                if (DialogResult.OK == OFDImage.ShowDialog())
                {
                    if (OFDImage.FileName != "")
                    {
                        try
                        {
                            CompanyHeaderImage = ReadFile(OFDImage.FileName);
                            MemoryStream ms = new MemoryStream(CompanyHeaderImage);
                            Image newImage = Image.FromStream(ms);
                            pbCompanyHeader.Image = newImage;
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BC9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        byte[] ReadFile(string strImagePath)
        {
            //Read image from the specified location 
            // return byte form image

            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(strImagePath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(strImagePath, FileMode.Open,
                                                    FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to 

            //supply number of bytes to read from file.
            //In this case we want to read entire file. 

            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void btnCancelHeader_Click(object sender, EventArgs e)
        {
            try
            {
                GetDefaultImageHeader();
            }
            catch (Exception ex)
            {
                //MDIE_Mahallu.infoError.ErrorString = "A87:" + ex.Message;
            }
        }

        private void btnCancelFooter_Click(object sender, EventArgs e)
        {
            try
            {
                GetDefaultImageFooter();
            }
            catch (Exception ex)
            {
                //MDIE_Mahallu.infoError.ErrorString = "A87:" + ex.Message;
            }
        }
        public void GetDefaultImageHeader()
        {
            // To get deafult image
            // As we store default image in start up path,  we assign the file path as its path
            // string strImagePath = Application.StartupPath + "\\Logo.JPG";
            string strImagePath = System.Windows.Forms.Application.StartupPath + "\\Logo.JPG";
            CompanyHeaderImage = ReadFile(strImagePath);
            MemoryStream ms = new MemoryStream(CompanyHeaderImage);
            Image newImage = Image.FromStream(ms);
            pbCompanyHeader.Image = newImage;
        }
        public void GetDefaultImageFooter()
        {
            // To get deafult image
            // As we store default image in start up path,  we assign the file path as its path
            string strImagePath = System.Windows.Forms.Application.StartupPath + "\\Logo.JPG";
            CompanyFooterImage = ReadFile(strImagePath);
            MemoryStream ms = new MemoryStream(CompanyFooterImage);
            Image newImage = Image.FromStream(ms);
            pblCompanyFooter.Image = newImage;
        }
        private void bwrk1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (frmCompanyProgress != null && frmCompanyProgress.Visible)
                frmCompanyProgress.Close();
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnBrowse1_Click(object sender, EventArgs e)
        {
           
            SaveFileDialog saveBackupPath1 = new SaveFileDialog();

            saveBackupPath1.Filter = "Backup Files (*.bak)|*.bak";
            saveBackupPath1.FileName = "backup.bak";

            if (saveBackupPath1.ShowDialog() == DialogResult.OK)
            {
                // folder path only
                txtPath1.Text = Path.GetDirectoryName(saveBackupPath1.FileName);
            }
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
             
            SaveFileDialog saveBackupPath2 = new SaveFileDialog();
            saveBackupPath2.Filter = "Backup Files (*.bak)|*.bak";
            saveBackupPath2.FileName = "backup.bak";

            if (saveBackupPath2.ShowDialog() == DialogResult.OK)
            {
              
                txtPath2.Text = Path.GetDirectoryName(saveBackupPath2.FileName);
            }
        }

        private void cbSuffix_CheckedChanged(object sender, EventArgs e)
        {

        }
        bool isFromCurrentDate = false;

        public void CallThisFormFromCurrentDateForm()
        {
            // Funsciton to call after saving a finacial year which is called after company creation
            this.isFromCurrentDate = true;
            base.Show();
            base.Activate();
        }

    }
}