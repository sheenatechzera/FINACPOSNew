using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class FinacControlSettingsSP : DBConnection
    {
        public string FinacControlSettingsAdd(FinacControlSettingsInfo infoFinacControlSettings)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("FinacControlSettings", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@Action", SqlDbType.VarChar);
                sprmparam.Value = infoFinacControlSettings.Action;
                sprmparam = sccmd.Parameters.Add("@FormId", SqlDbType.VarChar);
                sprmparam.Value = infoFinacControlSettings.FormId;
                sprmparam = sccmd.Parameters.Add("@FormName", SqlDbType.DateTime);
                sprmparam.Value = infoFinacControlSettings.FormName;
                sprmparam = sccmd.Parameters.Add("@FormCaption", SqlDbType.DateTime);
                sprmparam.Value = infoFinacControlSettings.FormCaption;
                id = sccmd.ExecuteScalar().ToString();

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

        public string FinacControlSettingsEdit(FinacControlSettingsInfo infoFinacControlSettings)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("FinacControlSettings", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@Action", SqlDbType.Int);
                sprmparam.Value = infoFinacControlSettings.Action;
                sprmparam = sccmd.Parameters.Add("@FormId", SqlDbType.Int);
                sprmparam.Value = infoFinacControlSettings.FormId;
                sprmparam = sccmd.Parameters.Add("@FormCaption", SqlDbType.VarChar);
                sprmparam.Value = infoFinacControlSettings.FormCaption;
                sprmparam = sccmd.Parameters.Add("@FormName", SqlDbType.VarChar);
                sprmparam.Value = "";
                id = sccmd.ExecuteScalar().ToString();

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

        public DataTable FinacControlSettingsFormDtls(int intAction)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("FinacControlSettings", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@Action", SqlDbType.Int);
                sqlparameter.Value = intAction;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@FormId", SqlDbType.Int);
                sqlparameter.Value = 0;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@FormName", SqlDbType.VarChar);
                sqlparameter.Value = "";
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@FormCaption", SqlDbType.VarChar);
                sqlparameter.Value = "";


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
        public DataTable FinacControlSettingsFormLoadbyModule(string strModuleName)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("FinacControlSettingsFormByModule", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@ModuleName", SqlDbType.VarChar);
                sqlparameter.Value = strModuleName;


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
        public DataTable FinacControlSettingsModule()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("FinacControlSettingsModule", sqlcon);
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
        public string FinacControlSettingsWordSave(FinacControlSettingsWordInfo infoFinacControlSettingsWord)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("FinacControlWordAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@FormId", SqlDbType.VarChar);
                sprmparam.Value = infoFinacControlSettingsWord.FormId;
                sprmparam = sccmd.Parameters.Add("@ControlName", SqlDbType.VarChar);
                sprmparam.Value = infoFinacControlSettingsWord.ControlName;
                sprmparam = sccmd.Parameters.Add("@ControlType", SqlDbType.VarChar);
                sprmparam.Value = infoFinacControlSettingsWord.ControlType;
                sprmparam = sccmd.Parameters.Add("@English", SqlDbType.NVarChar);
                sprmparam.Value = infoFinacControlSettingsWord.English;
                sprmparam = sccmd.Parameters.Add("@Arabic", SqlDbType.NVarChar);
                sprmparam.Value = infoFinacControlSettingsWord.Arabic;
                id = sccmd.ExecuteScalar().ToString();




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

        public DataTable GetWord()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("FinacControlWordGet", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@Action", SqlDbType.Int);
                sqlparameter.Value = 0;
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

        public DataTable GetFontName()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("FinacControlWordGet", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@Action", SqlDbType.Int);
                sqlparameter.Value = 1;
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
