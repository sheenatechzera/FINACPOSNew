using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DevExpress.Utils.About;


namespace FinacPOS
{
    class SessionManagementSP : DBConnection
    {
        DBClass DBClass = new DBClass();

        public string SessionManagementAdd(SessionManagementInfo sessioninfo)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDateTime sqldatenull;
                sqldatenull = SqlDateTime.Null;  
                SqlCommand sccmd = new SqlCommand("SessionManagementAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.SessionNo;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = sessioninfo.SessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.CounterId;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.UserId;
                sprmparam = sccmd.Parameters.Add("@openingBalance", SqlDbType.Decimal);
                sprmparam.Value = sessioninfo.OpeningBalance;
                sprmparam = sccmd.Parameters.Add("@sessionStatus", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.SessionStatus;
                sprmparam = sccmd.Parameters.Add("@sessionCloseTime", SqlDbType.DateTime);
                sprmparam.Value = sqldatenull;
             
                sprmparam = sccmd.Parameters.Add("@createdUserId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.CreatedUserId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.Extra2;
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
        public void SessionManagementEdit(SessionManagementInfo sessioninfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDateTime sqldatenull;
                sqldatenull = SqlDateTime.Null;  
                SqlCommand sccmd = new SqlCommand("SessionManagementEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();

                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.SessionNo;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = sessioninfo.SessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.CounterId;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.UserId;
                sprmparam = sccmd.Parameters.Add("@openingBalance", SqlDbType.Decimal);
                sprmparam.Value = sessioninfo.OpeningBalance;
                sprmparam = sccmd.Parameters.Add("@sessionStatus", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.SessionStatus;
                sprmparam = sccmd.Parameters.Add("@sessionCloseTime", SqlDbType.DateTime);
                sprmparam.Value = sqldatenull;
                sprmparam = sccmd.Parameters.Add("@createdDate", SqlDbType.DateTime);
                sprmparam.Value = DateTime.Now;
                sprmparam = sccmd.Parameters.Add("@createdUserId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.CreatedUserId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = sessioninfo.Extra2;             
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
        public DataTable SessionManagementViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("SessionManagementViewAll", sqlcon);
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
        public SessionManagementInfo SessionManagementViewBysessionNo(string sessionNo)
        {
            SessionManagementInfo sessioninfo = new SessionManagementInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SessionManagementViewBySessionNo", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = sessionNo;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    sessioninfo.SessionNo = sdrreader["sessionNo"].ToString();
                    sessioninfo.SessionDate = DateTime.Parse( sdrreader["sessionDate"].ToString());
                    sessioninfo.CounterId = sdrreader["counterId"].ToString();
                    sessioninfo.UserId = sdrreader["userId"].ToString();
                    sessioninfo.OpeningBalance =decimal.Parse( sdrreader["openingBalance"].ToString());
                    sessioninfo.SessionStatus = sdrreader["sessionStatus"].ToString();
                    sessioninfo.SessionCloseTime = DateTime.Parse(sdrreader["sessionCloseTime"].ToString());
                    sessioninfo.CreatedDate = DateTime.Parse(sdrreader["createdDate"].ToString());
                    sessioninfo.CreatedUserId = sdrreader["createdUserId"].ToString();
                    sessioninfo.BranchId =sdrreader["branchId"].ToString();
                    sessioninfo.ExtraDate = DateTime.Parse(sdrreader["extraDate"].ToString());
                    sessioninfo.Extra1 = sdrreader["extra1"].ToString();
                    sessioninfo.Extra2 = sdrreader["extra2"].ToString();
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
            return sessioninfo;
        }
        public int SessionManagementGetMaxByDateandCounterId(DateTime sessionDate)
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SessionManagementGetByDateandCounterId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = sessionDate.Date;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._counterId;
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
        public DataTable POSCounterGetActiveCounter()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCounterGetActiveCounter", sqlcon);
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
        public void SessionManagementDelete(string sessionNo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SessionManagementDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = sessionNo;
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
        public DataTable GetActiveSession(string strUserId, string strCounterId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("SessionManagementGetActiveSession", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@userId", SqlDbType.VarChar);
                sqlparameter.Value = strUserId;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@counterId", SqlDbType.VarChar);
                sqlparameter.Value = strCounterId;
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
      
        public void UpdateSessionClose(string counterId)
        {
            try
            {
                int returnValue = 0;
                string strQuery = "UPDATE tbl_SessionManagement SET sessionStatus='C',sessionCloseTime=GETDATE() WHERE sessionStatus='O' AND counterId='" + counterId + "'";
                returnValue = DBClass.ExecuteNonQuery(strQuery);

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
        public string CheckAnySessionIsOpenedInCounter(string counterId)
        {
            string strUserId = "";
            try
            {
                DataTable dt = new DataTable();
                string strQuery = "SELECT U.userId + '-' + U.userName AS 'USER' FROM tbl_SessionManagement S,tbl_POSUser U WHERE U.userId=S.userId AND S.sessionStatus='O' AND S.counterId='" + counterId + "'";
                dt = DBClass.GetDataTable(strQuery);
                if (dt.Rows.Count > 0)
                {
                    strUserId = dt.Rows[0]["USER"].ToString(); 
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
            return strUserId;
        }
    

        public List<SessionManagementInfo> GetPOSSessionCloseDetails(DateTime dtSessionDate, string strCounterId, string strUserId, string strSessionNo)
        {
           
         List<SessionManagementInfo> sessionList = new List<SessionManagementInfo>();

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand cmd = new SqlCommand("GetPOSSessionCloseDetails", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@sessionDate", dtSessionDate);
                cmd.Parameters.AddWithValue("@counterId", strCounterId);
                cmd.Parameters.AddWithValue("@userId", strUserId);
                cmd.Parameters.AddWithValue("@sessionNo", strSessionNo);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    SessionManagementInfo sessionInfo = new SessionManagementInfo
                    {
                        Particular = reader["Particular"].ToString(),
                        AMOUNT = reader.GetDecimal(reader.GetOrdinal("AMOUNT"))
                    };

                    sessionList.Add(sessionInfo);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }

            return sessionList;
        }
    }
}
