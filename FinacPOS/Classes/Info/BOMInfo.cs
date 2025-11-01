using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for BOMInfo    
//</summary>    
namespace FinacPOS  
{    
class BOMInfo    
{    
    private string _bomId;    
    private string _productCode;    
    private string _rowMaterialId;    
    private decimal _quantity;    
    private string _unitId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;    
    private  string _productDetailsId;
    public string BomId    
    {    
        get { return _bomId; }    
        set { _bomId = value; }    
    }    
    public string ProductCode    
    {    
        get { return _productCode; }    
        set { _productCode = value; }    
    }    
    public string RowMaterialId    
    {    
        get { return _rowMaterialId; }    
        set { _rowMaterialId = value; }    
    }    
    public decimal Quantity    
    {    
        get { return _quantity; }    
        set { _quantity = value; }    
    }    
    public string UnitId    
    {    
        get { return _unitId; }    
        set { _unitId = value; }    
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
    public string ProductDetailsId
    {
        get { return _productDetailsId; }
        set { _productDetailsId = value; }
    }    
    
}    
}
