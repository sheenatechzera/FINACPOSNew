using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinacPOS
{
    class POSSettingsSP : DBConnection
    {
        public string POSSettingsEdit(POSSettingsInfo counterinfo)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSSettingsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSSettingsId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.POSSettingsId;
                sprmparam = sccmd.Parameters.Add("@BillClearAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.BillClearAuth;
                sprmparam = sccmd.Parameters.Add("@DiscountAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.DiscountAuth;
                sprmparam = sccmd.Parameters.Add("@HoldBillAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.HoldBillAuth;
                sprmparam = sccmd.Parameters.Add("@PriceChangeAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.PriceChangeAuth;
                sprmparam = sccmd.Parameters.Add("@CreditSalesAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CreditSalesAuth;
                sprmparam = sccmd.Parameters.Add("@ZeroStockAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ZeroStockAuth;
                sprmparam = sccmd.Parameters.Add("@ItemGroupinginPrint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ItemGroupinginPrint;
                sprmparam = sccmd.Parameters.Add("@lastBillPrintAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.lastBillPrintAuth;
                sprmparam = sccmd.Parameters.Add("@ExchangeItemAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ExchangeItemAuth;
                sprmparam = sccmd.Parameters.Add("@CashBoxOpenAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CashBoxOpenAuth;
                sprmparam = sccmd.Parameters.Add("@QtyChangeAuth", SqlDbType.Bit);
                sprmparam.Value = counterinfo.QtyChangeAuth;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = string.Empty;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = string.Empty;
                sprmparam = sccmd.Parameters.Add("@CNDays", SqlDbType.Int);
                sprmparam.Value = counterinfo.CNDays;
                sprmparam = sccmd.Parameters.Add("@POSCompanyName", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.POSCompanyName;
                sprmparam = sccmd.Parameters.Add("@POSCompanyNameArabic", SqlDbType.NVarChar);
                sprmparam.Value = counterinfo.POSCompanyNameArabic;
                sprmparam = sccmd.Parameters.Add("@POSAddress", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.POSAddress;
                sprmparam = sccmd.Parameters.Add("@POSPhone", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.POSPhone;
                sprmparam = sccmd.Parameters.Add("@CompanyH", SqlDbType.Int);
                sprmparam.Value = counterinfo.CompanyH;
                sprmparam = sccmd.Parameters.Add("@CompanyArabicH", SqlDbType.Int);
                sprmparam.Value = counterinfo.CompanyArabicH;
                sprmparam = sccmd.Parameters.Add("@AddressH", SqlDbType.Int);
                sprmparam.Value = counterinfo.AddressH;
                sprmparam = sccmd.Parameters.Add("@PhoneH", SqlDbType.Int);
                sprmparam.Value = counterinfo.PhoneH;
                sprmparam = sccmd.Parameters.Add("@CompanyVisible", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CompanyVisible;
                sprmparam = sccmd.Parameters.Add("@CompanyArabicVisible", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CompanyArabicVisible;
                sprmparam = sccmd.Parameters.Add("@AddressVisible", SqlDbType.Bit);
                sprmparam.Value = counterinfo.AddressVisible;
                sprmparam = sccmd.Parameters.Add("@PhoneVisible", SqlDbType.Bit);
                sprmparam.Value = counterinfo.PhoneVisible;
                sprmparam = sccmd.Parameters.Add("@ShowCustBalInPrint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowCustBalinPrint;
                sprmparam = sccmd.Parameters.Add("@CustBillCopy", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.CustBillCopy;
                sprmparam = sccmd.Parameters.Add("@AddQtyInSameBarcodeToGrid", SqlDbType.Bit);
                sprmparam.Value = counterinfo.AddQtyInSameBarcodeToGrid;
                sprmparam = sccmd.Parameters.Add("@ShowProductSummaryInSessionClose", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowProductSummaryInSessionClose;
                sprmparam = sccmd.Parameters.Add("@ActiveTableManage", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ActiveTableManage;
                sprmparam = sccmd.Parameters.Add("@StockView", SqlDbType.Bit);
                sprmparam.Value = counterinfo.StockView;

                id = sccmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return id;
        }
        public POSSettingsInfo POSSettingsViewByBranchId(string BranchId)
        {
            POSSettingsInfo settingsinfo = new POSSettingsInfo();

            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("POSSettingsViewByBranchId", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = BranchId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                settingsinfo.POSSettingsId = sdrreader["POSSettingsId"].ToString();
                settingsinfo.BillClearAuth = bool.Parse(sdrreader["BillClearAuth"].ToString());
                settingsinfo.DiscountAuth = bool.Parse(sdrreader["DiscountAuth"].ToString());
                settingsinfo.HoldBillAuth = bool.Parse(sdrreader["HoldBillAuth"].ToString());
                settingsinfo.PriceChangeAuth = bool.Parse(sdrreader["PriceChangeAuth"].ToString());
                settingsinfo.CreditSalesAuth = bool.Parse(sdrreader["CreditSalesAuth"].ToString());
                settingsinfo.ZeroStockAuth = bool.Parse(sdrreader["ZeroStockAuth"].ToString());
                settingsinfo.ItemGroupinginPrint = bool.Parse(sdrreader["ItemGroupinginPrint"].ToString());
                settingsinfo.lastBillPrintAuth = bool.Parse(sdrreader["lastBillPrintAuth"].ToString());
                settingsinfo.ExchangeItemAuth = bool.Parse(sdrreader["ExchangeItemAuth"].ToString());
                settingsinfo.CashBoxOpenAuth = bool.Parse(sdrreader["CashBoxOpenAuth"].ToString());
                settingsinfo.QtyChangeAuth = bool.Parse(sdrreader["QtyChangeAuth"].ToString());
                settingsinfo.CNDays = int.Parse(sdrreader["CNDays"].ToString());

                settingsinfo.POSCompanyName = sdrreader["POSCompanyName"].ToString();
                settingsinfo.POSCompanyNameArabic = sdrreader["POSCompanyNameArabic"].ToString();
                settingsinfo.POSAddress = sdrreader["POSAddress"].ToString();
                settingsinfo.POSPhone = sdrreader["POSPhone"].ToString();
                settingsinfo.CompanyH = int.Parse(sdrreader["CompanyH"].ToString());
                settingsinfo.CompanyArabicH = int.Parse(sdrreader["CompanyArabicH"].ToString());
                settingsinfo.AddressH = int.Parse(sdrreader["AddressH"].ToString());
                settingsinfo.PhoneH = int.Parse(sdrreader["PhoneH"].ToString());
                settingsinfo.CompanyVisible = bool.Parse(sdrreader["CompanyVisible"].ToString());
                settingsinfo.CompanyArabicVisible = bool.Parse(sdrreader["CompanyArabicVisible"].ToString());
                settingsinfo.AddressVisible = bool.Parse(sdrreader["AddressVisible"].ToString());
                settingsinfo.PhoneVisible = bool.Parse(sdrreader["PhoneVisible"].ToString());
                settingsinfo.ShowCustBalinPrint = bool.Parse(sdrreader["ShowCustBalinPrint"].ToString());
                settingsinfo.CustBillCopy = sdrreader["CustBillCopy"].ToString();
                settingsinfo.AddQtyInSameBarcodeToGrid = bool.Parse(sdrreader["AddQtyInSameBarcodeToGrid"].ToString());
                settingsinfo.ActiveTableManage = bool.Parse(sdrreader["ActiveTableManage"].ToString());
                settingsinfo.StockView = bool.Parse(sdrreader["StockView"].ToString());
            }
            sdrreader.Close();

            return settingsinfo;
        }
    }
}
