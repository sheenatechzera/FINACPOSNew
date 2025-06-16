using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes; 

namespace FinacPOS
{
    class POSUserSP : DBConnection
    {
        public string POSUserAdd(POSUserInfo userinfo)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = userinfo.UserId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = userinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@EmpName", SqlDbType.VarChar);
                sprmparam.Value = userinfo.EmpName;
                sprmparam = sccmd.Parameters.Add("@userName", SqlDbType.VarChar);
                sprmparam.Value = userinfo.UserName;
                sprmparam = sccmd.Parameters.Add("@password", SqlDbType.VarChar);
                sprmparam.Value = userinfo.Password;
                sprmparam = sccmd.Parameters.Add("@POSUserGroupId", SqlDbType.VarChar);
                sprmparam.Value = userinfo.POSUserGroupId;
                sprmparam = sccmd.Parameters.Add("@POSAdmin", SqlDbType.Bit);
                sprmparam.Value = userinfo.POSAdmin;
                sprmparam = sccmd.Parameters.Add("@MaxLineDiscountPer", SqlDbType.Decimal);
                sprmparam.Value = userinfo.MaxLineDiscountPer;
                sprmparam = sccmd.Parameters.Add("@MaxBillDiscountPer", SqlDbType.Decimal);
                sprmparam.Value = userinfo.MaxBillDiscountPer;
                sprmparam = sccmd.Parameters.Add("@Active", SqlDbType.Bit);
                sprmparam.Value = userinfo.Active;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = userinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = userinfo.Extra2;
                id = sccmd.ExecuteScalar().ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return id;
        }
        public void POSUserEdit(POSUserInfo userinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
            
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = userinfo.UserId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = userinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@EmpName", SqlDbType.VarChar);
                sprmparam.Value = userinfo.EmpName;
                sprmparam = sccmd.Parameters.Add("@userName", SqlDbType.VarChar);
                sprmparam.Value = userinfo.UserName;
                sprmparam = sccmd.Parameters.Add("@password", SqlDbType.VarChar);
                sprmparam.Value = userinfo.Password;
                sprmparam = sccmd.Parameters.Add("@POSUserGroupId", SqlDbType.VarChar);
                sprmparam.Value = userinfo.POSUserGroupId;
                sprmparam = sccmd.Parameters.Add("@POSAdmin", SqlDbType.Bit);
                sprmparam.Value = userinfo.POSAdmin;
                sprmparam = sccmd.Parameters.Add("@MaxLineDiscountPer", SqlDbType.Decimal);
                sprmparam.Value = userinfo.MaxLineDiscountPer;
                sprmparam = sccmd.Parameters.Add("@MaxBillDiscountPer", SqlDbType.Decimal);
                sprmparam.Value = userinfo.MaxBillDiscountPer;
                sprmparam = sccmd.Parameters.Add("@Active", SqlDbType.Bit);
                sprmparam.Value = userinfo.Active;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = userinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = userinfo.Extra2;
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
        public DataTable POSUserViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserViewAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
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
        public POSUserInfo POSUserView(string userId)
        {
            POSUserInfo userinfo = new POSUserInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = userId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    userinfo.UserId = sdrreader["userId"].ToString();
                    userinfo.BranchId = sdrreader["branchId"].ToString();
                    userinfo.EmpName = sdrreader["EmpName"].ToString();
                    userinfo.UserName = sdrreader["userName"].ToString();
                    userinfo.Password = sdrreader["password"].ToString();
                    userinfo.POSUserGroupId = sdrreader["POSUserGroupId"].ToString();
                    userinfo.POSAdmin =bool.Parse( sdrreader["POSAdmin"].ToString());
                    userinfo.MaxLineDiscountPer =decimal.Parse( sdrreader["MaxLineDiscountPer"].ToString());
                    userinfo.MaxBillDiscountPer = decimal.Parse(sdrreader["MaxBillDiscountPer"].ToString());
                    userinfo.Active = bool.Parse(sdrreader["Active"].ToString());
                    userinfo.ExtraDate = DateTime.Parse(sdrreader["extraDate"].ToString());
                    userinfo.Extra1 = sdrreader["extra1"].ToString();
                    userinfo.Extra2 = sdrreader["extra2"].ToString();
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return userinfo;
        }
        public POSUserInfo POSUserViewByName(string userName)
        {
            POSUserInfo userinfo = new POSUserInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserViewByName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userName", SqlDbType.NVarChar);
                sprmparam.Value = userName;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    userinfo.UserId = sdrreader["userId"].ToString();
                    userinfo.BranchId = sdrreader["branchId"].ToString();
                    userinfo.EmpName = sdrreader["EmpName"].ToString();
                    userinfo.UserName = sdrreader["userName"].ToString();
                    userinfo.Password = sdrreader["password"].ToString();
                    userinfo.POSUserGroupId = sdrreader["POSUserGroupId"].ToString();
                    userinfo.POSAdmin = bool.Parse(sdrreader["POSAdmin"].ToString());
                    userinfo.MaxLineDiscountPer = decimal.Parse(sdrreader["MaxLineDiscountPer"].ToString());
                    userinfo.MaxBillDiscountPer = decimal.Parse(sdrreader["MaxBillDiscountPer"].ToString());
                    userinfo.Active = bool.Parse(sdrreader["Active"].ToString());
                    userinfo.ExtraDate = DateTime.Parse(sdrreader["extraDate"].ToString());
                    userinfo.Extra1 = sdrreader["extra1"].ToString();
                    userinfo.Extra2 = sdrreader["extra2"].ToString();
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return userinfo;
        }
        public void POSUserDelete(string UserId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = UserId;
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
        public bool CheckExistanceOfUserName(string strusername)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserCheckExistanceOfName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@userName", SqlDbType.NVarChar);
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
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public bool CheckExistanceOfUserID(string userid)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserCheckExistanceOfUserId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                prm.Value = userid;
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
        public bool CheckExistanceOfUserNamebyuserid(string strusername,string userid)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserCheckExistanceOfNameandUserId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                prm.Value = userid;
                prm = sccmd.Parameters.Add("@userName", SqlDbType.NVarChar);              
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
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public bool POSUserAuthentication(string strusername,string strpwd)
        {
            bool isExist = false;
            int objvalue = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSUserAuthentication", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@Username", SqlDbType.NVarChar);
                prm.Value = strusername;
                prm = sccmd.Parameters.Add("@password", SqlDbType.NVarChar);
                prm.Value = strpwd;
                object obj = sccmd.ExecuteScalar();
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    objvalue = (int)obj;
                    if (objvalue == 1)
                        return true;
                    else
                        return false;
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

    }


}
