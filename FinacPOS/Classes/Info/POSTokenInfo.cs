using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS.Classes.Info
{
    internal class POSTokenInfo
    {
        public string _TokenId;
        public DateTime _SessionDate;
        public string _LastTokenNo;
        public bool _Active;

        public string TokenId
        {
            get { return _TokenId; }
            set { _TokenId = value; }
        }

        public DateTime SessionDate
        {
            get { return _SessionDate; }
            set { _SessionDate = value; }
        }

        public string LastTokenNo
        {
            get { return _LastTokenNo; }
            set { _LastTokenNo = value; }
        }

        public Boolean Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
    }
}
