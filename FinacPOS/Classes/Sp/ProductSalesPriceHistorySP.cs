using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class ProductSalesPriceHistorySP : DBConnection    
    {
        public void ProductSalesPriceHistoryAdd(ProductSalesPriceInfo productSalesPriceinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductSalesPriceHistorySave", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@salespriceId", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.SalespriceId;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.ProductCode;
                sprmparam = sccmd.Parameters.Add("@UnitId", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.UnitId;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.Amount;
                sprmparam = sccmd.Parameters.Add("@DiscPercentage", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.DiscPercentage;
                sprmparam = sccmd.Parameters.Add("@DiscAmount", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@SalesPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.SalesPrice;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.UserId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@pricingLevelId", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.PricingLevelId;
                sprmparam = sccmd.Parameters.Add("@oldSalesPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.oldSalesPrice;
                sprmparam = sccmd.Parameters.Add("@OperationType", SqlDbType.Char);
                sprmparam.Value = productSalesPriceinfo.OperationType;
                sprmparam = sccmd.Parameters.Add("@oldSalesUnit", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.oldSalesUnit;
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.CostPrice;
                sprmparam = sccmd.Parameters.Add("@marginPercentage", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.MarginPercentage;

                sccmd.ExecuteNonQuery();

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
    }
}
