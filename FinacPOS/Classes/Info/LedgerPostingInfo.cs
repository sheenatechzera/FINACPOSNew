using System;
using System.Collections.Generic;
using System.Text;
//<summary>    
//Summary description for LedgerPostingInfo    
//</summary>    
namespace FinacPOS
{
    class LedgerPostingInfo
    {
        private string _ledgerPostingId;
        private DateTime _date;
        private string _voucherType;
        private string _voucherNo;
        private string _ledgerId;
        private decimal _debit;
        private decimal _credit;
        private bool _optional;
        private string _branchId;
        private Decimal _exchangeRate;
        private DateTime _exchangeDate;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private string _invoiceNo;
        private string _postingType;
        private string _narration;

        public string LedgerPostingId
        {
            get { return _ledgerPostingId; }
            set { _ledgerPostingId = value; }
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
        public string LedgerId
        {
            get { return _ledgerId; }
            set { _ledgerId = value; }
        }
        public decimal Debit
        {
            get { return _debit; }
            set { _debit = value; }
        }
        public decimal Credit
        {
            get { return _credit; }
            set { _credit = value; }
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
        public Decimal exchangeRate
        {
            get { return _exchangeRate; ; }
            set { _exchangeRate = value; }
        }
       
        public DateTime exchangeDate
        {
            get { return _exchangeDate; ; }
            set { _exchangeDate = value; }
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
        public string InvoiceNo
        {
            get { return _invoiceNo; }
            set { _invoiceNo = value; }
        }
   
      
        public string postingType
        {
            get { return _postingType; }
            set { _postingType = value; }
        }
        public string Narration
        {
            get { return _narration; }
            set { _narration = value; }
        }
    }
}
