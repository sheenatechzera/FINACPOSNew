using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace FinacPOS
{
    // This class is used to write common functions used iin reports
    class ReportCommonFunctions:DBConnection 
    {
        public void InitialSEttings(Label lblName,Label lblAddress, Label lblPhone,ComboBox cmbBranch,Label lblBranch,string strReportName,bool isAll,string status)
        {
            // For loading company details in all reports
            try
            {
                BranchInfo InfoBranch = new BranchInfo();
                BranchSP SpBranch = new BranchSP();
                InfoBranch = SpBranch.BranchView(PublicVariables._branchId);
                lblName.Text = InfoBranch.BranchName;
                if (InfoBranch.PhoneNo != "")
                {
                    lblPhone.Text = InfoBranch.PhoneNo;
                }
                else
                {
                    lblPhone.Text = InfoBranch.Mobile ;
                }
                lblAddress.Text = InfoBranch.Address.Replace("\r\n", " ");
                if (cmbBranch != null)
                {
                    BranchSettings(cmbBranch, lblBranch, strReportName, isAll, status);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CR1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
      
        public void BranchSettings(ComboBox cmbBranch, Label lblBranch, string strPrivilegeName, bool isAll, string status)
        {
            // Filling branch combo
            try
            {
                BranchSP SpBranch = new BranchSP();
                DataTable dtbl = new DataTable();
                //if (PublicVariables._currentUserId == "1")
                //{
                    dtbl = SpBranch.BranchViewAll("");

                //}
                //else
                //{
                //    dtbl = SpBranch.BranchViewAllAccordingToPrivilege(strPrivilegeName);
                //}
                dtbl = dtbl.DefaultView.ToTable(true);


                if (status == "Expiry")
                {
                    // checkign the short expiry settings of btanch
                    for (int i = 0; i < dtbl.Rows.Count; ++i)
                    {
                        string branchId = dtbl.Rows[i]["branchId"].ToString();
                        SettingsInfo InfoSettings = new SettingsInfo();
                        SettingsSP SpSettings = new SettingsSP();
                        InfoSettings = SpSettings.SettingsViewByBranchId(branchId);
                        bool isOk = false;
                        if (InfoSettings.MaintainBatch)
                        {
                            string[] p = InfoSettings.ExpiryReminder.Split('+');
                            if (p[2].ToString() == "Y")
                            {
                                isOk = true;
                            }
                        }
                        if (!isOk)
                        {
                            dtbl.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else if (status == "Godown")
                {
                    // checkign the short expiry settings of btanch
                    for (int i = 0; i < dtbl.Rows.Count; ++i)
                    {
                        string branchId = dtbl.Rows[i]["branchId"].ToString();
                        SettingsInfo InfoSettings = new SettingsInfo();
                        SettingsSP SpSettings = new SettingsSP();
                        InfoSettings = SpSettings.SettingsViewByBranchId(branchId);
                  
                        if (!InfoSettings.MaintainGodown)
                        {
                            dtbl.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else if (status == "Tax")
                {
                    // checkign the short expiry settings of btanch
                    for (int i = 0; i < dtbl.Rows.Count; ++i)
                    {
                        string branchId = dtbl.Rows[i]["branchId"].ToString();
                        SettingsInfo InfoSettings = new SettingsInfo();
                        SettingsSP SpSettings = new SettingsSP();
                        InfoSettings = SpSettings.SettingsViewByBranchId(branchId);
                        FinanceSettingsInfo InfoFinace = new FinanceSettingsInfo();
                        InventorySettingsSP spInventory = new InventorySettingsSP();
                        InfoFinace = spInventory.FinanceSettingsViewAll(branchId);
                        //if (!InfoSettings.Tax)
                        if (!InfoFinace.ActivateTax)
                        {
                            dtbl.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else if (status == "Budget")
                {
                    // checkign the short expiry settings of btanch
                    for (int i = 0; i < dtbl.Rows.Count; ++i)
                    {
                        string branchId = dtbl.Rows[i]["branchId"].ToString();
                        SettingsInfo InfoSettings = new SettingsInfo();
                        SettingsSP SpSettings = new SettingsSP();
                        InfoSettings = SpSettings.SettingsViewByBranchId(branchId);
                 
                        if (!InfoSettings.Budget)
                        {
                            dtbl.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                }
                else if (status == "PayRoll")
                {
                    // checkign the short expiry settings of btanch
                    for (int i = 0; i < dtbl.Rows.Count; ++i)
                    {
                        string branchId = dtbl.Rows[i]["branchId"].ToString();
                        SettingsInfo InfoSettings = new SettingsInfo();
                        SettingsSP SpSettings = new SettingsSP();
                        InfoSettings = SpSettings.SettingsViewByBranchId(branchId);
                      
                        if (!InfoSettings.PayRoll)
                        {
                            dtbl.Rows.RemoveAt(i);
                            i--;
                        }
                    }

                }

                DataRow dr = dtbl.NewRow();
                dr["branchId"] = "All";
                dr["branchName"] = "All";
                dtbl.Rows.Add(dr);

                cmbBranch.DataSource = dtbl;
                cmbBranch.ValueMember = "branchId";
                cmbBranch.DisplayMember = "branchName";


                if (dtbl.Select("branchId" + " = '" + PublicVariables._branchId + "'").Length > 0)
                {
                    cmbBranch.SelectedValue = PublicVariables._branchId;

                }
                // Hide /Show branch combo according to settings
                PrimaryDBSP SpPrimary = new PrimaryDBSP();
                CompanyPathInfo InfoPath = new CompanyPathInfo();
                InfoPath = SpPrimary.CompanyPathView(PublicVariables._companyId);
                if (InfoPath.BranchEnabled)
                {
                    cmbBranch.Visible = true;
                    lblBranch.Visible = true;
                }
                else
                {
                    cmbBranch.Visible = false;
                    lblBranch.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CR2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillCurrencyPRoperToBranch(ComboBox cmb, string branchId)
        {
            try
            {
                CurrencySP SpCurrency = new CurrencySP();
                DataTable dtbl = new DataTable();
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("CurrencyViewAllForBranch", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sdaadapter.Fill(dtbl);
                cmb.DataSource = dtbl;
                cmb.ValueMember = "currencyId";
                cmb.DisplayMember = "currencySymbol";
                BranchInfo InfoBrnach = new BranchInfo();
                BranchSP SpBracnh = new BranchSP();
                InfoBrnach = SpBracnh.BranchView(branchId);
                cmb.SelectedValue = InfoBrnach.CurrencyId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("CR3:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillCostCentreComboReport(ComboBox cmbCostCentre)
        {
            // To fill CostCentre combo
            try
            {
                //// to fill bank comob
                //cmbCostCentre.DataSource = null;
                //CostCentreSP spcostCentre = new CostCentreSP();
                //DataTable dtbl = new DataTable();
                //dtbl = spcostCentre.CostCentreGetAll();
                //cmbCostCentre.DataSource = dtbl;

                //if (dtbl.Rows.Count > 0)
                //{
                //    cmbCostCentre.DisplayMember = "costCentreName";
                //    cmbCostCentre.ValueMember = "costCentreId";

                //    DataRow dr1 = dtbl.NewRow();
                //    dr1["costCentreName"] = "All";
                //    dr1["costCentreId"] = "All";
                //    dtbl.Rows.InsertAt(dr1, 0);
                //    cmbCostCentre.SelectedValue = "All";

                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show("RC05:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillTransactionBatchComboReport(ComboBox cmb, string strVoucherType)
        {
            // To fill CostCentre combo
            try
            {
                // to fill bank comob
                cmb.DataSource = null;
                //TransactionBatchSP spBatch = new TransactionBatchSP();
                //DataTable dtbl = new DataTable();
                //dtbl = spBatch.TransactionBatchViewByVoucherType(strVoucherType);
                //cmb.DataSource = dtbl;

                //if (dtbl.Rows.Count > 0)
                //{
                //    cmb.DisplayMember = "BatchName";
                //    cmb.ValueMember = "BatchId";

                //    DataRow dr1 = dtbl.NewRow();
                //    dr1["BatchName"] = "All";
                //    dr1["BatchId"] = "All";
                //    dtbl.Rows.InsertAt(dr1, 0);
                //    cmb.SelectedValue = "All";

                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show("RC05:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
