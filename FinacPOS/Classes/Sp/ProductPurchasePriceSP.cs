using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class ProductPurchasePriceSP : DBConnection    
    {
        public void ProductPurchasePriceAdd(ProductPurchasePriceInfo productPurchasePriceInfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductPurchasePriceAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = productPurchasePriceInfo.LedgerId;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productPurchasePriceInfo.ProductCode;
                sprmparam = sccmd.Parameters.Add("@UnitId", SqlDbType.VarChar);
                sprmparam.Value = productPurchasePriceInfo.UnitId;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = productPurchasePriceInfo.Amount;
                sprmparam = sccmd.Parameters.Add("@DiscPercentage", SqlDbType.Decimal);
                sprmparam.Value = productPurchasePriceInfo.DiscPercentage;
                sprmparam = sccmd.Parameters.Add("@DiscAmount", SqlDbType.Decimal);
                sprmparam.Value = productPurchasePriceInfo.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@PurchasePrice", SqlDbType.Decimal);
                sprmparam.Value = productPurchasePriceInfo.PurchasePrice;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._currentUserId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = productPurchasePriceInfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = productPurchasePriceInfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@purchasedate", SqlDbType.Date);
                sprmparam.Value = productPurchasePriceInfo.PurchaseDate;
                sprmparam = sccmd.Parameters.Add("@purchasemasterid", SqlDbType.VarChar);
                sprmparam.Value = productPurchasePriceInfo.PurchaseMasterId;

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

        public DataTable ProductPurchasePriceView(string productCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductPurchasePriceView", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = productCode;
                sdaadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public void ProductPurchasePriceDelete(string proPurchaseIds)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductPurchasePriceDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@proPurchaseIds", SqlDbType.VarChar);
                sprmparam.Value = proPurchaseIds;

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
