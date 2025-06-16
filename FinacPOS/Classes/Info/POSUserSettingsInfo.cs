using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class POSUserSettingsInfo
    {
        int _settingsCopyId;
        int _settingsId;
        string _userGroup;
        string _Menu;
        string _subMenu;
        string _menuStripName;
        string _formName;
        bool _status;
        string _branchId;
        DateTime _extraDate;
        string _extra1;
        string _extra2;

        public int SettingsCopyId
        {
            get { return _settingsCopyId; }
            set { _settingsCopyId = value; }
        }
        public int SettingsId
        {
            get { return _settingsId; }
            set { _settingsId = value; }
        }
        public string UserGroup
        {
            get { return _userGroup; }
            set { _userGroup = value; }
        }
      
        public string Menu
        {
            get { return _Menu; }
            set { _Menu = value; }
        }
        public string SubMenu
        {
            get { return _subMenu; }
            set { _subMenu = value; }
        }
        public string MenuStripName
        {
            get { return _menuStripName; }
            set { _menuStripName = value; }
        }
        public string FormName
        {
            get { return _formName; }
            set { _formName = value; }
        }
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
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

    }
}
