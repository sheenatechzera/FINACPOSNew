using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace FinacPOS
{
    class clsGeneral : DBConnection
    {

        public void formSettings(Control objCFrom)
        {
        //if (PublicVariables._ModuleLanguage == "ARB")
        //{
                SetAllControlsInForm(objCFrom);
            //}
            //else
            //{

            //}
        }


        string formName = "";

        private void SetAllControlsInForm(Control ctrlContainer)
        {
            if (ctrlContainer is Form)
                formName = ctrlContainer.Name;

            foreach (Control ctrl in ctrlContainer.Controls)
            {

                if (ctrl.GetType() == typeof(DataGridView))
                {
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

        //private void SetLabelText(string formname, Control objControl)
        //{
        //    LanguageEntrySP LanguageEntry = new LanguageEntrySP();
        //    DataTable dtLang = LanguageEntry.GetWord();

        //    DataTable dtFont = LanguageEntry.GetFontName();
        //    string strfontname = "";
        //    if (dtFont.Rows.Count > 0)
        //    {
        //        if (dtFont.Rows[0]["ArabicFont"].ToString() != "")
        //        {
        //            strfontname = Convert.ToString(dtFont.Rows[0]["ArabicFont"]);
        //        }
        //        else
        //            strfontname = "Andalus Regular";
        //    }
        //    else
        //        strfontname = "Andalus Regular";

        //    if (objControl is DataGridView)
        //    {
        //        DataGridView objDGV = (DataGridView)objControl;
        //        for (int i = 0; i <= objDGV.Columns.Count - 1; i++)
        //        {
        //            if (dtLang.Rows.Count > 0)
        //            {
        //                DataRow[] dtFIlterRow;
        //                dtFIlterRow = dtLang.Select("ControlName='" + objDGV.Columns[i].Name + "' and FormName='" + formname + "'");
        //                if (dtFIlterRow.Length > 0)
        //                {
        //                    if (dtFIlterRow[0]["Arabic"].ToString() != "")
        //                    {
        //                        Font newFont = new Font(strfontname, 10, FontStyle.Regular);
        //                        objDGV.Columns[i].HeaderCell.Style.Font = newFont;
        //                        objDGV.Columns[i].HeaderText = getArabicText(dtFIlterRow, objControl);
        //                        objDGV.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;


        //                        //objControl.Font = new Font("Andalus Regular", 10, FontStyle.Regular);
        //                        //objControl.Text = getArabicText(dtFIlterRow, objControl);
        //                        //objControl.RightToLeft = RightToLeft.Yes;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    else
        //    {
        //        if (dtLang.Rows.Count > 0)
        //        {
        //            DataRow[] dtFIlterRow;
        //            dtFIlterRow = dtLang.Select("ControlName='" + objControl.Name + "' and FormName='" + formname + "'");
        //            if (dtFIlterRow.Length > 0)
        //            {
        //                if (PublicVariables._ModuleLanguage == "ARB")
        //                {
        //                    if (dtFIlterRow[0]["Arabic"].ToString() != "")
        //                    {
        //                        objControl.Font = new Font(strfontname, 10, FontStyle.Regular);
        //                        objControl.Text = getArabicText(dtFIlterRow, objControl);
        //                        objControl.RightToLeft = RightToLeft.Yes;
        //                    }
        //                }
        //                else
        //                {
        //                    if (dtFIlterRow[0]["English"].ToString() != "")
        //                    {
        //                        objControl.Font = new Font(strfontname, 10, FontStyle.Regular);
        //                        objControl.Text = getEnglishText(dtFIlterRow, objControl);
        //                        objControl.RightToLeft = RightToLeft.No;
        //                    }
        //                }
        //            }
        //        }
        //    }


        //}
        private void SetLabelText(string formname, Control objControl)
        {
            LanguageEntrySP LanguageEntry = new LanguageEntrySP();
            DataTable dtLang = LanguageEntry.GetWord();
            DataTable dtFont = LanguageEntry.GetFontName();

            string strfontname = dtFont.Rows.Count > 0 && !string.IsNullOrEmpty(dtFont.Rows[0]["ArabicFont"].ToString())
                ? dtFont.Rows[0]["ArabicFont"].ToString()
                : "Arial Unicode MS";

            if (objControl is DataGridView dgv)
            {
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    DataRow[] rows = dtLang.Select($"(ControlName='{col.Name}' OR ControlName='{col.HeaderText}') AND FormName='{formname}'");
                    if (rows.Length > 0)
                    {
                        DataRow row = rows[0];
                        if (PublicVariables._ModuleLanguage == "ARB" && !string.IsNullOrEmpty(row["Arabic"].ToString()))
                        {
                            col.HeaderCell.Style.Font = new Font(strfontname, 10, FontStyle.Regular);
                            col.HeaderText = getArabicText(rows, objControl);
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        else if (!string.IsNullOrEmpty(row["English"].ToString()))
                        {
                            col.HeaderCell.Style.Font = new Font(strfontname, 10, FontStyle.Regular);
                            col.HeaderText = getEnglishText(rows, objControl);
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        }
                    }
                }
            }
            else
            {
                DataRow[] rows = dtLang.Select($"ControlName='{objControl.Name}' AND FormName='{formname}'");
                if (rows.Length > 0)
                {
                    DataRow row = rows[0];
                    string langField = PublicVariables._ModuleLanguage == "ARB" ? "Arabic" : "English";
                    if (!string.IsNullOrEmpty(row[langField].ToString()) && objControl.GetType().GetProperty("Text") != null)
                    {
                        objControl.Font = new Font(strfontname, 10, FontStyle.Regular);
                        objControl.Text = PublicVariables._ModuleLanguage == "ARB"
                            ? getArabicText(rows, objControl)
                            : getEnglishText(rows, objControl);
                        objControl.RightToLeft = PublicVariables._ModuleLanguage == "ARB" ? RightToLeft.Yes : RightToLeft.No;
                    }
                }
            }

            // Recurse for nested controls
            foreach (Control child in objControl.Controls)
            {
                SetLabelText(formname, child);
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
                        byte[] arabicBytes = (byte[])drFilter[0]["ARABIC"];
                        arabicText = System.Text.Encoding.UTF8.GetString(arabicBytes);
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
        private string getEnglishText(DataRow[] drFilter, Control objControl)
        {
            string ENGText = "";
            try
            {
                if (drFilter.Length > 0)
                {
                    if (drFilter[0]["English"].ToString() != "")
                    {
                        ENGText = drFilter[0]["English"].ToString();
                        //ENGText = System.Text.Encoding.UTF8.GetString(ENGLISHBytes);
                        return ENGText;
                    }
                    else
                    {
                        return ENGText;
                    }
                }
                else
                    return ENGText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ENGText;
        }

        public void UserActivityAdd(string voucherType, string activityType, string masterId, string voucherNo, DateTime voucherdate, decimal oldAmount, decimal newAmount)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("UserActivityAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@VoucherType", SqlDbType.NVarChar);
                sprmparam.Value = voucherType;
                sprmparam = sccmd.Parameters.Add("@Date", SqlDbType.DateTime);
                sprmparam.Value = DateTime.Now;
                sprmparam = sccmd.Parameters.Add("@UserId", SqlDbType.NVarChar);
                sprmparam.Value = PublicVariables._currentUserId;
                sprmparam = sccmd.Parameters.Add("@SystemName", SqlDbType.NVarChar);
                sprmparam.Value = Environment.MachineName;
                sprmparam = sccmd.Parameters.Add("@ActivityType", SqlDbType.NVarChar);
                sprmparam.Value = activityType;
                sprmparam = sccmd.Parameters.Add("@MasterId", SqlDbType.NVarChar);
                sprmparam.Value = masterId;
                sprmparam = sccmd.Parameters.Add("@VoucherNo", SqlDbType.NVarChar);
                sprmparam.Value = voucherNo;
                sprmparam = sccmd.Parameters.Add("@VoucherDate", SqlDbType.DateTime);
                sprmparam.Value = voucherdate;
                sprmparam = sccmd.Parameters.Add("@VoucherOldAmount", SqlDbType.Decimal);
                sprmparam.Value = oldAmount;
                sprmparam = sccmd.Parameters.Add("@VoucherNewAmount", SqlDbType.Decimal);
                sprmparam.Value = newAmount;
                sccmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
        }
        public byte[] ReadFile(string strImagePath)
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

        public static string MessageFunction(string strProcessType)
        {
            string strmsg = "";

            if (strProcessType == "Save")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Do you want to save?";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "هل تريد الحفظ؟";
                }
            }
            else if (strProcessType == "Update")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Do you want to update?";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "هل تريد التحديث؟";
                }
            }
            else if (strProcessType == "Deleted")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Deleted successfully";
                }
                  
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "تم الحذف بنجاح";
                }
                  
            }

            else if (strProcessType == "Close")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Do you want to close?";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "هل تريد الإغلاق؟";
                }
            }
            else if (strProcessType == "SelectSessionDate")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Select Session date";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "اختر تاريخ الجلسة";
                }
            }
            else if (strProcessType == "SessionCreated")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Session created successfully";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "تم إنشاء الجلسة بنجاح";
                }
            }
            else if (strProcessType == "Print")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Do you want to print?";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "هل تريد الطباعة؟";
                }
            }
            else if (strProcessType == "BarcodeNotFound")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Barcode not Found";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "لم يتم العثور على الباركود";
                }
            }
            else if (strProcessType == "InvalidBarcode")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Please enter a valid barcode.";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "الرجاء إدخال باركود صالح.";
                }
            }
            else if (strProcessType == "ConfirmDelete")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Do you want to delete?";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "هل تريد الحذف؟";
                }
            }
            else if (strProcessType == "Updated")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Updated successfully";
                }
                    
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "تم التحديث بنجاح"; 
                }
                    
            }
            else if (strProcessType == "Saved")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    strmsg = "Saved Successfully";
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    strmsg = "تم الحفظ بنجاح";
                }
            }

            return strmsg;
        }


    }
}
