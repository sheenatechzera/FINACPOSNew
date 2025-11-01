using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for AccountGroupSP    
//</summary>    
namespace FinacPOS
{
    class AccountGroupSP : DBConnection
    {
        public string AccountGroupAdd(AccountGroupInfo accountgroupinfo)
        {
            string strId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountGroupAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@accountGroupName", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.AccountGroupName;
                sprmparam = sccmd.Parameters.Add("@groupUnder", SqlDbType.VarChar);
                sprmparam.Value = accountgroupinfo.GroupUnder;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.Narration;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = accountgroupinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@defaultt", SqlDbType.Bit);
                sprmparam.Value = accountgroupinfo.Default;
                sprmparam = sccmd.Parameters.Add("@AccountGroupCode", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.AccountGroupCode;
                sprmparam = sccmd.Parameters.Add("@LedgerNextNo", SqlDbType.Int);
                sprmparam.Value = accountgroupinfo.LedgerNextNo;
                strId = sccmd.ExecuteScalar().ToString();

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
        public void AccountGroupEdit(AccountGroupInfo accountgroupinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountGroupEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = accountgroupinfo.GroupId;
                sprmparam = sccmd.Parameters.Add("@accountGroupName", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.AccountGroupName;
                sprmparam = sccmd.Parameters.Add("@groupUnder", SqlDbType.VarChar);
                sprmparam.Value = accountgroupinfo.GroupUnder;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.Narration;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = accountgroupinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@defaultt", SqlDbType.Bit);
                sprmparam.Value = accountgroupinfo.Default;
                sprmparam = sccmd.Parameters.Add("@AccountGroupCode", SqlDbType.NVarChar);
                sprmparam.Value = accountgroupinfo.AccountGroupCode;
                sprmparam = sccmd.Parameters.Add("@LedgerNextNo", SqlDbType.Int);
                sprmparam.Value = accountgroupinfo.LedgerNextNo;
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
        public DataTable AccountGroupViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountGroupViewAll", sqlcon);
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
        public DataTable AccountGroupViewAllByName(string strStartText)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountGroupViewAllByName", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.NVarChar);
                prm.Value = strStartText;
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
        public AccountGroupInfo AccountGroupView(string groupId)
        {
            AccountGroupInfo accountgroupinfo = new AccountGroupInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountGroupView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = groupId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    accountgroupinfo.GroupId = sdrreader[0].ToString();
                    accountgroupinfo.AccountGroupName = sdrreader[1].ToString();
                    accountgroupinfo.GroupUnder = sdrreader[2].ToString();
                    accountgroupinfo.Narration = sdrreader[3].ToString();
                    accountgroupinfo.BranchId = sdrreader[4].ToString();
                    accountgroupinfo.ExtraDate = DateTime.Parse(sdrreader[5].ToString());
                    accountgroupinfo.Extra1 = sdrreader[6].ToString();
                    accountgroupinfo.Extra2 = sdrreader[7].ToString();
                    accountgroupinfo.Default = bool.Parse(sdrreader[8].ToString());
                    accountgroupinfo.AccountGroupCode = sdrreader["AccountGroupCode"].ToString();
                    accountgroupinfo.LedgerNextNo = Convert.ToInt32(sdrreader["LedgerNextNo"].ToString());
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
            return accountgroupinfo;
        }
        public void AccountGroupDelete(string GroupId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountGroupDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = GroupId;
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
        public int AccountGroupGetMax()
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountGroupMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                max = int.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return max;
        }
        public bool CheckExistanceOfAccountGroup(string strAccountGroupName)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountGroupCheckExistanceOfName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@accountGroupName", SqlDbType.NVarChar);
                prm.Value = strAccountGroupName;
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

        public DataTable AccountLedgerGetGroupUnder(string strGroupId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetGroupUnder", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = strGroupId;
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



        public DataTable ViewBankAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ViewBankAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                //SqlParameter prm = new SqlParameter();
                //prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                //prm.Value = strGroupId;
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

        public DataTable CheckGroupIdUnderSundryDebtorCreditor(string strGroupId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("CheckGroupIdUnderSundryDebtorCreditor", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = strGroupId;
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
        public DataTable AccountGroupForChart(string strGroupId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountGroupForChart", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = strGroupId;
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
        public DataTable AccountGroupGetVendor()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountGroupGetVendor", sqlcon);
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
        public DataTable AccountGroupGetCustomer()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountGroupGetCustomer", sqlcon);
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
    }
}
