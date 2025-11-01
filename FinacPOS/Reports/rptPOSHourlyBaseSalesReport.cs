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
    public partial class rptPOSHourlyBaseSalesReport : Form
    {
        public rptPOSHourlyBaseSalesReport()
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
        private void print()
        {
            try
            {
                DataTable dtblCompanyDetails = new DataTable();
                DataTable dtblReport = new DataTable();
                BranchSP SpBranch = new BranchSP();
                frmReportViewer frmreportviewobj = new frmReportViewer();
                string strReportName = "Hourly Sales Report";
                string strDatePeriod = "";

                strDatePeriod = dtpFrom.Text.ToString() + " to " + dtpTo.Text.ToString();
                dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);

                dtblReport = objPOSReportSP.POSHourlySalesCount(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text));
                if (dtblReport.Rows.Count > 0)
                    frmreportviewobj.POSHourlySalesReportPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }

        private void rptPOSHourlyBaseSalesReport_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;

            bwrkControlSettings.RunWorkerAsync();
            frmCompanyProgress.ShowInTaskbar = false;
            frmCompanyProgress.ShowFromReport();
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
