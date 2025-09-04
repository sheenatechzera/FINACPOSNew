using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.CodeParser;
using DevExpress.Utils.Win.Hook;
using System.Windows.Forms;
using System.Data.Common;

namespace FinacPOS
{
    class SessionCloseReprintSP : DBConnection
    {
        public DataTable GetPOSCounters()
        {
            DataTable dt = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("GetPOSCounters", sqlcon);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetPOSCounters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return dt;
        }
        public DataTable GetSessionNoByDateAndCounter(DateTime SelectedDate,String SeletedCounter )
        {
            DataTable dt = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter("GetSessionIDByDateAndCounter", sqlcon);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = da.SelectCommand.Parameters.Add("@SelectedDate", SqlDbType.DateTime);
                prm.Value = SelectedDate;
                prm = da.SelectCommand.Parameters.Add("@SelectedCounter", SqlDbType.VarChar);
                prm.Value = SeletedCounter;
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetPOSCounters", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return dt;
        }
    }


}



