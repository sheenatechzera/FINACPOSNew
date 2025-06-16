using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
namespace FinacPOS
{
    class PrimaryDBSP:DBConnectionForPDB 
    {
        public DataTable CompanyPathViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("CompanyPathViewAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                sdaadapter.Fill(dtbl);
                //Estimate company name is also saved in company path so to remove that row
                for (int i = 0; i < dtbl.Rows.Count; ++i)
                    if (dtbl.Rows[i]["extra1"].ToString() == "estimate")
                    {
                        dtbl.Rows.RemoveAt(i);
                        break;
                    }
                //---------------------------------------------------
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message );
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        
        public void ShowMessageBox(string message)
        {
            if (File.Exists(Application.StartupPath + "\\file.txt"))
            {
                TextReader reder = new StreamReader(Application.StartupPath + "\\file.txt");
                string path = reder.ReadLine();
                if (path != null && path.Trim().ToString() != "")
                {
                    string ms = "An attempt to attach an auto-named database for file " + path + @":\Data\DBFinacAcount.mdf failed. A database with the same name exists, or specified file cannot be opened, or it is located on UNC share.";
                    if (message != ms)
                    {
                        MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public CompanyPathInfo CompanyPathView(string companyId)
        {
            CompanyPathInfo companypathinfo = new CompanyPathInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CompanyPathView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@companyId", SqlDbType.VarChar);
                sprmparam.Value = companyId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    companypathinfo.CompanyId = sdrreader[0].ToString();
                    companypathinfo.CompanyName = sdrreader[1].ToString();
                    companypathinfo.CompanyPath = sdrreader[2].ToString();
                    companypathinfo.BranchEnabled = bool.Parse(sdrreader[3].ToString());
                    companypathinfo.Defaultt = bool.Parse(sdrreader[4].ToString());
                    companypathinfo.ExtraDate = DateTime.Parse(sdrreader[5].ToString());
                    companypathinfo.Extra1 = sdrreader[6].ToString();
                    companypathinfo.Extra2 = sdrreader[7].ToString();
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            return companypathinfo;
        }
        public DataTable CurrencyViewAll()
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("CurrencyViewAll", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }

            return dtbl;
        }
        public bool CompanyPathCheckExistanceOfName(string strcompanyname)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CompanyPathCheckExistanceOfName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@companyName", SqlDbType.VarChar);
                prm.Value = strcompanyname;
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
                ShowMessageBox(ex.Message);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public int CompanyPathGetMax()
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CompanyPathMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                max = int.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

            finally
            {
                sqlcon.Close();
            }
            return max;
        }
        public void CompanyPathAdd(CompanyPathInfo companypathinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CompanyPathAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@companyId", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.CompanyId;
                sprmparam = sccmd.Parameters.Add("@companyName", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.CompanyName;
                sprmparam = sccmd.Parameters.Add("@companyPath", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.CompanyPath;
                sprmparam = sccmd.Parameters.Add("@branchEnabled", SqlDbType.Bit);
                sprmparam.Value = companypathinfo.BranchEnabled;
                sprmparam = sccmd.Parameters.Add("@defaultt", SqlDbType.Bit);
                sprmparam.Value = companypathinfo.Defaultt;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.Extra2;
                sccmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public void CompanyPathEdit(CompanyPathInfo companypathinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CompanyPathEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@companyId", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.CompanyId;
                sprmparam = sccmd.Parameters.Add("@companyName", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.CompanyName;
                sprmparam = sccmd.Parameters.Add("@branchEnabled", SqlDbType.Bit);
                sprmparam.Value = companypathinfo.BranchEnabled;
                sprmparam = sccmd.Parameters.Add("@defaultt", SqlDbType.Bit);
                sprmparam.Value = companypathinfo.Defaultt;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = companypathinfo.Extra2;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public void CompanyPathDelete(string CompanyId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CompanyPathDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@companyId", SqlDbType.VarChar);
                sprmparam.Value = CompanyId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

            finally
            {
                sqlcon.Close();
            }
        }
    }
}
