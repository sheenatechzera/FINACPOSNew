using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for SettingsSP    
//</summary>    
namespace FinacPOS
{
    class SettingsSP : DBConnection
    {
        public SettingsInfo SettingsViewByBranchId(string BranchId)
        {
            SettingsInfo settingsinfo = new SettingsInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SettingsViewByBranchId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = BranchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    settingsinfo.SettingsId = sdrreader["settingsId"].ToString();
                    settingsinfo.BranchId = sdrreader["branchId"].ToString();
                    settingsinfo.Budget = bool.Parse(sdrreader["budget"].ToString());
                    settingsinfo.PayRoll = bool.Parse(sdrreader["payRoll"].ToString());
                    settingsinfo.Tax = bool.Parse(sdrreader["tax"].ToString());
                    settingsinfo.MultiCurrency = bool.Parse(sdrreader["multiCurrency"].ToString());
                    settingsinfo.BillByBill = bool.Parse(sdrreader["billByBill"].ToString());
                    settingsinfo.InterestCalculation = bool.Parse(sdrreader["interestCalculation"].ToString());
                    settingsinfo.ShowDiscountPercentage = bool.Parse(sdrreader["discount"].ToString());
                    settingsinfo.FreeQuantity = bool.Parse(sdrreader["freeQuantity"].ToString());
                    settingsinfo.AutomaticProductCodeGeneration = bool.Parse(sdrreader["automaticProductCodeGeneration"].ToString());
                    settingsinfo.BarcodeGeneration = bool.Parse(sdrreader["barcodeGeneration"].ToString());
                    settingsinfo.MaintainBatch = bool.Parse(sdrreader["maintainBatch"].ToString());
                    settingsinfo.ExpiryReminder = sdrreader["expiryReminder"].ToString();
                    settingsinfo.MaintainGodown = bool.Parse(sdrreader["maintainGodown"].ToString());
                    settingsinfo.SuffixPrefix = bool.Parse(sdrreader["suffixPrefix"].ToString());
                    settingsinfo.LowStockReminderPopUp = bool.Parse(sdrreader["lowStockReminderPopUp"].ToString());
                    settingsinfo.Printer = sdrreader["printer"].ToString();
                    settingsinfo.StockCalculatingMethod = sdrreader["stockCalculatingMethod"].ToString();
                    settingsinfo.NegativeStockStatus = sdrreader["negativeStockStatus"].ToString();
                    settingsinfo.PurchaseReturnLimit = int.Parse(sdrreader["purchaseReturnLimit"].ToString());
                    settingsinfo.SalesReturnLimit = int.Parse(sdrreader["salesReturnLimit"].ToString());
                    settingsinfo.DirectPrinting = bool.Parse(sdrreader["directPrint"].ToString());
                    settingsinfo.ExtraDate = DateTime.Parse(sdrreader["extraDate"].ToString());
                    settingsinfo.Extra1 = sdrreader["extra1"].ToString();
                    settingsinfo.Extra2 = sdrreader["extra2"].ToString();
                    settingsinfo.CurrencySuffix = bool.Parse(sdrreader["currencySuffix"].ToString());
                    settingsinfo.NegativeCashTransaction = sdrreader["negativeCashTransaction"].ToString();
                    settingsinfo.AllowRack = bool.Parse(sdrreader["allowRack"].ToString());
                    settingsinfo.MessageBoxAddEdit = bool.Parse(sdrreader["messageBoxAddEdit"].ToString());
                    settingsinfo.MessageBoxDelete = bool.Parse(sdrreader["messageBoxDelete"].ToString());
                    settingsinfo.MessageBoxClose = bool.Parse(sdrreader["messageBoxClose"].ToString());
                    settingsinfo.MessageBoxRowRemove = bool.Parse(sdrreader["messageBoxRowRemove"].ToString());
                    settingsinfo.TaxType = sdrreader["taxType"].ToString();
                    settingsinfo.FormType = sdrreader["formType"].ToString();
                    settingsinfo.ShowPurchaseRate = bool.Parse(sdrreader["showPurchaseRate"].ToString());
                    settingsinfo.ShowMRP = bool.Parse(sdrreader["showMRP"].ToString());
                    settingsinfo.ShowUnit = bool.Parse(sdrreader["showUnit"].ToString());
                    settingsinfo.ShowDiscountAmount = bool.Parse(sdrreader["showDiscountAmount"].ToString());
                    settingsinfo.TickPrintAfterSave = bool.Parse(sdrreader["tickPrintAfterSave"].ToString());
                    settingsinfo.ShowProductCode = bool.Parse(sdrreader["showProductCode"].ToString());

                    settingsinfo.ShowCompanyForEstimatePrint = bool.Parse(sdrreader["showCompanyForEstimatePrint"].ToString());
                    settingsinfo.PaperOut = int.Parse(sdrreader["paperOut"].ToString());
                    settingsinfo.ShowBrand = bool.Parse(sdrreader["showBrand"].ToString());
                    settingsinfo.ShowSalesmanInPrint = bool.Parse(sdrreader["showSalesmanInPrint"].ToString());
                    settingsinfo.Vehicle = bool.Parse(sdrreader["vehicle"].ToString());   
                    settingsinfo.VatIncluded = bool.Parse(sdrreader["vatIncluded"].ToString()); 
                    settingsinfo.VatCessIncluded = bool.Parse(sdrreader["vatCessIncluded"].ToString());
                    settingsinfo.BillDiscountPer = bool.Parse(sdrreader["billDiscountPer"].ToString() == "" ? "false" : sdrreader["billDiscountPer"].ToString());
                    settingsinfo.Profit = bool.Parse(sdrreader["Profit"].ToString());
                    settingsinfo.ProfitPercentage = bool.Parse(sdrreader["ProfitPercentage"].ToString());
                    settingsinfo.TotalProfit = bool.Parse(sdrreader["TotalProfit"].ToString());
                    settingsinfo.ProductDescription = bool.Parse(sdrreader["productDescription"].ToString());
                    settingsinfo.RoundOff = bool.Parse(sdrreader["roundOff"].ToString());
                    settingsinfo.RoundDecimal = int.Parse(sdrreader["RoundDecimal"].ToString());
                    string dec = "0";
                    settingsinfo.RoundDecimalPart = "#" + dec + "." + dec.PadLeft(SettingsInfo._roundDecimal, '0');
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return settingsinfo;
        }
        
    }
}
