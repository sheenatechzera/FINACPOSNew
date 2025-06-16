using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class POSSalesReturnDetails1SP : DBConnection
    {
        public string POSSalesReturnDetails1Add(POSSalesReturnDetails1Info infoPOSSalesReturnDetails1)
        {
            string id = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("POSSalesReturnDetails1Add", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@POSSalesReturnMasterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.POSSalesReturnMasterId;
                sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.InvoiceNo;
                sprmparam = sccmd.Parameters.Add("@billDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesReturnDetails1.BillDate;
                sprmparam = sccmd.Parameters.Add("@sessionDate", SqlDbType.DateTime);
                sprmparam.Value = infoPOSSalesReturnDetails1.SessionDate;
                sprmparam = sccmd.Parameters.Add("@sessionNo", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.SessionNo;
                sprmparam = sccmd.Parameters.Add("@counterId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.CounterId;
                sprmparam = sccmd.Parameters.Add("@LineNumber", SqlDbType.Int);
                sprmparam.Value = infoPOSSalesReturnDetails1.LineNumber;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.ProductCode;
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.Barcode;
                sprmparam = sccmd.Parameters.Add("@productName", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.ProductName;
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.UnitId;
                sprmparam = sccmd.Parameters.Add("@billQty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.BillQty;
                sprmparam = sccmd.Parameters.Add("@qty", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.Qty;
                sprmparam = sccmd.Parameters.Add("@rate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.Rate;
                sprmparam = sccmd.Parameters.Add("@excludeRate", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.ExcludeRate;
                sprmparam = sccmd.Parameters.Add("@costPrice", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.CostPrice;
                sprmparam = sccmd.Parameters.Add("@grossValue", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.GrossValue;
                sprmparam = sccmd.Parameters.Add("@discPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.DiscPer;
                sprmparam = sccmd.Parameters.Add("@discAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.DiscAmount;
                sprmparam = sccmd.Parameters.Add("@netAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.NetAmount;
                sprmparam = sccmd.Parameters.Add("@taxId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.TaxId;
                sprmparam = sccmd.Parameters.Add("@taxPer", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.TaxPer;
                sprmparam = sccmd.Parameters.Add("@taxAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.TaxAmount;
                sprmparam = sccmd.Parameters.Add("@Amount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.Amount;
                sprmparam = sccmd.Parameters.Add("@billDiscAmountperItem", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.BillDiscAmountperItem;
                sprmparam = sccmd.Parameters.Add("@ConversionFactor", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.ConversionFactor;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.UserId;

                sprmparam = sccmd.Parameters.Add("@amountBeforeDisc", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.AmountBeforeDisc;
                sprmparam = sccmd.Parameters.Add("@rateDiscAmount", SqlDbType.Decimal);
                sprmparam.Value = infoPOSSalesReturnDetails1.RateDiscAmount;
                sprmparam = sccmd.Parameters.Add("@offerId", SqlDbType.VarChar);
                sprmparam.Value = infoPOSSalesReturnDetails1.OfferId;
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
    }
}
