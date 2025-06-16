using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class POSSalesReturnMasterInfo
    {
        private string _POSSalesReturnMasterId;
        private string _invoiceNo;
        private DateTime _billDate;
        private string _billTime;
        private DateTime _sessionDate;
        private string _counterId;
        private string _sessionNo;
        private string _returnMethod;
        private string _billtype;
        private string _ledgerId;
        private string _ledgerName;
        private decimal _subTotalAmount;
        private decimal _taxableAmount;
        private decimal _totalTaxAmount;
        private decimal _totalAmount;
        private decimal _totalQty;
        private string _returnStatus;
        private string _userId;
        private string _salesInvoiceNo;

        public string POSSalesReturnMasterId
        {
            get { return _POSSalesReturnMasterId; }
            set { _POSSalesReturnMasterId = value; }
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
        public string BillTime
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
        public string ReturnMethod
        {
            get { return _returnMethod; }
            set { _returnMethod = value; }
        }
        public string Billtype
        {
            get { return _billtype; }
            set { _billtype = value; }
        }
        public string LedgerId
        {
            get { return _ledgerId; }
            set { _ledgerId = value; }
        }
        public string LedgerName
        {
            get { return _ledgerName; }
            set { _ledgerName = value; }
        }
        public decimal SubTotalAmount
        {
            get { return _subTotalAmount; }
            set { _subTotalAmount = value; }
        }

        public decimal TaxableAmount
        {
            get { return _taxableAmount; }
            set { _taxableAmount = value; }
        }
        public decimal TotalTaxAmount
        {
            get { return _totalTaxAmount; }
            set { _totalTaxAmount = value; }
        }
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }
        public decimal TotalQty
        {
            get { return _totalQty; }
            set { _totalQty = value; }
        }
        public string ReturnStatus
        {
            get { return _returnStatus; }
            set { _returnStatus = value; }
        }
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string SalesInvoiceNo
        {
            get { return _salesInvoiceNo; }
            set { _salesInvoiceNo = value; }
        }
    }
}
