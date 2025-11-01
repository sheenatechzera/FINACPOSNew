using System;
using System.Collections.Generic;
using System.Text;   
//<summary>
//Summary description for ProductDetailsInfo
//</summary>
public class ProductDetailsInfo
{
    private string _productDetailsId;
    private string _productCode;
    private string _branchId;
    private decimal _purchaseRate;
    private decimal _salesRate;
    private decimal _mrp;
    private string _taxId;
    private decimal _minimumStock;
    private decimal _maximunStock;
    private decimal _reorderLevel;
    private decimal _openingStock;
    private string _taxType;
    private string _narration;
    private bool _allowBatch;
    private bool _bom;
    private bool _allowComponentSale;
    private bool _active;
    private bool _showReminder;
    private DateTime _extraDate;
    private string _extra1;
    private string _extra2;
    private bool _gatePass;
    private decimal _fixedSalesRate;
    private string _category;
    private string _groupCode;
    private bool _IsVanSaleProduct;
    private bool _ShowExpiry;
    private int _ExpiryDays;
    private decimal _PurchaseRatePer;
    private bool _withOutBarcode;
    public string Ingredients { get; set; }
    public bool NutritionFact { get; set; }
    public string NutritionName { get; set; }
    public string NutritionDetails { get; set; }
    public string Location { get; set; }
    public string alternateNo { get; set; }
    public string ProductDetailsId
    {
        get { return _productDetailsId; }
        set { _productDetailsId = value; }
    }
    public string ProductCode
    {
        get { return _productCode; }
        set { _productCode = value; }
    }
    public string BranchId
    {
        get { return _branchId; }
        set { _branchId = value; }
    }
    public decimal PurchaseRate
    {
        get { return _purchaseRate; }
        set { _purchaseRate = value; }
    }
    public decimal SalesRate
    {
        get { return _salesRate; }
        set { _salesRate = value; }
    }
    public decimal Mrp
    {
        get { return _mrp; }
        set { _mrp = value; }
    }
    public string TaxId
    {
        get { return _taxId; }
        set { _taxId = value; }
    }
    public decimal MinimumStock
    {
        get { return _minimumStock; }
        set { _minimumStock = value; }
    }
    public decimal MaximunStock
    {
        get { return _maximunStock; }
        set { _maximunStock = value; }
    }
    public decimal ReorderLevel
    {
        get { return _reorderLevel; }
        set { _reorderLevel = value; }
    }
    public decimal OpeningStock
    {
        get { return _openingStock; }
        set { _openingStock = value; }
    }
    public string TaxType
    {
        get { return _taxType; }
        set { _taxType = value; }
    }
    public string Narration
    {
        get { return _narration; }
        set { _narration = value; }
    }
    public bool AllowBatch
    {
        get { return _allowBatch; }
        set { _allowBatch = value; }
    }
    public bool Bom
    {
        get { return _bom; }
        set { _bom = value; }
    }
    public bool AllowComponentSale
    {
        get { return _allowComponentSale; }
        set { _allowComponentSale = value; }
    }
    public bool Active
    {
        get { return _active; }
        set { _active = value; }
    }
    public bool ShowReminder
    {
        get { return _showReminder; }
        set { _showReminder = value; }
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
    public bool GatePass
    {
        get { return _gatePass; }
        set { _gatePass = value; }
    }
    public decimal FixedSalesRate
    {
        get { return _fixedSalesRate ; }
        set { _fixedSalesRate = value; }
    }
    public string Category
    {
        get { return _category; }
        set { _category = value; }
    }
    public string GroupCode
    {
        get { return _groupCode; }
        set { _groupCode = value; }
    }
    public bool IsVanSaleProduct
    {
        get { return _IsVanSaleProduct; }
        set { _IsVanSaleProduct = value; }
    }
    public bool ShowExpiry
    {
        get { return _ShowExpiry; }
        set { _ShowExpiry = value; }
    }
    public int ExpiryDays
    {
        get { return _ExpiryDays; }
        set { _ExpiryDays = value; }
    }
    public decimal PurchaseRatePer
    {
        get { return _PurchaseRatePer; }
        set { _PurchaseRatePer = value; }
    }
    public bool WithOutBarcode
    {
        get { return _withOutBarcode; }
        set { _withOutBarcode = value; }
    }
}
