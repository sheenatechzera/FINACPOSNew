using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for RouteInfo    
//</summary>    
namespace FinacPOS        
{    
class RouteInfo    
{    
    private string _routeId;    
    private string _marketId;    
    private string _routeName;    
    private string _narration;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string RouteId    
    {    
        get { return _routeId; }    
        set { _routeId = value; }    
    }    
    public string MarketId    
    {    
        get { return _marketId; }    
        set { _marketId = value; }    
    }    
    public string RouteName    
    {    
        get { return _routeName; }    
        set { _routeName = value; }    
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
