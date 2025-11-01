using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinacPOS
{
    class VanSaleSettingsInfo
    {
        public static string _VanSaleSettingsId;
        public static bool _DirectSalesPosting;
        public static string _SalesPostingDuration;
        public static bool _BillDiscountIncludeVat;
        public string _branchId;
        public string _extra1;
        public string _extra2;
        public static bool _EnableVanSale;
        public static bool _ShowPurchaseCostInProduct;

        public string VanSaleSettingsId
        {
            get { return _VanSaleSettingsId; }
            set { _VanSaleSettingsId = value; }
        }
        public bool DirectSalesPosting
        {
            get { return _DirectSalesPosting; }
            set { _DirectSalesPosting = value; }
        }
        public string SalesPostingDuration
        {
            get { return _SalesPostingDuration; }
            set { _SalesPostingDuration = value; }
        }
        public bool BillDiscountIncludeVat
        {
            get { return _BillDiscountIncludeVat; }
            set { _BillDiscountIncludeVat = value; }
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
        public string extra2
        {
            get { return _extra2; }
            set { _extra2 = value; }
        }
        public bool EnableVanSale
        {
            get { return _EnableVanSale; }
            set { _EnableVanSale = value; }
        }
        public bool ShowPurchaseCostInProduct
        {
            get { return _ShowPurchaseCostInProduct; }
            set { _ShowPurchaseCostInProduct = value; }
        }
    }
}
