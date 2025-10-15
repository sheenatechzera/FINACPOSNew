using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using FinacPOS;


namespace FinacPOS
{
    partial class frmUserLogin : Form
    {
        // UL7
        public frmUserLogin()
        {
            InitializeComponent();
        }

        POSCounterSP counterSP = new POSCounterSP();
        
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
                
                
                POSCounterInfo counterInfo = new POSCounterInfo();
                txtUserName.Text = txtUserName.Text.Trim();

                txtPassword.Text = txtPassword.Text.Trim();

                //if username and password matches then menu form will be loaded
                POSUserInfo userinfo = new POSUserInfo();
                POSUserSP userloginsp = new POSUserSP();
                if (txtUserName.Text == "")
                {
                    if (rbtnEnglish.Checked) 
                    {
                        MessageBox.Show("Enter user name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUserName.Focus();
                    }
                    else if (rbtnArabic.Checked) 
                    {
                        MessageBox.Show("أدخل اسم المستخدم", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUserName.Focus();
                    }

                }

                else if (txtPassword.Text == "")
                {
                    if (rbtnEnglish.Checked) 
                    {
                        MessageBox.Show("Enter password", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Focus();
                    }
                    else if (rbtnArabic.Checked) 
                    {
                        MessageBox.Show("أدخل كلمة المرور", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Focus();
                    }

                }
                else
                {
                    userinfo = userloginsp.POSUserViewByName(txtUserName.Text);
                    if (userinfo.UserId != null)
                    {
                        if (userinfo.Active == true)
                        {
                            if (userinfo.Password == txtPassword.Text)
                            {
                                PublicVariables.IsFormUserLoginOpen = true;

                                counterInfo = counterSP.POSCounterViewBySystemName();
                                if (counterInfo.CounterId != null)
                                {
                                    PublicVariables._counterId = counterInfo.CounterId;
                                    PublicVariables._counterName = counterInfo.CounterName;
                                    PublicVariables._SalesScreenType = counterInfo.SalesType;

                                }

                                this.Close();
                                PublicVariables._currentUserId = userinfo.UserId;
                                PublicVariables._EmpName = userinfo.EmpName;
                                PublicVariables._userGroup = userinfo.POSUserGroupId;

                                BranchInfo InfoBranch = new BranchInfo();
                                BranchSP SpBranch = new BranchSP();
                                InfoBranch = SpBranch.BranchView(PublicVariables._branchId);
                                PublicVariables._currencyId = InfoBranch.CurrencyId;

                                if (userinfo.language == "English")
                                {
                                    PublicVariables._ModuleLanguage = "ENG";
                                }
                                else
                                {
                                    PublicVariables._ModuleLanguage = "ARB";
                                }

                                // Assigning values to public variables   
                                // Opening form to set currentDate
                                // frmChangeCurrentDate objFrom = new frmChangeCurrentDate();

                                FinancialYearSp SpFinance = new FinancialYearSp();
                                DataTable dtbl = new DataTable();
                                dtbl = SpFinance.FinancialYearViewallActingYear(true);
                                PublicVariables._fromDate = DateTime.Parse(dtbl.Rows[0]["fromDate"].ToString());
                                PublicVariables._toDate = DateTime.Parse(dtbl.Rows[0]["toDate"].ToString());

                                MDIFinacPOS.MDIObj.EnableMenuItems();
                                // MDIFinacPOS.MDIObj.Activate();

                                MDIFinacPOS.MDIObj.LoadEasyAccess();

                                PublicVariables.IsFormUserLoginOpen = false;
                                //MDIFinacPOS.MDIObj.ShowReminderIfAny(false);
                                //objFrom.CallFromFinancialYear("Login");
                                //objFrom.Activate();
                            }
                            else
                             {
                                if (rbtnEnglish.Checked)
                                {
                                    MessageBox.Show("Invalid password", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtPassword.Clear();
                                    txtPassword.Focus();
                                }
                                else 
                                {
                                    MessageBox.Show("كلمة المرور غير صحيحة", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtPassword.Clear();
                                    txtPassword.Focus();
                                }



                            }
                        }
                        else
                        {
                            if (rbtnEnglish.Checked)
                            {
                                MessageBox.Show("Blocked user", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtUserName.SelectAll();
                                txtUserName.Focus();
                            }
                            else 
                            {
                                MessageBox.Show("مستخدم محظور", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtUserName.SelectAll();
                                txtUserName.Focus();
                            }


                        }
                    }
                    else
                    {
                        if (rbtnEnglish.Checked) 
                        {
                            MessageBox.Show("Invalid user name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtUserName.SelectAll();
                            txtUserName.Focus();
                        }
                        else 
                        {
                            MessageBox.Show("اسم المستخدم غير صالح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtUserName.SelectAll();
                            txtUserName.Focus();
                        }


                    }
                }

            }
            
            catch (Exception ex)
            {
                MessageBox.Show("UL4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmUserLogin_Load(object sender, EventArgs e)
        {

            try
            {
                txtUserName.Focus();
                 rbtnEnglish.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {              
                    if (MessageBox.Show(clsGeneral.MessageFunction("Close"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.Close();
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL6:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                
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
        private void rbtnArabic_CheckedChanged_1(object sender, EventArgs e)
        {

            if (rbtnArabic.Checked)
            {
                PublicVariables._ModuleLanguage = "ARB";
                setLanguage(PublicVariables._ModuleLanguage);
                clsGeneral objGeneral = new clsGeneral();
                objGeneral.formSettings(this);
            }
        }
        public void setLanguage(String language)
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            if (language == "ARB")
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;
            }
            else // English
            {
                this.RightToLeft = RightToLeft.No;
                this.RightToLeftLayout = false;
            }
            //this.Controls.Clear();
        }

        private void rbtnEnglish_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbtnEnglish.Checked)
            {
                PublicVariables._ModuleLanguage = "ENG";
              
            }
        }
    }



    }
