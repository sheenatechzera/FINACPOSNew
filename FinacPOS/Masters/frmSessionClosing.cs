using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using FinacPOS.Masters;
using DevExpress.Xpo;

namespace FinacPOS
{
    public partial class frmSessionClosing : Form
    {
        public frmSessionClosing()
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
        SessionManagementSP sessionSp = new SessionManagementSP();
        POSCounterInfo counterInfo = new POSCounterInfo();
        //varis form thermal print
        DataTable dtblCompanyDetailsThermal;

        public static frmSessionClosing Instance;



        private void frmSessionClosing_Load(object sender, EventArgs e)
        {
            POSCounterSP counterSP = new POSCounterSP();
            counterInfo = counterSP.POSCounterViewbyCounterId(PublicVariables._counterId);
            Instance = this; // Store the reference when the form loads

        }
        public void DoWhenComingFromPOSMDI(string strSessionNo, DateTime dtSessionDate)
        {
            lblSessionDate.Text = dtSessionDate.ToString("dd-MMM-yyyy");
            lblSessionNO.Text = strSessionNo;
            lblUser.Text = PublicVariables._EmpName;
            lblCounter.Text = PublicVariables._counterName;
 
            loadSessionClosingData(strSessionNo, dtSessionDate, PublicVariables._counterId, PublicVariables._currentUserId);
             base.ShowDialog();
        }

        public void loadSessionClosingData(string strSessionNo, DateTime dtSessionDate,string counterid,string userid)
        {
            List<SessionManagementInfo> sessionInfoList = sessionSp.GetPOSSessionCloseDetails(dtSessionDate, counterid, userid, strSessionNo);

            foreach (SessionManagementInfo sessionInfo in sessionInfoList)
            {
                switch (sessionInfo.Particular)
                {
                    case "CASHSALES":
                        lblCashSales.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        lblCashSalesSummery.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CARDSALES":
                        lblCardSales.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "UPISALES":
                        lblUPISales.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CREDITSALES":
                        lblCreditSales.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "TOTALSALES":
                        lblTotalSales.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        lblTotalSalesSummery.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CASHRETURN":
                        lblCashRefund.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        lblCashRefundSummery.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CNRETURN":
                        lblCreditNote.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CREDITRETURN":
                        lblcreditReturn.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "TOTALRETURN":
                        lbltotalReturn.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        lblTotalReturnSummery.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CASHRECEIPT":
                        lblcashreceipt.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "UPIRECEIPT":
                        lblupireceipt.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CARDRECEIPT":
                        lblcardreceipt.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "TOTALRECEIPT":
                        lblTotalReceipt.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CASHPAYMENT":
                        lblcashpayment.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "UPIPAYMENT":
                        lblupipayment.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "CARDPAYMENT":
                        lblcardpayment.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "TOTALPAYMENT":
                        lblTotalPayment.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                    case "OB":
                        lblOB.Text = sessionInfo.AMOUNT.ToString(FinanceSettingsInfo._roundDecimalPart);
                        break;
                }
                
            }
            lblNetSales.Text = (Convert.ToDecimal(lblTotalSales.Text) - Convert.ToDecimal(lbltotalReturn.Text)).ToString(FinanceSettingsInfo._roundDecimalPart);

            lbltotalcashreceipt.Text = lblcashreceipt.Text;
            lbltotalcashpayment.Text = lblcashpayment.Text;

            lblCashBalance.Text = ((Convert.ToDecimal(lblOB.Text) + Convert.ToDecimal(lblCashSales.Text) + Convert.ToDecimal(lbltotalcashreceipt.Text)) -
             (Convert.ToDecimal(lblCashRefund.Text) + Convert.ToDecimal(lbltotalcashpayment.Text))).ToString(FinanceSettingsInfo._roundDecimalPart);
            lblSessionDate.Text = dtSessionDate.ToString("dd-MMM-yyyy");


        }
        private void btnClose_Click(object sender, EventArgs e)
        {

            //--------Company Details Datatable--------------

            //BranchSP SpBranch = new BranchSP();
            //dtblCompanyDetailsThermal = SpBranch.BranchViewByBranchId(PublicVariables._branchId);

            FillDatatatablesforDevPrint();
            //int pageWidth;
            //int PageHight = 0;
            //pageSetupDialog1.PageSettings = printDocumentClosing.DefaultPageSettings;
            //pageSetupDialog1.AllowOrientation = false;
            //PageHight = 800; //set rowcount
            //printDocumentClosing.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Cs", 300, PageHight);
            //pageWidth = printDocumentClosing.DefaultPageSettings.PaperSize.Width - printDocumentClosing.DefaultPageSettings.Margins.Left - printDocumentClosing.DefaultPageSettings.Margins.Right;
            //PrintDocument document = new PrintDocument();
            //printPreviewDialog1.Document = printDocumentClosing;
            ////printPreviewDialog1.ShowDialog();
            //printDocumentClosing.Print();
    
            this.Close();
       }

