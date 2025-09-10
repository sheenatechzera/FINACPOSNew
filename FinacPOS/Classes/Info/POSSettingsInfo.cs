using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class POSSettingsInfo
    {
        public static string _POSSettingsId;
        public static bool _BillClearAuth;
        public static bool _DiscountAuth;
        public static bool _HoldBillAuth;
        public static bool _PriceChangeAuth;
        public static bool _CreditSalesAuth;
        public static bool _ZeroStockAuth;
        public static bool _ItemGroupinginPrint;
        public static bool _lastBillPrintAuth;
        public static bool _ExchangeItemAuth;
        public static bool _CashBoxOpenAuth;
        public static bool _QtyChangeAuth;
        public static int _CNDays;
        public static string _POSCompanyName;
        public static string _POSCompanyNameArabic;
        public static string _POSAddress;
        public static string _POSPhone;
        public static int _CompanyH;
        public static int _CompanyArabicH;
        public static int _AddressH;
        public static int _PhoneH;
        public static bool _CompanyVisible;
        public static bool _CompanyArabicVisible;
        public static bool _AddressVisible;
        public static bool _PhoneVisible;
        public static bool _ShowCustBalinPrint;
        public static string _custBillCopy;
        public  static bool _AddQtyInSameBarcodeToGrid;
        public static bool _ShowProductSummaryInSessionClose;
        public static bool _ActiveTableManage;
        public static bool _StockView;
        public static bool _SessionManagmentByAdmin;
        public static bool _AlwaysEnableHoldBillView;
        public static bool _BlockZeroPriceInSales;
        public static string _ZeroQtyAlert;
        public static string _DeleteMode;
        public static string _StartingTokenNo;
        public string POSSettingsId
        {
            get { return _POSSettingsId; }
            set { _POSSettingsId = value; }
        }
        public bool BillClearAuth
        {
            get { return _BillClearAuth; }
            set { _BillClearAuth = value; }
        }
        public bool DiscountAuth
        {
            get { return _DiscountAuth; }
            set { _DiscountAuth = value; }
        }
        public bool HoldBillAuth
        {
            get { return _HoldBillAuth; }
            set { _HoldBillAuth = value; }
        }
        public bool PriceChangeAuth
        {
            get { return _PriceChangeAuth; }
            set { _PriceChangeAuth = value; }
        }
        public bool CreditSalesAuth
        {
            get { return _CreditSalesAuth; }
            set { _CreditSalesAuth = value; }
        }
        public bool ZeroStockAuth
        {
            get { return _ZeroStockAuth; }
            set { _ZeroStockAuth = value; }
        }
        public bool ItemGroupinginPrint
        {
            get { return _ItemGroupinginPrint; }
            set { _ItemGroupinginPrint = value; }
        }
        public bool lastBillPrintAuth
        {
            get { return _lastBillPrintAuth; }
            set { _lastBillPrintAuth = value; }
        }
        public bool ExchangeItemAuth
        {
            get { return _ExchangeItemAuth; }
            set { _ExchangeItemAuth = value; }
        }
        public bool CashBoxOpenAuth
        {
            get { return _CashBoxOpenAuth; }
            set { _CashBoxOpenAuth = value; }
        }
        public bool QtyChangeAuth
        {
            get { return _QtyChangeAuth; }
            set { _QtyChangeAuth = value; }
        }
        public int CNDays 
        {
            get { return _CNDays; }
            set { _CNDays = value; }
        }
        public string POSCompanyName
        {
            get { return _POSCompanyName; }
            set { _POSCompanyName = value; }
        }
        public string POSCompanyNameArabic
        {
            get { return _POSCompanyNameArabic; }
            set { _POSCompanyNameArabic = value; }
        }
        public string POSAddress
        {
            get { return _POSAddress; }
            set { _POSAddress = value; }
        }
        public string POSPhone
        {
            get { return _POSPhone; }
            set { _POSPhone = value; }
        }
        public int CompanyH
        {
            get { return _CompanyH; }
            set { _CompanyH = value; }
        }
        public int CompanyArabicH
        {
            get { return _CompanyArabicH; }
            set { _CompanyArabicH = value; }
        }
        public int AddressH
        {
            get { return _AddressH; }
            set { _AddressH = value; }
        }
        public int PhoneH
        {
            get { return _PhoneH; }
            set { _PhoneH = value; }
        }
        public bool CompanyVisible
        {
            get { return _CompanyVisible; }
            set { _CompanyVisible = value; }
        }
        public bool CompanyArabicVisible
        {
            get { return _CompanyArabicVisible; }
            set { _CompanyArabicVisible = value; }
        }
        public bool AddressVisible
        {
            get { return _AddressVisible; }
            set { _AddressVisible = value; }
        }
        public bool PhoneVisible
        {
            get { return _PhoneVisible; }
            set { _PhoneVisible = value; }
        }
        public bool ShowCustBalinPrint
        {
            get { return _ShowCustBalinPrint; }
            set { _ShowCustBalinPrint = value; }
        }
        public string CustBillCopy
        {
            get { return _custBillCopy; }
            set { _custBillCopy = value; }
        }
        
          public bool AddQtyInSameBarcodeToGrid
          {
            get { return _AddQtyInSameBarcodeToGrid; }
            set { _AddQtyInSameBarcodeToGrid = value; }
        }
          public bool ShowProductSummaryInSessionClose
          {
              get { return _ShowProductSummaryInSessionClose; }
              set { _ShowProductSummaryInSessionClose = value; }
          }

          public bool ActiveTableManage
          {
              get { return _ActiveTableManage; }
              set { _ActiveTableManage = value; }
          }
        public bool StockView
        {
            get { return _StockView; }
            set { _StockView = value; }
        }
        public bool SessionManagmentByAdmin
        {
            get { return _SessionManagmentByAdmin; }
            set { _SessionManagmentByAdmin = value; }
        }
        public bool AlwaysEnableHoldBillView
        {
            get { return _AlwaysEnableHoldBillView; }
            set { _AlwaysEnableHoldBillView = value; }
        }
        public bool BlockZeroPriceInSales
        {
            get { return _BlockZeroPriceInSales; }
            set { _BlockZeroPriceInSales = value; }
        }
        public string ZeroQtyAlert
        {
            get { return _ZeroQtyAlert; }
            set { _ZeroQtyAlert = value; }
        }
        public string DeleteMode
        {
            get { return _DeleteMode; }
            set { _DeleteMode = value; }
        }
        public string StartingTokenNo
        {
            get { return _StartingTokenNo; }
            set { _StartingTokenNo = value; }
        }
    }
}
