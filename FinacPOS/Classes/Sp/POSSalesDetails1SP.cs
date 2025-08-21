using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class POSSalesDetails1SP : DBConnection
    {
        public string POSSalesDetails1Add(POSSalesDetails1Info infoPOSSalesDetails1)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSSalesDetails1Add", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSSalesMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.POSSalesMasterId;
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.BillDate;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.SessionDate;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.SessionNo;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.CounterId;
                sprmparam = sccmd.Parameters.Add("@LineNumber", SqlDbType.Int);
                sprmparam.Value = infoPOSSalesDetails1.LineNumber;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductCode;
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.Barcode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.NVarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductName;
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UnitId;
                sprmparam = sccmd.Parameters.Add("@qty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Qty;
                sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Rate;
                sprmparam = sccmd.Parameters.Add("@excludeRate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ExcludeRate;
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.CostPrice;
                sprmparam = sccmd.Parameters.Add("@grossValue", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.GrossValue;
                sprmparam = sccmd.Parameters.Add("@discPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscPer;
                sprmparam = sccmd.Parameters.Add("@discAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@netAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.NetAmount;
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.TaxId;
                sprmparam = sccmd.Parameters.Add("@taxPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxPer;
                sprmparam = sccmd.Parameters.Add("@taxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxAmount;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Amount;
                sprmparam = sccmd.Parameters.Add("@billDiscAmountperItem", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.BillDiscAmountperItem;
                sprmparam = sccmd.Parameters.Add("@ConversionFactor", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ConversionFactor;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UserId;
                sprmparam = sccmd.Parameters.Add("@amountBeforeDisc", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.AmountBeforeDisc;
                sprmparam = sccmd.Parameters.Add("@rateDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.RateDiscAmount;
                sprmparam = sccmd.Parameters.Add("@offerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.OfferId;
                sprmparam = sccmd.Parameters.Add("@DiscPerc", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscPercentage;
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
        public string POSDeletedSalesDetails1HistoryAdd(POSSalesDetails1Info infoPOSSalesDetails1)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSDeletedSalesDetails1HistoryAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSSalesMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.POSSalesMasterId;
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.BillDate;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.SessionDate;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.SessionNo;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.CounterId;
                sprmparam = sccmd.Parameters.Add("@LineNumber", SqlDbType.Int);
                sprmparam.Value = infoPOSSalesDetails1.LineNumber;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductCode;
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.Barcode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.NVarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductName;
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UnitId;
                sprmparam = sccmd.Parameters.Add("@qty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Qty;
                sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Rate;
                sprmparam = sccmd.Parameters.Add("@excludeRate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ExcludeRate;
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.CostPrice;
                sprmparam = sccmd.Parameters.Add("@grossValue", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.GrossValue;
                sprmparam = sccmd.Parameters.Add("@discPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscPer;
                sprmparam = sccmd.Parameters.Add("@discAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@netAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.NetAmount;
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.TaxId;
                sprmparam = sccmd.Parameters.Add("@taxPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxPer;
                sprmparam = sccmd.Parameters.Add("@taxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxAmount;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Amount;
                sprmparam = sccmd.Parameters.Add("@billDiscAmountperItem", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.BillDiscAmountperItem;
                sprmparam = sccmd.Parameters.Add("@ConversionFactor", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ConversionFactor;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UserId;
                sprmparam = sccmd.Parameters.Add("@amountBeforeDisc", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.AmountBeforeDisc;
                sprmparam = sccmd.Parameters.Add("@rateDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.RateDiscAmount;
                sprmparam = sccmd.Parameters.Add("@offerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.OfferId;
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
        public string POSGridDeletedItemHistoryAdd(POSSalesDetails1Info infoPOSSalesDetails1)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSGridDeletedItemHistoryAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSSalesMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.POSSalesMasterId;
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.BillDate;
                sprmparam = sccmd.Parameters.Add("@billTime", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.BillTime;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.SessionDate;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.SessionNo;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.CounterId;
                sprmparam = sccmd.Parameters.Add("@LineNumber", SqlDbType.Int);
                sprmparam.Value = infoPOSSalesDetails1.LineNumber;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductCode;
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.Barcode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.NVarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductName;
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UnitId;
                sprmparam = sccmd.Parameters.Add("@qty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Qty;
                sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Rate;
                sprmparam = sccmd.Parameters.Add("@excludeRate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ExcludeRate;
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.CostPrice;
                sprmparam = sccmd.Parameters.Add("@grossValue", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.GrossValue;
                sprmparam = sccmd.Parameters.Add("@discPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscPer;
                sprmparam = sccmd.Parameters.Add("@discAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@netAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.NetAmount;
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.TaxId;
                sprmparam = sccmd.Parameters.Add("@taxPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxPer;
                sprmparam = sccmd.Parameters.Add("@taxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxAmount;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Amount;
                sprmparam = sccmd.Parameters.Add("@billDiscAmountperItem", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.BillDiscAmountperItem;
                sprmparam = sccmd.Parameters.Add("@ConversionFactor", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ConversionFactor;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UserId;
                sprmparam = sccmd.Parameters.Add("@amountBeforeDisc", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.AmountBeforeDisc;
                sprmparam = sccmd.Parameters.Add("@rateDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.RateDiscAmount;
                sprmparam = sccmd.Parameters.Add("@offerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.OfferId;
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
        public string POSHoldDetails1Add(POSSalesDetails1Info infoPOSSalesDetails1)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSHoldDetails1Add", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSHoldMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.POSSalesMasterId;
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.BillDate;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesDetails1.SessionDate;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.SessionNo;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.CounterId;
                sprmparam = sccmd.Parameters.Add("@LineNumber", SqlDbType.Int);
                sprmparam.Value = infoPOSSalesDetails1.LineNumber;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductCode;
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.Barcode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.ProductName;
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UnitId;
                sprmparam = sccmd.Parameters.Add("@qty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Qty;
                sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Rate;
                sprmparam = sccmd.Parameters.Add("@excludeRate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ExcludeRate;
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.CostPrice;
                sprmparam = sccmd.Parameters.Add("@grossValue", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.GrossValue;
                sprmparam = sccmd.Parameters.Add("@discPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscPer;
                sprmparam = sccmd.Parameters.Add("@discAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@netAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.NetAmount;
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.TaxId;
                sprmparam = sccmd.Parameters.Add("@taxPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxPer;
                sprmparam = sccmd.Parameters.Add("@taxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.TaxAmount;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.Amount;
                sprmparam = sccmd.Parameters.Add("@billDiscAmountperItem", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.BillDiscAmountperItem;
                sprmparam = sccmd.Parameters.Add("@ConversionFactor", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.ConversionFactor;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.UserId;

                sprmparam = sccmd.Parameters.Add("@amountBeforeDisc", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.AmountBeforeDisc;
                sprmparam = sccmd.Parameters.Add("@rateDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesDetails1.RateDiscAmount;
                sprmparam = sccmd.Parameters.Add("@offerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesDetails1.OfferId;
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
        public void POSHoldDetails1Delete(string strPOSHoldMasterId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSHoldDetails1Delete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSHoldMasterId", SqlDbType.VarChar);
                sprmparam.Value = strPOSHoldMasterId;
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
    }
}
