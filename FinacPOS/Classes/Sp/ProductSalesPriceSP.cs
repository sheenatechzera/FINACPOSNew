using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
     class ProductSalesPriceSP : DBConnection    
    {
         public void ProductSalesPriceAdd(ProductSalesPriceInfo productSalesPriceinfo)
         {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductSalesPriceAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
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
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.CostPrice;
                sprmparam = sccmd.Parameters.Add("@marginPercentage", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.MarginPercentage;
                sprmparam = sccmd.Parameters.Add("@LowestSellingPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.LowestSellingPrice;

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
         public void ProductSalesPriceEdit(ProductSalesPriceInfo productSalesPriceinfo)
         {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductSalesPriceEdit", sqlcon);
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
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.CostPrice;
                sprmparam = sccmd.Parameters.Add("@marginPercentage", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.MarginPercentage;
                sprmparam = sccmd.Parameters.Add("@LowestSellingPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.LowestSellingPrice;

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
         public DataTable ProductSalesPriceViewAll()
         {
             DataTable dtbl = new DataTable();
             try
             {
                 if (sqlcon.State == ConnectionState.Closed)
                 {
                     sqlcon.Open();
                 }
                 SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductSalesPriceViewAll", sqlcon);
                 sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
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
        public ProductSalesPriceInfo ProductSalesPriceView(string salespriceid)
        {
            ProductSalesPriceInfo productSalesPriceinfo = new ProductSalesPriceInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductSalesPriceView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@salespriceId", SqlDbType.VarChar);
                sprmparam.Value = salespriceid;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    productSalesPriceinfo.SalespriceId = sdrreader["salespriceId"].ToString();
                    productSalesPriceinfo.ProductCode = sdrreader["productCode"].ToString();
                    productSalesPriceinfo.UnitId = sdrreader["unitId"].ToString();
                    productSalesPriceinfo.Amount = decimal.Parse(sdrreader["amount"].ToString());
                    productSalesPriceinfo.DiscPercentage = decimal.Parse(sdrreader["discPercentage"].ToString());
                    productSalesPriceinfo.DiscAmount = decimal.Parse(sdrreader["discAmount"].ToString());
                    productSalesPriceinfo.SalesPrice = decimal.Parse(sdrreader["salesPrice"].ToString());
                    productSalesPriceinfo.UserId = sdrreader["userId"].ToString();
                    productSalesPriceinfo.BranchId = sdrreader["branchId"].ToString();
                    productSalesPriceinfo.ExtraDate = DateTime.Parse(sdrreader["extraDate"].ToString());
                    productSalesPriceinfo.Extra1 = sdrreader["extra1"].ToString();
                    productSalesPriceinfo.Extra2 = sdrreader["extra2"].ToString();
                    productSalesPriceinfo.LowestSellingPrice = decimal.Parse(sdrreader["LowestSellingPrice"].ToString());
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return productSalesPriceinfo;
        }
         public void ProductSalesPriceDelete(string salesPriceId)
         {
             try
             {
                 if (sqlcon.State == ConnectionState.Closed)
                 {
                     sqlcon.Open();
                 }
                 SqlCommand sccmd = new SqlCommand("ProductSalesPriceDelete", sqlcon);
                 sccmd.CommandType = CommandType.StoredProcedure;
                 SqlParameter sprmparam = new SqlParameter();
                 sprmparam = sccmd.Parameters.Add("@salespriceId", SqlDbType.VarChar);
                 sprmparam.Value = salesPriceId;
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
         public void ProductSalesPriceDeleteByproductCode(string strProductCode)
         {
             try
             {
                 if (sqlcon.State == ConnectionState.Closed)
                 {
                     sqlcon.Open();
                 }
                 SqlCommand sccmd = new SqlCommand("ProductSalesPriceDeleteByproductCode", sqlcon);
                 sccmd.CommandType = CommandType.StoredProcedure;
                 SqlParameter sprmparam = new SqlParameter();
                 sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                 sprmparam.Value = strProductCode;
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
         public DataTable ProductSalesPriceByProductCode(string strProductCode)
         {
             DataTable dtbl = new DataTable();
             try
             {
                 if (sqlcon.State == ConnectionState.Closed)
                 {
                     sqlcon.Open();
                 }
                 SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductSalesPriceByProductCode", sqlcon);
                 sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                 SqlParameter sprmparam = new SqlParameter();
                 sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                 sprmparam.Value = strProductCode;
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
         public DataTable ProductSalesPriceByProductCodeUnitAndPricingLevel(string strProductCode,string strunitid,string strPricingLevel)
         {
             DataTable dtbl = new DataTable();
             try
             {
                 if (sqlcon.State == ConnectionState.Closed)
                 {
                     sqlcon.Open();
                 }
                 SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductSalesPriceByProductCodeUnitAndPricingLevel", sqlcon);
                 sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                 SqlParameter sprmparam = new SqlParameter();
                 sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                 sprmparam.Value = strProductCode;
                 sprmparam = sdaadapter.SelectCommand.Parameters.Add("@UnitId", SqlDbType.VarChar);
                 sprmparam.Value = strunitid;
                 sprmparam = sdaadapter.SelectCommand.Parameters.Add("@pricinglevelId", SqlDbType.VarChar);
                 sprmparam.Value = strPricingLevel;
                 sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                 sprmparam.Value = PublicVariables._branchId;
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
        public void ProductSalesPriceUpdateByUnitIdAndProductCode(ProductSalesPriceInfo productSalesPriceinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductSalesPriceUpdateByUnitIdAndProductCode", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
              
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.ProductCode;
                sprmparam = sccmd.Parameters.Add("@UnitId", SqlDbType.VarChar);
                sprmparam.Value = productSalesPriceinfo.UnitId;
          
                sprmparam = sccmd.Parameters.Add("@SalesPrice", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.SalesPrice;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.Amount;
                sprmparam = sccmd.Parameters.Add("@discPerc", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.DiscPercentage;
                sprmparam = sccmd.Parameters.Add("@discAmount", SqlDbType.Decimal);
                sprmparam.Value = productSalesPriceinfo.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;

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
