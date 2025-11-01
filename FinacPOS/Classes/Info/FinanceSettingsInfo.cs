using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Filtering.Templates;

namespace FinacPOS
{
    class FinanceSettingsInfo
    {
        public static int _FinanceSettingsId;
        public static bool _ActivateTax;
        public static bool _BillByBill;
        public static bool _ActivateInterstCalc;
        public static bool _UseMultiCurrency;
        public static bool _SufPrefixVoucherNoGen;
        public static bool _ActivateCosteCentre;
        public static string _NegativeCashTransaction;
        public static string _AccountsCalcMethod;
        public static string _TaxType;
        public static string _FormType;
        public static bool _VatIncluded;
        public static bool _VatandCessIncluded;
        public string _branchId;
        public string _extra1;
        public static bool _AccountsPosting;
        public static string _ZatcaType;
        public static bool _showCompanyHeader;
        public static bool _showCompanyFooter;
        public static byte[] _companyHeader;
        public static byte[] _companyFooter;
        public static int _roundDecimal;
        public static string _roundDecimalPart;
        public static bool _EnablePOS;
        public static string _Userdef1;
        public static string _Userdef2;
        public static string _Userdef3;
        public static string _Userdef4;
        public static string _DateFormat;
        public static string _paymentMode;
        public static bool _showReminder;
        public static bool _validateVatNumber;


        public int FinanceSettingsId
        {
            get { return _FinanceSettingsId; }
            set { _FinanceSettingsId = value; }
        }
        public bool ActivateTax
        {
            get { return _ActivateTax; }
            set { _ActivateTax = value; }
        }
        public bool BillByBill
        {
            get { return _BillByBill; }
            set { _BillByBill = value; }
        }
        public bool ActivateInterstCalc
        {
            get { return _ActivateInterstCalc; }
            set { _ActivateInterstCalc = value; }
        }
        public bool UseMultiCurrency
        {
            get { return _UseMultiCurrency; }
            set { _UseMultiCurrency = value; }
        }
        public bool SufPrefixVoucherNoGen
        {
            get { return _SufPrefixVoucherNoGen; }
            set { _SufPrefixVoucherNoGen = value; }
        }
        public bool ActivateCosteCentre
        {
            get { return _ActivateCosteCentre; }
            set { _ActivateCosteCentre = value; }
        }
        public string NegativeCashTransaction
        {
            get { return _NegativeCashTransaction; }
            set { _NegativeCashTransaction = value; }
        }
        public string AccountsCalcMethod
        {
            get { return _AccountsCalcMethod; }
            set { _AccountsCalcMethod = value; }
        }
        public string TaxType
        {
            get { return _TaxType; }
            set { _TaxType = value; }
        }
        public string FormType
        {
            get { return _FormType; }
            set { _FormType = value; }
        }
        public bool VatIncluded
        {
            get { return _VatIncluded; }
            set { _VatIncluded = value; }
        }
        public bool VatandCessIncluded
        {
            get { return _VatandCessIncluded; }
            set { _VatandCessIncluded = value; }
        }
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        public string extra1
        {
            get { return _extra1; }
            set { _extra1 = value; }
        }
        public bool AccountsPosting
        {
            get { return _AccountsPosting; }
            set { _AccountsPosting = value; }
        }
        public string ZatcaType
        {
            get { return _ZatcaType; }
            set { _ZatcaType = value; }
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
        public bool EnablePOS
        {
            get { return _EnablePOS; }
            set { _EnablePOS = value; }
        }
        public string Userdef1
        {
            get { return _Userdef1; }
            set { _Userdef1 = value; }
        }
        public string Userdef2
        {
            get { return _Userdef2; }
            set { _Userdef2 = value; }
        }
        public string Userdef3
        {
            get { return _Userdef3; }
            set { _Userdef3 = value; }
        }
        public string Userdef4
        {
            get { return _Userdef4; }
            set { _Userdef4 = value; }
        }
        public string DateFormat
        {
            get { return _DateFormat; }
            set { _DateFormat = value; }
        }
        public bool ShowReminder
        {
            get { return _showReminder; }
            set { _showReminder = value; }
        }
        public string PaymentMode
        {
            get { return _paymentMode; }
            set { _paymentMode = value; }
        }
        public bool ValidateVatNumber
        {
            get { return _validateVatNumber; }
            set { _validateVatNumber = value; }
        }
    }
}
