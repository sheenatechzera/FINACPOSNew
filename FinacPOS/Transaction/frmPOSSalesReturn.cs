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
using OnBarcode.Barcode.WinForms; 

namespace FinacPOS
{
    public partial class frmPOSSalesReturn : Form
    {
        public frmPOSSalesReturn()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);
        }
        #region PUBLICVARIABLES
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
        public string strSessionNo = "";
        public string strSessionDate = "";
        int dgvCurRow = 0;
        int dgvSlno = 0;
        string strFocusedControl = "";
        bool blTextBoxFocus;
        bool blGridFocus;
        TextBox txtTouchTextBox = new TextBox();
        string strGridValue = "";
        int CurColIndex = 0;
        int CurEditRowIndex = 0;

        //varis form thermal print
        DataTable dtblCompanyDetailsThermal;
        DataTable dtblGridDetailsThermal;
        DataTable dtblOtherDetailsThermal;
        DataTable dtblTaxDetailsThermal;
        int PrintPageHight;
        //--------------------------

        POSCounterInfo counterInfo = new POSCounterInfo();
        GeneralSP SPGeneral = new GeneralSP();
        POSSalesReturnMasterSP POSSalesReturnMasterSP = new POSSalesReturnMasterSP();
        POSSalesReturnDetails1SP POSSalesReturnDetails1SP = new POSSalesReturnDetails1SP();
        ComboValidation objComboValidation = new ComboValidation();
        ProductSP spProduct = new ProductSP();

        bool isRateChanged = false;

        DataGridViewTextBoxEditingControl TextBoxControl;

        //for Posting Account setting
        string strCashSalesLedgerId = "";
        string strCCSalesLedgerId = "";
        string strUPISalesLedgerId = "";
        string strSalesReturnLedgerId = "";

        #endregion
      

        private void frmPOSSalesReturn_Load(object sender, EventArgs e)
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
        public string POSBillNumberMax()
        {
            string PartBillNo = "";
            int billNumber = 1;
            int lastBillNo = 0;

            try
            {
                DataTable dtbl = SPGeneral.GetPOSLastBillNo(PublicVariables._counterId, "Sales");

                if (dtbl.Rows.Count > 0 && !dtbl.Rows[0].IsNull("LastBillNo"))
                {
                    // Try parsing the last bill number to int
                    if (int.TryParse(dtbl.Rows[0]["LastBillNo"].ToString(), out lastBillNo))
                    {
                        billNumber = lastBillNo + 1;
                    }
                }

                if (!counterInfo.ShowPrefixInBillNo)
                {
                    PartBillNo = billNumber.ToString();
                }
                else
                {

                    PartBillNo = PublicVariables._counterId + DateTime.Now.ToString("yy") + lastBillNo.ToString().PadLeft(7, '0');
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PartBillNo = "1"; // fallback
            }

            return PartBillNo;
        }

        public void FormLoadFunction()
        {
            timer1.Start();
            lblSessionNO.Text = strSessionNo;
            lblSessionDate.Text = strSessionDate;
            lblCounter.Text = PublicVariables._counterName;
            lblUser.Text = PublicVariables._EmpName;

           // POSSalesPostingAccount(); //blocked on 29/Mar/2025
            POSCounterSP counterSP = new POSCounterSP();
            counterInfo = counterSP.POSCounterViewbyCounterId(PublicVariables._counterId);
   
            //29/Mar/2025
            strCashSalesLedgerId = counterInfo.CashAccountLedgerId;
            strCCSalesLedgerId = counterInfo.BankAccountLedgerId;
            strUPISalesLedgerId = counterInfo.UPIAccountLedgerId;
            strSalesReturnLedgerId = counterInfo.SalesAccountLedgerId;
        }
        public void barcodeFocus()
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
        public void ClearFunction()
        {
            lblBillNo.Text = POSBillNumberMax();

            lblTotalQty.Text = "0";
            txtSubTotal.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            
            txtTaxable.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtTaxAmt.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtTotal.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

            txtCustName.Text = "";
            txtCustomerId.Text = "";
            lblLedgerId.Text = "";

            dgvProduct.Rows.Clear();
            dgvProduct.RowCount = 1;
            dgvCurRow = 0;
            dgvSlno = 1;

            rbtCash.Checked = true;
            
            barcodeFocus();
        }
        //public void POSSalesPostingAccount()
        //{
        //    DataTable dtbl = new DataTable();
        //    dtbl = SPGeneral.GetPostingAccount("POS");
        //    if (dtbl.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dtbl.Rows.Count; i++)
        //        {
        //            if (dtbl.Rows[i]["PostingAccount"].ToString() == "Sales Return")
        //            {
        //                strSalesReturnLedgerId = dtbl.Rows[i]["ledgerId"].ToString();
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

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                barcodeScanning();
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
           // bool IsDiscounted = false;
            decimal amountBeforeDisc = 0;
            decimal rateDiscAmount = 0;
            string offerId = "";
            DataTable dtblSalesRate = new DataTable();


            //Call Full Bill Details for return
            if (lblBarcodeScanningType.Visible == true && lblBarcodeScanningType.Text == "<- Scan Bill No Here")
            {
                LoadPOSBillDetailsforReturn();
                lblBarcodeScanningType.Text = "";
                lblBarcodeScanningType.Visible = false;
                return;
            }

            DataTable dtbl = new DataTable();
            dtbl = SPGeneral.GetProductDetailsByBarcode(txtBarcode.Text.Trim());
            if (dtbl.Rows.Count > 0) //load details by Barcode
            {
                strBarcode = txtBarcode.Text.Trim();
                strItemCode = dtbl.Rows[0]["productCode"].ToString();
                strItemName = dtbl.Rows[0]["productName"].ToString();
                strUnitId = dtbl.Rows[0]["unitId"].ToString();
                strUnitName = dtbl.Rows[0]["unitName"].ToString();
                strBaseUnitId = dtbl.Rows[0]["BaseUnitId"].ToString();
                decUnitConversion = Convert.ToDecimal(dtbl.Rows[0]["conversionRate"].ToString());

                dtblSalesRate = SPGeneral.ProductSalesRateForSalePOS(strItemCode, "1", DateTime.Parse(lblBillDate.Text), strUnitId);
                decSalesPrice = Convert.ToDecimal(dtblSalesRate.Rows[0]["rate"].ToString());
              //  IsDiscounted = Convert.ToBoolean(dtblSalesRate.Rows[0]["IsDiscounted"].ToString());
                amountBeforeDisc = Convert.ToDecimal(dtblSalesRate.Rows[0]["amountBeforeDisc"].ToString());
                rateDiscAmount = Convert.ToDecimal(dtblSalesRate.Rows[0]["rateDiscAmount"].ToString());
                offerId = dtblSalesRate.Rows[0]["offerId"].ToString();
                if (POSSettingsInfo._BlockZeroPriceInSales)
                {
                    if (decSalesPrice == 0)
                    {
                        Console.Beep(500, 500);
                        MessageBox.Show("Sales Price is ZERO", "WARNING");
                        barcodeFocus();
                        return;
                    }
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
                    strUnitId = dtbl.Rows[0]["unitId"].ToString();
                    strUnitName = dtbl.Rows[0]["unitName"].ToString();
                    strBaseUnitId = dtbl.Rows[0]["unitId"].ToString();
                    decUnitConversion = Convert.ToDecimal(dtbl.Rows[0]["conversionRate"].ToString());

                    dtblSalesRate = SPGeneral.ProductSalesRateForSalePOS(strItemCode, "1", DateTime.Parse(lblBillDate.Text), strUnitId);
                    decSalesPrice = Convert.ToDecimal(dtblSalesRate.Rows[0]["rate"].ToString());
                  //  IsDiscounted = Convert.ToBoolean(dtblSalesRate.Rows[0]["IsDiscounted"].ToString());
                    amountBeforeDisc = Convert.ToDecimal(dtblSalesRate.Rows[0]["amountBeforeDisc"].ToString());
                    rateDiscAmount = Convert.ToDecimal(dtblSalesRate.Rows[0]["rateDiscAmount"].ToString());
                    offerId = dtblSalesRate.Rows[0]["offerId"].ToString();
                    if (POSSettingsInfo._BlockZeroPriceInSales)
                    {
                        if (decSalesPrice == 0)
                        {
                            Console.Beep(500, 500);
                            MessageBox.Show("Sales Price is ZERO", "WARNING");
                            barcodeFocus();
                            return;
                        }
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

            if (strItemCode != "")
            {
                dgvProduct.RowCount = dgvCurRow + 2;
                dgvProduct.Rows[dgvCurRow].Cells["SLNo"].Value = dgvSlno;
                dgvProduct.Rows[dgvCurRow].Cells["ProductCode"].Value = strItemCode;
                dgvProduct.Rows[dgvCurRow].Cells["Barcode"].Value = strBarcode;
                dgvProduct.Rows[dgvCurRow].Cells["ItemName"].Value = strItemName;
                if (strScaleItemType == "Inventory")
                {
                    dgvProduct.Rows[dgvCurRow].Cells["Qty"].Value = strScaleQtyPart;
                }
                else
                {
                    dgvProduct.Rows[dgvCurRow].Cells["Qty"].Value = 1;
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
                    dgvProduct.Rows[dgvCurRow].Cells["PurchaseRate"].Value = (dRt * decUnitConversion).ToString(FinanceSettingsInfo._roundDecimalPart);
                }
                else
                {
                    dRt = 0;
                    dgvProduct.Rows[dgvCurRow].Cells["PurchaseRate"].Value = "0";
                }
                //
                dgvProduct.Rows[dgvCurRow].Cells["SalesRate"].Value = Math.Round(decSalesPrice, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                dgvProduct.Rows[dgvCurRow].Cells["amountBeforeDisc"].Value = Math.Round(amountBeforeDisc, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                dgvProduct.Rows[dgvCurRow].Cells["rateDiscAmount"].Value = Math.Round(rateDiscAmount, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
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
                decimal dGrossValue = 0;
                decimal dTaxPerc = 0;
                decimal dTaxAmt = 0;
                try { dQty = decimal.Parse(dgvProduct.Rows[dgvCurRow].Cells["Qty"].Value.ToString()); }
                catch { }

                try { dRate = decimal.Parse(dgvProduct.Rows[dgvCurRow].Cells["ExcludeRate"].Value.ToString()); }
                catch { }

                dGrossValue = dQty * dRate;
                dgvProduct.Rows[dgvCurRow].Cells["GrossValue"].Value = Math.Round(dGrossValue, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                dgvProduct.Rows[dgvCurRow].Cells["DiscAmt"].Value = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

                dgvProduct.Rows[dgvCurRow].Cells["NetValue"].Value = Math.Round(dGrossValue, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

                try { dTaxPerc = decimal.Parse(dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value.ToString()); }
                catch { }

                if (dTaxPerc != 0)
                {
                    dTaxAmt = Math.Round(((dGrossValue * dTaxPerc) / (100)), FinanceSettingsInfo._roundDecimal);

                    dgvProduct.Rows[dgvCurRow].Cells["TaxAmt"].Value = dTaxAmt.ToString(FinanceSettingsInfo._roundDecimalPart);
                }
                else
                {
                    dgvProduct.Rows[dgvCurRow].Cells["TaxAmt"].Value = "0.00";
                }

                dgvProduct.Rows[dgvCurRow].Cells["Total"].Value = (dGrossValue + dTaxAmt).ToString(FinanceSettingsInfo._roundDecimalPart);

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
                amountBeforeDisc = 0;
                rateDiscAmount = 0;
                offerId = "0";

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

                    dgvProduct.Rows[inIndex].Cells["SalesRate"].Value = Math.Round(dcRate, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);


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
                                dcTaxExcludedRate = Math.Round(dcTaxExcludedRate, FinanceSettingsInfo._roundDecimal);
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
        public void LoadPOSBillDetailsforReturn()
        {
            DataTable dtblSales = new DataTable();
            dtblSales = POSSalesReturnMasterSP.GetPOSBillProductsforReturn(txtBarcode.Text.Trim());
            if (dtblSales.Rows.Count > 0)
            {
                //strHoldMasterIdToEdit = dtblSales.Rows[0]["POSHoldMasterId"].ToString();
                
                    dgvProduct.Rows.Clear();
                    foreach (DataRow drowDetails in dtblSales.Rows)
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
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["SalesRate"].Value = Convert.ToDecimal(drowDetails["rate"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["ExcludeRate"].Value = Convert.ToDecimal(drowDetails["excludeRate"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["PurchaseRate"].Value = Convert.ToDecimal(drowDetails["costPrice"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["GrossValue"].Value = Convert.ToDecimal(drowDetails["grossValue"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["DiscAmt"].Value = Convert.ToDecimal("0.00").ToString(FinanceSettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["NetValue"].Value = Convert.ToDecimal(drowDetails["netAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxId"].Value = drowDetails["taxId"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxPerc"].Value = drowDetails["taxPer"].ToString();
                    //dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxAmt"].Value = Convert.ToDecimal(drowDetails["taxAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["Total"].Value = Convert.ToDecimal(drowDetails["Amount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                    dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["BillDiscIndProductAmt"].Value = "0.00";
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxId"].Value = drowDetails["taxId"].ToString();
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["amountBeforeDisc"].Value = Convert.ToDecimal(drowDetails["amountBeforeDisc"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["rateDiscAmount"].Value = Convert.ToDecimal(drowDetails["rateDiscAmount"]).ToString(FinanceSettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["offerId"].Value = drowDetails["offerId"].ToString();


                        decimal dQty = 0;
                        decimal dRate = 0;
                        decimal dGrossValue = 0;
                        decimal dTaxPerc = 0;
                        decimal dTaxAmt = 0;
                        try { dQty = decimal.Parse(dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["Qty"].Value.ToString()); }
                        catch { }

                        try { dRate = decimal.Parse(dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["ExcludeRate"].Value.ToString()); }
                        catch { }

                        dGrossValue = dQty * dRate;
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["GrossValue"].Value = Math.Round(dGrossValue, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["DiscAmt"].Value = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["NetValue"].Value = Math.Round(dGrossValue, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

                        try { dTaxPerc = decimal.Parse(dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxPerc"].Value.ToString()); }
                        catch { }

                        if (dTaxPerc != 0)
                        {
                            dTaxAmt = Math.Round(((dGrossValue * dTaxPerc) / (100)), FinanceSettingsInfo._roundDecimal);

                            dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxAmt"].Value = dTaxAmt.ToString(FinanceSettingsInfo._roundDecimalPart);
                        }
                        else
                        {
                            dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["TaxAmt"].Value = "0.00";
                        }

                        dgvProduct.Rows[dgvProduct.Rows.Count - 2].Cells["Total"].Value = (dGrossValue + dTaxAmt).ToString(FinanceSettingsInfo._roundDecimalPart);

                    }

                    CalculateBillTotal();


                    dgvSlno = dgvProduct.RowCount;
                    dgvCurRow = dgvProduct.RowCount - 1;
                    dgvProduct.CurrentCell = dgvProduct.Rows[dgvCurRow - 1].Cells["Barcode"];

                    barcodeFocus();
                
            }
            else
            {
                MessageBox.Show("Record not found in this Bill No", "WARNING");
                barcodeFocus();
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
                            try { dTaxPerc = decimal.Parse(dgvrow.Cells["TaxPerc"].Value.ToString()); }
                            catch { }
                            if (dTaxPerc != 0)
                            {
                                dcTaxable = dcTaxable + decimal.Parse(dgvrow.Cells["NetValue"].Value.ToString());
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

                lblTotalQty.Text = Math.Round(dcQtyTotal, FinanceSettingsInfo._roundDecimal).ToString();
                txtSubTotal.Text = Math.Round(dcSubTotal, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                txtTaxable.Text = Math.Round(dcTaxable, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                txtTaxAmt.Text = Math.Round(dcTaxAmt, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
                txtTotal.Text = Math.Round(dcTotal, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (blTextBoxFocus == false && blGridFocus == true)
            {
                if (txtTouchTextBox.Name == "txtQty")
                {
                    TextboxClearButton(txtTouchTextBox);
                }
            }
        }
        private void TextboxClearButton(TextBox txt)
        {
            if (txt.Text.Trim() != "")
            {
                txt.Text = txt.Text.Substring(0, txt.Text.Length - 1);
            }
        }

        private void btnQty_Click(object sender, EventArgs e)
        {
            if (dgvProduct.RowCount > 1)
            {
                dgvProduct.Focus();
                Qty.ReadOnly = false;
                if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                {
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
                barcodeFocus();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (blTextBoxFocus == false && blGridFocus == true)
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


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dSalesRate, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);


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


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dLineDisc, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);


                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        barcodeFocus();
                    }
                }
            }
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            barcodeFocus();
        }

        private void btnPriceChange_Click(object sender, EventArgs e)
        {
            if (dgvProduct.RowCount > 1)
            {
                dgvProduct.Focus();
                SalesRate.ReadOnly = false;
                if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                {
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProduct.RowCount > 1)
            {
                DataGridViewRow CurRow = dgvProduct.CurrentRow;
                dgvProduct.Rows.Remove(CurRow);

                //CalculateBillDiscforIndivProduct();
                CalculateBillTotal();
                barcodeFocus();
            }
        }

        private void dgvProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvProduct.Columns[e.RowIndex].Name == "Qty")
            if (e.ColumnIndex == 4) //Qty
            {
                CalculateGridTotal(e.RowIndex);

            }
            if (e.ColumnIndex == 8) //SalesRate
            {
                AssignExludeRate(e.RowIndex);
                CalculateGridTotal(e.RowIndex);

            }
            if (e.ColumnIndex == 11) //DiscAmt
            {
                CalculateGridTotal(e.RowIndex);

            }
            barcodeFocus();
        }

        private void CalculateGridTotal(int inIndex)
        {
            decimal dQty = 0;
            decimal dRate = 0;
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
            try { dDiscAmt = decimal.Parse(dgvProduct.Rows[inIndex].Cells["DiscAmt"].Value.ToString()); }
            catch { }
            try { dBillItemDisc = decimal.Parse(dgvProduct.Rows[inIndex].Cells["BillDiscIndProductAmt"].Value.ToString()); }
            catch { }
            try { dTaxPerc = decimal.Parse(dgvProduct.Rows[inIndex].Cells["TaxPerc"].Value.ToString()); }
            catch { }

            dgvProduct.Rows[inIndex].Cells["DiscAmt"].Value = Math.Round(dDiscAmt, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);


            dGrossValue = dQty * dRate;
            dgvProduct.Rows[inIndex].Cells["GrossValue"].Value = Math.Round(dGrossValue, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

            dNetValue = dGrossValue - dDiscAmt;
            dgvProduct.Rows[inIndex].Cells["NetValue"].Value = Math.Round(dNetValue, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

            if (dTaxPerc != 0)
            {
                dTaxAmt = Math.Round((((dNetValue - dBillItemDisc) * dTaxPerc) / (100)), FinanceSettingsInfo._roundDecimal);

                dgvProduct.Rows[inIndex].Cells["TaxAmt"].Value = dTaxAmt.ToString(FinanceSettingsInfo._roundDecimalPart);
            }
            else
            {
                dTaxAmt = 0;
                dgvProduct.Rows[inIndex].Cells["TaxAmt"].Value = "0.00";
            }

            dgvProduct.Rows[inIndex].Cells["Total"].Value = ((dNetValue - dBillItemDisc) + dTaxAmt).ToString(FinanceSettingsInfo._roundDecimalPart);

            CalculateBillTotal();
        }

        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBoxControl = e.Control as DataGridViewTextBoxEditingControl;

            if (TextBoxControl != null)
            {
                TextBoxControl.KeyPress += TextBoxCellEditControlKeyPress;
            }
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
                DataGridViewRow CurRow = dgvProduct.CurrentRow;
                dgvProduct.Rows.Remove(CurRow);

                //CalculateBillDiscforIndivProduct();
                CalculateBillTotal();
                barcodeFocus();
            }
        
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Control == false) //Focus on Qty
            {
                if (dgvProduct.RowCount > 1)
                {
                    dgvProduct.Focus();
                    Qty.ReadOnly = false;
                    if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                    {
                        dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["Qty"];
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
                    Qty.ReadOnly = false;
                    if (dgvProduct.CurrentRow.Cells["Barcode"].Value != null)
                    {
                        dgvProduct.CurrentCell = dgvProduct.CurrentRow.Cells["SalesRate"];
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
            else if (e.KeyCode == Keys.F10 && e.Control == false)//find products
            {
                btnFindProduct_Click(e, e); 
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


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dSalesRate, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);


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


                        dgvProduct.Rows[CurEditRowIndex].Cells[CurColIndex].Value = Math.Round(dLineDisc, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);


                        DataGridViewCellEventArgs x = new DataGridViewCellEventArgs(CurColIndex, CurEditRowIndex);
                        dgvProduct_CellEndEdit(null, x);


                        txtQty.Text = "";
                        barcodeFocus();
                    }
                }
            }
        }

        private void btnNewSale_Click(object sender, EventArgs e)
        {
            ClearFunction();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isOk = true;
            if (rbtCredit.Checked == true)
            {
                if (txtCustomerId.Text.ToString() != "")
                {
                    DataTable dtbl = new DataTable();
                    dtbl = spProduct.POSAccountLedgerGetNameByCode(txtCustomerId.Text);
                    if (dtbl.Rows.Count > 0)
                    {
                        lblLedgerId.Text = dtbl.Rows[0]["ledgerId"].ToString();
                        txtCustomerId.Text = dtbl.Rows[0]["ledgerCode"].ToString();
                        txtCustName.Text = dtbl.Rows[0]["ledgerName"].ToString();
                        isOk = true;
                    }
                    else
                    {
                        MessageBox.Show("Customer Code not found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isOk = false;
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Customer for Credit Return", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isOk = false;
                }
                

            }
            if (txtCustomerId.Text != "" && rbtCredit.Checked == false)
            {
                MessageBox.Show("Please Select Return method as Credit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isOk = false;
            }
            if(isOk)
            {
                if (dgvProduct.Rows.Count > 0)
                {
                    decimal dcTotal = 0;

                    try { dcTotal = decimal.Parse(txtTotal.Text.Trim().ToString()); }
                    catch { }

                    if (dcTotal != 0)
                    {
                        string strReturnMethod = "";
                        string strReturnStatus = "";
                        if (rbtCreditNote.Checked == true)
                        {
                            strReturnMethod = "CreditNote";
                            strReturnStatus = "P";
                        }
                        else if (rbtCash.Checked == true)
                        {
                            strReturnMethod = "Cash";
                            strReturnStatus = "C";
                        }
                        else if (rbtCredit.Checked == true)
                        {
                            strReturnMethod = "Credit";
                            strReturnStatus = "C";
                        }
                        else
                        {
                            MessageBox.Show("Please Select any Return Method", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            barcodeFocus();
                            return;
                        }

                        string strMasterId = SaveFunction(strReturnMethod, strReturnStatus).ToString();

                        if (strMasterId != "")
                        {
                            DataTable dtblTaxSummery = new DataTable();
                            dtblTaxSummery = GetTaxSum();
                            //MessageBox.Show("Saved");
                            bool isPrintSuccess = false;
                            try
                            {
                                dtblTaxDetailsThermal = dtblTaxSummery;
                                //FillDatatatablesforPrint();
                                FillDatatatablesforDevPrint();
                                isPrintSuccess = true;
                            }
                            catch (Exception ex)
                            {
                                isPrintSuccess = false;

                            }

                            if (isPrintSuccess == true)
                            {
                                if (rbtCreditNote.Checked == false)
                                {
                                    SaveLedgerPosting(strMasterId, dtblTaxSummery);
                                }

                                SavetoStockPosting(strMasterId);
                                if (txtCustomerId.Text != "")
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
                                        InfoPartyBalance.Credit = decimal.Parse(txtTotal.Text);
                                        InfoPartyBalance.Debit = 0;
                                        InfoPartyBalance.invoiceNo = lblBillNo.Text; //added on 24/03/2025  by  Nishana
                                        InfoPartyBalance.referenceNo = "";  //24/03/2025
                                        InfoPartyBalance.BillAmount = decimal.Parse(txtTotal.Text); //24/03/2025
                                        InfoPartyBalance.invoiceDate = DateTime.Parse(lblSessionDate.Text); //24/03/2025
                                        InfoPartyBalance.costCentreId = "1"; //24/03/2025
                                        InfoPartyBalance.exchangeDate = PublicVariables._fromDate; //24/03/2025
                                        InfoPartyBalance.exchangeRate = 1; //24/03/2025
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
                                        InfoPartyBalance.VoucherType = "POS SalesReturn";
                                       
                                        SPPartyBalance.PartyBalanceAdd(InfoPartyBalance);
                                    }
                                    //-------------------------------------------------------------------------------------
                                }
                            }
                            SPGeneral.POSBillUpdate(PublicVariables._counterId, PublicVariables._currentUserId, "SalesReturn");
                            ClearFunction();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot make bill as Amount ZERO", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        barcodeFocus();
                    }

                }
            }
            else
            {
                //MessageBox.Show("Please select other payment options", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                barcodeFocus();
            }
        }

        public void SaveLedgerPosting(string strMasterId, DataTable dtbltaxSummery)
        {
            
            LedgerPostingInfo InfoLedgerPosting = new LedgerPostingInfo();
            InfoLedgerPosting.VoucherNo = strMasterId;
            InfoLedgerPosting.VoucherType = "POS SalesReturn";
            InfoLedgerPosting.Optional = false;
            InfoLedgerPosting.Date = DateTime.Parse(lblSessionDate.Text);
            InfoLedgerPosting.Extra1 = "";
            InfoLedgerPosting.Extra2 = "";
            InfoLedgerPosting.InvoiceNo = lblBillNo.Text;
            InfoLedgerPosting.exchangeDate = PublicVariables._fromDate; //added on 24/03/2025 by Nishana
            InfoLedgerPosting.exchangeRate = 1;
            InfoLedgerPosting.postingType = "POS SalesReturn";
            //-------Debit--------------------------------------------------------
            if (rbtCash.Checked == true)
            {
                InfoLedgerPosting.LedgerId = strCashSalesLedgerId;
                InfoLedgerPosting.Debit = 0;
                InfoLedgerPosting.Credit = decimal.Parse(txtTotal.Text.ToString());
                new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);
            }
            else if (rbtCredit.Checked == true)
            {
                InfoLedgerPosting.LedgerId = lblLedgerId.Text.ToString();
                InfoLedgerPosting.Debit = 0;
                InfoLedgerPosting.Credit = decimal.Parse(txtTotal.Text.ToString());
                new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);
            }
            //-------------------------------------------

            //-------Credit-------------------------------------------------------
            InfoLedgerPosting.LedgerId = strSalesReturnLedgerId;
            InfoLedgerPosting.Debit = Convert.ToDecimal(txtTotal.Text) - Convert.ToDecimal(txtTaxAmt.Text);
            InfoLedgerPosting.Credit = 0;
            new LedgerPostingSP().LedgerPostingAdd(InfoLedgerPosting);

            //------------Tax Amount
            if (dtbltaxSummery.Rows.Count > 0)
            {
                for (int i = 0; i < dtbltaxSummery.Rows.Count; i++)
                {
                    if (decimal.Parse(dtbltaxSummery.Rows[i]["amt"].ToString()) != 0m)
                    {
                        InfoLedgerPosting.LedgerId = dtbltaxSummery.Rows[i]["ledgerId"].ToString();
                        InfoLedgerPosting.Debit = decimal.Parse(dtbltaxSummery.Rows[i]["amt"].ToString());
                        InfoLedgerPosting.Credit = 0;
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
                InfoStockPosting.VoucherType = "POS SalesReturn";
                InfoStockPosting.RackId = "Primary Rack";
                foreach (DataGridViewRow dgvrowCurChk in dgvProduct.Rows)
                {
                    if (!dgvrowCurChk.IsNewRow)
                    {
                        InfoStockPosting.InwardQty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoStockPosting.Optional = false;
                        InfoStockPosting.OutwardQty = 0m;
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
        public void FillDatatatablesforPrint()
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


            int inRowIndex = 0;
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
                    dr["NameArabic"] = "".ToString();
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
            dtblOtherDetails.Columns.Add("ReturnType");
            dtblOtherDetails.Columns.Add("QtyTotal");
            dtblOtherDetails.Columns.Add("TenderPaid");
            dtblOtherDetails.Columns.Add("TenderBalance");
            dtblOtherDetails.Columns.Add("TenderCash");
            dtblOtherDetails.Columns.Add("TenderCC");
            dtblOtherDetails.Columns.Add("TenderUPI");
            dtblOtherDetails.Columns.Add("qrCode", typeof(byte[]));


            DataRow dRowDetails = dtblOtherDetails.NewRow();
            dRowDetails["PartyName"] = "";
            dRowDetails["PartyAddress"] = "";
            dRowDetails["BillDate"] = lblBillDate.Text;
            dRowDetails["BillTime"] = lblBillTime.Text;
            dRowDetails["SessionDate"] = lblSessionDate.Text;
            dRowDetails["SessionNo"] = lblSessionNO.Text;
            dRowDetails["CounterId"] = lblCounter.Text;
            dRowDetails["UserName"] = lblUser.Text;
            dRowDetails["InvoiceNo"] = lblBillNo.Text;
            dRowDetails["SubTotal"] = txtSubTotal.Text;
            dRowDetails["BillDiscount"] = "";
            dRowDetails["TaxableAmount"] = txtTaxable.Text;
            dRowDetails["TaxAmount"] = txtTaxAmt.Text;
            dRowDetails["GrandTotal"] = txtTotal.Text;
            dRowDetails["AmountInWords"] = "";
            if (rbtCash.Checked == true)
            {
                dRowDetails["ReturnType"] = "Cash";
            }
            else if (rbtCredit.Checked == true)
            {
                dRowDetails["ReturnType"] = "Credit";
            }
            else if (rbtCreditNote.Checked == true)
            {
                dRowDetails["ReturnType"] = "CreditNote";
            }
            
            dRowDetails["QtyTotal"] = lblTotalQty.Text;




            //------------------------ QR Code Generation ----------- by Navas --------------------
            Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string companyname = dtblCompanyDetails.Rows[0][2].ToString();
            string vatno = dtblCompanyDetails.Rows[0][9].ToString();
            string invoicedate = Convert.ToDateTime(lblBillDate.Text).ToString("yyyy-MM-dd");
            string invoicetime = DateTime.Now.ToString("HH:mm:ss");
            invoicedate = invoicedate + "T" + invoicetime;
            string invoicetotal = ("-")+txtTotal.Text.Replace("SR", "");
            string invoicevatamount = "-"+txtTaxAmt.Text.Replace("SR", "");

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


            //---------------------------------
            dtblOtherDetails.Rows.Add(dRowDetails);

            
            dtblCompanyDetailsThermal = dtblCompanyDetails;
            dtblGridDetailsThermal = dtblGridDetails;
            dtblOtherDetailsThermal = dtblOtherDetails;


            if (dtblOtherDetailsThermal.Rows[0]["ReturnType"].ToString() != "CreditNote")
            {
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
            else if (dtblOtherDetailsThermal.Rows[0]["ReturnType"].ToString() == "CreditNote")
            {
                int pageWidth;
                int PageHight = 0;
                pageSetupDialog1.PageSettings = printDocumentCreditNote.DefaultPageSettings;
                pageSetupDialog1.AllowOrientation = false;
                PageHight = dtblGridDetailsThermal.Rows.Count + 570; //set rowcount
                printDocumentCreditNote.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Cs", 300, PageHight);
                pageWidth = printDocumentCreditNote.DefaultPageSettings.PaperSize.Width - printDocumentCreditNote.DefaultPageSettings.Margins.Left - printDocumentCreditNote.DefaultPageSettings.Margins.Right;
                PrintDocument document = new PrintDocument();
                printPreviewDialog1.Document = printDocumentCreditNote;
                //printPreviewDialog1.ShowDialog();
                printDocumentCreditNote.Print();
            }
            
        }

        public void FillDatatatablesforDevPrint()
        {
            //--------Company Details Datatable--------------
            DataTable dtblCompanyDetails = new DataTable();
            BranchSP SpBranch = new BranchSP();
            dtblCompanyDetails = SpBranch.BranchViewByBranchId(PublicVariables._branchId);
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


            int inRowIndex = 0;
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
                    dr["NameArabic"] = "".ToString();
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
            dtblOtherDetails.Columns.Add("ReturnType");
            dtblOtherDetails.Columns.Add("QtyTotal");

            dtblOtherDetails.Columns.Add("TenderPaid");
            dtblOtherDetails.Columns.Add("TenderBalance");
            dtblOtherDetails.Columns.Add("TenderCash");
            dtblOtherDetails.Columns.Add("TenderCC");
            dtblOtherDetails.Columns.Add("TenderUPI");
            //dtblOtherDetails.Columns.Add("qrCode", typeof(byte[]));
            dtblOtherDetails.Columns.Add("qrCode");


            DataRow dRowDetails = dtblOtherDetails.NewRow();
            dRowDetails["PartyName"] = "";
            dRowDetails["PartyAddress"] = "";
            dRowDetails["BillDate"] = lblBillDate.Text;
            dRowDetails["BillTime"] = lblBillTime.Text;
            dRowDetails["SessionDate"] = lblSessionDate.Text;
            dRowDetails["SessionNo"] = lblSessionNO.Text;
            dRowDetails["CounterId"] = lblCounter.Text;
            dRowDetails["UserName"] = lblUser.Text;
            dRowDetails["InvoiceNo"] = lblBillNo.Text;
            dRowDetails["SubTotal"] = txtSubTotal.Text;
            dRowDetails["BillDiscount"] = "";
            dRowDetails["TaxableAmount"] = txtTaxable.Text;
            dRowDetails["TaxAmount"] = txtTaxAmt.Text;
            dRowDetails["GrandTotal"] = txtTotal.Text;
            dRowDetails["AmountInWords"] = "";
            if (rbtCash.Checked == true)
            {
                dRowDetails["ReturnType"] = "Cash";
            }
            else if (rbtCredit.Checked == true)
            {
                dRowDetails["ReturnType"] = "Credit";
            }
            else if (rbtCreditNote.Checked == true)
            {
                dRowDetails["ReturnType"] = "CreditNote";
            }

            dRowDetails["QtyTotal"] = lblTotalQty.Text;


            ////------------------------ QR Code Generation ----------- by Navas --------------------
            //Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            //string companyname = dtblCompanyDetails.Rows[0][2].ToString();
            //string vatno = dtblCompanyDetails.Rows[0][9].ToString();
            //string invoicedate = Convert.ToDateTime(lblBillDate.Text).ToString("yyyy-MM-dd");
            //string invoicetime = DateTime.Now.ToString("HH:mm:ss");
            //invoicedate = invoicedate + "T" + invoicetime;
            //string invoicetotal = ("-") + txtTotal.Text.Replace("SR", "");
            //string invoicevatamount = "-" + txtTaxAmt.Text.Replace("SR", "");

            //int lencompanyname = companyname.Length;
            //int lenvatno = vatno.Length;
            //int leninvoicedate = invoicedate.Length;
            //int leninvoicetime = invoicetime.Length;
            //int leninvoicetotal = invoicetotal.Length;
            //int leninvoicevatamount = invoicevatamount.Length;

            //string strQRvariable = Convert.ToChar(1).ToString() + Convert.ToChar(lencompanyname).ToString() + companyname
            //    + Convert.ToChar(2).ToString() + Convert.ToChar(lenvatno).ToString() + vatno + Convert.ToChar(3).ToString() + Convert.ToChar(19).ToString()
            //    + invoicedate + Convert.ToChar(4).ToString() + Convert.ToChar(leninvoicetotal).ToString() + invoicetotal + Convert.ToChar(5).ToString()
            //    + Convert.ToChar(leninvoicevatamount).ToString() + invoicevatamount;

            //////string strQRvariable = dtblCompanyDetails.Rows[0][2].ToString() + "\n" + "VAT No:" + dtblCompanyDetails.Rows[0][9].ToString() + "\n" + "VAT total:" + lblTaxAmt.Text + "\n" + "Grand Total: " + lblGrand.Text + "\n" + "Invoice Date:" + dtpVoucherDate.Text;
            //////string strQRvariable = dtblCompanyDetails.Rows[0][2].ToString() + "\n" + "VAT No:" + dtblCompanyDetails.Rows[0][9].ToString();
            ////var utf8text = System.Text.Encoding.UTF8.GetBytes(strQRvariable);
            ////string qrdata = System.Convert.ToBase64String(utf8text);
            ////Image img = qrBarcode.Draw(qrdata, 500); //pictureBox1.Image;
            ////byte[] arr;
            ////ImageConverter converter = new ImageConverter();
            ////arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
            ////dRowDetails["qrCode"] = arr;

            //dRowDetails["qrCode"] = strQRvariable;

            //------------------------ QR Code Generation ----------- by Navas --------------------
            Zen.Barcode.CodeQrBarcodeDraw qrBarcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string companyname = dtblCompanyDetails.Rows[0]["branchName"].ToString();
            string vatno = dtblCompanyDetails.Rows[0]["tinNo"].ToString();
            string invoicedate = DateTime.Parse(lblBillDate.Text.ToString()).ToString("yyyy-MM-dd");
            string invoicetime = DateTime.Now.ToString("HH:mm:ss");
            invoicedate = invoicedate + "T" + invoicetime;
            string invoicetotal = "-" + Convert.ToDecimal(txtTotal.Text).ToString(FinanceSettingsInfo._roundDecimalPart);
            string invoicevatamount = "-" + Convert.ToDecimal(txtTaxAmt.Text).ToString(FinanceSettingsInfo._roundDecimalPart);

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


            //---------------------------------
            dtblOtherDetails.Rows.Add(dRowDetails);


            dtblCompanyDetailsThermal = dtblCompanyDetails;
            dtblGridDetailsThermal = dtblGridDetails;
            dtblOtherDetailsThermal = dtblOtherDetails;


            if (dtblOtherDetailsThermal.Rows[0]["ReturnType"].ToString() != "CreditNote")
            {
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

                DevPrint spPrint = new DevPrint();
                spPrint.PrintSalesReturnPOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesReturnPrintCopy);



            }
            else if (dtblOtherDetailsThermal.Rows[0]["ReturnType"].ToString() == "CreditNote")
            {
                //int pageWidth;
                //int PageHight = 0;
                //pageSetupDialog1.PageSettings = printDocumentCreditNote.DefaultPageSettings;
                //pageSetupDialog1.AllowOrientation = false;
                //PageHight = dtblGridDetailsThermal.Rows.Count + 570; //set rowcount
                //printDocumentCreditNote.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Cs", 300, PageHight);
                //pageWidth = printDocumentCreditNote.DefaultPageSettings.PaperSize.Width - printDocumentCreditNote.DefaultPageSettings.Margins.Left - printDocumentCreditNote.DefaultPageSettings.Margins.Right;
                //PrintDocument document = new PrintDocument();
                //printPreviewDialog1.Document = printDocumentCreditNote;
                ////printPreviewDialog1.ShowDialog();
                //printDocumentCreditNote.Print();
                DevPrint spPrint = new DevPrint();
                spPrint.PrintSalesReturnPOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesReturnPrintCopy);
                spPrint.PrintCreditNotePOS(dtblCompanyDetails, dtblGridDetails, dtblOtherDetails, dtblTaxDetailsThermal, counterInfo.DefaultPrinter, counterInfo.Directprint, counterInfo.SalesReturnPrintCopy);
               

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
                dTaxableTotal = Math.Round(dTaxableTotal, FinanceSettingsInfo._roundDecimal);
                dTotal = Math.Round(dTotal, FinanceSettingsInfo._roundDecimal);
                dtbl.Rows[i]["amt"] = dTotal.ToString();
                dtbl.Rows[i]["taxableAmt"] = dTaxableTotal.ToString();
            }

            return dtbl;
        }
        public string SaveFunction(string strReturnMethod, string strReturnStatus)
        {
            string strMasterId = "";

            

            lblBillNo.Text = POSBillNumberMax();

            POSSalesReturnMasterInfo InfoPOSSalesReturnMaster = new POSSalesReturnMasterInfo();
            InfoPOSSalesReturnMaster.InvoiceNo = lblBillNo.Text;
            InfoPOSSalesReturnMaster.BillDate = Convert.ToDateTime(lblBillDate.Text);
            InfoPOSSalesReturnMaster.BillTime = lblBillTime.Text;
            InfoPOSSalesReturnMaster.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
            InfoPOSSalesReturnMaster.CounterId = PublicVariables._counterId;
            InfoPOSSalesReturnMaster.SessionNo = lblSessionNO.Text;
            InfoPOSSalesReturnMaster.ReturnMethod = strReturnMethod;
            InfoPOSSalesReturnMaster.Billtype = "I";
            InfoPOSSalesReturnMaster.LedgerId = lblLedgerId.Text.ToString();
            InfoPOSSalesReturnMaster.LedgerName = txtCustName.Text.ToString();
            InfoPOSSalesReturnMaster.SubTotalAmount = Convert.ToDecimal(txtSubTotal.Text);
            InfoPOSSalesReturnMaster.TaxableAmount = Convert.ToDecimal(txtTaxable.Text);
            InfoPOSSalesReturnMaster.TotalTaxAmount = Convert.ToDecimal(txtTaxAmt.Text);
            InfoPOSSalesReturnMaster.TotalAmount = Convert.ToDecimal(txtTotal.Text);
            InfoPOSSalesReturnMaster.TotalQty = Convert.ToDecimal(lblTotalQty.Text);
            InfoPOSSalesReturnMaster.ReturnStatus = strReturnStatus;
            InfoPOSSalesReturnMaster.UserId = PublicVariables._currentUserId;

            strMasterId = POSSalesReturnMasterSP.POSSalesReturnMasterAdd(InfoPOSSalesReturnMaster);

            if (strMasterId != "")
            {
                POSSalesReturnDetails1Info InfoPOSSalesReturnDetails1 = new POSSalesReturnDetails1Info();

                InfoPOSSalesReturnDetails1.POSSalesReturnMasterId = strMasterId;
                InfoPOSSalesReturnDetails1.InvoiceNo = lblBillNo.Text;
                InfoPOSSalesReturnDetails1.BillDate = Convert.ToDateTime(lblBillDate.Text);
                InfoPOSSalesReturnDetails1.SessionDate = Convert.ToDateTime(lblSessionDate.Text);
                InfoPOSSalesReturnDetails1.CounterId = PublicVariables._counterId;
                InfoPOSSalesReturnDetails1.SessionNo = lblSessionNO.Text;
                InfoPOSSalesReturnDetails1.UserId = PublicVariables._currentUserId;

                foreach (DataGridViewRow dgvrowCurChk in dgvProduct.Rows)
                {
                    if (!dgvrowCurChk.IsNewRow)
                    {
                        InfoPOSSalesReturnDetails1.LineNumber = int.Parse(dgvrowCurChk.Cells["SLNo"].Value.ToString());
                        InfoPOSSalesReturnDetails1.ProductCode = dgvrowCurChk.Cells["ProductCode"].Value.ToString();
                        InfoPOSSalesReturnDetails1.Barcode = dgvrowCurChk.Cells["Barcode"].Value.ToString();
                        InfoPOSSalesReturnDetails1.ProductName = dgvrowCurChk.Cells["ItemName"].Value.ToString();
                        InfoPOSSalesReturnDetails1.UnitId = dgvrowCurChk.Cells["UnitId"].Value.ToString();
                        InfoPOSSalesReturnDetails1.BillQty = decimal.Parse("0");
                        InfoPOSSalesReturnDetails1.Qty = decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesReturnDetails1.Rate = decimal.Parse(dgvrowCurChk.Cells["SalesRate"].Value.ToString());
                        InfoPOSSalesReturnDetails1.ExcludeRate = decimal.Parse(dgvrowCurChk.Cells["ExcludeRate"].Value.ToString());
                        InfoPOSSalesReturnDetails1.CostPrice = decimal.Parse("0".ToString());
                        InfoPOSSalesReturnDetails1.GrossValue = decimal.Parse(dgvrowCurChk.Cells["GrossValue"].Value.ToString());
                        InfoPOSSalesReturnDetails1.DiscPer = decimal.Parse("0".ToString());
                        InfoPOSSalesReturnDetails1.DiscAmount = decimal.Parse(dgvrowCurChk.Cells["DiscAmt"].Value.ToString());
                        InfoPOSSalesReturnDetails1.NetAmount = decimal.Parse(dgvrowCurChk.Cells["NetValue"].Value.ToString());
                        InfoPOSSalesReturnDetails1.TaxId = dgvrowCurChk.Cells["TaxId"].Value.ToString();
                        InfoPOSSalesReturnDetails1.TaxPer = decimal.Parse(dgvrowCurChk.Cells["TaxPerc"].Value.ToString());
                        InfoPOSSalesReturnDetails1.TaxAmount = decimal.Parse(dgvrowCurChk.Cells["TaxAmt"].Value.ToString());
                        InfoPOSSalesReturnDetails1.Amount = decimal.Parse(dgvrowCurChk.Cells["Total"].Value.ToString());
                        InfoPOSSalesReturnDetails1.BillDiscAmountperItem = dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value == null ? 0 : decimal.Parse(dgvrowCurChk.Cells["BillDiscIndProductAmt"].Value.ToString());
                        InfoPOSSalesReturnDetails1.ConversionFactor = decimal.Parse(dgvrowCurChk.Cells["UnitConversion"].Value.ToString());
                        InfoPOSSalesReturnDetails1.AmountBeforeDisc = decimal.Parse(dgvrowCurChk.Cells["amountBeforeDisc"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());
                        InfoPOSSalesReturnDetails1.RateDiscAmount = decimal.Parse(dgvrowCurChk.Cells["rateDiscAmount"].Value.ToString()) * decimal.Parse(dgvrowCurChk.Cells["Qty"].Value.ToString());                       
                        InfoPOSSalesReturnDetails1.OfferId =dgvrowCurChk.Cells["offerId"].Value.ToString();

                        string strPOSSalesReturnDetails1Id = "";

                        strPOSSalesReturnDetails1Id = POSSalesReturnDetails1SP.POSSalesReturnDetails1Add(InfoPOSSalesReturnDetails1);
                    }
                }

                //CreditNote insert
                if (strReturnMethod == "CreditNote")
                {
                    POSSalesReturnMasterSP.POSCreditNoteMasterAdd(InfoPOSSalesReturnMaster);
                }
                
            }

            return strMasterId;
        }

        private void btnClosePOS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printDocumentThermal3_PrintPage(object sender, PrintPageEventArgs e)
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
            e.Graphics.DrawString("SALES RETURN", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);

            lineGap = lineGap + 40;
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["invoiceNo"].ToString() + ": رقم الفاتورة", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
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
            lineGap = lineGap + 30;
            e.Graphics.DrawString("Subtotal/ المجموع الفرعي", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["SubTotal"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 25;
            e.Graphics.DrawString("VAT/ ضريبة", new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["TaxAmount"].ToString(), new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 280, lineGap, SFRight);
            lineGap = lineGap + 25;
            e.Graphics.DrawString("Grand Total/ المجموع الإجمالي", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 15, lineGap);
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["GrandTotal"].ToString(), new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 280, lineGap, SFRight);


            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);

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

            lineGap = lineGap + 30;
            e.Graphics.DrawString(dtblOtherDetailsThermal.Rows[0]["CounterId"].ToString() + ": عداد", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            lineGap = lineGap + 30;
            e.Graphics.DrawString("Return Type: " + dtblOtherDetailsThermal.Rows[0]["ReturnType"].ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);



            //------------------ QRCode ----------------------
            //PrintPageHight = PrintPageHight + 40;
            lineGap = lineGap + 40;
            byte[] logo11 = null;
            logo11 = (byte[])dtblOtherDetailsThermal.Rows[0]["qrCode"];
            MemoryStream ms2 = new MemoryStream(logo11);
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

        private void printDocumentCreditNote_PrintPage(object sender, PrintPageEventArgs e)
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
            e.Graphics.DrawString("CREDIT NOTE", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, 150, lineGap, SFCentre);

            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);

            lineGap = lineGap + 40;
            e.Graphics.DrawString("CreditNote No: " + dtblOtherDetailsThermal.Rows[0]["invoiceNo"].ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Bill Date: " + dtblOtherDetailsThermal.Rows[0]["BillDate"].ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);
            lineGap = lineGap + 20;
            e.Graphics.DrawString("Total Amount: " + dtblOtherDetailsThermal.Rows[0]["GrandTotal"].ToString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, 15, lineGap);

            lineGap = lineGap + 20;
            e.Graphics.DrawLine(Pens.Black, 15, lineGap, 290, lineGap);

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

            lineGap = lineGap + 40;
            e.Graphics.DrawImage(im1, 30, lineGap, 200, 40);
            
        }

        private void frmPOSSalesReturn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dgvProduct.Rows.Count > 1)
            {
                MessageBox.Show("Cannot Close POS Return!! bill not Clear!");
                e.Cancel = true;
                return;
            }
        }

        private void btnFindCustomer_Click(object sender, EventArgs e)
        {
            frmProductPopUpGrid frm = new frmProductPopUpGrid();
            frm.CallFromPOSSalesReturn(this, "customer");
            if (lblLedgerId.Text.ToString() != "")
            {
                DataTable dtbl = new DataTable();
                dtbl = spProduct.GetCustomerDetailsbyLedgerId(lblLedgerId.Text.ToString());
                if (dtbl.Rows.Count > 0)
                {
                    txtCustomerId.Text = dtbl.Rows[0]["ledgerCode"].ToString();
                    txtCustName.Text = dtbl.Rows[0]["ledgerName"].ToString();
                    rbtCredit.Checked = true; 
                }
            }
            else
            {
                lblLedgerId.Text = "";
                txtCustomerId.Text = "";
                txtCustName.Text = "";

            }
            barcodeFocus();
        }
        public void FillrowAfterPickingCustomer(string ledgerId)
        {
            lblLedgerId.Text = ledgerId;
        }

        public void FillrowAfterPickingProduct(string productCode)
        {
            txtBarcode.Text = productCode;
            if (productCode != "")
            {
                barcodeScanning();
            }
            else
            {
                barcodeFocus();
            }
            txtBarcode.Focus();
        }

        private void txtCustomerId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtbl = new DataTable();
                dtbl = spProduct.POSAccountLedgerGetNameByCode(txtCustomerId.Text);
                if (dtbl.Rows.Count > 0)
                {
                    lblLedgerId.Text = dtbl.Rows[0]["ledgerId"].ToString();
                    txtCustomerId.Text = dtbl.Rows[0]["ledgerCode"].ToString();
                    txtCustName.Text = dtbl.Rows[0]["ledgerName"].ToString();

                    rbtCredit.Checked = true; 
                }
                else
                {
                    MessageBox.Show("Customer Code not found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblLedgerId.Text = "";
                    txtCustomerId.Text = "";
                    txtCustName.Text = "";

                }
                barcodeFocus();

            }
        }

        private void btnFindProduct_Click(object sender, EventArgs e)
        {
            frmProductPopUpGrid frm = new frmProductPopUpGrid();
            frm.CallFromPOSSalesReturn(this, "product");
            txtBarcode.Focus();
        }

        private void rbtCreditNote_CheckedChanged(object sender, EventArgs e)
        {
            rbtCash.ForeColor = Color.White;
            rbtCredit.ForeColor = Color.White;
            rbtCreditNote.ForeColor = Color.Yellow; 
        }

        private void rbtCash_CheckedChanged(object sender, EventArgs e)
        {
            rbtCash.ForeColor = Color.Yellow;
            rbtCredit.ForeColor = Color.White;
            rbtCreditNote.ForeColor = Color.White;
        }

        private void rbtCredit_CheckedChanged(object sender, EventArgs e)
        {
            rbtCash.ForeColor = Color.White;
            rbtCredit.ForeColor = Color.Yellow ;
            rbtCreditNote.ForeColor = Color.White;
        }

        private void btnReturnFullBill_Click(object sender, EventArgs e)
        {
            if (lblBarcodeScanningType.Text == "<- Scan Bill No Here")
            {
                lblBarcodeScanningType.Text = "";
                lblBarcodeScanningType.Visible = false;
            }
            else
            {
                lblBarcodeScanningType.Text = "<- Scan Bill No Here";
                lblBarcodeScanningType.Visible = true;
            }
            barcodeFocus();
        }

       
    }
}
