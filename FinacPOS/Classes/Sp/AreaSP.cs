using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

//<summary>    
//Summary description for AreaSP    
//</summary>    
namespace FinacPOS
{
    class AreaSP : DBConnection
    {
        public string AreaAdd(AreaInfo areainfo)
        {
            string strId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AreaAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@areaName", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.AreaName;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.Narration;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = areainfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.Extra2;
                strId = sccmd.ExecuteScalar().ToString();

               

            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return strId;
        }
        public void AreaEdit(AreaInfo areainfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AreaEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
                sprmparam.Value = areainfo.AreaId;
                sprmparam = sccmd.Parameters.Add("@areaName", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.AreaName;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.Narration;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = areainfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = areainfo.Extra2;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public DataTable AreaViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AreaViewAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value =  PublicVariables._branchId;
                sdaadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AreaViewAllForSearch(string str, string strBranchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AreaViewAllForSearch", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                sprmparam.Value = str;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = strBranchId;
                sdaadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AreaViewForReport(string strBranchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AreaViewForReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = strBranchId;
                sdaadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public AreaInfo AreaView(string areaId)
        {
            AreaInfo areainfo = new AreaInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AreaView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
                sprmparam.Value = areaId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    areainfo.AreaId = sdrreader[0].ToString();
                    areainfo.AreaName = sdrreader[1].ToString();
                    areainfo.Narration = sdrreader[2].ToString();
                    areainfo.BranchId = sdrreader[3].ToString();
                    areainfo.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                    areainfo.Extra1 = sdrreader[5].ToString();
                    areainfo.Extra2 = sdrreader[6].ToString();
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return areainfo;
        }
        public void AreaDelete(string AreaId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AreaDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
                sprmparam.Value = AreaId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
        }
        public int AreaGetMax()
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AreaMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                max = int.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return max;
        }

        public bool AreaExistance(string areaName)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AreaExistance", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@areaName", SqlDbType.VarChar);
                sprmparam.Value = areaName;
                isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public bool AreaReferenceCheck(string strAreaId)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AreaReferenceCheck", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
                sprmparam.Value = strAreaId;
                isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public DataTable AreaViewAllWithNA()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AreaViewAllWithNA", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value =  PublicVariables._branchId;
                sdaadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        
    }
}
