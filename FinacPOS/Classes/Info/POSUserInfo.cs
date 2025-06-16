using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class POSUserInfo
    {
        private string _userId;
        private string _branchId;
        private string _empName;
        private string _userName;
        private string _password;
        private string _POSUserGroupId;
        private bool _POSAdmin;
        private decimal _MaxLineDiscountPer;
        private decimal _MaxBillDiscountPer;
        private bool _Active;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
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
        public string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
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
        public string POSUserGroupId
        {
            get { return _POSUserGroupId; }
            set { _POSUserGroupId = value; }
        }
        public bool POSAdmin
        {
            get { return _POSAdmin; }
            set { _POSAdmin = value; }
        }
        public decimal MaxLineDiscountPer
        {
            get { return _MaxLineDiscountPer; }
            set { _MaxLineDiscountPer = value; }
        }

        public decimal MaxBillDiscountPer
        {
            get { return _MaxBillDiscountPer; }
            set { _MaxBillDiscountPer = value; }
        }

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
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
