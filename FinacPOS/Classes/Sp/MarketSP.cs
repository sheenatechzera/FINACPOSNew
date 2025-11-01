using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for MarketSP    
//</summary>    
namespace FinacPOS    
{    
class MarketSP:DBConnection    
{    
    public string  MarketAdd(MarketInfo marketinfo)
    {
        string strMarket = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("MarketAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
            sprmparam.Value = marketinfo.AreaId;
            sprmparam = sccmd.Parameters.Add("@marketName", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.MarketName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.Extra2;
           strMarket= sccmd.ExecuteScalar().ToString();

        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return strMarket;
    }
    public void MarketEdit(MarketInfo marketinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("MarketEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
            sprmparam.Value = marketinfo.MarketId;
            sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
            sprmparam.Value = marketinfo.AreaId;
            sprmparam = sccmd.Parameters.Add("@marketName", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.MarketName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = marketinfo.Extra2;
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

    public DataTable MarketViewAllByArea(string strAreaId, bool isFromReport)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter();
            if (isFromReport)
            {
                sdaadapter = new SqlDataAdapter("MarketViewAllByAreaExceptNA", sqlcon);
            }
            else
            {
                 sdaadapter = new SqlDataAdapter("MarketViewAllByArea", sqlcon);
            }
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@areaId", SqlDbType.VarChar);
            prm.Value = strAreaId;
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

    public DataTable MarketViewAllByCondition(string strStartText)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("MarketViewAllByCondition", sqlcon);
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
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public DataTable MarketViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("MarketViewAll", sqlcon);
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
    public MarketInfo MarketView(string marketId )
    {
        MarketInfo marketinfo =new MarketInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("MarketView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
            sprmparam.Value = marketId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                marketinfo.MarketId= sdrreader[0].ToString();
                marketinfo.AreaId= sdrreader[1].ToString();
                marketinfo.MarketName= sdrreader[2].ToString();
                marketinfo.Narration= sdrreader[3].ToString();
                marketinfo.ExtraDate=DateTime.Parse(sdrreader[4].ToString());
                marketinfo.Extra1= sdrreader[5].ToString();
                marketinfo.Extra2= sdrreader[6].ToString();
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
        return marketinfo;
    }
    public void MarketDelete(string MarketId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("MarketDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
            sprmparam.Value = MarketId;
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
    public int MarketGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("MarketMax", sqlcon);
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
    public bool MarketCheckExistanceOfName(string strMarketName)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("MarketCheckExistanceOfName", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sccmd.Parameters.Add("@marketName", SqlDbType.VarChar);
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
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
    public bool MarketCheckReferenceInAccountLedger(string strMarketId)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("MarketCheckReferenceInAccountLedger", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
            prm.Value = strMarketId;
            object obj = sccmd.ExecuteScalar();
             
            if (obj == null || obj.ToString()=="")
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

    public DataTable MarketViewAllWithNA()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("MarketViewAllWithNA", sqlcon);
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
}
}
