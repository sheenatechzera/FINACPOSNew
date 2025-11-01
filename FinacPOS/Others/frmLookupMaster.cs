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
    partial class frmLookupMaster : Form
    {
        public frmLookupMaster()
        {
            InitializeComponent();
        }
        #region PUBLIC VARIABLES

        public string strSearchQry = "";
        public string strSearchTable = "";
        public string strSearchCondition = "";
        public string strSearchOrder = "";
        public string strSearchColumn = "";
        public int IntSearchFiledCount = 0;
        public string strFromFormName = "";
        public string strSearchingName = "";

        public string strMasterIdColumnName = "";
        public string strMasterIdColumnValue = "";

        int inCurrenRowIndex = 0;// To keep row index


        frmCustomer frmCust;
        //frmContraVoucher frmContra;
        //frmReceipt frmreceipt;
        //frmPayment frmpayment;
        //frmJournalVoucher frmjournal;
        //frmPurchaseInvoice frmpurchase;
        //frmPurchaseReturn frmpurchasereturn;
        //frmSalesInvoice frmsalesinvoice;
        //frmSalesInvoiceSimple frmsalesinvoicesimple;
        //frmSalesReturn frmsalesreturn;
        //frmProductHistory frmproductHstory;
        //frmSalesOrder frmsalesorder;
        //frmProformaInvoice frmProformaInvoice;
        //frmSalesQuotation frmsalesquotation;
        //frmPurchaseOrder frmpurchaseOrder;
        //frmMaterialReceipt frmmaterialReceipt;
        //frmRejectionOut frmrejectionOut;
        frmProductCreation frmproductCreation;
        //frmBarcodePrinting frmbarcodePrinting;
        //frmPayableVoucher frmpayable;
        //frmReceivableVoucher frmReceivable;
        //frmDeliveryNote frmDeliveryNote;
        //frmRejectionIn frmRejectionIn;
        //frmPurchaseInvoicePOS frmpurchasePOS;
        //frmDamageStock frmdamagestock;
        //frmPhysicalStock frmPphysicalstock;
        //frmUsedStock frmusedstock;
        //frmOfferRate frmofferrate;
        //frmPOSPaymentMethod frmPOSpayment;
        //frmTransfer frmtransfer = new frmTransfer();


        bool isFromCustomerForm = false;
        bool isFromContraVoucherForm = false;
        bool isFromReceiptVoucherForm = false;
        bool isFromPaymentVoucherForm = false;
        bool isFromJournalVoucherForm = false;
        bool isFromPurchaseInvoiceForm = false;
        bool isFromPurchaseReturnForm = false;
        bool isFromSalesInvoiceForm = false;
        bool isFromSalesInvoiceFormSimple = false;
        bool isFromSalesReturnForm = false;
        bool isFromProductHistory = false;
        bool isFromSalesOrderForm = false;
        bool isFromProformaInvoiceForm = false;
        bool isFromsalesquotationForm = false;
        bool isFromPurchaseOrderForm = false;
        bool isFromMaterialReceiptForm=false;
        bool isFromRejectionOutForm = false;
        bool isFromProductCreationForm = false;
        bool isFrombarcodePrintingForm = false;
        bool isFromPayableVoucherForm = false;
        bool isFromReceivableVoucherForm = false;
        bool isFromDeliveryNote = false;
        bool isFromRejectionIn=false;
        bool isFromPurchaseInvoicePOSForm = false;
        bool isFromDamageStockForm = false;
        bool isFromPhysicalStockForm = false;
        bool isFromUSedStockForm = false;
        bool isFromOfferRateForm = false;
        bool isFromPOSPayment = false;
        bool isFromTransfer = false;

        DBClass DBClass = new DBClass();
        #endregion

        private void frmLookupMaster_Load(object sender, EventArgs e)
        {
            this.Text = "Searching " + strSearchingName;

            lblSearchColumn.Text = "Search By: ";

            if (strSearchColumn != "")
            {
                lblSearchColumn.Text = lblSearchColumn.Text + "" + strSearchColumn;
            }


        }

        private void SearchFunction(string searchQry, string searchTable, string searchCnt, string searchOrder, int searchFiledCount,string MasterId)
        {
            int ColumnWidth;
            string selectQry;
            string ColumnHead;
            int gridTotWidth;
            string BrkStr;

            dgvRegister.Visible = true;

            ColumnWidth = (5400 / searchFiledCount);
            ColumnHead = searchQry;
            dgvRegister.Columns.Clear();

            selectQry = "SELECT " + searchQry + " FROM " + searchTable;

            if (searchCnt.Trim() != "")
            {
                selectQry = selectQry + " WHERE " + searchCnt;
            }

            if (searchOrder.Trim() != "")
            {
                selectQry = selectQry + " ORDER BY " + searchOrder;
            }

            DataTable dt = new DataTable();
            dt = DBClass.GetDataTable(selectQry);
            if (dt.Rows.Count == 0)
            {
                dt.Dispose();
                return;
            }
            else
            {
                dgvRegister.DataSource = dt;
                if (MasterId != "")
                {
                    if (dgvRegister.Columns.Contains(MasterId.ToString()) == true)
                    {
                        dgvRegister.Columns[MasterId].Visible = false;
                    }
                    
                }
                dgvRegister.Visible = true;
                dgvRegister.ClearSelection(); 
            }


            for (int i = 0; i < searchFiledCount; i++)
            {
                if (ColumnHead.IndexOf(",") == -1)
                {
                    BrkStr = ColumnHead.Trim();

                    if (ColumnHead.IndexOf("RTRIM(", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        BrkStr = ColumnHead.Replace("RTRIM(", "");
                    }

                    if (BrkStr.IndexOf(")", StringComparison.Ordinal) != -1)
                    {
                        BrkStr = BrkStr.Replace(")", "");
                    }

                    dgvRegister.Columns[i].HeaderText = BrkStr.Trim();
                }
                else
                {
                    BrkStr = ColumnHead.Substring(0, ColumnHead.IndexOf(",")).Trim();

                    if (BrkStr.IndexOf("RTRIM(", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        BrkStr = BrkStr.Replace("RTRIM(", "");
                    }

                    if (BrkStr.IndexOf(")", StringComparison.Ordinal) != -1)
                    {
                        BrkStr = BrkStr.Replace(")", "");
                    }

                    dgvRegister.Columns[i].HeaderText = BrkStr.Trim();

                    ColumnHead = ColumnHead.Substring(ColumnHead.IndexOf(",") + 1);
                }
            }

            gridTotWidth = 0;
            for (int i = 0; i < searchFiledCount; i++)
            {
                gridTotWidth = gridTotWidth + dgvRegister.Columns[i].Width;
            }

            if (gridTotWidth > 2400)
            {
                gridTotWidth = 2400;
            }

            txtSearch.Focus();
        }
        private void SearchFunctionForProduct(string searchQry, string searchTable, string searchCnt, string searchOrder, int searchFiledCount, string MasterId)
        {
            int ColumnWidth;
            string selectQry;
            string ColumnHead;
            int gridTotWidth;
            string BrkStr;

            dgvRegister.Visible = true;
            ColumnWidth = (5400 / searchFiledCount);
            ColumnHead = searchQry;
            dgvRegister.Columns.Clear();

            // Build dynamic WHERE clause if none provided
            if (string.IsNullOrWhiteSpace(searchCnt))
            {
                string searchTerm = txtSearch.Text.Trim().Replace("'", "''");
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    if (PurchaseSettingsInfo._ShowPartNo)
                    {
                        searchCnt = $@"
                (
                    REPLACE(REPLACE(productName, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchTerm}%', ' ', ''), '/', '') OR
                    REPLACE(REPLACE(partNo, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchTerm}%', ' ', ''), '/', '') OR
                    REPLACE(REPLACE(alternateNo, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchTerm}%', ' ', ''), '/', '') OR
                    REPLACE(REPLACE(productCode, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchTerm}%', ' ', ''), '/', '')
                )";
                    }
                    else
                    {
                        searchCnt = $@"
                (
                    REPLACE(REPLACE(productName, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchTerm}%', ' ', ''), '/', '') OR                  
                    REPLACE(REPLACE(productCode, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchTerm}%', ' ', ''), '/', '')
                )";
                    }
                }
            }

            // Build SQL
            selectQry = "SELECT " + searchQry + " FROM " + searchTable;

            if (!string.IsNullOrWhiteSpace(searchCnt))
            {
                selectQry += " WHERE " + searchCnt;
            }

            if (!string.IsNullOrWhiteSpace(searchOrder))
            {
                selectQry += " ORDER BY " + searchOrder;
            }

            // Get data
            DataTable dt = DBClass.GetDataTable(selectQry);
            if (dt.Rows.Count == 0)
            {
                dgvRegister.DataSource = null;
                return;
            }

            dgvRegister.DataSource = dt;

            // Hide master ID column if needed
            if (!string.IsNullOrWhiteSpace(MasterId) && dgvRegister.Columns.Contains(MasterId))
            {
                dgvRegister.Columns[MasterId].Visible = false;
            }

            dgvRegister.ClearSelection();

            // Column headers
            for (int i = 0; i < searchFiledCount; i++)
            {
                if (ColumnHead.IndexOf(",") == -1)
                {
                    BrkStr = ColumnHead.Trim();
                    BrkStr = BrkStr.Replace("RTRIM(", "").Replace(")", "");
                    dgvRegister.Columns[i].HeaderText = BrkStr.Trim();
                }
                else
                {
                    BrkStr = ColumnHead.Substring(0, ColumnHead.IndexOf(",")).Trim();
                    BrkStr = BrkStr.Replace("RTRIM(", "").Replace(")", "");
                    dgvRegister.Columns[i].HeaderText = BrkStr.Trim();
                    ColumnHead = ColumnHead.Substring(ColumnHead.IndexOf(",") + 1);
                }
            }

            // Width adjustment
            gridTotWidth = 0;
            for (int i = 0; i < searchFiledCount; i++)
            {
                gridTotWidth += dgvRegister.Columns[i].Width;
            }

            if (gridTotWidth > 2400)
            {
                gridTotWidth = 2400;
            }

            txtSearch.Focus();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------
        public void DoWhenComingFromCustomerForm(frmCustomer frm)
        {
            this.frmCust = frm;
            isFromCustomerForm = true;
            base.ShowInTaskbar = false;
            SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            base.ShowDialog();
        }
        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromContraForm(frmContraVoucher frm)
        //{
        //    this.frmContra = frm;
        //    isFromContraVoucherForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}

        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromReceiptForm(frmReceipt frm)
        //{
        //    this.frmreceipt = frm;
        //    isFromReceiptVoucherForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromPaymentForm(frmPayment frm)
        //{
        //    this.frmpayment = frm;
        //    isFromPaymentVoucherForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromPOSPaymentForm(frmPOSPaymentMethod frm)
        //{
        //    this.frmPOSpayment = frm;
        //    isFromPOSPayment = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromPayableForm(frmPayableVoucher frm)
        //{
        //    this.frmpayable = frm;
        //    isFromPayableVoucherForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromReceivableForm(frmReceivableVoucher frm)
        //{
        //    this.frmReceivable = frm;
        //    isFromReceivableVoucherForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromJournalForm(frmJournalVoucher frm)
        //{
        //    this.frmjournal = frm;
        //    isFromJournalVoucherForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromPurchaseInvoiceForm(frmPurchaseInvoice frm)
        //{
        //    this.frmpurchase = frm;
        //    isFromPurchaseInvoiceForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromPurchaseInvoicePOSForm(frmPurchaseInvoicePOS frm)
        //{
        //    this.frmpurchasePOS = frm;
        //    isFromPurchaseInvoicePOSForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromSalesOrderForm(frmSalesOrder frm)
        //{
        //    this.frmsalesorder = frm;
        //    isFromSalesOrderForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromProformaInvoiceForm(frmProformaInvoice frm)
        //{
        //    this.frmProformaInvoice = frm;
        //    isFromProformaInvoiceForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromPurchaseReturnForm(frmPurchaseReturn frm)
        //{
        //    this.frmpurchasereturn = frm;
        //    isFromPurchaseReturnForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromSalesInvoiceForm(frmSalesInvoice frm)
        //{
        //    this.frmsalesinvoice = frm;
        //    isFromSalesInvoiceForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromSalesInvoiceFormSimple(frmSalesInvoiceSimple frm)
        //{
        //    this.frmsalesinvoicesimple = frm;
        //    isFromSalesInvoiceFormSimple = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromDamageStockForm(frmDamageStock frm)
        //{
        //    this.frmdamagestock = frm;
        //    isFromDamageStockForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromPhysicalStockForm(frmPhysicalStock frm)
        //{
        //    this.frmPphysicalstock = frm;
        //    isFromPhysicalStockForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        ////---------------------------------------------------------------------------------------------------------------------------------------
        //public void DoWhenComingFromSalesReturnForm(frmSalesReturn frm)
        //{
        //    this.frmsalesreturn = frm;
        //    isFromSalesReturnForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromProductHistoryForm(frmProductHistory frm)
        //{
        //    this.frmproductHstory = frm;
        //    isFromProductHistory = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromSalesQuotationForm(frmSalesQuotation frm)
        //{
        //    this.frmsalesquotation = frm;
        //    isFromsalesquotationForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromDeliveryNote(frmDeliveryNote frm)
        //{
        //    this.frmDeliveryNote = frm;
        //    isFromDeliveryNote = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromRejectionIn(frmRejectionIn frm)
        //{
        //    this.frmRejectionIn = frm;
        //    isFromRejectionIn = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromPurchaseOrderForm(frmPurchaseOrder frm)
        //{
        //    this.frmpurchaseOrder = frm;
        //    isFromPurchaseOrderForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromMaterialReceiptForm(frmMaterialReceipt frm)
        //{
        //    this.frmmaterialReceipt = frm;
        //    isFromMaterialReceiptForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromRejectionOutForm(frmRejectionOut frm)
        //{
        //    this.frmrejectionOut = frm;
        //    isFromRejectionOutForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        public void DoWhenComingFromProductCreationForm(frmProductCreation frm)
        {
            this.frmproductCreation = frm;
            isFromProductCreationForm = true;
            base.ShowInTaskbar = false;
            SearchFunctionForProduct(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            base.ShowDialog();
        }
        //public void DoWhenComingFromBarcodeForm(frmBarcodePrinting frm)
        //{
        //    this.frmbarcodePrinting = frm;
        //    isFrombarcodePrintingForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromUSedStockForm(frmUsedStock frm)
        //{
        //    this.frmusedstock = frm;
        //    isFromUSedStockForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromOfferRateForm(frmOfferRate frm)
        //{
        //    this.frmofferrate = frm;
        //    isFromOfferRateForm = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        //public void DoWhenComingFromgodownTransfer(frmTransfer frm)
        //{
        //    this.frmtransfer = frm;
        //    isFromTransfer = true;
        //    base.ShowInTaskbar = false;
        //    SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
        //    base.ShowDialog();
        //}
        private void frmLookupMaster_KeyDown(object sender, KeyEventArgs e)
        {
            int CurCol;
            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {
                CurCol = (int)e.KeyCode - (int)Keys.F1;
                if (dgvRegister.ColumnCount < CurCol + 1)
                {
                    return;
                }
                if (dgvRegister.Columns[CurCol].HeaderText.Trim().IndexOf(" ") > 0)
                {
                    strSearchColumn = dgvRegister.Columns[CurCol].HeaderText.Trim().Replace(" ", "");
                }
                else
                {
                    strSearchColumn = dgvRegister.Columns[CurCol].HeaderText.Trim();
                }

                lblSearchColumn.Text = "Search By: ";

                if (!string.IsNullOrWhiteSpace(strSearchColumn))
                {
                    lblSearchColumn.Text += strSearchColumn;
                }

                SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            }
          

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLookupMaster_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string SrchTxtCond = "";string searchText = "";
            //if (e.KeyValue != 13 && e.KeyValue != 9 && e.KeyValue != 37 && e.KeyValue != 38 && e.KeyValue != 39 && e.KeyValue != 40)
            //{
            if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.Tab && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            { 
                SrchTxtCond = strSearchCondition;
                if (!isFromProductCreationForm)
                {
                    if (string.IsNullOrWhiteSpace(SrchTxtCond))
                    {
                        if (chkIntermediateSearch.Checked)
                        {
                            SrchTxtCond = "" + strSearchColumn.Replace(" desc", " ") + " LIKE N'%" + txtSearch.Text.Trim().Replace(" ", "%").Replace("'", "''") + "%'";
                        }
                        else
                        {
                            SrchTxtCond = "" + strSearchColumn.Replace(" desc", " ") + " LIKE N'" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                        }
                    }
                    else
                    {
                        if (chkIntermediateSearch.Checked)
                        {
                            SrchTxtCond = "" + SrchTxtCond + " AND " + strSearchColumn.Replace(" desc", " ") + " LIKE N'%" + txtSearch.Text.Trim().Replace(" ", "%").Replace("'", "''") + "%'";
                        }
                        else
                        {
                            SrchTxtCond = "" + SrchTxtCond + " AND " + strSearchColumn + " LIKE N'" + txtSearch.Text.Trim().Replace("'", "''") + "%'";
                        }
                    }

                    SearchFunction(strSearchQry, strSearchTable, SrchTxtCond, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
                }
                else
                {
                     searchText = txtSearch.Text.Trim().Replace("'", "''");

                     SrchTxtCond = strSearchCondition;

                    if (!string.IsNullOrEmpty(searchText))
                    {
                        string pattern ="";
                        // Normalized search ignoring spaces and "/"
                        if (PurchaseSettingsInfo._ShowPartNo)
                        {
                            pattern = $@"
        (
            REPLACE(REPLACE(productName, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchText}%', ' ', ''), '/', '') OR
            REPLACE(REPLACE(partNo, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchText}%', ' ', ''), '/', '') OR
            REPLACE(REPLACE(alternateNo, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchText}%', ' ', ''), '/', '') OR
            REPLACE(REPLACE(productCode, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchText}%', ' ', ''), '/', '')
        )";
                        }
                        else
                        {
                            pattern = $@"
        (
            REPLACE(REPLACE(productName, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchText}%', ' ', ''), '/', '') OR          
            REPLACE(REPLACE(productCode, ' ', ''), '/', '') LIKE REPLACE(REPLACE(N'%{searchText}%', ' ', ''), '/', '')
        )";
                        }

                        if (!string.IsNullOrWhiteSpace(SrchTxtCond))
                            SrchTxtCond += " AND " + pattern;
                        else
                            SrchTxtCond = pattern;
                    }

                    SearchFunctionForProduct(strSearchQry, strSearchTable, SrchTxtCond, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
                }
                
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                dgvRegister.Focus();
            }


        }

        private void dgvRegister_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int CurCol;
            if (e.RowIndex < 0)
            {
                CurCol = e.ColumnIndex;
                if (dgvRegister.Columns[CurCol].HeaderText.Trim().IndexOf(" ") > 0)
                {
                    strSearchColumn = dgvRegister.Columns[CurCol].HeaderText.Trim().Replace(" ", "");
                }
                else
                {
                    strSearchColumn = dgvRegister.Columns[CurCol].HeaderText.Trim();
                }

                lblSearchColumn.Text = "Search By: ";
                if (!string.IsNullOrWhiteSpace(strSearchColumn))
                {
                    lblSearchColumn.Text = lblSearchColumn.Text.Trim() + strSearchColumn;
                }

                SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            }
            else
            {
                //strMasterIdColumnValue = dgvRegister.Rows[e.RowIndex].Cells[strMasterIdColumnName].Value.ToString();

                inCurrenRowIndex = e.RowIndex;
            }

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (dgvRegister.CurrentRow != null)
            {
                //inCurrenRowIndex = dgvRegister.CurrentRow.Index;
                strMasterIdColumnValue = dgvRegister.Rows[inCurrenRowIndex].Cells[strMasterIdColumnName].Value.ToString();

                this.Close(); 

            }
        }

        private void frmLookupMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            string strId = "";
            //if (inCurrenRowIndex >= 0)
            //{
            //    try { strId = dgvRegister.Rows[inCurrenRowIndex].Cells[strMasterIdColumnName].Value.ToString(); }
            //    catch { strId = ""; }
                
            //}
            strId = strMasterIdColumnValue;

            if (isFromCustomerForm)
            {
                frmCust.DowhenReturningFromSearchForm(strId);
            }
            //else if (isFromContraVoucherForm)
            //{
            //    frmContra.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromReceiptVoucherForm)
            //{
            //    frmreceipt.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromPaymentVoucherForm)
            //{
            //    frmpayment.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromJournalVoucherForm)
            //{
            //    frmjournal.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromPurchaseInvoiceForm)
            //{
            //    frmpurchase.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromPurchaseReturnForm)
            //{
            //    frmpurchasereturn.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromSalesInvoiceForm)
            //{
            //    frmsalesinvoice.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromSalesReturnForm)
            //{
            //    frmsalesreturn.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromProductHistory)
            //{
            //    frmproductHstory.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromProformaInvoiceForm)
            //{
            //    frmProformaInvoice.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromSalesOrderForm)
            //{
            //    frmsalesorder.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromsalesquotationForm)
            //{
            //    frmsalesquotation.DowhenReturningFromSearchForm(strId);
            //}
            //else if(isFromPurchaseOrderForm)
            //{
            //    frmpurchaseOrder.DowhenReturningFromSearchForm(strId);
            //}
            //else if(isFromMaterialReceiptForm)
            //{
            //    frmmaterialReceipt.DowhenReturningFromSearchForm(strId);
            //}
            //else if(isFromRejectionOutForm)
            //{
            //    frmrejectionOut.DowhenReturningFromSearchForm(strId);
            //}
            if (isFromProductCreationForm)
            {
                frmproductCreation.DowhenReturningFromSearchForm(strId);
            }
            //else if (isFrombarcodePrintingForm)
            //{
            //    frmbarcodePrinting.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromPayableVoucherForm)
            //{
            //    frmpayable.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromReceivableVoucherForm)
            //{
            //    frmReceivable.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromDeliveryNote)
            //{
            //    frmDeliveryNote.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromRejectionIn)
            //{
            //    frmRejectionIn.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromPurchaseInvoicePOSForm)
            //{
            //    frmpurchasePOS.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromDamageStockForm)
            //{
            //    frmdamagestock.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromPhysicalStockForm)
            //{
            //    frmPphysicalstock.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromUSedStockForm)
            //{
            //    frmusedstock.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromOfferRateForm)
            //{
            //    frmofferrate.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromSalesInvoiceFormSimple)
            //{
            //    frmsalesinvoicesimple.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromPOSPayment)
            //{
            //    frmPOSpayment.DowhenReturningFromSearchForm(strId);
            //}
            //else if (isFromTransfer)
            //{
            //    frmtransfer.DowhenReturningFromSearchForm(strId);
            //}
        }

        private void dgvRegister_DoubleClick(object sender, EventArgs e)
        {
            if (dgvRegister.CurrentRow != null)
            {
               // strMasterIdColumnValue = dgvRegister.Rows[inCurrenRowIndex].Cells[strMasterIdColumnName].Value.ToString();

               // this.Close();
                //btnSelect_Click(e, e); 
                if (dgvRegister.Rows.Count > 0)
                {
                    //if (dgvRegister.Rows.Count == 1)
                    //{
                    //    inCurrenRowIndex = dgvRegister.CurrentRow.Index;
                    //}
                    //else
                    //    inCurrenRowIndex = dgvRegister.CurrentRow.Index - 1;
                    inCurrenRowIndex = dgvRegister.CurrentRow.Index;

                    btnSelect_Click(e, e);
                }
            }


        }
        private void dgvRegister_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //strMasterIdColumnValue = dgvRegister.Rows[e.RowIndex].Cells[strMasterIdColumnName].Value.ToString();
            inCurrenRowIndex = e.RowIndex;
        }

        private void dgvRegister_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //strMasterIdColumnValue = dgvRegister.Rows[e.RowIndex].Cells[strMasterIdColumnName].Value.ToString();
            inCurrenRowIndex = e.RowIndex;
        }

        private void dgvRegister_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvRegister.Rows.Count > 0)
            {
                if (e.KeyChar == (char)13)
                {
                    //strMasterIdColumnValue = dgvRegister.Rows[dgvRegister.CurrentRow.Index - 1].Cells[strMasterIdColumnName].Value.ToString();

                    //if (dgvRegister.Rows.Count == 1)
                    //{
                    //    inCurrenRowIndex = dgvRegister.CurrentRow.Index;
                    //}
                    //else
                    //    inCurrenRowIndex = dgvRegister.CurrentRow.Index - 1; 

                    //btnSelect_Click(e, e); 
                    if (dgvRegister.Rows.Count > 0)
                    {
                        //if (dgvRegister.Rows.Count == 1)
                        //{
                        //    inCurrenRowIndex = dgvRegister.CurrentRow.Index;
                        //}
                        //else
                        //    inCurrenRowIndex = dgvRegister.CurrentRow.Index - 1;
                        inCurrenRowIndex = dgvRegister.CurrentRow.Index;

                        btnSelect_Click(e, e);
                    }
                }
            }
         

        }

        private void dgvRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txtSearch_KeyUp(sender, new KeyEventArgs(Keys.None));
        }
    }
}
