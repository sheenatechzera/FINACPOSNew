using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    partial class frmProductPopUpGrid : Form
    {
        public frmProductPopUpGrid()
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
        /******************************************************************************************************************
         *                                       PUBLIC VARIABLES
         *****************************************************************************************************************/
        #region Public Variables
        bool isFromPOSSales = false;
        bool isFromPOSSales2 = false;
        bool isFromPOSSalesReturn = false;
        bool isFromPOSReceiptVoucher = false;
        bool isFromPOSPaymnetVoucher = false;
        bool isFromBillPrint = false;
        frmPOSSales ObjfrmPOSSales;
        frmPOSSales2 ObjfrmPOSSales2;
        frmPOSSalesReturn ObjfrmPOSSalesReturn;
        frmPOSReceiptVoucher ObjfrmPOSReceiptVoucher;
        frmPOSPaymentVoucher ObjfrmPOSPaymentVoucher;
        frmBillPrint ObjfrmBillPrint;
        ProductSP spProduct = new ProductSP();

        bool isDontExecute = false;
        bool isFormLoad = false;
        public int inRowIndex = 0; // To keep row index of grid of called form
        public string strStartText = ""; // To keep product name while coming from other forms
        private string strcontrolname = "";// To keep control name while coming from other forms
        //public string ledgerNameToReturn = "";
        #endregion
        /******************************************************************************************************************
         *                                       FUNCTIONS
         *****************************************************************************************************************/
        #region Functions
        #region ForOtherForms
        DataTable dtblPoruducts;
        //public void DoWhenComingFromOtherForms(string prdName, int rowIndex, DataTable dtbl)
        //{
        //    inRowIndex = rowIndex;
        //    strStartText = prdName;
        //    dtblPoruducts = dtbl;
        //    if (dtbl != null && dtbl.Rows.Count > 0)
        //        if (dtblPoruducts.Rows[0]["productCode"].ToString() == "")
        //            dtblPoruducts.Rows.RemoveAt(0);
        //    base.ShowInTaskbar = false;
        //    base.ShowDialog();
        //}
        public void FillSearchCombo()
        {
            try
            {

                if (strcontrolname == "product")
                {
                    cmbCriteria.Items.Add("Product Name");
                    cmbCriteria.Items.Add("Product Code");
                    cmbCriteria.Items.Add("Barcode");
                }
                else if (strcontrolname == "customer")
                {
                    cmbCriteria.Items.Add("Ledger Name");
                    cmbCriteria.Items.Add("Ledger Code");
                }
                else if (strcontrolname == "party")
                {
                    cmbCriteria.Items.Add("Ledger Name");
                    cmbCriteria.Items.Add("Ledger Code");
                }
                else if (strcontrolname == "general")
                {
                    cmbCriteria.Items.Add("Ledger Name");
                    cmbCriteria.Items.Add("Ledger Code");
                }
                else if (strcontrolname == "reciept")
                {
                    cmbCriteria.Items.Add("Bill No");
                    cmbCriteria.Items.Add("Bill Date");
                    cmbCriteria.Items.Add("Counter No");
                    cmbCriteria.Items.Add("Cashier Name");
                    cmbCriteria.Items.Add("Customer Name");
                    cmbCriteria.Items.Add("Total Amount");
                    cmbCriteria.Items.Add("Barcode");
                    

                }
            }
            catch { }
        }

        public void DoWhenComingFromOtherForms()
        {
            //inRowIndex = rowIndex;
            //strStartText = prdName;
            //dtblPoruducts = dtbl;
            //if (dtbl != null && dtbl.Rows.Count > 0)
            //    if (dtblPoruducts.Rows[0]["productCode"].ToString() == "")
            //        dtblPoruducts.Rows.RemoveAt(0);
            base.ShowInTaskbar = false;
            base.ShowDialog();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------
        public void CheckWhenQuiting()
        {
            // Function to check while quiting the form
            // To return to the parent form
            // REturn datatable dtblProducts to parent form

            if (isFromPOSSales)
            {
                //ObjfrmPOSSales.Activate();
                ObjfrmPOSSales.Enabled = true;
                ObjfrmPOSSales.Activate();
                if (strcontrolname == "product")
                    ObjfrmPOSSales.FillrowAfterPickingProduct(productCodeToReturn);
                else if (strcontrolname == "customer")
                    ObjfrmPOSSales.FillrowAfterPickingCustomer(productCodeToReturn);
                else if (strcontrolname == "reciept")
                    ObjfrmPOSSales.FillrowAfterPickingReciept(productCodeToReturn);
                ObjfrmPOSSales.BringToFront();
            }
            else if (isFromPOSSales2)
            {
                //ObjfrmPOSSales.Activate();
                ObjfrmPOSSales2.Enabled = true;
                ObjfrmPOSSales2.Activate();
                if (strcontrolname == "product")
                    ObjfrmPOSSales2.FillrowAfterPickingProduct(productCodeToReturn);
                else if (strcontrolname == "customer")
                    ObjfrmPOSSales2.FillrowAfterPickingCustomer(productCodeToReturn);
                else if (strcontrolname == "reciept")
                    ObjfrmPOSSales2.FillrowAfterPickingReciept(productCodeToReturn);
                ObjfrmPOSSales2.BringToFront();
            }
            else if (isFromPOSSalesReturn)
            {
                ObjfrmPOSSalesReturn.Enabled = true;
                ObjfrmPOSSalesReturn.Activate();
                if (strcontrolname == "product")
                    ObjfrmPOSSalesReturn.FillrowAfterPickingProduct(productCodeToReturn);
                else if (strcontrolname == "customer")
                    ObjfrmPOSSalesReturn.FillrowAfterPickingCustomer(productCodeToReturn);
                ObjfrmPOSSalesReturn.BringToFront();
            }
            else if (isFromPOSReceiptVoucher)
            {
                ObjfrmPOSReceiptVoucher.Enabled = true;
                ObjfrmPOSReceiptVoucher.Activate();
                //if (strcontrolname == "product")
                //    ObjfrmPOSReceiptVoucher.FillrowAfterPickingProduct(productCodeToReturn);
                //else 
                if (strcontrolname == "customer")
                    ObjfrmPOSReceiptVoucher.FillrowAfterPickingCustomer(productCodeToReturn);
                ObjfrmPOSReceiptVoucher.BringToFront();
            }
            else if (isFromPOSPaymnetVoucher)
            {
                ObjfrmPOSPaymentVoucher.Enabled = true;
                ObjfrmPOSPaymentVoucher.Activate();
               
                if (strcontrolname == "party")
                    ObjfrmPOSPaymentVoucher.FillrowAfterPickingCustomer(productCodeToReturn);
                else if (strcontrolname == "general")
                    ObjfrmPOSPaymentVoucher.FillrowAfterPickingCustomer(productCodeToReturn);
                ObjfrmPOSPaymentVoucher.BringToFront();
            }
            else if (isFromBillPrint)
            {
                //ObjfrmPOSSales.Activate();
                ObjfrmBillPrint.Enabled = true;
                ObjfrmBillPrint.Activate();
                ObjfrmBillPrint.FillrowAfterPickingReciept(productCodeToReturn);
                ObjfrmBillPrint.BringToFront();
            }

        }
        #endregion
        //------------------------------------------------------------------------------------------------------------------------

        //public void FillGrid()
        //{
        //    // To fill product grid according to search criteria
        //    // if (!isFormLoad && !isDontExecute && dtblPoruducts != null)
        //    //{
        //    DataTable dtblPoruducts;
        //    dtblPoruducts = spProduct.ProductFillforLsit(PublicVariables._branchId);    //cretaed by sheena    
        //    DataTable dtblDatasource = new DataTable();
        //    dtblDatasource = dtblPoruducts.Copy();
        //    string strSearch = txtSearch.Text;
        //    string strGroupName = txtGroupName.Text;

        //    if (strSearch.Contains("'") || strGroupName.Contains("'"))
        //    {
        //        string[] str = strSearch.Split('\'');
        //        strSearch = "";
        //        for (int i = 0; i < str.Length; ++i)
        //        {
        //            if (i == 0)
        //                strSearch = strSearch + str[i];
        //            else
        //                strSearch = strSearch + "''" + str[i];
        //        }
        //    }
        //    if (strSearch.Contains("["))
        //    {
        //        strSearch = strSearch.Replace("[", "");
        //    }
        //    string strCondition = "";
        //    if (strSearch == "" && strGroupName == "")
        //    {
        //        strCondition = "productName like " + "'" + strSearch + "%'" + "or productCode like " + "'" + strSearch + "%'" + "";
        //    }
        //    else if (strSearch == "")
        //    {
        //        strCondition = "groupName like " + "'" + strGroupName + "%'" + "";
        //    }
        //    else if (strGroupName == "")
        //    {
        //        strCondition = "productName like " + "'" + strSearch + "%'" + "or productCode like " + "'" + strSearch + "%'" + "";
        //    }
        //    else
        //    {
        //        strCondition = "productName like " + "'" + strSearch + "%'" + "or productCode like " + "'" + strSearch + "%'" + "and groupName like " + "'" + strGroupName + "%'" + "";
        //    }

        //    dtblDatasource.DefaultView.RowFilter = strCondition;

        //    dgvProducts.DataSource = dtblDatasource;
        //    if (dgvProducts.Columns.Contains("prdCode"))
        //        dgvProducts.Columns["prdCode"].Visible = false;
        //    if (dgvProducts.CurrentRow != null)
        //        dgvProducts.CurrentRow.Selected = true;
        //    //}
        //}
        public void FillGrid()
        {
            try
            {
                string criteria = "";
                criteria = cmbCriteria.SelectedItem.ToString();
                string serachValue = txtSearch.Text;
                DataTable dtGrid = new DataTable();
                if (strcontrolname == "product")
                {
                    //DataTable dtCopy = new DataTable();
                    //dtCopy.Columns.Add(new DataColumn("Barcode", typeof(string)));
                    //dtCopy.Columns.Add(new DataColumn("Product Code", typeof(string)));
                    //dtCopy.Columns.Add(new DataColumn("Product Name", typeof(string)));
                    //dtCopy.Columns.Add(new DataColumn("Unit", typeof(string)));
                    //dtCopy.Columns.Add(new DataColumn("Sales Price", typeof(decimal)));
                    ////dtCopy.Columns.Add(new DataColumn("Including Tax Rate", typeof(decimal)));

                    dtGrid = spProduct.POSProductSearchForSales(criteria, serachValue, PublicVariables._branchId);    //cretaed by sheena 


                    //foreach (DataRow row in dtGrid.Rows)
                    //{
                    //    row["salesPrice"] = Math.Round(decimal.Parse(row["salesPrice"].ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    //    //row["TaxInclude"] = Math.Round(decimal.Parse(row["TaxInclude"].ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);

                    //    dtCopy.Rows.Add(row.ItemArray);
                    //}
                  
                    //dgvProducts.DataSource = dtCopy;
                    dgvProducts.DataSource = dtGrid;
                }
                else if (strcontrolname == "customer")
                {
                    dtGrid = spProduct.AccountLedgerGetDetails(criteria, serachValue, PublicVariables._branchId);                   
                    dgvProducts.DataSource = dtGrid;
                    dgvProducts.Columns[0].Visible = false;
                }
                else if (strcontrolname == "party")
                {
                    dtGrid = spProduct.AccountLedgerGetPartyDetails(criteria, serachValue, PublicVariables._branchId);
                    dgvProducts.DataSource = dtGrid;
                    dgvProducts.Columns[0].Visible = false;
                }
                else if (strcontrolname == "reciept")
                {
                    dtGrid = spProduct.POSSalesRecieptCopy(criteria, serachValue, PublicVariables._branchId, DateTime.Parse(dtpFromDate.Text), DateTime.Parse(dtpToDate.Text),PublicVariables._counterId, PublicVariables._userGroup);
                    dgvProducts.DataSource = dtGrid;


                    foreach (DataGridViewRow row in dgvProducts.Rows)
                    {

                        if (row.Cells["TotalAmount"].Value != null)
                        {

                            row.Cells["TotalAmount"].Value = Math.Round(decimal.Parse(row.Cells["TotalAmount"].Value.ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        }
                    }
                    dgvProducts.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                }
                else if (strcontrolname == "general")
                {
                    dtGrid = spProduct.AccountLedgerGetDiectExpenseList(criteria, serachValue, PublicVariables._branchId);
                    dgvProducts.DataSource = dtGrid;
                    dgvProducts.Columns[0].Visible = false;
                }
               
            }
            catch { }
            
        }
        public void CallFromPOSSales(frmPOSSales frm,string controlname)
        {
            isFromPOSSales = true;
            ObjfrmPOSSales = frm;
            strcontrolname = controlname;
            DoWhenComingFromOtherForms();
        }
        public void CallFromBillPrint(frmBillPrint frm, string controlname)
        {
            isFromBillPrint = true;
            ObjfrmBillPrint = frm;
            strcontrolname = controlname;
            DoWhenComingFromOtherForms();
        }
        public void CallFromPOSSales2(frmPOSSales2 frm, string controlname)
        {
            isFromPOSSales2 = true;
            ObjfrmPOSSales2 = frm;
            strcontrolname = controlname;
            DoWhenComingFromOtherForms();
        }
        public void CallFromPOSSalesReturn(frmPOSSalesReturn frm, string controlname)
        {
            isFromPOSSalesReturn = true;
            ObjfrmPOSSalesReturn = frm;
            strcontrolname = controlname;
            DoWhenComingFromOtherForms();
        }
        public void CallFromReceiptVoucher(frmPOSReceiptVoucher frm, string controlname)
        {
            isFromPOSReceiptVoucher = true;
            ObjfrmPOSReceiptVoucher = frm;
            strcontrolname = controlname;
            DoWhenComingFromOtherForms();
        }
        public void CallFromPaymentVoucher(frmPOSPaymentVoucher frm, string controlname)
        {
            isFromPOSPaymnetVoucher = true;
            ObjfrmPOSPaymentVoucher = frm;
            strcontrolname = controlname;
            DoWhenComingFromOtherForms();
        }
        #endregion
        /******************************************************************************************************************
         *                                      EVENTS
         *****************************************************************************************************************/
        #region EVENTS
        private void frmProductPopUp_Load(object sender, EventArgs e)
        {
            
            this.Dock = DockStyle.Fill;
            try
            {
                this.dtpFromDate.Value = DateTime.Now;
                this.dtpToDate.Value = DateTime.Now;
             
               isFormLoad = true;
                txtSearch.Text = strStartText;
                FillSearchCombo();
                cmbCriteria.SelectedIndex = 0;
           
                isFormLoad = false;
                FillGrid();
               
                if (dgvProducts.Rows.Count > 0)
                {
                    if (strcontrolname == "product")
                    {
                        pnlDates.Visible = false;
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Barcode"];
                    }
                    else if (strcontrolname == "customer")
                    {
                        pnlDates.Visible = false;
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Ledger Code"];
                    }
                    else if (strcontrolname == "party")
                    {
                        pnlDates.Visible = false;
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Ledger Code"];
                    }
                    else if (strcontrolname == "reciept")
                    {
                        pnlDates.Visible = true;
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Bill No"];
                    }
                    else if (strcontrolname == "general")
                    {
                        pnlDates.Visible = false;
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Ledger Code"];
                    }
                    dgvProducts.Rows[0].Selected = true;
                    dgvProducts.Focus();
                }
                txtSearch.Focus();


            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG41:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
        private void frmProductPopUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CheckWhenQuiting();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
        string productCodeToReturn = "";
        private void dgvProducts_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvProducts.CurrentRow != null)
                    {
                        if (strcontrolname == "product")
                        {
                            string barcode = dgvProducts.CurrentRow.Cells["Barcode"].Value == null ? string.Empty : dgvProducts.CurrentRow.Cells["Barcode"].Value.ToString();
                            if (barcode == "")
                                productCodeToReturn = dgvProducts.CurrentRow.Cells["Product Code"].Value.ToString();
                            else
                                productCodeToReturn = barcode;
                        }
                        else if (strcontrolname == "customer")
                        {
                            productCodeToReturn = dgvProducts.CurrentRow.Cells["ledgerId"].Value.ToString();                            

                        }
                        else if (strcontrolname == "party")
                        {
                            productCodeToReturn = dgvProducts.CurrentRow.Cells["ledgerId"].Value.ToString();
                        }
                        else if (strcontrolname == "general")
                        {
                            productCodeToReturn = dgvProducts.CurrentRow.Cells["ledgerId"].Value.ToString();
                        }
                        else if (strcontrolname == "reciept")
                        {
                            productCodeToReturn = dgvProducts.CurrentRow.Cells["Bill No"].Value.ToString();


                        }
                        this.Close();


                    }
                }
                else
                {
                    // Search gs = new Search();
                    //gs.MultiFocus(e, txtSearch);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG14:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG17:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isFormLoad)
                {
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG18:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FillGrid();
                //if (dgvProducts.CurrentRow != null)
                //    dgvProducts.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG39:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void btnReset_Click(object sender, EventArgs e)
        {
            frmProductPopUp_Load(sender, e);
        }
        #endregion

        private void dgvProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {

                if (strcontrolname == "product")
                {
                    string barcode = dgvProducts.CurrentRow.Cells["Barcode"].Value == null ? string.Empty : dgvProducts.CurrentRow.Cells["Barcode"].Value.ToString();
                    if (barcode == "")
                        productCodeToReturn = dgvProducts.CurrentRow.Cells["ProductCode"].Value.ToString();
                    else
                        productCodeToReturn = barcode;
                }
                else if (strcontrolname == "customer")
                {
                    productCodeToReturn = dgvProducts.CurrentRow.Cells["ledgerId"].Value.ToString();
                    
                }
                else if (strcontrolname == "party")
                {
                    productCodeToReturn = dgvProducts.CurrentRow.Cells["ledgerId"].Value.ToString();

                }
                else if (strcontrolname == "general")
                {
                    productCodeToReturn = dgvProducts.CurrentRow.Cells["ledgerId"].Value.ToString();

                }
                else if (strcontrolname == "reciept")
                {
                    productCodeToReturn = dgvProducts.CurrentRow.Cells["Bill No"].Value.ToString();

                }
              
                this.Close();

            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                if (dgvProducts.Rows.Count > 0)
                {
                    dgvProducts.Focus();
                    if (strcontrolname == "product")
                    {
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Barcode"];
                    }
                    else if (strcontrolname == "customer")
                    {
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Ledger Code"];
                    }
                    else if (strcontrolname == "reciept")
                    {
                        dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells["Bill No"];
                    }
                    dgvProducts.Rows[0].Selected = true;
                }
        }

        private void txtGroupName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FillGrid();
                if (dgvProducts.CurrentRow != null)
                    dgvProducts.Focus();



            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG39B:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                  string criteria = "";
                criteria = cmbCriteria.SelectedItem.ToString();
                if (strcontrolname == "reciept")
                {
                    if (criteria == "Bill Date")
                    {
                        txtSearch.Enabled = false;
                    }
                    else
                        txtSearch.Enabled = true; ;
                }
                else
                {
                    FillGrid();
                }
                txtSearch.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG39B:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          
        }
    

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)//added by Nishana on 29-04-2025
        {
            FillGrid();
        }
        //--------------
    }
}
