using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    class ProductMainGroupSP : DBConnection    
    {
        public string ProductMainGroupAdd(ProductMainGroupInfo productmaingroupinfo)
        {
            string strId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupName", SqlDbType.NVarChar);
                sprmparam.Value = productmaingroupinfo.GroupName;
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = productmaingroupinfo.GroupCode;
                sprmparam = sccmd.Parameters.Add("@scaleGroup", SqlDbType.Bit);
                sprmparam.Value = productmaingroupinfo.ScaleGroup;
                sprmparam = sccmd.Parameters.Add("@productCodeLength", SqlDbType.Int );
                sprmparam.Value = productmaingroupinfo.ProductCodeLength;
                sprmparam = sccmd.Parameters.Add("@nextNumber", SqlDbType.Int);
                sprmparam.Value = productmaingroupinfo.NextNumber;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = productmaingroupinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = productmaingroupinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = productmaingroupinfo.Extra2;
               
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
        public void ProductMainGroupEdit(ProductMainGroupInfo productmaingroupinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = productmaingroupinfo.GroupCode;
                sprmparam = sccmd.Parameters.Add("@groupName", SqlDbType.NVarChar);
                sprmparam.Value = productmaingroupinfo.GroupName;
                sprmparam = sccmd.Parameters.Add("@scaleGroup", SqlDbType.Bit);
                sprmparam.Value = productmaingroupinfo.ScaleGroup;
                sprmparam = sccmd.Parameters.Add("@productCodeLength", SqlDbType.Int);
                sprmparam.Value = productmaingroupinfo.ProductCodeLength;
                sprmparam = sccmd.Parameters.Add("@nextNumber", SqlDbType.Int);
                sprmparam.Value = productmaingroupinfo.NextNumber;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = productmaingroupinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = productmaingroupinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = productmaingroupinfo.Extra2;
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
        public DataTable ProductMainGroupViewAllForSearch(string str)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductMainGroupViewAllForSearch", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.NVarChar);
                sprmparam.Value = str;
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
        public ProductMainGroupInfo ProductMainGroupView(string groupCode)
        {
            ProductMainGroupInfo productmaingroupinfo = new ProductMainGroupInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = groupCode;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    productmaingroupinfo.GroupCode = sdrreader[0].ToString();
                    productmaingroupinfo.GroupName = sdrreader[1].ToString();
                    productmaingroupinfo.ScaleGroup = Boolean.Parse( sdrreader[2].ToString());
                    productmaingroupinfo.ProductCodeLength = Int32.Parse(sdrreader[3].ToString());
                    productmaingroupinfo.NextNumber = Int32.Parse(sdrreader[4].ToString());
                    productmaingroupinfo.BranchId = sdrreader[5].ToString();
                    productmaingroupinfo.ExtraDate = DateTime.Parse(sdrreader[6].ToString());
                    productmaingroupinfo.Extra1 = sdrreader[7].ToString();
                    productmaingroupinfo.Extra2 = sdrreader[8].ToString();
                
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
            return productmaingroupinfo;
        }
        public void ProductMainGroupDelete(string GroupCode)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = GroupCode;
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
        public int ProductMainGroupGetMax()
        {
            int max = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupMax", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                max = int.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return max;
        }
        public bool ProductMainGroupExistance(string groupName, string groupCode, bool isInEditMode)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("ProductMainGroupExistance", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@groupName", SqlDbType.NVarChar);
                sprmparam.Value = groupName;
                sprmparam = sccmd.Parameters.Add("@groupCode", SqlDbType.VarChar);
                sprmparam.Value = groupCode;
                sprmparam = sccmd.Parameters.Add("@isInEditMode", SqlDbType.Bit);
                sprmparam.Value = isInEditMode;
                isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
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
