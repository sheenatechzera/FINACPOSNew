using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    partial class frmPOSUserSettings : Form
    {
        public frmPOSUserSettings()
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

        //  MDIForm frmQuick = new MDIForm();

        DataTable dtblViewAllSelectedCustomization = new DataTable();
        DataTable dtblViewAllNonSelectedCustomization = new DataTable();
        POSUserSettingsSP SPUserSettings = new POSUserSettingsSP();
        POSUserSettingsInfo InfoPOSUserSettings = new POSUserSettingsInfo();
     

        
        private void frmPOSUserSettings_Load(object sender, EventArgs e)
        {
            try
            {
                clsGeneral objGeneral = new clsGeneral();
                objGeneral.formSettings(this);
                FillUserGroup();
                //cmbUSerGroup.SelectedIndex = 0;
                clear(); // clear all the datas
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public void Showss(MDIForm frm)
        //{
        //    try
        //    {
        //        frmQuick = frm;
        //        base.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        private void filldata()// for list all the selected and nonselected form names in the listboxs
        {

            try
            {
               
                lstbSelsected.DataSource = dtblViewAllSelectedCustomization;
                lstbSelsected.DisplayMember = "menuStripName";
                lstbSelsected.ValueMember = "settingsId";

                lstbNonSelsected.DataSource = dtblViewAllNonSelectedCustomization;
                lstbNonSelsected.DisplayMember = "menuStripName";
                lstbNonSelsected.ValueMember = "settingsCopyId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Update all the details 
                //if (lstbSelsected.Items.Count == 8)
                //{
                    int inOrder = 0;
                    SPUserSettings.POSUserSettingsDelete(cmbUSerGroup.SelectedValue.ToString());
                    foreach (DataRow dRoW in dtblViewAllSelectedCustomization.Rows) 
                    {
                        inOrder++;
                        InfoPOSUserSettings.UserGroup = cmbUSerGroup.SelectedValue.ToString();
                        InfoPOSUserSettings.Menu = dRoW.ItemArray[2].ToString();
                        InfoPOSUserSettings.SubMenu = dRoW.ItemArray[3].ToString();
                        InfoPOSUserSettings.MenuStripName = dRoW.ItemArray[4].ToString();
                        InfoPOSUserSettings.FormName = dRoW.ItemArray[5].ToString();
                        InfoPOSUserSettings.Status = true;
                        InfoPOSUserSettings.Extra1 = inOrder.ToString();
                        InfoPOSUserSettings.Extra2 = inOrder.ToString();
                        SPUserSettings.POSUserSettingsAdd(InfoPOSUserSettings);
                       
                    }
                  
                   


                    clear();
                    this.Close();
                   
                //}
                //else
                //{
                //    MessageBox.Show("Select eight items for quick launch menus", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        
        string _CustomizedIdSel = "";
      

        private void lstbSelsected_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstbSelsected.SelectedValue != null)
                {
                    _CustomizedIdSel = lstbSelsected.SelectedValue.ToString(); // take the selected value for move "the names" to the next listbox
                    _CustomizedIdNonSel = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void SortDataTable()
        {
            DataSet dst = new DataSet();
            dst.Merge(dtblViewAllNonSelectedCustomization.Select("", "menuStripName asc"));
            dtblViewAllNonSelectedCustomization = dst.Tables[0];
            lstbNonSelsected.DataSource = dtblViewAllNonSelectedCustomization;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (DataRowView li in lstbNonSelsected.SelectedItems)
                {
                    DataRow dr;
                    dr = dtblViewAllSelectedCustomization.NewRow();
                    dr[0] = li.Row.ItemArray[0].ToString();
                    dr[2] = li.Row.ItemArray[1].ToString();
                    dr[3] = li.Row.ItemArray[2].ToString();
                    dr[4] = li.Row.ItemArray[3].ToString();
                    dr[5] = li.Row.ItemArray[4].ToString();
                    dr[6] = li.Row.ItemArray[5].ToString();
                    dtblViewAllSelectedCustomization.Rows.Add(dr);
                }

                int[] SelIndex = new int[20];
                int inIndex = 0;
                for (int i = 0; i < lstbNonSelsected.Items.Count; i++)
                {
                    if (lstbNonSelsected.GetSelected(i))
                    {
                        SelIndex[inIndex] = i;
                        inIndex++;

                    }
                }
                for (int inK = 0; inK < inIndex; inK++)
                {
                    dtblViewAllNonSelectedCustomization.Rows.RemoveAt(SelIndex[inK] - inK);
                }


                lstbNonSelsected.SelectedIndex = -1;
                lstbSelsected.SelectedIndex = -1;
                lstbNonSelsected.ClearSelected();
                lstbSelsected.ClearSelected();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        string _CustomizedIdNonSel = "";
    
        private void lstbNonSelsected_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstbNonSelsected.SelectedValue != null)
                {
                    _CustomizedIdNonSel = lstbNonSelsected.SelectedValue.ToString();// take the selected value for move "the names" to the next listbox
                    _CustomizedIdSel = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNonSelect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(DataRowView  li in lstbSelsected.SelectedItems)
                {
                    DataRow dr;
                            dr = dtblViewAllNonSelectedCustomization.NewRow();
                            dr[0] = li.Row.ItemArray[0].ToString();
                            dr[1] = li.Row.ItemArray[2].ToString();
                            dr[2] = li.Row.ItemArray[3].ToString();
                            dr[3] = li.Row.ItemArray[4].ToString();
                           
                            dtblViewAllNonSelectedCustomization.Rows.Add(dr);
                          
                }
                SortDataTable();
               
                int[] SelIndex = new int[20];
                int inIndex = 0;
                for (int i = 0; i < lstbSelsected.Items.Count; i++)
                {
                    if (lstbSelsected.GetSelected(i))
                    {
                        SelIndex[inIndex] = i;
                        inIndex++;
                      
                    }
                }
                for (int inK = 0; inK < inIndex; inK++)
                {
                    dtblViewAllSelectedCustomization.Rows.RemoveAt((SelIndex[inK] - inK));
                }
              
                lstbNonSelsected.SelectedIndex = -1;
                lstbSelsected.SelectedIndex = -1;
                lstbNonSelsected.ClearSelected();
                lstbSelsected.ClearSelected();

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                FillUserGroup();
                clear();
           _CustomizedIdSel = "";
            _CustomizedIdNonSel = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clear() // clear all the details in the form
        {
            try
            {
                if (cmbUSerGroup.DataSource != null)
                {
                    string userGroup = cmbUSerGroup.SelectedValue.ToString();
                    dtblViewAllNonSelectedCustomization.Rows.Clear();
                    dtblViewAllSelectedCustomization.Rows.Clear();
                    //selected
                    dtblViewAllSelectedCustomization = SPUserSettings.POSUserSettingsViewByUserGroup(userGroup);

                    //not selected
                    dtblViewAllNonSelectedCustomization = SPUserSettings.POSUserSettingsCopyViewAll(userGroup);
                    if (dtblViewAllNonSelectedCustomization.Rows.Count > 0)
                    {
                        SortDataTable();

                    }
                    filldata();
                    lstbNonSelsected.SelectedIndex = -1;
                    lstbSelsected.SelectedIndex = -1;
                }
                else
                    MessageBox.Show("Please add usergroup", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //private void btntest_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        frmIconImageSettings frmObj = new frmIconImageSettings();
        //        frmIconImageSettings open = Application.OpenForms["frmIconImageSettings"] as frmIconImageSettings;
        //        if (open == null)
        //        {

        //            frmObj.MdiParent = MDIFinacAcount.MDIObj;
        //            frmObj.Show();
        //        }
        //        else
        //        {
        //            frmObj.MdiParent = MDIFinacAcount.MDIObj ;
        //            open.Activate();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void frmPOSUserSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                
            //    frmQuick.Enabled = true;
           //     frmQuick.AssignAndEnableMenuItems();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (SettingsInfo._messageBoxClose == true)
                {
                    if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    else
                    {
                        _CustomizedIdSel = "";
                        _CustomizedIdNonSel = "";
                    }
                }
                else
                {
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstbSelsected_Leave(object sender, EventArgs e)
        {
            
        }

        private void lstbNonSelsected_Leave(object sender, EventArgs e)
        {
           
        }

      
        private void lstbSelsected_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                {
                    _CustomizedIdSel = lstbSelsected.SelectedValue.ToString();// take the selected value for move "the names" to the next listbox
                    _CustomizedIdNonSel = "";



                }
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                {
                    _CustomizedIdSel = lstbSelsected.SelectedValue.ToString();// take the selected value for move "the names" to the next listbox
                    _CustomizedIdNonSel = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstbNonSelsected_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                {
                    _CustomizedIdNonSel = lstbNonSelsected.SelectedValue.ToString();// take the selected value for move "the names" to the next listbox
                    _CustomizedIdSel = "";
                }
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                {
                    _CustomizedIdNonSel = lstbNonSelsected.SelectedValue.ToString();// take the selected value for move "the names" to the next listbox
                    _CustomizedIdSel = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void frmPOSUserSettings_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (SettingsInfo._messageBoxClose)
                    {
                        if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            this.Close();
                        }
                        else
                        {
                            _CustomizedIdSel = "";
                            _CustomizedIdNonSel = "";
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else if (e.Control && e.KeyCode == Keys.S)
                {
                    btnSave_Click(btnSave, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbUSerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear();
        }
        private void FillUserGroup()
        {
            DataTable dt = SPUserSettings.POSUserGetUserGroupId();

            cmbUSerGroup.ValueMember = dt.Columns[0].ToString();
            cmbUSerGroup.DisplayMember = dt.Columns[0].ToString();
            if (dt.Rows.Count > 0)
            {
                cmbUSerGroup.DataSource = dt;
            }
          

            //cmbUSerGroup.DataSource=  SPUserSettings.POSUserGetUserGroupId();
            //if (cmbUSerGroup.DataSource != null)
            //{
            //    cmbUSerGroup.DisplayMember = "POSUserGroupId";
            //    cmbUSerGroup.ValueMember = "POSUserGroupId";

            //    cmbUSerGroup.SelectedIndex = -1;
            //}
        }

      
    }
}