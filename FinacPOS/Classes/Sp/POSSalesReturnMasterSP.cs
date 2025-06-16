using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class POSSalesReturnMasterSP : DBConnection
    {
        public string POSSalesReturnMasterAdd(POSSalesReturnMasterInfo infoPOSSalesReturnMaster)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSSalesReturnMasterAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesReturnMaster.BillDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.BillTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesReturnMaster.SessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.CounterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.SessionNo;
                sprmparam = sccmd.Parameters.Add("@returnMethod", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.ReturnMethod;
                sprmparam = sccmd.Parameters.Add("@billtype", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.Billtype;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.LedgerId;
                sprmparam = sccmd.Parameters.Add("@ledgerName", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.LedgerName;
                sprmparam = sccmd.Parameters.Add("@subTotalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnMaster.SubTotalAmount;
                sprmparam = sccmd.Parameters.Add("@taxableAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnMaster.TaxableAmount;
                sprmparam = sccmd.Parameters.Add("@totalTaxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnMaster.TotalTaxAmount;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnMaster.TotalAmount;
                sprmparam = sccmd.Parameters.Add("@totalQty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnMaster.TotalQty;
                sprmparam = sccmd.Parameters.Add("@returnStatus", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.ReturnStatus;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.UserId;
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
        public void POSCreditNoteMasterAdd(POSSalesReturnMasterInfo infoPOSSalesReturnMaster)
        {
            
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCreditNoteMasterAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@CreditNoteNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesReturnMaster.SessionDate;
                sprmparam = sccmd.Parameters.Add("@CNAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnMaster.TotalAmount;
                sprmparam = sccmd.Parameters.Add("@CNExpiryDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesReturnMaster.SessionDate;
                sprmparam = sccmd.Parameters.Add("@CNStatus", SqlDbType.VarChar);
                sprmparam.Value = "P";
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.UserId;
                sccmd.ExecuteScalar();

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
        public void POSCreditNoteMasterEdit(POSSalesReturnMasterInfo infoPOSSalesReturnMaster)
        {

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCreditNoteMasterEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@CreditNoteNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@SalesInvoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.SalesInvoiceNo;
                sprmparam = sccmd.Parameters.Add("@CNStatus", SqlDbType.VarChar);
                sprmparam.Value = "C";
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnMaster.UserId;
                sccmd.ExecuteScalar();

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
        public DataTable GetPOSBillProductsforReturn(string strBillNo)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("GetPOSBillProductsforReturn", sqlcon);
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
    }
}
