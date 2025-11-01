using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for TaxMasterInfo    
//</summary>    
namespace FinacPOS    
{    
class TaxMasterInfo    
{    
    private string _taxId;    
    private string _taxName;    
    private decimal _rate;    
    private string _calculatingMode;    
    private bool _active;    
    private string _narration;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string TaxId    
    {    
        get { return _taxId; }    
        set { _taxId = value; }    
    }    
    public string TaxName    
    {    
        get { return _taxName; }    
        set { _taxName = value; }    
    }    
    public decimal Rate    
    {    
        get { return _rate; }    
        set { _rate = value; }    
    }    
    public string CalculatingMode    
    {    
        get { return _calculatingMode; }    
        set { _calculatingMode = value; }    
    }    
    public bool Active    
    {    
        get { return _active; }    
        set { _active = value; }    
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
