using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.XtraEditors.Design;

namespace FinacPOS.Masters
{
    public partial class frmSessionControl : Form
    {
        int sessionNo = 0;
        public frmSessionControl()
        {
            InitializeComponent();
        }
        SessionManagementInfo sessionInfo = new SessionManagementInfo();
        ComboValidation objComboValidation = new ComboValidation();
        SessionControlSp objsessionControlsSp = new SessionControlSp();
        frmSessionClosing objsessionClosing = new frmSessionClosing();
        SessionManagementSP sessionSp = new SessionManagementSP();
      
        SessionManagementSP SPSessionManagement = new SessionManagementSP();
        string selectedCounter = "";
        private void SessionManagmentByAdmin_Load(object sender, EventArgs e)
        {
            lblSessionDate.ForeColor = System.Drawing.Color.Red;
          
            Clear();  
        }

        //public void Clear()
        //{
        //    //lblCounter.Text = PublicVariables._counterName;
        //    // lblUserId.Text = PublicVariables._EmpName;
        //    cmbCounter.SelectedItem = -1;
        //    cmbUser.SelectedItem = -1;
        //    txtOpenBal.Text = "0.00";
            
        //    LoadSessionNo();

        //}
        public void Clear()
        {
            IsValueChanged = false;

            cmbCounter.SelectedIndex = -1;
            cmbUser.SelectedIndex = -1;

   
            txtOpenBal.Text = "0.00";
            rbSessionOpening.Checked = true;
            //txtOpenBal.ReadOnly = true;  


            //dtpSessionDate.Value = DateTime.Today;
            //dtpSessionDate.Enabled = false; 
            LoadCounter();
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
        string userid = "";
        public void LoadSessionNo()
        {
            string counterid="";
            if (cmbUser.SelectedValue == null || string.IsNullOrEmpty(cmbUser.SelectedValue.ToString()))
            {
                userid = ""; // or set to default like "0"
            }
            else
            {
                userid = cmbUser.SelectedValue.ToString();
            }
            if (cmbCounter.SelectedValue == null || string.IsNullOrEmpty(cmbCounter.SelectedValue.ToString()))
            {
                counterid = ""; // or set to default like "0"
            }
            else
            {
                counterid = cmbCounter.SelectedValue.ToString();
            }
            if (rbSessionOpening.Checked)
                sessionNo = sessionSp.SessionManagementGetMaxByDateandCounterId(dtpSessionDate.Value, counterid, userid, false);
            else if(rbSessionClose.Checked)
                sessionNo = sessionSp.SessionManagementGetMaxByDateandCounterId(dtpSessionDate.Value, counterid, userid, true);
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
            else if (cmbCounter.SelectedValue == null || string.IsNullOrEmpty(cmbCounter.SelectedValue.ToString()))
            {
                MessageBox.Show("Select Counter", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbCounter.Focus();
            }
            else if (cmbUser.SelectedValue == null || string.IsNullOrEmpty(cmbUser.SelectedValue.ToString()))
            {
                MessageBox.Show("Select User", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbUser.Focus();
            }
            else if (rbSessionOpening.Checked)
            {

                if (txtOpenBal.Text == "")
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


                    sessionInfo.SessionNo = sessionNo.ToString();
                    sessionInfo.SessionDate = Convert.ToDateTime(dtpSessionDate.Text);
                    // sessionInfo.CounterId = PublicVariables._counterId;
                    // sessionInfo.UserId = PublicVariables._currentUserId;
                    sessionInfo.CounterId = Convert.ToString(cmbCounter.SelectedValue);
                    sessionInfo.UserId = Convert.ToInt32(cmbUser.SelectedValue).ToString();
                    sessionInfo.OpeningBalance = decimal.Parse(txtOpenBal.Text);
                    sessionInfo.SessionStatus = "O".ToString();
                    sessionInfo.CreatedDate = DateTime.Now;
                    sessionInfo.CreatedUserId = PublicVariables._currentUserId;
                    sessionInfo.BranchId = PublicVariables._branchId;
                    sessionInfo.Extra1 = "";
                    sessionInfo.Extra2 = "";
                    sessionSp.SessionManagementAdd(sessionInfo);
                    MessageBox.Show("Session created successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOpenBal.Text = "0";
                    dtpSessionDate.ResetText();

                    if (PublicVariables._SalesScreenType == "Type1")
                    {
                        frmPOSSales objfrmPOSSales = new frmPOSSales();
                        objfrmPOSSales.strSessionNo = sessionNo.ToString();
                        objfrmPOSSales.strSessionDate = dtpSessionDate.Text.ToString();
                        objfrmPOSSales.CallFromSessionControl(this);
                    }
                    else
                    {
                        frmPOSSales2 objfrmPOSSales = new frmPOSSales2();
                        objfrmPOSSales.strSessionNo = sessionNo.ToString();
                        objfrmPOSSales.strSessionDate = dtpSessionDate.Text.ToString();
                        objfrmPOSSales.CallFromSessionControl(this);
                    }


                }
                //Clear();
            }
            else if (rbSessionClose.Checked)
            {
                LoadSessionNo();
                SPSessionManagement.UpdateSessionClose(selectedCounter);
                MessageBox.Show("Session Closed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                objsessionClosing.loadSessionClosingData(sessionNo.ToString(), Convert.ToDateTime(dtpSessionDate.Text), selectedCounter, userid);
                objsessionClosing.FillDatatatablesforDevPrint();
            }
            Clear();
        }

        private void txtOpenBal_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }
        private void LoadCounter()
        {
            cmbCounter.DataSource = null;
            if (rbSessionOpening.Checked)
            {
                DataTable dtClosedCounters = objsessionControlsSp.GetClosedCounters();

                cmbCounter.DataSource = dtClosedCounters;
                cmbCounter.DisplayMember = "counterName";
                cmbCounter.ValueMember = "counterId";
            }
            else
            {
                DataTable dtOpenedCounters = objsessionControlsSp.GetOpenedCounters();

                cmbCounter.DataSource = dtOpenedCounters;
                cmbCounter.DisplayMember = "counterName";
                cmbCounter.ValueMember = "counterId";
            }
            IsValueChanged = true;
        }
        private void LoadUser()
        {
            DataTable dtAllUsers = objsessionControlsSp.GetAllUsers();

            cmbUser.DataSource = dtAllUsers;
            cmbUser.DisplayMember = "EmpName";
            cmbUser.ValueMember = "userId";
        }

        private void rbSessionOpening_CheckedChanged(object sender, EventArgs e)
        {  IsValueChanged  =false;
            //IsValueChanged = true;
            LoadCounter();
            //IsValueChanged = false;
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

        private void cmbCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsValueChanged)
            {
                if (rbSessionOpening.Checked)
                {
                    LoadUser();
                    cmbUser.Enabled = true;
                    txtOpenBal.ReadOnly = false;
                    dtpSessionDate.Enabled = true;
                }
                else if (rbSessionClose.Checked)
                {
                    cmbUser.DataSource = null;
                     selectedCounter = cmbCounter.SelectedValue.ToString();

                    DataTable dt = objsessionControlsSp.GetSessionCloseInfo(selectedCounter);

                    if (dt.Rows.Count > 0)
                    {
                        cmbUser.DataSource = dt;
                        cmbUser.DisplayMember = "userName";
                        cmbUser.ValueMember = "userId";
                       
                        txtOpenBal.Text = Convert.ToDecimal(dt.Rows[0]["openingBalance"]).ToString("0.00");
                        dtpSessionDate.Value = Convert.ToDateTime(dt.Rows[0]["sessionDate"]);
                        cmbUser.Enabled = false;
                        txtOpenBal.ReadOnly = true;
                        dtpSessionDate.Enabled = false;
                     }


                }
              
            }
           
            IsValueChanged = false;
            
        }

        private void rbSessionClose_CheckedChanged(object sender, EventArgs e)
        {
            IsValueChanged  =false;
            LoadCounter();
           // IsValueChanged = false;

        }
     
       bool IsValueChanged = false;
        private void cmbCounter_SelectedValueChanged(object sender, EventArgs e)
        {

        }
    }
}