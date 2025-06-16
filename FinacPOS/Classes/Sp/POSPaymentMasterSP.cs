using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace FinacPOS
{
    class POSPaymentMasterSP : DBConnection
    {
        public string POSPaymentMasterAdd(POSPaymentMasterInfo infoPOSPaymentMaster)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSPaymentMasterAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();

                sprmparam = sccmd.Parameters.Add("@paymentNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.paymentNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSPaymentMaster.billDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.billTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSPaymentMaster.sessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.counterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.sessionNo;
                sprmparam = sccmd.Parameters.Add("@CashOrBankledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.CashOrBankledgerId;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = infoPOSPaymentMaster.narration;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.userId;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSPaymentMaster.totalAmount;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.extra1;
                sprmparam = sccmd.Parameters.Add("@LedgerType", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.LedgerType; 
                sprmparam = sccmd.Parameters.Add("@PaymentType", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.PaymentType;
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
        public DataTable POSPaymentMasterViewByMasterId(string strMAsterId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSPaymentMasterViewByMasterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@POSPaymentMasterId", SqlDbType.VarChar);
                sprmparam.Value = strMAsterId;
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
        public void POSPaymentVoucherDeleteDetails(string ReceiptMasterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSPaymentVoucherDeleteDetails", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSPaymentMasterId", SqlDbType.VarChar);
                sprmparam.Value = ReceiptMasterId;
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
        public void POSPaymentMasterEdit(POSPaymentMasterInfo infoPOSPaymentMaster)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSPaymentMasterEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSPaymentMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.POSPaymentMasterId;
                sprmparam = sccmd.Parameters.Add("@paymentNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.paymentNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSPaymentMaster.billDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.billTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSPaymentMaster.sessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.counterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.sessionNo;
                sprmparam = sccmd.Parameters.Add("@CashOrBankledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.CashOrBankledgerId;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = infoPOSPaymentMaster.narration;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.userId;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSPaymentMaster.totalAmount;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.extra1;
                sprmparam = sccmd.Parameters.Add("@LedgerType", SqlDbType.VarChar);
                sprmparam.Value = infoPOSPaymentMaster.LedgerType;
                sprmparam = sccmd.Parameters.Add("@PaymentType", SqlDbType.VarChar); //added on 29-03-2025 
                sprmparam.Value = infoPOSPaymentMaster.PaymentType;
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
        public DataTable GetPOSPaymentBillDetails(string paymentNo)
        {
            DataTable dtblPaymentVoucherDetails = new DataTable();

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand cmd = new SqlCommand("GetPOSPaymentBillDetails", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@paymentNo", SqlDbType.VarChar)).Value = paymentNo;

                SqlDataAdapter sdaadapter = new SqlDataAdapter(cmd);
                sdaadapter.Fill(dtblPaymentVoucherDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblPaymentVoucherDetails;
        }



    }
}
