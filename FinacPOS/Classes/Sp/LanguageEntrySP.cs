using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class LanguageEntrySP : DBConnection
    {
        public string LanguageEntryAdd(LanguageEntryInfo infoLanguageEntry)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("POSFormDetails", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@Action", SqlDbType.VarChar);
                sprmparam.Value = infoLanguageEntry.Action;
                sprmparam = sccmd.Parameters.Add("@FormId", SqlDbType.VarChar);
                sprmparam.Value = infoLanguageEntry.FormId;
                sprmparam = sccmd.Parameters.Add("@FormName", SqlDbType.DateTime);
                sprmparam.Value = infoLanguageEntry.FormName;
                sprmparam = sccmd.Parameters.Add("@FormCaption", SqlDbType.DateTime);
                sprmparam.Value = infoLanguageEntry.FormCaption;
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

        public string LanguageEntryEdit(LanguageEntryInfo infoLanguageEntry)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("POSFormDetails", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@Action", SqlDbType.Int);
                sprmparam.Value = infoLanguageEntry.Action;
                sprmparam = sccmd.Parameters.Add("@FormId", SqlDbType.Int);
                sprmparam.Value = infoLanguageEntry.FormId;
                sprmparam = sccmd.Parameters.Add("@FormCaption", SqlDbType.VarChar);
                sprmparam.Value = infoLanguageEntry.FormCaption;
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

        public DataTable LanguageEntryFormDtls(int intAction)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSFormDetails", sqlcon);
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

        public string LanguageEntryWordSave(LanguageEntryWordInfo infoLanguageWordEntry)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("POSMultiLanguageWordAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@FormId", SqlDbType.VarChar);
                sprmparam.Value = infoLanguageWordEntry.FormId;
                sprmparam = sccmd.Parameters.Add("@ControlName", SqlDbType.VarChar);
                sprmparam.Value = infoLanguageWordEntry.ControlName;
                sprmparam = sccmd.Parameters.Add("@ControlType", SqlDbType.VarChar);
                sprmparam.Value = infoLanguageWordEntry.ControlType;
                sprmparam = sccmd.Parameters.Add("@English", SqlDbType.VarChar);
                sprmparam.Value = infoLanguageWordEntry.English;
                sprmparam = sccmd.Parameters.Add("@Arabic", SqlDbType.VarBinary);
                sprmparam.Value = infoLanguageWordEntry.Arabic;
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

                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSMultiLanguageWordGet", sqlcon);
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

                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSMultiLanguageWordGet", sqlcon);
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
