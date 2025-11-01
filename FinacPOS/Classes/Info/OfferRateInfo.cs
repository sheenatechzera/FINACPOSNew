using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class OfferRateInfo
    {
        private string _offerId;
        private string _offerCode;
        private string _offerName;
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _pricingLevel;
        private bool _IsApproved;
        private bool _IsActive;
        private string _userId;
        private string _branchId;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private string _currecyConversionId;

        public string offerId
        {
            get { return _offerId; }
            set { _offerId = value; }
        }
        public string offerCode
        {
            get { return _offerCode; }
            set { _offerCode = value; }
        }
        public string offerName
        {
            get { return _offerName; }
            set { _offerName = value; }
        }
        public DateTime fromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }
        public string pricingLevel
        {
            get { return _pricingLevel; }
            set { _pricingLevel = value; }
        }
        public DateTime toDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }
        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public string branchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        public string userId
        {
            get { return _userId; }
            set { _userId = value; }
        }
        public DateTime extraDate
        {
            get { return _extraDate; }
            set { _extraDate = value; }
        }
        public string extra1
        {
            get { return _extra1; }
            set { _extra1 = value; }
        }
        public string extra2
        {
            get { return _extra2; }
            set { _extra2 = value; }
        }
        public string CurrecyConversionId
        {
            get { return _currecyConversionId; }
            set { _currecyConversionId = value; }
        }
    }
}
