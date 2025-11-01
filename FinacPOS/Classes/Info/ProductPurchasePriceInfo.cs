using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class ProductPurchasePriceInfo
    {
        private string _purchasepriceId;
        private string _ledgerId;
        private string _productCode;
        private string _unitId;
        private decimal _amount;
        private decimal _discPercentage;
        private decimal _discAmount;
        private decimal _purchasePrice;
        private string _userId;
        private string _branchId;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private DateTime _purchaseDate;
        private string _purchasemasterid;

        public string PurchasepriceId
        {
            get { return _purchasepriceId; }
            set { _purchasepriceId = value; }
        }
        public string LedgerId
        {
            get { return _ledgerId; }
            set { _ledgerId = value; }
        }
        public string ProductCode
        {
            get { return _productCode; }
            set { _productCode = value; }
        }
        public string UnitId
        {
            get { return _unitId; }
            set { _unitId = value; }
        }
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public decimal DiscPercentage
        {
            get { return _discPercentage; }
            set { _discPercentage = value; }
        }
        public decimal DiscAmount
        {
            get { return _discAmount; }
            set { _discAmount = value; }
        }
        public decimal PurchasePrice
        {
            get { return _purchasePrice; }
            set { _purchasePrice = value; }
        }
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
        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set { _purchaseDate = value; }
        }
        public string PurchaseMasterId
        {
            get { return _purchasemasterid; }
            set { _purchasemasterid = value; }
        }
    }
}
