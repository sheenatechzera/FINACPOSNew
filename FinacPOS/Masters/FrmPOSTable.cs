using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinacPOS
{
    public partial class FrmPOSTable : Form
    {
        public FrmPOSTable()
        {
            InitializeComponent();
        }
        int TableId;
        POSTableSp tableSP = new POSTableSp();
        POSTableInfo tableInfo = new POSTableInfo();
        ComboValidation objComboValidation = new ComboValidation();
        AutoCompleteStringCollection GroupName = new AutoCompleteStringCollection();
        private void BtnSave_Click(object sender, EventArgs e)
        {

            try
            {

                SaveOrEdit();

            }
            catch (Exception ex)
            {
                MessageBox.Show("P6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

         private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CR2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CR3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
              
            }
        }
        public void SaveOrEdit()
        {
            
            try
            {
                if (txtTblNo.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Enter TableNo", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTblNo.Focus();
                }
                else if (txtNoOfSeats.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Enter a valid number of seats", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNoOfSeats.Focus();
                }
                else if (txtGroup.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("Enter Group Name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtGroup.Focus();
                }
                else
                {
                    POSTableInfo infoPOSTable = new POSTableInfo();
                    infoPOSTable.TableId = Int32.Parse(TableId.ToString());
                    infoPOSTable.TableNo = txtTblNo.Text;
                    infoPOSTable.NumberOfSeats = Int32.Parse(txtNoOfSeats.Text);
                    infoPOSTable.GroupName = txtGroup.Text;
                    infoPOSTable.Active = ChkActive.Checked;

                    if (BtnSave.Text == "Save")
                    {
                        if (MessageBox.Show("Do you want to Save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (tableSP.TableNoCheckExistence(infoPOSTable.TableId, txtTblNo.Text.Trim()) == false)
                            {

                                TableId = tableSP.POSTableAdd(tableInfo);

                                MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();

                            }
                            else
                            {
                                MessageBox.Show("TableNo already exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtTblNo.Focus();
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Do you want to update?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (tableSP.TableNoCheckExistence(infoPOSTable.TableId, txtTblNo.Text.Trim()) == false)
                            {
                                tableInfo.TableId = Int32.Parse(TableId.ToString());
                                if (tableSP.POSTableEdit(tableInfo))
                                {
                                    MessageBox.Show("Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Clear();
                                }
                            }
                            else
                            {
                                MessageBox.Show("TableNo already exists!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtTblNo.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("P3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
     public void Delete()
      {
     try
      {
        if (MessageBox.Show("Do you want to Delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            int result = tableSP.POSTableDelete(TableId);

            if (result > 0) 
            {
                MessageBox.Show("Deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
            }
            else
            {
                MessageBox.Show("Cannot delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
     }
    catch (Exception ex)
    {
        MessageBox.Show("Error in deletion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }


        public void Clear()
        {
            try
            {
                txtTblNo.Text = string.Empty;
                txtNoOfSeats.Text = string.Empty;
                txtGroup.Text = string.Empty;
                ChkActive.Checked = true;
                txtTblNo.Focus();
                BtnSave.Text = "Save";
                btnClear.Text = "Clear";
                btnDelete.Enabled = false;
                FillGroupName();
            }
            catch (Exception ex)
            {
                MessageBox.Show("CR54:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                frmLookup frmlookup = new frmLookup();
                frmlookup.strSearchingName = "POSTable";
                frmlookup.strFromFormName = "POSTable";
                frmlookup.strSearchColumn = "TableNo";
                frmlookup.strSearchOrder = " TableId DESC ";
                frmlookup.strSearchQry = "TableId,TableNo,NumberOfSeats,GroupName";
                
               
                frmlookup.strSearchTable = " tbl_POSTable ";
               
                frmlookup.strMasterIdColumnName = "TableId";
                 frmlookup.IntSearchFiledCount = 4;

               frmlookup.DoWhenComingFromPOSTableForm(this);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DowhenReturningFromSearchForm(string strId)
        {
            try
            {
                this.Enabled = true;
                if (strId != "")
                {
                    
                    TableId = Int32.Parse(strId);
                    FillTableDetailsForEdit();
                }

                txtTblNo.Focus();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FillTableDetailsForEdit()
        {
            try
            {
              
                POSTableSp tablesp = new POSTableSp();
                POSTableInfo tableInfo = tablesp.GetPOSTableDetails(TableId);

                  
                    txtTblNo.Text = tableInfo.TableNo;
                    txtNoOfSeats.Text = tableInfo.NumberOfSeats.ToString();
                    txtGroup.Text = tableInfo.GroupName;
                    ChkActive.Checked = tableInfo.Active;

                    BtnSave.Text = "Update";
                    btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error in FillTableDetails: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void FillGroupName()
        {
            try
            {
                DataTable Dtbl = new DataTable();
                POSTableSp tablespObj = new POSTableSp();
                Dtbl = tablespObj.GroupNameFill();
                GroupName = new AutoCompleteStringCollection();
                foreach (DataRow dr in Dtbl.Rows)
                {
                    GroupName.Add(dr["GroupName"].ToString());
                }
                txtGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtGroup.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtGroup.AutoCompleteCustomSource = GroupName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("FM2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTblNo_Enter(object sender, EventArgs e)
        {
            lblTblNo.ForeColor = System.Drawing.Color.Red;
        }

        private void txtTblNo_Leave(object sender, EventArgs e)
        {
            lblTblNo.ForeColor = System.Drawing.Color.Black;
        }

        private void txtTblNo_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
            {

                txtNoOfSeats.Focus();
            }
        }

        private void txtNoOfSeats_Enter(object sender, EventArgs e)
        {
            lblNoOfSeats.ForeColor = System.Drawing.Color.Red;
        }

        private void txtNoOfSeats_Leave(object sender, EventArgs e)
        {
            lblNoOfSeats.ForeColor = System.Drawing.Color.Black;
        }

        private void txtNoOfSeats_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtGroup.Focus();
            }

        }

        private void txtGroup_Enter(object sender, EventArgs e)
        {
            lblGroup.ForeColor = System.Drawing.Color.Red;
        }

        private void txtGroup_Leave(object sender, EventArgs e)
        {
            lblGroup.ForeColor = System.Drawing.Color.Black;
        }

        private void txtGroup_KeyDown(object sender, KeyEventArgs e)
        {
           
             if (e.KeyCode == Keys.Enter)
            {

                ChkActive.Focus();
            }

        }

      
        private void ChkActive_Enter(object sender, EventArgs e)
        {
          
           ChkActive.ForeColor = System.Drawing.Color.Red;
        }

        private void ChkActive_KeyDown(object sender, KeyEventArgs e)
        {

             if (e.KeyCode == Keys.Enter)
            {

                BtnSave.Focus();
            }
        }

        private void ChkActive_Leave(object sender, EventArgs e)
        {

            ChkActive.ForeColor = System.Drawing.Color.Black;
        }

        private void FrmPOSTable_Load(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("P10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNoOfSeats_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }
    }
}
