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
    public partial class rptPOSOfferReport : Form
    {
        public rptPOSOfferReport()
        {
            InitializeComponent();
        }
        #region DECLARATIONS

        bool iswork = false;
        OfferRateSP spOfferRAte = new OfferRateSP();

        POSReportSP objPOSReportSP = new POSReportSP();
        TransactionsGeneralFill objTransactionsGeneralFill = new TransactionsGeneralFill();

        ComboValidation objComboValidation = new ComboValidation();
        ReportCommonFunctions objCommon = new ReportCommonFunctions();
        frmProgress frmCompanyProgress = new frmProgress();

        #endregion
        private void rptPOSOfferReport_Load(object sender, EventArgs e)
        {
            try
            {
                bwrkControlSettings.RunWorkerAsync();
                frmCompanyProgress.ShowInTaskbar = false;
                frmCompanyProgress.ShowFromReport();

                InitialDateSettings();
               
                FillOfferRate();
                fillFilterListMainGroup();
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
            cmbOffer.SelectedIndex = 0;
            cmbExport.SelectedIndex = 0;
            chkSelectAll.Checked = false; 
        }

        #endregion
        public void FillOfferRate()
        {
            // To fill CostCentre combo
            try
            {
                cmbOffer.DataSource = null;
              
                DataTable dtbl = new DataTable();
                dtbl = spOfferRAte.OfferRateGetAllByBranch();
                cmbOffer.DataSource = dtbl;
                cmbOffer.DisplayMember = "offerName";
                cmbOffer.ValueMember = "offerId";
                cmbOffer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RV10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void rptPOSOfferReport_KeyDown(object sender, KeyEventArgs e)
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
        private void fillFilterListMainGroup()
        {
            DataTable dtbl = new DataTable();
            chkListFilterMain.Items.Clear();

            dtbl = objPOSReportSP.POSGetMainGroup();   
            // Set up the data source
            chkListFilterMain.DataSource = dtbl;

            // Set the DisplayMember to specify which property should be displayed
            chkListFilterMain.DisplayMember = "groupName";

            // Set the ValueMember to specify which property should be used as the value
            chkListFilterMain.ValueMember = "groupCode";
            //if (dtbl.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtbl.Rows.Count; i++)
            //    {
            //        chkListFilterCashier.Items.Add(dtbl.Rows[i][0].ToString() + "-" + dtbl.Rows[i][1].ToString());
            //    }
            //}
            for (int i = 0; i < chkListFilterMain.Items.Count; i++)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkListFilterMain.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterMain.SetItemChecked(i, false);
                }
            }




            //DataTable dtbl = new DataTable();
            //chkListFilterMain.Items.Clear();

            //dtbl = objPOSReportSP.POSGetMainGroup();   
            //if (dtbl.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtbl.Rows.Count; i++)
            //    {
            //        chkListFilterMain.Items.Add(dtbl.Rows[i][0].ToString() + "-" + dtbl.Rows[i][1].ToString());
            //    }
            //}
            //for (int i = 0; i < chkListFilterMain.Items.Count; i++)
            //{
            //    if (chkSelectAll.Checked == true)
            //    {
            //        chkListFilterMain.SetItemChecked(i, true);
            //    }
            //    else
            //    {
            //        chkListFilterMain.SetItemChecked(i, false);
            //    }
            //}
        }
        private void fillFilterListGroups()
        {
            DataTable dtbl = new DataTable();
            if (chkListFilterGrp.Items.Count > 0)
            {
                chkListFilterGrp.DataSource = null;
                chkListFilterGrp.Items.Clear();
            }

            dtbl = objPOSReportSP.POSGetProductGroupsByCategory(cmbReportType.Text.Trim());
            // Set up the data source
            chkListFilterGrp.DataSource = dtbl;

            // Set the DisplayMember to specify which property should be displayed
            chkListFilterGrp.DisplayMember = "groupName";

            // Set the ValueMember to specify which property should be used as the value
            chkListFilterGrp.ValueMember = "groupId";
            //if (dtbl.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtbl.Rows.Count; i++)
            //    {
            //        chkListFilterCashier.Items.Add(dtbl.Rows[i][0].ToString() + "-" + dtbl.Rows[i][1].ToString());
            //    }
            //}
            for (int i = 0; i < chkListFilterGrp.Items.Count; i++)
            {
                if (chkCatSelectAll.Checked == true)
                {
                    chkListFilterGrp.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterGrp.SetItemChecked(i, false);
                }
            }




            //DataTable dtbl = new DataTable();
            //chkListFilterGrp.Items.Clear();
            //dtbl = objPOSReportSP.POSGetProductGroupsByCategory(cmbReportType.Text.Trim());
            //if (dtbl.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtbl.Rows.Count; i++)
            //    {
            //        chkListFilterGrp.Items.Add(dtbl.Rows[i][0].ToString() + "-" + dtbl.Rows[i][1].ToString());
            //    }
            //}
            //for (int i = 0; i < chkListFilterGrp.Items.Count; i++)
            //{
            //    if (chkCatSelectAll.Checked == true)
            //    {
            //        chkListFilterGrp.SetItemChecked(i, true);
            //    }
            //    else
            //    {
            //        chkListFilterGrp.SetItemChecked(i, false);
            //    }
            //}
        }
        private string chkListGroupString()
        {
            //var statList = "(";
            //for (var item = 0; item < chkListFilterGrp.CheckedItems.Count; item++)
            //    statList += "'" + chkListFilterGrp.CheckedItems[item] + "',";
            //statList = statList.Substring(0, statList.Length - 1) + ")";

            //string ItemValue = "";
            //foreach (object item in chkListFilterGrp.CheckedItems)
            //{
            //    string[] myStr = item.ToString().Split('-');
            //    ItemValue = ItemValue + myStr[0] + ',';

            //}
            //return ItemValue.TrimEnd(',');
           // return statList;


            string ItemValue = "";           
            // Iterate through each checked item in the CheckedListBox
            foreach (object checkedItem in chkListFilterGrp.CheckedItems)
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
        private string chkListMainGroupString()
        {
            //var statList = "(";
            //for (var item = 0; item < chkListFilterMain.CheckedItems.Count; item++)
            //    statList += "'" + chkListFilterMain.CheckedItems[item] + "',";
            //statList = statList.Substring(0, statList.Length - 1) + ")";




            //string ItemValue = "";
            //foreach (object item in chkListFilterMain.CheckedItems)
            //{

            //    // DataRowView row = item as DataRowView;

            //    // ItemValue = row["Name"].ToString();
            //    string[] myStr = item.ToString().Split('-');
            //    ItemValue = ItemValue + myStr[0] + ',';

            //}
            //return ItemValue.TrimEnd(',');


            string ItemValue = "";
            // Iterate through each checked item in the CheckedListBox
            foreach (object checkedItem in chkListFilterMain.CheckedItems)
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
            DataTable dtblCompanyDetails = new DataTable();
            DataTable dtblReport = new DataTable();
            BranchSP SpBranch = new BranchSP();
            frmReportViewer frmreportviewobj = new frmReportViewer();
            string strReportName = "";
            string strDatePeriod = "";
            string groupIds = chkListGroupString();
            string maingroupIds = chkListMainGroupString();
            strDatePeriod = dtpFrom.Text.ToString() + " to " + dtpTo.Text.ToString();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
            string reportnamepart = cmbOffer.Text.Trim();
            if (groupIds == "")
            {
                MessageBox.Show("Please Select Group");
            }
            else if (maingroupIds == "")
            {
                MessageBox.Show("Please Select Main Group");
            }
            else
            {
                reportnamepart = "POS Product Offer Report";
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = reportnamepart + " - " + cmbReportType.Text.Trim() + " (Bill Date)";
                    dtblReport = spOfferRAte.POSOfferRateProductReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE", cmbReportType.SelectedItem.ToString(), cmbOffer.SelectedValue.ToString());
                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = reportnamepart + " - " + cmbReportType.Text.Trim() + " (Session Date)";
                    dtblReport = spOfferRAte.POSOfferRateProductReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE", cmbReportType.SelectedItem.ToString(), cmbOffer.SelectedValue.ToString());
                }
                if (dtblReport.Rows.Count > 0)
                {
                    DataView dv1 = new DataView(dtblReport);
                    if (maingroupIds != "")
                    {
                        maingroupIds = "MaingrpCode IN (" + maingroupIds + ")";
                    }
                    if (groupIds != "")
                    {
                        if (maingroupIds != "")
                            maingroupIds = maingroupIds + " And CatGroupId IN (" + groupIds + ")";
                        else
                            maingroupIds = "CatGroupId IN (" + groupIds + ")";

                    }
                    if (maingroupIds.Trim() != "")
                        dv1.RowFilter = maingroupIds;

                    DataTable dtFinal = dv1.ToTable();









                    //                DataTable dtFilter = new DataTable();
                    //                dtFilter = dtblReport.Clone();
                    //                foreach (object item in chkListFilterMain.CheckedItems)
                    //                {
                    //                    string[] myStr = item.ToString().Split('-');
                    //                    DataView dv = new DataView(dtblReport);
                    //                    dv.RowFilter = "MaingrpCode= " + myStr[0] + "";

                    //                    foreach (DataRowView rowView in dv)
                    //                    {
                    //                        DataRow row = rowView.Row;
                    //                        dtFilter.ImportRow(row);
                    //                    }
                    //                }
                    //                DataView dv1 = new DataView(dtFilter);
                    //                if (groupIds != "")
                    //                {
                    //                    dv1.RowFilter = "CatGroupId IN (" + groupIds + ") ";
                    //                    dtblReport = dv1.ToTable();
                    //                }
                    //                else
                    //                    dtblReport = dtFilter.Copy();
                    //                DataTable dtFinal = new DataTable();
                    //                if (reportnamepart == "Category wise sales report")
                    //                {
                    //                    dtFinal = dtblReport.AsEnumerable()
                    //        .GroupBy(r => new
                    //        {
                    //            CatGroupId = r.Field<string>("CatGroupId"),
                    //            CatGroupName = r.Field<string>("CatGroupName"),
                    //            MaingrpCode = r.Field<string>("MaingrpCode"),
                    //            MainGrpName = r.Field<string>("MainGrpName"),
                    //            category = r.Field<string>("category")
                    //        })
                    //        .Select(x =>
                    //        {
                    //            var row = dtblReport.NewRow();
                    //            row["CatGroupId"] = x.Key.CatGroupId;
                    //            row["CatGroupName"] = x.Key.CatGroupName;
                    //            row["MaingrpCode"] = x.Key.MaingrpCode;
                    //            row["MainGrpName"] = x.Key.MainGrpName;
                    //            row["category"] = x.Key.category;
                    //            row["TotQty"] = x.Sum(r => Convert.ToDecimal(r["TotQty"]));
                    //            row["TotSales"] = x.Sum(r => Convert.ToDecimal(r["TotSales"]));
                    //            row["TotTax"] = x.Sum(r => Convert.ToDecimal(r["TotTax"]));
                    //            row["Total"] = x.Sum(r => Convert.ToDecimal(r["Total"]));
                    //            return row;
                    //        }).CopyToDataTable();
                    //                }
                    //                else if (reportnamepart == "Category wise sales profit report")
                    //                {
                    //                    dtFinal = dtblReport.AsEnumerable()
                    //.GroupBy(r => new
                    //{
                    //    CatGroupId = r.Field<string>("CatGroupId"),
                    //    CatGroupName = r.Field<string>("CatGroupName"),
                    //    MaingrpCode = r.Field<string>("MaingrpCode"),
                    //    MainGrpName = r.Field<string>("MainGrpName"),
                    //    category = r.Field<string>("category")
                    //})
                    //.Select(x =>
                    //{
                    //    var row = dtblReport.NewRow();
                    //    row["CatGroupId"] = x.Key.CatGroupId;
                    //    row["CatGroupName"] = x.Key.CatGroupName;
                    //    row["MaingrpCode"] = x.Key.MaingrpCode;
                    //    row["MainGrpName"] = x.Key.MainGrpName;
                    //    row["category"] = x.Key.category;
                    //    row["TotQty"] = x.Sum(r => Convert.ToDecimal(r["TotQty"]));
                    //    row["TotSales"] = x.Sum(r => Convert.ToDecimal(r["TotSales"]));
                    //    row["TotTax"] = x.Sum(r => Convert.ToDecimal(r["TotTax"]));
                    //    row["Total"] = x.Sum(r => Convert.ToDecimal(r["Total"]));
                    //    row["TotCost"] = x.Sum(r => Convert.ToDecimal(r["TotCost"]));
                    //    row["Profit"] = x.Sum(r => Convert.ToDecimal(r["Profit"]));
                    //    row["ProfitPer"] = x.Sum(r => Convert.ToDecimal(r["ProfitPer"]));
                    //    return row;
                    //}).CopyToDataTable();
                    //                }
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
                        //if (reportnamepart == "Category wise sales report")
                        //    frmreportviewobj.POSProductReportPrinting(dtFinal, dtblCompanyDetails, strReportName, strDatePeriod);
                        //else if (reportnamepart == "Product wise sales report")
                        //{
                        frmreportviewobj.POSOfferReportPRoductwisePrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
                        //}
                        //else if (reportnamepart == "Product wise sales profit report")
                        //{
                        //    frmreportviewobj.POSProductReportPRoductwiseProfitPrinting(dtblReport, dtblCompanyDetails, strReportName, strDatePeriod);
                        //}
                        //else  if (reportnamepart == "Category wise sales profit report")
                        //    frmreportviewobj.POSProductProfitReportPrinting(dtFinal, dtblCompanyDetails, strReportName, strDatePeriod);
                    }
                }
            }
        }
        
        private void cmbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReportType.Text.Trim() != "")
            {
                fillFilterListGroups();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListFilterMain.Items.Count; i++)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkListFilterMain.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterMain.SetItemChecked(i, false);
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

        private void chkCatSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkListFilterGrp.Items.Count; i++)
            {
                if (chkCatSelectAll.Checked == true)
                {
                    chkListFilterGrp.SetItemChecked(i, true);
                }
                else
                {
                    chkListFilterGrp.SetItemChecked(i, false);
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
                    ex.ExportExcelDtbl(dtblPublic, "Exchaged Item", 0, 0, strFormat == "Export to Excel" ? "Excel" : "Html", DateTime.Parse(dtpFrom.Text), DateTime.Parse(dtpTo.Text), "");//, );//"", "", strAmount);

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

            printDoc.DocumentName = "POS Product Report";
            printDoc.PrinterSettings = MyPrintDialog.PrinterSettings;
            printDoc.DefaultPageSettings = MyPrintDialog.PrinterSettings.DefaultPageSettings;
            printDoc.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);




            // string strAmount = lblTotal.Text + lblSum.Text;
            string strDate = "(" + dtpFrom.Text + " - " + dtpTo.Text + ")";
            //  MyDataGridViewPrinter = new DataGridViewPrinter(dgvReport, printDoc, true, true, "Exchanged Item Report", lblName.Text, lblAdress.Text, lblPhone.Text, new Font("Arial", 18, FontStyle.Bold, GraphicsUnit.Point), Color.Black, true, strDate, "", "", strAmount, "");

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

        private void cmbOffer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbl = spOfferRAte.OfferRateViewByOfferId(cmbOffer.SelectedValue.ToString());
                if (dtbl.Rows.Count > 0)
                {
                    dtpFrom.Text = dtbl.Rows[0]["fromDate"].ToString();
                    dtpTo.Text = dtbl.Rows[0]["toDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OR17:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
