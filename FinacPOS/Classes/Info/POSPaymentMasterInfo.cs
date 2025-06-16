using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class POSPaymentMasterInfo
    {
        string _POSPaymentMasterId;

        string _paymentNo;
        DateTime _billDate;
        string _billTime;
        DateTime _sessionDate;
        string _counterId;
        string _sessionNo;
        string _CashOrBankledgerId;
        string _narration;
        string _userId;
        decimal _totalAmount;
        string _extra1;
        string _ledgerType;
        string _PaymentType;
        public string POSPaymentMasterId
        {
            get { return _POSPaymentMasterId; }
            set { _POSPaymentMasterId = value; }
        }

        public string paymentNo
        {
            get { return _paymentNo; }
            set { _paymentNo = value; }
        }
        public DateTime billDate
        {
            get { return _billDate; }
            set { _billDate = value; }
        }
        public string billTime
        {
            get { return _billTime; }
            set { _billTime = value; }
        }
        public DateTime sessionDate
        {
            get { return _sessionDate; }
            set { _sessionDate = value; }
        }
        public string counterId
        {
            get { return _counterId; }
            set { _counterId = value; }
        }
        public string sessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }
        public string CashOrBankledgerId
        {
            get { return _CashOrBankledgerId; }
            set { _CashOrBankledgerId = value; }
        }
        public string narration
        {
            get { return _narration; }
            set { _narration = value; }
        }
        public string userId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public decimal totalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }
        public string extra1
        {
            get { return _extra1; }
            set { _extra1 = value; }
        }
        public string LedgerType
        {
            get { return _ledgerType; }
            set { _ledgerType = value; }
        }
        public string PaymentType
        {
            get { return _PaymentType; }
            set { _PaymentType = value; }
        }
     
    }
}
