using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for DesignationInfo    
//</summary>    
namespace FinacPOS    
{    
class DesignationInfo    
{    
    private string _designationId;    
    private string _designationName;    
    private int _leaveDays;    
    private decimal _advancePercentage;    
    private string _narration;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string DesignationId    
    {    
        get { return _designationId; }    
        set { _designationId = value; }    
    }    
    public string DesignationName    
    {    
        get { return _designationName; }    
        set { _designationName = value; }    
    }    
    public int LeaveDays    
    {    
        get { return _leaveDays; }    
        set { _leaveDays = value; }    
    }    
    public decimal AdvancePercentage    
    {    
        get { return _advancePercentage; }    
        set { _advancePercentage = value; }    
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
