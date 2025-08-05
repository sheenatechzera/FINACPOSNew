using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevExpress.CodeParser;


namespace FinacPOS
{
    partial class frmBillPrint : Form
    {
 
       public frmBillPrint()
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
        POSCounterSP counterSP = new POSCounterSP();
        DBClass DBClass = new DBClass();
        bool isFromPOSSales = false;
        bool isFromPOSSales2 = false;
        frmPOSSales ObjfrmPOSSales;
        frmPOSSales2 ObjfrmPOSSales2;
        bool isTrue = false;
        bool isClose = false;
        string _conditionFromSales = "";
        private void btnPrint_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Back)
                    SendKeys.Send("+{Tab}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

     
        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable dt = new DataTable();
                    string strQuery = "SELECT ledgerName,CustomerAddress,CustomerPhone,CustomerVATNo FROM tbl_POSSalesMaster(NOLOCK) WHERE invoiceNo='" + txtBillNo.Text.Trim() + "'";
                    dt = DBClass.GetDataTable(strQuery);
                    if (dt.Rows.Count > 0)
                    {
                        txtCustomerName.Text = dt.Rows[0]["ledgerName"].ToString();
                        txtAddress.Text = dt.Rows[0]["CustomerAddress"].ToString();
                        txtPhoneNo.Text = dt.Rows[0]["CustomerPhone"].ToString();
                        txtVATNo.Text = dt.Rows[0]["CustomerVATNo"].ToString();
                    }

                    txtBillNo.Focus();  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL3:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        protected override void OnShown(EventArgs e)
        {
            txtBillNo.Focus();
            base.OnShown(e);
        }
      
        public DataTable GetTaxSumforDuplicatePrint(DataTable dtblProductDetails)
        {
            DataTable dtbl = new DataTable();

            dtbl = new TaxMasterSP().TaxGetByCondition("", true, "Sales Invoice", "");

            dtbl.Columns.Add("taxableAmt", typeof(decimal));
            dtbl.Columns.Add("amt", typeof(decimal));

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                decimal dTotal = 0;
                decimal dTaxableTotal = 0;
                for (int p = 0; p < dtblProductDetails.Rows.Count; p++)
                {
                    if (dtblProductDetails.Rows[p]["taxId"].ToString() == dtbl.Rows[i]["taxId"].ToString())
                    {
                        decimal dcItemBillDisc = 0;
                        try { dcItemBillDisc = decimal.Parse(dtblProductDetails.Rows[p]["billDiscAmountperItem"].ToString()); }
                        catch { }
                        dTotal = dTotal + decimal.Parse(dtblProductDetails.Rows[p]["taxAmount"].ToString());
                        dTaxableTotal = dTaxableTotal + (decimal.Parse(dtblProductDetails.Rows[p]["netAmount"].ToString()) - dcItemBillDisc);
                    }
                }
                dTaxableTotal = Math.Round(dTaxableTotal, FinanceSettingsInfo._roundDecimal);
                dTotal = Math.Round(dTotal, FinanceSettingsInfo._roundDecimal);
                dtbl.Rows[i]["amt"] = dTotal.ToString();
                dtbl.Rows[i]["taxableAmt"] = dTaxableTotal.ToString();
            }

            return dtbl;
        }
        POSSalesMasterSP POSSalesMasterSP = new POSSalesMasterSP();
        POSReceiptMasterSP objPOSReceiptMasterSP = new POSReceiptMasterSP();
        POSPaymentMasterSP POSpaymentMasterSP = new POSPaymentMasterSP();
       
