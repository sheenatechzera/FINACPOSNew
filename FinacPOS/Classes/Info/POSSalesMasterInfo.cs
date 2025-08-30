using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class POSSalesMasterInfo
    {
        private string _POSSalesMasterId;
        private string _invoiceNo;
        private DateTime _billDate;
        private string _billTime;
        private DateTime _sessionDate;
        private string _counterId;
        private string _sessionNo;
        private string _ledgerId;
        private string _ledgerName;
        private decimal _subTotalAmount;
        private string _CustomerAddress;
        private string _CustomerPhone;
        private string _CustomerVATNo;
        private decimal _billDiscPer;
        private decimal _billDiscAmount;
        private decimal _taxableAmount;
        private decimal _totalTaxAmount;
        private decimal _totalAmount;
        private decimal _totalQty;
        private decimal _paidAmount;
        private decimal _balanceAmount;
        private string _creditCardNo;
        private decimal _creditCardAmount;
        private decimal _UPIAmount;
        private decimal _creditAmount;
        private decimal _cashAmount;
        private decimal _cashPaidAmount;
        private string _creditNoteNo;
        private decimal _creditNoteAmount;
        private string _userId;
        private String _SalesMode;
        private string _TokenNo;
        private string _SalesManId;


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

        public decimal BillDiscPer
        {
            get { return _billDiscPer; }
            set { _billDiscPer = value; }
        }
        public decimal BillDiscAmount
        {
            get { return _billDiscAmount; }
            set { _billDiscAmount = value; }
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
        public decimal PaidAmount
        {
            get { return _paidAmount; }
            set { _paidAmount = value; }
        }
        public decimal BalanceAmount
        {
            get { return _balanceAmount; }
            set { _balanceAmount = value; }
        }
        public string CreditCardNo
        {
            get { return _creditCardNo; }
            set { _creditCardNo = value; }
        }
        public decimal CreditCardAmount
        {
            get { return _creditCardAmount; }
            set { _creditCardAmount = value; }
        }
        public decimal UPIAmount
        {
            get { return _UPIAmount; }
            set { _UPIAmount = value; }
        }
        public decimal CreditAmount
        {
            get { return _creditAmount; }
            set { _creditAmount = value; }
        }
        public decimal CashAmount
        {
            get { return _cashAmount; }
            set { _cashAmount = value; }
        }
        public decimal CashPaidAmount
        {
            get { return _cashPaidAmount; }
            set { _cashPaidAmount = value; }
        }
        public string CreditNoteNo
        {
            get { return _creditNoteNo; }
            set { _creditNoteNo = value; }
        }
        public decimal CreditNoteAmount
        {
            get { return _creditNoteAmount; }
            set { _creditNoteAmount = value; }
        }
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public string SalesMode
        {
            get { return _SalesMode; }
            set { _SalesMode = value; }
        }

        public string CustomerAddress
        {
            get { return _CustomerAddress; }
            set { _CustomerAddress = value; }
        }
        public string CustomerPhone
        {
            get { return _CustomerPhone; }
            set { _CustomerPhone = value; }
        }
        public string CustomerVATNo
        {
            get { return _CustomerVATNo; }
            set { _CustomerVATNo = value; }
        }

        public string TokenNo
        {
            get { return _TokenNo; }
            set { _TokenNo = value; }
        }
        public string SalesManId
        {
            get { return _SalesManId; }
            set { _SalesManId = value; }
        }

    }
}