using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    public partial class rptTenderTypeWiseSalesReport : Form
    {
        public rptTenderTypeWiseSalesReport()
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
        private void rptTenderTypeWiseSalesReport_Load(object sender, EventArgs e)
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
            //cmbReportType.SelectedIndex = 0;
            cmbReportName.SelectedIndex = 0;
            cmbExport.SelectedIndex = 0;
          
            chkSelectAll.Checked = false; 
        }

        #endregion

        private void rptTenderTypeWiseSalesReport_KeyDown(object sender, KeyEventArgs e)
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
        
        private void fillFilterListCounterOrCashier()
        {
            if (cmbReportName.Text == "Counter Wise")
            {
                DataTable dtbl = new DataTable();
                if (chkListCashierOrConter.Items.Count > 0)
                {
                    chkListCashierOrConter.DataSource = null;
                    chkListCashierOrConter.Items.Clear();
                }
                dtbl = objPOSReportSP.POSActiveCounterViewAll();
                // Set up the data source
                chkListCashierOrConter.DataSource = dtbl;

                // Set the DisplayMember to specify which property should be displayed
                chkListCashierOrConter.DisplayMember = "counterName";

                // Set the ValueMember to specify which property should be used as the value
                chkListCashierOrConter.ValueMember = "counterId";
                for (int i = 0; i < chkListCashierOrConter.Items.Count; i++)
                {
                    if (chkCatSelectAll.Checked == true)
                    {
                        chkListCashierOrConter.SetItemChecked(i, true);
                    }
                    else
                    {
                        chkListCashierOrConter.SetItemChecked(i, false);
                    }
                }
            }
            else
            {
                DataTable dtbl = new DataTable();

                if (chkListCashierOrConter.Items.Count > 0)
                {
                    chkListCashierOrConter.DataSource = null;
                    chkListCashierOrConter.Items.Clear();
                }


                dtbl = objPOSReportSP.POSActiveUserViewAll();
                // Set up the data source
                chkListCashierOrConter.DataSource = dtbl;

                // Set the DisplayMember to specify which property should be displayed
                chkListCashierOrConter.DisplayMember = "EmpName";

                // Set the ValueMember to specify which property should be used as the value
                chkListCashierOrConter.ValueMember = "userId";
                //if (dtbl.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtbl.Rows.Count; i++)
                //    {
                //        chkListFilterCashier.Items.Add(dtbl.Rows[i][0].ToString() + "-" + dtbl.Rows[i][1].ToString());
                //    }
                //}
                for (int i = 0; i < chkListCashierOrConter.Items.Count; i++)
                {
                    if (chkSelectAll.Checked == true)
                    {
                        chkListCashierOrConter.SetItemChecked(i, true);
                    }
                    else
                    {
                        chkListCashierOrConter.SetItemChecked(i, false);
                    }
                }
            }
        }
        private string GetCheckedPaymentValues()
        {
            // If nothing is checked, return empty parentheses
            if (chkListPayment.CheckedItems.Count == 0)
                return "";

            // Build the comma-separated string with single quotes
            string result = string.Join(",",
                chkListPayment.CheckedItems.Cast<object>()
                .Select(item => $"'{item.ToString()}'"));

            // Wrap in parentheses for SQL IN clause
            return $"{result}";
        }
        private string chkListCashierString()
        {
            //var statList = "(";
            //for (var item = 0; item < chkListFilterMain.CheckedItems.Count; item++)
            //    statList += "'" + chkListFilterMain.CheckedItems[item] + "',";
            //statList = statList.Substring(0, statList.Length - 1) + ")";




            string ItemValue = ""; int columnIndex = 0; // Change this to the index of the desired column
            foreach (object checkedItem in chkListCashierOrConter.CheckedItems)
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
            string cashierIds = "", payments = "";
            DataTable dtblCompanyDetails = new DataTable();
            DataTable dtblReport = new DataTable();
            BranchSP SpBranch = new BranchSP();
         
            string strReportName = "";
            string strDatePeriod = "",strProfitType="";
            payments = GetCheckedPaymentValues();
            cashierIds = chkListCashierString();
            strDatePeriod = dtpFrom.Text.ToString() + " to " + dtpTo.Text.ToString();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
            dtblCompanyDetails.Columns.Add("company_logo");
            dtblCompanyDetails.Columns.Add("companyheader_logo");
            dtblCompanyDetails.Columns.Add("companyfooter_logo");

            string image_path = "", image_headerpath = "", imagefooterpath = "";

            if (!Convert.IsDBNull(dtblCompanyDetails.Rows[0]["logo"]))
            {
                byte[] bytes = (byte[])dtblCompanyDetails.Rows[0]["logo"];
                try
                {
                    Image img = Image.FromStream(new MemoryStream(bytes));
                    string folderPath = @"Images";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fullpath = Path.GetFullPath(folderPath);
                    string filename = "CompanyLogo.bmp";
                    image_path = Path.Combine(fullpath, filename);
                    img.Save(image_path, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            if (!Convert.IsDBNull(FinanceSettingsInfo._companyHeader))
            {
                byte[] bytes = (byte[])FinanceSettingsInfo._companyHeader;
                try
                {
                    Image imgh = Image.FromStream(new MemoryStream(bytes));
                    string folderPath = @"Images";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fullpath = Path.GetFullPath(folderPath);
                    string filename = "CompanyHeader.bmp";
                    image_headerpath = Path.Combine(fullpath, filename);
                    imgh.Save(image_headerpath, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            if (!Convert.IsDBNull(FinanceSettingsInfo._companyFooter))
            {
                byte[] bytes = (byte[])FinanceSettingsInfo._companyFooter;
                try
                {
                    Image imgf = Image.FromStream(new MemoryStream(bytes));
                    string folderPath = @"Images";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string fullpath = Path.GetFullPath(folderPath);
                    string filename = "CompanyFooter.bmp";
                    imagefooterpath = Path.Combine(fullpath, filename);
                    imgf.Save(imagefooterpath, System.Drawing.Imaging.ImageFormat.Bmp);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }

            dtblCompanyDetails.Rows[0]["company_logo"] = image_path;
            dtblCompanyDetails.Rows[0]["companyheader_logo"] = image_headerpath;
            dtblCompanyDetails.Rows[0]["companyfooter_logo"] = imagefooterpath;

            string reportnamepart = "POS TENDERTYPE WISE SALES REPORT ";
            string strMessage = "";
            if (cmbReportName.Text == "Cashier Wise")
            {
                strMessage = "Please Select Cashier";
            }
            else
                strMessage = "Please Select Counter";
            if (cashierIds == "")
            {
                MessageBox.Show(strMessage);
            }
            else if (payments == "")
            {
                MessageBox.Show("Please Select Payment");
            }
            else
            {
                if (rbtnBillDate.Checked == true)
                {
                    strReportName = reportnamepart + " (Bill Date)";
                    dtblReport = objPOSReportSP.POSTenderTypeWiseSalesReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "BILLDATE");

                }
                else if (rbtnSessionDate.Checked == true)
                {
                    strReportName = reportnamepart + " (Session Date)";
                    dtblReport = objPOSReportSP.POSTenderTypeWiseSalesReport(Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text), "SESSIONDATE");
                }


            }
            if (dtblReport.Rows.Count > 0)
            {
               
                DataView dv1 = new DataView(dtblReport);
                if (cmbReportName.Text == "Cashier Wise")
                {
                    if (cashierIds != "")
                    {
                        cashierIds = "userId IN (" + cashierIds + ")";
                    }
                }
                else
                {
                    //if (cashierIds != "")
                    //    cashierIds = cashierIds + " And counterId IN (" + cashierIds + ")";
                    //else
                        cashierIds = "counterId IN (" + cashierIds + ")";
                }
                if(payments != "")
                {
                    payments = cashierIds + " And  TenderType IN (" + payments + ")";
                }


                    if (cashierIds.Trim() != "")
                    dv1.RowFilter = payments;

                DataTable dtFinal = dv1.ToTable();

                if (dtFinal.Rows.Count > 0)
                {
                    foreach (DataRow row in dtFinal.Rows)
                    {
                        row["totalQty"] = Math.Round(decimal.Parse(row["totalQty"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

                        row["subTotalAmount"] = Math.Round(decimal.Parse(row["subTotalAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["billDiscAmount"] = Math.Round(decimal.Parse(row["billDiscAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["taxableAmount"] = Math.Round(decimal.Parse(row["taxableAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["totalTaxAmount"] = Math.Round(decimal.Parse(row["totalTaxAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["totalAmount"] = Math.Round(decimal.Parse(row["totalAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["paidAmount"] = Math.Round(decimal.Parse(row["paidAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["balanceAmount"] = Math.Round(decimal.Parse(row["balanceAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["creditCardAmount"] = Math.Round(decimal.Parse(row["creditCardAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["UPIAmount"] = Math.Round(decimal.Parse(row["UPIAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["creditAmount"] = Math.Round(decimal.Parse(row["creditAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["cashAmount"] = Math.Round(decimal.Parse(row["cashAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["creditNoteAmount"] = Math.Round(decimal.Parse(row["creditNoteAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        row["TenderAmount"] = Math.Round(decimal.Parse(row["TenderAmount"].ToString()), FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                    }
                }

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
                    DevPrint devObj = new DevPrint();
                    devObj.PrintPOSTenderTypeWiseSalesReport(dtblCompanyDetails, dtFinal, strReportName, strDatePeriod, cmbExport.Text);
                    
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
            for (int i = 0; i < chkListCashierOrConter.Items.Count; i++)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkListCashierOrConter.SetItemChecked(i, true);
                }
                else
                {
                    chkListCashierOrConter.SetItemChecked(i, false);
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
            for (int i = 0; i < chkListPayment.Items.Count; i++)
            {
                if (chkCatSelectAll.Checked == true)
                {
                    chkListPayment.SetItemChecked(i, true);
                }
                else
                {
                    chkListPayment.SetItemChecked(i, false);
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

        private void cmbReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillFilterListCounterOrCashier();
        }
    }
}
