using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for UnitConversionSP    
//</summary>    
namespace FinacPOS
{    
class UnitConversionSP:DBConnection    
{    
    public void UnitConversionAdd(UnitConversionInfo unitconversioninfo)
    {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UnitConversionAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
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
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.UserId;
                sprmparam = sccmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                sprmparam.Value = unitconversioninfo.Description;

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
        public void UnitConversionEdit(UnitConversionInfo unitconversioninfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UnitConversionEdit", sqlcon);
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
                sprmparam = sccmd.Parameters.Add("@extraDate", SqlDbType.DateTime);
                sprmparam.Value = DateTime.Now;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@barcode", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.Barcode;
                sprmparam = sccmd.Parameters.Add("@userId", SqlDbType.VarChar);
                sprmparam.Value = unitconversioninfo.UserId;
                sprmparam = sccmd.Parameters.Add("@Description", SqlDbType.NVarChar);
                sprmparam.Value = unitconversioninfo.Description;
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
    public DataTable UnitConversionViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("UnitConversionViewAll", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaadapter.Fill(dtbl);

        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public UnitConversionInfo UnitConversionView(string unitConversionId )
    {
        UnitConversionInfo unitconversioninfo =new UnitConversionInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitConversionView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitConversionId", SqlDbType.VarChar);
            sprmparam.Value = unitConversionId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                unitconversioninfo.UnitConversionId= sdrreader[0].ToString();
                unitconversioninfo.ProductCode= sdrreader[1].ToString();
                unitconversioninfo.UnitId= sdrreader[2].ToString();
                unitconversioninfo.ConversionRate=float.Parse(sdrreader[3].ToString());
                unitconversioninfo.ExtraDate=DateTime.Parse(sdrreader[4].ToString());
                unitconversioninfo.Extra1= sdrreader[5].ToString();
                unitconversioninfo.Extra2= sdrreader[6].ToString();
                unitconversioninfo.Extra2 = sdrreader[7].ToString();
                    unitconversioninfo.Description = sdrreader["Description"].ToString();
                }
            sdrreader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return unitconversioninfo;
    }
    public void UnitConversionDelete(string UnitConversionId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitConversionDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@unitConversionId", SqlDbType.VarChar);
            sprmparam.Value = UnitConversionId;
            sccmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
    }
    public void UnitConversionDeleteByproductCode(string strProductCode)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitConversionDeleteByproductCode", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = strProductCode;
            sccmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
    }
    public int UnitConversionGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitConversionMax", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            max = int.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
        return max;
    }
    public decimal UnitConversionRateView(string strPrdCode,string strUnitId)
    {
        decimal  dRate = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitConversionRateView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();         
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = strPrdCode;
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = strUnitId;
            dRate = decimal.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
        return dRate;
    }
    public DataTable UnitViewAllByProductCode(string strPrdCode)
    {

        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }

            SqlDataAdapter sqldataadapter = new SqlDataAdapter("UnitViewAllByProductCode", sqlcon);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlparameter = new SqlParameter();
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
            sqlparameter.Value = strPrdCode;
            sqldataadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public DataTable UnitConversionViewByProductCode(string strProductCode)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("UnitConversionViewByProductCode", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = strProductCode;
            sdaadapter.Fill(dtbl);

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
   
    public decimal UnitConversionRate(string productCode, string unit)
    {
        decimal dcRate = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("UnitConversionRate", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            sccmd.Parameters.Add("@productCode", SqlDbType.VarChar).Value = productCode;
            sccmd.Parameters.Add("@unit", SqlDbType.VarChar).Value = unit;
        
            dcRate = decimal.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
        return dcRate;
    }
    public DataTable UnitConversionViewByProductCodeAllUnits(string strProductCode)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("UnitConversionViewByProductCodeAllUnits", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = strProductCode;
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
    public bool CheckBarcodeExist(string productcode,string barcode,string unitId,decimal conversionRate)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("CheckBarcodeExist", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            sccmd.Parameters.Add("@ProductCode", SqlDbType.VarChar).Value = productcode;
            sccmd.Parameters.Add("@barcode", SqlDbType.VarChar).Value = barcode;
            sccmd.Parameters.Add("@unitId", SqlDbType.VarChar).Value = unitId;
            sccmd.Parameters.Add("@conversionRate", SqlDbType.Decimal).Value = conversionRate;
            isExist =Convert.ToBoolean( sccmd.ExecuteScalar().ToString());

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
}
}
