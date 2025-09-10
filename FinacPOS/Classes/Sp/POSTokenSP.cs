using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.Helpers;
using FinacPOS.Classes.Info;

namespace FinacPOS
{
    class POSTokenSP : DBConnection
    {

        public string POSTokenNoExist(DateTime SessionDate)
        {
            string tokenNo = null;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                using (SqlCommand sccmd = new SqlCommand("POSTokenExist", sqlcon))
                {
                    sccmd.CommandType = CommandType.StoredProcedure;
                    sccmd.Parameters.Add("@SessionDate", SqlDbType.DateTime).Value = SessionDate;

                    object result = sccmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        tokenNo = result.ToString();
                    }
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

            return tokenNo;
        }

        public string POSTokenAdd(POSTokenInfo infoToken)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("POSTokenAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@SessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoToken.SessionDate;
                sprmparam = sccmd.Parameters.Add("@LastTokenNo", SqlDbType.NChar);
                sprmparam.Value = infoToken.LastTokenNo;
                sprmparam = sccmd.Parameters.Add("@Active", SqlDbType.Bit);
                sprmparam.Value = infoToken.Active;
                id = sccmd.ExecuteNonQuery().ToString();

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

        public void POSTokenNoUpdate(DateTime SessionDate,string LastTokenNo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSTokenUpdate", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = SessionDate;
                sprmparam = sccmd.Parameters.Add("@LastTokenNo", SqlDbType.NChar);
                sprmparam.Value = LastTokenNo;
                sccmd.ExecuteScalar();
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

        public DataTable GetPOSLastTokenNo(DateTime SessionDate)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSTokenViewWithSessionDate", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sqlparameter.Value = SessionDate;
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
