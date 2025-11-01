using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    public partial class rptPOSBillWiseSalesReport : Form
    {
        public rptPOSBillWiseSalesReport()
        {
            InitializeComponent();
        }
        #region DECLARATIONS

        bool iswork = false;


        POSReportSP objPOSReportSP = new POSReportSP();
        TransactionsGeneralFill objTransactionsGeneralFill = new TransactionsGeneralFill();

        ComboValidation objComboValidation = new ComboValidation();
        ReportCommonFunctions objCommon = new ReportCommonFunctions();
        frmProgress frmCompanyProgress = new frmProgress();

        #endregion
        private void rptPOSBillWiseSalesReport_Load(object sender, EventArgs e)
        {
            try
            {
                bwrkControlSettings.RunWorkerAsync();
                frmCompanyProgress.ShowInTaskbar = false;
                frmCompanyProgress.ShowFromReport();

                InitialDateSettings();
                clearform();
                fillFilterListCAshier();
                fillFilterListCounter();
                
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("RBS6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        #region FUNCTIONS

        public void InitialDateSettings()
        {
            try
            {
                //dtpFrom.MinDate = PublicVariables._fromDate;
                //dtpFrom.MaxDate = PublicVariables._toDate;
                //dtpTo.MinDate = PublicVariables._fromDate;
                //dtpTo.MaxDate = PublicVariables._toDate;
                dtpFrom.Value = DateTime.Now;
                dtpTo.Value = DateTime.Now; 
                rbtnSessionDate.Checked = true;
                objCommon.InitialSEttings(lblName, lblAdress, lblPhone, cmbBranch, lblBranch, this.Text + "Z", false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("RBS1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void clearform()
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            rbtnSessionDate.Checked = true;
            //cmbReportType.SelectedIndex = 0;
            cmbReportName.SelectedIndex = 0;
            cmbExport.SelectedIndex = 0;
            cmbProfitPer.SelectedIndex = 0;
            chkSelectAll.Checked = false; 
        }

        #endregion

        private void rptPOSBillWiseSalesReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RBS7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void fillFilterListCAshier()
        {
            DataTable dtbl = new DataTable();

            if (chkListFilterCashier.Items.Count > 0)
            {
                chkListFilterCashier.DataSource = null;
                chkListFilterCashier.Items.Clear();
            }
      

            dtbl = objPOSReportSP.POSActiveUserViewAll();
            // Set up the data source
            chkListFilterCashier.DataSource = dtbl;

            // Set the DisplayMember to specify which property should be displayed
            chkListFilterCashier.DisplayMember = "EmpName";

            // Set the ValueMember to specify which property should be used as the value
            chkListFilterCashier.ValueMember = "userId";
            //if (dtbl.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtbl.Rows.Count; i++)
            //    {
            //        chkListFilterCashier.Items.Add(dtbl.Rows[i][0].ToString() + "-" + dtbl.Rows[i][1].ToString());
            //    }
            //}
            for (int i = 0; i < chkListFilterCashier.Items.Count; i++)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkListFilterCashier.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterCashier.SetItemChecked(i, false);
                }
            }
        }
        private void fillFilterListCounter()
        {
            DataTable dtbl = new DataTable();
            if (chkListFilterCounter.Items.Count > 0)
            {
                chkListFilterCounter.DataSource = null;
                chkListFilterCounter.Items.Clear();
            }
            dtbl = objPOSReportSP.POSActiveCounterViewAll();
            // Set up the data source
            chkListFilterCounter.DataSource = dtbl;

            // Set the DisplayMember to specify which property should be displayed
            chkListFilterCounter.DisplayMember = "counterName";

            // Set the ValueMember to specify which property should be used as the value
            chkListFilterCounter.ValueMember = "counterId";
            for (int i = 0; i < chkListFilterCounter.Items.Count; i++)
            {
                if (chkCatSelectAll.Checked == true)
                {
                    chkListFilterCounter.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterCounter.SetItemChecked(i, false);
                }
            }
        }
        private string chkListCounterString()
        {
            //var statList = "(";
            //for (var item = 0; item < chkListFilterGrp.CheckedItems.Count; item++)
            //    statList += "'" + chkListFilterGrp.CheckedItems[item] + "',";
            //statList = statList.Substring(0, statList.Length - 1) + ")";

            string ItemValue = "";
            //foreach (object item in chkListFilterCounter.CheckedItems)
            //{
            //   // string[] myStr = item.ToString().Split('-');
            //    ItemValue = ItemValue+ "'"   +item.ToString() + "',";

            //}
            ItemValue = "";
            int columnIndex = 0; // Change this to the index of the desired column
            // Iterate through each checked item in the CheckedListBox
            foreach (object checkedItem in chkListFilterCounter.CheckedItems)
            {
                if (checkedItem is DataRowView)
                {
                    // Cast the checkedItem to DataRowView
                    DataRowView rowView = (DataRowView)checkedItem;

                    // Get the DataRow associated with the DataRowView
                    DataRow row = rowView.Row;

                    // Assuming columnIndex is the index of the column whose data you want to retrieve
                    // Replace columnIndex with the actual index of the column
                   
                    string columnValue = row[0].ToString();

                    // Append the column value to the StringBuilder
                    ItemValue = ItemValue + "'" + columnValue.ToString() + "',";
                }
            }
            return ItemValue.TrimEnd(',');
           // return statList;
        }
        private string chkListCashierString()
        {
            //var statList = "(";
            //for (var item = 0; item < chkListFilterMain.CheckedItems.Count; item++)
            //    statList += "'" + chkListFilterMain.CheckedItems[item] + "',";
            //statList = statList.Substring(0, statList.Length - 1) + ")";




            string ItemValue = ""; int columnIndex = 0; // Change this to the index of the desired column
            foreach (object checkedItem in chkListFilterCashier.CheckedItems)
            {
                if (checkedItem is DataRowView)
                {
                    // Cast the checkedItem to DataRowView
                    DataRowView rowView = (DataRowView)checkedItem;

                    // Get the DataRow associated with the DataRowView
                    DataRow row = rowView.Row;

                    // Assuming columnIndex is the index of the column whose data you want to retrieve
                    // Replace columnIndex with the actual index of the column
                   
                    string columnValue = row[0].ToString();

                    // Append the column value to the StringBuilder
                    ItemValue = ItemValue + "'" + columnValue.ToString() + "',";
                }
            }
            return ItemValue.TrimEnd(',');
        }
        public DataTable dtblPublic;
        private void Print()
        {
            string cashierIds = "", counterIds = "";
            DataTable dtblCompanyDetails = new DataTable();
            DataTable dtblReport = new DataTable();
            BranchSP SpBranch = new BranchSP();
            frmReportViewer frmreportviewobj = new frmReportViewer();
            string strReportName = "";
            string strDatePeriod = "",strProfitType="";
            counterIds = chkListCounterString();
            cashierIds = chkListCashierString();
            strDatePeriod = dtpFrom.Text.ToString() + " to " + dtpTo.Text.ToString();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
            string reportnamepart = cmbReportName.Text.Trim();

            if (cashierIds == "")
            {
                MessageBox.Show("Please Select Cashier");
            }
            else if (counterIds == "")
            {
                MessageBox.Show("Please Select Counter");
            }
            else
            {
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = reportnamepart + " (Bill Date)";
                    if (reportnamepart == "Sales Summary Report")
                        dtblReport = objPOSReportSP.POSBillWiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");
                    else if (reportnamepart == "Sales Details Report")
                        dtblReport = objPOSReportSP.POSBillWiseSalesDetailedReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");
                    else if (reportnamepart == "Profit Summary Report")
                        dtblReport = objPOSReportSP.POSBillWiseProfitSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");
                    else if (reportnamepart == "Profit Details Report")
                        dtblReport = objPOSReportSP.POSBillWiseProfitDetailedReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");
                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = reportnamepart + " (Session Date)";
                    if (reportnamepart == "Sales Summary Report")
                        dtblReport = objPOSReportSP.POSBillWiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
                    else if (reportnamepart == "Sales Details Report")
                        dtblReport = objPOSReportSP.POSBillWiseSalesDetailedReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
                    else if (reportnamepart == "Profit Summary Report")
                        dtblReport = objPOSReportSP.POSBillWiseProfitSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
                    else if (reportnamepart == "Profit Details Report")
                        dtblReport = objPOSReportSP.POSBillWiseProfitDetailedReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
             
                }


            }
            if (dtblReport.Rows.Count > 0)
            {
               
                DataView dv1 = new DataView(dtblReport);
                if (cashierIds != "")
                {
                    cashierIds = "userId IN (" + cashierIds + ")";                 
                }
                if (counterIds != "")
                {
                    if (cashierIds != "")
                        cashierIds = cashierIds + " And counterId IN (" + counterIds + ")";
                    else
                        cashierIds = "counterId IN (" + counterIds + ")";

                }             
                if (cashierIds.Trim() != "")
                    dv1.RowFilter = cashierIds;

                DataTable dtFinal = dv1.ToTable();

                strFormat = cmbExport.Text;
              
                if (strFormat != "Print")
                {
                    dtblPublic = dtFinal;
                    bwrk1.RunWorkerAsync();
                    frmCompanyProgress.ShowInTaskbar = false;
                    frmCompanyProgress.ShowFromReport();
                }
                else
                {
                    if (reportnamepart == "Sales Summary Report")
                        frmreportviewobj.POSBillSalesSummaryReportPrinting(dtFinal, dtblCompanyDetails, strReportName, strDatePeriod);
                    else if (reportnamepart == "Sales Details Report")
                        frmreportviewobj.POSBillSalesDetailsReportPrinting(dtFinal, dtblCompanyDetails, strReportName, strDatePeriod);
                    else if (reportnamepart == "Profit Summary Report")
                    {
                        if (cmbProfitPer.Text == "Profit Markup")
                        {
                            strProfitType = "ProfitMarkup";
                        }
                        else if (cmbProfitPer.Text == "Profit Margin")
                        {
                            strProfitType = "ProfitMargin";
                        }
                        frmreportviewobj.POSBillProfitSummaryReportPrinting(dtFinal, dtblCompanyDetails, strReportName, strDatePeriod, strProfitType);
                    }
                    else if (reportnamepart == "Profit Details Report")
                    {
                        if (cmbProfitPer.Text == "Profit Markup")
                        {
                            strProfitType = "ProfitMarkup";
                        }
                        else if (cmbProfitPer.Text == "Profit Margin")
                        {
                            strProfitType = "ProfitMargin";
                        }
                        frmreportviewobj.POSBillProfitDetailsReportPrinting(dtFinal, dtblCompanyDetails, strReportName, strDatePeriod, strProfitType);
                    }
                }


            }
            else 
                MessageBox.Show("Data Not Found");


        }
        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbReportType.Text.Trim() != "")
            //{
            //    fillFilterListCounter();
            //}
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListFilterCashier.Items.Count; i++)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkListFilterCashier.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterCashier.SetItemChecked(i, false);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearform();
        }
        string strFormat = "";
        private void btnGo_Click(object sender, EventArgs e)
        {

            Print();
        }

        private void chkCatSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListFilterCounter.Items.Count; i++)
            {
                if (chkCatSelectAll.Checked == true)
                {
                    chkListFilterCounter.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterCounter.SetItemChecked(i, false);
                }
            }
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

        private void bwrk1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ExportOrPrint();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSQ16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bwrk1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (frmCompanyProgress != null && frmCompanyProgress.Visible)
                    frmCompanyProgress.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSQ17:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        DataGridViewPrinter MyDataGridViewPrinter;
        private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }
        public void ExportOrPrint()
        {
            try
            {
                //string strAmount = lblTotal.Text + lblSum.Text;
                if (strFormat == "Print")
                {
                    if (SetupThePrinting())
                    {

                        PrintPreviewDialog MyPrintPreviewDialog = new PrintPreviewDialog();
                        MyPrintPreviewDialog.Document = printDoc;
                        MyPrintPreviewDialog.ShowDialog();

                    }
                }
                else if (strFormat == "Export to Excel" || strFormat == "Export to Html")
                {

                    ExportNew ex = new ExportNew();
                    ex.ExportExcelDtbl(dtblPublic, "Exchanged Item", 0, 0, strFormat == "Export to Excel" ? "Excel" : "Html", DateTime.Parse(dtpFrom.Text), DateTime.Parse(dtpTo.Text), "");//, );//"", "", strAmount);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("RSQ15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool SetupThePrinting()
        {
            PrintDialog MyPrintDialog = new PrintDialog();
            MyPrintDialog.AllowCurrentPage = false;
            MyPrintDialog.AllowPrintToFile = false;
            MyPrintDialog.AllowSelection = false;
            MyPrintDialog.AllowSomePages = false;
            MyPrintDialog.PrintToFile = false;
            MyPrintDialog.ShowHelp = false;
            MyPrintDialog.ShowNetwork = false;

            if (MyPrintDialog.ShowDialog() != DialogResult.OK)
                return false;

            printDoc.DocumentName = "Exchanged Item Report";
            printDoc.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDoc.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDoc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);




           // string strAmount = lblTotal.Text + lblSum.Text;
            string strDate = "(" + dtpFrom.Text + " - " + dtpTo.Text + ")";
          //  MyDataGridViewPrinter = new DataGridViewPrinter(dtblPublic, printDoc, true, true, "Exchanged Item Report", lblName.Text, lblAdress.Text, lblPhone.Text, new Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true, strDate, "", "", strAmount, "");

            return true;
        }

      
      
    }
}
