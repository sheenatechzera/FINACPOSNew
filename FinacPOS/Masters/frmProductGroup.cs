using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    partial class frmProductGroup : Form
    {
        public frmProductGroup()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);
        }
        
        /******************************************************************************************************************
         *                                       PUBLIC VARIABLES
         *****************************************************************************************************************/
        #region PUBLIC VARIABLES
       
        ProductGroupSP SPProductGroup = new ProductGroupSP();
        UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();//changed on 19/10/2023 sheena    
       // frmProgress frmCompanyProgress = new frmProgress();
        frmProductCreation frmproductcreationObj;
        string strGroupIdForEdit = "";   // to keep group id whileediting
        string strGroupIdForOtherForms = ""; // To keep id for returnign to other forms
        string strGroupName = "";            // To keep group name before editing
        bool isInEditMode = false;  // to indicate whetehr teh form is opened in edit mode
        bool isFromProductCreationForm = false;  // to indicate whetehr this form is called form other forms
        bool isFormLoad = false;
        int  inNarrationCount = 0; // to hold count of enter keys presses
        string  strCategoryText = "";
        string _mainMenuItem = "";
        #endregion
        /******************************************************************************************************************
         *                                      FUNCTIONS
         *****************************************************************************************************************/
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
                txtNarration.Clear();
                txtGroupName.Clear();

                FillProductGroupCombo();
                FillRegister();
                FinacMessage.SaveButtonText(btnSave, "New");
                FinacMessage.ClearButtonText(btnClear, "New"); 
                cmbGroup.SelectedValue = 1;
                txtGroupName.BackColor = Color.White;
                txtGroupName.Enabled = true;
                isInEditMode = false;
                btnDelete.Enabled = false;
                cmbGroup.Enabled = true;
                isFormLoad = false;
                if (!isFromProdCreation && isFormLoad)
                    cmbCategory.SelectedIndex = 0; ;// Select the first item
                cmbCategory.Focus();//by sheena 26-06-2023
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void FillRegister()
        {
            // Fill all product group in grid acoring to the text of search by combo
            try
            {
                DataTable dtblReg = new DataTable();
                dtblReg =SPProductGroup.ProductGroupViewAllForSearch(txtSearch.Text);
                dgvRegister.DataSource = dtblReg;
                dgvRegister.Columns[0].Visible = false;
                 dgvRegister .Columns[3].Visible=false;
                dgvRegister .Columns[4].Visible=false;
                dgvRegister .Columns[5].Visible=false;
                dgvRegister .Columns[6].Visible=false;
                dgvRegister .Columns[7].Visible=false;
                
                dgvRegister .Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG2:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void FillProductGroupCombo()
        {
            try
            {

                DataTable dtbl = new DataTable();
                //dtbl = SPProductGroup.ProductGroupViewAll();//commented by sheena- 26-06-2023
                dtbl = SPProductGroup.ProductGroupViewAllByCategory(strCategoryText);//added by sheena- 26-06-2023
                DataRow[] dr;
                if (strGroupIdForEdit != "")
                {

                    dr = dtbl.Select("groupId='" + strGroupIdForEdit + "'");
                    if (dr.Length > 0)
                    {
                        dtbl.Rows.Remove(dr[0]);
                    }

                }
                cmbGroup.DataSource = dtbl;
                cmbGroup.DisplayMember = dtbl.Columns[1].ToString();
                cmbGroup.ValueMember = dtbl.Columns[0].ToString();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    
        //---------------------------------------------------------------------------------------------------------------------------------------
      
        public void FillControlsProductGroup()
        {
            try
            {
                //To fill selected Product Group details to controls
                ProductGroupInfo ProductGroupInfo = new ProductGroupInfo();               
                ProductGroupInfo = SPProductGroup.ProductGroupView(strGroupIdForEdit);             
                txtGroupName.Text = ProductGroupInfo.GroupName;
                txtNarration.Text = ProductGroupInfo.Narration;
                cmbGroup.SelectedValue = ProductGroupInfo.GroupUnder;
                cmbCategory.Text = ProductGroupInfo.Category;
                btnDelete.Enabled = true;
                FinacMessage.SaveButtonText(btnSave, "Edit");
                FinacMessage.ClearButtonText(btnClear, "Edit"); 
                cmbGroup.Enabled = true;
           
                if (SPProductGroup.ProductGroupReferenceCheck(strGroupIdForEdit,true ))
                    cmbGroup.Enabled = false;
                isInEditMode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
       
        //---------------------------------------------------------------------------------------------------------------------------------------
      
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void SaveOrEdit()
        {
          
            try
            {
                  
                    txtGroupName.Text = txtGroupName.Text.Trim();
                    txtNarration.Text = txtNarration.Text.Trim();
                    
                   
                        if (txtGroupName.Text == "")
                        {
                            MessageBox.Show("Enter product group name","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtGroupName.Focus();
                        }
                        else
                        {
                            if (cmbGroup.SelectedValue == null || cmbGroup.Text .Trim()=="")
                            {
                                MessageBox.Show("Select group under","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                cmbGroup .Focus();
                            }
                            else
                            {
                                bool isSave = true ;
                               
                                    if (InventorySettingsInfo._messageBoxAddEdit)
                                    {
                                        if (!isInEditMode)
                                        {
                                            if (MessageBox.Show("Do you want to save?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                            {
                                                isSave = false;
                                            }
                                        }
                                        else
                                        {
                                            if (MessageBox.Show("Do you want to update?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                                            {
                                                isSave = false;
                                            }
                                        }
                                    }
                                    if (isSave)
                                    {
                                        if (spUsergroupSettings.CheckUSerGroupPrivilage(this.Text, (strGroupIdForEdit == "" ? "Add" : "Edit"), _mainMenuItem) == true)//added on 19/10/2023 sheena
                                        {

                                            if (!SPProductGroup.ProductGroupExistance(txtGroupName.Text, strGroupIdForEdit, cmbCategory.Text, cmbGroup.SelectedValue.ToString()))
                                            {

                                                ProductGroupInfo InfoProductGroup = new ProductGroupInfo();
                                                InfoProductGroup.GroupName = txtGroupName.Text;
                                                InfoProductGroup.GroupUnder = cmbGroup.SelectedValue.ToString();

                                                InfoProductGroup.Narration = txtNarration.Text.Trim();
                                                InfoProductGroup.Extra1 = "";
                                                InfoProductGroup.Extra2 = "";
                                                InfoProductGroup.BranchId = PublicVariables._branchId;
                                                InfoProductGroup.Category = cmbCategory.Text;
                                                if (!isInEditMode)
                                                {
                                                    strGroupIdForOtherForms = SPProductGroup.ProductGroupAdd(InfoProductGroup);
                                                    if (cmbCategory.Text == "Category 1")
                                                    {
                                                        if (strGroupIdForOtherForms != "")
                                                        {
                                                            InfoProductGroup = new ProductGroupInfo();
                                                            InfoProductGroup.GroupName = "NA";
                                                            InfoProductGroup.GroupUnder = strGroupIdForOtherForms;//category 1 id
                                                            InfoProductGroup.Narration = "";
                                                            InfoProductGroup.Extra1 = "";
                                                            InfoProductGroup.Extra2 = "";
                                                            InfoProductGroup.BranchId = PublicVariables._branchId;
                                                            InfoProductGroup.Category = "Category 2";
                                                            string strGroupIdForCat2 = SPProductGroup.ProductGroupAdd(InfoProductGroup);
                                                            if (strGroupIdForCat2 != "")
                                                            {
                                                                InfoProductGroup = new ProductGroupInfo();
                                                                InfoProductGroup.GroupName = "NA";
                                                                InfoProductGroup.GroupUnder = strGroupIdForCat2;//category 2 id
                                                                InfoProductGroup.Narration = "";
                                                                InfoProductGroup.Extra1 = "";
                                                                InfoProductGroup.Extra2 = "";
                                                                InfoProductGroup.BranchId = PublicVariables._branchId;
                                                                InfoProductGroup.Category = "Category 3";
                                                                string strGroupIdForCat3 = SPProductGroup.ProductGroupAdd(InfoProductGroup);
                                                                if (strGroupIdForCat3 != "")
                                                                {
                                                                    InfoProductGroup = new ProductGroupInfo();
                                                                    InfoProductGroup.GroupName = "NA";
                                                                    InfoProductGroup.GroupUnder = strGroupIdForCat3;//category 3 id
                                                                    InfoProductGroup.Narration = "";
                                                                    InfoProductGroup.Extra1 = "";
                                                                    InfoProductGroup.Extra2 = "";
                                                                    InfoProductGroup.BranchId = PublicVariables._branchId;
                                                                    InfoProductGroup.Category = "Category 4";
                                                                    string strGroupIdForCat4 = SPProductGroup.ProductGroupAdd(InfoProductGroup);
                                                                }
                                                            }
                                                        }

                                                    }

                                                    MessageBox.Show("Saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    if (isFromProductCreationForm )
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
                                                    // Do editing
                                                    InfoProductGroup.GroupId = strGroupIdForEdit;
                                                    SPProductGroup.ProductGroupEdit(InfoProductGroup);

                                                    strGroupIdForOtherForms = InfoProductGroup.GroupId;

                                                    MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    txtSearch.Text = "";
                                                    ClearFunction();

                                                }
                                            }

                                            else
                                            {
                                                MessageBox.Show("Product group already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                txtGroupName.SelectAll();
                                                cmbCategory.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                
                            }
                        }
                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG5:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
     
        public void DeleteProductGroup()
        {
            try
            {
                //To delete from Product Group table if reference not exist in other tables
                
                if (!SPProductGroup.ProductGroupReferenceCheck(strGroupIdForEdit,false ))
                {
                    SPProductGroup.ProductGroupDelete(strGroupIdForEdit);
                    MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Clear();
                    ClearFunction();
                   
                }
                else
                {
                    MessageBox.Show("Can't delete, reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        string prdCreationCategory = ""; bool isFromProdCreation = false;
        public void DoWhenComingFromProductCreationForm(frmProductCreation frm, string category)
        {
            // Function to call from account ledger form to create new group
            gpbRegister.Enabled = false;
            this.frmproductcreationObj = frm;        
            isFromProductCreationForm = true;
            prdCreationCategory = category;
            isFromProdCreation = true;
            cmbCategory.Text = category;
            base.Show();
        }
        
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void DoWhenQuitingForm()
        {
            // Function to execute at the time of closing the form
            // To return to the form rom which this form is called
            if (isFromProductCreationForm)
            {
                frmproductcreationObj.Enabled = true;
                frmproductcreationObj.DowhenReturningFromProductGroupForm(strGroupIdForOtherForms, prdCreationCategory);
              
            }
            //else if (isFromMultiProductCreationForm)
            //{
            //    frmMultipleProductCreationObj.Enabled = true;
            //    frmMultipleProductCreationObj.DowhenReturningFromMultiProductGroupForm(strGroupIdForOtherForms);

            //}
            
        }
        #endregion
        /******************************************************************************************************************
         *                                       EVENTS
         *****************************************************************************************************************/
        #region  EVENTS
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
                MessageBox.Show("AG7:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------
        private void frmAccountGroup_Load(object sender, EventArgs e)
        {
            // Initial settings at the time of form load
            try
            {
                _mainMenuItem = "Masters"; //PublicVariables._mainMenuItem;
                //bwrkControlSettings.RunWorkerAsync();
                //frmCompanyProgress.ShowInTaskbar = false;
                //frmCompanyProgress.ShowFromReport();

                txtSearch.Text = "";
                ClearFunction();
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG8:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void frmAccountGroup_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DoWhenQuitingForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG9:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveOrEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG10:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = "";
                ClearFunction();
               
               // txtGroupName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG11:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
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
                            DeleteProductGroup();
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
                        DeleteProductGroup();
                    }

                    else
                    {
                        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG12:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void txtGroupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // To focus into account group combo when enter key is pressed
            try
            {
                if (e.KeyChar == 13)
                    if (cmbGroup .Enabled == true)
                    {
                        cmbGroup.Focus();
                    }
                    else
                    {
                        txtNarration.Focus();
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG12:"+ ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void cmbGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void cmbGroup_KeyDown(object sender, KeyEventArgs e)
        {
            // To drop down the combo while pressing space
            try
            {
               
                    if (e.KeyCode  == Keys.Enter )
                    {
                        cmbGroup.Text = strGroupText;

                        string strDescrip = txtNarration.Text.Trim();
                        if (strDescrip == "")
                        {
                            txtNarration.SelectionStart = 0;
                            txtNarration.SelectionLength = 0;
                            txtNarration.Focus();
                        }
                        else
                        {
                            txtNarration.SelectionStart = strDescrip.Length;
                            txtNarration.Focus();
                        }

                    }
                    else  if (e.KeyCode == Keys.Back)
                {
                  

                    if (cmbGroup.Text.Trim() == "" || cmbGroup.SelectionStart == 0)
                    {
                        txtGroupName .Focus();
                        txtGroupName.SelectionStart = 0;
                        txtGroupName.SelectionLength = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG13:"+ ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            // To count number of time enter key is pressed from this text box.focus should goto save button when enter key is pressed twice simultaneously
            try
            {
                if (e.KeyChar == 13)
                {

                    inNarrationCount ++;
                    if (inNarrationCount  == 2)
                    {
                        inNarrationCount = 0;
                        btnSave.Focus();
                    }
                }
                else
                {
                    inNarrationCount = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG14:" +ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void txtNarration_Enter(object sender, EventArgs e)
        {
            try
            {
                inNarrationCount = 0;
                txtNarration.Text = txtNarration.Text.Trim();
                if (txtNarration.Text == "")
                {
                    txtNarration.SelectionStart = 0;
                    txtNarration.SelectionLength = 0;
                    txtNarration.Focus();
                }
                else
                {
                    txtNarration.SelectionStart = txtNarration.Text.Length;
                    txtNarration.Focus();
                }
                label7.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG15:"+ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
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
                                FillProductGroupCombo();
                                FillControlsProductGroup();
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
                MessageBox.Show("AG16:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
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
                MessageBox.Show("AG17:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void dgvRegister_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // To clear the grid selection after data biniding
            ((DataGridView)sender).ClearSelection();
            if (txtSearch.Text == "")
            {
                ((DataGridView)sender).CurrentCell = null;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
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
                MessageBox.Show("AG18:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
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
                MessageBox.Show("AG19:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------

        private void cmbGroup_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroup.SelectedIndex == -1)
                    cmbGroup.Text = strGroupText;
                ComboValidation objComboValidation = new ComboValidation();
                objComboValidation.CheckCollection(cmbGroup );
                label2.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG20:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void cmbGroup_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroup.Text.Trim() == "")
                {
                    cmbGroup.Text = cmbGroup.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG21:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void frmAccountGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Short cut keys
                if (e.Control && e.KeyCode == Keys.S)
                {
                    SaveOrEdit();
                }
                else if ((e.Control && e.KeyCode == Keys.D) && isInEditMode )
                {

                    btnDelete_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG22" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void btnSave_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    
                        txtNarration .Focus();
                        txtNarration .SelectionStart = 0;
                        txtNarration.SelectionLength = 0;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG23:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void txtNarration_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtNarration.Text == "" || txtNarration.SelectionStart == 0)
                    {
                        if (cmbGroup.Enabled == true)
                        {
                            cmbGroup.Focus();
                            cmbGroup.SelectionStart = 0;
                            cmbGroup.SelectionLength = 0;
                        }
                        else
                        {
                            txtGroupName .Focus();
                            txtGroupName.SelectionStart = 0;
                            txtGroupName.SelectionLength = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG24:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                    if (btnDelete.Enabled)
                        btnDelete .Focus();
                    else
                        btnClear.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG25:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void btnDelete_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                    btnClear.Focus();
                       
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG26:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void btnClear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                    btnSave .Focus();
                       
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG27:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        private void dgvRegister_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //Search gs = new Search();
                //gs.MultiFocus(e, txtSearch);
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG28:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        string strGroupText = "";
        private void cmbGroup_KeyUp(object sender, KeyEventArgs e)
        {
            strGroupText = cmbGroup.Text;
        }
        //---------------------------------------------------------------------------------------------------------------------------------------

        #endregion
       // frmMultipleProductCreation frmMultipleProductCreationObj;
        //bool isFromMultiProductCreationForm = false;
        //public void DoWhenComingFromMultiProductCreationForm(frmMultipleProductCreation frm)
        //{
        //    // Function to call from account ledger form to create new group
        //    gpbRegister.Enabled = false;
        //    this.frmMultipleProductCreationObj = frm;
        //    isFromMultiProductCreationForm = true;
        //    base.Show();
        //}

        private void cmbGroup_Enter(object sender, EventArgs e)
        {
            strGroupText = cmbGroup.Text;
            label2.ForeColor = Color.Red;
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            strGroupText = cmbGroup.Text;
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.Text == "Category 1")
                strCategoryText = "NULL";
            else if(cmbCategory.Text == "Category 2")
                strCategoryText = "Category 1";
            else if (cmbCategory.Text == "Category 3")
                strCategoryText = "Category 2";
            else if (cmbCategory.Text == "Category 4")
                strCategoryText = "Category 3";
            FillProductGroupCombo();
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                   

                    string strDescrip = txtGroupName.Text.Trim();
                    if (strDescrip == "")
                    {
                        txtGroupName.SelectionStart = 0;
                        txtGroupName.SelectionLength = 0;
                        txtGroupName.Focus();
                    }
                    else
                    {
                        txtGroupName.SelectionStart = strDescrip.Length;
                        txtGroupName.Focus();
                    }

                }
            }
            catch (Exception ex)
            { }
        }

        private void txtGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (txtGroupName.Text == "" || txtGroupName.SelectionStart == 0)
                    {
                       
                            cmbCategory.Focus();
                            //cmbCategory.SelectionStart = 0;
                            //cmbCategory.SelectionLength = 0;
                       
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("AG24:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Red;
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
        }

        private void txtGroupName_Enter(object sender, EventArgs e)
        {
            label1.ForeColor=Color.Red;
        }

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Black;
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Black;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Red;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Black;
        }
    }
}