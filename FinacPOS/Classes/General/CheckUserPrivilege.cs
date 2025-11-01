using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using System.Data.SqlClient;

// To check whether a user have privilage to use forms
namespace FinacPOS
{
    class CheckUserPrivilege : DBConnection
    {
        //public bool CheckPrivilage(string strItemName,string  strOperation)
        //{
        //    bool isPrivilaged = false;
        //    UserInfo  userinfo = new  UserInfo ();
        //    UserSP  usersp = new  UserSP ();
        //    if (PublicVariables._currentUserId == "1")
        //    {
        //        // user "admin " have full privilage
        //        isPrivilaged = true;
        //    }
        //    else
        //    {
        //        // Check privilege for add or edit or delete
        //        DataTable dtbl = new DataTable();
        //        if (sqlcon.State == ConnectionState.Closed)
        //        {
        //            sqlcon.Open();
        //        }
        //        SqlDataAdapter sdaadapter = new SqlDataAdapter("PrivilegeViewAllByUserID", sqlcon);
        //        sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        SqlParameter prm = new SqlParameter();
        //        prm = sdaadapter.SelectCommand.Parameters.Add("@userId", SqlDbType.VarChar);
        //        prm.Value = PublicVariables._currentUserId;
        //        prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
        //        prm.Value =  PublicVariables._branchId;
        //        sdaadapter.Fill(dtbl);
        //        sqlcon.Close();
        //        DataRow[] dr = dtbl.Select("formName='" + strItemName + "'");
        //        if (dr.Length > 0)
        //        {
        //            // 
        //            if (strOperation != "")
        //            {
        //                // Checking privilege for each items
        //                string strPrivilegeId = dr[0]["privilegeId"].ToString();
        //                if (sqlcon.State == ConnectionState.Closed)
        //                {
        //                    sqlcon.Open();
        //                }
        //                PrivilegeDetailsInfo InfoPrivilege = new PrivilegeDetailsInfo();
        //                SqlCommand sccmd = new  SqlCommand ("PrivilegeDetailsViewByprivilegeId", sqlcon);
        //                sccmd.CommandType = CommandType.StoredProcedure;
        //                prm = new SqlParameter();
        //                prm = sccmd.Parameters.Add("@privilegeId", SqlDbType.VarChar);
        //                prm.Value = strPrivilegeId;
        //                SqlDataReader sdrreader = sccmd.ExecuteReader();
        //                while (sdrreader.Read())
        //                {
        //                    InfoPrivilege.PrivilegeDetailsId = sdrreader[0].ToString();
        //                    InfoPrivilege.PrivilegeId = sdrreader[1].ToString();
        //                    InfoPrivilege.AddBit = bool.Parse(sdrreader[2].ToString());
        //                    InfoPrivilege.EditBit = bool.Parse(sdrreader[3].ToString());
        //                    InfoPrivilege.DeleteBit = bool.Parse(sdrreader[4].ToString());
        //                    InfoPrivilege.ExtraDate = DateTime.Parse(sdrreader[5].ToString());
        //                    InfoPrivilege.Extra1 = sdrreader[6].ToString();
        //                    InfoPrivilege.Extra2 = sdrreader[7].ToString();
        //                }
        //                sdrreader.Close();
        //                sqlcon.Close();
        //                if (strOperation == "Add")
        //                {
        //                    if (InfoPrivilege.AddBit == true)
        //                    {
        //                        isPrivilaged = true;
        //                    }
        //                    else
        //                    {
        //                        isPrivilaged = false;
        //                    }
        //                }
        //                else if (strOperation == "Edit")
        //                {
        //                    if (InfoPrivilege.EditBit == true)
        //                    {
        //                        isPrivilaged = true;
        //                    }
        //                    else
        //                    {
        //                        isPrivilaged = false;
        //                    }
        //                }
        //                else if (strOperation == "Delete")
        //                {
        //                    if (InfoPrivilege.DeleteBit == true)
        //                    {
        //                        isPrivilaged = true;
        //                    }
        //                    else
        //                    {
        //                        isPrivilaged = false;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // Privilege for master form
        //                isPrivilaged = true;
        //            }
        //        }
        //        else
        //        {
        //            isPrivilaged = false;
        //        }
        //    }
        //    return isPrivilaged;
        //}
    }
}
