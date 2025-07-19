using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for ProductSP    
//</summary>    
namespace FinacPOS
{
    class ProductSP : DBConnection
    {
        
        public DataTable POSPriceCheckFillGrid(string ProductCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSPriceCheckFillGrid", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@ProductCode", SqlDbType.VarChar);
                prm.Value = ProductCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
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

        public DataTable ProductViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewAll", sqlcon);
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
        public DataTable ProductViewAllByBranch()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewAllByBranch", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
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
        public DataTable GetProductTaxDetails(string ProductCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductTaxDetails", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@ProductCode", SqlDbType.VarChar);
                prm.Value = ProductCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
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
        //to get the Latest Sales Rate of the product for POS
        public decimal ProductSalesRateForSalePOS(string strProductCode, string strPricingLevelId, DateTime date, string strUnitId)
        {
            decimal dRate = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductRateForSalePOS", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@pricingLevelId", SqlDbType.VarChar);
                sprmparam.Value = strPricingLevelId;
                sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
                sprmparam.Value = date;
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = strUnitId;
                dRate = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dRate;
        }

        public bool ProductReferenceInAllBranchCheck(string ProductCode)
        {
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductReferenceInAllBranchCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = ProductCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString()); ;
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
        public string ProductCheckForPOS(string ProductCode, string productName)
        {
            string strProductcode = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductCheckForPOS", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = ProductCode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.VarChar);
                sprmparam.Value = productName;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                strProductcode = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return strProductcode;
        }

