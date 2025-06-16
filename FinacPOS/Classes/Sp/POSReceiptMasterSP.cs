using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class POSReceiptMasterSP: DBConnection
    {
        public string POSReceiptMasterAdd(POSReceiptMasterInfo infoPOSReceiptMaster)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSReceiptMasterAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
              
                sprmparam = sccmd.Parameters.Add("@receiptNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.receiptNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSReceiptMaster.billDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.billTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSReceiptMaster.sessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.counterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.sessionNo;
                sprmparam = sccmd.Parameters.Add("@CashOrBankledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.CashOrBankledgerId;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = infoPOSReceiptMaster.narration;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.userId;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSReceiptMaster.totalAmount;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.extra1;
                sprmparam = sccmd.Parameters.Add("@ReceiptType", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.ReceiptType;
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
        public DataTable POSReceiptMasterViewByMasterId(string strMAsterId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSReceiptMasterViewByMasterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@POSReceiptMasterId", SqlDbType.VarChar);
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
        public void POSReceiptVoucherDeleteDetails(string ReceiptMasterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSReceiptVoucherDeleteDetails", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSReceiptMasterId", SqlDbType.VarChar);
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
        public void POSReceiptMasterEdit(POSReceiptMasterInfo infoPOSReceiptMaster)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSReceiptMasterEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSReceiptMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.POSReceiptMasterId;
                sprmparam = sccmd.Parameters.Add("@receiptNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.receiptNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSReceiptMaster.billDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.billTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSReceiptMaster.sessionDate;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.counterId;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.sessionNo;
                sprmparam = sccmd.Parameters.Add("@CashOrBankledgerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.CashOrBankledgerId;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = infoPOSReceiptMaster.narration;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSReceiptMaster.userId;
                sprmparam = sccmd.Parameters.Add("@totalAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSReceiptMaster.totalAmount;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar); 
                sprmparam.Value = infoPOSReceiptMaster.extra1;
                sprmparam = sccmd.Parameters.Add("@ReceiptType", SqlDbType.VarChar); //added on 29\03-2025
                sprmparam.Value = infoPOSReceiptMaster.ReceiptType;
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
        public DataTable GetPOSReceiptBillDetails(string ReceiptNo)
        {
            DataTable dtblReceiptVoucherDetails = new DataTable();

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand cmd = new SqlCommand("GetPOSReceiptBillDetails", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.VarChar)).Value = ReceiptNo;

                SqlDataAdapter sdaadapter = new SqlDataAdapter(cmd);
                sdaadapter.Fill(dtblReceiptVoucherDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblReceiptVoucherDetails;
        }


    }
}
