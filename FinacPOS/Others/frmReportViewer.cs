using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;
using FinacPOS.CrystalReports;


namespace FinacPOS
{
    partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            InitializeComponent();
        }

        // Print Contra Voucher
        //internal void PrintContraVoucher(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptContraVoucher crptContraVoucher = new crptContraVoucher();
        //    crptContraVoucher.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptContraVoucher.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptContraVoucher.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptContraVoucher;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptContraVoucher.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        internal void POSProductReportPRoductwisePrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSProductwiseSummery Crptobj = new crptPOSProductwiseSummery();
                Crptobj.Database.Tables["dtblProductReport"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSOfferReportPRoductwisePrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSOfferReport Crptobj = new crptPOSOfferReport();
                Crptobj.Database.Tables["dtblProductReport"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSHourlySalesReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSHourlySalesSummary Crptobj = new crptPOSHourlySalesSummary();
                Crptobj.Database.Tables["dtblHourlySales"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSProductReportPRoductwiseProfitPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSProductwiseProfitSummery Crptobj = new crptPOSProductwiseProfitSummery();
                Crptobj.Database.Tables["dtblProductProfitReport"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSProductProfitReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSProductProfitSummery Crptobj = new crptPOSProductProfitSummery();
                Crptobj.Database.Tables["dtblProductProfitReport"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSProductReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSProductSummery Crptobj = new crptPOSProductSummery();
                Crptobj.Database.Tables["dtblProductReport"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        //POS Cashier wise report
        internal void POSCashierwiseSalesSummeryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSCashierwiseSalesSummery Crptobj = new crptPOSCashierwiseSalesSummery();
                Crptobj.Database.Tables["dtblCashierwiseSalesSummery"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSTenderTypeSummeryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSTenderTypeSalesSummery Crptobj = new crptPOSTenderTypeSalesSummery();
                Crptobj.Database.Tables["dtblTenderTypeSalesSummery"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSDailySalesSummeryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSDailySalesSummery Crptobj = new crptPOSDailySalesSummery();
                Crptobj.Database.Tables["dtblTenderTypeSalesSummery"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSDailySalesBilCountSummeryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSDailySalesBillCountSummery Crptobj = new crptPOSDailySalesBillCountSummery();
                Crptobj.Database.Tables["dtblTenderTypeSalesSummery"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSCashierwisebillsummeryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSCashierwiseBillSummery Crptobj = new crptPOSCashierwiseBillSummery();
                Crptobj.Database.Tables["dtblCashierwiseBillSummery"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSCounterwiseSalesSummeryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSCounterwiseSalesSummery Crptobj = new crptPOSCounterwiseSalesSummery();
                Crptobj.Database.Tables["dtblCashierwiseSalesSummery"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSProductSalesSummeryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod, string productcode)
        {
            try
            {
                crptPOSProductSalesSummery Crptobj = new crptPOSProductSalesSummery();
                Crptobj.Database.Tables["dtblProductSales"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                Crptobj.SetParameterValue("productcode", productcode);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        //// Print Payment Voucher
        //internal void PrintPayMentVoucher(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptPaymentVoucher crptpaymentvoucher = new crptPaymentVoucher();
        //    crptpaymentvoucher.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptpaymentvoucher.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptpaymentvoucher.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptpaymentvoucher;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptpaymentvoucher.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}

        //// Print Receipt Voucher

        //internal void PrintReceiptVoucher(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptReceiptVoucher crptReceiptVoucher = new crptReceiptVoucher();
        //    crptReceiptVoucher.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptReceiptVoucher.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptReceiptVoucher.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptReceiptVoucher;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptReceiptVoucher.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}

        //// Print Journal Voucher
        //internal void PrintJournalVoucher(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptJournalVoucher crptJournalVoucher = new crptJournalVoucher();
        //    crptJournalVoucher.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptJournalVoucher.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptJournalVoucher.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptJournalVoucher;

        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptJournalVoucher.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print Intrest Payable
        //internal void PrintIntrestPayable(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptIntrestPayable crptIntrestPayable = new crptIntrestPayable();
        //    crptIntrestPayable.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptIntrestPayable.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptIntrestPayable.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptIntrestPayable;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptIntrestPayable.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print Intrest Receive
        //internal void PrintIntrestReceive(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{

        //    crptInterestReceivable crptInterestReceivable = new crptInterestReceivable();
        //    crptInterestReceivable.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptInterestReceivable.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptInterestReceivable.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptInterestReceivable;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptInterestReceivable.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();

        //}
        //// Print PDC PAYABLE


        //internal void PrintPDCPayable(DataTable dtblCompanyDetails, DataTable dtblOtherDetails)
        //{
        //    crptPDCPayable crptPDCPayable = new crptPDCPayable();
        //    crptPDCPayable.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptPDCPayable.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptPDCPayable;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptPDCPayable.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print PDC Receivable

        //internal void PrintPDCReceivable(DataTable dtblCompanyDetails, DataTable dtblOtherDetails)
        //{
        //    crptPDCReceivable crptPDCReceivable = new crptPDCReceivable();
        //    crptPDCReceivable.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptPDCReceivable.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptPDCReceivable;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptPDCReceivable.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print Purchase Order

        //internal void PrintPurchaseOrder(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptPurchaseOrder crptPurchaseOrder = new crptPurchaseOrder();
        //    crptPurchaseOrder.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptPurchaseOrder.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptPurchaseOrder.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptPurchaseOrder;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptPurchaseOrder.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print Material Receipt
        //internal void PrintMaterialReceipt(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptMaterialReceipt crptMaterialReceipt = new crptMaterialReceipt();
        //    crptMaterialReceipt.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptMaterialReceipt.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptMaterialReceipt.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptMaterialReceipt;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptMaterialReceipt.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print  Rejection Out
        //internal void PrintRejectionOut(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptRejectionOut crptRejectionOut = new crptRejectionOut();
        //    crptRejectionOut.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptRejectionOut.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptRejectionOut.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptRejectionOut;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptRejectionOut.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print  Purchase Invoice
        //internal void PrintPurchaseInvoice(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblTaxDetails, DataTable dtblOtherDetails)
        //{
        //    crptPurchaseInvoice crptPurchaseInvoice = new crptPurchaseInvoice();
        //    crptPurchaseInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptPurchaseInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptPurchaseInvoice.Database.Tables["dtblTaxDetails"].SetDataSource(dtblTaxDetails);
        //    crptPurchaseInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptPurchaseInvoice;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptPurchaseInvoice.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        //// Print Purchase Return
        //internal void PrintPurchaseReturn(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptPurchaseReturn crptPurchaseReturn = new crptPurchaseReturn();
        //    crptPurchaseReturn.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptPurchaseReturn.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptPurchaseReturn.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptPurchaseReturn;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptPurchaseReturn.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}

        //internal void PrintSalesQuotation(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptSalesQuotationDetailed crptSalesQuotation = new crptSalesQuotationDetailed();
        //    crptSalesQuotation.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptSalesQuotation.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptSalesQuotation.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptSalesQuotation;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptSalesQuotation.PrintToPrinter(1, false, 0, 0);
        //    }
        //}
        //// Print Sales Order
        //internal void PrintSalesOrder(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptSalesOrder crptSalesOrder = new crptSalesOrder();
        //    crptSalesOrder.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptSalesOrder.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptSalesOrder.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptSalesOrder;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptSalesOrder.PrintToPrinter(1, false, 0, 0);
        //    }
        //}
        //// Print Delivery Note
        //internal void PrintDeliveryNote(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptDeliveryNote crptDeliveryNote = new crptDeliveryNote();
        //    crptDeliveryNote.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptDeliveryNote.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptDeliveryNote.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptDeliveryNote;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptDeliveryNote.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        ////Print Rejection In
        //internal void PrintRejectionIn(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptRejectionIn crptRejectionIn = new crptRejectionIn();
        //    crptRejectionIn.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptRejectionIn.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptRejectionIn.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptRejectionIn;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptRejectionIn.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        ////Print Physical Stock
        //internal void PrintPhysicalStock(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptPhysicalStock crptPhysicalStock = new crptPhysicalStock();
        //    crptPhysicalStock.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptPhysicalStock.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptPhysicalStock.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptPhysicalStock;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptPhysicalStock.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        ////PrintDamageStock
        //internal void PrintDamageStock(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptDamageStock crptDamageStock = new crptDamageStock();
        //    crptDamageStock.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptDamageStock.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptDamageStock.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptDamageStock;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptDamageStock.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}


        ////PrintDamageStock
        //internal void PrintUsedStock(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptUsedStock crptUsedStock = new crptUsedStock();
        //    crptUsedStock.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptUsedStock.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptUsedStock.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptUsedStock;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptUsedStock.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}

        //// Print SalesreportItemwise

        //internal void PrintSalesreportItemwise(DataTable dtblCompanyDetails, DataTable dtblGridDetails, string reportName, string TotalWords,string subBranchName)
        //{
        //    crptSalesreportItemwise crptSalesreportItemwise = new crptSalesreportItemwise();
        //    crptSalesreportItemwise.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptSalesreportItemwise.Database.Tables["dtItemWiseSaleReport"].SetDataSource(dtblGridDetails);
        //    crptSalesreportItemwise.SetParameterValue("Report Name", reportName);
        //    crptSalesreportItemwise.SetParameterValue("TotalWords", TotalWords);
        //    crptSalesreportItemwise.SetParameterValue("SubBranchName", subBranchName);
        //    this.crystalReportViewer1.ReportSource = crptSalesreportItemwise;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptSalesreportItemwise.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        ////Print Branch Transfer
        //internal void PrintBranchToBranchTransfer(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptBranchToBranchTransfer crptBranchToBranchTransfer = new crptBranchToBranchTransfer();
        //    crptBranchToBranchTransfer.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptBranchToBranchTransfer.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptBranchToBranchTransfer.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptBranchToBranchTransfer;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptBranchToBranchTransfer.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        ////print Pay Slip
        //internal void PrintPaySlip(DataSet dsPaySlip)
        //{
        //    crptSalarySlip crptSalarySlip = new crptSalarySlip();
        //    decimal ttlDed = 0;
        //    decimal ttlAdd = 0;
        //    foreach (DataTable dtbl in dsPaySlip.Tables)
        //    {
        //        if (dtbl.TableName == "Table")
        //        {
        //            crptSalarySlip.Database.Tables["dtblCompanyDetails"].SetDataSource(dtbl);
        //        }
        //        else if (dtbl.TableName == "Table1")
        //        {
        //            crptSalarySlip.Database.Tables["dtblEmployeeDetails"].SetDataSource(dtbl);
        //            try
        //            {
        //                foreach (DataRow drow in dtbl.Rows)
        //                {
        //                    if (drow["ADDamount"].ToString() != "")
        //                    {
        //                        ttlAdd = ttlAdd + decimal.Parse(drow["ADDamount"].ToString());
        //                    }
        //                    if (drow["DEDamount"].ToString() != "")
        //                    {
        //                        string str = drow["DEDamount"].ToString();
        //                        ttlDed = ttlDed + decimal.Parse(drow["DEDamount"].ToString());
        //                    }
        //                }
        //                foreach (DataRow drow in dtbl.Rows)
        //                {
        //                    if (drow["LOP"].ToString() != "")
        //                    {
        //                        ttlDed = ttlDed + decimal.Parse(drow["LOP"].ToString());
        //                    }
        //                    if (drow["Deduction"].ToString() != "")
        //                    {
        //                        ttlDed = ttlDed + decimal.Parse(drow["Deduction"].ToString());
        //                    }
        //                    if (drow["Bonus"].ToString() != "")
        //                    {
        //                        ttlAdd = ttlAdd + decimal.Parse(drow["Bonus"].ToString());
        //                    }
        //                    if (drow["OTAmount"].ToString() != "")
        //                    {
        //                        ttlAdd = ttlAdd + decimal.Parse(drow["OTAmount"].ToString());
        //                    }
        //                    break;
        //                }
        //            }
        //            catch (Exception)
        //            {
        //            }
        //        }
        //        else if (dtbl.TableName == "Table2")
        //        {
        //            try
        //            {
        //                DataColumn dtclmn = new DataColumn("AmountInWords");
        //                dtbl.Columns.Add(dtclmn);
        //                foreach (DataRow drow in dtbl.Rows)
        //                {
        //                    drow["AmountInWords"] = new NumToText().ConvertAmountToWordsForPrint(ttlAdd - ttlDed, PublicVariables._currencyId);
        //                }
        //            }
        //            catch (Exception)
        //            {
        //            }
        //            crptSalarySlip.Database.Tables["dtblOther"].SetDataSource(dtbl);
        //        }
        //    }

        //    this.crystalReportViewer1.ReportSource = crptSalarySlip;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptSalarySlip.PrintToPrinter(1, false, 0, 0);
        //    }

        //}
        ////Print Sales Invoice

        //private object GetField(Object obj, String fieldName)
        //{
        //    System.Reflection.FieldInfo fi = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        //    return fi.GetValue(obj);
        //}
        //private CrystalDecisions.Shared.PaperSize CType(object p, CrystalDecisions.Shared.PaperSize paperSize)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}
        //internal void PrintSalesReturn(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblTaxDetails, DataTable dtblOtherDetails)
        //{
        //    crptSalesReturn crptSalesReturn = new crptSalesReturn();
        //    crptSalesReturn.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptSalesReturn.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);

        //    crptSalesReturn.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptSalesReturn;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptSalesReturn.PrintToPrinter(1, false, 0, 0);
        //    }

        //}

        //internal void PrintSalesReturnTaxInvoice(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblTaxDetails, DataTable dtblOtherDetails)
        //{
        //    crptSalesReturnTaxInvoice crptSalesReturn = new crptSalesReturnTaxInvoice();
        //    crptSalesReturn.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptSalesReturn.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);

        //    crptSalesReturn.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //    this.crystalReportViewer1.ReportSource = crptSalesReturn;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptSalesReturn.PrintToPrinter(1, false, 0, 0);
        //    }

        //}

        //internal void PrintPackageSalesInvoice(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //            }
        //internal void PrintGatePass(DataTable dtblMainDetails, DataTable dtblGridDetails)
        //{
        //    crptGatePass crptGatePass = new crptGatePass();
        //    crptGatePass.Database.Tables["dtblMainDetails"].SetDataSource(dtblMainDetails);
        //    crptGatePass.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    this.crystalReportViewer1.ReportSource = crptGatePass;

        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptGatePass.PrintToPrinter(1, false, 0, 0);
        //    }

        //}

        //public void PrintServiceVoucher(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    crptServiceVoucher crptServiceVoucher = new crptServiceVoucher();

        //    crptServiceVoucher.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptServiceVoucher.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //    crptServiceVoucher.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);


        //    this.crystalReportViewer1.ReportSource = crptServiceVoucher;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptServiceVoucher.PrintToPrinter(1, false, 0, 0);
        //    }

        //}
        //internal void PrintDebitNote(string strDebitNoteId)
        //{
        //    DataSet dset = new DebitNoteMasterSP().DebitNotePrint(strDebitNoteId);
        //    crptDebitNote crptDebitNote = new crptDebitNote();
        //    crptDebitNote.Database.Tables["dtblDetails"].SetDataSource(dset.Tables[0]);
        //    crptDebitNote.Database.Tables["dtblCess"].SetDataSource(dset.Tables[1]);
        //    this.crystalReportViewer1.ReportSource = crptDebitNote;
        //    base.Show();
        //    this.BringToFront();
        //}
        //internal void PrintChartOfAccounts(DataTable dtblChart)
        //{
        //    crptChartOfAccount crptChartOfAccount = new crptChartOfAccount();
        //    crptChartOfAccount.Database.Tables["dtblItems"].SetDataSource(dtblChart);
        //    this.crystalReportViewer1.ReportSource = crptChartOfAccount;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptChartOfAccount.PrintToPrinter(1, false, 0, 0);
        //    }
        //}

        //internal void PrintSalesInvoiceEstimate(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {

        //        crptEstimate crptSalesInvoice = new crptEstimate();
        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //internal void PrintSalesInvoiceRetailInvoice(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {
        //        crptRetailInvoice crptSalesInvoice = new crptRetailInvoice();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintSalesDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptSalesReportDetailed crptSalesInvoice = new crptSalesReportDetailed();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //        crptSalesInvoice.SetParameterValue("isCostCentre", FinanceSettingsInfo._ActivateCosteCentre);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //internal void PrintSalesReturnDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptSalesReturnReportDetailed crptSalesInvoice = new crptSalesReturnReportDetailed();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //        crptSalesInvoice.SetParameterValue("isCostCentre", FinanceSettingsInfo._ActivateCosteCentre);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintProformaInvoiceDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptProformaInvoiceReportDetailed crptSalesInvoice = new crptProformaInvoiceReportDetailed();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //        crptSalesInvoice.SetParameterValue("isCostCentre", FinanceSettingsInfo._ActivateCosteCentre);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //internal void PrintSalesInvoiceRetailInvoiceNoFreeColumn(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {
        //        crptRetailInvoiceNoFreeColumn crptSalesInvoice = new crptRetailInvoiceNoFreeColumn();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //internal void PrintSalesInvoiceRetailInvoiceNoTax(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {
        //        //crptWithoutTax crptSalesInvoice = new crptWithoutTax();
        //        CrptRetailInvoiceNoTax crptSalesInvoice = new CrptRetailInvoiceNoTax();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //internal void PrintSalesInvoiceTaxInvoice(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {
        //        crptTaxInvoice crptSalesInvoice = new crptTaxInvoice();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //internal void PrintSalesInvoiceTaxInvoiceNoFree(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency,bool isPrintPreview)
        //{
        //    try
        //    {
        //        ////crptWithoutTax crptSalesInvoice = new crptWithoutTax();
        //        //crptTaxInvoiceNoFreeColumn crptSalesInvoice = new crptTaxInvoiceNoFreeColumn();

        //        //crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        //crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        //crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        //PrintDocument printdoc = new PrintDocument();
        //        //int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        ////crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        //this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        //if (!SettingsInfo._directPrinting && SettingsInfo._printer == "Other")
        //        //{
        //        //    base.Show();
        //        //    this.BringToFront();
        //        //}
        //        //else
        //        //{
        //        //    crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        //}


        //        //added by Varis on 31/Oct/2023
        //        reportdocum = new ReportDocument();
        //        string Reportpath = Application.StartupPath.Replace("\\bin\\Debug", "") + "\\CrystalReports\\Reports\\Sales\\crptTaxInvoiceNoFreeColumn.rpt";
        //        reportdocum.Load(Reportpath);
        //        reportdocum.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        reportdocum.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        reportdocum.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);


        //        crystalReportViewer1.ReportSource = reportdocum;

        //        //if (!SettingsInfo._directPrinting && SettingsInfo._printer == "Other")
        //        if (isPrintPreview && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            reportdocum.PrintToPrinter(1, false, 0, 0);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintSalesInvoiceTaxInvoiceNoFreeThermal(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {
        //        //crptWithoutTax crptSalesInvoice = new crptWithoutTax();
        //        crptTaxInvoiceNoFreeColumnThermal crptSalesInvoice = new crptTaxInvoiceNoFreeColumnThermal();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Thermal")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        //internal void PrintSalesInvoiceWithoutTAX(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {
        //        crptWithoutTax crptSalesInvoice = new crptWithoutTax();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintSalesInvoiceWithoutTAXNoFree(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate, string strCurrrency)
        //{
        //    try
        //    {
        //        crptWithoutTaxNoFreeColumn crptSalesInvoice = new crptWithoutTaxNoFreeColumn();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintSalesQuotationDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptSalesQuotationReportDetailed crptSalesInvoice = new crptSalesQuotationReportDetailed();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //        crptSalesInvoice.SetParameterValue("isCostCentre", FinanceSettingsInfo._ActivateCosteCentre);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintSalesOrderDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptSalesOrderReportDetailed crptSalesInvoice = new crptSalesOrderReportDetailed();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //        crptSalesInvoice.SetParameterValue("isCostCentre", FinanceSettingsInfo._ActivateCosteCentre);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintDeliveryNoteDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptDeliveryNoteReportDetailed crptSalesInvoice = new crptDeliveryNoteReportDetailed();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //        crptSalesInvoice.SetParameterValue("isCostCentre", FinanceSettingsInfo._ActivateCosteCentre);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintRejectionInDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptRejectionInReportDetailed crptSalesInvoice = new crptRejectionInReportDetailed();

        //        crptSalesInvoice.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptSalesInvoice.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptSalesInvoice.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);
        //        crptSalesInvoice.SetParameterValue("isCostCentre", FinanceSettingsInfo._ActivateCosteCentre);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptSalesInvoice;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptSalesInvoice.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintPurchaseReturnDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptPurchaseReturnReportDetailed crptPurchaseReturnReportDetailed = new crptPurchaseReturnReportDetailed();

        //        crptPurchaseReturnReportDetailed.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptPurchaseReturnReportDetailed.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptPurchaseReturnReportDetailed.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptPurchaseReturnReportDetailed;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptPurchaseReturnReportDetailed.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}


        //internal void PrintPurchaseOrderDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptPurchaseOrderReportDetailed crptPurchaseOrderReportDetailed = new crptPurchaseOrderReportDetailed();

        //        crptPurchaseOrderReportDetailed.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptPurchaseOrderReportDetailed.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptPurchaseOrderReportDetailed.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptPurchaseOrderReportDetailed;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptPurchaseOrderReportDetailed.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintPurchaseDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        crptPurchaseReportDetailed crptPurchaseReportDetailed = new crptPurchaseReportDetailed();

        //        crptPurchaseReportDetailed.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptPurchaseReportDetailed.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptPurchaseReportDetailed.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = crptPurchaseReportDetailed;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptPurchaseReportDetailed.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintMaterialReceiptDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string Taxtype, string FormType, bool isShowRate)
        //{
        //    try
        //    {
        //        CrptMaterialReceiptDetail CrptMaterialReceiptDetail = new CrptMaterialReceiptDetail();

        //        CrptMaterialReceiptDetail.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        CrptMaterialReceiptDetail.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        CrptMaterialReceiptDetail.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;
        //        //crptSalesInvoice.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)rawkind;

        //        this.crystalReportViewer1.ReportSource = CrptMaterialReceiptDetail;
        //        if (!SalesSettingsInfo._directPrint && InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            CrptMaterialReceiptDetail.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //internal void PrintRejectionOutDetaildReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails)
        //{
        //    try
        //    {
        //        CrptRejectionOutDetail crptRejectionOutDetail = new CrptRejectionOutDetail();
        //        crptRejectionOutDetail.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //        crptRejectionOutDetail.Database.Tables["dsGridDetails"].SetDataSource(dtblGridDetails);
        //        crptRejectionOutDetail.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //        PrintDocument printdoc = new PrintDocument();
        //        int rawkind = printdoc.DefaultPageSettings.PaperSize.RawKind;

        //        this.crystalReportViewer1.ReportSource = crptRejectionOutDetail;
        //        if (!SalesSettingsInfo._directPrint&& InventorySettingsInfo._printer == "Other")
        //        {
        //            base.Show();
        //            this.BringToFront();
        //        }
        //        else
        //        {
        //            crptRejectionOutDetail.PrintToPrinter(1, false, 0, 0);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        internal void POSExchangedItemReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSExchangedItem Crptobj = new crptPOSExchangedItem();
                Crptobj.Database.Tables["dtblGridDetails"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSBillSalesSummaryReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSBillSalesSummary Crptobj = new crptPOSBillSalesSummary();
                Crptobj.Database.Tables["dtblGridDetails"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSBillSalesDetailsReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSBillWiseSalesDetailed Crptobj = new crptPOSBillWiseSalesDetailed();
                Crptobj.Database.Tables["dtblGridDetails"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSBillProfitSummaryReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod, string strProfitType)
        {
            try
            {
                crptPOSBillProfitSummary Crptobj = new crptPOSBillProfitSummary();
                Crptobj.Database.Tables["dtblGridDetails"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                Crptobj.SetParameterValue("ProfitType", strProfitType);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        internal void POSBillProfitDetailsReportPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod, string strProfitType)
        {
            try
            {
                crptPOSBillWiseProfitDetailed Crptobj = new crptPOSBillWiseProfitDetailed();
                Crptobj.Database.Tables["dtblGridDetails"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                Crptobj.SetParameterValue("ProfitType", strProfitType);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        //internal void POSHoldBillSummaryPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        //{
        //    try
        //    {
        //        crptPOSHoldBillSummary Crptobj = new crptPOSHoldBillSummary();
        //        Crptobj.Database.Tables["dtblGridDetails"].SetDataSource(Dtbl);
        //        Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
        //        Crptobj.SetParameterValue("ReportName", strReportName);
        //        Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
        //        Crptobj.SetParameterValue("username", PublicVariables._currentUserId);

        //        this.crystalReportViewer1.ReportSource = Crptobj;
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //    }

        //}
        internal void POSHoldBillDetailedPrinting(DataTable Dtbl, DataTable dtblCompany, string strReportName, string strDatePeriod)
        {
            try
            {
                crptPOSHoldBillDetailed Crptobj = new crptPOSHoldBillDetailed();
                Crptobj.Database.Tables["dtblGridDetails"].SetDataSource(Dtbl);
                Crptobj.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompany);
                Crptobj.SetParameterValue("ReportName", strReportName);
                Crptobj.SetParameterValue("DatePeriod", strDatePeriod);
                Crptobj.SetParameterValue("username", PublicVariables._currentUserId);
                this.crystalReportViewer1.ReportSource = Crptobj;
                base.Show();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("FR46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        //internal void PrintTaxReturnSummaryReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, string strReportName, string strDatePeriod)
        //{
        //    crptTaxReturnReport crptTaxReturnReport = new crptTaxReturnReport();
        //    crptTaxReturnReport.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
        //    crptTaxReturnReport.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
        //   // crptBranchToBranchTransfer.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

        //    crptTaxReturnReport.SetParameterValue("ReportName", strReportName);
        //    crptTaxReturnReport.SetParameterValue("DatePeriod", strDatePeriod);
        //    this.crystalReportViewer1.ReportSource = crptTaxReturnReport;
        //    if (!SalesSettingsInfo._directPrint)
        //    {
        //        base.Show();
        //        this.BringToFront();
        //    }
        //    else
        //    {
        //        crptTaxReturnReport.PrintToPrinter(1, false, 0, 0);
        //    }
        //    //base.Show();
        //    //this.BringToFront();
        //}
        internal void PrintTaxReturnDetailedReport(DataTable dtblCompanyDetails, DataTable dtblGridDetails, string strReportName, string strDatePeriod)
        {
            //crptTaxReturnDetailed crptTaxReturnDetailed = new crptTaxReturnDetailed();
            //crptTaxReturnDetailed.Database.Tables["dtblCompanyDetails"].SetDataSource(dtblCompanyDetails);
            //crptTaxReturnDetailed.Database.Tables["dtblGridDetails"].SetDataSource(dtblGridDetails);
            //// crptBranchToBranchTransfer.Database.Tables["dtblOtherDetails"].SetDataSource(dtblOtherDetails);

            //crptTaxReturnDetailed.SetParameterValue("ReportName", strReportName);
            //crptTaxReturnDetailed.SetParameterValue("DatePeriod", strDatePeriod);
            //this.crystalReportViewer1.ReportSource = crptTaxReturnDetailed;
            //if (!SalesSettingsInfo._directPrint)
            //{
            //    base.Show();
            //    this.BringToFront();
            //}
            //else
            //{
            //    crptTaxReturnDetailed.PrintToPrinter(1, false, 0, 0);
            //}
            //base.Show();
            //this.BringToFront();
        }


    }
}