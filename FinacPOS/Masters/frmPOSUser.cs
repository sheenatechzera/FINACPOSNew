using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    public partial class frmPOSUser : Form
    {
        public frmPOSUser()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);
        }
        public void setLanguage(String language)
        {
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
            if (language == "ARB")
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;
            }
            //this.Controls.Clear();
        }
        #region PUBLIC VARIABLES

        bool isInEditMode = false;
        POSUserSP userSP = new POSUserSP();
        frmUsersReport objfrmUsers = null;
        string strUserIdToEdit = "";
        ComboValidation objComboValidation = new ComboValidation();
        #endregion

        #region EVENTS

        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtName.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtName.Text.Trim() == "" || txtName.SelectionStart == 0)
                    {

                        txtUserId.Focus();
                        txtUserId.SelectionStart = txtUserId.Text.Trim().Length;
                        txtUserId.SelectionLength = 0;

                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtUsername.Text.Trim() == "" || txtUsername.SelectionStart == 0)
                    {

                        txtName.Focus();

                        txtName.SelectionStart = txtName.Text.Trim().Length;
                        txtName.SelectionLength = 0;

                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    txtPassword.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtPassword.Text.Trim() == "" || txtPassword.SelectionStart == 0)
                    {

                        txtUsername.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    cmbGroupId.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbGroupId_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    txtPassword.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    txtDiscPer.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDiscPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    cmbGroupId.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {

                    txtBillPer.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtBillPer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {

                    txtDiscPer.Focus();

                }
                else if (e.KeyCode == Keys.Enter)
                {

                    btnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    txtBillPer.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmPOSUser_Load(object sender, EventArgs e)
        {
            clsGeneral objGeneral = new clsGeneral();
            objGeneral.formSettings(this);

            lblUserId.ForeColor = System.Drawing.Color.Red;        
            Clear();
        }

        private void txtUserId_Leave(object sender, EventArgs e)
        {
            lblUserId.ForeColor = System.Drawing.Color.Black;
        }

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            lblUserId.ForeColor = System.Drawing.Color.Red;
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            lblName.ForeColor = System.Drawing.Color.Red;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            lblName.ForeColor = System.Drawing.Color.Black;
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            lblUserName.ForeColor = System.Drawing.Color.Black;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            lblUserName.ForeColor = System.Drawing.Color.Red;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            lblPwd.ForeColor = System.Drawing.Color.Black;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            lblPwd.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbGroupId_Enter(object sender, EventArgs e)
        {
            lblGroup.ForeColor = System.Drawing.Color.Red;
        }

        private void cmbGroupId_Leave(object sender, EventArgs e)
        {
            lblGroup.ForeColor = System.Drawing.Color.Black;
        }

        private void txtDiscPer_Enter(object sender, EventArgs e)
        {
            try
            {
                lblDisc.ForeColor = System.Drawing.Color.Red;
                if (decimal.Parse(txtDiscPer.Text) == 0m)
                {
                    txtDiscPer.Text = "";
                }
            }
            catch
            {
                txtDiscPer.Text = "";
            }

        }

        private void txtDiscPer_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtDiscPer.Text);
                lblDisc.ForeColor = System.Drawing.Color.Black;
            }
            catch
            {
                txtDiscPer.Text = "0";
            }
        }

        private void txtBillPer_Leave(object sender, EventArgs e)
        {
            try
            {
                lblBill.ForeColor = System.Drawing.Color.Black;
                decimal.Parse(txtBillPer.Text);
            }
            catch
            {
                txtBillPer.Text = "0";
            }
        }

        private void txtBillPer_Enter(object sender, EventArgs e)
        {
            try
            {
                lblBill.ForeColor = System.Drawing.Color.Red;
                if (decimal.Parse(txtBillPer.Text) == 0m)
                {
                    txtBillPer.Text = "";
                }
            }
            catch
            {
                txtBillPer.Text = "";
            }
        }

        public void IntigerFieldKeypress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txtObj = (TextBox)sender;

                if ((e.KeyChar > 47 && e.KeyChar < 58) || (e.KeyChar == 8))
                {
                    e.Handled = false;
                }

                else
                {
                    e.Handled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SI16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtDiscPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtBillPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveOrEdit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("AG7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                frmUsersReport frmobj = new frmUsersReport();
                frmobj.MdiParent = MDIFinacPOS.ActiveForm;
                frmUsersReport open = Application.OpenForms["frmUsersReport"] as frmUsersReport;
                if (open == null)
                {
                    frmobj.WindowState = FormWindowState.Normal;
                    frmobj.MdiParent = MDIFinacPOS.ActiveForm;
                    frmobj.CallFromPOSUser(this);
                }
                else
                {
                    open.MdiParent = MDIFinacPOS.ActiveForm;
                    open.CallFromPOSUser(this);
                    open.BringToFront();
                    if (open.WindowState == FormWindowState.Minimized)
                    {
                        open.WindowState = FormWindowState.Normal;
                    }
                }
                this.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PU76:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void CallFromPOSUserReport(frmUsersReport frmUsersReport, string StrUserId)
        {
            try
            {
                isInEditMode = true;
                this.objfrmUsers = frmUsersReport;
                base.Show();
                this.Enabled = true;
                this.BringToFront();
                FillUserForEdit(StrUserId);

                txtName.Focus();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                objfrmUsers.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("FM12:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (isInEditMode)
            {
                if (MessageBox.Show("Do you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    DeleteUser();
                }
            }
        }

        #endregion

        #region FUNCTIONS

        public void SaveOrEdit()
        {
            // Doing Save Or Edit
            txtUserId.Text = txtUserId.Text.Trim();
            txtName.Text = txtName.Text.Trim();
            if (txtUserId.Text == "")
            {
                MessageBox.Show("Enter User Id", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserId.Focus();
            }
            else if (txtName.Text == "")
            {
                MessageBox.Show("Enter Employee Name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
            }
            else if (txtUsername.Text == "")
            {
                MessageBox.Show("Enter Username", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Enter Password", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
            }
            else if (cmbGroupId.Text == "")
            {
                MessageBox.Show("Select Group", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbGroupId.Focus();
            }
            else if (cmbLanguage.Text == "")
            {
                MessageBox.Show("Select Group", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbLanguage.Focus();
            }
            else
            {
                bool isSave = true;
                // Asking confirmation if settings is true
                //if (SettingsInfo._messageBoxAddEdit)
                //{
                if (isInEditMode)
                {
                    if (MessageBox.Show("Do you want to update?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        isSave = false;
                    }
                }
                else
                {
                    if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        isSave = false;
                    }
                }
                //   }
                if (isSave)
                {
                    POSUserInfo UserInfo = new POSUserInfo();
                    UserInfo.UserId = txtUserId.Text;
                    UserInfo.EmpName = txtName.Text;
                    UserInfo.UserName = txtUsername.Text;
                    UserInfo.Password = txtPassword.Text;
                    UserInfo.POSUserGroupId = cmbGroupId.Text.ToString();
                    UserInfo.POSAdmin = chkAdmin.Checked;
                    UserInfo.MaxLineDiscountPer = Convert.ToDecimal(txtDiscPer.Text);
                    UserInfo.MaxBillDiscountPer = Convert.ToDecimal(txtBillPer.Text);
                    UserInfo.Active = chkActive.Checked;
                    UserInfo.Extra1 = "";
                    UserInfo.Extra2 = "";
                    UserInfo.BranchId = PublicVariables._branchId;
                    UserInfo.language = cmbLanguage.Text.ToString();
                    if (!isInEditMode)
                    {
                        if (userSP.CheckExistanceOfUserID(txtUserId.Text))
                        {
                            MessageBox.Show("UserID already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtUserId.Focus();
                            txtUserId.SelectAll();

                        }
                        else
                        {
                            if (userSP.CheckExistanceOfUserName(txtUsername.Text))
                            {
                                MessageBox.Show("Username already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtUsername.Focus();
                                txtUsername.SelectAll();
                            }
                            else
                            {
                                // Save 
                                userSP.POSUserAdd(UserInfo);
                                MessageBox.Show("User created successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }

                        }
                    }

                    else
                    {
                        if (userSP.CheckExistanceOfUserNamebyuserid(txtUsername.Text, txtUserId.Text))
                        {
                            MessageBox.Show("Username already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtUsername.Focus();
                            txtUsername.SelectAll();
                        }
                        else
                        {
                            if (isInEditMode)
                            {
                                userSP.POSUserEdit(UserInfo);
                                MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }

                        }
                    }
                }
            }
        }

        public void Clear()
        {
            txtUserId.Enabled = true;
            txtUserId.Clear();
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtDiscPer.Text = "0";
            txtBillPer.Text = "0";
            btnSave.Text = "Save";
            btnClear.Text = "Clear";
            chkActive.Checked = true;
            chkAdmin.Checked = false;
            isInEditMode = false;
            strUserIdToEdit = "";
            btnDelete.Enabled = false;
            txtUserId.ReadOnly = false;
            cmbGroupId.SelectedIndex = -1;
            txtUserId.Focus();
            cmbLanguage.SelectedIndex = 0;

        }
        //---------------------------------call from other form----------------------------------------------------
        bool isFromOther = false;
        public void CallFromUsers(frmUsersReport frm, string struserId)
        {
            this.Enabled = true; ;
            objfrmUsers = frm;
            isFromOther = true;
            FillUserForEdit(struserId);
        }

        public void FillUserForEdit(string strUserid)
        {
            isInEditMode = true;
            strUserIdToEdit = txtUserId.Text = strUserid;
            txtUserId.ReadOnly = true;
            POSUserInfo InfoPOSUser = new POSUserInfo();
            InfoPOSUser = userSP.POSUserView(txtUserId.Text);
            if (InfoPOSUser.UserId != null)
            {
                btnSave.Text = "Update";
                btnClear.Text = "New";
                btnDelete.Enabled = true;
                txtName.Text = InfoPOSUser.EmpName;
                txtUsername.Text = InfoPOSUser.UserName;
                txtPassword.Text = InfoPOSUser.Password;
                cmbGroupId.Text = InfoPOSUser.POSUserGroupId;
                chkAdmin.Checked = InfoPOSUser.POSAdmin;
                chkActive.Checked = InfoPOSUser.Active;
                txtDiscPer.Text = InfoPOSUser.MaxLineDiscountPer.ToString();
                txtBillPer.Text = InfoPOSUser.MaxBillDiscountPer.ToString();
            }

        }

    

        public void DeleteUser()
        {
            try
            {
                userSP.POSUserDelete(txtUserId.Text);
                MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (objfrmUsers != null)
                {
                    this.Close();
                }
                else
                {
                    strUserIdToEdit = "";
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        #endregion

        private void pnlPOSUser_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
