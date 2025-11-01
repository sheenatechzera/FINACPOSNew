using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for RouteSP    
//</summary>    
namespace FinacPOS    
{    
class RouteSP:DBConnection    
{    
    public string RouteAdd(RouteInfo routeinfo)
    {
        string strRoute = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("RouteAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
            sprmparam.Value = routeinfo.MarketId;
            sprmparam = sccmd.Parameters.Add("@routeName", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.RouteName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.Extra2;
            strRoute = sccmd.ExecuteScalar().ToString();

        }
        catch (Exception ex)
        {
             MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return strRoute;
    }
    public void RouteEdit(RouteInfo routeinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("RouteEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@routeId", SqlDbType.VarChar);
            sprmparam.Value = routeinfo.RouteId;
            sprmparam = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
            sprmparam.Value = routeinfo.MarketId;
            sprmparam = sccmd.Parameters.Add("@routeName", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.RouteName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = routeinfo.Extra2;
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
    public DataTable RouteViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("RouteViewAll", sqlcon);
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
    public RouteInfo RouteView(string routeId )
    {
        RouteInfo routeinfo =new RouteInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("RouteView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@routeId", SqlDbType.VarChar);
            sprmparam.Value = routeId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                routeinfo.RouteId= sdrreader[0].ToString();
                routeinfo.MarketId= sdrreader[1].ToString();
                routeinfo.RouteName= sdrreader[2].ToString();
                routeinfo.Narration= sdrreader[3].ToString();
                routeinfo.ExtraDate=DateTime.Parse(sdrreader[4].ToString());
                routeinfo.Extra1= sdrreader[5].ToString();
                routeinfo.Extra2= sdrreader[6].ToString();
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
        return routeinfo;
    }
    public void RouteDelete(string RouteId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("RouteDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@routeId", SqlDbType.VarChar);
            sprmparam.Value = RouteId;
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
    public int RouteGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("RouteMax", sqlcon);
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
    public DataTable RouteViewAllByCondition(string strStartText)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("RouteViewAllByCondition", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
            prm.Value = strStartText;
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

    public bool RouteCheckExistanceOfName(string strMarketName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("RouteCheckExistanceOfName", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sccmd.Parameters.Add("@routeName", SqlDbType.VarChar);
            prm.Value = strMarketName;
            prm = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
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
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
    public bool RouteCheckReferenceInEmployee(string routeId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("RouteCheckReferenceInEmployee", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            sccmd.Parameters.Add("@routeId", SqlDbType.VarChar).Value = routeId;
            object obj = sccmd.ExecuteScalar();
            if (obj != null)
                isExist = true;
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
    public DataTable RouteViewAllWithNA()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("RouteViewAllWithNA", sqlcon);
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
    public DataTable RouteViewAllByMarket(string marketId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("RouteViewAllByMarket", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@marketId", SqlDbType.VarChar);
            prm.Value = marketId;
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
    public DataTable RouteViewAllByMarketWithoutNA(string marketId, string areaId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("RouteViewAllByMarketWithoutNA", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@marketId", SqlDbType.VarChar);
            prm.Value = marketId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@areaId", SqlDbType.VarChar);
            prm.Value = areaId;
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
