using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for ComponentsSettingsInfo    
//</summary>    
namespace FinacPOS    
{    
class ComponentsSettingsInfo    
{    
    private string _componentSettingsId;    
    private string _productCode;    
    private string _componentId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    private string _productDetailsId;
    public string ComponentSettingsId    
    {    
        get { return _componentSettingsId; }    
        set { _componentSettingsId = value; }    
    }    
    public string ProductCode    
    {    
        get { return _productCode; }    
        set { _productCode = value; }    
    }    
    public string ComponentId    
    {    
        get { return _componentId; }    
        set { _componentId = value; }    
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
