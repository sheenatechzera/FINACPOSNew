using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for BranchInfo    
//</summary>    
namespace FinacPOS
{
    class BranchInfo
    {
        string _branchId;
        string _currencyId;
        private string _branchName;
        private string _address;
        private string _phoneNo;
        private string _fax;
        private string _mobile;
        private string _email;
        private string _web;
        private string _tinNo;
        private string _cstNo;
        private string _panNo;
        private byte[] _logo;
        private DateTime _startDate;
        private DateTime _extraDate;
        private string _extra1;    
        private DateTime _currentDate;
        private string _extra2;
        private bool _mainBranch;
        private string _pinNo; 

        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        public string CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string PhoneNo
        {
            get { return _phoneNo; }
            set { _phoneNo = value; }
        }
        public string Fax
        {
            get { return _fax; }
            set { _fax = value; }
        }
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Web
        {
            get { return _web; }
            set { _web = value; }
        }
        public string TinNo
        {
            get { return _tinNo; }
            set { _tinNo = value; }
        }
        public string CstNo
        {
            get { return _cstNo; }
            set { _cstNo = value; }
        }
        public string PanNo
        {
            get { return _panNo; }
            set { _panNo = value; }
        }
        public byte[] Logo
        {
            get { return _logo; }
            set { _logo = value; }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
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
        public bool MainBranch
        {
            get { return _mainBranch; }
            set { _mainBranch = value; }
        }
        public DateTime  CurrentDate
        {
            get { return _currentDate; }
            set { _currentDate = value; }
        }
        public string pinNo
        {
            get { return _pinNo; }
            set { _pinNo = value; }
        }
      
    }
}
