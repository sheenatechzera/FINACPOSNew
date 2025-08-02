using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraPrinting.Preview;
using System.IO;
using System.Web;
using System.Net;
using DevExpress.XtraPrinting;
//using Newtonsoft.Json;

namespace FinacPOS
{
    class DevPrint
    {
        public static XtraReport report = new XtraReport();


        public void PrintSalesInvoicePOS(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, DataTable dtblTaxDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptSalesInvoicePOS_THER.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["AmountInWords"].Value = dtblOtherDetails.Rows[0]["AmountInWords"];
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();
            report.Parameters["BillDiscount"].Value = dtblOtherDetails.Rows[0]["BillDiscount"];
            report.Parameters["BillName"].Value = dtblOtherDetails.Rows[0]["BillName"];
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["GrandTotal"].Value = dtblOtherDetails.Rows[0]["GrandTotal"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["PartyAddress"].Value = dtblOtherDetails.Rows[0]["PartyAddress"].ToString();
            report.Parameters["PartyName"].Value = dtblOtherDetails.Rows[0]["PartyName"].ToString();
            report.Parameters["qrCode"].Value = dtblOtherDetails.Rows[0]["qrCode"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["SubTotal"].Value = dtblOtherDetails.Rows[0]["SubTotal"].ToString();
            report.Parameters["TaxableAmount"].Value = dtblOtherDetails.Rows[0]["TaxableAmount"];
            report.Parameters["TaxAmount"].Value = dtblOtherDetails.Rows[0]["TaxAmount"];

            report.Parameters["TenderPaid"].Value = dtblOtherDetails.Rows[0]["TenderPaid"].ToString();
            report.Parameters["TenderBalance"].Value = dtblOtherDetails.Rows[0]["TenderBalance"].ToString();

            report.Parameters["TenderCash"].Value = dtblOtherDetails.Rows[0]["TenderCash"].ToString();
            report.Parameters["TenderCC"].Value = dtblOtherDetails.Rows[0]["TenderCC"].ToString();
            report.Parameters["TenderUPI"].Value = dtblOtherDetails.Rows[0]["TenderUPI"];

            report.Parameters["TenderCashText"].Value = dtblOtherDetails.Rows[0]["TenderCashText"].ToString();
            report.Parameters["TenderCCText"].Value = dtblOtherDetails.Rows[0]["TenderCCText"].ToString();
            report.Parameters["TenderUPIText"].Value = dtblOtherDetails.Rows[0]["TenderUPIText"];

            report.Parameters["UserName"].Value = dtblOtherDetails.Rows[0]["UserName"].ToString();
            report.Parameters["customerCode"].Value = dtblOtherDetails.Rows[0]["customerCode"].ToString();
            report.Parameters["customerName"].Value = dtblOtherDetails.Rows[0]["customerName"].ToString();
            report.Parameters["isCredit"].Value = dtblOtherDetails.Rows[0]["isCredit"].ToString();
            report.Parameters["prevBalance"].Value = dtblOtherDetails.Rows[0]["prevBalance"].ToString();
            report.Parameters["BillAmount"].Value = dtblOtherDetails.Rows[0]["BillAmount"].ToString();
            report.Parameters["totalBalance"].Value = dtblOtherDetails.Rows[0]["totalBalance"].ToString();
            report.Parameters["showCustBalance"].Value = dtblOtherDetails.Rows[0]["showCustBalance"].ToString();

            //Added on 03/May/2025
            report.Parameters["CustomerAddress"].Value = dtblOtherDetails.Rows[0]["CustomerAddress"].ToString();
            report.Parameters["CustomerPhone"].Value = dtblOtherDetails.Rows[0]["CustomerPhone"].ToString();
            report.Parameters["CustomerVatNo"].Value = dtblOtherDetails.Rows[0]["CustomerVATNo"].ToString();
            //Added on 29/Jul/2025
            report.Parameters["TokenNo"].Value = dtblOtherDetails.Rows[0]["TokenNo"].ToString();
            //Tax Details
            if (dtblTaxDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtblTaxDetails.Rows.Count; i++)
                {
                    if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "15%")
                    {
                        report.Parameters["taxper15"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt15"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt15"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                    else if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "0%")
                    {
                        report.Parameters["taxper0"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt0"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt0"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                }
            }


            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {

                //report.Print();
                ReportPrintTool printTools = new ReportPrintTool(report);
                printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
                printTools.PrintingSystem.ShowMarginsWarning = false;
                printTools.Print();
            }
        }
        public void PrintSalesInvoiceHoldBill(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, DataTable dtblTaxDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptHOLD_THER.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["AmountInWords"].Value = dtblOtherDetails.Rows[0]["AmountInWords"];
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();
            report.Parameters["BillDiscount"].Value = dtblOtherDetails.Rows[0]["BillDiscount"];
            report.Parameters["BillName"].Value = dtblOtherDetails.Rows[0]["BillName"];
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["GrandTotal"].Value = dtblOtherDetails.Rows[0]["GrandTotal"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["PartyAddress"].Value = dtblOtherDetails.Rows[0]["PartyAddress"].ToString();
            report.Parameters["PartyName"].Value = dtblOtherDetails.Rows[0]["PartyName"].ToString();
            report.Parameters["qrCode"].Value = dtblOtherDetails.Rows[0]["qrCode"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["SubTotal"].Value = dtblOtherDetails.Rows[0]["SubTotal"].ToString();
            report.Parameters["TaxableAmount"].Value = dtblOtherDetails.Rows[0]["TaxableAmount"];
            report.Parameters["TaxAmount"].Value = dtblOtherDetails.Rows[0]["TaxAmount"];

            report.Parameters["TenderPaid"].Value = dtblOtherDetails.Rows[0]["TenderPaid"].ToString();
            report.Parameters["TenderBalance"].Value = dtblOtherDetails.Rows[0]["TenderBalance"].ToString();

            report.Parameters["TenderCash"].Value = dtblOtherDetails.Rows[0]["TenderCash"].ToString();
            report.Parameters["TenderCC"].Value = dtblOtherDetails.Rows[0]["TenderCC"].ToString();
            report.Parameters["TenderUPI"].Value = dtblOtherDetails.Rows[0]["TenderUPI"];

            report.Parameters["TenderCashText"].Value = dtblOtherDetails.Rows[0]["TenderCashText"].ToString();
            report.Parameters["TenderCCText"].Value = dtblOtherDetails.Rows[0]["TenderCCText"].ToString();
            report.Parameters["TenderUPIText"].Value = dtblOtherDetails.Rows[0]["TenderUPIText"];

            report.Parameters["UserName"].Value = dtblOtherDetails.Rows[0]["UserName"].ToString();

            //Tax Details
            if (dtblTaxDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtblTaxDetails.Rows.Count; i++)
                {
                    if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "15%")
                    {
                        report.Parameters["taxper15"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt15"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt15"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                    else if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "0%")
                    {
                        report.Parameters["taxper0"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt0"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt0"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                }
            }


            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();

            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {
                ReportPrintTool printTools = new ReportPrintTool(report);
                printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
                printTools.Print();
            }
        }

        public void PrintSalesReturnPOS(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, DataTable dtblTaxDetails, string strPrinterName, bool directprint, int SalesReturnPrintCopy)
        {


            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptSalesReturnPOS_THER.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["AmountInWords"].Value = dtblOtherDetails.Rows[0]["AmountInWords"];
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();
            report.Parameters["BillDiscount"].Value = dtblOtherDetails.Rows[0]["BillDiscount"];
            report.Parameters["ReturnType"].Value = dtblOtherDetails.Rows[0]["ReturnType"];
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["GrandTotal"].Value = dtblOtherDetails.Rows[0]["GrandTotal"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["PartyAddress"].Value = dtblOtherDetails.Rows[0]["PartyAddress"].ToString();
            report.Parameters["PartyName"].Value = dtblOtherDetails.Rows[0]["PartyName"].ToString();
            report.Parameters["qrCode"].Value = dtblOtherDetails.Rows[0]["qrCode"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["SubTotal"].Value = dtblOtherDetails.Rows[0]["SubTotal"].ToString();
            report.Parameters["TaxableAmount"].Value = dtblOtherDetails.Rows[0]["TaxableAmount"];
            report.Parameters["TaxAmount"].Value = dtblOtherDetails.Rows[0]["TaxAmount"];
            //report.Parameters["TenderPaid"].Value = dtblOtherDetails.Rows[0]["TenderPaid"].ToString();
            //report.Parameters["TenderBalance"].Value = dtblOtherDetails.Rows[0]["TenderBalance"].ToString();

            //report.Parameters["TenderCash"].Value = dtblOtherDetails.Rows[0]["TenderCash"].ToString();
            //report.Parameters["TenderCC"].Value = dtblOtherDetails.Rows[0]["TenderCC"].ToString();
            //report.Parameters["TenderUPI"].Value = dtblOtherDetails.Rows[0]["TenderUPI"];



            report.Parameters["UserName"].Value = dtblOtherDetails.Rows[0]["UserName"].ToString();

            //Tax Details
            if (dtblTaxDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtblTaxDetails.Rows.Count; i++)
                {
                    if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "15%")
                    {
                        report.Parameters["taxper15"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt15"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt15"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                    else if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "0%")
                    {
                        report.Parameters["taxper0"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt0"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt0"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                }
            }


            //Tax Details
            //report.Parameters["taxper"].Value = dtblTaxDetails.Rows[0]["taxName"].ToString();
            //report.Parameters["taxableamt"].Value = dtblTaxDetails.Rows[0]["taxableAmt"].ToString();
            //report.Parameters["Taxamt"].Value = dtblTaxDetails.Rows[0]["amt"].ToString();

            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();

            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {
                // report.Print();
                ReportPrintTool printTools = new ReportPrintTool(report);
                printTools.PrinterSettings.Copies = (short)SalesReturnPrintCopy;
                printTools.Print();
            }
        }
        public void PrintCreditNotePOS(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, DataTable dtblTaxDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {

            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptCreditNote.repx");



            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["AmountInWords"].Value = dtblOtherDetails.Rows[0]["AmountInWords"];
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();
            report.Parameters["BillDiscount"].Value = dtblOtherDetails.Rows[0]["BillDiscount"];
            report.Parameters["ReturnType"].Value = dtblOtherDetails.Rows[0]["ReturnType"];
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["GrandTotal"].Value = dtblOtherDetails.Rows[0]["GrandTotal"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["PartyAddress"].Value = dtblOtherDetails.Rows[0]["PartyAddress"].ToString();
            report.Parameters["PartyName"].Value = dtblOtherDetails.Rows[0]["PartyName"].ToString();
            report.Parameters["qrCode"].Value = dtblOtherDetails.Rows[0]["qrCode"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["SubTotal"].Value = dtblOtherDetails.Rows[0]["SubTotal"].ToString();
            report.Parameters["TaxableAmount"].Value = dtblOtherDetails.Rows[0]["TaxableAmount"];
            report.Parameters["TaxAmount"].Value = dtblOtherDetails.Rows[0]["TaxAmount"];
            //report.Parameters["TenderPaid"].Value = dtblOtherDetails.Rows[0]["TenderPaid"].ToString();
            //report.Parameters["TenderBalance"].Value = dtblOtherDetails.Rows[0]["TenderBalance"].ToString();

            //report.Parameters["TenderCash"].Value = dtblOtherDetails.Rows[0]["TenderCash"].ToString();
            //report.Parameters["TenderCC"].Value = dtblOtherDetails.Rows[0]["TenderCC"].ToString();
            //report.Parameters["TenderUPI"].Value = dtblOtherDetails.Rows[0]["TenderUPI"];



            report.Parameters["UserName"].Value = dtblOtherDetails.Rows[0]["UserName"].ToString();

            //Tax Details
            if (dtblTaxDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtblTaxDetails.Rows.Count; i++)
                {
                    if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "15%")
                    {
                        report.Parameters["taxper15"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt15"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt15"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                    else if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "0%")
                    {
                        report.Parameters["taxper0"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt0"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt0"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                }
            }


            //Tax Details
            //report.Parameters["taxper"].Value = dtblTaxDetails.Rows[0]["taxName"].ToString();
            //report.Parameters["taxableamt"].Value = dtblTaxDetails.Rows[0]["taxableAmt"].ToString();
            //report.Parameters["Taxamt"].Value = dtblTaxDetails.Rows[0]["amt"].ToString();

            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;


            report.CreateDocument();

            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {
                //  report.Print();
                ReportPrintTool printTools = new ReportPrintTool(report);
                printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
                printTools.Print();

            }
        }

        public void PrintCreditCustomerSummaryPOS(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, DataTable dtblTaxDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptCreditCustomerSummary.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["AmountInWords"].Value = dtblOtherDetails.Rows[0]["AmountInWords"];
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();
            report.Parameters["BillDiscount"].Value = dtblOtherDetails.Rows[0]["BillDiscount"];
            report.Parameters["BillName"].Value = dtblOtherDetails.Rows[0]["BillName"];
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["GrandTotal"].Value = dtblOtherDetails.Rows[0]["GrandTotal"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["PartyAddress"].Value = dtblOtherDetails.Rows[0]["PartyAddress"].ToString();
            report.Parameters["PartyName"].Value = dtblOtherDetails.Rows[0]["PartyName"].ToString();
            report.Parameters["qrCode"].Value = dtblOtherDetails.Rows[0]["qrCode"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["SubTotal"].Value = dtblOtherDetails.Rows[0]["SubTotal"].ToString();
            report.Parameters["TaxableAmount"].Value = dtblOtherDetails.Rows[0]["TaxableAmount"];
            report.Parameters["TaxAmount"].Value = dtblOtherDetails.Rows[0]["TaxAmount"];

            report.Parameters["TenderPaid"].Value = dtblOtherDetails.Rows[0]["TenderPaid"].ToString();
            report.Parameters["TenderBalance"].Value = dtblOtherDetails.Rows[0]["TenderBalance"].ToString();

            report.Parameters["TenderCash"].Value = dtblOtherDetails.Rows[0]["TenderCash"].ToString();
            report.Parameters["TenderCC"].Value = dtblOtherDetails.Rows[0]["TenderCC"].ToString();
            report.Parameters["TenderUPI"].Value = dtblOtherDetails.Rows[0]["TenderUPI"];

            report.Parameters["TenderCashText"].Value = dtblOtherDetails.Rows[0]["TenderCashText"].ToString();
            report.Parameters["TenderCCText"].Value = dtblOtherDetails.Rows[0]["TenderCCText"].ToString();
            report.Parameters["TenderUPIText"].Value = dtblOtherDetails.Rows[0]["TenderUPIText"];

            report.Parameters["UserName"].Value = dtblOtherDetails.Rows[0]["UserName"].ToString();
            report.Parameters["customerCode"].Value = dtblOtherDetails.Rows[0]["customerCode"].ToString();
            report.Parameters["customerName"].Value = dtblOtherDetails.Rows[0]["customerName"].ToString();
            report.Parameters["isCredit"].Value = dtblOtherDetails.Rows[0]["isCredit"].ToString();
            report.Parameters["prevBalance"].Value = dtblOtherDetails.Rows[0]["prevBalance"].ToString();
            report.Parameters["BillAmount"].Value = dtblOtherDetails.Rows[0]["BillAmount"].ToString();
            report.Parameters["totalBalance"].Value = dtblOtherDetails.Rows[0]["totalBalance"].ToString();
            report.Parameters["showCustBalance"].Value = dtblOtherDetails.Rows[0]["showCustBalance"].ToString();
            //Tax Details
            if (dtblTaxDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtblTaxDetails.Rows.Count; i++)
                {
                    if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "15%")
                    {
                        report.Parameters["taxper15"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt15"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt15"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                    else if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "0%")
                    {
                        report.Parameters["taxper0"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt0"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt0"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                }
            }






            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();

            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {
                //  report.Print();
                ReportPrintTool printTools = new ReportPrintTool(report);
                printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
                printTools.Print();
            }
        }




        public void PrintSessionClose(DataTable dtblCompanyDetails, DataTable dtblOtherDetails, string strPrinterName, bool directprint)
        {


            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptSessionClose.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["Username"].Value = dtblOtherDetails.Rows[0]["Username"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["PrintingTime"].Value = dtblOtherDetails.Rows[0]["PrintingTime"].ToString();

            report.Parameters["CashSales"].Value = dtblOtherDetails.Rows[0]["CashSales"].ToString();
            report.Parameters["CardSales"].Value = dtblOtherDetails.Rows[0]["CardSales"].ToString();
            report.Parameters["UPISales"].Value = dtblOtherDetails.Rows[0]["UPISales"].ToString();
            report.Parameters["CreditSales"].Value = dtblOtherDetails.Rows[0]["CreditSales"].ToString();

            report.Parameters["TotalSales"].Value = dtblOtherDetails.Rows[0]["TotalSales"].ToString();
            report.Parameters["CashReturn"].Value = dtblOtherDetails.Rows[0]["CashReturn"].ToString();
            report.Parameters["CreditNote"].Value = dtblOtherDetails.Rows[0]["CreditNote"].ToString();
            report.Parameters["CreditReturn"].Value = dtblOtherDetails.Rows[0]["CreditReturn"].ToString();

            report.Parameters["TotalReturn"].Value = dtblOtherDetails.Rows[0]["TotalReturn"].ToString();
            report.Parameters["TotalSales1"].Value = dtblOtherDetails.Rows[0]["TotalSales1"].ToString();
            report.Parameters["TotalReturn1"].Value = dtblOtherDetails.Rows[0]["TotalReturn1"].ToString();
            report.Parameters["NetSales"].Value = dtblOtherDetails.Rows[0]["NetSales"].ToString();

            report.Parameters["CashReceipt"].Value = dtblOtherDetails.Rows[0]["CashReceipt"].ToString();
            report.Parameters["UPIReceipt"].Value = dtblOtherDetails.Rows[0]["UPIReceipt"].ToString();
            report.Parameters["CardReceipt"].Value = dtblOtherDetails.Rows[0]["CardReceipt"].ToString();
            report.Parameters["TotalReceipt"].Value = dtblOtherDetails.Rows[0]["TotalReceipt"].ToString();


            report.Parameters["CashPayment"].Value = dtblOtherDetails.Rows[0]["CashPayment"].ToString();
            report.Parameters["UPIPayment"].Value = dtblOtherDetails.Rows[0]["UPIPayment"].ToString();
            report.Parameters["CardPayment"].Value = dtblOtherDetails.Rows[0]["CardPayment"].ToString();
            report.Parameters["TotalPayment"].Value = dtblOtherDetails.Rows[0]["TotalPayment"].ToString();

            report.Parameters["OpeningBal"].Value = dtblOtherDetails.Rows[0]["OpeningBal"];
            report.Parameters["SumCashSales"].Value = dtblOtherDetails.Rows[0]["SumCashSales"];
            report.Parameters["SumCashReturn"].Value = dtblOtherDetails.Rows[0]["SumCashReturn"];
            report.Parameters["TotalCashReceipt"].Value = dtblOtherDetails.Rows[0]["TotalCashReceipt"];
            report.Parameters["TotalCashPayment"].Value = dtblOtherDetails.Rows[0]["TotalCashPayment"];

            report.Parameters["CashBalance"].Value = dtblOtherDetails.Rows[0]["CashBalance"];




            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {
                report.Print();
            }
        }
        public void PrintSalesInvoicePOSA4(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, DataTable dtblTaxDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptSalesInvoicePOS_A4.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["AmountInWords"].Value = dtblOtherDetails.Rows[0]["AmountInWords"];
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();
            report.Parameters["BillDiscount"].Value = dtblOtherDetails.Rows[0]["BillDiscount"];
            report.Parameters["BillName"].Value = dtblOtherDetails.Rows[0]["BillName"];
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["GrandTotal"].Value = dtblOtherDetails.Rows[0]["GrandTotal"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["PartyAddress"].Value = dtblOtherDetails.Rows[0]["PartyAddress"].ToString();
            report.Parameters["PartyName"].Value = dtblOtherDetails.Rows[0]["PartyName"].ToString();
            report.Parameters["qrCode"].Value = dtblOtherDetails.Rows[0]["qrCode"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["SubTotal"].Value = dtblOtherDetails.Rows[0]["SubTotal"].ToString();
            report.Parameters["TaxableAmount"].Value = dtblOtherDetails.Rows[0]["TaxableAmount"];
            report.Parameters["TaxAmount"].Value = dtblOtherDetails.Rows[0]["TaxAmount"];

            report.Parameters["TenderPaid"].Value = dtblOtherDetails.Rows[0]["TenderPaid"].ToString();
            report.Parameters["TenderBalance"].Value = dtblOtherDetails.Rows[0]["TenderBalance"].ToString();

            report.Parameters["TenderCash"].Value = dtblOtherDetails.Rows[0]["TenderCash"].ToString();
            report.Parameters["TenderCC"].Value = dtblOtherDetails.Rows[0]["TenderCC"].ToString();
            report.Parameters["TenderUPI"].Value = dtblOtherDetails.Rows[0]["TenderUPI"];

            report.Parameters["TenderCashText"].Value = dtblOtherDetails.Rows[0]["TenderCashText"].ToString();
            report.Parameters["TenderCCText"].Value = dtblOtherDetails.Rows[0]["TenderCCText"].ToString();
            report.Parameters["TenderUPIText"].Value = dtblOtherDetails.Rows[0]["TenderUPIText"];

            report.Parameters["UserName"].Value = dtblOtherDetails.Rows[0]["UserName"].ToString();
            report.Parameters["customerCode"].Value = dtblOtherDetails.Rows[0]["customerCode"].ToString();
            report.Parameters["customerName"].Value = dtblOtherDetails.Rows[0]["customerName"].ToString();
            report.Parameters["phoneNo"].Value = dtblOtherDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["VatNo"].Value = dtblOtherDetails.Rows[0]["VatNo"].ToString();
            report.Parameters["isCredit"].Value = dtblOtherDetails.Rows[0]["isCredit"].ToString();
            report.Parameters["prevBalance"].Value = dtblOtherDetails.Rows[0]["prevBalance"].ToString();
            report.Parameters["BillAmount"].Value = dtblOtherDetails.Rows[0]["BillAmount"].ToString();
            report.Parameters["totalBalance"].Value = dtblOtherDetails.Rows[0]["totalBalance"].ToString();
            report.Parameters["showCustBalance"].Value = dtblOtherDetails.Rows[0]["showCustBalance"].ToString();

            report.Parameters["ReportName"].Value = dtblOtherDetails.Rows[0]["ReportName"].ToString();
            report.Parameters["PartyArabic"].Value = dtblOtherDetails.Rows[0]["PartyArabic"].ToString();
            report.Parameters["AmountInWordsArabic"].Value = dtblOtherDetails.Rows[0]["AmountInWordsArabic"].ToString();


            //Tax Details
            if (dtblTaxDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtblTaxDetails.Rows.Count; i++)
                {
                    if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "15%")
                    {
                        report.Parameters["taxper15"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt15"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt15"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                    else if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "0%")
                    {
                        report.Parameters["taxper0"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt0"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt0"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                }
            }


            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            //if (!directprint == true)
            //{
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.AutoShowParametersPanel = false;
                printTool.PrintingSystem.ShowMarginsWarning = false;
                printTool.ShowPreviewDialog();
            }
            //}
            //else
            //{


            //    ReportPrintTool printTools = new ReportPrintTool(report); // added on 15-04-2025
            //    printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
            //    printTools.Print();
            //}
        }
        public void PrintSalesInvoicePOSA5(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, DataTable dtblTaxDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptSalesInvoicePOS_A5.repx");


            string invoiceNumber = dtblOtherDetails.Rows[0]["InvoiceNo"].ToString();
            report.DisplayName = "Invoice_" + invoiceNumber;

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["AmountInWords"].Value = dtblOtherDetails.Rows[0]["AmountInWords"];
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();
            report.Parameters["BillDiscount"].Value = dtblOtherDetails.Rows[0]["BillDiscount"];
            report.Parameters["BillName"].Value = dtblOtherDetails.Rows[0]["BillName"];
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["GrandTotal"].Value = dtblOtherDetails.Rows[0]["GrandTotal"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["PartyAddress"].Value = dtblOtherDetails.Rows[0]["PartyAddress"].ToString();
            report.Parameters["PartyName"].Value = dtblOtherDetails.Rows[0]["PartyName"].ToString();
            report.Parameters["qrCode"].Value = dtblOtherDetails.Rows[0]["qrCode"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["SessionDate"].Value = dtblOtherDetails.Rows[0]["SessionDate"].ToString();
            report.Parameters["SessionNo"].Value = dtblOtherDetails.Rows[0]["SessionNo"].ToString();
            report.Parameters["SubTotal"].Value = dtblOtherDetails.Rows[0]["SubTotal"].ToString();
            report.Parameters["TaxableAmount"].Value = dtblOtherDetails.Rows[0]["TaxableAmount"];
            report.Parameters["TaxAmount"].Value = dtblOtherDetails.Rows[0]["TaxAmount"];

            report.Parameters["TenderPaid"].Value = dtblOtherDetails.Rows[0]["TenderPaid"].ToString();
            report.Parameters["TenderBalance"].Value = dtblOtherDetails.Rows[0]["TenderBalance"].ToString();

            report.Parameters["TenderCash"].Value = dtblOtherDetails.Rows[0]["TenderCash"].ToString();
            report.Parameters["TenderCC"].Value = dtblOtherDetails.Rows[0]["TenderCC"].ToString();
            report.Parameters["TenderUPI"].Value = dtblOtherDetails.Rows[0]["TenderUPI"];

            report.Parameters["TenderCashText"].Value = dtblOtherDetails.Rows[0]["TenderCashText"].ToString();
            report.Parameters["TenderCCText"].Value = dtblOtherDetails.Rows[0]["TenderCCText"].ToString();
            report.Parameters["TenderUPIText"].Value = dtblOtherDetails.Rows[0]["TenderUPIText"];

            report.Parameters["UserName"].Value = dtblOtherDetails.Rows[0]["UserName"].ToString();
            report.Parameters["customerCode"].Value = dtblOtherDetails.Rows[0]["customerCode"].ToString();
            report.Parameters["customerName"].Value = dtblOtherDetails.Rows[0]["customerName"].ToString();
            report.Parameters["phoneNo"].Value = dtblOtherDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["VatNo"].Value = dtblOtherDetails.Rows[0]["VatNo"].ToString();
            report.Parameters["isCredit"].Value = dtblOtherDetails.Rows[0]["isCredit"].ToString();
            report.Parameters["prevBalance"].Value = dtblOtherDetails.Rows[0]["prevBalance"].ToString();
            report.Parameters["BillAmount"].Value = dtblOtherDetails.Rows[0]["BillAmount"].ToString();
            report.Parameters["totalBalance"].Value = dtblOtherDetails.Rows[0]["totalBalance"].ToString();
            report.Parameters["showCustBalance"].Value = dtblOtherDetails.Rows[0]["showCustBalance"].ToString();

            report.Parameters["ReportName"].Value = dtblOtherDetails.Rows[0]["ReportName"].ToString();
            report.Parameters["PartyArabic"].Value = dtblOtherDetails.Rows[0]["PartyArabic"].ToString();
            report.Parameters["AmountInWordsArabic"].Value = dtblOtherDetails.Rows[0]["AmountInWordsArabic"].ToString();


            //Tax Details
            if (dtblTaxDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dtblTaxDetails.Rows.Count; i++)
                {
                    if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "15%")
                    {
                        report.Parameters["taxper15"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt15"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt15"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                    else if (dtblTaxDetails.Rows[i]["Rate"].ToString() == "0%")
                    {
                        report.Parameters["taxper0"].Value = dtblTaxDetails.Rows[i]["taxName"].ToString();
                        report.Parameters["taxableamt0"].Value = dtblTaxDetails.Rows[i]["taxableAmt"].ToString();
                        report.Parameters["Taxamt0"].Value = dtblTaxDetails.Rows[i]["amt"].ToString();
                    }
                }
            }


            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            //if (!directprint == true)
            //{
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.AutoShowParametersPanel = false;
                printTool.ShowPreviewDialog();
            }
            //}
            //else
            //{


            //    ReportPrintTool printTools = new ReportPrintTool(report); // added on 15-04-2025
            //    printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
            //    printTools.Print();
            //}
        }

        public void PrintPaymentVoucher(DataTable dtblCompanyDetails,  DataTable dtblPaymentVoucherDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptPaymentPOS_THER.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
             report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["PaymentNo"].Value = dtblPaymentVoucherDetails.Rows[0]["PaymentNo"].ToString();
            report.Parameters["Code"].Value = dtblPaymentVoucherDetails.Rows[0]["Code"].ToString();
            report.Parameters["name"].Value = dtblPaymentVoucherDetails.Rows[0]["name"].ToString();
            report.Parameters["ChequeNo"].Value = dtblPaymentVoucherDetails.Rows[0]["ChequeNo"].ToString();
            report.Parameters["ChequeDate"].Value = Convert.ToDateTime(dtblPaymentVoucherDetails.Rows[0]["ChequeDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["PaymentMode"].Value = dtblPaymentVoucherDetails.Rows[0]["PaymentMode"].ToString();
            report.Parameters["BillDate"].Value = Convert.ToDateTime(dtblPaymentVoucherDetails.Rows[0]["BillDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["BillTime"].Value = dtblPaymentVoucherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblPaymentVoucherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["User"].Value = dtblPaymentVoucherDetails.Rows[0]["User"].ToString();
            report.Parameters["PaidAmount"].Value = Convert.ToDecimal(dtblPaymentVoucherDetails.Rows[0]["PaidAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
            report.Parameters["Narration"].Value = dtblPaymentVoucherDetails.Rows[0]["Narration"].ToString();
            report.Parameters["AmountInWords"].Value = dtblPaymentVoucherDetails.Rows[0]["AmountInWords"].ToString();
            report.Parameters["BillName"].Value = dtblPaymentVoucherDetails.Rows[0]["BillName"].ToString();

            // DataSet ds = new DataSet();

            // DataTable dtcopy = new DataTable();
            //  dtcopy = dtblGridDetails.Copy();
            //  ds.Tables.Add(dtcopy);

            // report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            //if (!directprint == true)
            //{
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.AutoShowParametersPanel = false;
                printTool.ShowPreviewDialog();
            }
            //}
            //else
            //{


            //    ReportPrintTool printTools = new ReportPrintTool(report); // added on 15-04-2025
            //    printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
            //    printTools.Print();
            //}
        }
        public void PrintReceiptVoucher(DataTable dtblCompanyDetails, DataTable dtblReceiptVoucherDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptReceiptPOS_THER.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            //Bill Header Details
            report.Parameters["ReceiptNo"].Value = dtblReceiptVoucherDetails.Rows[0]["ReceiptNo"].ToString();
            report.Parameters["Code"].Value = dtblReceiptVoucherDetails.Rows[0]["Code"].ToString();
            report.Parameters["name"].Value = dtblReceiptVoucherDetails.Rows[0]["name"].ToString();
            report.Parameters["ChequeNo"].Value = dtblReceiptVoucherDetails.Rows[0]["ChequeNo"].ToString();
            report.Parameters["ChequeDate"].Value = Convert.ToDateTime(dtblReceiptVoucherDetails.Rows[0]["ChequeDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["ReceiptMode"].Value = dtblReceiptVoucherDetails.Rows[0]["ReceiptMode"].ToString();
            report.Parameters["BillDate"].Value = Convert.ToDateTime(dtblReceiptVoucherDetails.Rows[0]["BillDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["BillTime"].Value = dtblReceiptVoucherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblReceiptVoucherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["User"].Value = dtblReceiptVoucherDetails.Rows[0]["User"].ToString();
            report.Parameters["PaidAmount"].Value = Convert.ToDecimal(dtblReceiptVoucherDetails.Rows[0]["PaidAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
            report.Parameters["Narration"].Value = dtblReceiptVoucherDetails.Rows[0]["Narration"].ToString();
            report.Parameters["AmountInWords"].Value = dtblReceiptVoucherDetails.Rows[0]["AmountInWords"].ToString();
            report.Parameters["BillName"].Value = dtblReceiptVoucherDetails.Rows[0]["BillName"].ToString();

            // DataSet ds = new DataSet();

            // DataTable dtcopy = new DataTable();
            //  dtcopy = dtblGridDetails.Copy();
            //  ds.Tables.Add(dtcopy);

            // report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            //if (!directprint == true)
            //{
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.AutoShowParametersPanel = false;
                printTool.ShowPreviewDialog();
            }
            //}
            //else
            //{


            //    ReportPrintTool printTools = new ReportPrintTool(report); // added on 15-04-2025
            //    printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
            //    printTools.Print();
            //}
        }
        public void PrintPaymentVoucherA4(DataTable dtblCompanyDetails, DataTable dtblPaymentVoucherDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptPaymentPOS_A4.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["PaymentNo"].Value = dtblPaymentVoucherDetails.Rows[0]["PaymentNo"].ToString();
            report.Parameters["Code"].Value = dtblPaymentVoucherDetails.Rows[0]["Code"].ToString();
            report.Parameters["name"].Value = dtblPaymentVoucherDetails.Rows[0]["name"].ToString();
            report.Parameters["ChequeNo"].Value = dtblPaymentVoucherDetails.Rows[0]["ChequeNo"].ToString();
            report.Parameters["ChequeDate"].Value = Convert.ToDateTime(dtblPaymentVoucherDetails.Rows[0]["ChequeDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["PaymentMode"].Value = dtblPaymentVoucherDetails.Rows[0]["PaymentMode"].ToString();
            report.Parameters["BillDate"].Value = Convert.ToDateTime(dtblPaymentVoucherDetails.Rows[0]["BillDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["BillTime"].Value = dtblPaymentVoucherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblPaymentVoucherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["User"].Value = dtblPaymentVoucherDetails.Rows[0]["User"].ToString();
            report.Parameters["PaidAmount"].Value = Convert.ToDecimal(dtblPaymentVoucherDetails.Rows[0]["PaidAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
            report.Parameters["Narration"].Value = dtblPaymentVoucherDetails.Rows[0]["Narration"].ToString();
            report.Parameters["AmountInWords"].Value = dtblPaymentVoucherDetails.Rows[0]["AmountInWords"].ToString();
            report.Parameters["BillName"].Value = dtblPaymentVoucherDetails.Rows[0]["BillName"].ToString();

            // DataSet ds = new DataSet();

            // DataTable dtcopy = new DataTable();
            //  dtcopy = dtblGridDetails.Copy();
            //  ds.Tables.Add(dtcopy);

            // report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            //if (!directprint == true)
            //{
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.AutoShowParametersPanel = false;
                printTool.ShowPreviewDialog();
            }
            //}
            //else
            //{


            //    ReportPrintTool printTools = new ReportPrintTool(report); // added on 15-04-2025
            //    printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
            //    printTools.Print();
            //}
        }
        public void PrintReceiptVoucherA4(DataTable dtblCompanyDetails, DataTable dtblReceiptVoucherDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptReceiptPOS_A4.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();

            //Bill Header Details
            report.Parameters["ReceiptNo"].Value = dtblReceiptVoucherDetails.Rows[0]["ReceiptNo"].ToString();
            report.Parameters["Code"].Value = dtblReceiptVoucherDetails.Rows[0]["Code"].ToString();
            report.Parameters["name"].Value = dtblReceiptVoucherDetails.Rows[0]["name"].ToString();
            report.Parameters["ChequeNo"].Value = dtblReceiptVoucherDetails.Rows[0]["ChequeNo"].ToString();
            report.Parameters["ChequeDate"].Value = Convert.ToDateTime(dtblReceiptVoucherDetails.Rows[0]["ChequeDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["ReceiptMode"].Value = dtblReceiptVoucherDetails.Rows[0]["ReceiptMode"].ToString();
            report.Parameters["BillDate"].Value = Convert.ToDateTime(dtblReceiptVoucherDetails.Rows[0]["BillDate"]).ToString("dd/MMM/yyyy");
            report.Parameters["BillTime"].Value = dtblReceiptVoucherDetails.Rows[0]["BillTime"].ToString();
            report.Parameters["CounterId"].Value = dtblReceiptVoucherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["User"].Value = dtblReceiptVoucherDetails.Rows[0]["User"].ToString();
            report.Parameters["PaidAmount"].Value = Convert.ToDecimal(dtblReceiptVoucherDetails.Rows[0]["PaidAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
            report.Parameters["Narration"].Value = dtblReceiptVoucherDetails.Rows[0]["Narration"].ToString();
            report.Parameters["AmountInWords"].Value = dtblReceiptVoucherDetails.Rows[0]["AmountInWords"].ToString();
            report.Parameters["BillName"].Value = dtblReceiptVoucherDetails.Rows[0]["BillName"].ToString();

            // DataSet ds = new DataSet();

            // DataTable dtcopy = new DataTable();
            //  dtcopy = dtblGridDetails.Copy();
            //  ds.Tables.Add(dtcopy);

            // report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            //if (!directprint == true)
            //{
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.AutoShowParametersPanel = false;
                printTool.ShowPreviewDialog();
            }
            //}
            //else
            //{


            //    ReportPrintTool printTools = new ReportPrintTool(report); // added on 15-04-2025
            //    printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
            //    printTools.Print();
            //}
        }
        public void PrintKOTPOS(DataTable dtblCompanyDetails, DataTable dtblGridDetails, DataTable dtblOtherDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptKOTPrint_THER.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();
            //report.Parameters["TokenNo"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();

            report.Parameters["BillName"].Value = "Kitchen Order Token  تذكرة طلب المطبخ";
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"];
            report.Parameters["InvoiceNo"].Value = dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["TokenNo"].Value = dtblOtherDetails.Rows[0]["TokenNo"].ToString();
            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtblGridDetails.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {

                //report.Print();
                ReportPrintTool printTools = new ReportPrintTool(report);
                printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
                printTools.PrintingSystem.ShowMarginsWarning = false;
                printTools.Print();
            }
        }
        public void POSCategoryWaysPrint(DataTable dtblCompanyDetails, DataTable dtblGridDetails,DataTable dtCategoryProductPrint, DataTable dtblOtherDetails, string strPrinterName, bool directprint, int SalesPrintCopy)
        {
            report.LoadLayout(Application.StartupPath + "\\Report_files\\rptCategoryWaysPrint_THER.repx");

            //Company Details
            report.Parameters["address"].Value = dtblCompanyDetails.Rows[0]["address"].ToString();
            report.Parameters["branchName"].Value = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            report.Parameters["cstNo"].Value = dtblCompanyDetails.Rows[0]["cstNo"].ToString();
            report.Parameters["email"].Value = dtblCompanyDetails.Rows[0]["email"].ToString();
            report.Parameters["fax"].Value = dtblCompanyDetails.Rows[0]["fax"].ToString();
            report.Parameters["logo"].Value = dtblCompanyDetails.Rows[0]["logo"].ToString();
            report.Parameters["mobile"].Value = dtblCompanyDetails.Rows[0]["mobile"].ToString();
            report.Parameters["panNo"].Value = dtblCompanyDetails.Rows[0]["panNo"].ToString();
            report.Parameters["phoneNo"].Value = dtblCompanyDetails.Rows[0]["phoneNo"].ToString();
            report.Parameters["pinNo"].Value = dtblCompanyDetails.Rows[0]["pinNo"].ToString();
            report.Parameters["tinNo"].Value = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            report.Parameters["web"].Value = dtblCompanyDetails.Rows[0]["web"].ToString();
            report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();

            report.Parameters["CRNumber"].Value = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
            report.Parameters["StreetName"].Value = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
            report.Parameters["BiuldingNo"].Value = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
            report.Parameters["AdditionalNumber"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumber"].ToString();
            report.Parameters["CityName"].Value = dtblCompanyDetails.Rows[0]["CityName"].ToString();
            report.Parameters["District"].Value = dtblCompanyDetails.Rows[0]["District"].ToString();
            report.Parameters["Country"].Value = dtblCompanyDetails.Rows[0]["Country"].ToString();
            report.Parameters["PostalCode"].Value = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
            report.Parameters["branchNameArabic"].Value = dtblCompanyDetails.Rows[0]["branchNameArabic"].ToString();
            report.Parameters["AddressArabic"].Value = dtblCompanyDetails.Rows[0]["AddressArabic"].ToString();
            report.Parameters["StreetNameARB"].Value = dtblCompanyDetails.Rows[0]["StreetNameARB"].ToString();
            report.Parameters["BiuldingNoARB"].Value = dtblCompanyDetails.Rows[0]["BiuldingNoARB"].ToString();
            report.Parameters["AdditionalNumberARB"].Value = dtblCompanyDetails.Rows[0]["AdditionalNumberARB"].ToString();
            report.Parameters["CityNameARB"].Value = dtblCompanyDetails.Rows[0]["CityNameARB"].ToString();
            report.Parameters["DistrictARB"].Value = dtblCompanyDetails.Rows[0]["DistrictARB"].ToString();
            report.Parameters["CountryARB"].Value = dtblCompanyDetails.Rows[0]["CountryARB"].ToString();
            report.Parameters["PostalCodeARB"].Value = dtblCompanyDetails.Rows[0]["PostalCodeARB"].ToString();


            //report.Parameters["extra1"].Value = dtblCompanyDetails.Rows[0]["extra1"].ToString();
            //report.Parameters["extra2"].Value = dtblCompanyDetails.Rows[0]["extra2"].ToString();

            //report.Parameters["companyheader_logo"].Value = dtblCompanyDetails.Rows[0]["companyheader_logo"].ToString();
            //report.Parameters["companyfooter_logo"].Value = dtblCompanyDetails.Rows[0]["companyfooter_logo"].ToString();
            //report.Parameters["TokenNo"].Value = dtblOtherDetails.Rows[0]["CounterId"].ToString();
            report.Parameters["BillDate"].Value = dtblOtherDetails.Rows[0]["BillDate"].ToString();

            report.Parameters["BillName"].Value = "Category Ways Printers  فئة طرق الطابعة";
            report.Parameters["BillTime"].Value = dtblOtherDetails.Rows[0]["BillTime"];
            report.Parameters["InvoiceNo"].Value= dtblOtherDetails.Rows[0]["InvoiceNo"];
            report.Parameters["QtyTotal"].Value = dtblOtherDetails.Rows[0]["QtyTotal"];
            report.Parameters["TokenNo"].Value = dtblOtherDetails.Rows[0]["TokenNo"].ToString();
            DataSet ds = new DataSet();

            DataTable dtcopy = new DataTable();
            dtcopy = dtCategoryProductPrint.Copy();
            ds.Tables.Add(dtcopy);

            report.DataSource = ds;

            report.RequestParameters = false;
            report.PrinterName = strPrinterName;

            report.CreateDocument();


            if (!directprint == true)
            {
                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.AutoShowParametersPanel = false;
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {

                //report.Print();
                ReportPrintTool printTools = new ReportPrintTool(report);
                printTools.PrinterSettings.Copies = (short)SalesPrintCopy;
                printTools.PrintingSystem.ShowMarginsWarning = false;
                printTools.Print();
            }
        }
    }
}

