using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class SalesSettingsInfo
    {
        public static string _SaleSettingsId;
        public static bool _showSize;
        public static bool _showProductCode;
        public static bool _showProductDescription;
        public static bool _showPaymntInSalesOrder;
        public static string _QuotationValidity;
        public static string _QuotationDeliveredWithIn;
        public static string _QuotationPaymntTerms;
        public static int _GridRowHeight;
        public static bool _ShowFreeQtyColumns;
        public static string _branchId;
        public static DateTime _extraDate;
        public static string _extra1;
        public static string _extra2;
        public static bool _ActivateRetention;
        //Add YBN ON 04-03-2024
        public static int _ProductNameColumnWidth;
        public static int _ProductDiscriptionColumnWidth;
        public static string _SalesOrderTaxType;
        public static bool _tickPrintAfterSave;
        public static bool _directPrint;
        public static string _defaultBank;
        public static bool _showPaymentInReport;
        public static bool _ApplyDiscountLimit;
        public static bool _ShowBillProfit;
        public static string _salesInvoiceType;
        public static bool _showVehicleDetails;
        public static bool _ShowLineDiscount;
        public static string _AutomaticRoundOff;
        public static bool _AdvancePerc;
        public static bool _showPurchaseRate;
        public static bool _billDiscountPer;
        public static bool _ShowStockinLookup;
        public static bool _ActivatIntermediateSearch;
        public static bool _ShowPartNo;
        //public static bool _showAlternateCodeInLookup;
        public static bool _showLookupForBarcode;
       
        public bool ShowLineDiscount
        {
            get { return _ShowLineDiscount; }
            set { _ShowLineDiscount = value; }
        }
        public string AutomaticRoundOff
        {
            get { return _AutomaticRoundOff; }
            set { _AutomaticRoundOff = value; }
        }
        public string SaleSettingsId
        {
            get { return _SaleSettingsId; }
            set { _SaleSettingsId = value; }
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
        public bool showPaymntInSalesOrder
        {
            get { return _showPaymntInSalesOrder; }
            set { _showPaymntInSalesOrder = value; }
        }
        public string QuotationValidity
        {
            get { return _QuotationValidity; }
            set { _QuotationValidity = value; }
        }
        public string QuotationDeliveredWithIn
        {
            get { return _QuotationDeliveredWithIn; }
            set { _QuotationDeliveredWithIn = value; }
        }
        public string QuotationPaymntTerms
        {
            get { return _QuotationPaymntTerms; }
            set { _QuotationPaymntTerms = value; }
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
        public bool ActivateRetention
        {
            get { return _ActivateRetention; }
            set { _ActivateRetention = value; }
        }

        //Add YBN ON 04-03-2024
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
        public string SalesOrderTaxType
        {
            get { return _SalesOrderTaxType; }
            set { _SalesOrderTaxType = value; }
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
        public string DefaultBank
        {
            get { return _defaultBank; }
            set { _defaultBank = value; }
        }
        public bool ShowPaymentInReport
        {
            get { return _showPaymentInReport; }
            set { _showPaymentInReport = value; }
        }
        public bool ApplyDiscountLimit
        {
            get { return _ApplyDiscountLimit; }
            set { _ApplyDiscountLimit = value; }
        }
        public bool ShowBillProfit
        {
            get { return _ShowBillProfit; }
            set { _ShowBillProfit = value; }
        }
        public string SalesInvoiceType
        {
            get { return _salesInvoiceType; }
            set { _salesInvoiceType = value; }
        }
        public bool ShowVehicleDetails
        {
            get { return _showVehicleDetails; }
            set { _showVehicleDetails = value; }
        }
        public bool AdvancePerc
        {
            get { return _AdvancePerc; }
            set { _AdvancePerc = value; }
        }
        public bool ShowPurchaseRate
        {
            get { return _showPurchaseRate; }
            set { _showPurchaseRate = value; }
        }
        public bool BillDiscountPer
        {
            get { return _billDiscountPer; }
            set { _billDiscountPer = value; }
        }
        public bool ShowStockinLookup
        {
            get { return _ShowStockinLookup; }
            set { _ShowStockinLookup = value; }
        }
        public bool ActivatIntermediateSearch
        {
            get { return _ActivatIntermediateSearch; }
            set { _ActivatIntermediateSearch = value; }
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
