using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for UnitConversionInfo    
//</summary>    
namespace FinacPOS
{
    class UnitConversionInfo
    {
        private string _unitConversionId;
        private string _productCode;
        private string _unitId;
        private float _conversionRate;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private string _barcode;
        private string _oldBarcode;
        private char _OperationType;

        private string _userId;
        private string _oldBarcodeUnit;
        private string _description;


        public string UnitConversionId
        {
            get { return _unitConversionId; }
            set { _unitConversionId = value; }
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
        public float ConversionRate
        {
            get { return _conversionRate; }
            set { _conversionRate = value; }
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
        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
        public string oldBarcode
        {
            get { return _oldBarcode; }
            set { _oldBarcode = value; }
        }
        public char OperationType
        {
            get { return _OperationType; }
            set { _OperationType = value; }
        }
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string OldBarcodeUnit
        {
            get { return _oldBarcodeUnit; }
            set { _oldBarcodeUnit = value; }
        }
        public string Description      
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
