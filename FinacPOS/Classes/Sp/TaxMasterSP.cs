using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for TaxMasterSP    
//</summary>    
namespace FinacPOS   
{    
class TaxMasterSP:DBConnection    
{    
    
    public DataTable TaxGetProductTax()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("TaxGetProductTax", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value =  PublicVariables._branchId;
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
    public DataTable TaxGetByCondition(string strCondition,bool isActive,string strVoucherType,string strMasterId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("TaxGetByCondition", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@active", SqlDbType.Bit);
            prm.Value = isActive;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@condition", SqlDbType.VarChar);
            prm.Value = strCondition;
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
            prm.Value = strVoucherType;
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherId", SqlDbType.VarChar);
            prm.Value = strMasterId;
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
    public DataTable TaxMasterViewAll(string strCurrentId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("TaxMasterViewAll", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value =  PublicVariables._branchId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@currentId", SqlDbType.VarChar);
            prm.Value = strCurrentId;
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
    public DataTable TaxMasterViewAllForSearch(string str, string strbranchId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("TaxMasterViewAllForSearch", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
            sprmparam.Value = str;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = strbranchId;
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
    
    public void TaxMasterDelete(string TaxId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("TaxMasterDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
            sprmparam.Value = TaxId;
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
    public int TaxMasterGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("TaxMasterMax", sqlcon);
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
    public bool TaxMasterExistance(string strTaxName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("TaxMasterExistance", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@taxName", SqlDbType.VarChar);
            sprmparam.Value = strTaxName;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;
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
    public bool TaxReferenceCheck(string strTaxId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("TaxReferenceCheck", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
            sprmparam.Value = strTaxId;
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
    public DataTable TaxGetProductTaxWithNA()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("TaxGetProductTaxWithNA", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value =  PublicVariables._branchId;
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
    public DataTable TaxReportByVoucherWiseFill(DateTime fromDate, DateTime toDate, string strTaxId, string strType, bool isInput, string strBranchId, bool isOptional, string strCurrencyId, string strFormType)
    {
        DataTable dtbl = new DataTable();
        try
        {
          

            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            dtbl.Columns.Add("SlNo.", typeof(int));
            dtbl.Columns["SlNo."].AutoIncrement = true;
            dtbl.Columns["SlNo."].AutoIncrementStep = 1;
            dtbl.Columns["SlNo."].AutoIncrementSeed = 1;
            SqlDataAdapter sdaadapter = new SqlDataAdapter("TaxReportByVoucherWiseFill", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime);
            sprmparam.Value = fromDate;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@todate", SqlDbType.DateTime);
            sprmparam.Value = toDate;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@taxId", SqlDbType.VarChar);
            sprmparam.Value = strTaxId;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@type", SqlDbType.VarChar);
            sprmparam.Value = strType;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@input", SqlDbType.Bit);
            sprmparam.Value = isInput;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = strBranchId;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = isOptional;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sprmparam.Value = strCurrencyId;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@formType", SqlDbType.VarChar);
            sprmparam.Value = strFormType;
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
    public DataTable TaxReportByProductWiseFill(DateTime fromDate, DateTime toDate, string strTaxId, string strType, bool isInput, string strBranchId, bool isOptional, string strCurrencyId, string strFormType)
    {
        DataTable dtbl = new DataTable();
        try
        {
          


            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            dtbl.Columns.Add("SlNo.", typeof(int));
            dtbl.Columns["SlNo."].AutoIncrement = true;
            dtbl.Columns["SlNo."].AutoIncrementStep = 1;
            dtbl.Columns["SlNo."].AutoIncrementSeed = 1;
            SqlDataAdapter sdaadapter = new SqlDataAdapter("TaxReportByProductWiseFill", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime);
            sprmparam.Value = fromDate;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@todate", SqlDbType.DateTime);
            sprmparam.Value = toDate;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@taxId", SqlDbType.VarChar);
            sprmparam.Value = strTaxId;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@type", SqlDbType.VarChar);
            sprmparam.Value = strType;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@input", SqlDbType.Bit);
            sprmparam.Value = isInput;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = strBranchId;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = isOptional;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sprmparam.Value = strCurrencyId;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@formType", SqlDbType.VarChar);
            sprmparam.Value = strFormType;
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
    public bool TaxMasterExistanceInDetails(string strTaxName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("TaxMasterExistanceInDetails", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@taxName", SqlDbType.VarChar);
            sprmparam.Value = strTaxName;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
    public decimal TaxGetCessDetails(string strTaxId)
    {
        decimal dcRate = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("TaxGetCessDetails", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@selectedTaxId", SqlDbType.VarChar);
            sprmparam.Value = strTaxId;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;
            object obj = sccmd.ExecuteScalar();
            if (obj != null)
                dcRate = decimal.Parse(obj.ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dcRate ;
    }
}
}
