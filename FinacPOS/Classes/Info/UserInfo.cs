using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for UserInfo    
//</summary>    
namespace FinacPOS  
{    
class UserInfo    
{    
    private string _userId;    
    private string _branchId;    
    private string _userName;    
    private string _password;
    private string _userGroupId;
    private bool _active;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    private decimal _DiscountPercLimit;
    private string _Language;
        public string UserId    
    {    
        get { return _userId; }    
        set { _userId = value; }    
    }    
    public string BranchId    
    {    
        get { return _branchId; }    
        set { _branchId = value; }    
    }    
    public string UserName    
    {    
        get { return _userName; }    
        set { _userName = value; }    
    }    
    public string Password    
    {    
        get { return _password; }    
        set { _password = value; }    
    } 
      public string UserGroupId    
    {    
        get { return _userGroupId; }    
        set { _userGroupId = value; }    
    } 
    public bool Active    
    {    
        get { return _active; }    
        set { _active = value; }    
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
    public decimal DiscountPercLimit
    {
        get { return _DiscountPercLimit; }
        set { _DiscountPercLimit = value; }
    }
    public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }
    }    
}
