using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FinacPOS.Classes.Info;




namespace FinacPOS
{
    public partial class frmSessionManagement : Form
    {
        public frmSessionManagement()
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
        SessionManagementSP sessionSp = new SessionManagementSP();
        POSTokenSP tokenSp = new POSTokenSP();
        int sessionNo = 0;
        ComboValidation objComboValidation = new ComboValidation();
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

        private void frmSessionManagement_Load(object sender, EventArgs e)
        {
            lblSessionDate.ForeColor = System.Drawing.Color.Red;
            Clear();
        }
        public void Clear()
        {            
            lblCounter.Text = PublicVariables._counterName;
            lblUserId.Text = PublicVariables._EmpName;
            LoadSessionNo();
        }

       

        private void dtpSessionDate_Leave(object sender, EventArgs e)
        {
            lblSessionDate.ForeColor = System.Drawing.Color.Black;
        }

        private void dtpSessionDate_Enter(object sender, EventArgs e)
        {
            lblSessionDate.ForeColor = System.Drawing.Color.Red;

        }

       

        private void txtOpenBal_Enter(object sender, EventArgs e)
        {          
            try
            {
                lblOpenBal.ForeColor = System.Drawing.Color.Red;
                if (decimal.Parse(txtOpenBal.Text) == 0m)
                {
                    txtOpenBal.Text = "";
                }
            }
            catch
            {
                txtOpenBal.Text = "";
            }
        }

        private void txtOpenBal_Leave(object sender, EventArgs e)
        {
            lblOpenBal.ForeColor = System.Drawing.Color.Black;
        }

       
        private void dtpSessionDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                 if (e.KeyCode == Keys.Enter)
                {

                    txtOpenBal.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

     
      

        private void txtOpenBal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtOpenBal.Text.Trim() == "" || txtOpenBal.SelectionStart == 0)
                    {
                        dtpSessionDate.Focus();
                    }
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

        public void LoadSessionNo()
        {
            sessionNo = sessionSp.SessionManagementGetMaxByDateandCounterId(dtpSessionDate.Value, PublicVariables._counterId, PublicVariables._currentUserId, false);
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        public void Save()
        {
            bool isSave = true;
            if (dtpSessionDate.Text == "")
            {
                MessageBox.Show("Select Session date", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpSessionDate.Focus();
            }
            else if (txtOpenBal.Text == "")
            {
                txtOpenBal.Text = "0";
            }
            if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                isSave = false;
            }
            if (isSave)
            {
                LoadSessionNo();
                SessionManagementInfo sessionInfo = new SessionManagementInfo();
                POSTokenInfo TokenInfo = new POSTokenInfo();
                POSSettingsInfo InfoPOSSettings = new POSSettingsInfo();
                POSSettingsSP SpPOSSettings = new POSSettingsSP();
                InfoPOSSettings = SpPOSSettings.POSSettingsViewByBranchId(PublicVariables._branchId);
                sessionInfo.SessionNo = sessionNo.ToString();
                sessionInfo.SessionDate = Convert.ToDateTime(dtpSessionDate.Text);
                sessionInfo.CounterId = PublicVariables._counterId;
                sessionInfo.UserId = PublicVariables._currentUserId;
                sessionInfo.OpeningBalance = decimal.Parse(txtOpenBal.Text);
                sessionInfo.SessionStatus = "O".ToString();
                sessionInfo.CreatedDate = DateTime.Now;
                sessionInfo.CreatedUserId = PublicVariables._currentUserId;
                sessionInfo.BranchId = PublicVariables._branchId;
                sessionInfo.Extra1 = "";
                sessionInfo.Extra2 = "";
                TokenInfo.SessionDate= Convert.ToDateTime(dtpSessionDate.Text);
                TokenInfo.LastTokenNo = (Convert.ToInt32(InfoPOSSettings.StartingTokenNo) - 1).ToString();
                TokenInfo.Active = true;
                if (string.IsNullOrEmpty(tokenSp.POSTokenNoExist(sessionInfo.SessionDate)))
                {
                    tokenSp.POSTokenAdd(TokenInfo);
                }
                sessionSp.SessionManagementAdd(sessionInfo);
                MessageBox.Show("Session created successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOpenBal.Text = "0";
                dtpSessionDate.ResetText();

                if (PublicVariables._SalesScreenType =="Type1" )
                {
                    frmPOSSales objfrmPOSSales = new frmPOSSales();
                    objfrmPOSSales.strSessionNo = sessionNo.ToString();
                    objfrmPOSSales.strSessionDate = dtpSessionDate.Text.ToString();
                    objfrmPOSSales.CallFromSessionManagement(this);
                }
                else
                {
                    frmPOSSales2 objfrmPOSSales = new frmPOSSales2();
                    objfrmPOSSales.strSessionNo = sessionNo.ToString();
                    objfrmPOSSales.strSessionDate = dtpSessionDate.Text.ToString();
                    objfrmPOSSales.CallFromSessionManagement(this);
                }
                
            }
        }

        private void txtOpenBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

      
    }
}
