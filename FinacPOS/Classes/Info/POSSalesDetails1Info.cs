using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class POSSalesDetails1Info
    {
        private string _POSSalesDetails1Id;
        private string _POSSalesMasterId;
        private string _invoiceNo;
        private DateTime _billDate;
        private string _billTime;
        private DateTime _sessionDate;
        private string _counterId;
        private string _sessionNo;
    
        private Int32 _LineNumber;
        private string _productCode;
        private string _barcode;
        private string _productName;
        private string _unitId;
        private decimal _qty;
        private decimal _rate;
        private decimal _excludeRate;
        private decimal _costPrice;
        private decimal _grossValue;
        private decimal _discPer;
        private decimal _discAmount;
        private decimal _netAmount;
        private string _taxId;
        private decimal _taxPer;
        private decimal _taxAmount;
        private decimal _Amount;
        private decimal _billDiscAmountperItem;
        private decimal _ConversionFactor;
        private string _userId;
        private decimal _amountBeforeDisc;
        private decimal _rateDiscAmount;
        private string _offerId;
        public string POSSalesDetails1Id
        {
            get { return _POSSalesDetails1Id; }
            set { _POSSalesDetails1Id = value; }
        }
        public string POSSalesMasterId
        {
            get { return _POSSalesMasterId; }
            set { _POSSalesMasterId = value; }
        }
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }
        public DateTime BillDate
        {
            get { return _billDate; }
            set { _billDate = value; }
        }
        public String BillTime
        {
            get { return _billTime; }
            set { _billTime = value; }
        }
        public DateTime SessionDate
        {
            get { return _sessionDate; }
            set { _sessionDate = value; }
        }
        public string CounterId
        {
            get { return _counterId; }
            set { _counterId = value; }
        }
        public string SessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }
      
        public Int32 LineNumber
        {
            get { return _LineNumber; }
            set { _LineNumber = value; }
        }
        public string ProductCode
        {
            get { return _productCode; }
            set { _productCode = value; }
        }
        public string Barcode
        {
            get { return _barcode; }
            set { _barcode = value; }
        }
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        public string UnitId
        {
            get { return _unitId; }
            set { _unitId = value; }
        }
        public decimal Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }
        public decimal Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        public decimal ExcludeRate
        {
            get { return _excludeRate; }
            set { _excludeRate = value; }
        }
        public decimal CostPrice
        {
            get { return _costPrice; }
            set { _costPrice = value; }
        }
        public decimal GrossValue
        {
            get { return _grossValue; }
            set { _grossValue = value; }
        }
        public decimal DiscPer
        {
            get { return _discPer; }
            set { _discPer = value; }
        }
        public decimal DiscAmount
        {
            get { return _discAmount; }
            set { _discAmount = value; }
        }
        public decimal NetAmount
        {
            get { return _netAmount; }
            set { _netAmount = value; }
        }
        public string TaxId
        {
            get { return _taxId; }
            set { _taxId = value; }
        }
        public decimal TaxPer
        {
            get { return _taxPer; }
            set { _taxPer = value; }
        }
        public decimal TaxAmount
        {
            get { return _taxAmount; }
            set { _taxAmount = value; }
        }
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public decimal BillDiscAmountperItem
        {
            get { return _billDiscAmountperItem; }
            set { _billDiscAmountperItem = value; }
        }
        public decimal ConversionFactor
        {
            get { return _ConversionFactor; }
            set { _ConversionFactor = value; }
        }

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public decimal AmountBeforeDisc
        {
            get { return _amountBeforeDisc; }
            set { _amountBeforeDisc = value; }
        }

        public decimal RateDiscAmount
        {
            get { return _rateDiscAmount; }
            set { _rateDiscAmount = value; }
        }
        public string OfferId
        {
            get { return _offerId; }
            set { _offerId = value; }
        }

    }
}
