using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class RackSP : DBConnection 
    {
        public string RackAdd(RackInfo rackinfo)
        {
            string str = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = rackinfo.GodownId;
                sprmparam = sccmd.Parameters.Add("@rackName", SqlDbType.NVarChar);
                sprmparam.Value = rackinfo.RackName;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.VarChar);
                sprmparam.Value = rackinfo.Narration;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = rackinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = rackinfo.Extra2;
                str = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return str;
        }
        public void RackEdit(RackInfo rackinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
                sprmparam.Value = rackinfo.RackId;
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = rackinfo.GodownId;
                sprmparam = sccmd.Parameters.Add("@rackName", SqlDbType.NVarChar);
                sprmparam.Value = rackinfo.RackName;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = rackinfo.Narration;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = rackinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = rackinfo.Extra2;
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

       
        public DataTable RackViewAllByGodown(string godownId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("RackViewAllByGodown", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = godownId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId ;
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
        public DataTable RackViewAllByCondition(string strGodownId, string strStartText)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("RackViewAllByCondition", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                prm.Value = strStartText;
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = strGodownId;
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
        public DataTable RackViewAllForSearch(string strGodownId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("RackViewAllForSearch", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = strGodownId;
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
       
        public RackInfo RackView(string strRackId)
        {
            RackInfo InfoRack = new RackInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
                sprmparam.Value = strRackId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    InfoRack.RackId = sdrreader[0].ToString();
                    InfoRack.GodownId= sdrreader[1].ToString();
                    InfoRack.RackName = sdrreader[2].ToString();
                    InfoRack.Narration = sdrreader[3].ToString();
                    InfoRack.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                    InfoRack.Extra1 = sdrreader[5].ToString();
                    InfoRack.Extra2 = sdrreader[6].ToString();
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
            return InfoRack;
        }
        public void RackDelete(string strRackId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
                sprmparam.Value = strRackId;
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
        
        public bool RackCheckExistanceOfName(string strRackName, string strGodownId,string rackId)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackCheckExistanceOfName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@rackName", SqlDbType.VarChar);
                prm.Value = strRackName;
                prm = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = strGodownId;
                prm = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
                prm = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
                prm.Value = rackId ;
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
        public bool RackReferenceCheck(string strRackId)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
                prm.Value = strRackId;
                isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
               
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

        public RackInfo RackViewByName(string godownId, string branchId, string rackName)
        {
            RackInfo InfoRack = new RackInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackViewByName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = godownId;
                sprmparam = sccmd.Parameters.Add("@rackName", SqlDbType.VarChar);
                sprmparam.Value = rackName;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    InfoRack.RackId = sdrreader[0].ToString();
                    InfoRack.GodownId = sdrreader[1].ToString();
                    InfoRack.RackName = sdrreader[2].ToString();
                    InfoRack.Narration = sdrreader[3].ToString();
                    InfoRack.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                    InfoRack.Extra1 = sdrreader[5].ToString();
                    InfoRack.Extra2 = sdrreader[6].ToString();
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
            return InfoRack;
        }
        public DataTable RackViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("RackViewAll", sqlcon);
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
