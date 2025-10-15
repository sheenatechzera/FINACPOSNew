using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace FinacPOS
{
    public partial class frmPOSControlSettings : Form
    {
        public frmPOSControlSettings()
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
        LanguageEntrySP SPLanguageEntry = new LanguageEntrySP();
        LanguageEntryInfo InfoLanguageEntry = new LanguageEntryInfo();

        const int GRD_WORD_ID = 0;
        const int GRD_FORM_ID = 1;
        const int GRD_CONTROL_NAME = 2;
        const int GRD_CONTROL_TYPE = 3;
        const int GRD_WORD_ENGLISH = 4;
        const int GRD_WORD_ARABIC = 5;



        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;

            for (i = 0; i <= dgvLabels.Rows.Count - 1; i++)
            {

                LanguageEntryWordInfo InfoWord = new LanguageEntryWordInfo();
                InfoWord.FormId = int.Parse(Convert.ToString(cmbFormName.SelectedValue));
                InfoWord.ControlName = Convert.ToString(dgvLabels.Rows[i].Cells[GRD_CONTROL_NAME].Value);
                InfoWord.ControlType = Convert.ToString(dgvLabels.Rows[i].Cells[GRD_CONTROL_TYPE].Value);
                InfoWord.English = Convert.ToString(dgvLabels.Rows[i].Cells[GRD_WORD_ENGLISH].Value);


                string arabicText = Convert.ToString(dgvLabels.Rows[i].Cells[GRD_WORD_ARABIC].Value);
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(arabicText);

                InfoWord.Arabic = bytes;
                LanguageEntrySP LangEntry = new LanguageEntrySP();
                LangEntry.LanguageEntryWordSave(InfoWord);
            }



            if (PublicVariables._ModuleLanguage == "ENG")
            {
                MessageBox.Show("Settings saved successfully", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (PublicVariables._ModuleLanguage == "ARB")
            {
                MessageBox.Show("تم حفظ الإعدادات بنجاح", "معلومة",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void frmLanguageEntry_Load(object sender, EventArgs e)
        {
            clsGeneral objGeneral = new clsGeneral();
            objGeneral.formSettings(this);
            FillCombo();
        }
        private void FillCombo()
        {
            LanguageEntrySP LanguageEntry = new LanguageEntrySP();
            DataTable dt = LanguageEntry.LanguageEntryFormDtls(3);

            if (dt.Rows.Count > 0)
            {
                cmbFormName.DataSource = dt;
                cmbFormName.DisplayMember = dt.Columns["FormCaption"].ColumnName;
                cmbFormName.ValueMember = dt.Columns["FormId"].ColumnName;
            }
            cmbFormName.SelectedIndex = -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetFormNames_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                LanguageEntrySP LanguageEntry = new LanguageEntrySP();

                DataTable dt = LanguageEntry.LanguageEntryFormDtls(0);

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (dt.Rows[i]["FormCaption"] + "" == "")
                    {
                        string fullFormName = this.GetType().Namespace + "." + dt.Rows[i]["FormName"];
                        Type formType = Assembly.GetEntryAssembly().GetType(fullFormName);

                        if (formType != null && formType.IsSubclassOf(typeof(Form)))
                        {
                            // Check if the formType has a parameterless constructor
                            System.Reflection.ConstructorInfo paramLessConstructor = formType.GetConstructor(Type.EmptyTypes);

                            if (paramLessConstructor == null)
                            {
                                // Create a parameterless constructor for the formType
                                AssemblyBuilder dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynamicAssembly"), AssemblyBuilderAccess.Run);
                                ModuleBuilder dynamicModule = dynamicAssembly.DefineDynamicModule("DynamicModule");
                                TypeBuilder dynamicType = dynamicModule.DefineType(fullFormName + "DynamicType", TypeAttributes.Public, formType);

                                // Define the parameterless constructor
                                MethodAttributes constructorAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
                                ConstructorBuilder constructor = dynamicType.DefineConstructor(constructorAttributes, CallingConventions.Standard, Type.EmptyTypes);
                                ILGenerator ilGen = constructor.GetILGenerator();
                                ilGen.Emit(OpCodes.Ldarg_0);
                                ilGen.Emit(OpCodes.Call, typeof(Form).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null));
                                ilGen.Emit(OpCodes.Ret);

                                Type dynamicFormType = dynamicType.CreateType();

                                Form formInstance = (Form)Activator.CreateInstance(dynamicFormType);

                                LanguageEntryInfo InfoLang = new LanguageEntryInfo();
                                InfoLang.Action = 2;
                                InfoLang.FormId = int.Parse(Convert.ToString(dt.Rows[i]["FormId"]));
                                InfoLang.FormCaption = Convert.ToString(formInstance.Text);
                                LanguageEntrySP LangEntry = new LanguageEntrySP();
                                LangEntry.LanguageEntryEdit(InfoLang);

                            }
                            else
                            {
                                Form formInstance = (Form)Activator.CreateInstance(formType);
                                LanguageEntryInfo InfoLang = new LanguageEntryInfo();
                                InfoLang.Action = 2;
                                InfoLang.FormId = int.Parse(Convert.ToString(dt.Rows[i]["FormId"]));
                                InfoLang.FormCaption = Convert.ToString(formInstance.Text);
                                LanguageEntrySP LangEntry = new LanguageEntrySP();
                                LangEntry.LanguageEntryEdit(InfoLang);
                            }
                        }
                        else
                            MessageBox.Show("Form not found: " + dt.Rows[i]["FormName"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                MessageBox.Show("Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.None);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Form not found: " + i, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool bolLabels = false, bolCheckBox = false, bolButton = false, bolRadioButton = false, bolDataGridView = false;
        private void FillControlsinGrid()
        {
            try
            {
                DataRow[] drFrmFilter;

                LanguageEntrySP LanguageEntry = new LanguageEntrySP();

                DataTable dt = LanguageEntry.LanguageEntryFormDtls(4);

                dgvLabels.Rows.Clear();

                if (dt.Rows.Count > 0)
                {
                    drFrmFilter = dt.Select("FormId=" + cmbFormName.SelectedValue);
                    string fullFormName = this.GetType().Namespace + "." + drFrmFilter[0]["FormName"];

                    if (Convert.ToString(drFrmFilter[0]["Labels"]) == "Y")
                    {
                        bolLabels = true;
                    }
                    else
                    { bolLabels = false; }


                    if (Convert.ToString(drFrmFilter[0]["CheckBox"]) == "Y")
                    { bolCheckBox = true; }
                    else
                    { bolCheckBox = false; }

                    if (Convert.ToString(drFrmFilter[0]["Button"]) == "Y")
                    { bolButton = true; }
                    else
                    { bolButton = false; }

                    if (Convert.ToString(drFrmFilter[0]["RadioButton"]) == "Y")
                    { bolRadioButton = true; }
                    else
                    { bolRadioButton = false; }

                    if (Convert.ToString(drFrmFilter[0]["DataGridView"]) == "Y")
                    { bolDataGridView = true; }
                    else
                    { bolDataGridView = false; }


                     Type formType = Assembly.GetEntryAssembly().GetType(fullFormName);
                    

                    if (formType != null && formType.IsSubclassOf(typeof(Form)))
                    {
                        System.Reflection.ConstructorInfo paramLessConstructor = formType.GetConstructor(Type.EmptyTypes);

                        if (paramLessConstructor == null)
                        {
                            AssemblyBuilder dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("DynamicAssembly"), AssemblyBuilderAccess.Run);
                            ModuleBuilder dynamicModule = dynamicAssembly.DefineDynamicModule("DynamicModule");
                            TypeBuilder dynamicType = dynamicModule.DefineType(fullFormName + "DynamicType", TypeAttributes.Public, formType);

                            Type dynamicFormType = dynamicType.CreateType();
                            Form formInstance = (Form)Activator.CreateInstance(dynamicFormType);
                            GetControls(dt, formInstance);
                        }
                        else
                        {
                            Form formInstance = (Form)Activator.CreateInstance(formType);
                            GetControls(dt, formInstance);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cmbFormName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillControlsinGrid();
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvLabels.Rows.Count; ++i)
            {
                if (dgvLabels["colWordArabic", i].Value == null || dgvLabels["colWordArabic", i].Value.ToString() == "")
                {
                    try
                    {
                        dgvLabels["colWordArabic", i].Value = GoogleTranslate(dgvLabels["colWordEnglish", i].Value.ToString(), "en", "ar").ToString();
                    }
                    catch { dgvLabels["colWordArabic", i].Value = dgvLabels["colWordEnglish", i].Value.ToString(); }
                }
            }
        }
        public static string GoogleTranslate(string inputtext, string fromlangid, string tolangid)
        {
            inputtext = HttpUtility.HtmlAttributeEncode(inputtext);
            using (WebClient step1 = new WebClient())
            {
                step1.Encoding = Encoding.UTF8;
                string step2 = step1.DownloadString("https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=" + tolangid + "&hl=" + fromlangid + "&dt=t&dt=bd&dj=1&source=icon&q=" + inputtext);
                Newtonsoft.Json.Linq.JObject step3 = Newtonsoft.Json.Linq.JObject.Parse(step2);
                string step4 = step3.SelectToken("sentences[0]").SelectToken("trans").ToString();
                return step4;
            }
        }
        private void GetControls(DataTable dt, Control parentControl)
        {
            try
            {
                foreach (Control control in parentControl.Controls)
                {
                    //if (control is System.Windows.Forms.Label || control is CheckBox || control is Button || control is RadioButton || control is DataGridView)
                    //    ProcessControl(dt, control);

                    if (control is System.Windows.Forms.Label)
                    {
                        if (bolLabels == true)
                        {
                            ProcessControl(dt, control);
                        }
                    }

                    if (control is CheckBox)
                    {
                        if (bolCheckBox == true)
                        {
                            ProcessControl(dt, control);
                        }
                    }

                    if (control is Button)
                    {
                        if (bolButton == true)
                        {
                            ProcessControl(dt, control);

                        }
                    }

                    if (control is RadioButton)
                    {
                        if (bolRadioButton == true)
                        {
                            ProcessControl(dt, control);
                        }
                    }

                    if (control is DataGridView)
                    {
                        if (bolDataGridView == true)
                        {
                            ProcessControl(dt, control);
                        }
                    }



                    if (control.HasChildren)
                        GetControls(dt, control);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessControl(DataTable dt, Control control)
        {
            try
            {
                if (chkDGV.Checked == false)
                {
                    if (control is System.Windows.Forms.Label)
                    {
                        System.Windows.Forms.Label labelControl = (System.Windows.Forms.Label)control;
                        if (labelControl.Text != "" & labelControl.Text != "*" & labelControl.Text != ":")
                        {
                            dgvLabels.Rows.Add();
                            //dataGridView1.Rows[i].Cells["Column1"].Value = dataItem.Property1;
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_NAME].Value = labelControl.Name;
                            string strText = labelControl.Text;
                            if (strText.Contains("&"))
                                strText = strText.Replace("&", "");
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_WORD_ENGLISH].Value = strText;
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_TYPE].Value = "LBL";
                            getArabicText(dgvLabels.Rows.Count - 1, dt, labelControl);
                        }
                    }
                    else if (control is CheckBox)
                    {
                        CheckBox ChkBxControl = (CheckBox)control;
                        if (ChkBxControl.Text != "" & ChkBxControl.Text != "*")
                        {
                            dgvLabels.Rows.Add();
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_NAME].Value = ChkBxControl.Name;
                            string strText = ChkBxControl.Text;
                            if (strText.Contains("&"))
                                strText = strText.Replace("&", "");
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_WORD_ENGLISH].Value = strText;
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_TYPE].Value = "CHKBX";
                            getArabicText(dgvLabels.Rows.Count - 1, dt, ChkBxControl);
                        }
                    }
                    else if (control is Button)
                    {
                        Button BtnControl = (Button)control;
                        if (BtnControl.Text != "" & BtnControl.Text != "*")
                        {
                            dgvLabels.Rows.Add();
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_NAME].Value = BtnControl.Name;
                            string strText = BtnControl.Text;
                            if (strText.Contains("&"))
                                strText = strText.Replace("&", "");
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_WORD_ENGLISH].Value = strText;
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_TYPE].Value = "BTN";
                            getArabicText(dgvLabels.Rows.Count - 1, dt, BtnControl);
                        }
                    }
                    else if (control is RadioButton)
                    {
                        RadioButton RbtnControl = (RadioButton)control;
                        if (RbtnControl.Text != "" & RbtnControl.Text != "*" & RbtnControl.Text != ":")
                        {
                            dgvLabels.Rows.Add();
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_NAME].Value = RbtnControl.Name;
                            string strText = RbtnControl.Text;
                            if (strText.Contains("&"))
                                strText = strText.Replace("&", "");
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_WORD_ENGLISH].Value = strText;
                            dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_TYPE].Value = "RBTN";
                            getArabicText(dgvLabels.Rows.Count - 1, dt, RbtnControl);
                        }
                    }

                }
                else
                {
                    if (control is DataGridView)
                    {
                        DataGridView GridControl = (DataGridView)control;
                        if (GridControl.Columns.Count > 0)
                        {
                            for (int i = 0; i <= GridControl.Columns.Count - 1; i++)
                            {
                                if (GridControl.Columns[i].Visible == true)
                                {
                                    dgvLabels.Rows.Add();
                                    dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_NAME].Value = GridControl.Columns[i].Name;
                                    dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_WORD_ENGLISH].Value = GridControl.Columns[i].HeaderText;
                                    dgvLabels.Rows[dgvLabels.Rows.Count - 1].Cells[GRD_CONTROL_TYPE].Value = "GRD";
                                    getArabicText(dgvLabels.Rows.Count - 1, dt, GridControl);
                                }

                            }
                        }
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getArabicText(int intRwNum, DataTable dt, Control ctrl)
        {
            try
            {
                if (ctrl is DataGridView)
                {
                    DataGridView GridControl = (DataGridView)ctrl;
                    //for (int i = 0; i <= GridControl.Columns.Count - 1; i++)
                    //{
                    DataRow[] drFilter;
                    drFilter = dt.Select("controlName='" + dgvLabels.Rows[intRwNum].Cells[GRD_CONTROL_NAME].Value + "' AND controlType='GRD' AND FormId=" + cmbFormName.SelectedValue);
                    if (drFilter.Length > 0)
                    {
                        if (drFilter[0]["ARABIC"].ToString() != "")
                        {

                            byte[] arabicBytes = (byte[])drFilter[0]["ARABIC"];
                            string arabicText = Encoding.UTF8.GetString(arabicBytes);
                            dgvLabels.Rows[intRwNum].Cells[GRD_WORD_ARABIC].Value = arabicText;

                        }
                        else
                        {

                            dgvLabels.Rows[intRwNum].Cells[GRD_WORD_ARABIC].Value = "";
                        }
                    }
                    else
                        dgvLabels.Rows[intRwNum].Cells[GRD_WORD_ARABIC].Value = "";

                    //}

                }
                else
                {
                    DataRow[] drFilter;
                    drFilter = dt.Select("controlName='" + ctrl.Name + "' AND FormId=" + cmbFormName.SelectedValue);

                    if (drFilter.Length > 0)
                    {
                        if (drFilter[0]["ARABIC"].ToString() != "")
                        {

                            byte[] arabicBytes = (byte[])drFilter[0]["ARABIC"];
                            //System.Text.Encoding encoding = encoding.UTF8;
                            string arabicText = Encoding.UTF8.GetString(arabicBytes);
                            dgvLabels.Rows[intRwNum].Cells[GRD_WORD_ARABIC].Value = arabicText;

                            ctrl.Text = arabicText;

                        }
                        else
                        {

                            dgvLabels.Rows[intRwNum].Cells[GRD_WORD_ARABIC].Value = "";
                        }
                    }
                    else
                        dgvLabels.Rows[intRwNum].Cells[GRD_WORD_ARABIC].Value = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkDGV_CheckedChanged(object sender, EventArgs e)
        {
            FillControlsinGrid();
        }

    }
}
