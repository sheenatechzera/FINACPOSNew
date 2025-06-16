using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace FinacPOS
{
    class ComboValidation
    {
       
       
        /*this function is for normal combobox with datasource assigned .
       it clears text entered in combobox if there is no such item in their datasource */

       
        public void CheckCollection(ComboBox cmb)
        {
            try
            {
                if (cmb.SelectedIndex == -1)
                {
                    cmb.Text = "";
                  
                }
                else
                {
                        DataTable dtbl = (DataTable)cmb.DataSource;
                        if (dtbl.Rows.Count > 0)
                        {
                            DataSet dsCurr = new DataSet();

                            //------Replace is given to avoid error when cmb.Text contains single quotes----------------------------//
                            dsCurr.Merge(dtbl.Select(dtbl.Columns[cmb.DisplayMember].ToString() + " = '" + cmb.Text.ToString().Replace(@"'", "''") + "'"));

                        
                            //--------------------------------------------------------------------------------

                            if (dsCurr.Tables.Count>0&&( cmb.SelectedValue == null || dsCurr.Tables[0].Rows[0][cmb.ValueMember].ToString() != cmb.SelectedValue.ToString()))
                            {
                                cmb.SelectedValue = dsCurr.Tables[0].Rows[0][cmb.ValueMember].ToString();
                            }
                        }
                    
                }
            }
            catch (Exception ex)
            {
                cmb.Text = "";
          
                MessageBox.Show("CV2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        public void DecimalValidation(object sender, KeyPressEventArgs e, bool isNegativeFiled)
        {
            try
            {
                TextBox txt = (TextBox)sender;
                if (!char.IsNumber(e.KeyChar))
                {
                    e.Handled = true;
                }

                if (e.KeyChar == 8)
                {
                    e.Handled = false;
                }
                if (e.KeyChar == 46)
                {
                    if (txt.Text.Contains(".") && txt.SelectionStart != 0)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        if (txt.Text == "" || txt.SelectionStart == 0)
                        {
                            txt.Clear();
                            txt.Text = "0.";
                            txt.SelectionStart = txt.Text.Length;

                        }
                        else
                        {
                            txt.Text = txt.Text + ".";
                            txt.SelectionStart = txt.Text.Length;
                        }
                    }
                }
                else if (e.KeyChar == 45 && (isNegativeFiled))
                {
                    if (txt.Text.Contains("-") && txt.SelectionStart != 0)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        txt.Clear();
                        txt.Text = "-";
                        txt.SelectionStart = txt.Text.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("SI46:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        public void DecimalValidationGRid(object sender, KeyPressEventArgs e, DataGridViewTextBoxEditingControl TextBoxControl,bool  isNegativeFiled)
        {
            if (!char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
            if (e.KeyChar == 46)
            {
                if (TextBoxControl.Text.Contains(".") && TextBoxControl.SelectionStart!=0)
                {
                    e.Handled = true;
                }
                else
                {
                    if (TextBoxControl.Text == "" || TextBoxControl.SelectionStart == 0)
                    {
                        TextBoxControl.Clear();
                        TextBoxControl.Text = "0.";
                        TextBoxControl.SelectionStart = TextBoxControl.Text.Length;

                    }
                    else
                    {
                        TextBoxControl.Text = TextBoxControl.Text + ".";
                        TextBoxControl.SelectionStart = TextBoxControl.Text.Length;
                    }
                }
            }
            else if (e.KeyChar == 45 && (isNegativeFiled))
            {
                if (TextBoxControl.Text.Contains("-") && TextBoxControl.SelectionStart != 0)
                {
                    e.Handled = true;
                }
                else
                {
                    TextBoxControl.Clear();
                    TextBoxControl.Text = "-";
                    TextBoxControl.SelectionStart = TextBoxControl.Text.Length;
                }
            }
        }
        public void IntigerFieldKeypress(object sender, KeyPressEventArgs e)
        {
            try
            {
                TextBox txtObj = (TextBox)sender;

                if ((e.KeyChar > 47 && e.KeyChar < 58) || (e.KeyChar == 8))
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
                MessageBox.Show("SI16:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
      
    }
}