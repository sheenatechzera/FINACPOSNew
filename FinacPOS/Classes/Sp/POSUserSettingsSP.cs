using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class POSUserSettingsSP : DBConnection
    {
        public int  POSUserSettingsAdd(POSUserSettingsInfo userInfo)
        {
            int settingsid = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserSettingAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userGroup", SqlDbType.VarChar);
                sprmparam.Value = userInfo.UserGroup;
                sprmparam = sccmd.Parameters.Add("@Menu", SqlDbType.VarChar);
                sprmparam.Value = userInfo.Menu;
                sprmparam = sccmd.Parameters.Add("@subMenu", SqlDbType.VarChar);
                sprmparam.Value = userInfo.SubMenu;
                sprmparam = sccmd.Parameters.Add("@menuStripName", SqlDbType.VarChar);
                sprmparam.Value = userInfo.MenuStripName;
                sprmparam = sccmd.Parameters.Add("@formName", SqlDbType.VarChar);
                sprmparam.Value = userInfo.FormName;
                sprmparam = sccmd.Parameters.Add("@status", SqlDbType.Bit);
                sprmparam.Value = userInfo.Status;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = userInfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = userInfo.Extra2;

                settingsid=sccmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return settingsid;
        }
        public void POSUserSettingsDelete(string userGroup)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserSettingDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userGroup", SqlDbType.VarChar);
                sprmparam.Value = userGroup;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId; 
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
        }
        public DataTable POSUserSettingsViewByUserGroup(string userGroup)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserSettingsViewByUserGroup", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;          
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@userGroup", SqlDbType.VarChar);
                sqlparameter.Value = userGroup;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
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
        public DataTable POSUserSettingsCopyViewAll(string userGroup)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserSettingsViewAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@UserGroup", SqlDbType.VarChar);
                sqlparameter.Value = userGroup;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
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
        public DataTable POSUserGetUserGroupId()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserGetUserGroupId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();           
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
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
        public DataTable POSUserSettingsGetMenuByUSerGroup(string usergroup)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserSettingsGetMenuByUSerGroup", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@userGroup", SqlDbType.VarChar);
                sqlparameter.Value = usergroup;
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
        public DataTable POSUserSettingsGetByMenuAndUserGroup(string Menu, string usergroup)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserSettingsGetByMenuAndUserGroup", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@Menu", SqlDbType.VarChar);
                sqlparameter.Value = Menu;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@userGroup", SqlDbType.VarChar);
                sqlparameter.Value = usergroup;
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
        public DataTable POSUserSettingsGetBysubMenuandUserGroup(string subMenu, string usergroup)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserSettingsGetBysubMenuandUserGroup", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@subMenu", SqlDbType.VarChar);
                sqlparameter.Value = subMenu;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@userGroup", SqlDbType.VarChar);
                sqlparameter.Value = usergroup;
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