        public bool ProductGetMax()
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                sccmd.ExecuteScalar().ToString();
            }
            catch
            {
                isExist = true;
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public string ProductMax()
        {
            string str = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                str = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Conversion failed when converting the varchar value"))
                {
                    str = "string";
                }
                else
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            finally
            {
                sqlcon.Close();
            }
            return str;
        }

        public DataTable ShortExpiryProductGetByGodown(string groupId, string productCode, string brandId, string taxId, string taxType, string godownId, string rackId, string startText, string branchId, DateTime date)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ShortExpiryProductGet", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = groupId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = productCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@brandId", SqlDbType.VarChar);
                prm.Value = brandId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@taxId", SqlDbType.VarChar);
                prm.Value = taxId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@taxType", SqlDbType.VarChar);
                prm.Value = taxType;
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = godownId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@rackId", SqlDbType.VarChar);
                prm.Value = rackId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                prm.Value = startText;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@date", SqlDbType.DateTime);
                prm.Value = date;
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

        public DataTable ProductSearchTest(string groupId, string productCode, string brandId, string taxId, string taxType, string godownId, string rackId, string criteria, string startText, string branchId, bool isActive, bool isDatewise, DateTime fromDate, DateTime toDate, bool isFromReminder)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductSearchTest", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = groupId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = productCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@brandId", SqlDbType.VarChar);
                prm.Value = brandId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@taxId", SqlDbType.VarChar);
                prm.Value = taxId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@taxType", SqlDbType.VarChar);
                prm.Value = taxType;
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = godownId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@rackId", SqlDbType.VarChar);
                prm.Value = rackId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@criteria", SqlDbType.VarChar);
                prm.Value = criteria;
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                prm.Value = startText;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@isActive", SqlDbType.Bit);
                prm.Value = isActive;
                prm = sdaadapter.SelectCommand.Parameters.Add("@isDatewise", SqlDbType.Bit);
                prm.Value = isDatewise;
                prm = sdaadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                prm.Value = fromDate;
                prm = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                prm.Value = toDate;
                prm = sdaadapter.SelectCommand.Parameters.Add("@isFromReminder", SqlDbType.Bit);
                prm.Value = isFromReminder;
                sdaadapter.Fill(dtbl);
                if (isFromReminder)
                    if (dtbl.Columns.Contains("defaultStock"))
                        dtbl.Columns.Remove("defaultStock");

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

        public DataTable ProductSearchByGodown(string strCriteria, string strStartText, string strgodownId, bool isFromReminder, string branchId, bool isActive, bool isDatewise, DateTime dtFrom, DateTime dtTo)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductSearchByGodown", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@criteria", SqlDbType.VarChar);
                prm.Value = strCriteria;
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                prm.Value = strStartText;
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = strgodownId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@isFromReminder", SqlDbType.Bit);
                prm.Value = isFromReminder;
                prm = sdaadapter.SelectCommand.Parameters.Add("@isActive", SqlDbType.Bit);
                prm.Value = isActive;
                prm = sdaadapter.SelectCommand.Parameters.Add("@isDatewise", SqlDbType.Bit);
                prm.Value = isDatewise;
                prm = sdaadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                prm.Value = dtFrom;
                prm = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                prm.Value = dtTo;
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
        public DataTable ProductViewWithAllDetails(string strProductCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewWithAllDetails", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = strProductCode;
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

        public DataTable ProductViewWithAllGodownBatchs(string strProductCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewWithAllGodownBatchs", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = strProductCode;
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


        public DataTable ProductViewAllActiveByGroupId(string strGroupId, string strBranchId)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewAllActiveByGroupId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = strGroupId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = strBranchId;
                sdaadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }

        public DataTable ProductFill(string branchId)// ,string starttext)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductFill", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable ProductViewAllActiveWithStock(string branchId, string godownId, bool isBOM, bool isPackage, string starttext, string rackId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductViewAllActiveWithStock", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = godownId;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@isBOM", SqlDbType.Bit);
                sprmparam.Value = isBOM;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@isPackage", SqlDbType.Bit);
                sprmparam.Value = isPackage;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                sprmparam.Value = starttext;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@rackId", SqlDbType.VarChar);
                sprmparam.Value = rackId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable ProductViewAllActiveWithStockForBarCode(string branchId, string strProductCode, string strProductName, int inStartIndex, int instopIndex, string strPurchaseInvoice)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductViewAllActiveWithStockForBarCode", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@productName", SqlDbType.VarChar);
                sprmparam.Value = strProductName;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@purchaseInvoiceNo", SqlDbType.VarChar);
                sprmparam.Value = strPurchaseInvoice;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sqldataadapter.Fill(inStartIndex, instopIndex, dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }

        public DataTable ProductViewAllActiveWithName(string branchId)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductViewAllActiveWithName", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable ProductViewAllActiveWithNameTwo(string branchId)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductViewAllActiveWithNameTwo", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        //to get the rate of the product for purchase related
        public decimal ProductRateForPurchase(string strProductCode)
        {
            decimal dRate = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductRateForPurchase", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                dRate = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dRate;
        }
        //get product Cost
        public DataTable ProductPurchaseRate(string strProductCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductRateForPurchase", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
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
        //to get the rate of the product for purchase PRICE
        //Added on 25/May/2023
        public DataTable ProductRateForPurchasebyLedgerId(string strProductCode, string strUnitId, string strLedgerId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductRateForPurchasebyLedgerId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@UnitId", SqlDbType.VarChar);
                sprmparam.Value = strUnitId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = strLedgerId;
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
        public DataTable GetProductDefaultSalesPrice(string strProductCode, string strUnitId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductDefaultSalesPrice", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@UnitId", SqlDbType.VarChar);
                sprmparam.Value = strUnitId;
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
        //to get the rate of the product for sales related
        public decimal ProductRateForSale(string strProductCode, string strPricingLevelId, DateTime date)
        {
            decimal dRate = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductRateForSale", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@pricingLevelId", SqlDbType.VarChar);
                sprmparam.Value = strPricingLevelId;
                sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
                sprmparam.Value = date;
                dRate = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dRate;
        }
        //to get the Latest Sales Rate of the product for POS
        public decimal ProductRateForSalePOS(string strProductCode, string strPricingLevelId, DateTime date, string strUnitId)
        {
            decimal dRate = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductRateForSalePOS", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@pricingLevelId", SqlDbType.VarChar);
                sprmparam.Value = strPricingLevelId;
                sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
                sprmparam.Value = date;
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = strUnitId;
                dRate = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dRate;
        }

        
        
        
        public DataTable ProductViewAllByCondition(string strGroupId, string strstartText, bool isAllUnderHeirarchy)
        {
            DataTable dtbl = new DataTable();
            try
            {
                dtbl.Columns.Add("Sl No");
                dtbl.Columns["Sl No"].AutoIncrement = true;
                dtbl.Columns["Sl No"].AutoIncrementSeed = 1;
                dtbl.Columns["Sl No"].AutoIncrementStep = 1;
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewAllByCondition", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = strGroupId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                sprmparam.Value = strstartText;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@isunderGroupHeirarchy", SqlDbType.Bit);
                sprmparam.Value = isAllUnderHeirarchy;
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
        public DataTable ProductViewAllByConditionNotInBranch(string strGroupId, string strstartText, bool isAllUnderHeirarchy, bool isInBranch)
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Sl No");
            dtbl.Columns["Sl No"].AutoIncrement = true;
            dtbl.Columns["Sl No"].AutoIncrementSeed = 1;
            dtbl.Columns["Sl No"].AutoIncrementStep = 1;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewAllByConditionNotInBranch", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = strGroupId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                sprmparam.Value = strstartText;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@isunderGroupHeirarchy", SqlDbType.Bit);
                sprmparam.Value = isAllUnderHeirarchy;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@isInBranch", SqlDbType.Bit);
                sprmparam.Value = isInBranch;
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

        public decimal ProductViewActiveWithStock(string strFrom, string strMasterId, string strPrdCode)
        {
            decimal dStock = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductViewActiveWithStock", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@fromCall", SqlDbType.VarChar);
                sprmparam.Value = strFrom;
                sprmparam = sccmd.Parameters.Add("@masterId", SqlDbType.VarChar);
                sprmparam.Value = strMasterId;
                sprmparam = sccmd.Parameters.Add("@prdCode", SqlDbType.VarChar);
                sprmparam.Value = strPrdCode;
                object obj = sccmd.ExecuteScalar();
                if (obj != null)
                    dStock = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dStock;
        }
        public decimal ProductStockGetCorrespondingtoBatchAndGodown(string strFrom, string strMasterId, string strPrdCode, string strBatchId, string strGodownId, string strRackId)
        {
            decimal dStock = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductStockGetCorrespondingtoBatchAndGodown", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@fromCall", SqlDbType.VarChar);
                sprmparam.Value = strFrom;
                sprmparam = sccmd.Parameters.Add("@masterId", SqlDbType.VarChar);
                sprmparam.Value = strMasterId;
                sprmparam = sccmd.Parameters.Add("@prdCode", SqlDbType.VarChar);
                sprmparam.Value = strPrdCode;
                sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
                sprmparam.Value = strBatchId;
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = strGodownId;
                sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
                sprmparam.Value = strRackId;
                dStock = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dStock;
        }
        public DataTable ProductViewAllForPromotionRegister(string strProductCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductViewAllForPromotionRegister", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = strProductCode;
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

        public DataTable ProductFromExcel(string strBranch, string strProductGroup, string strBrand, string strTax, string strBatchNo, string strUnit, string strGodown, string strRack, string strProductCode, string strProductName)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductFromExcel", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaadapter.SelectCommand.Parameters.Add("@branch", SqlDbType.VarChar).Value = strBranch;
                sdaadapter.SelectCommand.Parameters.Add("@productGroup", SqlDbType.VarChar).Value = strProductGroup;
                sdaadapter.SelectCommand.Parameters.Add("@brand", SqlDbType.VarChar).Value = strBrand;
                sdaadapter.SelectCommand.Parameters.Add("@tax", SqlDbType.VarChar).Value = strTax;
                sdaadapter.SelectCommand.Parameters.Add("@batchNo", SqlDbType.VarChar).Value = strBatchNo;
                sdaadapter.SelectCommand.Parameters.Add("@unit", SqlDbType.VarChar).Value = strUnit;
                sdaadapter.SelectCommand.Parameters.Add("@godown", SqlDbType.VarChar).Value = strGodown;
                sdaadapter.SelectCommand.Parameters.Add("@rack", SqlDbType.VarChar).Value = strRack;
                sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar).Value = strProductCode;
                sdaadapter.SelectCommand.Parameters.Add("@productName", SqlDbType.VarChar).Value = strProductName;
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
        public bool ProductCodeChangeReferenceCheck(string ProductCode)
        {
            bool hasReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductCodeChangeReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = ProductCode;

                hasReference = bool.Parse(sccmd.ExecuteScalar().ToString()); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return hasReference;
        }
        public bool ExcelCheckSameCodeWithDiffrentName(string ProductCode, string productName)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ExcelCheckSameCodeWithDiffrentName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = ProductCode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.VarChar);
                sprmparam.Value = productName;
                isExist = bool.Parse(sccmd.ExecuteScalar().ToString()); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public decimal ProductGetStockDetails(string productCode, string godownId, string rackId, string branchId, bool isDatewise, DateTime fromDate, DateTime toDate)
        {
            decimal dcStock = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductGetStockDetails", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                sccmd.Parameters.Add("@productCode", SqlDbType.VarChar).Value = productCode;
                sccmd.Parameters.Add("@godownId", SqlDbType.VarChar).Value = godownId;
                sccmd.Parameters.Add("@rackId", SqlDbType.VarChar).Value = rackId;
                sccmd.Parameters.Add("@branchId", SqlDbType.VarChar).Value = productCode;
                sccmd.Parameters.Add("@isDatewise", SqlDbType.Bit).Value = isDatewise;
                sccmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = fromDate;
                sccmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = toDate;
                dcStock = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch
            {

            }

            finally
            {
                sqlcon.Close();
            }
            return dcStock;
        }
        public string GetProductCodeByBarcode(string Barcode)
        {
            string strPrdCode = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GetProductCodeByBarcode", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = Barcode;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                strPrdCode = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                strPrdCode = "";
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return strPrdCode;
        }
        public DataTable GetProductDetailsByBarcode(string Barcode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("GetProductDetailsByBarcode", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@barcode", SqlDbType.VarChar);
                prm.Value = Barcode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
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
        public DataTable GetProductDetailsByProductCode(string ProductCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("GetProductDetailsByProductCode", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@ProductCode", SqlDbType.VarChar);
                prm.Value = ProductCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
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
        public DataTable ProductCodeGetByPartNoOrProductCode(string strBarcode, string strBranchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductCodeGetByPartNoOrProductCode", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@barcode", SqlDbType.VarChar);
                prm.Value = strBarcode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = strBranchId;
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
        public decimal ProductCountForBarCode(string branchId, string strProductCode, string strProductName, string strPurchaseInvoiceNo)
        {
            decimal count = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductCountForBarCode", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.VarChar);
                sprmparam.Value = strProductName;
                sprmparam = sccmd.Parameters.Add("@purchaseInvoiceNo", SqlDbType.VarChar);
                sprmparam.Value = strPurchaseInvoiceNo;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                count = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return count;
        }
        public bool ProductRelatedExistanceCheck(string ProductCode, string partNo)
        {
            //using
            bool isReference = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductRelatedExistanceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = ProductCode;

                sprmparam = sccmd.Parameters.Add("@partNo", SqlDbType.VarChar);
                sprmparam.Value = partNo;
                isReference = bool.Parse(sccmd.ExecuteScalar().ToString()); ;
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
        //
        public bool CheckProductNameExistance(string ProductName, string ProductCode)
        {
            //using
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CheckProductNameExistance", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.VarChar);
                sprmparam.Value = ProductName;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = ProductCode;
                isExist = bool.Parse(sccmd.ExecuteScalar().ToString()); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public DataTable ProductSearchForChangeTax(string strPCode, string strPName, string strGroup, string strTax)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductSearchForChangeTax", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar).Value = strPCode;
                sdaadapter.SelectCommand.Parameters.Add("@productName", SqlDbType.VarChar).Value = strPName;
                sdaadapter.SelectCommand.Parameters.Add("@taxId", SqlDbType.VarChar).Value = strTax;
                sdaadapter.SelectCommand.Parameters.Add("@gruopId", SqlDbType.VarChar).Value = strGroup;
                dtbl.Columns.Add("SlNo.", typeof(int));
                dtbl.Columns["SlNo."].AutoIncrement = true;
                dtbl.Columns["SlNo."].AutoIncrementStep = 1;
                dtbl.Columns["SlNo."].AutoIncrementSeed = 1;
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
        public AutoCompleteStringCollection ProductCodeViewAllByBranchCollection()
        {
            AutoCompleteStringCollection strCollection = new AutoCompleteStringCollection();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductViewAllByBranch", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    strCollection.Add(sdrreader[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlcon.Close();
            }
            return strCollection;
        }
        public AutoCompleteStringCollection ProductNameViewAllByBranchCollection()
        {
            AutoCompleteStringCollection strCollection = new AutoCompleteStringCollection();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductViewAllByBranch", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    strCollection.Add(sdrreader[1].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlcon.Close();
            }
            return strCollection;
        }
        public decimal ProductRateForSales(string strProductCode)
        {
            decimal decSalesRate = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand scmd = new SqlCommand("ProductRateForSales", sqlcon);
                scmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = scmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = scmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                decSalesRate = decimal.Parse(scmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return decSalesRate;
        }
        public decimal ProductRateForSalesbyUnit(string strProductCode, string strUnitId)
        {
            decimal decSalesRate = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand scmd = new SqlCommand("ProductRateForSalesbyUnit", sqlcon);
                scmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = scmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = scmd.Parameters.Add("@unitid", SqlDbType.VarChar);
                sprmparam.Value = strUnitId;
                sprmparam = scmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                decSalesRate = decimal.Parse(scmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                decSalesRate = 0;
                //MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return decSalesRate;
        }
        public decimal ProductRateForSalesProfit(string strProductCode)
        {
            decimal decPrift = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand scmd = new SqlCommand("ProductRateForSalesInProfit", sqlcon);
                scmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = scmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = strProductCode;
                sprmparam = scmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                decPrift = decimal.Parse(scmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return decPrift;
        }
        public DataTable StockValueReportBranchView(string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("StockValueReportBranchView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                SqlDataAdapter dataadapter = new SqlDataAdapter();
                dataadapter.SelectCommand = sccmd;
                dataadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show("CDSP :" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable StockValueReportBranchAll(string groupId, string productCode, string brandId, string taxId, string taxType, string godownId, string rackId, string startText, string branchId, bool ZeroStock)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("StockValueReportBranchAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = groupId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = productCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@brandId", SqlDbType.VarChar);
                prm.Value = brandId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@taxId", SqlDbType.VarChar);
                prm.Value = taxId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@taxType", SqlDbType.VarChar);
                prm.Value = taxType;
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = godownId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@rackId", SqlDbType.VarChar);
                prm.Value = rackId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                prm.Value = startText;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@ZeroStock", SqlDbType.Bit);
                prm.Value = ZeroStock;
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
        public DataTable StockValueBranchAllWiseReport(string groupId, string productCode, string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("StockValueBranchAllWiseReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = groupId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = productCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
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
        public DataTable POSProductSearchForSales(string criteria, string SearchValue, string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSProductSearchForSales", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@Criteria", SqlDbType.VarChar);
                prm.Value = criteria;
                prm = sdaadapter.SelectCommand.Parameters.Add("@SearchValue", SqlDbType.VarChar);
                prm.Value = SearchValue;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
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
        public DataTable ProductGroupViewAllByCategory(string category)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductGroupViewAllByCategory", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@category", SqlDbType.NVarChar);
                sprmparam.Value = category;
                sdaadapter.Fill(dtbl);
                DataRow dr = dtbl.NewRow();
                dr["groupId"] = "All";
                dr["groupName"] = "All";
                dtbl.Rows.Add(dr);
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
        public DataTable POSProductSearchWithImage(string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSProductSearchWithImage", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
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
        public DataTable AccountLedgerGetDetails(string criteria, string SearchValue, string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetDetails", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@Criteria", SqlDbType.VarChar);
                prm.Value = criteria;
                prm = sdaadapter.SelectCommand.Parameters.Add("@SearchValue", SqlDbType.VarChar);
                prm.Value = SearchValue;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
               
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
        public DataTable POSAccountLedgerGetNameByCode(string ledgerCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSAccountLedgerGetNameByCode", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@ledgerCode", SqlDbType.VarChar);
                sqlparameter.Value = ledgerCode;
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
        public DataTable GetCustomerDetailsbyLedgerId(string strLedgerId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerView", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sqlparameter.Value = strLedgerId;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
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
        public DataTable POSSalesRecieptCopy(string criteria, string SearchValue, string branchId,DateTime fromDate,DateTime toDate,string CounterId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSSalesRecieptCopy", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@Criteria", SqlDbType.VarChar);
                prm.Value = criteria;
                prm = sdaadapter.SelectCommand.Parameters.Add("@SearchValue", SqlDbType.VarChar);
                prm.Value = SearchValue;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                prm.Value = fromDate;
                prm = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                prm.Value = toDate;
                prm = sdaadapter.SelectCommand.Parameters.Add("@CounterId", SqlDbType.VarChar);
                prm.Value = @CounterId;


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
        public DataTable AccountLedgerGetPartyDetails(string criteria, string SearchValue, string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetPartyDetails", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@Criteria", SqlDbType.VarChar);
                prm.Value = criteria;
                prm = sdaadapter.SelectCommand.Parameters.Add("@SearchValue", SqlDbType.VarChar);
                prm.Value = SearchValue;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;

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
        public DataTable AccountLedgerGetDiectExpenseList(string criteria, string SearchValue, string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetDiectExpenseList", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@Criteria", SqlDbType.VarChar);
                prm.Value = criteria;
                prm = sdaadapter.SelectCommand.Parameters.Add("@SearchValue", SqlDbType.VarChar);
                prm.Value = SearchValue;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;

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
    }
}
