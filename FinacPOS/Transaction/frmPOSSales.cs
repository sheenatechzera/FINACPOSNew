using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Drawing.Printing;
using System.IO;
using OnBarcode.Barcode.WinForms;
using System.IO.Ports;
using FinacPOS.Masters;

namespace FinacPOS
{
    public partial class frmPOSSales : Form
    {
        public frmPOSSales()
        {
            InitializeComponent();


        }
        string strGroupId = "0";
        DataTable dtblProductFiltered = new DataTable();
        DataTable dtblProductAll = new DataTable();

        POSSettingsInfo InfoPosSetting = new POSSettingsInfo();
        POSSalesMasterSP salesMasterSP = new POSSalesMasterSP();


        #region PUBLICVARIABLES

        public string strSessionNo = "";
        public string strSessionDate = "";
        string strHoldMasterIdToEdit = "";
        int dgvCurRow = 0;
        int dgvSlno = 0;
        string strFocusedControl = "";
        bool isSavefromButton = true;
        bool blTextBoxFocus;
        bool blGridFocus;
        TextBox txtTouchTextBox = new TextBox();
        //DataGridViewCell txtTouchGridTextbox;
        string strGridValue = "";
        int CurColIndex = 0;
        int CurEditRowIndex = 0;
        string strBarcode = "";

        //varis form thermal print
        DataTable dtblCompanyDetailsThermal;
        DataTable dtblGridDetailsThermal;
        DataTable dtblOtherDetailsThermal;
        DataTable dtblTaxDetailsThermal;
        int PrintPageHight;
        //--------------------------

        bool isRateChanged = false;

        POSCounterInfo counterInfo = new POSCounterInfo();
        GeneralSP SPGeneral = new GeneralSP();
        POSSalesMasterSP POSSalesMasterSP = new POSSalesMasterSP();
        POSSalesDetails1SP POSSalesDetails1SP = new POSSalesDetails1SP();
        ComboValidation objComboValidation = new ComboValidation();
        ProductSP spProduct = new ProductSP();

        DataGridViewTextBoxEditingControl TextBoxControl;



        //for Posting Account setting
        string strCashSalesLedgerId = "";
        string strCCSalesLedgerId = "";
        string strUPISalesLedgerId = "";
        string strSalesLedgerId = "";
        bool IsAuthenticationApproved = false;

        DataTable dtblProductWithImage;
        #endregion

        #region FUNCTIONS

        public void FormLoadFunction()
        {
            timerSessionDate.Start();
            timer1.Start();
            lblSessionNO.Text = strSessionNo;
            lblSessionDate.Text = strSessionDate;
            lblCounter.Text = PublicVariables._counterName;
            lblUser.Text = PublicVariables._EmpName;

            if (DateTime.Compare(Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(strSessionDate)) > 0)
            {
                MessageBox.Show("Opened Session Date is not Today's Date");
            }

            // POSSalesPostingAccount(); //blocked on 29/Mar/2025

            POSCounterSP counterSP = new POSCounterSP();
            counterInfo = counterSP.POSCounterViewbyCounterId(PublicVariables._counterId);

            //29/Mar/2025
            strCashSalesLedgerId = counterInfo.CashAccountLedgerId;
            strCCSalesLedgerId = counterInfo.BankAccountLedgerId;
            strUPISalesLedgerId = counterInfo.UPIAccountLedgerId;
            strSalesLedgerId = counterInfo.SalesAccountLedgerId;

            //POS Settings
            POSSettingsInfo InfoPOSSettings = new POSSettingsInfo();
            POSSettingsSP SpPOSSettings = new POSSettingsSP();
            InfoPOSSettings = SpPOSSettings.POSSettingsViewByBranchId(PublicVariables._branchId);
            DataTable dtbl = new DataTable();
            showproductInload(dtbl);

            productFill(); //Added on 10/Mar/2025 Varis

            //DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            //deleteButtonColumn.Name = "Delete";
            //deleteButtonColumn.HeaderText = "Action";
            //deleteButtonColumn.Text = "Delete";
            //deleteButtonColumn.UseColumnTextForButtonValue = true;
            //deleteButtonColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //deleteButtonColumn.DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            //deleteButtonColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //deleteButtonColumn.DefaultCellStyle.ForeColor = Color.Red;


            //dgvProduct.Columns.Add(deleteButtonColumn);
            //dgvProduct.CellClick += new DataGridViewCellEventHandler(dgvProduct_CellClick);
        }
        private void TextboxClearButton(TextBox txt)
        {
            if (txt.Text.Trim() != "")
            {
                txt.Text = txt.Text.Substring(0, txt.Text.Length - 1);
            }
        }
        public void productFill()
        {
            ProductSP SpProduct = new ProductSP();
            dtblProductWithImage = SpProduct.POSProductSearchWithImage(PublicVariables._branchId);
            if (dtblProductWithImage.Rows.Count > 0)
            {
                dtblProductWithImage.Columns.Add("pic", typeof(byte[]));
                for (int i = 0; i < dtblProductWithImage.Rows.Count; i++)
                {
                    dtblProductWithImage.Rows[i]["pic"] = ConvertImageToByteArray(dtblProductWithImage.Rows[i]["productImage"].ToString());
                }
            }
        }
        private static byte[] ConvertImageToByteArray(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                return File.ReadAllBytes(imagePath);
            }
            return null; // Return null if file not found
        }
        public void showproductInload(DataTable dtbl)   //added on 17/03/2025 By Nishana
        {
            if (counterInfo.ShowProductInSalesInvoice == true)
            {
                this.Size = new Size(1381, 747);
                FlpanelProductGroup.Visible = true;
                flowLayoutPanel.Visible = true;
                flowLayoutPanel.Size = new Size(304, 600);
                flowLayoutPanel.Location = new Point(1061, 108);
                FlpanelProductGroup.Size = new Size(308, 99);
                FlpanelProductGroup.Location = new Point(1061, 8);
            }
            LoadProductGroup();
            LoadProducts(dtbl);
        }
        private void LoadProductGroup()
        {
            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductGroupViewAllByCategory("Category 4");
            strGroupId = "0";
            FlpanelProductGroup.Controls.Clear();
            foreach (DataRow row in dtbl.Rows)
            {
                AddProductGroupButton(row);
            }
        }
        private void AddProductGroupButton(DataRow row)
        {
            Button btn = new Button
            {
                Width = 100,  // Adjust as needed
                Height = 70, // Adjust as needed
                Text = row["groupName"].ToString(),
                //TextAlign = ContentAlignment.BottomCenter,
                TextAlign = ContentAlignment.MiddleCenter,  // Keep text at the bottom
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.Black,
                Tag = row["groupId"] // Store product name or ID for reference
            };

            btn.Click += ProductGroupButton_Click;
            FlpanelProductGroup.Controls.Add(btn);
        }
        private void LoadProducts(DataTable dt)
        {
            flowLayoutPanel.Controls.Clear();

            foreach (DataRow row in dt.Rows)
            {
                AddProductButton(row);
            }

        }
        private void AddProductButton(DataRow row)
        {
            //Button btn = new Button
            //{
            //    Width = 150,  // Adjust as needed
            //    Height = 150, // Adjust as needed
            //    Text = row["productName"].ToString() + Environment.NewLine + "" + row["salesPrice"].ToString(),
            //    //TextAlign = ContentAlignment.BottomCenter,
            //    TextAlign = ContentAlignment.BottomCenter,  // Keep text at the bottom
            //    ImageAlign = ContentAlignment.TopCenter, 
            //    Font = new Font("Arial", 10, FontStyle.Bold),
            //    BackColor = Color.White,
            //    ForeColor = Color.Red, 
            //    Tag = row["productCode"] // Store product name or ID for reference
            //};

            //// Load image if available
            //if (row["pic"] != DBNull.Value)
            //{
            //    byte[] imageBytes = (byte[])row["pic"];
            //    using (MemoryStream ms = new MemoryStream(imageBytes))
            //    {
            //        btn.Image = Image.FromStream(ms);
            //        btn.ImageAlign = ContentAlignment.TopCenter;
            //    }
            //}

            //btn.Click += ProductButton_Click;
            //flowLayoutPanel1.Controls.Add(btn);

            Panel panel = new Panel
            {
                Width = 130,
                Height = 130,
                BackColor = Color.FromArgb(245, 245, 245),
                Tag = row["barcode"]

            };

            // Add Image
            byte[] imageBytes = row["pic"] != DBNull.Value ? (byte[])row["pic"] : null; // (byte[])row["pic"];

            PictureBox pictureBox = new PictureBox
            {
                Width = 130,
                Height = 60,
                Image = imageBytes != null ? ByteArrayToImage(imageBytes) : null,  // ByteArrayToImage(imageBytes), 
                SizeMode = PictureBoxSizeMode.StretchImage,
                Top = 5,
                Left = 5,
                Tag = row["barcode"]

            };

            Label lbl = new Label
            {
                Text = row["productName"].ToString() + Environment.NewLine + row["salesPrice"].ToString() + Environment.NewLine + row["unitName"].ToString(),
                Width = 120,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Font = new Font("Arial", 8, FontStyle.Bold),
                Top = 70,
                Tag = row["barcode"]

            };



            // Add controls to panel
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(lbl);

            // Add panel to FlowLayoutPanel
            panel.Click += ProductButton_Click;
            pictureBox.Click += ProductImage_Click;
            lbl.Click += ProductLabel_Click;

            flowLayoutPanel.Controls.Add(panel);
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private void ProductGroupButton_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            Button clickedButton = sender as Button;
            string groupId = clickedButton.Tag.ToString();


            //MessageBox.Show("Selected Group:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

            strGroupId = groupId;
            //MessageBox.Show("a");
            if (strGroupId == "All")
            {
                LoadProducts(dtblProductWithImage);
            }
            else
            {
                string searchvalue = "groupid='" + strGroupId + "'";

                DataView dv = dtblProductWithImage.DefaultView;
                //dv.RowFilter = "LedgerName LIKE '%" + searchvalue + "%' OR ledgerCode LIKE '%" + searchvalue + "%'";
                dv.RowFilter = searchvalue;

                dtblProductFiltered = dv.ToTable();
                LoadProducts(dtblProductFiltered);
            }
            //MessageBox.Show("b");
            //  txtSearch.Focus();
        }
        private void ProductButton_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            Panel clickedButton = sender as Panel;
            string strBarcode = clickedButton.Tag.ToString();

