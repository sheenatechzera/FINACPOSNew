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
     partial class frmProductMainGroup : Form
    {
        public frmProductMainGroup()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);
        }
        /******************************************************************************************************************
        *                                       PUBLIC VARIABLES
        *****************************************************************************************************************/
        #region PUBLIC VARIABLES
        UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();//changed on 19/10/2023 sheena    
        bool isDeleteEnable = false;// added 19/10/2023
        bool isEditEnable = true;// added 19/10/2023

        ProductMainGroupSP SPProductmainGroup = new ProductMainGroupSP();
        ComboValidation objComboValidation = new ComboValidation();
        //frmProgress frmCompanyProgress = new frmProgress();
        frmProductCreation frmproductcreationObj;
        string strGroupIdForEdit = "";   // to keep group id whileediting
        string strGroupIdForOtherForms = ""; // To keep id for returnign to other forms
        string strGroupName = "";            // To keep group name before editing
        bool isInEditMode = false;  // to indicate whetehr teh form is opened in edit mode
        bool isFromProductCreationForm = false;  // to indicate whetehr this form is called form other forms
        bool isFormLoad = false;
        int inNarrationCount = 0; // to hold count of enter keys presses
        string _mainMenuItem = "";
        #endregion

        

        #region FUNCTIONS
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
        public void ClearFunction()
        {
            //Clear All Controls of form
            try
            {
                isFormLoad = true;
                strGroupIdForEdit = "";
                strGroupIdForOtherForms = "";
                strGroupName = "";
                txtGroupCode.Clear();
                txtGroupName.Clear();
                txtProductCodeLgh.Text = "0";
                txtNxtNumber.Text = "0";
                FillRegister();
                FinacMessage.SaveButtonText(btnSave, "New");
                FinacMessage.ClearButtonText(btnClear, "New"); 
                chkScaleGroup.Checked = false;
                txtGroupName.BackColor = Color.White;
                txtGroupName.Enabled = true;
                txtGroupCode.Enabled = true;
                isInEditMode = false;
                btnDelete.Enabled = false;
               
                isFormLoad = false;
                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SaveOrEdit()
        {

            try
            {
                txtGroupCode.Text = txtGroupCode.Text.Trim();
                txtGroupName.Text = txtGroupName.Text.Trim();



                if (txtGroupName.Text == "")
                {
                    MessageBox.Show("Enter product group name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGroupName.Focus();
                }
                else
                {

                    bool isSave = true;

                    if (InventorySettingsInfo._messageBoxAddEdit)
                    {
                        if (!isInEditMode)
                        {
                            if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                isSave = false;
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("Do you want to update?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            {
                                isSave = false;
                            }
                        }
                    }
                    if (isSave)
                    {
                        if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, (strGroupIdForEdit == "" ? "Add" : "Edit"), _mainMenuItem) == true)//added on 19/10/2023 sheena
                        {


                            ProductMainGroupInfo InfoProductmainGroup = new ProductMainGroupInfo();
                            InfoProductmainGroup.GroupCode = txtGroupCode.Text;
                            InfoProductmainGroup.GroupName = txtGroupName.Text;
                            InfoProductmainGroup.ScaleGroup = chkScaleGroup.Checked;

                            InfoProductmainGroup.ProductCodeLength = Convert.ToInt32(txtProductCodeLgh.Text);
                            InfoProductmainGroup.NextNumber = Convert.ToInt32(txtNxtNumber.Text);
                            InfoProductmainGroup.Extra1 = "";
                            InfoProductmainGroup.Extra2 = "";
                            InfoProductmainGroup.BranchId = PublicVariables._branchId;
                            if (!isInEditMode)
                            {
                                if (!SPProductmainGroup.ProductMainGroupExistance(txtGroupName.Text, txtGroupCode.Text, isInEditMode))
                                {

                                    strGroupIdForOtherForms = SPProductmainGroup.ProductMainGroupAdd(InfoProductmainGroup);

                                    MessageBox.Show("Saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (isFromProductCreationForm)
                                    {
                                        this.Close();
                                    }
                                    else
                                    {
                                        txtSearch.Text = "";

                                        ClearFunction();

                                    }


                                }

                                else
                                {
                                    MessageBox.Show("Product group already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtGroupName.SelectAll();
                                    txtGroupName.Focus();
                                }


                            }
                            else
                            {
                                // Do editing
                                InfoProductmainGroup.GroupCode = strGroupIdForEdit;
                                SPProductmainGroup.ProductMainGroupEdit(InfoProductmainGroup);

                                strGroupIdForOtherForms = InfoProductmainGroup.GroupCode;

                                MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Text = "";
                                ClearFunction();

                            }

                        }
                        else
                        {
                            MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("AG5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void Clear()
        {
            try
            {
                //strPrvUnitId = "";
                //StPostingDate = "";
                //cmbTax.Enabled = true;
                //txtProductCode.ReadOnly = false;
                //InitialSettingsForSave();
                //CommonInitialSettings();
                isFormLoad = true;
                strGroupIdForEdit = "";
                strGroupIdForOtherForms = "";
                
                txtGroupCode.Clear();
                txtGroupName.Clear();
               
                txtProductCodeLgh.Text = "0";
                txtNxtNumber.Text = "0";
                chkScaleGroup.Checked = false; 
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                btnDelete.Enabled = false;               
                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC19:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void DeleteProductMainGroup()
        {
            try
            {
                //To delete from Product Group table if reference not exist in other tables

               
                    SPProductmainGroup.ProductMainGroupDelete(strGroupIdForEdit);
                    MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Clear();
                    ClearFunction();

            }
            catch (Exception ex)
            {
                MessageBox.Show("AG6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillRegister()
        {
            // Fill all product group in grid acoring to the text of search by combo
            try
            {
                DataTable dtblReg = new DataTable();
                dtblReg = SPProductmainGroup.ProductMainGroupViewAllForSearch(txtSearch.Text);
                dgvRegister.DataSource = dtblReg;
                dgvRegister.Columns[2].Visible = false;
                dgvRegister.Columns[3].Visible = false;
                dgvRegister.Columns[4].Visible = false;
                dgvRegister.Columns[5].Visible = false;
              
                dgvRegister.Columns[6].Visible = false;
                dgvRegister.Columns[7].Visible = false;
                dgvRegister.Columns[8].Visible = false;

                dgvRegister.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillControlsProductMainGroup()
        {
            try
            {
                if (spUsergroupSettings.CheckUSerGroupPrivilage("Product Main Group", "Edit", "Masters") == true)
                {
                    isEditEnable = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    isEditEnable = false;
                    btnSave.Enabled = false;
                }

                //To fill selected Product Group details to controls
                ProductMainGroupInfo ProductmainGroupInfo = new ProductMainGroupInfo();
                ProductmainGroupInfo.GroupCode = strGroupIdForEdit;
                ProductmainGroupInfo = SPProductmainGroup.ProductMainGroupView(strGroupIdForEdit);
                txtGroupName.Text = ProductmainGroupInfo.GroupName;
                txtGroupCode.Text = ProductmainGroupInfo.GroupCode;
                chkScaleGroup.Checked = ProductmainGroupInfo.ScaleGroup;
                txtProductCodeLgh.Text = ProductmainGroupInfo.ProductCodeLength.ToString();
                txtNxtNumber.Text = ProductmainGroupInfo.NextNumber.ToString();
               // btnDelete.Enabled = true;

                if (isDeleteEnable)//added 19/10/2023
                    btnDelete.Enabled = true;
                else
                    btnDelete.Enabled = false;
                FinacMessage.SaveButtonText(btnSave, "Edit");
                FinacMessage.ClearButtonText(btnClear, "Edit"); 
                txtGroupCode.Enabled = false;
                
                //if (SPProductmainGroup.ProductGroupReferenceCheck(strGroupIdForEdit, true))
                //    cmbGroup.Enabled = false;
                isInEditMode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DoWhenComingFromProductCreationForm(frmProductCreation frm)
        {
            // Function to call from account ledger form to create new group
            gpbRegister.Enabled = false;
            this.frmproductcreationObj = frm;
            isFromProductCreationForm = true;
            base.Show();
        }
        public void DoWhenQuitingForm()
        {
            // Function to execute at the time of closing the form
            // To return to the form rom which this form is called
            if (isFromProductCreationForm)
            {
                frmproductcreationObj.Enabled = true;
                frmproductcreationObj.DowhenReturningFromProductMainGroupForm(strGroupIdForOtherForms);

            }
            //else if (isFromMultiProductCreationForm)
            //{
            //    frmMultipleProductCreationObj.Enabled = true;
            //    frmMultipleProductCreationObj.DowhenReturningFromMultiProductGroupForm(strGroupIdForOtherForms);

            //}

        }

        #endregion
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveOrEdit();
        }

        private void txtProductCodeLgh_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.IntigerFieldKeypress(sender, e);
        }

        private void txtProductCodeLgh_Leave(object sender, EventArgs e)
        {
            try
            {
                Int32.Parse(txtProductCodeLgh.Text);
               
            }
            catch
            {
                txtProductCodeLgh.Text = "0";
            }
            label6.ForeColor = Color.Black;
        }

        private void txtProductCodeLgh_Enter(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(txtProductCodeLgh.Text) == 0m)
                {
                    txtProductCodeLgh.Text = "";
                }
                label6.ForeColor=Color.Red;
            }
            catch
            {
                txtProductCodeLgh.Text = "";
            }
        }

        private void txtNxtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.IntigerFieldKeypress(sender, e);
        }

        private void txtNxtNumber_Enter(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(txtNxtNumber.Text) == 0m)
                {
                    txtNxtNumber.Text = "";
                }
                label9.ForeColor = Color.Red;
            }
            catch
            {
                txtNxtNumber.Text = "";
            }
        }

        private void txtNxtNumber_Leave(object sender, EventArgs e)
        {
            try
            {
                Int32.Parse(txtNxtNumber.Text);
                
            }
            catch
            {
                txtNxtNumber.Text = "0";
            }
            label9.ForeColor = Color.Black;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, "Add", _mainMenuItem) == true)
                {
                    isEditEnable = true;
                    btnSave.Enabled = isEditEnable;
                }
                else
                {
                    isEditEnable = false;
                    btnSave.Enabled = isEditEnable;

                }
                txtSearch.Text = "";
                ClearFunction();

                txtGroupCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG11:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //try
            //{

            //    Clear();
            //   // strProductIdToEdit = "";
            //   // strPrdDtlsIdToEdit = "";
            //    txtGroupCode.Clear();
            //    txtGroupName.Clear();
                

                

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("PC25:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (InventorySettingsInfo._messageBoxDelete)
                {
                    if (MessageBox.Show("Do you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, "Delete", _mainMenuItem) == true)//added on 19/10/2023 sheena
                        {
                            DeleteProductMainGroup();
                        }
                        else
                        {
                            MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
                else
                {
                    if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, "Delete", _mainMenuItem) == true)//added on 19/10/2023 sheena
                    {
                        DeleteProductMainGroup();
                    }
                    else
                    {
                        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG12:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (InventorySettingsInfo._messageBoxClose == true)
                {

                    if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!isFormLoad)
                {
                    FillRegister();
                    if (dgvRegister.CurrentRow != null)
                    {
                        DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(dgvRegister.CurrentCell.ColumnIndex, dgvRegister.CurrentCell.RowIndex);
                        dgvRegister_CellClick(sender, ex);
                    }
                    else
                    {

                        ClearFunction();

                        txtSearch.Focus();
                    }
                    if (dgvRegister.CurrentRow != null)
                        dgvRegister.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG19:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvRegister_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // To fill text box and controls when a row of gri is clicked
            try
            {
                if (dgvRegister.CurrentRow != null)
                {
                    if (dgvRegister.Rows.Count > 0 && e.ColumnIndex > -1)
                    {
                        if (dgvRegister.CurrentRow.Cells[0].Value != null)
                        {
                            if (dgvRegister.CurrentRow.Cells[0].Value.ToString() != "")
                            {
                                strGroupIdForEdit = dgvRegister.CurrentRow.Cells[0].Value.ToString();
                                strGroupName = dgvRegister.CurrentRow.Cells[1].Value.ToString();
                                FillControlsProductMainGroup();
                                dgvRegister.CurrentRow.Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    ClearFunction();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvRegister_KeyUp(object sender, KeyEventArgs e)
        {
            // To execute same function of cell click when up, down, enter, tab key is pressed
            try
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    if (dgvRegister.CurrentRow != null)
                    {
                        DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(dgvRegister.CurrentCell.ColumnIndex, dgvRegister.CurrentCell.RowIndex);
                        dgvRegister_CellClick(sender, ex);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG17:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvRegister_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // To execute same function of cell click when cliking the header also
            try
            {
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex);
                dgvRegister_CellClick(sender, e1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG18:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvRegister_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // To clear the grid selection after data biniding
            ((DataGridView)sender).ClearSelection();
            if (txtSearch.Text == "")
            {
                ((DataGridView)sender).CurrentCell = null;
            }
        }

        private void dgvRegister_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Search gs = new Search();
                //gs.MultiFocus(e, txtSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG28:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmProductMainGroup_Load(object sender, EventArgs e)
        {
            // Initial settings at the time of form load
            try
            {
                _mainMenuItem = "Masters"; //PublicVariables._mainMenuItem;
                if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, "Delete", _mainMenuItem) == true)
                {
                    isDeleteEnable = true;
                    btnDelete.Enabled = isDeleteEnable;
                }
                else
                {
                    isDeleteEnable = false;
                    btnDelete.Enabled = isDeleteEnable;

                }

                //bwrkControlSettings.RunWorkerAsync();
                //frmCompanyProgress.ShowInTaskbar = false;
                //frmCompanyProgress.ShowFromReport();

                txtSearch.Text = "";
                ClearFunction();

            }
            catch (Exception ex)
            {
                MessageBox.Show("AG8:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmProductMainGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DoWhenQuitingForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtGroupCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtGroupName.SelectionStart = 0;
                    txtGroupName.SelectionLength = 0;
                    txtGroupName.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    if (txtGroupCode.Text.Trim() == "" || txtGroupCode.SelectionStart == 0)
                    {
                        btnClose.Focus();
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtProductCodeLgh.SelectionStart = 0;
                    txtProductCodeLgh.SelectionLength = 0;
                    txtProductCodeLgh.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    if (txtGroupName.Text.Trim() == "" || txtGroupName.SelectionStart == 0)
                    {
                        txtGroupCode.Focus();
                        txtGroupCode.SelectionStart = 0;
                        txtGroupCode.SelectionLength = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtProductCodeLgh_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    txtNxtNumber.SelectionStart = 0;
                    txtNxtNumber.SelectionLength = 0;
                    txtNxtNumber.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    if (txtProductCodeLgh.Text.Trim() == "" || txtProductCodeLgh.SelectionStart == 0)
                    {
                        txtGroupName.Focus();
                        txtGroupName.SelectionStart = 0;
                        txtGroupName.SelectionLength = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNxtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    btnSave.Focus();
                }
                else if (e.KeyCode == Keys.Back)
                {
                    if (txtNxtNumber.Text.Trim() == "" || txtNxtNumber.SelectionStart == 0)
                    {
                        txtProductCodeLgh.Focus();
                        txtProductCodeLgh.SelectionStart = 0;
                        txtProductCodeLgh.SelectionLength = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bwrkControlSettings_DoWork(object sender, DoWorkEventArgs e)
        {
            //FinacFormControl objGeneral = new FinacFormControl();
            //objGeneral.formSettings(this);
        }

        private void bwrkControlSettings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (frmCompanyProgress != null && frmCompanyProgress.Visible)
            //    frmCompanyProgress.Close();
        }

        private void frmProductMainGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(e, e);
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                if (btnDelete.Enabled)
                    btnDelete_Click(sender, e);
            }
        }

        private void txtGroupCode_Enter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
        }

        private void txtGroupCode_Leave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
        }

        private void txtGroupName_Enter(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Black;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Red;
        }
    }
}
