using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class UnitConversionHistorySP : DBConnection    
    {
        public void UnitConversionHistoryAdd(UnitConversionInfo unitconversioninfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UnitConversionHistoryAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@unitConversionId", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.UnitConversionId;
                sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.ProductCode;             
                sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.UnitId;
                sprmparam = sccmd.Parameters.Add("@conversionRate", SqlDbType.Float);
                sprmparam.Value = unitconversioninfo.ConversionRate;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.Barcode;
                sprmparam = sccmd.Parameters.Add("@oldBarcode", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.oldBarcode;
                sprmparam = sccmd.Parameters.Add("@OperationType", SqlDbType.Char);
                sprmparam.Value = unitconversioninfo.OperationType;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._currentUserId;
                sprmparam = sccmd.Parameters.Add("@oldBarcodeUnit", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.OldBarcodeUnit;
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
