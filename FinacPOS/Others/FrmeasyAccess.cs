using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinacPOS
{
    public partial class FrmeasyAccess : Form
    {
        public FrmeasyAccess()
        {
            InitializeComponent();
        }
        bool IsAuthenticationApproved = false;
        frmPOSSettings objSettingsForm = new frmPOSSettings();
        private void FrmeasyAccess_Load(object sender, EventArgs e)
        {
            LoadIcons();             
            EnableDoubleBuffering();
            AppyiconBasedOnLanaguage();
        }
           // AppyiconBasedOnLanaguage();
        
        //public  void AppyiconBasedOnLanaguage()
        //{
        //    // Counter Master
        //    panelCounterMaster.BackgroundImage = GetPanelIcon("counterMaster.png", "CounterMasterArabic.png"); // added by Nishana on 1-10-2025

        //    // Sales Invoice
        //    panelSalesInvoice.BackgroundImage = GetPanelIcon("salesInvoice.png", "SalesInvoiceArabic.png");

        //    // Sales Return
        //    panelSalesReturn.BackgroundImage = GetPanelIcon("salesReturn.png", "SalesReturnArabic.png");

        //    // Payment
        //    panelPOSPayment.BackgroundImage = GetPanelIcon("POS PAYMENT.png", "POSPaymentArabic.png");

        //    // Receipt
        //    panelPOSReceipt.BackgroundImage = GetPanelIcon("POSRECIEPT.png", "POSReceiptArabic.png");

        //    // Session Close
        //    panelSessionClose.BackgroundImage = GetPanelIcon("sessionClose.png", "SessionCloseArabic.png");

        //    // POS Close
        //    panelPOSClose.BackgroundImage = GetPanelIcon("posClose.png", "POSCloseArabic.png");

       // }
        //private Image GetPanelIcon(string englishFile, string arabicFile)
        //{
        //    string basePath = Application.StartupPath + "\\Resources\\";
        //    return PublicVariables._ModuleLanguage == "ARB"
        //        ? Image.FromFile(basePath + arabicFile)
        //        : Image.FromFile(basePath + englishFile);
        //}
        private Dictionary<string, Image> _icons = new Dictionary<string, Image>();

        private void LoadIcons()
        {
            string basePath = Application.StartupPath + "\\Resources\\";

            _icons["CounterMaster"] = Image.FromFile(basePath + "counterMaster.png");
            _icons["CounterMasterARB"] = Image.FromFile(basePath + "CounterMasterArabic.png");

            _icons["SalesInvoice"] = Image.FromFile(basePath + "salesInvoice.png");
            _icons["SalesInvoiceARB"] = Image.FromFile(basePath + "SalesInvoiceArabic.png");

            _icons["SalesReturn"] = Image.FromFile(basePath + "salesReturn.png");
            _icons["SalesReturnARB"] = Image.FromFile(basePath + "SalesReturnArabic.png");

            _icons["POSPayment"] = Image.FromFile(basePath + "POS PAYMENT.png");
            _icons["POSPaymentARB"] = Image.FromFile(basePath + "POSPaymentArabic.png");

            _icons["POSReceipt"] = Image.FromFile(basePath + "POSRECIEPT.png");
            _icons["POSReceiptARB"] = Image.FromFile(basePath + "POSReceiptArabic.png");

            _icons["SessionClose"] = Image.FromFile(basePath + "sessionClose.png");
            _icons["SessionCloseARB"] = Image.FromFile(basePath + "SessionCloseArabic.png");

            _icons["POSClose"] = Image.FromFile(basePath + "posClose.png");
            _icons["POSCloseARB"] = Image.FromFile(basePath + "POSCloseArabic.png");
        }
        private Image GetPanelIcon(string key)
        {
            return PublicVariables._ModuleLanguage == "ARB"
                ? _icons[key + "ARB"]
                : _icons[key];
        }
        private void EnableDoubleBuffering()
        {
            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panelCounterMaster, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, panelSalesInvoice, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
           System.Reflection.BindingFlags.SetProperty |
           System.Reflection.BindingFlags.Instance |
           System.Reflection.BindingFlags.NonPublic,
           null, panelSalesReturn, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
       System.Reflection.BindingFlags.SetProperty |
       System.Reflection.BindingFlags.Instance |
       System.Reflection.BindingFlags.NonPublic,
       null, panelPOSReceipt, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
        System.Reflection.BindingFlags.SetProperty |
        System.Reflection.BindingFlags.Instance |
        System.Reflection.BindingFlags.NonPublic,
        null, panelPOSClose, new object[] { true });

            typeof(Panel).InvokeMember("DoubleBuffered",
           System.Reflection.BindingFlags.SetProperty |
           System.Reflection.BindingFlags.Instance |
           System.Reflection.BindingFlags.NonPublic,
           null, panelSessionClose, new object[] { true });
           
            typeof(Panel).InvokeMember("DoubleBuffered",
System.Reflection.BindingFlags.SetProperty |
System.Reflection.BindingFlags.Instance |
System.Reflection.BindingFlags.NonPublic,
null, panelPOSPayment, new object[] { true });

        }
        public void AppyiconBasedOnLanaguage()
        {
            panelCounterMaster.BackgroundImage = GetPanelIcon("CounterMaster");
            panelSalesInvoice.BackgroundImage = GetPanelIcon("SalesInvoice");
            panelSalesReturn.BackgroundImage = GetPanelIcon("SalesReturn");
            panelPOSPayment.BackgroundImage = GetPanelIcon("POSPayment");
            panelPOSReceipt.BackgroundImage = GetPanelIcon("POSReceipt");
            panelSessionClose.BackgroundImage = GetPanelIcon("SessionClose");
            panelPOSClose.BackgroundImage = GetPanelIcon("POSClose");
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (PublicVariables._userGroup != "Admin")
            {

                POSUserSettingsSP spUserSettings = new POSUserSettingsSP();
                DataTable dtSubMenu = new DataTable();
                dtSubMenu = spUserSettings.POSUserSettingsGetBysubMenuandUserGroup("Sales Invoice", PublicVariables._userGroup);
                if (dtSubMenu.Rows.Count > 0)
                {

                    if (dtSubMenu.Rows[0]["menuStripName"].ToString() == "Sales Invoice")
                        MDIFinacPOS.MDIObj.openSalesInvoice();
                }
                else
                    MessageBox.Show("You have no permission to access", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                //MDIFinacPOS.MDIObj.openSalesInvoice();
                objSettingsForm.openSalesInvoice();




        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (PublicVariables._userGroup != "Admin")
            {

                POSUserSettingsSP spUserSettings = new POSUserSettingsSP();
                DataTable dtSubMenu = new DataTable();
                dtSubMenu = spUserSettings.POSUserSettingsGetBysubMenuandUserGroup("Sales Return", PublicVariables._userGroup);
                if (dtSubMenu.Rows.Count > 0)
                {
                    if (dtSubMenu.Rows[0]["menuStripName"].ToString() == "Sales Return")
                        MDIFinacPOS.MDIObj.openSalesReturn();
                 

                }
                else
                    MessageBox.Show("You have no permission to access", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MDIFinacPOS.MDIObj.openSalesReturn();
           
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (PublicVariables._userGroup != "Admin")
            {

                POSUserSettingsSP spUserSettings = new POSUserSettingsSP();
                DataTable dtSubMenu = new DataTable();
                dtSubMenu = spUserSettings.POSUserSettingsGetBysubMenuandUserGroup("Counter creation", PublicVariables._userGroup);
                if (dtSubMenu.Rows.Count > 0)
                {

                    if (dtSubMenu.Rows[0]["menuStripName"].ToString() == "Counter creation")
                    {
                        frmPOSCounter frmPOSCounter = new frmPOSCounter();
                        frmPOSCounter _isOpen = Application.OpenForms["frmPOSCounter"] as frmPOSCounter;
                        if (_isOpen == null)
                        {
                            frmPOSCounter.WindowState = FormWindowState.Normal;
                            frmPOSCounter.MdiParent = MDIFinacPOS.ActiveForm;
                            frmPOSCounter.Show();
                        }
                        else
                        {
                            _isOpen.MdiParent = MDIFinacPOS.ActiveForm;
                            if (_isOpen.WindowState == FormWindowState.Minimized)
                            {
                                _isOpen.WindowState = FormWindowState.Normal;
                            }
                            if (_isOpen.Enabled)
                            {
                                _isOpen.Activate();
                                _isOpen.BringToFront();
                            }

                        }
                    }


                }
                else
                    MessageBox.Show("You have no permission to access", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                frmPOSCounter frmPOSCounter = new frmPOSCounter();
                frmPOSCounter _isOpen = Application.OpenForms["frmPOSCounter"] as frmPOSCounter;
                if (_isOpen == null)
                {
                    frmPOSCounter.WindowState = FormWindowState.Normal;
                    frmPOSCounter.MdiParent = MDIFinacPOS.ActiveForm;
                    frmPOSCounter.Show();
                }
                else
                {
                    _isOpen.MdiParent = MDIFinacPOS.ActiveForm;
                    if (_isOpen.WindowState == FormWindowState.Minimized)
                    {
                        _isOpen.WindowState = FormWindowState.Normal;
                    }
                    if (_isOpen.Enabled)
                    {
                        _isOpen.Activate();
                        _isOpen.BringToFront();
                    }

                }

            }
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            //
            //MDIFinacPOS.MDIObj.sessionClose();   
            bool holdExists = false;

            SessionControlSp objsessioncontrol = new SessionControlSp();
            SessionManagementSP sessionSp = new SessionManagementSP();

            DataTable dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);
            if ( dtbl.Rows.Count > 0)
            {
                 holdExists = objsessioncontrol.IsHoldBillStillActive(dtbl.Rows[0]["sessionNo"].ToString());

            }

            if (holdExists)
            {
                DialogResult result = MessageBox.Show( "Hold bill exists and cannot be closed.Do you want to continue?","Hold Bill Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return; 
                }
                else
                {
                    if (POSSettingsInfo._HoldBillAuth)
                    {
                        IsAuthenticationApproved = false;
                        string condition = "_HoldBillAuth";

                        frmUserAuthentication frm = new frmUserAuthentication();
                        frm.CallFromFrmEasyAccess(this, condition);

                        if (IsAuthenticationApproved)
                        {
                            objSettingsForm.sessionClose();
                        }
                        else
                        {
                            return;
                        }
                    }
                    objSettingsForm.sessionClose();
                }
            }
            else
                objSettingsForm.sessionClose();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void panelPOSReceipt_Click(object sender, EventArgs e)
        {
            if (PublicVariables._userGroup != "Admin")
            {

                POSUserSettingsSP spUserSettings = new POSUserSettingsSP();
                DataTable dtSubMenu = new DataTable();
                dtSubMenu = spUserSettings.POSUserSettingsGetBysubMenuandUserGroup("POS Receipt", PublicVariables._userGroup);
                if (dtSubMenu.Rows.Count > 0)
                {
                    if (dtSubMenu.Rows[0]["menuStripName"].ToString() == "POS Receipt")
                        MDIFinacPOS.MDIObj.openPOSReceipt();


                }
                else
                    MessageBox.Show("You have no permission to access", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MDIFinacPOS.MDIObj.openPOSReceipt();
        }

        private void panelPOSPayment_Click(object sender, EventArgs e)
        {
            if (PublicVariables._userGroup != "Admin")
            {

                POSUserSettingsSP spUserSettings = new POSUserSettingsSP();
                DataTable dtSubMenu = new DataTable();
                dtSubMenu = spUserSettings.POSUserSettingsGetBysubMenuandUserGroup("POS Payment", PublicVariables._userGroup);
                if (dtSubMenu.Rows.Count > 0)
                {
                    if (dtSubMenu.Rows[0]["menuStripName"].ToString() == "POS Payment")
                        MDIFinacPOS.MDIObj.openPOSPayment();


                }
                else
                    MessageBox.Show("You have no permission to access", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MDIFinacPOS.MDIObj.openPOSPayment();
        }

        public void AuthenticateUser(bool IstrueUser, bool isClose, string condition)
        {
            IsAuthenticationApproved = IstrueUser;
            if (!isClose)
            {
                if (IsAuthenticationApproved == false)
                {
                    MessageBox.Show("You are not an authenticated user!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //else
            //{
            //    barcodeFocus();
            //}
        }
    }
}
