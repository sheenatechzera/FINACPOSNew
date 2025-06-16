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
        private void FrmeasyAccess_Load(object sender, EventArgs e)
        {
            //this.Location = new Point(10, 10);
           
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (PublicVariables._userGroup != "Admin")
            {

                POSUserSettingsSP spUserSettings = new POSUserSettingsSP();
                DataTable dtSubMenu = new DataTable();
                dtSubMenu = spUserSettings.POSUserSettingsGetBysubMenuandUserGroup("Sales Invoice",PublicVariables._userGroup);
                if (dtSubMenu.Rows.Count > 0)
                {

                    if (dtSubMenu.Rows[0]["menuStripName"].ToString() == "Sales Invoice")
                        MDIFinacPOS.MDIObj.openSalesInvoice();                   
                }
                else
                    MessageBox.Show("You have no permission to access", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MDIFinacPOS.MDIObj.openSalesInvoice();    

           
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
            MDIFinacPOS.MDIObj.sessionClose();   
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


    }
}
