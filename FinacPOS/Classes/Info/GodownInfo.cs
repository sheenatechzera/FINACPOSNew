using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for GodownInfo    
//</summary>    
namespace FinacPOS
{    
class GodownInfo    
{    
    private string _godownId;    
    private string _godownName;    
    private string _narration;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string GodownId    
    {    
        get { return _godownId; }    
        set { _godownId = value; }    
    }    
    public string GodownName    
    {    
        get { return _godownName; }    
        set { _godownName = value; }    
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
