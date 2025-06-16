using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinacPOS
{
    class LanguageEntryWordInfo
    {

        
        private Int32 _FormId;
        private string _ControlName;
        private string _ControlType;
        private string _English;
        private byte[] _Arabic;

        public Int32 FormId
        {
            get { return _FormId; }
            set { _FormId = value; }
        }
        public string ControlName
        {
            get { return _ControlName; }
            set { _ControlName = value; }
        }
        public string ControlType
        {
            get { return _ControlType; }
            set { _ControlType = value; }
        }
        public string English
        {
            get { return _English; }
            set { _English = value; }
        }
        public byte[] Arabic
        {
            get { return _Arabic; }
            set { _Arabic = value; }
        }
        
    }
}
