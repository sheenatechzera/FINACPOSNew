using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class POSPaymentDetailsInfo
    {
        string _POSPaymentMasterId;
        string _POSPaymentDetailsId;
        string _ledgerId;
        decimal _amount;
        string _chequeNo;
        DateTime _chequeDate;
        string _extra1;
        public string POSPaymentMasterId
        {
            get { return _POSPaymentMasterId; }
            set { _POSPaymentMasterId = value; }
        }
        public string POSPaymentDetailsId
        {
            get { return _POSPaymentDetailsId; }
            set { _POSPaymentDetailsId = value; }
        }
        public string ledgerId
        {
            get { return _ledgerId; }
            set { _ledgerId = value; }
        }
        public decimal amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string chequeNo
        {
            get { return _chequeNo; }
            set { _chequeNo = value; }
        }
        public DateTime chequeDate
        {
            get { return _chequeDate; }
            set { _chequeDate = value; }
        }
        public string extra1
        {
            get { return _extra1; }
            set { _extra1 = value; }
        }
    }
}
