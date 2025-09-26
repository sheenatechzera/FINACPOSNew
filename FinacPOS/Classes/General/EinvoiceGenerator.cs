using DevExpress.Utils.About;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZatcaEinvoicing;
using ZatcaEInvoicing;

namespace FinacPOS
{
    class EinvoiceGenerator : DBConnection
    {
        public static (bool success, string message) EinvoiceReq(string strMasterId,string strVoucherType)
        {
            string strServer = ".\\sqlExpress";
            string strPrimaryDbName = "DB";
            if (File.Exists(Application.StartupPath + "\\sys.txt"))
            {
                string DbData = File.ReadAllText(Application.StartupPath + "\\sys.txt"); ;
                string[] values = DbData.Split(',');

                strServer = values[0].Trim();
                strPrimaryDbName = values[1].Trim();

                //strServer = File.ReadAllText(Application.StartupPath + "\\sys.txt"); // getting ip of server
            }

            PublicData._compDBName = strPrimaryDbName;
            PublicData._compId = PublicVariables._companyId;
            PublicData._createCompany = false;
            PublicData._dbLoc = "Local";
            PublicData._isDemo = false;
           


            ZatcaSigner objZatca = new ZatcaSigner();
            

            
            DataTable dtblCompanyDetails = new DataTable();
            DataSet DsDetails = new DataSet();
            DataTable dtDetails = new DataTable();
            DataTable dtMaster = new DataTable();

            BranchSP SpBranch = new BranchSP();
            POSSalesMasterSP SPSalesMaster = new POSSalesMasterSP();
            POSSalesReturnMasterSP SPSalesReturnMaster = new POSSalesReturnMasterSP();

            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);

            if (strVoucherType == "POS Sales Invoice")
            {
                DsDetails = SPSalesMaster.POSSalesReportFillByPOSSalesMasterId(strMasterId);
            }
            else if (strVoucherType == "POS Sales Return")
            {
                DsDetails = SPSalesReturnMaster.POSSalesReturnReportFillByPOSSalesReturnMasterId(strMasterId);
            }


            dtMaster = DsDetails.Tables[0];
            dtDetails = DsDetails.Tables[1];

            //AccountLedgerInfo Info = new AccountLedgerInfo();
            //Info = new AccountLedgerSP().AccountLedgerView(dtMaster.Rows[0]["ledgerId"].ToString());




