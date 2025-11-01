using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraPrinting.Native.Lines;

//<summary>    
//Summary description for UserSP    
//</summary>    
namespace FinacPOS    
{    
class UserSP:DBConnection    
{    
    public string  UserAdd(UserInfo userinfo)
    {
        string id = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UserAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = userinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@userName", SqlDbType.VarChar);
            sprmparam.Value = userinfo.UserName;
            sprmparam = sccmd.Parameters.Add("@password", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Password;
            sprmparam = sccmd.Parameters.Add("@userGroupId", SqlDbType.VarChar);
            sprmparam.Value = userinfo.UserGroupId;
            sprmparam = sccmd.Parameters.Add("@active", SqlDbType.Bit);
            sprmparam.Value = userinfo.Active;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@DiscountPercLimit", SqlDbType.Decimal);
            sprmparam.Value = userinfo.DiscountPercLimit;
            sprmparam = sccmd.Parameters.Add("@Language", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Language;
                id = sccmd.ExecuteScalar().ToString();

        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return id;
    }
    public void UserEdit(UserInfo userinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UserEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
            sprmparam.Value = userinfo.UserId;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = userinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@userName", SqlDbType.VarChar);
            sprmparam.Value = userinfo.UserName;
            sprmparam = sccmd.Parameters.Add("@password", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Password;
            sprmparam = sccmd.Parameters.Add("@userGroupId", SqlDbType.VarChar);
            sprmparam.Value = userinfo.UserGroupId;
            sprmparam = sccmd.Parameters.Add("@active", SqlDbType.Bit);
            sprmparam.Value = userinfo.Active;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@DiscountPercLimit", SqlDbType.Decimal);
            sprmparam.Value = userinfo.DiscountPercLimit;
            sprmparam = sccmd.Parameters.Add("@Language", SqlDbType.VarChar);
            sprmparam.Value = userinfo.Language;
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
    public DataTable UserViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("UserViewAll", sqlcon);
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
    public UserInfo UserView(string userId )
    {
        UserInfo userinfo =new UserInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UserView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
            sprmparam.Value = userId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                userinfo.UserId= sdrreader[0].ToString();
                userinfo.BranchId= sdrreader[1].ToString();
                userinfo.UserName= sdrreader[2].ToString();
                userinfo.Password= sdrreader[3].ToString();
                userinfo.Active=bool.Parse(sdrreader[4].ToString());
                userinfo.ExtraDate=DateTime.Parse(sdrreader[5].ToString());
                userinfo.Extra1= sdrreader[6].ToString();
                userinfo.Extra2= sdrreader[7].ToString();
                userinfo.UserGroupId = sdrreader[8].ToString();
                userinfo.DiscountPercLimit =decimal.Parse( sdrreader["DiscountPercLimit"].ToString());
                userinfo.Language = sdrreader["Language"].ToString();
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
        return userinfo;
    }
    public UserInfo UserViewByName(string userName)
    {
        UserInfo userinfo = new UserInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UserViewByName", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@userName", SqlDbType.VarChar);
            sprmparam.Value = userName;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                userinfo.UserId = sdrreader[0].ToString();
                userinfo.BranchId = sdrreader[1].ToString();
                userinfo.UserName = sdrreader[2].ToString();
                userinfo.Password = sdrreader[3].ToString();
                userinfo.Active = bool.Parse(sdrreader[4].ToString());
                userinfo.ExtraDate = DateTime.Parse(sdrreader[5].ToString());
                userinfo.Extra1 = sdrreader[6].ToString();
                userinfo.Extra2 = sdrreader[7].ToString();
                userinfo.UserGroupId = sdrreader[8].ToString();
                userinfo.DiscountPercLimit =decimal.Parse( sdrreader["DiscountPercLimit"].ToString());
                userinfo.Language = sdrreader["Language"].ToString();
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
        return userinfo;
    }
    public void UserDelete(string UserId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UserDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
            sprmparam.Value = UserId;
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
    public int UserGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UserMax", sqlcon);
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
    public bool CheckExistanceOfUserName(string strusername)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UserCheckExistanceOfName", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sccmd.Parameters.Add("@userName", SqlDbType.VarChar);
            prm.Value = strusername;
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
