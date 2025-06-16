using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class POSReceiptDetailsInfo
    {
        string _POSReceiptMasterId;
        string _POSReceiptDetailsId;
        string _ledgerId;
        decimal _amount;
        string _chequeNo;
        DateTime _chequeDate;
        string _extra1;
        public string POSReceiptMasterId
        {
            get { return _POSReceiptMasterId; }
            set { _POSReceiptMasterId = value; }
        }
        public string POSReceiptDetailsId
        {
            get { return _POSReceiptDetailsId; }
            set { _POSReceiptDetailsId = value; }
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
