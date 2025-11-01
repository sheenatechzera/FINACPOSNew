using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for UnitInfo    
//</summary>    
namespace FinacPOS  
{    
class UnitInfo    
{    
    private string _unitId;    
    private string _unitName;    
    private string _narration;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string UnitId    
    {    
        get { return _unitId; }    
        set { _unitId = value; }    
    }    
    public string UnitName    
    {    
        get { return _unitName; }    
        set { _unitName = value; }    
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
