using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class PosCounterPrinterDetailsInfo
    {
        private string _CounterId;
        private String _CounterPrinterDetailsId;
        private string _ProductGroupCategory;
        private string _DefaultPrinter;
        private string _extra1;
        private string _extra2;

        public string CounterId
        {
            get { return _CounterId; }
            set { _CounterId = value; }
        }
        public string CounterPrinterDetailsId
        {
            get { return _CounterPrinterDetailsId; }
            set { _CounterPrinterDetailsId = value; }
        }
        public string ProductGroupCategory
        {
            get { return _ProductGroupCategory; }
            set { _ProductGroupCategory = value; }
        }
        public string DefaultPrinter
        {
            get { return _DefaultPrinter; }
            set { _DefaultPrinter = value; }
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
    }
    
}

