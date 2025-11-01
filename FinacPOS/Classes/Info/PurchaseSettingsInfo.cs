using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class PurchaseSettingsInfo
    {
        public static string _PurchaseSettingsId;
        public static bool _showSize;
        public static bool _showProductCode;
        public static bool _showProductDescription;
        public static bool _ActivateRetention;
        public static int _GridRowHeight;
        public static bool _ShowFreeQtyColumns;
        public static int _ProductNameColumnWidth;
        public static int _ProductDiscriptionColumnWidth;
        public static bool _VendorInvoiceChecking;
        public static string _branchId;
        public static DateTime _extraDate;
        public static string _extra1;
        public static string _extra2;
        public static bool _tickPrintAfterSave;
        public static bool _directPrint;
        public static bool _ChangeSalesPricInPurcchase;       
      //  public static bool _showAlternateCodeInLookup;
        public static bool _showLookupForBarcode;
        public static bool _ShowPartNo;


        public string PurchaseSettingsId
        {
            get { return _PurchaseSettingsId; }
            set { _PurchaseSettingsId = value; }
        }
        public bool showSize
        {
            get { return _showSize; }
            set { _showSize = value; }
        }
        public bool showProductCode
        {
            get { return _showProductCode; }
            set { _showProductCode = value; }
        }
        public bool showProductDescription
        {
            get { return _showProductDescription; }
            set { _showProductDescription = value; }
        }
        public bool ActivateRetention
        {
            get { return _ActivateRetention; }
            set { _ActivateRetention = value; }
        }
        public int GridRowHeight
        {
            get { return _GridRowHeight; }
            set { _GridRowHeight = value; }
        }
        public bool ShowFreeQtyColumns
        {
            get { return _ShowFreeQtyColumns; }
            set { _ShowFreeQtyColumns = value; }
        }
        public int ProductNameColumnWidth
        {
            get { return _ProductNameColumnWidth; }
            set { _ProductNameColumnWidth = value; }
        }
        public int ProductDiscriptionColumnWidth
        {
            get { return _ProductDiscriptionColumnWidth; }
            set { _ProductDiscriptionColumnWidth = value; }
        }
        public bool VendorInvoiceChecking
        {
            get { return _VendorInvoiceChecking; }
            set { _VendorInvoiceChecking = value; }
        }
        public string branchId
        {
            get { return _branchId; }
            set { _branchId = value; }
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
        public bool TickPrintAfterSave
        {
            get { return _tickPrintAfterSave; }
            set { _tickPrintAfterSave = value; }
        }
        public bool DirectPrint
        {
            get { return _directPrint; }
            set { _directPrint = value; }
        }
        public bool ChangeSalesPricInPurcchase
        {
            get { return _ChangeSalesPricInPurcchase; }
            set { _ChangeSalesPricInPurcchase = value; }

        }
        public bool ShowPartNo
        {
            get { return _ShowPartNo; }
            set { _ShowPartNo = value; }
        }
        //public bool ShowAlternateCodeInLookup
        //{
        //    get { return _showAlternateCodeInLookup; }
        //    set { _showAlternateCodeInLookup = value; }
        //}
        public bool ShowLookupForBarcode
        {
            get { return _showLookupForBarcode; }
            set { _showLookupForBarcode = value; }
        }
    }
}
