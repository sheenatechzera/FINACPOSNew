using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class UserGroupSettingsInfo
    {
        private string _userGroupSettingId;
        private string _userGroupId;
        private string _settingsId;
        private string _formName;
        private bool _addBit;
        private bool _editBit;
        private bool _deleteBit;
        private bool _postBit;
        private string _branchId;
        private DateTime _extraDate;
        private string _extra1;
        private string _extra2;
        private bool _viewBit;

        public string UserGroupSettingId
        {
            get { return _userGroupSettingId; }
            set { _userGroupSettingId = value; }
        }
        public string UserGroupId
        {
            get { return _userGroupId; }
            set { _userGroupId = value; }
        }
        public string SettingsId
        {
            get { return _settingsId; }
            set { _settingsId = value; }
        }
        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }
        public bool AddBit
        {
            get { return _addBit; }
            set { _addBit = value; }
        }
        public bool EditBit
        {
            get { return _editBit; }
            set { _editBit = value; }
        }
        public bool DeleteBit
        {
            get { return _deleteBit; }
            set { _deleteBit = value; }
        }
        public bool PostBit
        {
            get { return _postBit; }
            set { _postBit = value; }
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
        public bool ViewBit
        {
            get { return _viewBit; }
            set { _viewBit = value; }
        }
    }
}
