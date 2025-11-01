using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace FinacPOS
{
    class General:DBConnection
    {
        static public SqlDataAdapter da;
        public string GenerateVoucherNumber(TextBox txt, string voucherType )
        {
            // txt is the text box into whicih the voucher numbe ris to be assigned
            string voucherNumber = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("VoucherNumberGenerationForClass", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                prm.Value = voucherType;
                prm = sccmd.Parameters.Add("@yearId", SqlDbType.VarChar);
                prm.Value = PublicVariables._yearId; ;
                prm = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value =  PublicVariables._branchId;
                voucherNumber = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SP1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            txt.Text = voucherNumber;
            return voucherNumber;
        }
        public void AssignCurrentBalanceLedger(string txt, Label lbl, string branchId)
        {
            //cmb is the combo containign ledgers , lbl is the lable to which the current balance to assign
            try
            {

                // To get current balance of ledger and assign it inot label
                if (txt != null && txt != "")
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
                        sqlparameter.Value = txt;
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
                    if (dtbl.Rows.Count > 0)
                    {
                        decimal dcBlnc = decimal.Parse(dtbl.Rows[0][0].ToString());
                        if (dtbl.Rows[0][2].ToString() != null && dtbl.Rows[0][2].ToString() != "")
                        {
                            if (bool.Parse(dtbl.Rows[0][2].ToString()) == false)
                            {
                                // Debit
                                lbl.Text = dcBlnc.ToString() + " Dr";
                                lbl.ForeColor = Color.Green;
                                lbl.BackColor = Color.Transparent;
                            }
                            else
                            {
                                // Credit
                                lbl.Text = dcBlnc.ToString() + " Cr";
                                lbl.ForeColor = Color.Red;
                                lbl.BackColor = Color.Transparent;
                            }
                        }
                        else
                        {
                            lbl.Text = "0.00 ";
                            lbl.ForeColor = Color.Black;
                            lbl.BackColor = Color.Transparent;
                        }
                    }
                    else
                    {
                        lbl.Text = "0.00 ";
                        lbl.ForeColor = Color.Black;
                        lbl.BackColor = Color.Transparent;
                    }
                }
                else
                {
                    lbl.Text = "0.00 ";
                    lbl.ForeColor = Color.Black;
                    lbl.BackColor = Color.Transparent;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("CB2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void AssignCurrentBalance(ComboBox cmb,Label lbl,string branchId)
        {
            //cmb is the combo containign ledgers , lbl is the lable to which the current balance to assign
            try
            {

                // To get current balance of ledger and assign it inot label
                if (cmb.SelectedValue != null && cmb.SelectedValue.ToString() != "" && cmb.SelectedValue.ToString()!="System.Data.DataRowView" && cmb.SelectedIndex!=-1)
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
                        sqlparameter.Value = cmb.SelectedValue.ToString();
                        sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                        sqlparameter.Value = branchId;
                        sqldataadapter.Fill(dtbl);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("CB1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    finally
                    {
                        sqlcon.Close();
                    }
                    if (dtbl.Rows.Count > 0)
                    {
                        decimal dcBlnc = decimal.Parse(dtbl.Rows[0][0].ToString());                      
                        if (dtbl.Rows[0][2].ToString() != null && dtbl.Rows[0][2].ToString() != "")
                        {
                            if (bool.Parse(dtbl.Rows[0][2].ToString()) == false)
                            {
                                // Debit
                                lbl.Text = dcBlnc.ToString() + " Dr";
                                lbl.ForeColor = Color.Green;
                                lbl.BackColor = Color.Transparent;
                            }
                            else
                            {
                                // Credit
                                lbl.Text = dcBlnc.ToString() + " Cr";
                                lbl.ForeColor = Color.Red;
                                lbl.BackColor = Color.Transparent;
                            }
                        }
                        else
                        {
                            lbl.Text = "0.00 ";
                            lbl.ForeColor = Color.Black;
                            lbl.BackColor = Color.Transparent;
                        }
                    }
                    else
                    {
                        lbl.Text = "0.00 ";
                        lbl.ForeColor = Color.Black;
                        lbl.BackColor = Color.Transparent;
                    }
                }
                else
                {
                    lbl.Text = "0.00 ";
                    lbl.ForeColor = Color.Black;
                    lbl.BackColor = Color.Transparent;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("CB2:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void AssignCurrentBalanceTextBox(TextBox txt, Label lbl, string branchId)
        {
            //cmb is the combo containign ledgers , lbl is the lable to which the current balance to assign
            try
            {

                // To get current balance of ledger and assign it inot label
                if (txt.Tag != null && txt.Text.ToString() != "")
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
                        sqlparameter.Value = txt.Tag.ToString();
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
                    if (dtbl.Rows.Count > 0)
                    {
                        decimal dcBlnc = decimal.Parse(dtbl.Rows[0][0].ToString());
                        if (dtbl.Rows[0][2].ToString() != null && dtbl.Rows[0][2].ToString() != "")
                        {
                            if (bool.Parse(dtbl.Rows[0][2].ToString()) == false)
                            {
                                // Debit
                                lbl.Text = dcBlnc.ToString() + " Dr";
                                lbl.ForeColor = Color.Green;
                                lbl.BackColor = Color.Transparent;
                            }
                            else
                            {
                                // Credit
                                lbl.Text = dcBlnc.ToString() + " Cr";
                                lbl.ForeColor = Color.Red;
                                lbl.BackColor = Color.Transparent;
                            }
                        }
                        else
                        {
                            lbl.Text = "0.00 ";
                            lbl.ForeColor = Color.Black;
                            lbl.BackColor = Color.Transparent;
                        }
                    }
                    else
                    {
                        lbl.Text = "0.00 ";
                        lbl.ForeColor = Color.Black;
                        lbl.BackColor = Color.Transparent;
                    }
                }
                else
                {
                    lbl.Text = "0.00 ";
                    lbl.ForeColor = Color.Black;
                    lbl.BackColor = Color.Transparent;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("CB2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        //to set colour of current balance label
        public void CurrentBalanceLableSettings(Label lbl)
        {
         
            try
            {
                if (lbl.Text.Contains("Dr"))
                {
                    lbl.ForeColor = Color.Red;
                    lbl.BackColor = Color.Transparent;
                }
                else if (lbl.Text.Contains("Cr"))
                {
                    lbl.ForeColor = Color.Green;
                    lbl.BackColor = Color.Transparent;
                }
                else
                {
                    lbl.ForeColor = Color.Black;
                    lbl.BackColor = Color.Transparent;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("CB3:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ClearFunction(Control panel)
        {
            foreach (Control ctl in panel.Controls)
            {

                if (ctl is TextBox)
                {
                    ((TextBox)ctl).Text = string.Empty;
                }
                

            }
        }
       public byte[] ReadFile(string strImagePath)
        {
            //Read image from the specified location 
            // return byte form image

            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(strImagePath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(strImagePath, FileMode.Open,
                                                    FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to 

            //supply number of bytes to read from file.
            //In this case we want to read entire file. 

            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

       public DataTable dbReaderFill(string qry)
       {
           DataTable _temp = new DataTable();
           try
           {
               if (sqlcon.State == ConnectionState.Closed)
               {
                   sqlcon.Open();
               }
               da = new SqlDataAdapter(qry, sqlcon);
               da.SelectCommand.CommandTimeout = 0;
               da.Fill(_temp);
               return _temp;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
           finally
           {
               sqlcon.Close();
           }
           return _temp;
       }
       public void UserActivityAdd( string voucherType ,string activityType,string masterId,string voucherNo,DateTime voucherdate,decimal oldAmount,decimal newAmount)
       {
           try
           {
               if (sqlcon.State == ConnectionState.Closed)
               {
                   sqlcon.Open();
               }
               SqlCommand sccmd = new SqlCommand("UserActivityAdd", sqlcon);
               sccmd.CommandType = CommandType.StoredProcedure;
               SqlParameter sprmparam = new SqlParameter();
               sprmparam = sccmd.Parameters.Add("@VoucherType", SqlDbType.NVarChar);
               sprmparam.Value = voucherType;
               sprmparam = sccmd.Parameters.Add("@Date", SqlDbType.DateTime);
               sprmparam.Value = DateTime.Now;
               sprmparam = sccmd.Parameters.Add("@UserId", SqlDbType.NVarChar);
               sprmparam.Value = PublicVariables._currentUserId;
               sprmparam = sccmd.Parameters.Add("@SystemName", SqlDbType.NVarChar);
               sprmparam.Value = Environment.MachineName;
               sprmparam = sccmd.Parameters.Add("@ActivityType", SqlDbType.NVarChar);
               sprmparam.Value = activityType;
               sprmparam = sccmd.Parameters.Add("@MasterId", SqlDbType.NVarChar);
               sprmparam.Value = masterId;
               sprmparam = sccmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
               sprmparam.Value = voucherNo;
               sprmparam = sccmd.Parameters.Add("@VoucherDate", SqlDbType.DateTime);
               sprmparam.Value = voucherdate;
               sprmparam = sccmd.Parameters.Add("@VoucherOldAmount", SqlDbType.Decimal);
               sprmparam.Value = oldAmount;
               sprmparam = sccmd.Parameters.Add("@VoucherNewAmount", SqlDbType.Decimal);
               sprmparam.Value = newAmount;
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
       public void TransactionMasterPostedStatusUpdate(string postedStatus,string postedBy,string voucherNo,string voucherType)
       {
           try
           {
               if (sqlcon.State == ConnectionState.Closed)
               {
                   sqlcon.Open();
               }
               SqlCommand sccmd = new SqlCommand("TransactionMasterPostedStatusUpdate", sqlcon);
               sccmd.CommandType = CommandType.StoredProcedure;
               SqlParameter sprmparam = new SqlParameter();
               sprmparam = sccmd.Parameters.Add("@PostedStatus", SqlDbType.VarChar);
               sprmparam.Value = postedStatus;
               sprmparam = sccmd.Parameters.Add("@PostedBy", SqlDbType.VarChar);
               sprmparam.Value = postedBy;
               sprmparam = sccmd.Parameters.Add("@VoucherNo", SqlDbType.VarChar);
               sprmparam.Value = voucherNo;
               sprmparam = sccmd.Parameters.Add("@VoucherType", SqlDbType.VarChar);
               sprmparam.Value = voucherType;

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
        public void UpdateQrLinkByVoucherType(string qr_link, string masterId, string voucherType)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UpdateQrLinkByVoucherType", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@qr_link", SqlDbType.NVarChar);
                sprmparam.Value = qr_link;
                sprmparam = sccmd.Parameters.Add("@MasterId", SqlDbType.VarChar);
                sprmparam.Value = masterId;
     
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = voucherType;

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
        public string LoadNextPreviousInvoice(string currentMasterId,string nextOrPrevious, string voucherType,string salesVoucherType)
        {
            // txt is the text box into whicih the voucher numbe ris to be assigned
            string masterId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("LoadNextPreviousInvoice", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();              
                prm = sccmd.Parameters.Add("@CurrentMasterId", SqlDbType.VarChar);
                prm.Value = currentMasterId;
                prm = sccmd.Parameters.Add("@nextOrPrevious", SqlDbType.VarChar);
                prm.Value = nextOrPrevious;
                prm = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                prm.Value = voucherType;
                prm = sccmd.Parameters.Add("@YearId", SqlDbType.VarChar);
                prm.Value = PublicVariables._yearId;
                prm = sccmd.Parameters.Add("@SalesVoucherType", SqlDbType.VarChar);
                prm.Value = salesVoucherType;
                masterId = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SP1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }            
            return masterId;
        }
        public bool AccountLedgerValidationForZatcaSave(out string message,string invoiceType,string ledgerCode)
        {
            message = string.Empty;

            if (FinanceSettingsInfo._ZatcaType == "Phase 2" && invoiceType == "Tax Invoice")
            {
                DataTable dtLedger = new AccountLedgerSP().AccountLedgerGetByLedgerCodeForZatcaSave(ledgerCode);
                if (dtLedger.Rows.Count > 0)
                {
                    DataRow row = dtLedger.Rows[0];

                    // 1. Check mandatory fields
                    if (string.IsNullOrWhiteSpace(row["ledgerName"].ToString()))
                    {
                        message = "Customer Ledger Name is missing.";
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(row["StreetName"].ToString()))
                    {
                        message = "Customer Street Name is missing.";
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(row["BuildingNo"].ToString()))
                    {
                        message = "Customer Building Number is missing.";
                        return false;
                    }
                    string PostboxNo = row["PostboxNo"]?.ToString().Trim();
                    if (string.IsNullOrWhiteSpace(row["PostboxNo"].ToString()))
                    {
                        message = "Customer Postbox Number is missing.";
                        return false;
                    }
                    if (PostboxNo.Length != 5 || !PostboxNo.All(char.IsDigit))
                    {
                        message = "Customer PostboxNo Number must be 5 digits.";
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(row["CityName"].ToString()))
                    {
                        message = "Customer City Name is missing.";
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(row["District"].ToString()))
                    {
                        message = "Customer District is missing.";
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(row["cstNumber"].ToString()))
                    {
                        message = "CR Number is missing.";
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(row["AdditionalNo"].ToString()))
                    {
                        message = "Additional No is missing.";
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(row["Country"].ToString()))
                    {
                        message = "Country is missing.";
                        return false;
                    }
                    // 2. Check VAT number
                    string tinNumber = row["tinNumber"]?.ToString().Trim();
                    if (string.IsNullOrWhiteSpace(tinNumber))
                    {
                        message = "Customer VAT Number is missing.";
                        return false;
                    }
                    if (FinanceSettingsInfo._validateVatNumber)
                    {
                        if (!(tinNumber.StartsWith("3") && tinNumber.EndsWith("3")))
                        {
                            message = "Customer VAT Number must start and end with 3.";
                            return false;
                        }
                        if (tinNumber.Length != 15 || !tinNumber.All(char.IsDigit))
                        {
                            message = "Customer VAT Number must be 15 digits.";
                            return false;
                        }
                    }
                    return true; // ✅ All good
                }
                else
                {
                    message = "Ledger not found.";
                    return false;
                }
            }

            return true; // Not Phase 2 or not Tax Invoice → skip checks
        }
    }
}
