using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace FinacPOS
{
    class UserGroupSettingsSP:DBConnection
    {
        public string UserGroupSettingsAdd(UserGroupSettingsInfo usergroupinfo)
        {
            string strId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UserGroupSettingsAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userGroupId", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.UserGroupId;
                sprmparam = sccmd.Parameters.Add("@settingsId", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.SettingsId;
                sprmparam = sccmd.Parameters.Add("@formName", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.FormName;
                sprmparam = sccmd.Parameters.Add("@addBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.AddBit;
                sprmparam = sccmd.Parameters.Add("@editBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.EditBit;
                sprmparam = sccmd.Parameters.Add("@deleteBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.DeleteBit;
                sprmparam = sccmd.Parameters.Add("@postBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.PostBit;               
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@viewBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.ViewBit;
              strId=  sccmd.ExecuteNonQuery().ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return strId;
        }
        public void UserGroupSettingsEdit(UserGroupSettingsInfo usergroupinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UserGroupSettingsEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userGroupSettingsId", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.UserGroupSettingId;
                sprmparam = sccmd.Parameters.Add("@userGroupId", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.UserGroupId;
                sprmparam = sccmd.Parameters.Add("@settingsId", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.SettingsId;
                sprmparam = sccmd.Parameters.Add("@formName", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.FormName;
                sprmparam = sccmd.Parameters.Add("@addBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.AddBit;
                sprmparam = sccmd.Parameters.Add("@editBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.EditBit;
                sprmparam = sccmd.Parameters.Add("@deleteBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.DeleteBit;
                sprmparam = sccmd.Parameters.Add("@postBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.PostBit;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = usergroupinfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@viewBit", SqlDbType.Bit);
                sprmparam.Value = usergroupinfo.ViewBit;
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
        public void UserGroupSettingsDelete(string usergroupId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UserGroupSettingsDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@usergroupId", SqlDbType.VarChar);
                sprmparam.Value = usergroupId;
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
        public DataTable UserGroupViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("UserGroupViewAll", sqlcon);
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
        public DataTable UserGRoupSettingsViewById(string userGroupSettingId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("UserGRoupSettingsViewById", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@userGroupSettingId", SqlDbType.VarChar);
                prm.Value = userGroupSettingId;
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
        public DataTable UserGRoupSettingsViewByuserGroupId(string userGroupId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("UserGRoupSettingsViewByuserGroupId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@userGroupId", SqlDbType.VarChar);
                prm.Value = userGroupId;
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
        public DataTable UserGroupSettingGetMenu( )
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("UserGroupSettingGetMenu", sqlcon);
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
        public DataTable UserGroupSettingGetAllformName(string userGroupId, string SaveOrEdit)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("UserGroupSettingGetAllformName", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@userGroupId", SqlDbType.VarChar);
                prm.Value = userGroupId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@SaveOrEdit", SqlDbType.VarChar);
                prm.Value = SaveOrEdit;
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
        public DataTable MenuSettingsViewAllForUserSettings()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("MenuSettingsViewAllForUserSettings", sqlcon);
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
        //public ArrayList GetPrivilageData()
        //{
        //    ArrayList myArrayList = new ArrayList();
        //    try
        //    {
        //        DataTable dtbl = new DataTable();
        //        if (sqlcon.State == ConnectionState.Closed)
        //        {
        //            sqlcon.Open();
        //        }
        //        SqlDataAdapter sdaadapter = new SqlDataAdapter("UserGroupGetPrivilageByUserGroupId", sqlcon);
        //        sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        SqlParameter prm = new SqlParameter();
        //        prm = sdaadapter.SelectCommand.Parameters.Add("@userGroupId", SqlDbType.VarChar);
        //        prm.Value = PublicVariables._userGroupId;
        //        prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
        //        prm.Value = PublicVariables._branchId;
        //        sdaadapter.Fill(dtbl);
        //        myArrayList.Add(dtbl);
        //        //foreach (DataRow dtRow in dtbl.Rows)
        //        //{
        //        //    myArrayList.Add(dtRow);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    finally
        //    {
        //        sqlcon.Close();
        //    }
        //    return myArrayList;
        //}
        public bool CheckUSerGroupPrivilage(string strItemName, string strOperation, string strMainmenu)
        {
            bool isPrivilaged = false, addBit = false, deletebit = false, editbit = false, postbit = false,viewBit=false;
            UserInfo userinfo = new UserInfo();
            UserSP usersp = new UserSP();
            if (PublicVariables._currentUserId == "1")
            {
                // user "admin " have full privilage
                isPrivilaged = true;
            }
            else
            {
                // Check privilege for add or edit or delete   
                DataTable dtbl = (DataTable)PublicVariables._arrayPrivilage[0];
                DataRow[] dr = dtbl.Select("formName='" + strItemName + "' AND windowmenu='" + strMainmenu + "'");
                if (dr.Length > 0)
                {
                    // 
                    if (strOperation != "")
                    {
                        // Checking privilege for each items

                        addBit = bool.Parse(dr[0]["addBit"].ToString());
                        editbit = bool.Parse(dr[0]["editBit"].ToString());
                        deletebit = bool.Parse(dr[0]["deleteBit"].ToString());
                        postbit = bool.Parse(dr[0]["postBit"].ToString());
                        viewBit = bool.Parse(dr[0]["viewBit"].ToString());

                        if (strOperation == "Add")
                        {
                            if (addBit == true)
                            {
                                isPrivilaged = true;
                            }
                            else
                            {
                                isPrivilaged = false;
                            }
                        }
                        else if (strOperation == "Edit")
                        {
                            if (editbit == true)
                            {
                                isPrivilaged = true;
                            }
                            else
                            {
                                isPrivilaged = false;
                            }
                        }
                        else if (strOperation == "Delete")
                        {
                            if (deletebit == true)
                            {
                                isPrivilaged = true;
                            }
                            else
                            {
                                isPrivilaged = false;
                            }
                        }
                        else if (strOperation == "Post")
                        {
                            if (postbit == true)
                            {
                                isPrivilaged = true;
                            }
                            else
                            {
                                isPrivilaged = false;
                            }
                        }
                        else if (strOperation == "View")
                        {
                            if (viewBit == true)
                            {
                                isPrivilaged = true;
                            }
                            else
                            {
                                isPrivilaged = false;
                            }
                        }
                    }
                    else
                    {
                        // Privilege for master form
                        isPrivilaged = true;
                    }
                }
                else
                {
                    isPrivilaged = false;
                }
            }
            return isPrivilaged;
        }
        public bool CheckUSerGroupPrivilageForView(string strItemName, string strOperation, string strMainmenu)
        {
            bool isPrivilaged = false, addBit = false, deletebit = false, editbit = false, postbit = false, viewBit = false;
            UserInfo userinfo = new UserInfo();
            UserSP usersp = new UserSP();
            if (PublicVariables._currentUserId == "1")
            {
                // user "admin " have full privilage
                isPrivilaged = true;
            }
            else
            {
                // Check privilege for add or edit or delete   
                DataTable dtbl = (DataTable)PublicVariables._arrayPrivilage[0];
                DataRow[] dr = dtbl.Select("formName='" + strItemName + "' AND windowmenu='" + strMainmenu + "'");
                if (dr.Length > 0)
                {
                    // 
                    if (strOperation != "")
                    {
                        // Checking privilege for each items

                        addBit = bool.Parse(dr[0]["addBit"].ToString());
                        editbit = bool.Parse(dr[0]["editBit"].ToString());
                        deletebit = bool.Parse(dr[0]["deleteBit"].ToString());
                        postbit = bool.Parse(dr[0]["postBit"].ToString());
                        viewBit = bool.Parse(dr[0]["viewBit"].ToString());

                        if (editbit == true || deletebit == true || postbit == true || viewBit == true)
                        {
                            isPrivilaged = true;
                        }
                        else
                            isPrivilaged = false;
                       
                    }
                    else
                    {
                        // Privilege for master form
                        isPrivilaged = true;
                    }
                }
                else
                {
                    isPrivilaged = false;
                }
            }
            return isPrivilaged;
        }
        public bool CheckUSerGroupPrivilageForView(string strItemName, string strOperation)
        {
            bool isPrivilaged = false, addBit = false, deletebit = false, editbit = false, postbit = false, viewBit = false;
            UserInfo userinfo = new UserInfo();
            UserSP usersp = new UserSP();
            if (PublicVariables._currentUserId == "1")
            {
                // user "admin " have full privilage
                isPrivilaged = true;
            }
            else
            {
                // Check privilege for add or edit or delete   
                DataTable dtbl = (DataTable)PublicVariables._arrayPrivilage[0];
                DataRow[] dr = dtbl.Select("formName='" + strItemName + "' ");
                if (dr.Length > 0)
                {
                    // 
                    if (strOperation != "")
                    {
                        // Checking privilege for each items

                        addBit = bool.Parse(dr[0]["addBit"].ToString());
                        editbit = bool.Parse(dr[0]["editBit"].ToString());
                        deletebit = bool.Parse(dr[0]["deleteBit"].ToString());
                        postbit = bool.Parse(dr[0]["postBit"].ToString());
                        viewBit = bool.Parse(dr[0]["viewBit"].ToString());

                        if (editbit == true || deletebit == true || postbit == true || viewBit == true)
                        {
                            isPrivilaged = true;
                        }
                        else
                            isPrivilaged = false;

                    }
                    else
                    {
                        // Privilege for master form
                        isPrivilaged = true;
                    }
                }
                else
                {
                    isPrivilaged = false;
                }
            }
            return isPrivilaged;
        }
    }
}
