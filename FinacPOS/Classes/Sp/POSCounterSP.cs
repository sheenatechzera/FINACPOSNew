using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace FinacPOS
{
    class POSCounterSP : DBConnection
    {
        public string POSCounterAdd(POSCounterInfo counterinfo)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCounterAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.CounterId;
                sprmparam = sccmd.Parameters.Add("@counterName", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.CounterName;
                sprmparam = sccmd.Parameters.Add("@systemName", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SystemName;
                sprmparam = sccmd.Parameters.Add("@saleBillType", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SaleBillType;
                sprmparam = sccmd.Parameters.Add("@refundBillType", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.RefundBillType;
                sprmparam = sccmd.Parameters.Add("@displayStatus", SqlDbType.Bit);
                sprmparam.Value = counterinfo.DisplayStatus;
                sprmparam = sccmd.Parameters.Add("@displayPort", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.DisplayPort;
                sprmparam = sccmd.Parameters.Add("@cashDrawerStatus", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CashDrawerStatus;
                sprmparam = sccmd.Parameters.Add("@defaultPrinter", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.DefaultPrinter;
                sprmparam = sccmd.Parameters.Add("@footerDetails", SqlDbType.NVarChar);
                sprmparam.Value = counterinfo.FooterDetails;
                sprmparam = sccmd.Parameters.Add("@footerH", SqlDbType.Int);
                sprmparam.Value = counterinfo.FooterH;
                sprmparam = sccmd.Parameters.Add("@status", SqlDbType.Bit);
                sprmparam.Value = counterinfo.Status;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@ShowProductInSalesInvoice", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowProductInSalesInvoice;
                sprmparam = sccmd.Parameters.Add("@directprint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.Directprint;
                sprmparam = sccmd.Parameters.Add("@SalesAccountLedgerId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SalesAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@CashAccountLedgerId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.CashAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@BankAccountLedgerId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.BankAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@UPIAccountLedgerId ", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.UPIAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@SalesPrintCopy", SqlDbType.Int);
                sprmparam.Value = counterinfo.SalesPrintCopy;
                sprmparam = sccmd.Parameters.Add("@SalesReturnPrintCopy", SqlDbType.Int);
                sprmparam.Value = counterinfo.SalesReturnPrintCopy;
                sprmparam = sccmd.Parameters.Add("@SalesType", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SalesType;
                sprmparam = sccmd.Parameters.Add("@ProductSearchWithImage", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ProductSearchWithImage;
                sprmparam = sccmd.Parameters.Add("@ShowPrefixInBillNo", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowPrefixInBillNo;
                sprmparam = sccmd.Parameters.Add("@CategoryWaysPrint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CategoryWaysPrint;
                sprmparam = sccmd.Parameters.Add("@KOTPrint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.KOTPrint;
                sprmparam = sccmd.Parameters.Add("@ShowPreview", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowPreview;
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
        public void POSCounterEdit(POSCounterInfo counterinfo)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCounterEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.CounterId;
                sprmparam = sccmd.Parameters.Add("@counterName", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.CounterName;
                sprmparam = sccmd.Parameters.Add("@systemName", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SystemName;
                sprmparam = sccmd.Parameters.Add("@saleBillType", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SaleBillType;
                sprmparam = sccmd.Parameters.Add("@refundBillType", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.RefundBillType;
                sprmparam = sccmd.Parameters.Add("@displayStatus", SqlDbType.Bit);
                sprmparam.Value = counterinfo.DisplayStatus;
                sprmparam = sccmd.Parameters.Add("@displayPort", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.DisplayPort;
                sprmparam = sccmd.Parameters.Add("@cashDrawerStatus", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CashDrawerStatus;
                sprmparam = sccmd.Parameters.Add("@defaultPrinter", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.DefaultPrinter;
                sprmparam = sccmd.Parameters.Add("@footerDetails", SqlDbType.NVarChar);
                sprmparam.Value = counterinfo.FooterDetails;
                sprmparam = sccmd.Parameters.Add("@footerH", SqlDbType.Int);
                sprmparam.Value = counterinfo.FooterH;
                sprmparam = sccmd.Parameters.Add("@status", SqlDbType.Bit);
                sprmparam.Value = counterinfo.Status;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@ShowProductInSalesInvoice", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowProductInSalesInvoice;
                sprmparam = sccmd.Parameters.Add("@directprint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.Directprint;
                sprmparam = sccmd.Parameters.Add("@SalesAccountLedgerId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SalesAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@CashAccountLedgerId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.CashAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@BankAccountLedgerId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.BankAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@UPIAccountLedgerId", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.UPIAccountLedgerId;
                sprmparam = sccmd.Parameters.Add("@SalesPrintCopy", SqlDbType.Int);
                sprmparam.Value = counterinfo.SalesPrintCopy;
                sprmparam = sccmd.Parameters.Add("@SalesReturnPrintCopy", SqlDbType.Int);
                sprmparam.Value = counterinfo.SalesReturnPrintCopy;
                sprmparam = sccmd.Parameters.Add("@SalesType", SqlDbType.VarChar);
                sprmparam.Value = counterinfo.SalesType;
                sprmparam = sccmd.Parameters.Add("@ProductSearchWithImage", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ProductSearchWithImage;
                sprmparam = sccmd.Parameters.Add("@ShowPrefixInBillNo", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowPrefixInBillNo;
                sprmparam = sccmd.Parameters.Add("@CategoryWaysPrint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.CategoryWaysPrint;
                sprmparam = sccmd.Parameters.Add("@KOTPrint", SqlDbType.Bit);
                sprmparam.Value = counterinfo.KOTPrint;
                sprmparam = sccmd.Parameters.Add("@ShowPreview", SqlDbType.Bit);
                sprmparam.Value = counterinfo.ShowPreview;

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
        public DataTable POSCounterViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCounterViewAll", sqlcon);
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
        public POSCounterInfo POSCounterViewbyCounterId(string counterId)
        {
            POSCounterInfo counterinfo = new POSCounterInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCounterViewbyCounterId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = counterId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    counterinfo.CounterId = sdrreader["counterId"].ToString();
                    counterinfo.CounterName = sdrreader["counterName"].ToString();
                    counterinfo.SystemName = sdrreader["systemName"].ToString();
                    counterinfo.SaleBillType = sdrreader["saleBillType"].ToString();
                    counterinfo.RefundBillType = sdrreader["refundBillType"].ToString();
                    counterinfo.DisplayStatus =  bool.Parse(sdrreader["displayStatus"].ToString());
                    counterinfo.DisplayPort = sdrreader["displayPort"].ToString();
                    counterinfo.CashDrawerStatus =bool.Parse( sdrreader["cashDrawerStatus"].ToString());
                    counterinfo.DefaultPrinter =sdrreader["defaultPrinter"].ToString();
                    counterinfo.FooterDetails = sdrreader["footerDetails"].ToString();
                    counterinfo.FooterH = Convert.ToInt32(sdrreader["footerH"].ToString());
                    counterinfo.Status = bool.Parse(sdrreader["status"].ToString());
                    counterinfo.BranchId = sdrreader["branchId"].ToString();
                    counterinfo.ExtraDate = DateTime.Parse(sdrreader["extraDate"].ToString());
                    counterinfo.ShowProductInSalesInvoice = bool.Parse(sdrreader["ShowProductInSalesInvoice"].ToString());
                    counterinfo.Directprint = bool.Parse(sdrreader["directprint"].ToString());
                    counterinfo.SalesAccountLedgerId = sdrreader["SalesAccountLedgerId"].ToString();
                    counterinfo.CashAccountLedgerId =sdrreader["CashAccountLedgerId"].ToString();
                    counterinfo.BankAccountLedgerId = sdrreader["BankAccountLedgerId"].ToString();
                    counterinfo.UPIAccountLedgerId = sdrreader["UPIAccountLedgerId"].ToString();
                    counterinfo.SalesPrintCopy = Convert.ToInt32(sdrreader["SalesPrintCopy"].ToString());
                    counterinfo.SalesReturnPrintCopy = Convert.ToInt32(sdrreader["SalesReturnPrintCopy"].ToString());
                    counterinfo.SalesType = sdrreader["SalesType"].ToString();
                    counterinfo.ProductSearchWithImage = bool.Parse(sdrreader["ProductSearchWithImage"].ToString());
                    counterinfo.ShowPrefixInBillNo = bool.Parse(sdrreader["ShowPrefixInBillNo"].ToString());
                    counterinfo.CategoryWaysPrint = bool.Parse(sdrreader["CategoryWaysPrint"].ToString());
                    counterinfo.KOTPrint = bool.Parse(sdrreader["KOTPrint"].ToString());
                    counterinfo.ShowPreview = bool.Parse(sdrreader["ShowPreview"].ToString());
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
            return counterinfo ;
        }
        public void POSCounterDelete(string counterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCounterDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = counterId;
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
        public bool CheckExistanceOfCounterID(string counterid)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCounterCheckExistanceOfCounterId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                prm.Value = counterid;
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
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public POSCounterInfo POSCounterViewBySystemName()
        {
            POSCounterInfo counterinfo = new POSCounterInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSCounterViewBySystemName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@systemName", SqlDbType.VarChar);
                sprmparam.Value = Environment.MachineName;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    counterinfo.CounterId = sdrreader["counterId"].ToString();
                    counterinfo.CounterName = sdrreader["counterName"].ToString();
                    counterinfo.SalesType = sdrreader["SalesType"].ToString();  
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
            return counterinfo;
        }
        public void POSCounterPrinterDetailsAdd(PosCounterPrinterDetailsInfo  info)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand sccmd = new SqlCommand("POSCounterPrinterDetailsAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;

                SqlParameter sprmparam = new SqlParameter();

             
                sprmparam = sccmd.Parameters.Add("@CounterId", SqlDbType.VarChar);
                sprmparam.Value = info.CounterId;

                sprmparam = sccmd.Parameters.Add("@ProductGroupCategory", SqlDbType.VarChar);
                sprmparam.Value = info.ProductGroupCategory;

                sprmparam = sccmd.Parameters.Add("@DefaultPrinter", SqlDbType.VarChar);
                sprmparam.Value = info.DefaultPrinter;

                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = info.extra1;

                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = info.extra2;

                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing printer details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        public DataTable POSCounterDetailsViewByCounterId(string counterId)
        {
            DataTable dtPrinterDetails = new DataTable();

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand cmd = new SqlCommand("POSCounterPrinterDetailsViewByCounterId", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CounterId", counterId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dtPrinterDetails);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading printer details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }

            return dtPrinterDetails;
        }


          
        
        public void POSCounterPrinterDetailsDelete(string counterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlCommand cmd = new SqlCommand("POSCounterPrinterDetailsDelete", sqlcon);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = cmd.Parameters.Add("@CounterId", SqlDbType.NVarChar, 50);
                param.Value = counterId;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting printer details: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlcon.Close();
            }
        }
    }
}
