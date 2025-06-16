using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinacPOS
{
    class FinancialYearSp : DBConnection
    {
          public DataTable FinancialYearViewallActingYear(bool isLogin)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter;
                if (!isLogin)
                {
                    sdaadapter = new SqlDataAdapter("FinancialYearViewallActingYear", sqlcon);
                }
                else
                {
                    sdaadapter = new SqlDataAdapter("FinancialYearGetActiveOrFirst", sqlcon);
                }
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
