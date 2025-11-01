using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for BatchInfo    
//</summary>    
namespace FinacPOS  
{    
class BatchInfo    
{    
    private string _batchId;    
    private string _batchName;    
    private DateTime _mfd;    
    private DateTime _expd;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string BatchId    
    {    
        get { return _batchId; }    
        set { _batchId = value; }    
    }    
    public string BatchName    
    {    
        get { return _batchName; }    
        set { _batchName = value; }    
    }    
    public DateTime Mfd    
    {    
        get { return _mfd; }    
        set { _mfd = value; }    
    }    
    public DateTime Expd    
    {    
        get { return _expd; }    
        set { _expd = value; }    
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
