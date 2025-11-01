using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class CostCentreOpeningInfo
    {
        private string _costCentreOpeningId;
        private string _costCentreId;
        private decimal _drAmount;
        private decimal _crAmount;
        private string _ledgerId;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private string _branchId;

        public string CostCentreOpeningId
        {
            get { return _costCentreOpeningId; }
            set { _costCentreOpeningId = value; }
        }
        public string CostCentreId
        {
            get { return _costCentreId; }
            set { _costCentreId = value; }
        }
        public decimal DrAmount
        {
            get { return _drAmount; }
            set { _drAmount = value; }
        }
        public decimal CrAmount
        {
            get { return _crAmount; }
            set { _crAmount = value; }
        }
        public string LedgerId
        {
            get { return _ledgerId; }
            set { _ledgerId = value; }
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
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }    
    
    }
}
