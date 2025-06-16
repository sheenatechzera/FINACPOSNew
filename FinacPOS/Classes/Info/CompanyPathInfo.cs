using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for CompanyPathInfo    
//</summary>    
namespace FinacPOS    
{    
class CompanyPathInfo    
{    
    private string _companyId;    
    private string _companyName;    
    private string _companyPath;    
    private bool _branchEnabled;    
    private bool _defaultt;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    
    public string CompanyId    
    {    
        get { return _companyId; }    
        set { _companyId = value; }    
    }    
    public string CompanyName    
    {    
        get { return _companyName; }    
        set { _companyName = value; }    
    }    
    public string CompanyPath    
    {    
        get { return _companyPath; }    
        set { _companyPath = value; }    
    }    
    public bool BranchEnabled    
    {    
        get { return _branchEnabled; }    
        set { _branchEnabled = value; }    
    }    
    public bool Defaultt    
    {    
        get { return _defaultt; }    
        set { _defaultt = value; }    
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
