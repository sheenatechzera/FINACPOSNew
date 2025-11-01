using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for BrandSP    
//</summary>    
namespace FinacPOS
{    
class BrandSP:DBConnection    
{    
    public string BrandAdd(BrandInfo brandinfo)
    {
        string strId = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BrandAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@brandName", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.BrandName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = brandinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.Extra2;
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
    public void BrandEdit(BrandInfo brandinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BrandEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@brandId", SqlDbType.VarChar);
            sprmparam.Value = brandinfo.BrandId;
            sprmparam = sccmd.Parameters.Add("@brandName", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.BrandName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = brandinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = brandinfo.Extra2;
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
    public DataTable BrandViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BrandViewAll", sqlcon);
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
    public DataTable BrandViewAllForSearch(string str)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BrandViewAllForSearch", sqlcon);
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
    public BrandInfo BrandView(string brandId )
    {
        BrandInfo brandinfo =new BrandInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BrandView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@brandId", SqlDbType.VarChar);
            sprmparam.Value = brandId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                brandinfo.BrandId= sdrreader[0].ToString();
                brandinfo.BrandName= sdrreader[1].ToString();
                brandinfo.Narration= sdrreader[2].ToString();
                brandinfo.BranchId= sdrreader[3].ToString();
                brandinfo.ExtraDate=DateTime.Parse(sdrreader[4].ToString());
                brandinfo.Extra1= sdrreader[5].ToString();
                brandinfo.Extra2= sdrreader[6].ToString();
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
        return brandinfo;
    }
    public void BrandDelete(string BrandId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BrandDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@brandId", SqlDbType.VarChar);
            sprmparam.Value = BrandId;
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
    public int BrandGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BrandMax", sqlcon);
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
    public bool BrandExistance(string brandName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BrandExistance", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@brandName", SqlDbType.VarChar);
            sprmparam.Value = brandName;
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
    public bool BrandReferenceCheck(string strBrandId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BrandReferenceCheck", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@brandId", SqlDbType.VarChar);
            sprmparam.Value = strBrandId;
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
