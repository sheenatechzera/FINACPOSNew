using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for ComponentsSettingsSP    
//</summary>    
namespace FinacPOS    
{    
class ComponentsSettingsSP:DBConnection    
{    
    public void ComponentsSettingsAdd(ComponentsSettingsInfo componentssettingsinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ComponentsSettingsAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
           
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@componentId", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.ComponentId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.ProductDetailsId;
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
    public void ComponentsSettingsEdit(ComponentsSettingsInfo componentssettingsinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ComponentsSettingsEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@componentSettingsId", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.ComponentSettingsId;
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@componentId", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.ComponentId;
            sprmparam = sccmd.Parameters.Add("@extraDate", SqlDbType.DateTime);
            sprmparam.Value = componentssettingsinfo.ExtraDate;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = componentssettingsinfo.ProductDetailsId;
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
    public DataTable ComponentsSettingsViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ComponentsSettingsViewAll", sqlcon);
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
    public ComponentsSettingsInfo ComponentsSettingsView(string componentSettingsId )
    {
        ComponentsSettingsInfo componentssettingsinfo =new ComponentsSettingsInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ComponentsSettingsView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@componentSettingsId", SqlDbType.VarChar);
            sprmparam.Value = componentSettingsId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                componentssettingsinfo.ComponentSettingsId= sdrreader[0].ToString();
                componentssettingsinfo.ProductCode= sdrreader[1].ToString();
                componentssettingsinfo.ComponentId= sdrreader[2].ToString();
                componentssettingsinfo.ExtraDate=DateTime.Parse(sdrreader[3].ToString());
                componentssettingsinfo.Extra1= sdrreader[4].ToString();
                componentssettingsinfo.Extra2= sdrreader[5].ToString();
                componentssettingsinfo.ProductDetailsId = sdrreader[6].ToString();
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
        return componentssettingsinfo;
    }
    public void ComponentsSettingsDelete(string ComponentSettingsId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ComponentsSettingsDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@componentSettingsId", SqlDbType.VarChar);
            sprmparam.Value = ComponentSettingsId;
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
    public void ComponentsSettingsDeleteByproductDetailsId(string strProductDetailsId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ComponentsSettingsDeleteByproductDetailsId", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = strProductDetailsId;
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
    public int ComponentsSettingsGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ComponentsSettingsMax", sqlcon);
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
    public DataTable ComponentViewByProductDetailsId(string strProductDetailsId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ComponentViewByProductDetailsId", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = strProductDetailsId;
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
}
}
