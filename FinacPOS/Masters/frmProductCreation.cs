using DevExpress.CodeParser;
using DevExpress.XtraPrinting.Export.Pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
namespace FinacPOS
{

    //Extra1 using for ProductName Arabic //varis
    partial class frmProductCreation : Form
    {
        public frmProductCreation()
        {
            InitializeComponent();
            setLanguage(PublicVariables._ModuleLanguage);

        }

        //****************************************************************************************************************************************
        //PUBLIC VARIABLE DECLARATION
        //****************************************************************************************************************************************
        #region PUBLIC VARIABLE DECLARATIONS

        UserGroupSettingsSP spUsergroupSettings = new UserGroupSettingsSP();//changed on 19/10/2023 sheena    
        bool isDeleteEnable = false;// added 19/10/2023
        bool isEditEnable = true;// added 19/10/2023

        ProductSP SPProduct = new ProductSP();
        TaxMasterSP SPTaxMaster = new TaxMasterSP();
        CheckUserPrivilege checkuserprivilege = new CheckUserPrivilege();
        BrandSP BrandSP = new BrandSP();
        ProductGroupSP ProductGroupSP = new ProductGroupSP();
        UnitSP UnitSP = new UnitSP();

        ProductDetailsSP SPProductDetails = new ProductDetailsSP();
        BOMSP SPBOM = new BOMSP();
        ComponentsSettingsSP SPComponentsSettings = new ComponentsSettingsSP();
        UnitConversionSP SPUnitConversion = new UnitConversionSP();
        UnitConversionHistorySP SPUnitConversionHistory = new UnitConversionHistorySP();
        StockPostingSP SPStockPosting = new StockPostingSP();
        BatchSP SPBatch = new BatchSP();
        ComboValidation objComboValidation = new ComboValidation();
        TransactionsGeneralFill objTransactionsGeneralFill = new TransactionsGeneralFill();
        OtherDateValidationFunction objDate = new OtherDateValidationFunction();
        DataGridViewTextBoxEditingControl TextBoxControl;
        //frmProductRegister objfrmProductRegister;
       // frmProgress frmCompanyProgress = new frmProgress();
        string strOldGrpId = "", strOldBrandId = "", strOldUnitId = "", strPrvUnitId, strProductIdToEdit = "", strPrdDtlsIdToEdit = "", strDefaultRackId = "", strOldMainGrpCode = "";
        DataTable dtblMultiUnits = new DataTable();//for keeping details from multiple unit form
        DataTable dtblRowMaterials = new DataTable();//for keeping details from BOM form
        DataTable dtblPackage = new DataTable();//for keeping details from Package form
        int inKeyPrsCou = 0, inArrOfRemoveIndex = 0;
        string[] strArrOfRemove = new string[100];
        bool isClose = false;
        string StPostingDate = "";

        byte[] logo = null;
        byte[] tempLogo = null;
        string destinationPath = "", oldDestinationPath = "";


        DataTable dtblProdcucts = new DataTable();
        string strDefault = "";///created by sheena
        DataTable dtblUnits = new DataTable();//created by sheena
        bool isDontExecuteCellEnter = false;
        General spgeneral = new General();
        decimal decOldAmount = 0;

        string strFromCall = "", strDefaultId = "";
        bool isProductNameTextFill = false;
        string strLookupType = "";
        DataTable dtblProductsLookup = new DataTable();
        bool isExecuteEndEdit = true;
        string strUnitIdfromProductPopup = "";
        int inLookupCurrenRowIndex = 0;
        bool isProductPurchasePrice = false;
        DataTable dtblPartylookup = new DataTable();
        AccountLedgerSP SpAccountLedger = new AccountLedgerSP();
        bool isDontExecuteCashValueChange = false;
        DataTable dtblsalesprice = new DataTable();
        bool isFormLoad = false;
        public string _mainMenuItem = "";
        public string returningFormName = "";
        private DataRowView taxComboSelectedRow;
        #endregion
        //****************************************************************************************************************************************
        //FUNCTIONS
        //****************************************************************************************************************************************
        #region FUNCTIONS
        //-----------------------------------initial settings for save----------------------------------------
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
        public void InitialSettingsForSave()
        {
            try
            {
                TCProduct.TabPages[1].Enabled = false;
                TCProduct.TabPages[2].Enabled = false;
                btnDelete.Enabled = false;
                btnPrint.Enabled = false;
                cmbAllowBatch.Text = "No";
                cmbBom.Text = "No";
                cmbMultipleUnit.Text = "No";

                cmbOpeningStock.Text = "No";
                dgvDetails.Enabled = false;
                objTransactionsGeneralFill.FillGodownToGridComboColumn(godown);
                strDefaultRackId = objTransactionsGeneralFill.FillRackToGridComboColumn(rack, "Primary Location");
                if (!InventorySettingsInfo._maintainGodown)
                {
                    dgvDetails.Columns["godown"].Visible = false;
                }
                if (!InventorySettingsInfo._maintainRack)
                {
                    dgvDetails.Columns["rack"].Visible = false;

                }
                if (!SettingsInfo._maintainBatch)
                {
                    dgvDetails.Columns["batch"].Visible = false;
                    dgvDetails.Columns["mfd"].Visible = false;
                    dgvDetails.Columns["expd"].Visible = false;
                    cmbAllowBatch.Enabled = false;
                }

                cbxGatePass.Visible = false;

                //GenerateProductCode();
                //generateNewProductCode();
                GetDefaultImage();
                oldDestinationPath = "";
                strDeleteIds = "";
                chkShowExpiry.Checked = false;
                txtExpiryDays.Text = "0";
                if (chkShowExpiry.Checked)
                {
                    lblExpiryDays.Visible = true;
                    txtExpiryDays.Visible = true;
                }
                else
                {
                    lblExpiryDays.Visible = false;
                    txtExpiryDays.Visible = false;
                }
                grpNutritionFact.Visible = false;

                //TextBoxControl = TextBoxControl(txtProductName);

                //if (TextBoxControl != null)
                //{
                //    //-------------

                //    if (dgvProduct.CurrentCell != null && dgvProduct.Columns[dgvProduct.CurrentCell.ColumnIndex].Name == "productName")
                //    {


                //    }
                //    else if (dgvProduct.CurrentCell != null && dgvProduct.Columns[dgvProduct.CurrentCell.ColumnIndex].Name == "productCode")
                //    {
                //        TextBoxControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //        TextBoxControl.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //        TextBoxControl.AutoCompleteCustomSource = CodeColletion;

                //    }
                //    if (dgvProduct.CurrentCell != null && dgvProduct.Columns[dgvProduct.CurrentCell.ColumnIndex].Name != "productName" && dgvProduct.Columns[dgvProduct.CurrentCell.ColumnIndex].Name != "productCode") //"barcode") comented by Rasha on 16-Fev-2011
                //    {
                //        DataGridViewTextBoxEditingControl editControl = (DataGridViewTextBoxEditingControl)dgvProduct.EditingControl;
                //        editControl.AutoCompleteMode = AutoCompleteMode.None;
                //    }
                //    //-------------
                //    TextBoxControl.KeyPress += TextBoxCellEditControlKeyPress;
                //}
                //Added on 13/Apr/2025
                if (FinanceSettingsInfo._ActivateTax)
                {
                    if (FinanceSettingsInfo._VatIncluded == true || FinanceSettingsInfo._VatandCessIncluded == true)
                    {
                        cmbTaxType.Text = "Included";
                    }
                    else
                        cmbTaxType.Text = "Excluded";
                }
                else
                    cmbTaxType.Text = "NA";

                chkWithoutBarcode.Checked = false;
                lstStock.Visible = InventorySettingsInfo._ShowGodownWiseStock;

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //----------------------------------Common Initial Settings--------------------------------------------
        bool isUnitClear = false;
        public void CommonInitialSettings()
        {
            try
            {
                isClear = true;
                isUnitClear = true;
               // if (!SettingsInfo._tax)
                if (!FinanceSettingsInfo._ActivateTax)
                {
                    cmbTax.Enabled = false;
                }
                FillProducts(false, null);
                txtProductName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtProductName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtProductName.AutoCompleteCustomSource = NameColletion;
                FillTaxCombo();
                FillProductMainGroupCombo();
                FillGroupCategory1();
                FillGroupCategory2();
                FillGroupCategory3();
                FillProductGroupCombo();
                // FillProductGroupCombo();
             
                FillUnitCombo();
                FillBrandCombo();

                isClear = false;
                cmbCategory.SelectedIndex = 0;
                //cmbProductMainGrp.SelectedValue = 0;
                cmbUnit.SelectedIndex = 0;
                FillUnitToGridComboColumn();
                FillCashOrPartyCombo();
               
                isUnitClear = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        AutoCompleteStringCollection NameColletion = new AutoCompleteStringCollection();
        public void FillProducts(bool isProductName, DataGridViewTextBoxEditingControl editControl)
        {
            dtblProdcucts = new TransactionsGeneralFill().ProductFillforLsit(PublicVariables._branchId, "");
            NameColletion = new AutoCompleteStringCollection();
            dtblProductsLookup = new TransactionsGeneralFill().ProductFillforLookup(PublicVariables._branchId, "SalesInvoice"); //SPProduct.ProductViewAllWithOutBOM();

            foreach (DataRow dr in dtblProdcucts.Rows)
            {
                NameColletion.Add(dr["productName"].ToString());
            }


        }

        //-------------------------------------------------------------------------------------------------------------------------
        byte[] ReadFile(string strImagePath)
        {
            //Read image from the specified location 
            // return byte form image

            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(strImagePath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(strImagePath, FileMode.Open,
                                                    FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to 

            //supply number of bytes to read from file.
            //In this case we want to read entire file. 

            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }
        public void GetDefaultImage()
        {
            // To get deafult image
            // As we store default image in start up path,  we assign the file path as its path
            string strImagePath = Application.StartupPath + "\\logo.JPG";
            logo = ReadFile(strImagePath);
            MemoryStream ms = new MemoryStream(logo);
            Image newImage = Image.FromStream(ms);
            pbLogo.Image = newImage;
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;

            destinationPath = "";

        }
        public void GenerateProductCode()
        {
            if (SettingsInfo._automaticProductCodeGeneration)
            {
                if (strPrdDtlsIdToEdit == "")
                {
                    decimal decNewProductcode = 1;
                    string strProductcode = SPProduct.ProductMax().ToString();
                    if (strProductcode != "string")
                    {
                        decNewProductcode = decimal.Parse(strProductcode.ToString());
                        string strNewProductCode = decNewProductcode.ToString();
                        int _NoOfDigt = decNewProductcode.ToString().Length;
                        if (_NoOfDigt == 3)
                        {
                            strNewProductCode = (decNewProductcode.ToString()).PadLeft(4, '0');
                        }
                        else if (_NoOfDigt == 2)
                        {
                            strNewProductCode = (decNewProductcode.ToString()).PadLeft(4, '0');
                        }
                        else if (_NoOfDigt == 1)
                        {
                            strNewProductCode = (decNewProductcode.ToString()).PadLeft(4, '0');
                        }
                        else
                        {
                            strNewProductCode = (decNewProductcode.ToString());
                        }
                        txtProductCode.Text = strNewProductCode;
                    }
                    else
                    {
                        // Cant Generate Automatically string entry exist
                        MessageBox.Show("Cant generate automatically", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SettingsInfo Info = new SettingsInfo();
                        SettingsSP sp = new SettingsSP();
                        //Info = sp.SettingsViewByBranchId1(PublicVariables._branchId);
                        //Info.AutomaticProductCodeGeneration = false;
                        //sp.SettingsEdit(Info);
                        txtProductCode.ReadOnly = false;
                        txtProductCode.Clear();
                    }
                }
                txtProductCode.ReadOnly = true;
            }
            else
            {
                if (strPrdDtlsIdToEdit == "")
                {
                    txtProductCode.ReadOnly = false;
                    txtProductCode.Clear();
                }
            }
        }
        public void FillRackAccordingToGodown(int inRowIndex, int inColIndex)
        {
            // To fill rack according to godown
            string godownId = "Primary Location";
            if (dgvDetails.Rows[inRowIndex].Cells["godown"].Value != null && dgvDetails.Rows[inRowIndex].Cells["godown"].Value.ToString() != "")
                godownId = dgvDetails.Rows[inRowIndex].Cells["godown"].Value.ToString();




            objTransactionsGeneralFill.FillRackComboForGridCellByPrdAndBranchandGodown(dgvDetails, txtProductCode.Text, PublicVariables._branchId, "rack", inRowIndex, godownId);

        }
        //--------------------------------Delete Product---------------------------------------------------------
        public void DeleteProduct()
        {
            try
            {
                if (!SPProduct.ProductDelete(strProductIdToEdit, true))
                {
                    MessageBox.Show("Cant delete , reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    DeleteImage();
                    spgeneral.UserActivityAdd("Product Creation", "Delete", strProductIdToEdit, txtProductCode.Text, DateTime.Now, decOldAmount, 0);//20-03-2024 sheena
                    MessageBox.Show("Deleted successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //if (objfrmProductRegister != null)
                    //{
                    //    this.Close();
                    //}
                    //else
                    //{
                    //    strPrdDtlsIdToEdit = "";
                    //    strProductIdToEdit = "";

                    //    Clear();


                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public void DeleteImage()
        {
            if (oldDestinationPath != "")
            {
                DirectoryInfo drinfo = new DirectoryInfo(Application.StartupPath + "\\ProductImage");
                if (!drinfo.Exists)
                {
                    drinfo.Create();
                }
                if (File.Exists(oldDestinationPath))
                {
                    try
                    {
                        File.Delete(oldDestinationPath);
                    }
                    catch
                    {
                    }
                }
            }
        }

        //frmSalesInvoice frmObjSales;
        int inProductRowIndex;
        bool isFromOther = false;
        //public void CallFromSalesInvoice(frmSalesInvoice frm, int inRowIndex)
        //{
        //    frmObjSales = frm;

        //    inProductRowIndex = inRowIndex;
        //    isFromOther = true;
        //    base.Show();
        //}
        //frmSalesReturn frmObjSalesReturn;
        //public void CallFromSalesReturn(frmSalesReturn frm, int inRowIndex)
        //{
        //    frmObjSalesReturn = frm;

        //    inProductRowIndex = inRowIndex;
        //    isFromOther = true;
        //    base.Show();
        //}
        //frmSalesOrder frmObjSalesOrder;
        //public void CallFromSalesOrder(frmSalesOrder frm, int inRowIndex)
        //{
        //    frmObjSalesOrder = frm;

        //    inProductRowIndex = inRowIndex;
        //    isFromOther = true;
        //    base.Show();
        //}
        //frmDeliveryNote frmObjDeliveryNote;
        //public void CallFromDeliveryNote(frmDeliveryNote frm, int inRowIndex)
        //{
        //    frmObjDeliveryNote = frm;

        //    inProductRowIndex = inRowIndex;
        //    isFromOther = true;
        //    base.Show();
        //}
        //frmSalesQuotation frmObjSalesQuot;
        //public void CallFromSalesQuotation(frmSalesQuotation frm, int inRowIndex)
        //{
        //    frmObjSalesQuot = frm;

        //    inProductRowIndex = inRowIndex;
        //    isFromOther = true;
        //    base.Show();
        //}
        ////---------------------------
        //frmPurchaseOrder frmObjpurchaseorder;
        //public void CallFromPurchaseOrder(frmPurchaseOrder frm, int inRowIndex)
        //{
        //    frmObjpurchaseorder = frm;

        //    inProductRowIndex = inRowIndex;
        //    isFromOther = true;
        //    base.Show();
        //}
        //frmMaterialReceipt frmObjmaterialreceipt;
        //public void CallFromMaterialReceipt(frmMaterialReceipt frm, int inRowIndex)
        //{
        //    frmObjmaterialreceipt = frm;

        //    inProductRowIndex = inRowIndex;
        //    isFromOther = true;
        //    base.Show();
        //}

        //frmPurchaseInvoice frmObjPurchase;
        //public void CallFromPurchaseInvoice(frmPurchaseInvoice frm, int inRowIndex)
        //{
        //    frmObjPurchase = frm;

        //    isFromOther = true;
        //    inProductRowIndex = inRowIndex;
        //    base.Show();
        //}
        //frmPurchaseInvoicePOS frmObjPurchasePOS;
        //public void CallFromPurchaseInvoicePOS(frmPurchaseInvoicePOS frm, int inRowIndex)
        //{
        //    frmObjPurchasePOS = frm;

        //    isFromOther = true;
        //    inProductRowIndex = inRowIndex;
        //    base.Show();
        //}
        //frmPurchaseReturn frmObjPurchaseReurn;
        //public void CallFromPurchaseReturn(frmPurchaseReturn frm, int inRowIndex)
        //{
        //    frmObjPurchaseReurn = frm;

        //    isFromOther = true;
        //    inProductRowIndex = inRowIndex;
        //    base.Show();
        //}
        //frmPOS frmObjPos;
        //public void CallFromPOS(frmPOS frm)
        //{
        //    frmObjPos = frm;
        //    isFromOther = true;

        //    base.Show();
        //}

        //frmTransfer objfrmTRansfer;
        //public void CallFromTransfer(frmTransfer frm, int inRowIndex)
        //{
        //    objfrmTRansfer = frm;

        //    isFromOther = true;
        //    inProductRowIndex = inRowIndex;
        //    base.Show();
        //}
        ////---------------------------------call from other form----------------------------------------------------
        //public void CallFromProductRegister(frmProductRegister frm, string strProductCode)
        //{

        //    objfrmProductRegister = frm;
        //    isFromOther = true;

        //    FillProductForEdit(strProductCode);

        //}
        //---------------------------------to fill tax combo-------------------------------------------------------
        public void FillTaxCombo()
        {
            try
            {
                cmbTax.DataSource = SPTaxMaster.TaxGetProductTaxWithNA();
                if (cmbTax.DataSource != null)
                {
                    cmbTax.DisplayMember = "taxName";
                    cmbTax.ValueMember = "taxId";
                    if (FinanceSettingsInfo._ActivateTax)
                    {

                        if (((DataTable)cmbTax.DataSource).Select("taxId ='" + 3 + "'").Length > 0)
                        {
                            cmbTax.SelectedValue = "3";
                        }
                        else if (((DataTable)cmbTax.DataSource).Select("taxId ='" + 2 + "'").Length > 0)
                        {
                            cmbTax.SelectedValue = "2";
                        }
                        else
                        {
                            if (((DataTable)cmbTax.DataSource).Select("taxId ='" + 1 + "'").Length > 0)
                            {
                                cmbTax.SelectedValue = "1";
                            }
                        }
                    }
                    else
                        cmbTax.SelectedValue = "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--------------------------Fill Brand  to brand combo--------------------------------------------------------------
        public void FillBrandCombo()
        {
            try
            {


                cmbBrand.DataSource = BrandSP.BrandViewAll();
                if (cmbBrand.DataSource != null)
                {
                    cmbBrand.DisplayMember = "brandName";
                    cmbBrand.ValueMember = "brandId";


                    if (((DataTable)cmbBrand.DataSource).Select("brandId ='" + 1 + "'").Length > 0)
                    {
                        cmbBrand.SelectedValue = "1";
                    }


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC6:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //------------------------------Fill Unit combo------------------------------------------------------------
        public void FillUnitCombo()
        {
            try
            {
                cmbUnit.DataSource = UnitSP.UnitViewAll();
                if (cmbUnit.DataSource != null)
                {
                    cmbUnit.DisplayMember = "unitName";
                    cmbUnit.ValueMember = "unitId";
                    cmbUnit.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC8:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DefaultUnitAdd()
        {
            try
            {
                dtblMultiUnits = new DataTable();
                DataColumn c1 = new DataColumn("qty");
                dtblMultiUnits.Columns.Add(c1);
                DataColumn c2 = new DataColumn("unitId");
                dtblMultiUnits.Columns.Add(c2);
                DataColumn c3 = new DataColumn("unitName");
                dtblMultiUnits.Columns.Add(c3);
                DataColumn c4 = new DataColumn("view");
                dtblMultiUnits.Columns.Add(c4);
                //DataColumn c5 = new DataColumn("barcode");////created by sheena on 10-05-2023
                //dtblMultiUnits.Columns.Add(c5);
                DataRow drow = dtblMultiUnits.NewRow();
                drow["qty"] = "1";
                drow["unitId"] = cmbUnit.SelectedValue.ToString();
                drow["unitName"] = cmbUnit.Text;
                // drow["barcode"] = "";////created by sheena on 10-05-2023
                dtblMultiUnits.Rows.InsertAt(drow, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC9:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //------------------------------
        public decimal ConversionRate(string strUnitId)
        {
            decimal dCRate = 1;
            if (dtblMultiUnits.Rows.Count > 0)
            {
                DataRow[] drArr = dtblMultiUnits.Select("unitId ='" + strUnitId + "'");
                if (drArr.Length > 0)
                {
                    DataSet dsCurr = new DataSet();
                    dsCurr.Merge(drArr);
                    dCRate = decimal.Parse(dsCurr.Tables[0].Rows[0].ItemArray[0].ToString());
                }
            }
            return dCRate;
        }
        //------------------------------fill units to grid combo column------------------------------------------------------

        public void FillUnitToGridComboColumn()
        {
            try
            {
                if (dtblMultiUnits.Rows.Count > 0)
                {
                    DataRow drow = dtblMultiUnits.NewRow();
                    drow["qty"] = "1";
                    drow["unitId"] = cmbUnit.SelectedValue.ToString();
                    drow["unitName"] = cmbUnit.Text;
                    //   drow["barcode"] = "";
                    dtblMultiUnits.Rows.InsertAt(drow, 0);

                }
                else
                {
                    if ((cmbUnit.SelectedValue == null ? "System.Data.DataRowView" : cmbUnit.SelectedValue.ToString()) != "System.Data.DataRowView")
                    {

                        DefaultUnitAdd();

                    }

                }

                foreach (DataGridViewRow dgvrowCur in dgvDetails.Rows)
                {
                    if (dtblMultiUnits.Rows.Count > 0)
                    {
                        if (dgvrowCur.Cells["unitPerQty"].Value != null)
                        {
                            if (dtblMultiUnits.Select("unitId ='" + dgvrowCur.Cells["unitPerQty"].Value.ToString() + "'").Length == 0)
                            {
                                dgvrowCur.Cells["unitPerQty"].Value = null;
                            }
                        }
                        if (dgvrowCur.Cells["unitPerRate"].Value != null)
                        {
                            if (dtblMultiUnits.Select("unitId ='" + dgvrowCur.Cells["unitPerRate"].Value.ToString() + "'").Length == 0)
                            {
                                dgvrowCur.Cells["unitPerRate"].Value = null;
                            }
                        }


                    }
                    else
                    {
                        dgvrowCur.Cells["unitPerQty"].Value = null;
                        dgvrowCur.Cells["unitPerRate"].Value = null;

                    }

                }
                if (dtblMultiUnits.Rows.Count > 0)
                {
                    unitPerQty.DataSource = dtblMultiUnits;
                    unitPerRate.DataSource = dtblMultiUnits;
                    unitPerQty.DisplayMember = "unitName";
                    unitPerQty.ValueMember = "unitId";
                    unitPerRate.DisplayMember = "unitName";
                    unitPerRate.ValueMember = "unitId";
                }
                else
                {
                    unitPerQty.DataSource = null;
                    unitPerRate.DataSource = null;
                }
                if ((cmbUnit.SelectedValue == null ? "System.Data.DataRowView" : cmbUnit.SelectedValue.ToString()) != "System.Data.DataRowView")
                {
                    if (cmbUnit.SelectedValue.ToString() != "")
                    {
                        foreach (DataGridViewRow dgvrowCur in dgvDetails.Rows)
                        {
                            if (dgvrowCur.IsNewRow)
                            {
                                isToNotDo = true;
                                if (dgvrowCur.Cells["godown"].Value == null || dgvDetails.Columns["godown"].Visible == false)
                                {
                                    dgvrowCur.Cells["check"].Value = "";
                                }
                                dgvrowCur.Cells["unitPerQty"].Value = cmbUnit.SelectedValue.ToString();
                                dgvrowCur.Cells["unitPerRate"].Value = cmbUnit.SelectedValue.ToString();
                                isToNotDo = false;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC10:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //-------------------------------mfd and expd setting according  to row change---------------------------------
        public void MFDandEXPDSettings(int inRowIn)
        {
            string strCur = dgvDetails.Rows[inRowIn].Cells["batch"].Value == null ? "" : dgvDetails.Rows[inRowIn].Cells["batch"].Value.ToString();
            string strCurMFD = dgvDetails.Rows[inRowIn].Cells["mfd"].Value == null ? "" : dgvDetails.Rows[inRowIn].Cells["mfd"].Value.ToString();
            string strCurEXPD = dgvDetails.Rows[inRowIn].Cells["expd"].Value == null ? "" : dgvDetails.Rows[inRowIn].Cells["expd"].Value.ToString();
            if (strCur != "" && strCur != "NA")
            {
                foreach (DataGridViewRow dgvrowObj in dgvDetails.Rows)
                {
                    if (dgvrowObj.Index != inRowIn)
                    {
                        if (strCur == (dgvrowObj.Cells["batch"].Value == null ? "" : dgvrowObj.Cells["batch"].Value.ToString()))
                        {
                            if (strCurMFD == "")
                            {
                                if ((dgvrowObj.Cells["mfd"].Value == null ? "" : dgvrowObj.Cells["mfd"].Value.ToString()) != "")
                                {
                                    dgvDetails.Rows[inRowIn].Cells["mfd"].Value = dgvrowObj.Cells["mfd"].Value.ToString();
                                }
                            }
                            else
                            {
                                dgvrowObj.Cells["mfd"].Value = strCurMFD;
                            }
                            if (strCurEXPD == "")
                            {
                                if ((dgvrowObj.Cells["expd"].Value == null ? "" : dgvrowObj.Cells["expd"].Value.ToString()) != "")
                                {
                                    dgvDetails.Rows[inRowIn].Cells["expd"].Value = dgvrowObj.Cells["expd"].Value.ToString();
                                }
                            }
                            else
                            {
                                dgvrowObj.Cells["expd"].Value = strCurEXPD;
                            }
                        }
                    }
                }
            }
        }
        bool isToNotDo = false;
        //------------------------------is ok row ckecking-------------------------------------------------------------
        public void OkRowChecking(DataGridViewCellEventArgs e)
        {
            strUnitPerQtyId = ""; strUnitPerRateId = "";
            dQty = 0; dRate = 0;

            string strGodownId = "", strBatch = "";
            DateTime dtimeExpd, dtimeMfd;
            decimal dAmt = 0;
            int inCou = 0;
            if (!isToNotDo)
            {
                try
                {

                    if (InventorySettingsInfo._maintainGodown)
                    {
                        strGodownId = dgvDetails.Rows[e.RowIndex].Cells["godown"].Value.ToString();
                    }
                    inCou++;
                }
                catch { }
                try
                {

                    if (SettingsInfo._allowRack)
                    {
                        dgvDetails.Rows[e.RowIndex].Cells["rack"].Value.ToString();
                    }
                    inCou++;
                }
                catch { }
                try
                {
                    if (cmbAllowBatch.Text == "Yes")
                    {
                        strBatch = dgvDetails.Rows[e.RowIndex].Cells["batch"].Value.ToString();
                    }
                    inCou++;
                }
                catch { }
                try
                {
                    if (cmbAllowBatch.Text == "Yes")
                    {
                        dtimeMfd = DateTime.Parse(dgvDetails.Rows[e.RowIndex].Cells["mfd"].Value.ToString());
                    }
                    inCou++;
                }
                catch { dgvDetails.Rows[e.RowIndex].Cells["mfd"].Value = ""; }
                try
                {
                    if (cmbAllowBatch.Text == "Yes")
                    {
                        dtimeExpd = DateTime.Parse(dgvDetails.Rows[e.RowIndex].Cells["expd"].Value.ToString());
                    }
                    inCou++;
                }
                catch { dgvDetails.Rows[e.RowIndex].Cells["expd"].Value = ""; }
                try
                {
                    dQty = decimal.Parse(dgvDetails.Rows[e.RowIndex].Cells["qty"].Value.ToString());
                    if (dQty != 0m)
                    {
                        inCou++;
                    }
                }
                catch { dgvDetails.Rows[e.RowIndex].Cells["qty"].Value = ""; }

                try
                {
                    strUnitPerQtyId = dgvDetails.Rows[e.RowIndex].Cells["unitPerQty"].Value.ToString();
                    inCou++;
                }
                catch { }
                try
                {
                    strUnitPerRateId = dgvDetails.Rows[e.RowIndex].Cells["unitPerRate"].Value.ToString();
                    inCou++;

                }
                catch { }
                try
                {
                    dRate = decimal.Parse(dgvDetails.Rows[e.RowIndex].Cells["rate"].Value.ToString());
                    if (dRate != 0m)
                    {
                        inCou++;
                    }
                }
                catch { dgvDetails.Rows[e.RowIndex].Cells["rate"].Value = ""; }
                try
                {
                    dAmt = decimal.Parse(dgvDetails.Rows[e.RowIndex].Cells["amount"].Value.ToString());

                }
                catch { dgvDetails.Rows[e.RowIndex].Cells["amount"].Value = ""; }
                if (!isRemove)
                {
                    if ((inCou == 7 && strBatch == "NA") || (inCou == 9) || (inCou == 0 && dgvDetails.Rows[e.RowIndex].IsNewRow))
                    {

                        dgvDetails.Rows[e.RowIndex].Cells["check"].Value = "";
                    }
                    else
                    {

                        dgvDetails.Rows[e.RowIndex].Cells["check"].Value = "x";
                        dgvDetails["check", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }
        //------------------------------to drop down combobox while clicking space-------------------------------------
        public void DropDownCombo(KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC11:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //------------------------function to return to product creation after creating product group,unit or brand------------------------------
        public void DowhenReturningFromProductGroupForm(string strId, string category)
        {
            try
            {
                this.Enabled = true;
                if (category == "Category 1")
                {
                    FillGroupCategory1();
                    if (strId != "")
                    {
                        // Assign newly created id
                        cmbGroupCat1.SelectedValue = strId;
                    }
                    else if (strOldGrpId != "")
                    {
                        // Assign old id as new one is not created
                        cmbGroupCat1.SelectedValue = strOldGrpId;
                    }
                    cmbGroupCat1.Focus();
                }
                else if (category == "Category 2")
                {
                    FillGroupCategory2();
                    if (strId != "")
                    {
                        // Assign newly created id
                        cmbGroupCat2.SelectedValue = strId;
                    }
                    else if (strOldGrpId != "")
                    {
                        // Assign old id as new one is not created
                        cmbGroupCat2.SelectedValue = strOldGrpId;
                    }
                    cmbGroupCat2.Focus();
                }
                else if (category == "Category 3")
                {
                    FillGroupCategory3();
                    if (strId != "")
                    {
                        // Assign newly created id
                        cmbGroupCat3.SelectedValue = strId;
                    }
                    else if (strOldGrpId != "")
                    {
                        // Assign old id as new one is not created
                        cmbGroupCat3.SelectedValue = strOldGrpId;
                    }
                    cmbGroupCat3.Focus();
                }
                else if (category == "Category 4")
                {
                    FillProductGroupCombo();
                    if (strId != "")
                    {
                        // Assign newly created id
                        cmbGroup.SelectedValue = strId;
                    }
                    else if (strOldGrpId != "")
                    {
                        // Assign old id as new one is not created
                        cmbGroup.SelectedValue = strOldGrpId;
                    }
                    cmbGroup.Focus();
                }


                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void DowhenReturningFromMasterCreationForm(string strId, string strTo)
        {
            try
            {

                this.Enabled = true;

                if (strTo == "Brand")
                {
                    FillBrandCombo();
                    if (strId != "")
                    {
                        // Assign newly created id
                        cmbBrand.SelectedValue = strId;
                    }
                    else if (strOldBrandId != "")
                    {
                        // Assign old id as new one is not created
                        cmbBrand.SelectedValue = strOldBrandId;
                    }
                    cmbBrand.Focus();
                }
                else
                {
                    FillUnitCombo();
                    if (strId != "")
                    {
                        // Assign newly created id
                        cmbUnit.SelectedValue = strId;
                    }
                    else if (strOldUnitId != "")
                    {
                        // Assign old id as new one is not created
                        cmbUnit.SelectedValue = strOldUnitId;
                    }
                    cmbUnit.Focus();

                }
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC14:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--------------------------------------decimal field key press---------------------------------------------------------------------
        public void DecimalFieldKeypress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txtObj = (TextBox)sender;

                if ((e.KeyChar > 47 && e.KeyChar < 58) || (e.KeyChar == 8))
                {
                    e.Handled = false;
                }
                else if (e.KeyChar == 46)
                {
                    if (txtObj.Text.Contains("."))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        if (txtObj.Text == "")
                        {
                            e.Handled = true;
                            txtObj.Clear();
                            txtObj.Text = "0.";
                            txtObj.SelectionStart = txtObj.Text.Length;

                        }
                        else
                        {
                            e.Handled = true;
                            txtObj.Text = txtObj.Text + ".";
                            txtObj.SelectionStart = txtObj.Text.Length;
                        }
                    }


                }
                else
                {
                    e.Handled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //-----------------------function to return to product creation after multiple unit settings----------------------------------------
        public void DowhenReturningFromMultipleUnitsForm(DataTable dtblUnits)
        {
            try
            {
                this.Enabled = true;
                dtblMultiUnits = dtblUnits;
                FillUnitToGridComboColumn();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //-----------------------function to return to product creation after bom settings----------------------------------------
        public void DowhenReturningFromBOMForm(DataTable dtblRow)
        {
            try
            {
                this.Enabled = true;
                dtblRowMaterials = dtblRow;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC17:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //-----------------------function to return to product creation after package  settings----------------------------------------
        public void DowhenReturningFromPackageForm(DataTable dtblPack)
        {
            try
            {
                this.Enabled = true;
                dtblPackage = dtblPack;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC18:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //--------------------------------------clear form----------------------------------------------------------------------------------

        bool isClear = false;
        public void Clear()
        {
            try
            {
                isGridFill = false;
                strPrvUnitId = "";
                StPostingDate = "";
                cmbTax.Enabled = true;
                txtProductCode.ReadOnly = false;
                InitialSettingsForSave();
                CommonInitialSettings();

                dgvDetails.Rows.Clear();
                txtProductName.Clear();
                txtProductNameArabic.Clear();
                txtPartNo.Clear();
                txtMaximumStock.Text = "0";
                txtMinimumStock.Text = "0";
                txtMRP.Text = "0";
                txtNarration.Clear();
                txtPurchaseRate.Text = "0";
                txtReorderLevel.Text = "0";
                txtfixedSalesRate.Text = "0.00";
                cbxActive.Checked = true;

                cbxReminder.Checked = false;
                chkIsVanSale.Checked = false;

                dtblMultiUnits = new DataTable();
                dtblMultiUnitsNew = new DataTable();
                dtblsalesprice = new DataTable();
                dtblPackage = new DataTable();
                dtblRowMaterials = new DataTable();
                FinacMessage.SaveButtonText(btnSave, "New");
                FinacMessage.ClearButtonText(btnClear, "New"); 
                btnDelete.Enabled = false;
                btnPrint.Enabled = false;
                cbxActive.Enabled = true;
                cmbUnit.Enabled = true;
                btnNew.Enabled = true;
                unitPerQty.DataSource = null;
                unitPerRate.DataSource = null;
                cmbBom.Enabled = true;
                cmbCategory.Enabled = true;////created by sheena on 04-05-2023
                //GenerateProductCode();//commented by sheena on 09-05-2023
                // generateNewProductCode();
                // txtProductName.Focus();//by sheena on 15-june-2023
                txtProductCode.Focus();
                cmbCategory.SelectedIndex = 0;////created by sheena on 04-05-2023
                txtProductCode.Clear();
                dgvSalesPrice.Rows.Clear();
                dgvUnits.Rows.Clear();
                grpHierarchy.Visible = false;
                decOldAmount = 0;

                cmbBom.Text = "";
                DgvBom.Rows.Clear();
                DgvBom.Enabled = false;
                lnklblBomRemove.Enabled = false;
                dgvProductPurchasePrice.Rows.Clear();
                txtPercentage.Text = "0";
                isSalesValueChange = true;
                isFromCostPricePer = false;
                txtIngredients.Text = "";
                chkNutritionFact.Checked = false;
                txtName.Text = "";
                dgvNutritionFacts.Rows.Clear();
                txtLocation.Text = "";
                txtAlternateNo.Text = "";
                txtSalesPrice.Text = "0";

                TCProduct.SelectedTab = TCProduct.TabPages["TPProduct"];
                generateNewProductCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC19:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        //-----------------------------------check ok before save and edit------------------------------------------------------------------
        public bool EverythingOk()
        {
            bool isOk = true;
            try
            {
                if (txtProductName.Text.Trim() == "")
                {
                    isOk = false;
                    txtProductName.Focus();
                    MessageBox.Show("Enter product name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else if (txtProductCode.Text.Trim() == "")
                {
                    isOk = false;
                    txtProductCode.Focus();
                    MessageBox.Show("Enter product code", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else if (txtProductCode.Text.Length < 3)
                //{
                //    isOk = false;
                //    txtProductCode.Focus();
                //    MessageBox.Show("Product code should contain at least three characters ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                else if (CheckInvalidEntriesInProductCode(txtProductCode) != "")
                {
                    isOk = false;
                    txtProductCode.Focus();
                    txtProductCode.SelectAll();
                    MessageBox.Show("' " + CheckInvalidEntriesInProductCode(txtProductCode) + " ' Not allowed in product code", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbProductMainGrp.Text.Trim() == "")//created by sheena on 05-05-2023
                {
                    isOk = false;
                    cmbProductMainGrp.Focus();
                    MessageBox.Show("Select main group", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbGroup.Text.Trim() == "")
                {
                    isOk = false;
                    cmbGroup.Focus();
                    MessageBox.Show("Select product group", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cmbUnit.Text.Trim() == "")
                {
                    isOk = false;
                    cmbUnit.Focus();
                    MessageBox.Show("Select default unit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (txtSalesPrice.Text.Trim() == "")
                {
                    isOk = false;
                    txtSalesPrice.Focus();
                    MessageBox.Show("Please Enter Sales price", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //else if (txtfixedSalesRate.Text.Trim() == "0")
                //{
                //    isOk = false;
                //    txtfixedSalesRate.Focus();
                //    MessageBox.Show("Enter Sales Rate", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                else if (txtMaximumStock.Text != "" && txtMinimumStock.Text != "")
                {
                    if (decimal.Parse(txtMinimumStock.Text) > decimal.Parse(txtMaximumStock.Text))
                    {
                        isOk = false;
                        txtMinimumStock.Focus();
                        MessageBox.Show("Minimum stock should not be greater than maximum stock", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }

                if (cmbOpeningStock.Text == "Yes" && isOk)
                {
                    int inCk = 0;
                    foreach (DataGridViewRow dgvrowCurChk in dgvDetails.Rows)
                    {
                        if ((dgvrowCurChk.Cells["check"].Value == null ? "" : dgvrowCurChk.Cells["check"].Value.ToString()) != "x")
                        {
                            if (!dgvrowCurChk.IsNewRow)
                            {

                                inCk++;
                            }
                        }
                    }
                    if (inCk > 0)
                    {
                        string strMessage = "Rows ";
                        //checking for atleast one row contain completed entry
                        int inC = 0, inForFirst = 0;
                        foreach (DataGridViewRow dgvrowCur in dgvDetails.Rows)
                        {
                            if ((dgvrowCur.Cells["check"].Value == null ? "" : dgvrowCur.Cells["check"].Value.ToString()) == "x")
                            {
                                isOk = false;
                                if (inC == 0)
                                {
                                    strMessage = strMessage + Convert.ToString(dgvrowCur.Index + 1);
                                    inForFirst = dgvrowCur.Index;
                                    inC++;
                                }
                                else
                                {
                                    strMessage = strMessage + ", " + Convert.ToString(dgvrowCur.Index + 1);
                                }
                            }

                        }

                        //if alteast one row contain completed entry check for continue with incompleted rows
                        if (!isOk)
                        {
                            strMessage = strMessage + " contains invalid entries. Do you want to continue?";
                            if (MessageBox.Show(strMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                //if user decide to continue delete incomplete row
                                isOk = true;
                                for (int inK = 0; inK < dgvDetails.Rows.Count; inK++)
                                {
                                    if ((dgvDetails.Rows[inK].Cells["check"].Value == null ? "" : dgvDetails.Rows[inK].Cells["check"].Value.ToString()) == "x")
                                    {
                                        if (!dgvDetails.Rows[inK].IsNewRow)
                                        {
                                            if ((dgvDetails.Rows[inK].Cells["id"].Value == null ? "" : dgvDetails.Rows[inK].Cells["id"].Value.ToString()) != "")
                                            {
                                                strArrOfRemove[inArrOfRemoveIndex] = dgvDetails.Rows[inK].Cells["id"].Value.ToString();
                                                inArrOfRemoveIndex++;
                                            }
                                            dgvDetails.Rows.RemoveAt(inK);

                                        }
                                        else
                                        {
                                            //clearing only that fields which may contain value even the row is new row 
                                            dgvDetails.Rows[inK].Cells["qty"].Value = "";
                                            dgvDetails.Rows[inK].Cells["rate"].Value = "";
                                            DataGridViewComboBoxCell dgvccgdwn = (DataGridViewComboBoxCell)dgvDetails[dgvDetails.Columns["godown"].Index, inK];

                                            dgvDetails[dgvDetails.Columns["godown"].Index, inK].Value = null;
                                            dgvccgdwn.DataSource = null;

                                            DataGridViewComboBoxCell dgvccRck = (DataGridViewComboBoxCell)dgvDetails[dgvDetails.Columns["rack"].Index, inK];

                                            dgvDetails[dgvDetails.Columns["rack"].Index, inK].Value = null;
                                            dgvccRck.DataSource = null;
                                            //--------------------------------
                                            DataGridViewComboBoxCell dgvccQty = (DataGridViewComboBoxCell)dgvDetails[dgvDetails.Columns["unitPerQty"].Index, inK];
                                            DataGridViewComboBoxCell dgvccRate = (DataGridViewComboBoxCell)dgvDetails[dgvDetails.Columns["unitPerRate"].Index, inK];
                                            dgvDetails[dgvDetails.Columns["unitPerQty"].Index, inK].Value = null;
                                            dgvDetails[dgvDetails.Columns["unitPerRate"].Index, inK].Value = null;
                                            dgvccQty.DataSource = null;
                                            dgvccRate.DataSource = null;
                                            dgvDetails.Rows[inK].Cells["amount"].Value = "";
                                            isRemove = true;
                                            dgvDetails.Rows[inK].Cells["check"].Value = "";
                                            isRemove = false;
                                        }
                                        inK--;
                                    }
                                }

                            }
                            else
                            {
                                //to set focus to incomplete cell

                                dgvDetails.Focus();
                                if ((dgvDetails.Rows[inForFirst].Cells["godown"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["godown"].Value.ToString()) == "" && dgvDetails.Columns["godown"].Visible)
                                    dgvDetails.Rows[inForFirst].Cells["godown"].Selected = true;

                                else if ((dgvDetails.Rows[inForFirst].Cells["rack"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["rack"].Value.ToString()) == "" && dgvDetails.Columns["rack"].Visible)
                                    dgvDetails.Rows[inForFirst].Cells["rack"].Selected = true;

                                else if ((dgvDetails.Rows[inForFirst].Cells["batch"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["batch"].Value.ToString()) == "" && cmbAllowBatch.Text == "Yes")
                                    dgvDetails.Rows[inForFirst].Cells["batch"].Selected = true;


                                else if ((dgvDetails.Rows[inForFirst].Cells["mfd"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["mfd"].Value.ToString()) == "" && cmbAllowBatch.Text == "Yes" && !dgvDetails.Rows[inForFirst].Cells["mfd"].ReadOnly)
                                    dgvDetails.Rows[inForFirst].Cells["mfd"].Selected = true;

                                else if ((dgvDetails.Rows[inForFirst].Cells["expd"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["expd"].Value.ToString()) == "" && cmbAllowBatch.Text == "Yes" && !dgvDetails.Rows[inForFirst].Cells["expd"].ReadOnly)
                                    dgvDetails.Rows[inForFirst].Cells["expd"].Selected = true;

                                else if ((dgvDetails.Rows[inForFirst].Cells["qty"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["qty"].Value.ToString()) == "")
                                    dgvDetails.Rows[inForFirst].Cells["qty"].Selected = true;

                                else if (decimal.Parse(dgvDetails.Rows[inForFirst].Cells["qty"].Value.ToString()) == 0m)
                                    dgvDetails.Rows[inForFirst].Cells["qty"].Selected = true;

                                else if ((dgvDetails.Rows[inForFirst].Cells["unitPerQty"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["unitPerQty"].Value.ToString()) == "")
                                    dgvDetails.Rows[inForFirst].Cells["unitPerQty"].Selected = true;

                                else if ((dgvDetails.Rows[inForFirst].Cells["rate"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["rate"].Value.ToString()) == "")
                                    dgvDetails.Rows[inForFirst].Cells["rate"].Selected = true;

                                else if (decimal.Parse(dgvDetails.Rows[inForFirst].Cells["rate"].Value.ToString()) == 0m)
                                    dgvDetails.Rows[inForFirst].Cells["rate"].Selected = true;

                                else if ((dgvDetails.Rows[inForFirst].Cells["unitPerRate"].Value == null ? "" : dgvDetails.Rows[inForFirst].Cells["rate"].Value.ToString()) == "")
                                    dgvDetails.Rows[inForFirst].Cells["unitPerRate"].Selected = true;

                            }


                        }

                    }
                    else
                    {
                        MessageBox.Show("Can't save product without atleast one opening stock  entry with complete details", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isOk = false;
                        dgvDetails.Focus();
                        if (dgvDetails.Columns["godown"].Visible)
                            dgvDetails.Rows[0].Cells["godown"].Selected = true;
                        else if (dgvDetails.Columns["rack"].Visible)
                            dgvDetails.Rows[0].Cells["rack"].Selected = true;
                        else if (dgvDetails.Columns["batch"].Visible)
                            dgvDetails.Rows[0].Cells["batch"].Selected = true;
                        else dgvDetails.Rows[0].Cells["qty"].Selected = true;
                    }
                }

            }



            catch (Exception ex)
            {
                isOk = false;
                MessageBox.Show("PC20:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            return isOk;
        }
        //--------------------------------Remove Row--------------------------------------------------------------------
        bool isRemove = false;
        public void RemoveRow()
        {

            int inColIndex = dgvDetails.CurrentCell.ColumnIndex;
            if (!dgvDetails.Rows[dgvDetails.CurrentRow.Index].IsNewRow)
            {
                if ((dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["id"].Value == null ? "" : dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["id"].Value.ToString()) != "")
                {
                    strArrOfRemove[inArrOfRemoveIndex] = dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["id"].Value.ToString();
                    inArrOfRemoveIndex++;
                }
                dgvDetails.Rows.RemoveAt(dgvDetails.CurrentRow.Index);
                dgvDetails.CurrentCell = dgvDetails.CurrentRow.Cells[inColIndex];
                dgvDetails.Focus();



            }
            else
            {

                dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["batch"].Value = "";
                dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["expd"].Value = "";
                dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["mfd"].Value = "";
                dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["rate"].Value = "";
                dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["qty"].Value = "";

                dgvDetails[dgvDetails.Columns["godown"].Index, dgvDetails.CurrentRow.Index].Value = null;
                dgvDetails[dgvDetails.Columns["rack"].Index, dgvDetails.CurrentRow.Index].Value = null;

                dgvDetails[dgvDetails.Columns["unitPerQty"].Index, dgvDetails.CurrentRow.Index].Value = null;
                dgvDetails[dgvDetails.Columns["unitPerRate"].Index, dgvDetails.CurrentRow.Index].Value = null;
                if ((cmbUnit.SelectedValue == null ? "System.Data.DataRowView" : cmbUnit.SelectedValue.ToString()) != "System.Data.DataRowView")
                {
                    if (cmbUnit.SelectedValue.ToString() != "")
                    {
                        isToNotDo = true;
                        dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["unitPerQty"].Value = cmbUnit.SelectedValue.ToString();
                        dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["unitPerRate"].Value = cmbUnit.SelectedValue.ToString();
                        isToNotDo = false;

                    }
                }

                dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["amount"].Value = "";
                isRemove = true;
                dgvDetails.Rows[dgvDetails.CurrentRow.Index].Cells["check"].Value = "";
                isRemove = false;
                dgvDetails.CurrentCell = dgvDetails.CurrentRow.Cells[inColIndex];
                dgvDetails.Focus();
            }
        }
        public void ArrageSalesPriceGrid()
        {
           
            ProductSalesPriceSP productSalesPriceSP = new ProductSalesPriceSP();
            try
            {              
                for (int i = 0; i < dtblsalesprice.Rows.Count; i++)
                {
                    bool isDeleted = true;
                    foreach (DataGridViewRow dgvrowObj in dgvSalesPrice.Rows)
                    {
                        if (!dgvrowObj.IsNewRow)
                        {
                            if (dgvrowObj.Cells["salespriceId"].Value != null)
                            {
                                if (dtblsalesprice.Rows[i]["salespriceId"].ToString() == dgvrowObj.Cells["salespriceId"].Value.ToString())
                                {
                                    isDeleted = false;
                                    if (dgvrowObj.Cells["status"].Value.ToString() == "IsFromDb")
                                    {
                                        ////update                                    
                                        break;
                                    }
                                }
                            }
                            else
                                isDeleted = true;
                        }
                    }
                    if(isDeleted)
                    {
                        //Delete
                        productSalesPriceSP.ProductSalesPriceDelete(dtblsalesprice.Rows[i]["salespriceId"].ToString());

                        ProductSalesPriceHistorySP productSalesPriceHistorySP = new ProductSalesPriceHistorySP();
                        ProductSalesPriceInfo productSalesPriceInfo = new ProductSalesPriceInfo();
                        productSalesPriceInfo.ProductCode = txtProductCode.Text;
                        productSalesPriceInfo.UnitId = dtblsalesprice.Rows[i]["UnitId"].ToString();
                        try
                        {
                            productSalesPriceInfo.PricingLevelId = dtblsalesprice.Rows[i]["pricingLevelId"].ToString();
                        }
                        catch { productSalesPriceInfo.PricingLevelId = "1"; }
                        productSalesPriceInfo.SalespriceId = dtblsalesprice.Rows[i]["salespriceId"].ToString();
                        productSalesPriceInfo.Amount = Decimal.Parse(dtblsalesprice.Rows[i]["amount"].ToString());
                        productSalesPriceInfo.DiscPercentage = Decimal.Parse(dtblsalesprice.Rows[i]["DiscPercentage"].ToString());
                        productSalesPriceInfo.DiscAmount = Decimal.Parse(dtblsalesprice.Rows[i]["DiscAmount"].ToString());
                        productSalesPriceInfo.SalesPrice = Decimal.Parse(dtblsalesprice.Rows[i]["SalesPrice"].ToString());
                        productSalesPriceInfo.UserId = PublicVariables._currentUserId;
                        productSalesPriceInfo.BranchId = PublicVariables._branchId;
                        productSalesPriceInfo.Extra1 = "";
                        productSalesPriceInfo.Extra2 = "";                        

                        productSalesPriceInfo.oldSalesPrice = decimal.Parse( dtblsalesprice.Rows[i]["SalesPrice"].ToString());
                        productSalesPriceInfo.oldSalesUnit = dtblsalesprice.Rows[i]["UnitId"].ToString();
                        productSalesPriceInfo.OperationType = 'D';
                        productSalesPriceInfo.CostPrice = Decimal.Parse(dtblsalesprice.Rows[i]["costPrice"].ToString());
                        productSalesPriceInfo.MarginPercentage = Decimal.Parse(dtblsalesprice.Rows[i]["marginPercentage"].ToString());
                        productSalesPriceHistorySP.ProductSalesPriceHistoryAdd(productSalesPriceInfo);
                    }
                }
            }
            catch (Exception ex) { }

        }
        public void ArrageBarcodeGrid()
        {          
            try
            {
                for (int i = 0; i < dtblMultiUnitsNew.Rows.Count; i++)
                {
                    bool isDeleted = true;
                    foreach (DataGridViewRow dgvrowObj in dgvUnits.Rows)
                    {
                        if (!dgvrowObj.IsNewRow)
                        {
                            if (dgvrowObj.Cells["unitConversionId"].Value != null)
                            {
                                if (dtblMultiUnitsNew.Rows[i]["unitConversionId"].ToString() == dgvrowObj.Cells["unitConversionId"].Value.ToString())
                                {
                                    isDeleted = false;
                                    if (dgvrowObj.Cells["unitStatus"].Value.ToString() == "IsFromDb")
                                    {
                                        ////update                                    
                                        break;
                                    }
                                }
                            }
                            else
                                isDeleted = true;
                        }
                    }
                    if (isDeleted)
                    {
                       
                        //Delete
                        SPUnitConversion.UnitConversionDelete(dtblMultiUnitsNew.Rows[i]["unitConversionId"].ToString());

                        UnitConversionInfo InfoUnitConversion = new UnitConversionInfo();
                        InfoUnitConversion.ProductCode = txtProductCode.Text;
                        InfoUnitConversion.UnitConversionId = dtblMultiUnitsNew.Rows[i]["unitConversionId"].ToString();
                        InfoUnitConversion.Extra1 = dtblMultiUnitsNew.Rows[i]["qty"].ToString();//defaultQty
                        InfoUnitConversion.ConversionRate = float.Parse(dtblMultiUnitsNew.Rows[i]["qty"].ToString());//defaultQty);
                        InfoUnitConversion.UnitId = dtblMultiUnitsNew.Rows[i]["unitId"].ToString();
                        InfoUnitConversion.Extra2 = "";
                        try
                        {
                            InfoUnitConversion.Barcode = dtblMultiUnitsNew.Rows[i]["barcode"].ToString();
                        }
                        catch
                        {
                            InfoUnitConversion.Barcode = "";
                        }
                        InfoUnitConversion.oldBarcode = dtblMultiUnitsNew.Rows[i]["barcode"].ToString();
                        InfoUnitConversion.OldBarcodeUnit = dtblMultiUnitsNew.Rows[i]["unitId"].ToString();
                        InfoUnitConversion.OperationType = 'D';
                        SPUnitConversionHistory.UnitConversionHistoryAdd(InfoUnitConversion);
                    }
                }
            }
            catch (Exception ex) { }

        }
        //----------------------------------Save or Edit-------------------------------------------------------------------------------------
       
       private async Task<bool> SaveOrEdit()
        {
           
            try
            {
                generateNewProductCode();
                //save

                //---------------------to product table --------------------------------------------------------- 

                ProductInfo InfoProduct = new ProductInfo();
                ProductMainGroupInfo maininfo = new ProductMainGroupInfo();
                InfoProduct.BrandId = (cmbBrand.SelectedValue == null ? "1" : cmbBrand.SelectedValue.ToString());
                InfoProduct.Extra1 = txtProductNameArabic.Text.Trim();  //"";
                InfoProduct.Extra2 = "";
                InfoProduct.GroupId = cmbGroup.SelectedValue.ToString();
                InfoProduct.MultipleUnit = (cmbMultipleUnit.Text == "Yes" ? true : false);
                InfoProduct.ProductCode = txtProductCode.Text.Trim();
                InfoProduct.ProductName = txtProductName.Text.Trim();
                InfoProduct.UnitId = cmbUnit.SelectedValue.ToString();
                InfoProduct.PartNo = txtPartNo.Text.Trim();
             
                try
                {
                    if (MDIFinacPOS.DBLocation == "Local")
                    {
                        InfoProduct.ProductImage = destinationPath;

                        SaveImage();
                    }
                    else if (MDIFinacPOS.DBLocation == "Cloud")
                    {
                        // Upload image to cloud
                        if (!string.IsNullOrWhiteSpace(destinationPath) && File.Exists(destinationPath))
                        {
                            string cloudUrl = await UploadImageToCloudAsync(destinationPath, txtProductCode.Text.Trim(), PublicVariables._companyDatabaseName);
                            InfoProduct.ProductImage = cloudUrl;
                        }
                        else
                        {
                            InfoProduct.ProductImage = ""; // or leave it null
                        }
                    }
                }
                catch { InfoProduct.ProductImage = ""; }

                string newProductCode = "";

                if (strProductIdToEdit == "")
                {
                    DataTable dtbl = new DataTable();
                    dtbl = SPProduct.ProductAdd(InfoProduct);
                    if (dtbl.Rows.Count > 0)
                        newProductCode = dtbl.Rows[0][0].ToString();
                    if (newProductCode != "")
                    {
                        InfoProduct.ProductCode = newProductCode;// product code may changed
                    }
                }

                if (strProductIdToEdit != "")
                {
                    newProductCode = strProductIdToEdit;
                    SPProduct.ProductEdit(InfoProduct);
//SPUnitConversion.UnitConversionDeleteByproductCode(strProductIdToEdit);
                    SPStockPosting.StPostingDeleteByVoucherTypeAndVoucherNumberAndBranch(strProductIdToEdit, "Opening Stock", PublicVariables._branchId);
                }
             
                //----------------------to unit conversion table----------------------------------------
                if (newProductCode == "")
                {
                    MessageBox.Show("Product code already exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProductCode.Focus();
                    return false;
                }
                else
                {
                    ArrageBarcodeGrid();
                    UnitConversionInfo InfoUnitConversion = new UnitConversionInfo();

                    InfoUnitConversion.Extra2 = "";
                    InfoUnitConversion.ProductCode = InfoProduct.ProductCode;
                    InfoUnitConversion.Extra1 = "";
                    InfoUnitConversion.ConversionRate = 1;
                    InfoUnitConversion.UnitId = cmbUnit.SelectedValue.ToString();
                    InfoUnitConversion.UserId = PublicVariables._currentUserId;
                    InfoUnitConversion.Barcode = "";
                    InfoUnitConversion.Description = "";
                    SPUnitConversion.UnitConversionAdd(InfoUnitConversion);
                    //-----------------------------------
                    //if (dgvUnits.Rows.Count > 0)
                    //{
                    //    foreach (DataGridViewRow dgvrowObj in dgvUnits.Rows)//created by sheena on 11-05-2023
                    //    {
                    //        string str1 = dgvrowObj.Cells["unit"].Value == null ? string.Empty : dgvrowObj.Cells["unit"].Value.ToString();
                    //        if (str1 != "")
                    //        {
                    //            InfoUnitConversion.Extra1 = dgvrowObj.Cells["defaultQty"].Value.ToString();//defaultQty
                    //            InfoUnitConversion.ConversionRate = float.Parse(dgvrowObj.Cells["defaultQty"].Value.ToString());//defaultQty);
                    //            InfoUnitConversion.UnitId = dgvrowObj.Cells["unit"].Value.ToString();
                    //            InfoUnitConversion.UserId = PublicVariables._currentUserId;
                    //            try
                    //            {
                    //                InfoUnitConversion.Barcode = dgvrowObj.Cells["barcode"].Value.ToString();
                    //            }
                    //            catch
                    //            {
                    //                InfoUnitConversion.Barcode = "";
                    //            }
                    //            try
                    //            {
                    //                InfoUnitConversion.Description = dgvrowObj.Cells["Description"].Value.ToString();
                    //            }
                    //            catch { InfoUnitConversion.Description = ""; }
                    //            if (dgvrowObj.Cells["unitStatus"].Value == null)
                    //            {
                    //                SPUnitConversion.UnitConversionAdd(InfoUnitConversion);
                    //            }
                    //            else if (dgvrowObj.Cells["unitStatus"].Value.ToString() == "IsFromDb")
                    //            {
                    //                InfoUnitConversion.UnitConversionId = dgvrowObj.Cells["unitConversionId"].Value.ToString();                                   
                    //                SPUnitConversion.UnitConversionEdit(InfoUnitConversion);

                    //                string oldBarcode = dgvrowObj.Cells["oldbarcode"].Value != null ? dgvrowObj.Cells["oldbarcode"].Value.ToString() : "";
                    //                string barcode = dgvrowObj.Cells["barcode"].Value != null ? dgvrowObj.Cells["barcode"].Value.ToString() : "";
                    //                string oldBarcodeUnit = dgvrowObj.Cells["oldBarcodeUnit"].Value != null ? dgvrowObj.Cells["oldBarcodeUnit"].Value.ToString() : "";
                    //                string unit = dgvrowObj.Cells["unit"].Value != null ? dgvrowObj.Cells["unit"].Value.ToString() : "";

                    //                if (oldBarcode != barcode || oldBarcodeUnit != unit)
                    //                {
                    //                    //update history table
                    //                    InfoUnitConversion.oldBarcode = dgvrowObj.Cells["oldBarcode"].Value.ToString();
                    //                    InfoUnitConversion.OldBarcodeUnit = dgvrowObj.Cells["oldBarcodeUnit"].Value.ToString(); 
                    //                    InfoUnitConversion.OperationType = 'U';
                    //                    SPUnitConversionHistory.UnitConversionHistoryAdd(InfoUnitConversion);
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    InfoUnitConversion.Extra1 = "";
                    //    InfoUnitConversion.ConversionRate = 1;
                    //    InfoUnitConversion.UnitId = cmbUnit.SelectedValue.ToString();
                    //    InfoUnitConversion.Barcode = "";
                    //    SPUnitConversion.UnitConversionAdd(InfoUnitConversion);
                    //}

                    //-------------------old code from datatable
                    //if (dtblMultiUnits.Rows.Count > 0)
                    //{
                    //    foreach (DataRow drowObj in dtblMultiUnits.Rows)
                    //    {
                    //        InfoUnitConversion.Extra1 = drowObj["view"].ToString();
                    //        InfoUnitConversion.ConversionRate = decimal.Parse(drowObj.ItemArray[0].ToString());
                    //        InfoUnitConversion.UnitId = drowObj.ItemArray[1].ToString();
                    //        SPUnitConversion.UnitConversionAdd(InfoUnitConversion);
                    //    }
                    //}
                    //else
                    //{
                    //    InfoUnitConversion.Extra1 = "";
                    //    InfoUnitConversion.ConversionRate = 1;
                    //    InfoUnitConversion.UnitId = cmbUnit.SelectedValue.ToString();
                    //    SPUnitConversion.UnitConversionAdd(InfoUnitConversion);
                    //}

                    //---------------------- to product details table---------------------------------------------
                    ProductDetailsInfo InfoProductDetails = new ProductDetailsInfo();
                    if (cbxGatePass.Visible)
                    {
                        InfoProductDetails.GatePass = cbxGatePass.Checked;
                    }

                    InfoProductDetails.Active = cbxActive.Checked;
                    InfoProductDetails.AllowBatch = (cmbAllowBatch.Text == "Yes" ? true : false);
                    InfoProductDetails.AllowComponentSale = false;
                    InfoProductDetails.Bom = (cmbBom.Text == "Yes" ? true : false);
                    InfoProductDetails.Extra1 = "";
                    InfoProductDetails.Extra2 = "";
                    InfoProductDetails.MaximunStock = decimal.Parse((txtMaximumStock.Text == "" ? "0" : txtMaximumStock.Text));
                    InfoProductDetails.MinimumStock = decimal.Parse((txtMinimumStock.Text == "" ? "0" : txtMinimumStock.Text));
                    InfoProductDetails.Mrp = decimal.Parse((txtMRP.Text == "" ? "0" : txtMRP.Text));
                    InfoProductDetails.FixedSalesRate = decimal.Parse((txtfixedSalesRate.Text == "" ? "0" : txtfixedSalesRate.Text));
                    InfoProductDetails.Narration = txtNarration.Text.Trim();
                    InfoProductDetails.OpeningStock = (cmbOpeningStock.Text == "Yes" ? 1 : 0);
                    InfoProductDetails.TaxId = cmbTax.SelectedValue.ToString();
                    InfoProductDetails.TaxType = cmbTaxType.Text;
                    InfoProductDetails.ProductCode = InfoProduct.ProductCode;
                    InfoProductDetails.PurchaseRate = decimal.Parse((txtPurchaseRate.Text == "" ? "0" : txtPurchaseRate.Text));
                    InfoProductDetails.ReorderLevel = decimal.Parse((txtReorderLevel.Text == "" ? "0" : txtReorderLevel.Text));
                    InfoProductDetails.ShowReminder = cbxReminder.Checked;
                    InfoProductDetails.SalesRate = 0;
                    InfoProductDetails.Category = cmbCategory.Text;////created by sheena on 04-05-2023, new field
                    InfoProductDetails.GroupCode = cmbProductMainGrp.SelectedValue.ToString();////created by sheena on 05-05-2023,new field
                    InfoProductDetails.IsVanSaleProduct = chkIsVanSale.Checked;////created by sheena on 30-05-2024,new field
                    InfoProductDetails.ShowExpiry = chkShowExpiry.Checked;
                    InfoProductDetails.ExpiryDays = int.Parse(txtExpiryDays.Text);
                    InfoProductDetails.PurchaseRatePer = decimal.Parse(txtPercentage.Text);

                    #region Nutrition Fact

                    DataTable dt = new DataTable();
                    dt.Columns.Add("NutName", typeof(string));
                    dt.Columns.Add("Value", typeof(string));

                    foreach (DataGridViewRow row in dgvNutritionFacts.Rows)
                    {
                        if (!row.IsNewRow) // Ignore the empty new row
                        {
                            DataRow dr = dt.NewRow();
                            dr["NutName"] = row.Cells["NutName"].Value ?? DBNull.Value;
                            dr["Value"] = row.Cells["Value"].Value ?? DBNull.Value;
                            dt.Rows.Add(dr);
                        }
                    }

                    string jsonData = JsonConvert.SerializeObject(dt);
                    InfoProductDetails.Ingredients=txtIngredients.Text;
                    InfoProductDetails.NutritionFact = chkNutritionFact.Checked;
                    InfoProductDetails.NutritionName = txtName.Text;
                    InfoProductDetails.NutritionDetails = jsonData;

                    #endregion

                    //29/Jul/2025
                    InfoProductDetails.Location = txtLocation.Text;
                    InfoProductDetails.alternateNo = txtAlternateNo.Text;
                    InfoProductDetails.WithOutBarcode = chkWithoutBarcode.Checked;

                    string strProductDetailsId = "";

                    if (strPrdDtlsIdToEdit == "")
                    {
                        DataTable dtblResutt = new DataTable();
                        dtblResutt = SPProductDetails.ProductDetailsAdd(InfoProductDetails);
                        if (dtblResutt.Rows.Count > 0)
                        {
                            if (bool.Parse(dtblResutt.Rows[0]["status"].ToString()))
                            {
                                strPrdDtlsIdToEdit = dtblResutt.Rows[0]["productDetailsId"].ToString();
                            }
                            else
                            {
                                strProductDetailsId = dtblResutt.Rows[0]["productDetailsId"].ToString();
                            }
                        }
                        //-----------function to update nextnumber to +1 in  productmain group table-----------------
                        SPProductDetails.ProductMainGroupNxtNmbrUpdate(cmbProductMainGrp.SelectedValue.ToString());////created by sheena on 09-05-2023
                    }
                    if (strPrdDtlsIdToEdit != "")
                    {
                        InfoProductDetails.ProductDetailsId = strPrdDtlsIdToEdit;
                        SPProductDetails.ProductDetailsEdit(InfoProductDetails);
                        strProductDetailsId = strPrdDtlsIdToEdit;
                        SPBOM.BOMDeleteByproductDetailsId(strPrdDtlsIdToEdit);
                        SPComponentsSettings.ComponentsSettingsDeleteByproductDetailsId(strPrdDtlsIdToEdit);
                    }

                    //----------------------------to BOM table-----------------------------------------------
                    if (InfoProductDetails.Bom)
                    {
                        foreach (DataGridViewRow dgvrowObj in DgvBom.Rows)
                        {
                            if ((dgvrowObj.Cells["BomrowMaterial"].Value == null ? "" : dgvrowObj.Cells["BomrowMaterial"].Value.ToString()) != "")
                            {
                                if ((dgvrowObj.Cells["BomrowMaterialCode"].Value == null ? "" : dgvrowObj.Cells["BomrowMaterialCode"].Value.ToString()) != "")
                                {
                                    DataRow drow = dtblRowMaterials.NewRow();
                                    drow["BomrowMaterial"] = dgvrowObj.Cells["BomrowMaterialCode"].Value.ToString();
                                    drow["Bomqty"] = dgvrowObj.Cells["Bomqty"].Value.ToString();
                                    drow["Bomunitid"] = dgvrowObj.Cells["Bomunit"].Value.ToString();
                                    drow["BomrowMaterialname"] = dgvrowObj.Cells["BomrowMaterial"].Value.ToString();
                                    dtblRowMaterials.Rows.Add(drow);
                                }
                            }
                        }

                        BOMInfo InfoBOM = new BOMInfo();
                        InfoBOM.Extra1 = "";
                        InfoBOM.Extra2 = "";
                        InfoBOM.ProductCode = InfoProduct.ProductCode;
                        InfoBOM.ProductDetailsId = strProductDetailsId;
                        foreach (DataRow drowObj in dtblRowMaterials.Rows)
                        {
                            InfoBOM.Quantity = decimal.Parse(drowObj.ItemArray[1].ToString());
                            InfoBOM.RowMaterialId = drowObj.ItemArray[0].ToString();
                            InfoBOM.UnitId = drowObj.ItemArray[2].ToString();
                            SPBOM.BOMAdd(InfoBOM);
                        }
                    }

                    //----------------------to component settings table---------------------------------------------
                    if (InfoProductDetails.AllowComponentSale)
                    {
                        ComponentsSettingsInfo InfoComponentsSettings = new ComponentsSettingsInfo();
                        InfoComponentsSettings.Extra1 = "";
                        InfoComponentsSettings.Extra2 = "";
                        InfoComponentsSettings.ProductDetailsId = strProductDetailsId;
                        InfoComponentsSettings.ProductCode = InfoProduct.ProductCode;
                        foreach (DataRow drowObj in dtblPackage.Rows)
                        {
                            InfoComponentsSettings.ComponentId = drowObj.ItemArray[0].ToString();
                            SPComponentsSettings.ComponentsSettingsAdd(InfoComponentsSettings);
                        }
                    }
                    //-------------------to stock posting table----------------------------------------------------
                    if (cmbOpeningStock.Text == "Yes")
                    {
                        StockPostingInfo InfoStockPosting = new StockPostingInfo();
                        if (StPostingDate == "" || btnSave.Text == "Save")
                        {
                            InfoStockPosting.Date = PublicVariables._fromDate;
                        }
                        else
                        {
                            InfoStockPosting.Date = DateTime.Parse(StPostingDate);
                        }
                        InfoStockPosting.Extra1 = "";
                        InfoStockPosting.Extra2 = "";
                        InfoStockPosting.Optional = false;
                        InfoStockPosting.OutwardQty = 0;
                        InfoStockPosting.ProductCode = InfoProduct.ProductCode;
                        InfoStockPosting.VoucherNo = InfoProduct.ProductCode;
                        InfoStockPosting.VoucherType = "Opening Stock";

                        foreach (DataGridViewRow dgvrowObj in dgvDetails.Rows)
                        {
                            if ((dgvrowObj.Cells["qty"].Value == null ? "" : dgvrowObj.Cells["qty"].Value.ToString()) != "")
                            {
                                if ((dgvrowObj.Cells["batch"].Value == null ? "" : dgvrowObj.Cells["batch"].Value.ToString()) != "")
                                {
                                    BatchInfo InfoBatch = new BatchInfo();
                                    InfoBatch.BatchName = dgvrowObj.Cells["batch"].Value.ToString();
                                    if (InfoBatch.BatchName.ToLower() != "na")
                                    {
                                        InfoBatch.Mfd = DateTime.Parse(dgvrowObj.Cells["mfd"].Value.ToString());
                                        InfoBatch.Expd = DateTime.Parse(dgvrowObj.Cells["expd"].Value.ToString());
                                    }
                                    else
                                    {
                                        InfoBatch.Mfd = DateTime.Parse("1/1/1753");
                                        InfoBatch.Expd = DateTime.Parse("1/1/1753");
                                    }
                                    InfoBatch.Extra1 = "";
                                    InfoBatch.Extra2 = "";
                                    InfoStockPosting.BatchId = SPBatch.BatchSaveorEditandGetId(InfoBatch);
                                }
                                else
                                {
                                    InfoStockPosting.BatchId = "1";
                                }
                                if ((dgvrowObj.Cells["godown"].Value == null ? "" : dgvrowObj.Cells["godown"].Value.ToString()) != "")
                                {
                                    InfoStockPosting.GodownId = dgvrowObj.Cells["godown"].Value.ToString();
                                }
                                else
                                {
                                    InfoStockPosting.GodownId = "Primary Location";
                                }
                                if ((dgvrowObj.Cells["rack"].Value == null ? "" : dgvrowObj.Cells["rack"].Value.ToString()) != "")
                                {
                                    InfoStockPosting.RackId = dgvrowObj.Cells["rack"].Value.ToString();
                                }
                                else
                                {
                                    InfoStockPosting.RackId = "Primary Rack";
                                }
                                InfoStockPosting.InwardQty = decimal.Parse(dgvrowObj.Cells["qty"].Value.ToString());
                                InfoStockPosting.Rate = decimal.Parse(dgvrowObj.Cells["rate"].Value.ToString());
                                InfoStockPosting.UnitId = dgvrowObj.Cells["unitPerQty"].Value.ToString();

                                //Added on 24/oct/2023
                                InfoStockPosting.VoucherQty = InfoStockPosting.InwardQty;
                                InfoStockPosting.VoucherUnitId = InfoStockPosting.UnitId;
                                if (dgvrowObj.Index == 0)
                                {
                                    if (strPrdDtlsIdToEdit == "")
                                    {
                                        InfoProductDetails.ProductDetailsId = strProductDetailsId;
                                    }
                                    else
                                    {
                                        InfoProductDetails.ProductDetailsId = strPrdDtlsIdToEdit;
                                    }

                                    if (strPrdDtlsIdToEdit == "" || InfoProductDetails.PurchaseRate == 0)// purchase rate update only in save case
                                    {
                                        InfoProductDetails.PurchaseRate = InfoStockPosting.Rate;
                                        SPProductDetails.ProductDetailsEdit(InfoProductDetails);
                                    }
                                }
                                //InfoStockPosting.VoucherRate = decimal.Parse(dgvrowObj.Cells["rate"].Value.ToString());
                                SPStockPosting.StockPostingAdd(InfoStockPosting);
                            }
                        }
                    }

                    //created by sheena on 13-05-2023
                    ProductSalesPriceSP productSalesPriceSP = new ProductSalesPriceSP();
                    ProductSalesPriceHistorySP productSalesPriceHistorySP = new ProductSalesPriceHistorySP();
                    if (strProductIdToEdit != "")
                    {
                        newProductCode = strProductIdToEdit;
                       // productSalesPriceSP.ProductSalesPriceDeleteByproductCode(strProductIdToEdit);
                        // SPStockPosting.StPostingDeleteByVoucherTypeAndVoucherNumberAndBranch(strProductIdToEdit, "Opening Stock", PublicVariables._branchId);
                    }
                    ProductSalesPriceInfo productSalesPriceInfo = new ProductSalesPriceInfo();
                    productSalesPriceInfo.ProductCode = txtProductCode.Text;
                    productSalesPriceInfo.UnitId = cmbUnit.SelectedValue.ToString();
                    try
                    {
                        productSalesPriceInfo.PricingLevelId = "1";
                    }
                    catch { productSalesPriceInfo.PricingLevelId = "1"; }//added sheena 04-12-2023
                    productSalesPriceInfo.Amount = Decimal.Parse(txtSalesPrice.Text);
                    productSalesPriceInfo.DiscPercentage = 0;// Decimal.Parse(dgvrowObj.Cells["DiscPercentage"].Value.ToString());
                    productSalesPriceInfo.DiscAmount = 0;// Decimal.Parse(dgvrowObj.Cells["DiscAmount"].Value.ToString());
                    productSalesPriceInfo.SalesPrice = Decimal.Parse(txtSalesPrice.Text);// Decimal.Parse(dgvrowObj.Cells["SalesPrice"].Value.ToString());
                    productSalesPriceInfo.UserId = PublicVariables._currentUserId;
                    productSalesPriceInfo.BranchId = PublicVariables._branchId;
                    productSalesPriceInfo.Extra1 = "";
                    productSalesPriceInfo.Extra2 = "";
                    try
                    {
                        productSalesPriceInfo.CostPrice = Decimal.Parse(txtSalesPrice.Text);// Decimal.Parse(dgvrowObj.Cells["costPrice"].Value.ToString());
                    }
                    catch { productSalesPriceInfo.CostPrice = 0; }
                    try
                    {
                        productSalesPriceInfo.MarginPercentage = 0;// Decimal.Parse(dgvrowObj.Cells["marginPercentage"].Value.ToString());
                    }
                    catch { productSalesPriceInfo.MarginPercentage = 0; }
                    try
                    {
                        productSalesPriceInfo.LowestSellingPrice = 0;// Decimal.Parse(dgvrowObj.Cells["LowestSellingPrice"].Value.ToString());
                    }
                    catch { productSalesPriceInfo.LowestSellingPrice = 0; }

                    productSalesPriceSP.ProductSalesPriceDeleteByproductCode(txtProductCode.Text);
                    productSalesPriceSP.ProductSalesPriceAdd(productSalesPriceInfo);

                    //try
                    //{
                    //    if (productSalesPriceSP.sqlcon.State == ConnectionState.Closed)
                    //        productSalesPriceSP.sqlcon.Open();

                    //    using (SqlTransaction tran = productSalesPriceSP.sqlcon.BeginTransaction())
                    //    {
                    //        productSalesPriceSP.ProductSalesPriceDeleteByproductCode(txtProductCode.Text, tran);
                    //        productSalesPriceSP.ProductSalesPriceAdd(productSalesPriceInfo, tran);
                    //        tran.Commit();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Error saving Product Sales Price: " + ex.Message);
                    //}
                    //finally
                    //{
                    //    productSalesPriceSP.sqlcon.Close();
                    //}


                    //if (dgvrowObj.Cells["status"].Value == null)
                    //{
                    //    productSalesPriceSP.ProductSalesPriceAdd(productSalesPriceInfo);
                    //}
                    //else if (dgvrowObj.Cells["status"].Value.ToString() == "IsFromDb")
                    //{
                    //    productSalesPriceInfo.SalespriceId = dgvrowObj.Cells["salespriceId"].Value.ToString();
                    //    productSalesPriceSP.ProductSalesPriceEdit(productSalesPriceInfo);
                    //    if (dgvrowObj.Cells["oldSalesPrice"].Value.ToString() != dgvrowObj.Cells["SalesPrice"].Value.ToString() || dgvrowObj.Cells["oldSalesUnit"].Value.ToString() != dgvrowObj.Cells["UnitId"].Value.ToString())
                    //    {
                    //        productSalesPriceInfo.OperationType = 'U';
                    //        productSalesPriceInfo.oldSalesPrice = decimal.Parse(dgvrowObj.Cells["oldSalesPrice"].Value.ToString());
                    //        productSalesPriceInfo.oldSalesUnit = dgvrowObj.Cells["oldSalesUnit"].Value.ToString();
                    //        //update history table
                    //        productSalesPriceHistorySP.ProductSalesPriceHistoryAdd(productSalesPriceInfo);
                    //    }
                    //}
                    // ArrageSalesPriceGrid();
                    //-------------------------------save to ProductSalesPrice--------------------
                    //if (dgvSalesPrice.Rows.Count > 0)
                    //{
                    //    foreach (DataGridViewRow dgvrowObj in dgvSalesPrice.Rows)
                    //    {
                    //        if (!dgvrowObj.IsNewRow)
                    //        {
                    //            ProductSalesPriceInfo productSalesPriceInfo = new ProductSalesPriceInfo();
                    //            productSalesPriceInfo.ProductCode = txtProductCode.Text;
                    //            productSalesPriceInfo.UnitId = dgvrowObj.Cells["UnitId"].Value.ToString();
                    //            try
                    //            {
                    //                productSalesPriceInfo.PricingLevelId = dgvrowObj.Cells["pricingLevelId"].Value.ToString();
                    //            }
                    //            catch { productSalesPriceInfo.PricingLevelId = "1"; }//added sheena 04-12-2023
                    //            productSalesPriceInfo.Amount = Decimal.Parse(dgvrowObj.Cells["PriceAmount"].Value.ToString());
                    //            productSalesPriceInfo.DiscPercentage = Decimal.Parse(dgvrowObj.Cells["DiscPercentage"].Value.ToString());
                    //            productSalesPriceInfo.DiscAmount = Decimal.Parse(dgvrowObj.Cells["DiscAmount"].Value.ToString());
                    //            productSalesPriceInfo.SalesPrice = Decimal.Parse(dgvrowObj.Cells["SalesPrice"].Value.ToString());
                    //            productSalesPriceInfo.UserId = PublicVariables._currentUserId;
                    //            productSalesPriceInfo.BranchId = PublicVariables._branchId;
                    //            productSalesPriceInfo.Extra1 = "";
                    //            productSalesPriceInfo.Extra2 = "";
                    //            try
                    //            {
                    //                productSalesPriceInfo.CostPrice = Decimal.Parse(dgvrowObj.Cells["costPrice"].Value.ToString());
                    //            }
                    //            catch { productSalesPriceInfo.CostPrice = 0; }
                    //            try
                    //            {
                    //                productSalesPriceInfo.MarginPercentage = Decimal.Parse(dgvrowObj.Cells["marginPercentage"].Value.ToString());
                    //            }
                    //            catch { productSalesPriceInfo.MarginPercentage = 0; }
                    //            try
                    //            {
                    //                productSalesPriceInfo.LowestSellingPrice = Decimal.Parse(dgvrowObj.Cells["LowestSellingPrice"].Value.ToString());
                    //            }
                    //            catch { productSalesPriceInfo.LowestSellingPrice = 0; }
                    //            if (dgvrowObj.Cells["status"].Value==null)
                    //            {                                   
                    //                productSalesPriceSP.ProductSalesPriceAdd(productSalesPriceInfo);
                    //            }
                    //            else if(dgvrowObj.Cells["status"].Value.ToString()=="IsFromDb")
                    //            {
                    //                productSalesPriceInfo.SalespriceId =dgvrowObj.Cells["salespriceId"].Value.ToString();
                    //                productSalesPriceSP.ProductSalesPriceEdit(productSalesPriceInfo);
                    //                if (dgvrowObj.Cells["oldSalesPrice"].Value.ToString() != dgvrowObj.Cells["SalesPrice"].Value.ToString() || dgvrowObj.Cells["oldSalesUnit"].Value.ToString() != dgvrowObj.Cells["UnitId"].Value.ToString())
                    //                {
                    //                    productSalesPriceInfo.OperationType = 'U';
                    //                    productSalesPriceInfo.oldSalesPrice = decimal.Parse(dgvrowObj.Cells["oldSalesPrice"].Value.ToString());
                    //                    productSalesPriceInfo.oldSalesUnit =dgvrowObj.Cells["oldSalesUnit"].Value.ToString(); 
                    //                    //update history table
                    //                    productSalesPriceHistorySP.ProductSalesPriceHistoryAdd(productSalesPriceInfo);
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    strProductCodeToREturn = InfoProduct.ProductCode;
                    strUnitIdToReturn = InfoProduct.UnitId;

                    ProductPurchasePriceInfo infoProductPurchasePrice = new ProductPurchasePriceInfo();
                    ProductPurchasePriceSP spProductPurchasePrice = new ProductPurchasePriceSP();

                    //------------delete from tbl_ProdutcPurchase if remove button clicked----------------\\

                    if (strDeleteIds != "")
                    {
                        strDeleteIds = strDeleteIds.Trim(',');
                        spProductPurchasePrice.ProductPurchasePriceDelete(strDeleteIds);
                    }

                    if(dgvProductPurchasePrice.Rows.Count>0)
                    {
                     

                        for (int i = 0; i < dgvProductPurchasePrice.Rows.Count;i++)
                        {
                            if (dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value != null)
                            {
                                if (dgvProductPurchasePrice.Rows[i].Cells["ppp_purchasepriceid"].Value != null)
                                {
                                    infoProductPurchasePrice.PurchasepriceId = dgvProductPurchasePrice.Rows[i].Cells["ppp_purchasepriceid"].Value.ToString();
                                }
                                else
                                {
                                    infoProductPurchasePrice.PurchasepriceId = "";
                                }


                                infoProductPurchasePrice.LedgerId = dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value.ToString();
                                infoProductPurchasePrice.ProductCode = txtProductCode.Text;
                                infoProductPurchasePrice.UnitId = dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].Value.ToString();
                                infoProductPurchasePrice.Amount = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_amount"].Value.ToString());
                                infoProductPurchasePrice.DiscPercentage = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value.ToString());
                                infoProductPurchasePrice.DiscAmount = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value.ToString());
                                infoProductPurchasePrice.PurchasePrice = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_purchaseprice"].Value.ToString());
                                infoProductPurchasePrice.Extra1 = "";
                                infoProductPurchasePrice.Extra2 = "";
                                infoProductPurchasePrice.PurchaseDate = Convert.ToDateTime(dgvProductPurchasePrice.Rows[i].Cells["ppp_date"].Value.ToString());

                                if (dgvProductPurchasePrice.Rows[i].Cells["ppp_purchasemasterid"].Value != null)
                                {
                                    infoProductPurchasePrice.PurchaseMasterId = dgvProductPurchasePrice.Rows[i].Cells["ppp_purchasemasterid"].Value.ToString();
                                }
                                else
                                {
                                    infoProductPurchasePrice.PurchaseMasterId = "";
                                }


                                spProductPurchasePrice.ProductPurchasePriceAdd(infoProductPurchasePrice);
                            }

                            

                            
                        }
                    }

                    if (strPrdDtlsIdToEdit == "")
                    {
                        spgeneral.UserActivityAdd("Product Creation", "Save", newProductCode, txtProductCode.Text,DateTime.Now, decOldAmount, 0);//20-03-2024 sheena
                        MessageBox.Show("Saved successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        spgeneral.UserActivityAdd("Product Creation", "Edit", strProductIdToEdit, txtProductCode.Text, DateTime.Now, decOldAmount, 0);//20-03-2024 sheena
                        MessageBox.Show("Updated successfully", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC21:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            //return isNotExist;
        }
      
        public async Task<string> UploadImageToCloudAsync(string localFilePath, string productCode, string customerCode)
        {
            if (!CheckForInternetConnection())
            {
                MessageBox.Show("No internet connection. Please check your network and try again.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            //  string baseUrl = txtApiUrl.Text.Trim().TrimEnd('/');
            string strServer = ".\\sqlExpress";
            // string strPrimaryDbName = "DBFINACACCOUNT";
            if (File.Exists(Application.StartupPath + "\\sys.txt"))
            {
                string DbData = File.ReadAllText(Application.StartupPath + "\\sys.txt"); ;
                string[] values = DbData.Split(',');

                strServer = values[0].Trim();
                // strPrimaryDbName = values[1].Trim();

                //strServer = File.ReadAllText(Application.StartupPath + "\\sys.txt"); // getting ip of server
            }
            string uploadUrl = $"http://{strServer}:666/api/ProductImage/SaveProductImage"; // your API endpoint //DbBackupDownload is the website in Remote IID


            using (var client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(productCode), "ProductCode");
                content.Add(new StringContent(customerCode), "CustomerCode");

                using (var fileStream = File.OpenRead(localFilePath))
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    content.Add(fileContent, "file", Path.GetFileName(localFilePath));

                    try
                    {
                        var response = await client.PostAsync(uploadUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            dynamic result = JsonConvert.DeserializeObject(json);
                            return result.imageUrl;
                        }
                        else
                        {
                            MessageBox.Show("Failed to upload image. Server error.", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Upload failed: " + ex.Message, "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return null;
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void SaveImage()
        {
            DirectoryInfo drinfo = new DirectoryInfo(Application.StartupPath + "\\ProductImage");
            if (!drinfo.Exists)
            {
                drinfo.Create();
            }

            if (!string.IsNullOrEmpty(strProductIdToEdit) && !string.IsNullOrEmpty(oldDestinationPath))
            {
                try
                {
                    if (File.Exists(oldDestinationPath))
                    {
                        File.Delete(oldDestinationPath);
                    }
                }
                catch { }
            }

            if (!string.IsNullOrEmpty(destinationPath))
            {
                string sourcePath = Path.Combine(Application.StartupPath, "ProductImage", txtProductCode.Text);
                string extension = Path.GetExtension(ofdPhoto.FileName).ToLower();

                if (string.IsNullOrEmpty(extension))
                    extension = ".jpg";

                destinationPath = sourcePath + extension;

                try
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                }
                catch { }

                try
                {
                    if (pbLogo.Image != null)
                    {
                        // ✅ Clone image into a new Bitmap to avoid GDI+ save error
                        using (Bitmap bmp = new Bitmap(pbLogo.Image))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            if (extension == ".jpg" || extension == ".jpeg")
                            {
                                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                                EncoderParameters encoderParams = new EncoderParameters(1);
                                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L);
                                bmp.Save(ms, jpgEncoder, encoderParams);
                            }
                            else if (extension == ".png")
                            {
                                bmp.Save(ms, ImageFormat.Png);
                            }
                            else
                            {
                                bmp.Save(ms, ImageFormat.Bmp);
                            }

                            byte[] imgBytes = ms.ToArray();

                            if (imgBytes.Length > 0)
                            {
                                File.WriteAllBytes(destinationPath, imgBytes);
                            }
                            else
                            {
                                MessageBox.Show("Image is empty. Nothing was saved.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No image to save.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image: " + ex.Message);
                }
            }
        }


        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        //-----------------------------Function to print----------------------------------------------------------------
        public void Print()
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC22:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //-------------------------If comming from other forms it is used to refill parent--------------------------------
        string strProductCodeToREturn = "", strUnitIdToReturn = "";
        //public void CheckWhenQuiting()
        //{
        //    try
        //    {
        //        // Function to check while quiting the form
        //        // To return to the parent form
        //        // Fill Register again
        //        if (objfrmProductRegister != null)
        //        {
        //            objfrmProductRegister.Enabled = true;
        //            objfrmProductRegister.Activate();
        //            objfrmProductRegister.FillGrid();
        //            objfrmProductRegister.BringToFront();
        //        }
        //        else if (frmObjSales != null)
        //        {
        //            frmObjSales.Enabled = true;
        //            frmObjSales.Activate();
        //            frmObjSales.FillrowAfterPickingProduct(strProductCodeToREturn,"", inProductRowIndex, true, "", strUnitIdToReturn);
        //            frmObjSales.BringToFront();
        //        }
        //        else if (frmObjSalesReturn != null)
        //        {
        //            frmObjSalesReturn.Enabled = true;
        //            frmObjSalesReturn.Activate();
        //            frmObjSalesReturn.FillrowAfterPickingProduct(strProductCodeToREturn,"", inProductRowIndex, true, "");
        //            frmObjSalesReturn.BringToFront();
        //        }
        //        else if (frmObjSalesOrder != null)
        //        {
        //            frmObjSalesOrder.Enabled = true;
        //            frmObjSalesOrder.Activate();
        //            frmObjSalesOrder.FillrowAfterPickingProduct(strProductCodeToREturn, inProductRowIndex, true);
        //            frmObjSalesOrder.BringToFront();
        //        }
        //        else if (frmObjDeliveryNote != null)
        //        {
        //            frmObjDeliveryNote.Enabled = true;
        //            frmObjDeliveryNote.Activate();
        //            frmObjDeliveryNote.FillrowAfterPickingProduct(strProductCodeToREturn,"", inProductRowIndex, true);
        //            frmObjDeliveryNote.BringToFront();
        //        }
        //        else if (frmObjSalesQuot != null)
        //        {
        //            frmObjSalesQuot.Enabled = true;
        //            frmObjSalesQuot.Activate();
        //            frmObjSalesQuot.FillrowAfterPickingProduct(strProductCodeToREturn, inProductRowIndex, true);
        //            frmObjSalesQuot.BringToFront();
        //        }
        //        //----
        //        else if (frmObjpurchaseorder != null)
        //        {
        //            frmObjpurchaseorder.Enabled = true;
        //            frmObjpurchaseorder.Activate();
        //            frmObjpurchaseorder.FillrowAfterPickingProduct(strProductCodeToREturn, inProductRowIndex, true);
        //            frmObjpurchaseorder.BringToFront();
        //        }
        //        else if (frmObjmaterialreceipt != null)
        //        {
        //            frmObjmaterialreceipt.Enabled = true;
        //            frmObjmaterialreceipt.Activate();
        //            frmObjmaterialreceipt.FillrowAfterPickingProduct(strProductCodeToREturn, inProductRowIndex, true);
        //            frmObjmaterialreceipt.BringToFront();
        //        }
        //        else if (frmObjPurchase != null)
        //        {
        //            frmObjPurchase.Enabled = true;
        //            frmObjPurchase.Activate();
        //            frmObjPurchase.FillrowAfterPickingProduct(strProductCodeToREturn,"", inProductRowIndex, true);
        //            frmObjPurchase.BringToFront();
        //        }
        //        else if (frmObjPurchaseReurn != null)
        //        {
        //            frmObjPurchaseReurn.Enabled = true;
        //            frmObjPurchaseReurn.Activate();
        //            frmObjPurchaseReurn.FillrowAfterPickingProduct(strProductCodeToREturn, inProductRowIndex, true);
        //            frmObjPurchaseReurn.BringToFront();
        //        }
        //        else if (frmObjPos != null)
        //        {
        //            frmObjPos.Enabled = true;
        //            frmObjPos.Activate();
        //            frmObjPos.FillrowAfterPickingProduct(strProductCodeToREturn, true);
        //            frmObjPos.BringToFront();
        //        }

        //        else if (objfrmTRansfer != null)
        //        {
        //            objfrmTRansfer.Enabled = true;
        //            objfrmTRansfer.Activate();
        //            objfrmTRansfer.FillrowAfterPickingProduct(strProductCodeToREturn, inProductRowIndex, true, strUnitIdToReturn);
        //            objfrmTRansfer.BringToFront();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("PC23:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}
        //----------------------Fill Product Group Main  combo-----------------------------------------------------------
        public void FillProductMainGroupCombo()////created by sheena on 05-05-2023
        {
            try
            {
                cmbProductMainGrp.DataSource = SPProductDetails.ProductMainGroupViewAll();
                if (cmbProductMainGrp.DataSource != null)
                {
                    cmbProductMainGrp.DisplayMember = "groupName";
                    cmbProductMainGrp.ValueMember = "groupCode";
                    cmbProductMainGrp.SelectedIndex = 0;
                    //cmbProductMainGrp.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //------------------------function to return to product creation after creating product main group,unit or brand------------------------------
        public void DowhenReturningFromProductMainGroupForm(string strId)////created by sheena on 05-05-2023
        {
            try
            {
                this.Enabled = true;
                FillProductMainGroupCombo();
                if (strId != "")
                {
                    // Assign newly created id
                    cmbProductMainGrp.SelectedValue = strId;
                }
                else if (strOldMainGrpCode != "")
                {
                    // Assign old id as new one is not created
                    cmbProductMainGrp.SelectedValue = strOldMainGrpCode;
                }
                cmbProductMainGrp.Focus();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //-------------------function to create new product code(maingroup code+productcode)----------
        public void generateNewProductCode()////created by sheena on 09-05-2023
        {
           
            if (cmbProductMainGrp.SelectedValue != null)
            {
                string groupCode = cmbProductMainGrp.SelectedValue.ToString();
                if (groupCode != "-1" || groupCode != "0")
                {
                    int nxtNmbrLength = 0; int prodtcCodeLength = 0; int nxtNumber = 0; int grpcodelngth = 0;
                    ProductMainGroupInfo InfoDetails = new ProductDetailsSP().getMainGroupNextNmbr(groupCode);
                    prodtcCodeLength = InfoDetails.ProductCodeLength;
                    nxtNumber = InfoDetails.NextNumber;
                    grpcodelngth = groupCode.Length;
                    nxtNmbrLength = Convert.ToString(nxtNumber).Length;
                    if (grpcodelngth < prodtcCodeLength)
                    {
                        if ((grpcodelngth + nxtNmbrLength) <= prodtcCodeLength)
                        {
                            //if (SettingsInfo._automaticProductCodeGeneration)
                            //{
                            if (strPrdDtlsIdToEdit == "")
                            {
                                txtProductCode.Clear();
                                decimal decNewProductcode = 1;
                                string strProductcode = nxtNumber.ToString();
                                if (strProductcode != "string")
                                {
                                    decNewProductcode = decimal.Parse(strProductcode.ToString());
                                    string strNewProductCode = decNewProductcode.ToString();
                                    int _NoOfDigt = decNewProductcode.ToString().Length;
                                    int nxtProductCodelngth = prodtcCodeLength - grpcodelngth;
                                    if (nxtNmbrLength < nxtProductCodelngth)
                                    {
                                        strNewProductCode = (decNewProductcode.ToString()).PadLeft(nxtProductCodelngth, '0');
                                    }
                                    else
                                    {
                                        strNewProductCode = (decNewProductcode.ToString());
                                    }
                                    //main group code + product code

                                    txtProductCode.Text = groupCode + strNewProductCode;
                                }
                                else
                                {
                                    // Cant Generate Automatically string entry exist
                                    MessageBox.Show("Cant generate automatically", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //SettingsInfo Info = new SettingsInfo();
                                    //SettingsSP sp = new SettingsSP();
                                    //Info = sp.SettingsViewByBranchId1(PublicVariables._branchId);
                                    //Info.AutomaticProductCodeGeneration = false;
                                    //sp.SettingsEdit(Info);
                                    txtProductCode.ReadOnly = false;
                                    txtProductCode.Clear();
                                }
                            }
                            //txtProductCode.ReadOnly = true;//by sheena 15-06-2023
                            //}
                            //else
                            //{
                            //    if (strPrdDtlsIdToEdit == "")
                            //    {
                            //        txtProductCode.ReadOnly = false;
                            //        txtProductCode.Clear();
                            //    }
                            //}

                        }
                        else
                            MessageBox.Show("Cant generate automatically", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }

        }

        //fill all units in grid combo column ////created by sheena on 10-05-2023
        public void FillUnitToGridComboColumn(DataGridViewComboBoxColumn dgvcmbUnit)
        {
            UnitSP SPUnit = new UnitSP();
            dgvcmbUnit.DataSource = SPUnit.UnitViewAll();
            if (dgvcmbUnit.DataSource != null)
            {
                dgvcmbUnit.DisplayMember = "unitName";
                dgvcmbUnit.ValueMember = "unitId";
            }
        }
        public void InitialSettingsUnitGrid()///created by sheena on 10-05-2023
        {
            if (cmbUnit.SelectedValue != null)
            {
                strDefault = cmbUnit.SelectedValue.ToString();
                objTransactionsGeneralFill.FillUnitToGridComboColumn(unit);
                objTransactionsGeneralFill.FillUnitToGridComboColumn(defaultUnit);
                  
                //if (strDefault != "")
                //{

                //    if (unit.DataSource != null)
                //    {
                //        foreach (DataRow drowObj in ((DataTable)unit.DataSource).Rows)
                //        {
                //            if (drowObj.ItemArray[0].ToString() == strDefault)
                //            {
                //                ((DataTable)unit.DataSource).Rows.Remove(drowObj);
                //                break;
                //            }
                //        }

                //    }
                //}

                dgvUnits.Columns["defaultUnit"].ReadOnly = true;
                dgvUnits.Rows.Clear();
            }

        }
        //------------------------------remove row--------------------------------------------
        public void RemoveRowUnitGRid()///created by sheena on 10-05-2023
        {
            int inColIndex = dgvUnits.CurrentCell.ColumnIndex;
            bool isContinue = true;
            if (!dgvUnits.Rows[dgvUnits.CurrentRow.Index].IsNewRow)
            {
                if (isContinue)
                {
                    dgvUnits.Rows.RemoveAt(dgvUnits.CurrentRow.Index);
                    dgvUnits.CurrentCell = dgvUnits.CurrentRow.Cells[inColIndex];
                    dgvUnits.Focus();
                }
            }
            else
            {
                dgvUnits.Rows[dgvUnits.CurrentRow.Index].Cells["qty"].Value = "";
                dgvUnits.Rows[dgvUnits.CurrentRow.Index].Cells["unit"].Value = null;
                dgvUnits.CurrentCell = dgvUnits.CurrentRow.Cells[inColIndex];
                dgvUnits.Focus();
            }
        }
        //----------------------------check everything ok---------------------------------------
        public bool checkBaseUnitExistInUnitGrid()
        {
            try
            {
                string baseUnit = cmbUnit.SelectedValue?.ToString();
                if (!string.IsNullOrEmpty(baseUnit) && dgvUnits.Rows.Count > 1)
                {
                    foreach (DataGridViewRow dgvrowCur in dgvUnits.Rows)
                    {
                        if (dgvrowCur.Cells["Unit"].Value?.ToString() == baseUnit)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;  // If not found
        }

        public bool EverythingOkUnitGrid()///created by sheena on 17-05-2023
        {
            string strMessage = "Rows ";
            //checking for atleast one row contain completed entry
            int inC = 0, inForFirst = 0;
            bool isOk = true;
            bool isRowOk = true;

            if (dgvUnits.Rows.Count > 1)
            {
                foreach (DataGridViewRow dgvrowCur in dgvUnits.Rows)
                {
                    if ((dgvrowCur.Cells["ucheck"].Value == null ? "" : dgvrowCur.Cells["ucheck"].Value.ToString()) == "x")
                    {

                        isOk = false;
                        if (inC == 0)
                        {
                            strMessage = strMessage + Convert.ToString(dgvrowCur.Index + 1);
                            inForFirst = dgvrowCur.Index;
                            inC++;
                        }
                        else
                        {
                            strMessage = strMessage + ", " + Convert.ToString(dgvrowCur.Index + 1);
                        }

                    }
                    else if (dgvrowCur.Cells["barcode"].Value == null || dgvrowCur.Cells["barcode"].Value.ToString() == "")
                    {
                        if (!chkWithoutBarcode.Checked)
                        {
                            if (!dgvrowCur.IsNewRow)
                            {
                                isRowOk = false;
                                break;
                                //if (inC == 0)
                                //{
                                //    strMessage = strMessage + Convert.ToString(dgvrowCur.Index + 1);
                                //    inForFirst = dgvrowCur.Index;
                                //    inC++;
                                //}
                                //else
                                //{
                                //    strMessage = strMessage + ", " + Convert.ToString(dgvrowCur.Index + 1);
                                //}
                            }
                        }                       
                    }
                }
                bool isNoClicked = false;
                //if alteast one row contain completed entry check for continue with incompleted rows
                if (!isOk)
                {
                    strMessage = "Unit conversion: " + strMessage + " contains invalid entries. Cannot save the entry.";
                    MessageBox.Show(strMessage, "", MessageBoxButtons.OK);
                    //strMessage = "Unit conversion: " + strMessage + " contains invalid entries. Do you want to continue?";
                    //if (MessageBox.Show(strMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    //{
                    //    //if user decide to continue delete incomplete row
                    //    isOk = true;
                    //    for (int inK = 0; inK < dgvUnits.Rows.Count; inK++)
                    //    {
                    //        if ((dgvUnits.Rows[inK].Cells["ucheck"].Value == null ? "" : dgvUnits.Rows[inK].Cells["ucheck"].Value.ToString()) == "x")
                    //        {
                    //            if (!dgvUnits.Rows[inK].IsNewRow)
                    //            {
                    //                dgvUnits.Rows.RemoveAt(inK);
                    //            }
                    //            else
                    //            {
                    //                dgvUnits.Rows[inK].Cells["defaultQty"].Value = "";
                    //                dgvUnits.Rows[inK].Cells["unit"].Value = null;
                    //                dgvUnits.Rows[inK].Cells["ucheck"].Value = "";
                    //                dgvUnits.Rows[inK].Cells["barcode"].Value = "";

                    //            }
                    //            inK--;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    //to set focus to incomplete cell
                    //    isNoClicked = true;
                    //    dgvUnits.Focus();
                    //    if ((dgvUnits.Rows[inForFirst].Cells["defaultQty"].Value == null ? "" : dgvUnits.Rows[inForFirst].Cells["defaultQty"].Value.ToString()) == "")
                    //        dgvUnits.Rows[inForFirst].Cells["defaultQty"].Selected = true;

                    //    else if (decimal.Parse(dgvUnits.Rows[inForFirst].Cells["defaultQty"].Value.ToString()) == 0m)
                    //        dgvUnits.Rows[inForFirst].Cells["defaultQty"].Selected = true;

                    //    else if ((dgvUnits.Rows[inForFirst].Cells["unit"].Value == null ? "" : dgvUnits.Rows[inForFirst].Cells["unit"].Value.ToString()) == "")
                    //        dgvUnits.Rows[inForFirst].Cells["unit"].Selected = true;
                    //}
                }
                //else if (!isRowOk)
                //{
                //    isOk = false;
                //    isNoClicked = true;
                //    strMessage = "Please enter Barcode";
                //    MessageBox.Show(strMessage, "", MessageBoxButtons.OK);
                //}
                //if allow for continue check for repated product selection
                ////if (!isNoClicked)
                ////{
                ////    foreach (DataGridViewRow dgvrowFirst in dgvUnits.Rows)
                ////    {
                ////        foreach (DataGridViewRow dgvrowSecond in dgvUnits.Rows)
                ////        {
                ////            if (dgvrowFirst.Index != dgvrowSecond.Index)
                ////            {
                ////                if (dgvrowFirst.Cells["unit"].Value != null && dgvrowSecond.Cells["unit"].Value != null)
                ////                    if (dgvrowFirst.Cells["barcode"].Value != null && dgvrowSecond.Cells["barcode"].Value != null)
                ////                    {
                ////                        if (dgvrowFirst.Cells["barcode"].Value.ToString() == dgvrowSecond.Cells["barcode"].Value.ToString())
                ////                        {
                ////                            isOk = false;
                ////                            MessageBox.Show("Repeated entry of same unit exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////                            dgvUnits.Focus();
                ////                            dgvrowSecond.Cells["unit"].Selected = true;
                ////                            break;
                ////                        }
                ////                    }
                ////            }
                ////        }
                ////        if (!isOk)
                ////        {
                ////            break;
                ////        }
                ////    }
                ////}
                if (!isNoClicked)
                {
                    foreach (DataGridViewRow dgvrowFirst in dgvUnits.Rows)
                    {
                        foreach (DataGridViewRow dgvrowSecond in dgvUnits.Rows)
                        {
                            if (dgvrowFirst.Index != dgvrowSecond.Index)
                            {
                                string strfirstBarcode = "";
                                string strSecondBarcode = "";
                                try
                                {
                                    strfirstBarcode = dgvrowFirst.Cells["barcode"].Value.ToString();
                                }
                                catch { strfirstBarcode = ""; }
                                try
                                {
                                    strSecondBarcode = dgvrowSecond.Cells["barcode"].Value.ToString();
                                }
                                catch { strSecondBarcode = ""; }


                                if (dgvrowFirst.Cells["unit"].Value != null && dgvrowSecond.Cells["unit"].Value != null)
                                    if (dgvrowFirst.Cells["unit"].Value.ToString() != dgvrowSecond.Cells["unit"].Value.ToString())//different units
                                    {
                                        if (strfirstBarcode != "" && strSecondBarcode != "")
                                        {
                                            if (strfirstBarcode == strSecondBarcode)
                                            {
                                                isOk = false;
                                                MessageBox.Show("Repeated entry of same unit exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                dgvUnits.Focus();
                                                dgvrowSecond.Cells["unit"].Selected = true;
                                                break;
                                            }
                                        }
                                        else if (strfirstBarcode == "" && strSecondBarcode == "")
                                        {
                                            isOk = false;
                                            MessageBox.Show("Cannot save without barcode", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            dgvUnits.Focus();
                                            dgvrowSecond.Cells["unit"].Selected = true;
                                            break;
                                        }
                                    }
                                    else// same units
                                    {
                                        //if (strfirstBarcode != "" && strSecondBarcode != "")
                                        //{

                                        if (strfirstBarcode != "" && strSecondBarcode != "")
                                        {
                                            if (strfirstBarcode == strSecondBarcode)
                                            {
                                                isOk = false;
                                                MessageBox.Show("Repeated entry of same unit exists", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                dgvUnits.Focus();
                                                dgvrowSecond.Cells["unit"].Selected = true;
                                                break;
                                            }
                                        }
                                        // }
                                    }
                            }
                        }
                        if (!isOk)
                        {
                            break;
                        }
                    }
                }
                //if(!isOk)
                //{
                //    for (int inK = 0; inK < dgvUnits.Rows.Count; inK++)
                //    {
                //        if (SPUnitConversion.CheckBarcodeExist(txtProductCode.Text, dgvUnits.Rows[inK].Cells["barcode"].Value.ToString(), dgvUnits.Rows[inK].Cells["unit"].Value.ToString(),decimal.Parse( dgvUnits.Rows[inK].Cells["defaultQty"].Value.ToString())))
                //        {
                //            MessageBox.Show("Barcode already exist", "WARNING");
                //            dgvUnits.Rows[inK].Cells["barcode"].Value = "";
                //            isOk = false;
                //        }
                //    }
                //}
            }
            else
            {
                isOk = false;
                MessageBox.Show("Please enter unit conversion", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return isOk;
        }
        #endregion
        //****************************************************************************************************************************************
        //EVENTS
        //****************************************************************************************************************************************
        #region BUTTON CLICK EVENTS
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOk = true;
                dgvDetails.CurrentCell = null;
                dgvDetails.EndEdit();

                if (PublicVariables._closed)
                {
                    if (MessageBox.Show("Selected financial year has been closed. Do you want to open it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        FinancialYearSp SpFinance = new FinancialYearSp();
                        SpFinance.FinancialYearchangeStatus(false);
                        isOk = true;
                    }
                    else
                    {
                        isOk = false;
                    }
                }
                if (isOk)
                {

                    //if (EverythingOk() && EverythingOkSalesGrid() && EverythingOkUnitGrid() && EverythingOkPurchaseGrid())
                    if (EverythingOk() )
                    {
                        //if (checkBaseUnitExistInUnitGrid())
                        //{
                            if (InventorySettingsInfo._messageBoxAddEdit)
                            {
                                if (MessageBox.Show("Do you want to " + (strPrdDtlsIdToEdit == "" ? "save" : "update") + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {

                                    //if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, (strPrdDtlsIdToEdit == "" ? "Add" : "Edit"), _mainMenuItem) == true)//added on 19/10/2023 sheena
                                    //{
                                        bool success = await SaveOrEdit();
                                        if (success)
                                        {
                                            if (cbxPrint.Checked == true)
                                            {
                                                Print();
                                            }
                                            if (isFromOther)
                                            {
                                                this.Close();
                                            }
                                            else
                                            {
                                                txtProductCode.Clear();
                                                strProductIdToEdit = "";
                                                strPrdDtlsIdToEdit = "";
                                                Clear();
                                            }
                                        }
                                    //}

                                    //else
                                    //{
                                    //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //}
                                }
                            }
                            else
                            {
                                //if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, (strPrdDtlsIdToEdit == "" ? "Add" : "Edit"), _mainMenuItem) == true)//added on 19/10/2023 sheena
                                //{
                                    bool success = await SaveOrEdit();
                                    if (success)
                                    {
                                        if (cbxPrint.Checked == true)
                                        {
                                            Print();
                                        }
                                        if (isFromOther)
                                        {
                                            this.Close();
                                        }
                                        else
                                        {
                                            txtProductCode.Clear();
                                            strProductIdToEdit = "";
                                            strPrdDtlsIdToEdit = "";
                                            Clear();
                                        }
                                    }
                                //}
                                //else
                                //{
                                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                            }
                        //}
                        //else
                        //    MessageBox.Show("Please add barcode for base unit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC24:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        bool isEditFill = false;
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (InventorySettingsInfo._messageBoxClear)
                {
                    if (MessageBox.Show("Do you want to clear?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        //if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Add", _mainMenuItem) == true)
                        //{
                            isEditEnable = true;
                            btnSave.Enabled = isEditEnable;
                        //}
                        //else
                        //{
                        //    isEditEnable = false;
                        //    btnSave.Enabled = isEditEnable;

                        //}
                       
                        strProductIdToEdit = "";
                        strPrdDtlsIdToEdit = "";
                        Clear();
                        // txtProductCode.Clear();

                        //if (objfrmProductRegister != null)
                        //{
                        //    objfrmProductRegister.Close();
                        //    objfrmProductRegister = null;
                        //}
                        // generateNewProductCode();
                        // GenerateProductCode();

                    }
                }
                else
                {
                    //if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Add", _mainMenuItem) == true)
                    //{
                        isEditEnable = true;
                        btnSave.Enabled = isEditEnable;
                    //}
                    //else
                    //{
                    //    isEditEnable = false;
                    //    btnSave.Enabled = isEditEnable;

                    //}
                    strProductIdToEdit = "";
                    strPrdDtlsIdToEdit = "";
                    Clear();
                   
                   // txtProductCode.Clear();

                    //if (objfrmProductRegister != null)
                    //{
                    //    objfrmProductRegister.Close();
                    //    objfrmProductRegister = null;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC25:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool isOk = true;

                if (PublicVariables._closed)
                {
                    if (MessageBox.Show("Selected financial year has been closed. Do you want to open it?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        FinancialYearSp SpFinance = new FinancialYearSp();
                        SpFinance.FinancialYearchangeStatus(false);
                        isOk = true;
                    }
                    else
                    {
                        isOk = false;
                    }
                }
                if (isOk)
                {
                    if (InventorySettingsInfo._messageBoxDelete)
                    {
                        if (MessageBox.Show("Do you want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            //if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Delete", _mainMenuItem) == true)//added on 19/10/2023 sheena
                            //{

                                DeleteProduct();
                            //}

                            //else
                            //{
                            //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}

                        }
                    }
                    else
                    {

                        //if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Delete", _mainMenuItem) == true)//added on 19/10/2023 sheena
                        //{
                            DeleteProduct();
                        //}

                        //else
                        //{
                        //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC26:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            try
            {
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Product Group", "", "Masters") == true)
                // //   if (checkuserprivilege.CheckPrivilage("Product Group", "") == true)
                //{
                    // Save current product group Id
                    if (cmbGroup.SelectedValue != null)
                    {
                        strOldGrpId = cmbGroup.SelectedValue.ToString();
                    }
                    else
                    {
                        strOldGrpId = "";
                    }
                    frmProductGroup frmproductgroup = new frmProductGroup();


                    frmproductgroup.MdiParent = MDIFinacPOS.MDIObj;
                    frmproductgroup.DoWhenComingFromProductCreationForm(this, "Category 4");
                    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("PC28" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNewBrand_Click(object sender, EventArgs e)
        {
            try
            {
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Brand", "", "Masters") == true)                
                //{
                    // Save current product group Id
                    if (cmbBrand.SelectedValue != null)
                    {
                        strOldBrandId = cmbBrand.SelectedValue.ToString();
                    }
                    else
                    {
                        strOldBrandId = "";
                    }
                    frmMasterCreation frmmastercreation = new frmMasterCreation();
                    frmmastercreation.MdiParent = MDIFinacPOS.MDIObj;
                    frmmastercreation.DoWhenComingFromProductCreationForm(this, "Brand");
                    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("PC29" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {

                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Unit", "", "Masters") == true)
                //{
                    // Save current product group Id
                    if (cmbUnit.SelectedValue != null)
                    {
                        strOldUnitId = cmbUnit.SelectedValue.ToString();
                    }
                    else
                    {
                        strOldUnitId = "";
                    }
                    frmMasterCreation frmmastercreation = new frmMasterCreation();
                    frmmastercreation.MdiParent = MDIFinacPOS.MDIObj;
                    frmmastercreation.DoWhenComingFromProductCreationForm(this, "Unit");
                    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("PC30" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
        #region NAVIGATIONS
        private void cmbProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbProductName_Leave(sender, e);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    if (txtProductCode.Text.Trim() == "" || txtProductCode.SelectionStart == 0)
                //    {

                //        txtProductNameArabic.Focus();
                //        txtProductNameArabic.SelectionStart = txtProductName.Text.Trim().Length;
                //        txtProductNameArabic.SelectionLength = 0;

                //    }
                //}
                if (e.KeyCode == Keys.Enter)
                {
                    txtProductName.Focus();

                }
                if (e.Control && e.KeyCode == Keys.F)
                {                  
                    btnSearch_Click(sender, e);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC32:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void cmbGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    MessageBox.Show("hi");
                //    e.Handled = true;
                //    cmbGroupCat3.Focus();
                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    cmbGroup.Text = strGroupText;
                    cmbBrand.Focus();

                }
                else if (e.Alt && e.KeyCode == Keys.C)
                {
                    SendKeys.Send("{F10}");
                    btnNewGroup_Click(sender, e);
                }

                DropDownCombo(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC33:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void cmbBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (cmbBrand.Text.Trim() == "" || cmbBrand.SelectionStart == 0)
                //    //{
                //        cmbGroup.Focus();
                //   // }
                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    if (cmbUnit.Enabled)
                        cmbUnit.Focus();
                    else if (cmbTax.Enabled)
                        cmbTax.Focus();
                    else if (cmbTaxType.Enabled)
                        cmbTaxType.Focus();
                    else
                    {
                        txtPurchaseRate.Focus();
                        txtPurchaseRate.SelectionStart = 0;
                        txtPurchaseRate.SelectionLength = 0;
                    }


                }
                else if (e.Alt && e.KeyCode == Keys.C)
                {
                    SendKeys.Send("{F10}");
                    btnNewBrand_Click(sender, e);
                }

                DropDownCombo(e);


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC34:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (cmbUnit.Text.Trim() == "" || cmbUnit.SelectionStart == 0)
                //    //{
                //        cmbBrand.Focus();
                //   // }
                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    cmbUnit.Text = strUnitText;
                    //if (cmbTax.Enabled)
                    //    cmbTax.Focus();
                    //else if (cmbTaxType.Enabled)
                    //    cmbTaxType.Focus();
                    //else
                    //{
                    txtPurchaseRate.Focus();
                    txtPurchaseRate.SelectionStart = 0;
                    txtPurchaseRate.SelectionLength = 0;
                    //}
                }
                else if (e.Alt && e.KeyCode == Keys.C)
                {
                    SendKeys.Send("{F10}");
                    btnNew_Click(sender, e);
                }

                DropDownCombo(e);

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC35:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbTax_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (cmbTax.Text.Trim() == "" || cmbTax.SelectionStart == 0)
                //    //{
                //        txtPurchaseRate.Focus();
                //        txtPurchaseRate.SelectionStart = 0;
                //        txtPurchaseRate.SelectionLength = 0;
                //    //}
                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    cmbTax.Text = strTaxText;
                    //if (cmbTaxType.Visible)
                    //{
                    //    cmbTaxType.Focus();
                    //}
                    //else
                    //{
                    //    txtMinimumStock.Focus();
                    //    txtMinimumStock.SelectionStart = 0;
                    //    txtMinimumStock.SelectionLength = 0;
                    //}
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);

                }

                DropDownCombo(e);



            }
            catch (Exception ex)
            {
                MessageBox.Show("PC36:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbTaxType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{

                //    cmbTax.Focus();
                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    txtPurchaseRate.Focus();
                    txtPurchaseRate.SelectionStart = 0;
                    txtPurchaseRate.SelectionLength = 0;

                }

                DropDownCombo(e);


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC37:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtPurchaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtPurchaseRate.Text.Trim() == "" || txtPurchaseRate.SelectionStart == 0)
                //    //{
                //        //if (cmbTaxType.Enabled)
                //        //{
                //        //    cmbTaxType.Focus();
                //        //}
                //        //else if (cmbTax.Enabled)
                //        //{
                //        //    cmbTax.Focus();
                //        //}
                //        //else
                //        //{
                //            cmbUnit.Focus();
                //            //txtMinimumStock.Focus(); 
                //        //}

                //  //  }
                //}
                //else 
                //if (e.KeyCode == Keys.Enter)
                //{
                    //txtMRP.Focus();
                    //txtMRP.SelectionStart = 0;
                    //txtMRP.SelectionLength = 0;
                    if (e.KeyCode == Keys.Enter)
                    {
                        //if (!txtProductCode.ReadOnly)
                        //{
                        //    txtProductCode.Focus();
                        //    txtProductCode.SelectionStart = 0;
                        //    txtProductCode.SelectionLength = 0;

                        //}
                        //else
                        //{
                        //  cmbProductMainGrp.Focus();//created by sheena on 05-05-2023
                        e.Handled = true;
                        this.SelectNextControl((Control)sender, true, true, true, true);
                        // }

                    }
                    //cmbTax.Focus();
                    //txtMinimumStock.SelectionStart = 0;
                    //txtMinimumStock.SelectionLength = 0;

                //}


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC38:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtMRP_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtMRP.Text.Trim() == "" || txtMRP.SelectionStart == 0)
                //    //{
                //        txtPurchaseRate.Focus();
                //        txtPurchaseRate.SelectionStart = 0;
                //        txtPurchaseRate.SelectionLength = 0;
                //    //}




                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    txtfixedSalesRate.Focus();
                    txtfixedSalesRate.SelectionStart = 0;
                    txtfixedSalesRate.SelectionLength = 0;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC39:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtMinimumStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtMinimumStock.Text.Trim() == "" || txtMinimumStock.SelectionStart == 0)
                //    //{
                //        //txtfixedSalesRate.Focus();
                //        //txtfixedSalesRate.SelectionStart = 0;
                //        //txtfixedSalesRate.SelectionLength = 0;

                //        if (cmbTaxType.Visible)
                //        {
                //            cmbTaxType.Focus();
                //        }
                //        else if (cmbTax.Enabled)
                //        {
                //            cmbTax.Focus();
                //        }
                //        else
                //        {
                //            txtPurchaseRate.Focus();
                //            txtPurchaseRate.SelectionStart = 0;
                //            txtPurchaseRate.SelectionLength = 0;
                //        }

                //  //  }




                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    txtMaximumStock.Focus();
                    txtMaximumStock.SelectionStart = 0;
                    txtMaximumStock.SelectionLength = 0;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC40:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtMaximumStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtMaximumStock.Text.Trim() == "" || txtMaximumStock.SelectionStart == 0)
                //    //{
                //        txtMinimumStock.Focus();
                //        txtMinimumStock.SelectionStart = 0;
                //        txtMinimumStock.SelectionLength = 0;
                //   // }




                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    txtReorderLevel.Focus();
                    txtReorderLevel.SelectionStart = 0;
                    txtReorderLevel.SelectionLength = 0;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC41:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txtReorderLevel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtReorderLevel.Text.Trim() == "" || txtReorderLevel.SelectionStart == 0)
                //    //{
                //        txtMaximumStock.Focus();
                //        txtMaximumStock.SelectionStart = 0;
                //        txtMaximumStock.SelectionLength = 0;
                //   // }
                //}
                //else
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvUnits.Enabled)
                    {
                        dgvUnits.Focus();
                        if (dgvUnits.Rows.Count > 0)
                        {
                            if (dgvUnits.Columns["unit"].Visible)
                            {
                                dgvUnits.CurrentCell = dgvUnits.Rows[0].Cells["unit"];
                                // dgvProduct.CurrentCell.Value = "";
                                dgvUnits.BeginEdit(true);
                            }

                        }

                    }
                    else
                    {
                        //cmbOpeningStock.Focus();
                        e.Handled = true;
                        this.SelectNextControl((Control)sender, true, true, true, true);
                    }
                   

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC42:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbAllowBatch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    if (cmbCategory.Enabled)////created by sheena on 04-05-2023
                //        cmbCategory.Focus();
                //    else
                //    {

                //        txtReorderLevel.Focus();
                //        txtReorderLevel.SelectionStart = 0;
                //        txtReorderLevel.SelectionLength = 0;


                //    }

                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    cmbBom.Focus();

                }

                DropDownCombo(e);


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC43:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbBom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    if (cmbAllowBatch.Enabled)
                //        cmbAllowBatch.Focus();
                //    else
                //    {
                //        cmbCategory.Focus();////created by sheena on 04-05-2023
                //    }



                //}
                //else





                //if (e.KeyCode == Keys.Enter)
                //{
                //    cmbMultipleUnit.Focus();

                //}

                //DropDownCombo(e);


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC44:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbMultipleUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{

                //    cmbBom.Focus();



                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {

                    txtPartNo.Focus();

                }

                DropDownCombo(e);

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC45:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        private void cmbPackage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{

                //    cmbMultipleUnit.Focus();



                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {

                    txtPartNo.Focus();

                }

                DropDownCombo(e);


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void cmbOpeningStock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{


                //    txtPartNo.Focus();
                //    txtPartNo.SelectionStart = 0;
                //    txtPartNo.SelectionLength = 0;

                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                   

                }

                DropDownCombo(e);


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC47:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtNarration_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == 13)
                {
                    inKeyPrsCou++;
                    if (inKeyPrsCou == 2)
                    {
                        inKeyPrsCou = 0;
                        btnSave.Focus();
                    }
                }
                else
                {
                    inKeyPrsCou = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC48:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void txtNarration_Enter(object sender, EventArgs e)
        {
            try
            {
                lblNarration.ForeColor = Color.Red;

                inKeyPrsCou = 0;
                dgvDetails.ClearSelection();
                dgvDetails.CurrentCell = null;
                txtNarration.Text = txtNarration.Text.Trim();
                if (txtNarration.Text == "")
                {
                    txtNarration.SelectionStart = 0;
                    txtNarration.Focus();
                }
                else
                {
                    txtNarration.SelectionStart = txtNarration.Text.Length;
                    txtNarration.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC49:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtNarration.Text == "" || txtNarration.SelectionStart == 0)
                //    //{
                //        if (dgvDetails.Enabled)
                //        {
                //            dgvDetails.Focus();
                //            dgvDetails.Rows[dgvDetails.RowCount - 1].Cells["amount"].Selected = true;
                //        }
                //        else
                //        {
                //            cmbOpeningStock.Focus();
                //        }
                //   // }

                //}
                //cmbOpeningStock.Focus();
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC50:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnClear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{


                //    btnSave.Focus();


                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show("PC52:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnDelete_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{


                //    btnClear.Focus();


                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show("PC53:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {

        }

        #endregion
        #region OTHER EVENTS

        private void frmProductCreation_Load(object sender, EventArgs e)
        {
            //this.Dock = DockStyle.Fill;
            try
            {
                //if (_mainMenuItem == "")
                //    _mainMenuItem = PublicVariables._mainMenuItem;
                isFormLoad = true;
                //if (spUsergroupSettings.CheckUSerGroupPrivilage(PublicVariables._formName, "Delete", _mainMenuItem) == true)
                //{
                    isDeleteEnable = true;
                    btnDelete.Enabled = isDeleteEnable;
                //}
                //else
                //{
                //    isDeleteEnable = false;
                //    btnDelete.Enabled = isDeleteEnable;

                //}

                //bwrkControlSettings.RunWorkerAsync();
                //frmCompanyProgress.ShowInTaskbar = false;
                //frmCompanyProgress.ShowFromReport();

                dgvDetails.Columns["check"].Frozen = true;

                this.cmbTaxType.Items.AddRange(new object[] { "NA" });
                this.cmbTaxType.Items.AddRange(new object[] { "Excluded" });
                this.cmbTaxType.Items.AddRange(new object[] { "Included" });
                

                InitialSettingsForSave();
                CommonInitialSettings();
                if (FinanceSettingsInfo._ActivateTax)
                {

                    if (!dgvSalesPrice.Columns.Contains("TaxAmount"))
                    {
                        DataGridViewTextBoxColumn colTax = new DataGridViewTextBoxColumn();
                        colTax.Name = "TaxAmount";

                        //// Read tax rate from your selected row
                        //string taxRateStr = "0%";
                        //if (taxComboSelectedRow != null && taxComboSelectedRow["rate"] != DBNull.Value)
                        //    taxRateStr = taxComboSelectedRow["rate"].ToString().Trim();  // e.g. "15%"

                        //colTax.HeaderText = FinanceSettingsInfo._VatIncluded || FinanceSettingsInfo._VatandCessIncluded
                        //    ? $"Included Tax ({taxRateStr})"
                        //    : $"Excluded Tax ({taxRateStr})";

                        colTax.ReadOnly = true;
                        dgvSalesPrice.Columns.Add(colTax);
                    }

                }
                if (MDIFinacPOS.clientName != "productImage")
                {
                    btnBrowse.Visible = false;
                    btnClearImg.Visible = false;
                    pbLogo.Visible = false;
                }
                txtProductCode.Focus();
                isFormLoad = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC55:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
        private void frmProductCreation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    if (InventorySettingsInfo._messageBoxClose)
                    {
                        if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else if (e.Control && e.KeyCode == Keys.S)
                {
                    if (txtMaximumStock.Focused)
                    {
                        txtMaximumStock_Leave(sender, e);
                    }
                    else if (txtMinimumStock.Focused)
                    {
                        txtMinimumStock_Leave(sender, e);
                    }
                    else if (txtMRP.Focused)
                    {
                        txtMRP_Leave(sender, e);
                    }
                    else if (txtPurchaseRate.Focused)
                    {
                        txtPurchaseRate_Leave(sender, e);
                    }
                    else if (txtReorderLevel.Focused)
                    {
                        txtReorderLevel_Leave(sender, e);
                    }

                    else if (txtProductCode.Focused)
                    {
                        txtProductCode_Leave(sender, e);
                    }
                    else if (cmbUnit.Focused)
                    {
                        cmbUnit_Leave(sender, e);
                    }
                    else if (cmbTax.Focused)
                    {
                        cmbTax_Leave(sender, e);
                    }
                    else if (cmbGroup.Focused)
                    {
                        cmbGroup_Leave(sender, e);
                    }
                    else if (cmbBrand.Focused)
                    {
                        cmbBrand_Leave(sender, e);
                    }

                    btnSave_Click(sender, e);
                }
                else if (e.Control && e.KeyCode == Keys.D)
                {
                    if (btnDelete.Enabled)
                        btnDelete_Click(sender, e);
                }
                else if (e.Alt && e.KeyCode == Keys.D)
                {
                    SendKeys.Send("{F10}");
                    LinkLabel.Link lnk = new LinkLabel.Link();
                    LinkLabelLinkClickedEventArgs arg = new LinkLabelLinkClickedEventArgs(lnk);
                    lnklblRemove_LinkClicked(sender, arg);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC56:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        bool isPrdFil = true;

        DataTable dtblMultiUnitsNew = new DataTable();
        public void FillProductForEdit(string strProductCode)
        {
            isFormLoad = true;
            if (!isClear)
            {
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Product Creation", "Edit", "Masters") == true)
                //{
                    isEditEnable = true;
                    btnSave.Enabled = true;
                //}
                //else
                //{
                //    isEditEnable = false;
                //    btnSave.Enabled = false;
                //}
              
               
                isPrdFil = false;
                //if (objfrmProductRegister != null && isFromOther)
                //{
                //    InitialSettingsForSave();
                //    CommonInitialSettings();
                //}
                //else  if (objfrmProductRegister == null && !isFromOther)
                //{
                //    InitialSettingsForSave();
                //    CommonInitialSettings();
                //}
                isPrdFil = true;

                strProductIdToEdit = txtProductCode.Text = strProductCode;
                txtProductCode.ReadOnly = true;
                ProductInfo InfoProduct = new ProductInfo();
                InfoProduct = SPProduct.ProductView(txtProductCode.Text);
                txtProductName.Text = InfoProduct.ProductName;

                if (InfoProduct.BrandId != "")
                {
                    cmbBrand.SelectedValue = InfoProduct.BrandId;
                }
                else
                {
                    cmbBrand.SelectedIndex = -1;
                } cmbUnit.SelectedValue = InfoProduct.UnitId;
                strPrvUnitId = InfoProduct.UnitId;

                DataTable dtblGroups = new DataTable(); // added on 20-11-2023 sheena
                dtblGroups = ProductGroupSP.ProductGroupHierarchyViewByGroupId(InfoProduct.GroupId);
                if (dtblGroups.Rows.Count > 0)
                {
                    string[] path = dtblGroups.Rows[0]["path1"].ToString().Split('*');
                    grpHierarchy.Visible = true;
                    cmbGroupCat1.Text = path[1];
                    cmbGroupCat2.Text = path[2];
                    cmbGroupCat3.Text = path[3];
                }
                cmbGroup.SelectedValue = InfoProduct.GroupId;
                cmbMultipleUnit.Text = InfoProduct.MultipleUnit == true ? "Yes" : "No";
                //if (cmbMultipleUnit.Text == "Yes")//---old code commented by sheena on 11-05-2023-----
                //{
                //    dtblMultiUnits = SPUnitConversion.UnitConversionViewByProductCode(strProductIdToEdit);                  
                //}

                //--------created by sheena on 11-05-2023----------------

                dtblMultiUnits = SPUnitConversion.UnitConversionViewByProductCodeAllUnits(strProductIdToEdit);
                dtblMultiUnitsNew = dtblMultiUnits.Copy();

                FillUnitToGridComboColumn();
                txtPartNo.Text = InfoProduct.PartNo;

                txtProductNameArabic.Text = InfoProduct.Extra1;

                //if (SPProduct.ProductReferenceInAllBranchCheck(strProductIdToEdit))//Commented by sheena 20-06-2024
                //{
                //    cmbUnit.Enabled = false;
                //    btnNew.Enabled = false;

                //}
                ProductDetailsInfo InfoProductDetails = new ProductDetailsInfo();
                InfoProductDetails = SPProductDetails.ProductDetailsViewByBranch(strProductIdToEdit);
                if (InfoProductDetails.ProductDetailsId != null)
                {
                    strPrdDtlsIdToEdit = InfoProductDetails.ProductDetailsId;
                    FinacMessage.SaveButtonText(btnSave, "Edit");
                    FinacMessage.ClearButtonText(btnClear, "Edit");
                    btnPrint.Enabled = true;
                    //  btnDelete.Enabled = true;
                    if (isDeleteEnable)//added 19/10/2023
                        btnDelete.Enabled = true;
                    else
                        btnDelete.Enabled = false;
                    cmbAllowBatch.Text = InfoProductDetails.AllowBatch == true ? "Yes" : "No";
                    //-----------------------------------------------------------------------------------
                    if ((((DataTable)cmbTax.DataSource).Select("taxId ='" + InfoProductDetails.TaxId + "'")).Length > 0)
                        cmbTax.SelectedValue = InfoProductDetails.TaxId;
                    else
                    {
                        DataRow drow = ((DataTable)cmbTax.DataSource).NewRow();
                        TaxMasterInfo InfoTaxMaster = SPTaxMaster.TaxMasterView(InfoProductDetails.TaxId);
                        drow["taxId"] = InfoTaxMaster.TaxId;
                        drow["taxName"] = InfoTaxMaster.TaxName;
                        ((DataTable)cmbTax.DataSource).Rows.Add(drow);
                        cmbTax.SelectedValue = InfoProductDetails.TaxId;


                    }

                    //-----------------------------------------------------------------------------------

                    if (cmbTax.Text != "NA")
                    {
                        if (SPProductDetails.TaxReferenceForProductCheck(cmbTax.SelectedValue.ToString(), strProductIdToEdit))
                        {
                            cmbTax.Enabled = false;
                        }
                    }
                    cmbTaxType.Text = InfoProductDetails.TaxType;
                    txtMRP.Text = InfoProductDetails.Mrp == 0m ? "0" : InfoProductDetails.Mrp.ToString();
                    txtfixedSalesRate.Text = InfoProductDetails.FixedSalesRate == 0m ? "0" : InfoProductDetails.FixedSalesRate.ToString();
                    txtPurchaseRate.Text = InfoProductDetails.PurchaseRate == 0m ? "0" : InfoProductDetails.PurchaseRate.ToString(FinanceSettingsInfo._roundDecimalPart);
                    txtMinimumStock.Text = InfoProductDetails.MinimumStock == 0m ? "0" : InfoProductDetails.MinimumStock.ToString();
                    txtMaximumStock.Text = InfoProductDetails.MaximunStock == 0m ? "0" : InfoProductDetails.MaximunStock.ToString();
                    txtReorderLevel.Text = InfoProductDetails.ReorderLevel == 0m ? "0" : InfoProductDetails.ReorderLevel.ToString();
                    cmbBom.Text = InfoProductDetails.Bom == true ? "Yes" : "No";
                    cbxGatePass.Checked = InfoProductDetails.GatePass;
                    if (cmbBom.Text == "Yes")
                    {
                        dtblRowMaterials = SPBOM.BOMViewByProductDetailsId(strPrdDtlsIdToEdit);

                        InitialSettingsAccordingToCallingForm();
                    }
                    else if (SPProductDetails.ProductReferenceInBOMCheck(txtProductCode.Text))
                    {
                        cmbBom.Enabled = false;
                    }

                    cmbOpeningStock.Text = InfoProductDetails.OpeningStock == 0m ? "No" : "Yes";
                    cbxReminder.Checked = InfoProductDetails.ShowReminder;
                    cbxActive.Checked = InfoProductDetails.Active;
                    if (cbxActive.Checked)
                    {
                        if (!SPProduct.ProductDelete(InfoProductDetails.ProductCode, false))
                        {
                            cbxActive.Enabled = false;
                           // cmbUnit.Enabled = false;
                            btnNew.Enabled = false;
                        }
                    }

                    txtNarration.Text = InfoProductDetails.Narration;
                    cmbCategory.Text = InfoProductDetails.Category;//new field ////created by sheena on 04-05-2023
                    cmbProductMainGrp.SelectedValue = InfoProductDetails.GroupCode.ToString();//new field ////created by sheena on 05-05-2023
                    chkIsVanSale.Checked = InfoProductDetails.IsVanSaleProduct;//new field ////created by sheena on 30-05-2024
                    chkShowExpiry.Checked = InfoProductDetails.ShowExpiry;
                    txtExpiryDays.Text = InfoProductDetails.ExpiryDays.ToString() ;
                    txtPercentage.Text = InfoProductDetails.PurchaseRatePer.ToString();

                    #region Nutrition Fact

                    grpNutritionFact.Visible= InfoProductDetails.NutritionFact;
                    txtIngredients.Text=InfoProductDetails.Ingredients;
                    chkNutritionFact.Checked = InfoProductDetails.NutritionFact;
                    txtName.Text = InfoProductDetails.NutritionName;

                    if (!string.IsNullOrEmpty(InfoProductDetails.NutritionDetails))
                    {
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(InfoProductDetails.NutritionDetails);

                        foreach (DataRow drowObj in dt.Rows)
                        {
                            dgvNutritionFacts.Rows.Add();
                            dgvNutritionFacts.Rows[dgvNutritionFacts.Rows.Count-2 ].Cells["NutName"].Value = drowObj["NutName"].ToString();
                            dgvNutritionFacts.Rows[dgvNutritionFacts.Rows.Count-2 ].Cells["Value"].Value = drowObj["Value"].ToString();
                        }
                    }
                    #endregion
                    //29/Jul/2025
                    txtLocation.Text = InfoProductDetails.Location;
                    txtAlternateNo.Text = InfoProductDetails.alternateNo;
                    chkWithoutBarcode.Checked = InfoProductDetails.WithOutBarcode;

                    //destinationPath = oldDestinationPath = InfoProduct.ProductImage;
                    //DirectoryInfo drinfo = new DirectoryInfo(Application.StartupPath + "\\ProductImage");
                    //if (!drinfo.Exists)
                    //{
                    //    drinfo.Create();
                    //}
                    //if (destinationPath != "" && destinationPath != null)
                    //{
                    //    if (File.Exists(destinationPath))
                    //    {
                    //        tempLogo = ReadFile(destinationPath);
                    //        MemoryStream ms = new MemoryStream(tempLogo);
                    //        Image newImage = Image.FromStream(ms);
                    //        pbLogo.Image = newImage;
                    //        ms.Close();
                    //    }
                    //}
                    if (InventorySettingsInfo._ShowGodownWiseStock)
                        FillStockList(strProductCode);
                    else
                        lstStock.Visible = false;

                    string imagePath = InfoProduct.ProductImage; // This could be a local path or a URL
                                                                 //  string baseUrl = txtApiUrl.Text.Trim().TrimEnd('/');
                    if (MDIFinacPOS.DBLocation == "Cloud")
                    {
                        string strServer = ".\\sqlExpress";
                        // string strPrimaryDbName = "DBFINACACCOUNT";
                        if (File.Exists(Application.StartupPath + "\\sys.txt"))
                        {
                            string DbData = File.ReadAllText(Application.StartupPath + "\\sys.txt"); ;
                            string[] values = DbData.Split(',');

                            strServer = values[0].Trim();
                            // strPrimaryDbName = values[1].Trim();

                            //strServer = File.ReadAllText(Application.StartupPath + "\\sys.txt"); // getting ip of server
                        }
                        // Load from API
                        string uploadUrl = $"http://{strServer}:666/api/ProductImage/GetProductImage?dbName={PublicVariables._companyDatabaseName}&productCode={InfoProductDetails.ProductCode}"; // your API endpoint //DbBackupDownload is the website in Remote IID

                        try
                        {
                            pbLogo.Load(uploadUrl); // auto handles stream
                            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        catch (Exception ex)
                        {
                            GetDefaultImage();
                        }
                    }
                    else if (MDIFinacPOS.DBLocation == "Local")
                    {
                        destinationPath = oldDestinationPath = InfoProduct.ProductImage;

                        DirectoryInfo drinfo = new DirectoryInfo(Application.StartupPath + "\\ProductImage");
                        if (!drinfo.Exists)
                        {
                            drinfo.Create();
                        }

                        if (!string.IsNullOrEmpty(destinationPath))
                        {
                            FileInfo fi = new FileInfo(destinationPath);

                            if (fi.Exists && fi.Length > 0)
                            {
                                try
                                {
                                    byte[] tempLogo = File.ReadAllBytes(destinationPath);
                                    using (MemoryStream ms = new MemoryStream(tempLogo))
                                    {
                                        pbLogo.Image = Image.FromStream(ms);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    GetDefaultImage();
                                    //MessageBox.Show("Error loading image: " + ex.Message);
                                    //pbLogo.Image = Properties.Resources.DefaultImage; // Replace with your default
                                }
                            }
                            else
                            {
                                GetDefaultImage();
                                //pbLogo.Image = Properties.Resources.DefaultImage; // Replace with your default
                            }
                        }
                    }
                    if (cmbOpeningStock.Text == "Yes")
                    {

                        DataTable dtblDetails = SPStockPosting.StPostingViewByVoucherTypeAndVoucherNumberAndBranch(txtProductCode.Text, "Opening Stock");
                        if (dtblDetails.Rows.Count > 0)
                        {
                            if (dtblDetails.Columns.Contains("date"))
                                StPostingDate = dtblDetails.Rows[0]["date"].ToString();
                        }
                        foreach (DataRow drowObj in dtblDetails.Rows)
                        {
                            dgvDetails.Rows.Add();
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["id"].Value = drowObj.ItemArray[0].ToString();
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["godown"].Value = drowObj.ItemArray[6].ToString();
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["rack"].Value = drowObj.ItemArray[17].ToString();
                            if (dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["batch"].Visible)
                            {
                                dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["batch"];
                            }
                            else if ((drowObj.ItemArray[16].ToString() == "" ? "NA" : drowObj.ItemArray[16].ToString()) != "NA")
                            {
                                dgvDetails.Columns["batch"].Visible = true;
                                dgvDetails.Columns["mfd"].Visible = true;
                                dgvDetails.Columns["expd"].Visible = true;
                                dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["batch"];
                            }
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["batch"].Value = drowObj.ItemArray[16].ToString();
                            if (dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["batch"].Value.ToString() == "NA")
                            {
                                dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["mfd"].ReadOnly = true;
                                dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["expd"].ReadOnly = true;

                            }
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["qty"].Value = drowObj.ItemArray[7].ToString();
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["unitPerQty"].Value = drowObj.ItemArray[10].ToString();
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["unitPerRate"].Value = drowObj.ItemArray[10].ToString();
                            dgvDetails.CurrentCell = dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["rate"];
                            dgvDetails.Rows[dgvDetails.Rows.Count - 2].Cells["rate"].Value = drowObj.ItemArray[9].ToString();
                        }
                        dgvDetails.ClearSelection();
                        dgvDetails.CurrentCell = null;
                    }

                    /////--------Fill unit grid with barcode----------created by sheena on 11-05-2023------------------
                    dgvUnits.Rows.Clear();
                    if (dtblMultiUnitsNew.Rows.Count > 0)
                    {
                        foreach (DataRow drowObj in dtblMultiUnitsNew.Rows)
                        {
                            dgvUnits.Rows.Add();
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["unitConversionId"].Value = drowObj.ItemArray[5].ToString();
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["unit"].Value = drowObj.ItemArray[1].ToString();
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["defaultQty"].Value = drowObj.ItemArray[0].ToString();
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["barcode"].Value = drowObj.ItemArray[4].ToString();
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["unitStatus"].Value = "IsFromDb";
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["oldbarcode"].Value = drowObj.ItemArray[4].ToString();
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["oldBarcodeUnit"].Value = drowObj.ItemArray[1].ToString();
                            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["Description"].Value = drowObj["Description"].ToString();
                        }
                    }
                    //-----------------Fill salesprice grid--------------- created by sheena 13-05-2023---------------
                    dgvSalesPrice.Rows.Clear();
                    dtblsalesprice = new DataTable();
                    ProductSalesPriceSP PriceSP = new ProductSalesPriceSP();
                    dtblsalesprice = PriceSP.ProductSalesPriceByProductCode(strProductIdToEdit);
                   // objTransactionsGeneralFill.FillUnitToGridComboColumn(UnitId);
                   // objTransactionsGeneralFill.FillPricingLevelGridCombo(pricingLevelId);
                    
                    //dgvSalesPrice.DataSource = dtblsalesprice;
                    isEditFill = true;
                    foreach (DataRow drowObj in dtblsalesprice.Rows)
                    {
                        txtSalesPrice.Text = drowObj.ItemArray[7].ToString(); ;
                        dgvSalesPrice.Rows.Add();
                        isSalesValueChange = false;
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["UnitId"].Value = drowObj.ItemArray[2].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["PriceAmount"].Value = drowObj.ItemArray[4].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["DiscPercentage"].Value = drowObj.ItemArray[5].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["DiscAmount"].Value = drowObj.ItemArray[6].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["SalesPrice"].Value = drowObj.ItemArray[7].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["pricingLevelId"].Value = drowObj["pricingLevelId"].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["status"].Value = "IsFromDb";
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["oldSalesPrice"].Value = drowObj.ItemArray[7].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["salespriceId"].Value = drowObj.ItemArray[0].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["oldSalesUnit"].Value = drowObj.ItemArray[2].ToString();
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["costPrice"].Value =Math.Round(decimal.Parse( drowObj["costPrice"].ToString()),FinanceSettingsInfo._roundDecimal);
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["marginPercentage"].Value = Math.Round(decimal.Parse(drowObj["marginPercentage"].ToString()), FinanceSettingsInfo._roundDecimal);
                        dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2].Cells["LowestSellingPrice"].Value = Math.Round(decimal.Parse(drowObj["LowestSellingPrice"].ToString()), FinanceSettingsInfo._roundDecimal);
                        RecalculateSalesAndTax(dgvSalesPrice.Rows[dgvSalesPrice.Rows.Count - 2]);
                        isSalesValueChange = true;
                    }
                    isEditFill = false;
                    isGridFill = true;

                    dgvProductPurchasePrice.Rows.Clear();
                    DataTable dtblProductPurchasePrice = new DataTable();
                    ProductPurchasePriceSP spProductPurchasePrice = new ProductPurchasePriceSP();

                    dtblProductPurchasePrice = spProductPurchasePrice.ProductPurchasePriceView(strProductIdToEdit);
                    if(dtblProductPurchasePrice.Rows.Count > 0)
                    {
                        objTransactionsGeneralFill.FillUnitToGridComboColumn(ppp_unit);
                        dgvProductPurchasePrice.Rows.Clear();
                        for (int i = 0; i < dtblProductPurchasePrice.Rows.Count;i++ )
                        {
                            dgvProductPurchasePrice.Rows.Add();
                            isPurchaseValueChange = false;
                            objTransactionsGeneralFill.FillUnitPerQtyComboForGridProductPurchasePrice(dgvProductPurchasePrice, strProductIdToEdit, i);
                           
                            //dgvProductPurchasePrice.Rows[i].Cells["ppp_slno"].Value = i+1;
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_purchasepriceid"].Value = dtblProductPurchasePrice.Rows[i]["purchasePriceId"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value = dtblProductPurchasePrice.Rows[i]["ledgerId"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_lrdgername"].Value = dtblProductPurchasePrice.Rows[i]["ledgerName"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].Value = dtblProductPurchasePrice.Rows[i]["unitId"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_amount"].Value = dtblProductPurchasePrice.Rows[i]["amount"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value = dtblProductPurchasePrice.Rows[i]["discPercentage"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value = dtblProductPurchasePrice.Rows[i]["discAmount"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_purchaseprice"].Value = dtblProductPurchasePrice.Rows[i]["purchasePrice"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_date"].Value = dtblProductPurchasePrice.Rows[i]["purchasedate"].ToString();
                            dgvProductPurchasePrice.Rows[i].Cells["ppp_purchasemasterid"].Value = dtblProductPurchasePrice.Rows[i]["purchasemasterid"].ToString();
                            isPurchaseValueChange = true;
                        }   
                    }
                }
                else
                {

                }

                isClear = true;
            }
            isFormLoad = false;
        }
        private void FillStockList(string strprodutCode)
        {
            try
            {
                lstStock.Visible= true;
                lstStock.Items.Clear();

                // Create and fill DataTable
                DataTable dt = new DataTable();

                dt = SPProduct.GetProductGodownWiseStock(strprodutCode);
               
                // Loop through DataTable and add to ListBox
                foreach (DataRow row in dt.Rows)
                {
                    string godown = row["godownName"].ToString();
                    decimal stock = Convert.ToDecimal(row["CurrentStock"]);

                    lstStock.Items.Add($"  {godown}  |  Stock: {stock}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stock: " + ex.Message);
            }
        }

        private void cmbTax_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cmbTax.SelectedValue == null ? "System.Data.DataRowView" : cmbTax.SelectedValue.ToString()) != "System.Data.DataRowView")
                {
                    if (cmbTax.SelectedValue.ToString() == "1")
                    {
                        if (!cmbTaxType.Items.Contains("NA"))
                        {
                            cmbTaxType.Items.Insert(0, "NA");

                        }
                        cmbTaxType.Text = "NA";
                        cmbTaxType.Enabled = false;
                    }
                    else
                    {
                        if (cmbTaxType.Items.Contains("NA"))
                        {
                            for (int inI = 0; inI < cmbTaxType.Items.Count; inI++)
                            {
                                if (cmbTaxType.Items[inI].ToString() == "NA")
                                {
                                    cmbTaxType.Items.RemoveAt(inI);
                                    inI--;
                                }
                            }
                        }
                        if (FinanceSettingsInfo._VatIncluded == true || FinanceSettingsInfo._VatandCessIncluded == true)
                        {
                            cmbTaxType.Text = "Included";
                        }
                        else
                        {
                            cmbTaxType.Text = "Excluded";
                        }

                        cmbTaxType.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC58:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbProductName_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC59:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        private void cmbOpeningStock_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOpeningStock.Text == "Yes")
                {
                    dgvDetails.Enabled = true;
                }
                else
                {
                    dgvDetails.Enabled = false;
                    dgvDetails.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC60:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbOpeningStock_Enter(object sender, EventArgs e)
        {
            try
            {
                lblOpeningStock.ForeColor = Color.Red;

                dgvDetails.ClearSelection();
                dgvDetails.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC61:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void cmbGroup_Leave(object sender, EventArgs e)
        {
            lblGroup.ForeColor = Color.Black;

            if (cmbGroup.SelectedIndex == -1)
            {
                cmbGroup.Text = strGroupText;
            }
            objComboValidation.CheckCollection(cmbGroup);
        }

        private void cmbBrand_Leave(object sender, EventArgs e)
        {

            lblBrand.ForeColor = Color.Black;

            if (cmbBrand.SelectedIndex == -1)
                cmbBrand.Text = strBrandText;
            objComboValidation.CheckCollection(cmbBrand);
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                lblUnit.ForeColor = Color.Black;

                if (cmbUnit.SelectedIndex == -1)
                    cmbUnit.Text = strUnitText;

                objComboValidation.CheckCollection(cmbUnit);
                if ((cmbUnit.SelectedValue == null ? "System.Data.DataRowView" : cmbUnit.SelectedValue.ToString()) == "System.Data.DataRowView")
                {
                    cmbMultipleUnit.Text = "No";
                    dtblMultiUnits = new DataTable();
                    FillUnitToGridComboColumn();
                }
                else
                {

                    if (strPrvUnitId == "")
                    {
                        strPrvUnitId = cmbUnit.SelectedValue.ToString();
                    }
                    else
                    {
                        if (strPrvUnitId != cmbUnit.SelectedValue.ToString())
                        {
                            cmbMultipleUnit.Text = "No";
                            dtblMultiUnits = new DataTable();
                            FillUnitToGridComboColumn();
                            strPrvUnitId = cmbUnit.SelectedValue.ToString();
                        }
                    }
                    if (unitPerQty.DataSource == null || unitPerRate.DataSource == null)
                    {
                        FillUnitToGridComboColumn();
                    }


                }
                if (dgvUnits.RowCount == 1 && strUnitText!="")
                {
                    dgvUnits.Rows.Add();
                  
                    try
                    {
                        dgvUnits.Rows[0].Cells["unit"].Value = strPrvUnitId;
                    }
                    catch { }
                }
                //if (dgvSalesPrice.RowCount == 1 && strUnitText != "")
                //{
                //    dgvSalesPrice.Rows.Add();
                    
                //    try
                //    {
                //        dgvSalesPrice.Rows[0].Cells["UnitId"].Value = strPrvUnitId;
                //    }
                //    catch { }
                //    dgvSalesPrice.Rows[0].Cells["PriceAmount"].Value = 0;
                //    dgvSalesPrice.Rows[0].Cells["DiscAmount"].Value = 0;
                //    dgvSalesPrice.Rows[0].Cells["DiscPercentage"].Value = 0;
                //    dgvSalesPrice.Rows[0].Cells["SalesPrice"].Value = 0;
                //    dgvSalesPrice.Rows[0].Cells["pricingLevelId"].Value = "1";//Default pricinglevel
                //    //dgvSalesPrice.Rows[1].Cells["pricingLevelId"].Value = "1";//Default pricinglevel
                //    isGridFill = true;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC62:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
        {

            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {

            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtMinimumStock_KeyPress(object sender, KeyPressEventArgs e)
        {

            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtMaximumStock_KeyPress(object sender, KeyPressEventArgs e)
        {

            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtReorderLevel_KeyPress(object sender, KeyPressEventArgs e)
        {

            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtMRP_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtMRP.Text);
            }
            catch
            {
                txtMRP.Text = "0";
            }

        }

        private void txtPurchaseRate_Leave(object sender, EventArgs e)
        {
            try
            {
                lblPurchaserate.ForeColor = Color.Black;

                decimal.Parse(txtPurchaseRate.Text);
            }
            catch
            {
                txtPurchaseRate.Text = "0";
            }
        }

        private void txtMinimumStock_Leave(object sender, EventArgs e)
        {
            try
            {

                lblMinStock.ForeColor = Color.Black;
                decimal.Parse(txtMinimumStock.Text);
            }
            catch
            {
                txtMinimumStock.Text = "0";
            }
        }

        private void txtMaximumStock_Leave(object sender, EventArgs e)
        {
            try
            {
                lblMaxStock.ForeColor = Color.Black;

                decimal.Parse(txtMaximumStock.Text);
            }
            catch
            {
                txtMaximumStock.Text = "0";
            }
        }

        private void txtReorderLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                lblReOrderLevel.ForeColor = Color.Black;

                decimal.Parse(txtReorderLevel.Text);
            }
            catch
            {
                txtReorderLevel.Text = "0";
            }
        }





        private void cmbUnit_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((cmbUnit.SelectedValue == null ? "System.Data.DataRowView" : cmbUnit.SelectedValue.ToString()) != "System.Data.DataRowView")
            {


            }
        }

        private void dgvDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBoxControl = e.Control as DataGridViewTextBoxEditingControl;
                if (TextBoxControl != null)
                {
                    TextBoxControl.KeyPress += TextBoxCellEditControlKeyPress;
                    if (dgvDetails.CurrentCell != null && dgvDetails.Columns[dgvDetails.CurrentCell.ColumnIndex].Name == "batch")
                    {
                        TextBoxControl = e.Control as DataGridViewTextBoxEditingControl;
                        DataGridViewTextBoxEditingControl te = (DataGridViewTextBoxEditingControl)e.Control;
                        TextBoxControl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        TextBoxControl.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        FillBatch(TextBoxControl);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC63:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void FillBatch(DataGridViewTextBoxEditingControl TextBoxControl)
        {
            // To fill batch
            BatchSP SpBatch = new BatchSP();
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            collection = SpBatch.BatchViewAllForList();
            foreach (DataGridViewRow dgvrowObj in dgvDetails.Rows)
            {
                if ((dgvrowObj.Cells["batch"].Value == null ? "" : dgvrowObj.Cells["batch"].Value.ToString()) != "")
                {
                    ListBox lstbxBatch = new ListBox();
                    if (!collection.Contains(dgvrowObj.Cells["batch"].Value.ToString()))
                    {
                        collection.Add(dgvrowObj.Cells["batch"].Value.ToString());
                    }

                }
            }
            TextBoxControl.AutoCompleteCustomSource = collection;
        }
        private void TextBoxCellEditControlKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dgvDetails.CurrentCell != null)
                {
                    if (dgvDetails.Columns[dgvDetails.CurrentCell.ColumnIndex].Name == "qty" || dgvDetails.Columns[dgvDetails.CurrentCell.ColumnIndex].Name == "rate")
                    {
                        objComboValidation.DecimalValidationGRid(sender, e, TextBoxControl, false);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC64:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        string strUnitPerQtyId = "", strUnitPerRateId = "";

        decimal dQty = 0, dRate = 0;

        bool isForeach = false, isNoEntry = false;
        private void dgvDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex != -1)
                {
                    if (!isNoEntry)
                    {
                        decimal dRateConversion = 1;


                        if (dgvDetails.Columns[e.ColumnIndex].Name == "godown")
                        {
                            FillRackAccordingToGodown(e.RowIndex, e.ColumnIndex);

                        }
                        //

                        //-----------------batch list box display settings-------------------------------------------------------------------------

                        if (dgvDetails.Columns[e.ColumnIndex].Name == "batch")
                        {
                            string strSearchContent = "";



                            string strBatch = dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ? "" : dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            if (strBatch.ToLower().Trim() == "na")// by sumana
                            {

                                dgvDetails.Rows[e.RowIndex].Cells["mfd"].Value = "";
                                dgvDetails.Rows[e.RowIndex].Cells["expd"].Value = "";
                                dgvDetails.Rows[e.RowIndex].Cells["mfd"].ReadOnly = true;
                                dgvDetails.Rows[e.RowIndex].Cells["expd"].ReadOnly = true;

                            }
                            else
                            {
                                if (strBatch != "")
                                {
                                    BatchInfo InfoBatch = new BatchInfo();
                                    InfoBatch = SPBatch.BatchViewByName(strBatch);
                                    if (InfoBatch.BatchId != null)
                                    {
                                        dgvDetails.Rows[e.RowIndex].Cells["mfd"].Value = InfoBatch.Mfd.ToString("dd-MMM-yyyy");
                                        dgvDetails.Rows[e.RowIndex].Cells["expd"].Value = InfoBatch.Expd.ToString("dd-MMM-yyyy");
                                    }
                                }
                                dgvDetails.Rows[e.RowIndex].Cells["mfd"].ReadOnly = false;
                                dgvDetails.Rows[e.RowIndex].Cells["expd"].ReadOnly = false;
                            }

                            MFDandEXPDSettings(e.RowIndex);

                        }
                        //-----------------------------date settings and checking-------------------------------------------------
                        else if (dgvDetails.Columns[e.ColumnIndex].Name == "mfd" || dgvDetails.Columns[e.ColumnIndex].Name == "expd")
                        {
                            TextBox txt = new TextBox();
                            if (dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                            {
                                txt.Text = "";
                            }
                            else
                            {
                                txt.Text = dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            }
                            objDate.DateValidationFunction(txt, false);
                            dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = txt.Text;
                            if ((dgvDetails.Rows[e.RowIndex].Cells["mfd"].Value == null ? "" : dgvDetails.Rows[e.RowIndex].Cells["mfd"].Value.ToString()) != "")
                                if ((dgvDetails.Rows[e.RowIndex].Cells["expd"].Value == null ? "" : dgvDetails.Rows[e.RowIndex].Cells["expd"].Value.ToString()) != "")
                                {
                                    if (DateTime.Parse(dgvDetails.Rows[e.RowIndex].Cells["expd"].Value.ToString()) < DateTime.Parse(dgvDetails.Rows[e.RowIndex].Cells["mfd"].Value.ToString()))
                                    {

                                        dgvDetails.Rows[e.RowIndex].Cells["expd"].Value = "";
                                    }
                                }
                            MFDandEXPDSettings(e.RowIndex);
                        }
                        //---------------------------checking for ok row-----------------------------------------------
                        OkRowChecking(e);

                        //------------------------------------convert qty,free to default unit,corresponding change to amount too----------------------------------------------------------------------------------

                        if (dgvDetails.Columns[e.ColumnIndex].Name == "unitPerQty")
                        {
                            if (strUnitPerQtyId != "")
                            {

                                if (dQty != 0m)
                                {
                                    dRateConversion = ConversionRate(strUnitPerQtyId);
                                    dgvDetails.Rows[e.RowIndex].Cells["qty"].Value = (Math.Round((dQty / dRateConversion), 2));

                                    dgvDetails.Rows[e.RowIndex].Cells["amount"].Value = Math.Round(((dQty * dRate) / dRateConversion), 2);
                                    dgvDetails.Rows[e.RowIndex].Cells["unitPerQty"].Value = cmbUnit.SelectedValue.ToString();
                                }



                            }
                        }
                        //------------------------------convert rate to default unit,corresponding change to amount too-------------------------------------------
                        else if (dgvDetails.Columns[e.ColumnIndex].Name == "unitPerRate")
                        {
                            if (strUnitPerRateId != "")

                                if (dRate != 0m)
                                {

                                    dRateConversion = ConversionRate(strUnitPerRateId);
                                    dgvDetails.Rows[e.RowIndex].Cells["rate"].Value = Math.Round((dRate * dRateConversion), 2);
                                    dgvDetails.Rows[e.RowIndex].Cells["amount"].Value = Math.Round(((dQty * dRate) * dRateConversion), 2);
                                    dgvDetails.Rows[e.RowIndex].Cells["unitPerRate"].Value = cmbUnit.SelectedValue.ToString();
                                }
                        }


                        //-------------------------while changing Qty,corresponding change in amount---------------------
                        else if (dgvDetails.Columns[e.ColumnIndex].Name == "qty")
                        {

                            dgvDetails.Rows[e.RowIndex].Cells["amount"].Value = Math.Round((dQty * dRate), 2);
                        }
                        //-------------------------while chnaging Rate,corresponding change in amount---------------------
                        else if (dgvDetails.Columns[e.ColumnIndex].Name == "rate")
                        {

                            dgvDetails.Rows[e.RowIndex].Cells["amount"].Value = Math.Round((dQty * dRate), 2);
                        }
                        if (!isForeach)
                        {
                            foreach (DataGridViewRow dgvRowObj in dgvDetails.Rows)
                            {
                                isForeach = true;
                                if (0 != dgvRowObj.Index)
                                {
                                    if (dgvRowObj.IsNewRow)
                                    {
                                        isNoEntry = true;
                                    }
                                    else
                                    {
                                        isNoEntry = false;
                                    }
                                    dgvRowObj.Cells["rate"].Value = dgvDetails.Rows[0].Cells["rate"].Value == null ? "" : dgvDetails.Rows[0].Cells["rate"].Value.ToString();
                                    dgvRowObj.Cells["unitPerRate"].Value = dgvDetails.Rows[0].Cells["unitPerRate"].Value == null ? "" : dgvDetails.Rows[0].Cells["unitPerRate"].Value.ToString();


                                }
                            }

                            isForeach = false;
                        }
                        isNoEntry = false;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC65:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbMultipleUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                lblMultipleUnit.ForeColor = Color.Black;
                if (cmbMultipleUnit.Text == "Yes")
                {
                    if (!isClose && !btnClear.Focused && !btnClose.Focused && MDIFinacPOS.MDIObj.ActiveMdiChild == this)
                    {
                        if ((cmbUnit.SelectedValue == null ? "System.Data.DataRowView" : cmbUnit.SelectedValue.ToString()) != "System.Data.DataRowView")
                        {
                            //DataTable dtblToPopUp = dtblMultiUnits.Copy();
                            //if (dtblToPopUp.Rows.Count > 0)
                            //{
                            //    dtblToPopUp.Rows.RemoveAt(0);
                            //}
                            //frmMultipleUnit frmMultipleUnitObj = new frmMultipleUnit();
                            //  frmMultipleUnitObj.DoWhenComingFromProductCreation(this, cmbUnit.SelectedValue.ToString(), dtblToPopUp);
                        }
                        else
                        {
                            cmbMultipleUnit.Text = "No";
                            MessageBox.Show("Select default unit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbUnit.Focus();
                        }
                    }
                }
                else
                {
                    dtblMultiUnits = new DataTable();
                    FillUnitToGridComboColumn();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC66:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbTax_Leave(object sender, EventArgs e)
        {
            try
            {

                lblTax.ForeColor = Color.Black;

                if (cmbTax.SelectedIndex == -1)
                    cmbTax.Text = strTaxText;

                objComboValidation.CheckCollection(cmbTax);
                if (cmbTax.Text == "")
                {
                    if (((DataTable)cmbTax.DataSource).Select("taxId ='" + 1 + "'").Length > 0)
                    {
                        cmbTax.SelectedValue = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC67:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbProductName_TextChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            try
            {
                if (cmb.Text.Trim() == "")
                {

                    cmb.Text = cmb.Text.Trim();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC68:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbAllowBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAllowBatch.Text == "Yes")
                {
                    dgvDetails.Columns["batch"].Visible = true;
                    dgvDetails.Columns["mfd"].Visible = true;
                    dgvDetails.Columns["expd"].Visible = true;

                }
                else
                {
                    dgvDetails.Columns["batch"].Visible = false;
                    dgvDetails.Columns["mfd"].Visible = false;
                    dgvDetails.Columns["expd"].Visible = false;

                }
                foreach (DataGridViewRow dgvRow in dgvDetails.Rows)
                {
                    if (dgvRow.IsNewRow)
                    {
                        isToNotDo = true;
                    }
                    dgvRow.Cells["batch"].Value = null;
                    dgvRow.Cells["mfd"].Value = "";
                    dgvRow.Cells["expd"].Value = "";
                    isToNotDo = false;

                }
                if (dgvDetails.Enabled)
                {
                    foreach (DataGridViewRow dgvrowObj in dgvDetails.Rows)
                    {
                        if (!dgvrowObj.IsNewRow)
                        {
                            DataGridViewCellEventArgs dgvEv = new DataGridViewCellEventArgs(0, dgvrowObj.Index);
                            OkRowChecking(dgvEv);
                        }
                    }
                    dgvDetails.ClearSelection();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC69:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void cmbBom_Leave(object sender, EventArgs e)
        {
            try
            {
            lblBOM.ForeColor = Color.Black;

            //    if (cmbBom.Text == "Yes")
            //    {
            //        if (!isClose && !btnClear.Focused && MDIFinacAcount.MDIObj.ActiveMdiChild == this)
            //            if (!btnClose.Focused)
            //            {
            //                if (txtProductName.Text != "")
            //                {
            //                    frmBillofMaterials frmBillofMaterialsObj = new frmBillofMaterials();
            //                    frmBillofMaterialsObj.DoWhenComingFromProductCreation(this, txtProductName.Text, (txtProductCode.Text), dtblRowMaterials, "BOM");
            //                }
            //                else
            //                {
            //                    cmbBom.Text = "No";
            //                    MessageBox.Show("Enter product name", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    txtProductName.Focus();

            //                }
            //            }
            //            else
            //            {
            //                if (dtblRowMaterials.Rows.Count == 0)
            //                {
            //                    cmbBom.Text = "No";
            //                }
            //            }

            //    }
            //    else
            //    {
            //        if (SPProductDetails.ProductBOMReferenceCheck(txtProductCode.Text))
            //        {
            //            cmbBom.Text = "Yes";
            //            MessageBox.Show("Can't chnage , reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        }
            //        else
            //        {
            //            dtblRowMaterials = new DataTable();
            //        }
            //    }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC70:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbPackage_Leave(object sender, EventArgs e)
        {

        }

        #endregion

        private void frmProductCreation_FormClosing(object sender, FormClosingEventArgs e)
        {
            isClose = true;
           // CheckWhenQuiting();
        }
        public string GoogleTranslate(string inputtext, string fromlangid, string tolangid)
        {
            string step4 = "";
            try
            {
                inputtext = HttpUtility.HtmlAttributeEncode(inputtext);
                using (WebClient step1 = new WebClient())
                {
                    step1.Encoding = Encoding.UTF8;
                    string step2 = step1.DownloadString("https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=" + tolangid + "&hl=" + fromlangid + "&dt=t&dt=bd&dj=1&source=icon&q=" + inputtext);
                    Newtonsoft.Json.Linq.JObject step3 = Newtonsoft.Json.Linq.JObject.Parse(step2);
                    step4 = step3.SelectToken("sentences[0]").SelectToken("trans").ToString();
                    
                }
            }
            catch 
            {
                
                
            }
            return step4;
        }
        private void dgvDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvDetails.CurrentRow != null)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (dgvDetails.Rows.Count > 1 && dgvDetails.CurrentRow.Index == dgvDetails.Rows.Count - 1)
                        {
                            bool isNavigate = false;
                            if (InventorySettingsInfo._maintainGodown)
                            {
                                if ((dgvDetails.CurrentRow.Cells["godown"].Value == null ? "" : dgvDetails.CurrentRow.Cells["godown"].Value.ToString()) == "" && (dgvDetails.CurrentCell == dgvDetails.CurrentRow.Cells["godown"]))
                                {
                                    isNavigate = true;
                                }
                            }
                            else if (InventorySettingsInfo._maintainRack)
                            {
                                if ((dgvDetails.CurrentRow.Cells["rack"].Value == null ? "" : dgvDetails.CurrentRow.Cells["rack"].Value.ToString()) == "" && (dgvDetails.CurrentCell == dgvDetails.CurrentRow.Cells["rack"]))
                                {
                                    isNavigate = true;
                                }
                            }
                            else if (dgvDetails.Columns["batch"].Visible)
                            {
                                if ((dgvDetails.CurrentRow.Cells["batch"].Value == null ? "" : dgvDetails.CurrentRow.Cells["batch"].Value.ToString()) == "" && (dgvDetails.CurrentCell == dgvDetails.CurrentRow.Cells["batch"]))
                                {
                                    isNavigate = true;
                                }
                            }
                            else if ((dgvDetails.CurrentRow.Cells["rate"].Value == null ? "" : dgvDetails.CurrentRow.Cells["rate"].Value.ToString()) == "" && (dgvDetails.CurrentCell == dgvDetails.CurrentRow.Cells["rate"]))
                            {
                                isNavigate = true;
                            }
                                
                            if (dgvDetails.CurrentCell == dgvDetails.CurrentRow.Cells["amount"])
                            {
                                isNavigate = true;
                            }
                            if (isNavigate)
                            {
                                txtNarration.Focus();
                                dgvDetails.ClearSelection();
                                dgvDetails.CurrentCell = null;
                                e.Handled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC72:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstbxBatch_DoubleClick(object sender, EventArgs e)
        {
            try
            {



            }
            catch (Exception ex)
            {
                MessageBox.Show("PC73" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void lstbxBatch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    if (dgvDetails.CurrentRow != null)
                    {


                    }

                }
                else
                {
                    // Uncomment if any issues occur

                    if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
                    {
                        dgvDetails.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC74:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void lstbxBatch_MouseClick(object sender, MouseEventArgs e)
        {
            lstbxBatch_DoubleClick(sender, e);
        }

        private void dgvDetails_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC75:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dgvDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string strColumnName = dgvDetails.Columns[e.ColumnIndex].Name;
                if (strColumnName == "rate" || strColumnName == "unitPerRate")
                {
                    if (e.RowIndex == 0)
                    {
                        dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;

                    }
                    else
                    {
                        dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    }
                }
                if (strColumnName == "batch")
                {

                    if (dgvDetails.CurrentRow != null)
                    {

                        if (dgvDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.Cornsilk)
                        {




                            Point p = new Point();
                            p.X = dgvDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left + dgvDetails.Left;
                            p.Y = dgvDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Bottom + dgvDetails.Top;


                        }

                    }
                }
                else
                {


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC76:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void dgvDetails_Scroll(object sender, ScrollEventArgs e)
        {


        }

        private void lnklblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvDetails.SelectedCells.Count > 0 && dgvDetails.CurrentRow != null)
                {
                    if (dgvDetails.CurrentCell != dgvDetails.CurrentRow.Cells["check"])
                    {
                        if (dgvDetails.RowCount > 1)
                        {

                            if (InventorySettingsInfo._messageBoxRowRemove)
                            {
                                if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    RemoveRow();
                                }
                                else
                                {
                                    dgvDetails.Focus();
                                }
                            }

                            else
                            {
                                RemoveRow();
                            }
                        }
                        else
                        {
                            if (dgvDetails.CurrentRow.Cells["amount"].Value != null)
                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        dgvDetails.Rows.Clear();
                                    }
                                }
                                else
                                { dgvDetails.Rows.Clear(); }



                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC79:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dgvDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            if (dgvDetails.Columns[e.ColumnIndex].Name == "godown")
            {
                if (dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    objTransactionsGeneralFill.FillGodownToGridComboColumn(godown);
                    DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)dgvDetails[e.ColumnIndex, e.RowIndex];
                    DataTable dtbl = (DataTable)cmb.DataSource;
                    if (dtbl != null && dtbl.Rows.Count != 0)
                    {
                        dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dtbl.Rows[0]["godownId"].ToString();
                    }
                }
            }

            if (dgvDetails.Columns[e.ColumnIndex].Name == "rack")
            {
                if (dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)dgvDetails[e.ColumnIndex, e.RowIndex];
                    DataTable dtbl = (DataTable)cmb.DataSource;
                    if (dtbl != null && dtbl.Rows.Count != 0)
                    {
                        dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dtbl.Rows[0]["rackId"].ToString();
                    }
                }
            }
            //-----------------------------------------------
            else if (dgvDetails.Columns[e.ColumnIndex].Name == "unitPerQty" || (dgvDetails.Columns[e.ColumnIndex].Name == "unitPerRate" && e.RowIndex == 0))
            {
                if (dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)dgvDetails[e.ColumnIndex, e.RowIndex];
                    DataTable dtbl = (DataTable)cmb.DataSource;
                    if (dtbl != null && dtbl.Rows.Count != 0)
                    {
                        dgvDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dtbl.Rows[0]["unitId"].ToString();
                    }
                }
            }
        }


        private void lstbxBatch_SelectedValueChanged(object sender, EventArgs e)
        {


        }

        private void txtProductCode_Leave(object sender, EventArgs e)
        {
            lblProductCode.ForeColor = Color.Black;

            if (txtProductCode.Text.Contains("'"))
            {
                txtProductCode.Text = txtProductCode.Text.Replace("'", "");
            }
            if (txtProductCode.Text.Trim() != "")
            {
                //by sheena on 15-06-2023
                DataTable dtbl = new DataTable();
                dtbl = SPProduct.GetProductDetailsByBarcode(txtProductCode.Text);
                if (dtbl.Rows.Count > 0) //load details by Barcode
                {
                    txtProductCode.Text = dtbl.Rows[0]["productCode"].ToString();
                }
                //------------------
                ProductInfo info = new ProductSP().ProductView(txtProductCode.Text.Trim());
                if (info.ProductCode != null && info.ProductCode != "")
                {

                    FillProductForEdit(info.ProductCode);
                }
            }
        }



        private void txtPurchaseRate_Enter(object sender, EventArgs e)
        {
            try
            {
                lblPurchaserate.ForeColor = Color.Red;

                if (decimal.Parse(txtPurchaseRate.Text) == 0m)
                {
                    txtPurchaseRate.Text = "";
                }
            }
            catch
            {
                txtPurchaseRate.Text = "";
            }
        }

        private void txtMRP_Enter(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtMRP.Text) == 0m)
                {
                    txtMRP.Text = "";
                }
            }
            catch
            {
                txtMRP.Text = "";
            }
        }

        private void txtMinimumStock_Enter(object sender, EventArgs e)
        {
            try
            {
                lblMinStock.ForeColor = Color.Red;

                if (decimal.Parse(txtMinimumStock.Text) == 0m)
                {
                    txtMinimumStock.Text = "";
                }
            }
            catch
            {
                txtMinimumStock.Text = "";
            }
        }

        private void txtMaximumStock_Enter(object sender, EventArgs e)
        {
            try
            {
                lblMaxStock.ForeColor = Color.Red;

                if (decimal.Parse(txtMaximumStock.Text) == 0m)
                {
                    txtMaximumStock.Text = "";
                }
            }
            catch
            {
                txtMaximumStock.Text = "";
            }
        }

        private void txtReorderLevel_Enter(object sender, EventArgs e)
        {
            try
            {
                lblReOrderLevel.ForeColor = Color.Red;

                if (decimal.Parse(txtReorderLevel.Text) == 0m)
                {
                    txtReorderLevel.Text = "";
                }
            }
            catch
            {
                txtReorderLevel.Text = "";
            }
        }

        private void cbxActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbxActive.Checked)
            {
                if (txtProductCode.Text.Trim() != "")
                {
                    if (!SPProduct.ProductDelete(txtProductCode.Text.Trim(), false))
                    {
                        MessageBox.Show("Can't deactivate,reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        cbxActive.Checked = true;
                    }
                }
            }
        }

        private void dgvDetails_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            if ((cmbUnit.SelectedValue == null ? "System.Data.DataRowView" : cmbUnit.SelectedValue.ToString()) != "System.Data.DataRowView")
            {
                if (cmbUnit.SelectedValue.ToString() != "")
                {
                    isToNotDo = true;
                    dgvDetails.Rows[e.RowIndex].Cells["unitPerQty"].Value = cmbUnit.SelectedValue.ToString();
                    dgvDetails.Rows[e.RowIndex].Cells["unitPerRate"].Value = cmbUnit.SelectedValue.ToString();
                    isToNotDo = false;

                }
            }

        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtProductName.Text.Trim() == "" || txtProductName.SelectionStart == 0)
                //    //{
                //        txtProductCode.Focus();                       
                //   // }
                //}
                //else
                if (e.KeyCode == Keys.Enter)
                {

                    if (string.IsNullOrWhiteSpace(txtProductNameArabic.Text))
                    {
                        txtProductNameArabic.Text = GoogleTranslate(txtProductName.Text, "en", "ar");
                    }

                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                   // txtProductNameArabic.Focus();
                    //if (!txtProductCode.ReadOnly)
                    //{
                    //    txtProductCode.Focus();
                    //    txtProductCode.SelectionStart = 0;
                    //    txtProductCode.SelectionLength = 0;

                    //}
                    //else
                    //{
                    //    cmbGroup.Focus();
                    //}

                }

                if (e.Control && e.KeyCode == Keys.F)
                {
                    btnSearch_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool isUserEditingArabic = false;
        private void txtProductName_TextChanged(object sender, EventArgs e)
        {


            if (txtProductName.Text.Trim() == "")
            {
                txtProductName.Text = txtProductName.Text.Trim();
            }
            //if (!string.IsNullOrWhiteSpace(txtProductName.Text))
            //{
            //   // if (string.IsNullOrWhiteSpace(txtProductNameArabic.Text))
            //    //if (txtProductNameArabic.Text == "" || txtProductNameArabic.Text == null)
            //        txtProductNameArabic.Text = GoogleTranslate(txtProductName.Text, "en", "ar");
            //}
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {


                if (e.KeyChar == 8 || e.KeyChar == '' || (e.KeyChar >= 48 && e.KeyChar <= 57) || (e.KeyChar >= 97 && e.KeyChar <= 122) || (e.KeyChar >= 65 && e.KeyChar <= 90) || e.KeyChar == '-' || e.KeyChar == '.' || e.KeyChar == ' ' || e.KeyChar == '*' || e.KeyChar == '$' || e.KeyChar == '/' || e.KeyChar == '+' || e.KeyChar == '%')
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC15:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public string CheckInvalidEntriesInProductCode(TextBox txt)
        {

            foreach (char c in txt.Text)
            {
                if (c == '' || (c >= 48 && c <= 57) || (c >= 97 && c <= 122) || (c >= 65 && c <= 90) || c == '-' || c == '.' || c == ' ' || c == '*' || c == '$' || c == '/' || c == '+' || c == '%')
                {

                }
                else
                {
                    return c.ToString();
                }
            }
            return "";
        }

        private void txtPartNo_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void txtPartNo_Leave(object sender, EventArgs e)
        {
            lblPartNo.ForeColor = Color.Black;
        }



        private void txtPartNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //cmbMultipleUnit.Focus();
                //    cmbCategory.Focus();

                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {

                    //cmbOpeningStock.Focus();
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC81:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtfixedSalesRate_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtfixedSalesRate.Text);
            }
            catch
            {
                txtfixedSalesRate.Text = "0";
            }
        }

        private void txtfixedSalesRate_KeyPress(object sender, KeyPressEventArgs e)
        {

            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtfixedSalesRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtfixedSalesRate.Text.Trim() == "" || txtfixedSalesRate.SelectionStart == 0)
                //    //{
                //        txtMRP.Focus();
                //        txtMRP.SelectionStart = 0;
                //        txtMRP.SelectionLength = 0;
                //   // }
                //}
                //else
                if (e.KeyCode == Keys.Enter)
                {
                    txtMinimumStock.Focus();
                    txtMinimumStock.SelectionStart = 0;
                    txtMinimumStock.SelectionLength = 0;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC39:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void txtfixedSalesRate_Enter(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtfixedSalesRate.Text) == 0m)
                {
                    txtfixedSalesRate.Text = "";
                }
            }
            catch
            {
                txtfixedSalesRate.Text = "";
            }
        }

        private void cmbProductName_MouseDown(object sender, MouseEventArgs e)
        {
            isClear = false;
        }

        private void cmbProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                isClear = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string strGroupText = "";
        private void cmbGroup_KeyUp(object sender, KeyEventArgs e)
        {
            strGroupText = cmbGroup.Text;
        }

        string strBrandText = "";
        private void cmbBrand_KeyUp(object sender, KeyEventArgs e)
        {
            strBrandText = cmbBrand.Text;
        }

        string strUnitText = "";
        private void cmbUnit_KeyUp(object sender, KeyEventArgs e)
        {
            strUnitText = cmbUnit.Text;
        }

        private void cmbGroup_Enter(object sender, EventArgs e)
        {
            strGroupText = cmbGroup.Text;
            lblGroup.ForeColor = Color.Red;
        }
        bool isValueChange = false;
        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //isValueChange = true;
            strGroupText = cmbGroup.Text;
            if (isValueChange)
            {
                DataTable dtbl = new DataTable();
                if (cmbGroup.SelectedValue != null)
                {
                    dtbl = ProductGroupSP.ProductGroupHierarchyViewByGroupId(cmbGroup.SelectedValue.ToString());
                    if (dtbl.Rows.Count > 0)
                    {
                        string[] path = dtbl.Rows[0]["path1"].ToString().Split('*');
                        grpHierarchy.Visible = true;
                        lblCat1.Text = path[1];
                        lblCat2.Text = path[2];
                        lblCat3.Text = path[3];
                    }
                    else
                        grpHierarchy.Visible = true;
                }
            }
            isValueChange = true;
        }

        private void cmbBrand_Enter(object sender, EventArgs e)
        {
            strBrandText = cmbBrand.Text;
            lblBrand.ForeColor = Color.Red;
        }

        private void cmbBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            strBrandText = cmbBrand.Text;
        }

        private void cmbUnit_Enter(object sender, EventArgs e)
        {
            strUnitText = cmbUnit.Text;
            lblUnit.ForeColor = Color.Red;
        }
        bool isGridFill = false;
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            strUnitText = cmbUnit.Text;
            //--------------Fill and Load salesprice grid
            InitialSettingsSalesPriceGrid();
            ////////--------------Fill and Load Unitconversion grid----------////created by sheena on 10-05-2023
            DataTable dtblFromCreation = dtblMultiUnits.Copy();
            if (dtblFromCreation.Rows.Count > 0)
            {
                dtblFromCreation.Rows.RemoveAt(0);
            }
            InitialSettingsUnitGrid();

            dtblUnits = new DataTable();
            //if (objFrmMultipleProductCreation != null)
            //{
            //    DataColumn c0 = new DataColumn("productCode");
            //    dtblUnits.Columns.Add(c0);
            //}
            DataColumn c1 = new DataColumn("qty");
            dtblUnits.Columns.Add(c1);
            DataColumn c2 = new DataColumn("unitId");
            dtblUnits.Columns.Add(c2);
            DataColumn c3 = new DataColumn("unitName");
            dtblUnits.Columns.Add(c3);
            DataColumn c4 = new DataColumn("view");
            dtblUnits.Columns.Add(c4);
            DataColumn c5 = new DataColumn("barcode");//created by sheena 11-05-2023
            dtblUnits.Columns.Add(c5);//created by sheena 11-05-2023
            if (dtblFromCreation.Rows.Count > 0)
            {
                foreach (DataRow drowCur in dtblFromCreation.Rows)
                {
                    //---------------old code commented by sheena-----------
                    //dgvUnits.Rows.Add();
                    //string[] ar = new string[2];
                    //if (drowCur["view"] != null && drowCur["view"].ToString() != "")
                    //{
                    //    ar = drowCur["view"].ToString().Split('-');
                    //}
                    //dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["uqty"].Value = ar[0].ToString();
                    //dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["defaultQty"].Value = ar[1].ToString();

                    //dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["unit"].Value = drowCur["unitId"].ToString();
                    ////---------------created by sheena 11-05-2023------------------
                    dgvUnits.Rows.Add();

                    dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["defaultQty"].Value = drowCur["qty"].ToString();
                    dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["unit"].Value = drowCur["unitId"].ToString();
                    dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells["barcode"].Value = drowCur["barcode"].ToString();
                    ////---------------
                    isGridFill = true;
                }
            }
            //dgvUnits.Focus();
            //dgvUnits.CurrentCell = dgvUnits.Rows[0].Cells["unit"];
            //dgvUnits.Rows[0].Cells["unit"].Selected = true;
            //----------------------------------
        }
        string strTaxText = "";
        private void cmbTax_KeyUp(object sender, KeyEventArgs e)
        {
            strTaxText = cmbTax.Text;
        }

        private void cmbTax_Enter(object sender, EventArgs e)
        {
            strTaxText = cmbTax.Text;
            lblTax.ForeColor = Color.Red;
        }

        private void cmbTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            strTaxText = cmbTax.Text;
            taxComboSelectedRow = cmbTax.SelectedItem as DataRowView;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ofdPhoto.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG";
            //    ofdPhoto.FileName = "";
            //    if (DialogResult.OK == ofdPhoto.ShowDialog())
            //    {
            //        if (ofdPhoto.FileName != "")
            //        {
            //            try
            //            {
            //                logo = ReadFile(ofdPhoto.FileName);
            //                MemoryStream ms = new MemoryStream(logo);
            //                Image newImage = Image.FromStream(ms);
            //                pbLogo.Image = newImage;
            //                pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;

            //                string sourcePath = Path.Combine(Application.StartupPath, "ProductImage" + "\\" + txtProductCode.Text);
            //                destinationPath = sourcePath + Path.GetExtension(ofdPhoto.FileName);
            //            }
            //            catch (Exception)
            //            {

            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            // Create directory if not exists
            try
            {
                ofdPhoto.Filter = "Image Files (*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG)|*.BMP;*.JPG;*.JPEG;*.GIF;*.PNG";
                ofdPhoto.FileName = "";

                if (ofdPhoto.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(ofdPhoto.FileName))
                {
                    // 1. Load image into PictureBox
                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(ofdPhoto.FileName)))
                    {
                        pbLogo.Image = Image.FromStream(ms);
                        pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    // 2. Create ProductImage folder if it doesn't exist
                    string productImageFolder = Path.Combine(Application.StartupPath, "ProductImage");
                    if (!Directory.Exists(productImageFolder))
                    {
                        Directory.CreateDirectory(productImageFolder);
                    }

                    // 3. Build destination path (save copy of image)
                    string extension = Path.GetExtension(ofdPhoto.FileName);
                    string fileName = txtProductCode.Text.Trim() + extension;
                    destinationPath = Path.Combine(productImageFolder, fileName);

                    try
                    {
                        File.Copy(ofdPhoto.FileName, destinationPath, true); // Overwrite if exists
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving image locally: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearImg_Click(object sender, EventArgs e)
        {
            try
            {
                GetDefaultImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtProductNameArabic_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //if (!txtProductCode.ReadOnly)
                    //{
                    //    txtProductCode.Focus();
                    //    txtProductCode.SelectionStart = 0;
                    //    txtProductCode.SelectionLength = 0;

                    //}
                    //else
                    //{
                  //  cmbProductMainGrp.Focus();//created by sheena on 05-05-2023
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                    // }

                }
                //else if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (txtProductNameArabic.Text.Trim() == "" || txtProductNameArabic.SelectionStart == 0)
                //    //{

                //        txtProductName.Focus();
                //        txtProductName.SelectionStart = txtProductName.Text.Trim().Length;
                //        txtProductName.SelectionLength = 0;

                //    //}
                //}



            }
            catch (Exception ex)
            {
                MessageBox.Show("PC31:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)////created by sheena on 04-05-2023
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{

                //    txtReorderLevel.Focus();
                //    txtReorderLevel.SelectionStart = 0;
                //    txtReorderLevel.SelectionLength = 0;

                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                    //if (cmbAllowBatch.Enabled)
                    //    cmbAllowBatch.Focus();
                    //else
                    //    cmbBom.Focus();
                   // txtPartNo.Focus();
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }

                DropDownCombo(e);


            }
            catch (Exception ex)
            {
                MessageBox.Show("PC43:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnMainGroup_Click(object sender, EventArgs e)//created by sheena on 05-05-2023
        {
            try
            {
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Product Main Group", "", "Masters") == true)
                //  //  if (checkuserprivilege.CheckPrivilage("Product Main Group", "") == true)
                //{
                    // Save current product group Id
                    if (cmbProductMainGrp.SelectedValue != null)
                    {
                        strOldMainGrpCode = cmbProductMainGrp.SelectedValue.ToString();
                    }
                    else
                    {
                        strOldMainGrpCode = "";
                    }
                    frmProductMainGroup frmproductmaingroup = new frmProductMainGroup();


                    frmproductmaingroup.MdiParent = MDIFinacPOS.MDIObj;
                    frmproductmaingroup.DoWhenComingFromProductCreationForm(this);
                    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("PC28" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        string strMainGroupText = "";
        private void cmbProductMainGrp_SelectedIndexChanged(object sender, EventArgs e)//created by sheena on 05-05-2023
        {
            strMainGroupText = cmbProductMainGrp.Text;
            generateNewProductCode();//created by sheena on 09-05-2023

        }

        private void cmbProductMainGrp_Enter(object sender, EventArgs e)//created by sheena on 05-05-2023
        {
            strMainGroupText = cmbProductMainGrp.Text;
            lblMainGroup.ForeColor = Color.Red;
        }

        private void cmbProductMainGrp_KeyDown(object sender, KeyEventArgs e)//created by sheena on 05-05-2023
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (cmbProductMainGrp.Text.Trim() == "" || cmbProductMainGrp.SelectionStart == 0)
                //    //{

                //        txtProductNameArabic.Focus();
                //        txtProductNameArabic.SelectionStart = 0;
                //        txtProductNameArabic.SelectionLength = 0;

                //   // }
                //}
                //else
                if (e.KeyCode == Keys.Enter)
                {
                    //cmbProductMainGrp.Text = strMainGroupText;
                    //cmbGroupCat1.Focus();
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);

                }
                else if (e.Alt && e.KeyCode == Keys.C)
                {
                    SendKeys.Send("{F10}");
                    btnNewGroup_Click(sender, e);
                }

                DropDownCombo(e);



            }
            catch (Exception ex)
            {
                MessageBox.Show("PC33:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbProductMainGrp_KeyUp(object sender, KeyEventArgs e)//created by sheena on 05-05-2023
        {
            strMainGroupText = cmbProductMainGrp.Text;
        }

        private void cmbProductMainGrp_Leave(object sender, EventArgs e)//created by sheena on 05-05-2023
        {
            // strMainGroupText = cmbProductMainGrp.Text;
            lblMainGroup.ForeColor = Color.Black;

            if (cmbProductMainGrp.SelectedIndex == -1)
            {
                cmbProductMainGrp.Text = strMainGroupText;
            }
            objComboValidation.CheckCollection(cmbProductMainGrp);
        }

        private void cmbProductMainGrp_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        bool isDontEceute = false;
        private void dgvUnits_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)//created by sheena on 10-05-2023
        {
            dgvUnits["ucheck", e.RowIndex].Style.BackColor = Color.FromName("Control");
            if (strDefault != "")
            {
                isDontEceute = true;
                dgvUnits.Rows[e.RowIndex].Cells["equal"].Value = "=";
                dgvUnits.Rows[e.RowIndex].Cells["defaultUnit"].Value = strDefault;
                dgvUnits.Rows[e.RowIndex].Cells["defaultQty"].Value = "1";
                isDontEceute = false;

            }
        }
        bool isUnitValueChange = true;
        private void dgvUnits_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex != -1 && e.ColumnIndex != -1 && !isDontEceute && isUnitValueChange)
            {
                decimal dQty = 0;
                bool isOk = true;
                //if (dgvUnits.Rows[e.RowIndex].Cells["uqty"].Value != null)
                //{
                //    decimal.TryParse(dgvUnits.Rows[e.RowIndex].Cells["uqty"].Value.ToString(), out dQty);
                //}
                //if (dQty == 0)
                //{
                //    isDontEceute = true;
                //    dgvUnits.Rows[e.RowIndex].Cells["uqty"].Value = "0";
                //    isDontEceute = false;
                //    isOk = false;
                //}
                if (dgvUnits.Rows[e.RowIndex].Cells["defaultQty"].Value != null)
                {
                    decimal.TryParse(dgvUnits.Rows[e.RowIndex].Cells["defaultQty"].Value.ToString(), out dQty);
                }
                if (dQty == 0)
                {
                    isDontEceute = true;
                    dgvUnits.Rows[e.RowIndex].Cells["defaultQty"].Value = "1";
                    isDontEceute = false;
                }
                if (dgvUnits.Rows[e.RowIndex].Cells["unit"].Value == null || dgvUnits.Rows[e.RowIndex].Cells["unit"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }
                if (isOk)
                {
                    dgvUnits.Rows[e.RowIndex].Cells["ucheck"].Value = "";
                }
                else
                {

                    dgvUnits.Rows[e.RowIndex].Cells["ucheck"].Value = "x";
                    dgvUnits["ucheck", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                //for Block seperate unit conversion for same unit
                if (dgvUnits.Columns[e.ColumnIndex].Name == "defaultQty")///created by sheena on 15-05-2023
                {
                    isUnitValueChange = false;
                    if (dgvUnits.Rows.Count > 2)
                    {
                        string str1 = "", str2 = ""; int count = 0; double iQty = 0, jQty = 0;
                        for (int i = 0; i < dgvUnits.Rows.Count; i++)
                        {
                            for (int j = 1; j < dgvUnits.Rows.Count; j++)
                            {
                                if (dgvUnits.Rows[i].Cells["unit"].RowIndex != dgvUnits.Rows[j].Cells["unit"].RowIndex)
                                {
                                    if (!dgvUnits.Rows[j].IsNewRow && !dgvUnits.Rows[i].IsNewRow)
                                    {
                                        str1 = dgvUnits.Rows[i].Cells["unit"].Value == null ? string.Empty : dgvUnits.Rows[i].Cells["unit"].Value.ToString();
                                        str2 = dgvUnits.Rows[j].Cells["unit"].Value == null ? string.Empty : dgvUnits.Rows[j].Cells["unit"].Value.ToString();
                                        iQty = Convert.ToDouble(dgvUnits.Rows[i].Cells["defaultQty"].Value.ToString());
                                        jQty = Convert.ToDouble(dgvUnits.Rows[j].Cells["defaultQty"].Value.ToString());
                                        if (str1 == str2 && iQty != jQty)
                                        {
                                            count = count + 1;

                                            //  dgvUnits.Rows[j].Cells["ucheck"].Value = "x";
                                            //  dgvUnits["ucheck", j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                        }
                                        else if (isOk && (str1 == str2) && (iQty == jQty))
                                        {
                                            dgvUnits.Rows[j].Cells["ucheck"].Value = "";
                                        }

                                    }
                                }
                            }
                        }
                        if (count > 0)
                        {
                            dgvUnits.CurrentRow.Cells["ucheck"].Value = "x";
                            dgvUnits.CurrentRow.Cells["ucheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            MessageBox.Show("Please enter same unit conversion", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    isUnitValueChange = true;
                }
                if (!isFormLoad && isGridFill && !isUnitClear)
                {
                    bool isasOk = SalesPriceandUnitGridCompare();

                    if (isasOk)
                    {
                        try
                        {
                            dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "";
                        }
                        catch { }
                    }
                }

            }
        }

        private void dgvUnits_KeyDown(object sender, KeyEventArgs e)
        {


            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        if (dgvUnits.CurrentRow != null)
            //        {

            //            if (dgvUnits.CurrentRow.IsNewRow && dgvUnits.Rows.Count > 1 && dgvUnits.CurrentRow.Index == dgvUnits.Rows.Count - 1)
            //            {
            //                if (((dgvUnits.CurrentRow.Cells["amount"].Value == null ? "" : dgvUnits.CurrentRow.Cells["amount"].Value.ToString()) == "" && dgvUnits.CurrentCell == dgvUnits.CurrentRow.Cells["productCode"]) || dgvProduct.CurrentCell == dgvProduct.CurrentRow.Cells["amount"])
            //                {
            //                    txtNarration.Focus();
            //                    dgvProduct.ClearSelection();
            //                    e.Handled = true;
            //                }

            //            }
            //        }
            //    }

            //    else if (e.KeyCode == Keys.Delete && dgvProduct.CurrentCell != null && !dgvProduct.CurrentCell.IsInEditMode && dgvProduct.Columns[dgvProduct.CurrentCell.ColumnIndex].Name == "productName" && !dgvProduct.Columns[dgvProduct.CurrentCell.ColumnIndex].ReadOnly)
            //    {
            //        dgvProduct.CurrentCell.Value = "";
            //        dgvProduct.BeginEdit(true);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("PI61:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}




            try
            {

                //if (e.KeyCode == Keys.Enter)
                //{


                //    if (dgvUnits.CurrentRow != null)
                //    {

                //        if (dgvUnits.CurrentRow.IsNewRow && dgvUnits.Rows.Count > 1 && dgvUnits.CurrentRow.Index == dgvUnits.Rows.Count - 1)
                //        {
                //            if ((dgvUnits.CurrentRow.Cells["barcode"].Value == null ? "" : dgvUnits.CurrentRow.Cells["barcode"].Value.ToString()) == "")
                //            {
                //                dgvSalesPrice.Focus();
                //                dgvUnits.ClearSelection();
                //                dgvUnits.CurrentCell = null;
                //                e.Handled = true;
                //            }
                //        }
                //    }

                //}
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgvUnits.CurrentRow != null)
                    {
                        int rowIndex = dgvUnits.CurrentRow.Index;
                        int lastRealRowIndex = dgvUnits.Rows.Count - 2; // last data row before "new row"

                        string barcode = dgvUnits.CurrentRow.Cells["barcode"].Value?.ToString().Trim() ?? "";

                        // if we are on last real row AND barcode is empty
                        //if (rowIndex == lastRealRowIndex && string.IsNullOrEmpty(barcode))
                        if (rowIndex == lastRealRowIndex )
                        {
                            e.Handled = true; // stop default Enter navigation

                            dgvUnits.ClearSelection();
                            dgvUnits.CurrentCell = null;

                            // jump to dgvSalesPrice and start edit in first cell
                            dgvSalesPrice.Focus();
                            if (dgvSalesPrice.Rows.Count > 0)
                            {
                                dgvSalesPrice.CurrentCell = dgvSalesPrice.Rows[0].Cells[0];
                                dgvSalesPrice.BeginEdit(true);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("MU3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dgvUnits_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dgvUnits.Columns[e.ColumnIndex].Name == "unit")
            {
                if (dgvUnits.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)dgvUnits[e.ColumnIndex, e.RowIndex];
                    DataTable dtbl = (DataTable)cmb.DataSource;
                    if (dtbl != null && dtbl.Rows.Count != 0)
                    {
                        dgvUnits.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dtbl.Rows[0]["unitId"].ToString();
                    }
                }
            }
        }
        DataGridViewTextBoxEditingControl TextBoxControl1;
        private void dgvUnits_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBoxControl1 = e.Control as DataGridViewTextBoxEditingControl;
                if (TextBoxControl1 != null)
                {
                    TextBoxControl1.KeyPress += TextBoxCellEditControlKeyPress1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TextBoxCellEditControlKeyPress1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dgvUnits.CurrentCell != null)
                {
                    if (dgvUnits.Columns[dgvUnits.CurrentCell.ColumnIndex].Name == "qty" || dgvUnits.Columns[dgvUnits.CurrentCell.ColumnIndex].Name == "defaultQty")
                    {
                        ComboValidation objComboValidation = new ComboValidation();
                        objComboValidation.DecimalValidationGRid(sender, e, TextBoxControl1, false);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void lnklblUnitRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvUnits.SelectedCells.Count > 0 && dgvUnits.CurrentRow != null)
                {
                    if (dgvUnits.CurrentCell != dgvUnits.CurrentRow.Cells["ucheck"])
                    {

                        if (!dgvUnits.CurrentRow.IsNewRow)
                        {
                            if (dgvUnits.RowCount > 1)
                            {

                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        RemoveRowUnitGRid();
                                    }
                                    else
                                    {
                                        dgvUnits.Focus();
                                    }
                                }

                                else
                                {
                                    RemoveRowUnitGRid();
                                }
                            }
                            else
                            {

                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        dgvUnits.Rows.Clear();
                                    }
                                }
                                else
                                { dgvUnits.Rows.Clear(); }



                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("MU2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void dgvUnits_DataError(object sender, DataGridViewDataErrorEventArgs e)  //created by sheena on 11-05-2023
        {
            try
            {
            }
            catch { }
        }
        public void InitialSettingsSalesPriceGrid()///created by sheena on 13-05-2023
        {
            if (cmbUnit.SelectedValue != null)
            {
                strDefault = cmbUnit.SelectedValue.ToString();
                if (strProductIdToEdit == "")
                {
                    objTransactionsGeneralFill.FillUnitToGridComboColumn(UnitId);
                    objTransactionsGeneralFill.FillPricingLevelGridCombo(pricingLevelId);


                    objTransactionsGeneralFill.FillUnitToGridComboColumn(ppp_unit);


                    //if (strDefault != "")
                    //{

                    //    if (unit.DataSource != null)
                    //    {
                    //        foreach (DataRow drowObj in ((DataTable)unit.DataSource).Rows)
                    //        {
                    //            if (drowObj.ItemArray[0].ToString() == strDefault)
                    //            {
                    //                ((DataTable)unit.DataSource).Rows.Remove(drowObj);
                    //                break;
                    //            }
                    //        }

                    //    }
                    //}

                    //dgvSalesPrice.Columns["defaultUnit"].ReadOnly = true;
                    dgvSalesPrice.Rows.Clear();
                }
            }

        }
        private void dgvSalesPrice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvSalesPrice.Rows[e.RowIndex];

            decimal costPrice = Convert.ToDecimal(row.Cells["costPrice"].Value ?? 0);
            decimal marginPercent = Convert.ToDecimal(row.Cells["marginPercentage"].Value ?? 0);
            decimal mrp = Convert.ToDecimal(row.Cells["PriceAmount"].Value ?? 0);
            decimal discountAmt = Convert.ToDecimal(row.Cells["DiscAmount"].Value ?? 0);
            decimal discountPercent = Convert.ToDecimal(row.Cells["DiscPercentage"].Value ?? 0);
            decimal salesPrice = Convert.ToDecimal(row.Cells["SalesPrice"].Value ?? 0);

            string colName = dgvSalesPrice.Columns[e.ColumnIndex].Name;

            if (colName == "costPrice" || colName == "marginPercentage")
            {
                // If MRP already exists, confirm before overwrite
                if (mrp > 0)
                {
                    DialogResult dr = MessageBox.Show(
                        "MRP already has a value. Do you want to recalculate it based on Cost Price + Margin?",
                        "Confirm MRP Change",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (dr == DialogResult.No)
                    {
                        // revert to old value
                        row.Cells[colName].Value = oldValue;
                        return;
                    }
                }

                // Rule 1: MRP = Cost + Margin%
                mrp = costPrice + (costPrice * marginPercent / 100);
                row.Cells["PriceAmount"].Value = Math.Round( mrp,FinanceSettingsInfo._roundDecimal);

                // reset discounts
                row.Cells["DiscAmount"].Value = 0;
                row.Cells["DiscPercentage"].Value = 0;

                // Sales = MRP
                row.Cells["SalesPrice"].Value = Math.Round(mrp, FinanceSettingsInfo._roundDecimal);
            }
            else if (colName == "PriceAmount")
            {
                string pricingLevel = dgvSalesPrice.CurrentRow.Cells["pricingLevelId"].Value == null ? string.Empty : dgvSalesPrice.CurrentRow.Cells["pricingLevelId"].Value.ToString();
                if (pricingLevel == "")
                    dgvSalesPrice.CurrentRow.Cells["pricingLevelId"].Value = "1";//Default pricinglevel
                // Rule 2: Sales = MRP
                row.Cells["SalesPrice"].Value = Math.Round(mrp, FinanceSettingsInfo._roundDecimal);
                row.Cells["DiscAmount"].Value = 0;
                row.Cells["DiscPercentage"].Value = 0;
            }
            else if (colName == "DiscAmount")
            {
                // Rule 3: Sales = MRP – DiscAmount
                salesPrice = mrp - discountAmt;
                if (salesPrice < 0) salesPrice = 0;
                row.Cells["SalesPrice"].Value = Math.Round(salesPrice, FinanceSettingsInfo._roundDecimal); 

                // back-calc percentage
                if (mrp > 0)
                    row.Cells["DiscPercentage"].Value = Math.Round((discountAmt / mrp) * 100, FinanceSettingsInfo._roundDecimal);
            }
            else if (colName == "DiscPercentage")
            {
                // Rule 4: Sales = MRP – (MRP × Discount%)
                salesPrice = mrp - (mrp * discountPercent / 100);
                if (salesPrice < 0) salesPrice = 0;
                row.Cells["SalesPrice"].Value = Math.Round(salesPrice, FinanceSettingsInfo._roundDecimal);

                // back-calc amount
                row.Cells["DiscAmount"].Value = Math.Round(mrp - salesPrice, FinanceSettingsInfo._roundDecimal);
               
            }
            if (FinanceSettingsInfo._ActivateTax)
                RecalculateSalesAndTax(row);
        }
        private void RecalculateSalesAndTax(DataGridViewRow row)
        {
            decimal mrp = Convert.ToDecimal(row.Cells["PriceAmount"].Value ?? 0);
            decimal discountAmt = Convert.ToDecimal(row.Cells["DiscAmount"].Value ?? 0);
            decimal discountPercent = Convert.ToDecimal(row.Cells["DiscPercentage"].Value ?? 0);

            decimal afterDiscount = mrp;

            if (discountAmt > 0)
                afterDiscount = mrp - discountAmt;
            else if (discountPercent > 0)
                afterDiscount = mrp - (mrp * discountPercent / 100);

            if (afterDiscount < 0) afterDiscount = 0;

            // ✅ Parse tax rate from "15%" format
            decimal taxRate = 0;
            if (taxComboSelectedRow != null && taxComboSelectedRow["rate"] != DBNull.Value)
            {
                string rateStr = taxComboSelectedRow["rate"].ToString().Trim(); // e.g. "15%"
                if (rateStr.EndsWith("%"))
                    rateStr = rateStr.Substring(0, rateStr.Length - 1);
                decimal.TryParse(rateStr, out taxRate);
            }

            decimal salesPrice = 0;
            decimal taxAmount = 0;

            if (FinanceSettingsInfo._VatIncluded || FinanceSettingsInfo._VatandCessIncluded)
            {
                // 🔹 SalesPrice entered already includes tax
                salesPrice = afterDiscount;

                // Excluded tax value
                decimal netWithoutTax = salesPrice / (1 + taxRate / 100);

                // Show excluded-tax sales price in TaxAmount column
                taxAmount = netWithoutTax;
            }
            else
            {
                // 🔹 SalesPrice entered excludes tax
                salesPrice = afterDiscount;

                // Add tax on top
                decimal grossWithTax = salesPrice * (1 + taxRate / 100);

                // Show with-tax sales price in TaxAmount column
                taxAmount = grossWithTax;
            }

            // ✅ Keep SalesPrice as entered (afterDiscount)
            row.Cells["SalesPrice"].Value = Math.Round(salesPrice, FinanceSettingsInfo._roundDecimal);

            // ✅ TaxAmount column now shows excluded or included final price
            row.Cells["TaxAmount"].Value = Math.Round(taxAmount, FinanceSettingsInfo._roundDecimal);

            // Update header to clarify
            dgvSalesPrice.Columns["TaxAmount"].HeaderText =
                (FinanceSettingsInfo._VatIncluded || FinanceSettingsInfo._VatandCessIncluded)
                ? $"Excl. Tax Price ({taxRate}%)"
                : $"Incl. Tax Price ({taxRate}%)";
        }


        //private void RecalculateSalesAndTax(DataGridViewRow row)
        //{
        //    decimal mrp = Convert.ToDecimal(row.Cells["PriceAmount"].Value ?? 0);
        //    decimal discountAmt = Convert.ToDecimal(row.Cells["DiscAmount"].Value ?? 0);
        //    decimal discountPercent = Convert.ToDecimal(row.Cells["DiscPercentage"].Value ?? 0);

        //    decimal afterDiscount = mrp;

        //    if (discountAmt > 0)
        //        afterDiscount = mrp - discountAmt;
        //    else if (discountPercent > 0)
        //        afterDiscount = mrp - (mrp * discountPercent / 100);

        //    if (afterDiscount < 0) afterDiscount = 0;

        //    // ✅ Parse tax rate from "15%" format
        //    decimal taxRate = 0;
        //    if (taxComboSelectedRow != null && taxComboSelectedRow["rate"] != DBNull.Value)
        //    {
        //        string rateStr = taxComboSelectedRow["rate"].ToString().Trim(); // "15%"
        //        if (rateStr.EndsWith("%"))
        //            rateStr = rateStr.Substring(0, rateStr.Length - 1);
        //        decimal.TryParse(rateStr, out taxRate);
        //    }
        //    decimal taxAmount = 0;
        //    decimal salesPrice = 0;

        //    if (FinanceSettingsInfo._VatIncluded||FinanceSettingsInfo._VatandCessIncluded) // if tax included
        //    {
        //        salesPrice = afterDiscount;
        //        decimal netWithoutTax = salesPrice / (1 + taxRate / 100);
        //        taxAmount = salesPrice - netWithoutTax;
        //    }
        //    else // tax excluded
        //    {
        //        taxAmount = afterDiscount * (taxRate / 100);
        //        salesPrice = afterDiscount + taxAmount;
        //    }

        //    row.Cells["SalesPrice"].Value = Math.Round(salesPrice, FinanceSettingsInfo._roundDecimal);
        //    row.Cells["TaxAmount"].Value = Math.Round(taxAmount, FinanceSettingsInfo._roundDecimal);

        //    // update header dynamically
        //    dgvSalesPrice.Columns["TaxAmount"].HeaderText =
        //      FinanceSettingsInfo._VatIncluded||FinanceSettingsInfo._VatandCessIncluded ? $"Included Tax ({taxRate}%)" : $"Excluded Tax ({taxRate}%)";
        //}


        //private void dgvSalesPrice_CellEndEdit(object sender, DataGridViewCellEventArgs e)///created by sheena on 15-05-2023
        //{
        //    try
        //    {
        //        decimal DiscPercentage = 0, DiscAmount = 0, Amount = 0, SalesPrice = 0,marginPerc=0
        //        if (dgvSalesPrice.CurrentCell != null)
        //        {
        //            if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "PriceAmount")
        //            {
        //                Amount = 0;
        //                try
        //                {
        //                    string pricingLevel = dgvSalesPrice.CurrentRow.Cells["pricingLevelId"].Value == null ? string.Empty : dgvSalesPrice.CurrentRow.Cells["pricingLevelId"].Value.ToString();
        //                    if(pricingLevel=="")
        //                        dgvSalesPrice.CurrentRow.Cells["pricingLevelId"].Value = "1";//Default pricinglevel
        //                    if (dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value != null || dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value.ToString() != "")
        //                    {
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);

        //                    }
        //                }
        //                catch
        //                {
        //                    Amount = 0;
        //                    dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value = 0;
        //                }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value != null || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() != "0")
        //                    {
        //                        dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = 0;
        //                        //Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                        //DiscPercentage = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value);
        //                        //Amount = Amount - ((Amount*DiscPercentage)/100);
        //                        //dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;

        //                    }
        //                }
        //                catch
        //                {
        //                    dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = 0;

        //                }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value != null || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "0")
        //                    {
        //                        dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = 0;
        //                        //Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                        //DiscAmount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value);
        //                        //Amount = Amount - DiscAmount;
        //                        //dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;
        //                    }
        //                }
        //                catch
        //                {
        //                    dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = 0;

        //                }
        //                if (InventorySettingsInfo._SalePriceUpdateByCostPerc)
        //                {
        //                    decimal decCostAmount = 0;
        //                    decCostAmount = decimal.Parse(txtPurchaseRate.Text);
        //                    txtPercentage.Text = (((Amount - decCostAmount) / Amount) * 100).ToString();
        //                    txtPercentage.Text = ((decimal)Math.Round(decimal.Parse(txtPercentage.Text), 2)).ToString();
        //                }
        //                dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;
        //                //if( dgvSalesPrice.CurrentRow.Cells["status"].Value.ToString()!=="IsFromDb" )
        //                // dgvSalesPrice.CurrentRow.Cells["status"].Value 
        //            }
        //            else if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "DiscPercentage")
        //            {
        //                Amount = 0;
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value != null || dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value.ToString() != "")
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                }
        //                catch { Amount = 0; }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value != null || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() != "0")
        //                    {
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                        DiscPercentage = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value);
        //                        dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = (Amount * DiscPercentage) / 100;
        //                        Amount = Amount - ((Amount * DiscPercentage) / 100);
        //                        dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;

        //                    }
        //                }
        //                catch {
        //                dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = 0;
        //                }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value != null || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "0")
        //                    {
        //                        //DiscAmount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value);
        //                        //Amount = Amount - DiscAmount;
        //                        //dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount ;


        //                    }
        //                }
        //                catch
        //                {
        //                    dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = 0;
        //                }

        //                dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;
        //            }
        //            else if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "DiscAmount")
        //            {
        //                Amount = 0;
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value != null || dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value.ToString() != "")
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                }
        //                catch
        //                {
        //                    Amount = 0;
        //                }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value != null || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() != "0")
        //                    {

        //                        //DiscPercentage = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value);
        //                        //Amount = Amount - ((Amount * DiscPercentage) / 100);
        //                        //dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;
        //                        // dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = 0;
        //                    }
        //                }
        //                catch
        //                {

        //                    dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = 0;
        //                }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value != null || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "0")
        //                    {
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                        decimal postdiscountprice = 0;
        //                        DiscAmount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value);
        //                        postdiscountprice = Amount - DiscAmount;
        //                        dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = Math.Round(((Amount - postdiscountprice) / Amount) * 100, 2);
        //                        Amount = Amount - DiscAmount;
        //                        dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;
        //                    }
        //                }
        //                catch { dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = 0; }

        //                dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;

        //            }
        //            else if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "SalesPrice")
        //            {
        //                Amount = 0;
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value == null || dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value.ToString() == "" || dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value.ToString() == "0")
        //                    {
        //                        dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value = dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value.ToString();
        //                        Amount =Convert.ToDecimal( dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value);
        //                    }
        //                    else
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);



        //                }
        //                catch
        //                {
        //                    Amount = 0;
        //                }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value == null || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() == "" || dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value.ToString() == "0")
        //                    {

        //                        //DiscPercentage = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value);
        //                        //Amount = Amount - ((Amount * DiscPercentage) / 100);
        //                        //dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;
        //                        // dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = 0;

        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                        decimal postdiscountprice = 0;
        //                        SalesPrice = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value);
        //                        postdiscountprice = Amount - SalesPrice;
        //                        dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = Math.Round(postdiscountprice, 2);

        //                        dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = Math.Round((( postdiscountprice) / Amount) * 100, 2);

        //                    }
        //                }
        //                catch
        //                {

        //                    dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = 0;
        //                }
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value != null || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value.ToString() != "0")
        //                    {
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                        decimal postdiscountprice = 0;
        //                        SalesPrice = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value);
        //                        postdiscountprice = Amount - SalesPrice;
        //                        dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = Math.Round(postdiscountprice, 2);

        //                        dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = Math.Round((( postdiscountprice) / Amount) * 100, 2);

        //                    }
        //                }
        //                catch { dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = 0; }

        //                //dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;

        //            }
        //            else if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "costPrice")
        //            {
        //                marginPerc = 0;
        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["marginPercentage"].Value != null || dgvSalesPrice.CurrentRow.Cells["marginPercentage"].Value.ToString() != "")
        //                        marginPerc = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["marginPercentage"].Value);
        //                }
        //                catch
        //                {
        //                    marginPerc = 0;
        //                }

        //                try
        //                {
        //                    if (dgvSalesPrice.CurrentRow.Cells["costPrice"].Value != null || dgvSalesPrice.CurrentRow.Cells["costPrice"].Value.ToString() != "" || dgvSalesPrice.CurrentRow.Cells["costPrice"].Value.ToString() != "0")
        //                    {
        //                        Amount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["PriceAmount"].Value);
        //                        decimal postdiscountprice = 0;
        //                        DiscAmount = Convert.ToDecimal(dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value);
        //                        postdiscountprice = Amount - DiscAmount;
        //                        dgvSalesPrice.CurrentRow.Cells["DiscPercentage"].Value = Math.Round(((Amount - postdiscountprice) / Amount) * 100, 2);
        //                        Amount = Amount - DiscAmount;
        //                        dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;
        //                    }
        //                }
        //                catch { dgvSalesPrice.CurrentRow.Cells["DiscAmount"].Value = 0; }

        //                dgvSalesPrice.CurrentRow.Cells["SalesPrice"].Value = Amount;

        //            }


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("MU5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //}



        private void dgvSalesPrice_KeyPress(object sender, KeyPressEventArgs e)
        {


        }
        //-----created by sheena on 12-05-2023
        DataGridViewTextBoxEditingControl TextBoxControlSalesPrice;
        private void dgvSalesPrice_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBoxControlSalesPrice = e.Control as DataGridViewTextBoxEditingControl;
                if (TextBoxControlSalesPrice != null)
                {
                    TextBoxControlSalesPrice.KeyPress += TextBoxCellEditControlKeyPressSalesPrice;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TextBoxCellEditControlKeyPressSalesPrice(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dgvSalesPrice.CurrentCell != null)
                {
                    if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "PriceAmount" || dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "DiscPercentage" || dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "DiscAmount" || dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "SalesPrice")
                    {
                        ComboValidation objComboValidation = new ComboValidation();
                        objComboValidation.DecimalValidationGRid(sender, e, TextBoxControlSalesPrice, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lnklblSalesPrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvSalesPrice.SelectedCells.Count > 0 && dgvSalesPrice.CurrentRow != null)
                {
                    if (dgvSalesPrice.CurrentCell != dgvSalesPrice.CurrentRow.Cells["UnitId"])
                    {

                        if (!dgvSalesPrice.CurrentRow.IsNewRow)
                        {
                            if (dgvSalesPrice.RowCount > 1)
                            {

                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        RemoveRowSalesPriceGRid();
                                        UntickCheckMarkSales();
                                    }
                                    else
                                    {
                                        dgvSalesPrice.Focus();
                                    }
                                }

                                else
                                {
                                    RemoveRowSalesPriceGRid();
                                    UntickCheckMarkSales();
                                }
                            }
                            else
                            {

                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        dgvSalesPrice.Rows.Clear();
                                    }
                                }
                                else
                                { dgvSalesPrice.Rows.Clear(); }



                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("MU2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        public void RemoveRowSalesPriceGRid()///created by sheena on 13-05-2023
        {
            int inColIndex = dgvSalesPrice.CurrentCell.ColumnIndex;
            bool isContinue = true;
            if (!dgvSalesPrice.Rows[dgvSalesPrice.CurrentRow.Index].IsNewRow)
            {
                if (isContinue)
                {
                    dgvSalesPrice.Rows.RemoveAt(dgvSalesPrice.CurrentRow.Index);
                    dgvSalesPrice.CurrentCell = dgvSalesPrice.CurrentRow.Cells[inColIndex];
                    dgvSalesPrice.Focus();
                }
            }
            else
            {
                dgvSalesPrice.Rows[dgvSalesPrice.CurrentRow.Index].Cells["qty"].Value = "";
                dgvSalesPrice.Rows[dgvSalesPrice.CurrentRow.Index].Cells["unit"].Value = null;
                dgvSalesPrice.CurrentCell = dgvSalesPrice.CurrentRow.Cells[inColIndex];
                dgvSalesPrice.Focus();
            }
        }

        private void dgvSalesPrice_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
            }
            catch { }
        }



        private void dgvUnits_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUnits.Columns[dgvUnits.CurrentCell.ColumnIndex].Name == "barcode")
            {
                if (!dgvUnits.CurrentRow.IsNewRow && dgvUnits.CurrentRow.Cells["barcode"].Value != null)
                    //if (!dgvUnits.CurrentRow.IsNewRow)
                    //{
                    //    if (dgvUnits.CurrentRow.Cells["barcode"].Value != null)
                    //    {
                    //        string strBarcode = dgvUnits.CurrentRow.Cells["barcode"].Value.ToString();
                    //        if (strBarcode != "")
                    //        {
                    try
                    {
                        if (SPUnitConversion.CheckBarcodeExist(txtProductCode.Text, dgvUnits.CurrentRow.Cells["barcode"].Value.ToString(), dgvUnits.CurrentRow.Cells["unit"].Value.ToString(), decimal.Parse(dgvUnits.CurrentRow.Cells["defaultQty"].Value.ToString())))
                        {
                            MessageBox.Show("Barcode already exist", "WARNING");
                            dgvUnits.CurrentRow.Cells["barcode"].Value = "";

                        }
                    }
                    catch { }
                //        }
                //    }
                //}
            }
            ////for Block seperate unit conversion for same unit
            //if (dgvUnits.Rows.Count > 2)
            //{
            //    bool isOk = true; string str1 = "", str2 = ""; int count = 0;double iQty = 0, jQty = 0;
            //    for (int i = 0; i < dgvUnits.Rows.Count; i++)
            //    {
            //        for (int j = 1; j < dgvUnits.Rows.Count; j++)
            //        {
            //            if (dgvUnits.Rows[i].Cells["unit"].RowIndex != dgvUnits.Rows[j].Cells["unit"].RowIndex)
            //            {
            //                if (!dgvUnits.Rows[j].IsNewRow && !dgvUnits.Rows[i].IsNewRow)
            //                {
            //                    str1 = dgvUnits.Rows[i].Cells["unit"].Value == null ? string.Empty : dgvUnits.Rows[i].Cells["unit"].Value.ToString();
            //                    str2 = dgvUnits.Rows[j].Cells["unit"].Value == null ? string.Empty : dgvUnits.Rows[j].Cells["unit"].Value.ToString();
            //                    iQty = Convert.ToDouble(dgvUnits.Rows[i].Cells["defaultQty"].Value.ToString());
            //                    jQty = Convert.ToDouble(dgvUnits.Rows[j].Cells["defaultQty"].Value.ToString());
            //                    if (str1 == str2 && iQty != jQty)
            //                    {
            //                        count = count + 1;
            //                        //DataGridViewCell cell = dgvUnits.Rows[i].Cells["ucheck"];
            //                        //cell.Value = string.Copy("x");
            //                        //MessageBox.Show(dgvUnits.Rows[i].Cells["ucheck"].Value.ToString(), "Current Cell");
            //                        //dgvUnits.Rows[i].Cells["ucheck"].Value = "x";
            //                       // dgvUnits.UpdateCellValue(0, i);
            //                       // dgvUnits["ucheck",i].Style.BackColor = Color.FromName("Red");
            //                        dgvUnits["ucheck", i].Style.ForeColor = Color.FromName("Red");
            //                        dgvUnits.Rows[i].Cells["ucheck"].Value = string.Copy("x");
            //                        dgvUnits["ucheck", i].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //                        //dgvUnits["ucheck", i].Style.ForeColor = Color.FromName("Red");
            //                    }

            //                }
            //            }
            //        }
            //    }
            //    if (count > 0)
            //        MessageBox.Show("Please enter same unit conversion", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}
        }
        bool isDontEceutesale; bool isSalesValueChange = true;
        private void dgvSalesPrice_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1 && isSalesValueChange && !isFormLoad && !isDontEceute)
            {
                decimal dQty = 0;
                bool isOk = true;
                //if (dgvUnits.Rows[e.RowIndex].Cells["uqty"].Value != null)
                //{
                //    decimal.TryParse(dgvUnits.Rows[e.RowIndex].Cells["uqty"].Value.ToString(), out dQty);
                //}
                //if (dQty == 0)
                //{
                //    isDontEceute = true;
                //    dgvUnits.Rows[e.RowIndex].Cells["uqty"].Value = "0";
                //    isDontEceute = false;
                //    isOk = false;
                //}
                if (dgvSalesPrice.Rows[e.RowIndex].Cells["PriceAmount"].Value != null)
                {
                    decimal.TryParse(dgvSalesPrice.Rows[e.RowIndex].Cells["PriceAmount"].Value.ToString(), out dQty);
                }
                //if (dQty == 0)
                //{
                //    isDontEceutesale = true;
                //    dgvSalesPrice.Rows[e.RowIndex].Cells["PriceAmount"].Value = "0";
                //    isDontEceutesale = false;
                //}
                var colName = dgvSalesPrice.Columns[e.ColumnIndex].Name;
                if (colName == "UnitId" || colName == "pricingLevelId")
                {
                    if (dgvSalesPrice.IsCurrentCellInEditMode)
                        return;
                }
                if (dgvSalesPrice.Rows[e.RowIndex].Cells["UnitId"].Value == null || dgvSalesPrice.Rows[e.RowIndex].Cells["UnitId"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }
                if (dgvSalesPrice.Rows[e.RowIndex].Cells["PriceAmount"].Value == null || dgvSalesPrice.Rows[e.RowIndex].Cells["PriceAmount"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }
                if (dgvSalesPrice.Rows[e.RowIndex].Cells["SalesPrice"].Value == null || dgvSalesPrice.Rows[e.RowIndex].Cells["SalesPrice"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }
                if (isOk)
                {
                    dgvSalesPrice.Rows[e.RowIndex].Cells["scheck"].Value = "";
                }
                else
                {

                    dgvSalesPrice.Rows[e.RowIndex].Cells["scheck"].Value = "x";
                    dgvSalesPrice["scheck", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                //string salesUnitId = "", unitUnitId = ""; bool isasOk = true;
                //if (dgvSalesPrice.Rows.Count > 0)
                //{
                //    isSalesValueChange = false;
                //    if (!dgvSalesPrice.CurrentRow.IsNewRow)
                //    {
                //        //if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "UnitId")
                //        //{
                //        if (dgvSalesPrice.CurrentRow.Cells["UnitId"].Value != null || dgvSalesPrice.CurrentRow.Cells["UnitId"].Value.ToString() != "")
                //        {
                //            salesUnitId = dgvSalesPrice.CurrentRow.Cells["UnitId"].Value.ToString();
                //            if (dgvUnits.Rows.Count > 1)
                //            {
                //                foreach (DataGridViewRow dgvRowObj in dgvUnits.Rows)
                //                {
                //                    if (!dgvRowObj.IsNewRow)
                //                    {
                //                        if (dgvRowObj.Cells["unit"].Value.ToString() != null)
                //                        {
                //                            unitUnitId = dgvRowObj.Cells["unit"].Value.ToString();
                //                            if (unitUnitId == salesUnitId)
                //                            {
                //                                isasOk = true;
                //                                break;
                //                            }
                //                            else
                //                            {
                //                                isasOk = false;
                //                                dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "x";
                //                                dgvSalesPrice.CurrentRow.Cells["scheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                isasOk = false;
                //            }
                //        }
                //        if (isasOk == false)
                //            MessageBox.Show("Please select assigned units", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        else if (isasOk && isOk)
                //            dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "";
                //        // }
                //    }

                //}
                 bool isasOk= false;
                // if (!isEditEnable)
                 if (!isFromCostPricePer)
                     isasOk = SalesPriceandUnitGridCompare();

              isSalesValueChange = false;
              if (isasOk && isOk)
              {                
                  dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "";

              }
                isSalesValueChange = true;
                if (!isFromCostPricePer)
                {
                    if (dgvSalesPrice.Rows.Count > 2)
                    {
                        isSalesValueChange = false; bool strOk = true;
                        string str1 = "", str2 = "", strPrice1 = "", strPrice2 = ""; int count = 0;
                        for (int i = 0; i < dgvSalesPrice.Rows.Count; i++)
                        {
                            for (int j = i + 1; j < dgvSalesPrice.Rows.Count; j++)
                            {
                                if (!dgvSalesPrice.Rows[j].IsNewRow && !dgvSalesPrice.Rows[i].IsNewRow)
                                {
                                    if (dgvSalesPrice.Rows[i].Cells["UnitId"].RowIndex != dgvSalesPrice.Rows[j].Cells["UnitId"].RowIndex && dgvSalesPrice.Rows[i].Cells["pricingLevelId"].RowIndex != dgvSalesPrice.Rows[j].Cells["pricingLevelId"].RowIndex)
                                    {

                                        str1 = dgvSalesPrice.Rows[i].Cells["UnitId"].Value == null ? string.Empty : dgvSalesPrice.Rows[i].Cells["UnitId"].Value.ToString();
                                        str2 = dgvSalesPrice.Rows[j].Cells["UnitId"].Value == null ? string.Empty : dgvSalesPrice.Rows[j].Cells["UnitId"].Value.ToString();

                                        strPrice1 = dgvSalesPrice.Rows[i].Cells["pricingLevelId"].Value == null ? string.Empty : dgvSalesPrice.Rows[i].Cells["pricingLevelId"].Value.ToString();
                                        strPrice2 = dgvSalesPrice.Rows[j].Cells["pricingLevelId"].Value == null ? string.Empty : dgvSalesPrice.Rows[j].Cells["pricingLevelId"].Value.ToString();

                                        if (str1 == str2 && strPrice1 == strPrice2)
                                        {
                                            strOk = false;
                                            count = count + 1;
                                            dgvSalesPrice.Rows[j].Cells["scheck"].Value = "x";
                                            dgvSalesPrice.Rows[j].Cells["scheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        }
                                        if (isasOk && isOk && strOk)
                                        {
                                            dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "";
                                            dgvSalesPrice.Rows[j].Cells["scheck"].Value = "";
                                        }
                                    }
                                }
                            }
                        }
                        if (count > 0)
                        {
                            MessageBox.Show("Please select different unit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                    isSalesValueChange = true;
                }
            }
        }
        public bool SalesPriceandUnitGridCompare()
        {
            string salesUnitId = "", unitUnitId = ""; bool isasOk = true;
            if (dgvSalesPrice.Rows.Count > 0)
            {
                isSalesValueChange = false;

                if (dgvSalesPrice.CurrentRow != null && !dgvSalesPrice.CurrentRow.IsNewRow)
                {
                    //if (dgvSalesPrice.Columns[dgvSalesPrice.CurrentCell.ColumnIndex].Name == "UnitId")
                    //{

                    if (dgvSalesPrice.CurrentRow.Cells["UnitId"].Value != null && dgvSalesPrice.CurrentRow.Cells["UnitId"].Value.ToString() != "")
                    {
                        salesUnitId = dgvSalesPrice.CurrentRow.Cells["UnitId"].Value.ToString();
                        if (dgvUnits.Rows.Count > 1)
                        {
                            foreach (DataGridViewRow dgvRowObj in dgvUnits.Rows)
                            {
                                if (!dgvRowObj.IsNewRow)
                                {
                                    if (dgvRowObj.Cells["unit"].Value != null)
                                    {
                                        unitUnitId = dgvRowObj.Cells["unit"].Value.ToString();
                                        if (unitUnitId == salesUnitId)
                                        {
                                            isasOk = true;
                                            break;
                                        }
                                        else
                                        {
                                            isasOk = false;
                                            dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "x";
                                            dgvSalesPrice.CurrentRow.Cells["scheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            isasOk = false;
                        }
                    }
                    if (isasOk == false)
                        MessageBox.Show("Please select assigned units", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //else if (isasOk && isOk)
                    //    dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "";
                    // }
                }
                isSalesValueChange = true;
            }
            return isasOk;
        }
        public bool EverythingOkSalesGrid()///created by sheena on 17-05-2023
        {
            bool isOk = true;
            string strMessage = "Rows ";
            //checking for atleast one row contain completed entry
            int inC = 0, inForFirst = 0;

            if (dgvSalesPrice.Rows.Count > 1)
            {
                foreach (DataGridViewRow dgvrowCur in dgvSalesPrice.Rows)
                {

                    if ((dgvrowCur.Cells["scheck"].Value == null ? "" : dgvrowCur.Cells["scheck"].Value.ToString()) == "x")
                    {
                        isOk = false;
                        if (inC == 0)
                        {
                            strMessage = strMessage + Convert.ToString(dgvrowCur.Index + 1);
                            inForFirst = dgvrowCur.Index;
                            inC++;
                        }
                        else
                        {
                            strMessage = strMessage + ", " + Convert.ToString(dgvrowCur.Index + 1);
                        }

                    }


                }
                bool isNoClicked = false;
                //if alteast one row contain completed entry check for continue with incompleted rows
                if (!isOk)
                {
                    strMessage = "Sales price: " + strMessage + " contains invalid entries. Cannot save the entry.";
                    MessageBox.Show(strMessage, "", MessageBoxButtons.OK);
                    //strMessage = "Sales price: " + strMessage + " contains invalid entries. Do you want to continue?";
                    //if (MessageBox.Show(strMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    //{
                    //    //if user decide to continue delete incomplete row
                    //    isOk = true;
                    //    for (int inK = 0; inK < dgvSalesPrice.Rows.Count; inK++)
                    //    {
                    //        if ((dgvSalesPrice.Rows[inK].Cells["scheck"].Value == null ? "" : dgvSalesPrice.Rows[inK].Cells["scheck"].Value.ToString()) == "x")
                    //        {
                    //            if (!dgvSalesPrice.Rows[inK].IsNewRow)
                    //            {

                    //                dgvSalesPrice.Rows.RemoveAt(inK);


                    //            }
                    //            else
                    //            {
                    //                dgvSalesPrice.Rows[inK].Cells["UnitId"].Value = null;
                    //                dgvSalesPrice.Rows[inK].Cells["PriceAmount"].Value = "";
                    //                dgvSalesPrice.Rows[inK].Cells["DiscPercentage"].Value = "";
                    //                dgvSalesPrice.Rows[inK].Cells["DiscAmount"].Value = "";
                    //                dgvSalesPrice.Rows[inK].Cells["SalesPrice"].Value = "";

                    //            }
                    //            inK--;
                    //        }
                    //    }


                    //}
                    //else
                    //{
                    //    //to set focus to incomplete cell
                    isNoClicked = true;
                    //    dgvSalesPrice.Focus();
                    //    decimal amnt=0;

                    //    try
                    //    {
                    //        amnt = decimal.Parse(dgvSalesPrice.Rows[inForFirst].Cells["PriceAmount"].Value.ToString());
                    //    }
                    //    catch
                    //    {
                    //        amnt = 0;
                    //    }
                    //    if (amnt == 0m)
                    //        dgvSalesPrice.Rows[inForFirst].Cells["PriceAmount"].Selected = true;
                    //    try
                    //    {
                    //        if (decimal.Parse(dgvSalesPrice.Rows[inForFirst].Cells["DiscPercentage"].Value.ToString()) == 0m)
                    //            dgvSalesPrice.Rows[inForFirst].Cells["DiscPercentage"].Selected = true;
                    //    }
                    //    catch
                    //    {
                    //        dgvSalesPrice.Rows[inForFirst].Cells["DiscPercentage"].Selected = true;
                    //    }
                    //    try
                    //    {
                    //        if (decimal.Parse(dgvSalesPrice.Rows[inForFirst].Cells["DiscAmount"].Value.ToString()) == 0m)
                    //            dgvSalesPrice.Rows[inForFirst].Cells["DiscAmount"].Selected = true;
                    //    }
                    //    catch
                    //    {
                    //        dgvSalesPrice.Rows[inForFirst].Cells["DiscAmount"].Selected = true;
                    //    }
                    //    try
                    //    {
                    //        if ((dgvSalesPrice.Rows[inForFirst].Cells["UnitId"].Value == null ? "" : dgvSalesPrice.Rows[inForFirst].Cells["UnitId"].Value.ToString()) == "")
                    //            dgvSalesPrice.Rows[inForFirst].Cells["UnitId"].Selected = true;
                    //    }
                    //    catch { dgvSalesPrice.Rows[inForFirst].Cells["UnitId"].Selected = true; }


                    //}


                }
                //if allow for continue check for repated product selection
                if (!isNoClicked)
                {
                    if (dgvSalesPrice.Rows.Count > 2)
                    {
                        string str1 = "", str2 = "", strPricinglevel1 = "", strPricinglevel2 = ""; int count = 0;
                        for (int i = 0; i < dgvSalesPrice.Rows.Count; i++)
                        {
                            for (int j = i + 1; j < dgvSalesPrice.Rows.Count; j++)
                            {
                                if (dgvSalesPrice.Rows[i].Cells["UnitId"].RowIndex != dgvSalesPrice.Rows[j].Cells["UnitId"].RowIndex && dgvSalesPrice.Rows[i].Cells["pricingLevelId"].RowIndex != dgvSalesPrice.Rows[j].Cells["pricingLevelId"].RowIndex)
                                {
                                    if (!dgvSalesPrice.Rows[j].IsNewRow && !dgvSalesPrice.Rows[i].IsNewRow)
                                    {
                                        str1 = dgvSalesPrice.Rows[i].Cells["UnitId"].Value.ToString();
                                        str2 = dgvSalesPrice.Rows[j].Cells["UnitId"].Value.ToString();

                                        strPricinglevel1 = dgvSalesPrice.Rows[i].Cells["pricingLevelId"].Value.ToString();
                                        strPricinglevel2 = dgvSalesPrice.Rows[j].Cells["pricingLevelId"].Value.ToString();
                                        if (str1 == str2 && strPricinglevel1 == strPricinglevel2)
                                        {
                                            count = count + 1;
                                        }
                                    }
                                }
                            }
                        }
                        if (count > 0)
                        {
                            isOk = false;
                            MessageBox.Show("Please select different unit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
            else
            {
                isOk = false;
                MessageBox.Show("Please enter sales price", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return isOk;
        }


        public bool EverythingOkPurchaseGrid()
        {

            string strMessage = "Rows ";
            //checking for atleast one row contain completed entry
            int inC = 0, inForFirst = 0;
            bool isOk = true;

            if (dgvProductPurchasePrice.Rows.Count > 1)
            {
                foreach (DataGridViewRow dgvrowCur in dgvProductPurchasePrice.Rows)
                {
                    if ((dgvrowCur.Cells["ppp_ucheck"].Value == null ? "" : dgvrowCur.Cells["ppp_ucheck"].Value.ToString()) == "x")
                    {
                        isOk = false;
                        if (inC == 0)
                        {
                            strMessage = strMessage + Convert.ToString(dgvrowCur.Index + 1);
                            inForFirst = dgvrowCur.Index;
                            inC++;
                        }
                        else
                        {
                            strMessage = strMessage + ", " + Convert.ToString(dgvrowCur.Index + 1);
                        }
                    }
                }
                bool isNoClicked = false;
                //if alteast one row contain completed entry check for continue with incompleted rows
                if (!isOk)
                {
                    strMessage = "Purchase price: " + strMessage + " contains invalid entries. Do you want to continue?";
                    if (MessageBox.Show(strMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        //if user decide to continue delete incomplete row
                        isOk = true;
                        for (int inK = 0; inK < dgvProductPurchasePrice.Rows.Count; inK++)
                        {
                            if ((dgvProductPurchasePrice.Rows[inK].Cells["ppp_ucheck"].Value == null ? "" : dgvProductPurchasePrice.Rows[inK].Cells["ppp_ucheck"].Value.ToString()) == "x")
                            {
                                if (!dgvProductPurchasePrice.Rows[inK].IsNewRow)
                                {
                                    dgvProductPurchasePrice.Rows.RemoveAt(inK);
                                }
                                else
                                {
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_unit"].Value = null;
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_amount"].Value = "";
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_discper"].Value = "";
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_discamt"].Value = "";
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_purchaseprice"].Value = "";

                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_purchasepriceid"].Value = null;
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_ledgerid"].Value = null;
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_lrdgername"].Value = null;
                                    dgvProductPurchasePrice.Rows[inK].Cells["ppp_date"].Value = null;
                                }
                                inK--;
                            }
                        }
                    }
                    else
                    {
                        //to set focus to incomplete cell
                        isNoClicked = true;
                        dgvProductPurchasePrice.Focus();
                        decimal amnt = 0;
                        try
                        {
                            amnt = decimal.Parse(dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_amount"].Value.ToString());
                        }
                        catch
                        {
                            amnt = 0;
                        }
                        if (amnt == 0m)
                            dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_amount"].Selected = true;



                        try
                        {
                            if (decimal.Parse(dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_discper"].Value.ToString()) == 0m)
                                dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_discper"].Selected = true;
                        }
                        catch
                        {
                            dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_discper"].Selected = true;
                        }
                        try
                        {
                            if (decimal.Parse(dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_discamt"].Value.ToString()) == 0m)
                                dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_discamt"].Selected = true;
                        }
                        catch
                        {
                            dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_discamt"].Selected = true;
                        }
                        try
                        {
                            if ((dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_unit"].Value == null ? "" : dgvSalesPrice.Rows[inForFirst].Cells["UnitId"].Value.ToString()) == "")
                                dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_unit"].Selected = true;
                        }
                        catch
                        {
                            dgvProductPurchasePrice.Rows[inForFirst].Cells["ppp_unit"].Selected = true;
                        }
                    }
                }


                //if allow for continue check for repated product selection
                if (!isNoClicked)
                {
                    if (dgvProductPurchasePrice.Rows.Count > 2)
                    {
                        string str1 = "", str2 = ""; int count = 0;
                        string strlederid1 = "", strledgerid2 = "";
                        for (int i = 0; i < dgvProductPurchasePrice.Rows.Count; i++)
                        {
                            for (int j = 1; j < dgvProductPurchasePrice.Rows.Count; j++)
                            {
                                if (dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].RowIndex != dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].RowIndex)
                                {
                                    if (!dgvProductPurchasePrice.Rows[j].IsNewRow && !dgvProductPurchasePrice.Rows[i].IsNewRow)
                                    {
                                        str1 = dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].Value.ToString();
                                        str2 = dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].Value.ToString();
                                        strlederid1 = dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value.ToString();
                                        strledgerid2 = dgvProductPurchasePrice.Rows[j].Cells["ppp_ledgerid"].Value.ToString();
                                        if (str1 == str2 && strlederid1 == strledgerid2)
                                        {
                                            count = count + 1;
                                        }
                                    }
                                }
                            }
                        }
                        if (count > 0)
                        {
                            isOk = false;
                            MessageBox.Show("Please select different unit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            return isOk;
        }

        private void txtProductName_Enter(object sender, EventArgs e)
        {
            lblProductName.ForeColor = Color.Red;
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            lblProductName.ForeColor = Color.Black;
            // Only translate if txtArabicName is empty or has not been manually edited
            
        }

        private void txtProductNameArabic_Leave(object sender, EventArgs e)
        {
            lblProductNameArabic.ForeColor = Color.Black;
        }

        private void txtProductNameArabic_Enter(object sender, EventArgs e)
        {
            lblProductNameArabic.ForeColor = Color.Red;
        }

        private void txtProductCode_Enter(object sender, EventArgs e)
        {
            lblProductCode.ForeColor = Color.Red;
        }

        private void cmbTaxType_Enter(object sender, EventArgs e)
        {
            lblTaxType.ForeColor = Color.Red;
        }

        private void cmbTaxType_Leave(object sender, EventArgs e)
        {
            lblTaxType.ForeColor = Color.Black;
        }

        private void cmbCategory_Enter(object sender, EventArgs e)
        {
            lblCategory.ForeColor = Color.Red;
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            lblCategory.ForeColor = Color.Black;
        }

        private void cmbAllowBatch_Enter(object sender, EventArgs e)
        {
            lblAllowBatch.ForeColor = Color.Red;
        }

        private void cmbAllowBatch_Leave(object sender, EventArgs e)
        {
            lblAllowBatch.ForeColor = Color.Black;
        }

        private void cmbBom_Enter(object sender, EventArgs e)
        {
            lblBOM.ForeColor = Color.Red;
        }

        private void cmbMultipleUnit_Enter(object sender, EventArgs e)
        {
            lblMultipleUnit.ForeColor = Color.Red;
        }

        private void txtPartNo_Enter(object sender, EventArgs e)
        {
            lblPartNo.ForeColor = Color.Red;
        }

        private void cmbOpeningStock_Leave(object sender, EventArgs e)
        {
            lblOpeningStock.ForeColor = Color.Black;
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            lblNarration.ForeColor = Color.Black;
        }

        private void dgvUnits_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUnits.Columns["barcode"].Index == e.ColumnIndex && !isDontExecuteCellEnter)
            {
                // dgvProduct_CellClick(sender, e);
            }
        }

        private void FillGroupCategory1()
        {
            try
            {


                //cmbGroup.DataSource = ProductGroupSP.ProductGroupViewAll();//commented by sheena 26-06-2023
                cmbGroupCat1.DataSource = ProductGroupSP.ProductGroupViewAllSubGroupByCategory("Category 1", "1");
                if (cmbGroupCat1.DataSource != null)
                {
                    cmbGroupCat1.DisplayMember = "groupName";
                    cmbGroupCat1.ValueMember = "groupId";

                    try { cmbGroupCat1.SelectedIndex = 0; }
                    catch
                    { }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC52:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FillGroupCategory2()
        {
            try
            {
                if (cmbGroupCat1.SelectedValue != null)
                {
                    string strGroupUnder = cmbGroupCat1.SelectedValue.ToString();
                    //cmbGroup.DataSource = ProductGroupSP.ProductGroupViewAll();//commented by sheena 26-06-2023
                    DataTable dt=ProductGroupSP.ProductGroupViewAllSubGroupByCategory("Category 2", strGroupUnder);
                    if (dt.Rows.Count > 0)
                    {
                        cmbGroupCat2.DataSource = dt;
                        if (cmbGroupCat2.DataSource != null)
                        {
                            cmbGroupCat2.DisplayMember = "groupName";
                            cmbGroupCat2.ValueMember = "groupId";

                            try { cmbGroupCat2.SelectedIndex = 0; }
                            catch
                            { }
                        }

                    }
                    else
                    {
                        cmbGroupCat2.DataSource = null;
                        cmbGroupCat2.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC53:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FillGroupCategory3()
        {
            try
            {
                if (cmbGroupCat2.SelectedValue != null)
                {
                    string strGroupUnder = cmbGroupCat2.SelectedValue.ToString();
                    //cmbGroup.DataSource = ProductGroupSP.ProductGroupViewAll();//commented by sheena 26-06-2023
                    DataTable dt = ProductGroupSP.ProductGroupViewAllSubGroupByCategory("Category 3", strGroupUnder);
                    if (dt.Rows.Count > 0)
                    {
                        cmbGroupCat3.DataSource = dt;
                        if (cmbGroupCat3.DataSource != null)
                        {
                            cmbGroupCat3.DisplayMember = "groupName";
                            cmbGroupCat3.ValueMember = "groupId";
                            try { cmbGroupCat3.SelectedIndex = 0; }
                            catch
                            { }

                        }
                    }
                    else
                    {
                        cmbGroupCat3.DataSource = null;
                        cmbGroupCat3.Text = "";
                    }
                }
                else
                {
                    cmbGroupCat3.DataSource = null;
                    cmbGroupCat3.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC54:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //----------------------Fill Product Group  combo-----------------------------------------------------------
        public void FillProductGroupCombo()
        {
            try
            {
                if (cmbGroupCat3.SelectedValue != null)
                {
                    string strGroupUnder = cmbGroupCat3.SelectedValue.ToString();
                    //cmbGroup.DataSource = ProductGroupSP.ProductGroupViewAll();//commented by sheena 26-06-2023
                    DataTable dt = ProductGroupSP.ProductGroupViewAllSubGroupByCategory("Category 4", strGroupUnder);
                    if (dt.Rows.Count > 0)
                    {
                        cmbGroup.DataSource = dt;
                        if (cmbGroup.DataSource != null)
                        {
                            cmbGroup.DisplayMember = "groupName";
                            cmbGroup.ValueMember = "groupId";

                            try { cmbGroupCat3.SelectedIndex = 0; }
                            catch
                            { }
                        }
                    }
                    else
                    {
                        cmbGroup.DataSource = null;
                        cmbGroup.Text = "";
                    }
                }
                else
                {
                    cmbGroup.DataSource = null;
                    cmbGroup.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC55:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbGroupCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGroupCategory2();
            //FillGroupCategory3();
        }

        private void cmbGroupCat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGroupCategory3();
        }

        private void cmbGroupCat3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProductGroupCombo();
        }

        private void btnCat1New_Click(object sender, EventArgs e)
        {
            try
            {
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Product Group", "", "Masters") == true)
                //  //  if (checkuserprivilege.CheckPrivilage("Product Group", "") == true)
                //{
                    // Save current product group Id
                    //if (cmbGroup.SelectedValue != null)
                    //{
                    //    strOldGrpId = cmbGroup.SelectedValue.ToString();
                    //}
                    //else
                    //{
                    //    strOldGrpId = "";
                    //}
                    frmProductGroup frmproductgroup = new frmProductGroup();


                    frmproductgroup.MdiParent = MDIFinacPOS.MDIObj;
                    frmproductgroup.DoWhenComingFromProductCreationForm(this, "Category 1");
                    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("PC57" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCat2New_Click(object sender, EventArgs e)
        {
            try
            {
                //if (spUsergroupSettings.CheckUSerGroupPrivilage("Product Group", "", "Masters") == true)
                //   // if (checkuserprivilege.CheckPrivilage("Product Group", "") == true)
                //{
                    // Save current product group Id
                    //if (cmbGroup.SelectedValue != null)
                    //{
                    //    strOldGrpId = cmbGroup.SelectedValue.ToString();
                    //}
                    //else
                    //{
                    //    strOldGrpId = "";
                    //}
                    frmProductGroup frmproductgroup = new frmProductGroup();


                    frmproductgroup.MdiParent = MDIFinacPOS.MDIObj;
                    frmproductgroup.DoWhenComingFromProductCreationForm(this, "Category 2");
                    this.Enabled = false;
                //}
                //else
                //{
                //    MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show("PC58" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCat3New_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (spUsergroupSettings.CheckUSerGroupPrivilage("Product Group", "", "Masters") == true)
            //      //  if (checkuserprivilege.CheckPrivilage("Product Group", "") == true)
            //    {
            //        // Save current product group Id
            //        //if (cmbGroup.SelectedValue != null)
            //        //{
            //        //    strOldGrpId = cmbGroup.SelectedValue.ToString();
            //        //}
            //        //else
            //        //{
            //        //    strOldGrpId = "";
            //        //}
            //        frmProductGroup frmproductgroup = new frmProductGroup();


            //        frmproductgroup.MdiParent = MDIFinacAcount.MDIObj;
            //        frmproductgroup.DoWhenComingFromProductCreationForm(this, "Category 3");
            //        this.Enabled = false;
            //    }
            //    else
            //    {
            //        MessageBox.Show("You don’t have privilege", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Enabled = true;
            //    MessageBox.Show("PC59" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void cmbGroupCat1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (cmbProductMainGrp.Text.Trim() == "" || cmbProductMainGrp.SelectionStart == 0)
                //    //{
                //    cmbProductMainGrp.Focus();     
                //    // }
                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                   // cmbGroupCat2.Focus();
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
                else if (e.Alt && e.KeyCode == Keys.C)
                {
                    SendKeys.Send("{F10}");
                    btnCat1New_Click(sender, e);
                }

                DropDownCombo(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC33:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void cmbGroupCat2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (cmbProductMainGrp.Text.Trim() == "" || cmbProductMainGrp.SelectionStart == 0)
                //    //{
                //    cmbGroupCat1.Focus();
                //    // }
                //}
                //else
                if (e.KeyCode == Keys.Enter)
                {
                   // cmbGroupCat3.Focus();
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
                else if (e.Alt && e.KeyCode == Keys.C)
                {
                    SendKeys.Send("{F10}");
                    btnCat2New_Click(sender, e);
                }

                DropDownCombo(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC33:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbGroupCat3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.Control && e.KeyCode == Keys.Tab)
                //{
                //    //if (cmbProductMainGrp.Text.Trim() == "" || cmbProductMainGrp.SelectionStart == 0)
                //    //{
                //    cmbGroupCat2.Focus();
                //    // }
                //}
                //else 
                if (e.KeyCode == Keys.Enter)
                {
                   // cmbGroup.Focus();
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
                else if (e.Alt && e.KeyCode == Keys.C)
                {
                    SendKeys.Send("{F10}");
                    btnCat3New_Click(sender, e);
                }

                DropDownCombo(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("PC33:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if(e.KeyChar==(char)Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbGroupCat3_Leave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Black;
            if (isEditEnable==false )
            {

                FillProductGroupCombo();
            }
        }

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    frmLookup frmlookup = new frmLookup();

        //    frmlookup.strSearchingName = "Product";
        //    frmlookup.strFromFormName = "Product";
        //    frmlookup.strSearchColumn = "productName";
        //    frmlookup.strSearchOrder = " productCode desc ";
        //    frmlookup.strSearchQry = " productCode,prdCode,productName,groupName,partNo,alternateNo ";
        //    if (PurchaseSettingsInfo._ShowPartNo)
        //    {
        //        frmlookup.strSearchQry = " productCode,prdCode,productName,groupName,partNo,alternateNo ";
        //        frmlookup.strSearchTable = " (SELECT P.productCode productCode,P.partNo,D.alternateNo, P.productCode prdCode,P.productName,G.groupName FROM tbl_Product (NOLOCK) AS P,tbl_ProductDetails (NOLOCK) AS D,tbl_ProductGroup(NOLOCK) AS G WHERE P.productCode=D.productCode and  P.groupId=G.groupId ) AS A ";
        //    }
        //    else
        //    {
        //        frmlookup.strSearchQry = " productCode,prdCode,productName,groupName ";
        //        frmlookup.strSearchTable = " (SELECT P.productCode productCode,P.productCode prdCode,P.productName,G.groupName FROM tbl_Product (NOLOCK) AS P,tbl_ProductGroup(NOLOCK) AS G WHERE P.groupId=G.groupId ) AS A ";
        //    }
        //    frmlookup.strSearchCondition = "";
        //    frmlookup.strMasterIdColumnName = "productCode";
        //    frmlookup.IntSearchFiledCount = 3;

        //    frmlookup.DoWhenComingFromProductCreationForm(this);
        //}
        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmLookupMaster frmlookup = new frmLookupMaster();

            frmlookup.strSearchingName = "Product";
            frmlookup.strFromFormName = "Product";
           
            frmlookup.strSearchOrder = " productCode desc ";

            if (PurchaseSettingsInfo._ShowPartNo)
            {
                if (PublicVariables._ModuleLanguage == "ARB")
                { 
                    frmlookup.strSearchColumn = "ArabicProductName";

                frmlookup.strSearchQry = "productCode,prdCode,ArabicProductName,groupName,partNo,alternateNo";
                    frmlookup.strSearchTable = @"(
            SELECT 
                P.productCode productCode,
                P.partNo,
                D.alternateNo,
                P.productCode prdCode,
     P.extra1 ArabicProductName,
                G.groupName
            FROM tbl_Product (NOLOCK) AS P
            INNER JOIN tbl_ProductDetails (NOLOCK) AS D ON P.productCode = D.productCode
            INNER JOIN tbl_ProductGroup (NOLOCK) AS G ON P.groupId = G.groupId
        ) AS A";
                    frmlookup.IntSearchFiledCount = 6; // since we have 7 columns in the _ShowPartNo=true and arabic case
                }
                else if(PublicVariables._ModuleLanguage=="ENG")
                {
                    frmlookup.strSearchColumn = "productName";

                    frmlookup.strSearchQry = "productCode,prdCode,productName,groupName,partNo,alternateNo";
                    frmlookup.strSearchTable = @"(
            SELECT 
                P.productCode productCode,
                P.partNo,
                D.alternateNo,
                P.productCode prdCode,
                P.productName,
                G.groupName
            FROM tbl_Product (NOLOCK) AS P
            INNER JOIN tbl_ProductDetails (NOLOCK) AS D ON P.productCode = D.productCode
            INNER JOIN tbl_ProductGroup (NOLOCK) AS G ON P.groupId = G.groupId
        ) AS A";
                    frmlookup.IntSearchFiledCount = 6; // since we have 7 columns in the _ShowPartNo=true and arabic case

                }
            }
            else
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    frmlookup.strSearchColumn = "productName";
                    frmlookup.strSearchQry = "productCode,prdCode,productName,groupName";
                    frmlookup.strSearchTable = @"(
            SELECT 
                P.productCode productCode,
                P.productCode prdCode,
                P.productName,
                G.groupName
            FROM tbl_Product (NOLOCK) AS P
            INNER JOIN tbl_ProductGroup (NOLOCK) AS G ON P.groupId = G.groupId
        ) AS A";
                    frmlookup.IntSearchFiledCount = 4; // since we have 4 columns in the _ShowPartNo=false case
                }
                else if(PublicVariables._ModuleLanguage=="ARB")
                {
                    frmlookup.strSearchColumn = "ArabicProductName";
                    frmlookup.strSearchQry = "productCode,prdCode,ArabicProductName,groupName";
                    frmlookup.strSearchTable = @"(
            SELECT 
                P.productCode productCode,
                P.productCode prdCode,
     P.extra1 ArabicProductName,
                G.groupName
            FROM tbl_Product (NOLOCK) AS P
            INNER JOIN tbl_ProductGroup (NOLOCK) AS G ON P.groupId = G.groupId
        ) AS A";
                    frmlookup.IntSearchFiledCount = 4; // since we have 4 columns in the _ShowPartNo=false case
                }
            }

            frmlookup.strSearchCondition = "";
            frmlookup.strMasterIdColumnName = "productCode";
            frmlookup.DoWhenComingFromProductCreationForm(this);
        }


        public void DowhenReturningFromSearchForm(string strId)
        {
            try
            {
                if (strId != "")
                {
                    strProductIdToEdit = strId;
                    isClear = false;
                    FillProductForEdit(strProductIdToEdit);
                    isFromOther = false;
                }

                txtProductName.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("PC13:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillProductMainGroupCombo();
            FillGroupCategory1();
            FillGroupCategory2();
            FillGroupCategory3();
            FillProductGroupCombo();
            FillBrandCombo();
            FillUnitCombo();
            FillTaxCombo();
            cmbCategory.SelectedIndex = 0;
            cmbAllowBatch.Text = "No";
            cmbBom.Text = "No";
            cmbMultipleUnit.Text = "No";

            cmbOpeningStock.Text = "No";
        }

        private void bwrkControlSettings_DoWork(object sender, DoWorkEventArgs e)
        {
            //FinacFormControl objGeneral = new FinacFormControl();
            //objGeneral.formSettings(this);
        }

        private void bwrkControlSettings_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if (frmCompanyProgress != null && frmCompanyProgress.Visible)
            //    frmCompanyProgress.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //frmBarcodePrinting frmObj = new frmBarcodePrinting();
            //frmObj.MdiParent = MDIFinacAcount.MDIObj;
            //frmObj.Show();  
            //frmObj.CallThisFormFromProductCreation(this, txtProductCode.Text.Trim());
        }


        DataTable dtblFromCreation = new DataTable();
        public void InitialSettingsAccordingToCallingForm()
        {
            dtblFromCreation = dtblRowMaterials;

            DgvBom.Rows.Clear();
            
            dtblRowMaterials = new DataTable();
            DataColumn c0 = new DataColumn("BomrowMaterial");
            dtblRowMaterials.Columns.Add(c0);
            DataColumn c1 = new DataColumn("Bomqty");
            dtblRowMaterials.Columns.Add(c1);
            DataColumn c2 = new DataColumn("Bomunitid");
            dtblRowMaterials.Columns.Add(c2);
            DataColumn c3 = new DataColumn("BomrowMaterialname");
            dtblRowMaterials.Columns.Add(c3);

            if (dtblFromCreation.Rows.Count > 0)
            {
                foreach (DataRow drowCur in dtblFromCreation.Rows)
                {
                    DgvBom.Rows.Add();
                    //FillProductComboForGridCell(DgvBom.Rows.Count - 2);
                    DgvBom.CurrentCell = DgvBom.Rows[DgvBom.Rows.Count - 2].Cells["BomrowMaterial"];
                    DgvBom.Rows[DgvBom.Rows.Count - 2].Cells["BomrowMaterialCode"].Value = drowCur.ItemArray[0].ToString();
                    DgvBom.Rows[DgvBom.Rows.Count - 2].Cells["Bomqty"].Value = drowCur.ItemArray[1].ToString();
                    
                    DgvBom.Rows[DgvBom.Rows.Count - 2].Cells["Bomunit"].Value = drowCur.ItemArray[2].ToString();
                    DgvBom.Rows[DgvBom.Rows.Count - 2].Cells["BomrowMaterial"].Value = drowCur.ItemArray[3].ToString();
                }
            }

            txtrawmeterial.Visible = false;
        }
        public void FillProductComboForGridCell(int inRowIndex)
        {

            //DataGridViewComboBoxCell dgvccProductCode = (DataGridViewComboBoxCell)DgvBom[DgvBom.Columns["BomrowMaterial"].Index, inRowIndex];

            DataTable dtbl = new DataTable();
            
            //if (strFromCall == "BOM")
            //{
            dtbl = SPProduct.ProductViewAllWithOutBOM();
            //}
            //else
            //{
            //    dtbl = SPProduct.ProductViewAllWithOutPackage();
            //}

            strDefaultId = txtProductCode.Text;

            if (strDefaultId != "")
            {
                if (dtbl != null)
                {
                    foreach (DataRow drowObj in dtbl.Rows)
                    {
                        if (drowObj.ItemArray[0].ToString() == strDefaultId)
                        {
                            dtbl.Rows.Remove(drowObj);
                            break;
                        }
                    }
                }
            }
            DataRow drow = dtbl.NewRow();
            dtbl.Rows.InsertAt(drow, 0);
            try
            {
                //dgvccProductCode.DataSource = dtbl;

                if (dtbl.Rows.Count > 1 && DgvBom.Rows.Count > 0)
                {
                    for (int i = 1; i < dtbl.Rows.Count; i++)
                    {
                        foreach (DataGridViewRow dgvRow in DgvBom.Rows)
                        {

                            if (dgvRow.Cells["BomrowMaterial"].Value != null && dgvRow.Cells["BomrowMaterial"].Value.ToString() != "")
                            {
                                if (inRowIndex != dgvRow.Index)
                                {

                                    if (dtbl.Rows[i].ItemArray[0].ToString() == dgvRow.Cells["BomrowMaterial"].Value.ToString())
                                    {
                                        dtbl.Rows.RemoveAt(i);
                                        i--;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (dtbl.Rows.Count == 1)
                {
                    dtbl.Rows.Clear();
                }

                //dgvccProductCode.DisplayMember = "productName";
                //dgvccProductCode.ValueMember = "productCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show("BM1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void RemoveRowBom()
        {
            strDefaultId = txtProductCode.Text;
            int inColIndex = DgvBom.CurrentCell.ColumnIndex;
            bool isContinue = true;
            if (!DgvBom.Rows[DgvBom.CurrentRow.Index].IsNewRow)
            {
                if (SPProductDetails.ProductBOMSubPrdReferenceCheck(strDefaultId, DgvBom.CurrentRow.Cells["BomrowMaterial"].Value.ToString()))
                {
                    isContinue = false;
                }

                if (!isContinue)
                {
                    MessageBox.Show("Cant remove , reference exist", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                if (isContinue)
                {
                    DgvBom.Rows.RemoveAt(DgvBom.CurrentRow.Index);
                    DgvBom.CurrentCell = DgvBom.CurrentRow.Cells[inColIndex];
                    DgvBom.Focus();
                }
            }
            else
            {
                DgvBom.Rows[DgvBom.CurrentRow.Index].Cells["BomrowMaterial"].Value = null;
                DgvBom.Rows[DgvBom.CurrentRow.Index].Cells["Bomqty"].Value = "";
                DgvBom.Rows[DgvBom.CurrentRow.Index].Cells["Bomunit"].Value = "";
                DgvBom.CurrentCell = DgvBom.CurrentRow.Cells[inColIndex];
                DgvBom.Focus();
            }
        }
        public void RemoveRowNutrition()
        {
            int inColIndex = dgvUnits.CurrentCell.ColumnIndex;
            bool isContinue = true;          
          
            if (!dgvNutritionFacts.Rows[dgvNutritionFacts.CurrentRow.Index].IsNewRow)
            {               
                if (isContinue)
                {
                    dgvNutritionFacts.Rows.RemoveAt(dgvNutritionFacts.CurrentRow.Index);
                    dgvNutritionFacts.CurrentCell = dgvNutritionFacts.CurrentRow.Cells[inColIndex];
                    dgvNutritionFacts.Focus();
                }
            }
            else
            {
                dgvNutritionFacts.Rows[dgvNutritionFacts.CurrentRow.Index].Cells["NutName"].Value = "";
                dgvNutritionFacts.Rows[dgvNutritionFacts.CurrentRow.Index].Cells["Value"].Value = "";
                dgvNutritionFacts.CurrentCell = dgvNutritionFacts.CurrentRow.Cells[inColIndex];
                dgvNutritionFacts.Focus();
            }
        }


        private void DgvBom_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (DgvBom.Columns[e.ColumnIndex].Name == "BomrowMaterial")
            {
                strDefaultId = txtProductCode.Text;
               


                //FillProductComboForGridCell(e.RowIndex);

                //if (DgvBom.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                //{
                //    DataGridViewComboBoxCell cmb = (DataGridViewComboBoxCell)DgvBom[e.ColumnIndex, e.RowIndex];
                //    DataTable dtbl = (DataTable)cmb.DataSource;
                //    if (dtbl != null && dtbl.Rows.Count > 1)
                //    {
                //        DgvBom.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dtbl.Rows[0]["productCode"].ToString();
                //    }
                //}
                //else
                //{
                //    if (SPProductDetails.ProductBOMSubPrdReferenceCheck(strDefaultId, DgvBom.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                //    {
                //        e.Cancel = true;
                //    }
                //}
            }
        }
        public void AssignProductDefaultValues(int index, string productCode)
        {
           
            isValueChange = false;
            ProductInfo infoproduct = new ProductInfo();
            infoproduct = SPProduct.ProductViewByBranch(productCode, PublicVariables._branchId);
            DgvBom.Rows[index].Cells["BomrowMaterial"].Value = infoproduct.ProductName;

            ProductDetailsInfo InfoProductDetails = new ProductDetailsInfo();
            ProductDetailsSP spdetails = new ProductDetailsSP();
            InfoProductDetails = spdetails.ProductDetailsViewByBranch(infoproduct.ProductCode);

            objTransactionsGeneralFill.FillUnitPerQtyComboForGridBom(DgvBom, productCode, index);

            //Added on 15/Sep/2023
            if (strUnitIdfromProductPopup != "")
            {
                DgvBom.Rows[index].Cells["Bomunit"].Value = strUnitIdfromProductPopup;
                strUnitIdfromProductPopup = "";
            }
            isValueChange = true;
        }
        private void DgvBom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                string strPrdId = "";
                
                decimal dQty = 0, dChkQty = 1;

                    int inCouChk = 0;
                    try
                    {
                        strPrdId = DgvBom.Rows[e.RowIndex].Cells["BomrowMaterial"].Value.ToString();
                        if (strPrdId != "")
                        {
                            inCouChk++;
                        }
                    }
                    catch
                    {
                    }

                    try
                    {
                        dQty = decimal.Parse(DgvBom.Rows[e.RowIndex].Cells["Bomqty"].Value.ToString());
                        dChkQty = decimal.Parse(DgvBom.Rows[e.RowIndex].Cells["Bomqty"].Value.ToString());
                        inCouChk++;
                    }
                    catch
                    {
                        DgvBom.Rows[e.RowIndex].Cells["Bomqty"].Value = "";
                    }

                    // -------------------------set ok or not ok-------------------------------------------
                    if ((inCouChk == 2 && dChkQty != 0m) || inCouChk == 0)
                    {
                        try
                        {
                            DgvBom.Rows[e.RowIndex].Cells["BomrowMaterial"].Value.ToString();
                            DgvBom.Rows[e.RowIndex].Cells["Bomcheck"].Value = "";
                        }
                        catch
                        {
                            DgvBom.Rows[e.RowIndex].Cells["Bomcheck"].Value = "x";
                            DgvBom["Bomcheck", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    else
                    {

                        DgvBom.Rows[e.RowIndex].Cells["Bomcheck"].Value = "x";
                        DgvBom["Bomcheck", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }


                    if (DgvBom.Columns[e.ColumnIndex].Name == "BomrowMaterialCode")
                    {
                        string strPrdCode = "";
                        ProductInfo InfoProduct = new ProductInfo(); //Changed to function
                        if (DgvBom.Rows[e.RowIndex].Cells["BomrowMaterialCode"].Value != null && DgvBom.Rows[e.RowIndex].Cells["BomrowMaterialCode"].Value.ToString() != "")
                        {
                            strPrdCode = DgvBom.Rows[e.RowIndex].Cells["BomrowMaterialCode"].Value.ToString();
                            InfoProduct = SPProduct.ProductViewByBranch(strPrdCode, PublicVariables._branchId);
                            strPrdCode = InfoProduct.ProductCode;
                            if (strPrdCode == null)
                                strPrdCode = "";
                        }
                        if (strPrdCode != "")
                        {
                            AssignProductDefaultValues(e.RowIndex, strPrdCode);
                        }
                        else
                        {
                            isValueChange = false;
                            //dgvProduct.Rows[e.RowIndex].Cells["prdCodeToKeep"].Value = "";
                            isValueChange = true;
                            DgvBom.Rows[e.RowIndex].Cells["BomrowMaterial"].Value = "";
                            DgvBom.Rows[e.RowIndex].Cells["BomrowMaterialCode"].Value = "";

                        }
                    }
                    else if (DgvBom.Columns[e.ColumnIndex].Name == "BomrowMaterial")
                    {
                        if (strPrdId != "")
                        {
                            //DataGridViewComboBoxCell dgvccProductCode = (DataGridViewComboBoxCell)DgvBom[e.ColumnIndex, e.RowIndex];

                            //DataRow[] dr = ((DataTable)dgvccProductCode.DataSource).Select("productCode ='" + strPrdId + "'");
                            //DataSet dset = new DataSet();
                            //dset.Merge(dr);

                            //DgvBom.CurrentRow.Cells["Bomunit"].Value = dset.Tables[0].Rows[0].ItemArray[11].ToString();
                        }
                    }
            }
        }

        private void DgvBom_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBoxControl = e.Control as DataGridViewTextBoxEditingControl;
                if (TextBoxControl != null)
                {
                    TextBoxControl.KeyPress += TextBoxCellEditControlKeyPress;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BM5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DgvBom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (DgvBom.Rows.Count > 1)
                    {
                        if ((DgvBom.CurrentRow.Cells["BomrowMaterial"].Value == null ? "" : DgvBom.CurrentRow.Cells["BomrowMaterial"].Value.ToString()) == "")
                        {
                            
                            DgvBom.ClearSelection();
                            DgvBom.CurrentCell = null;
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if ((DgvBom.CurrentRow.Cells["BomrowMaterial"].Value == null ? "" : DgvBom.CurrentRow.Cells["BomrowMaterial"].Value.ToString()) != "")
                        {
                            
                            DgvBom.ClearSelection();
                            DgvBom.CurrentCell = null;
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BM4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DgvBom_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DgvBom["Bomcheck", e.RowIndex].Style.BackColor = Color.FromName("Control");
        }

        private void lnklblBomRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (DgvBom.SelectedCells.Count > 0 && DgvBom.CurrentRow != null)
                {
                    if (DgvBom.CurrentCell != DgvBom.CurrentRow.Cells["Bomcheck"])
                    {

                        if (!DgvBom.CurrentRow.IsNewRow)
                        {
                            if (DgvBom.RowCount > 1)
                            {
                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        RemoveRowBom();
                                    }
                                    else
                                    {
                                        DgvBom.Focus();
                                    }
                                }

                                else
                                {
                                    RemoveRowBom();
                                }
                            }
                            else
                            {

                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        DgvBom.Rows.Clear();
                                    }
                                }
                                else
                                {
                                    DgvBom.Rows.Clear(); 
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BM3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cmbBom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbBom.Text=="Yes")
            {
                DgvBom.Enabled = true;
                lnklblBomRemove.Enabled = true;
                //button1.Enabled = true;
                //button2.Enabled = true;
                InitialSettingsAccordingToCallingForm();
            }
            else
            {
                DgvBom.Enabled = false;
                lnklblBomRemove.Enabled = false;
                //button1.Enabled = false;
                //button2.Enabled = false;
                DgvBom.Rows.Clear();
            }
        }

        private void DgvBom_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvBom.CurrentCellAddress.X == 1)
            {
                int X = 0;
                int Y = 0;
                if (PublicVariables._ModuleLanguage == "ARB")
                {
                    X = DgvBom.GetCellDisplayRectangle(DgvBom.CurrentCell.ColumnIndex, DgvBom.CurrentCell.RowIndex, true).Left + DgvBom.Left - 805;
                    Y = DgvBom.GetCellDisplayRectangle(DgvBom.CurrentCell.ColumnIndex, DgvBom.CurrentCell.RowIndex, true).Bottom + 143;
                }
                else
                {
                    X = DgvBom.GetCellDisplayRectangle(DgvBom.CurrentCell.ColumnIndex, DgvBom.CurrentCell.RowIndex, true).Left+DgvBom.Left;
                    Y = DgvBom.GetCellDisplayRectangle(DgvBom.CurrentCell.ColumnIndex, DgvBom.CurrentCell.RowIndex, true).Bottom + 25;
                }

                txtrawmeterial.Location = new Point(X, Y);
                txtrawmeterial.Size = new Size(DgvBom.Columns[DgvBom.CurrentCell.ColumnIndex].Width, txtProductName.Height);
                txtrawmeterial.Text = "";
                if (DgvBom.Rows[DgvBom.CurrentCell.RowIndex].Cells["BomrowMaterial"].Value != null && DgvBom.Rows[DgvBom.CurrentCell.RowIndex].Cells["BomrowMaterial"].Value.ToString().Trim() != "")
                {
                    isProductNameTextFill = true;
                    txtrawmeterial.Text = DgvBom.Rows[DgvBom.CurrentCell.RowIndex].Cells["BomrowMaterial"].Value.ToString();
                    isProductNameTextFill = false;
                }
                txtrawmeterial.Visible = true;
                txtrawmeterial.Focus();
                txtrawmeterial.SelectionStart = txtrawmeterial.Text.Length;
                isProductPurchasePrice = false;
            }
            else
            {
                txtrawmeterial.Visible = false;
                if (panelSearchLookup.Visible == true)
                {
                    panelSearchLookup.Visible = false;
                }
            }
        }

        //public void FillProducts(bool isProductName, DataGridViewTextBoxEditingControl editControl)
        //{
        //    dtblProductsLookup = SPProduct.ProductViewAllWithOutBOM();
        //}
        private string CreateFilterQry(string strVal, string strSearchField)
        {
            string strqry = "";
            if (strSearchField == "Product")
            {
                for (int i = 0; i < dgvLookup.Columns.Count; ++i)
                {
                    if (dgvLookup.Columns[i].Visible == true)
                    {
                        if (dgvLookup.Columns[i].HeaderText != "SalesRate" && dgvLookup.Columns[i].HeaderText != "unitId")
                        {
                            if (strqry == "")
                            {
                                strqry = dgvLookup.Columns[i].HeaderText + " LIKE '%" + strVal + "%' ";
                            }
                            else
                            {
                                strqry = strqry + " OR " + dgvLookup.Columns[i].HeaderText + " LIKE '%" + strVal + "%' ";
                            }
                        }
                    }
                }
            }
            else if (strSearchField == "Ledger")
            {
                for (int i = 0; i < dgvLookup.Columns.Count; ++i)
                {
                    if (dgvLookup.Columns[i].Visible == true)
                    {
                        if (dgvLookup.Columns[i].HeaderText == "ledgerName")
                        {
                            if (strqry == "")
                            {
                                strqry = dgvLookup.Columns[i].HeaderText + " LIKE '%" + strVal + "%' ";
                            }
                            else
                            {
                                strqry = strqry + " OR " + dgvLookup.Columns[i].HeaderText + " LIKE '%" + strVal + "%' ";
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dgvLookup.Columns.Count; ++i)
                {
                    if (dgvLookup.Columns[i].Visible == true)
                    {
                        if (strqry == "")
                        {
                            strqry = dgvLookup.Columns[i].HeaderText + " LIKE '%" + strVal + "%' ";
                        }
                        else
                        {
                            strqry = strqry + " OR " + dgvLookup.Columns[i].HeaderText + " LIKE '%" + strVal + "%' ";
                        }
                    }
                }
            }

            return strqry;
        }
        private void fillLookupGrid(DataTable dt, string MasterId, int searchFiledCount, string strSearchValue, string strSearchField)
        {
            dgvLookup.Columns.Clear();
            if (dt.Rows.Count == 0)
            {
                dt.Dispose();
                return;
            }
            else
            {
                dgvLookup.DataSource = dt;
                string searchvalue = CreateFilterQry(strSearchValue, strSearchField);
                if (strSearchField == "SO")
                {

                }
                else
                {

                }

                if (dgvLookup.Columns.Contains("ledgerCode") == true)
                {
                    dgvLookup.Columns["ledgerCode"].FillWeight = 50;
                }
                if (dgvLookup.Columns.Contains("ledgerName") == true)
                {
                    dgvLookup.Columns["ledgerName"].FillWeight = 200;
                }

                DataView dv = dt.DefaultView;
                //dv.RowFilter = "LedgerName LIKE '%" + searchvalue + "%' OR ledgerCode LIKE '%" + searchvalue + "%'";
                dv.RowFilter = searchvalue;

                dgvLookup.DataSource = dv.ToTable();

                if (MasterId != "")
                {
                    if (dgvLookup.Columns.Contains(MasterId.ToString()) == true)
                    {
                        dgvLookup.Columns[MasterId].Visible = false;
                    }
                }

                if (dgvLookup.Columns.Contains("totalAmount") == true)
                {
                    dgvLookup.Columns["totalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }


                if (dgvLookup.Columns.Contains("unitId") == true)
                {
                    dgvLookup.Columns["unitId"].Visible = false;
                }

                if (dgvLookup.Columns.Contains("productCode") == true)
                {
                    dgvLookup.Columns["productCode"].FillWeight = 1;
                }
                if (dgvLookup.Columns.Contains("barcode") == true)
                {
                    dgvLookup.Columns["barcode"].FillWeight = 4;
                }
                if (dgvLookup.Columns.Contains("productName") == true)
                {
                    dgvLookup.Columns["productName"].FillWeight = 20;
                }
                if (dgvLookup.Columns.Contains("unitName") == true)
                {
                    dgvLookup.Columns["unitName"].FillWeight = 4;
                }
                if (dgvLookup.Columns.Contains("SalesRate") == true)
                {
                    dgvLookup.Columns["SalesRate"].FillWeight = 4;
                }
                if (dgvLookup.Columns.Contains("groupName") == true)
                {
                    dgvLookup.Columns["groupName"].FillWeight = 4;
                }
                if (dgvLookup.Columns.Contains("unitId") == true)
                {
                    dgvLookup.Columns["unitId"].FillWeight = 1;
                }

                //dgvLookup.Visible = true;
                //dgvRegister.ClearSelection();
                //dgvRegister.Focus();

                try
                {
                    //dgvLookup.Focus();
                    dgvLookup.Rows[0].Selected = true;
                }
                catch { }
            }
        }

        public void setGridAllFilter(DataTable dt, string strSearchValue, string strSearchField)
        {
            if (dt.Rows.Count > 0)
            {
                string searchvalue = CreateFilterQry(strSearchValue, strSearchField);

                DataView dv = dt.DefaultView;
                //dv.RowFilter = "LedgerName LIKE '%" + searchvalue + "%' OR ledgerCode LIKE '%" + searchvalue + "%'";
                dv.RowFilter = searchvalue;

                dgvLookup.DataSource = dv.ToTable();
            }
        }

        private void txtrawmeterial_TextChanged(object sender, EventArgs e)
        {
            if (isProductPurchasePrice)
            {
                if (txtrawmeterial.Text != "")
                {
                    strLookupType = "Ledger";
                    panelSearchLookup.Size = new Size(700, 260);
                    int LocX = txtrawmeterial.Location.X;
                    int LocY = txtrawmeterial.Location.Y;
                    panelSearchLookup.Location = new System.Drawing.Point(LocX, LocY + 23);

                    fillLookupGrid(dtblProductsLookup, "productCode", 3, txtrawmeterial.Text.ToString(), strLookupType);
                    panelSearchLookup.Visible = true;
                    panelSearchLookup.BringToFront();
                }
            }
            else
            {
                if (isProductNameTextFill == false)
                {
                    if (txtrawmeterial.Text != "")
                    {
                        if (panelSearchLookup.Visible == false)
                        {
                            strLookupType = "Product";
                            panelSearchLookup.Size = new Size(700, 260);
                            int LocX = txtrawmeterial.Location.X;
                            int LocY = txtrawmeterial.Location.Y;
                            panelSearchLookup.Location = new System.Drawing.Point(LocX, LocY + 23);

                            fillLookupGrid(dtblProductsLookup, "prdCode", 3, txtrawmeterial.Text.ToString(), strLookupType);
                            panelSearchLookup.Visible = true;
                            panelSearchLookup.BringToFront();
                        }
                        else if (panelSearchLookup.Visible == true && strLookupType == "Product")
                        {
                            setGridAllFilter(dtblProductsLookup, txtrawmeterial.Text.ToString(), strLookupType);
                        }
                    }
                    else
                    {
                        panelSearchLookup.Visible = false;
                        strLookupType = "";
                    }
                }
            }
        }
        public void setLookupGridFocus()
        {
            dgvLookup.Focus();
            try 
            { 
                dgvLookup.Rows[0].Selected = true; 
            }
            catch 
            {
 
            }
        }
        public void selectFirstRowinLookupGrid(string strSearchField)
        {
            if (dgvLookup.Rows.Count > 0)
            {
                if (strSearchField == "Ledger")
                {
                    string strMasterIdColumnValue = dgvLookup.Rows[0].Cells["ledgerId"].Value.ToString();
                    loadPartyDetailsfromLookup(strMasterIdColumnValue);
                }
                else if (strSearchField == "Product")
                {
                    FillrowAfterPickingProduct(dgvLookup.Rows[0].Cells["productCode"].Value.ToString(), DgvBom.CurrentCell.RowIndex, false, "", dgvLookup.Rows[0].Cells["unitId"].Value.ToString());
                }
            }
            else
            {

            }
        }
        public void FillrowAfterPickingProduct(string productCode, int index, bool isFromProductCreation, string searchedContent, string strUnitId)
        {
            if (panelSearchLookup.Visible == true)
            {
                panelSearchLookup.Visible = false;
            }
            isDontExecuteCellEnter = true;
            if (productCode != null && productCode != "")
            {
                if (index == DgvBom.Rows.Count - 1 && DgvBom.Rows[index].IsNewRow)
                {
                    isValueChange = false;
                    DgvBom.Rows.Add();
                    isValueChange = true;
                }
            }
            //if (isFromProductCreation)
            //{
            //    FillProducts(true, null);
            //}

            isExecuteEndEdit = false;
            //ProductInfo infoproduct = new ProductInfo();
            if (productCode != null && productCode != "")
            {
                strUnitIdfromProductPopup = strUnitId;
                DgvBom.Rows[index].Cells["BomrowMaterialCode"].Value = productCode;
            }
            if (DgvBom.Rows[index].Cells["BomrowMaterialCode"].Value != null && DgvBom.Rows[index].Cells["BomrowMaterialCode"].Value.ToString() != "")
            {
                DgvBom.CurrentCell = DgvBom.Rows[index].Cells["Bomqty"];
            }
            else
            {
                DgvBom.CurrentCell = DgvBom.Rows[index].Cells["BomproductName"];
            }
            if (!isFromProductCreation && DgvBom.Rows[index + 1].IsNewRow && MDIFinacPOS.clientName.ToLower() == "oktraders")
            {
                DgvBom.Rows[index + 1].Cells["BomrowMaterial"].Value = searchedContent;
            }
            DgvBom.Focus();

            DgvBom.CurrentCell.Selected = true;
            isDontExecuteCellEnter = false;
            isExecuteEndEdit = true;

            //blocked on 28/feb/2025 varis
            //dtblProductsLookup = new TransactionsGeneralFill().ProductFillforLookup(PublicVariables._branchId, "SalesInvoice");
        }
        private void txtrawmeterial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (panelSearchLookup.Visible == true && strLookupType == "Product")
                {
                    setLookupGridFocus();
                }
                else
                {
                    DgvBom.Focus();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                DgvBom.Focus();
                DgvBom.CurrentCell = DgvBom.Rows[DgvBom.CurrentRow.Index].Cells[2];
                DgvBom.BeginEdit(true);
            }
            else if (e.KeyCode == Keys.Right)
            {
                DgvBom.Focus();
                DgvBom.CurrentCell = DgvBom.Rows[DgvBom.CurrentRow.Index].Cells["Bomqty"];
                DgvBom.BeginEdit(true);
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                if (panelSearchLookup.Visible == false)
                {
                    strLookupType = "Product";
                    panelSearchLookup.Size = new Size(700, 260);
                    int LocX = txtrawmeterial.Location.X;
                    int LocY = txtrawmeterial.Location.Y;
                    panelSearchLookup.Location = new System.Drawing.Point(LocX, LocY + 23);
                    fillLookupGrid(dtblProductsLookup, "productCode", 3, "", strLookupType);
                    panelSearchLookup.Visible = true;
                    panelSearchLookup.BringToFront();
                }
                else
                {
                    panelSearchLookup.Visible = false;
                }

            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (panelSearchLookup.Visible == true && strLookupType == "Product")
                {
                    selectFirstRowinLookupGrid(strLookupType);
                }
                else
                {
                    DgvBom.Focus();
                    DgvBom.CurrentCell = DgvBom.Rows[DgvBom.CurrentRow.Index].Cells["Bomqty"];
                    DgvBom.BeginEdit(true);
                }
            }
        }

        private void txtrawmeterial_Leave(object sender, EventArgs e)
        {
            //txtrawmeterial.Visible = false;
        }

        private void txtrawmeterial_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                strLookupType = "";
                panelSearchLookup.Visible = false;
                DgvBom.Focus();
                DgvBom.CurrentCell = DgvBom.Rows[DgvBom.CurrentRow.Index].Cells["Bomqty"];

                e.IsInputKey = true;
                DgvBom.Focus();
                DgvBom.CurrentCell = DgvBom.Rows[DgvBom.CurrentRow.Index].Cells["Bomqty"];
                e.IsInputKey = true;
            }
        }

        private void dgvLookup_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            inLookupCurrenRowIndex = e.RowIndex;
        }

        private void dgvLookup_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            inLookupCurrenRowIndex = e.RowIndex;
        }

        private void dgvLookup_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {

            }
            else
            {
                inLookupCurrenRowIndex = e.RowIndex;
            }
        }

        private void dgvLookup_DoubleClick(object sender, EventArgs e)
        {
            if (dgvLookup.CurrentRow != null)
            {
                if (strLookupType == "Product")
                {
                    FillrowAfterPickingProduct(dgvLookup.Rows[inLookupCurrenRowIndex].Cells["productCode"].Value.ToString(), DgvBom.CurrentCell.RowIndex, false, "", dgvLookup.Rows[inLookupCurrenRowIndex].Cells["unitId"].Value.ToString());
                }
                else if (strLookupType == "Ledger")
                {
                    string strMasterIdColumnValue = dgvLookup.Rows[inLookupCurrenRowIndex].Cells["ledgerId"].Value.ToString();
                    FillCashOrPartyCombo();
                    loadPartyDetailsfromLookup(strMasterIdColumnValue);
                    
                }
            }
        }

        private void dgvLookup_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    Search gs = new Search();
            //    if (strLookupType == "Ledger")
            //    {
            //        gs.MultiFocus(e, txtLedgerName);
            //    }
            //    else if (strLookupType == "Product")
            //    {
            //        gs.MultiFocus(e, txtrawmeterial);
            //    }
            //}
        }

        private void dgvLookup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvLookup.Rows.Count > 0)
            {
                if (e.KeyChar == (char)13)
                {
                    inLookupCurrenRowIndex = dgvLookup.CurrentRow.Index;
                    if (strLookupType == "Ledger")
                    {
                        string strMasterIdColumnValue = dgvLookup.Rows[inLookupCurrenRowIndex].Cells["ledgerId"].Value.ToString();
                        loadPartyDetailsfromLookup(strMasterIdColumnValue);
                    }
                    else if (strLookupType == "Product")
                    {
                        FillrowAfterPickingProduct(dgvLookup.Rows[inLookupCurrenRowIndex].Cells["productCode"].Value.ToString(), DgvBom.CurrentCell.RowIndex, false, "", dgvLookup.Rows[inLookupCurrenRowIndex].Cells["unitId"].Value.ToString());
                    }
                }
            }
        }

        private void dgvProductPurchasePrice_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (dgvProductPurchasePrice.Columns[e.ColumnIndex].Name == "ppp_ledgerid")
            //{
            //    strDefaultId = txtProductCode.Text;
            //}


            try
            {
                //if (this.dgvProductPurchasePrice.Columns[e.ColumnIndex].Name == "productCode")
                //{
                //    strPrdToClear = dgvProductPurchasePrice.Rows[e.RowIndex].Cells["prdCodeToKeep"].Value == null ? "" : dgvProductPurchasePrice.Rows[e.RowIndex].Cells["prdCodeToKeep"].Value.ToString();
                //}
                //else if (this.dgvProductPurchasePrice.Columns[e.ColumnIndex].Name == "productName")
                //{
                //    strPrdToClear = dgvProductPurchasePrice.Rows[e.RowIndex].Cells["prdCodeToKeep"].Value == null ? "" : dgvProductPurchasePrice.Rows[e.RowIndex].Cells["prdCodeToKeep"].Value.ToString();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("PI55:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvProductPurchasePrice_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductPurchasePrice.CurrentCellAddress.X == 3)
            {
                int X = 0;
                int Y = 0;
                if (PublicVariables._ModuleLanguage == "ARB")
                {
                    X = dgvProductPurchasePrice.GetCellDisplayRectangle(dgvProductPurchasePrice.CurrentCell.ColumnIndex, dgvProductPurchasePrice.CurrentCell.RowIndex, true).Left + dgvProductPurchasePrice.Left - 805;
                    Y = dgvProductPurchasePrice.GetCellDisplayRectangle(dgvProductPurchasePrice.CurrentCell.ColumnIndex, dgvProductPurchasePrice.CurrentCell.RowIndex, true).Bottom + 143;
                }
                else
                {
                    X = dgvProductPurchasePrice.GetCellDisplayRectangle(dgvProductPurchasePrice.CurrentCell.ColumnIndex, dgvProductPurchasePrice.CurrentCell.RowIndex, true).Left + dgvProductPurchasePrice.Left;
                    Y = dgvProductPurchasePrice.GetCellDisplayRectangle(dgvProductPurchasePrice.CurrentCell.ColumnIndex, dgvProductPurchasePrice.CurrentCell.RowIndex, true).Bottom + 255;
                }

                txtLedgerName.Location = new Point(X, Y);
                txtLedgerName.Size = new Size(dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Width, txtLedgerName.Height);
                txtLedgerName.Text = "";
                if (dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentCell.RowIndex].Cells["ppp_lrdgername"].Value != null && dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentCell.RowIndex].Cells["ppp_lrdgername"].Value.ToString().Trim() != "")
                {
                    
                    txtLedgerName.Text = dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentCell.RowIndex].Cells["ppp_lrdgername"].Value.ToString();
                    
                }
                txtLedgerName.Visible = true;
                txtLedgerName.Focus();
                txtLedgerName.SelectionStart = txtrawmeterial.Text.Length;
                txtLedgerName.BringToFront();
                isProductPurchasePrice = true;
            }
            else
            {
                txtLedgerName.Visible = false;
                if (panelSearchLookup.Visible == true)
                {
                    panelSearchLookup.Visible = false;
                }
            }
        }

        public void calcDiscAmt(int i)
        {
            decimal decpppamount = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_amount"].Value.ToString());
            if (dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value != null)
            {
                if (dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value.ToString() != "")
                {
                    //ClaclDiscAmount
                    decimal decpppdiscper = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value.ToString());
                    decimal decpppdiscamount = (decpppamount * (decpppdiscper / 100));
                    dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value = decpppdiscamount.ToString();
                }
            }
            else
            {
                dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value = "0";
                dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value = "0";
            }
        }

        public void calcDiscPer(int i)
        {
            decimal decpppamount = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_amount"].Value.ToString());
            if (dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value != null)
            {
                if (dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value.ToString() != "")
                {
                    //ClaclDiscPer
                    decimal decpppdiscamount = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value.ToString());
                    decimal decpppdiscper = (decpppdiscamount * 100) / decpppamount;
                    dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value = decpppdiscper.ToString();
                }
            }
            else
            {
                dgvProductPurchasePrice.Rows[i].Cells["ppp_discper"].Value = "0";
                dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value = "0";

            }
        }
        public void CalcPurchasePrice(int i)
        {
            decimal decpppamount = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_amount"].Value.ToString());
            decimal decpppdiscamount = Convert.ToDecimal(dgvProductPurchasePrice.Rows[i].Cells["ppp_discamt"].Value.ToString());
            decimal decppppurchaseprice = decpppamount - decpppdiscamount;
            dgvProductPurchasePrice.Rows[i].Cells["ppp_purchaseprice"].Value = decppppurchaseprice.ToString();
        }


        bool isDontEceutepurchase; bool isPurchaseValueChange = true;

        private void dgvProductPurchasePrice_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1 && e.ColumnIndex != -1)
            //{
            //    if (dgvProductPurchasePrice.Columns[e.ColumnIndex].Name == "ppp_amount")
            //    {
            //        calcDiscAmt(e.RowIndex);
            //        CalcPurchasePrice(e.RowIndex);
            //    }
            //    else if (dgvProductPurchasePrice.Columns[e.ColumnIndex].Name == "ppp_discper")
            //    {
            //        calcDiscAmt(e.RowIndex);
            //        CalcPurchasePrice(e.RowIndex);
            //    }
            //    else if (dgvProductPurchasePrice.Columns[e.ColumnIndex].Name == "ppp_discamt")
            //    {
            //        calcDiscPer(e.RowIndex);
            //        CalcPurchasePrice(e.RowIndex);
            //    }
            //}






            if (e.RowIndex != -1 && e.ColumnIndex != -1 && isPurchaseValueChange)
            {
                if (dgvProductPurchasePrice.Rows[e.RowIndex].Cells[e.ColumnIndex].OwningColumn.Name == "ppp_date")
                {
                    TextBox txt = new TextBox();
                    if (dgvProductPurchasePrice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                    {
                        txt.Text = "";
                    }
                    else
                    {
                        txt.Text = dgvProductPurchasePrice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                    //OtherDateValidationFunction objDate = new OtherDateValidationFunction();
                    //objDate.DateValidationFunction(txt, false);
                    //dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_date"].Value = txt.Text;
                }






                decimal dQty = 0;
                bool isOk = true;

                if (dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_amount"].Value != null)
                {
                    decimal.TryParse(dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_amount"].Value.ToString(), out dQty);
                }

                if (dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_ledgerid"].Value == null || dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_ledgerid"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }
                if (dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_unit"].Value == null || dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_unit"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }
                if (dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_amount"].Value == null || dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_amount"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }
                if (dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_purchaseprice"].Value == null || dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_purchaseprice"].Value.ToString().Trim() == "")
                {
                    isOk = false;
                }

                if (isOk)
                {
                    dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_ucheck"].Value = "";
                }
                else
                {
                    dgvProductPurchasePrice.Rows[e.RowIndex].Cells["ppp_ucheck"].Value = "x";
                    dgvProductPurchasePrice["ppp_ucheck", e.RowIndex].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                string purchaseUnitId = "", unitUnitId = ""; bool isasOk = true;

                if (dgvProductPurchasePrice.Rows.Count > 0)
                {
                    isPurchaseValueChange = false;
                    if (!dgvProductPurchasePrice.CurrentRow.IsNewRow)
                    {
                        if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_unit"].Value !=  null)
                        {
                            purchaseUnitId = dgvProductPurchasePrice.CurrentRow.Cells["ppp_unit"].Value.ToString();
                            if (dgvUnits.Rows.Count > 1)
                            {
                                foreach (DataGridViewRow dgvRowObj in dgvUnits.Rows)
                                {
                                    if (!dgvRowObj.IsNewRow)
                                    {
                                        if (dgvRowObj.Cells["unit"].Value.ToString() != null)
                                        {
                                            unitUnitId = dgvRowObj.Cells["unit"].Value.ToString();
                                            if (unitUnitId == purchaseUnitId)
                                            {
                                                isasOk = true;
                                                break;
                                            }
                                            else
                                            {
                                                isasOk = false;
                                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_ucheck"].Value = "x";
                                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_ucheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                isasOk = false;
                            }
                        }




                        if (isasOk == false)
                        {
                            MessageBox.Show("Please select assigned units", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (isasOk && isOk)
                        {
                            dgvProductPurchasePrice.CurrentRow.Cells["ppp_ucheck"].Value = "";
                        }
                    }
                    isPurchaseValueChange = true;
                }
                if (dgvProductPurchasePrice.Rows.Count > 1)
                {
                    isPurchaseValueChange = false; bool strOk = true;
                    string str1 = "", str2 = ""; int count = 0;
                    string strlederid1 = "", strledgerid2="";
                    for (int i = 0; i < dgvProductPurchasePrice.Rows.Count; i++)
                    {
                        for (int j = 1; j < dgvProductPurchasePrice.Rows.Count; j++)
                        {
                            if (dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].RowIndex != dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].RowIndex)
                            {
                                if (!dgvProductPurchasePrice.Rows[j].IsNewRow && !dgvProductPurchasePrice.Rows[i].IsNewRow)
                                {
                                    str1 = dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].Value.ToString();
                                    str2 = dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].Value.ToString();
                                    strlederid1 = dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value.ToString();
                                    strledgerid2 = dgvProductPurchasePrice.Rows[j].Cells["ppp_ledgerid"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[j].Cells["ppp_ledgerid"].Value.ToString();
                                   
                                    if (str1 == str2 && strlederid1 == strledgerid2)
                                    {
                                        count = count + 1;
                                    }
                                    if (str1 == str2 && strlederid1 == strledgerid2)
                                    {
                                        strOk = false;
                                        count = count + 1;
                                        dgvProductPurchasePrice.Rows[j].Cells["ppp_ucheck"].Value = "x";
                                        dgvProductPurchasePrice.Rows[j].Cells["ppp_ucheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    }
                                    if (isasOk && isOk && strOk)
                                    {
                                        dgvProductPurchasePrice.CurrentRow.Cells["ppp_ucheck"].Value = "";
                                        dgvProductPurchasePrice.Rows[j].Cells["ppp_ucheck"].Value = "";
                                    }
                                }
                            }
                        }
                    }
                    if (count > 0)
                    {
                        MessageBox.Show("Please select different unit", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                isPurchaseValueChange = true; 
            }
        }

        private void txtLedgerName_TextChanged(object sender, EventArgs e)
        {
            if (txtLedgerName.Text.Trim().Length > 0)
            {
                if (panelSearchLookup.Visible == false)
                {
                    strLookupType = "Ledger";
                    panelSearchLookup.Size = new Size(700, 260);
                    int LocX = txtLedgerName.Location.X;
                    int LocY = txtLedgerName.Location.Y;
                    panelSearchLookup.Location = new System.Drawing.Point(LocX, LocY + 23);

                    fillLookupGrid(dtblPartylookup, "ledgerId", 3, txtLedgerName.Text.Trim(), strLookupType);
                    panelSearchLookup.Visible = true;
                    panelSearchLookup.BringToFront();
                }
                else if (panelSearchLookup.Visible == true && strLookupType == "Ledger")
                {
                    setGridAllFilter(dtblPartylookup, txtLedgerName.Text.Trim(), strLookupType);
                }
            }
            else
            {
                strLookupType = "";
                panelSearchLookup.Visible = false;
            }
        }

        public void FillCashOrPartyCombo()
        {
            try
            {
                isDontExecuteCashValueChange = true;
                DataTable dtblCashOrParty = SpAccountLedger.AccountLedgerGetVendorForTransaction();
                dtblPartylookup = dtblCashOrParty;
                isDontExecuteCashValueChange = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("PI14:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void loadPartyDetailsfromLookup(string strledger)
        {
            if (strledger != "")
            {
                if (!isDontExecuteCashValueChange)
                {
                    AccountLedgerInfo InfoAccountLedger = new AccountLedgerInfo();

                    InfoAccountLedger = SpAccountLedger.AccountLedgerView(strledger);
                    if (InfoAccountLedger.LedgerId != null)
                    {
                        int i = dgvProductPurchasePrice.CurrentRow.Index;
                        dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value = InfoAccountLedger.LedgerId;
                        dgvProductPurchasePrice.Rows[i].Cells["ppp_lrdgername"].Value = InfoAccountLedger.LedgerName;

                        //objTransactionsGeneralFill.FillUnitPerQtyComboForGridProductPurchasePrice(dgvProductPurchasePrice, strProductIdToEdit, i);

                        panelSearchLookup.Visible = false;
                        dgvProductPurchasePrice.Focus();
                        dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.Rows[i].Cells["ppp_date"];

                        dgvProductPurchasePrice.CurrentCell.Selected = true;
                    }
                }
            }
            //panelSearchLookup.Visible = false;
            //dgvProductPurchasePrice.Focus();
            //dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.Rows[0].Cells["ppp_unit"];
        }

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Down)
            //{
            //    //ctrlLookup.setGridFocus();
            //    if (panelSearchLookup.Visible == true && strLookupType == "Ledger")
            //    {
            //        setLookupGridFocus();
            //    }
            //}
            //else if (e.Control && e.KeyCode == Keys.F)
            //{
            //    if (panelSearchLookup.Visible == false)
            //    {
            //        strLookupType = "Ledger";
            //        panelSearchLookup.Size = new Size(700, 260);
            //        int LocX = txtLedgerName.Location.X;
            //        int LocY = txtLedgerName.Location.Y;
            //        panelSearchLookup.Location = new System.Drawing.Point(LocX, LocY + 23);
            //        fillLookupGrid(dtblPartylookup, "ledgerId", 3, "", strLookupType);
            //        panelSearchLookup.Visible = true;
            //    }
            //    else
            //    {
            //        panelSearchLookup.Visible = false;
            //    }
            //}
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    if (panelSearchLookup.Visible == true && strLookupType == "Ledger")
            //    {
            //        selectFirstRowinLookupGrid(strLookupType);
            //    }
            //    else
            //    {
                   
            //    }
            //}
            //if (e.Alt && e.KeyCode == Keys.C)
            //{
            //    SendKeys.Send("{F10}");
            //}







            if (e.KeyCode == Keys.Down)
            {
                if (panelSearchLookup.Visible == true && strLookupType == "Ledger")
                {
                    setLookupGridFocus();

                    
                }
                else
                {
                    dgvProductPurchasePrice.Focus();
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                dgvProductPurchasePrice.Focus();
                dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].Cells["ppp_date"];
                dgvProductPurchasePrice.BeginEdit(true);

            }
            else if (e.KeyCode == Keys.Right)
            {

                dgvProductPurchasePrice.Focus();

                dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].Cells["ppp_date"];

                dgvProductPurchasePrice.BeginEdit(true);
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                if (panelSearchLookup.Visible == false)
                {
                    strLookupType = "Ledger";
                    panelSearchLookup.Size = new Size(700, 260);
                    int LocX = txtProductName.Location.X;
                    int LocY = txtProductName.Location.Y;
                    panelSearchLookup.Location = new System.Drawing.Point(LocX, LocY + 23);
                    fillLookupGrid(dtblPartylookup, "ledgerId", 3, "", strLookupType);
                    panelSearchLookup.Visible = true;
                }
                else
                {
                    panelSearchLookup.Visible = false;
                }

            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (panelSearchLookup.Visible == true && strLookupType == "Ledger")
                {
                    selectFirstRowinLookupGrid(strLookupType);

                    int a = dgvProductPurchasePrice.CurrentRow.Index;
                    int b = dgvProductPurchasePrice.CurrentCell.ColumnIndex;

                    dgvProductPurchasePrice.Focus();
                    dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.CurrentRow.Cells["ppp_date"];

                    dgvProductPurchasePrice.CurrentCell.Selected = true;
                }
                else
                {
                    if (txtLedgerName.Text == "")
                    {
                        dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentCell.RowIndex].Cells["ppp_ledgerid"].Value = null;
                        dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentCell.RowIndex].Cells["ppp_lrdgername"].Value = null;
                    }

                    dgvProductPurchasePrice.Focus();

                    dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].Cells["ppp_date"];
                    //e.IsInputKey = true;
                    dgvProductPurchasePrice.BeginEdit(true);
                }
            }
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            txtLedgerName.Visible = false;
        }

        private void dgvProductPurchasePrice_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal ppp_DiscPercentage = 0, ppp_DiscAmount = 0, ppp_Amount = 0, ppp_PurchasePrice = 0;
                if (dgvProductPurchasePrice.CurrentCell != null)
                {
                    if (dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_date")
                    {
                        //dgvProductPurchasePrice.Focus();
                        //dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.CurrentRow.Cells["ppp_unit"];
                    }
                    else if (dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_unit")
                    {
                        //dgvProductPurchasePrice.Focus();
                        //dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"];
                    }
                    else if (dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_amount")
                    {
                        ppp_Amount = 0;
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value.ToString() != "")
                            {
                                ppp_Amount = Convert.ToDecimal(dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value);
                            }
                        }
                        catch
                        {
                            ppp_Amount = 0;
                            dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value = 0;
                        }
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value.ToString() != "" || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value.ToString() != "0")
                            {
                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value = 0;
                            }
                        }
                        catch
                        {
                            dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value = 0;
                        }
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value != "" || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value.ToString() != "0")
                            {
                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value = 0;
                            }
                        }
                        catch
                        {
                            dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value = 0;
                        }

                        dgvProductPurchasePrice.CurrentRow.Cells["ppp_purchaseprice"].Value = ppp_Amount;

                        //dgvProductPurchasePrice.Focus();
                        //dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"];
                    }
                    else if (dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_discper")
                    {
                        ppp_Amount = 0;
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value.ToString() != "")
                                ppp_Amount = Convert.ToDecimal(dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value);
                        }
                        catch { ppp_Amount = 0; }
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value.ToString() != "" || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value.ToString() != "0")
                            {
                                ppp_Amount = Convert.ToDecimal(dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value);
                                ppp_DiscPercentage = Convert.ToDecimal(dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value);
                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value = (ppp_Amount * ppp_DiscPercentage) / 100;
                                ppp_Amount = ppp_Amount - ((ppp_Amount * ppp_DiscPercentage) / 100);
                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_purchaseprice"].Value = ppp_Amount;
                            }
                        }
                        catch
                        {
                            dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value = 0;
                        }
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value.ToString() != "" || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value.ToString() != "0")
                            {
                                
                            }
                        }
                        catch
                        {
                            dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value = 0;
                        }

                        dgvProductPurchasePrice.CurrentRow.Cells["ppp_purchaseprice"].Value = ppp_Amount;

                        //dgvProductPurchasePrice.Focus();
                        //dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"];
                    }
                    else if (dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_discamt")
                    {
                        ppp_Amount = 0;
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value.ToString() != "")
                                ppp_Amount = Convert.ToDecimal(dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value);
                        }
                        catch
                        {
                            ppp_Amount = 0;
                        }
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value.ToString() != "" || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value.ToString() != "0")
                            {
                            }
                        }
                        catch
                        {
                            dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value = 0;
                        }
                        try
                        {
                            if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value != null || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value.ToString() != "" || dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value.ToString() != "0")
                            {
                                ppp_Amount = Convert.ToDecimal(dgvProductPurchasePrice.CurrentRow.Cells["ppp_amount"].Value);
                                decimal postdiscountprice = 0;
                                ppp_DiscAmount = Convert.ToDecimal(dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value);
                                postdiscountprice = ppp_Amount - ppp_DiscAmount;
                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_discper"].Value = Math.Round(((ppp_Amount - postdiscountprice) / ppp_Amount) * 100, 2);
                                ppp_Amount = ppp_Amount - ppp_DiscAmount;
                                dgvProductPurchasePrice.CurrentRow.Cells["ppp_purchaseprice"].Value = ppp_Amount;
                            }
                        }
                        catch { dgvProductPurchasePrice.CurrentRow.Cells["ppp_discamt"].Value = 0; }

                        dgvProductPurchasePrice.CurrentRow.Cells["ppp_purchaseprice"].Value = ppp_Amount;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvProductPurchasePrice_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProductPurchasePrice_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
            }
            catch { }
        }

        DataGridViewTextBoxEditingControl TextBoxControlPurchasePrice;

        private void dgvProductPurchasePrice_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBoxControlPurchasePrice = e.Control as DataGridViewTextBoxEditingControl;
                if (TextBoxControlPurchasePrice != null)
                {
                    TextBoxControlPurchasePrice.KeyPress += TextBoxCellEditControlKeyPressPurchasePrice;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU4:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TextBoxCellEditControlKeyPressPurchasePrice(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dgvProductPurchasePrice.CurrentCell != null)
                {
                    if (dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_amount" || dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_discper" || dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_discamt" || dgvProductPurchasePrice.Columns[dgvProductPurchasePrice.CurrentCell.ColumnIndex].Name == "ppp_purchaseprice")
                    {
                        ComboValidation objComboValidation = new ComboValidation();
                        objComboValidation.DecimalValidationGRid(sender, e, TextBoxControlPurchasePrice, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lnklblPurchasePrice_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvProductPurchasePrice.SelectedCells.Count > 0 && dgvProductPurchasePrice.CurrentRow != null)
                {
                    if (dgvProductPurchasePrice.CurrentCell != dgvProductPurchasePrice.CurrentRow.Cells["ppp_unit"])
                    {
                        if (!dgvProductPurchasePrice.CurrentRow.IsNewRow)
                        {
                            if (dgvProductPurchasePrice.RowCount > 1)
                            {

                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        RemoveRowPurchasePriceGRid();
                                        UntickCheckMarkPurchase();
                                    }
                                    else
                                    {
                                        dgvProductPurchasePrice.Focus();
                                    }
                                }

                                else
                                {
                                    RemoveRowPurchasePriceGRid();
                                    UntickCheckMarkPurchase();
                                }
                            }
                            else
                            {

                                if (InventorySettingsInfo._messageBoxRowRemove)
                                {
                                    if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                    {
                                        dgvProductPurchasePrice.Rows.Clear();
                                    }
                                }
                                else
                                {
                                    dgvProductPurchasePrice.Rows.Clear(); 
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("MU2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void UntickCheckMarkPurchase()
        {
            if (dgvProductPurchasePrice.Rows.Count > 1)
            {
                isPurchaseValueChange = false; bool strOk = true;
                string str1 = "", str2 = ""; int count = 0;
                string strlederid1 = "", strledgerid2 = "";
                for (int i = 0; i < dgvProductPurchasePrice.Rows.Count; i++)
                {
                    for (int j = 1; j < dgvProductPurchasePrice.Rows.Count; j++)
                    {
                        if (dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].RowIndex != dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].RowIndex)
                        {
                            if (!dgvProductPurchasePrice.Rows[j].IsNewRow && !dgvProductPurchasePrice.Rows[i].IsNewRow)
                            {
                                str1 = dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[i].Cells["ppp_unit"].Value.ToString();
                                str2 = dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[j].Cells["ppp_unit"].Value.ToString();
                                strlederid1 = dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[i].Cells["ppp_ledgerid"].Value.ToString();
                                strledgerid2 = dgvProductPurchasePrice.Rows[j].Cells["ppp_ledgerid"].Value == null ? string.Empty : dgvProductPurchasePrice.Rows[j].Cells["ppp_ledgerid"].Value.ToString();

                                if (str1 == str2 && strlederid1 == strledgerid2)
                                {
                                    count = count + 1;
                                }
                                if (str1 == str2 && strlederid1 == strledgerid2)
                                {
                                    strOk = false;
                                    count = count + 1;
                                    //dgvProductPurchasePrice.Rows[j].Cells["ppp_ucheck"].Value = "x";
                                    //dgvProductPurchasePrice.Rows[j].Cells["ppp_ucheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                if ( strOk)
                                {
                                    dgvProductPurchasePrice.CurrentRow.Cells["ppp_ucheck"].Value = "";
                                    dgvProductPurchasePrice.Rows[j].Cells["ppp_ucheck"].Value = "";
                                }
                            }
                        }
                    }
                }
                isPurchaseValueChange = true;
              
            }
        }
        public void UntickCheckMarkSales()
        {
            if (dgvSalesPrice.Rows.Count > 2)
            {
                isSalesValueChange = false; bool strOk = true;
                string str1 = "", str2 = ""; int count = 0;
                for (int i = 0; i < dgvSalesPrice.Rows.Count; i++)
                {
                    for (int j = 1; j < dgvSalesPrice.Rows.Count; j++)
                    {
                        if (dgvSalesPrice.Rows[i].Cells["UnitId"].RowIndex != dgvSalesPrice.Rows[j].Cells["UnitId"].RowIndex && dgvSalesPrice.Rows[i].Cells["pricingLevelId"].RowIndex != dgvSalesPrice.Rows[i].Cells["pricingLevelId"].RowIndex)
                        {
                            if (!dgvSalesPrice.Rows[j].IsNewRow && !dgvSalesPrice.Rows[i].IsNewRow)
                            {
                                str1 = dgvSalesPrice.Rows[i].Cells["UnitId"].Value == null ? string.Empty : dgvSalesPrice.Rows[i].Cells["UnitId"].Value.ToString();
                                str2 = dgvSalesPrice.Rows[j].Cells["UnitId"].Value == null ? string.Empty : dgvSalesPrice.Rows[j].Cells["UnitId"].Value.ToString();

                                if (str1 == str2)
                                {
                                    strOk = false;
                                    count = count + 1;
                                    //dgvSalesPrice.Rows[j].Cells["scheck"].Value = "x";
                                    //dgvSalesPrice.Rows[j].Cells["scheck"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                if (strOk)
                                {
                                    dgvSalesPrice.CurrentRow.Cells["scheck"].Value = "";
                                    dgvSalesPrice.Rows[j].Cells["scheck"].Value = "";
                                }
                            }
                        }
                    }
                }

                isSalesValueChange = true;
            }
        }
        public string strDeleteIds = "";
        public void RemoveRowPurchasePriceGRid()///created by sheena on 13-05-2023
        {
            int inColIndex = dgvProductPurchasePrice.CurrentCell.ColumnIndex;
            bool isContinue = true;
            if (!dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].IsNewRow)
            {
                if (isContinue)
                {
                    if (dgvProductPurchasePrice.CurrentRow.Cells["ppp_purchasepriceid"].Value != null)
                        strDeleteIds = strDeleteIds + dgvProductPurchasePrice.CurrentRow.Cells["ppp_purchasepriceid"].Value.ToString() + ",";             
                    dgvProductPurchasePrice.Rows.RemoveAt(dgvProductPurchasePrice.CurrentRow.Index);
                    dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.CurrentRow.Cells[inColIndex];
                    
                    dgvProductPurchasePrice.Focus();
                }
            }
            else
            {
                dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].Cells["qty"].Value = "";
                dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].Cells["unit"].Value = null;
                dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.CurrentRow.Cells[inColIndex];
                dgvProductPurchasePrice.Focus();
            }
        }

        private void txtLedgerName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {

                strLookupType = "";
                panelSearchLookup.Visible = false;
                dgvProductPurchasePrice.Focus();

                dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].Cells["ppp_date"];
                
                e.IsInputKey = true;
                dgvProductPurchasePrice.Focus();

                dgvProductPurchasePrice.CurrentCell = dgvProductPurchasePrice.Rows[dgvProductPurchasePrice.CurrentRow.Index].Cells["ppp_date"];
                
                e.IsInputKey = true;
            }

        }

        private void dgvProductPurchasePrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PI56:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvLookup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProductPurchasePrice_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                int a = dgvProductPurchasePrice.CurrentRow.Index;
                int b = dgvProductPurchasePrice.CurrentCell.ColumnIndex;
            }
        }

        private void cmbGroupCat3_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void chkNutritionFact_CheckedChanged(object sender, EventArgs e)
        {
            grpNutritionFact.Visible = chkNutritionFact.Checked;
        }

        private void lnkNutrRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (dgvNutritionFacts.SelectedCells.Count > 0 && dgvNutritionFacts.CurrentRow != null)
                {


                    if (!dgvNutritionFacts.CurrentRow.IsNewRow)
                    {
                        if (dgvNutritionFacts.RowCount > 1)
                        {
                            if (InventorySettingsInfo._messageBoxRowRemove)
                            {
                                if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    RemoveRowNutrition();
                                }
                                else
                                {
                                    dgvNutritionFacts.Focus();
                                }
                            }

                            else
                            {
                                RemoveRowNutrition();
                            }
                        }
                        else
                        {

                            if (InventorySettingsInfo._messageBoxRowRemove)
                            {
                                if (MessageBox.Show("Do you want to remove current row?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                {
                                    dgvNutritionFacts.Rows.Clear();
                                }
                            }
                            else
                            {
                                dgvNutritionFacts.Rows.Clear();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BM3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PRC98:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void cmbGroupCat1_Enter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
        }

        private void cmbGroupCat1_Leave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
        }

        private void cmbGroupCat2_Enter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Red;
        }

        private void cmbGroupCat2_Leave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Black;
        }

        private void cmbGroupCat3_Enter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Red;
        }

        private void txtIngredients_Enter(object sender, EventArgs e)
        {
            lblIngredients.ForeColor = Color.Red;
        }

        private void txtIngredients_Leave(object sender, EventArgs e)
        {
            lblIngredients.ForeColor = Color.Black;
        }

        private void txtLocation_Enter(object sender, EventArgs e)
        {
            lblLocation.ForeColor = Color.Red;
        }

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            lblLocation.ForeColor = Color.Black;
        }

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtAlternateNo_Enter(object sender, EventArgs e)
        {
            lblAlternateNo.ForeColor = Color.Red;
        }

        private void txtAlternateNo_Leave(object sender, EventArgs e)
        {
            lblAlternateNo.ForeColor = Color.Black;
        }

        private object oldValue;
        private void dgvSalesPrice_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldValue = dgvSalesPrice.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

        private void cmbProductMainGrp_SelectedValueChanged(object sender, EventArgs e)
        {
            generateNewProductCode();
        }

        private void dgvSalesPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // prevent default ding

                var dgv = sender as DataGridView;
                if (dgv == null) return;

                int col = dgv.CurrentCell.ColumnIndex;
                int row = dgv.CurrentCell.RowIndex;

                bool isLastCol = (col == dgv.ColumnCount - 1);
                bool isLastRow = (row == dgv.RowCount - 1);

                if (isLastCol && isLastRow)
                {
                    // last cell → jump to Save button
                    btnSave.Focus();
                }
                else
                {
                    // move to next cell
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void dgvSalesPrice_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvSalesPrice.IsCurrentCellDirty)
            {
                dgvSalesPrice.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void TCProduct_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == TPBom||e.TabPage== tabPage1)
            {
                // Cancel selection for this tab
                e.Cancel = true;
            }
        }

        private void txtSalesPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtSalesPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtSalesPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                lblSalesprice.ForeColor = Color.Black;

                decimal.Parse(txtSalesPrice.Text);
            }
            catch
            {
                txtSalesPrice.Text = "0";
            }
        }

        private void txtSalesPrice_Enter(object sender, EventArgs e)
        {
            try
            {
                lblSalesprice.ForeColor = Color.Red;

                if (decimal.Parse(txtSalesPrice.Text) == 0m)
                {
                    txtSalesPrice.Text = "";
                }
            }
            catch
            {
                txtSalesPrice.Text = "";
            }
        }

        private void txtAlternateNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbGroupCat2_SelectedValueChanged(object sender, EventArgs e)
        {
           // FillGroupCategory3();
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {

        }

        private void TPProduct_Click(object sender, EventArgs e)
        {

        }

        private void txtExpiryDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

        private void txtExpiryDays_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(txtExpiryDays.Text);
            }
            catch
            {
                txtExpiryDays.Text = "0";
            }
            lblExpiryDays.ForeColor = Color.Black;
        }

        private void txtExpiryDays_Enter(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(txtExpiryDays.Text) == 0)
                {
                    txtExpiryDays.Text = "";
                }
            }
            catch
            {
                txtExpiryDays.Text = "";
            }
            lblExpiryDays.ForeColor = Color.Red;
        }

        private void chkShowExpiry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowExpiry.Checked)
            {
                lblExpiryDays.Visible = true;
                txtExpiryDays.Visible = true;
            }
            else
            {
                lblExpiryDays.Visible = false;
                txtExpiryDays.Visible = false;
            }
        }

        private void dgvSalesPrice_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            if (strDefault != "")
            {
                isDontEceute = true;

                dgvSalesPrice.Rows[e.RowIndex].Cells["costPrice"].Value = "0";
                dgvSalesPrice.Rows[e.RowIndex].Cells["marginPercentage"].Value = "0";
                dgvSalesPrice.Rows[e.RowIndex].Cells["LowestSellingPrice"].Value = "0";
                isDontEceute = false;
            }
            //objTransactionsGeneralFill.FillPricingLevelGridCombo(pricingLevelId);
            //if (!isEditFill)
            //{
            //    if (dgvSalesPrice.RowCount > 1)
            //    {
            //        dgvSalesPrice.Rows[e.RowIndex].Cells["pricingLevelId"].Value = "1";//Default pricinglevel

            //    }
            //}

        }
        bool isFromCostPricePer = false;
        private void txtPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            decimal decPurchaseRate = 0, decPerc = 0, decSales;
            if (e.KeyCode == Keys.Enter)
            {
                if (InventorySettingsInfo._SalePriceUpdateByCostPerc)
                {
                    if (MessageBox.Show("Do you want to update this in salesprice?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // Purchase Rate
                        if (!decimal.TryParse(txtPurchaseRate.Text, out decPurchaseRate))
                            decPurchaseRate = 0;

                        // Percentage
                        if (!decimal.TryParse(txtPercentage.Text, out decPerc))
                            decPerc = 0;

                        decPerc = 100 - decPerc;

                        decimal decimalValue = decPerc / 100;
                        if (decimalValue > 0)
                        {
                            // Perform the division
                            decimal result = decPurchaseRate / decimalValue;

                            isFromCostPricePer = true;

                            if (dgvUnits.RowCount > 0)
                            {
                                for (int i = 0; i < dgvUnits.Rows.Count; i++)
                                {
                                    // string unit = dgvUnits.Rows[i].Cells["unit"].Value.ToString();
                                    for (int iSales = 0; iSales < dgvSalesPrice.Rows.Count; iSales++)
                                    {

                                        if (!dgvUnits.Rows[i].IsNewRow)
                                        {
                                            if (!dgvSalesPrice.Rows[iSales].IsNewRow)
                                            {
                                                if (dgvUnits.Rows[i].Cells["unit"].Value != null)
                                                {
                                                    if (dgvUnits.Rows[i].Cells["unit"].Value.ToString() == dgvSalesPrice.Rows[iSales].Cells["UnitId"].Value.ToString())
                                                    {
                                                        dgvSalesPrice.Rows[iSales].Cells["PriceAmount"].Value = result;
                                                        dgvSalesPrice.Rows[iSales].Cells["SalesPrice"].Value = result;
                                                        //dgvSalesPrice.Rows[iSales].Cells["PriceAmount"].Value = decPurchaseRate * decimal.Parse(dgvUnits.Rows[i].Cells["defaultQty"].Value.ToString()) + (decPurchaseRate * decPerc) / 100;
                                                        //dgvSalesPrice.Rows[iSales].Cells["SalesPrice"].Value =  decPurchaseRate * decimal.Parse(dgvUnits.Rows[i].Cells["defaultQty"].Value.ToString()) + (decPurchaseRate * decPerc) / 100;
                                                        dgvSalesPrice.Rows[iSales].Cells["DiscPercentage"].Value = "0";
                                                        dgvSalesPrice.Rows[iSales].Cells["DiscAmount"].Value = "0";
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        isFromCostPricePer = false;
                    }
                    else
                    {
                        e.Handled = true;
                        this.SelectNextControl((Control)sender, true, true, true, true);
                    }
                }
                else
                {
                    e.Handled = true;
                    this.SelectNextControl((Control)sender, true, true, true, true);
                }

            }
        }

        private void txtPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                lblPurchaserate.ForeColor = Color.Black;
                decimal.Parse(txtPercentage.Text);
            }
            catch
            {
                txtPercentage.Text = "0";
            }
        }

        private void txtPercentage_Enter(object sender, EventArgs e)
        {
            try
            {
                lblPurchaserate.ForeColor = Color.Red;
                if (decimal.Parse(txtPercentage.Text) == 0)
                {
                    txtPercentage.Text = "";
                }
            }
            catch
            {
                txtPercentage.Text = "";
            }
        }

        private void txtPercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
        }

      

    }
}