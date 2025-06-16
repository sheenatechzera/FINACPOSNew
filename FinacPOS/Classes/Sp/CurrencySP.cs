using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for CurrencySP    
//</summary>    
namespace FinacPOS    
{    
class CurrencySP:DBConnection    
{    
    public string  CurrencyAdd(CurrencyInfo currencyinfo)
    {
        string strid = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@currencySymbol", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.CurrencySymbol;
            sprmparam = sccmd.Parameters.Add("@currencyName", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.CurrencyName;
            sprmparam = sccmd.Parameters.Add("@subunitName", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.SubunitName;
            sprmparam = sccmd.Parameters.Add("@noOfDecimalPlace", SqlDbType.Int);
            sprmparam.Value = currencyinfo.NoOfDecimalPlace;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@defaultt", SqlDbType.Bit);
            sprmparam.Value = currencyinfo.Defaultt;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = currencyinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.Extra2;
            strid = sccmd.ExecuteScalar().ToString();

        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return strid;
    }
    public void CurrencyEdit(CurrencyInfo currencyinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sprmparam.Value = currencyinfo.CurrencyId;
            sprmparam = sccmd.Parameters.Add("@currencySymbol", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.CurrencySymbol;
            sprmparam = sccmd.Parameters.Add("@currencyName", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.CurrencyName;
            sprmparam = sccmd.Parameters.Add("@subunitName", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.SubunitName;
            sprmparam = sccmd.Parameters.Add("@noOfDecimalPlace", SqlDbType.Int);
            sprmparam.Value = currencyinfo.NoOfDecimalPlace;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@defaultt", SqlDbType.Bit);
            sprmparam.Value = currencyinfo.Defaultt;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = currencyinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = currencyinfo.Extra2;
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
    public DataTable CurrencyViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("CurrencyViewAll", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaadapter.Fill(dtbl);
            // to avoid invalid currencies
           
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
    
    public DataTable CurrencyViewAllByCondition( string strstartText)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("CurrencyViewAllByCondition", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
           
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
            sprmparam.Value = strstartText;
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
    public DataTable CurrencyViewAllofBranch()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("CurrencyViewAllForBranch", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value =  PublicVariables._branchId;
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
    public CurrencyInfo CurrencyView(string currencyId )
    {
        CurrencyInfo currencyinfo =new CurrencyInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sprmparam.Value = currencyId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                currencyinfo.CurrencyId= sdrreader[0].ToString();
                currencyinfo.CurrencySymbol= sdrreader[1].ToString();
                currencyinfo.CurrencyName= sdrreader[2].ToString();
                currencyinfo.SubunitName= sdrreader[3].ToString();
                currencyinfo.NoOfDecimalPlace=int.Parse(sdrreader[4].ToString());
                currencyinfo.Narration= sdrreader[5].ToString();
                currencyinfo.Defaultt=bool.Parse(sdrreader[6].ToString());
                currencyinfo.BranchId= sdrreader[7].ToString();
                currencyinfo.ExtraDate=DateTime.Parse(sdrreader[8].ToString());
                currencyinfo.Extra1= sdrreader[9].ToString();
                currencyinfo.Extra2= sdrreader[10].ToString();
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
        return currencyinfo;
    }
    public CurrencyInfo CurrencyViewBySymbol(string strCSymbol)
    {
        CurrencyInfo currencyinfo = new CurrencyInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyViewBySymbol", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@currencySymbol", SqlDbType.NVarChar);
            sprmparam.Value = strCSymbol;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                currencyinfo.CurrencyId = sdrreader[0].ToString();
                currencyinfo.CurrencySymbol = sdrreader[1].ToString();
                currencyinfo.CurrencyName = sdrreader[2].ToString();
                currencyinfo.SubunitName = sdrreader[3].ToString();
                currencyinfo.NoOfDecimalPlace = int.Parse(sdrreader[4].ToString());
                currencyinfo.Narration = sdrreader[5].ToString();
                currencyinfo.Defaultt = bool.Parse(sdrreader[6].ToString());
                currencyinfo.BranchId = sdrreader[7].ToString();
                currencyinfo.ExtraDate = DateTime.Parse(sdrreader[8].ToString());
                currencyinfo.Extra1 = sdrreader[9].ToString();
                currencyinfo.Extra2 = sdrreader[10].ToString();
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
        return currencyinfo;
    }
    public void CurrencyDelete(string CurrencyId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sprmparam.Value = CurrencyId;
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
    public int CurrencyGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyMax", sqlcon);
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
    
    public bool  CurrencyCheckReference(string CurrencyId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyCheckReference", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sprmparam.Value = CurrencyId;
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

    public bool CurrencyCheckExistanceOfSymbol(string strCurrencySymbol)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyCheckExistanceOfSymbol", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sccmd.Parameters.Add("@currencySymbol", SqlDbType.NVarChar);
            prm.Value = strCurrencySymbol;
            object obj = sccmd.ExecuteScalar();
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
    public bool CurrencyCheckExistanceOfName(string strCurrencyName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CurrencyCheckExistanceOfName", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sccmd.Parameters.Add("@currencyName", SqlDbType.NVarChar);
            prm.Value = strCurrencyName;
            object obj = sccmd.ExecuteScalar();
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
}
}