            if (dtDetails.Rows.Count > 0)
            {
                DataTable dtZatcaMaster = new DataTable();

                // Define the columns for the DataTable
                dtZatcaMaster.Columns.Add("VoucherNo", typeof(string));
                dtZatcaMaster.Columns.Add("ZInvoiceType", typeof(string));
                dtZatcaMaster.Columns.Add("ZInvoiceSubType", typeof(string));
                dtZatcaMaster.Columns.Add("PaymentMeansCode", typeof(string));
                dtZatcaMaster.Columns.Add("SchemeID", typeof(string));
                dtZatcaMaster.Columns.Add("SchemeIDNo", typeof(string));
                dtZatcaMaster.Columns.Add("TaxCode", typeof(string));
                dtZatcaMaster.Columns.Add("BuildingNo", typeof(string));
                dtZatcaMaster.Columns.Add("CityName", typeof(string));
                dtZatcaMaster.Columns.Add("CitySubDivisionName", typeof(string));
                dtZatcaMaster.Columns.Add("CountrySubentity", typeof(string));
                dtZatcaMaster.Columns.Add("PlotIdentification", typeof(string));
                dtZatcaMaster.Columns.Add("CompanyPostalZone", typeof(string));
                dtZatcaMaster.Columns.Add("PartyRegistrationName", typeof(string));
                dtZatcaMaster.Columns.Add("StreetName", typeof(string));
                dtZatcaMaster.Columns.Add("DistrictName", typeof(string));

                // Buyer Details
                dtZatcaMaster.Columns.Add("BuyerCityName", typeof(string)); 
                dtZatcaMaster.Columns.Add("BuyerBuildingNo", typeof(string));  
                dtZatcaMaster.Columns.Add("BuyerCitySubDivisionName", typeof(string));
                dtZatcaMaster.Columns.Add("BuyerStreetName", typeof(string)); 
                dtZatcaMaster.Columns.Add("BuyerPlotIdentification", typeof(string));
                dtZatcaMaster.Columns.Add("BuyerPostalZone", typeof(string)); 
                dtZatcaMaster.Columns.Add("BuyerContactName", typeof(string));
                dtZatcaMaster.Columns.Add("BuyerSchemeID", typeof(string));
                dtZatcaMaster.Columns.Add("BuyerSchemIDNo", typeof(string));
                dtZatcaMaster.Columns.Add("BuyerTaxCode", typeof(string));
                dtZatcaMaster.Columns.Add("BuyerDistrict", typeof(string));
                dtZatcaMaster.Columns.Add("BuyerCountryCode", typeof(string));

                //FOR RETURN
                dtZatcaMaster.Columns.Add("ReasonforReturn", typeof(string));
                dtZatcaMaster.Columns.Add("LF", typeof(string));

                

                // Invoice Products Details
                dtZatcaMaster.Columns.Add("ItemName", typeof(string));
                dtZatcaMaster.Columns.Add("Rate", typeof(decimal));
                dtZatcaMaster.Columns.Add("Qty", typeof(decimal));
                dtZatcaMaster.Columns.Add("Discount", typeof(decimal));
                dtZatcaMaster.Columns.Add("billDateTime", typeof(DateTime));

                string entrynoWithPrefix1 = dtMaster.Rows[0]["invoiceNo"].ToString();
                string EinvoiceMode = "";
                string salestype = "";
                string salestype1 = "";
                string productname = "";

                string BillType = dtMaster.Rows[0]["BillType"].ToString();
                //BillType = "Tax Invoice";// comment later. Hard coded For HOOKAH 

                if (strVoucherType == "Sales Invoice" || strVoucherType == "POS Sales Invoice")
                {
                    if (dtMaster.Rows[0]["BillType"].ToString() == "Tax Invoice")
                    {
                        EinvoiceMode = "01";  // B2B
                        salestype = "388";    // Standard Invoice
                    }
                    else if (dtMaster.Rows[0]["BillType"].ToString() == "Retail Invoice")
                    {
                        EinvoiceMode = "02";  // B2C
                        salestype = "388";    // Simplified Invoice
                    }
                }
                else if (strVoucherType == "Sales Return" || strVoucherType == "POS Sales Return")
                {
                    if (dtMaster.Rows[0]["BillType"].ToString() == "Tax Invoice")
                    {
                        EinvoiceMode = "01";  // B2B
                        salestype = "381";    // Standard Debit Note
                    }
                    else if (dtMaster.Rows[0]["BillType"].ToString() == "Retail Invoice")
                    {
                        EinvoiceMode = "02";  // B2C
                        salestype = "381";    // Simplified Debit Note
                    }
                }

             


                foreach (DataRow Dv in dtDetails.Rows)
                {
                    if (Dv["ProductName"] != DBNull.Value)
                    {
                        productname = Dv["productName"].ToString().Replace("&", "and");
                        DataRow Drn = dtZatcaMaster.NewRow();
                        Drn["VoucherNo"] = entrynoWithPrefix1;
                        Drn["ZInvoiceType"] = salestype;
                        Drn["ZInvoiceSubType"] = EinvoiceMode + "00000";

                        // Branch details
                        Drn["SchemeID"] = "CRN";
                        Drn["SchemeIDNo"] = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
                        Drn["TaxCode"] = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
                        Drn["BuildingNo"] = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
                        Drn["CityName"] = dtblCompanyDetails.Rows[0]["CityName"].ToString();
                        Drn["CitySubDivisionName"] = dtblCompanyDetails.Rows[0]["District"].ToString();
                        Drn["CountrySubentity"] = "SA";
                        Drn["PlotIdentification"] = "";
                        Drn["CompanyPostalZone"] = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
                        Drn["PartyRegistrationName"] = dtblCompanyDetails.Rows[0]["branchName"].ToString();
                        Drn["StreetName"] = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
                        Drn["DistrictName"] = dtblCompanyDetails.Rows[0]["District"].ToString();


                        if (dtMaster.Rows[0]["CustomerCode"].ToString() != "")
                        {
                            // Buyer Details
                            Drn["BuyerCityName"] = dtMaster.Rows[0]["CityName"].ToString();
                            Drn["BuyerCitySubDivisionName"] = dtMaster.Rows[0]["District"].ToString();
                            Drn["BuyerBuildingNo"] = dtMaster.Rows[0]["BuildingNo"].ToString();
                            Drn["BuyerStreetName"] = dtMaster.Rows[0]["StreetName"].ToString();
                            Drn["BuyerPlotIdentification"] = "";
                            Drn["BuyerPostalZone"] = dtMaster.Rows[0]["PostboxNo"].ToString();
                            Drn["BuyerContactName"] = dtMaster.Rows[0]["customerName"].ToString();
                            Drn["BuyerSchemeID"] = "CRN";
                            Drn["BuyerSchemIDNo"] = dtMaster.Rows[0]["cstNumber"].ToString();
                            Drn["BuyerTaxCode"] = dtMaster.Rows[0]["VATNumber"].ToString();
                            Drn["BuyerDistrict"] = dtMaster.Rows[0]["District"].ToString();
                            Drn["BuyerCountryCode"] = "SA";

                        }
                        else
                        {
                            Drn["BuyerCityName"] = dtblCompanyDetails.Rows[0]["CityName"].ToString();
                            Drn["BuyerCitySubDivisionName"] = dtblCompanyDetails.Rows[0]["District"].ToString();
                            Drn["BuyerBuildingNo"] = dtblCompanyDetails.Rows[0]["BiuldingNo"].ToString();
                            Drn["BuyerStreetName"] = dtblCompanyDetails.Rows[0]["StreetName"].ToString();
                            Drn["BuyerPlotIdentification"] = "";
                            Drn["BuyerPostalZone"] = dtblCompanyDetails.Rows[0]["PostalCode"].ToString();
                            Drn["BuyerContactName"] = "Cash Customer";
                            Drn["BuyerSchemeID"] = "";
                            Drn["BuyerSchemIDNo"] = dtblCompanyDetails.Rows[0]["CRNumber"].ToString();
                            Drn["BuyerTaxCode"] = "";
                            Drn["BuyerDistrict"] = dtblCompanyDetails.Rows[0]["District"].ToString();
                            Drn["BuyerCountryCode"] = "SA";
                        }

                        


                        //10 Cash
                        //30 Credit
                        //42 Payment to bank acnt
                        //48 Bank Card swipe
                        //1 Other(Free)

                        if (dtMaster.Rows[0]["payMode"].ToString() == "Cash")
                        {
                            Drn["PaymentMeansCode"] = "10"; // cash
                        }
                        else if (dtMaster.Rows[0]["payMode"].ToString() == "Bank")
                        {
                            Drn["PaymentMeansCode"] = "42"; // Payment to bank acnt
                        }
                        else
                        {
                            Drn["PaymentMeansCode"] = "30"; // Credit
                        }


                        // Add item details
                        Drn["ItemName"] = productname;
                        //Drn["Rate"] = Dv["rate"];
                        decimal rate = (Convert.ToDecimal(Dv["Amount"].ToString()) - Convert.ToDecimal(Dv["taxAmount"].ToString())) / Convert.ToDecimal(Dv["qty"].ToString());
                        Drn["Rate"] = rate;
                        Drn["Discount"] = 0;

                       // Drn["Rate"] = Dv["excludeRate"];
                        Drn["Qty"] = Dv["qty"];                        
                        Drn["billDatetime"] = Dv["billDatetime"];//17-Sep-2025 sheena
                        // Add the new row to dtZatcaMaster
                        dtZatcaMaster.Rows.Add(Drn);
                    }
                }

                if (strVoucherType == "POS Sales Invoice")
                {
                    strVoucherType = "POS";

                }
                else if (strVoucherType == "POS Sales Return")
                {
                    strVoucherType = "POS Return";

                }
                // Uncomment and implement the invoice submission logic if needed
                string Result = objZatca.signAndSubmitInvoice(entrynoWithPrefix1, dtZatcaMaster, strMasterId, false, false, PublicVariables._companyId, strPrimaryDbName, "Local", strVoucherType);

                // Return success
                return (true, Result);
            }

