using System;
using System.Data;
using System.Configuration;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Windows.Forms;
//<summary>
//Summary description for ProductDetailsSP
//</summary>
namespace FinacPOS
{
    public class ProductDetailsSP : DBConnection
    {
        public static int inEffected = 0;
        public DataTable  ProductDetailsAdd(ProductDetailsInfo productdetailsinfo)
        {
            DataTable Result = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter  sccmd = new  SqlDataAdapter ("ProductDetailsAdd", sqlcon);
                sccmd.SelectCommand. CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();               
                sprmparam = sccmd.SelectCommand .Parameters .Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.ProductCode;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@purchaseRate", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.PurchaseRate;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@salesRate", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.SalesRate;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@mrp", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.Mrp;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.TaxId;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@minimumStock", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.MinimumStock;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@maximunStock", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.MaximunStock;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@reorderLevel", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.ReorderLevel;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@openingStock", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.OpeningStock;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@taxType", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.TaxType;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@narration", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Narration;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@allowBatch", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.AllowBatch;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@bom", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.Bom;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@allowComponentSale", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.AllowComponentSale;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@active", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.Active;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@showReminder", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.ShowReminder;               
                sprmparam = sccmd.SelectCommand .Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Extra1;
                sprmparam = sccmd.SelectCommand .Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Extra2;
                
                SqlCommand sccmdExt = new SqlCommand("SELECT COUNT(NAME)FROM sys.procedures"+
" WHERE OBJECT_DEFINITION(OBJECT_ID) LIKE '%@gatePass%' AND NAME='ProductDetailsAdd'", sqlcon);
                sccmdExt.CommandType = CommandType.Text;               
                if (sccmdExt.ExecuteScalar().ToString() != "0")
                {
                    sprmparam = sccmd.SelectCommand .Parameters.Add("@gatePass", SqlDbType.Bit);

                 
                        sprmparam.Value = false;
                  
                }
                sprmparam = sccmd.SelectCommand .Parameters.Add("@fixedSalesRate", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.FixedSalesRate;
                sprmparam = sccmd.SelectCommand.Parameters. Add("@category", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Category;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.GroupCode;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@IsVanSaleProduct", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.IsVanSaleProduct;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@ShowExpiry", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.ShowExpiry;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@ExpiryDays", SqlDbType.Int);
                sprmparam.Value = productdetailsinfo.ExpiryDays;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@PurchaseRatePer", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.PurchaseRatePer;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@Ingredients", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.Ingredients;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@NutritionFact", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.NutritionFact;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@NutritionName", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.NutritionName;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@NutritionDetails", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.NutritionDetails;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@Location", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.Location;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@alternateNo", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.alternateNo;
                sprmparam = sccmd.SelectCommand.Parameters.Add("@withOutBarcode", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.WithOutBarcode;
                sccmd.Fill(Result);
           


            }
            catch (Exception ex)
            {
                Result = new DataTable();
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return Result;
        }
        public void ProductDetailsEdit(ProductDetailsInfo productdetailsinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductDetailsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.ProductDetailsId;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.ProductCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@purchaseRate", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.PurchaseRate;
                sprmparam = sccmd.Parameters.Add("@salesRate", SqlDbType.Decimal);
               
                    sprmparam.Value = productdetailsinfo.SalesRate;
                
                sprmparam = sccmd.Parameters.Add("@mrp", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.Mrp;
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.TaxId;
                sprmparam = sccmd.Parameters.Add("@minimumStock", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.MinimumStock;
                sprmparam = sccmd.Parameters.Add("@maximunStock", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.MaximunStock;
                sprmparam = sccmd.Parameters.Add("@reorderLevel", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.ReorderLevel;
                sprmparam = sccmd.Parameters.Add("@openingStock", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.OpeningStock;
                sprmparam = sccmd.Parameters.Add("@taxType", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.TaxType;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Narration;
                sprmparam = sccmd.Parameters.Add("@allowBatch", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.AllowBatch;
                sprmparam = sccmd.Parameters.Add("@bom", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.Bom;
                sprmparam = sccmd.Parameters.Add("@allowComponentSale", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.AllowComponentSale;
                sprmparam = sccmd.Parameters.Add("@active", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.Active;
                sprmparam = sccmd.Parameters.Add("@showReminder", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.ShowReminder;
             
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Extra2;
                  SqlCommand sccmdExt = new SqlCommand("SELECT COUNT(NAME)FROM sys.procedures"+
" WHERE OBJECT_DEFINITION(OBJECT_ID) LIKE '%@gatePass%' AND NAME='ProductDetailsEdit'", sqlcon);
                sccmdExt.CommandType = CommandType.Text;
                if (sccmdExt.ExecuteScalar().ToString() != "0")
                {
                    sprmparam = sccmd.Parameters.Add("@gatePass", SqlDbType.Bit);
                  
                        sprmparam.Value = false;
                    
                }
                sprmparam = sccmd.Parameters.Add("@fixedSalesRate", SqlDbType.Decimal);                
                sprmparam.Value = productdetailsinfo.FixedSalesRate;
                sprmparam = sccmd.Parameters.Add("@category", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.Category;
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.GroupCode;
                sprmparam = sccmd.Parameters.Add("@IsVanSaleProduct", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.IsVanSaleProduct;
                sprmparam = sccmd.Parameters.Add("@ShowExpiry", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.ShowExpiry;
                sprmparam = sccmd.Parameters.Add("@ExpiryDays", SqlDbType.Int);
                sprmparam.Value = productdetailsinfo.ExpiryDays;
                sprmparam = sccmd.Parameters.Add("@PurchaseRatePer", SqlDbType.Decimal);
                sprmparam.Value = productdetailsinfo.PurchaseRatePer;
                sprmparam = sccmd.Parameters.Add("@Ingredients", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.Ingredients;
                sprmparam = sccmd.Parameters.Add("@NutritionFact", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.NutritionFact;
                sprmparam = sccmd.Parameters.Add("@NutritionName", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.NutritionName;
                sprmparam = sccmd.Parameters.Add("@NutritionDetails", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.NutritionDetails;
                sprmparam = sccmd.Parameters.Add("@Location", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.Location;
                sprmparam = sccmd.Parameters.Add("@alternateNo", SqlDbType.NVarChar);
                sprmparam.Value = productdetailsinfo.alternateNo;
                sprmparam = sccmd.Parameters.Add("@withOutBarcode", SqlDbType.Bit);
                sprmparam.Value = productdetailsinfo.WithOutBarcode;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }

        public void ProductDetailsSaleRateEdit(decimal salesRate, string productCode)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductDetailsSaleRateEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();        
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;              
                sprmparam = sccmd.Parameters.Add("@salesRate", SqlDbType.Decimal);
                sprmparam.Value = salesRate;              
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
        public ProductDetailsInfo ProductDetailsViewByBranch(string productCode)
        {
            ProductDetailsInfo productdetailsinfo = new ProductDetailsInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductDetailsViewByBranch", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                SqlCommand sccmdExt = new SqlCommand("SELECT COUNT(NAME)FROM sys.procedures" +
" WHERE OBJECT_DEFINITION(OBJECT_ID) LIKE '%gatePass%' AND NAME='ProductDetailsViewByBranch'", sqlcon);
                    sccmdExt.CommandType = CommandType.Text;
                string strExt=sccmdExt.ExecuteScalar().ToString();
                   
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    productdetailsinfo.ProductDetailsId = sdrreader[0].ToString();
                    productdetailsinfo.ProductCode = sdrreader[1].ToString();
                    productdetailsinfo.BranchId = sdrreader[2].ToString();
                    productdetailsinfo.PurchaseRate = decimal.Parse(sdrreader[3].ToString());
                    productdetailsinfo.SalesRate = decimal.Parse(sdrreader[4].ToString());
                    productdetailsinfo.Mrp = decimal.Parse(sdrreader[5].ToString());
                    productdetailsinfo.TaxId = sdrreader[6].ToString();
                    productdetailsinfo.MinimumStock = decimal.Parse(sdrreader[7].ToString());
                    productdetailsinfo.MaximunStock = decimal.Parse(sdrreader[8].ToString());
                    productdetailsinfo.ReorderLevel = decimal.Parse(sdrreader[9].ToString());
                    productdetailsinfo.OpeningStock = decimal.Parse(sdrreader[10].ToString());
                    productdetailsinfo.TaxType = sdrreader[11].ToString();
                    productdetailsinfo.Narration = sdrreader[12].ToString();
                    productdetailsinfo.AllowBatch = bool.Parse(sdrreader[13].ToString());
                    productdetailsinfo.Bom = bool.Parse(sdrreader[14].ToString());
                    productdetailsinfo.AllowComponentSale = bool.Parse(sdrreader[15].ToString());
                    productdetailsinfo.Active = bool.Parse(sdrreader[16].ToString());
                    productdetailsinfo.ShowReminder = bool.Parse(sdrreader[17].ToString());
                    productdetailsinfo.ExtraDate = DateTime.Parse(sdrreader[18].ToString());
                    productdetailsinfo.Extra1 = sdrreader[19].ToString();
                    productdetailsinfo.Extra2 = sdrreader[20].ToString();
                   
                    productdetailsinfo.FixedSalesRate = decimal.Parse(sdrreader["fixedSalesRate"].ToString());
                    productdetailsinfo.Category = sdrreader["category"].ToString();
                    productdetailsinfo.GroupCode = sdrreader["groupCode"].ToString();
                    productdetailsinfo.IsVanSaleProduct =bool.Parse( sdrreader["IsVanSaleProduct"].ToString());
                    productdetailsinfo.ShowExpiry = bool.Parse(sdrreader["ShowExpiry"].ToString());
                    productdetailsinfo.ExpiryDays = int.Parse(sdrreader["ExpiryDays"].ToString());
                    productdetailsinfo.PurchaseRatePer = decimal.Parse(sdrreader["PurchaseRatePer"].ToString());

                    productdetailsinfo.Ingredients = sdrreader["Ingredients"].ToString();
                    productdetailsinfo.NutritionFact =bool.Parse( sdrreader["NutritionFact"].ToString());
                    productdetailsinfo.NutritionName = sdrreader["NutritionName"].ToString();
                    productdetailsinfo.NutritionDetails = sdrreader["NutritionDetails"].ToString();
                    productdetailsinfo.Location = sdrreader["Location"].ToString();
                    productdetailsinfo.alternateNo = sdrreader["alternateNo"].ToString();
                    productdetailsinfo.WithOutBarcode =bool.Parse( sdrreader["withOutBarcode"].ToString());
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return productdetailsinfo;
        }
        public void ProductDetailsDelete(string ProductDetailsId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductDetailsDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
                sprmparam.Value = ProductDetailsId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
        }
        public int ProductDetailsGetMax()
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductDetailsMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                max = int.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return max;
        }
        public bool TaxReferenceForProductCheck(string strTaxId,string strProductCode)
        {
            bool  isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("TaxReferenceForProductCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = strTaxId;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isReference;
        }
        public bool ProductBOMSubPrdReferenceCheck(string strMainPrdCode, string strSubPrdCode)
        {
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductBOMSubPrdReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@mainPrdCode", SqlDbType.VarChar);
                sprmparam.Value = strMainPrdCode;
                sprmparam = sccmd.Parameters.Add("@subPrdCode", SqlDbType.VarChar);
                sprmparam.Value = strSubPrdCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isReference;
        }
        public bool ProductBOMReferenceCheck(string strMainPrdCode)
        {
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductBOMReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@mainPrdCode", SqlDbType.VarChar);
                sprmparam.Value = strMainPrdCode;                
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isReference;
        }
        public bool ProductPackageSaleSubPrdReferenceCheck(string strMainPrdCode, string strSubPrdCode)
        {
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductPackageSaleSubPrdReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@mainPrdCode", SqlDbType.VarChar);
                sprmparam.Value = strMainPrdCode;
                sprmparam = sccmd.Parameters.Add("@subPrdCode", SqlDbType.VarChar);
                sprmparam.Value = strSubPrdCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isReference;
        }
        public bool ProductPackageSaleReferenceCheck(string strMainPrdCode)
        {
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductPackageSaleReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@mainPrdCode", SqlDbType.VarChar);
                sprmparam.Value = strMainPrdCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isReference;
        }
        public bool ProductReferenceInComponentSettingsCheck(string strPrdCode)
        {
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductReferenceInComponentSettingsCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strPrdCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isReference;
        }
        public bool ProductReferenceInBOMCheck(string strPrdCode)
        {
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductReferenceInBOMCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strPrdCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isReference;
        }
        public void ProductTaxChange(ProductDetailsInfo productdetailsinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductTaxChange", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.ProductCode;
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = productdetailsinfo.TaxId;
                inEffected = sccmd.ExecuteNonQuery();
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
        public DataTable ProductMainGroupViewAll()////created by sheena on 04-05-2023
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductMainGroupViewAll", sqlcon);
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
        public ProductMainGroupInfo getMainGroupNextNmbr(string groupCode)////created by sheena on 09-05-2023
        {
            ProductMainGroupInfo productmaingroupinfo = new ProductMainGroupInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupGetNextNumber", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = groupCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
               

                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    productmaingroupinfo.NextNumber = int.Parse(sdrreader["nextNumber"].ToString());
                    productmaingroupinfo.ProductCodeLength = int.Parse(sdrreader["productCodeLength"].ToString());
                   

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

            return productmaingroupinfo;
        }
        //-----------function to update nextnumber to +1 in  productmain group table-----------------
        public void ProductMainGroupNxtNmbrUpdate(string groupCode)////created by sheena on 09-05-2023
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupNxtNmbrUpdate", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = groupCode;


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

