using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class POSSalesMasterSP : DBConnection
    {
        public string POSSalesMasterAdd(POSSalesMasterInfo infoPOSSalesMaster)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSSalesMasterAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.BillDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.BillTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.SessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CounterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.SessionNo;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerId;
                sprmparam = sccmd.Parameters.Add("@ledgerName", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerName;
                sprmparam = sccmd.Parameters.Add("@subTotalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.SubTotalAmount;
                sprmparam = sccmd.Parameters.Add("@billDiscPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscPer;
                sprmparam = sccmd.Parameters.Add("@billDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscAmount;
                sprmparam = sccmd.Parameters.Add("@taxableAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TaxableAmount;
                sprmparam = sccmd.Parameters.Add("@totalTaxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalTaxAmount;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalAmount;
                sprmparam = sccmd.Parameters.Add("@totalQty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalQty;
                sprmparam = sccmd.Parameters.Add("@paidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.PaidAmount;
                sprmparam = sccmd.Parameters.Add("@balanceAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BalanceAmount;
                sprmparam = sccmd.Parameters.Add("@creditCardNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditCardNo;
                sprmparam = sccmd.Parameters.Add("@creditCardAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditCardAmount;
                sprmparam = sccmd.Parameters.Add("@UPIAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.UPIAmount;
                sprmparam = sccmd.Parameters.Add("@creditAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditAmount;
                sprmparam = sccmd.Parameters.Add("@cashAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashAmount;
                sprmparam = sccmd.Parameters.Add("@CashPaidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashPaidAmount;
                sprmparam = sccmd.Parameters.Add("@creditNoteNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteNo;
                sprmparam = sccmd.Parameters.Add("@creditNoteAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteAmount;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.UserId;
                sprmparam = sccmd.Parameters.Add("@SalesMode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.SalesMode;
                sprmparam = sccmd.Parameters.Add("@CustomerAddress", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CustomerAddress;
                sprmparam = sccmd.Parameters.Add("@CustomerPhone", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CustomerPhone;
                sprmparam = sccmd.Parameters.Add("@CustomerVATNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CustomerVATNo;
                sprmparam = sccmd.Parameters.Add("@TokenNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.TokenNo;
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
        public string POSDeletedSalesMasterHistoryAdd(POSSalesMasterInfo infoPOSSalesMaster)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSDeletedSalesMasterHistoryAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.BillDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.BillTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.SessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CounterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.SessionNo;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerId;
                sprmparam = sccmd.Parameters.Add("@ledgerName", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerName;
                sprmparam = sccmd.Parameters.Add("@subTotalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.SubTotalAmount;
                sprmparam = sccmd.Parameters.Add("@billDiscPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscPer;
                sprmparam = sccmd.Parameters.Add("@billDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscAmount;
                sprmparam = sccmd.Parameters.Add("@taxableAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TaxableAmount;
                sprmparam = sccmd.Parameters.Add("@totalTaxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalTaxAmount;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalAmount;
                sprmparam = sccmd.Parameters.Add("@totalQty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalQty;
                sprmparam = sccmd.Parameters.Add("@paidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.PaidAmount;
                sprmparam = sccmd.Parameters.Add("@balanceAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BalanceAmount;
                sprmparam = sccmd.Parameters.Add("@creditCardNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditCardNo;
                sprmparam = sccmd.Parameters.Add("@creditCardAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditCardAmount;
                sprmparam = sccmd.Parameters.Add("@UPIAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.UPIAmount;
                sprmparam = sccmd.Parameters.Add("@creditAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditAmount;
                sprmparam = sccmd.Parameters.Add("@cashAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashAmount;
                sprmparam = sccmd.Parameters.Add("@CashPaidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashPaidAmount;
                sprmparam = sccmd.Parameters.Add("@creditNoteNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteNo;
                sprmparam = sccmd.Parameters.Add("@creditNoteAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteAmount;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.UserId;
                sprmparam = sccmd.Parameters.Add("@SalesMode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.SalesMode;
                sprmparam = sccmd.Parameters.Add("@CustomerAddress", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CustomerAddress;
                sprmparam = sccmd.Parameters.Add("@CustomerPhone", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CustomerPhone;
                sprmparam = sccmd.Parameters.Add("@CustomerVATNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CustomerVATNo;
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
        public string POSHoldMasterAdd(POSSalesMasterInfo infoPOSSalesMaster)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSHoldMasterAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.BillDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.BillTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.SessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CounterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.SessionNo;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerId;
                sprmparam = sccmd.Parameters.Add("@ledgerName", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerName;
                sprmparam = sccmd.Parameters.Add("@subTotalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.SubTotalAmount;
                sprmparam = sccmd.Parameters.Add("@billDiscPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscPer;
                sprmparam = sccmd.Parameters.Add("@billDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscAmount;
                sprmparam = sccmd.Parameters.Add("@taxableAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TaxableAmount;
                sprmparam = sccmd.Parameters.Add("@totalTaxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalTaxAmount;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalAmount;
                sprmparam = sccmd.Parameters.Add("@totalQty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalQty;
                sprmparam = sccmd.Parameters.Add("@paidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.PaidAmount;
                sprmparam = sccmd.Parameters.Add("@balanceAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BalanceAmount;
                sprmparam = sccmd.Parameters.Add("@creditCardNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditCardNo;
                sprmparam = sccmd.Parameters.Add("@creditCardAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditCardAmount;
                sprmparam = sccmd.Parameters.Add("@UPIAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.UPIAmount;
                sprmparam = sccmd.Parameters.Add("@creditAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditAmount;
                sprmparam = sccmd.Parameters.Add("@cashAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashAmount;
                sprmparam = sccmd.Parameters.Add("@CashPaidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashPaidAmount;
                sprmparam = sccmd.Parameters.Add("@creditNoteNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteNo;
                sprmparam = sccmd.Parameters.Add("@creditNoteAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteAmount;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.UserId;
                sprmparam = sccmd.Parameters.Add("@HoldStatus", SqlDbType.VarChar);
                sprmparam.Value = "P";
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
        public string POSHoldMasterEdit(POSSalesMasterInfo infoPOSSalesMaster)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSHoldMasterEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSHoldMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.POSSalesMasterId;
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.BillDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.BillTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesMaster.SessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CounterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.SessionNo;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerId;
                sprmparam = sccmd.Parameters.Add("@ledgerName", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.LedgerName;
                sprmparam = sccmd.Parameters.Add("@subTotalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.SubTotalAmount;
                sprmparam = sccmd.Parameters.Add("@billDiscPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscPer;
                sprmparam = sccmd.Parameters.Add("@billDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BillDiscAmount;
                sprmparam = sccmd.Parameters.Add("@taxableAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TaxableAmount;
                sprmparam = sccmd.Parameters.Add("@totalTaxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalTaxAmount;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalAmount;
                sprmparam = sccmd.Parameters.Add("@totalQty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.TotalQty;
                sprmparam = sccmd.Parameters.Add("@paidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.PaidAmount;
                sprmparam = sccmd.Parameters.Add("@balanceAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.BalanceAmount;
                sprmparam = sccmd.Parameters.Add("@creditCardNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditCardNo;
                sprmparam = sccmd.Parameters.Add("@creditCardAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditCardAmount;
                sprmparam = sccmd.Parameters.Add("@UPIAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.UPIAmount;
                sprmparam = sccmd.Parameters.Add("@creditAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditAmount;
                sprmparam = sccmd.Parameters.Add("@cashAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashAmount;
                sprmparam = sccmd.Parameters.Add("@CashPaidAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CashPaidAmount;
                sprmparam = sccmd.Parameters.Add("@creditNoteNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteNo;
                sprmparam = sccmd.Parameters.Add("@creditNoteAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesMaster.CreditNoteAmount;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesMaster.UserId;
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
        public string GetCreditNoteAmount(string strCNNo)
        {
            string strCNAmout ="";
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GetPOSCNAmount", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sqlparameter.Value = strCNNo;
                sdaadapter.Fill(dtbl);
                if (dtbl.Rows.Count > 0)
                {
                    strCNAmout = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                }
                else
                {
                    strCNAmout = Convert.ToDecimal("0").ToString(FinanceSettingsInfo._roundDecimalPart);
                }

            }
            catch (Exception ex)
            {
                strCNAmout = Convert.ToDecimal("0").ToString(FinanceSettingsInfo._roundDecimalPart);
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return strCNAmout;
        }
        public string GetPOSLastBillNoforLastBillPrint(string strCounterId, DateTime dtSessionDate)
        {
            string strLastBillNo = "";
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("SELECT ISNULL(MAX(invoiceNo),'') AS invoiceNo FROM tbl_POSSalesMaster(NOLOCK) WHERE counterId='" + strCounterId + "' AND sessionDate='" + dtSessionDate.ToString("dd/MMM/yyyy") + "'", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.Text;
                sdaadapter.Fill(dtbl);
                if (dtbl.Rows.Count > 0)
                {
                    strLastBillNo = dtbl.Rows[0]["invoiceNo"].ToString();
                }
                else
                {
                    strLastBillNo = "";
                }

            }
            catch (Exception ex)
            {
                strLastBillNo = "";
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return strLastBillNo;
        }
        public DataTable GetPOSLastBillProductsforLastBillPrint(string strBillNo)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GetPOSLastBillProductsforLastBillPrint", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sqlparameter.Value = strBillNo;
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
        public DataTable GetPOSBillDetailsforBillPrint(string strBillNo)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GetPOSBillDetailsforBillPrint", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sqlparameter.Value = strBillNo;
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
        public DataTable GetPOSLastBillDetialsforLastBillPrint(string strBillNo)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GetPOSLastBillDetailsforLastBillPrint", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sqlparameter.Value = strBillNo;
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
        public DataTable POSHoldMasterViewByPOSHoldMasterId(string strPOSHoldMasterId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSHoldMasterViewByPOSHoldMasterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@POSHoldMasterId", SqlDbType.VarChar);
                sqlparameter.Value = strPOSHoldMasterId;
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
        public DataTable POSHoldDetails1ViewByPOSHoldMasterId(string strPOSHoldMasterId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSHoldDetails1ViewByPOSHoldMasterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@POSHoldMasterId", SqlDbType.VarChar);
                sqlparameter.Value = strPOSHoldMasterId;
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
        public void UpdatePOSHoldBillStatus(string strHoldMasterId,string billNo)
        {
            try
            {
                int returnValue = 0;
                DBClass DBClass = new DBClass();
                string strQuery = "UPDATE tbl_POSHoldMaster SET HoldStatus='C' , invoiceNo='"+billNo+"' WHERE POSHoldMasterId='" + strHoldMasterId + "'";
                returnValue = DBClass.ExecuteNonQuery(strQuery);

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

        public DataTable GetCustomerCurrentBalance(string ledgerId, string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("AccountLedgerGetCurerntBalanceOfLedger", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sqlparameter.Value = ledgerId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("CB1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataSet POSSalesReportFillByPOSSalesMasterId(string strPOSMasterId)
        {
            DataSet ds = new DataSet();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSSalesReportFillByPOSSalesMasterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@POSsalesMasterId", SqlDbType.VarChar);
                sqlparameter.Value = strPOSMasterId;
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sdaadapter.Fill(ds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return ds;
        }
        public decimal ProductStockGetCorrespondingtoBatchAndGodown(string strFrom, string strMasterId, string strPrdCode, string strBatchId, string strGodownId, string strRackId)
        {
            decimal dStock = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductStockGetCorrespondingtoBatchAndGodown", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@fromCall", SqlDbType.VarChar);
                sprmparam.Value = strFrom;
                sprmparam = sccmd.Parameters.Add("@masterId", SqlDbType.VarChar);
                sprmparam.Value = strMasterId;
                sprmparam = sccmd.Parameters.Add("@prdCode", SqlDbType.VarChar);
                sprmparam.Value = strPrdCode;
                sprmparam = sccmd.Parameters.Add("@batchId", SqlDbType.VarChar);
                sprmparam.Value = strBatchId;
                sprmparam = sccmd.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = strGodownId;
                sprmparam = sccmd.Parameters.Add("@rackId", SqlDbType.VarChar);
                sprmparam.Value = strRackId;
                dStock = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dStock;
        }
        public DataTable POSGetCategoryPrintersByCounterId(string strCounterId)
        {
            DataTable dt = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSGetCategoryPrintersByCounterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sdaadapter.SelectCommand.Parameters.Add("@CounterId", SqlDbType.VarChar);
                sqlparameter.Value = strCounterId;

                sdaadapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetPOSCounterPrintersByCounterId: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return dt;
        }

    }
}
