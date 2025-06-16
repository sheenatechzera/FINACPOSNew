using System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class CurrencyConversionSP : DBConnection  
    {
        public string CurrencyConversionRateIdViewByCurrencyId(string strCurrecyId, DateTime currentDate, string branchId)
        {
            string strId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CurrencyConversionRateIdViewByCurrencyId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@currencyId", SqlDbType.VarChar);
                sprmparam.Value = strCurrecyId;
                sprmparam = sccmd.Parameters.Add("@currentDate", SqlDbType.DateTime);
                sprmparam.Value = currentDate;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                if (sccmd.ExecuteScalar() != null)
                {
                    strId = sccmd.ExecuteScalar().ToString();
                }
            }
            catch
            {
                strId = "";
            }

            finally
            {
                sqlcon.Close();
            }
            return strId;
        }
        public bool SettingsViewByBranchId(string BranchId)
        {
            bool BillByBill = false;
         
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SettingsViewByBranchId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = BranchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {                
                    BillByBill = bool.Parse(sdrreader["billByBill"].ToString());                

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
            return BillByBill;
        }
        public CurrencyConversionInfo CurrencyConversionView(string currecyConversionId)
        {
            CurrencyConversionInfo currencyconversioninfo = new CurrencyConversionInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("CurrencyConversionView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@currecyConversionId", SqlDbType.VarChar);
                sprmparam.Value = currecyConversionId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    currencyconversioninfo.CurrecyConversionId = sdrreader[0].ToString();
                    currencyconversioninfo.CurrencyId = sdrreader[1].ToString();
                    currencyconversioninfo.Date = DateTime.Parse(sdrreader[2].ToString());
                    currencyconversioninfo.Rate = decimal.Parse(sdrreader[3].ToString());
                    currencyconversioninfo.ExtraDate = DateTime.Parse(sdrreader[4].ToString());
                    currencyconversioninfo.Extra1 = sdrreader[5].ToString();
                    currencyconversioninfo.Extra2 = sdrreader[6].ToString();
                    currencyconversioninfo.Narration = sdrreader[7].ToString();
                    currencyconversioninfo.BranchId = sdrreader[8].ToString();
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
            return currencyconversioninfo;
        }
        public void FillCurrencyCombo(ComboBox cmb)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("CurrencyViewAllForBranch", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                if (dtbl.Rows.Count > 0)
                {
                    cmb.DataSource = dtbl;
                    cmb.DisplayMember = "currencySymbol";
                    cmb.ValueMember = "currencyId";
                    //if (dtbl.Select("currencyId ='" + PublicVariables._currencyId + "'").Length > 0)
                    //{
                    //    cmb.SelectedValue = PublicVariables._currencyId;
                    //}

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