            //MessageBox.Show("Selected Product:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SelectProduct(strBarcode);

        }
        private void ProductImage_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            PictureBox clickedButton = sender as PictureBox;
            string strBarcode = clickedButton.Tag.ToString();
            //MessageBox.Show("Selected Product:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SelectProduct(strBarcode);
        }
        private void ProductLabel_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            Label clickedButton = sender as Label;
            String strBarcode = clickedButton.Tag.ToString();
            //MessageBox.Show("Selected Product:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SelectProduct(strBarcode);
        }
        public void setGridAllFilter(DataTable dt, string strSearchValue)
        {
            if (dt.Rows.Count > 0)
            {
                string searchvalue = CreateFilterQry(strSearchValue, dt);

                DataView dv = dt.DefaultView;
                //dv.RowFilter = "LedgerName LIKE '%" + searchvalue + "%' OR ledgerCode LIKE '%" + searchvalue + "%'";
                dv.RowFilter = searchvalue;

                dtblProductFiltered = dv.ToTable();
                LoadProducts(dtblProductFiltered);
            }
        }
        private string CreateFilterQry(string strVal, DataTable dt)
        {
            string strqry = "";
            if (!string.IsNullOrEmpty(strVal))
            {
                strVal = strVal.Replace("'", "''"); // Escape single quotes to prevent syntax errors
            }
            for (int i = 0; i < dt.Columns.Count; ++i)
            {

                if (dt.Columns[i].ColumnName != "salesPrice" && dt.Columns[i].ColumnName != "productImage" && dt.Columns[i].ColumnName != "pic")
                {
                    if (strqry == "")
                    {
                        strqry = dt.Columns[i].ColumnName + " LIKE '%" + strVal + "%' ";
                    }
                    else
                    {
                        strqry = strqry + " OR " + dt.Columns[i].ColumnName + " LIKE '%" + strVal + "%' ";
                    }
                }
            }
            return strqry;

        }

        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    setGridAllFilter(dtblProductAll, txtSearch.Text.Trim());
        //}
        string productCodeToReturn = "";
        private void SelectProduct(string strBarcode)
        {

            // if (strProductCode != "")
            //    {
            //        productCodeToReturn = strProductCode;
            //    }
            FillrowAfterPickingProduct(strBarcode);

        }
        public void ClearFunction()
        {
            lblBillNo.Text = POSBillNumberMax();
            btnCash.Enabled = true;
            btnCreditCard.Enabled = true;
            btnUPI.Enabled = true;
            lblTotalQty.Text = "0";
            txtSubTotal.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            txtDiscAmt.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            txtTaxable.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            txtTaxAmt.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            txtTotal.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            txtDiscPer.Text = Math.Round(0m, 2).ToString("0.00");
            txtCustCode.Text = "";
            txtCustomerName.Text = "";
            txtVatNo.Text = "";
            txtAdress.Text = "";
            txtphone.Text = "";
            lblLedgerId.Text = "";
            dgvProduct.Rows.Clear();
            dgvProduct.RowCount = 1;
            dgvCurRow = 0;
            dgvSlno = 1;
            isSavefromButton = true;

            strHoldMasterIdToEdit = "";
            lblBarcodeScanningType.Text = "";
            lblBarcodeScanningType.Visible = false;

            if (counterInfo.DisplayStatus == true)
            {
                PoleDisplay("New");
            }

            barcodeFocus();
        }
        private void PoleDisplay(string strFunction)
        {
            serialPort1.PortName = counterInfo.DisplayPort;
            serialPort1.BaudRate = 9600;
            serialPort1.Parity = System.IO.Ports.Parity.None;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = System.IO.Ports.StopBits.One;
            serialPort1.DtrEnable = true;
            serialPort1.RtsEnable = true;
            serialPort1.ReceivedBytesThreshold = 1;
            serialPort1.Open();

            if (strFunction == "New")
            {
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)14 + (char)13 + "" + (char)10 + (char)14);
                serialPort1.Write("   NEXT CUSTOMER" + (char)13 + (char)10);

            }
            else if (strFunction == "barcodeScan")
            {
                serialPort1.Write("" + (char)31);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)13 + (char)24 + (dgvProduct.CurrentRow.Cells["ItemName"].Value.ToString().Substring(1, dgvProduct.CurrentRow.Cells["ItemName"].Value.ToString().Length < 18 ? dgvProduct.CurrentRow.Cells["ItemName"].Value.ToString().Length : 18)).ToString() + Environment.NewLine + (char)10 + (char)14);
                serialPort1.Write((char)13 + (char)24 + dgvProduct.CurrentRow.Cells["SalesRate"].Value.ToString() + "         " + txtTotal.Text.ToString() + (char)13 + (char)10);
            }
            else if (strFunction == "Total")
            {
                serialPort1.Write("" + (char)31);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)14 + (char)13 + "" + (char)10 + (char)14);
                serialPort1.Write("Total Amount : " + txtTotal.Text.ToString() + (char)13 + (char)10);
            }
            else if (strFunction == "CashCollect")
            {
                serialPort1.Write("" + (char)31);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)14 + "Cash :    " + lblTenderTotalAmount.Text.ToString() + (char)13 + (char)10);
                serialPort1.Write((char)13 + "Balance : " + lblTenderBalanceAmount.Text.ToString() + (char)13 + (char)10);
            }
            else if (strFunction == "Close")
            {
                serialPort1.Write("" + (char)31);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)13 + (char)24 + "              " + (char)13 + (char)10);
                serialPort1.Write((char)14 + (char)13 + "" + (char)10 + (char)14);
                serialPort1.Write("   COUNTER CLOSED" + (char)13 + (char)10);
            }
            serialPort1.Close();
            serialPort1.Dispose();
        }
        public void barcodeFocus()
        {
            try
            {
                txtBarcode.Clear();
                txtBarcode.Focus();

                if (dgvProduct.RowCount > 1)
                {
                    dgvProduct.CurrentCell = dgvProduct.Rows[dgvCurRow - 1].Cells["Barcode"];
                }

                strFocusedControl = "txtBarcode";
                blGridFocus = false;
                blTextBoxFocus = true;
                txtTouchTextBox = txtBarcode;
                txtQty.Clear();
            }
            catch (Exception ex)
            {

            }
        }
        //public void POSSalesPostingAccount()
        //{
        //    DataTable dtbl = new DataTable();
        //    dtbl = SPGeneral.GetPostingAccount("POS");
        //    if (dtbl.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dtbl.Rows.Count; i++)
        //        {
        //            if (dtbl.Rows[i]["PostingAccount"].ToString() == "Sales")
        //            {
        //                strSalesLedgerId = dtbl.Rows[i]["ledgerId"].ToString();
        //            }
        //            if (dtbl.Rows[i]["PostingAccount"].ToString() == "Cash Sales")
        //            {
        //                strCashSalesLedgerId = dtbl.Rows[i]["ledgerId"].ToString();
        //            }
        //            if (dtbl.Rows[i]["PostingAccount"].ToString() == "Credit Card Sales")
        //            {
        //                strCCSalesLedgerId = dtbl.Rows[i]["ledgerId"].ToString();
        //            }
        //            if (dtbl.Rows[i]["PostingAccount"].ToString() == "UPI Sales")
        //            {
        //                strUPISalesLedgerId = dtbl.Rows[i]["ledgerId"].ToString();
        //            }
        //        }
        //    }

        //}
        public string POSBillNumberMax()
        {
            string PartBillNo = "";

            try
            {
                DataTable dtbl = new DataTable();
                dtbl = SPGeneral.GetPOSLastBillNo(PublicVariables._counterId, "Sales");


                if (dtbl.Rows.Count > 0)
                {
                    if (!dtbl.Rows[0].IsNull(0))
                        PartBillNo = dtbl.Rows[0]["LastBillNo"].ToString();
                    else
                        PartBillNo = "1";
                }
                else
                    PartBillNo = "1";

                PartBillNo = (PartBillNo.ToString()).PadLeft(7, '0');

                //PartBillNo = PublicVariables._counterId + "" + DateTime.Now.Date.ToString("yy") + "" + PartBillNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return PartBillNo;
        }
        public void CallFromSessionManagement(frmSessionManagement frm)
        {
            frm.Close();
            base.Show();
        }
        public void CallFromSessionControl(frmSessionControl frm)
        {
            frm.Close();
            base.Show();
        }
        public void FillDatatatablesforPrint(string strTenderPaid, string strTenderBalance, string strTenderCash, string strTenderCC, string strTenderUPI, bool isDuplicatePrint, string strDuplicateBillNo, string strHoldBillNo)
        {
            //--------Company Details Datatable--------------
            DataTable dtblCompanyDetails = new DataTable();
            BranchSP SpBranch = new BranchSP();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);

            //-------Grid details-------------------------
            DataTable dtblGridDetails = new DataTable();

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

            if (isDuplicatePrint == false)
            {
                //int inRowIndex = 0;
                foreach (DataGridViewRow gridrow in dgvProduct.Rows)
                {
                    if (!gridrow.IsNewRow)
                    {
                        DataRow dr = dtblGridDetails.NewRow();
                        dr["Sl No"] = gridrow.Cells["SlNo"].Value.ToString();
                        dtblGridDetails.Rows.Add(dr);
                        dr["Barcode"] = gridrow.Cells["Barcode"].Value.ToString();
                        dr["ProductName"] = gridrow.Cells["ItemName"].Value.ToString();
                        dr["Tax%"] = gridrow.Cells["TaxPerc"].Value.ToString();
                        dr["Price"] = gridrow.Cells["SalesRate"].Value.ToString();
                        dr["Qty"] = gridrow.Cells["Qty"].Value.ToString();
                        dr["Gr.Value"] = gridrow.Cells["GrossValue"].Value.ToString();
                        dr["Tax Amt"] = gridrow.Cells["TaxAmt"].Value.ToString();
                        dr["Disc Amt"] = gridrow.Cells["DiscAmt"].Value.ToString();
                        dr["NETVALUE"] = gridrow.Cells["NetValue"].Value.ToString();
                        dr["Total Amt"] = gridrow.Cells["Total"].Value.ToString();
                        dr["NameArabic"] = gridrow.Cells["ArabicName"].Value.ToString();
                    }


                }
            }
            else
            {
                DataTable dtbl = new DataTable();
                dtbl = POSSalesMasterSP.GetPOSLastBillProductsforLastBillPrint(strDuplicateBillNo);

                if (dtbl.Rows.Count > 0)
                {
                    dtblTaxDetailsThermal = GetTaxSumforDuplicatePrint(dtbl);

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        DataRow dr = dtblGridDetails.NewRow();
                        dr["Sl No"] = dtbl.Rows[i]["LineNumber"].ToString();
                        dtblGridDetails.Rows.Add(dr);
                        dr["Barcode"] = dtbl.Rows[i]["barcode"].ToString();
                        dr["ProductName"] = dtbl.Rows[i]["productName"].ToString();
                        dr["Tax%"] = dtbl.Rows[i]["taxPer"].ToString();
                        dr["Price"] = Convert.ToDecimal(dtbl.Rows[i]["rate"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Qty"] = dtbl.Rows[i]["qty"].ToString();
                        dr["Gr.Value"] = Convert.ToDecimal(dtbl.Rows[i]["grossValue"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Tax Amt"] = Convert.ToDecimal(dtbl.Rows[i]["taxAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Disc Amt"] = Convert.ToDecimal(dtbl.Rows[i]["discAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["NETVALUE"] = Convert.ToDecimal(dtbl.Rows[i]["netAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Total Amt"] = Convert.ToDecimal(dtbl.Rows[i]["Amount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["NameArabic"] = dtbl.Rows[i]["ArabicName"].ToString();
                    }
                }
            }



            //--------- Other Details Datatable---------------
            DataTable dtblOtherDetails = new DataTable();

            dtblOtherDetails.Columns.Add("PartyName");
            dtblOtherDetails.Columns.Add("PartyAddress");
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
            dtblOtherDetails.Columns.Add("qrCode", typeof(byte[]));
          

            if (isDuplicatePrint == false)
            {
                DataRow dRowDetails = dtblOtherDetails.NewRow();
                dRowDetails["PartyName"] = "";
                dRowDetails["PartyAddress"] = "";
                dRowDetails["BillDate"] = lblBillDate.Text;
                dRowDetails["BillTime"] = lblBillTime.Text;
                dRowDetails["SessionDate"] = lblSessionDate.Text;
                dRowDetails["SessionNo"] = lblSessionNO.Text;
                dRowDetails["CounterId"] = lblCounter.Text;
                dRowDetails["UserName"] = lblUser.Text;
                if (strHoldBillNo != "")
                {
                    dRowDetails["InvoiceNo"] = strHoldBillNo;
                    dRowDetails["BillName"] = "HOLD BILL";
                }
                else
                {
                    dRowDetails["InvoiceNo"] = lblBillNo.Text;
                    dRowDetails["BillName"] = "TAX INVOICE / فاتورة ضريبية";
                }
                dRowDetails["SubTotal"] = txtSubTotal.Text;
                dRowDetails["BillDiscount"] = txtDiscAmt.Text;
                dRowDetails["TaxableAmount"] = txtTaxable.Text;
                dRowDetails["TaxAmount"] = txtTaxAmt.Text;
                dRowDetails["GrandTotal"] = txtTotal.Text;
                dRowDetails["AmountInWords"] = "";

                dRowDetails["QtyTotal"] = lblTotalQty.Text;
                dRowDetails["TenderPaid"] = strTenderPaid;
                dRowDetails["TenderBalance"] = strTenderBalance;
                dRowDetails["TenderCash"] = strTenderCash;
                dRowDetails["TenderCC"] = strTenderCC;
                dRowDetails["TenderUPI"] = strTenderUPI;

                //------------------------ QR Code Generation ----------- by Navas --------------------
                Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                string companyname = dtblCompanyDetails.Rows[0][2].ToString();
                string vatno = dtblCompanyDetails.Rows[0][9].ToString();
                string invoicedate = Convert.ToDateTime(lblBillDate.Text).ToString("yyyy-MM-dd");
                string invoicetime = DateTime.Now.ToString("HH:mm:ss");
                invoicedate = invoicedate + "T" + invoicetime;
                string invoicetotal = txtTotal.Text.Replace("SR", "");
                string invoicevatamount = txtTaxAmt.Text.Replace("SR", "");

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

                //string strQRvariable = dtblCompanyDetails.Rows[0][2].ToString() + "\n" + "VAT No:" + dtblCompanyDetails.Rows[0][9].ToString() + "\n" + "VAT total:" + lblTaxAmt.Text + "\n" + "Grand Total: " + lblGrand.Text + "\n" + "Invoice Date:" + dtpVoucherDate.Text;
                //string strQRvariable = dtblCompanyDetails.Rows[0][2].ToString() + "\n" + "VAT No:" + dtblCompanyDetails.Rows[0][9].ToString();
                var utf8text = System.Text.Encoding.UTF8.GetBytes(strQRvariable);
                string qrdata = System.Convert.ToBase64String(utf8text);
                Image img = qrBarcode.Draw(qrdata, 500); //pictureBox1.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                dRowDetails["qrCode"] = arr;
                //---------------------------------
                dtblOtherDetails.Rows.Add(dRowDetails);

            }
            else
            {

                DataTable dtbl = new DataTable();
                dtbl = POSSalesMasterSP.GetPOSLastBillDetialsforLastBillPrint(strDuplicateBillNo);

                if (dtbl.Rows.Count > 0)
                {
                    DataRow dRowDetails = dtblOtherDetails.NewRow();
                    dRowDetails["PartyName"] = "";
                    dRowDetails["PartyAddress"] = "";
                    dRowDetails["BillDate"] = Convert.ToDateTime(dtbl.Rows[0]["billDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["BillTime"] = dtbl.Rows[0]["billTime"].ToString();
                    dRowDetails["SessionDate"] = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["SessionNo"] = dtbl.Rows[0]["sessionNo"].ToString();
                    dRowDetails["CounterId"] = dtbl.Rows[0]["counterId"].ToString();
                    dRowDetails["UserName"] = dtbl.Rows[0]["userId"].ToString();
                    dRowDetails["InvoiceNo"] = dtbl.Rows[0]["invoiceNo"].ToString();
                    dRowDetails["SubTotal"] = Convert.ToDecimal(dtbl.Rows[0]["subTotalAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["BillDiscount"] = Convert.ToDecimal(dtbl.Rows[0]["billDiscAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TaxableAmount"] = Convert.ToDecimal(dtbl.Rows[0]["taxableAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TaxAmount"] = Convert.ToDecimal(dtbl.Rows[0]["totalTaxAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["GrandTotal"] = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["AmountInWords"] = "";
                    dRowDetails["BillName"] = "TAX INVOICE COPY / فاتورة ضريبية";
                    dRowDetails["QtyTotal"] = dtbl.Rows[0]["totalQty"].ToString();
                    dRowDetails["TenderPaid"] = Convert.ToDecimal(dtbl.Rows[0]["paidAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TenderBalance"] = Convert.ToDecimal(dtbl.Rows[0]["balanceAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TenderCash"] = Convert.ToDecimal(dtbl.Rows[0]["cashAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TenderCC"] = Convert.ToDecimal(dtbl.Rows[0]["creditCardAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TenderUPI"] = Convert.ToDecimal(dtbl.Rows[0]["UPIAmount"]).ToString(SettingsInfo._roundDecimalPart);

                    //------------------------ QR Code Generation ----------- by Navas --------------------
                    Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    string companyname = dtblCompanyDetails.Rows[0][2].ToString();
                    string vatno = dtblCompanyDetails.Rows[0][9].ToString();
                    string invoicedate = Convert.ToDateTime(dtbl.Rows[0]["billDate"]).ToString("yyyy-MM-dd");
                    string invoicetime = DateTime.Now.ToString("HH:mm:ss");
                    invoicedate = invoicedate + "T" + invoicetime;
                    string invoicetotal = Convert.ToDecimal(dtbl.Rows[0]["taxableAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    //txtTotal.Text.Replace("SR", "");
                    string invoicevatamount = Convert.ToDecimal(dtbl.Rows[0]["totalTaxAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    //txtTaxAmt.Text.Replace("SR", "");

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

                    //string strQRvariable = dtblCompanyDetails.Rows[0][2].ToString() + "\n" + "VAT No:" + dtblCompanyDetails.Rows[0][9].ToString() + "\n" + "VAT total:" + lblTaxAmt.Text + "\n" + "Grand Total: " + lblGrand.Text + "\n" + "Invoice Date:" + dtpVoucherDate.Text;
                    //string strQRvariable = dtblCompanyDetails.Rows[0][2].ToString() + "\n" + "VAT No:" + dtblCompanyDetails.Rows[0][9].ToString();
                    var utf8text = System.Text.Encoding.UTF8.GetBytes(strQRvariable);
                    string qrdata = System.Convert.ToBase64String(utf8text);
                    Image img = qrBarcode.Draw(qrdata, 500); //pictureBox1.Image;
                    byte[] arr;
                    ImageConverter converter = new ImageConverter();
                    arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                    dRowDetails["qrCode"] = arr;
                    //---------------------------------
                    dtblOtherDetails.Rows.Add(dRowDetails);
                }

            }

            dtblCompanyDetailsThermal = dtblCompanyDetails;
            dtblGridDetailsThermal = dtblGridDetails;
            dtblOtherDetailsThermal = dtblOtherDetails;


            int pageWidth;
            int PageHight = 0;
            pageSetupDialog1.PageSettings = printDocumentThermal3.DefaultPageSettings;
            pageSetupDialog1.AllowOrientation = false;
            PageHight = dtblGridDetailsThermal.Rows.Count + 570; //set rowcount
            printDocumentThermal3.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Cs", 300, PageHight);
            pageWidth = printDocumentThermal3.DefaultPageSettings.PaperSize.Width - printDocumentThermal3.DefaultPageSettings.Margins.Left - printDocumentThermal3.DefaultPageSettings.Margins.Right;
            PrintDocument document = new PrintDocument();
            printPreviewDialog1.Document = printDocumentThermal3;
            //printPreviewDialog1.ShowDialog();
            printDocumentThermal3.Print();
        }


        public void FillDatatatablesforDevPrint(string strTenderPaid, string strTenderBalance, string strTenderCash, string strTenderCC, string strTenderUPI, bool isDuplicatePrint, string strDuplicateBillNo, string strHoldBillNo, string strTenderType)
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



            //-------Grid details-------------------------
            DataTable dtblGridDetails = new DataTable();

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


            if (isDuplicatePrint == false)
            {
                //int inRowIndex = 0;
                foreach (DataGridViewRow gridrow in dgvProduct.Rows)
                {
                    if (!gridrow.IsNewRow)
                    {
                        DataRow dr = dtblGridDetails.NewRow();
                        dr["Sl No"] = gridrow.Cells["SlNo"].Value.ToString();
                        dtblGridDetails.Rows.Add(dr);
                        dr["Barcode"] = gridrow.Cells["Barcode"].Value.ToString();
                        dr["ProductName"] = gridrow.Cells["ItemName"].Value.ToString();
                        dr["Tax%"] = gridrow.Cells["TaxPerc"].Value.ToString();
                        dr["Price"] = gridrow.Cells["SalesRate"].Value.ToString();
                        dr["Qty"] = gridrow.Cells["Qty"].Value.ToString();
                        dr["Gr.Value"] = gridrow.Cells["GrossValue"].Value.ToString();
                        dr["Tax Amt"] = gridrow.Cells["TaxAmt"].Value.ToString();
                        dr["Disc Amt"] = gridrow.Cells["DiscAmt"].Value.ToString();
                        dr["NETVALUE"] = gridrow.Cells["NetValue"].Value.ToString();
                        dr["Total Amt"] = gridrow.Cells["Total"].Value.ToString();
                        dr["NameArabic"] = gridrow.Cells["ArabicName"].Value.ToString();
                    }
                }
            }
            else
            {
                DataTable dtbl = new DataTable();
                dtbl = POSSalesMasterSP.GetPOSLastBillProductsforLastBillPrint(strDuplicateBillNo);

                if (dtbl.Rows.Count > 0)
                {
                    dtblTaxDetailsThermal = GetTaxSumforDuplicatePrint(dtbl);

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        DataRow dr = dtblGridDetails.NewRow();
                        dr["Sl No"] = dtbl.Rows[i]["LineNumber"].ToString();
                        dtblGridDetails.Rows.Add(dr);
                        dr["Barcode"] = dtbl.Rows[i]["barcode"].ToString();
                        dr["ProductName"] = dtbl.Rows[i]["productName"].ToString();
                        dr["Tax%"] = dtbl.Rows[i]["taxPer"].ToString();
                        dr["Price"] = Convert.ToDecimal(dtbl.Rows[i]["rate"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Qty"] = dtbl.Rows[i]["qty"].ToString();
                        dr["Gr.Value"] = Convert.ToDecimal(dtbl.Rows[i]["grossValue"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Tax Amt"] = Convert.ToDecimal(dtbl.Rows[i]["taxAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Disc Amt"] = Convert.ToDecimal(dtbl.Rows[i]["discAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["NETVALUE"] = Convert.ToDecimal(dtbl.Rows[i]["netAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["Total Amt"] = Convert.ToDecimal(dtbl.Rows[i]["Amount"]).ToString(SettingsInfo._roundDecimalPart);
                        dr["NameArabic"] = dtbl.Rows[i]["ArabicName"].ToString();
                    }
                }
            }



            //--------- Other Details Datatable---------------
            DataTable dtblOtherDetails = new DataTable();

            dtblOtherDetails.Columns.Add("PartyName");
            dtblOtherDetails.Columns.Add("PartyAddress");
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
            dtblOtherDetails.Columns.Add("CustomerAddress");
            dtblOtherDetails.Columns.Add("CustomerPhone");
            dtblOtherDetails.Columns.Add("CustomerVatNo");

            if (isDuplicatePrint == false)
            {
                DataRow dRowDetails = dtblOtherDetails.NewRow();
                dRowDetails["PartyName"] = "";
                dRowDetails["PartyAddress"] = "";
                dRowDetails["BillDate"] = lblBillDate.Text;
                dRowDetails["BillTime"] = lblBillTime.Text;
                dRowDetails["SessionDate"] = lblSessionDate.Text;
                dRowDetails["SessionNo"] = lblSessionNO.Text;
                dRowDetails["CounterId"] = lblCounter.Text;
                dRowDetails["UserName"] = lblUser.Text;
                if (strHoldBillNo != "")
                {
                    dRowDetails["InvoiceNo"] = strHoldBillNo;
                    dRowDetails["BillName"] = "HOLD BILL";
                }
                else
                {
                    dRowDetails["InvoiceNo"] = lblBillNo.Text;
                    if (txtVatNo.Text != "")
                    {
                        dRowDetails["BillName"] = "TAX INVOICE / ضريبية مبسطة";
                    }
                    else
                    {
                        dRowDetails["BillName"] = "SIMPLIFIED TAX INVOICE / فاتورة ضريبية مبسطة";
                    }
                    
                }
                dRowDetails["SubTotal"] = txtSubTotal.Text;
                dRowDetails["BillDiscount"] = txtDiscAmt.Text;
                dRowDetails["TaxableAmount"] = txtTaxable.Text;
                dRowDetails["TaxAmount"] = txtTaxAmt.Text;
                dRowDetails["GrandTotal"] = txtTotal.Text;
                dRowDetails["AmountInWords"] = "";

                dRowDetails["QtyTotal"] = lblTotalQty.Text;
                if (strTenderType == "Credit Bill")
                {
                    dRowDetails["TenderPaid"] = "";
                    dRowDetails["TenderBalance"] = "";

                    dRowDetails["TenderCashText"] = "";
                    dRowDetails["TenderCash"] = "";

                    dRowDetails["TenderCCText"] = "";
                    dRowDetails["TenderCC"] = "";

                    dRowDetails["TenderUPIText"] = "";
                    dRowDetails["TenderUPI"] = "";

                    dRowDetails["customerCode"] = txtCustCode.Text;
                    dRowDetails["customerName"] = txtCustomerName.Text;
                    dRowDetails["isCredit"] = true;
                    dRowDetails["CustomerAddress"] = txtAdress.Text;
                    dRowDetails["CustomerPhone"] = txtphone.Text;
                    dRowDetails["CustomerVatNo"] = txtVatNo.Text;
                  
                    DataTable dtblBalance = salesmaster.GetCustomerCurrentBalance(lblLedgerId.Text.ToString(), PublicVariables._branchId);
                    if (dtblBalance.Rows.Count > 0)
                    {
                        dRowDetails["prevBalance"] = Convert.ToDecimal(dtblBalance.Rows[0]["currentBal"]).ToString(SettingsInfo._roundDecimalPart);
                        dRowDetails["totalBalance"] = Convert.ToDecimal(decimal.Parse(dtblBalance.Rows[0]["currentBal"].ToString()) + decimal.Parse(txtTotal.Text)).ToString(SettingsInfo._roundDecimalPart);
                    }
                    else
                    {
                        dRowDetails["prevBalance"] = 0;
                        dRowDetails["totalBalance"] = 0;
                    }

                    dRowDetails["BillAmount"] = txtTotal.Text;
                    if (POSSettingsInfo._ShowCustBalinPrint)
                    {
                        dRowDetails["showCustBalance"] = true;
                    }
                    else
                        dRowDetails["showCustBalance"] = false;
                }
                else
                {
                    dRowDetails["TenderPaid"] = strTenderPaid;
                    dRowDetails["TenderBalance"] = strTenderBalance;

                    if (strTenderCash != "0.00")
                    {
                        dRowDetails["TenderCashText"] = "Cash Tendered";
                        dRowDetails["TenderCash"] = strTenderCash;
                    }
                    else
                    {
                        dRowDetails["TenderCashText"] = "";
                        dRowDetails["TenderCash"] = "";
                    }
                    if (strTenderCC != "0.00")
                    {
                        dRowDetails["TenderCCText"] = "CC Tendered";
                        dRowDetails["TenderCC"] = strTenderCC;
                    }
                    else
                    {
                        dRowDetails["TenderCCText"] = "";
                        dRowDetails["TenderCC"] = "";
                    }
                    if (strTenderUPI != "0.00")
                    {
                        dRowDetails["TenderUPIText"] = "UPI Tendered";
                        dRowDetails["TenderUPI"] = strTenderUPI;
                    }
                    else
                    {
                        dRowDetails["TenderUPIText"] = "";
                        dRowDetails["TenderUPI"] = "";
                    }
                    dRowDetails["customerCode"] = txtCustCode.Text;
                    dRowDetails["customerName"] = txtCustomerName.Text;
                    dRowDetails["CustomerAddress"] = txtAdress.Text;
                    dRowDetails["CustomerPhone"] = txtphone.Text;
                    dRowDetails["CustomerVatNo"] = txtVatNo.Text;
                    dRowDetails["isCredit"] = false;
                    dRowDetails["prevBalance"] = "";
                    dRowDetails["BillAmount"] = "";
                    dRowDetails["totalBalance"] = "";
                    dRowDetails["showCustBalance"] = false;
                }

                //////------------------------ QR Code Generation ----------- by Navas --------------------
                ////Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                ////string companyname = dtblCompanyDetails.Rows[0][2].ToString();
                ////string vatno = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
                ////string invoicedate = Convert.ToDateTime(lblBillDate.Text).ToString("yyyy-MM-dd");
                ////string invoicetime = DateTime.Now.ToString("HH:mm:ss");
                ////invoicedate = invoicedate + "T" + invoicetime;
                ////string invoicetotal = txtTotal.Text.Replace("SR", "");
                ////string invoicevatamount = txtTaxAmt.Text.Replace("SR", "");

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
                //////---------------------------------

                //------------------------ QR Code Generation ----------- by Navas --------------------
                Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                string companyname = dtblCompanyDetails.Rows[0]["branchName"].ToString();
                string vatno = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
                string invoicedate = DateTime.Parse(lblBillDate.Text.ToString()).ToString("yyyy-MM-dd");
                string invoicetime = DateTime.Now.ToString("HH:mm:ss");
                invoicedate = invoicedate + "T" + invoicetime;
                string invoicetotal = Convert.ToDecimal(txtTotal.Text).ToString(SettingsInfo._roundDecimalPart);
                string invoicevatamount = Convert.ToDecimal(txtTaxAmt.Text).ToString(SettingsInfo._roundDecimalPart);

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



                //for (int i = 0; i < dtblTaxDetailsThermal.Rows.Count; i++)
                //{
                //    e.Graphics.DrawString(dtblTaxDetailsThermal.Rows[i]["taxName"].ToString(), new Font("Arial", 7), Brushes.Black, 15, lineGap);
                //    e.Graphics.DrawString(dtblTaxDetailsThermal.Rows[i]["taxableAmt"].ToString(), new Font("Arial", 7), Brushes.Black, 120, lineGap);
                //    e.Graphics.DrawString(dtblTaxDetailsThermal.Rows[i]["amt"].ToString(), new Font("Arial", 7), Brushes.Black, 280, lineGap, SFRight);
                //    lineGap = lineGap + 20;
                //}





                dtblOtherDetails.Rows.Add(dRowDetails);
            }
            else
            {

                DataTable dtbl = new DataTable();
                dtbl = POSSalesMasterSP.GetPOSLastBillDetialsforLastBillPrint(strDuplicateBillNo);

                if (dtbl.Rows.Count > 0)
                {
                    DataRow dRowDetails = dtblOtherDetails.NewRow();
                    dRowDetails["PartyName"] = "";
                    dRowDetails["PartyAddress"] = "";
                    dRowDetails["BillDate"] = Convert.ToDateTime(dtbl.Rows[0]["billDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["BillTime"] = dtbl.Rows[0]["billTime"].ToString();
                    dRowDetails["SessionDate"] = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"]).ToString("dd/MMM/yyyy");
                    dRowDetails["SessionNo"] = dtbl.Rows[0]["sessionNo"].ToString();
                    dRowDetails["CounterId"] = dtbl.Rows[0]["counterId"].ToString();
                    dRowDetails["UserName"] = dtbl.Rows[0]["userId"].ToString();
                    dRowDetails["InvoiceNo"] = dtbl.Rows[0]["invoiceNo"].ToString();
                    dRowDetails["SubTotal"] = Convert.ToDecimal(dtbl.Rows[0]["subTotalAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["BillDiscount"] = Convert.ToDecimal(dtbl.Rows[0]["billDiscAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TaxableAmount"] = Convert.ToDecimal(dtbl.Rows[0]["taxableAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TaxAmount"] = Convert.ToDecimal(dtbl.Rows[0]["totalTaxAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["GrandTotal"] = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["AmountInWords"] = "";
                   // dRowDetails["BillName"] = "TAX INVOICE COPY / فاتورة ضريبية";
                    dRowDetails["QtyTotal"] = dtbl.Rows[0]["totalQty"].ToString();
                    dRowDetails["TenderPaid"] = Convert.ToDecimal(dtbl.Rows[0]["paidAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    dRowDetails["TenderBalance"] = Convert.ToDecimal(dtbl.Rows[0]["balanceAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    if (dtbl.Rows[0]["CustomerVatNo"].ToString() != "")
                    {
                        dRowDetails["BillName"] = "TAX INVOICE COPY / فاتورة ضريبية";
                    }
                    else
                    {
                        dRowDetails["BillName"] = "SIMPLIFIED TAX INVOICE COPY / فاتورة ضريبية مبسطة";
                    }

                    if (Convert.ToDecimal(dtbl.Rows[0]["cashAmount"]).ToString(SettingsInfo._roundDecimalPart) != "0.00")
                    {
                        dRowDetails["TenderCashText"] = "Cash Tendered";
                        dRowDetails["TenderCash"] = Convert.ToDecimal(dtbl.Rows[0]["cashAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    }
                    else
                    {
                        dRowDetails["TenderCashText"] = "";
                        dRowDetails["TenderCash"] = "";
                    }
                    if (Convert.ToDecimal(dtbl.Rows[0]["creditCardAmount"]).ToString(SettingsInfo._roundDecimalPart) != "0.00")
                    {
                        dRowDetails["TenderCCText"] = "CC Tendered";
                        dRowDetails["TenderCC"] = Convert.ToDecimal(dtbl.Rows[0]["creditCardAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    }
                    else
                    {
                        dRowDetails["TenderCCText"] = "";
                        dRowDetails["TenderCC"] = "";
                    }
                    if (Convert.ToDecimal(dtbl.Rows[0]["UPIAmount"]).ToString(SettingsInfo._roundDecimalPart) != "0.00")
                    {
                        dRowDetails["TenderUPIText"] = "UPI Tendered";
                        dRowDetails["TenderUPI"] = Convert.ToDecimal(dtbl.Rows[0]["UPIAmount"]).ToString(SettingsInfo._roundDecimalPart);
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
                    dRowDetails["CustomerAddress"] = dtbl.Rows[0]["CustomerAddress"].ToString();
                    dRowDetails["CustomerPhone"] = dtbl.Rows[0]["CustomerPhone"].ToString();
                    dRowDetails["CustomerVatNo"] = dtbl.Rows[0]["CustomerVATNo"].ToString();
                    DataTable dtblBalance = salesmaster.GetCustomerCurrentBalance(dtbl.Rows[0]["customerCode"].ToString(), PublicVariables._branchId);
                    if (dtblBalance.Rows.Count > 0)
                    {
                        dRowDetails["prevBalance"] = Convert.ToDecimal(decimal.Parse(dtblBalance.Rows[0]["currentBal"].ToString()) - Convert.ToDecimal(dtbl.Rows[0]["totalAmount"].ToString())).ToString(SettingsInfo._roundDecimalPart);
                        dRowDetails["totalBalance"] = decimal.Parse(dtblBalance.Rows[0]["currentBal"].ToString()).ToString(SettingsInfo._roundDecimalPart);
                    }
                    else
                    {
                        dRowDetails["prevBalance"] = 0;
                        dRowDetails["totalBalance"] = 0;
                    }

                    dRowDetails["BillAmount"] = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(SettingsInfo._roundDecimalPart);

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

                    //------------------------ QR Code Generation ----------- by Navas --------------------
                    Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                    string companyname = dtblCompanyDetails.Rows[0]["branchName"].ToString();
                    string vatno = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
                    string invoicedate = DateTime.Parse(dtbl.Rows[0]["billDate"].ToString()).ToString("yyyy-MM-dd");
                    string invoicetime = DateTime.Now.ToString("HH:mm:ss");
                    invoicedate = invoicedate + "T" + invoicetime;
                    string invoicetotal = Convert.ToDecimal(dtbl.Rows[0]["totalAmount"]).ToString(SettingsInfo._roundDecimalPart);
                    string invoicevatamount = Convert.ToDecimal(dtbl.Rows[0]["totalTaxAmount"]).ToString(SettingsInfo._roundDecimalPart);

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

                    //---------------------------------
                    dtblOtherDetails.Rows.Add(dRowDetails);
                }

            }

            dtblCompanyDetailsThermal = dtblCompanyDetails;
            dtblGridDetailsThermal = dtblGridDetails;
            dtblOtherDetailsThermal = dtblOtherDetails;





            DevPrint spPrint = new DevPrint();
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

        private void printDocumentThermal3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
            e.Graphics.DrawString("VATIN: " + dtblCompanyDetailsThermal.Rows[0]["tinNo"].ToString(), new Font("Arial", 8), Brushes.Black, 150, lineGap, SFCentre);
            lineGap = lineGap + 40;
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["BillName"].ToString(), new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);

            lineGap = lineGap + 40;
            if (dtblOtherDetailsThermal.Rows[0]["BillName"].ToString() == "HOLD BILL")
            {
                e.Graphics.DrawString("Hold No: " + dtblOtherDetailsThermal.Rows[0]["invoiceNo"].ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            }
            else
            {
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["invoiceNo"].ToString() + ": رقم الفاتورة", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            }

            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["BillDate"].ToString() + ": تاريخ", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 170, lineGap);
            lineGap = lineGap + 20;
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["CounterId"].ToString() + ": عداد", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["BillTime"].ToString() + ": وقت الفاتورة", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 170, lineGap);

            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString(" الباركود" + Environment.NewLine + "Barcode", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(" معدل" + Environment.NewLine + "Rate", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 150, lineGap, SFRight);
            e.Graphics.DrawString(" كمية" + Environment.NewLine + "Qty", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 190, lineGap, SFRight);
            e.Graphics.DrawString(" ضريبة" + Environment.NewLine + "VAT", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 230, lineGap, SFRight);
            e.Graphics.DrawString(" مجموع" + Environment.NewLine + "Total", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 25;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);

            //DtblGridDetails
            lineGap = lineGap + 5;
            for (int i = 0; i < dtblGridDetailsThermal.Rows.Count; i++)
            {

                e.Graphics.DrawString(dtblGridDetailsThermal.Rows[i]["ProductName"].ToString(), new Font("Arial", 7), Brushes.Black, 15, lineGap);
                if (dtblGridDetailsThermal.Rows[i]["NameArabic"].ToString() != "")
                {
                    lineGap = lineGap + 20;
                    e.Graphics.DrawString(dtblGridDetailsThermal.Rows[i]["NameArabic"].ToString(), new Font("Arial", 7), Brushes.Black, 15, lineGap);
                }

                lineGap = lineGap + 20;
                e.Graphics.DrawString(dtblGridDetailsThermal.Rows[i]["Barcode"].ToString(), new Font("Arial", 7), Brushes.Black, 15, lineGap);

                e.Graphics.DrawString(dtblGridDetailsThermal.Rows[i]["Price"].ToString(), new Font("Arial", 7), Brushes.Black, 150, lineGap, SFRight);
                e.Graphics.DrawString(dtblGridDetailsThermal.Rows[i]["Qty"].ToString(), new Font("Arial", 7), Brushes.Black, 190, lineGap, SFRight);
                e.Graphics.DrawString(dtblGridDetailsThermal.Rows[i]["Tax Amt"].ToString(), new Font("Arial", 7), Brushes.Black, 230, lineGap, SFRight);
                e.Graphics.DrawString(dtblGridDetailsThermal.Rows[i]["Total Amt"].ToString(), new Font("Arial", 7), Brushes.Black, 280, lineGap, SFRight);


                lineGap = lineGap + 20;

            }

            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            lineGap = lineGap + 5;
            e.Graphics.DrawString("Total Qty/ الكمية الإجمالية", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["QtyTotal"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            if (dtblOtherDetailsThermal.Rows[0]["BillName"].ToString() != "HOLD BILL")
            {
                lineGap = lineGap + 30;
                e.Graphics.DrawString("Subtotal/ المجموع الفرعي", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["SubTotal"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
                lineGap = lineGap + 25;
                e.Graphics.DrawString("Discount/ تخفيض", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["BillDiscount"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
                lineGap = lineGap + 25;
                e.Graphics.DrawString("VAT/ ضريبة", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["TaxAmount"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            }

            lineGap = lineGap + 25;
            e.Graphics.DrawString("Grand Total/ المجموع الإجمالي", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["GrandTotal"].ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);


            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);

            if (dtblOtherDetailsThermal.Rows[0]["BillName"].ToString() != "HOLD BILL")
            {
                lineGap = lineGap + 5;
                e.Graphics.DrawString("Total Paid/ إجمالي المدفوع", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["TenderPaid"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
                lineGap = lineGap + 25;
                e.Graphics.DrawString("Balance/ مقدار وسطي", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["TenderBalance"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);

                if (decimal.Parse(dtblOtherDetailsThermal.Rows[0]["TenderCash"].ToString()) != 0)
                {
                    lineGap = lineGap + 20;
                    e.Graphics.DrawString("Cash Tendered: " + dtblOtherDetailsThermal.Rows[0]["TenderCash"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                }
                if (decimal.Parse(dtblOtherDetailsThermal.Rows[0]["TenderCC"].ToString()) != 0)
                {
                    lineGap = lineGap + 20;
                    e.Graphics.DrawString("CC Tendered: " + dtblOtherDetailsThermal.Rows[0]["TenderCC"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                }
                if (decimal.Parse(dtblOtherDetailsThermal.Rows[0]["TenderUPI"].ToString()) != 0)
                {
                    lineGap = lineGap + 20;
                    e.Graphics.DrawString("UPI Tendered: " + dtblOtherDetailsThermal.Rows[0]["TenderUPI"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
                }

                lineGap = lineGap + 20;
                e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);

                lineGap = lineGap + 5;
                e.Graphics.DrawString(" نسبة الضريبة" + Environment.NewLine + "  Tax %", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
                e.Graphics.DrawString(" خاضع للضريبة" + Environment.NewLine + "  Taxable", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 120, lineGap);
                e.Graphics.DrawString(" قيمة الضريبة" + Environment.NewLine + "Tax Amt", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);
                lineGap = lineGap + 25;
                e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
                lineGap = lineGap + 10;
                for (int i = 0; i < dtblTaxDetailsThermal.Rows.Count; i++)
                {
                    e.Graphics.DrawString(dtblTaxDetailsThermal.Rows[i]["taxName"].ToString(), new Font("Arial", 7), Brushes.Black, 15, lineGap);
                    e.Graphics.DrawString(dtblTaxDetailsThermal.Rows[i]["taxableAmt"].ToString(), new Font("Arial", 7), Brushes.Black, 120, lineGap);
                    e.Graphics.DrawString(dtblTaxDetailsThermal.Rows[i]["amt"].ToString(), new Font("Arial", 7), Brushes.Black, 280, lineGap, SFRight);
                    lineGap = lineGap + 20;
                }

                e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
                lineGap = lineGap + 5;
                e.Graphics.DrawString("Total", new Font("Arial", 8), Brushes.Black, 15, lineGap);
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["TaxableAmount"].ToString(), new Font("Arial", 8), Brushes.Black, 120, lineGap);
                e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["TaxAmount"].ToString(), new Font("Arial", 8), Brushes.Black, 280, lineGap, SFRight);

                lineGap = lineGap + 20;
                e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);
            }
            lineGap = lineGap + 30;
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["UserName"].ToString() + ": محاسب", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);



            if (dtblOtherDetailsThermal.Rows[0]["BillName"].ToString() != "HOLD BILL")
            {
                lineGap = lineGap + 30;
                e.Graphics.DrawString("شكرا للتسوق معنا" + Environment.NewLine + "Thank you for Shopping with us.", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);

                lineGap = lineGap + 30;
                rect1.Y = lineGap;
                rect1.Height = counterInfo.FooterH;
                e.Graphics.DrawString(counterInfo.FooterDetails.ToString(), new Font("Arial", 8), Brushes.Black, rect1, SFCentre);
            }


            //------------------ QRCode ----------------------
            //PrintPageHight = PrintPageHight + 40;
            lineGap = lineGap + 40;
            byte[] logo1 = null;
            logo1 = (byte[])dtblOtherDetailsThermal.Rows[0]["qrCode"];
            MemoryStream ms2 = new MemoryStream(logo1);
            Image im2 = Image.FromStream(ms2);
            Point p1 = new Point(100, 100);
            e.Graphics.DrawImage(im2, 50, lineGap, 150, 150);
            //------------------------------------------------



            byte[] barcodeImage = null;
            // logo = CType(dtLoadHead.Rows(0)("pic1"), Byte())

            OnBarcode.Barcode.Linear barcode;
            // Create linear barcode object
            barcode = new OnBarcode.Barcode.Linear();
            // Set barcode symbology type to Code-39
            barcode.Type = OnBarcode.Barcode.BarcodeType.CODE128A;
            // Set barcode data to encode
            barcode.Data = dtblOtherDetailsThermal.Rows[0]["invoiceNo"].ToString();
            // Set barcode bar width (X    dimension) in pixel
            barcode.X = 1;
            // Set barcode bar height (Y dimension) in pixel
            barcode.Y = 60;
            // Draw & print generated barcode to png image file

            barcodeImage = (byte[])barcode.drawBarcodeAsBytes();
            MemoryStream ms1 = new MemoryStream(barcodeImage);
            Image im1 = Image.FromStream(ms1);

            lineGap = lineGap + 160;
            e.Graphics.DrawImage(im1, 30, lineGap, 200, 40);
        }

        public void DowhenReturningFromPOSPaymentForm(bool formcancel, string strCreditCardNo, decimal decCreditCardAmt, decimal decUPIAmt, decimal decCreditAmt, decimal decCashAmt, string strCreditNoteNo, decimal decCreditNoteAmt, decimal decTotalTenderAmt, decimal decBalanceAmt, string strTenderType)
        {
            this.Enabled = true;
            isSavefromButton = true;
            if (formcancel == true)
            {
                barcodeFocus();
            }
            else //Save function
            {
                lblTenderTotalAmount.Visible = true;
                lblTenderBalanceAmount.Visible = true;
                lblTenderTotal.Visible = true;
                lblTenderBalance.Visible = true;
                lblTenderTotalAmount.Text = decTotalTenderAmt.ToString(SettingsInfo._roundDecimalPart);
                lblTenderBalanceAmount.Text = decBalanceAmt.ToString(SettingsInfo._roundDecimalPart);

                if (counterInfo.DisplayStatus == true)
                {
                    PoleDisplay("CashCollect");
                }

                string strMasterId = SaveFunction(strCreditCardNo, decCreditCardAmt, decUPIAmt, decCreditAmt, decCashAmt, strCreditNoteNo, decCreditNoteAmt, decTotalTenderAmt, decBalanceAmt, strTenderType).ToString();

                if (strMasterId != "")
                {
                    DataTable dtblTaxSummery = new DataTable();
                    dtblTaxSummery = GetTaxSum();
                    //MessageBox.Show("Saved");
                    bool isPrintSuccess = false;
                    try
                    {
                        dtblTaxDetailsThermal = dtblTaxSummery;
                        //FillDatatatablesforPrint(decTotalTenderAmt.ToString(SettingsInfo._roundDecimalPart), decBalanceAmt.ToString(SettingsInfo._roundDecimalPart), decCashAmt.ToString(SettingsInfo._roundDecimalPart), decCreditCardAmt.ToString(SettingsInfo._roundDecimalPart), decUPIAmt.ToString(SettingsInfo._roundDecimalPart), false, "", "");
                        FillDatatatablesforDevPrint(decTotalTenderAmt.ToString(SettingsInfo._roundDecimalPart), decBalanceAmt.ToString(SettingsInfo._roundDecimalPart), decCashAmt.ToString(SettingsInfo._roundDecimalPart), decCreditCardAmt.ToString(SettingsInfo._roundDecimalPart), decUPIAmt.ToString(SettingsInfo._roundDecimalPart), false, "", "", strTenderType);




                        isPrintSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        isPrintSuccess = false;

                    }

                    if (isPrintSuccess == true)
                    {
                        SaveLedgerPosting(strMasterId, strCreditCardNo, decCreditCardAmt, decUPIAmt, decCreditAmt, decCashAmt, strCreditNoteNo, decCreditNoteAmt, decTotalTenderAmt, decBalanceAmt, dtblTaxSummery);
                        SavetoStockPosting(strMasterId);

                        if (txtCustomerName.Text != "")
                        {
                            //party balance
                            PartyBalanceSP SPPartyBalance = new PartyBalanceSP();
                            AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
                            AccountLedgerInfo InfoLedger = SpAccountLedger.AccountLedgerView(lblLedgerId.Text);
                            CurrencyConversionSP SPCurrencyConversion = new CurrencyConversionSP();
                            bool BillByBill = SPCurrencyConversion.SettingsViewByBranchId(PublicVariables._branchId);
                            if (InfoLedger.BillByBill && BillByBill)
                            {
                                PartyBalanceInfo InfoPartyBalance = new PartyBalanceInfo();
                                InfoPartyBalance.AgainstvoucherNo = "NA";
                                InfoPartyBalance.AgainstVoucherType = "NA";
                                InfoPartyBalance.BranchId = PublicVariables._branchId;
                                InfoPartyBalance.Credit = 0;
                                //InfoPartyBalance.Debit = decimal.Parse(txtGrand.Text.Replace(cmbCurrency.Text, "").Trim()) * InfoCurrencyConversion.Rate;
                                InfoPartyBalance.Debit = decimal.Parse(txtTotal.Text);
                                InfoPartyBalance.CreditPeriod = 0;
                                SPCurrencyConversion.CurrencyConversionRateIdViewByCurrencyId(InfoLedger.CurrencyId, DateTime.Parse(lblBillDate.Text), PublicVariables._branchId);
                                InfoPartyBalance.CurrencyConversionId = SPCurrencyConversion.CurrencyConversionRateIdViewByCurrencyId(InfoLedger.CurrencyId, DateTime.Parse(lblBillDate.Text), PublicVariables._branchId);
                                InfoPartyBalance.Date = Convert.ToDateTime(lblBillDate.Text);
                                InfoPartyBalance.Extra1 = "";
                                InfoPartyBalance.Extra2 = "";
                                //InfoPartyBalance.LedgerId = cmbCashOrParty.SelectedValue.ToString();
                                InfoPartyBalance.LedgerId = lblLedgerId.Text;
                                InfoPartyBalance.Optional = false;
                                InfoPartyBalance.ReferenceType = "New";
                                InfoPartyBalance.VoucherNo = strMasterId;
                                InfoPartyBalance.VoucherType = "POS Sales";
                                InfoPartyBalance.invoiceNo = lblBillNo.Text; //added on 21/03/2025  by  Nishana
                                InfoPartyBalance.referenceNo = "";  //21/03/2025
                                InfoPartyBalance.BillAmount = decimal.Parse(txtTotal.Text); //21/03/2025
                                InfoPartyBalance.invoiceDate = DateTime.Parse(lblSessionDate.Text); //21/03/2025
                                InfoPartyBalance.costCentreId = "1"; //21/03/2025
                                InfoPartyBalance.exchangeDate = PublicVariables._fromDate; //21/03/2025
                                InfoPartyBalance.exchangeRate = 1; //21/03/2025

                                SPPartyBalance.PartyBalanceAdd(InfoPartyBalance);
                            }
                        }
                        //-------------------------------------------------------------------------------------
                    }
                    SPGeneral.POSBillUpdate(PublicVariables._counterId, PublicVariables._currentUserId, "Sales");
                    ClearFunction();
                }
            }

        }
        public DataTable GetTaxSum()
        {
            DataTable dtbl = new DataTable();

            dtbl = new TaxMasterSP().TaxGetByCondition("", true, "Sales Invoice", "");

            dtbl.Columns.Add("taxableAmt", typeof(decimal));
            dtbl.Columns.Add("amt", typeof(decimal));

            for (int i = 0; i < dtbl.Rows.Count; i++)
            {
                decimal dTotal = 0;
                decimal dTaxableTotal = 0;
                foreach (DataGridViewRow dgvrowProduct in dgvProduct.Rows)
                {
                    if (dgvrowProduct.Cells["taxAmt"].Value != null && dgvrowProduct.Cells["TaxId"].Value != null)
                    {
                        if (dgvrowProduct.Cells["taxAmt"].Value.ToString() != "" && dgvrowProduct.Cells["TaxId"].Value.ToString() != "")
                        {
                            if (dgvrowProduct.Cells["TaxId"].Value.ToString() == dtbl.Rows[i]["taxId"].ToString())
                            {
                                decimal dcItemBillDisc = 0;
                                try { dcItemBillDisc = decimal.Parse(dgvrowProduct.Cells["BillDiscIndProductAmt"].Value.ToString()); }
                                catch { }
                                dTotal = dTotal + decimal.Parse(dgvrowProduct.Cells["taxAmt"].Value.ToString());
                                dTaxableTotal = dTaxableTotal + (decimal.Parse(dgvrowProduct.Cells["NetValue"].Value.ToString()) - dcItemBillDisc);
                            }
                        }
                    }
                }
                dTaxableTotal = Math.Round(dTaxableTotal, SettingsInfo._roundDecimal);
                dTotal = Math.Round(dTotal, SettingsInfo._roundDecimal);
                dtbl.Rows[i]["amt"] = dTotal.ToString();
                dtbl.Rows[i]["taxableAmt"] = dTaxableTotal.ToString();
            }

            return dtbl;
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
                dTaxableTotal = Math.Round(dTaxableTotal, SettingsInfo._roundDecimal);
                dTotal = Math.Round(dTotal, SettingsInfo._roundDecimal);
                dtbl.Rows[i]["amt"] = dTotal.ToString();
                dtbl.Rows[i]["taxableAmt"] = dTaxableTotal.ToString();
            }

            return dtbl;
        }
        public void SaveLedgerPosting(string strMasterId, string strCreditCardNo, decimal decCreditCardAmt, decimal decUPIAmt, decimal decCreditAmt, decimal decCashAmt, string strCreditNoteNo, decimal decCreditNoteAmt, decimal decTotalTenderAmt, decimal decBalanceAmt, DataTable dtbltaxSummery)
        {
            LedgerPostingInfo InfoLedgerPosting = new LedgerPostingInfo();
            InfoLedgerPosting.VoucherNo = strMasterId;
            InfoLedgerPosting.VoucherType = "POS Sales";
            InfoLedgerPosting.Optional = false;
            InfoLedgerPosting.Date = DateTime.Parse(lblSessionDate.Text);
            InfoLedgerPosting.Extra1 = "";
            InfoLedgerPosting.Extra2 = "";
            InfoLedgerPosting.InvoiceNo = lblBillNo.Text;
            InfoLedgerPosting.postingType = "POS Sales";
            InfoLedgerPosting.exchangeDate = PublicVariables._fromDate; //added on 20/03/2025  by  Nishana
            InfoLedgerPosting.exchangeRate = 1; //added on 20/03/2025
            //-------Debit--------------------------------------------------------
            if (decCreditAmt > 0) //Credit Customer
            {
                InfoLedgerPosting.LedgerId = lblLedgerId.Text.ToString();
                InfoLedgerPosting.Debit = decCreditAmt;
                InfoLedgerPosting.Credit = 0;
                new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);
            }
            else //Cash/Card/UPI
            {
                decimal dcNetTotalCash = 0;
                dcNetTotalCash = Convert.ToDecimal(txtTotal.Text) - (decCreditCardAmt + decUPIAmt);
                if (dcNetTotalCash > 0) //Cash
                {
                    InfoLedgerPosting.LedgerId = strCashSalesLedgerId;
                    InfoLedgerPosting.Debit = dcNetTotalCash;
                    InfoLedgerPosting.Credit = 0;
                    new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);
                }
                if (decCreditCardAmt > 0) //CC
                {
                    InfoLedgerPosting.LedgerId = strCCSalesLedgerId;
                    InfoLedgerPosting.Debit = decCreditCardAmt;
                    InfoLedgerPosting.Credit = 0;
                    new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);
                }
                if (decUPIAmt > 0) //UPI
                {
                    InfoLedgerPosting.LedgerId = strUPISalesLedgerId;
                    InfoLedgerPosting.Debit = decUPIAmt;
                    InfoLedgerPosting.Credit = 0;
                    new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);
                }
            }

            //-------------------------------------------

            //-------Credit-------------------------------------------------------
            InfoLedgerPosting.LedgerId = strSalesLedgerId;
            InfoLedgerPosting.Debit = 0;
            InfoLedgerPosting.Credit = Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtTaxAmt.Text);
            new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);

            //------------Tax Amount
            if (dtbltaxSummery.Rows.Count > 0)
            {
                for (int i = 0; i < dtbltaxSummery.Rows.Count; i++)
                {
                    if (decimal.Parse(dtbltaxSummery.Rows[i]["amt"].ToString()) != 0m)
                    {
                        InfoLedgerPosting.LedgerId = dtbltaxSummery.Rows[i]["ledgerId"].ToString();
                        InfoLedgerPosting.Debit = 0;
                        InfoLedgerPosting.Credit = decimal.Parse(dtbltaxSummery.Rows[i]["amt"].ToString());
                        new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);
                    }
                }
            }

        }
        public void SavetoStockPosting(string strMaster)
        {
            try
            {
                StockPostingInfo InfoStockPosting = new StockPostingInfo();
                InfoStockPosting.BatchId = "1";
                InfoStockPosting.Date = DateTime.Parse(lblBillDate.Text);
                InfoStockPosting.Extra1 = "";
                InfoStockPosting.Extra2 = "";
                InfoStockPosting.GodownId = "Primary Location";
                InfoStockPosting.VoucherNo = strMaster;
                InfoStockPosting.VoucherType = "POS Sales";
                InfoStockPosting.RackId = "Primary Rack";
                foreach (DataGridViewRow dgvrowCurChk in dgvProduct.Rows)
                {
                    if (!dgvrowCurChk.IsNewRow)
                    {
                        InfoStockPosting.InwardQty = 0m;
                        InfoStockPosting.Optional = false;
                        InfoStockPosting.OutwardQty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoStockPosting.ProductCode = dgvrowCurChk.Cells["ProductCode"].Value.ToString();
                        InfoStockPosting.Rate = decimal.Parse(dgvrowCurChk.Cells["PurchaseRate"].Value.ToString()) / decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoStockPosting.UnitId = dgvrowCurChk.Cells["BaseUnitId"].Value.ToString();
                        InfoStockPosting.VoucherQty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoStockPosting.VoucherUnitId = dgvrowCurChk.Cells["UnitId"].Value.ToString();
                        new StockPostingSP().StockPostingAdd(InfoStockPosting);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("POS34:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public string SaveFunction(string strCreditCardNo, decimal decCreditCardAmt, decimal decUPIAmt, decimal decCreditAmt, decimal decCashAmt, string strCreditNoteNo, decimal decCreditNoteAmt, decimal decTotalTenderAmt, decimal decBalanceAmt,string strTenderType)
        {
            string strMasterId = "";

            lblBillNo.Text = POSBillNumberMax();

            POSSalesMasterInfo InfoPOSSalesMaster = new POSSalesMasterInfo();
            InfoPOSSalesMaster.InvoiceNo = lblBillNo.Text;
            InfoPOSSalesMaster.BillDate = Convert.ToDateTime(lblBillDate.Text);
            InfoPOSSalesMaster.BillTime = lblBillTime.Text;
            InfoPOSSalesMaster.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
            InfoPOSSalesMaster.CounterId = PublicVariables._counterId;
            InfoPOSSalesMaster.SessionNo = lblSessionNO.Text;
            InfoPOSSalesMaster.LedgerId = lblLedgerId.Text.ToString().Trim();
            InfoPOSSalesMaster.LedgerName = txtCustomerName.Text.ToString().Trim();
            InfoPOSSalesMaster.SubTotalAmount = Convert.ToDecimal(txtSubTotal.Text);
            InfoPOSSalesMaster.BillDiscPer = Convert.ToDecimal(txtDiscPer.Text);
            InfoPOSSalesMaster.BillDiscAmount = Convert.ToDecimal(txtDiscAmt.Text);
            InfoPOSSalesMaster.TaxableAmount = Convert.ToDecimal(txtTaxable.Text);
            InfoPOSSalesMaster.TotalTaxAmount = Convert.ToDecimal(txtTaxAmt.Text);
            InfoPOSSalesMaster.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            InfoPOSSalesMaster.TotalQty = Convert.ToDecimal(lblTotalQty.Text);
            InfoPOSSalesMaster.PaidAmount = decTotalTenderAmt;
            InfoPOSSalesMaster.BalanceAmount = decBalanceAmt;
            InfoPOSSalesMaster.CreditCardNo = strCreditCardNo;
            InfoPOSSalesMaster.CreditCardAmount = decCreditCardAmt;
            InfoPOSSalesMaster.UPIAmount = decUPIAmt;
            InfoPOSSalesMaster.CreditAmount = decCreditAmt;
            if (strTenderType == "Credit Bill")
            {
                InfoPOSSalesMaster.CashAmount = 0;
            }
            else
                InfoPOSSalesMaster.CashAmount = Convert.ToDecimal(txtTotal.Text) - (decCreditCardAmt + decUPIAmt);
            InfoPOSSalesMaster.CashPaidAmount = decCashAmt;
            InfoPOSSalesMaster.CreditNoteNo = strCreditNoteNo;
            InfoPOSSalesMaster.CreditNoteAmount = decCreditNoteAmt;
            InfoPOSSalesMaster.UserId = PublicVariables._currentUserId;
            InfoPOSSalesMaster.SalesMode = "Take Way";
            InfoPOSSalesMaster.CustomerAddress = txtAdress.Text.ToString();
            InfoPOSSalesMaster.CustomerPhone = txtphone.Text.ToString();
            InfoPOSSalesMaster.CustomerVATNo = txtVatNo.Text.ToString();


            strMasterId = POSSalesMasterSP.POSSalesMasterAdd(InfoPOSSalesMaster);

            if (strMasterId != "")
            {
                POSSalesDetails1Info InfoPOSSalesDetails1 = new POSSalesDetails1Info();

                InfoPOSSalesDetails1.POSSalesMasterId = strMasterId;
                InfoPOSSalesDetails1.InvoiceNo = lblBillNo.Text;
                InfoPOSSalesDetails1.BillDate = Convert.ToDateTime(lblBillDate.Text);
                InfoPOSSalesDetails1.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
                InfoPOSSalesDetails1.CounterId = PublicVariables._counterId;
                InfoPOSSalesDetails1.SessionNo = lblSessionNO.Text;
                InfoPOSSalesDetails1.UserId = PublicVariables._currentUserId;

                foreach (DataGridViewRow dgvrowCurChk in dgvProduct.Rows)
                {
                    if (!dgvrowCurChk.IsNewRow)
                    {
                        InfoPOSSalesDetails1.LineNumber = int.Parse(dgvrowCurChk.Cells["SLNo"].Value.ToString());
                        InfoPOSSalesDetails1.ProductCode = dgvrowCurChk.Cells["ProductCode"].Value.ToString();
                        InfoPOSSalesDetails1.Barcode = dgvrowCurChk.Cells["Barcode"].Value.ToString();
                        InfoPOSSalesDetails1.ProductName = dgvrowCurChk.Cells["ItemName"].Value.ToString();
                        InfoPOSSalesDetails1.UnitId = dgvrowCurChk.Cells["UnitId"].Value.ToString();
                        InfoPOSSalesDetails1.Qty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.Rate = decimal.Parse(dgvrowCurChk.Cells["SalesRate"].Value.ToString());
                        InfoPOSSalesDetails1.ExcludeRate = decimal.Parse(dgvrowCurChk.Cells["ExcludeRate"].Value.ToString());
                        InfoPOSSalesDetails1.CostPrice = decimal.Parse(dgvrowCurChk.Cells["PurchaseRate"].Value.ToString());
                        InfoPOSSalesDetails1.GrossValue = decimal.Parse(dgvrowCurChk.Cells["GrossValue"].Value.ToString());
                        InfoPOSSalesDetails1.DiscPer = decimal.Parse("0".ToString());
                        InfoPOSSalesDetails1.DiscAmount = decimal.Parse(dgvrowCurChk.Cells["DiscAmt"].Value.ToString());
                        InfoPOSSalesDetails1.NetAmount = decimal.Parse(dgvrowCurChk.Cells["NetValue"].Value.ToString());
                        InfoPOSSalesDetails1.TaxId = dgvrowCurChk.Cells["TaxId"].Value.ToString();
                        InfoPOSSalesDetails1.TaxPer = decimal.Parse(dgvrowCurChk.Cells["TaxPerc"].Value.ToString());
                        InfoPOSSalesDetails1.TaxAmount = decimal.Parse(dgvrowCurChk.Cells["TaxAmt"].Value.ToString());
                        InfoPOSSalesDetails1.Amount = decimal.Parse(dgvrowCurChk.Cells["Total"].Value.ToString());
                        InfoPOSSalesDetails1.BillDiscAmountperItem = dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value == null ? 0 : decimal.Parse(dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value.ToString());
                        InfoPOSSalesDetails1.ConversionFactor = decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoPOSSalesDetails1.AmountBeforeDisc = decimal.Parse(dgvrowCurChk.Cells["amountBeforeDisc"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.RateDiscAmount = decimal.Parse(dgvrowCurChk.Cells["rateDiscAmount"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.OfferId = dgvrowCurChk.Cells["offerId"].Value.ToString();

                        string strPOSSalesDetails1Id = "";

                        strPOSSalesDetails1Id = POSSalesDetails1SP.POSSalesDetails1Add(InfoPOSSalesDetails1);
                    }
                }


            }

            //Update Hold Bill Status as Close
            if (strHoldMasterIdToEdit != "")
            {
                POSSalesMasterSP.UpdatePOSHoldBillStatus(strHoldMasterIdToEdit, lblBillNo.Text);//updated sheena 04-05-2024
            }

            //CreditNote Status Update
            if (InfoPOSSalesMaster.CreditNoteNo != "")
            {
                POSSalesReturnMasterSP POSSalesReturnMasterSP = new POSSalesReturnMasterSP();
                POSSalesReturnMasterInfo InfoPOSSalesReturnMaster = new POSSalesReturnMasterInfo();

                InfoPOSSalesReturnMaster.InvoiceNo = InfoPOSSalesMaster.CreditNoteNo;
                InfoPOSSalesReturnMaster.SalesInvoiceNo = lblBillNo.Text;
                InfoPOSSalesReturnMaster.UserId = PublicVariables._currentUserId;
                POSSalesReturnMasterSP.POSCreditNoteMasterEdit(InfoPOSSalesReturnMaster);
            }

            return strMasterId;
        }
        public string SaveDeletedSaleHistory()
        {
            string strMasterId = "";

            lblBillNo.Text = POSBillNumberMax();

            POSSalesMasterInfo InfoPOSSalesMaster = new POSSalesMasterInfo();
            InfoPOSSalesMaster.InvoiceNo = lblBillNo.Text;
            InfoPOSSalesMaster.BillDate = Convert.ToDateTime(lblBillDate.Text);
            InfoPOSSalesMaster.BillTime = lblBillTime.Text;
            InfoPOSSalesMaster.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
            InfoPOSSalesMaster.CounterId = PublicVariables._counterId;
            InfoPOSSalesMaster.SessionNo = lblSessionNO.Text;
            InfoPOSSalesMaster.LedgerId = lblLedgerId.Text.ToString().Trim();
            InfoPOSSalesMaster.LedgerName = txtCustomerName.Text.ToString().Trim();
            InfoPOSSalesMaster.SubTotalAmount = Convert.ToDecimal(txtSubTotal.Text);
            InfoPOSSalesMaster.BillDiscPer = Convert.ToDecimal(txtDiscPer.Text);
            InfoPOSSalesMaster.BillDiscAmount = Convert.ToDecimal(txtDiscAmt.Text);
            InfoPOSSalesMaster.TaxableAmount = Convert.ToDecimal(txtTaxable.Text);
            InfoPOSSalesMaster.TotalTaxAmount = Convert.ToDecimal(txtTaxAmt.Text);
            InfoPOSSalesMaster.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            InfoPOSSalesMaster.TotalQty = Convert.ToDecimal(lblTotalQty.Text);
            InfoPOSSalesMaster.PaidAmount = 0;
            InfoPOSSalesMaster.BalanceAmount = 0;
            InfoPOSSalesMaster.CreditCardNo = "";
            InfoPOSSalesMaster.CreditCardAmount = 0;
            InfoPOSSalesMaster.UPIAmount = 0;
            InfoPOSSalesMaster.CreditAmount = 0;
            InfoPOSSalesMaster.CashAmount = 0;
            InfoPOSSalesMaster.CashPaidAmount = 0;
            InfoPOSSalesMaster.CreditNoteNo = "";
            InfoPOSSalesMaster.CreditNoteAmount = 0;
            InfoPOSSalesMaster.UserId = PublicVariables._currentUserId;
            InfoPOSSalesMaster.SalesMode = "Take Way";
            InfoPOSSalesMaster.CustomerAddress = txtAdress.Text.ToString();
            InfoPOSSalesMaster.CustomerPhone = txtphone.Text.ToString();
            InfoPOSSalesMaster.CustomerVATNo = txtVatNo.Text.ToString();


            strMasterId = POSSalesMasterSP.POSDeletedSalesMasterHistoryAdd(InfoPOSSalesMaster);

            if (strMasterId != "")
            {
                POSSalesDetails1Info InfoPOSSalesDetails1 = new POSSalesDetails1Info();

                InfoPOSSalesDetails1.POSSalesMasterId = strMasterId;
                InfoPOSSalesDetails1.InvoiceNo = lblBillNo.Text;
                InfoPOSSalesDetails1.BillDate = Convert.ToDateTime(lblBillDate.Text);
                InfoPOSSalesDetails1.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
                InfoPOSSalesDetails1.CounterId = PublicVariables._counterId;
                InfoPOSSalesDetails1.SessionNo = lblSessionNO.Text;
                InfoPOSSalesDetails1.UserId = PublicVariables._currentUserId;

                foreach (DataGridViewRow dgvrowCurChk in dgvProduct.Rows)
                {
                    if (!dgvrowCurChk.IsNewRow)
                    {
                        InfoPOSSalesDetails1.LineNumber = int.Parse(dgvrowCurChk.Cells["SLNo"].Value.ToString());
                        InfoPOSSalesDetails1.ProductCode = dgvrowCurChk.Cells["ProductCode"].Value.ToString();
                        InfoPOSSalesDetails1.Barcode = dgvrowCurChk.Cells["Barcode"].Value.ToString();
                        InfoPOSSalesDetails1.ProductName = dgvrowCurChk.Cells["ItemName"].Value.ToString();
                        InfoPOSSalesDetails1.UnitId = dgvrowCurChk.Cells["UnitId"].Value.ToString();
                        InfoPOSSalesDetails1.Qty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.Rate = decimal.Parse(dgvrowCurChk.Cells["SalesRate"].Value.ToString());
                        InfoPOSSalesDetails1.ExcludeRate = decimal.Parse(dgvrowCurChk.Cells["ExcludeRate"].Value.ToString());
                        InfoPOSSalesDetails1.CostPrice = decimal.Parse(dgvrowCurChk.Cells["PurchaseRate"].Value.ToString());
                        InfoPOSSalesDetails1.GrossValue = decimal.Parse(dgvrowCurChk.Cells["GrossValue"].Value.ToString());
                        InfoPOSSalesDetails1.DiscPer = decimal.Parse("0".ToString());
                        InfoPOSSalesDetails1.DiscAmount = decimal.Parse(dgvrowCurChk.Cells["DiscAmt"].Value.ToString());
                        InfoPOSSalesDetails1.NetAmount = decimal.Parse(dgvrowCurChk.Cells["NetValue"].Value.ToString());
                        InfoPOSSalesDetails1.TaxId = dgvrowCurChk.Cells["TaxId"].Value.ToString();
                        InfoPOSSalesDetails1.TaxPer = decimal.Parse(dgvrowCurChk.Cells["TaxPerc"].Value.ToString());
                        InfoPOSSalesDetails1.TaxAmount = decimal.Parse(dgvrowCurChk.Cells["TaxAmt"].Value.ToString());
                        InfoPOSSalesDetails1.Amount = decimal.Parse(dgvrowCurChk.Cells["Total"].Value.ToString());
                        InfoPOSSalesDetails1.BillDiscAmountperItem = dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value == null ? 0 : decimal.Parse(dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value.ToString());
                        InfoPOSSalesDetails1.ConversionFactor = decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoPOSSalesDetails1.AmountBeforeDisc = decimal.Parse(dgvrowCurChk.Cells["amountBeforeDisc"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.RateDiscAmount = decimal.Parse(dgvrowCurChk.Cells["rateDiscAmount"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.OfferId = dgvrowCurChk.Cells["offerId"].Value.ToString();

                        string strPOSSalesDetails1Id = "";

                        strPOSSalesDetails1Id = POSSalesDetails1SP.POSDeletedSalesDetails1HistoryAdd(InfoPOSSalesDetails1);
                    }
                }    
            }
            return strMasterId;
        }
        public String SaveGridDeletedItem()
        {

            string strPOSSalesDetails1Id = "";
           
            
                POSSalesDetails1Info InfoPOSSalesDetails1 = new POSSalesDetails1Info();

                InfoPOSSalesDetails1.POSSalesMasterId = "";
                InfoPOSSalesDetails1.InvoiceNo = lblBillNo.Text;
                InfoPOSSalesDetails1.BillDate = Convert.ToDateTime(lblBillDate.Text);
                InfoPOSSalesDetails1.BillTime = lblBillTime.Text;
                InfoPOSSalesDetails1.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
                InfoPOSSalesDetails1.CounterId = PublicVariables._counterId;
                InfoPOSSalesDetails1.SessionNo = lblSessionNO.Text;
                InfoPOSSalesDetails1.UserId = PublicVariables._currentUserId;

                foreach (DataGridViewRow dgvrowCurChk in dgvProduct.Rows)
                {
                    if (!dgvrowCurChk.IsNewRow)
                    {
                        InfoPOSSalesDetails1.LineNumber = int.Parse(dgvrowCurChk.Cells["SLNo"].Value.ToString());
                        InfoPOSSalesDetails1.ProductCode = dgvrowCurChk.Cells["ProductCode"].Value.ToString();
                        InfoPOSSalesDetails1.Barcode = dgvrowCurChk.Cells["Barcode"].Value.ToString();
                        InfoPOSSalesDetails1.ProductName = dgvrowCurChk.Cells["ItemName"].Value.ToString();
                        InfoPOSSalesDetails1.UnitId = dgvrowCurChk.Cells["UnitId"].Value.ToString();
                        InfoPOSSalesDetails1.Qty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.Rate = decimal.Parse(dgvrowCurChk.Cells["SalesRate"].Value.ToString());
                        InfoPOSSalesDetails1.ExcludeRate = decimal.Parse(dgvrowCurChk.Cells["ExcludeRate"].Value.ToString());
                        InfoPOSSalesDetails1.CostPrice = decimal.Parse(dgvrowCurChk.Cells["PurchaseRate"].Value.ToString());
                        InfoPOSSalesDetails1.GrossValue = decimal.Parse(dgvrowCurChk.Cells["GrossValue"].Value.ToString());
                        InfoPOSSalesDetails1.DiscPer = decimal.Parse("0".ToString());
                        InfoPOSSalesDetails1.DiscAmount = decimal.Parse(dgvrowCurChk.Cells["DiscAmt"].Value.ToString());
                        InfoPOSSalesDetails1.NetAmount = decimal.Parse(dgvrowCurChk.Cells["NetValue"].Value.ToString());
                        InfoPOSSalesDetails1.TaxId = dgvrowCurChk.Cells["TaxId"].Value.ToString();
                        InfoPOSSalesDetails1.TaxPer = decimal.Parse(dgvrowCurChk.Cells["TaxPerc"].Value.ToString());
                        InfoPOSSalesDetails1.TaxAmount = decimal.Parse(dgvrowCurChk.Cells["TaxAmt"].Value.ToString());
                        InfoPOSSalesDetails1.Amount = decimal.Parse(dgvrowCurChk.Cells["Total"].Value.ToString());
                        InfoPOSSalesDetails1.BillDiscAmountperItem = dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value == null ? 0 : decimal.Parse(dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value.ToString());
                        InfoPOSSalesDetails1.ConversionFactor = decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoPOSSalesDetails1.AmountBeforeDisc = decimal.Parse(dgvrowCurChk.Cells["amountBeforeDisc"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.RateDiscAmount = decimal.Parse(dgvrowCurChk.Cells["rateDiscAmount"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.OfferId = dgvrowCurChk.Cells["offerId"].Value.ToString();

                  

                        strPOSSalesDetails1Id = POSSalesDetails1SP.POSGridDeletedItemHistoryAdd(InfoPOSSalesDetails1);
                    }
                }
            
            return strPOSSalesDetails1Id;
        }
        public string HoldBillSaveFunction()
        {
            string strHoldMasterId = "";

            lblBillNo.Text = POSBillNumberMax();

            POSSalesMasterInfo InfoPOSSalesMaster = new POSSalesMasterInfo();
            InfoPOSSalesMaster.InvoiceNo = lblBillNo.Text;
            InfoPOSSalesMaster.BillDate = Convert.ToDateTime(lblBillDate.Text);
            InfoPOSSalesMaster.BillTime = lblBillTime.Text;
            InfoPOSSalesMaster.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
            InfoPOSSalesMaster.CounterId = PublicVariables._counterId;
            InfoPOSSalesMaster.SessionNo = lblSessionNO.Text;
            InfoPOSSalesMaster.LedgerId = "";
            InfoPOSSalesMaster.LedgerName = "";
            InfoPOSSalesMaster.SubTotalAmount = Convert.ToDecimal(txtSubTotal.Text);
            InfoPOSSalesMaster.BillDiscPer = Convert.ToDecimal(txtDiscPer.Text);
            InfoPOSSalesMaster.BillDiscAmount = Convert.ToDecimal(txtDiscAmt.Text);
            InfoPOSSalesMaster.TaxableAmount = Convert.ToDecimal(txtTaxable.Text);
            InfoPOSSalesMaster.TotalTaxAmount = Convert.ToDecimal(txtTaxAmt.Text);
            InfoPOSSalesMaster.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            InfoPOSSalesMaster.TotalQty = Convert.ToDecimal(lblTotalQty.Text);
            InfoPOSSalesMaster.PaidAmount = 0m;
            InfoPOSSalesMaster.BalanceAmount = 0m;
            InfoPOSSalesMaster.CreditCardNo = "";
            InfoPOSSalesMaster.CreditCardAmount = 0m;
            InfoPOSSalesMaster.UPIAmount = 0m;
            InfoPOSSalesMaster.CreditAmount = 0m;
            InfoPOSSalesMaster.CashAmount = 0m;
            InfoPOSSalesMaster.CashPaidAmount = 0m;
            InfoPOSSalesMaster.CreditNoteNo = "";
            InfoPOSSalesMaster.CreditNoteAmount = 0m;
            InfoPOSSalesMaster.UserId = PublicVariables._currentUserId;

            if (strHoldMasterIdToEdit == "")
            {
                strHoldMasterId = POSSalesMasterSP.POSHoldMasterAdd(InfoPOSSalesMaster);
            }
            else
            {
                InfoPOSSalesMaster.POSSalesMasterId = strHoldMasterIdToEdit;
                strHoldMasterId = POSSalesMasterSP.POSHoldMasterEdit(InfoPOSSalesMaster);
            }


            if (strHoldMasterId != "")
            {

                POSSalesDetails1Info InfoPOSSalesDetails1 = new POSSalesDetails1Info();

                InfoPOSSalesDetails1.POSSalesMasterId = strHoldMasterId;
                InfoPOSSalesDetails1.InvoiceNo = lblBillNo.Text;
                InfoPOSSalesDetails1.BillDate = Convert.ToDateTime(lblBillDate.Text);
                InfoPOSSalesDetails1.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
                InfoPOSSalesDetails1.CounterId = PublicVariables._counterId;
                InfoPOSSalesDetails1.SessionNo = lblSessionNO.Text;
                InfoPOSSalesDetails1.UserId = PublicVariables._currentUserId;

                //Delete All records from details table under holdmasterId
                POSSalesDetails1SP.POSHoldDetails1Delete(strHoldMasterId);


                foreach (DataGridViewRow dgvrowCurChk in dgvProduct.Rows)
                {
                    if (!dgvrowCurChk.IsNewRow)
                    {
                        InfoPOSSalesDetails1.LineNumber = int.Parse(dgvrowCurChk.Cells["SLNo"].Value.ToString());
                        InfoPOSSalesDetails1.ProductCode = dgvrowCurChk.Cells["ProductCode"].Value.ToString();
                        InfoPOSSalesDetails1.Barcode = dgvrowCurChk.Cells["Barcode"].Value.ToString();
                        InfoPOSSalesDetails1.ProductName = dgvrowCurChk.Cells["ItemName"].Value.ToString();
                        InfoPOSSalesDetails1.UnitId = dgvrowCurChk.Cells["UnitId"].Value.ToString();
                        InfoPOSSalesDetails1.Qty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.Rate = decimal.Parse(dgvrowCurChk.Cells["SalesRate"].Value.ToString());
                        InfoPOSSalesDetails1.ExcludeRate = decimal.Parse(dgvrowCurChk.Cells["ExcludeRate"].Value.ToString());
                        InfoPOSSalesDetails1.CostPrice = decimal.Parse(dgvrowCurChk.Cells["PurchaseRate"].Value.ToString());
                        InfoPOSSalesDetails1.GrossValue = decimal.Parse(dgvrowCurChk.Cells["GrossValue"].Value.ToString());
                        InfoPOSSalesDetails1.DiscPer = decimal.Parse("0".ToString());
                        InfoPOSSalesDetails1.DiscAmount = decimal.Parse(dgvrowCurChk.Cells["DiscAmt"].Value.ToString());
                        InfoPOSSalesDetails1.NetAmount = decimal.Parse(dgvrowCurChk.Cells["NetValue"].Value.ToString());
                        InfoPOSSalesDetails1.TaxId = dgvrowCurChk.Cells["TaxId"].Value.ToString();
                        InfoPOSSalesDetails1.TaxPer = decimal.Parse(dgvrowCurChk.Cells["TaxPerc"].Value.ToString());
                        InfoPOSSalesDetails1.TaxAmount = decimal.Parse(dgvrowCurChk.Cells["TaxAmt"].Value.ToString());
                        InfoPOSSalesDetails1.Amount = decimal.Parse(dgvrowCurChk.Cells["Total"].Value.ToString());
                        InfoPOSSalesDetails1.BillDiscAmountperItem = dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value == null ? 0 : decimal.Parse(dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value.ToString());
                        InfoPOSSalesDetails1.ConversionFactor = decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoPOSSalesDetails1.AmountBeforeDisc = decimal.Parse(dgvrowCurChk.Cells["amountBeforeDisc"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.RateDiscAmount = decimal.Parse(dgvrowCurChk.Cells["rateDiscAmount"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesDetails1.OfferId = dgvrowCurChk.Cells["offerId"].Value.ToString();

                        string strPOSHoldDetails1Id = "";

                        strPOSHoldDetails1Id = POSSalesDetails1SP.POSHoldDetails1Add(InfoPOSSalesDetails1);
                    }
                }


            }



            return strHoldMasterId;
        }
        private void TextBoxCellEditControlKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dgvProduct.CurrentCell != null)
                {
                    if (dgvProduct.CurrentCell.ColumnIndex != -1)
                    {
                        string strColName = dgvProduct.Columns[dgvProduct.CurrentCell.ColumnIndex].Name;
                        if (strColName == "SalesRate" || strColName == "Qty" || strColName == "DiscAmt")
                        {
                            bool isNegative = false;
                            if (strColName == "Qty")
                                isNegative = true;
                            objComboValidation.DecimalValidationGRid(sender, e, TextBoxControl, isNegative);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SI75:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void NumberKeyPressFunction(string strPressedKey)
        {
            if (strFocusedControl == "Barcode")
            {
                txtBarcode.Text = txtBarcode.Text + strPressedKey;
            }
            else if (strFocusedControl == "BillDiscAmt")
            {
                txtDiscAmt.Text = txtDiscAmt.Text + strPressedKey;
            }
            else if (strFocusedControl == "BillDiscPer")
            {
                txtDiscPer.Text = txtDiscPer.Text + strPressedKey;
            }
        }
        private void CalculateBillDiscforIndivProduct()
        {
            decimal dcDiscAmt = 0;
            decimal dcDiscPer = 0;
            try { dcDiscPer = decimal.Parse(txtDiscPer.Text.ToString()); }
            catch { }

            //foreach (DataGridViewRow dgvrow in dgvProduct.Rows)
            for (int i = 0; i < dgvProduct.Rows.Count; i++)
            {
                if (dgvProduct.Rows[i].Cells["NetValue"].Value != null)
                {
                    dcDiscAmt = decimal.Parse(dgvProduct.Rows[i].Cells["NetValue"].Value.ToString()) * dcDiscPer / 100;
                    dgvProduct.Rows[i].Cells["BillDiscIndProductAmt"].Value = dcDiscAmt.ToString();
                    CalculateGridTotal(i);
                }
            }
        }
        private void CalculateBillTotal()
        {
            if (dgvProduct.RowCount > 1)
            {
                decimal dcSubTotal = 0;
                decimal dcQtyTotal = 0;
                decimal dcTaxable = 0;
                decimal dcTaxAmt = 0;
                decimal dcTotal = 0;

                foreach (DataGridViewRow dgvrow in dgvProduct.Rows)
                {
                    if (dgvrow.Cells["NetValue"].Value != null)
                    {
                        if (dgvrow.Cells["NetValue"].Value.ToString() != "")
                        {
                            dcSubTotal = dcSubTotal + decimal.Parse(dgvrow.Cells["NetValue"].Value.ToString());
                        }
                    }
                    if (dgvrow.Cells["Qty"].Value != null)
                    {
                        if (dgvrow.Cells["Qty"].Value.ToString() != "")
                        {
                            dcQtyTotal = dcQtyTotal + decimal.Parse(dgvrow.Cells["Qty"].Value.ToString());
                        }
                    }

                    if (dgvrow.Cells["TaxPerc"].Value != null)
                    {
                        if (dgvrow.Cells["TaxPerc"].Value.ToString() != "")
                        {
                            decimal dTaxPerc = 0;
                            decimal dcItemBillDisc = 0;
                            try { dTaxPerc = decimal.Parse(dgvrow.Cells["TaxPerc"].Value.ToString()); }
                            catch { }
                            try { dcItemBillDisc = decimal.Parse(dgvrow.Cells["BillDiscIndProductAmt"].Value.ToString()); }
                            catch { }

                            if (dTaxPerc != 0)
                            {
                                dcTaxable = dcTaxable + (decimal.Parse(dgvrow.Cells["NetValue"].Value.ToString()) - dcItemBillDisc);
                            }
                        }
                    }

                    if (dgvrow.Cells["TaxAmt"].Value != null)
                    {
                        if (dgvrow.Cells["TaxAmt"].Value.ToString() != "")
                        {
                            dcTaxAmt = dcTaxAmt + decimal.Parse(dgvrow.Cells["TaxAmt"].Value.ToString());
                        }
                    }

                    if (dgvrow.Cells["Total"].Value != null)
                    {
                        if (dgvrow.Cells["Total"].Value.ToString() != "")
                        {
                            dcTotal = dcTotal + decimal.Parse(dgvrow.Cells["Total"].Value.ToString());
                        }
                    }
                }

                lblTotalQty.Text = Math.Round(dcQtyTotal, SettingsInfo._roundDecimal).ToString();
                txtSubTotal.Text = Math.Round(dcSubTotal, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                txtTaxable.Text = Math.Round(dcTaxable, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                txtTaxAmt.Text = Math.Round(dcTaxAmt, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                txtTotal.Text = Math.Round(dcTotal, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);

            }
        }
        private void CalculateGridTotal(int inIndex)
        {
            decimal dQty = 0;
            decimal dRate = 0;
            decimal dIncludeRate = 0;
            decimal dDiscAmt = 0;
            decimal dBillItemDisc = 0;
            decimal dGrossValue = 0;
            decimal dNetValue = 0;
            decimal dTaxPerc = 0;
            decimal dTaxAmt = 0;

            try { dQty = decimal.Parse(dgvProduct.Rows[inIndex].Cells["Qty"].Value.ToString()); }
            catch { }
            try { dRate = decimal.Parse(dgvProduct.Rows[inIndex].Cells["ExcludeRate"].Value.ToString()); }
            catch { }
            try { dIncludeRate = decimal.Parse(dgvProduct.Rows[inIndex].Cells["SalesRate"].Value.ToString()); }
            catch { }
            try { dDiscAmt = decimal.Parse(dgvProduct.Rows[inIndex].Cells["DiscAmt"].Value.ToString()); }
            catch { }
            try { dBillItemDisc = decimal.Parse(dgvProduct.Rows[inIndex].Cells["BillDiscIndProductAmt"].Value.ToString()); }
            catch { }
            try { dTaxPerc = decimal.Parse(dgvProduct.Rows[inIndex].Cells["TaxPerc"].Value.ToString()); }
            catch { }

            dgvProduct.Rows[inIndex].Cells["DiscAmt"].Value = Math.Round(dDiscAmt, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


            dGrossValue = dQty * dRate;


            dNetValue = dGrossValue - dDiscAmt;


            if (dTaxPerc != 0)
            {
                dTaxAmt = Math.Round((((dNetValue - dBillItemDisc) * dTaxPerc) / (100)), SettingsInfo._roundDecimal);

                dgvProduct.Rows[inIndex].Cells["TaxAmt"].Value = dTaxAmt.ToString(SettingsInfo._roundDecimalPart);
            }
            else
            {
                dTaxAmt = 0;
                dgvProduct.Rows[inIndex].Cells["TaxAmt"].Value = "0.00";
            }

            //Adjestment
            decimal dcNetTot = 0;
            decimal dcTot = 0;
            dcNetTot = dGrossValue + dTaxAmt;
            dcTot = dQty * dIncludeRate;

            if (dcNetTot != dcTot && dDiscAmt == 0 && dBillItemDisc == 0)
            {
                decimal dcDiff = dcTot - dcNetTot;
                dNetValue += dcDiff;
            }
            //

            dgvProduct.Rows[inIndex].Cells["GrossValue"].Value = Math.Round(dGrossValue, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            dgvProduct.Rows[inIndex].Cells["NetValue"].Value = Math.Round(dNetValue, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);

            dgvProduct.Rows[inIndex].Cells["Total"].Value = ((dNetValue - dBillItemDisc) + dTaxAmt).ToString(SettingsInfo._roundDecimalPart);

            CalculateBillTotal();

            if (counterInfo.DisplayStatus == true)
            {
                PoleDisplay("BarcodeScan");
            }

        }
        private void AssignExludeRate(int inIndex)
        {
            //-------------------TAX INCLUDED
            if (!isRateChanged)
            {
                decimal dcRate = 0;
                decimal dcTaxExcludedRate = 0;
                bool isIncluded = false;

                if (dgvProduct.Rows[inIndex].Cells["SalesRate"].Value != null && dgvProduct.Rows[inIndex].Cells["SalesRate"].Value.ToString() != "")
                {
                    if (dgvProduct.Rows[inIndex].Cells["SalesRate"].Value != null)
                    {
                        decimal.TryParse(dgvProduct.Rows[inIndex].Cells["SalesRate"].Value.ToString(), out dcRate);
                    }

                    dgvProduct.Rows[inIndex].Cells["SalesRate"].Value = Math.Round(dcRate, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                    if (SettingsInfo._taxType == "Applicable to product" && dcRate != 0)
                    {
                        if (SettingsInfo._vatIncluded == true || SettingsInfo._vatCessIncluded == true)
                        {
                            if (dgvProduct.Rows[inIndex].Cells["TaxId"].Value != null && dgvProduct.Rows[inIndex].Cells["TaxId"].Value.ToString() != "")
                            {
                                isIncluded = true;
                                decimal dTaxPerc = 0;
                                decimal.TryParse(dgvProduct.Rows[inIndex].Cells["TaxPerc"].Value.ToString(), out dTaxPerc);

                                decimal dTaxAmt = 0;

                                dTaxAmt = ((dcRate * dTaxPerc) / (dTaxPerc + 100));

                                dcTaxExcludedRate = dcRate - dTaxAmt;
                                dcTaxExcludedRate = Math.Round(dcTaxExcludedRate, SettingsInfo._roundDecimal);
                            }

                        }
                    }
                }
                if (!isIncluded)
                    dcTaxExcludedRate = dcRate;
                //isUserRateChanged = true;
                dgvProduct.Rows[inIndex].Cells["ExcludeRate"].Value = dcTaxExcludedRate.ToString();
                //isUserRateChanged = false;
            }
        }
        public void LoadHoldBillDetails()
        {
            DataTable dtblhold = new DataTable();
            dtblhold = POSSalesMasterSP.POSHoldMasterViewByPOSHoldMasterId(txtBarcode.Text.Trim());
            if (dtblhold.Rows.Count > 0)
            {
                strHoldMasterIdToEdit = dtblhold.Rows[0]["POSHoldMasterId"].ToString();
                txtDiscAmt.Text = dtblhold.Rows[0]["billDiscAmount"].ToString();
                txtDiscPer.Text = dtblhold.Rows[0]["billDiscPer"].ToString();

                DataTable dtblholdDetails1 = new DataTable();
                dtblholdDetails1 = POSSalesMasterSP.POSHoldDetails1ViewByPOSHoldMasterId(strHoldMasterIdToEdit);
                if (dtblholdDetails1.Rows.Count > 0)
                {
                    dgvProduct.Rows.Clear();
                    foreach (DataRow drowDetails in dtblholdDetails1.Rows)
                    {
                        dgvProduct.Rows.Add();

                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["SLNo"].Value = drowDetails["LineNumber"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["ProductCode"].Value = drowDetails["productCode"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["Barcode"].Value = drowDetails["barcode"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["ItemName"].Value = drowDetails["productName"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["Qty"].Value = drowDetails["qty"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["UnitId"].Value = drowDetails["unitId"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["Unit"].Value = drowDetails["unitName"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["BaseUnitId"].Value = drowDetails["BaseUnitId"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["UnitConversion"].Value = drowDetails["ConversionFactor"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["SalesRate"].Value = Convert.ToDecimal(drowDetails["rate"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["ExcludeRate"].Value = Convert.ToDecimal(drowDetails["excludeRate"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["PurchaseRate"].Value = Convert.ToDecimal(drowDetails["costPrice"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["GrossValue"].Value = Convert.ToDecimal(drowDetails["grossValue"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["DiscAmt"].Value = Convert.ToDecimal(drowDetails["discAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["NetValue"].Value = Convert.ToDecimal(drowDetails["netAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxId"].Value = drowDetails["taxId"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["ArabicName"].Value = drowDetails["ArabicName"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxPerc"].Value = drowDetails["taxPer"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxAmt"].Value = Convert.ToDecimal(drowDetails["taxAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["Total"].Value = Convert.ToDecimal(drowDetails["Amount"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["BillDiscIndProductAmt"].Value = drowDetails["billDiscAmountperItem"].ToString();

                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["amountBeforeDisc"].Value = Convert.ToDecimal(drowDetails["amountBeforeDisc"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["rateDiscAmount"].Value = Convert.ToDecimal(drowDetails["rateDiscAmount"]).ToString(SettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["offerId"].Value = drowDetails["offerId"].ToString();


                    }

                    CalculateBillTotal();


                    dgvSlno = dgvProduct.RowCount;
                    dgvCurRow = dgvProduct.RowCount - 1;
                    dgvProduct.CurrentCell = dgvProduct.Rows[dgvCurRow - 1].Cells["Barcode"];

                    barcodeFocus();
                }
            }
            else
            {
                MessageBox.Show("Record not found in this Bill No", "WARNING");
                barcodeFocus();
            }
        }
        public void barcodeScanning()
        {
            string strBarcode = "";
            string strItemCode = "";
            string strItemName = "";
            string strItemNameArabic = "";
            string strUnitId = "";
            string strUnitName = "";
            string strBaseUnitId = "";
            decimal decUnitConversion = 1;
            decimal decSalesPrice = 0;
            string strScaleItemType = "";
            string strScaleQtyPart = "";
            string strMasterId = "";
            // bool IsDiscounted = false;
            decimal amountBeforeDisc = 0;
            decimal rateDiscAmount = 0;
            string offerId = "";
            DataTable dtblSalesRate = new DataTable();

            if (lblTenderTotalAmount.Visible == true)
            {
                lblTenderTotalAmount.Visible = false;
                lblTenderBalanceAmount.Visible = false;
                lblTenderTotal.Visible = false;
                lblTenderBalance.Visible = false;
                lblTenderTotalAmount.Text = "0";
                lblTenderBalanceAmount.Text = "0";
            }

            //Re Call Hold Bill Details
            if (lblBarcodeScanningType.Visible == true && lblBarcodeScanningType.Text == "Scan Hold Bill No")
            {
                LoadHoldBillDetails();
                lblBarcodeScanningType.Text = "";
                lblBarcodeScanningType.Visible = false;

                return;
            }



            // Delete Product from Grid when barcode is entered
            bool IsBarcodeExist = false; // added on 12-04-2025 by Nishana
            int RoWnO = -1;
            string Barcode = txtBarcode.Text.Trim();

            if (lblBarcodeScanningType.Visible == true && lblBarcodeScanningType.Text == "Delete Product")
            {

                for (int i = 0; i < dgvProduct.RowCount; i++)
                {
                    if (dgvProduct.Rows[i].Cells["Barcode"].Value != null && dgvProduct.Rows[i].Cells["Barcode"].Value.ToString() == Barcode)
                    {
                        IsBarcodeExist = true;
                        RoWnO = i;
                        break;

                    }
                }
                if (IsBarcodeExist == true)
                {
                    if (!dgvProduct.Rows[RoWnO].IsNewRow)
                    {
                        SaveGridDeletedItem();
                        DeleteRow(RoWnO);

                        if (dgvProduct.Rows.Count > 1)
                        {
                            dgvCurRow = dgvProduct.Rows.Count - 1;
                        }
                        else
                        {
                            dgvCurRow = 0;
                            dgvSlno = 1;
                        }

                        CalculateBillDiscforIndivProduct();
                        CalculateBillTotal();
                    }
                }

                lblBarcodeScanningType.Text = "";
                lblBarcodeScanningType.Visible = false;
                txtBarcode.Text = "";

                return;
            }

            POSSettingsInfo settingsinfo = new POSSettingsInfo();

            DataTable dtbl = new DataTable();
            dtbl = SPGeneral.GetProductDetailsByBarcode(txtBarcode.Text.Trim());
            if (dtbl.Rows.Count > 0) //load details by Barcode
            {
                strBarcode = txtBarcode.Text.Trim();
                strItemCode = dtbl.Rows[0]["productCode"].ToString();
                strItemName = dtbl.Rows[0]["productName"].ToString();
                strItemNameArabic = dtbl.Rows[0]["ArabicName"].ToString();
                strUnitId = dtbl.Rows[0]["unitId"].ToString();
                strUnitName = dtbl.Rows[0]["unitName"].ToString();
                strBaseUnitId = dtbl.Rows[0]["BaseUnitId"].ToString();
                decUnitConversion = Convert.ToDecimal(dtbl.Rows[0]["conversionRate"].ToString());
                dtblSalesRate = SPGeneral.ProductSalesRateForSalePOS(strItemCode, "1", DateTime.Parse(lblBillDate.Text), strUnitId);
                decSalesPrice = Convert.ToDecimal(dtblSalesRate.Rows[0]["rate"].ToString());
                // IsDiscounted = Convert.ToBoolean(dtblSalesRate.Rows[0]["IsDiscounted"].ToString());
                amountBeforeDisc = Convert.ToDecimal(dtblSalesRate.Rows[0]["amountBeforeDisc"].ToString());
                rateDiscAmount = Convert.ToDecimal(dtblSalesRate.Rows[0]["rateDiscAmount"].ToString());
             

                offerId = dtblSalesRate.Rows[0]["offerId"].ToString();
                if (decSalesPrice == 0)
                {
                    Console.Beep(500, 500);
                    MessageBox.Show("Sales Price is ZERO", "WARNING");
                    barcodeFocus();
                    return;
                }
            }
            else
            {
                dtbl = SPGeneral.GetProductDetailsByProductCode(txtBarcode.Text.Trim());
                if (dtbl.Rows.Count > 0) //load details by ProductCode
                {
                    strBarcode = txtBarcode.Text.Trim();
                    strItemCode = dtbl.Rows[0]["productCode"].ToString();
                    strItemName = dtbl.Rows[0]["productName"].ToString();
                    strItemNameArabic = dtbl.Rows[0]["ArabicName"].ToString();
                    strUnitId = dtbl.Rows[0]["unitId"].ToString();
                    strUnitName = dtbl.Rows[0]["unitName"].ToString();
                    strBaseUnitId = dtbl.Rows[0]["unitId"].ToString();
                    decUnitConversion = Convert.ToDecimal(dtbl.Rows[0]["conversionRate"].ToString());

                    dtblSalesRate = SPGeneral.ProductSalesRateForSalePOS(strItemCode, "1", DateTime.Parse(lblBillDate.Text), strUnitId);
                    decSalesPrice = Convert.ToDecimal(dtblSalesRate.Rows[0]["rate"].ToString());
                    // IsDiscounted = Convert.ToBoolean(dtblSalesRate.Rows[0]["IsDiscounted"].ToString());
                    amountBeforeDisc = Convert.ToDecimal(dtblSalesRate.Rows[0]["amountBeforeDisc"].ToString());
                    rateDiscAmount = Convert.ToDecimal(dtblSalesRate.Rows[0]["rateDiscAmount"].ToString());
                    offerId = dtblSalesRate.Rows[0]["offerId"].ToString();
                    
                    if (decSalesPrice == 0)
                    {
                        Console.Beep(500, 500);
                        MessageBox.Show("Sales Price is ZERO", "WARNING");
                        barcodeFocus();
                        return;
                    }
                }
                else
                {
                    //for Scale Items Loading
                    if (txtBarcode.Text.Length == 13)
                    {
                        string strScaleProductCode = "";
                        decimal decScalePrice;

                        strScaleProductCode = txtBarcode.Text.Substring(0, 7).Trim();
                        decScalePrice = Convert.ToDecimal(txtBarcode.Text.Substring(7, 5).Trim());

                        dtbl = SPGeneral.GetScaleProductDetailsByProductCode(strScaleProductCode);
                        if (dtbl.Rows.Count > 0) //load scale product details by ProductCode
                        {
                            strBarcode = txtBarcode.Text.Trim();
                            strItemCode = dtbl.Rows[0]["productCode"].ToString();
                            strItemName = dtbl.Rows[0]["productName"].ToString();
                            strItemNameArabic = dtbl.Rows[0]["ArabicName"].ToString();
                            strUnitId = dtbl.Rows[0]["unitId"].ToString();
                            strUnitName = dtbl.Rows[0]["unitName"].ToString();
                            strBaseUnitId = dtbl.Rows[0]["unitId"].ToString();

                            decSalesPrice = decScalePrice / 1000;
                            strScaleItemType = dtbl.Rows[0]["category"].ToString();
                            if (strScaleItemType == "Inventory")
                            {
                                if (strUnitName.ToUpper().Trim() != "PCS")
                                {
                                    strScaleQtyPart = (decScalePrice / 1000).ToString();
                                }
                                else if (strUnitName.ToUpper().Trim() == "PCS")
                                {
                                    strScaleQtyPart = decScalePrice.ToString();
                                }
                                decSalesPrice = Convert.ToDecimal(dtbl.Rows[0]["salesPrice"].ToString());
                            }
                        }
                        else
                        {
                            Console.Beep(500, 500);
                            MessageBox.Show("Barcode not Found", "WARNING");
                            barcodeFocus();
                            return;
                        }
                    }
                    else
                    {
                        Console.Beep(500, 500);
                        MessageBox.Show("Barcode not Found", "WARNING");
                        barcodeFocus();
                        return;
                    }
                }
            }
            bool IsExist = false;   //added by Nishana for adding qty  of same barcode to grid

            if (strItemCode != "")
            {
                int Rownumber = 0;
                if (settingsinfo.AddQtyInSameBarcodeToGrid == true)
                {
                    string barcode = txtBarcode.Text.Trim();
                    if (String.IsNullOrEmpty(barcode))
                    {
                        MessageBox.Show("Please enter a valid barcode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int i = 0; i < dgvProduct.RowCount; i++)
                    {

                        if (dgvProduct.Rows[i].Cells["Barcode"].Value != null &&
                            dgvProduct.Rows[i].Cells["Barcode"].Value.ToString() == barcode)
                        {
                            IsExist = true;
                            Rownumber = i;
                            break;

                        }
                    }
                }

                if (IsExist)
                {
                    int currentQty = Convert.ToInt32(dgvProduct.Rows[Rownumber].Cells["Qty"].Value);
                    dgvProduct.Rows[Rownumber].Cells["Qty"].Value = currentQty + 1;


                    decimal dQty = 0;
                    decimal dRate = 0;
                    decimal dIncludeRate = 0;
                    decimal dGrossValue = 0;
                    decimal dTaxPerc = 0;
                    decimal dTaxAmt = 0;
                    try { dQty = decimal.Parse(dgvProduct.Rows[Rownumber].Cells["Qty"].Value.ToString()); }
                    catch { }

                    try { dRate = decimal.Parse(dgvProduct.Rows[Rownumber].Cells["ExcludeRate"].Value.ToString()); }
                    catch { }
                    try { dIncludeRate = decimal.Parse(dgvProduct.Rows[Rownumber].Cells["SalesRate"].Value.ToString()); }
                    catch { }

                    dGrossValue = dQty * dRate;

                    try { dTaxPerc = decimal.Parse(dgvProduct.Rows[Rownumber].Cells["TaxPerc"].Value.ToString()); }
                    catch { }

                    if (dTaxPerc != 0)
                    {
                        dTaxAmt = Math.Round(((dGrossValue * dTaxPerc) / (100)), SettingsInfo._roundDecimal);

                        dgvProduct.Rows[Rownumber].Cells["TaxAmt"].Value = dTaxAmt.ToString(SettingsInfo._roundDecimalPart);
                    }
                    else
                    {
                        dgvProduct.Rows[Rownumber].Cells["TaxAmt"].Value = "0.00";
                    }

                    //Adjestment
                    decimal dcNetTot = 0;
                    decimal dcTot = 0;
                    dcNetTot = dGrossValue + dTaxAmt;
                    dcTot = dQty * dIncludeRate;

                    if (dcNetTot != dcTot)
                    {
                        decimal dcDiff = dcTot - dcNetTot;
                        dGrossValue += dcDiff;
                    }
                    //

                    dgvProduct.Rows[Rownumber].Cells["GrossValue"].Value = Math.Round(dGrossValue, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    dgvProduct.Rows[Rownumber].Cells["DiscAmt"].Value = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);

                    dgvProduct.Rows[Rownumber].Cells["NetValue"].Value = Math.Round(dGrossValue, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                    dgvProduct.Rows[Rownumber].Cells["Total"].Value = (dGrossValue + dTaxAmt).ToString(SettingsInfo._roundDecimalPart);

                    CalculateBillTotal();

                    //dgvSlno = dgvSlno + 1;
                    //dgvCurRow = dgvCurRow + 1;

                    //dgvProduct.CurrentCell = dgvProduct.Rows[dgvCurRow - 1].Cells["Barcode"];

                    strBarcode = "";
                    strItemCode = "";
                    strItemName = "";
                    strItemNameArabic = "";
                    strUnitId = "";
                    strUnitName = "";
                    decUnitConversion = 0;
                    decSalesPrice = 0;
                    strScaleItemType = "";
                    strScaleQtyPart = "";
                   

                    if (lblBarcodeScanningType.Visible == true)
                    {
                        lblBarcodeScanningType.Text = "";
                        lblBarcodeScanningType.Visible = false;
                    }
                }
                else
                {
                    dgvProduct.RowCount = dgvCurRow + 2;
                    dgvProduct.Rows[dgvCurRow].Cells["SLNo"].Value = dgvSlno;
                    dgvProduct.Rows[dgvCurRow].Cells["ProductCode"].Value = strItemCode;
                    dgvProduct.Rows[dgvCurRow].Cells["Barcode"].Value = strBarcode;
                    dgvProduct.Rows[dgvCurRow].Cells["ItemName"].Value = strItemName;
                    dgvProduct.Rows[dgvCurRow].Cells["ArabicName"].Value = strItemNameArabic;
                    if (strScaleItemType == "Inventory")
                    {
                        dgvProduct.Rows[dgvCurRow].Cells["Qty"].Value = strScaleQtyPart;
                    }
                    else
                    {
                        if (lblBarcodeScanningType.Visible == true && lblBarcodeScanningType.Text == "Exchange Item")
                        {
                            dgvProduct.Rows[dgvCurRow].Cells["Qty"].Value = -1;
                        }
                        else
                        {
                            dgvProduct.Rows[dgvCurRow].Cells["Qty"].Value = 1;
                        }
                    }


                    dgvProduct.Rows[dgvCurRow].Cells["UnitId"].Value = strUnitId;
                    dgvProduct.Rows[dgvCurRow].Cells["Unit"].Value = strUnitName;
                    dgvProduct.Rows[dgvCurRow].Cells["BaseUnitId"].Value = strBaseUnitId;
                    dgvProduct.Rows[dgvCurRow].Cells["UnitConversion"].Value = decUnitConversion;

                   

                    //PurchaseRate
                    DataTable dtblRate = new DataTable();
                    decimal dRt = 0;

                    dtblRate = SPGeneral.ProductPurchaseRate(dgvProduct.Rows[dgvCurRow].Cells["ProductCode"].Value.ToString());


                    if (dtblRate.Rows.Count > 0)
                    {
                        dRt = Convert.ToDecimal(dtblRate.Rows[0]["rate"]);
                        dgvProduct.Rows[dgvCurRow].Cells["PurchaseRate"].Value = (dRt * decUnitConversion).ToString(SettingsInfo._roundDecimalPart);
                    }
                    else
                    {
                        dRt = 0;
                        dgvProduct.Rows[dgvCurRow].Cells["PurchaseRate"].Value = "0";
                    }

                    dgvProduct.Rows[dgvCurRow].Cells["SalesRate"].Value = Math.Round(decSalesPrice, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    dgvProduct.Rows[dgvCurRow].Cells["amountBeforeDisc"].Value = Math.Round(amountBeforeDisc, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    dgvProduct.Rows[dgvCurRow].Cells["rateDiscAmount"].Value = Math.Round(rateDiscAmount, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    dgvProduct.Rows[dgvCurRow].Cells["offerId"].Value = offerId;
                    if (rateDiscAmount > 0)
                        dgvProduct.Rows[dgvCurRow].DefaultCellStyle.ForeColor = Color.Red;

                    dtbl = SPGeneral.GetProductTaxDetails(strItemCode);
                    if (dtbl.Rows.Count > 0) //load tax details details by ProductCode
                    {
                        dgvProduct.Rows[dgvCurRow].Cells["TaxId"].Value = dtbl.Rows[0]["taxId"].ToString();
                        dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value = float.Parse(dtbl.Rows[0]["rate"].ToString());
                    }
                    else
                    {
                        dgvProduct.Rows[dgvCurRow].Cells["TaxId"].Value = "1";
                        dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value = 0;
                    }

                    AssignExludeRate(dgvCurRow);


                    decimal dQty = 0;
                    decimal dRate = 0;
                    decimal dIncludeRate = 0;
                    decimal dGrossValue = 0;
                    decimal dTaxPerc = 0;
                    decimal dTaxAmt = 0;
                    try { dQty = decimal.Parse(dgvProduct.Rows[dgvCurRow].Cells["Qty"].Value.ToString()); }
                    catch { }

                    try { dRate = decimal.Parse(dgvProduct.Rows[dgvCurRow].Cells["ExcludeRate"].Value.ToString()); }
                    catch { }
                    try { dIncludeRate = decimal.Parse(dgvProduct.Rows[dgvCurRow].Cells["SalesRate"].Value.ToString()); }
                    catch { }

                    dGrossValue = dQty * dRate;

                    try { dTaxPerc = decimal.Parse(dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value.ToString()); }
                    catch { }

                    if (dTaxPerc != 0)
                    {
                        dTaxAmt = Math.Round(((dGrossValue * dTaxPerc) / (100)), SettingsInfo._roundDecimal);

                        dgvProduct.Rows[dgvCurRow].Cells["TaxAmt"].Value = dTaxAmt.ToString(SettingsInfo._roundDecimalPart);
                    }
                    else
                    {
                        dgvProduct.Rows[dgvCurRow].Cells["TaxAmt"].Value = "0.00";
                    }

                    //Adjestment
                    decimal dcNetTot = 0;
                    decimal dcTot = 0;
                    dcNetTot = dGrossValue + dTaxAmt;
                    dcTot = dQty * dIncludeRate;

                    if (dcNetTot != dcTot)
                    {
                        decimal dcDiff = dcTot - dcNetTot;
                        dGrossValue += dcDiff;
                    }
                    //

                    dgvProduct.Rows[dgvCurRow].Cells["GrossValue"].Value = Math.Round(dGrossValue, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    dgvProduct.Rows[dgvCurRow].Cells["DiscAmt"].Value = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);

                    dgvProduct.Rows[dgvCurRow].Cells["NetValue"].Value = Math.Round(dGrossValue, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                    dgvProduct.Rows[dgvCurRow].Cells["Total"].Value = (dGrossValue + dTaxAmt).ToString(SettingsInfo._roundDecimalPart);

                    CalculateBillTotal();

                    dgvSlno = dgvSlno + 1;
                    dgvCurRow = dgvCurRow + 1;

                    dgvProduct.CurrentCell = dgvProduct.Rows[dgvCurRow - 1].Cells["Barcode"];

                    strBarcode = "";
                    strItemCode = "";
                    strItemName = "";
                    strItemNameArabic = "";
                    strUnitId = "";
                    strUnitName = "";
                    decUnitConversion = 0;
                    decSalesPrice = 0;
                    strScaleItemType = "";
                    strScaleQtyPart = "";

                    if (lblBarcodeScanningType.Visible == true)
                    {
                        lblBarcodeScanningType.Text = "";
                        lblBarcodeScanningType.Visible = false;
                    }

                }


                if (counterInfo.DisplayStatus == true)
                {
                    PoleDisplay("BarcodeScan");
                }

                barcodeFocus();
            }
            else
            {
                Console.Beep(500, 500);
                MessageBox.Show("Barcode not Found", "WARNING");
                barcodeFocus();
                return;
            }

        }
        #endregion

        #region Events


        private void frmPOSSales_Load(object sender, EventArgs e)
        {
            //this.Bounds = Screen.PrimaryScreen.WorkingArea;  
            this.WindowState = FormWindowState.Maximized;

            //MessageBox.Show(this.Size.Width.ToString());
            panelMain.Size = this.Size;
            panelBillDetails.Width = this.Size.Width;
            panelMainButton.Width = this.Size.Width;
            panelBarcode.Width = this.Size.Width;

            FormLoadFunction();
            ClearFunction();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblBillDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            lblBillTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void btnNewSale_Click(object sender, EventArgs e)
        {
   

            if (POSSettingsInfo._BillClearAuth == true)
            {
                IsAuthenticationApproved = false;
                string condition = "_BillClearAuth";
                frmUserAuthentication frm = new frmUserAuthentication();
                frm.CallFromPOSSales(this, condition);
                SaveDeletedSaleHistory();
                if (IsAuthenticationApproved)
                {
                    ClearFunction();
                }
                else
                {
                    barcodeFocus();
                }
            }
            else
            {
                SaveDeletedSaleHistory();
                ClearFunction();
            }

        }

        
        private void btnBarcode_Click(object sender, EventArgs e)
        {
            barcodeFocus();
        }


        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                barcodeScanning();
            }
        }

        private void txtDiscPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);

            if (e.KeyChar == Convert.ToChar(13))
            {
                barcodeFocus();
                txtDiscAmt.ReadOnly = true;
                txtDiscPer.ReadOnly = true;
            }
        }
        private void txtDiscAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
            if (e.KeyChar == Convert.ToChar(13))
            {
                barcodeFocus();
                txtDiscAmt.ReadOnly = true;
                txtDiscPer.ReadOnly = true;
            }
        }
        private void txtDiscAmt_Validated(object sender, EventArgs e)
        {
            decimal dcDiscAmt = 0;

            try { dcDiscAmt = decimal.Parse(txtDiscAmt.Text.ToString()); }
            catch { }

            if (dcDiscAmt != 0)
            {
                txtDiscAmt.Text = dcDiscAmt.ToString(SettingsInfo._roundDecimalPart);
                if (dcDiscAmt != 0 && Convert.ToDecimal(txtSubTotal.Text.ToString()) != 0)
                {
                    dcDiscAmt = dcDiscAmt * 100 / Convert.ToDecimal(txtSubTotal.Text.ToString());
                }
                dcDiscAmt = Math.Round(dcDiscAmt, SettingsInfo._roundDecimal);
                txtDiscPer.Text = dcDiscAmt.ToString("0.00");

            }
            else
            {
                if (strFocusedControl != "txtDiscAmt" && strFocusedControl != "txtDiscPer")
                {
                    txtDiscPer.Text = dcDiscAmt.ToString("0.00");
                    txtDiscAmt.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                }

            }

            CalculateBillDiscforIndivProduct();
        }

        private void txtDiscPer_Validated(object sender, EventArgs e)
        {
            decimal dcDiscPer = 0;

            try { dcDiscPer = decimal.Parse(txtDiscPer.Text.ToString()); }
            catch { }

            if (dcDiscPer != 0)
            {
                txtDiscPer.Text = dcDiscPer.ToString("0.00");
                if (dcDiscPer != 0)
                {
                    dcDiscPer = Convert.ToDecimal(txtSubTotal.Text.ToString()) * dcDiscPer / 100;
                }
                dcDiscPer = Math.Round(dcDiscPer, SettingsInfo._roundDecimal);
                txtDiscAmt.Text = dcDiscPer.ToString(SettingsInfo._roundDecimalPart);
            }
            else
            {
                if (strFocusedControl != "txtDiscAmt" && strFocusedControl != "txtDiscPer")
                {
                    txtDiscPer.Text = dcDiscPer.ToString("0.00");
                    txtDiscAmt.Text = Math.Round(0m, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                }

            }

            CalculateBillDiscforIndivProduct();
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F4 && e.Control == false) //Focus on Qty
            {
                if (dgvProduct.RowCount > 1)
                {
                    dgvProduct.Focus();

                    if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                    {
                        if (POSSettingsInfo._QtyChangeAuth == true)
                        {
                            IsAuthenticationApproved = false;
                            string condition = "_QtyChangeAuth";
                            frmUserAuthentication frm = new frmUserAuthentication();
                            frm.CallFromPOSSales(this, condition);
                            if (IsAuthenticationApproved)
                            {
                                Qty.ReadOnly = false;
                                dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["Qty"];
                            }
                            else
                            {
                                barcodeFocus();
                            }
                        }
                        else
                        {
                            Qty.ReadOnly = false;
                            dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["Qty"];
                        }
                    }
                    else
                    {
                        barcodeFocus();
                    }
                }
                else
                {
                    barcodeFocus();
                }

            }
            else if (e.KeyCode == Keys.F7 && e.Control == false)//Focus on Price Change
            {
                if (dgvProduct.RowCount > 1)
                {
                    dgvProduct.Focus();
                    SalesRate.ReadOnly = false;
                    if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                    {
                        if (POSSettingsInfo._PriceChangeAuth == true)
                        {
                            IsAuthenticationApproved = false;
                            string condition = "_PriceChangeAuth";
                            frmUserAuthentication frm = new frmUserAuthentication();
                            frm.CallFromPOSSales(this, condition);
                            if (IsAuthenticationApproved)
                                dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["SalesRate"];
                            else
                                barcodeFocus();
                        }
                        else
                        {
                            dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["SalesRate"];
                        }

                    }
                    else
                    {
                        barcodeFocus();
                    }
                }
                else
                {
                    barcodeFocus();
                }

            }
            else if (e.KeyCode == Keys.F7 && e.Control == false)//Focus on Line Disc
            {
                if (dgvProduct.RowCount > 1)
                {
                    dgvProduct.Focus();
                    DiscAmt.ReadOnly = false;
                    if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                    {
                        dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["DiscAmt"];
                    }
                    else
                    {
                        barcodeFocus();
                    }
                }
                else
                {
                    barcodeFocus();
                }
            }
            else if (e.KeyCode == Keys.F2 && e.Control == false)//New Sales
            {
                btnNewSale_Click(e, e);
            }
            else if (e.KeyCode == Keys.F9 && e.Control == false)//Bill DiscAmt
            {
                btnBillDiscAmt_Click(e, e);
            }
            else if (e.KeyCode == Keys.F5 && e.Control == false)//Exchange Item
            {
                btnExchange_Click(e, e);
            }
            else if (e.KeyCode == Keys.P && e.Control == true)//Price Check
            {
                btnPriceCheck_Click(e, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control == false)//Grid focus
            {
                if (dgvProduct.RowCount > 1)
                {
                    dgvProduct.Focus();
                    dgvProduct.CurrentCell = dgvProduct.Rows[dgvCurRow - 1].Cells["SLNo"];
                }
            }
            else if (e.KeyCode == Keys.F12 && e.Control == false)//Cash Save
            {
                isSavefromButton = false;
                //btnCash_Click(e, e);
                btnCash.PerformClick();
            }
            else if (e.KeyCode == Keys.D && e.Control == true)//Last bill print
            {
                btnLastBill_Click(e, e);
            }
            else if (e.KeyCode == Keys.F11 && e.Control == false)//Hold Bill
            {
                btnHold_Click(e, e);
            }
            else if (e.KeyCode == Keys.F8 && e.Control == false)//Un Hold Bill
            {
                btnUnhold_Click(e, e);
            }
            else if (e.KeyCode == Keys.F10 && e.Control == false)//find products
            {
                btnFindProduct_Click(e, e);
            }
            else if (e.KeyCode == Keys.L && e.Control == true)//Reciept copy print
            {
                btnReceiptCopy_Click(e, e);
            }
        }
        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtTouchTextBox.Name == "txtQty")
                {
                    if (strFocusedControl == "Qty")
                    {
                        decimal dQty = 0;
                        try { dQty = decimal.Parse(txtQty.Text.ToString()); }
                        catch { dQty = 0; }


                        if (dQty > 0)
                        {
                            dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = dQty.ToString();
                        }
                        else if (dQty < 0)
                        {
                            dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = (-dQty).ToString();
                        }

                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        barcodeFocus();
                    }
                    else if (strFocusedControl == "PriceChange")
                    {
                        decimal dSalesRate = 0;
                        try { dSalesRate = decimal.Parse(txtQty.Text.ToString()); }
                        catch { dSalesRate = 0; }


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dSalesRate, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        barcodeFocus();
                    }
                    else if (strFocusedControl == "LineDisc")
                    {
                        decimal dLineDisc = 0;
                        try { dLineDisc = decimal.Parse(txtQty.Text.ToString()); }
                        catch { dLineDisc = 0; }


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dLineDisc, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        barcodeFocus();
                    }
                }
            }
        }

        #region gridEvents

        private void dgvProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvProduct.Columns[e.RowIndex].Name == "Qty")
            if (e.ColumnIndex == 4) //Qty
            {
                CalculateGridTotal(e.RowIndex);
                Qty.ReadOnly = true;

            }
            if (e.ColumnIndex == 9) //SalesRate
            {
                decimal purchaseRate = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells["PurchaseRate"].Value);
                decimal salesRate = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells["SalesRate"].Value);

                if (purchaseRate > salesRate)
                {
                    MessageBox.Show("Purchase Rate exceeds theSales Rate. Please check the price.", "Pricing Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                AssignExludeRate(e.RowIndex);
                CalculateGridTotal(e.RowIndex);
                SalesRate.ReadOnly = true;
            }
              
        
           
            if (e.ColumnIndex == 13) //DiscAmt
            {
                CalculateGridTotal(e.RowIndex);
                DiscAmt.ReadOnly = true;
            }
            barcodeFocus();
        }
        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBoxControl = e.Control as DataGridViewTextBoxEditingControl;

            if (TextBoxControl != null)
            {
                TextBoxControl.KeyPress += TextBoxCellEditControlKeyPress;
            }
        }
        #endregion
        #region ButtonClick
        private void btnQty_Click(object sender, EventArgs e)
        {
            if (dgvProduct.RowCount > 1)
            {
                dgvProduct.Focus();
                if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                {
                    if (POSSettingsInfo._QtyChangeAuth == true)
                    {
                        IsAuthenticationApproved = false;
                        string condition = "_QtyChangeAuth";
                        frmUserAuthentication frm = new frmUserAuthentication();
                        frm.CallFromPOSSales(this, condition);
                        if (IsAuthenticationApproved)
                        {
                            Qty.ReadOnly = false;
                            strFocusedControl = "Qty";
                            blGridFocus = true;
                            blTextBoxFocus = false;
                            strGridValue = "";
                            txtTouchTextBox = txtQty;
                            txtTouchTextBox.Focus();
                            txtTouchTextBox.Clear();
                            dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["Qty"];
                            CurColIndex = dgvProduct.CurrentCell.ColumnIndex;
                            CurEditRowIndex = dgvProduct.CurrentCell.RowIndex;
                        }
                        else
                        {
                            barcodeFocus();
                        }
                    }
                    else
                    {
                        Qty.ReadOnly = false;
                        strFocusedControl = "Qty";
                        blGridFocus = true;
                        blTextBoxFocus = false;
                        strGridValue = "";
                        txtTouchTextBox = txtQty;
                        txtTouchTextBox.Focus();
                        txtTouchTextBox.Clear();
                        dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["Qty"];
                        CurColIndex = dgvProduct.CurrentCell.ColumnIndex;
                        CurEditRowIndex = dgvProduct.CurrentCell.RowIndex;
                    }
                }
                else
                {
                    barcodeFocus();
                }
            }
            else
            {
                barcodeFocus();
            }

        }

        private void btnPriceChange_Click(object sender, EventArgs e)
        {
            if (dgvProduct.RowCount > 1)
            {
                if (POSSettingsInfo._PriceChangeAuth == true)
                {
                    IsAuthenticationApproved = false;
                    string condition = "_PriceChangeAuth";
                    frmUserAuthentication frm = new frmUserAuthentication();
                    frm.CallFromPOSSales(this, condition);
                    if (IsAuthenticationApproved)
                    {
                        dgvProduct.Focus();
                        if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                        {
                            SalesRate.ReadOnly = false;
                            strFocusedControl = "PriceChange";
                            blGridFocus = true;
                            blTextBoxFocus = false;
                            strGridValue = "";
                            txtTouchTextBox = txtQty;
                            txtTouchTextBox.Focus();
                            txtTouchTextBox.Clear();
                            dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["SalesRate"];
                            CurColIndex = dgvProduct.CurrentCell.ColumnIndex;
                            CurEditRowIndex = dgvProduct.CurrentCell.RowIndex;
                        }
                        else
                        {
                            barcodeFocus();
                        }
                    }
                    else
                    {
                        barcodeFocus();
                    }

                }
                else
                {
                    dgvProduct.Focus();
                    if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                    {
                        SalesRate.ReadOnly = false;
                        strFocusedControl = "PriceChange";
                        blGridFocus = true;
                        blTextBoxFocus = false;
                        strGridValue = "";
                        txtTouchTextBox = txtQty;
                        txtTouchTextBox.Focus();
                        txtTouchTextBox.Clear();
                        dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["SalesRate"];
                        CurColIndex = dgvProduct.CurrentCell.ColumnIndex;
                        CurEditRowIndex = dgvProduct.CurrentCell.RowIndex;
                    }
                    else
                    {
                        barcodeFocus();
                    }
                }

            }
            else
            {
                barcodeFocus();
            }

        }

        private void btnLineDisc_Click(object sender, EventArgs e)
        {
            if (dgvProduct.RowCount > 1)
            {
                dgvProduct.Focus();
                DiscAmt.ReadOnly = false;
                if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                {
                    strFocusedControl = "LineDisc";
                    blGridFocus = true;
                    blTextBoxFocus = false;
                    strGridValue = "";
                    txtTouchTextBox = txtQty;
                    txtTouchTextBox.Focus();
                    txtTouchTextBox.Clear();
                    dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["DiscAmt"];
                    CurColIndex = dgvProduct.CurrentCell.ColumnIndex;
                    CurEditRowIndex = dgvProduct.CurrentCell.RowIndex;
                }
                else
                {
                    barcodeFocus();
                }
            }
            else
            {
                barcodeFocus();
            }
        }

        private void btnBillDiscPer_Click(object sender, EventArgs e)
        {
            if (POSSettingsInfo._DiscountAuth == true)
            {
                IsAuthenticationApproved = false;
                string condition = "_DiscountAuthPer";
                frmUserAuthentication frm = new frmUserAuthentication();
                frm.CallFromPOSSales(this, condition);
                if (IsAuthenticationApproved)
                {
                    txtDiscPer.ReadOnly = false;
                    txtDiscAmt.ReadOnly = false;
                    txtDiscPer.Text = "";

                    strFocusedControl = "txtDiscPer";
                    blGridFocus = false;
                    blTextBoxFocus = true;
                    txtTouchTextBox = txtDiscPer;

                    txtDiscPer.Focus();
                    txtDiscPer.SelectAll();
                }
                else
                {
                    barcodeFocus();
                }
            }
            else
            {
                txtDiscPer.ReadOnly = false;
                txtDiscAmt.ReadOnly = false;
                txtDiscPer.Text = "";

                strFocusedControl = "txtDiscPer";
                blGridFocus = false;
                blTextBoxFocus = true;
                txtTouchTextBox = txtDiscPer;

                txtDiscPer.Focus();
                txtDiscPer.SelectAll();
            }


        }

        private void btnBillDiscAmt_Click(object sender, EventArgs e)
        {
            if (POSSettingsInfo._DiscountAuth == true)
            {
                IsAuthenticationApproved = false;
                string condition = "_DiscountAuthAmt";
                frmUserAuthentication frm = new frmUserAuthentication();
                frm.CallFromPOSSales(this, condition);
                if (IsAuthenticationApproved)
                {
                    txtDiscPer.ReadOnly = false;
                    txtDiscAmt.ReadOnly = false;
                    txtDiscAmt.Text = "";
                    strFocusedControl = "txtDiscAmt";

                    blGridFocus = false;
                    blTextBoxFocus = true;
                    txtTouchTextBox = txtDiscAmt;


                    txtDiscAmt.Focus();
                    txtDiscAmt.SelectAll();
                }
                else
                {
                    barcodeFocus();
                }
            }
            else
            {
                txtDiscPer.ReadOnly = false;
                txtDiscAmt.ReadOnly = false;
                txtDiscAmt.Text = "";
                strFocusedControl = "txtDiscAmt";

                blGridFocus = false;
                blTextBoxFocus = true;
                txtTouchTextBox = txtDiscAmt;

                txtDiscAmt.Focus();
                txtDiscAmt.SelectAll();
            }
        }
        private void UpdateSerialNumbers()
        {
            for (int i = 0; i < dgvProduct.Rows.Count-1; i++)
            {
                dgvProduct.Rows[i].Cells["SlNo"].Value = i + 1;
                dgvSlno = i + 2;
            }
        }
        private void DeleteRow(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dgvProduct.Rows.Count)
            {
                dgvProduct.Rows.RemoveAt(rowIndex);
                UpdateSerialNumbers();
            }
        }


        private void btnOne_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnOne.Text;
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnTwo.Text;
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnThree.Text;
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnFour.Text;
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnFive.Text;
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnSix.Text;
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnSeven.Text;
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnEight.Text;
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnNine.Text;
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnZero.Text;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Text = txtTouchTextBox.Text + btnDot.Text;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (blTextBoxFocus == true && blGridFocus == false)
            {
                if (txtTouchTextBox.Name == "txtDiscPer")
                {
                    if (txtDiscPer.Text.Trim() == "")
                    {
                        txtDiscPer.Text = "0.00";
                    }

                    txtDiscPer_Validated(null, null);
                    txtDiscAmt.ReadOnly = true;
                    txtDiscPer.ReadOnly = true;
                    barcodeFocus();
                }
                else if (txtTouchTextBox.Name == "txtDiscAmt")
                {
                    if (txtDiscAmt.Text.Trim() == "")
                    {
                        txtDiscAmt.Text = "0.00";
                    }

                    txtDiscAmt_Validated(null, null);
                    txtDiscAmt.ReadOnly = true;
                    txtDiscPer.ReadOnly = true;
                    barcodeFocus();
                }
                else if (txtTouchTextBox.Name == "txtBarcode")
                {
                    barcodeScanning();
                }
            }
            else if (blTextBoxFocus == false && blGridFocus == true)
            {
                if (txtTouchTextBox.Name == "txtQty")
                {
                    if (strFocusedControl == "Qty")
                    {
                        decimal dQty = 0;
                        try { dQty = decimal.Parse(txtQty.Text.ToString()); }
                        catch { dQty = 0; }


                        if (dQty > 0)
                        {
                            dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = dQty.ToString();
                        }
                        else if (dQty < 0)
                        {
                            dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = (-dQty).ToString();
                        }

                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        Qty.ReadOnly = true;
                        barcodeFocus();
                    }
                    else if (strFocusedControl == "PriceChange")
                    {
                        decimal dSalesRate = 0;
                        try { dSalesRate = decimal.Parse(txtQty.Text.ToString()); }
                        catch { dSalesRate = 0; }


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dSalesRate, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        SalesRate.ReadOnly = true;
                        barcodeFocus();
                    }
                    else if (strFocusedControl == "LineDisc")
                    {
                        decimal dLineDisc = 0;
                        try { dLineDisc = decimal.Parse(txtQty.Text.ToString()); }
                        catch { dLineDisc = 0; }


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dLineDisc, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        DiscAmt.ReadOnly = true;
                        barcodeFocus();
                    }
                }
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (blTextBoxFocus == false && blGridFocus == true)
            {
                if (txtTouchTextBox.Name == "txtQty")
                {
                    TextboxClearButton(txtTouchTextBox);
                }
            }
            else if (blTextBoxFocus == true && blGridFocus == false)
            {
                if (strFocusedControl == "txtBarcode")
                {
                    TextboxClearButton(txtTouchTextBox);
                    txtBarcode.Focus();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lblBarcodeScanningType.Text == "Delete Product")
            {
                lblBarcodeScanningType.Text = "";
                lblBarcodeScanningType.Visible = false;
            }

            else
            {
                lblBarcodeScanningType.Text = "Delete Product";
                lblBarcodeScanningType.Visible = true;
            }
            barcodeFocus();
            //if (dgvProduct.RowCount > 1)
            //{
            //    if (!dgvProduct.CurrentRow.IsNewRow)
            //    {
            //        DataGridViewRow CurRow = dgvProduct.CurrentRow;
            //        dgvProduct.Rows.Remove(CurRow);

            //        if (dgvCurRow > 1)
            //        {
            //            dgvCurRow = dgvProduct.Rows.Count - 1;
            //        }
            //        else
            //        {
            //            dgvCurRow = 0;
            //            dgvSlno = 1;
            //        }


            //        CalculateBillDiscforIndivProduct();
            //        CalculateBillTotal();
            //    }

            //    barcodeFocus();
            //}
            //else
            //{
            //  barcodeFocus();
            //}
        }
        //string strButtonNameCondition = "";
        private void btnCash_Click(object sender, EventArgs e)
        {
            bool isOk = true;
            if (((Button)sender).Name == "btnCredit")
            {
                DataTable dtbl = new DataTable();
                dtbl = spProduct.POSAccountLedgerGetNameByCode(txtCustCode.Text);
                if (dtbl.Rows.Count > 0)
                {
                    lblLedgerId.Text = dtbl.Rows[0]["ledgerId"].ToString();
                    txtCustCode.Text = dtbl.Rows[0]["ledgerCode"].ToString();
                    txtCustomerName.Text = dtbl.Rows[0]["ledgerName"].ToString();
                }
                else
              {
                    MessageBox.Show("Customer Code not found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    barcodeFocus();
                    return;
                }

                if (POSSettingsInfo._CreditSalesAuth == true)
                {
                    IsAuthenticationApproved = false;
                    isOk = false;
                    string condition = "_CreditSalesAuth";
                    frmUserAuthentication frm = new frmUserAuthentication();
                    frm.CallFromPOSSales(this, condition);
                    if (IsAuthenticationApproved)
                    {
                        isOk = true;
                    }
                    else
                        isOk = false;

                }

            }

            if (isOk)
            {
                if (dgvProduct.Rows.Count > 0)
                {
                    decimal dcTotal = 0;

                    try { dcTotal = decimal.Parse(txtTotal.Text.Trim().ToString()); }
                    catch { }

                    bool isExchangebill = false;
                    for (int i = 0; i < dgvProduct.Rows.Count - 1; i++)
                    {
                        decimal dcQty = 0;
                        decimal dcSalesRate = 0;
                        try { dcQty = decimal.Parse(dgvProduct.Rows[i].Cells["Qty"].Value.ToString()); }
                        catch { }
                        try { dcSalesRate = decimal.Parse(dgvProduct.Rows[i].Cells["SalesRate"].Value.ToString()); }
                        catch { }

                        if (dcQty < 0)
                        {
                            isExchangebill = true;
                        }

                        if (dcSalesRate == 0 && dgvProduct.Rows[i].Cells["SalesRate"].Value.ToString() != "Barcode")
                        {
                            MessageBox.Show("Product with ZERO PRICE on Line number " + (i + 1) + " - " + dgvProduct.Rows[i].Cells["ItemName"].Value.ToString());
                            barcodeFocus();
                            return;
                        }

                    }

                    if (dcTotal <= 0 && isExchangebill == false)
                    {
                        MessageBox.Show("Cannot make bill as Amount ZERO", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        barcodeFocus();
                        return;
                    }


                    string strButtonName = "";
                    if (isSavefromButton == true)
                    {
                        if (((Button)sender).Name == "btnCash")
                        {
                            strButtonName = "Cash";
                        }
                        else if (((Button)sender).Name == "btnCreditCard")
                        {
                            strButtonName = "CreditCard";
                        }
                        else if (((Button)sender).Name == "btnUPI")
                        {
                            strButtonName = "UPI";
                        }
                        else if (((Button)sender).Name == "btnCredit")
                        {
                            strButtonName = "Credit";
                        }
                    }
                    else
                    {
                        strButtonName = "Cash";
                    }

                    if (counterInfo.DisplayStatus == true)
                    {
                        PoleDisplay("Total");
                    }

                    frmPOSPayment frmObjPOSPayment = new frmPOSPayment();
                    frmObjPOSPayment.DoWhenComingFromPOSSalesForm(this, txtTotal.Text.Trim(), strButtonName);
                    this.Enabled = false;
                }
            }
            else
            {
                barcodeFocus();
            }

        }

        #endregion

        private void dgvProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                if (dgvCurRow > 1)
                {
                    barcodeFocus();
                }
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvProduct.RowCount > 1)
                {
                    if (!dgvProduct.CurrentRow.IsNewRow)
                    {
                        SaveGridDeletedItem();
                        int selectedRowIndex = dgvProduct.CurrentCell.RowIndex; 
                        DeleteRow(selectedRowIndex);

                        if (dgvCurRow > 1)
                        {
                            dgvCurRow = dgvProduct.Rows.Count - 1;
                        }
                        else
                        {
                            dgvCurRow = 0;
                            dgvSlno = 1;
                        }

                        CalculateBillDiscforIndivProduct();
                        CalculateBillTotal();
                    }
                    barcodeFocus();
                }

                else
                {
                    barcodeFocus();
                }
            }
        }


        #endregion

        public void PrintLastBill(string fromButton, string BillNo)
        {
            if (dgvProduct.Rows.Count > 1)
            {
                MessageBox.Show("Sorry, Cannot Print Last Bill Now");
                return;
            }
            string strLastBillNo = "";
            if (fromButton == "lastbill")
            {
                strLastBillNo = POSSalesMasterSP.GetPOSLastBillNoforLastBillPrint(PublicVariables._counterId, Convert.ToDateTime(lblSessionDate.Text));
            }
            else if (fromButton == "reciept")
            {
                strLastBillNo = BillNo;
            }
            if (strLastBillNo != "")
            {
                //FillDatatatablesforPrint("", "", "", "", "", true, strLastBillNo,"");
                FillDatatatablesforDevPrint("", "", "", "", "", true, strLastBillNo, "", "");
            }

        }



        private void btnExchange_Click(object sender, EventArgs e)
        {
            if (lblBarcodeScanningType.Text == "Exchange Item")
            {
                lblBarcodeScanningType.Text = "";
                lblBarcodeScanningType.Visible = false;
            }
            else
            {
                if (POSSettingsInfo._ExchangeItemAuth == true)
                {
                    IsAuthenticationApproved = false;
                    string condition = "_ExchangeItemAuth";
                    frmUserAuthentication frm = new frmUserAuthentication();
                    frm.CallFromPOSSales(this, condition);
                    if (IsAuthenticationApproved)
                    {
                        lblBarcodeScanningType.Text = "Exchange Item";
                        lblBarcodeScanningType.Visible = true;
                    }
                    else
                    {
                        barcodeFocus();
                    }
                }
                else
                {
                    lblBarcodeScanningType.Text = "Exchange Item";
                    lblBarcodeScanningType.Visible = true;
                }

            }
            barcodeFocus();

        }

        private void frmPOSSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dgvProduct.Rows.Count > 1)
            {
                MessageBox.Show("Cannot Close POS!! bill not Clear!");
                e.Cancel = true;
                return;
            }
            if (counterInfo.DisplayStatus == true)
            {
                PoleDisplay("Close");
            }
        }

        private void btnClosePOS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPriceCheck_Click(object sender, EventArgs e)
        {
          
            frmPriceCheck _isOpen = Application.OpenForms["frmPriceCheck"] as frmPriceCheck;
            if (_isOpen == null)
            {
                frmPriceCheck frmObj = new frmPriceCheck();

                frmObj.strbarcode = strBarcode;
                frmObj.WindowState = FormWindowState.Normal;
                //frmObj.MdiParent = MDIFinacPOS.MDIObj;
                frmObj.ShowDialog();
               
            }
            else
            {
                //_isOpen.MdiParent = MDIFinacPOS.MDIObj;
                if (_isOpen.WindowState == FormWindowState.Minimized)
                {
                    _isOpen.WindowState = FormWindowState.Normal;
                }
                if (_isOpen.Enabled)
                {
                    _isOpen.Activate();
                    _isOpen.BringToFront();
                }

            }
        }

        private void btnLastBill_Click(object sender, EventArgs e)
        {
            if (POSSettingsInfo._lastBillPrintAuth == true)
            {
                IsAuthenticationApproved = false;
                string condition = "_lastBillPrintAuth";
                frmUserAuthentication frm = new frmUserAuthentication();
                frm.CallFromPOSSales(this, condition);

                if (IsAuthenticationApproved)
                {
                    PrintLastBill("lastbill", "");
                }
                else
                {
                    barcodeFocus();
                }
            }
            else
            {
                PrintLastBill("lastbill", "");
            }
        }

        private void timerSessionDate_Tick(object sender, EventArgs e)
        {
            if (DateTime.Compare(Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(strSessionDate)) > 0)
            {
                if (lblSessionDate.ForeColor == Color.Red)
                {
                    lblSessionDate.ForeColor = Color.White;
                }
                else
                {
                    lblSessionDate.ForeColor = Color.Red;
                }

            }
        }

        private void btnFindProduct_Click(object sender, EventArgs e)
        {

            if (counterInfo.ProductSearchWithImage == true) // added on 10-04-2025 by Nishana
            {

                if (dtblProductWithImage.Rows.Count > 0)
                {
                    frmProductImageListing frmObj = new frmProductImageListing();
                    frmObj.CallFromSalesInvoice(this, dtblProductWithImage);
                }
            }
            else
            {

                frmProductPopUpGrid frm = new frmProductPopUpGrid();
                frm.CallFromPOSSales(this, "product");
                txtBarcode.Focus();
            }

        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            if (dgvProduct.Rows.Count > 1)
            {
                if (MessageBox.Show("Do you want to HOLD this bill?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (POSSettingsInfo._HoldBillAuth == true)
                    {
                        IsAuthenticationApproved = false;
                        string condition = "_HoldBillAuth";
                        frmUserAuthentication frm = new frmUserAuthentication();
                        frm.CallFromPOSSales(this, condition);
                        if (IsAuthenticationApproved)
                        {
                            string strHoldBillNo = "";
                            strHoldBillNo = HoldBillSaveFunction();
                            if (strHoldBillNo != "")
                            {
                                DataTable dtblTaxSummery = new DataTable();
                                dtblTaxSummery = GetTaxSum();
                                dtblTaxDetailsThermal = dtblTaxSummery;

                                //FillDatatatablesforPrint("", "", "", "", "", false, "", strHoldBillNo);
                                FillDatatatablesforDevPrint("", "", "", "", "", false, "", strHoldBillNo, "");
                            }

                            ClearFunction();
                        }
                        else
                        {
                            barcodeFocus();
                        }
                    }
                    else
                    {
                        string strHoldBillNo = "";
                        strHoldBillNo = HoldBillSaveFunction();
                        if (strHoldBillNo != "")
                        {
                            DataTable dtblTaxSummery = new DataTable();
                            dtblTaxSummery = GetTaxSum();
                            dtblTaxDetailsThermal = dtblTaxSummery;

                            //FillDatatatablesforPrint("", "", "", "", "", false, "", strHoldBillNo);
                            FillDatatatablesforDevPrint("", "", "", "", "", false, "", strHoldBillNo, "");
                        }

                        ClearFunction();
                    }

                }
                else
                {
                    barcodeFocus();
                }
            }
            else
            {
                barcodeFocus();
            }

        }

        private void btnUnhold_Click(object sender, EventArgs e)
        {
            if (lblBarcodeScanningType.Text == "Scan Hold Bill No")
            {
                lblBarcodeScanningType.Text = "";
                lblBarcodeScanningType.Visible = false;
            }
            else
            {
                if (dgvProduct.Rows.Count > 1)
                {
                    MessageBox.Show("Please Clear Current Bill!");
                }
                else
                {
                    lblBarcodeScanningType.Text = "Scan Hold Bill No";
                    lblBarcodeScanningType.Visible = true;

                }

            }
            barcodeFocus();
        }
        public void FillrowAfterPickingReciept(string BillNo)
        {
            lblLedgerId.Text = BillNo;
            PrintLastBill("reciept", BillNo);
        }

        public void FillrowAfterPickingCustomer(string ledgerId)
        {
            lblLedgerId.Text = ledgerId;
        }

        public void FillrowAfterPickingProduct(string strBarcode)
        {

            txtBarcode.Text = strBarcode;
            if (strBarcode != "")
            {
                barcodeScanning();
            }
            else
            {
                barcodeFocus();
            }
            txtBarcode.Focus();
        }

        public void AuthenticateUser(bool IstrueUser, bool isClose, string condition)
        {
            IsAuthenticationApproved = IstrueUser;
            if (!isClose)
            {
                if (IsAuthenticationApproved == false)
                {
                    MessageBox.Show("You are not an authenticated user!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                barcodeFocus();
            }
        }

        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            if (btnFindCustomer.Text == "Find Customer")// added on 29-04-2025  by nishana
            {
                frmProductPopUpGrid frm = new frmProductPopUpGrid();
                frm.CallFromPOSSales(this, "customer");
                if (lblLedgerId.Text.ToString() != "")
                {
                    DataTable dtbl = new DataTable();
                    dtbl = spProduct.GetCustomerDetailsbyLedgerId(lblLedgerId.Text.ToString());
                    if (dtbl.Rows.Count > 0)
                    {
                        txtCustCode.Text = dtbl.Rows[0]["ledgerCode"].ToString();
                        txtCustomerName.Text = dtbl.Rows[0]["ledgerName"].ToString();
                        txtphone.Text = dtbl.Rows[0]["phoneNo"].ToString();
                        txtAdress.Text = dtbl.Rows[0]["address"].ToString();
                        txtVatNo.Text = dtbl.Rows[0]["tinNumber"].ToString();

                        btnFindCustomer.Text = "Clear Customer";

                        btnCash.Enabled = false;
                        btnCreditCard.Enabled = false;
                        btnUPI.Enabled = false;
                    }
                }
            }
            else
            {
                lblLedgerId.Text = "";
                txtCustomerName.Text = "";
                txtCustCode.Text = "";
                txtAdress.Text = "";
                txtphone.Text = "";
                txtVatNo.Text = "";

                btnCash.Enabled = true;
                btnCreditCard.Enabled = true;
                btnUPI.Enabled = true;

                btnFindCustomer.Text = "Find Customer";
            }
                barcodeFocus();
            
        }
        private void txtCustomerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                DataTable dtbl = new DataTable();
                dtbl = spProduct.POSAccountLedgerGetNameByCode(txtCustomerName.Text);
                if (dtbl.Rows.Count > 0)
                {
                    lblLedgerId.Text = dtbl.Rows[0]["ledgerId"].ToString();
                    txtCustomerName.Text = dtbl.Rows[0]["ledgerCode"].ToString();
                    txtCustCode.Text = dtbl.Rows[0]["ledgerName"].ToString();

                    btnCash.Enabled = false;
                    btnCreditCard.Enabled = false;
                    btnUPI.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Customer Code not found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblLedgerId.Text = "";
                    txtCustomerName.Text = "";
                    txtCustCode.Text = "";

                    btnCash.Enabled = true;
                    btnCreditCard.Enabled = true;
                    btnUPI.Enabled = true;

                }
                barcodeFocus();
            }
        }

        private void txtDiscAmt_Leave(object sender, EventArgs e)
        {
            if (txtDiscAmt.Text.ToString() == "")
            {
                txtDiscAmt.Text = "0";
                txtDiscPer.Text = "0";
            }
        }

        private void txtDiscPer_Leave(object sender, EventArgs e)
        {
            if (txtDiscPer.Text.ToString() == "")
            {
                txtDiscAmt.Text = "0";
                txtDiscPer.Text = "0";
            }
        }

        private void btnReceiptCopy_Click(object sender, EventArgs e)
        {
            frmProductPopUpGrid frm = new frmProductPopUpGrid();
            frm.CallFromPOSSales(this, "reciept");
            txtBarcode.Focus();
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTenderTotalAmount_Click(object sender, EventArgs e)
        {

        }

        private void lblTenderTotal_Click(object sender, EventArgs e)
        {

        }

        private void lblTenderBalanceAmount_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void txtDiscAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblSessionDt_Click(object sender, EventArgs e)
        {

        }

        private void lblBillDate_Click(object sender, EventArgs e)
        {

        }

        private void panelBillDetails_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void lblHoldMasterId_Click(object sender, EventArgs e)
        {

        }

        private void panelMainButton_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTenderBalance_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void txtDiscPer_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblTotalQty_Click(object sender, EventArgs e)
        {

        }

        private void lblCustomerCode_Click(object sender, EventArgs e)
        {

        }

        private void txtTaxAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblBillNo_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblSessionNO_Click(object sender, EventArgs e)
        {

        }

        private void lblSessionDate_Click(object sender, EventArgs e)
        {

        }

        private void lblUser_Click(object sender, EventArgs e)
        {

        }

        private void lblCustomerName_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblBarcodeScanningType_Click(object sender, EventArgs e)
        {

        }

        private void lblBillTime_Click(object sender, EventArgs e)
        {

        }

        private void panelBarcode_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTaxable_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblCounter_Click(object sender, EventArgs e)
        {

        }

        private void btnCashBox_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCustomerId_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void lblLedgerId_Click(object sender, EventArgs e)
        {

        }

     

       private void dgvProduct_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
       {

           if (e.RowIndex >= 0 && e.ColumnIndex == dgvProduct.Columns["Qty"].Index)
           {
                decimal purchaseRate = Convert.ToDecimal(dgvProduct.Rows[e.RowIndex].Cells["PurchaseRate"].Value);
            
                dgvProduct.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "PR: " + purchaseRate.ToString();

            }
        }

       private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
       {
          
           if (e.RowIndex >= 0) 
           {
                try
                {
                    strBarcode = dgvProduct.Rows[e.RowIndex].Cells["Barcode"].Value.ToString();
                }
                catch { }
           }

       }

      
    }
}
