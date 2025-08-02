
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class POSCounterInfo
    {
        private string _counterId;
        private string _counterName;
        private string _systemName;
        private string _saleBillType;
        private string _refundBillType;
        private bool _displayStatus;
        private string _displayPort;
        private bool _cashDrawerStatus;
        private string _defaultPrinter;
        private string _footerDetails;
        private int _footerH;
        private bool _status;
        private string _branchId;
        private DateTime _extraDate;
        private bool _ShowProductInSalesInvoice;
        private bool _Directprint;
        private string _SalesAccountLedgerId ;
        private string _CashAccountLedgerId ;
        private string _BankAccountLedgerId ;
        private string _UPIAccountLedgerId;
        private int _SalesPrintCopy;
        private int _SalesReturnPrintCopy;
        private String _SalesType;
        private bool _ProductSearchWithImage;
        private bool _ShowPrefixInBillNo;
        private bool _CategoryWaysPrint;
        private bool _KOTPrint;



        public string CounterId
        {
            get { return _counterId; }
            set { _counterId = value; }
        }
        public string CounterName
        {
            get { return _counterName; }
            set { _counterName = value; }
        }
        public string SystemName
        {
            get { return _systemName; }
            set { _systemName = value; }
        }
        public string SaleBillType
        {
            get { return _saleBillType; }
            set { _saleBillType = value; }
        }
        public string RefundBillType
        {
            get { return _refundBillType; }
            set { _refundBillType = value; }
        }
        public bool DisplayStatus
        {
            get { return _displayStatus; }
            set { _displayStatus = value; }
        }
        public string DisplayPort
        {
            get { return _displayPort; }
            set { _displayPort = value; }
        }
        public bool CashDrawerStatus
        {
            get { return _cashDrawerStatus; }
            set { _cashDrawerStatus = value; }
        }
        public string DefaultPrinter
        {
            get { return _defaultPrinter; }
            set { _defaultPrinter = value; }
        }
        public string FooterDetails
        {
            get { return _footerDetails; }
            set { _footerDetails = value; }
        }
        public int FooterH
        {
            get { return _footerH; }
            set { _footerH = value; }
        }
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
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
        public bool ShowProductInSalesInvoice 
        {
            get { return _ShowProductInSalesInvoice; }
            set { _ShowProductInSalesInvoice = value; }
        }
         public bool Directprint
        {
            get { return _Directprint; }
            set { _Directprint= value; }
        }
         public string SalesAccountLedgerId
         {
             get { return _SalesAccountLedgerId; }
             set { _SalesAccountLedgerId = value; }
         }
         public string CashAccountLedgerId
         {
             get { return _CashAccountLedgerId; }
             set { _CashAccountLedgerId = value; }
         }
         public string BankAccountLedgerId 
         {
             get { return _BankAccountLedgerId; }
             set { _BankAccountLedgerId = value; }
         }
         public string UPIAccountLedgerId
         {
             get { return _UPIAccountLedgerId; }
             set { _UPIAccountLedgerId = value; }
         }
         public int SalesPrintCopy
         {
             get { return _SalesPrintCopy; }
             set { _SalesPrintCopy= value; }
         }
         public int  SalesReturnPrintCopy
         {
             get { return _SalesReturnPrintCopy; }
             set { _SalesReturnPrintCopy = value; }
         }

         public string  SalesType
         {
             get { return _SalesType; }
             set { _SalesType = value; }
         }
         public bool ProductSearchWithImage
         {
             get { return _ProductSearchWithImage; }
             set { _ProductSearchWithImage = value; }
         }
        public bool ShowPrefixInBillNo
        {
            get { return _ShowPrefixInBillNo; }
            set { _ShowPrefixInBillNo = value; }
        }
        public bool CategoryWaysPrint
        {
            get { return _CategoryWaysPrint; }
            set { _CategoryWaysPrint = value; }
        }
        public bool KOTPrint
        {
            get { return _KOTPrint; }
            set { _KOTPrint = value; }
        }
    }
}
