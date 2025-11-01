using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    partial class frmMasterCreation : Form
    {
        public frmMasterCreation()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);
        }
   
        #region Declarations
        UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();//changed on 19/10/2023 sheena    
        bool isDeleteEnable = false;// added 19/10/2023
        bool isEditEnable = true;// added 19/10/2023
        int i = 0; // for keypress event of txtNarration

        string strId = ""; //to keep the Id for Edit mode
        string strNewId = ""; //to keep Id for
        string strCallFor = ""; //to confirm which form is calling

        bool isAddOrModify = false; // false for Save
        bool isClose = false; // To decide form close after save

        MDIFinacPOS _MDIFinacAcount; // Object of MDI form
       // frmAccountLedger frmaccount; // Object Account Ledger
        frmCustomer frmcust; // Object Customer
      //  frmMarket frmmarket;
       // frmSalesOrder frmsalesorder;
       // frmProformaInvoice frmProformaInvoice;
        frmProductCreation frmproductcreation;
        //frmProgress frmCompanyProgress = new frmProgress();
      
       
        #endregion

        #region Navigation

        private void cmbMaterType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus into txtName when enter key is pressed
                if (e.KeyCode == Keys.Enter)
                {
                    cmbMaterType.Text = strMasterText;
                    txtName.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("MC1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus into txtName when enter key is pressed
                if (e.KeyCode == Keys.Enter)
                {
                    txtNarration.Focus();
                }
                // To focus into combo cmbMaterType when Back key is pressed
                if (e.KeyCode == Keys.Back)
                {
                    if (txtName.Text == "" || txtName.SelectionStart == 0)
                    {
                        cmbMaterType.Focus();
                        cmbMaterType.SelectionStart = 0;
                        cmbMaterType.SelectionLength = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC2:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // To count number of time enter key press from this text box.
                // Focus should goto save button when enter key is pressed twice simultaneously
                if (e.KeyChar == 13)
                {
                    i++;
                    if (i == 2)
                    {
                        i = 0;
                        btnSave.Focus();
                    }
                }
                else
                {
                    i = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC3:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus to txtName text box when Back key pressed
                if (e.KeyCode == Keys.Back)
                {
                    if (txtNarration.Text == "" || txtNarration.SelectionStart == 0)
                    {
                        txtName.Focus();
                        txtName.SelectionStart = 0;
                        txtName.SelectionLength = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC4:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus to txtNarration text box when Back key pressed
                if (e.KeyCode == Keys.Back)
                {
                    txtNarration.Focus();
                    txtNarration.SelectionStart = 0;
                    txtNarration.SelectionLength = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC5:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnDelete_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus to save button when Back key pressed
                if (e.KeyCode == Keys.Back)
                {
                    btnSave.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC6:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus to Delete button when Back key pressed
                if (e.KeyCode == Keys.Back)
                {

                    btnDelete.Focus();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC7:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus to clear button when Back key pressed
                if (e.KeyCode == Keys.Back)
                {
                    btnClear.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC8:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Functions
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
        public void CreateDatasource()
        {
           
            // To create datasource for combo after chekign privilege
            DataTable dtbl = new DataTable();
            DataRow dr = dtbl.NewRow();
            DataColumn c = new DataColumn();
            dtbl.Columns.Add(c);
            dr[c] = "--Select--";
            dtbl.Rows.Add(dr);
            // CheckUserPrivilege SPPrivilege = new CheckUserPrivilege();
            UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();
            if (spUsergroupSettings.CheckUSerGroupPrivilage("Area", "", "Masters") == true)             
            {
                dr = dtbl.NewRow();
                dr[c] = "Area";
                dtbl.Rows.Add(dr);
            }
            if (spUsergroupSettings.CheckUSerGroupPrivilage("Brand", "", "Masters") == true)              
            {
                dr = dtbl.NewRow();
                dr[c] = "Brand";
                dtbl.Rows.Add(dr);
            }
            if (InventorySettingsInfo._maintainGodown == true && spUsergroupSettings.CheckUSerGroupPrivilage("Go-down", "", "Masters") == true)
            {
                dr = dtbl.NewRow();
                dr[c] = "Go-down";
                dtbl.Rows.Add(dr);
            }
            if (spUsergroupSettings.CheckUSerGroupPrivilage("Pricing Level", "","Masters") == true)
            {
                dr = dtbl.NewRow();
                dr[c] = "Pricing Level";
                dtbl.Rows.Add(dr);
            }
            
            if (spUsergroupSettings.CheckUSerGroupPrivilage("Unit", "","Masters") == true)
            {
                dr = dtbl.NewRow();
                dr[c] = "Unit";
                dtbl.Rows.Add(dr);
            }
            cmbMaterType.DataSource = dtbl;
            cmbMaterType.ValueMember = c.ToString();
            cmbMaterType.DisplayMember = c.ToString();
        }
        //---Clear Function--- //

        public void ClearFunction()
        {
            //---Clear All Text boxes and Combo Box---//
            try
            {
                txtName.Clear();
                txtNarration.Clear();

                if (strCallFor == "")
                    cmbMaterType.Focus();
                else
                    txtName.Focus();
                FillRegister();
                isAddOrModify = false;
                btnSave.Text = "Save";
                btnClear.Text = "Clear";
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC9:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Save Functions--- //

        public void SaveFunction() 
        {
            // Function for to decide whether the details save to which table
            try
            {
                if (cmbMaterType.Text.ToString().Trim() != "--Select--" && cmbMaterType.Text.ToString().Trim() != "")
                {
                    if (txtName.Text.Trim() != "")
                    {
                        if (InventorySettingsInfo._messageBoxAddEdit == true)
                        {
                            if (!isAddOrModify)
                            {
                                if (MessageBox.Show("Do you want to save?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Add" , PublicVariables._mainMenuItem) == true)//added on 19/10/2023 sheena
                                    {
                                        SaveSelect();
                                    }
                                    else
                                    {
                                        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Do you want to update?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Add" , PublicVariables._mainMenuItem) == true)//added on 19/10/2023 sheena
                                    {
                                        SaveSelect();
                                    }
                                    else
                                    {
                                        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                            }
                        }
                        else
                        {
                            SaveSelect();
                        }
                    }
                    else
                    {
                        Message();
                        txtName.Clear();
                        txtName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Select master type","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbMaterType.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC10:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SaveSelect()
        {
            try
            {
                if (cmbMaterType.Text .ToString() == "Area")
                {
                    SaveArea();
                }
                if (cmbMaterType.Text.ToString() == "Brand")
                {
                    SaveBrand();
                }
                if (cmbMaterType.Text.ToString() == "Go-down")
                {
                    SaveGodown();
                }
                if (cmbMaterType.Text.ToString() == "Pricing Level")
                {
                    //SavePricingLevel();
                }
               
                if (cmbMaterType.Text.ToString() == "Unit")
                {
                    SaveUnit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC11:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SaveArea()
        {
            //--- Function for Save Details to tbl_Area---//
            try
            {
                bool isExist = false; // to check area name exist or not
                txtName.Text = txtName.Text.Trim();
                txtNarration.Text = txtNarration.Text.Trim();
                AreaInfo AreaInfo = new AreaInfo();
                AreaSP AreaSP = new AreaSP();
                if (!isAddOrModify)
                {
                    //---- Add ----//
                    isExist = AreaSP.AreaExistance(txtName.Text);
                    if (!isExist)
                    {
                        //if Area name not exist
                        AreaInfo.AreaName = txtName.Text;
                        AreaInfo.Narration = txtNarration.Text;
                        AreaInfo.BranchId =PublicVariables._branchId;
                        AreaInfo.Extra1 = "";
                        AreaInfo.Extra2 = "";
                        strNewId = AreaSP.AreaAdd(AreaInfo);
                        MessageBox.Show("Saved successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearch.Clear();
                        ClearFunction();
                        
                        if (isClose)
                            this.Close();
                    }
                    else
                    {
                        //if Area name exist show message box
                        MessageBox.Show("Area already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Focus();
                    }
                }
                else
                {
                    //---- Edit ----//
                    if (dgvRegister.RowCount > 0)
                    {
                        string strName = dgvRegister.CurrentRow.Cells[1].Value.ToString();
                        if (strName.ToLower() != txtName.Text.ToLower())
                        {
                            isExist = AreaSP.AreaExistance(txtName.Text);
                        }
                        else
                        {
                            isExist = false;
                        }
                        if (!isExist)
                        {
                            //if Area name not exist
                            AreaInfo.AreaId = strId;
                            AreaInfo.AreaName = txtName.Text;
                            AreaInfo.Narration = txtNarration.Text;
                            AreaInfo.BranchId =PublicVariables._branchId;
                            AreaInfo.Extra1 = "";
                            AreaInfo.Extra2 = "";
                            AreaSP.AreaEdit(AreaInfo);
                            MessageBox.Show("Updated successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSearch.Clear();
                            ClearFunction();
                           
                        }
                        else
                        {
                            //if Area name exist
                            MessageBox.Show("Area already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtName.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC12:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SaveBrand()
        {
            //--- Function for Save Details to tbl_Brand---//
            try
            {
                bool isExist = false;// to check Godown name exist or not
                txtName.Text = txtName.Text.Trim();
                txtNarration.Text = txtNarration.Text.Trim();
                BrandInfo BrandInfo = new BrandInfo();
                BrandSP BrandSP = new BrandSP();
                if (!isAddOrModify)
                {
                    //---- Add ----//
                    isExist = BrandSP.BrandExistance(txtName.Text);
                    if (!isExist)
                    {
                        //if Brand name not exist
                        BrandInfo.BrandName = txtName.Text;
                        BrandInfo.Narration = txtNarration.Text;
                        BrandInfo.BranchId = "NA";
                        BrandInfo.Extra1 = "";
                        BrandInfo.Extra2 = "";
                        strNewId = BrandSP.BrandAdd(BrandInfo);
                        MessageBox.Show("Saved successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearch.Clear();
                        ClearFunction();
                      
                        if (isClose)
                            this.Close();
                    }
                    else
                    {
                        //if Brand name exist
                        MessageBox.Show("Brand already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Focus();
                    }
                }
                else
                {
                    //----- Edit -----//
                    if (dgvRegister.RowCount > 0)
                    {
                        string strName = dgvRegister.CurrentRow.Cells[1].Value.ToString();
                        if (strName.ToLower() != txtName.Text.ToLower())
                        {
                            isExist = BrandSP.BrandExistance(txtName.Text);
                        }
                        else
                        {
                            isExist = false;
                        }
                        if (!isExist)
                        {
                            //if Area name not exist
                            BrandInfo.BrandId = strId;
                            BrandInfo.BrandName = txtName.Text;
                            BrandInfo.Narration = txtNarration.Text;
                            BrandInfo.BranchId = "NA";
                            BrandInfo.Extra1 = "";
                            BrandInfo.Extra2 = "";
                            BrandSP.BrandEdit(BrandInfo);
                            MessageBox.Show("Updated successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSearch.Clear();
                            ClearFunction();
                        
                        }
                        else
                        {
                            //if Area name exist
                            MessageBox.Show("Brand already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtName.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC13:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SaveGodown()
        {
            //--- Function for Save Details to tbl_Godown---//
            try
            {
                bool isExist = false; // to check godown name exist or not
                txtName.Text = txtName.Text.Trim();
                txtNarration.Text = txtNarration.Text.Trim();
                GodownInfo GodownInfo = new GodownInfo();
                GodownSP GodownSP = new GodownSP();
                if (!isAddOrModify)
                {
                    // ------ Add ------ //
                    isExist = GodownSP.GodownExistance(txtName.Text);
                    if (!isExist)
                    {
                        //if Godown name not exist
                        GodownInfo.GodownName = txtName.Text;
                        GodownInfo.Narration = txtNarration.Text;
                        GodownInfo.BranchId =PublicVariables._branchId;
                        GodownInfo.Extra1 = "";
                        GodownInfo.Extra2 = "";
                        strNewId = GodownSP.GodownAdd(GodownInfo);
                        MessageBox.Show("Saved successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearch.Clear();
                        ClearFunction();
                      
                        if (isClose)
                            this.Close();
                    }
                    else
                    {
                        //if Godown name exist
                        MessageBox.Show("Go-down already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Focus();
                    }
                }
                else
                {
                    //----- Edit-------//
                    if (dgvRegister.RowCount > 0)
                    {
                        string strName = dgvRegister.CurrentRow.Cells[1].Value.ToString();
                        if (strName.ToLower() != txtName.Text.ToLower())
                        {
                            isExist = GodownSP.GodownExistance(txtName.Text);
                        }
                        else
                        {
                            isExist = false;
                        }
                        if (!isExist)
                        {
                            //if Godown name not exist
                            GodownInfo.GodownId = strId;
                            GodownInfo.GodownName = txtName.Text;
                            GodownInfo.Narration = txtNarration.Text;
                            GodownInfo.BranchId =PublicVariables._branchId;
                            GodownInfo.Extra1 = "";
                            GodownInfo.Extra2 = "";
                            GodownSP.GodownEdit(GodownInfo);
                            MessageBox.Show("Updated successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSearch.Clear();
                            ClearFunction();
                         
                        }
                        else
                        {
                            //if Godown name exist
                            MessageBox.Show("Go-down already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtName.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC14:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public void SavePricingLevel()
        //{
        //    //--- Function for Save Details to tbl_PringLevel---//
        //    try
        //    {
        //        bool isExist = false; // to check pricing level name exist or not
        //        txtName.Text = txtName.Text.Trim();
        //        txtNarration.Text = txtNarration.Text.Trim();
        //        PricingLevelInfo PricingLevelInfo = new PricingLevelInfo();
        //        PricingLevelSP PricingLevelSP = new PricingLevelSP();
        //        if (!isAddOrModify)
        //        {
        //            //---- Add-----//
        //            isExist = PricingLevelSP.PricingLevelExistance(txtName.Text);
        //            if (!isExist)
        //            {
        //                //if pricing level name not exist
        //                PricingLevelInfo.PricingLevelName = txtName.Text;
        //                PricingLevelInfo.Narration = txtNarration.Text;
        //                PricingLevelInfo.BranchId =PublicVariables._branchId;
        //                PricingLevelInfo.Extra1 = "";
        //                PricingLevelInfo.Extra2 = "";
        //                strNewId = PricingLevelSP.PricingLevelAdd(PricingLevelInfo);
        //                MessageBox.Show("Saved successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                txtSearch.Clear();
        //                ClearFunction();
                     
        //                if (isClose)
        //                    this.Close();
        //            }
        //            else
        //            {
        //                //if pricing level name exist
        //                MessageBox.Show("Pricing Level already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                txtName.Focus();
        //            }
        //        }
        //        else
        //        {
        //            //------ Edit-------//
        //            if (dgvRegister.RowCount > 0)
        //            {
        //                string strName = dgvRegister.CurrentRow.Cells[1].Value.ToString();
        //                if (strName.ToLower() != txtName.Text.ToLower())
        //                {
        //                    isExist = PricingLevelSP.PricingLevelExistance(txtName.Text);
        //                }
        //                else
        //                {
        //                    isExist = false;
        //                }
        //                if (!isExist)
        //                {
        //                    //if pricing level name not exist
        //                    PricingLevelInfo.PricingLevelId = strId;
        //                    PricingLevelInfo.PricingLevelName = txtName.Text;
        //                    PricingLevelInfo.Narration = txtNarration.Text;
        //                    PricingLevelInfo.BranchId =PublicVariables._branchId;
        //                    PricingLevelInfo.Extra1 = "";
        //                    PricingLevelInfo.Extra2 = "";
        //                    PricingLevelSP.PricingLevelEdit(PricingLevelInfo);
        //                    MessageBox.Show("Updated successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    txtSearch.Clear();
        //                    ClearFunction();
                          
        //                }
        //                else
        //                {
        //                    //if pricing level name exist
        //                    MessageBox.Show("Pricing Level already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    txtName.Focus();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("MC15:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
      
        public void SaveUnit()
        {
            //--- Function for Save Details to tbl_Unit---//
            try
            {
                bool isExist = false; // to check Unit name exist or not
                txtName.Text = txtName.Text.Trim();
                txtNarration.Text = txtNarration.Text.Trim();
                UnitInfo UnitInfo = new UnitInfo();
                UnitSP UnitSP = new UnitSP();
                if (!isAddOrModify)
                {
                    //--------Add -----//
                    isExist = UnitSP.UnitExistance(txtName.Text);
                    if (!isExist)
                    {
                        //if unit name not exist
                        UnitInfo.UnitName = txtName.Text;
                        UnitInfo.Narration = txtNarration.Text;
                        UnitInfo.BranchId =PublicVariables._branchId;
                        UnitInfo.Extra1 = "";
                        UnitInfo.Extra2 = "";
                        strNewId = UnitSP.UnitAdd(UnitInfo);
                        MessageBox.Show("Saved successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearch.Clear();
                        ClearFunction();
                      
                        if (isClose)
                            this.Close();
                    }
                    else
                    {
                        //if unit name exist
                        MessageBox.Show("Unit already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtName.Focus();
                    }
                }
                else
                {
                    //-------Edit------//
                    if (dgvRegister.RowCount > 0)
                    {
                        string strName = dgvRegister.CurrentRow.Cells[1].Value.ToString();
                        if (strName.ToLower() != txtName.Text.ToLower())
                        {
                            isExist = UnitSP.UnitExistance(txtName.Text);
                        }
                        else
                        {
                            isExist = false;
                        }
                        if (!isExist)
                        {
                            //if unit name not exist
                            UnitInfo.UnitId = strId;
                            UnitInfo.UnitName = txtName.Text;
                            UnitInfo.Narration = txtNarration.Text;
                            UnitInfo.BranchId =PublicVariables._branchId;
                            UnitInfo.Extra1 = "";
                            UnitInfo.Extra2 = "";
                            UnitSP.UnitEdit(UnitInfo);
                            MessageBox.Show("Updated successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSearch.Clear();
                            ClearFunction();
                          
                        }
                        else
                        {
                            //if unit name exist
                            MessageBox.Show("Unit already exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtName.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC17:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Message Function--- //

        public void Message()
        {
            //Function to decide which message is to show
            try
            {
                if (cmbMaterType.Text.ToString() == "Area")
                {
                    MessageBox.Show("Enter area name","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbMaterType.Text.ToString() == "Brand")
                {
                    MessageBox.Show("Enter brand name","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbMaterType.Text.ToString() == "Go-down")
                {
                    MessageBox.Show("Enter Go-down name","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (cmbMaterType.Text.ToString() == "Pricing Level")
                {
                    MessageBox.Show("Enter pricing level name","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
                if (cmbMaterType.Text.ToString() == "Unit")
                {
                    MessageBox.Show("Enter unit name","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC18:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Fill Register Functions--- //

        public void FillRegister()
        {
            try
            {
                //Function to decide, details from which table to fill in grid 
                if (cmbSearchType.Text.Trim() != "")
                {
                    if (cmbSearchType.SelectedItem.ToString() == "--Select--")
                    {
                        dgvRegister.DataSource = null;
                    }
                    if (cmbSearchType.SelectedItem.ToString() == "Area")
                    {
                        FillArea();
                    }
                    if (cmbSearchType.SelectedItem.ToString() == "Brand")
                    {
                        FillBrand();
                    }
                    if (cmbSearchType.SelectedItem.ToString() == "Go-down")
                    {
                        FillGodown();
                    }
                    if (cmbSearchType.SelectedItem.ToString() == "Pricing Level")
                    {
                        //FillPricingLevel();
                    }
                    
                    if (cmbSearchType.SelectedItem.ToString() == "Unit")
                    {
                        FillUnit();
                    }
                }
                if (dgvRegister.Rows.Count > 0 && txtSearch.Text != "")
                    dgvRegister.CurrentCell = dgvRegister.Rows[0].Cells[1];
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC19:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillArea()
        {
            try
            {
                //Fill Area name to dgvRegister for edit and delete
                AreaSP AreaSP = new AreaSP();
                dgvRegister.DataSource = AreaSP.AreaViewAllForSearch(txtSearch.Text,PublicVariables._branchId);
                dgvRegister.Columns[0].Visible = false;
                dgvRegister.Columns[2].Visible = false;
                dgvRegister.Columns[3].Visible = false;
                dgvRegister.Columns[4].Visible = false;
                dgvRegister.Columns[5].Visible = false;
                dgvRegister.Columns[6].Visible = false;
                dgvRegister.ClearSelection();
                dgvRegister.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (PublicVariables._ModuleLanguage == "ARB")
                    dgvRegister.Columns[1].HeaderText = "اسم المنطقة";
                else
                    dgvRegister.Columns[1].HeaderText = "Area name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC20:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillBrand()
        {
            try
            {
                //Fill Brand name to dgvRegister for edit and delete
                BrandSP BrandSP = new BrandSP();
                dgvRegister.DataSource = BrandSP.BrandViewAllForSearch(txtSearch.Text);
                dgvRegister.Columns[0].Visible = false;
                dgvRegister.Columns[2].Visible = false;
                dgvRegister.Columns[3].Visible = false;
                dgvRegister.Columns[4].Visible = false;
                dgvRegister.Columns[5].Visible = false;
                dgvRegister.Columns[6].Visible = false;
                dgvRegister.ClearSelection();
                dgvRegister.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (PublicVariables._ModuleLanguage == "ARB")
                    dgvRegister.Columns[1].HeaderText = "اسم العلامة التجارية";
                else
                    dgvRegister.Columns[1].HeaderText = "Brand name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC21:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillGodown()
        {
            try
            {
                //Fill Area Godown to dgvRegister for edit and delete
                GodownSP GodownSP = new GodownSP();
                dgvRegister.DataSource = GodownSP.GodownViewAllForSearch(txtSearch.Text);
                dgvRegister.Columns[0].Visible = false;
                dgvRegister.Columns[2].Visible = false;
                dgvRegister.Columns[3].Visible = false;
                dgvRegister.Columns[4].Visible = false;
                dgvRegister.Columns[5].Visible = false;
                dgvRegister.Columns[6].Visible = false;
                dgvRegister.ClearSelection();
                dgvRegister.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
               
                if (PublicVariables._ModuleLanguage == "ARB")
                    dgvRegister.Columns[1].HeaderText = "اسم جودا";
                else
                    dgvRegister.Columns[1].HeaderText = "Godown name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC22:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public void FillPricingLevel()
        //{
        //    try
        //    {
        //        //Fill Pricing Level name to dgvRegister for edit and delete
        //        PricingLevelSP PricingLevelSP = new PricingLevelSP();
        //        dgvRegister.DataSource = PricingLevelSP.PricingLevelViewAllForSearch(txtSearch.Text);
        //        dgvRegister.Columns[0].Visible = false;
        //        dgvRegister.Columns[2].Visible = false;
        //        dgvRegister.Columns[3].Visible = false;
        //        dgvRegister.Columns[4].Visible = false;
        //        dgvRegister.Columns[5].Visible = false;
        //        dgvRegister.Columns[6].Visible = false;
        //        dgvRegister.ClearSelection();
        //        dgvRegister.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        //        if (PublicVariables._ModuleLanguage == "ARB")
        //            dgvRegister.Columns[1].HeaderText = "اسم مستوى التسعير";
        //        else
        //            dgvRegister.Columns[1].HeaderText = "Pricing level name";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("MC23:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        public void FillUnit()
        {
            try
            {
                //Fill Unit name to dgvRegister for edit and delete
                UnitSP UnitSP = new UnitSP();
                dgvRegister.DataSource = UnitSP.UnitViewAllForSearch(txtSearch.Text);
                dgvRegister.Columns[0].Visible = false;
                dgvRegister.Columns[2].Visible = false;
                dgvRegister.Columns[3].Visible = false;
                dgvRegister.Columns[4].Visible = false;
                dgvRegister.Columns[5].Visible = false;
                dgvRegister.Columns[6].Visible = false;
                dgvRegister.ClearSelection();
                dgvRegister.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                if (PublicVariables._ModuleLanguage == "ARB")
                    dgvRegister.Columns[1].HeaderText = "إسم الوحدة";
                else
                    dgvRegister.Columns[1].HeaderText = "Unit name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC24:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Fill Control Functions--- //

        public void FillControlsArea()
        {
            try
            {
                //To fill selected area details to controls
                AreaInfo AreaInfo = new AreaInfo();
                AreaSP AreaSP = new AreaSP();
                AreaInfo = AreaSP.AreaView(strId);
                cmbMaterType.Text = "Area";
                txtName.Text = AreaInfo.AreaName;
                txtNarration.Text = AreaInfo.Narration;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC25:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillControlsBrand()
        {
            try
            {
                //To fill selected Brand details to controls
                BrandInfo BrandInfo = new BrandInfo();
                BrandSP BrandSP = new BrandSP();
                BrandInfo = BrandSP.BrandView(strId);
                cmbMaterType.Text = "Brand";
                txtName.Text = BrandInfo.BrandName;
                txtNarration.Text = BrandInfo.Narration;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC26:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillControlsGodown()
        {
            try
            {
                //To fill selected Godown details to controls
                GodownInfo GodownInfo = new GodownInfo();
                GodownSP GodownSP = new GodownSP();
                GodownInfo = GodownSP.GodownView(strId);
                cmbMaterType.Text = "Go-down";
                txtName.Text = GodownInfo.GodownName;
                txtNarration.Text = GodownInfo.Narration;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC27:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillControlsPricingLevel()
        {
            try
            {
                //To fill selected Pricing Level details to controls
                //PricingLevelInfo PricingLevelInfo = new PricingLevelInfo();
                //PricingLevelSP PricingLevelSP = new PricingLevelSP();
                //PricingLevelInfo = PricingLevelSP.PricingLevelView(strId);
                //cmbMaterType.Text = "Pricing Level";
                //txtName.Text = PricingLevelInfo.PricingLevelName;
                //txtNarration.Text = PricingLevelInfo.Narration;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC28:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        public void FillControlsUnit()
        {
            try
            {
                //To fill selected Unit details to controls
                UnitInfo UnitInfo = new UnitInfo();
                UnitSP UnitSP = new UnitSP();
                UnitInfo = UnitSP.UnitView(strId);
                cmbMaterType.Text = "Unit";
                txtName.Text = UnitInfo.UnitName;
                txtNarration.Text = UnitInfo.Narration;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC29:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Delete Functions--- //

        public void DeleteFunction()
        {
            // Function to decide whether details from which table is to delete
            try
            {
                if (isAddOrModify)
                {
                    if (cmbMaterType.Text.ToString().Trim() != "--Select--" && cmbMaterType.Text.ToString().Trim() != "")
                    {
                        if (txtName.Text.Trim() != "")
                        {
                            if (strId != "")
                            {
                                if (InventorySettingsInfo._messageBoxDelete == true)
                                {
                                    if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Delete", PublicVariables._mainMenuItem) == true)//added on 19/10/2023 sheena
                                    {
                                        if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                        {
                                            DeleteSelect();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Delete", PublicVariables._mainMenuItem) == true)//added on 19/10/2023 sheena
                                    {
                                        DeleteSelect();
                                    }
                                    else
                                    {
                                        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC30:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DeleteSelect()
        {
            try
            {
                if (cmbMaterType.Text.ToString() == "Area")
                {
                    DeleteArea();
                }
                if (cmbMaterType.Text.ToString() == "Brand")
                {
                    DeleteBrand();
                }
                if (cmbMaterType.Text.ToString() == "Go-down")
                {
                    DeleteGodown();
                }
                if (cmbMaterType.Text.ToString() == "Pricing Level")
                {
                    DeletePricingLevel();
                }
                
                if (cmbMaterType.Text.ToString() == "Unit")
                {
                    DeleteUnit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC31:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DeleteArea()
        {
            try
            {
                //To delete from Area table if reference not exist in other tables
                AreaSP AreaSP = new AreaSP();
                bool isExist = AreaSP.AreaReferenceCheck(strId);
                if (!isExist)
                {
                    AreaSP.AreaDelete(strId);
                    MessageBox.Show("Deleted successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Clear();
                    ClearFunction();
                   
                }
                else
                {
                    MessageBox.Show("Can't delete, reference exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC32:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DeleteBrand()
        {
            try
            {
                //To delete from Brand table if reference not exist in other tables
                BrandSP BrandSP = new BrandSP();
                bool isExist = BrandSP.BrandReferenceCheck(strId);
                if (!isExist)
                {
                    BrandSP.BrandDelete(strId);
                    MessageBox.Show("Deleted successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Clear();
                    ClearFunction();
                 
                }
                else
                {
                    MessageBox.Show("Can't delete, reference exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC33:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void DeleteGodown()
        {
            try
            {
                //To delete from Godown table if reference not exist in other tables
                GodownSP GodownSP = new GodownSP();
                bool isExist = GodownSP.GodownReferenceCheck(strId);
                if (!isExist)
                {
                    GodownSP.GodownDelete(strId);
                    MessageBox.Show("Deleted successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Clear();
                    ClearFunction();
                   
                }
                else
                {
                    MessageBox.Show("Can't delete, reference exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC34:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DeletePricingLevel()
        {
            try
            {
                ////To delete from Pricing Level table if reference not exist in other tables
                //PricingLevelSP PricingLevelSP = new PricingLevelSP();
                //bool isExist = PricingLevelSP.PricingLevelReferenceCheck(strId);
                //if (!isExist)
                //{
                //    PricingLevelSP.PricingLevelDelete(strId);
                //    MessageBox.Show("Deleted successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtSearch.Clear();
                //    ClearFunction();
                    
                //}
                //else
                //{
                //    MessageBox.Show("Can't delete, reference exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC35:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        public void DeleteUnit()
        {
            try
            {
                //To delete from Unit table if reference not exist in other tables
                UnitSP UnitSP = new UnitSP();
                bool isExist = UnitSP.UnitReferenceCheck(strId);
                if (!isExist)
                {
                    UnitSP.UnitDelete(strId);
                    MessageBox.Show("Deleted successfully","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearch.Clear();
                    ClearFunction();
                 
                }
                else
                {
                    MessageBox.Show("Can't delete, reference exist","", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC36:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Call from Other Forms--- //

        public void CallFromMDI(MDIFinacPOS frm, string strForm)
        {
            //Function to call this form from MDI
            try
            {
                _MDIFinacAcount = frm;
                strCallFor = strForm;
                FormSettings(strCallFor);
                ActiveControl = txtName;
                base.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC37:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //public void DoWhenComingFromAccountLedgerForm(frmAccountLedger frm, string strForm)
        //{
        //    // Function to call from account ledger form to create new group

        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmaccount = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}

        public void DoWhenComingFromCustomerForm(frmCustomer frm, string strForm)
        {
            // Function to call from account ledger form to create new group

            gbxSearch.Enabled = false;
            dgvRegister.Enabled = false;
            btnDelete.Enabled = false;
            this.frmcust = frm;
            strCallFor = strForm;
            FormSettings(strCallFor);
            ActiveControl = txtName;
            isClose = true;
            base.Show();
        }

        //frmSalesMan frmSalesMan;
        //public void DoWhenComingFromSalesManForm(frmSalesMan frm, string strForm)
        //{
        //    // Function to call from sales man form to create new area

        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmSalesMan = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}

        //frmEmployeeCreation frmEmployeeCreation;
        //public void DoWhenComingEmployeeCreationForm(frmEmployeeCreation frm, string strForm)
        //{
        //    // Function to call from employee creation form to create new area

        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmEmployeeCreation = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}

        //frmRack frmRack;
        //public void DoWhenComingFromRackForm(frmRack frm, string strForm)
        //{
        //    // Function to call from account ledger form to create new group

        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmRack = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}
        //public void DoWhenComingFromSalesOrderForm(frmSalesOrder frm, string strForm)
        //{
        //    // Function to call from sales order form to create new pricing level

        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmsalesorder = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}
        //public void DoWhenComingFromProformaInvoiceForm(frmProformaInvoice frm, string strForm)
        //{
        //    // Function to call from sales order form to create new pricing level

        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmProformaInvoice = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}

        //frmSalesQuotation frmquotation;
        //public void DoWhenComingFromQuotationForm(frmSalesQuotation frm, string strForm)
        //{
        //    // Function to call from sales order form to create new pricing level

        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmquotation = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}
        public void DoWhenComingFromProductCreationForm(frmProductCreation frm, string strForm)
        {

            // Function to call from account ledger form to create new product group,brand,unit etc
            base.Show();
            gbxSearch.Enabled = false;
            dgvRegister.Enabled = false;
            btnDelete.Enabled = false;
            this.frmproductcreation = frm;
            strCallFor = strForm;
            FormSettings(strCallFor);
            ActiveControl = txtName;
            isClose = true;

        }

        //frmMultipleProductCreation frmMultiplePr;
        //public void DoWhenComingFromMultipleProductCreationForm(frmMultipleProductCreation  frmMultiple, string strForm)
        //{

        //    // Function to call from account ledger form to create new product group,brand,unit etc
        //    base.Show();
        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmMultiplePr = frmMultiple;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;

        //}
        //public void DoWhenComingFromMarketCreationForm(frmMarket frm, string strForm)
        //{
        //    // Function to call from account ledger form to create new group
        //    gbxSearch.Enabled = false;
        //    dgvRegister.Enabled = false;
        //    btnDelete.Enabled = false;
        //    this.frmmarket = frm;
        //    strCallFor = strForm;
        //    FormSettings(strCallFor);
        //    ActiveControl = txtName;
        //    isClose = true;
        //    base.Show();
        //}
        public void DoWhenQuitingForm()
        {
            //    // Function to execute at the time of closing the form
            //    // To return to the form rom which this form is called
            //    if (frmaccount != null)
            //    {
            //        frmaccount.Enabled = true;
            //        frmaccount.DowhenReturningFromMasterCreationForm(strNewId, strCallFor);
            //    }
            //    else if (frmmarket != null)
            //    {
            //        frmmarket.Enabled = true;
            //        frmmarket.DowhenReturningFromMasterCreationForm(strNewId);
            //    }
            //    else
            if (frmcust != null)
            {
                frmcust.Enabled = true;
                frmcust.DowhenReturningFromMasterCreationForm(strNewId, strCallFor);
            }
            //    else if (frmsalesorder != null)
            //    {
            //        frmsalesorder.Enabled = true;
            //        frmsalesorder.DowhenReturningFromMasterCreationForm(strNewId);
            //    }

            //    else if (frmquotation != null)
            //    {
            //        frmquotation.Enabled = true;
            //        frmquotation.DowhenReturningFromMasterCreationForm(strNewId);
            //    }
            else if (frmproductcreation != null)
            {

                frmproductcreation.Enabled = true;
                frmproductcreation.DowhenReturningFromMasterCreationForm(strNewId, strCallFor);
            }
            //    else if (frmMultiplePr != null)
            //    {
            //        frmMultiplePr.Enabled = true;
            //        frmMultiplePr.DowhenReturningFromMasterCreationForm(strNewId, strCallFor);
            //    }
            //    else if (frmRack != null)
            //    {
            //        frmRack.Enabled = true;
            //        frmRack.DowhenReturningFromMasterCreationForm(strNewId);
            //    }
            //    else if (frmSalesMan != null)
            //    {
            //        frmSalesMan.Enabled = true;
            //        frmSalesMan.DowhenReturningFromMasterCreationForm(strNewId);
            //    }
            //    else if (frmEmployeeCreation != null)
            //    {
            //        frmEmployeeCreation.Enabled = true;
            //        frmEmployeeCreation.DowhenReturningFromMasterCreationForm(strNewId);
            //    }

        }
        //--- Form settings --- //

        public void FormSettings(string strName)
        {
            //Function to set form according to form calling
            try
            {
                cmbMaterType.Text = strName;
                cmbMaterType.DropDownStyle = ComboBoxStyle.Simple;
                cmbMaterType.Visible = false;
                lblMandatoryOne.Visible = false;
                lblMasterType.Visible = false;
                this.Text = strName;
                cmbSearchType.Visible = false;
                lblMasterTypeSearch.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC38:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Events

        //---Grid Events ---//

        private void dgvRegister_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Edit",PublicVariables._mainMenuItem) == true)
                {
                    isEditEnable = true;
                    btnSave.Enabled = true;
                }
                else
                {
                    isEditEnable = false;
                    btnSave.Enabled = false;
                }


               
                //----------- To show details corresponding to customer Id-------//
                if (dgvRegister.RowCount > 0 && e.RowIndex > -1)
                {
                    if (dgvRegister.CurrentRow != null)
                    {
                        if (cmbSearchType.Text.Trim() != "")
                        {
                            strId = dgvRegister.CurrentRow.Cells[0].Value.ToString();
                            if (cmbSearchType.SelectedItem.ToString() == "Area")
                            {
                                FillControlsArea();
                            }
                            if (cmbSearchType.SelectedItem.ToString() == "Brand")
                            {
                                FillControlsBrand();
                            }
                            if (cmbSearchType.SelectedItem.ToString() == "Go-down")
                            {
                                FillControlsGodown();
                            }
                            if (cmbSearchType.SelectedItem.ToString() == "Pricing Level")
                            {
                                FillControlsPricingLevel();
                            }
                            
                            if (cmbSearchType.SelectedItem.ToString() == "Unit")
                            {
                                FillControlsUnit();
                            }
                        }
                        btnSave.Text = "Update";
                        btnClear.Text = "New";
                       // btnDelete.Enabled = true;
                         if (isDeleteEnable)//added 19/10/2023
                    btnDelete.Enabled = true;
                else
                    btnDelete.Enabled = false;
                        isAddOrModify = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC39:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dgvRegister_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                // To clear the grid selection after data biniding
                if (txtSearch.Text == "")
                {
                    ((DataGridView)sender).CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC40:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dgvRegister_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // To execute same function of cell click when cliking the header also
                DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex);
                dgvRegister_CellClick(sender, e1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC41:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void dgvRegister_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // To execute same function of cell click when up, down, enter, tab key is pressed
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
                MessageBox.Show("MC42:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Form Events ---//

        private void frmMasterCreation_Load(object sender, EventArgs e)
        {
            // Initial settings at the time of form load
            try
            {
                if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Delete", PublicVariables._mainMenuItem) == true)
                {
                    isDeleteEnable = true;
                    btnDelete.Enabled = isDeleteEnable;
                }
                else
                {
                    isDeleteEnable = false;
                    btnDelete.Enabled = isDeleteEnable;

                }
                CreateDatasource();
                if (strCallFor == "")
                {
                    cmbMaterType.SelectedIndex = 0;
                    cmbMaterType.Focus();
                }
                else
                {
                    cmbMaterType.Text = strCallFor;
                }

                bwrkControlSettings.RunWorkerAsync();
                //frmCompanyProgress.ShowInTaskbar = false;
                //frmCompanyProgress.ShowFromReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC43:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmMasterCreation_KeyDown(object sender, KeyEventArgs e)
        {
            // close form if press Escape key
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (InventorySettingsInfo._messageBoxClose == true)
                    {
                        if (MessageBox.Show("Do you want to close?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                if (e.Control && e.KeyCode == Keys.S)
                {
                    SaveFunction();
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    DeleteFunction();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC44:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void frmMasterCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            //To navigate to the corresponding forms
            try
            {
                DoWhenQuitingForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC45:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Selected Index change ---//

        private void cmbMaterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                strMasterText = cmbMaterType.Text;
                if (cmbMaterType.Text.ToString() == "--Select--")
                {
                    cmbSearchType.SelectedItem = "--Select--";
                }
                if (cmbMaterType.Text.ToString() == "Area")
                {
                    cmbSearchType.SelectedItem = "Area";
                }
                if (cmbMaterType.Text.ToString() == "Brand")
                {
                    cmbSearchType.SelectedItem = "Brand";
                }
                if (cmbMaterType.Text.ToString() == "Go-down")
                {
                    cmbSearchType.SelectedItem = "Go-down";
                }
                if (cmbMaterType.Text.ToString() == "Pricing Level")
                {
                    cmbSearchType.SelectedItem = "Pricing Level";
                }
                if (cmbMaterType.Text.ToString() == "Product Group")
                {
                    cmbSearchType.SelectedItem = "Product Group";
                }
                if (cmbMaterType.Text.ToString() == "Unit")
                {
                    cmbSearchType.SelectedItem = "Unit";
                }
                ClearFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC46:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void cmbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                strSearchText = cmbSearchType.Text;
                Control c = (Control)sender;
                FillRegister();
              
                if (dgvRegister.CurrentRow != null)
                {
                    DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(dgvRegister.CurrentCell.ColumnIndex, dgvRegister.CurrentCell.RowIndex);
                    dgvRegister_CellClick(sender, ex);
                }
                else
                {
                    txtName.Clear();
                    txtNarration.Clear();
                    isAddOrModify = false;
                    btnSave.Text = "Save";
                    btnClear.Text = "Clear";
                    btnDelete.Enabled = false;
                    if (c is TextBox)

                        txtSearch.Focus();
                }
                if (dgvRegister.CurrentRow != null)
                {
                    dgvRegister.CurrentRow.Selected = true;
                    dgvRegister.Focus();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("MC47:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---Other Events ---//

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
          
            
        }
        private void txtNarration_Enter(object sender, EventArgs e)
        {
            try
            {
                i = 0;
                txtNarration.Text = txtNarration.Text.Trim();
                if (txtNarration.Text == "")
                {
                    txtNarration.SelectionStart = 0;
                    txtNarration.Focus();
                }
                else
                {
                    txtNarration.SelectionStart = txtNarration.Text.Length;
                    txtNarration.Focus();
                }
                label9.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC48:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void cmbMaterType_KeyPress(object sender, KeyPressEventArgs e)
        {
            // To make combo as readonly
            try
            {
                if (strCallFor != "")
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC49:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void cmbSearchType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // To focus to txtSearch text box on pressing enter key
                if (e.KeyCode == Keys.Enter)
                {
                    cmbSearchType.Text = strSearchText;
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC50:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string strMasterText = "";
        private void cmbMaterType_KeyUp(object sender, KeyEventArgs e)
        {
            strMasterText = cmbMaterType.Text;
        }
        private void cmbMaterType_Leave(object sender, EventArgs e)
        {
            if (cmbMaterType.SelectedIndex == -1)
                cmbMaterType.Text = strMasterText;
            lblMasterType.ForeColor = Color.Black;
        }

        //---Button Click ---//

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC51:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteFunction();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC52:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Add", PublicVariables._mainMenuItem) == true)
                {
                    isEditEnable = true;
                    btnSave.Enabled = isEditEnable;
                }
                else
                {
                    isEditEnable = false;
                    btnSave.Enabled = isEditEnable;

                }
                if (strCallFor == "")
                {
                    CreateDatasource();
                    cmbMaterType.SelectedIndex = 0;
                }
                txtSearch.Clear();
                ClearFunction();
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC53:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (InventorySettingsInfo._messageBoxClose == true)
                {
                    if (MessageBox.Show("Do you want to close?","", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
                MessageBox.Show("MC54:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string strSearchText = "";
        private void cmbSearchType_KeyUp(object sender, KeyEventArgs e)
        {
            strSearchText = cmbSearchType.Text;
        }
        private void cmbSearchType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbSearchType.SelectedIndex == -1)
                    cmbSearchType.Text = strSearchText;
               lblMasterTypeSearch.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                MessageBox.Show("MC55:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("MC56:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        private void cmbMaterType_Enter(object sender, EventArgs e)
        {
            strMasterText = cmbMaterType.Text;
            lblMasterType.ForeColor = Color.Red;
        }

        private void cmbSearchType_Enter(object sender, EventArgs e)
        {
            strSearchText = cmbSearchType.Text;
            lblMasterTypeSearch.ForeColor = Color.Red;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

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

        private void txtName_Enter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.Black;
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
        }
    }
}