        private void printDocumentClosing_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat SFCentre = new StringFormat();
            StringFormat SFRight = new StringFormat();
            SFCentre.Alignment = StringAlignment.Center;
            SFRight.FormatFlags = StringFormatFlags.DirectionRightToLeft;

            int j = 1;
            int lineGap = 0;
            //int RowHight = 290;
            Rectangle rect1 = new Rectangle(0, 0, 280, 20);

            byte[] logo = null;
            logo = (byte[])dtblCompanyDetailsThermal.Rows[0]["Logo"];
            MemoryStream ms = new MemoryStream(logo);
            Image im = Image.FromStream(ms);
            //

            Point p = new Point(100, 100);
            e.Graphics.DrawImage(im, 125, 10, 50, 50);

            //Header
            lineGap = lineGap + 70;
            e.Graphics.DrawString(dtblCompanyDetailsThermal.Rows[0]["BranchName"].ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);
            lineGap = lineGap + POSSettingsInfo._CompanyH;
            e.Graphics.DrawString(dtblCompanyDetailsThermal.Rows[0]["extra1"].ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);
            lineGap = lineGap + POSSettingsInfo._CompanyArabicH;

            rect1.Y = lineGap;
            rect1.Height = POSSettingsInfo._AddressH;
            e.Graphics.DrawString(dtblCompanyDetailsThermal.Rows[0]["Address"].ToString(), new Font("Arial", 8), Brushes.Black, rect1, SFCentre);
            lineGap = lineGap + POSSettingsInfo._PhoneH;
            string Contact = "Phone :" + dtblCompanyDetailsThermal.Rows[0]["PhoneNo"].ToString();
            e.Graphics.DrawString(Contact, new Font("Arial", 8), Brushes.Black, 150, lineGap, SFCentre);
            lineGap = lineGap + 20;

