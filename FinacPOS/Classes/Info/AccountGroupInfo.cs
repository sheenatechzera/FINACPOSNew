using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for AccountGroupInfo    
//</summary>    
namespace FinacPOS 
{    
class AccountGroupInfo    
{    
    private string _groupId;    
    private string _accountGroupName;    
    private string _groupUnder;    
    private string _narration;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    private bool _defaultt;
    private string _accountGroupCode;
    private int _ledgerNextNo;  

    public string GroupId    
    {    
        get { return _groupId; }    
        set { _groupId = value; }    
    }    
    public string AccountGroupName    
    {    
        get { return _accountGroupName; }    
        set { _accountGroupName = value; }    
    }    
    public string GroupUnder    
    {    
        get { return _groupUnder; }    
        set { _groupUnder = value; }    
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
    public bool  Default
    {
        get { return _defaultt ; }
        set { _defaultt  = value; }    
    }
    public string AccountGroupCode
    {
        get { return _accountGroupCode; }
        set { _accountGroupCode = value; }
    }
    public int LedgerNextNo
    {
        get { return _ledgerNextNo; }
        set { _ledgerNextNo = value; }
    }   
    
}    
}
