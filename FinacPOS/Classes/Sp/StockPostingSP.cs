using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for StockPostingSP    
//</summary>    
namespace FinacPOS    
{    
class StockPostingSP:DBConnection    
{
    //
    public bool  StockPostingAddForQueryExecuter(StockPostingInfo stockpostinginfo)
    {
        bool isExecuted = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingAddForQueryExecuter", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();

            sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
            sprmparam.Value = stockpostinginfo.Date;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherType;
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherNo;
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.BatchId;
            sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.GodownId;
            sprmparam = sccmd.Parameters.Add("@inwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.InwardQty;
            sprmparam = sccmd.Parameters.Add("@outwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.OutwardQty;
            sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.Rate;
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.UnitId;
            sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = stockpostinginfo.Optional;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.RackId;
         
            isExecuted = bool.Parse(sccmd.ExecuteScalar().ToString());

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return isExecuted;
    }
    public void StockPostingAdd(StockPostingInfo stockpostinginfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
           
            sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
            sprmparam.Value = stockpostinginfo.Date;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherType;
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherNo;
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.BatchId;
            sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.GodownId;
            sprmparam = sccmd.Parameters.Add("@inwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.InwardQty;
            sprmparam = sccmd.Parameters.Add("@outwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.OutwardQty;
            sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.Rate;
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.UnitId;
            sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = stockpostinginfo.Optional;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;          
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.RackId;
            sprmparam = sccmd.Parameters.Add("@VoucherQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.VoucherQty;
            sprmparam = sccmd.Parameters.Add("@VoucherUnitId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherUnitId;
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
    public void StockPostingAddAgainst(StockPostingInfo stockpostinginfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingAddAgainst", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();

            sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
            sprmparam.Value = stockpostinginfo.Date;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherType;
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherNo;
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.BatchId;
            sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.GodownId;
            sprmparam = sccmd.Parameters.Add("@inwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.InwardQty;
            sprmparam = sccmd.Parameters.Add("@outwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.OutwardQty;
            sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.Rate;
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.UnitId;
            sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = stockpostinginfo.Optional;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@referenceNo", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.ReferenceNo;
            sprmparam = sccmd.Parameters.Add("@referenceType", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.ReferenceType;
            sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.RackId;
            sccmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
    }
    public void StockPostingAdd1(StockPostingInfo stockpostinginfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();

            sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
            sprmparam.Value = stockpostinginfo.Date;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherType;
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherNo;
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.BatchId;
            sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.GodownId;
            sprmparam = sccmd.Parameters.Add("@inwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.InwardQty;
            sprmparam = sccmd.Parameters.Add("@outwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.OutwardQty;
            sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.Rate;
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.UnitId;
            sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = stockpostinginfo.Optional;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.RackId;
            sccmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
    }
    public void StockPostingEdit(StockPostingInfo stockpostinginfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@stockPostingId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.StockPostingId;
            sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
            sprmparam.Value = stockpostinginfo.Date;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherType;
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.VoucherNo;
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.BatchId;
            sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.GodownId;
            sprmparam = sccmd.Parameters.Add("@inwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.InwardQty;
            sprmparam = sccmd.Parameters.Add("@outwardQty", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.OutwardQty;
            sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
            sprmparam.Value = stockpostinginfo.Rate;
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.UnitId;
            sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = stockpostinginfo.Optional;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extraDate", SqlDbType.DateTime);
            sprmparam.Value = stockpostinginfo.ExtraDate;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
            sprmparam.Value = stockpostinginfo.RackId;
            sccmd.ExecuteNonQuery();        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }

    }
    public DataTable StockPostingViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("StockPostingViewAll", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaadapter.Fill(dtbl);

        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public StockPostingInfo StockPostingView(string stockPostingId )
    {
        StockPostingInfo stockpostinginfo =new StockPostingInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@stockPostingId", SqlDbType.VarChar);
            sprmparam.Value = stockPostingId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                stockpostinginfo.StockPostingId= sdrreader[0].ToString();
                stockpostinginfo.Date=DateTime.Parse(sdrreader[1].ToString());
                stockpostinginfo.VoucherType= sdrreader[2].ToString();
                stockpostinginfo.VoucherNo= sdrreader[3].ToString();
                stockpostinginfo.ProductCode= sdrreader[4].ToString();
                stockpostinginfo.BatchId= sdrreader[5].ToString();
                stockpostinginfo.GodownId= sdrreader[6].ToString();
                stockpostinginfo.InwardQty=decimal.Parse(sdrreader[7].ToString());
                stockpostinginfo.OutwardQty=decimal.Parse(sdrreader[8].ToString());
                stockpostinginfo.Rate=decimal.Parse(sdrreader[9].ToString());
                stockpostinginfo.UnitId= sdrreader[10].ToString();
                stockpostinginfo.Optional=bool.Parse(sdrreader[11].ToString());
                stockpostinginfo.BranchId= sdrreader[12].ToString();
                stockpostinginfo.ExtraDate=DateTime.Parse(sdrreader[13].ToString());
                stockpostinginfo.Extra1= sdrreader[14].ToString();
                stockpostinginfo.Extra2= sdrreader[15].ToString();
                stockpostinginfo.RackId = sdrreader[16].ToString();
            }
            sdrreader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return stockpostinginfo;
    }
    public void StockPostingDelete(string StockPostingId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@stockPostingId", SqlDbType.VarChar);
            sprmparam.Value = StockPostingId;
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
    public int StockPostingGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingMax", sqlcon);
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
    
    public DataTable StPostingViewByVoucherTypeAndVoucherNumberAndBranch(string strVocuherNo, string strVoucherType)
    {
     
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("StPostingViewByVoucherTypeAndVoucherNumberAndBranch", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            prm.Value = strVocuherNo;
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
            prm.Value = strVoucherType;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            sdaadapter.Fill(dtbl);

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public void StPostingDeleteByVoucherTypeAndVoucherNumberAndBranch(string strVocuherNo, string strVoucherType,string brachId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StPostingDeleteByVoucherTypeAndVoucherNumberAndBranch", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();          
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = strVocuherNo;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = strVoucherType;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = brachId;
            sccmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
    }
    public decimal  ProductGetCurrentStock(string strproductCode,string godownId,string batchId,string rackId)
    {
        decimal dcStock = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGetCurrentStock", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            prm.Value = strproductCode;
            prm = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
            prm.Value = godownId;
            prm = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            prm.Value = batchId;
            prm = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
            prm.Value = rackId;
            prm = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            dcStock = decimal.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dcStock;
    }

    public void StockPostingGetOpeningStock(string strProductCode)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("StockPostingGetOpeningStock", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            sccmd.Parameters.Add("@productCode", SqlDbType.VarChar).Value = strProductCode;
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
