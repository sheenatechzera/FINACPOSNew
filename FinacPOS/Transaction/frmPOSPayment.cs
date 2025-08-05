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
    public partial class frmPOSPayment : Form
    {
        public frmPOSPayment()
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
        bool formCancel = false;
        frmPOSSales frmPOSSales;
        frmPOSSales2 frmPOSSales2;
        string strFocusedControl = "";
        string strFromPaymentMethod = "";
        TextBox txtTouchTextBox = new TextBox();

        POSSalesMasterSP POSSalesMasterSP = new POSSalesMasterSP();
        ComboValidation objComboValidation = new ComboValidation();

        public void DoWhenComingFromPOSSalesForm(frmPOSSales frm,string strtotalAmount,string strPaymentMethod)
        {
            base.Show();

            btnCreditCard.Enabled = true;
            btnUPI.Enabled = true;
            btnCash.Enabled = true;
            btnCreditNote.Enabled = true;


            txtTotalAmount.Text = strtotalAmount;

            txtTenderedAmount.Text = Math.Round(0m,FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtBalance.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

            txtCreditCard.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtUPI.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtCredit.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtCash.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtCreditNoteAmount.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

            strFromPaymentMethod = strPaymentMethod;
            if (strPaymentMethod == "Cash")
            {
                txtCash.Text = strtotalAmount;
                txtCash.Focus();
                txtCash.SelectAll();
                txtCredit.Enabled = false;
                btnCredit.Enabled = false;
                txtTouchTextBox = txtCash;
                txtKeyBoardText.Clear();
            }
            else if (strPaymentMethod == "CreditCard")
            {
                txtCreditCard.Text = strtotalAmount;
                txtCreditCard.Focus();
                txtCreditCard.SelectAll();
                txtCredit.Enabled = false;
                btnCredit.Enabled = false;
                txtTouchTextBox = txtCreditCard;
                txtKeyBoardText.Clear();
            }
            else if (strPaymentMethod == "UPI")
            {
                txtUPI.Text = strtotalAmount;
                txtUPI.Focus();
                txtUPI.SelectAll();
                txtCredit.Enabled = false;
                btnCredit.Enabled = false;
                txtTouchTextBox = txtUPI;
                txtKeyBoardText.Clear();
            }
            else if (strPaymentMethod == "Credit")
            {
                txtCredit.Text = strtotalAmount;
                txtCredit.Focus();
                txtCredit.SelectAll();
                txtCash.Enabled = false;
                txtUPI.Enabled = false;
                txtCreditCard.Enabled = false;
                txtCreditNoteAmount.Enabled = false;
                txtTouchTextBox = txtCredit;
                txtKeyBoardText.Clear();

                btnCreditCard.Enabled = false;
                btnUPI.Enabled = false;
                btnCash.Enabled = false;
                btnCreditNote.Enabled = false;
            }

            CalculatePaidTotal();
            this.frmPOSSales = frm;
        }
        public void DoWhenComingFromPOSSales2Form(frmPOSSales2 frm, string strtotalAmount, string strPaymentMethod)
        {
            base.Show();

            btnCreditCard.Enabled = true;
            btnUPI.Enabled = true;
            btnCash.Enabled = true;
            btnCreditNote.Enabled = true;


            txtTotalAmount.Text = strtotalAmount;

            txtTenderedAmount.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtBalance.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

            txtCreditCard.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtUPI.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtCredit.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtCash.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            txtCreditNoteAmount.Text = Math.Round(0m, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

            strFromPaymentMethod = strPaymentMethod;
            if (strPaymentMethod == "Cash")
            {
                txtCash.Text = strtotalAmount;
                txtCash.Focus();
                txtCash.SelectAll();
                txtCredit.Enabled = false;
                btnCredit.Enabled = false;
                txtTouchTextBox = txtCash;
                txtKeyBoardText.Clear();
            }
            else if (strPaymentMethod == "CreditCard")
            {
                txtCreditCard.Text = strtotalAmount;
                txtCreditCard.Focus();
                txtCreditCard.SelectAll();
                txtCredit.Enabled = false;
                btnCredit.Enabled = false;
                txtTouchTextBox = txtCreditCard;
                txtKeyBoardText.Clear();
            }
            else if (strPaymentMethod == "UPI")
            {
                txtUPI.Text = strtotalAmount;
                txtUPI.Focus();
                txtUPI.SelectAll();
                txtCredit.Enabled = false;
                btnCredit.Enabled = false;
                txtTouchTextBox = txtUPI;
                txtKeyBoardText.Clear();
            }
            else if (strPaymentMethod == "Credit")
            {
                txtCredit.Text = strtotalAmount;
                txtCredit.Focus();
                txtCredit.SelectAll();
                txtCash.Enabled = false;
                txtUPI.Enabled = false;
                txtCreditCard.Enabled = false;
                txtCreditNoteAmount.Enabled = false;
                txtTouchTextBox = txtCredit;
                txtKeyBoardText.Clear();

                btnCreditCard.Enabled = false;
                btnUPI.Enabled = false;
                btnCash.Enabled = false;
                btnCreditNote.Enabled = false;
            }

            CalculatePaidTotal();
            this.frmPOSSales2 = frm;
        }

        public void DoWhenQuitingForm()
        {
            if (frmPOSSales != null)
            {
                decimal dcCreditCard = 0;
                decimal dcUPI = 0;
                decimal dcCredit = 0;
                decimal dcCash = 0;
                decimal dcCreditNote = 0;

                decimal dcTotTenderedAmt = 0;
                decimal dcTotalAmount = 0;
                decimal dcBalance = 0;
                string strTenderType = "";

                try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
                catch { }
                try { dcTotTenderedAmt = decimal.Parse(txtTenderedAmount.Text.ToString()); }
                catch { }
                try { dcBalance = decimal.Parse(txtBalance.Text.ToString()); }
                catch { }


                try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
                catch { }
                try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
                catch { }
                try { dcCredit = decimal.Parse(txtCredit.Text.ToString());
                if (dcCredit != 0)
                    strTenderType = "Credit Bill";
                else
                    strTenderType = "";
                }
                catch { }
                try { dcCash = decimal.Parse(txtCash.Text.ToString()); }
                catch { }
                try { dcCreditNote = decimal.Parse(txtCreditNoteAmount.Text.ToString()); }
                catch { }

                frmPOSSales.Enabled = true;
                frmPOSSales.DowhenReturningFromPOSPaymentForm(formCancel, txtCardNo.Text.Trim(), dcCreditCard, dcUPI, dcCredit, dcCash, txtCreditNoteNo.Text.Trim(), dcCreditNote, dcTotTenderedAmt, dcBalance, strTenderType);
            }
            if (frmPOSSales2 != null)
            {
                decimal dcCreditCard = 0;
                decimal dcUPI = 0;
                decimal dcCredit = 0;
                decimal dcCash = 0;
                decimal dcCreditNote = 0;

                decimal dcTotTenderedAmt = 0;
                decimal dcTotalAmount = 0;
                decimal dcBalance = 0;
                string strTenderType = "";

                try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
                catch { }
                try { dcTotTenderedAmt = decimal.Parse(txtTenderedAmount.Text.ToString()); }
                catch { }
                try { dcBalance = decimal.Parse(txtBalance.Text.ToString()); }
                catch { }


                try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
                catch { }
                try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
                catch { }
                try
                {
                    dcCredit = decimal.Parse(txtCredit.Text.ToString());
                    if (dcCredit != 0)
                        strTenderType = "Credit Bill";
                    else
                        strTenderType = "";
                }
                catch { }
                try { dcCash = decimal.Parse(txtCash.Text.ToString()); }
                catch { }
                try { dcCreditNote = decimal.Parse(txtCreditNoteAmount.Text.ToString()); }
                catch { }

                frmPOSSales2.Enabled = true;
                frmPOSSales2.DowhenReturningFromPOSPaymentForm(formCancel, txtCardNo.Text.Trim(), dcCreditCard, dcUPI, dcCredit, dcCash, txtCreditNoteNo.Text.Trim(), dcCreditNote, dcTotTenderedAmt, dcBalance, strTenderType);
            }
        }

        private void CalculatePaidTotal()
        {
            decimal dcCreditCard = 0;
            decimal dcUPI = 0;
            decimal dcCredit = 0;
            decimal dcCash = 0;
            decimal dcCreditNote = 0;

            decimal dcTotTenderedAmt = 0;
            decimal dcTotalAmount = 0;
            decimal dcBalance = 0;

            try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
            catch { }

            try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
            catch { }
            try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
            catch { }
            try { dcCredit = decimal.Parse(txtCredit.Text.ToString()); }
            catch { }
            try { dcCash = decimal.Parse(txtCash.Text.ToString()); }
            catch { }
            try { dcCreditNote = decimal.Parse(txtCreditNoteAmount.Text.ToString()); }
            catch { }

            dcTotTenderedAmt = (dcCreditCard + dcUPI + dcCredit + dcCash);

            txtTenderedAmount.Text = Math.Round(dcTotTenderedAmt, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);
            dcBalance = (dcTotalAmount - dcTotTenderedAmt);
            if (dcBalance >= 0)
            {
                dcBalance = -dcBalance;
            }
            else
            {
                dcBalance = System.Math.Abs(dcBalance);
            }
            txtBalance.Text = Math.Round(dcBalance, FinanceSettingsInfo._roundDecimal).ToString(FinanceSettingsInfo._roundDecimalPart);

        }
        private decimal CalculateBalanceTenderedAmt()
        {
            decimal dcCreditCard = 0;
            decimal dcUPI = 0;
            decimal dcCredit = 0;
            decimal dcCash = 0;
            decimal dcCreditNote = 0;

            decimal dcTotTenderedAmt = 0;
            decimal dcTotalAmount = 0;
            decimal dcBalance = 0;

            try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
            catch { }

            try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
            catch { }
            try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
            catch { }
            try { dcCredit = decimal.Parse(txtCredit.Text.ToString()); }
            catch { }
            try { dcCash = decimal.Parse(txtCash.Text.ToString()); }
            catch { }
            try { dcCreditNote = decimal.Parse(txtCreditNoteAmount.Text.ToString()); }
            catch { }

            dcTotTenderedAmt = (dcCreditCard + dcUPI + dcCredit + dcCash);

            
            dcBalance = (dcTotalAmount - dcTotTenderedAmt);

            return dcBalance;
        }
        private void CNAdjestCalculate()
        {
            txtCreditNoteAmount.Text = POSSalesMasterSP.GetCreditNoteAmount(txtCreditNoteNo.Text.Trim());

            decimal dcCreditNote = 0;
            try { dcCreditNote = decimal.Parse(txtCreditNoteAmount.Text.ToString()); }
            catch { }

            if (dcCreditNote != 0)
            {
                decimal dcTotalAmount = 0;
                try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
                catch { }

                txtTotalAmount.Text = (dcTotalAmount - dcCreditNote).ToString(FinanceSettingsInfo._roundDecimalPart);

                if (strFromPaymentMethod == "Cash")
                {
                    txtCash.Text = txtTotalAmount.Text.ToString();
                    txtCash.Focus();
                    txtCash.SelectAll();
                    txtTouchTextBox = txtCash;
                    txtKeyBoardText.Clear();
                }
                else if (strFromPaymentMethod == "CreditCard")
                {
                    txtCreditCard.Text = txtTotalAmount.Text.ToString();
                    txtCreditCard.Focus();
                    txtCreditCard.SelectAll();
                    txtTouchTextBox = txtCreditCard;
                    txtKeyBoardText.Clear();
                }
                else if (strFromPaymentMethod == "UPI")
                {
                    txtUPI.Text = txtTotalAmount.Text.ToString();
                    txtUPI.Focus();
                    txtUPI.SelectAll();
                    txtTouchTextBox = txtUPI;
                    txtKeyBoardText.Clear();
                }
                else if (strFromPaymentMethod == "Credit")
                {
                    txtCredit.Text = txtTotalAmount.Text.ToString();
                    txtCredit.Focus();
                    txtCredit.SelectAll();
                    txtTouchTextBox = txtCredit;
                    txtKeyBoardText.Clear();
                }

                CalculatePaidTotal();
            }
            else
            {
                MessageBox.Show("Credit Note already used");
                txtCreditNoteNo.Clear(); 
            }
  
        }
        private void frmPOSPayment_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoWhenQuitingForm();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            formCancel = true;
            this.Close();
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            if (strFocusedControl == "txtCardNo")
            {
                strFocusedControl = "txtCreditCard";
                
                txtTouchTextBox = txtCreditCard;
                txtTouchTextBox.Text = "";
                txtCreditCard.Focus();
                txtCreditCard.SelectAll();
            }
            else
            {
                strFocusedControl = "txtCardNo";
               
                txtTouchTextBox = txtCardNo;
                txtTouchTextBox.Text = "";
                txtCardNo.Focus();
                txtCardNo.SelectAll();
            }
            
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnOne.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnTwo.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnThree.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnFour.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnFive.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnSix.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnSeven.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnEight.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnNine.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnZero.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            txtKeyBoardText.Text = txtKeyBoardText.Text + btnDot.Text;
            txtTouchTextBox.Text = txtKeyBoardText.Text;
        }

        private void txtCash_Enter(object sender, EventArgs e)
        {
            txtTouchTextBox = sender as TextBox;
            txtTouchTextBox.Select();
              
            lblCash.ForeColor = Color.Red; 
        }

        private void btnUPI_Click(object sender, EventArgs e)
        {
            //txtUPI.Text = "";
            strFocusedControl = "txtUPI";
            txtTouchTextBox = txtUPI;
            //txtUPI.Text = CalculateBalanceTenderedAmt().ToString(SettingsInfo._roundDecimalPart);  
            txtUPI.Focus();
            txtUPI.SelectAll();
        }

        private void btnCredit_Click(object sender, EventArgs e)
        {
            //txtCredit.Text = "";
            strFocusedControl = "txtCredit";
            txtTouchTextBox = txtCredit;
            //txtCredit.Text = CalculateBalanceTenderedAmt().ToString(SettingsInfo._roundDecimalPart);  
            txtCredit.Focus();
            txtCredit.SelectAll();
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            //txtCash.Text = "";
            strFocusedControl = "txtCash";
            txtTouchTextBox = txtCash;
            //txtCash.Text = CalculateBalanceTenderedAmt().ToString(SettingsInfo._roundDecimalPart);  
            txtCash.Focus();
            txtCash.SelectAll();
        }

        private void btnCreditNote_Click(object sender, EventArgs e)
        {
            //txtCreditNoteNo.Text = "";
            strFocusedControl = "txtCreditNoteNo";
            txtTouchTextBox = txtCreditNoteNo;
            txtCreditNoteNo.Focus();
            txtCreditNoteNo.SelectAll();
        }

        private void txtCardNo_Enter(object sender, EventArgs e)
        {
            lblCreditCard.ForeColor = Color.Red;  
        }

        private void txtCreditCard_Enter(object sender, EventArgs e)
        {
            lblCreditCard.ForeColor = Color.Red; 
        }

        private void txtUPI_Enter(object sender, EventArgs e)
        {
            lblUPI.ForeColor = Color.Red; 
        }

        private void txtCredit_Enter(object sender, EventArgs e)
        {
            lblCredit.ForeColor = Color.Red; 
        }

        private void txtCreditNoteNo_Enter(object sender, EventArgs e)
        {
            lblCreditNote.ForeColor = Color.Red; 
        }

        private void txtCreditNoteAmount_Enter(object sender, EventArgs e)
        {
            lblCreditNote.ForeColor = Color.Red; 
        }

        private void txtCardNo_Leave(object sender, EventArgs e)
        {
            lblCreditCard.ForeColor = Color.Black; 
        }

        private void txtCreditCard_Leave(object sender, EventArgs e)
        {
            lblCreditCard.ForeColor = Color.Black; 
        }

        private void txtUPI_Leave(object sender, EventArgs e)
        {
            lblUPI.ForeColor = Color.Black; 
        }

        private void txtCredit_Leave(object sender, EventArgs e)
        {
            lblCredit.ForeColor = Color.Black; 
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            lblCash.ForeColor = Color.Black; 
        }

        private void txtCreditNoteAmount_Leave(object sender, EventArgs e)
        {
            lblCreditNote.ForeColor = Color.Black; 
        }

        private void txtCreditNoteNo_Leave(object sender, EventArgs e)
        {
            lblCreditNote.ForeColor = Color.Black; 
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Focus(); 
            txtTouchTextBox.SelectAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTouchTextBox.Clear();
            txtKeyBoardText.Clear();
            CalculatePaidTotal(); 
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtTouchTextBox.Name == "txtCardNo")
            {
                strFocusedControl = "txtCreditCard";
                txtTouchTextBox = txtCreditCard;
                txtCreditCard.Focus();
                txtCreditCard.SelectAll();
            }
            else if (txtTouchTextBox.Name == "txtCreditCard")
            {
                decimal dcCreditCard = 0;
                decimal dcTotalAmount = 0;
                decimal dcUPI = 0;

                try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
                catch { }
                try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
                catch { }
                try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
                catch { }

                txtCreditCard.Text = dcCreditCard.ToString(FinanceSettingsInfo._roundDecimalPart);

                if (dcCreditCard != 0)
                {
                    if ((dcCreditCard + dcUPI) > dcTotalAmount)
                    {
                        MessageBox.Show("Cannot exceed Total Amount");

                        //txtCreditCard.Text = (dcTotalAmount - (dcUPI)).ToString(SettingsInfo._roundDecimalPart);
                        //try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
                        //catch { }
                        txtCreditCard.Focus();
                        txtCreditCard.SelectAll();

                        //txtCreditCard.Clear();
                        txtKeyBoardText.Clear();
                        txtCash.Text = (0m).ToString(FinanceSettingsInfo._roundDecimalPart);


                        return;
                    }
                    txtCash.Text = (dcTotalAmount - (dcCreditCard + dcUPI)).ToString(FinanceSettingsInfo._roundDecimalPart) ;
                    CalculatePaidTotal();
                    //btnCash_Click(e, e);
                }
                txtCash.Focus();
                txtCash.SelectAll();
            }
            else if (txtTouchTextBox.Name == "txtUPI")
            {
                decimal dcCreditCard = 0;
                decimal dcTotalAmount = 0;
                decimal dcUPI = 0;

             
                try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
                catch { }
                try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
                catch { }
                try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
                catch { }

                txtUPI.Text = dcUPI.ToString(FinanceSettingsInfo._roundDecimalPart);

                if (dcUPI != 0)
                {
                    if ((dcCreditCard + dcUPI) > dcTotalAmount)
                    {
                         MessageBox.Show("Cannot exceed Total Amount");

                        //txtUPI.Text = (dcTotalAmount - (dcCreditCard)).ToString(SettingsInfo._roundDecimalPart);
                        //try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
                        //catch { }
                        txtUPI.Focus();
                        txtUPI.SelectAll();
                        //txtUPI.Clear();
                        txtKeyBoardText.Clear();
                        txtCash.Text = (0m).ToString(FinanceSettingsInfo._roundDecimalPart);
                        return;
                    }
                    txtCash.Text = (dcTotalAmount - (dcCreditCard + dcUPI)).ToString(FinanceSettingsInfo._roundDecimalPart);
                    CalculatePaidTotal();
                    //btnCash_Click(e, e);
                }
                txtCash.Focus();
                txtCash.SelectAll();
            }
            else if (txtTouchTextBox.Name == "txtCash")
            {
                decimal dcCreditCard = 0;
                decimal dcTotalAmount = 0;
                decimal dcUPI = 0;
                decimal dcCash = 0;

                try { dcCash = decimal.Parse(txtCash.Text.ToString()); }
                catch { }
                try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
                catch { }
                try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
                catch { }
                try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
                catch { }

                //if (dcCash != 0)
                //{
                //txtCash.Text = (dcTotalAmount - (dcCreditCard + dcUPI)).ToString(SettingsInfo._roundDecimalPart);
                CalculatePaidTotal();
                //}

                btnSave.Focus();
            }
            else if (txtTouchTextBox.Name == "txtCreditNoteNo")
            {
                if (txtCreditNoteNo.Text.Trim() != "")
                {
                    CNAdjestCalculate();
                }
            }
            txtKeyBoardText.Clear();


            
            

        }
        public void caluclate()
        {
            float upi = 0;
            float credit = 0;
            float cash = 0;
            float tender = 0;
            float total = 0;

            try
            {
                total = float.Parse(txtTotalAmount.Text);
            }
            catch { }

            try
            {
                upi = float.Parse(txtUPI.Text);

            }
            catch
            {
            }
            try
            {
                credit = float.Parse(txtCredit.Text);
            }
            catch
            {
            }
            try
            {
                cash = float.Parse(txtCash.Text);
            }
            catch
            {
            }

            tender = upi + credit + cash;
            txtTenderedAmount.Text = tender.ToString("F2");

            float bal = total - tender;
            txtBalance.Text = bal.ToString("F2");

        
    }
        public void caluclatesecond()
        {
            float upi = 0;
            float credit = 0;
            float cash = 0;
            float sum = 0;
            float total = 0;

            try
            {
                total = float.Parse(txtTotalAmount.Text);
            }
            catch { }

            try
            {
                upi = float.Parse(txtUPI.Text);

            }
            catch
            {
            }
            try
            {
                credit = float.Parse(txtCredit.Text);
            }
            catch
            {
            }
            try
            {
                cash = float.Parse(txtCash.Text);
            }
            catch
            {
            }
            if (upi == 0 || credit ==0)
            {
                txtCash.Text = total.ToString("F2");
                       

            }
            else if (cash == 0 || credit == 0)
            {
                txtUPI.Text = total.ToString("F2");
            }
            else if (cash == 0 || upi == 0)
            {
                txtCredit.Text = total.ToString("F2");
            }
            


        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal dcTotalAmount = 0;
            decimal dcCreditCard = 0;
            decimal dcUPI = 0;
            decimal dcDiff = 0;
            try { dcCreditCard = decimal.Parse(txtCreditCard.Text.ToString()); }
            catch { }
            try { dcUPI = decimal.Parse(txtUPI.Text.ToString()); }
            catch { }
            try { dcTotalAmount = decimal.Parse(txtTotalAmount.Text.ToString()); }
            catch { }

            dcDiff = (dcCreditCard + dcUPI) - dcTotalAmount;
            if (dcDiff <= 0)
            {
                CalculatePaidTotal();
                decimal dcBalance = 0;
                try { dcBalance = decimal.Parse(txtBalance.Text.ToString()); }
                catch { }

                if (dcBalance >= 0)
                {
                    formCancel = false;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Cannot exceed Total Amount");
            }
            
            
        }

        private void txtCreditNoteNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCreditNoteNo.Text.Trim() != "")
                {
                    CNAdjestCalculate();
                }
                else
                {
                    CalculatePaidTotal(); 
                }
            }
        }

        private void txtCreditCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
            if (e.KeyChar == Convert.ToChar(13))
            {
                btnEnter.PerformClick();
            }
        }

        private void txtUPI_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
            if (e.KeyChar == Convert.ToChar(13))
            {
                btnEnter.PerformClick();
            }
        }

        private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
            if (e.KeyChar == Convert.ToChar(13))
            {
                btnEnter.PerformClick();
            }
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
            if (e.KeyChar == Convert.ToChar(13))
            {
                btnEnter.PerformClick();  
            }
            
        }

        private void txtCreditNoteAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            objComboValidation.DecimalValidation(sender, e, false);
            if (e.KeyChar == Convert.ToChar(13))
            {
                btnEnter.PerformClick();
            }
        }

        private void txtCardNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13))
            {
                btnEnter.PerformClick();
            }
        }

        private void txtCreditCard_TextChanged(object sender, EventArgs e)
        {
           // caluclate();
        }

        private void txtUPI_TextChanged(object sender, EventArgs e)
        {
            //caluclate();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
           // caluclate();
        }

        private void txtCredit_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmPOSPayment_Load(object sender, EventArgs e)
        {
            clsGeneral objGeneral = new clsGeneral();
            objGeneral.formSettings(this);
        }
    }
}
