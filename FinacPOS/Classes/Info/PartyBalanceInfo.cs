using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for PartyBalanceInfo    
//</summary>    
namespace FinacPOS    
{    
class PartyBalanceInfo    
{    
    private string _balanceId;    
    private DateTime _date;    
    private string _ledgerId;    
    private string _voucherType;    
    private string _voucherNo;    
    private string _againstVoucherType;    
    private string _againstvoucherNo;    
    private string _referenceType;    
    private decimal _debit;    
    private decimal _credit;    
    private bool _optional;    
    private int _creditPeriod;    
    private string _branchId;    
    private DateTime _extraDate;    
    private string _extra1;    
    private string _extra2;
    private string _currecyConversionId;
    private string _invoiceNo;
    private string _referenceNo;
    private Decimal _BillAmount;
    private DateTime _invoiceDate;
    private string _costCentreId;
    private Decimal _exchangeRate;
    private DateTime _exchangeDate;
    

    public string BalanceId    
    {    
        get { return _balanceId; }    
        set { _balanceId = value; }    
    }    
    public DateTime Date    
    {    
        get { return _date; }    
        set { _date = value; }    
    }    
    public string LedgerId    
    {    
        get { return _ledgerId; }    
        set { _ledgerId = value; }    
    }    
    public string VoucherType    
    {    
        get { return _voucherType; }    
        set { _voucherType = value; }    
    }    
    public string VoucherNo    
    {    
        get { return _voucherNo; }    
        set { _voucherNo = value; }    
    }    
    public string AgainstVoucherType    
    {    
        get { return _againstVoucherType; }    
        set { _againstVoucherType = value; }    
    }    
    public string AgainstvoucherNo    
    {    
        get { return _againstvoucherNo; }    
        set { _againstvoucherNo = value; }    
    }    
    public string ReferenceType    
    {    
        get { return _referenceType; }    
        set { _referenceType = value; }    
    }    
    public decimal Debit    
    {    
        get { return _debit; }    
        set { _debit = value; }    
    }    
    public decimal Credit    
    {    
        get { return _credit; }    
        set { _credit = value; }    
    }    
    public bool Optional    
    {    
        get { return _optional; }    
        set { _optional = value; }    
    }    
    public int CreditPeriod    
    {    
        get { return _creditPeriod; }    
        set { _creditPeriod = value; }    
    }    
    public string BranchId    
    {    
        get { return _branchId; }    
        set { _branchId = value; }    
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
    public string CurrencyConversionId
    {
        get { return _currecyConversionId; }
        set { _currecyConversionId = value; }
    }
    public string  invoiceNo
    {
        get { return _invoiceNo; }
        set { _invoiceNo = value; }
    }
    public string referenceNo
    {
        get { return _referenceNo; }
        set { _referenceNo = value; }
    }
    public  Decimal BillAmount
    {
        get { return _BillAmount; }
        set { _BillAmount = value; }
    }
    public DateTime invoiceDate
    {
        get { return _invoiceDate; }
        set { _invoiceDate = value; }
    }
    public string costCentreId
    {
        get { return _costCentreId; }
        set { _costCentreId = value; }
    }
    public Decimal exchangeRate
    {
        get { return _exchangeRate; ; }
        set { _exchangeRate = value; }
    }

    public DateTime exchangeDate
    {
        get { return _exchangeDate; ; }
        set { _exchangeDate = value; }
    }
}    
}
