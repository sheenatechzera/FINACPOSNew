using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinacPOS
{
    class POSTableInfo
    {
        public static int _TableId;
        public static string _TableNo;
        public static int _NumberOfSeats;
        public static string _GroupName;
        public static bool _Active;

        public int TableId
        {
            get { return _TableId; }
            set { _TableId = value; }
        }
        public string TableNo
        {
            get { return _TableNo; }
            set { _TableNo = value; }
        }
        public int NumberOfSeats
        {
            get { return _NumberOfSeats; }
            set { _NumberOfSeats = value; }
        }
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }
    }
}
