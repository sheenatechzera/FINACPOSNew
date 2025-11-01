using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class InventorySettingsSP:DBConnection
    {

        public void FinanceSettingsEdit(FinanceSettingsInfo settingsinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("FinanceSettingsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@FinanceSettingsId", SqlDbType.Int);
                sprmparam.Value = settingsinfo.FinanceSettingsId;
                sprmparam = sccmd.Parameters.Add("@ActivateTax", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ActivateTax;
                sprmparam = sccmd.Parameters.Add("@BillByBill", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.BillByBill;
                sprmparam = sccmd.Parameters.Add("@ActivateInterstCalc", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ActivateInterstCalc;
                sprmparam = sccmd.Parameters.Add("@UseMultiCurrency", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.UseMultiCurrency;
                sprmparam = sccmd.Parameters.Add("@SufPrefixVoucherNoGen", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.SufPrefixVoucherNoGen;
                sprmparam = sccmd.Parameters.Add("@ActivateCosteCentre", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ActivateCosteCentre;
                sprmparam = sccmd.Parameters.Add("@NegativeCashTransaction", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.NegativeCashTransaction;
                sprmparam = sccmd.Parameters.Add("@AccountsCalcMethod", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.AccountsCalcMethod;
                sprmparam = sccmd.Parameters.Add("@TaxType", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.TaxType;
                sprmparam = sccmd.Parameters.Add("@FormType", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.FormType;
                sprmparam = sccmd.Parameters.Add("@VatIncluded", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.VatIncluded;
                sprmparam = sccmd.Parameters.Add("@VatandCessIncluded", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.VatandCessIncluded;

                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.extra1;
                sprmparam = sccmd.Parameters.Add("@AccountsPosting", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.AccountsPosting;
                sprmparam = sccmd.Parameters.Add("@ZatcaType", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.ZatcaType;
                sprmparam = sccmd.Parameters.Add("@ShowCompanyHeader", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowCompanyHeader;
                sprmparam = sccmd.Parameters.Add("@ShowCompanyFooter", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowCompanyFooter;
                sprmparam = sccmd.Parameters.Add("@CompanyHeader", SqlDbType.Image);
                sprmparam.Value = settingsinfo.CompanyHeader;
                sprmparam = sccmd.Parameters.Add("@CompanyFooter", SqlDbType.Image);
                sprmparam.Value = settingsinfo.CompanyFooter;
                sprmparam = sccmd.Parameters.Add("@roundDecimal", SqlDbType.Int);
                sprmparam.Value = settingsinfo.RoundDecimal;
                sprmparam = sccmd.Parameters.Add("@EnablePOS", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.EnablePOS;
                sprmparam = sccmd.Parameters.Add("@Userdef1", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.Userdef1;
                sprmparam = sccmd.Parameters.Add("@Userdef2", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.Userdef2;
                sprmparam = sccmd.Parameters.Add("@Userdef3", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.Userdef3;
                sprmparam = sccmd.Parameters.Add("@Userdef4", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.Userdef4;
                sprmparam = sccmd.Parameters.Add("@DateFormat", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.DateFormat;
                sprmparam = sccmd.Parameters.Add("@PaymentMode", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.PaymentMode;
                sprmparam = sccmd.Parameters.Add("@ShowReminder", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowReminder;
                sprmparam = sccmd.Parameters.Add("@ValidateVatNumber", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ValidateVatNumber;

                int inA = int.Parse(sccmd.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public FinanceSettingsInfo FinanceSettingsViewAll(string branchid)
        {
            FinanceSettingsInfo infoFinanceSettings = new FinanceSettingsInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmda = new SqlCommand("FinanceSettingsViewAll", sqlcon);
                sccmda.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparama = new SqlParameter();
                sprmparama = sccmda.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparama.Value = branchid;

                SqlDataReader sdrreadera = sccmda.ExecuteReader();
                while (sdrreadera.Read())
                {
                    infoFinanceSettings.FinanceSettingsId = int.Parse(sdrreadera["FinanceSettingsId"].ToString());
                    infoFinanceSettings.ActivateTax = bool.Parse(sdrreadera["ActivateTax"].ToString());
                    infoFinanceSettings.BillByBill = bool.Parse(sdrreadera["BillByBill"].ToString());
                    infoFinanceSettings.ActivateInterstCalc = bool.Parse(sdrreadera["ActivateInterstCalc"].ToString());
                    infoFinanceSettings.UseMultiCurrency = bool.Parse(sdrreadera["UseMultiCurrency"].ToString());

                    infoFinanceSettings.SufPrefixVoucherNoGen = bool.Parse(sdrreadera["SufPrefixVoucherNoGen"].ToString());
                    infoFinanceSettings.ActivateCosteCentre = bool.Parse(sdrreadera["ActivateCosteCentre"].ToString());

                    infoFinanceSettings.NegativeCashTransaction = sdrreadera["NegativeCashTransaction"].ToString();
                    infoFinanceSettings.AccountsCalcMethod = sdrreadera["AccountsCalcMethod"].ToString();
                    infoFinanceSettings.TaxType = sdrreadera["TaxType"].ToString();
                    infoFinanceSettings.FormType = sdrreadera["FormType"].ToString();
                    infoFinanceSettings.VatIncluded = bool.Parse(sdrreadera["VatIncluded"].ToString());
                    infoFinanceSettings.VatandCessIncluded = bool.Parse(sdrreadera["VatandCessIncluded"].ToString());
                    infoFinanceSettings.BranchId = sdrreadera["branchId"].ToString();

                    infoFinanceSettings.extra1 = sdrreadera["extra1"].ToString();
                    infoFinanceSettings.AccountsPosting = bool.Parse(sdrreadera["AccountsPosting"].ToString());
                    infoFinanceSettings.ZatcaType = sdrreadera["ZatcaType"].ToString();

                    infoFinanceSettings.ShowCompanyHeader = bool.Parse(sdrreadera["ShowCompanyHeader"].ToString());
                    infoFinanceSettings.ShowCompanyFooter = bool.Parse(sdrreadera["ShowCompanyFooter"].ToString());
                    try
                    {
                        byte[] Header = (byte[])sdrreadera["CompanyHeader"];
                        infoFinanceSettings.CompanyHeader = Header;
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        byte[] Footer = (byte[])sdrreadera["CompanyFooter"];
                        infoFinanceSettings.CompanyFooter = Footer;
                    }
                    catch (Exception)
                    {
                    }
                    infoFinanceSettings.RoundDecimal = int.Parse(sdrreadera["RoundDecimal"].ToString());
                    string dec = "0";
                    infoFinanceSettings.RoundDecimalPart = "#" + dec + "." + dec.PadLeft(FinanceSettingsInfo._roundDecimal, '0');
                    infoFinanceSettings.EnablePOS = bool.Parse(sdrreadera["EnablePOS"].ToString());
                    infoFinanceSettings.Userdef1 = sdrreadera["Userdef1"].ToString();
                    infoFinanceSettings.Userdef2 = sdrreadera["Userdef2"].ToString();
                    infoFinanceSettings.Userdef3 = sdrreadera["Userdef3"].ToString();
                    infoFinanceSettings.Userdef4 = sdrreadera["Userdef4"].ToString();
                    infoFinanceSettings.DateFormat = sdrreadera["DateFormat"].ToString();
                    infoFinanceSettings.PaymentMode = sdrreadera["PaymentMode"].ToString();
                    infoFinanceSettings.ShowReminder = bool.Parse(sdrreadera["ShowReminder"].ToString());
                    infoFinanceSettings.ValidateVatNumber = bool.Parse(sdrreadera["ValidateVatNumber"].ToString());
                }
                sdrreadera.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlcon.Close();
            }
            return infoFinanceSettings;

        }
        public void InventorySettingsEdit(InventorySettingsInfo settingsinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("InventorySettingsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@InventorySettingsId", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.InventorySettingsId;
                sprmparam = sccmd.Parameters.Add("@ShowAllTransctn", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowAllTransctn;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@gridColumnHeight", SqlDbType.Int);
                sprmparam.Value = settingsinfo.GridColumnHeight;
                sprmparam = sccmd.Parameters.Add("@showSize", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowSize;
                sprmparam = sccmd.Parameters.Add("@SalePriceUpdateByCostPerc", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.SalePriceUpdateByCostPerc;
                sprmparam = sccmd.Parameters.Add("@expiryReminder", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.ExpiryReminder;
                sprmparam = sccmd.Parameters.Add("@lowStockReminderPopUp", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.LowStockReminderPopUp;
                sprmparam = sccmd.Parameters.Add("@stockCalculatingMethod", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.StockCalculatingMethod;
                sprmparam = sccmd.Parameters.Add("@negativeStockStatus", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.NegativeStockStatus;
                sprmparam = sccmd.Parameters.Add("@expiryStatus", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.ExpiryStatus;
                sprmparam = sccmd.Parameters.Add("@roundOff", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.RoundOff;
                sprmparam = sccmd.Parameters.Add("@maintainGodown", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.maintainGodown;
                sprmparam = sccmd.Parameters.Add("@maintainRack", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.maintainRack;
                sprmparam = sccmd.Parameters.Add("@messageBoxAddEdit", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.MessageBoxAddEdit;
                sprmparam = sccmd.Parameters.Add("@messageBoxDelete", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.MessageBoxDelete;
                sprmparam = sccmd.Parameters.Add("@messageBoxClose", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.MessageBoxClose;
                sprmparam = sccmd.Parameters.Add("@messageBoxRowRemove", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.MessageBoxRowRemove;
                sprmparam = sccmd.Parameters.Add("@messageBoxPrint", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.MessageBoxPrint;
                sprmparam = sccmd.Parameters.Add("@messageBoxClear", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.MessageBoxClear;
                sprmparam = sccmd.Parameters.Add("@ShowBalanceLabel", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowBalanceLabel;
                sprmparam = sccmd.Parameters.Add("@CurrencySuffix", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.CurrencySuffix;
                sprmparam = sccmd.Parameters.Add("@vehicle", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.Vehicle;
                sprmparam = sccmd.Parameters.Add("@showCompanyForEstimatePrint", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowCompanyForEstimatePrint;
                sprmparam = sccmd.Parameters.Add("@paperOut", SqlDbType.Int);
                sprmparam.Value = settingsinfo.PaperOut;
                sprmparam = sccmd.Parameters.Add("@showSalesmanInPrint", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowSalesmanInPrint;
                sprmparam = sccmd.Parameters.Add("@printer", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.Printer;
                sprmparam = sccmd.Parameters.Add("@showPartNoInLookup", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showPartNoInLookup;
                sprmparam = sccmd.Parameters.Add("@showAlternateCodeInLookup", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showAlternateCodeInLookup;
                sprmparam = sccmd.Parameters.Add("@showLocationInLookUp", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showLocationInLookUp;
                sprmparam = sccmd.Parameters.Add("@BackupPath1", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.BackupPath1;
                sprmparam = sccmd.Parameters.Add("@BackupPath2", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.BackupPath2;
                sprmparam = sccmd.Parameters.Add("@ShowGodownWiseStock", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowGodownWiseStock;
                int inA = int.Parse(sccmd.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public InventorySettingsInfo InventorySettingsViewAll()
        {
            InventorySettingsInfo settingsinfo = new InventorySettingsInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("InventorySettingsViewAll", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    settingsinfo.InventorySettingsId = sdrreader["InventorySettingsId"].ToString();
                    settingsinfo.ShowAllTransctn = bool.Parse(sdrreader["ShowAllTansctn"].ToString());
                    settingsinfo.GridColumnHeight = int.Parse(sdrreader["gridColumnHeight"].ToString());
                    settingsinfo.ShowSize = bool.Parse(sdrreader["showSize"].ToString());
                    settingsinfo.SalePriceUpdateByCostPerc = bool.Parse(sdrreader["SalePriceUpdateByCostPerc"].ToString());
                    settingsinfo.ExpiryReminder = sdrreader["expiryReminder"].ToString();
                    settingsinfo.LowStockReminderPopUp = bool.Parse(sdrreader["lowStockReminderPopUp"].ToString());
                    settingsinfo.StockCalculatingMethod = sdrreader["stockCalculatingMethod"].ToString();
                    settingsinfo.NegativeStockStatus = sdrreader["negativeStockStatus"].ToString();
                    settingsinfo.ExpiryStatus = sdrreader["expiryStatus"].ToString();
                    settingsinfo.RoundOff = bool.Parse(sdrreader["RoundOff"].ToString());
                    settingsinfo.maintainGodown = bool.Parse(sdrreader["maintainGodown"].ToString());
                    settingsinfo.maintainRack = bool.Parse(sdrreader["maintainRack"].ToString());
                    settingsinfo.MessageBoxAddEdit = bool.Parse(sdrreader["messageBoxAddEdit"].ToString());
                    settingsinfo.MessageBoxDelete = bool.Parse(sdrreader["messageBoxDelete"].ToString());
                    settingsinfo.MessageBoxClose = bool.Parse(sdrreader["messageBoxClose"].ToString());
                    settingsinfo.MessageBoxRowRemove = bool.Parse(sdrreader["messageBoxRowRemove"].ToString());
                    settingsinfo.MessageBoxPrint = bool.Parse(sdrreader["messageBoxPrint"].ToString());
                    settingsinfo.MessageBoxClear = bool.Parse(sdrreader["messageBoxClear"].ToString());
                    settingsinfo.ShowBalanceLabel = bool.Parse(sdrreader["ShowBalanceLabel"].ToString());
                    settingsinfo.CurrencySuffix = bool.Parse(sdrreader["CurrencySuffix"].ToString());
                    settingsinfo.Vehicle = bool.Parse(sdrreader["vehicle"].ToString());
                    settingsinfo.ShowCompanyForEstimatePrint = bool.Parse(sdrreader["showCompanyForEstimatePrint"].ToString());
                    settingsinfo.PaperOut = int.Parse(sdrreader["paperOut"].ToString());
                    settingsinfo.ShowSalesmanInPrint = bool.Parse(sdrreader["showSalesmanInPrint"].ToString());
                    settingsinfo.Printer = sdrreader["printer"].ToString();
                    settingsinfo.showPartNoInLookup = bool.Parse(sdrreader["showPartNoInLookup"].ToString());
                    settingsinfo.showAlternateCodeInLookup = bool.Parse(sdrreader["showAlternateCodeInLookup"].ToString());
                    settingsinfo.showAlternateCodeInLookup = bool.Parse(sdrreader["showLocationInLookUp"].ToString());
                    settingsinfo.BackupPath1 = sdrreader["BackupPath1"].ToString();
                    settingsinfo.BackupPath2 = sdrreader["BackupPath2"].ToString();
                    settingsinfo.ShowGodownWiseStock = bool.Parse(sdrreader["ShowGodownWiseStock"].ToString());
                }
                sdrreader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlcon.Close();
            }
            return settingsinfo;

        }

        public PurchaseSettingsInfo PurchaseSettingsViewAll()
        {
            PurchaseSettingsInfo infoPurchaseSettings = new PurchaseSettingsInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmda = new SqlCommand("PurchaseSettingsViewAll", sqlcon);
                sccmda.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparama = new SqlParameter();
                sprmparama = sccmda.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparama.Value = PublicVariables._branchId;
                SqlDataReader sdrreadera = sccmda.ExecuteReader();
                while (sdrreadera.Read())
                {
                    infoPurchaseSettings.PurchaseSettingsId = sdrreadera["PurchaseSettingsId"].ToString();
                    infoPurchaseSettings.showSize = bool.Parse(sdrreadera["showSize"].ToString());
                    infoPurchaseSettings.showProductCode = bool.Parse(sdrreadera["showProductCode"].ToString());
                    infoPurchaseSettings.showProductDescription = bool.Parse(sdrreadera["showProductDescription"].ToString());
                    infoPurchaseSettings.ActivateRetention = bool.Parse(sdrreadera["ActivateRetention"].ToString());

                    infoPurchaseSettings.GridRowHeight = int.Parse(sdrreadera["GridRowHeight"].ToString());
                    infoPurchaseSettings.ShowFreeQtyColumns = bool.Parse(sdrreadera["ShowFreeQtyColumns"].ToString());

                    infoPurchaseSettings.ProductNameColumnWidth = int.Parse(sdrreadera["ProductNameColumnWidth"].ToString());
                    infoPurchaseSettings.ProductDiscriptionColumnWidth = int.Parse(sdrreadera["ProductDiscriptionColumnWidth"].ToString());
                    infoPurchaseSettings.VendorInvoiceChecking = bool.Parse(sdrreadera["VendorInvoiceChecking"].ToString());
                    infoPurchaseSettings.TickPrintAfterSave = bool.Parse(sdrreadera["tickPrintAfterSave"].ToString());
                    infoPurchaseSettings.DirectPrint = bool.Parse(sdrreadera["directPrint"].ToString());
                    infoPurchaseSettings.ChangeSalesPricInPurcchase = bool.Parse(sdrreadera["ChangeSalesPricInPurcchase"].ToString());

                    infoPurchaseSettings.ShowLookupForBarcode = bool.Parse(sdrreadera["showLookupForBarcode"].ToString());
                    // infoPurchaseSettings.ShowAlternateCodeInLookup = bool.Parse(sdrreadera["showAlternateCodeInLookup"].ToString());
                    infoPurchaseSettings.ShowPartNo = bool.Parse(sdrreadera["ShowPartNo"].ToString());
                }
                sdrreadera.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlcon.Close();
            }
            return infoPurchaseSettings;

        }

        //Add YBN ON 04-03-2024
        public void PurchaseSettingsEdit(PurchaseSettingsInfo settingsinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("PurchaseSettingsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@PurchaseSettingsId", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.PurchaseSettingsId;
                sprmparam = sccmd.Parameters.Add("@showSize", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showSize;
                sprmparam = sccmd.Parameters.Add("@showProductCode", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showProductCode;
                sprmparam = sccmd.Parameters.Add("@showProductDescription", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showProductDescription;
                sprmparam = sccmd.Parameters.Add("@ActivateRetention", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ActivateRetention;
                sprmparam = sccmd.Parameters.Add("@GridRowHeight", SqlDbType.Int);
                sprmparam.Value = settingsinfo.GridRowHeight;
                sprmparam = sccmd.Parameters.Add("@ShowFreeQtyColumns", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowFreeQtyColumns;
                sprmparam = sccmd.Parameters.Add("@ProductNameColumnWidth", SqlDbType.Int);
                sprmparam.Value = settingsinfo.ProductNameColumnWidth;
                sprmparam = sccmd.Parameters.Add("@ProductDiscriptionColumnWidth", SqlDbType.Int);
                sprmparam.Value = settingsinfo.ProductDiscriptionColumnWidth;
                sprmparam = sccmd.Parameters.Add("@VendorInvoiceChecking", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.VendorInvoiceChecking;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@tickPrintAfterSave", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.TickPrintAfterSave;
                sprmparam = sccmd.Parameters.Add("@directPrint", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.DirectPrint;
                sprmparam = sccmd.Parameters.Add("@ChangeSalesPricInPurcchase", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ChangeSalesPricInPurcchase;
                sprmparam = sccmd.Parameters.Add("@showLookupForBarcode", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowLookupForBarcode;
                sprmparam = sccmd.Parameters.Add("@showAlternateCodeInLookup", SqlDbType.Bit);
                sprmparam.Value = false;
                sprmparam = sccmd.Parameters.Add("@ShowPartNo", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowPartNo;
                int inA = int.Parse(sccmd.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public void VanSaleSettingsEdit(VanSaleSettingsInfo settingsinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("VanSaleSettingsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@VanSaleSettingsId", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.VanSaleSettingsId;
                sprmparam = sccmd.Parameters.Add("@DirectSalesPosting", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.DirectSalesPosting;
                sprmparam = sccmd.Parameters.Add("@SalesPostingDuration", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.SalesPostingDuration;
                sprmparam = sccmd.Parameters.Add("@BillDiscountIncludeVat", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.BillDiscountIncludeVat;

                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.extra2;
                sprmparam = sccmd.Parameters.Add("@EnableVanSale", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.EnableVanSale;
                sprmparam = sccmd.Parameters.Add("@ShowPurchaseCostInProduct", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowPurchaseCostInProduct;

                int inA = int.Parse(sccmd.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public VanSaleSettingsInfo VanSaleSettingsViewAll(string branchid)
        {
            VanSaleSettingsInfo infoVansaleSettings = new VanSaleSettingsInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmda = new SqlCommand("VanSaleSettingsViewAll", sqlcon);
                sccmda.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparama = new SqlParameter();
                sprmparama = sccmda.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparama.Value = branchid;

                SqlDataReader sdrreadera = sccmda.ExecuteReader();
                while (sdrreadera.Read())
                {
                    infoVansaleSettings.VanSaleSettingsId = sdrreadera["VanSaleSettingsId"].ToString();
                    infoVansaleSettings.DirectSalesPosting = bool.Parse(sdrreadera["DirectSalesPosting"].ToString());
                    infoVansaleSettings.SalesPostingDuration = sdrreadera["SalesPostingDuration"].ToString();
                    infoVansaleSettings.BillDiscountIncludeVat = bool.Parse(sdrreadera["BillDiscountIncludeVat"].ToString());
                    infoVansaleSettings.BranchId = sdrreadera["branchId"].ToString();
                    infoVansaleSettings.extra1 = sdrreadera["extra1"].ToString();
                    infoVansaleSettings.extra2 = sdrreadera["extra2"].ToString();
                    infoVansaleSettings.EnableVanSale = bool.Parse(sdrreadera["EnableVanSale"].ToString());
                    infoVansaleSettings.ShowPurchaseCostInProduct = bool.Parse(sdrreadera["ShowPurchaseCostInProduct"].ToString());

                }
                sdrreadera.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlcon.Close();
            }
            return infoVansaleSettings;

        }
        public void SalesSettingsEdit(SalesSettingsInfo settingsinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SalesSettingsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@SalesSettingsId", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.SaleSettingsId;
                sprmparam = sccmd.Parameters.Add("@showSize", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showSize;
                sprmparam = sccmd.Parameters.Add("@showProductCode", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showProductCode;
                sprmparam = sccmd.Parameters.Add("@showProductDescription", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showProductDescription;
                sprmparam = sccmd.Parameters.Add("@showPaymntInSalesOrder", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.showPaymntInSalesOrder;
                sprmparam = sccmd.Parameters.Add("@QuotationValidity", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.QuotationValidity;
                sprmparam = sccmd.Parameters.Add("@QuotationDeliveredWithIn", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.QuotationDeliveredWithIn;
                sprmparam = sccmd.Parameters.Add("@QuotationPaymntTerms", SqlDbType.NVarChar);
                sprmparam.Value = settingsinfo.QuotationPaymntTerms;
                sprmparam = sccmd.Parameters.Add("@GridRowHeight", SqlDbType.Int);
                sprmparam.Value = settingsinfo.GridRowHeight;
                sprmparam = sccmd.Parameters.Add("@ShowFreeQtyColumns", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowFreeQtyColumns;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;

                //sprmparam = sccmd.Parameters.Add("@gridColumnHeight", SqlDbType.Int);
                //sprmparam.Value = settingsinfo.GridColumnHeight;
                //sprmparam = sccmd.Parameters.Add("@showSize", SqlDbType.Bit);
                //sprmparam.Value = settingsinfo.ShowSize;

                sprmparam = sccmd.Parameters.Add("@ActivateRetention", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ActivateRetention;

                //Add YBN ON 04-03-2024
                sprmparam = sccmd.Parameters.Add("@ProductNameColumnWidth", SqlDbType.Int);
                sprmparam.Value = settingsinfo.ProductNameColumnWidth;
                sprmparam = sccmd.Parameters.Add("@ProductDiscriptionColumnWidth", SqlDbType.Int);
                sprmparam.Value = settingsinfo.ProductDiscriptionColumnWidth;
                sprmparam = sccmd.Parameters.Add("@SalesOrderTaxType", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.SalesOrderTaxType;

                sprmparam = sccmd.Parameters.Add("@tickPrintAfterSave", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.TickPrintAfterSave;
                sprmparam = sccmd.Parameters.Add("@directPrint", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.DirectPrint;
                sprmparam = sccmd.Parameters.Add("@defaultBank", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.DefaultBank;
                sprmparam = sccmd.Parameters.Add("@showPaymentInReport", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowPaymentInReport;
                sprmparam = sccmd.Parameters.Add("@ApplyDiscountLimit", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ApplyDiscountLimit;
                sprmparam = sccmd.Parameters.Add("@ShowBillProfit", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowBillProfit;
                sprmparam = sccmd.Parameters.Add("@SalesInvoiceType", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.SalesInvoiceType;
                sprmparam = sccmd.Parameters.Add("@ShowVehicleDetails", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowVehicleDetails;
                sprmparam = sccmd.Parameters.Add("@ShowLineDiscount", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowLineDiscount;
                sprmparam = sccmd.Parameters.Add("@AutomaticRoundOff", SqlDbType.VarChar);
                sprmparam.Value = settingsinfo.AutomaticRoundOff;
                sprmparam = sccmd.Parameters.Add("@AdvancePerc", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.AdvancePerc;
                sprmparam = sccmd.Parameters.Add("@billDiscountPer", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.BillDiscountPer;
                sprmparam = sccmd.Parameters.Add("@showPurchaseRate", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowPurchaseRate;
                sprmparam = sccmd.Parameters.Add("@ShowStockinLookup", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowStockinLookup;
                sprmparam = sccmd.Parameters.Add("@ActivatIntermediateSearch", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ActivatIntermediateSearch;
                sprmparam = sccmd.Parameters.Add("@ShowPartNo", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowPartNo;
                sprmparam = sccmd.Parameters.Add("@showLookupForBarcode", SqlDbType.Bit);
                sprmparam.Value = settingsinfo.ShowLookupForBarcode;
                sprmparam = sccmd.Parameters.Add("@showAlternateCodeInLookup", SqlDbType.Bit);
                sprmparam.Value = false;


                int inA = int.Parse(sccmd.ExecuteNonQuery().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public SalesSettingsInfo SalesSettingsViewAll()
        {
            SalesSettingsInfo settingsinfo = new SalesSettingsInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SalesSettingsViewAll", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    settingsinfo.SaleSettingsId = sdrreader["SalesSettingsId"].ToString();
                    settingsinfo.showSize = bool.Parse(sdrreader["showSize"].ToString());
                    settingsinfo.showProductCode = bool.Parse(sdrreader["showProductCode"].ToString());
                    settingsinfo.showProductDescription = bool.Parse(sdrreader["showProductDescription"].ToString());
                    settingsinfo.showPaymntInSalesOrder = bool.Parse(sdrreader["showPaymntInSalesOrder"].ToString());
                    settingsinfo.QuotationValidity = sdrreader["QuotationValidity"].ToString();
                    settingsinfo.QuotationDeliveredWithIn = sdrreader["QuotationDeliveredWithIn"].ToString();
                    settingsinfo.QuotationPaymntTerms = sdrreader["QuotationPaymntTerms"].ToString();
                    settingsinfo.GridRowHeight = int.Parse(sdrreader["GridRowHeight"].ToString());
                    settingsinfo.ShowFreeQtyColumns = bool.Parse(sdrreader["ShowFreeQtyColumns"].ToString());
                    settingsinfo.ActivateRetention = bool.Parse(sdrreader["ActivateRetention"].ToString());

                    //Add YBN ON 04-03-2024
                    settingsinfo.ProductNameColumnWidth = int.Parse(sdrreader["ProductNameColumnWidth"].ToString());
                    settingsinfo.ProductDiscriptionColumnWidth = int.Parse(sdrreader["ProductDiscriptionColumnWidth"].ToString());
                    settingsinfo.SalesOrderTaxType = sdrreader["SalesOrderTaxType"].ToString();
                    settingsinfo.TickPrintAfterSave = bool.Parse(sdrreader["tickPrintAfterSave"].ToString());
                    settingsinfo.DirectPrint = bool.Parse(sdrreader["directPrint"].ToString());
                    settingsinfo.DefaultBank = sdrreader["defaultBank"].ToString();
                    settingsinfo.ShowPaymentInReport = bool.Parse(sdrreader["showPaymentInReport"].ToString());
                    settingsinfo.ApplyDiscountLimit = bool.Parse(sdrreader["ApplyDiscountLimit"].ToString());
                    settingsinfo.ShowBillProfit = bool.Parse(sdrreader["ShowBillProfit"].ToString());
                    settingsinfo.SalesInvoiceType = sdrreader["SalesInvoiceType"].ToString();
                    settingsinfo.ShowVehicleDetails = bool.Parse(sdrreader["ShowVehicleDetails"].ToString());
                    settingsinfo.ShowLineDiscount = bool.Parse(sdrreader["ShowLineDiscount"].ToString());
                    settingsinfo.AutomaticRoundOff = sdrreader["AutomaticRoundOff"].ToString();
                    settingsinfo.AdvancePerc = bool.Parse(sdrreader["AdvancePerc"].ToString());
                    settingsinfo.BillDiscountPer = bool.Parse(sdrreader["BillDiscountPer"].ToString());
                    settingsinfo.ShowPurchaseRate = bool.Parse(sdrreader["ShowPurchaseRate"].ToString());
                    settingsinfo.ShowStockinLookup = bool.Parse(sdrreader["ShowStockinLookup"].ToString());
                    settingsinfo.ActivatIntermediateSearch = bool.Parse(sdrreader["ActivatIntermediateSearch"].ToString());
                    settingsinfo.ShowPartNo = bool.Parse(sdrreader["ShowPartNo"].ToString());
                    settingsinfo.ShowLookupForBarcode = bool.Parse(sdrreader["showLookupForBarcode"].ToString());
                    //  settingsinfo.ShowAlternateCodeInLookup = bool.Parse(sdrreader["showAlternateCodeInLookup"].ToString());
                }
                sdrreader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                sqlcon.Close();
            }
            return settingsinfo;

        }
        public DataTable AccountLedgerGetBankForSettings(string branchId)
        {
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetBankForSettings", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                sdaadapter.Fill(dtblAccountLedger);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblAccountLedger;
        }
        public DataTable InventorySettingsGetBankDetails(string branchId)
        {
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("InventorySettingsGetBankDetails", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                sdaadapter.Fill(dtblAccountLedger);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblAccountLedger;
        }
    }
}