            lineGap = lineGap + 40;
            e.Graphics.DrawString("SESSION CLOSE", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);

            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString("Counter: " + lblCounter.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString("Session Date: " + lblSessionDate.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 150, lineGap);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("User: " + lblUser.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString("Session No: " + lblSessionNO.Text, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 150, lineGap);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Printing Time: " + DateTime.Today.ToString("dd-MMM-yyyy") + " " + DateTime.Now.ToLongTimeString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString("Cash Sales: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblCashSales.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Card Sales: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblCardSales.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("UPI Sales: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblUPISales.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Credit Sales: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblCreditSales.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString("Total Sales: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblTotalSales.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 30;
            e.Graphics.DrawString("Cash Return: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblCashRefund.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Credit Note: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblCreditNote.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Credit Return: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblcreditReturn.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString("Total Return: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lbltotalReturn.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 30;

            e.Graphics.DrawString("Total Sales: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblTotalSales.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Total Return: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString("-" + lbltotalReturn.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 10;
            e.Graphics.DrawString("Net Sales: ", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblNetSales.Text, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString("Cash Receipt: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblcashreceipt.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString(" UPI Receipt: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblUPISales.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Card Receipt: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblcardreceipt.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Total Receipt: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblTotalReceipt.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Cash Payment: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblcashpayment.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString(" UPI Payment: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblupipayment.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Card Payment: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblcardpayment.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Total Payment: ", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblTotalPayment.Text, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;

            lineGap = lineGap + 40;
            e.Graphics.DrawString("CASH SUMMERY", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);

            lineGap = lineGap + 20;
            e.Graphics.DrawString("Opening Balance: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblOB.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Cash Sales: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblCashSalesSummery.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Cash Return: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString("-" + lblCashRefundSummery.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Cash Receipt: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString("-" + lblTotalReceipt.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Cash Payment: ", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString("-" + lblcashpayment.Text, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString("Cash Balance: ", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(lblCashBalance.Text, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 50;
            e.Graphics.DrawString("Signature", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 170, lineGap);
       
        }

        private void frmSessionClosing_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManagementSP SPSessionManagement = new SessionManagementSP();
            SPSessionManagement.UpdateSessionClose(PublicVariables._counterId);
            MessageBox.Show("Session Closed Successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public  void FillDatatatablesforDevPrint()
        {
            //--------Company Details Datatable--------------
            DataTable dtblCompanyDetails = new DataTable();
            BranchSP SpBranch = new BranchSP();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
            POSSalesMasterSP salesmaster = new POSSalesMasterSP();
            dtblCompanyDetails.Columns.Add("companyheader_logo");
            //dtblCompanyDetails.Columns.Add("companyfooter_logo");

             string image_path = "";

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
                    string filename = PublicVariables._branchId + ".bmp";
                    image_path = Path.Combine(fullpath, filename);
                    img.Save(image_path, System.Drawing.Imaging.ImageFormat.Bmp);

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            //byte[] imageBytes = File.ReadAllBytes(image_path);
            dtblCompanyDetails.Rows[0]["companyheader_logo"] = image_path;

           


            //--------- Other Details Datatable---------------
            DataTable dtblOtherDetails = new DataTable();

            dtblOtherDetails.Columns.Add("CounterId");
            dtblOtherDetails.Columns.Add("SessionDate");
            dtblOtherDetails.Columns.Add("Username");
            dtblOtherDetails.Columns.Add("SessionNo");
            dtblOtherDetails.Columns.Add("PrintingTime");

            dtblOtherDetails.Columns.Add("CashSales");
            dtblOtherDetails.Columns.Add("CardSales");
            dtblOtherDetails.Columns.Add("UPISales");
            dtblOtherDetails.Columns.Add("CreditSales");
            dtblOtherDetails.Columns.Add("TotalSales");

            dtblOtherDetails.Columns.Add("CashReturn");
            dtblOtherDetails.Columns.Add("CreditNote");
            dtblOtherDetails.Columns.Add("CreditReturn");
            dtblOtherDetails.Columns.Add("TotalReturn");

            dtblOtherDetails.Columns.Add("TotalSales1");
            dtblOtherDetails.Columns.Add("TotalReturn1");
            dtblOtherDetails.Columns.Add("NetSales");


            dtblOtherDetails.Columns.Add("CashReceipt");
            dtblOtherDetails.Columns.Add("UPIReceipt");
            dtblOtherDetails.Columns.Add("CardReceipt");
            dtblOtherDetails.Columns.Add("TotalReceipt");


            dtblOtherDetails.Columns.Add("CashPayment");
            dtblOtherDetails.Columns.Add("UPIPayment");
            dtblOtherDetails.Columns.Add("CardPayment");
            dtblOtherDetails.Columns.Add("TotalPayment");

            dtblOtherDetails.Columns.Add("OpeningBal");
            dtblOtherDetails.Columns.Add("SumCashSales");
            dtblOtherDetails.Columns.Add("SumCashReturn");
            dtblOtherDetails.Columns.Add("TotalCashReceipt");
            dtblOtherDetails.Columns.Add("TotalCashPayment");
            dtblOtherDetails.Columns.Add("CashBalance");




            DataRow dRowDetails = dtblOtherDetails.NewRow();
            dRowDetails["CounterId"] = lblCounter.Text;
            dRowDetails["SessionDate"] = lblSessionDate.Text;
         

            dRowDetails["Username"] = lblUser.Text;
            dRowDetails["SessionNo"] = lblSessionNO.Text;
            dRowDetails["PrintingTime"] = DateTime.Today.ToString("dd-MMM-yyyy") + " " + DateTime.Now.ToLongTimeString();

            dRowDetails["CashSales"] = lblCashSales.Text;
            dRowDetails["CardSales"] = lblCardSales.Text;
            dRowDetails["UPISales"] = lblUPISales.Text;
            dRowDetails["CreditSales"] = lblCreditSales.Text;
            dRowDetails["TotalSales"] = lblTotalSales.Text;

            dRowDetails["CashReturn"] = lblCashRefund.Text;
            dRowDetails["CreditNote"] = lblCreditNote.Text;
            dRowDetails["CreditReturn"] = lblcreditReturn.Text;
            dRowDetails["TotalReturn"] = lbltotalReturn.Text;

            dRowDetails["TotalSales1"] = lblTotalSales.Text;
            dRowDetails["TotalReturn1"] = lbltotalReturn.Text;
            dRowDetails["NetSales"] = lblNetSales.Text;


            dRowDetails["CashReceipt"] = lblcashreceipt.Text;
            dRowDetails["UPIReceipt"] = lblupireceipt.Text;
            dRowDetails["CardReceipt"] = lblcardreceipt.Text;
            dRowDetails["TotalReceipt"] = lblTotalReceipt.Text;


            dRowDetails["CashPayment"] = lblcashpayment.Text;
            dRowDetails["UPIPayment"] = lblupipayment.Text;
            dRowDetails["CardPayment"] = lblcardpayment.Text;
            dRowDetails["TotalPayment"] = lblTotalPayment.Text;

            dRowDetails["OpeningBal"] = lblOB.Text;
            dRowDetails["SumCashSales"] = lblCashSalesSummery.Text;
            dRowDetails["TotalCashReceipt"] = lblcashreceipt.Text;
            dRowDetails["TotalCashPayment"] = lblcashpayment.Text;
            dRowDetails["SumCashReturn"] = "-" + lblCashRefundSummery.Text;
            
            dRowDetails["CashBalance"] = lblCashBalance.Text;



            dtblOtherDetails.Rows.Add(dRowDetails);

            DevPrint spPrint = new DevPrint();
            spPrint.PrintSessionClose(dtblCompanyDetails, dtblOtherDetails, counterInfo.DefaultPrinter,counterInfo.Directprint);


        }

     

    }
}
