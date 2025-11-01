using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for ServiceInfo    
//</summary>    
namespace FinacPOS    
{    
class ServiceInfo    
{    
    private string _serviceId;    
    private string _serviceName;    
    private string _narration;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    private decimal _rate;
    public string ServiceId    
    {    
        get { return _serviceId; }    
        set { _serviceId = value; }    
    }    
    public string ServiceName    
    {    
        get { return _serviceName; }    
        set { _serviceName = value; }    
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
    public decimal  Rate
    {
        get { return _rate ; }
        set { _rate = value; }
    }    
    
}    
}
