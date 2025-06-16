using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for CurrencyInfo    
//</summary>    
namespace FinacPOS    
{    
class CurrencyInfo    
{    
    private string _currencyId;    
    private string _currencySymbol;    
    private string _currencyName;    
    private string _subunitName;    
    private int _noOfDecimalPlace;    
    private string _narration;    
    private bool _defaultt;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string CurrencyId    
    {    
        get { return _currencyId; }    
        set { _currencyId = value; }    
    }    
    public string CurrencySymbol    
    {    
        get { return _currencySymbol; }    
        set { _currencySymbol = value; }    
    }    
    public string CurrencyName    
    {    
        get { return _currencyName; }    
        set { _currencyName = value; }    
    }    
    public string SubunitName    
    {    
        get { return _subunitName; }    
        set { _subunitName = value; }    
    }    
    public int NoOfDecimalPlace    
    {    
        get { return _noOfDecimalPlace; }    
        set { _noOfDecimalPlace = value; }    
    }    
    public string Narration    
    {    
        get { return _narration; }    
        set { _narration = value; }    
    }    
    public bool Defaultt    
    {    
        get { return _defaultt; }    
        set { _defaultt = value; }    
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
