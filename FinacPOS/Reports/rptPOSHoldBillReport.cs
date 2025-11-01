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
    public partial class rptPOSHoldBillReport : Form
    {
        public rptPOSHoldBillReport()
        {
            InitializeComponent();
        }
        POSReportSP objPOSReportSP = new POSReportSP();
        frmReportViewer frmreportviewobj = new frmReportViewer();
        frmProgress frmCompanyProgress = new frmProgress();
        private void btnGo_Click(object sender, EventArgs e)
        {
            print();
        }
        string strFormat = ""; public DataTable dtblPublic;
        private void print()
        {
            try
            {
                DataTable dtblCompanyDetails = new DataTable();
                DataTable dtblReport = new DataTable();
                BranchSP SpBranch = new BranchSP();
                frmReportViewer frmreportviewobj = new frmReportViewer();
                string strReportName = "Hold Bill Report";
                string strDatePeriod = "";
                string reportnamepart = cmbReportName.Text;
                string strStatus = "All";
                dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);

                strDatePeriod = dtpFrom.Text.ToString() + " to " + dtpTo.Text.ToString();
                if (cmbStatus.Text == "Closed")
                    strStatus = "C";
                else if (cmbStatus.Text == "Pending")
                    strStatus = "P";
                else
                    strStatus = "All";
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = reportnamepart + " (Bill Date)";
                    if (reportnamepart == "Hold Bill Summary Report")
                    {
                        dtblReport = objPOSReportSP.POSHoldBillSummaryReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE", strStatus);
                        foreach (DataRow row in dtblReport.Rows)
                        {

                            //row["billdate"] = Convert.ToDateTime(row["billdate"]).ToString("yyyy-MM-dd") + " " + row["billtime"].ToString();
                            row["subTotalAmount"] = ((decimal)Math.Round(decimal.Parse(row["subTotalAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["billDiscAmount"] = ((decimal)Math.Round(decimal.Parse(row["billDiscAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["taxableAmount"] = ((decimal)Math.Round(decimal.Parse(row["taxableAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["totalTaxAmount"] = ((decimal)Math.Round(decimal.Parse(row["totalTaxAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["totalAmount"] = ((decimal)Math.Round(decimal.Parse(row["totalAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["paidAmount"] = ((decimal)Math.Round(decimal.Parse(row["paidAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["balanceAmount"] = ((decimal)Math.Round(decimal.Parse(row["balanceAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();

                        }

                    }
                    else if (reportnamepart == "Hold Bill Details Report")
                    {
                        dtblReport = objPOSReportSP.POSHoldBillDetailedReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE", strStatus);
                    }

                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = reportnamepart + " (Session Date)";
                    if (reportnamepart == "Hold Bill Summary Report")
                    {
                        dtblReport = objPOSReportSP.POSHoldBillSummaryReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE", cmbStatus.Text);
                        foreach (DataRow row in dtblReport.Rows)
                        {

                            //row["billdate"] = Convert.ToDateTime(row["billdate"]).ToString("yyyy-MM-dd") + " " + row["billtime"].ToString();
                            row["subTotalAmount"] = ((decimal)Math.Round(decimal.Parse(row["subTotalAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["billDiscAmount"] = ((decimal)Math.Round(decimal.Parse(row["billDiscAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["taxableAmount"] = ((decimal)Math.Round(decimal.Parse(row["taxableAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["totalTaxAmount"] = ((decimal)Math.Round(decimal.Parse(row["totalTaxAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["totalAmount"] = ((decimal)Math.Round(decimal.Parse(row["totalAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["paidAmount"] = ((decimal)Math.Round(decimal.Parse(row["paidAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();
                            row["balanceAmount"] = ((decimal)Math.Round(decimal.Parse(row["balanceAmount"].ToString()), FinanceSettingsInfo._roundDecimal)).ToString();

                        }

                    }
                    else if (reportnamepart == "Hold Bill Details Report")
                    {
                        dtblReport = objPOSReportSP.POSHoldBillDetailedReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE", cmbStatus.Text);
                    }
                }
                DevPrint dev = new DevPrint();
                if (dtblReport.Rows.Count > 0)
                {
                    strFormat = cmbExport.Text;
                    if (strFormat != "Print")
                    {
                        dtblPublic = dtblReport;
                        bwrk1.RunWorkerAsync();
                        frmCompanyProgress.ShowInTaskbar = false;
                        frmCompanyProgress.ShowFromReport();
                    }
             
                    else
                    {
                        if (reportnamepart == "Hold Bill Summary Report")
                            dev.POSHoldBillSummaryPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
                        else if (reportnamepart == "Hold Bill Details Report")
                            frmreportviewobj.POSHoldBillDetailedPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
                    }
                }
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clearform();
        }

        private void rptPOSHoldBillReport_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            InitialDateSettings();
            clearform();
            bwrkControlSettings.RunWorkerAsync();
            frmCompanyProgress.ShowInTaskbar = false;
            frmCompanyProgress.ShowFromReport();
        }
        private void clearform()
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            rbtnSessionDate.Checked = true;
            //cmbReportType.SelectedIndex = 0;
            cmbReportName.SelectedIndex = 0;
            cmbExport.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;

        }
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
                // objCommon.InitialSEttings(lblName, lblAdress, lblPhone, cmbBranch, lblBranch, this.Text + "Z", false, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("RBS1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    ex.ExportExcelDtbl(dtblPublic, "Hold Bill", 0, 0, strFormat == "Export to Excel" ? "Excel" : "Html", DateTime.Parse(dtpFrom.Text), DateTime.Parse(dtpTo.Text), "");//, );//"", "", strAmount);

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

            printDoc.DocumentName = "Hold Bill Report";
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
