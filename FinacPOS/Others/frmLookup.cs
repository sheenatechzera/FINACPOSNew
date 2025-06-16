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
    partial class frmLookup : Form
    {
        public frmLookup()
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
        public String strBillNo = "";
        public string strcustomerName;
        public string strcustomerAddress;
        public string strcustomerPhone;
        public string strcustomerVATNo;


        int inCurrenRowIndex = 0;// To keep row index


        frmPOSReceiptVoucher frmReceipt;
        frmPOSPaymentVoucher frmPayment;
      
        FrmPOSTable frmTableObj;
        frmBillPrint frmBillPrintObj;
        frmBillPrint objReceiptPaymentBillNo = new frmBillPrint();


        bool isFromReceiptForm = false;
        bool isFromPaymentForm = false;
        bool isFromPOSTable = false;
        bool isFrmBillPrint = false;
        bool isReceiptPaymentBill = false;
       

        DBClass DBClass = new DBClass();
        #endregion

        private void frmLookup_Load(object sender, EventArgs e)
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

        //---------------------------------------------------------------------------------------------------------------------------------------
     
        public void DoWhenComingFromReceiptForm(frmPOSReceiptVoucher frm)
        {
            this.frmReceipt = frm;
            isFromReceiptForm = true;
            base.ShowInTaskbar = false;
            SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            base.ShowDialog();
        }
        public void DoWhenComingFromPaymentForm(frmPOSPaymentVoucher frm)
        {
            this.frmPayment = frm;
            isFromPaymentForm = true;
            base.ShowInTaskbar = false;
            SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            base.ShowDialog();
        }

        public void DoWhenComingFromPOSTableForm(FrmPOSTable frm)
        {

            this.frmTableObj = frm;
            isFromPOSTable = true;
            base.ShowInTaskbar = false;
            SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            base.ShowDialog();
        }
        public void DoWhencommingfrmBillPrintForReceiptPaymentbill(frmBillPrint frm)
        { 
        this.objReceiptPaymentBillNo = frm;
        isReceiptPaymentBill = true;
        base.ShowInTaskbar = false;
        SearchFunction(strSearchQry,strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount,strMasterIdColumnName);
        base.ShowDialog();
        }
        public void DoWhenComingFromBillPrintForm(frmBillPrint frm)
        {
            this.frmBillPrintObj = frm;
            isFrmBillPrint = true;
            base.ShowInTaskbar = false;
            SearchFunction(strSearchQry, strSearchTable, strSearchCondition, strSearchOrder, IntSearchFiledCount, strMasterIdColumnName);
            base.ShowDialog();
        }


        private void frmLookup_KeyDown(object sender, KeyEventArgs e)
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

        private void frmLookup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string SrchTxtCond = "";
            if (e.KeyValue != 13 && e.KeyValue != 9 && e.KeyValue != 37 && e.KeyValue != 38 && e.KeyValue != 39 && e.KeyValue != 40)
            {
                SrchTxtCond = strSearchCondition;

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
            try
            {
              
                if (dgvRegister.CurrentRow != null)
                {
                    //inCurrenRowIndex = dgvRegister.CurrentRow.Index;
                    strMasterIdColumnValue = dgvRegister.Rows[inCurrenRowIndex].Cells[strMasterIdColumnName].Value.ToString();
                   if(isReceiptPaymentBill)
                    { 
                        strBillNo = dgvRegister.Rows[inCurrenRowIndex].Cells["BillNo"].Value.ToString();
                    }
               
                    if (isFrmBillPrint)
                    {
                        strcustomerName = dgvRegister.Rows[inCurrenRowIndex].Cells["CustomerName"].Value.ToString();
                        strcustomerAddress = dgvRegister.Rows[inCurrenRowIndex].Cells["CustomerAddress"].Value.ToString();
                        strcustomerPhone = dgvRegister.Rows[inCurrenRowIndex].Cells["CustomerPhone"].Value.ToString();
                        strcustomerVATNo = dgvRegister.Rows[inCurrenRowIndex].Cells["CustomerVATNo"].Value.ToString();

                    }

                    this.Close();

                }
            }
            catch { }
           
        }

        private void frmLookup_FormClosing(object sender, FormClosingEventArgs e)
        {
            string strId = "";
            string StrBillNumber = "";
            //if (inCurrenRowIndex >= 0)
            //{
            //    try { strId = dgvRegister.Rows[inCurrenRowIndex].Cells[strMasterIdColumnName].Value.ToString(); }
            //    catch { strId = ""; }
                
            //}
            strId = strMasterIdColumnValue;
            StrBillNumber = strBillNo;

            if (isFromReceiptForm)
            {
                frmReceipt.DowhenReturningFromSearchForm(strId);
            }

            if (isFromPaymentForm)
            {
                frmPayment.DowhenReturningFromSearchForm(strId);
            }
            if (isFromPOSTable)
            {
               //frmTableObj.DowhenReturningFromSearchForm(strId);
            }
            if (isFrmBillPrint)
            {
                frmBillPrintObj.DoWhenReturningFromSearchForm(strId, strcustomerName, strcustomerAddress, strcustomerPhone, strcustomerVATNo);
            }
            if(isReceiptPaymentBill)
            {
                objReceiptPaymentBillNo.DoWhenReturningFromSearchofReceiptPaymentBill(StrBillNumber);
            }
           
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

        //internal void DoWhenComingFromPOSTableForm(FrmPOSTable frmPOSTable)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
