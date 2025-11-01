using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for GodownSP    
//</summary>    
namespace FinacPOS
{
    class GodownSP : DBConnection
    {
        public string GodownAdd(GodownInfo godowninfo)
        {
            string strId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownName", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.GodownName;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.Narration;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = godowninfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.Extra2;
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
        public void GodownEdit(GodownInfo godowninfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = godowninfo.GodownId;
                sprmparam = sccmd.Parameters.Add("@godownName", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.GodownName;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.Narration;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = godowninfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = godowninfo.Extra2;
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
        public DataTable GodownViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GodownViewAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
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
        public DataTable GodownViewWithCurrentStockOfProduct(string strPrdCode, string branchId, string godownName)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GodownViewWithCurrentStockOfProduct", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = strPrdCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownName", SqlDbType.VarChar);
                prm.Value = godownName;
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
        public DataTable RackViewWithCurrentStockOfProductByGodown(string strPrdCode, string branchId, string godownId, string startText)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("RackViewWithCurrentStockOfProduct", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                prm.Value = strPrdCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                prm.Value = godownId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                prm.Value = startText;

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
        //-------------------------------------------------------
        public DataTable GodownViewAllForSearch(string str)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GodownViewAllForSearch", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                sprmparam.Value = str;
                SqlParameter prm = new SqlParameter();
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
        public GodownInfo GodownView(string godownId)
        {
            GodownInfo godowninfo = new GodownInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = godownId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    godowninfo.GodownId = sdrreader[0].ToString();
                    godowninfo.GodownName = sdrreader[1].ToString();
                    godowninfo.Narration = sdrreader[2].ToString();
                    godowninfo.BranchId = sdrreader[3].ToString();
                    godowninfo.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                    godowninfo.Extra1 = sdrreader[5].ToString();
                    godowninfo.Extra2 = sdrreader[6].ToString();
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
            return godowninfo;
        }
        public void GodownDelete(string GodownId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = GodownId;
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
        public int GodownGetMax()
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownMax", sqlcon);
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
        public bool GodownExistance(string godownName)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownExistance", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownName", SqlDbType.VarChar);
                sprmparam.Value = godownName;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
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
        public bool GodownReferenceCheck(string strGodownId)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = strGodownId;
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
        public string GodownGetDefaultGodownId(string strBranchId)
        {
            string  strGodownId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownGetDefaultGodownId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = strBranchId;
                strGodownId = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return strGodownId;
        }
    
    public string 	RackGetDefaultRackId(string strgodownId)
        {
            string  strGodownId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("RackGetDefaultRackId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = strgodownId;
                strGodownId = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return strGodownId;
        }

        public GodownInfo GodownViewByName(string godownName, string branchId)
        {
            GodownInfo godowninfo = new GodownInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("GodownViewByName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@godownName", SqlDbType.VarChar);
                sprmparam.Value = godownName;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    godowninfo.GodownId = sdrreader[0].ToString();
                    godowninfo.GodownName = sdrreader[1].ToString();
                    godowninfo.Narration = sdrreader[2].ToString();
                    godowninfo.BranchId = sdrreader[3].ToString();
                    godowninfo.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                    godowninfo.Extra1 = sdrreader[5].ToString();
                    godowninfo.Extra2 = sdrreader[6].ToString();
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
            return godowninfo;
        }
    }
}
