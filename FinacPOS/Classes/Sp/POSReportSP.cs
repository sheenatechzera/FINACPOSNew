using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.Utils.About;

namespace FinacPOS
{
    class POSReportSP : DBConnection
    {
        public DataTable POSActiveUserViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSUserViewAllActive", sqlcon);
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
        public DataTable POSActiveCounterViewAll()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCounterViewAllActive", sqlcon);
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
        public DataTable POSCashierwiseSalesSummery(DateTime dtFrom,DateTime dtTo,string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCashierWiseSalesSummery", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
             
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
        public DataTable POSTenderTypeWiseSalesSummery(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSTenderTypeWiseSalesSummery", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
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
        public DataTable POSSalesSummery(DateTime dtFrom, DateTime dtTo, string strReportType, string ResultType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSSalesSummery", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ResultType", SqlDbType.VarChar);
                sprmparam.Value = ResultType;
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
        public DataTable POSCashierwisebillsummery(DateTime dtFrom, DateTime dtTo, string strReportType, string ResultType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCashierwisebillsummery", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
               
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
        public DataTable POSBillWiseSalesDetailedReport(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSBillWiseSalesDetailedReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                //sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                //sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;

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
        public DataTable POSBillWiseProfitDetailedReport(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSBillWiseProfitDetailedReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                //sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                //sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;

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
        public DataTable POSBillWiseSalesSummery(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSBillWiseSalesSummery", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                //sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                //sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;

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
        public DataTable POSBillWiseProfitSummery(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSBillWiseProfitSummery", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                //sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                //sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;

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
        public DataTable POSGetMainGroup()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSGetMainGRoup", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
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
        public DataTable POSGetProductGroupsByCategory(string category)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSGetProductGroupsByCategory", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@category", SqlDbType.VarChar);
                sprmparam.Value = category;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
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
        public DataTable POSProductReport(DateTime dtFrom, DateTime dtTo, string strReportType, string category)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSProductReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@category", SqlDbType.VarChar);
                sprmparam.Value = category;
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
        public DataTable POSExchagedItemReport(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSExchagedItemReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
           
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
               
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
        public DataTable POSProductProfitReport(DateTime dtFrom, DateTime dtTo, string strReportType, string category)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSProductProfitReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@category", SqlDbType.VarChar);
                sprmparam.Value = category;
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
        public DataTable POSProductSalesSummery(DateTime dtFrom, DateTime dtTo, string strReportType, string productCode)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSProductSalesSummery", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = productCode;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
           
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
        public DataTable POSHourlySalesCount(DateTime dtFrom, DateTime dtTo)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSDailySalesCount", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;              

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
        public DataTable POSHoldBillSummaryReport(DateTime dtFrom, DateTime dtTo, string strReportType, string strStatus)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSHoldBillSummaryReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@BillStatus", SqlDbType.VarChar);
                sprmparam.Value = strStatus;
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
        public DataTable POSHoldBillDetailedReport(DateTime dtFrom, DateTime dtTo, string strReportType, string strStatus)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSHoldBillDetailedReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@BillStatus", SqlDbType.VarChar);
                sprmparam.Value = strStatus;
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

        public DataTable POSSalesBillReport(DateTime dtFrom, DateTime dtTo, string strBillNo)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSSalesBillReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;  
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@BillNo", SqlDbType.VarChar);
                sprmparam.Value = strBillNo;
            
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
        public void POSSalesDeletebySalesMasterId(string posSalesMasterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSSalesDeletebySalesMasterId", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSSalesMasterId", SqlDbType.VarChar);
                sprmparam.Value = posSalesMasterId;
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
        public DataTable POSTenderTypeWiseSalesReport   (DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSTenderTypeWiseSalesReport", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                //sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                //sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;

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
        public DataTable POSBillNumberSelectAllByYear(int year,string counterId, string voucherType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSBillNumberSelectAllByYear", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@Year", SqlDbType.Int);
                sprmparam.Value = year;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = counterId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = voucherType;

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
        public void POSBillNumberLastBillUpdateForDelete(string strCounterId, int strYear, string strVoucherType)
        {

            try
            {   
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSBillNumberLastBillUpdateForDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@Year", SqlDbType.Int);
                sprmparam.Value = strYear;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = strCounterId;         
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = strVoucherType;
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
        public void POSBillUpdate(string strCounterId, string strUserId, string strVoucherType)
        {

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSBillnumberUpdate", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = strCounterId;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = strUserId;
                sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = strVoucherType;
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
        public DataSet POSSalesPaymentDetailsGetBySalesmasterId( string strBillNo)
        {
            DataSet dtbl = new DataSet();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSSalesPaymentDetailsGetBySalesmasterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
         
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@POSSalesMasterId", SqlDbType.VarChar);
                sprmparam.Value = strBillNo;

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
        public void POSSalesUpdatePaymentMethod(POSSalesMasterInfo info)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSSalesUpdatePaymentMethod", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSSalesMasterId", SqlDbType.VarChar);
                sprmparam.Value = info. POSSalesMasterId;
                sprmparam = sccmd.Parameters.Add("@CreditCardNo", SqlDbType.VarChar);
                sprmparam.Value = info.CreditCardNo;
                sprmparam = sccmd.Parameters.Add("@UPIAmount", SqlDbType.Decimal);
                sprmparam.Value = info.UPIAmount;
                sprmparam = sccmd.Parameters.Add("@CreditCardAmount", SqlDbType.Decimal);
                sprmparam.Value = info.CreditCardAmount;
                sprmparam = sccmd.Parameters.Add("@creditAmount", SqlDbType.Decimal);
                sprmparam.Value = info.CreditAmount;
                sprmparam = sccmd.Parameters.Add("@CashAmount", SqlDbType.Decimal);
                sprmparam.Value = info.CashAmount;
                sprmparam = sccmd.Parameters.Add("@CashPaidAmount", SqlDbType.Decimal);
                sprmparam.Value = info.CashPaidAmount;
                sprmparam = sccmd.Parameters.Add("@BalanceAmount", SqlDbType.Decimal);
                sprmparam.Value = info.BalanceAmount;
          

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
        public DataTable POSCounterViewbyCounterId(string counterId)
        {
            DataTable dtbl = new DataTable();
            try
            {
              
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCounterViewbyCounterId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = counterId;
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
        public DataTable POSCashierDeletedItemHistory(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCashierDeletedItemHistory", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;

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
        public DataTable POSCashierCancelledSalesHistory(DateTime dtFrom, DateTime dtTo, string strReportType)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSCashierCancelledSalesHistory", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sprmparam.Value = dtFrom;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = dtTo;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@ReportType", SqlDbType.VarChar);
                sprmparam.Value = strReportType;

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
