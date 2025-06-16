using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for BranchSP    
//</summary>    
namespace FinacPOS    
{
    class BranchSP : DBConnection
    {
        
        public DataTable BranchViewAll(string startText)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("BranchViewAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                prm.Value = startText;
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
        
        public DataTable BranchViewByBranchId(string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("BranchView", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
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
        public BranchInfo BranchView(string branchId)
        {
            BranchInfo branchinfo = new BranchInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("BranchView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    branchinfo.BranchId = sdrreader["branchId"].ToString();
                    branchinfo.CurrencyId = sdrreader["currencyId"].ToString();
                    branchinfo.BranchName = sdrreader["branchName"].ToString();
                    branchinfo.Address = sdrreader["address"].ToString();
                    branchinfo.PhoneNo = sdrreader["phoneNo"].ToString();
                    branchinfo.Fax = sdrreader["fax"].ToString();
                    branchinfo.Mobile = sdrreader["mobile"].ToString();
                    branchinfo.Email = sdrreader["email"].ToString();
                    branchinfo.Web = sdrreader["web"].ToString();
                    branchinfo.TinNo = sdrreader["tinNo"].ToString();

                    branchinfo.CstNo = sdrreader["cstNo"].ToString();
                    branchinfo.PanNo = sdrreader["panNo"].ToString();

                    try
                    {
                        byte[] logo = (byte[])sdrreader["logo"];
                        branchinfo.Logo = logo;
                    }
                    catch (Exception)
                    {
                    }
                    branchinfo.StartDate = DateTime.Parse(sdrreader["startDate"].ToString());
                    branchinfo.ExtraDate = DateTime.Parse(sdrreader["extraDate"].ToString());
                    branchinfo.Extra1 = sdrreader["extra1"].ToString();
                    branchinfo.Extra2 = sdrreader["extra2"].ToString();
                    branchinfo.MainBranch = bool.Parse(sdrreader["mainBranch"].ToString());
                    if (sdrreader["currentDate"] != null && sdrreader["currentDate"].ToString() != "")
                    {

                        branchinfo.CurrentDate = DateTime.Parse(sdrreader["currentDate"].ToString());
                    }
                    //Added on 28/Jan/2024 Varis
                    branchinfo.pinNo = sdrreader["pinNo"].ToString();
                  
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CDSP :" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return branchinfo;
        }
    }
}
