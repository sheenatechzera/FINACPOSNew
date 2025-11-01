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
    public partial class rptPOSDailySales : Form
    {
        public rptPOSDailySales()
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
        private void rptPOSDailySales_Load(object sender, EventArgs e)
        {
            try
            {
                bwrkControlSettings.RunWorkerAsync();
                frmCompanyProgress.ShowInTaskbar = false;
                frmCompanyProgress.ShowFromReport();

                InitialDateSettings();
                clearform();

                
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
            cmbReportType.SelectedIndex = 0;
            cmbExport.SelectedIndex = 0;
            chkSelectAll.Checked = false; 
        }

        #endregion

        private void rptPOSDailySales_KeyDown(object sender, KeyEventArgs e)
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
        private void fillFilterList()
        {
            DataTable dtbl = new DataTable();
            if (chkListFilter.Items.Count > 0)
            {
                chkListFilter.DataSource = null;
                chkListFilter.Items.Clear();
            }
            if (cmbReportType.Text.Trim() == "Cashier wise Sales Summery" || cmbReportType.Text.Trim()=="Cashier wise bill summery")
            {
                dtbl = objPOSReportSP.POSActiveUserViewAll();
                chkListFilter.DataSource = dtbl;

                // Set the DisplayMember to specify which property should be displayed
                chkListFilter.DisplayMember = "EmpName";

                // Set the ValueMember to specify which property should be used as the value
                chkListFilter.ValueMember = "userId";
            }
            else if (cmbReportType.Text.Trim() == "Counter wise Sales Summery")
            {
                dtbl = objPOSReportSP.POSActiveCounterViewAll();
                chkListFilter.DataSource = dtbl;

                // Set the DisplayMember to specify which property should be displayed
                chkListFilter.DisplayMember = "counterName";

                // Set the ValueMember to specify which property should be used as the value
                chkListFilter.ValueMember = "counterId";
            }
          
            //if (dtbl.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtbl.Rows.Count; i++)
            //    {
            //        chkListFilter.Items.Add(dtbl.Rows[i][0].ToString() + " - " + dtbl.Rows[i][1].ToString());
            //    }
            //}
           
            //if (dtbl.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtbl.Rows.Count; i++)
            //    {
            //        chkListFilterCashier.Items.Add(dtbl.Rows[i][0].ToString() + "-" + dtbl.Rows[i][1].ToString());
            //    }
            //}
            for (int i = 0; i < chkListFilter.Items.Count; i++)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkListFilter.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilter.SetItemChecked(i, false);
                }
            }
        }
        private string chkListGroupString()
        {
            string ItemValue = "";
            // Iterate through each checked item in the CheckedListBox
            foreach (object checkedItem in chkListFilter.CheckedItems)
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
        DataTable dtblPublic = new DataTable();
        private void Print()
        {
            DataTable dtblCompanyDetails = new DataTable();
            DataTable dtblReport = new DataTable();
            BranchSP SpBranch = new BranchSP();
            frmReportViewer frmreportviewobj = new frmReportViewer();
            string strReportName = "";
            string strDatePeriod = "";
            string groupIds = chkListGroupString();
            strDatePeriod = dtpFrom.Text.ToString() + " to " + dtpTo.Text.ToString();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);


            if (cmbReportType.Text.Trim() == "Cashier wise Sales Summery")
            {
                if (groupIds == "")
                {
                    MessageBox.Show("Please Select Group");
                }
                else
                {
                    if (rbtnBillDate.Checked == true)
                    {
                        strReportName = cmbReportType.Text.Trim() + " (Bill Date)";

                        dtblReport = objPOSReportSP.POSCashierwiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");
                    }
                    else if (rbtnSessionDate.Checked == true)
                    {
                        strReportName = cmbReportType.Text.Trim() + " (Session Date)";

                        dtblReport = objPOSReportSP.POSCashierwiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
                    }
                    if (dtblReport.Rows.Count > 0)
                    {
                        DataView dv1 = new DataView(dtblReport);
                        if (groupIds != "")
                        {
                            groupIds = "userId IN (" + groupIds + ")";
                        }

                        if (groupIds.Trim() != "")
                            dv1.RowFilter = groupIds;

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
                        frmreportviewobj.POSCashierwiseSalesSummeryPrinting(dtFinal, dtblCompanyDetails, strReportName, strDatePeriod);
                    }
                }

            }
            else if (cmbReportType.Text.Trim() == "Counter wise Sales Summery")
            {
                if (groupIds == "")
                {
                    MessageBox.Show("Please Select Group");
                }
                else
                {
                    if (rbtnBillDate.Checked == true)
                    {
                        strReportName = cmbReportType.Text.Trim() + " (Bill Date)";

                        dtblReport = objPOSReportSP.POSCashierwiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");
                    }
                    else if (rbtnSessionDate.Checked == true)
                    {
                        strReportName = cmbReportType.Text.Trim() + " (Session Date)";

                        dtblReport = objPOSReportSP.POSCashierwiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
                    }
                    if (dtblReport.Rows.Count > 0)
                    {
                        DataView dv1 = new DataView(dtblReport);
                        if (groupIds != "")
                        {
                            groupIds = "counterId IN (" + groupIds + ")";
                        }

                        if (groupIds.Trim() != "")
                            dv1.RowFilter = groupIds;

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
                        frmreportviewobj.POSCounterwiseSalesSummeryPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
                    }
                }
            }
            else if (cmbReportType.Text.Trim() == "Tender Type Summery")
            {
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = cmbReportType.Text.Trim() + " (Bill Date)";

                    dtblReport = objPOSReportSP.POSTenderTypeWiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");
                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = cmbReportType.Text.Trim() + " (Session Date)";

                    dtblReport = objPOSReportSP.POSTenderTypeWiseSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
                }
                strFormat = cmbExport.Text;
                if (strFormat != "Print")
                {
                    dtblPublic = dtblReport;
                    bwrk1.RunWorkerAsync();
                    frmCompanyProgress.ShowInTaskbar = false;
                    frmCompanyProgress.ShowFromReport();
                }
                else
                frmreportviewobj.POSTenderTypeSummeryPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
            }
            else if (cmbReportType.Text.Trim() == "Daily Sales Summery")
            {
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = cmbReportType.Text.Trim() + " (Bill Date)";

                    dtblReport = objPOSReportSP.POSSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE", "SALES");
                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = cmbReportType.Text.Trim() + " (Session Date)";

                    dtblReport = objPOSReportSP.POSSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE", "SALES");
                }
                strFormat = cmbExport.Text;
                if (strFormat != "Print")
                {
                    dtblPublic = dtblReport;
                    bwrk1.RunWorkerAsync();
                    frmCompanyProgress.ShowInTaskbar = false;
                    frmCompanyProgress.ShowFromReport();
                }
                else
                frmreportviewobj.POSDailySalesSummeryPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
            }
            else if (cmbReportType.Text.Trim() == "Daily wise Bill Count")
            {
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = cmbReportType.Text.Trim() + " (Bill Date)";

                    dtblReport = objPOSReportSP.POSSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE", "SALESBILL");
                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = cmbReportType.Text.Trim() + " (Session Date)";

                    dtblReport = objPOSReportSP.POSSalesSummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE", "SALESBILL");
                }
                strFormat = cmbExport.Text;
                if (strFormat != "Print")
                {
                    dtblPublic = dtblReport;
                    bwrk1.RunWorkerAsync();
                    frmCompanyProgress.ShowInTaskbar = false;
                    frmCompanyProgress.ShowFromReport();
                }
                else
                frmreportviewobj.POSDailySalesBilCountSummeryPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
            }
            else if (cmbReportType.Text.Trim() == "Cashier wise bill summery")
            {
                if (groupIds == "")
                {
                    MessageBox.Show("Please Select Group");
                }
                else
                {
                    if (rbtnBillDate.Checked == true)
                    {
                        strReportName = cmbReportType.Text.Trim() + " (Bill Date)";

                        dtblReport = objPOSReportSP.POSCashierwisebillsummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE", "SALESBILL");
                    }
                    else if (rbtnSessionDate.Checked == true)
                    {
                        strReportName = cmbReportType.Text.Trim() + " (Session Date)";

                        dtblReport = objPOSReportSP.POSCashierwisebillsummery(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE", "SALESBILL");
                    }
                    if (dtblReport.Rows.Count > 0)
                    {
                        DataView dv1 = new DataView(dtblReport);
                        if (groupIds != "")
                        {
                            groupIds = "userId IN (" + groupIds + ")";
                        }

                        if (groupIds.Trim() != "")
                            dv1.RowFilter = groupIds;

                        DataTable dtFinal = dv1.ToTable();
                        strFormat = cmbExport.Text;
                        if (strFormat != "Print")
                        {
                            dtblPublic = dtblReport;
                            bwrk1.RunWorkerAsync();
                            frmCompanyProgress.ShowInTaskbar = false;
                            frmCompanyProgress.ShowFromReport();
                        }
                        else
                            frmreportviewobj.POSCashierwisebillsummeryPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
                    }

                }
            }
           

        }
        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportType.Text.Trim() != "")
            {
                fillFilterList();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListFilter.Items.Count; i++)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkListFilter.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilter.SetItemChecked(i, false);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearform();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Print();
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
        string strFormat = "";
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
                    ex.ExportExcelDtbl(dtblPublic, "POS Daily Sales", 0, 0, strFormat == "Export to Excel" ? "Excel" : "Html", DateTime.Parse(dtpFrom.Text), DateTime.Parse(dtpTo.Text), "");//, );//"", "", strAmount);

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

            printDoc.DocumentName = "POS Daily Sales";
            printDoc.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDoc.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDoc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);




            // string strAmount = lblTotal.Text + lblSum.Text;
            string strDate = "(" + dtpFrom.Text + " - " + dtpTo.Text + ")";
            //  MyDataGridViewPrinter = new DataGridViewPrinter(dtblPublic, printDoc, true, true, "Exchanged Item Report", lblName.Text, lblAdress.Text, lblPhone.Text, new Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true, strDate, "", "", strAmount, "");

            return true;
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
        private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            bool more = MyDataGridViewPrinter.DrawDataGridView(e.Graphics);
            if (more == true)
                e.HasMorePages = true;
        }
    }
}