            // Return failure
            return (false, "No invoice details found.");
        }

        public static (bool success, string message) EinvoiceReqAuto(string Entryno, string invno, string dayid)
        {
            //ZatcaSigner objZatca = new ZatcaSigner();
            //DataTable BillInfo = new DataTable();
            //DataTable dt = Npos.Cls_EinvoiceGcc.Company_Select();

            //BillInfo = Npos.Cls_EinvoiceGcc.GetInvDetails(4, Convert.ToInt32(invno), Convert.ToInt32(dayid));

            //if (BillInfo.Rows.Count > 0)
            //{
            //    string entryno1 = Entryno;
            //    DataTable dtZatcaMaster = new DataTable();

            //    // Define the columns for the DataTable
            //    dtZatcaMaster.Columns.Add("VoucherNo", typeof(string));
            //    dtZatcaMaster.Columns.Add("ZInvoiceType", typeof(string));
            //    dtZatcaMaster.Columns.Add("ZInvoiceSubType", typeof(string));
            //    dtZatcaMaster.Columns.Add("PaymentMeansCode", typeof(string));
            //    dtZatcaMaster.Columns.Add("SchemeID", typeof(string));
            //    dtZatcaMaster.Columns.Add("SchemeIDNo", typeof(string));
            //    dtZatcaMaster.Columns.Add("TaxCode", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuildingNo", typeof(string));
            //    dtZatcaMaster.Columns.Add("CityName", typeof(string));
            //    dtZatcaMaster.Columns.Add("CitySubDivisionName", typeof(string));
            //    dtZatcaMaster.Columns.Add("CountrySubentity", typeof(string));
            //    dtZatcaMaster.Columns.Add("PlotIdentification", typeof(string));
            //    dtZatcaMaster.Columns.Add("CompanyPostalZone", typeof(string));
            //    dtZatcaMaster.Columns.Add("PartyRegistrationName", typeof(string));
            //    dtZatcaMaster.Columns.Add("StreetName", typeof(string));

            //    // Buyer Details
            //    dtZatcaMaster.Columns.Add("BuyerCityName", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerBuildingNo", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerCitySubDivisionName", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerStreetName", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerPlotIdentification", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerPostalZone", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerContactName", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerSchemeID", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerSchemIDNo", typeof(string));
            //    dtZatcaMaster.Columns.Add("BuyerTaxCode", typeof(string));

            //    // Invoice Products Details
            //    dtZatcaMaster.Columns.Add("ItemName", typeof(string));
            //    dtZatcaMaster.Columns.Add("Rate", typeof(decimal));
            //    dtZatcaMaster.Columns.Add("Qty", typeof(decimal));
            //    dtZatcaMaster.Columns.Add("Discount", typeof(decimal));

            //    string entrynoWithPrefix1 = entryno1;
            //    string EinvoiceMode = "";
            //    string salestype = "";
            //    string salestype1 = "1";
            //    string productname = "";
            //    if (Convert.ToInt32(salestype1) == 1)
            //    {
            //        EinvoiceMode = "02";
            //        salestype = "388";
            //    }

            //    foreach (DataRow Dv in BillInfo.Rows)
            //    {
            //        if (Dv["ProductNameENG"] != DBNull.Value)
            //        {
            //            productname = Dv["ProductNameENG"].ToString().Replace("&", "and");
            //            DataRow Drn = dtZatcaMaster.NewRow();
            //            Drn["VoucherNo"] = entrynoWithPrefix1;
            //            Drn["ZInvoiceType"] = salestype;
            //            Drn["ZInvoiceSubType"] = EinvoiceMode + "00000";

            //            // Branch details
            //            Drn["SchemeID"] = "CRN";
            //            Drn["SchemeIDNo"] = dt.Rows[0]["CRnumber"].ToString();
            //            Drn["TaxCode"] = dt.Rows[0]["VatNumber"].ToString();
            //            Drn["BuildingNo"] = dt.Rows[0]["BuildingNo"].ToString();
            //            Drn["CityName"] = dt.Rows[0]["CityName"].ToString();
            //            Drn["CitySubDivisionName"] = "";
            //            Drn["CountrySubentity"] = "SA";
            //            Drn["PlotIdentification"] = "";
            //            Drn["CompanyPostalZone"] = dt.Rows[0]["PoztalZone"].ToString();
            //            Drn["PartyRegistrationName"] = dt.Rows[0]["CompanyNameENG"].ToString();
            //            Drn["StreetName"] = "";

            //            // Buyer Details
            //            Drn["BuyerCityName"] = "";
            //            Drn["BuyerBuildingNo"] = "";
            //            Drn["BuyerCitySubDivisionName"] = "";
            //            Drn["BuyerStreetName"] = "";
            //            Drn["BuyerPlotIdentification"] = "";
            //            Drn["BuyerPostalZone"] = "";
            //            Drn["BuyerContactName"] = "Cash";
            //            Drn["BuyerSchemeID"] = "TIN";
            //            Drn["BuyerSchemIDNo"] = "";
            //            Drn["BuyerTaxCode"] = "";

            //            Drn["PaymentMeansCode"] = "10"; // cash

            //            // Add item details
            //            Drn["ItemName"] = productname;
            //            Drn["Rate"] = Dv["SalePrice"];
            //            Drn["Qty"] = Dv["SaleQTY"];
            //            Drn["Discount"] = 0;

            //            // Add the new row to dtZatcaMaster
            //            dtZatcaMaster.Rows.Add(Drn);
            //        }
            //    }

            //    // Uncomment and implement the invoice submission logic if needed
            //    string Result = objZatca.signAndSubmitInvoiceAuto(entrynoWithPrefix1, dtZatcaMaster, invno, dayid);

            //    // Return success
            //    return (true, Result);
            //}

            //// Return failure
            return (false, "No invoice details found.");
        }
    }
}
