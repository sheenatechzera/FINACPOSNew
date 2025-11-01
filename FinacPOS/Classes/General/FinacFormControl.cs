using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;


namespace FinacPOS
{
    class FinacFormControl
    {
        public void formSettings(Control objCFrom)
        {
            if (PublicVariables._ModuleLanguage == "ARB")
            {
                FinacControlSettingsSP SPfinacControlSettings = new FinacControlSettingsSP();
                dtLang = SPfinacControlSettings.GetWord();

                dtFont = SPfinacControlSettings.GetFontName();

                SetAllControlsInForm(objCFrom);
            }
        }

        DataTable dtLang = new DataTable();
        DataTable dtFont = new DataTable();
        string formName = "";

        private void SetAllControlsInForm(Control ctrlContainer)
        {
            

            if (ctrlContainer is Form)
                formName = ctrlContainer.Name;

            foreach (Control ctrl in ctrlContainer.Controls)
            {
                //MessageBox.Show(ctrl.GetType().ToString());
                if (ctrl.GetType() == typeof(DataGridView))
                {
                    DataGridView objDGV = (DataGridView)ctrl;

                    SetLabelText(formName, objDGV);
                }
                else if (ctrl.GetType() == typeof(FinacPOS.dgv.DataGridViewEnter))
                {
                    //FinacAcount.dgv.DataGridViewEnter objDGV = (FinacAcount.dgv.DataGridViewEnter)ctrl;
                    DataGridView objDGV = (DataGridView)ctrl;
                    SetLabelText(formName, objDGV);
                }
                else if (ctrl.GetType() == typeof(ComboBox))
                {
                    ComboBox objctrlcbo = (ComboBox)ctrl;

                    SetLabelText(formName, objctrlcbo);
                }
                else if (ctrl.GetType() == typeof(Button))
                {
                    Button objbtn = (Button)ctrl;

                    SetLabelText(formName, objbtn);
                }
                else if (ctrl.GetType() == typeof(Label))
                {
                    Label objlBl = (Label)ctrl;

                    SetLabelText(formName, objlBl);
                }
                else if (ctrl.GetType() == typeof(CheckBox))
                {
                    CheckBox objlBl = (CheckBox)ctrl;

                    SetLabelText(formName, objlBl);
                }

                if (ctrl.HasChildren)
                    SetAllControlsInForm(ctrl);
            }

        }

        private void SetLabelText(string formname, Control objControl)
        {
           
            string strfontname = "";
            if (dtFont.Rows.Count > 0)
            {
                if (dtFont.Rows[0]["ArabicFont"].ToString() != "")
                {
                    strfontname = Convert.ToString(dtFont.Rows[0]["ArabicFont"]);
                }
                else
                    strfontname = "Andalus Regular";
            }
            else
                strfontname = "Andalus Regular";

            if (objControl is DataGridView || objControl is FinacPOS.dgv.DataGridViewEnter)
            {
                DataGridView objDGV = (DataGridView)objControl;
                for (int i = 0; i <= objDGV.Columns.Count - 1; i++)
                {
                    if (dtLang.Rows.Count > 0)
                    {
                        DataRow[] dtFIlterRow;
                        dtFIlterRow = dtLang.Select("ControlName='" + objDGV.Columns[i].Name + "' and FormName='" + formname + "'");
                        if (dtFIlterRow.Length > 0)
                        {
                            if (dtFIlterRow[0]["Arabic"].ToString() != "")
                            {

                                if (objDGV.InvokeRequired)
                                {
                                    objDGV.Invoke((MethodInvoker)delegate
                                    {
                                        Font newFont = new Font(strfontname, 10, FontStyle.Regular);
                                        objDGV.Columns[i].HeaderCell.Style.Font = newFont;
                                        objDGV.Columns[i].HeaderText = getArabicText(dtFIlterRow, objControl);
                                        objDGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    });
                                }
                                else
                                {
                                    Font newFont = new Font(strfontname, 10, FontStyle.Regular);
                                    objDGV.Columns[i].HeaderCell.Style.Font = newFont;
                                    objDGV.Columns[i].HeaderText = getArabicText(dtFIlterRow, objControl);
                                    objDGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                }

                                


                                //objControl.Font = new Font("Andalus Regular", 10, FontStyle.Regular);
                                //objControl.Text = getArabicText(dtFIlterRow, objControl);
                                //objControl.RightToLeft = RightToLeft.Yes;
                            }
                        }
                    }
                }

            }
            else
            {
                if (dtLang.Rows.Count > 0)
                {
                    DataRow[] dtFIlterRow;
                    dtFIlterRow = dtLang.Select("ControlName='" + objControl.Name + "' and FormName='" + formname + "'");
                    if (dtFIlterRow.Length > 0)
                    {
                        if (dtFIlterRow[0]["Arabic"].ToString() != "")
                        {
                            if (objControl.InvokeRequired)
                            {
                                objControl.Invoke((MethodInvoker)delegate
                                {
                                    objControl.Font = new Font(strfontname, 10, FontStyle.Regular);
                                    objControl.Text = getArabicText(dtFIlterRow, objControl);
                                    objControl.RightToLeft = RightToLeft.Yes;
                                });
                            }
                            else
                            {
                                objControl.Font = new Font(strfontname, 10, FontStyle.Regular);
                                objControl.Text = getArabicText(dtFIlterRow, objControl);
                                objControl.RightToLeft = RightToLeft.Yes;
                            }
                           
                        }
                    }
                }
            }


        }

        private string getArabicText(DataRow[] drFilter, Control objControl)
        {
            string arabicText = "";
            try
            {
                if (drFilter.Length > 0)
                {
                    if (drFilter[0]["ARABIC"].ToString() != "")
                    {
                        //byte[] arabicBytes = (byte[])drFilter[0]["ARABIC"];
                        //arabicText = System.Text.Encoding.UTF8.GetString(arabicBytes);
                        arabicText = drFilter[0]["ARABIC"].ToString();
                        return arabicText;
                    }
                    else
                    {
                        return arabicText;
                    }
                }
                else
                    return arabicText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return arabicText;
        }
    }
}
