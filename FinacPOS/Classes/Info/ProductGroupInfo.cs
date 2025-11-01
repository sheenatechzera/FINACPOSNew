using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for ProductGroupInfo    
//</summary>    
namespace FinacPOS 
{    
class ProductGroupInfo    
{    
    private string _groupId;    
    private string _groupName;
    private string _groupUnder;   
    private string _narration;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    private string _category;    
    
    public string GroupId    
    {    
        get { return _groupId; }    
        set { _groupId = value; }    
    }    
    public string GroupName    
    {    
        get { return _groupName; }    
        set { _groupName = value; }    
    }
    public string GroupUnder
    {
        get { return _groupUnder; }
        set { _groupUnder = value; }
    }    
    public string Narration    
    {    
        get { return _narration; }    
        set { _narration = value; }    
    }    
    public string BranchId    
    {    
        get { return _branchId; }    
        set { _branchId = value; }    
    }    
    public DateTime ExtraDate    
    {    
        get { return _extraDate; }    
        set { _extraDate = value; }    
    }    
    public string Extra1    
    {    
        get { return _extra1; }    
        set { _extra1 = value; }    
    }    
    public string Extra2    
    {    
        get { return _extra2; }    
        set { _extra2 = value; }    
    }
    public string Category
    {
        get { return _category; }
        set { _category = value; }
    }    
    
}    
}
