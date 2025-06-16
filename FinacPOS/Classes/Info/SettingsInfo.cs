using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for SettingsInfo    
//</summary>    
namespace FinacPOS
{
    class SettingsInfo
    {
        string _branchId;
       
        public static string _settingsId;

        public static bool _budget;
        public static bool _payRoll;
        public static bool _tax;
        public static bool _multiCurrency;
        public static bool _billByBill;
        public static bool _interestCalculation;
        public static bool _showDiscountPercentage;
        public static bool _freeQuantity;
        public static bool _automaticProductCodeGeneration;
        public static bool _barcodeGeneration;
        public static bool _maintainBatch; 
        public static string _expiryReminder;
        public static bool _maintainGodown;   
        public static bool _suffixPrefix;
        public static bool _lowStockReminderPopUp;
       
        public static string _printer;
        public static string _stockCalculatingMethod;
        public static string _negativeStockStatus;
        public static int _purchaseReturnLimit;
        public static int _salesReturnLimit;
        public static bool _directPrinting;
        public static DateTime _extraDate;
        public static string _extra1;
        public static string _extra2;
        public static bool _currencySuffix;
        public static string _negativeCashTransaction;
        public static bool _allowRack;
        public static bool _messageBoxAddEdit;
        public static bool _messageBoxDelete;
        public static bool _messageBoxClose;
        public static bool _messageBoxRowRemove;
        public static string _formType;// = "Sales Invoice-No Tax";
        public static string _taxType;// = "Applicable to product"; //NA  Applicable to bill  Applicable to product
        public static bool _showPurchaseRate; // by sumana to show or hise purchase rate column in slaes invoice
        public static bool _showMRP;
        public static bool _showUnit;
        public static bool _showDiscountAmount;
        public static bool _tickPrintAfterSave;
        public static bool _showProductCode;
        public static bool _showCompanyForEstimatePrint;
        public static int _paperOut;
        public static bool _showBrand;
        public static bool _showSalesmanInPrint;
        public static bool _vehicle; 
        public static bool _vatIncluded; 
        public static bool _vatCessIncluded;
        public static bool _billDiscountPer;
        public static bool _profit;
        public static bool _profitPercentage;
        public static bool _totalProfit;
        public static bool _productDescription;
        public static bool _roundOff;
        public static int _roundDecimal;
        public static string _roundDecimalPart;
        public static bool _showCompanyHeader;
        public static bool _showCompanyFooter;
        public static byte[] _companyHeader;
        public static byte[] _companyFooter;

