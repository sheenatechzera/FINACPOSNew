using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for StockPostingInfo    
//</summary>    
namespace FinacPOS    
{
    class StockPostingInfo
    {
        private string _stockPostingId;
        private DateTime _date;
        private string _voucherType;
        private string _voucherNo;
        private string _productCode;
        private string _batchId;
        private string _godownId;
        private decimal _inwardQty;
        private decimal _outwardQty;
        private decimal _rate;
        private string _unitId;
        private bool _optional;
        private string _branchId;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private string _referenceNo;
        private string _referenceType;
        private string _rackId;
        private decimal _voucherQty;
        private string _voucherUnitId;

        public string StockPostingId
        {
            get { return _stockPostingId; }
            set { _stockPostingId = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public string VoucherType
        {
            get { return _voucherType; }
            set { _voucherType = value; }
        }
        public string VoucherNo
        {
            get { return _voucherNo; }
            set { _voucherNo = value; }
        }
        public string ProductCode
        {
            get { return _productCode; }
            set { _productCode = value; }
        }
        public string BatchId
        {
            get { return _batchId; }
            set { _batchId = value; }
        }
        public string GodownId
        {
            get { return _godownId; }
            set { _godownId = value; }
        }
        public decimal InwardQty
        {
            get { return _inwardQty; }
            set { _inwardQty = value; }
        }
        public decimal OutwardQty
        {
            get { return _outwardQty; }
            set { _outwardQty = value; }
        }
        public decimal Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        public string UnitId
        {
            get { return _unitId; }
            set { _unitId = value; }
        }
        public bool Optional
        {
            get { return _optional; }
            set { _optional = value; }
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
        public string ReferenceNo
        {
            get { return _referenceNo; }
            set { _referenceNo = value; }
        }
        public string ReferenceType
        {
            get { return _referenceType; }
            set { _referenceType = value; }
        }
        public string RackId
        {
            get { return _rackId ; }
            set { _rackId = value; }
        }
        public decimal VoucherQty
        {
            get { return _voucherQty; }
            set { _voucherQty = value; }
        }
        public string VoucherUnitId
        {
            get { return _voucherUnitId; }
            set { _voucherUnitId = value; }
        }

    }  
}
