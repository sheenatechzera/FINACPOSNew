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
    public partial class rptPOSProductSalesSummery : Form
    {
        public rptPOSProductSalesSummery()
        {
            InitializeComponent();
        }

        ProductSP spProduct = new ProductSP();
        DataTable dtblProdcucts;
        string strBarcode = "";
        string strItemCode = "";
        string strItemName = "";
        bool isdontExecuteTextchange = false;
        POSReportSP objPOSReportSP = new POSReportSP();
        frmProgress frmCompanyProgress = new frmProgress();

       #region Functions
        private void InitialSettings()
        {
            //dtpFrom.MinDate = PublicVariables._fromDate;
            //dtpFrom.MaxDate = PublicVariables._toDate;
            //dtpTo.MinDate = PublicVariables._fromDate;
            //dtpTo.MaxDate = PublicVariables._toDate;
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            rbtnSessionDate.Checked = true;
        }
      
        public void barcodeScanning()
        {
            DataTable dtbl = new DataTable();
            dtbl = spProduct.GetProductDetailsByBarcode(txtProductCode.Text.Trim());
            if (dtbl.Rows.Count > 0) //load details by Barcode
            {
                strBarcode = txtProductCode.Text.Trim();
                strItemCode = dtbl.Rows[0]["productCode"].ToString();
                strItemName = dtbl.Rows[0]["productName"].ToString();
                lblProduct.Text = strItemName;
            }
            else
            {
                dtbl = spProduct.GetProductDetailsByProductCode(txtProductCode.Text.Trim());
                if (dtbl.Rows.Count > 0) //load details by ProductCode
                {
                    strBarcode = txtProductCode.Text.Trim();
                    strItemCode = dtbl.Rows[0]["productCode"].ToString();
                    strItemName = dtbl.Rows[0]["productName"].ToString();
                    lblProduct.Text = strItemName;
                }
                else
                {
                    MessageBox.Show("Barcode not Found", "WARNING");
                    txtProductCode.Focus();
                    return;
                }
            }
          
        }
        public void FillProducts()
        {
            AutoCompleteStringCollection collectionProductName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection collectionProductCode = new AutoCompleteStringCollection();
            txtProductCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtProductCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dtblProdcucts = new TransactionsGeneralFill().ProductFillforLsit(PublicVariables._branchId,"");//, dgvProduct, dgvProduct.CurrentCell.RowIndex, true);
            foreach (DataRow dr in dtblProdcucts.Rows)
            {
                collectionProductName.Add(dr["productName"].ToString());
                collectionProductCode.Add(dr["productCode"].ToString());
            }
            txtProductCode.AutoCompleteCustomSource = collectionProductName;
            //-------------------------------------------------
            txtProductCode.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtProductCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtProductCode.AutoCompleteCustomSource = collectionProductCode;
        }
       
        public void FillProductAfterPickingProduct(string productCode, bool isFromProductCreation)
        {          

            if (productCode != null && productCode != "")
            {
                txtProductCode.Text = productCode;
                strItemCode = productCode; 
                barcodeScanning();
            }
        }

        private void clearform()
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            rbtnSessionDate.Checked = true;
            txtProductCode.Text = "";
            strBarcode = "";
            strItemCode = "";
            strItemName = "";
            isdontExecuteTextchange = false;
            lblProduct.Text = "";
        }    
       
        private void Print()
        {
            DataTable dtblCompanyDetails = new DataTable();
            DataTable dtblReport = new DataTable();
            BranchSP SpBranch = new BranchSP();
            frmReportViewer frmreportviewobj = new frmReportViewer();
            string strReportName = "";
            string strDatePeriod = "";

            strDatePeriod = dtpFrom.Text.ToString() + " to " + dtpTo.Text.ToString();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
            
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = "Product Sales Summery Report" + " (Bill Date)";

                    dtblReport = objPOSReportSP.POSProductSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE", strItemCode);
                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = "Product Sales Summery Report" + " (Session Date)";

                    dtblReport = objPOSReportSP.POSProductSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE", strItemCode);
                }

                frmreportviewobj.POSProductSalesSummeryPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod, strItemCode + " - " + strItemName);
        }
       #endregion

        #region events
        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmProductPopUpGrid frm = new frmProductPopUpGrid();
            // frm.MdiParent = MDIFinacAcount.MDIObj; this.Enabled = false;
            string strCriteria = "";

            if (txtProductCode.Focused)
            {
                strCriteria = txtProductCode.Text;
            }
            else
            {
                strCriteria = txtProductCode.Text;
            }
            strCriteria = txtProductCode.Text;
            frm.CallFromPOSProductSalesReport(this, strCriteria, dtblProdcucts);
        }
        private void rptPOSProductSalesSummery_Load(object sender, EventArgs e)
        {
            bwrkControlSettings.RunWorkerAsync();
            frmCompanyProgress.ShowInTaskbar = false;
            frmCompanyProgress.ShowFromReport();

            InitialSettings();
            FillProducts();

            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearform();
        }
        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                barcodeScanning();
            }
        }
        private void btnGo_Click(object sender, EventArgs e)
        {
            if (strItemCode == "")
            {
                MessageBox.Show("Please enter product code", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
  
            }
            else
                Print();
        }
        #endregion

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
              if (e.KeyCode == Keys.Enter)
                  barcodeScanning();
        }

        private void bwrkControlSettings_DoWork(object sender, DoWorkEventArgs e)
        {
            FinacFormControl objGeneral = new FinacFormControl();
            objGeneral.formSettings(this);
        }

        private void bwrkControlSettings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (frmCompanyProgress != null && frmCompanyProgress.Visible)
                frmCompanyProgress.Close();
        }
    }
}
