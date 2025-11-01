using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for ServiceSP    
//</summary>    
namespace FinacPOS  
{    
class ServiceSP:DBConnection    
{    
    public string ServiceAdd(ServiceInfo serviceinfo)
    {
        string strId = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@serviceName", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.ServiceName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = serviceinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
            sprmparam.Value = serviceinfo.Rate;
            strId = sccmd.ExecuteScalar().ToString();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        finally
        {
            sqlcon.Close();
        }
        return strId;
    }
    public void ServiceEdit(ServiceInfo serviceinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@serviceId", SqlDbType.VarChar);
            sprmparam.Value = serviceinfo.ServiceId;
            sprmparam = sccmd.Parameters.Add("@serviceName", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.ServiceName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = serviceinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = serviceinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
            sprmparam.Value = serviceinfo.Rate;
            sccmd.ExecuteNonQuery();        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        finally
        {
            sqlcon.Close();
        }

    }
    public DataTable ServiceViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ServiceViewAll", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaadapter.Fill(dtbl);

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public ServiceInfo ServiceView(string serviceId )
    {
        ServiceInfo serviceinfo =new ServiceInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@serviceId", SqlDbType.VarChar);
            sprmparam.Value = serviceId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                serviceinfo.ServiceId= sdrreader[0].ToString();
                serviceinfo.ServiceName= sdrreader[1].ToString();
                serviceinfo.Narration= sdrreader[2].ToString();
                serviceinfo.BranchId= sdrreader[3].ToString();
                serviceinfo.ExtraDate=DateTime.Parse(sdrreader[4].ToString());
                serviceinfo.Extra1= sdrreader[5].ToString();
                serviceinfo.Extra2= sdrreader[6].ToString();
                serviceinfo.Rate = decimal.Parse(sdrreader["rate"].ToString());
            }
            sdrreader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        finally
        {
            sqlcon.Close();
        }
        return serviceinfo;
    }
    public ServiceInfo ServiceViewByName(string serviceName)
    {
        ServiceInfo serviceinfo = new ServiceInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceViewByName", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@serviceName", SqlDbType.VarChar);
            sprmparam.Value = serviceName;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId ;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                serviceinfo.ServiceId = sdrreader[0].ToString();
                serviceinfo.ServiceName = sdrreader[1].ToString();
                serviceinfo.Narration = sdrreader[2].ToString();
                serviceinfo.BranchId = sdrreader[3].ToString();
                serviceinfo.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                serviceinfo.Extra1 = sdrreader[5].ToString();
                serviceinfo.Extra2 = sdrreader[6].ToString();
                serviceinfo.Rate = decimal.Parse(sdrreader["rate"].ToString());
            }
            sdrreader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        finally
        {
            sqlcon.Close();
        }
        return serviceinfo;
    }
    public void ServiceDelete(string ServiceId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@serviceId", SqlDbType.VarChar);
            sprmparam.Value = ServiceId;
            sccmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        finally
        {
            sqlcon.Close();
        }
    }
    public int ServiceGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceMax", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            max = int.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        finally
        {
            sqlcon.Close();
        }
        return max;
    }
    public bool ServiceExistance(string ServiceName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceExistance", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@serviceName", SqlDbType.VarChar);
            sprmparam.Value = ServiceName;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId ;
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
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
    public DataTable ServiceViewAllForSearch(string str, string strBranchId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ServiceViewAllForSearch", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
            sprmparam.Value = str;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = strBranchId;
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
    public bool ServiceReferenceCheck(string strServiceId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ServiceReferenceCheck", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@serviceId", SqlDbType.VarChar);
            sprmparam.Value = strServiceId;
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
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


    public DataTable ServiceViewAllForFill(string str, string strBranchId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ServiceViewAllForFill", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
            sprmparam.Value = str;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = strBranchId;
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
