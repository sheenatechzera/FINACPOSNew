using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class SessionManagementInfo
    {
        private string _sessionNo;
        private DateTime _sessionDate;
        private string _counterId;
        private string _userId;
        private decimal _openingBalance;
        private string _sessionStatus;
        private DateTime _sessionCloseTime;
        private DateTime _createdDate;
        private string _createdUserId;
        private string _branchId;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private decimal _CashSales;
        private decimal _CardSales;
        private decimal _UPISales;
        private decimal _CreditSales;
        private decimal _TotalSales;
        private string _Particular;
        private decimal _AMOUNT;
        public string SessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
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
        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public decimal OpeningBalance
        {
            get { return _openingBalance; }
            set { _openingBalance = value; }
        }
        public string SessionStatus
        {
            get { return _sessionStatus; }
            set { _sessionStatus = value; }
        }
        public DateTime SessionCloseTime
        {
            get { return _sessionCloseTime; }
            set { _sessionCloseTime = value; }
        }
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }
        public string CreatedUserId
        {
            get { return _createdUserId; }
            set { _createdUserId = value; }
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
      
        public string Particular
        {
            get { return _Particular; }
            set { _Particular = value; }
        }
        public decimal AMOUNT
        {
            get { return _AMOUNT; }
            set { _AMOUNT = value; }
        }
    }
}
