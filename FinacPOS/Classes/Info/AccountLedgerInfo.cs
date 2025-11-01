using System;    
using System.Collections.Generic;    
using System.Text;    
//<summary>    
//Summary description for AccountLedgerInfo    
//</summary>    
namespace FinacPOS
{
    class AccountLedgerInfo
    {
        private string _ledgerId;
        private string _ledgerName;
        private string _groupId;
        private bool _affectInventory;
        private decimal _openingBalance;
        private string _crOrDr;
        private string _narration;
        private string _name;
        private string _accountNo;
        private string _address;
        private string _phoneNo;
        private string _faxNo;
        private string _email;
        private int _creditPeriod;
        private decimal _creditLimit;
        private string _pricingLevelId;
        private string _currencyId;
        private bool _interestOrNot;
        private string _branchId;
        private string _marketId;
        private bool _defaultt;
        private string _tinNumber;
        private string _cstNumber;
        private string _panNumber;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private string _areaId;
        private string _ledgerCode;

        private string _buildingNo;
        private string _additionalNo;
        private string _streetName;
        private string _district;
        private string _postboxNo;
        private string _cityName;
        private string _country;

        private string _buildingNoArb;
        private string _additionalNoArb;
        private string _streetNameArb;
        private string _districtArb;
        private string _postboxNoArb;
        private string _cityNameArb;
        private string _countryArb;

        private string _creditLimitStatus;

        private string _bankname;
        private string _ibanno;
        private string _bankaccname;

        private string _bankSwiftCode;
        private string _bankBranchName;
        private string _AddressArabic;
        private string _ledgerType;
        private string _routeId;
        private string _Userdef1;
        private string _Userdef2;
        private string _Userdef3;
        private string _Userdef4;

        public string LedgerId
        {
            get { return _ledgerId; }
            set { _ledgerId = value; }
        }
        public string LedgerName
        {
            get { return _ledgerName; }
            set { _ledgerName = value; }
        }
        public string GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }
        public bool BillByBill
        {
            get { return _affectInventory; }
            set { _affectInventory = value; }
        }
        public decimal OpeningBalance
        {
            get { return _openingBalance; }
            set { _openingBalance = value; }
        }
        public string CrOrDr
        {
            get { return _crOrDr; }
            set { _crOrDr = value; }
        }
        public string Narration
        {
            get { return _narration; }
            set { _narration = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string AccountNo
        {
            get { return _accountNo; }
            set { _accountNo = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string PhoneNo
        {
            get { return _phoneNo; }
            set { _phoneNo = value; }
        }
        public string FaxNo
        {
            get { return _faxNo; }
            set { _faxNo = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public int CreditPeriod
        {
            get { return _creditPeriod; }
            set { _creditPeriod = value; }
        }
        public decimal CreditLimit
        {
            get { return _creditLimit; }
            set { _creditLimit = value; }
        }
        public string PricingLevelId
        {
            get { return _pricingLevelId; }
            set { _pricingLevelId = value; }
        }
        public string CurrencyId
        {
            get { return _currencyId; }
            set { _currencyId = value; }
        }
        public bool InterestOrNot
        {
            get { return _interestOrNot; }
            set { _interestOrNot = value; }
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
        public bool Default
        {
            get { return _defaultt; }
            set { _defaultt = value; }
        }
        public string MarketId
        {
            get { return _marketId; }
            set { _marketId = value; }
        }
        public string TinNumber
        {
            get { return _tinNumber; }
            set { _tinNumber = value; }
        }
        public string CstNumber
        {
            get { return _cstNumber; }
            set { _cstNumber = value; }
        }
        public string PanNumber
        {
            get { return _panNumber; }
            set { _panNumber = value; }
        }
        public string AreaId
        {
            get { return _areaId; }
            set { _areaId  = value; }
        }
        public string LedgerCode
        {
            get { return _ledgerCode; }
            set { _ledgerCode = value; }
        }

        public string BuildingNo
        {
            get { return _buildingNo; }
            set { _buildingNo = value; }
        }
        public string AdditionalNo
        {
            get { return _additionalNo; }
            set { _additionalNo = value; }
        }
        public string StreetName
        {
            get { return _streetName; }
            set { _streetName = value; }
        }
        public string District
        {
            get { return _district; }
            set { _district = value; }
        }
        public string PostboxNo
        {
            get { return _postboxNo; }
            set { _postboxNo = value; }
        }
        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }
        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string BuildingNoArb
        {
            get { return _buildingNoArb; }
            set { _buildingNoArb = value; }
        }
        public string AdditionalNoArb
        {
            get { return _additionalNoArb; }
            set { _additionalNoArb = value; }
        }
        public string StreetNameArb
        {
            get { return _streetNameArb; }
            set { _streetNameArb = value; }
        }
        public string DistrictArb
        {
            get { return _districtArb; }
            set { _districtArb = value; }
        }
        public string PostboxNoArb
        {
            get { return _postboxNoArb; }
            set { _postboxNoArb = value; }
        }
        public string CityNameArb
        {
            get { return _cityNameArb; }
            set { _cityNameArb = value; }
        }
        public string CountryArb
        {
            get { return _countryArb; }
            set { _countryArb = value; }
        }

        public string CreditLimitStatus
        {
            get { return _creditLimitStatus; }
            set { _creditLimitStatus = value; }
        }

        public string bankname
        {
            get { return _bankname; }
            set { _bankname = value; }
        }
        public string ibanno
        {
            get { return _ibanno; }
            set { _ibanno = value; }
        }
        public string bankaccname
        {
            get { return _bankaccname; }
            set { _bankaccname = value; }
        }
        public string BankSwiftCode
        {
            get { return _bankSwiftCode; }
            set { _bankSwiftCode = value; }
        }
        public string BankBranchName
        {
            get { return _bankBranchName; }
            set { _bankBranchName = value; }
        }
        public string AddressArabic
        {
            get { return _AddressArabic; }
            set { _AddressArabic = value; }
        }
        public string LedgerType
        {
            get { return _ledgerType; }
            set { _ledgerType = value; }
        }
        public string routeId
        {
            get { return _routeId; }
            set { _routeId = value; }
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

    }
}
