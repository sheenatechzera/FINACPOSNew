using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class FinacControlSettingsInfo
    {

        private Int32 _Action;
        private Int32 _FormId;
        private string _FormName;
        private string _FormCaption;

        public Int32 Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        public Int32 FormId
        {
            get { return _FormId; }
            set { _FormId = value; }
        }
        public string FormName
        {
            get { return _FormName; }
            set { _FormName = value; }
        }
        public string FormCaption
        {
            get { return _FormCaption; }
            set { _FormCaption = value; }
        }
        
    }
}
