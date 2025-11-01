using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for ProductInfo    
//</summary>    
namespace FinacPOS
{
    class ProductInfo
    {
        private string _productCode;
        private string _productName;
        private string _groupId;
        private string _brandId;
        private string _unitId;
       
        private bool _allowBatch;
    
        private bool _multipleUnit;
    
        private string _branchId;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private string _partNo;
        private string _prodcutImage;
        private decimal _conversionFactor;
        private string _category;
        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        public string ProductCode
        {
            get { return _productCode; }
            set { _productCode = value; }
        }
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        public string GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }
        public string BrandId
        {
            get { return _brandId; }
            set { _brandId = value; }
        }
        public string UnitId
        {
            get { return _unitId; }
            set { _unitId = value; }
        }
       
        public bool AllowBatch
        {
            get { return _allowBatch; }
            set { _allowBatch = value; }
        }
       
        public bool MultipleUnit
        {
            get { return _multipleUnit; }
            set { _multipleUnit = value; }
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
        public string PartNo
        {
            get { return _partNo; }
            set { _partNo = value; }
        }
        public string ProductImage
        {
            get { return _prodcutImage; }
            set { _prodcutImage = value; }
        }
        public decimal ConversionFactor
        {
            get { return _conversionFactor; }
            set { _conversionFactor = value; }
        }
    }
}
