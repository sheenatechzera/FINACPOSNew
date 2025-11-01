using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinacPOS
{
    class OfferRateSP:DBConnection
    {
        public string OfferRateAdd(OfferRateInfo offerrateinfo)
        {
            String strId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("OfferRateSave", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@offerCode", SqlDbType.NVarChar);
                sprmparam.Value = offerrateinfo.offerCode;
                sprmparam = sccmd.Parameters.Add("@offerName", SqlDbType.NVarChar);
                sprmparam.Value = offerrateinfo.offerName;
                sprmparam = sccmd.Parameters.Add("@fromDate", SqlDbType.DateTime);
                sprmparam.Value = offerrateinfo.fromDate;
                sprmparam = sccmd.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = offerrateinfo.toDate;
                sprmparam = sccmd.Parameters.Add("@pricingLevel", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.pricingLevel;
                sprmparam = sccmd.Parameters.Add("@IsApproved", SqlDbType.Bit);
                sprmparam.Value = offerrateinfo.IsApproved;
                sprmparam = sccmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                sprmparam.Value = offerrateinfo.IsActive;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.userId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.extra2;
                strId = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return strId;
        }
        public void OfferRateEdit(OfferRateInfo offerrateinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("OfferRateEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@offerId", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.offerId;
                sprmparam = sccmd.Parameters.Add("@offerCode", SqlDbType.NVarChar);
                sprmparam.Value = offerrateinfo.offerCode;
                sprmparam = sccmd.Parameters.Add("@offerName", SqlDbType.NVarChar);
                sprmparam.Value = offerrateinfo.offerName;
                sprmparam = sccmd.Parameters.Add("@fromDate", SqlDbType.DateTime);
                sprmparam.Value = offerrateinfo.fromDate;
                sprmparam = sccmd.Parameters.Add("@toDate", SqlDbType.DateTime);
                sprmparam.Value = offerrateinfo.toDate;
                sprmparam = sccmd.Parameters.Add("@pricingLevel", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.pricingLevel;
                sprmparam = sccmd.Parameters.Add("@IsApproved", SqlDbType.Bit);
                sprmparam.Value = offerrateinfo.IsApproved;
                sprmparam = sccmd.Parameters.Add("@IsActive", SqlDbType.Bit);
                sprmparam.Value = offerrateinfo.IsActive;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId; 
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.userId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = offerrateinfo.extra2;
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
        public DataTable OfferRateViewByOfferId(string strOfferId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("OfferRateViewByOfferId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@offerId", SqlDbType.VarChar);
                sprmparam.Value = strOfferId;
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
        public bool OfferRateDelete(string strOfferId)
        {
            bool isOk = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("OfferRateDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@OfferId", SqlDbType.VarChar);
                sprmparam.Value = strOfferId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;

                isOk = bool.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return isOk;
        }
        public DataTable ProductViewAllForOfferRateLookup(string branchId, string godownId, bool isBOM, bool isPackage, string starttext, string rackId, string voucherType,string groupId,string pricinglevelId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductViewAllForOfferRateLookup", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@godownId", SqlDbType.VarChar);
                sprmparam.Value = godownId;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@isBOM", SqlDbType.Bit);
                sprmparam.Value = isBOM;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@isPackage", SqlDbType.Bit);
                sprmparam.Value = isPackage;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                sprmparam.Value = starttext;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@rackId", SqlDbType.VarChar);
                sprmparam.Value = rackId;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sprmparam.Value = voucherType;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = groupId;
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@pricingLevelId", SqlDbType.VarChar);
                sprmparam.Value = pricinglevelId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public void OfferRateUpdateApproval(string strOfferId)
        {
           
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("OfferRateUpdateApproval", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@OfferId", SqlDbType.VarChar);
                sprmparam.Value = strOfferId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;

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
        public DataTable OfferRateGetAllByBranch()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("OfferRateGetAllByBranch", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value =PublicVariables. _branchId;
                
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PSP2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable POSOfferRateProductReport(DateTime dtFrom, DateTime dtTo, string strReportType, string category,string offerId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("POSOfferRateProductReport", sqlcon);
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
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@OfferId", SqlDbType.VarChar);
                sprmparam.Value = offerId;
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
