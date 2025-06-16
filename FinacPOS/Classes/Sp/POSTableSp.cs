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
    class POSTableSp : DBConnection
    {
        public int POSTableAdd(POSTableInfo tableInfo)
        {
            int TableId = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSTableAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sprmparam = new SqlParameter();


                sprmparam = sccmd.Parameters.Add("@TableNo", SqlDbType.NVarChar);
                sprmparam.Value = tableInfo.TableNo;

                sprmparam = sccmd.Parameters.Add("@NumberOfSeats", SqlDbType.Int);
                sprmparam.Value = tableInfo.NumberOfSeats;

                sprmparam = sccmd.Parameters.Add("@GroupName", SqlDbType.NVarChar);
                sprmparam.Value = tableInfo.GroupName;

                sprmparam = sccmd.Parameters.Add("@Active", SqlDbType.Bit);
                sprmparam.Value = tableInfo.Active;
                Object objTableId = sccmd.ExecuteScalar();

                if (objTableId != null)
                {
                    TableId = Int32.Parse(objTableId.ToString());
                }
                else
                {
                    TableId = 0;

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
            return TableId;
        }

        public bool TableNoCheckExistence(int TableId, string TableNo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sqlcmd = new SqlCommand("POSTableNoCheckIfExist", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqlcmd.Parameters.Add("@TableId", SqlDbType.Int);
                sprmparam.Value = TableId;
                sprmparam = sqlcmd.Parameters.Add("@TableNo", SqlDbType.VarChar);
                sprmparam.Value = TableNo;
                object obj = sqlcmd.ExecuteScalar();
                int Count = 0;
                if (obj != null)
                {
                    Count = Int32.Parse(obj.ToString());
                }
                if (Count > 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return false;
        }


        public bool POSTableEdit(POSTableInfo tableInfo)
        {
            bool isUpdated = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("POSTableEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sprmparam = new SqlParameter();

                sprmparam = sccmd.Parameters.Add("@TableNo", SqlDbType.NVarChar);
                sprmparam.Value = tableInfo.TableNo;

                sprmparam = sccmd.Parameters.Add("@NumberOfSeats", SqlDbType.Int);
                sprmparam.Value = tableInfo.NumberOfSeats;

                sprmparam = sccmd.Parameters.Add("@GroupName", SqlDbType.NVarChar);
                sprmparam.Value = tableInfo.GroupName;

                sprmparam = sccmd.Parameters.Add("@Active", SqlDbType.Bit);
                sprmparam.Value = tableInfo.Active;

                sprmparam = sccmd.Parameters.Add("@TableId", SqlDbType.Int);
                sprmparam.Value = tableInfo.TableId;

                int AffectedRows = sccmd.ExecuteNonQuery();
                if (AffectedRows > 0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }

            return isUpdated;
        }


        public int POSTableDelete(int TableId)
        {
            int returnValue = 0;

            try
            {

                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }


                SqlCommand sqlcmd = new SqlCommand("POSTableDelete", sqlcon);
                sqlcmd.CommandType = CommandType.StoredProcedure;


                SqlParameter param = sqlcmd.Parameters.Add("@TableId", SqlDbType.Int);
                param.Value = TableId;


                returnValue = sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in POSTableDelete: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();

            }

            return returnValue;
        }

   public POSTableInfo GetPOSTableDetails(int tableId)
   {
      POSTableInfo tableInfo = new POSTableInfo();

    try
    {
        if (sqlcon.State == ConnectionState.Closed)
        {
            sqlcon.Open();
        }

        SqlCommand sqlcmd = new SqlCommand("GetPOSTableDetails", sqlcon);
        sqlcmd.CommandType = CommandType.StoredProcedure;

        SqlParameter param = new SqlParameter();
        param = sqlcmd.Parameters.Add("@TableId", SqlDbType.Int);
        param.Value = tableId;

        SqlDataReader reader = sqlcmd.ExecuteReader();
        while (reader.Read())
        {
            tableInfo.TableId = Convert.ToInt32(reader["TableId"]);
            tableInfo.TableNo = reader["TableNo"].ToString();
            tableInfo.NumberOfSeats = Convert.ToInt32(reader["NumberOfSeats"]);
            tableInfo.GroupName = reader["GroupName"].ToString();
            tableInfo.Active = Convert.ToBoolean(reader["Active"]);
        }
        reader.Close();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        sqlcon.Close();
    }

    return tableInfo;
   }
   public DataTable GroupNameFill()
   {
       DataTable dtbl = new DataTable();
       try
       {
           if (sqlcon.State == ConnectionState.Closed)
           {
               sqlcon.Open();
           }
           SqlCommand sccmd = new SqlCommand("POSTableGroupNameFill", sqlcon);
           sccmd.CommandType = CommandType.StoredProcedure;
           SqlDataAdapter sdaadapter = new SqlDataAdapter();
           sdaadapter.SelectCommand = sccmd;
           sdaadapter.Fill(dtbl);
       }
       catch (Exception ex)
       {
           MessageBox.Show(ex.ToString());
       }
       finally
       {
           sqlcon.Close();
       }
       return dtbl;
   }
      public DataTable POSTablesListOnLoad()
      {
          DataTable dtbl = new DataTable();
          try
          {
              if (sqlcon.State == ConnectionState.Closed)
              {
                  sqlcon.Open();
              }
              SqlCommand sccmd = new SqlCommand("POSTablesListOnLoad", sqlcon);
              sccmd.CommandType = CommandType.StoredProcedure;
              SqlDataAdapter sdaadapter = new SqlDataAdapter();
              sdaadapter.SelectCommand = sccmd;
              sdaadapter.Fill(dtbl);
          }
          catch (Exception ex)
          {
              MessageBox.Show(ex.ToString());
          }
          finally
          {
              sqlcon.Close();
          }
          return dtbl;
      }

   }

}


