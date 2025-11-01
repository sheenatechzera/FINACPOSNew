using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;   
//<summary>    
//Summary description for BatchSP    
//</summary>    
namespace FinacPOS
{    
class BatchSP:DBConnection
{
    SqlDateTime dtnull = SqlDateTime.Null;  
    public void BatchAdd(BatchInfo batchinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = batchinfo.BatchId;
            sprmparam = sccmd.Parameters.Add("@batchName", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.BatchName;
            sprmparam = sccmd.Parameters.Add("@mfd", SqlDbType.DateTime);
            sprmparam.Value = batchinfo.Mfd;
            sprmparam = sccmd.Parameters.Add("@expd", SqlDbType.DateTime);
            sprmparam.Value = batchinfo.Expd;
            sprmparam = sccmd.Parameters.Add("@extraDate", SqlDbType.DateTime);
            sprmparam.Value = batchinfo.ExtraDate;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.Extra2;
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
    public string  BatchSaveorEditandGetId(BatchInfo batchinfo)
    {
        string strBatchId = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchSaveorEditandGetId", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();

            sprmparam = sccmd.Parameters.Add("@batchName", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.BatchName;
            
            sprmparam = sccmd.Parameters.Add("@mfd", SqlDbType.DateTime);
            if (batchinfo.Mfd != DateTime.Parse("1/1/1753"))
            {
                sprmparam.Value = batchinfo.Mfd;
            }
            else
            {
                sprmparam.Value = dtnull;
            }
            sprmparam = sccmd.Parameters.Add("@expd", SqlDbType.DateTime);
       
            if (batchinfo.Expd != DateTime.Parse("1/1/1753"))
            {
                sprmparam.Value = batchinfo.Expd;
            }
            else
            {
                sprmparam.Value = dtnull;
            }
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.Extra2;
           strBatchId= sccmd.ExecuteScalar().ToString();


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return strBatchId;
    }
    public void BatchEdit(BatchInfo batchinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = batchinfo.BatchId;
            sprmparam = sccmd.Parameters.Add("@batchName", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.BatchName;
            sprmparam = sccmd.Parameters.Add("@mfd", SqlDbType.DateTime);
            sprmparam.Value = batchinfo.Mfd;
            sprmparam = sccmd.Parameters.Add("@expd", SqlDbType.DateTime);
            sprmparam.Value = batchinfo.Expd;
            sprmparam = sccmd.Parameters.Add("@extraDate", SqlDbType.DateTime);
            sprmparam.Value = batchinfo.ExtraDate;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = batchinfo.Extra2;
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
    public DataTable BatchViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BatchViewAll", sqlcon);
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
    public AutoCompleteStringCollection BatchViewAllForList()
    {
        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchViewAll", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                collection.Add(sdrreader[1].ToString());
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
        return collection;
    }
    public DataTable BatchViewAllForSearch(string strStartText)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BatchViewAllForSearch", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
            prm.Value = strStartText;
          
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
    public DataTable BatchViewAllForReport()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BatchViewAllForReport", sqlcon);
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
    public DataTable BatchwiseSalesReportFill(DateTime dtFrom, DateTime dtTo, string strBatchId, string strPrdCode, bool isVoucherwise, string strBranch, bool isOptional, string strCurrencyId, bool IsAccountsPosting)
    {
        DataTable dtbl = new DataTable();
        try
        {

            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BatchwiseSalesReportFill", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();
            param = sdaadapter.SelectCommand.Parameters.Add("@batchId", SqlDbType.VarChar);
            param.Value = strBatchId;
            param = sdaadapter.SelectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime);
            param.Value = dtFrom;
            param = sdaadapter.SelectCommand.Parameters.Add("@todate", SqlDbType.DateTime);
            param.Value = dtTo;
            param = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
            param.Value = strPrdCode;
            param = sdaadapter.SelectCommand.Parameters.Add("@voucherwise", SqlDbType.Bit);
            param.Value = isVoucherwise;
            param = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            param.Value = strBranch;
            param = sdaadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            param.Value = isOptional;
            param = sdaadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            param.Value = strCurrencyId;
            param = sdaadapter.SelectCommand.Parameters.Add("@IsAccountsPosting", SqlDbType.Bit);
            param.Value = IsAccountsPosting;
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
    public DataTable FillProductByBatchForRpt(string strBatchId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("FillProductByBatchForRpt", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();
            param = sdaadapter.SelectCommand.Parameters.Add("@batchId", SqlDbType.VarChar);
            param.Value = strBatchId;
            param = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            param.Value = PublicVariables._branchId;
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
    public BatchInfo BatchView(string batchId )
    {
        BatchInfo batchinfo =new BatchInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = batchId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                batchinfo.BatchId= sdrreader[0].ToString();
                batchinfo.BatchName= sdrreader[1].ToString();
                if (batchinfo.BatchName != "NA")
                {
                batchinfo.Mfd=DateTime.Parse(sdrreader[2].ToString());
                
                    batchinfo.Expd = DateTime.Parse(sdrreader[3].ToString());
                    
                }
                batchinfo.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                batchinfo.Extra1= sdrreader[5].ToString();
                batchinfo.Extra2= sdrreader[6].ToString();
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
        return batchinfo;
    }
    public BatchInfo BatchViewByName(string batchId)
    {
        BatchInfo batchinfo = new BatchInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchViewByName", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@batchName", SqlDbType.VarChar);
            sprmparam.Value = batchId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                batchinfo.BatchId = sdrreader[0].ToString();
                batchinfo.BatchName = sdrreader[1].ToString();
                batchinfo.Mfd = DateTime.Parse(sdrreader[2].ToString());
                batchinfo.Expd = DateTime.Parse(sdrreader[3].ToString());
                batchinfo.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                batchinfo.Extra1 = sdrreader[5].ToString();
                batchinfo.Extra2 = sdrreader[6].ToString();
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
        return batchinfo;
    }
    public void BatchDelete(string BatchId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = BatchId;
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
    public int BatchGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchMax", sqlcon);
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

    public bool BatchCheckReference(string batchId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BatchCheckReference", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
            sprmparam.Value = batchId;
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
}
}
