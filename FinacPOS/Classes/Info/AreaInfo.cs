using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for AreaInfo    
//</summary>    
namespace FinacPOS   
{    
class AreaInfo    
{    
    private string _areaId;    
    private string _areaName;    
    private string _narration;
    private string _branchId;
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string AreaId    
    {    
        get { return _areaId; }    
        set { _areaId = value; }    
    }    
    public string AreaName    
    {    
        get { return _areaName; }    
        set { _areaName = value; }    
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
    
}    
}
