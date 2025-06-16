using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for CurrencyConversionInfo    
//</summary>    
namespace FinacPOS    
{    
class CurrencyConversionInfo    
{    
    private string _currecyConversionId;    
    private string _currencyId;    
    private DateTime _date;    
    private decimal _rate;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    private string _narration;
    private string _branchId;
    public string CurrecyConversionId    
    {    
        get { return _currecyConversionId; }    
        set { _currecyConversionId = value; }    
    }    
    public string CurrencyId    
    {    
        get { return _currencyId; }    
        set { _currencyId = value; }    
    }    
    public DateTime Date    
    {    
        get { return _date; }    
        set { _date = value; }    
    }    
    public decimal Rate    
    {    
        get { return _rate; }    
        set { _rate = value; }    
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
    public string Narration
    {
        get { return _narration ; }
        set { _narration = value; }
    }
    public string BranchId
    {
        get { return _branchId ; }
        set { _branchId = value; }
    }    
    
}    
}
