using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for BOMSP    
//</summary>    
namespace FinacPOS   
{    
class BOMSP:DBConnection    
{    
    public void BOMAdd(BOMInfo bominfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BOMAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();         
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = bominfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@rowMaterialId", SqlDbType.VarChar);
            sprmparam.Value = bominfo.RowMaterialId;
            sprmparam = sccmd.Parameters.Add("@quantity", SqlDbType.Decimal);
            sprmparam.Value = bominfo.Quantity;

            sprmparam = sccmd.Parameters.Add("@UnitId", SqlDbType.VarChar);
            sprmparam.Value = bominfo.UnitId;

            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = bominfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = bominfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = bominfo.ProductDetailsId;
            
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
    public void BOMEdit(BOMInfo bominfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BOMEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@bomId", SqlDbType.VarChar);
            sprmparam.Value = bominfo.BomId;
            sprmparam = sccmd.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = bominfo.ProductCode;
            sprmparam = sccmd.Parameters.Add("@rowMaterialId", SqlDbType.VarChar);
            sprmparam.Value = bominfo.RowMaterialId;
            sprmparam = sccmd.Parameters.Add("@quantity", SqlDbType.Decimal);
            sprmparam.Value = bominfo.Quantity;
            sprmparam = sccmd.Parameters.Add("@unitId", SqlDbType.VarChar);
            sprmparam.Value = bominfo.UnitId;
            sprmparam = sccmd.Parameters.Add("@extraDate", SqlDbType.DateTime);
            sprmparam.Value = bominfo.ExtraDate;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = bominfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = bominfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = bominfo.ProductDetailsId;
            
            sccmd.ExecuteNonQuery();        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }

    }
    public DataTable BOMViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BOMViewAll", sqlcon);
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
    public BOMInfo BOMView(string bomId )
    {
        BOMInfo bominfo =new BOMInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BOMView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@bomId", SqlDbType.VarChar);
            sprmparam.Value = bomId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                bominfo.BomId= sdrreader[0].ToString();
                bominfo.ProductCode= sdrreader[1].ToString();
                bominfo.RowMaterialId= sdrreader[2].ToString();
                bominfo.Quantity=decimal.Parse(sdrreader[3].ToString());
                bominfo.UnitId= sdrreader[4].ToString();
                bominfo.ExtraDate=DateTime.Parse(sdrreader[5].ToString());
                bominfo.Extra1= sdrreader[6].ToString();
                bominfo.Extra2= sdrreader[7].ToString();
                bominfo.ProductDetailsId = sdrreader[8].ToString();
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
        return bominfo;
    }
    public void BOMDelete(string BomId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BOMDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@bomId", SqlDbType.VarChar);
            sprmparam.Value = BomId;
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
    public int BOMGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BOMMax", sqlcon);
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
    public DataTable BOMViewByProductDetailsId(string strProductDetailsId)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BOMViewByProductDetailsId", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = strProductDetailsId;
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
    public DataTable BOMViewByProductcode(string productCode)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("BOMViewByProductcode", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
            sprmparam.Value = productCode;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId ;
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
    public void BOMDeleteByproductDetailsId(string strProductDetailsId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("BOMDeleteByproductDetailsId", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@productDetailsId", SqlDbType.VarChar);
            sprmparam.Value = strProductDetailsId;
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
}
}
