using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for ProductGroupSP    
//</summary>    
namespace FinacPOS  
{    
class ProductGroupSP:DBConnection    
{    
    public string ProductGroupAdd(ProductGroupInfo productgroupinfo)
    {
        string strId = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGroupAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@groupName", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.GroupName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = productgroupinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@groupUnder", SqlDbType.VarChar);
            sprmparam.Value = productgroupinfo.GroupUnder;
            sprmparam = sccmd.Parameters.Add("@category", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Category;
            strId = sccmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return strId;
    }
    public void ProductGroupEdit(ProductGroupInfo productgroupinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGroupEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
            sprmparam.Value = productgroupinfo.GroupId;
            sprmparam = sccmd.Parameters.Add("@groupName", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.GroupName;
            sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Narration;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = productgroupinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@groupUnder", SqlDbType.VarChar);
            sprmparam.Value = productgroupinfo.GroupUnder;
            sprmparam = sccmd.Parameters.Add("@category", SqlDbType.NVarChar);
            sprmparam.Value = productgroupinfo.Category;
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
    //used in product creation
    public DataTable ProductGroupViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductGroupViewAll", sqlcon);
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
    public DataTable ProductGroupViewAllByCategory(string category)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }   
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductGroupViewAllByCategory", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@category", SqlDbType.NVarChar);
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
    public DataTable ProductGroupViewAllForSearch(string str)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductGroupViewAllForSearch", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.NVarChar);
            sprmparam.Value = str;
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
    public ProductGroupInfo ProductGroupView(string groupId )
    {
        ProductGroupInfo productgroupinfo =new ProductGroupInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGroupView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
            sprmparam.Value = groupId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                productgroupinfo.GroupId= sdrreader[0].ToString();
                productgroupinfo.GroupName= sdrreader[1].ToString();
                productgroupinfo.Narration= sdrreader[2].ToString();
                productgroupinfo.BranchId= sdrreader[3].ToString();
                productgroupinfo.ExtraDate=DateTime.Parse(sdrreader[4].ToString());
                productgroupinfo.Extra1= sdrreader[5].ToString();
                productgroupinfo.Extra2= sdrreader[6].ToString();
                productgroupinfo.GroupUnder = sdrreader[7].ToString();
                productgroupinfo.Category = sdrreader[8].ToString();
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
        return productgroupinfo;
    }
    public void ProductGroupDelete(string GroupId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGroupDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
            sprmparam.Value = GroupId;
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
    public int ProductGroupGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGroupMax", sqlcon);
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
    public bool ProductGroupExistance(string groupName,string groupId,string category,string groupUnder)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGroupExistance", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@groupName", SqlDbType.NVarChar);
            sprmparam.Value = groupName;
            sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
            sprmparam.Value = groupId ;
            sprmparam = sccmd.Parameters.Add("@Category", SqlDbType.VarChar);
            sprmparam.Value = category;
            sprmparam = sccmd.Parameters.Add("@groupUnder", SqlDbType.VarChar);
            sprmparam.Value = groupUnder;
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
  
    public bool ProductGroupReferenceCheck(string strGroupId, bool isReferenceInGroup)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("ProductGroupReferenceCheck", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
            sprmparam.Value = strGroupId;
            sprmparam = sccmd.Parameters.Add("@isReferenceInGroup", SqlDbType.VarChar);
            sprmparam.Value = isReferenceInGroup;
            
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
    public DataTable ProductGroupHierarchyViewByGroupId(string groupid)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductGroupHierarchyViewByGroupId", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@groupid", SqlDbType.VarChar);
            sprmparam.Value = groupid;
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
    public DataTable ProductGroupViewAllSubGroupByCategory(string category,string strGroupUder)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ProductGroupViewAllSubGroupByCategory", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@category", SqlDbType.NVarChar);
            sprmparam.Value = category;
            sprmparam = sdaadapter.SelectCommand.Parameters.Add("@groupUnder", SqlDbType.VarChar);
            sprmparam.Value = strGroupUder;
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
