using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
namespace FinacPOS
{
    public class ComboBoxItem
    {
        public string currencyId { get; set; }
        public string currencyName { get; set; }
        public string currencySymbol { get; set; }
        public string currecyConversionId { get; set; }
        public decimal rate { get; set; }
        public DateTime date { get; set; }
    }
    class TransactionsGeneralFill : DBConnection
    {
        //to fill currency combo
        public void FillCurrencyCombo(ComboBox cmb)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("CurrencyViewAllForBranch", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                if (dtbl.Rows.Count > 0)
                {
                    cmb.DataSource = dtbl;
                    cmb.DisplayMember = "currencySymbol";
                    cmb.ValueMember = "currencyId";
                    if (dtbl.Select("currencyId ='" + PublicVariables._currencyId + "'").Length > 0)
                    {
                        cmb.SelectedValue = PublicVariables._currencyId;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
      
        public void FillCurrencyComboWithMultpleData(ComboBox cmb, DateTime currentDate)
        {


            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("CurrencyViewAllForBranchAndDate", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@currentDate", SqlDbType.DateTime);
                sqlparameter.Value = currentDate;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                if (dtbl.Rows.Count > 0)
                {
                    cmb.DataSource = dtbl;
                    cmb.DisplayMember = "currencySymbol";
                    cmb.ValueMember = "currencyId";
                    if (dtbl.Select("currencyId ='" + PublicVariables._currencyId + "'").Length > 0)
                    {
                        cmb.SelectedValue = PublicVariables._currencyId;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //to fill cash or party in registers with 'All' as first item
        //public void FillCashOrPartyCombo(ComboBox cmbCurr)
        //{
        //    try
        //    {
             
        //        cmbCurr.DataSource = null;
        //        AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
        //        DataTable dtblCashOrParty = SpAccountLedger.AccountLedgerGetWithBalance();
        //        cmbCurr.DataSource = dtblCashOrParty;
        //        cmbCurr.DisplayMember = "ledgerName";
        //        cmbCurr.ValueMember = "ledgerId";
        //        DataRow dr1 = dtblCashOrParty.NewRow();
        //        dr1["ledgerName"] = "All";
        //        dr1["ledgerId"] = "All";
        //        dtblCashOrParty.Rows.InsertAt(dr1, 0);
        //        cmbCurr.SelectedValue = "All";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF11:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //public DataTable FillCashOrPartyLookup()
        //{
        //    DataTable dtblCashOrParty = new DataTable();
        //    try
        //    {
                
        //        AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
        //         dtblCashOrParty = SpAccountLedger.AccountLedgerGetWithBalance();
               
        //        //DataRow dr1 = dtblCashOrParty.NewRow();
        //        //dr1["ledgerName"] = "All";
        //        //dr1["ledgerId"] = "All";
        //        //dtblCashOrParty.Rows.InsertAt(dr1, 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF11:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    return dtblCashOrParty;
        //}

        public void FillLedgerUnderGroupCombo(ComboBox cmbCurr, string strGroupId)
        {
            try
            {
              
                cmbCurr.DataSource = null;
                AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
                DataTable dtblCashOrParty = SpAccountLedger.AccountLedgerGetWithBalanceByGroupId(strGroupId, PublicVariables._branchId);
                cmbCurr.DataSource = dtblCashOrParty;

                if (dtblCashOrParty.Rows.Count > 0)
                {
                    cmbCurr.DisplayMember = "ledgerName";
                    cmbCurr.ValueMember = "ledgerId";
                    cmbCurr.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public void FillCashOrPartyComboForRpt(ComboBox cmbCurr, string strBranchId)
        //{
        //    try
        //    {
          
        //        cmbCurr.DataSource = null;
        //        AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
        //        DataTable dtblCashOrParty = SpAccountLedger.AccountLedgerGetWithBalanceForRpt(strBranchId);
        //        cmbCurr.DataSource = dtblCashOrParty;
        //        cmbCurr.DisplayMember = "ledgerName";
        //        cmbCurr.ValueMember = "ledgerId";
        //        DataRow dr1 = dtblCashOrParty.NewRow();
        //        dr1["ledgerName"] = "All";
        //        dr1["ledgerId"] = "All";
        //        dtblCashOrParty.Rows.InsertAt(dr1, 0);
        //        cmbCurr.SelectedValue = "All";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //public void FillPartyByAreaAndMarketForReport(ComboBox cmbCurr, string strBranchId, string areaId, string marketId)
        //{
        //    try
        //    {
               
        //        cmbCurr.DataSource = null;
        //        AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
        //        DataTable dtblCashOrParty = SpAccountLedger.PartyGetByMarketAndAreaForReport(strBranchId, areaId, marketId);
        //        cmbCurr.DataSource = dtblCashOrParty;
        //        cmbCurr.DisplayMember = "ledgerName";
        //        cmbCurr.ValueMember = "ledgerId";
        //        DataRow dr1 = dtblCashOrParty.NewRow();
        //        dr1["ledgerName"] = "All";
        //        dr1["ledgerId"] = "All";
        //        dtblCashOrParty.Rows.InsertAt(dr1, 0);
        //        cmbCurr.SelectedValue = "All";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        public void FillCashOrBankComboForRpt(ComboBox cmbCurr, string strBranchId)
        {
            // To fill bank or cash combo
            try
            {
                cmbCurr.DataSource = null;
                AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
                DataTable dtbl = SpAccountLedger.AccountLedgerGetCashBankAndODWithBalance(strBranchId);
                cmbCurr.DataSource = dtbl;
                cmbCurr.DisplayMember = "ledgerName";
                cmbCurr.ValueMember = "ledgerId";
                DataRow dr1 = dtbl.NewRow();
                dr1["ledgerName"] = "All";
                dr1["ledgerId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillCashOrBankComboByGroupForRpt(ComboBox cmbCurr, string strBranchId)
        {
            // To fill bank or cash combo
            try
            {
                cmbCurr.DataSource = null;
                AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
              
                DataTable dtbl = SpAccountLedger.AccountLedgerGetWithBalanceByGroupId("50", strBranchId);

                cmbCurr.DataSource = dtbl;
                cmbCurr.DisplayMember = "ledgerName";
                cmbCurr.ValueMember = "ledgerId";
                DataRow dr1 = dtbl.NewRow();
                dr1["ledgerName"] = "All";
                dr1["ledgerId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public void FillEmployeeViewWithCodeForRpt(ComboBox cmbCurr, string strBranchId, string strStatus)
        //{
        //    try
        //    {
        //        cmbCurr.DataSource = null;
        //        EmployeeSP SpEmployee = new EmployeeSP();
              
        //        DataTable dtbl = SpEmployee.EmployeeViewWithCodeByBranchIdSearch(strBranchId, strStatus);
        //        cmbCurr.DataSource = dtbl;
        //        cmbCurr.DisplayMember = "employeeName";
        //        cmbCurr.ValueMember = "employeeId";
        //        DataRow dr1 = dtbl.NewRow();
        //        dr1["employeeName"] = "All";
        //        dr1["employeeId"] = "All";
        //        dtbl.Rows.InsertAt(dr1, 0);
        //        cmbCurr.SelectedValue = "All";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //public void FillEmployeeViewWithCodeByDesignationForRpt(ComboBox cmbCurr, string strDesignationId, string strBranchId)
        //{
        //    try
        //    {
        //        cmbCurr.DataSource = null;
        //        EmployeeSP SpEmployee = new EmployeeSP();
        //        DataTable dtbl = SpEmployee.EmployeeViewWithCodeByDesignationAndBranchId(strBranchId, strDesignationId, "All");
        //        cmbCurr.DataSource = dtbl;
        //        cmbCurr.DisplayMember = "employeeName";
        //        cmbCurr.ValueMember = "employeeId";
        //        DataRow dr1 = dtbl.NewRow();
        //        dr1["employeeName"] = "All";
        //        dr1["employeeId"] = "All";
        //        dtbl.Rows.InsertAt(dr1, 0);
        //        cmbCurr.SelectedValue = "All";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        public void FillProductGroupForRpt(ComboBox cmbCurr)
        {
            try
            {
                cmbCurr.DataSource = null;
                ProductGroupSP SpGroup = new ProductGroupSP();
                DataTable dtbl = SpGroup.ProductGroupViewAll();
                cmbCurr.DataSource = dtbl;
               
                cmbCurr.DisplayMember = "groupName";
                cmbCurr.ValueMember = "groupId";
                DataRow dr1 = dtbl.NewRow();
                dr1["groupName"] = "All";
                dr1["groupId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillAreaByBranchIdForRpt(ComboBox cmbCurr, string strBranchId)
        {
            try
            {
                cmbCurr.DataSource = null;
                AreaSP SpArea = new AreaSP();
                DataTable dtbl = SpArea.AreaViewForReport(strBranchId);
                cmbCurr.DataSource = dtbl;
               
                cmbCurr.DisplayMember = "areaName";
                cmbCurr.ValueMember = "areaId";
                DataRow dr1 = dtbl.NewRow();
                dr1["areaName"] = "All";
                dr1["areaId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillRouteByMarketForRpt(ComboBox cmbCurr, string strMarket, string strAreaId)
        {
            try
            {
                cmbCurr.DataSource = null;
                DataTable dtbl = new RouteSP().RouteViewAllByMarketWithoutNA(strMarket, strAreaId);
                cmbCurr.DataSource = dtbl;
                cmbCurr.DisplayMember = "routeName";
                cmbCurr.ValueMember = "routeId";
                DataRow dr1 = dtbl.NewRow();
                dr1["routeName"] = "All";
                dr1["routeId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillBatchForRpt(ComboBox cmbCurr)
        {
            try
            {
                cmbCurr.DataSource = null;
                BatchSP SpBatch = new BatchSP();
                DataTable dtbl = SpBatch.BatchViewAllForReport();

                
                cmbCurr.DataSource = dtbl;
                cmbCurr.DisplayMember = "batchName";
                cmbCurr.ValueMember = "batchId";
                DataRow dr1 = dtbl.NewRow();
                dr1["batchName"] = "All";
                dr1["batchId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillProductByGroupIdForRpt(ComboBox cmbCurr, string strGroup, string strBranchId)
        {
            try
            {
                cmbCurr.DataSource = null;
                ProductSP ProductSP = new ProductSP();
                DataTable dtbl = ProductSP.ProductViewAllActiveByGroupId(strGroup, strBranchId);
                cmbCurr.DataSource = dtbl;
                cmbCurr.DisplayMember = "productName";
                cmbCurr.ValueMember = "productCode";
                DataRow dr1 = dtbl.NewRow();
                dr1["productName"] = "All";
                dr1["productCode"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillMarketByAreaForRpt(ComboBox cmbCurr, string strArea)
        {
            try
            {
                cmbCurr.DataSource = null;
                MarketSP SpMarket = new MarketSP();
                DataTable dtbl = SpMarket.MarketViewAllByArea(strArea, true);

                DataRow dr1 = dtbl.NewRow();
                dr1["marketName"] = "All";
                dr1["marketId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.DataSource = dtbl;
                cmbCurr.DisplayMember = "marketName";
                cmbCurr.ValueMember = "marketId";
                cmbCurr.SelectedValue = "All";
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillProductByBatchForRpt(ComboBox cmbCurr, string strBatchId)
        {
            try
            {
                cmbCurr.DataSource = null;
                BatchSP SpBatch = new BatchSP();
                DataTable dtbl = SpBatch.FillProductByBatchForRpt(strBatchId);
                cmbCurr.DataSource = dtbl;
               
                cmbCurr.DisplayMember = "productName";
                cmbCurr.ValueMember = "productCode";
                DataRow dr1 = dtbl.NewRow();
                dr1["productName"] = "All";
                dr1["productCode"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public void FillDesignationViewWithCodeForRpt(ComboBox cmbCurr, string strBranchId)
        //{
        //    // To fill bank or cash combo
        //    try
        //    {
        //        cmbCurr.DataSource = null;
        //        DesignationSP SpDesignation = new DesignationSP();
        //        DataTable dtbl = SpDesignation.DesignationViewAll(strBranchId);
        //        cmbCurr.DataSource = dtbl;
                
        //        cmbCurr.DisplayMember = "designationName";
        //        cmbCurr.ValueMember = "designationId";
        //        DataRow dr1 = dtbl.NewRow();
        //        dr1["designationName"] = "All";
        //        dr1["designationId"] = "0";
        //        dtbl.Rows.InsertAt(dr1, 0);
        //        cmbCurr.SelectedValue = "0";
                
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("PV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        public void FillDebtorAndCreditorComboForRpt(ComboBox cmbCurr, string strBranchId)
        {
            // To fill bank or cash combo
            try
            {
                cmbCurr.DataSource = null;
                AccountLedgerSP spledger = new AccountLedgerSP();
                DataTable dtbl = new DataTable();
                dtbl = spledger.AccountLedgerGetByDebtorAndCreditorWithBalance(strBranchId);
                cmbCurr.DataSource = dtbl;
               
                cmbCurr.DisplayMember = "ledgerName";
                cmbCurr.ValueMember = "ledgerId";
                DataRow dr1 = dtbl.NewRow();
                dr1["ledgerName"] = "All";
                dr1["ledgerId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDCPR02:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //to fill product combo in grid
        public void FillProductComboForGridCell(MultiColumnComboBox cmb, DataGridView dgvCurrent, string branchId, bool isdontRemove, string godownId, string rackId, int inRowIndex)
        {
            cmb.DataSource = null;
            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            string strProductName = "";
            if (dgvCurrent.Columns.Contains("productName") && dgvCurrent.Rows[inRowIndex].Cells["productName"].Value != null)
                strProductName = dgvCurrent.Rows[inRowIndex].Cells["productName"].Value.ToString();
            dtbl = SPProduct.ProductViewAllActiveWithStock(branchId, godownId, false, false, "", rackId);
            try
            {
                DataGridViewComboBoxCell dgvccProductCode = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["productCode"].Index, inRowIndex];
                string StrOldId = (dgvCurrent[dgvCurrent.Columns["productCode"].Index, inRowIndex].Value == null ? "" : dgvCurrent[dgvCurrent.Columns["productCode"].Index, inRowIndex].Value.ToString());
                dgvCurrent[dgvCurrent.Columns["productCode"].Index, inRowIndex].Value = null;
                DataRow drow = dtbl.NewRow();
                dtbl.Rows.InsertAt(drow, 0);
                dtbl.Rows[0]["productcode"] = "";
                if (!isdontRemove)
                {
                    if (dtbl.Rows.Count > 1 && dgvCurrent.Rows.Count > 1)
                    {
                        string strCondition = "";

                        foreach (DataGridViewRow gridRow in dgvCurrent.Rows)
                        {
                            if ((gridRow.Cells["productCode"].Value != null && gridRow.Cells["productCode"].Value.ToString() != "") && inRowIndex != gridRow.Index)
                            {
                                if (strCondition == "")
                                    strCondition = "productCode <>'" + gridRow.Cells["productCode"].Value.ToString() + "'";
                                else
                                {
                                    strCondition += "AND productCode <>'" + gridRow.Cells["productCode"].Value.ToString() + "'";
                                }

                            }

                        }
                        if (strCondition != "")
                        {

                            strCondition = "(" + strCondition + ")";
                            strCondition += "OR(  productCode ='" + "''" + "')";
                            dtbl.DefaultView.RowFilter = strCondition;
                            dtbl = dtbl.DefaultView.ToTable();

                        }
                    }
                   
                }

                dgvccProductCode.DataSource = dtbl;
                cmb.DataSource = dtbl;
                if (dtbl.Rows.Count == 1)
                    dtbl.Rows.Clear();


             
                dgvccProductCode.DisplayMember = "productCode";
                dgvccProductCode.ValueMember = "productCode";

                dgvCurrent[dgvCurrent.Columns["productCode"].Index, inRowIndex].Value = null;
                cmb.DisplayMember = "prdCode";
                cmb.ValueMember = "productCode";
                cmb.SelectedIndex = -1;
                if (dtbl.Columns.Count > 0)
                {
                    DataRow[] dr = dtbl.Select("productName ='" + strProductName + "'");
                    if (dr.Length > 0)
                    {
                        dgvCurrent[dgvCurrent.Columns["productCode"].Index, inRowIndex].Value = dr[0]["productCode"].ToString();

                    }
                    else if (strProductName != "")
                    {
                        dgvCurrent[dgvCurrent.Columns["productName"].Index, inRowIndex].Value = "";
                    }
                    else if (StrOldId != "")
                    {
                        if (dtbl.Select("productCode ='" + StrOldId + "'").Length > 0)
                        {
                            dgvCurrent[dgvCurrent.Columns["productCode"].Index, inRowIndex].Value = StrOldId;
                        }

                    }
                    else
                    {
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public AutoCompleteStringCollection ProductFillforLsit(string branchId, DataGridView dgvCurrent, int inRowIndex, bool isdontRemove)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductViewAllActiveWithStock(branchId, "", false, false, "", "");
            if (dtbl.Rows.Count > 1 && dgvCurrent.Rows.Count > 1 && !isdontRemove)
            {
                string strCondition = "";

                foreach (DataGridViewRow gridRow in dgvCurrent.Rows)
                {
                    if ((gridRow.Cells["productCode"].Value != null && gridRow.Cells["productCode"].Value.ToString() != "") && inRowIndex != gridRow.Index)
                    {
                        if (strCondition == "")
                            strCondition = "productCode <>'" + gridRow.Cells["productCode"].Value.ToString() + "'";
                        else
                        {
                            strCondition += "AND productCode <>'" + gridRow.Cells["productCode"].Value.ToString() + "'";
                        }

                    }
                }

                if (strCondition != "")
                {

                    strCondition = "(" + strCondition + ")";
                    strCondition += "OR(  productCode ='" + "''" + "')";
                    dtbl.DefaultView.RowFilter = strCondition;
                    dtbl = dtbl.DefaultView.ToTable();

                }
            }
            foreach (DataRow dr in dtbl.Rows)
            {
                collection.Add(dr["productName"].ToString());
            }

            return collection;
        }
        public DataTable ProductFillforLsit(string branchId, string voucherType)
        {

            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductViewAllActiveForSearch(branchId, "", false, false, "", "", voucherType);
            return dtbl;
        }
        public DataTable ProductFillforLookup(string branchId, string voucherType)
        {

            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductViewAllActiveForSearchLookup(branchId, "", false, false, "", "", voucherType);
            return dtbl;
        }
        public DataTable ProductFillforLookupbyGroupId(string branchId, string voucherType,string groupId)
        {

            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductViewAllActiveForSearchLookupByGroupId(branchId, "", false, false, "", "", voucherType,groupId);
            return dtbl;
        }


        public void FillProductComboForGridCellWithProductName(MultiColumnComboBox cmb, DataGridView dgvCurrent, string branchId, bool isdontRemove)
        {
            cmb.DataSource = null;
            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductViewAllActiveWithName(branchId);
            try
            {
                DataGridViewComboBoxCell dgvccProductCode = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["productCode"].Index, dgvCurrent.CurrentRow.Index];
                string StrOldId = (dgvCurrent[dgvCurrent.Columns["productCode"].Index, dgvCurrent.CurrentRow.Index].Value == null ? "" : dgvCurrent[dgvCurrent.Columns["productCode"].Index, dgvCurrent.CurrentRow.Index].Value.ToString());

                DataRow drow = dtbl.NewRow();
                dtbl.Rows.InsertAt(drow, 0);

                if (!isdontRemove)
                {
                    if (dtbl.Rows.Count > 1 && dgvCurrent.Rows.Count > 1)
                    {
                        string strCondition = "";
                        foreach (DataGridViewRow gridRow in dgvCurrent.Rows)
                        {
                            if ((gridRow.Cells["productCode"].Value != null && gridRow.Cells["productCode"].Value.ToString() != "") && dgvCurrent.CurrentRow.Index != gridRow.Index)
                            {
                                if (strCondition == "")
                                    strCondition = "productCode <>'" + gridRow.Cells["productCode"].Value.ToString() + "'";
                                else
                                {
                                    strCondition += "AND productCode <>'" + gridRow.Cells["productCode"].Value.ToString() + "'";
                                }

                            }

                        }
                        if (strCondition != "")
                        {
                            dtbl.DefaultView.RowFilter = strCondition;
                            dtbl = dtbl.DefaultView.ToTable();
                        }
                    }

                   
                }
                dgvccProductCode.DataSource = dtbl;
                cmb.DataSource = dtbl;
                if (dtbl.Rows.Count == 1)
                    dtbl.Rows.Clear();


              

                dgvccProductCode.DisplayMember = "productCode";
                dgvccProductCode.ValueMember = "productCode";

                dgvCurrent[dgvCurrent.Columns["productCode"].Index, dgvCurrent.CurrentRow.Index].Value = null;
                cmb.DisplayMember = "prdCode";
                cmb.ValueMember = "productCode";
                cmb.SelectedIndex = -1;
                if (dtbl.Columns.Count > 0)
                {
                    if (StrOldId != "")
                    {
                        if (dtbl.Select("productCode ='" + StrOldId + "'").Length > 0)
                        {
                            dgvCurrent[dgvCurrent.Columns["productCode"].Index, dgvCurrent.CurrentRow.Index].Value = StrOldId;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void FillProductComboForGridCellWithProductNameForDetailsGrid(MultiColumnComboBox cmb, DataGridView dgvCurrent, string branchId, bool isdontRemove)
        {
            cmb.DataSource = null;
            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductViewAllActiveWithNameTwo(branchId);
            try
            {
                DataGridViewComboBoxCell dgvccProductCode = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["giftProduct"].Index, dgvCurrent.CurrentRow.Index];
                string StrOldId = (dgvCurrent[dgvCurrent.Columns["giftProduct"].Index, dgvCurrent.CurrentRow.Index].Value == null ? "" : dgvCurrent[dgvCurrent.Columns["giftProduct"].Index, dgvCurrent.CurrentRow.Index].Value.ToString());
             
                DataRow drow = dtbl.NewRow();
                dtbl.Rows.InsertAt(drow, 0);
                dgvccProductCode.DataSource = dtbl;
                cmb.DataSource = dtbl;
                if (dtbl.Rows.Count == 1)
                    dtbl.Rows.Clear();


       

                dgvccProductCode.DisplayMember = "giftProduct";
                dgvccProductCode.ValueMember = "giftProduct";

                dgvCurrent[dgvCurrent.Columns["giftProduct"].Index, dgvCurrent.CurrentRow.Index].Value = null;
                cmb.DisplayMember = "giftProduct";
                cmb.ValueMember = "giftProduct";
                cmb.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //--------

        //public void FillProductComboForGridCellWithGiftItemsForDetailsGrid(MultiColumnComboBox cmb, DataGridView dgvCurrent, string branchId)
        //{

          

        //    cmb.DataSource = null;
        //    GiftItemsSP SpGiftItems = new GiftItemsSP();
        //    DataTable dtbl = SpGiftItems.GiftItemsViewAll();
        //    try
        //    {
        //        DataGridViewComboBoxCell dgvccProductCode = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["giftProduct"].Index, dgvCurrent.CurrentRow.Index];
             
        //        dgvccProductCode.DataSource = dtbl;
        //        cmb.DataSource = dtbl;
                

        //        dgvccProductCode.DisplayMember = "giftItemsCode";
        //        dgvccProductCode.ValueMember = "giftItemsId";

        //        dgvCurrent[dgvCurrent.Columns["giftProduct"].Index, dgvCurrent.CurrentRow.Index].Value = null;
        //        cmb.DisplayMember = "giftItemsCode";
        //        cmb.ValueMember = "giftItemsId";
        //        cmb.SelectedIndex = -1;
        //        if (dtbl.Columns.Count > 0)
        //        {
                  


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}


        //to fill Unit/Qty Combo of Grid according to chnaged product code
        public void FillUnitPerQtyComboForGrid(DataGridView dgvCurrent, string strPrdCode, int inRowIndex)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("UnitViewAllByProductCode", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
              
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sqlparameter.Value = strPrdCode;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {

                MessageBox.Show("TGF7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                DataGridViewComboBoxCell dgvccQty = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["unitPerQty"].Index, inRowIndex];
                dgvCurrent[dgvCurrent.Columns["unitPerQty"].Index, inRowIndex].Value = null;
                dgvccQty.DataSource = dtbl;
                if (dtbl.Rows.Count > 0)
                {
                    dgvccQty.DisplayMember = "unitName";
                    dgvccQty.ValueMember = "unitId";
                    dgvCurrent[dgvCurrent.Columns["unitPerQty"].Index, inRowIndex].Value = dtbl.Rows[0].ItemArray[2].ToString();

                }
                if (dgvCurrent.Columns.Contains("unitPerRate"))
                {
                    DataGridViewComboBoxCell dgvccRate = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["unitPerRate"].Index, inRowIndex];
                    dgvCurrent[dgvCurrent.Columns["unitPerRate"].Index, inRowIndex].Value = null;

                    dgvccRate.DataSource = dtbl;
                    if (dtbl.Rows.Count > 0)
                    {

                        dgvccRate.DisplayMember = "unitName";
                        dgvccRate.ValueMember = "unitId";

                        dgvCurrent[dgvCurrent.Columns["unitPerRate"].Index, inRowIndex].Value = dtbl.Rows[0].ItemArray[2].ToString();



                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF8:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void FillGiftUnitComboForGridForSalesPromotion(DataGridView dgvCurrent, string strPrdCode, int inRowIndex, bool isProduct)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                if (isProduct)
                {
                    SqlDataAdapter sqldataadapter = new SqlDataAdapter("UnitViewAllByProductCode", sqlcon);
                    sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlparameter = new SqlParameter();
                    sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                    sqlparameter.Value = strPrdCode;
                    sqldataadapter.Fill(dtbl);
                }
                else
                {
                    SqlDataAdapter sqldataadapter = new SqlDataAdapter("UnitViewAll", sqlcon);
                    sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqldataadapter.Fill(dtbl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
              
                DataGridViewComboBoxCell dgvccGiftUnit = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["giftUnit"].Index, inRowIndex];
                dgvCurrent[dgvCurrent.Columns["giftUnit"].Index, inRowIndex].Value = null;
                dgvccGiftUnit.DataSource = dtbl;
                if (dtbl.Rows.Count > 0)
                {
                    dgvccGiftUnit.DisplayMember = "unitName";
                    dgvccGiftUnit.ValueMember = "unitId";
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF8:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void FillUnitPerQtyComboForGridBom(DataGridView dgvCurrent, string strPrdCode, int inRowIndex)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("UnitViewAllByProductCode", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();

                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sqlparameter.Value = strPrdCode;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {

                MessageBox.Show("TGF7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                DataGridViewComboBoxCell dgvccQty = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["Bomunit"].Index, inRowIndex];
                dgvCurrent[dgvCurrent.Columns["Bomqty"].Index, inRowIndex].Value = null;
                dgvccQty.DataSource = dtbl;
                if (dtbl.Rows.Count > 0)
                {
                    dgvccQty.DisplayMember = "unitName";
                    dgvccQty.ValueMember = "unitId";
                    dgvCurrent[dgvCurrent.Columns["Bomunit"].Index, inRowIndex].Value = dtbl.Rows[0].ItemArray[2].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF8:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void FillUnitPerQtyComboForGridProductPurchasePrice(DataGridView dgvCurrent, string strPrdCode, int inRowIndex)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("UnitViewAllByProductCode", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();

                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sqlparameter.Value = strPrdCode;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {

                MessageBox.Show("TGF7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                DataGridViewComboBoxCell dgvccQty = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns["ppp_unit"].Index, inRowIndex];
                
                dgvccQty.DataSource = dtbl;
                if (dtbl.Rows.Count > 0)
                {
                    dgvccQty.DisplayMember = "unitName";
                    dgvccQty.ValueMember = "unitId";
                    dgvCurrent[dgvCurrent.Columns["ppp_unit"].Index, inRowIndex].Value = dtbl.Rows[0].ItemArray[2].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF8:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public DataTable FillUnitComboForGridByProductCode(string strPrdCode)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("UnitViewAllByProductCode", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sqlparameter.Value = strPrdCode;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF27:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        } 
        public void FillPricingLevelCombo(ComboBox cmb)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("PricingLevelViewAllWithNa", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                if (dtbl.Rows.Count > 0)
                {
                    cmb.DataSource = dtbl;
                    cmb.ValueMember = "pricingLevelId";
                    cmb.DisplayMember = "pricingLevelName";
                    if (dtbl.Select("pricingLevelId ='" + 1 + "'").Length > 0)
                    {
                        cmb.SelectedValue = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void FillPricingLevelGridCombo(DataGridViewComboBoxColumn dgvcmbPricing)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("PricingLevelViewAllWithNa", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            try
            {
                if (dtbl.Rows.Count > 0)
                {
                    dgvcmbPricing.DataSource = dtbl;
                    if (dgvcmbPricing.DataSource != null)
                    {
                        dgvcmbPricing.DisplayMember = "pricingLevelName";
                        dgvcmbPricing.ValueMember = "pricingLevelId";
                      
                    }
                  
                    //if (dtbl.Select("pricingLevelId ='" + 1 + "'").Length > 0)
                    //{
                    //    dgvcmbPricing.SelectedValue = 1;
                    //}

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public decimal SelectedCrrencyToDefault(decimal dValue, string strConversionRateId)
        {
            decimal dResult = 0;

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("SelectedCrrencyToDefault", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@conversionRateId", SqlDbType.VarChar);
                sprmparam.Value = strConversionRateId;
                sprmparam = sccmd.Parameters.Add("@value", SqlDbType.Decimal);
                sprmparam.Value = dValue;
                dResult = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }


            return dResult;
        }
        public decimal DefaultCurrencyToSelectedWithRounding(decimal dValue, string strConversionRateId)
        {
            decimal dResult = 0;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("DefaultCurrencyToSelectedWithRounding", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@conversionRateId", SqlDbType.VarChar);
                sprmparam.Value = strConversionRateId;
                sprmparam = sccmd.Parameters.Add("@value", SqlDbType.Decimal);
                sprmparam.Value = dValue;
                dResult = decimal.Parse(sccmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return dResult;
        }
        //fill sales man to combo
        //public void FillSalesManCombo(ComboBox cmb, string salesmanId)
        //{
        //    cmb.DataSource = null;
        //    DataTable dtbl = new DataTable();
        //    EmployeeSP SPEmployee = new EmployeeSP();
        //    try
        //    {
        //        dtbl = SPEmployee.SalesmanViewAllByCondition("", salesmanId, PublicVariables._branchId);
        //        if (dtbl.Rows.Count > 0)
        //        {
        //            cmb.DataSource = dtbl;
        //            cmb.DisplayMember = "employeeName";
        //            cmb.ValueMember = "employeeId";
        //            cmb.SelectedIndex = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF12:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //public void FillSalesManComboRefresh(ComboBox cmb, string salesmanId,int SelectedValue,string SelectText)
        //{
        //    cmb.DataSource = null;
        //    DataTable dtbl = new DataTable();
        //    EmployeeSP SPEmployee = new EmployeeSP();
        //    try
        //    {
        //        dtbl = SPEmployee.SalesmanViewAllByCondition("", salesmanId, PublicVariables._branchId);
        //        if (dtbl.Rows.Count > 0)
        //        {
        //            cmb.DataSource = dtbl;
        //            cmb.DisplayMember = "employeeName";
        //            cmb.ValueMember = "employeeId";

        //            cmb.SelectedValue = SelectedValue;
        //            cmb.Text = SelectText;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF12:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}


        //fill sales man to combo
        //public void FillSalesManComboForActive(ComboBox cmb, string salesmanId, bool active)
        //{
        //    cmb.DataSource = null;
        //    DataTable dtbl = new DataTable();
        //    EmployeeSP SPEmployee = new EmployeeSP();
        //    try
        //    {
        //        dtbl = SPEmployee.SalesmanViewAllByConditionForActive("", salesmanId, PublicVariables._branchId, active);
        //        if (dtbl.Rows.Count > 0)
        //        {
        //            cmb.DataSource = dtbl;
        //            cmb.DisplayMember = "employeeName";
        //            cmb.ValueMember = "employeeId";
        //            cmb.SelectedIndex = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF12:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}




        //public void FillSalesManComboForActiveRefresh(ComboBox cmb, string salesmanId, bool active,int SelectedValue,string SelectText)
        //{
        //    cmb.DataSource = null;
        //    DataTable dtbl = new DataTable();
        //    EmployeeSP SPEmployee = new EmployeeSP();
        //    try
        //    {
        //        dtbl = SPEmployee.SalesmanViewAllByConditionForActive("", salesmanId, PublicVariables._branchId, active);
        //        if (dtbl.Rows.Count > 0)
        //        {
        //            cmb.DataSource = dtbl;
        //            cmb.DisplayMember = "employeeName";
        //            cmb.ValueMember = "employeeId";

        //            cmb.SelectedValue = SelectedValue;
        //            cmb.Text = SelectText;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF12:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}


        ////fill sales man to combo
        //public void FillSalesManComboForRpt(ComboBox cmb, string strBranchId)
        //{
        //    cmb.DataSource = null;
        //    DataTable dtbl = new DataTable();
        //    EmployeeSP SPEmployee = new EmployeeSP();
        //    try
        //    {
        //        dtbl = SPEmployee.SalesmanViewAllByCondition("", "SALES", strBranchId);
        //        if (dtbl.Rows.Count > 0)
        //        {
        //            cmb.DataSource = dtbl;
        //            cmb.DisplayMember = "employeeName";
        //            cmb.ValueMember = "employeeId";
        //            cmb.SelectedIndex = -1;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}
        //public void FillSalesManWithAllForRpt(ComboBox cmb, string strBranchId)
        //{
        //    cmb.DataSource = null;
        //    DataTable dtbl = new DataTable();
        //    EmployeeSP SPEmployee = new EmployeeSP();
        //    try
        //    {
        //        dtbl = SPEmployee.SalesmanViewAllByCondition("", "SALES", strBranchId);
        //        cmb.DataSource = dtbl;
        //        cmb.DisplayMember = "employeeName";
        //        cmb.ValueMember = "employeeId";
        //        DataRow dr1 = dtbl.NewRow();
        //        dr1["employeeName"] = "All";
        //        dr1["employeeId"] = "All";
        //        dtbl.Rows.InsertAt(dr1, 0);
        //        cmb.SelectedValue = "All";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}
        //public void FillSalesManByRouteIdForRpt(ComboBox cmb, string strRouteId, string strMarketId, string strAreaId, string strBranchId)
        //{
        //    cmb.DataSource = null;
        //    try
        //    {
        //        DataTable dtbl = new EmployeeSP().SalesmanViewAllByRouteIdForRpt(strRouteId, strMarketId, strAreaId, strBranchId);
        //        cmb.DataSource = dtbl;
        //        cmb.DisplayMember = "employeeName";
        //        cmb.ValueMember = "employeeId";
        //        DataRow dr1 = dtbl.NewRow();
        //        dr1["employeeName"] = "All";
        //        dr1["employeeId"] = "All";
        //        dtbl.Rows.InsertAt(dr1, 0);
        //        cmb.SelectedValue = "All";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("TGF16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}


        public void FillAccountLedgerIncomeExpenseInGrid(MultiColumnComboBox cmb, int columIndex, int rowIndex, DataGridView dgvCurrent, string branchId)
        {
            // to fill cash or bank accoutn in grid combo

            AccountLedgerSP spledger = new AccountLedgerSP();
            DataTable dtbl = new DataTable();

            dtbl = spledger.AccountLedgerGetByIncomeAndExpenseWithBalance(PublicVariables._branchId);

            DataRow drow = dtbl.NewRow();
            dtbl.Rows.InsertAt(drow, 0);
            dtbl.Rows[0]["ledgerId"] = "";

            string StrOldId = (dgvCurrent[columIndex, rowIndex].Value == null ? "" : dgvCurrent[columIndex, rowIndex].Value.ToString());
            dgvCurrent[columIndex, rowIndex].Value = null;
            if (dtbl.Rows.Count > 1 && dgvCurrent.Rows.Count > 1)
            {
                string strCondition = "";
                foreach (DataGridViewRow gridRow in dgvCurrent.Rows)
                {
                    if ((gridRow.Cells[columIndex].Value != null && gridRow.Cells[columIndex].Value.ToString() != "") && rowIndex != gridRow.Index)
                    {
                        if (strCondition == "")
                            strCondition = "ledgerId <>'" + gridRow.Cells[columIndex].Value.ToString() + "'";
                        else
                        {
                            strCondition += "AND ledgerId <>'" + gridRow.Cells[columIndex].Value.ToString() + "'";
                        }

                    }

                }
                if (strCondition != "")
                {
              
                    strCondition = "(" + strCondition + ")";
                    strCondition += "OR(  ledgerId ='" + "''" + "')";
                    dtbl.DefaultView.RowFilter = strCondition;
                    dtbl = dtbl.DefaultView.ToTable();
                }
            }
            
            DataGridViewComboBoxCell dgvccAccountLedger = (DataGridViewComboBoxCell)dgvCurrent[columIndex, rowIndex];
            try
            {
              

                if (dtbl.Columns.Count > 0)
                {
                    dgvccAccountLedger.DataSource = null;
                    cmb.DataSource = null;


                    dgvccAccountLedger.DataSource = dtbl;
                    dgvccAccountLedger.DisplayMember = "ledgerName";
                    dgvccAccountLedger.ValueMember = "ledgerId";

                    cmb.DataSource = dtbl;
                    cmb.DisplayMember = "ledgerName";
                    cmb.ValueMember = "ledgerId";
                    cmb.SelectedIndex = -1;
                    if (StrOldId != "")
                    {
                        if (dtbl.Select("ledgerId ='" + StrOldId + "'").Length > 0)
                        {
                            dgvCurrent[columIndex, rowIndex].Value = StrOldId;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TG13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        public void FillAccountLedgerInGrid(MultiColumnComboBox cmb, string vouchertype, int columIndex, int rowIndex, DataGridView dgvCurrent, string branchId)
        {
            // to fill cash or bank accoutn in grid combo

            AccountLedgerSP spledger = new AccountLedgerSP();
            DataTable dtbl = new DataTable();
            if (vouchertype == "Contra Voucher")
            {
                dtbl = spledger.AccountLedgerGetCashBankAndODWithBalance(PublicVariables._branchId);
            }
            else if (vouchertype == "PDC Clearance")
            {
                dtbl = spledger.AccountLedgerGetCashBankAndODWithBalance(PublicVariables._branchId);
            }
            else if (vouchertype == "Payment Voucher")
            {
                dtbl = spledger.AccountLedgerGetAllWithBalance();
            }
            else if (vouchertype == "Payment Staff Account")
            {
                dtbl = spledger.AccountLedgerGetWithBalanceByGroupId("50", PublicVariables._branchId);
            }
            else if (vouchertype == "Journal Voucher")
            {
                dtbl = spledger.AccountLedgerGetAllWithBalance();
            }
            else if (vouchertype == "BudgetGroup")
            {
                dtbl = spledger.AccountGroupGetAllWithBalance();
            }
            else if (vouchertype == "BudgetLedger")
            {
               
                // commented as shabeeb posted error as not filling all ledgers
                dtbl = spledger.AccountLedgerGetWithBalanceByGroupId("1", PublicVariables._branchId);
            }
            else if (vouchertype == "AdditionalCost")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetUnderExpenseWithBalance(branchId);
            }
            else if (vouchertype == "JobCreation")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetUnderExpenseWithBalance(branchId);
            }
            else if (vouchertype == "AccountsSale")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetByIncomeWithBalance(branchId);
            }
            else if (vouchertype == "AccountsPurchase")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetByExpenseWithBalance(branchId);
            }
            DataRow drow = dtbl.NewRow();
            dtbl.Rows.InsertAt(drow, 0);
            dtbl.Rows[0]["ledgerId"] = "";
            string StrOldId = (dgvCurrent[columIndex, rowIndex].Value == null ? "" : dgvCurrent[columIndex, rowIndex].Value.ToString());
            dgvCurrent[columIndex, rowIndex].Value = null;
            if (dtbl.Rows.Count > 1 && dgvCurrent.Rows.Count > 0)
            {
                

                string strCondition = "";
                foreach (DataGridViewRow gridRow in dgvCurrent.Rows)
                {
                    if ((gridRow.Cells[columIndex].Value != null && gridRow.Cells[columIndex].Value.ToString() != "") && rowIndex != gridRow.Index)
                    {
                        if (strCondition == "")
                            strCondition = "ledgerId <>'" + gridRow.Cells[columIndex].Value.ToString() + "'";
                        else
                        {
                            strCondition += "AND ledgerId <>'" + gridRow.Cells[columIndex].Value.ToString() + "'";
                        }

                    }

                }
                if (strCondition != "")
                {
                    
                    strCondition = "(" + strCondition + ")";
                    strCondition += "OR(  ledgerId ='" + "''" + "')";
                    dtbl.DefaultView.RowFilter = strCondition;
                    dtbl = dtbl.DefaultView.ToTable();
                }
            }

            DataGridViewComboBoxCell dgvccAccountLedger = (DataGridViewComboBoxCell)dgvCurrent[columIndex, rowIndex];
            try
            {
             

                if (dtbl.Columns.Count > 0)
                {
                    dgvccAccountLedger.DataSource = null;
                    cmb.DataSource = null;
                    if (vouchertype != "BudgetGroup")
                    {

                        dgvccAccountLedger.DataSource = dtbl;
                        dgvccAccountLedger.DisplayMember = "ledgerName";
                        dgvccAccountLedger.ValueMember = "ledgerId";

                        cmb.DataSource = dtbl;
                        cmb.DisplayMember = "ledgerName";
                        cmb.ValueMember = "ledgerId";
                        cmb.SelectedIndex = -1;
                        if (StrOldId != "")
                        {
                            if (dtbl.Select("ledgerId ='" + StrOldId + "'").Length > 0)
                            {
                                dgvCurrent[columIndex, rowIndex].Value = StrOldId;
                            }
                        }
                    }
                    else
                    {

                        dgvccAccountLedger.DataSource = dtbl;
                        dgvccAccountLedger.DisplayMember = "accountGroupName";
                        dgvccAccountLedger.ValueMember = "groupId";

                        cmb.DataSource = dtbl;
                        cmb.DisplayMember = "accountGroupName";
                        cmb.ValueMember = "groupId";
                        cmb.SelectedIndex = -1;
                        if (StrOldId != "")
                        {
                            if (dtbl.Select("groupId ='" + StrOldId + "'").Length > 0)
                            {
                                dgvCurrent[columIndex, rowIndex].Value = StrOldId;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TG13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        public DataTable FillAccountLedgerInGridLookup( string vouchertype,string branchId)
        {
            // to fill cash or bank accoutn in grid combo

            AccountLedgerSP spledger = new AccountLedgerSP();
            DataTable dtbl = new DataTable();
            if (vouchertype == "Contra Voucher")
            {
                dtbl = spledger.AccountLedgerGetCashBankAndODWithBalance(PublicVariables._branchId);
            }
            else if (vouchertype == "PDC Clearance")
            {
                dtbl = spledger.AccountLedgerGetCashBankAndODWithBalance(PublicVariables._branchId);
            }
            else if (vouchertype == "Payment Voucher")
            {
                dtbl = spledger.AccountLedgerGetAllWithBalance();
            }
            else if (vouchertype == "Payment Staff Account")
            {
                dtbl = spledger.AccountLedgerGetWithBalanceByGroupId("50", PublicVariables._branchId);
            }
            else if (vouchertype == "Journal Voucher")
            {
                dtbl = spledger.AccountLedgerGetAllWithBalance();
            }
            else if (vouchertype == "BudgetGroup")
            {
                dtbl = spledger.AccountGroupGetAllWithBalance();
            }
            else if (vouchertype == "BudgetLedger")
            {

                // commented as shabeeb posted error as not filling all ledgers
                dtbl = spledger.AccountLedgerGetWithBalanceByGroupId("1", PublicVariables._branchId);
            }
            else if (vouchertype == "AdditionalCost")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetUnderExpenseWithBalance(branchId);
            }
            else if (vouchertype == "JobCreation")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetUnderExpenseWithBalance(branchId);
            }
            else if (vouchertype == "AccountsSale")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetByIncomeWithBalance(branchId);
            }
            else if (vouchertype == "AccountsPurchase")
            {
                // all ledger under direct expense
                dtbl = spledger.AccountLedgerGetByExpenseWithBalance(branchId);
            }

            return dtbl;
        }

        public void FillServiceComboForGridCell(MultiColumnComboBox cmb, string str, int rowIndex, int columIndex, DataGridView dgvCurrent)
        {

            try
            {
                DataTable dtbl = new ServiceSP().ServiceViewAllForFill(str, PublicVariables._branchId);
                DataGridViewComboBoxCell dgvccAccountLedger = (DataGridViewComboBoxCell)dgvCurrent[columIndex, rowIndex];
                dgvccAccountLedger.DataSource = null;
                cmb.DataSource = null;
                dgvccAccountLedger.DataSource = dtbl;
                cmb.DataSource = dtbl;
                if (dtbl.Rows.Count > 0)
                {
                    dgvccAccountLedger.ValueMember = "Service Id";
                    dgvccAccountLedger.DisplayMember = "Service Name";

                    cmb.ValueMember = "Service Id";
                    cmb.DisplayMember = "Service Name";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void FillGridAccountComboCellByAllLedgers(MultiColumnComboBox cmb, string vouchertype, int columIndex, int rowIndex, DataGridView dgvCurrent)
        {
            // To fill all account ledgers in grid

            cmb.DataSource = null;
            AccountLedgerSP spledger = new AccountLedgerSP();
            DataTable dtbl = new DataTable();
            dtbl = spledger.AccountLedgerGetAllWithBalance();
            DataGridViewComboBoxCell dgvccAccountLedger = (DataGridViewComboBoxCell)dgvCurrent[columIndex, rowIndex];
            try
            {
                if (dtbl.Rows.Count > 0)
                {
                    dgvccAccountLedger.DataSource = dtbl;
                    dgvccAccountLedger.DisplayMember = "ledgerName";
                    dgvccAccountLedger.ValueMember = "ledgerId";

                    cmb.DataSource = dtbl;
                    cmb.DisplayMember = "ledgerName";
                    cmb.ValueMember = "ledgerId";
                    cmb.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //to fill godown drop down in grid cell (corresponding to branch
        public string FillGodownToGridComboColumn(DataGridViewComboBoxColumn dgvcmbGodown)
        {
            string strPrimaryId = "";
            GodownSP SpGodown = new GodownSP();
            DataTable dtbl = SpGodown.GodownViewAll();
            dgvcmbGodown.DataSource = dtbl;

            if (dgvcmbGodown.DataSource != null)
            {
                dgvcmbGodown.ValueMember = "godownId";
                dgvcmbGodown.DisplayMember = "godownName";

                strPrimaryId = dtbl.Rows[0].ItemArray[7].ToString();
            }
            return strPrimaryId;
        }
        //to fill rack drop down in grid cell (corresponding to godown
        public string FillRackToGridComboColumnFroInvoice(DataGridViewComboBoxColumn dgvcmbRack, string godownId, string strProductCode)
        {
          
            string strPrimaryId = "";
        
            GodownSP SpGodown = new GodownSP();
            DataTable dtbl = SpGodown.RackViewWithCurrentStockOfProductByGodown(strProductCode, PublicVariables._branchId, godownId, "");//spRack.RackViewAllByGodown(godownId);
            dgvcmbRack.DataSource = dtbl;
            if (dgvcmbRack.DataSource != null)
            {
                dgvcmbRack.ValueMember = "rackId";
                dgvcmbRack.DisplayMember = "rackName";

                strPrimaryId = dtbl.Rows[0]["rackId"].ToString();
            }
            return strPrimaryId;
        }




        //to fill rack drop down in grid cell (corresponding to godown
        public string FillRackToGridComboColumn(DataGridViewComboBoxColumn dgvcmbRack, string godownId)//string strProductCode)
        {
       
            string strPrimaryId = "";
            RackSP spRack = new RackSP();
            DataTable dtbl = spRack.RackViewAllByGodown(godownId);
            dgvcmbRack.DataSource = dtbl;
            if (dgvcmbRack.DataSource != null)
            {
                dgvcmbRack.ValueMember = "rackId";
                dgvcmbRack.DisplayMember = "rackName";

                strPrimaryId = dtbl.Rows[0]["defaultId"].ToString();
            }
            return strPrimaryId;
        }
        //to fill godown to multi column combo cell corresponding to branch and product-----------
        public void FillGodownMultiComboForGridCellByPrdAndBranch(MultiColumnComboBox cmb, DataGridView dgvCurrent, string strPrdCode, string branchId, string strcolName, int inrowindex)
        {

            cmb.DataSource = null;
            GodownSP SpGodown = new GodownSP();
            DataTable dtbl = SpGodown.GodownViewWithCurrentStockOfProduct(strPrdCode, branchId, "");

            try
            {
                DataGridViewComboBoxCell dgvccGodown = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex];
                string StrOldId = (dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value == null ? "" : dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value.ToString());
                dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value = null;
                dgvccGodown.DataSource = dtbl;
                cmb.DataSource = dtbl;
                if (dtbl.Rows.Count > 0)
                {



                    dgvccGodown.DisplayMember = "godownName";
                    dgvccGodown.ValueMember = "godownId";

                    cmb.DisplayMember = "godownName";
                    cmb.ValueMember = "godownId";
                    cmb.SelectedIndex = -1;
                    if (StrOldId != "")
                    {
                        if (dtbl.Select("godownId ='" + StrOldId + "'").Length > 0)
                        {
                            dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value = StrOldId;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(":" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        public void FillGodowninGrid(MultiColumnComboBox cmb, DataGridView dgvCurrent, string strPrdCode, string branchId, string strcolName)//, int inrowindex)
        {
            try
            {
                GodownSP SpGodown = new GodownSP();
                DataTable dtbl = SpGodown.GodownViewWithCurrentStockOfProduct(strPrdCode, branchId, "");
                DataGridViewComboBoxColumn cmbcell = (DataGridViewComboBoxColumn)dgvCurrent.Columns[strcolName];
                DataRow drow = dtbl.NewRow();
                dtbl.Rows.InsertAt(drow, 0);
                dtbl.Rows[0]["godownId"] = "";
                cmbcell.DataSource = dtbl;
                cmb.DataSource = dtbl;
                cmbcell.DisplayMember = "godownName";
                cmbcell.ValueMember = "godownId";
                cmb.DisplayMember = "godownName";
                cmb.ValueMember = "godownId";
                cmb.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(":" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    
        //To fill rack combo corresponding to branch, godown , and product
        public void FillRackMultiComboForGridCellByPrdAndBranchandGodown(MultiColumnComboBox cmb, DataGridView dgvCurrent, string strPrdCode, string branchId, string strcolName, int inrowindex, string godownId)
        {

            cmb.DataSource = null;
            GodownSP SpGodown = new GodownSP();
            DataTable dtbl = SpGodown.RackViewWithCurrentStockOfProductByGodown(strPrdCode, branchId, godownId, "");

            try
            {
                DataGridViewComboBoxCell dgvccGodown = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex];
                string StrOldId = (dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value == null ? "" : dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value.ToString());
                dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value = null;
                dgvccGodown.DataSource = dtbl;
                cmb.DataSource = dtbl;
                DataRow drow = dtbl.NewRow();
                dtbl.Rows.InsertAt(drow, 0);
                dtbl.Rows[0]["rackId"] = "";
                if (dtbl.Rows.Count > 0)
                {

       

                    dgvccGodown.DisplayMember = "rackName";
                    dgvccGodown.ValueMember = "rackId";

                    cmb.DisplayMember = "rackName";
                    cmb.ValueMember = "rackId";
                    cmb.SelectedIndex = -1;
                    if (StrOldId != "")
                    {
                        if (dtbl.Select("rackId ='" + StrOldId + "'").Length > 0)
                        {
                            dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value = StrOldId;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(":" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        public void FillAllRackInGrid(MultiColumnComboBox cmb, DataGridView dgvCurrent, string strPrdCode, string branchId, string strcolName, int inrowindex, string godownId)
        {
            try
            {
                GodownSP SpGodown = new GodownSP();
                DataTable dtbl = SpGodown.RackViewWithCurrentStockOfProductByGodown(strPrdCode, branchId, godownId, "");
                DataGridViewComboBoxColumn cmbcel = (DataGridViewComboBoxColumn)dgvCurrent.Columns[strcolName];
                DataRow drow = dtbl.NewRow();
                dtbl.Rows.InsertAt(drow, 0);
                dtbl.Rows[0]["rackId"] = "";
                cmbcel.DataSource = dtbl;
                cmb.DataSource = dtbl;
                cmbcel.DisplayMember = "rackName";
                cmbcel.ValueMember = "rackId";
                cmb.DisplayMember = "rackName";
                cmb.ValueMember = "rackId";
                cmb.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(":" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillRackComboForGridCellByPrdAndBranchandGodown(DataGridView dgvCurrent, string strPrdCode, string branchId, string strcolName, int inrowindex, string godownId)
        {


            GodownSP SpGodown = new GodownSP();
            DataTable dtbl = SpGodown.RackViewWithCurrentStockOfProductByGodown(strPrdCode, branchId, godownId, "");

            try
            {
                DataGridViewComboBoxCell dgvccGodown = (DataGridViewComboBoxCell)dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex];
                string StrOldId = (dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value == null ? "" : dgvCurrent[dgvCurrent.Columns[strcolName].Index, dgvCurrent.CurrentRow.Index].Value.ToString());
                dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value = null;
                dgvccGodown.DataSource = dtbl;

                if (dtbl.Rows.Count > 0)
                {

  

                    dgvccGodown.DisplayMember = "rackName";
                    dgvccGodown.ValueMember = "rackId";


                    if (StrOldId != "")
                    {
                        if (dtbl.Select("rackId ='" + StrOldId + "'").Length > 0)
                        {
                            dgvCurrent[dgvCurrent.Columns[strcolName].Index, inrowindex].Value = StrOldId;
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(":" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        //------------------------------------------------------
        //fill all units in grid combo column 
        public void FillUnitToGridComboColumn(DataGridViewComboBoxColumn dgvcmbUnit)
        {
            UnitSP SPUnit = new UnitSP();
            dgvcmbUnit.DataSource = SPUnit.UnitViewAll();
            if (dgvcmbUnit.DataSource != null)
            {
                dgvcmbUnit.DisplayMember = "unitName";
                dgvcmbUnit.ValueMember = "unitId";
            }
        }
        public void FillPartyBalanceByAccountLedgerInGrid(MultiColumnComboBox cmb, int columIndex, int rowIndex, DataGridView dgvCurrent, string strLId, string strCrDr, string strMasterId, string strVoucherType)
        {
            // to fill PartyBalance of Ledger in grid combo  

            cmb.DataSource = null;
            PartyBalanceSP partybalancesp = new PartyBalanceSP();
            DataTable dtbl = new DataTable();
            DataTable dtblAgainst = new DataTable();
            dtbl = partybalancesp.PartyBalanceViewByLedgerId(strLId, strCrDr);
            if (strMasterId != "")
            {
                PartyBalanceSP SpMaster = new PartyBalanceSP();
                dtblAgainst = SpMaster.PartyBalanceGetByAgainst(strVoucherType, strMasterId, strLId, strCrDr);
            }
            if (dtblAgainst.Rows.Count > 0)
            {
                bool isExist = true;
                for (int i = 0; i < dtblAgainst.Rows.Count; i++)
                {
                    if (dtbl.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtbl.Rows.Count; j++)
                        {
                           
                            if (dtblAgainst.Rows[i]["voucherType"].ToString() == dtbl.Rows[j]["voucherType"].ToString() && (dtblAgainst.Rows[i]["voucherType"].ToString() == "Opening Balance" || dtblAgainst.Rows[i]["voucherNo"].ToString() == dtbl.Rows[j]["voucherNo"].ToString()))
                            {
                                string[] strAmount = dtbl.Rows[j]["amount"].ToString().Split(' ');
                                decimal dcAmount = decimal.Parse(strAmount[0]) + decimal.Parse(dtblAgainst.Rows[i]["amount"].ToString());
                                dtbl.Rows[j]["amount"] = dcAmount.ToString() + " " + strAmount[1];
                                isExist = true;
                                break;
                            }
                            else
                            {
                                isExist = false;
                            }
                        }
                        if (!isExist)
                        {
                            DataRow nrow = dtbl.NewRow();
                            dtbl.Rows.Add(nrow);
                            nrow["ID"] = dtblAgainst.Rows[i]["ID"].ToString();
                            nrow["voucherType"] = dtblAgainst.Rows[i]["voucherType"].ToString();
                            nrow["voucherNo"] = dtblAgainst.Rows[i]["voucherNo"].ToString();
                            nrow["amount"] = dtblAgainst.Rows[i]["amount"].ToString() + " " + dtblAgainst.Rows[i]["currencySymbol"].ToString();
                        }
                    }
                    else
                    {
                        DataRow nrow = dtbl.NewRow();
                        dtbl.Rows.Add(nrow);
                        nrow["ID"] = dtblAgainst.Rows[i]["ID"].ToString();
                        nrow["voucherType"] = dtblAgainst.Rows[i]["voucherType"].ToString();
                        nrow["voucherNo"] = dtblAgainst.Rows[i]["voucherNo"].ToString();
                        nrow["amount"] = dtblAgainst.Rows[i]["amount"].ToString() + " " + dtblAgainst.Rows[i]["currencySymbol"].ToString();
                    }
                }
            }
            if (dtbl.Rows.Count > 0 && dgvCurrent.Rows.Count > 1)
            {
                string strCondition = "";
                foreach (DataGridViewRow gridRow in dgvCurrent.Rows)
                {
                    if ((gridRow.Cells[columIndex].Value != null && gridRow.Cells[columIndex].Value.ToString() != "") && rowIndex != gridRow.Index)
                    {
                        if (strCondition == "")
                            strCondition = "(voucherType <>'" + gridRow.Cells[columIndex + 1].Value.ToString() + "'OR ID <>'" + gridRow.Cells[columIndex].Value.ToString() + "')";
                        else
                        {
                            strCondition += "AND( voucherType <>'" + gridRow.Cells[columIndex + 1].Value.ToString() + "'OR ID <>'" + gridRow.Cells[columIndex].Value.ToString() + "')";
                        }

                    }

                }
                if (strCondition != "")
                {
                   
                    strCondition = "(" + strCondition + ")";
                    strCondition += "OR(  voucherType ='" + "''" + "')";
                    dtbl.DefaultView.RowFilter = strCondition;
                    dtbl = dtbl.DefaultView.ToTable();

                }
            }
          
            DataGridViewComboBoxCell dgvccAccountLedger = (DataGridViewComboBoxCell)dgvCurrent[columIndex, rowIndex];
            try
            {
                dgvccAccountLedger.DataSource = null;
                cmb.DataSource = null;
                if (dtbl.Rows.Count > 0)
                {
                    dgvccAccountLedger.DataSource = dtbl;//.DefaultView.ToTable();
                    dgvccAccountLedger.DisplayMember = "voucherType";
                    dgvccAccountLedger.ValueMember = "ID";

                    cmb.DataSource = dtbl;//.DefaultView.ToTable();
                    cmb.DisplayMember = "voucherType";
                    cmb.ValueMember = "ID";
                    cmb.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("TG13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public bool StoredProcedureInserter(string strParameter)
        {
            bool isOk = true;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                string str = "IF NOT EXISTS(SELECT name FROM sysobjects WHERE type = 'P' AND name='StoredProcedureInserter')"
                + " BEGIN EXECUTE('CREATE PROCEDURE StoredProcedureInserter @parameter varchar(max) AS execute(@parameter)')"
                + " END";
                SqlCommand sqlcmd = new SqlCommand(str, sqlcon);
                sqlcmd.ExecuteNonQuery();


                SqlCommand sccmd = new SqlCommand("StoredProcedureInserter", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@parameter", SqlDbType.VarChar);
                sprmparam.Value = strParameter;

                sccmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                isOk = false;
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return isOk;
        }
        //to fill units of a product to grid unitPerQty and unitPerRate columns
        public string FillUnitsOfProductToGridComboColumn(DataGridView dgvCurrent, string strPrdCode)
        {
            string strDefaultUnitId = "";
            try
            {

                foreach (DataGridViewRow dgvrow in dgvCurrent.Rows)
                {
                    dgvrow.Cells["unitPerQty"].Value = null;
                    dgvrow.Cells["unitPerRate"].Value = null;
                }
                ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerQty"]).DataSource = null;
                ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerRate"]).DataSource = null;
                UnitConversionSP unitconversionsp = new UnitConversionSP();
                DataTable dtbl = new DataTable();
                if (strPrdCode != null)
                {
                    dtbl = unitconversionsp.UnitViewAllByProductCode(strPrdCode);
                    ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerQty"]).DataSource = dtbl;
                    ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerRate"]).DataSource = dtbl;
                    if (dtbl.Rows.Count > 0)
                    {
                        ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerQty"]).ValueMember = "unitId";
                        ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerQty"]).DisplayMember = "unitName";
                        ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerRate"]).ValueMember = "unitId";
                        ((DataGridViewComboBoxColumn)dgvCurrent.Columns["unitPerRate"]).DisplayMember = "unitName";
                        strDefaultUnitId = dtbl.Rows[0].ItemArray[2].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("TGF:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return strDefaultUnitId;
        }
    
        public DataTable ProductSearchVocuherWise(DateTime fromDate, DateTime toDate, string strVoucherType, string strVoucherNo, string strLedgerId, string strEmployeeId, string productCode, string groupId, string startText)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                dtbl.Columns.Add("Sl No");
                dtbl.Columns["Sl No"].AutoIncrement = true;
                dtbl.Columns["Sl No"].AutoIncrementSeed = 1;
                dtbl.Columns["Sl No"].AutoIncrementStep = 1;
                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductSearchVocuherWise", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                sqlparameter.Value = fromDate;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sqlparameter.Value = toDate;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sqlparameter.Value = strVoucherType;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                sqlparameter.Value = strVoucherNo;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sqlparameter.Value = strLedgerId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@employeeId", SqlDbType.VarChar);
                sqlparameter.Value = strEmployeeId;
                //------------------------------------------------
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sqlparameter.Value = productCode;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                sqlparameter.Value = groupId;
            
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@startText", SqlDbType.VarChar);
                sqlparameter.Value = startText;

                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable VoucherSearchFill(DateTime fromDate, DateTime toDate, string strVoucherType, string strVoucherNo, string strLedgerId, string strEmployeeId)
        {

            DataTable dtbl = new DataTable();
            dtbl.Columns.Add("Sl No");
            dtbl.Columns["Sl No"].AutoIncrement = true;
            dtbl.Columns["Sl No"].AutoIncrementSeed = 1;
            dtbl.Columns["Sl No"].AutoIncrementStep = 1;
           
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataAdapter sqldataadapter = new SqlDataAdapter("VoucherSearch", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                sqlparameter.Value = fromDate;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
                sqlparameter.Value = toDate;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sqlparameter.Value = strVoucherType;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@voucherNo", SqlDbType.VarChar);
                sqlparameter.Value = strVoucherNo;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sqlparameter.Value = strLedgerId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@employeeId", SqlDbType.VarChar);
                sqlparameter.Value = strEmployeeId;
                sqldataadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }



        public void FillDebtorAndCreditorComboForPdcRpt(ComboBox cmbCurr, string strBranchId)
        {
            // To fill bank or cash combo
            try
            {
                cmbCurr.DataSource = null;
                AccountLedgerSP spledger = new AccountLedgerSP();
                DataTable dtbl = new DataTable();
                dtbl = spledger.AccountLedgerViewllForPdc(PublicVariables._branchId);
                cmbCurr.DataSource = dtbl;
              
                cmbCurr.DisplayMember = "ledgerName";
                cmbCurr.ValueMember = "ledgerId";
                DataRow dr1 = dtbl.NewRow();
                dr1["ledgerName"] = "All";
                dr1["ledgerId"] = "All";
                dtbl.Rows.InsertAt(dr1, 0);
                cmbCurr.SelectedValue = "All";
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public DataTable ProductMovemenetStockPosting(DateTime fromDate, DateTime toDate, string strVoucherType,  string strLedgerId, string productCode )
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                //dtbl.Columns.Add("Sl No");
                //dtbl.Columns["Sl No"].AutoIncrement = true;
                //dtbl.Columns["Sl No"].AutoIncrementSeed = 1;
                //dtbl.Columns["Sl No"].AutoIncrementStep = 1;
                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductMovementStockPosting", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime);
                sqlparameter.Value = fromDate;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime);
                sqlparameter.Value = toDate;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sqlparameter.Value = strVoucherType;

                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@party", SqlDbType.VarChar);
                sqlparameter.Value = strLedgerId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;
         
                //------------------------------------------------
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sqlparameter.Value = productCode;
           

                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable ProductMovementLookup(   string productCode,string strHistoryType,string ledgrId,string strVoucherType)
        {

            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                //dtbl.Columns.Add("Sl No");
                //dtbl.Columns["Sl No"].AutoIncrement = true;
                //dtbl.Columns["Sl No"].AutoIncrementSeed = 1;
                //dtbl.Columns["Sl No"].AutoIncrementStep = 1;
                SqlDataAdapter sqldataadapter = new SqlDataAdapter("ProductMovementLookup", sqlcon);
                sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sqlparameter = new SqlParameter();
                

                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._branchId;

                //------------------------------------------------
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@productCode", SqlDbType.VarChar);
                sqlparameter.Value = productCode;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
                sqlparameter.Value = PublicVariables._currencyId ;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@historyType", SqlDbType.VarChar);
                sqlparameter.Value = strHistoryType;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sqlparameter.Value = ledgrId;
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
                sqlparameter.Value = strVoucherType;
                sqldataadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
    }

}
