using System;
using System.Collections.Generic;
using System.Text; 

namespace FinacPOS
{
  public  class ProductMainGroupInfo
    {
        private string _groupCode;
        private string _groupName;
        private bool _scaleGroup;
        private int _productCodeLength;
        private int _nextNumber;
        private string _branchId;    
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        public string GroupCode
        {
            get { return _groupCode; }
            set { _groupCode = value; }
        }
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }
        public bool ScaleGroup
        {
            get { return _scaleGroup; }
            set { _scaleGroup = value; }
        }
        public int ProductCodeLength
        {
            get { return _productCodeLength; }
            set { _productCodeLength = value; }
        }
        public int NextNumber
        {
            get { return _nextNumber; }
            set { _nextNumber = value; }
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
    
    }
}
