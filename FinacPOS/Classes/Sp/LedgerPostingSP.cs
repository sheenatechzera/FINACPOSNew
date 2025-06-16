using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for LedgerPostingSP    
//</summary>    
namespace FinacPOS    
{
    class LedgerPostingSP : DBConnection
    {
        public void LedgerPostingAdd(LedgerPostingInfo ledgerpostinginfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
                sprmparam.Value = ledgerpostinginfo.Date.ToString("dd-MMM-yyyy");
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.VoucherType;
                sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.VoucherNo;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.LedgerId;
                sprmparam = sccmd.Parameters.Add("@debit", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.Debit;
                sprmparam = sccmd.Parameters.Add("@credit", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.Credit;
                sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
                sprmparam.Value = ledgerpostinginfo.Optional;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@postingType", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.postingType;
                sprmparam = sccmd.Parameters.Add("@exchangeRate", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.exchangeRate;
                sprmparam = sccmd.Parameters.Add("@exchangeDate", SqlDbType.DateTime);
                sprmparam.Value = ledgerpostinginfo.exchangeDate;
                sccmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public void LedgerPostingAdd1(LedgerPostingInfo ledgerpostinginfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
                sprmparam.Value = ledgerpostinginfo.Date;
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.VoucherType;
                sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.VoucherNo;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.LedgerId;
                sprmparam = sccmd.Parameters.Add("@debit", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.Debit;
                sprmparam = sccmd.Parameters.Add("@credit", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.Credit;
                sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
                sprmparam.Value = ledgerpostinginfo.Optional;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@exchangeRate", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.exchangeRate;
                sprmparam = sccmd.Parameters.Add("@exchangeDate", SqlDbType.DateTime);
                sprmparam.Value = ledgerpostinginfo.exchangeDate;
                sccmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public void LedgerPostingEdit(LedgerPostingInfo ledgerpostinginfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerPostingId", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.LedgerPostingId;
                sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
                sprmparam.Value = ledgerpostinginfo.Date;
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.VoucherType;
                sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.VoucherNo;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.LedgerId;
                sprmparam = sccmd.Parameters.Add("@debit", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.Debit;
                sprmparam = sccmd.Parameters.Add("@credit", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.Credit;
                sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
                sprmparam.Value = ledgerpostinginfo.Optional;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = ledgerpostinginfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@exchangeRate", SqlDbType.Decimal);
                sprmparam.Value = ledgerpostinginfo.exchangeRate;
                sprmparam = sccmd.Parameters.Add("@exchangeDate", SqlDbType.DateTime);
                sprmparam.Value = ledgerpostinginfo.exchangeDate;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public DataTable LedgerPostingViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("LedgerPostingViewAll", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
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
        public LedgerPostingInfo LedgerPostingView(string ledgerPostingId)
        {
            LedgerPostingInfo ledgerpostinginfo = new LedgerPostingInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerPostingId", SqlDbType.VarChar);
                sprmparam.Value = ledgerPostingId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    ledgerpostinginfo.LedgerPostingId = sdrreader[0].ToString();
                    ledgerpostinginfo.Date = DateTime.Parse(sdrreader[1].ToString());
                    ledgerpostinginfo.VoucherType = sdrreader[2].ToString();
                    ledgerpostinginfo.VoucherNo = sdrreader[3].ToString();
                    ledgerpostinginfo.LedgerId = sdrreader[4].ToString();
                    ledgerpostinginfo.Debit = decimal.Parse(sdrreader[5].ToString());
                    ledgerpostinginfo.Credit = decimal.Parse(sdrreader[6].ToString());
                    ledgerpostinginfo.Optional = bool.Parse(sdrreader[7].ToString());
                    ledgerpostinginfo.BranchId = sdrreader[8].ToString();
                    ledgerpostinginfo.ExtraDate = DateTime.Parse(sdrreader[9].ToString());
                    ledgerpostinginfo.Extra1 = sdrreader[10].ToString();
                    ledgerpostinginfo.Extra2 = sdrreader[11].ToString();
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return ledgerpostinginfo;
        }
        public void LedgerPostingDelete(string LedgerPostingId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerPostingId", SqlDbType.VarChar);
                sprmparam.Value = LedgerPostingId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
        }
        public int LedgerPostingGetMax()
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                max = int.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message ,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return max;
        }
        public bool LedgerPostingCheckExistanceOfLedgerId(string strLedgerId)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingCheckExistanceOfLedgerId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                prm.Value = strLedgerId;
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
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public void LedgerPostingDeleteByVoucherTypeAndVoucherNo(string vocuherNumber, string voucherType)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingDeleteByVoucherTypeAndVoucherNo", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                sprmparam.Value = vocuherNumber;
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = voucherType;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
        }
        public void LedgerPostingDeleteByVoucherTypeVoucherNoForClearance(string vocuherNumber, string voucherType)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingDeleteByVoucherTypeVoucherNoForClearance", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                sprmparam.Value = vocuherNumber;
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = voucherType;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
        }
        //LedgerPostingGetCashBankIdByVoucherTypeAndVoucherNo
        public string  LedgerPostingGetCashBankIdByVoucherTypeAndVoucherNo(string strVocuherNo, string strVoucherType)
        {
            string ledgerId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("LedgerPostingGetCashBankIdByVoucherTypeAndVoucherNo", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                prm.Value = strVocuherNo;
                prm = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                prm.Value = strVoucherType;
                object obj = sccmd.ExecuteScalar();
                if (obj != null)
                {
                    ledgerId = obj.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return ledgerId ;
        }
        public DataTable LedgerPostingViewByVoucherTypeAndVoucherNumber(string strVocuherNo, string strVoucherType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("LedgerPostingViewByVoucherTypeAndVoucherNumber", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                prm.Value = strVocuherNo;
                prm = sdaadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
                prm.Value = strVoucherType;
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
    }
}
