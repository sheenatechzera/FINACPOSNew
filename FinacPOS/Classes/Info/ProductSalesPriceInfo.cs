using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
     class ProductSalesPriceInfo
    {
       private string _salespriceId;
       private string _productCode;
       private string _unitId;
       private decimal _amount;
       private decimal _discPercentage;
       private decimal _discAmount;
       private decimal _salesPrice;
       private string _userId;
       private string _branchId;
       private DateTime _extraDate;
       private string _extra1;
       private string _extra2;
       private string _pricingLevelId;
       private decimal _oldSalesPrice;
       private char _OperationType;
       private string _oldSalesUnit;
        private decimal _costPrice;
        private decimal _marginPercentage;
        private decimal _lowestSellingPrice;
        public string PricingLevelId
       {
           get { return _pricingLevelId; }
           set { _pricingLevelId = value; }
       }
       public string SalespriceId
       {
           get { return _salespriceId; }
           set { _salespriceId = value; }
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
       public decimal SalesPrice
       {
           get { return _salesPrice; }
           set { _salesPrice = value; }
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
       public decimal oldSalesPrice
       {
           get { return _oldSalesPrice; }
           set { _oldSalesPrice = value; }
       }
       public char OperationType
       {
           get { return _OperationType; }
           set { _OperationType = value; }
       }
       public string oldSalesUnit
       {
           get { return _oldSalesUnit; }
           set { _oldSalesUnit = value; }
       }
        public decimal CostPrice
        {
            get { return _costPrice; }
            set { _costPrice = value; }
        }
        public decimal MarginPercentage
        {
            get { return _marginPercentage; }
            set { _marginPercentage = value; }
        }
        public decimal LowestSellingPrice
        {
            get { return _lowestSellingPrice; }
            set { _lowestSellingPrice = value; }
        }
    }
}
