using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class DBClass : DBConnection
    {
        public DataTable GetDataTable(string strQuery)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            da = new SqlDataAdapter(strQuery,sqlcon);
            da.Fill(dt);
            return dt;
        }

        public int ExecuteNonQuery(string strQuery)
        {
            int returnValue = 0;

            SqlCommand cmd = new SqlCommand();
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            cmd.Connection = sqlcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            returnValue = cmd.ExecuteNonQuery();

            cmd.Dispose();

            return returnValue;
   
        }
    }
}
