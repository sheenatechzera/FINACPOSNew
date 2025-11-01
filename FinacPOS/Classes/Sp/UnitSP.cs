using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for UnitSP    
//</summary>    
namespace FinacPOS  
{    
class UnitSP:DBConnection    
{    
    public string UnitAdd(UnitInfo unitinfo)
    {
        string strId = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitName", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.UnitName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = unitinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.Extra2;
            strId = sccmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return strId;
    }
    public void UnitEdit(UnitInfo unitinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = unitinfo.UnitId;
            sprmparam = sccmd.Parameters.Add("@unitName", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.UnitName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = unitinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = unitinfo.Extra2;
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
    public DataTable UnitViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("UnitViewAll", sqlcon);
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
    public DataTable UnitViewAllForSearch(string str)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("UnitViewAllForSearch", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
            sprmparam.Value = str;
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
    public UnitInfo UnitView(string unitId )
    {
        UnitInfo unitinfo =new UnitInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = unitId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                unitinfo.UnitId= sdrreader[0].ToString();
                unitinfo.UnitName= sdrreader[1].ToString();
                unitinfo.Narration= sdrreader[2].ToString();
                unitinfo.BranchId= sdrreader[3].ToString();
                unitinfo.ExtraDate=DateTime.Parse(sdrreader[4].ToString());
                unitinfo.Extra1= sdrreader[5].ToString();
                unitinfo.Extra2= sdrreader[6].ToString();
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
        return unitinfo;
    }
    public void UnitDelete(string UnitId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = UnitId;
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
    public int UnitGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitMax", sqlcon);
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
    public bool UnitExistance(string unitName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitExistance", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitName", SqlDbType.VarChar);
            sprmparam.Value = unitName;
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
    public bool UnitReferenceCheck(string strUnitId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitReferenceCheck", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = strUnitId;
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