        public void FillDatatatablesforDevPrint(string strTenderPaid, string strTenderBalance, string strTenderCash, string strTenderCC, string strTenderUPI, bool isDuplicatePrint, string strDuplicateBillNo, string strHoldBillNo, string strTenderType)
        {
            DataTable dtblCompanyDetailsThermal = new DataTable();
            DataTable dtblGridDetailsThermal = new DataTable(); 
            DataTable dtblOtherDetailsThermal = new DataTable(); 
            DataTable dtblTaxDetailsThermal = new DataTable();
            DataTable dtblGridDetails = new DataTable();
            DataTable dtblOtherDetails = new DataTable();


         

            //--------Company Details Datatable--------------
            DataTable dtblCompanyDetails = new DataTable();
            BranchSP SpBranch = new BranchSP();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
            POSSalesMasterSP salesmaster = new POSSalesMasterSP();

            dtblCompanyDetails.Columns.Add("companyheader_logo");
            dtblCompanyDetails.Columns.Add("companyfooter_logo");

            string image_headerpath = "", imagefooterpath = "";

            if (!Convert.IsDBNull(SettingsInfo._companyHeader))
            {
                byte[] bytes = (byte[])SettingsInfo._companyHeader;
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


            if (!Convert.IsDBNull(SettingsInfo._companyFooter))
            {
                byte[] bytes = (byte[])SettingsInfo._companyFooter;
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

            dtblCompanyDetails.Rows[0]["companyheader_logo"] = image_headerpath;
            dtblCompanyDetails.Rows[0]["companyfooter_logo"] = imagefooterpath;

              //-------Grid details-------------------------
             
                dtblGridDetails.Columns.Add("Sl No");
                dtblGridDetails.Columns.Add("Barcode");
                dtblGridDetails.Columns.Add("ProductName");
                dtblGridDetails.Columns.Add("Tax%");
                dtblGridDetails.Columns.Add("Price");
                dtblGridDetails.Columns.Add("Qty");
                dtblGridDetails.Columns.Add("Gr.Value");
                dtblGridDetails.Columns.Add("Tax Amt");
                dtblGridDetails.Columns.Add("Disc Amt");
                dtblGridDetails.Columns.Add("NETVALUE");
                dtblGridDetails.Columns.Add("Total Amt");
                dtblGridDetails.Columns.Add("NameArabic");
                dtblGridDetails.Columns.Add("unitName");
                DataTable dtbl = new DataTable();
                dtbl = POSSalesMasterSP.GetPOSLastBillProductsforLastBillPrint(strDuplicateBillNo);

            if (dtbl.Rows.Count > 0)
            {
                //Added on 27/Apr/2025 varis
                int returnValue = 0;
                string strQuery = "UPDATE tbl_POSSalesMaster SET ledgerName=N'" + txtCustomerName.Text.ToString() + "',CustomerAddress=N'" + txtAddress.Text.ToString() + "',CustomerPhone=N'" + txtPhoneNo.Text.ToString() + "',CustomerVATNo=N'" + txtVATNo.Text.ToString() + "' WHERE invoiceNo='" + txtBillNo.Text + "'";
                returnValue = DBClass.ExecuteNonQuery(strQuery);
                //-------------------

                dtblTaxDetailsThermal = GetTaxSumforDuplicatePrint(dtbl);

                for (int i = 0; i < dtbl.Rows.Count; i++)
                {
                    DataRow dr = dtblGridDetails.NewRow();
                    dr["Sl No"] = dtbl.Rows[i]["LineNumber"].ToString();
                    dtblGridDetails.Rows.Add(dr);
                    dr["Barcode"] = dtbl.Rows[i]["barcode"].ToString();
                    dr["ProductName"] = dtbl.Rows[i]["productName"].ToString();
                    dr["Tax%"] = dtbl.Rows[i]["taxPer"].ToString();
                    dr["Price"] = Convert.ToDecimal(dtbl.Rows[i]["rate"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dr["Qty"] = Math.Round(Convert.ToDecimal(dtbl.Rows[i]["qty"]), 2);
                    dr["Gr.Value"] = Convert.ToDecimal(dtbl.Rows[i]["grossValue"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dr["Tax Amt"] = Convert.ToDecimal(dtbl.Rows[i]["taxAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dr["Disc Amt"] = Convert.ToDecimal(dtbl.Rows[i]["discAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dr["NETVALUE"] = Convert.ToDecimal(dtbl.Rows[i]["netAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dr["Total Amt"] = Convert.ToDecimal(dtbl.Rows[i]["Amount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dr["NameArabic"] = dtbl.Rows[i]["ArabicName"].ToString();
                    dr["unitName"] = dtbl.Rows[i]["unitName"].ToString();
                }

            }

            else
            {
                MessageBox.Show("Bill not found. Please verify the bill number and try again.");
                return;
            }
                //--------- Other Details Datatable---------------
                dtblOtherDetails.Columns.Add("PartyName");
                dtblOtherDetails.Columns.Add("PartyAddress");
                dtblOtherDetails.Columns.Add("phoneNo");
                dtblOtherDetails.Columns.Add("VatNo");
                dtblOtherDetails.Columns.Add("BillDate");
                dtblOtherDetails.Columns.Add("BillTime");
                dtblOtherDetails.Columns.Add("SessionDate");
                dtblOtherDetails.Columns.Add("SessionNo");
                dtblOtherDetails.Columns.Add("CounterId");
                dtblOtherDetails.Columns.Add("UserName");
                dtblOtherDetails.Columns.Add("InvoiceNo");
                dtblOtherDetails.Columns.Add("SubTotal");
                dtblOtherDetails.Columns.Add("BillDiscount");
                dtblOtherDetails.Columns.Add("TaxableAmount");
                dtblOtherDetails.Columns.Add("TaxAmount");
                dtblOtherDetails.Columns.Add("GrandTotal");
                dtblOtherDetails.Columns.Add("AmountInWords");
                dtblOtherDetails.Columns.Add("BillName");
                dtblOtherDetails.Columns.Add("QtyTotal");
                dtblOtherDetails.Columns.Add("TenderPaid");
                dtblOtherDetails.Columns.Add("TenderBalance");
                dtblOtherDetails.Columns.Add("TenderCash");
                dtblOtherDetails.Columns.Add("TenderCC");
                dtblOtherDetails.Columns.Add("TenderUPI");
                dtblOtherDetails.Columns.Add("TenderCashText");
                dtblOtherDetails.Columns.Add("TenderCCText");
                dtblOtherDetails.Columns.Add("TenderUPIText");
                dtblOtherDetails.Columns.Add("qrCode");
                dtblOtherDetails.Columns.Add("customerCode");
                dtblOtherDetails.Columns.Add("customerName");
                dtblOtherDetails.Columns.Add("isCredit");
                dtblOtherDetails.Columns.Add("prevBalance");
                dtblOtherDetails.Columns.Add("BillAmount");
                dtblOtherDetails.Columns.Add("totalBalance");
                dtblOtherDetails.Columns.Add("showCustBalance");
                dtblOtherDetails.Columns.Add("ReportName");
                dtblOtherDetails.Columns.Add("PartyArabic");
                dtblOtherDetails.Columns.Add("AmountInWordsArabic");
                dtblOtherDetails.Columns.Add("CustomerAddress");
                dtblOtherDetails.Columns.Add("CustomerPhone");
                dtblOtherDetails.Columns.Add("CustomerVatNo");

                dtbl = new DataTable();
                dtbl = POSSalesMasterSP.GetPOSBillDetailsforBillPrint(strDuplicateBillNo);

            if (dtbl.Rows.Count > 0)
            {
                DataRow dRowDetails = dtblOtherDetails.NewRow();
                dRowDetails["PartyName"] = dtbl.Rows[0]["customerName"].ToString();
                dRowDetails["PartyAddress"] = dtbl.Rows[0]["PartyAddress"].ToString();
                dRowDetails["phoneNo"] = dtbl.Rows[0]["phoneNo"].ToString();
                dRowDetails["VatNo"] = dtbl.Rows[0]["VatNo"].ToString();
                dRowDetails["BillDate"] = Convert.ToDateTime(dtbl.Rows[0]["billDate"]).ToString("dd/MMM/yyyy");
                dRowDetails["BillTime"] = dtbl.Rows[0]["billTime"].ToString();
                dRowDetails["SessionDate"] = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"]).ToString("dd/MMM/yyyy");
                dRowDetails["SessionNo"] = dtbl.Rows[0]["sessionNo"].ToString();
                dRowDetails["CounterId"] = dtbl.Rows[0]["counterId"].ToString();
                dRowDetails["UserName"] = dtbl.Rows[0]["userId"].ToString();
                dRowDetails["InvoiceNo"] = dtbl.Rows[0]["invoiceNo"].ToString();
                dRowDetails["SubTotal"] = Convert.ToDecimal(dtbl.Rows[0]["subTotalAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                dRowDetails["BillDiscount"] = Convert.ToDecimal(dtbl.Rows[0]["billDiscAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                dRowDetails["TaxableAmount"] = Convert.ToDecimal(dtbl.Rows[0]["taxableAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                dRowDetails["TaxAmount"] = Convert.ToDecimal(dtbl.Rows[0]["totalTaxAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                dRowDetails["GrandTotal"] = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                decimal grandTotal = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"].ToString());
                dRowDetails["AmountInWords"] = new NumToText().ConvertAmountToWordsForPrint(grandTotal, dtbl.Rows[0]["currencyId"].ToString()); //General    
                                                                                                                                                //---------------------
                                                                                                                                                //List<CurrencyArabicInfo> currencies = new List<CurrencyArabicInfo>();
                CurrencyArabicInfo currencies = new CurrencyArabicInfo(PublicVariables._currencyId);
                ToWord toWord = new ToWord(Convert.ToDecimal(grandTotal), currencies);

                dRowDetails["AmountInWordsArabic"] = toWord.ConvertToArabic();
                //   dRowDetails["AmountInWords"] = "";
                dRowDetails["BillName"] = "TAX INVOICE COPY / فاتورة ضريبية";
                dRowDetails["QtyTotal"] = dtbl.Rows[0]["totalQty"].ToString();
                dRowDetails["TenderPaid"] = Convert.ToDecimal(dtbl.Rows[0]["paidAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                dRowDetails["TenderBalance"] = Convert.ToDecimal(dtbl.Rows[0]["balanceAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                dRowDetails["ReportName"] = dtbl.Rows[0]["ReportName"].ToString();
                dRowDetails["PartyArabic"] = dtbl.Rows[0]["PartyArabic"].ToString();
                if (Convert.ToDecimal(dtbl.Rows[0]["cashAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart) != "0.00")
                {
                    dRowDetails["TenderCashText"] = "Cash Tendered";
                    dRowDetails["TenderCash"] = Convert.ToDecimal(dtbl.Rows[0]["cashAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                }
                else
                {
                    dRowDetails["TenderCashText"] = "";
                    dRowDetails["TenderCash"] = "";
                }
                if (Convert.ToDecimal(dtbl.Rows[0]["creditCardAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart) != "0.00")
                {
                    dRowDetails["TenderCCText"] = "CC Tendered";
                    dRowDetails["TenderCC"] = Convert.ToDecimal(dtbl.Rows[0]["creditCardAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                }
                else
                {
                    dRowDetails["TenderCCText"] = "";
                    dRowDetails["TenderCC"] = "";
                }
                if (Convert.ToDecimal(dtbl.Rows[0]["UPIAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart) != "0.00")
                {
                    dRowDetails["TenderUPIText"] = "UPI Tendered";
                    dRowDetails["TenderUPI"] = Convert.ToDecimal(dtbl.Rows[0]["UPIAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                }
                else
                {
                    dRowDetails["TenderUPIText"] = "";
                    dRowDetails["TenderUPI"] = "";
                }
                dRowDetails["customerCode"] = dtbl.Rows[0]["customerCode"].ToString();
                dRowDetails["customerName"] = dtbl.Rows[0]["customerName"].ToString();

                if (dtbl.Rows[0]["customerCode"].ToString() != "")
                {
                    dRowDetails["isCredit"] = true;
                    if (POSSettingsInfo._ShowCustBalinPrint)
                    {
                        dRowDetails["showCustBalance"] = true;
                    }
                    else
                        dRowDetails["showCustBalance"] = false;
                }
                else
                {
                    dRowDetails["isCredit"] = false;
                    dRowDetails["showCustBalance"] = false;
                }
                dRowDetails["CustomerAddress"] = dtbl.Rows[0]["PartyAddress"].ToString();
                dRowDetails["CustomerPhone"] = dtbl.Rows[0]["phoneNo"].ToString();
                dRowDetails["CustomerVatNo"] = dtbl.Rows[0]["VatNo"].ToString();
                DataTable dtblBalance = salesmaster.GetCustomerCurrentBalance(dtbl.Rows[0]["customerCode"].ToString(), PublicVariables._branchId);
                if (dtblBalance.Rows.Count > 0)
                {
                    dRowDetails["prevBalance"] = Convert.ToDecimal(decimal.Parse(dtblBalance.Rows[0]["currentBal"].ToString()) - Convert.ToDecimal(dtbl.Rows[0]["totalAmount"].ToString())).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dRowDetails["totalBalance"] = decimal.Parse(dtblBalance.Rows[0]["currentBal"].ToString()).ToString(FinanceSettingsInfo._roundDecimalPart);
                }
                else
                {
                    dRowDetails["prevBalance"] = 0;
                    dRowDetails["totalBalance"] = 0;
                }

                dRowDetails["BillAmount"] = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);

                //////------------------------ QR Code Generation ----------- by Navas --------------------
                ////Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                ////string companyname = dtblCompanyDetails.Rows[0][2].ToString();
                ////string vatno = dtblCompanyDetails.Rows[0][9].ToString();
                ////string invoicedate = Convert.ToDateTime(dtbl.Rows[0]["billDate"]).ToString("yyyy-MM-dd");
                ////string invoicetime = DateTime.Now.ToString("HH:mm:ss");
                ////invoicedate = invoicedate + "T" + invoicetime;
                ////string invoicetotal = Convert.ToDecimal(dtbl.Rows[0]["taxableAmount"]).ToString(SettingsInfo._roundDecimalPart);
                //////txtTotal.Text.Replace("SR", "");
                ////string invoicevatamount = Convert.ToDecimal(dtbl.Rows[0]["totalTaxAmount"]).ToString(SettingsInfo._roundDecimalPart);
                //////txtTaxAmt.Text.Replace("SR", "");

                ////int lencompanyname = companyname.Length;
                ////int lenvatno = vatno.Length;
                ////int leninvoicedate = invoicedate.Length;
                ////int leninvoicetime = invoicetime.Length;
                ////int leninvoicetotal = invoicetotal.Length;
                ////int leninvoicevatamount = invoicevatamount.Length;

                ////string strQRvariable = Convert.ToChar(1).ToString() + Convert.ToChar(lencompanyname).ToString() + companyname
                ////    + Convert.ToChar(2).ToString() + Convert.ToChar(lenvatno).ToString() + vatno + Convert.ToChar(3).ToString() + Convert.ToChar(19).ToString()
                ////    + invoicedate + Convert.ToChar(4).ToString() + Convert.ToChar(leninvoicetotal).ToString() + invoicetotal + Convert.ToChar(5).ToString()
                ////    + Convert.ToChar(leninvoicevatamount).ToString() + invoicevatamount;



                //////var utf8text = System.Text.Encoding.UTF8.GetBytes(strQRvariable);
                //////string qrdata = System.Convert.ToBase64String(utf8text);
                //////Image img = qrBarcode.Draw(qrdata, 500); //pictureBox1.Image;
                //////byte[] arr;
                //////ImageConverter converter = new ImageConverter();
                //////arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                //////dRowDetails["qrCode"] = arr;

                ////dRowDetails["qrCode"] = strQRvariable;
                if (FinanceSettingsInfo._ZatcaType == "Phase 2")
                {
                    DataTable dtblQre = new DataTable();
                    dtblQre = POSSalesMasterSP.GetPOSLastBillProductsforLastBillPrint(strDuplicateBillNo);

                    dRowDetails["qrCode"] = dtblQre.Rows[0]["qr_link"].ToString();
                }
                else
                {

                    //------------------------ QR Code Generation ----------- by Navas --------------------
                    Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    string companyname = dtblCompanyDetails.Rows[0]["branchName"].ToString();
                    string vatno = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
                    string invoicedate = DateTime.Parse(dtbl.Rows[0]["billDate"].ToString()).ToString("yyyy-MM-dd");
                    string invoicetime = DateTime.Now.ToString("HH:mm:ss");
                    invoicedate = invoicedate + "T" + invoicetime;
                    string invoicetotal = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    string invoicevatamount = Convert.ToDecimal(dtbl.Rows[0]["totalTaxAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);

                    int lencompanyname = companyname.Length;
                    int lenvatno = vatno.Length;
                    int leninvoicedate = invoicedate.Length;
                    int leninvoicetime = invoicetime.Length;
                    int leninvoicetotal = invoicetotal.Length;
                    int leninvoicevatamount = invoicevatamount.Length;

                    string strQRvariable = Convert.ToChar(1).ToString() + Convert.ToChar(lencompanyname).ToString() + companyname
                        + Convert.ToChar(2).ToString() + Convert.ToChar(lenvatno).ToString() + vatno + Convert.ToChar(3).ToString() + Convert.ToChar(19).ToString()
                        + invoicedate + Convert.ToChar(4).ToString() + Convert.ToChar(leninvoicetotal).ToString() + invoicetotal + Convert.ToChar(5).ToString()
                        + Convert.ToChar(leninvoicevatamount).ToString() + invoicevatamount;

                    var utf8text = System.Text.Encoding.UTF8.GetBytes(strQRvariable);
                    string qrdata = System.Convert.ToBase64String(utf8text);

                    dRowDetails["qrCode"] = qrdata;
                }

                //---------------------------------
                dtblOtherDetails.Rows.Add(dRowDetails);
            }
            dtblCompanyDetailsThermal = dtblCompanyDetails;
            dtblGridDetailsThermal = dtblGridDetails;
            dtblOtherDetailsThermal = dtblOtherDetails;

            DevPrint spPrint = new DevPrint();
            if (cmbPrintFormat.Text == "Thermal")
            {
              
                if (strHoldBillNo != "")
                {
                    spPrint.PrintSalesInvoiceHoldBill(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                }
                else
                {
                    if (strTenderType == "Credit Bill")
                    {
                        if (POSSettingsInfo._custBillCopy == "Summary")
                        {
                            spPrint.PrintSalesInvoicePOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                            spPrint.PrintCreditCustomerSummaryPOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                        }
                        else if (POSSettingsInfo._custBillCopy == "Full Bill")
                        {
                            spPrint.PrintSalesInvoicePOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                            spPrint.PrintSalesInvoicePOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                        }
                    }
                    else
                        spPrint.PrintSalesInvoicePOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                }
            }
            else if (cmbPrintFormat.Text == "A5")
            {
                spPrint.PrintSalesInvoicePOSA5(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
            }

            else
            { 
                spPrint.PrintSalesInvoicePOSA4(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                
            }

            //  }
            //int pageWidth;
            //int PageHight = 0;
            //pageSetupDialog1.PageSettings = printDocumentThermal3.DefaultPageSettings;
            //pageSetupDialog1.AllowOrientation = false;
            //PageHight = dtblGridDetailsThermal.Rows.Count + 570; //set rowcount
            //printDocumentThermal3.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Cs", 300, PageHight);
            //pageWidth = printDocumentThermal3.DefaultPageSettings.PaperSize.Width - printDocumentThermal3.DefaultPageSettings.Margins.Left - printDocumentThermal3.DefaultPageSettings.Margins.Right;
            //PrintDocument document = new PrintDocument();
            //printPreviewDialog1.Document = printDocumentThermal3;
            ////printPreviewDialog1.ShowDialog();
            //printDocumentThermal3.Print();
        }
        public void FillDatatatablesforPaymentDevPrint( string strDuplicateBillNo)
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
          
            DataTable dtblPaymentVoucherDetails = new DataTable();

            dtblPaymentVoucherDetails.Columns.Add("PaymentNo");
            dtblPaymentVoucherDetails.Columns.Add("Code");
            dtblPaymentVoucherDetails.Columns.Add("name");
            dtblPaymentVoucherDetails.Columns.Add("ChequeNo");
            dtblPaymentVoucherDetails.Columns.Add("ChequeDate");
            dtblPaymentVoucherDetails.Columns.Add("PaymentMode");
            dtblPaymentVoucherDetails.Columns.Add("BillDate");
            dtblPaymentVoucherDetails.Columns.Add("BillTime");
            dtblPaymentVoucherDetails.Columns.Add("CounterId");
            dtblPaymentVoucherDetails.Columns.Add("User");
            dtblPaymentVoucherDetails.Columns.Add("PaidAmount");
            dtblPaymentVoucherDetails.Columns.Add("Narration");
            dtblPaymentVoucherDetails.Columns.Add("AmountInWords");
            dtblPaymentVoucherDetails.Columns.Add("BillName");

            DataTable dtbl = POSpaymentMasterSP.GetPOSPaymentBillDetails(strDuplicateBillNo);

            if (dtbl.Rows.Count > 0)
            {

                DataRow dRowDetails = dtblPaymentVoucherDetails.NewRow();


                dRowDetails["PaymentNo"] = dtbl.Rows[0]["paymentNo"].ToString();
                dRowDetails["Code"] = dtbl.Rows[0]["Code"].ToString();
                dRowDetails["name"] = dtbl.Rows[0]["Name"].ToString();
                dRowDetails["ChequeNo"] = dtbl.Rows[0]["ChequeNo"].ToString();
                dRowDetails["ChequeDate"] = Convert.ToDateTime(dtbl.Rows[0]["ChequeDate"]).ToString("dd/MMM/yyyy");
                dRowDetails["PaymentMode"] = dtbl.Rows[0]["PaymentMode"].ToString();
                dRowDetails["BillDate"] = Convert.ToDateTime(dtbl.Rows[0]["BillDate"]).ToString("dd/MMM/yyyy");
                dRowDetails["BillTime"] = dtbl.Rows[0]["BillTime"].ToString();
                dRowDetails["CounterId"] = dtbl.Rows[0]["CounterId"].ToString();
                dRowDetails["User"] = dtbl.Rows[0]["User"].ToString();

                decimal Amount = Convert.ToDecimal(dtbl.Rows[0]["PaidAmount"].ToString());
                dRowDetails["PaidAmount"] = Amount.ToString(FinanceSettingsInfo._roundDecimalPart);
                dRowDetails["Narration"] = dtbl.Rows[0]["Narration"].ToString();
                dRowDetails["AmountInWords"] = new NumToText().ConvertAmountToWordsForPrint(Amount, "");
                dRowDetails["BillName"] = "Payment Voucher";

                // Add populated DataRow to Payment Voucher DataTable
                dtblPaymentVoucherDetails.Rows.Add(dRowDetails);
            }
            DevPrint spPrint = new DevPrint();
            if (cmbPrintFormat.Text == "Thermal")
                {
                    spPrint.PrintPaymentVoucher(dtblCompanyDetails, dtblPaymentVoucherDetails, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                }
                else
                   spPrint.PrintPaymentVoucherA4(dtblCompanyDetails, dtblPaymentVoucherDetails, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
            
         }
        public void FillDatatatablesforReceiptDevPrint(string strDuplicateBillNo)
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
              DataTable dtblReceiptVoucherDetails = new DataTable();
               dtblReceiptVoucherDetails.Columns.Add("ReceiptNo");
                dtblReceiptVoucherDetails.Columns.Add("Code");
                dtblReceiptVoucherDetails.Columns.Add("name");
                dtblReceiptVoucherDetails.Columns.Add("ChequeNo");
                dtblReceiptVoucherDetails.Columns.Add("ChequeDate");
                dtblReceiptVoucherDetails.Columns.Add("ReceiptMode");
                dtblReceiptVoucherDetails.Columns.Add("BillDate");
                dtblReceiptVoucherDetails.Columns.Add("BillTime");
                dtblReceiptVoucherDetails.Columns.Add("CounterId");
                dtblReceiptVoucherDetails.Columns.Add("User");
                dtblReceiptVoucherDetails.Columns.Add("PaidAmount");
                dtblReceiptVoucherDetails.Columns.Add("Narration");
                dtblReceiptVoucherDetails.Columns.Add("AmountInWords");
                dtblReceiptVoucherDetails.Columns.Add("BillName");
                DataTable dtbl = objPOSReceiptMasterSP.GetPOSReceiptBillDetails(strDuplicateBillNo);

                if (dtbl.Rows.Count > 0)
                {

                    DataRow dRowDetails = dtblReceiptVoucherDetails.NewRow();


                    dRowDetails["ReceiptNo"] = dtbl.Rows[0]["ReceiptNo"].ToString();
                    dRowDetails["Code"] = dtbl.Rows[0]["Code"].ToString();
                    dRowDetails["name"] = dtbl.Rows[0]["Name"].ToString();
                    dRowDetails["ChequeNo"] = dtbl.Rows[0]["ChequeNo"].ToString();
                    dRowDetails["ChequeDate"] = Convert.ToDateTime(dtbl.Rows[0]["ChequeDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["ReceiptMode"] = dtbl.Rows[0]["ReceiptMode"].ToString();
                    dRowDetails["BillDate"] = Convert.ToDateTime(dtbl.Rows[0]["BillDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["BillTime"] = dtbl.Rows[0]["BillTime"].ToString();
                    dRowDetails["CounterId"] = dtbl.Rows[0]["CounterId"].ToString();
                    dRowDetails["User"] = dtbl.Rows[0]["User"].ToString();

                    decimal Amount = Convert.ToDecimal(dtbl.Rows[0]["PaidAmount"].ToString());
                    dRowDetails["PaidAmount"] = Amount.ToString(FinanceSettingsInfo._roundDecimalPart);
                    dRowDetails["Narration"] = dtbl.Rows[0]["Narration"].ToString();
                    dRowDetails["AmountInWords"] = new NumToText().ConvertAmountToWordsForPrint(Amount, "");
                    dRowDetails["BillName"] = "Receipt Voucher";

                   // Add populated DataRow to Payment Voucher DataTable
                    dtblReceiptVoucherDetails.Rows.Add(dRowDetails);
                }
             DevPrint spPrint = new DevPrint();
            if (cmbPrintFormat.Text == "Thermal")
                {
                   spPrint.PrintReceiptVoucher(dtblCompanyDetails, dtblReceiptVoucherDetails, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                }
               else 
               spPrint.PrintReceiptVoucherA4(dtblCompanyDetails, dtblReceiptVoucherDetails, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesPrintCopy);
                  
        }


        POSCounterInfo counterInfo = new POSCounterInfo();
        private void frmBillPrint_Load(object sender, EventArgs e)
        {            
            try
            {              
                txtBillNo.Focus();
                POSCounterSP counterSP = new POSCounterSP();
                counterInfo = counterSP.POSCounterViewbyCounterId(PublicVariables._counterId);
                rbtnSale.Checked = true;
                cmbPrintFormat.SelectedItem = "A4";
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL5:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                isClose = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void CallFromPOSSales(frmPOSSales frm,string condition)
        {
            isFromPOSSales = true;
            _conditionFromSales = condition;
            ObjfrmPOSSales = frm;
            DoWhenComingFromOtherForms();
        }
        public void CallFromPOSSales2(frmPOSSales2 frm, string condition)
        {
            isFromPOSSales = true;
            _conditionFromSales = condition;
            ObjfrmPOSSales2 = frm;
            DoWhenComingFromOtherForms();
        }
        public void DoWhenComingFromOtherForms()
        {           
            base.ShowInTaskbar = false;
            base.ShowDialog();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------
        public void CheckWhenQuiting()
        {
            // Function to check while quiting the form
            // To return to the parent form
            // REturn bool isTrue and isClose(simply closing without authentication) to parent form
            if (isFromPOSSales)
            {
                ObjfrmPOSSales.Enabled = true;
                ObjfrmPOSSales.Activate();
                ObjfrmPOSSales.AuthenticateUser(isTrue, isClose, _conditionFromSales);
                ObjfrmPOSSales.BringToFront();    
            }
            if (isFromPOSSales2)
            {
                ObjfrmPOSSales2.Enabled = true;
                ObjfrmPOSSales2.Activate();
                ObjfrmPOSSales2.AuthenticateUser(isTrue, isClose, _conditionFromSales);
                ObjfrmPOSSales2.BringToFront();
            }
           
        }

        private void frmBillPrint_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (txtBillNo.Text != "")
            {
                if (rbtnSale.Checked == true)
                {
                    FillDatatatablesforDevPrint("", "", "", "", "", true, txtBillNo.Text, "", "");
                }

                else if(rbtnReceipt.Checked == true)
                {
                    FillDatatatablesforReceiptDevPrint(txtBillNo.Text);

                }
                else if (rbtnPayment.Checked == true)
                {
                    FillDatatatablesforPaymentDevPrint(txtBillNo.Text);
                }


                
            }
                
            
            else
            {
                MessageBox.Show("Bill number cannot be empty. Please enter a valid bill number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
       
            frmLookup frmlookup = new frmLookup();
            if (rbtnReceipt.Checked == true)
            {
                frmlookup.strSearchingName = "ReceiptVoucher";
                frmlookup.strFromFormName = "ReceiptVoucher";
                frmlookup.strSearchColumn = "BillNo";
                frmlookup.strSearchOrder = " POSReceiptMasterId DESC ";
                frmlookup.strSearchQry = " POSReceiptMasterId,BillNo,Date,BankCash,Narration,totalAmount ";
                string strdate = "";
                //if (InventorySettingsInfo._ShowAllTransctn)
                //{
                //    strdate = "";
                //}
                //else
                //    strdate = " AND date BETWEEN '" + PublicVariables._fromDate + "' AND '" + PublicVariables._toDate + "'";
                //frmlookup.strSearchTable = " (SELECT CONVERT(INT,R.POSReceiptMasterId) POSReceiptMasterId,R.receiptNo AS 'ReceiptNo',REPLACE(CONVERT(VARCHAR(11), R.billDate, 106), ' ', '-') AS Date,A.ledgerName AS BankCash,R.Narration,CONVERT(DECIMAL(18," + SettingsInfo._roundDecimal + "),R.totalAmount) AS totalAmount FROM dbo.tbl_POSReceiptMaster(NOLOCK) AS R,tbl_AccountLedger(NOLOCK) A WHERE A.CashOrBankledgerId=R.ledgerId  " + strdate + ") AS A ";
                frmlookup.strSearchTable = " (SELECT CONVERT(INT,R.POSReceiptMasterId) POSReceiptMasterId,R.receiptNo AS 'BillNo',REPLACE(CONVERT(VARCHAR(11), R.billDate, 106), ' ', '-') AS Date,A.ledgerName AS BankCash,R.Narration,CONVERT(DECIMAL(18," + FinanceSettingsInfo._roundDecimal + "),R.totalAmount) AS totalAmount FROM dbo.tbl_POSReceiptMaster(NOLOCK) AS R,tbl_AccountLedger(NOLOCK) A WHERE A.ledgerId=R.CashOrBankledgerId  ) AS A ";
                frmlookup.strSearchCondition = "";
                frmlookup.strMasterIdColumnName = "POSReceiptMasterId";
                frmlookup.IntSearchFiledCount = 5;

                frmlookup.DoWhencommingfrmBillPrintForReceiptPaymentbill(this);

            }
            else if (rbtnPayment.Checked == true)
            {
                frmlookup.strSearchingName = "PaymentVoucher";
                frmlookup.strFromFormName = "PaymentVoucher";
                frmlookup.strSearchColumn = "BillNo";
                frmlookup.strSearchOrder = " POSPaymentMasterId DESC ";
                frmlookup.strSearchQry = " POSPaymentMasterId,BillNo,Date,BankCash,Narration,totalAmount ";
                string strdate = "";
                //if (InventorySettingsInfo._ShowAllTransctn)
                //{
                //    strdate = "";
                //}
                //else
                //    strdate = " AND date BETWEEN '" + PublicVariables._fromDate + "' AND '" + PublicVariables._toDate + "'";
                //frmlookup.strSearchTable = " (SELECT CONVERT(INT,R.POSReceiptMasterId) POSReceiptMasterId,R.receiptNo AS 'ReceiptNo',REPLACE(CONVERT(VARCHAR(11), R.billDate, 106), ' ', '-') AS Date,A.ledgerName AS BankCash,R.Narration,CONVERT(DECIMAL(18," + SettingsInfo._roundDecimal + "),R.totalAmount) AS totalAmount FROM dbo.tbl_POSReceiptMaster(NOLOCK) AS R,tbl_AccountLedger(NOLOCK) A WHERE A.CashOrBankledgerId=R.ledgerId  " + strdate + ") AS A ";
                frmlookup.strSearchTable = " (SELECT CONVERT(INT,R.POSPaymentMasterId) POSPaymentMasterId,R.paymentNo AS 'BillNo',REPLACE(CONVERT(VARCHAR(11), R.billDate, 106), ' ', '-') AS Date,A.ledgerName AS BankCash,R.Narration,CONVERT(DECIMAL(18," + FinanceSettingsInfo._roundDecimal + "),R.totalAmount) AS totalAmount FROM dbo.tbl_POSPaymentMaster(NOLOCK) AS R,tbl_AccountLedger(NOLOCK) A WHERE A.ledgerId=R.CashOrBankledgerId  ) AS A ";
                frmlookup.strSearchCondition = "";
                frmlookup.strMasterIdColumnName = "POSPaymentMasterId";
                frmlookup.IntSearchFiledCount = 5;
                
                frmlookup.DoWhencommingfrmBillPrintForReceiptPaymentbill(this);
            }
            else
            {
                 frmProductPopUpGrid frm = new frmProductPopUpGrid();
                 frm.CallFromBillPrint(this, "reciept");
            }
        }
        public void FillrowAfterPickingReciept(string BillNo)
        {
            txtBillNo.Text = BillNo;

            DataTable dt = new DataTable();
            string strQuery = "SELECT ledgerName,CustomerAddress,CustomerPhone,CustomerVATNo FROM tbl_POSSalesMaster(NOLOCK) WHERE invoiceNo='" + txtBillNo.Text.Trim()  + "'";
            dt = DBClass.GetDataTable(strQuery);
            if (dt.Rows.Count > 0)
            {
                txtCustomerName.Text = dt.Rows[0]["ledgerName"].ToString();
                txtAddress.Text = dt.Rows[0]["CustomerAddress"].ToString();
                txtPhoneNo.Text = dt.Rows[0]["CustomerPhone"].ToString();
                txtVATNo.Text = dt.Rows[0]["CustomerVATNo"].ToString();
            }

            txtBillNo.Focus();  
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            txtBillNo.Text = "";
            txtCustomerName.Text = "";
            txtAddress.Text = "";
            txtPhoneNo.Text = "";
            txtVATNo.Text = "";
            txtBillNo.Focus(); 
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
           try
            {
                frmLookup frmlookup = new frmLookup();
                frmlookup.strSearchingName = "CustomerName";
                frmlookup.strFromFormName = "BillPrint";
                frmlookup.strSearchColumn = "CustomerName";
                frmlookup.strSearchOrder = " CustomerName";
                frmlookup.strSearchQry = "CustName,CustomerName,CustomerAddress,CustomerPhone,CustomerVATNo";


                frmlookup.strSearchTable = " (SELECT  ledgerName As CustomerName,ledgerName As CustName,CustomerAddress,CustomerPhone,CustomerVATNo FROM tbl_POSSalesMaster where ledgerName<>'' GROUP BY ledgerName,CustomerAddress,CustomerPhone,CustomerVATNo  ) A ";

                frmlookup.strMasterIdColumnName = "CustName";
                 frmlookup.IntSearchFiledCount = 4;

               frmlookup.DoWhenComingFromBillPrintForm(this);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void DoWhenReturningFromSearchForm(string strId,String customername,String customerAddres,String CustomerPhoneNo,String CustomerVatNo)
        {
            POSSalesMasterInfo salesmasterinfo = new POSSalesMasterInfo();
            try
            {

                this.Enabled = true;
                if (strId != "")
                {
                    salesmasterinfo.LedgerId = strId.ToString();
                    txtCustomerName.Text = customername;
                    txtAddress.Text= customerAddres;
                    txtPhoneNo.Text= CustomerPhoneNo;
                    txtVATNo.Text = CustomerVatNo;
                        
                }

                
                txtBillNo.Focus();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DoWhenReturningFromSearchofReceiptPaymentBill(string StrBillNumber)
        {
              txtBillNo.Text = StrBillNumber;

        }
        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
        private void rbtnReceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnReceipt.Checked == true )
            {
                lblCustomerName.Visible = false;
                lblCustomerAddress.Visible = false;
                lblCustomerPhone.Visible = false;
                lblCustomerVATNo.Visible = false;

                txtCustomerName.Visible = false;
                txtAddress.Visible = false;
                txtPhoneNo.Visible = false;
                txtVATNo.Visible = false;
                btnCustomerSearch.Visible = false;
         
            }
         

        }

        private void rbtnPayment_CheckedChanged(object sender, EventArgs e)
        {
           if(rbtnPayment.Checked == true)
            {
                lblCustomerName.Visible = false;
                lblCustomerAddress.Visible = false;
                lblCustomerPhone.Visible = false;
                lblCustomerVATNo.Visible = false;

                txtCustomerName.Visible = false;
                txtAddress.Visible = false;
                txtPhoneNo.Visible = false;
                txtVATNo.Visible = false;
                btnCustomerSearch.Visible = false;
            }
        }

        private void rbtnSale_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnSale.Checked == true)
            {
                lblCustomerName.Visible = true;
                lblCustomerAddress.Visible = true;
                lblCustomerPhone.Visible = true;
                lblCustomerVATNo.Visible = true;

                txtCustomerName.Visible = true;
                txtAddress.Visible = true;
                txtPhoneNo.Visible = true;
                txtVATNo.Visible = true;
                btnCustomerSearch.Visible = true;
            }
        }
    }

 }
