using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class InventorySettingsInfo
    {
        public static string _InventorySettingsId;
        public static bool _ShowAllTransctn;
        public static int _gridColumnHeight;
        public static bool _showSize;
        public static bool _SalePriceUpdateByCostPerc;

        public static string _expiryReminder;
        public static bool _lowStockReminderPopUp;
        public static string _stockCalculatingMethod;
        public static string _negativeStockStatus;
        public static string _expiryStatus;
        public static bool _roundOff;
        public static bool _maintainGodown;
        public static bool _maintainRack;
        public static bool _messageBoxAddEdit;
        public static bool _messageBoxDelete;
        public static bool _messageBoxClose;
        public static bool _messageBoxRowRemove;
        public static bool _messageBoxPrint;
        public static bool _messageBoxClear;
        public static bool _ShowBalanceLabel;
        public static bool _CurrencySuffix;
        public static bool _showCompanyForEstimatePrint;
        public static int _paperOut;
        public static bool _showSalesmanInPrint;
        public static bool _vehicle;    
        public static string _printer;
        public static bool _showPartNoInLookup;
        public static bool _showAlternateCodeInLookup;
        public static bool _showLocationInLookUp;
        public  static string _BackupPath1;
        public static string _BackupPath2;
        public static bool _ShowGodownWiseStock;
        public string InventorySettingsId
        {
            get { return _InventorySettingsId; }
            set { _InventorySettingsId = value; }
        }
        public bool ShowAllTransctn
        {
            get { return _ShowAllTransctn; }
            set { _ShowAllTransctn = value; }
        }
        public int GridColumnHeight
        {
            get { return _gridColumnHeight; }
            set { _gridColumnHeight = value; }
        }
        public bool ShowSize
        {
            get { return _showSize; }
            set { _showSize = value; }
        }
        public bool SalePriceUpdateByCostPerc
        {
            get { return _SalePriceUpdateByCostPerc; }
            set { _SalePriceUpdateByCostPerc = value; }
        }
        public string ExpiryReminder
        {
            get { return _expiryReminder; }
            set { _expiryReminder = value; }
        }
        public bool LowStockReminderPopUp
        {
            get { return _lowStockReminderPopUp; }
            set { _lowStockReminderPopUp = value; }
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
        public string ExpiryStatus
        {
            get { return _expiryStatus; }
            set { _expiryStatus = value; }
        }
        public bool RoundOff
        {
            get { return _roundOff; }
            set { _roundOff = value; }
        }
        public bool maintainGodown
        {
            get { return _maintainGodown; }
            set { _maintainGodown = value; }
        }
        public bool maintainRack
        {
            get { return _maintainRack; }
            set { _maintainRack = value; }
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
        public bool MessageBoxPrint
        {
            get { return _messageBoxPrint; }
            set { _messageBoxPrint = value; }
        }
        public bool MessageBoxClear
        {
            get { return _messageBoxClear; }
            set { _messageBoxClear = value; }
        }
        public bool ShowBalanceLabel
        {
            get { return _ShowBalanceLabel; }
            set { _ShowBalanceLabel = value; }
        }
        public bool CurrencySuffix
        {
            get { return _CurrencySuffix; }
            set { _CurrencySuffix = value; }
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
      
      
        public string Printer
        {
            get { return _printer; }
            set { _printer = value; }
        }
        public bool showPartNoInLookup
        {
            get { return _showPartNoInLookup; }
            set { _showPartNoInLookup = value; }
        }
        public bool showAlternateCodeInLookup
        {
            get { return _showAlternateCodeInLookup; }
            set { _showAlternateCodeInLookup = value; }
        }
        public bool showLocationInLookUp
        {
            get { return _showLocationInLookUp; }
            set { _showLocationInLookUp = value; }
        }
        public string BackupPath1
        {
            get { return _BackupPath1; }
            set { _BackupPath1 = value; }
        }
        public string BackupPath2
        {
            get { return _BackupPath2; }
            set { _BackupPath2 = value; }
        }
        public bool ShowGodownWiseStock
        {
            get { return _ShowGodownWiseStock; }
            set { _ShowGodownWiseStock = value; }
        }
    }
}
