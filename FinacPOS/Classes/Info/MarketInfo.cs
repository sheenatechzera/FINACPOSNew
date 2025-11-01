using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for MarketInfo    
//</summary>    
namespace FinacPOS    
{    
class MarketInfo    
{    
    private string _marketId;    
    private string _areaId;    
    private string _marketName;    
    private string _narration;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string MarketId    
    {    
        get { return _marketId; }    
        set { _marketId = value; }    
    }    
    public string AreaId    
    {    
        get { return _areaId; }    
        set { _areaId = value; }    
    }    
    public string MarketName    
    {    
        get { return _marketName; }    
        set { _marketName = value; }    
    }    
    public string Narration    
    {    
        get { return _narration; }    
        set { _narration = value; }    
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
