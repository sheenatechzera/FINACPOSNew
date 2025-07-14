using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public  string _branchId;
        public string _extra1;
        public static  bool _AccountsPosting;
        public static string _ZatcaType;
        public static int _roundDecimal;
        public static string _roundDecimalPart;
        public static bool _EnablePOS;

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
    }
}
