using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace FinacPOS
{
    partial class frmUserAuthentication : Form
    {
        // UL7
        public frmUserAuthentication()
        {
            InitializeComponent();
        }

        POSCounterSP counterSP = new POSCounterSP();
        bool isFromPOSSales = false;
        bool isFromPOSSales2 = false;
        frmPOSSales ObjfrmPOSSales;
        frmPOSSales2 ObjfrmPOSSales2;
        bool isTrue = false;
        bool isClose = false;
        string _conditionFromSales = "";
        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                    SendKeys.Send("+{Tab}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Back) && (txtPassword.Text == "" || txtPassword.SelectionStart == 0))
                {
                    txtUserName.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    btnLogin.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL2:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtPassword .Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL3:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        protected override void OnShown(EventArgs e)
        {
            txtUserName.Focus();
            base.OnShown(e);
        }
        private void btnLogin_Click(object sender, EventArgs e)        
        {
            try
            {
                bool isExist = false;
                POSCounterInfo counterInfo = new POSCounterInfo();
                txtUserName.Text = txtUserName.Text.Trim();
                txtPassword.Text = txtPassword.Text.Trim();
                POSUserSP userloginsp = new POSUserSP();

                isExist = userloginsp.POSUserAuthentication(txtUserName.Text, txtPassword.Text);
                if (isExist)
                {
                    isTrue = true;
                    //this.Close(); 
                }
                else
                {
                    isTrue = false;
                    txtUserName.SelectAll();
                    txtUserName.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("UL4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void frmUserAuthentication_Load(object sender, EventArgs e)
        {            
            try
            {              
                txtUserName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL5:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                isClose = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                isTrue = false;
                txtPassword.Clear();
                if (!txtUserName.ReadOnly)
                {
                    txtUserName.Clear();
                    txtUserName.Focus();
                }
                else
                    txtPassword.Focus();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL7:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void CallFromPOSSales(frmPOSSales frm,string condition)
        {
            isFromPOSSales = true;
            _conditionFromSales = condition;
            ObjfrmPOSSales = frm;
            DoWhenComingFromOtherForms();
        }
        public void CallFromPOSSales2(frmPOSSales2 frm, string condition)
        {
            isFromPOSSales = true;
            _conditionFromSales = condition;
            ObjfrmPOSSales2 = frm;
            DoWhenComingFromOtherForms();
        }
        public void DoWhenComingFromOtherForms()
        {           
            base.ShowInTaskbar = false;
            base.ShowDialog();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------
        public void CheckWhenQuiting()
        {
            // Function to check while quiting the form
            // To return to the parent form
            // REturn bool isTrue and isClose(simply closing without authentication) to parent form
            if (isFromPOSSales)
            {
                ObjfrmPOSSales.Enabled = true;
                ObjfrmPOSSales.Activate();
                ObjfrmPOSSales.AuthenticateUser(isTrue, isClose, _conditionFromSales);
                ObjfrmPOSSales.BringToFront();    
            }
            if (isFromPOSSales2)
            {
                ObjfrmPOSSales2.Enabled = true;
                ObjfrmPOSSales2.Activate();
                ObjfrmPOSSales2.AuthenticateUser(isTrue, isClose, _conditionFromSales);
                ObjfrmPOSSales2.BringToFront();
            }
           
        }

        private void frmUserAuthentication_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CheckWhenQuiting();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}