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
    public partial class frmPriceCheck : Form
    {
        public string strbarcode = "";
        public frmPriceCheck()
        {
           
            InitializeComponent();
            
        }
        ProductSP SPProduct = new ProductSP();
        bool isClear = false;
        POSSettingsInfo InfoPosSetting = new POSSettingsInfo();
        POSSalesMasterSP salesMasterSP = new POSSalesMasterSP();
        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            //if (!isClear)
            //    BarcodeScanning();

        }
        private void BarcodeScanning()
        {
            try
            {
                string strBarcode = "";
                string strItemCode = "";
                string strItemName = "";
                string strUnitId = "";
                string strUnitName = "";
                decimal decUnitConversion = 1;
                decimal decSalesPrice = 0;
                decimal decPurchaseRate = 0;
                DataTable dtbl = new DataTable();
                dtbl = SPProduct.GetProductDetailsByBarcode(txtBarcode.Text);
                if (dtbl.Rows.Count > 0) //load details by Barcode
                {
                    strBarcode = txtBarcode.Text.Trim();
                    strItemCode = dtbl.Rows[0]["productCode"].ToString();
                    strItemName = dtbl.Rows[0]["productName"].ToString();
                    strUnitId = dtbl.Rows[0]["unitId"].ToString();
                    strUnitName = dtbl.Rows[0]["unitName"].ToString();
                    decUnitConversion = Convert.ToDecimal(dtbl.Rows[0]["conversionRate"].ToString());

                    decSalesPrice = SPProduct.ProductSalesRateForSalePOS(strItemCode, "1", DateTime.Now, strUnitId);

                    decPurchaseRate = Convert.ToDecimal(dtbl.Rows[0]["purchaseRate"].ToString());

                    if (decSalesPrice == 0)
                    {
                        MessageBox.Show("Sales Price is ZERO", "WARNING");
                        barcodeFocus();
                        return;
                    }
                }
                else
                {
                    dtbl = SPProduct.GetProductDetailsByProductCode(txtBarcode.Text.Trim());
                    if (dtbl.Rows.Count > 0) //load details by ProductCode
                    {
                        strBarcode = txtBarcode.Text.Trim();
                        strItemCode = dtbl.Rows[0]["productCode"].ToString();
                        strItemName = dtbl.Rows[0]["productName"].ToString();
                        strUnitId = dtbl.Rows[0]["unitId"].ToString();
                        strUnitName = dtbl.Rows[0]["unitName"].ToString();
                        decUnitConversion = Convert.ToDecimal(dtbl.Rows[0]["conversionRate"].ToString());

                        decSalesPrice = SPProduct.ProductSalesRateForSalePOS(strItemCode, "1", DateTime.Now, strUnitId);
                        decPurchaseRate = Convert.ToDecimal(dtbl.Rows[0]["purchaseRate"].ToString());
                        if (decSalesPrice == 0)
                        {
                            MessageBox.Show("Sales Price is ZERO", "WARNING");
                            barcodeFocus();
                            return;
                        }
                    }
                 
                    else
                    {
                        MessageBox.Show("Barcode not Found", "WARNING");
                        barcodeFocus();
                        return;
                    }
                }

                if (strItemCode != "")
                {
                    lblItemCode.Text = strItemCode;
                    lblItemName.Text = strItemName;
                    //   strUnitId = dtbl.Rows[0]["unitId"].ToString();
                    lblBaseUnit.Text = strUnitName;

                    lblPurchaseRate.Text = Math.Round(decPurchaseRate, SettingsInfo._roundDecimal).ToString();
                  // assiging stock to label
                    decimal decStock = salesMasterSP.ProductStockGetCorrespondingtoBatchAndGodown("POS Sales", "", strItemCode, "1", "1", "1");

                    if (InfoPosSetting.StockView)
                    {
                        if (strItemCode != "")
                        {
                            lblStock.Text = decStock.ToString(); 
                        }
                    }
                    calculateVatPerc(strItemCode);

                    //  dgvProduct.Rows[dgvCurRow].Cells["SalesRate"].Value = Math.Round(decSalesPrice, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);


                    AssignExludeRate(decSalesPrice);

                }
                FillGrid();
            }
            catch (Exception ex) { }
        }
        private void AssignExludeRate(decimal SalesPrice)
        {
            //-------------------TAX INCLUDED
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.GetProductTaxDetails(lblItemCode.Text);
            if (dtbl.Rows.Count > 0) //load tax details  by ProductCode
            {
                decimal dcRate = 0;
                decimal dcTaxExcludedRate = 0;
                bool isIncluded = false;
                if (SalesPrice != null && SalesPrice != 0)
                {
                    if (SalesPrice != null)
                    {
                        dcRate = SalesPrice;
                    }
                    if (SettingsInfo._taxType == "Applicable to product" && dcRate != 0)
                    {
                        if (SettingsInfo._vatIncluded == true || SettingsInfo._vatCessIncluded == true)
                        {
                            if (dtbl.Rows[0]["taxId"].ToString() != null && dtbl.Rows[0]["taxId"].ToString() != "")
                            {
                                isIncluded = true;
                                decimal dTaxPerc = 0;
                                decimal.TryParse(dtbl.Rows[0]["rate"].ToString(), out   dTaxPerc);
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

                CalculateIncludeRate(dcTaxExcludedRate, decimal.Parse(lblVat.Text));
            }
        }
        public void barcodeFocus()
        {
            txtBarcode.Clear();
            txtBarcode.Focus();
        }
        private void CalculateIncludeRate(decimal ExcludedRate, decimal taxPerc)
        {
            decimal dcTaxIncludedRate = 0;
            decimal dcVatAmount = 0;
            if (taxPerc != 0)
            {
                dcVatAmount = Math.Round(((ExcludedRate * taxPerc) / (100)), SettingsInfo._roundDecimal);
            }
            else
            {
                dcVatAmount = 0;
            }
            dcTaxIncludedRate = ExcludedRate + dcVatAmount;
            lblexcludeRate.Text = Math.Round(decimal.Parse(ExcludedRate.ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            lblincludeRate.Text = Math.Round(decimal.Parse(dcTaxIncludedRate.ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
        }
        private void calculateVatPerc(string ItemCode)
        {
            try
            {
                DataTable dtbl = new DataTable();
                dtbl = SPProduct.GetProductTaxDetails(ItemCode);
                if (dtbl.Rows.Count > 0) //load tax details details by ProductCode
                {
                    lblVat.Text = Math.Round(decimal.Parse(dtbl.Rows[0]["rate"].ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxId"].Value = dtbl.Rows[0]["taxId"].ToString();
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value = dtbl.Rows[0]["rate"].ToString();
                }
                else
                {
                    lblVat.Text = Math.Round(decimal.Parse("0"), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxId"].Value = "1";
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value = 0;
                }
            }
            catch { }

        }
        private decimal AssignExludeRateForGrid(decimal SalesPrice)
        {
            //-------------------TAX INCLUDED
            decimal dcRate = 0;
            decimal dcTaxExcludedRate = 0;
            bool isIncluded = false;
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.GetProductTaxDetails(lblItemCode.Text);
            if (dtbl.Rows.Count > 0) //load tax details  by ProductCode
            {
                
                if (SalesPrice != null && SalesPrice != 0)
                {
                    if (SalesPrice != null)
                    {
                        dcRate = SalesPrice;
                    }
                    if (SettingsInfo._taxType == "Applicable to product" && dcRate != 0)
                    {
                        if (SettingsInfo._vatIncluded == true || SettingsInfo._vatCessIncluded == true)
                        {
                            if (dtbl.Rows[0]["taxId"].ToString() != null && dtbl.Rows[0]["taxId"].ToString() != "")
                            {
                                isIncluded = true;
                                decimal dTaxPerc = 0;
                                decimal.TryParse(dtbl.Rows[0]["rate"].ToString(), out   dTaxPerc);
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
               
            }
            return dcTaxExcludedRate;
        }
        private decimal CalculateIncludeRateForgrid(decimal ExcludedRate, decimal taxPerc)
        {
            decimal dcTaxIncludedRate = 0;
            decimal dcVatAmount = 0;
            if (taxPerc != 0)
            {
                dcVatAmount = Math.Round(((ExcludedRate * taxPerc) / (100)), SettingsInfo._roundDecimal);
            }
            else
            {
                dcVatAmount = 0;
            }
            dcTaxIncludedRate = ExcludedRate + dcVatAmount;
           // lblexcludeRate.Text = Math.Round(decimal.Parse(ExcludedRate.ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
           // lblincludeRate.Text = Math.Round(decimal.Parse(dcTaxIncludedRate.ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
            return dcTaxIncludedRate;
        }
        private void calculateVatPercForgrid(string ItemCode)
        {
            try
            {
                DataTable dtbl = new DataTable();
                dtbl = SPProduct.GetProductTaxDetails(ItemCode);
                if (dtbl.Rows.Count > 0) //load tax details details by ProductCode
                {
                    lblVat.Text = Math.Round(decimal.Parse(dtbl.Rows[0]["rate"].ToString()), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxId"].Value = dtbl.Rows[0]["taxId"].ToString();
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value = dtbl.Rows[0]["rate"].ToString();
                }
                else
                {
                    lblVat.Text = Math.Round(decimal.Parse("0"), SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxId"].Value = "1";
                    //dgvProduct.Rows[dgvCurRow].Cells["TaxPerc"].Value = 0;
                }
            }
            catch { }

        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BarcodeScanning();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Clear()
        {
            isClear = true;
            txtBarcode.Clear();
            lblItemCode.Text = "";
            lblItemName.Text = "";
            lblBaseUnit.Text = "";
            lblexcludeRate.Text = "";
            lblincludeRate.Text = "";
            lblVat.Text = "";
            dgvProducts.DataSource = null;
            txtBarcode.Focus();
           // isClear = false;
        }
        private void FillGrid()
        {
            try
            {
                decimal salesprice = 0;
                decimal excludeRate = 0;
                decimal IncludeRate = 0;
                decimal taxperc = 0;
                DataTable dtbl = new DataTable();
                dtbl = SPProduct.POSPriceCheckFillGrid(lblItemCode.Text);
                if (dtbl.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtbl.Rows)
                    {
                        salesprice = 0;
                        excludeRate = 0;
                        IncludeRate = 0;
                        taxperc = 0;
                        DataTable dtbltx = new DataTable();
                        dtbltx = SPProduct.GetProductTaxDetails(lblItemCode.Text);
                        if (dtbltx.Rows.Count > 0) //load tax details details by ProductCode
                        {
                            taxperc = decimal.Parse(dtbltx.Rows[0]["rate"].ToString());
                        }
                        salesprice = decimal.Parse(dr["SalesRate"].ToString());
                        excludeRate = AssignExludeRateForGrid(salesprice);
                        dr["Exclude Rate"] = Math.Round(excludeRate, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                        dr["Vat%"] = Math.Round(taxperc, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                        IncludeRate = CalculateIncludeRateForgrid(excludeRate, taxperc);
                        dr["Include Rate"] = Math.Round(IncludeRate, SettingsInfo._roundDecimal).ToString(SettingsInfo._roundDecimalPart);
                    }

                }
                dgvProducts.DataSource = dtbl;
              
                dgvProducts.Columns[2].Visible = false;
                dgvProducts.Columns[3].Visible = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("UL6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmPriceCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
               if ( lblPRate.Visible == true )
               {
                   lblPRate.Visible = false;
                   lblPurchaseRate.Visible = false;
               }
               else
               {
                   lblPRate.Visible = true;
                   lblPurchaseRate.Visible = true;
               }
            }
        }

        private void frmPriceCheck_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(strbarcode))
            {
                txtBarcode.Text = strbarcode;
                BarcodeScanning();
            }
            else
            {
                txtBarcode.Focus();
            }

        }

        
    }

}