        public string SettingsId
        {
            get { return _settingsId; }
            set { _settingsId = value; }
        }
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        public bool Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }
        public bool PayRoll
        {
            get { return _payRoll; }
            set { _payRoll = value; }
        }
        public bool Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }
        public bool MultiCurrency
        {
            get { return _multiCurrency; }
            set { _multiCurrency = value; }
        }
        public bool BillByBill
        {
            get { return _billByBill; }
            set { _billByBill = value; }
        }
        public bool InterestCalculation
        {
            get { return _interestCalculation; }
            set { _interestCalculation = value; }
        }
        public bool ShowDiscountPercentage 
        {
            get { return _showDiscountPercentage; }
            set { _showDiscountPercentage = value; }
        }
        public bool FreeQuantity
        {
            get { return _freeQuantity; }
            set { _freeQuantity = value; }
        }
        public bool AutomaticProductCodeGeneration
        {
            get { return _automaticProductCodeGeneration; }
            set { _automaticProductCodeGeneration = value; }
        }
        public bool BarcodeGeneration
        {
            get { return _barcodeGeneration; }
            set { _barcodeGeneration = value; }
        }
        public bool MaintainBatch
        {
            get { return _maintainBatch; }
            set { _maintainBatch = value; }
        }
        public string ExpiryReminder
        {
            get { return _expiryReminder; }
            set { _expiryReminder = value; }
        }
        public bool MaintainGodown
        {
            get { return _maintainGodown; }
            set { _maintainGodown = value; }
        }
        public bool SuffixPrefix
        {
            get { return _suffixPrefix; }
            set { _suffixPrefix = value; }
        }
        public bool LowStockReminderPopUp
        {
            get { return _lowStockReminderPopUp; }
            set { _lowStockReminderPopUp = value; }
        }
     
        public string Printer
        {
            get { return _printer; }
            set { _printer = value; }
        }
        public string StockCalculatingMethod
        {
            get { return _stockCalculatingMethod; }
            set { _stockCalculatingMethod = value; }
        }
        public string NegativeStockStatus
        {
            get { return _negativeStockStatus; }
            set { _negativeStockStatus = value; }
        }
        public int PurchaseReturnLimit
        {
            get { return _purchaseReturnLimit; }
            set { _purchaseReturnLimit = value; }
        }
        public int SalesReturnLimit
        {
            get { return _salesReturnLimit; }
            set { _salesReturnLimit = value; }
        }
        public bool DirectPrinting
        {
            get { return _directPrinting; }
            set { _directPrinting = value; }
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
    
        public bool CurrencySuffix
        {
            get { return _currencySuffix; }
            set { _currencySuffix = value; }
        }
   
        public string NegativeCashTransaction
        {
            get { return _negativeCashTransaction; }
            set { _negativeCashTransaction = value; }
        }
  
        public bool AllowRack
        {
            get { return _allowRack; }
            set { _allowRack = value; }
        }
        public bool MessageBoxAddEdit
        {
            get { return _messageBoxAddEdit; }
            set { _messageBoxAddEdit = value; }
        }
        public bool MessageBoxDelete
        {
            get { return _messageBoxDelete; }
            set { _messageBoxDelete = value; }
        }
        public bool MessageBoxClose
        {
            get { return _messageBoxClose; }
            set { _messageBoxClose = value; }
        }
        public bool MessageBoxRowRemove
        {
            get { return _messageBoxRowRemove; }
            set { _messageBoxRowRemove = value; }
        }
        public string FormType
        {
            get { return _formType; }
            set { _formType = value; }
        }
            public string TaxType
        {
            get { return _taxType; }
            set { _taxType = value; }
        }
        public bool ShowPurchaseRate
        {
            get { return _showPurchaseRate; }
            set { _showPurchaseRate = value; }
        }
        public bool ShowMRP
        {
            get { return _showMRP ; }
            set { _showMRP = value; }
        }

        public bool ShowUnit
        {
            get { return _showUnit; }
            set { _showUnit = value; }
        }
        public bool ShowDiscountAmount
        {
            get { return _showDiscountAmount; }
            set { _showDiscountAmount = value; }
        }
        public bool TickPrintAfterSave
        {
            get { return _tickPrintAfterSave; }
            set { _tickPrintAfterSave = value; }
        }
        public bool ShowProductCode
        {
            get { return _showProductCode; }
            set { _showProductCode = value; }
        }


        public bool ShowCompanyForEstimatePrint
        {
            get { return _showCompanyForEstimatePrint; }
            set { _showCompanyForEstimatePrint = value; }
        }
        public int PaperOut
        {
            get { return _paperOut; }
            set { _paperOut = value; }
        }
        public bool  ShowBrand
        {
            get { return _showBrand ; }
            set { _showBrand = value; }
        }
        public bool ShowSalesmanInPrint
        {
            get { return _showSalesmanInPrint; }
            set { _showSalesmanInPrint = value; }
        }
        public bool Vehicle
        {
            get { return _vehicle; }
            set { _vehicle = value; }
        }
        public bool VatIncluded
        {
            get { return _vatIncluded; }
            set { _vatIncluded = value; }
        }
        public bool VatCessIncluded
        {
            get { return _vatCessIncluded ; }
            set { _vatCessIncluded = value; }
        }
        public bool BillDiscountPer
        {
            get { return _billDiscountPer; }
            set { _billDiscountPer = value; }
        }
        public bool Profit
        {
            get { return _profit; }
            set { _profit = value; }
        }
        public bool ProfitPercentage
        {
            get { return _profitPercentage; }
            set { _profitPercentage = value; }
        }
        public bool TotalProfit
        {
            get { return _totalProfit; }
            set { _totalProfit = value; }
        }
        public bool ProductDescription
        {
            get { return _productDescription; }
            set { _productDescription = value; }
        }
        public bool RoundOff
        {
            get { return _roundOff; }
            set { _roundOff = value; }
        }
        public int RoundDecimal
        {
            get { return _roundDecimal; }
            set { _roundDecimal = value; }
        }
        public string RoundDecimalPart
        {
            get { return _roundDecimalPart; }
            set { _roundDecimalPart = value; }
        }
        public byte[] CompanyHeader
        {
            get { return _companyHeader; }
            set { _companyHeader = value; }
        }
        public byte[] CompanyFooter
        {
            get { return _companyFooter; }
            set { _companyFooter = value; }
        }
        public bool ShowCompanyHeader
        {
            get { return _showCompanyHeader; }
            set { _showCompanyHeader = value; }
        }
        public bool ShowCompanyFooter
        {
            get { return _showCompanyFooter; }
            set { _showCompanyFooter = value; }
        }
    }
}
