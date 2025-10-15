using DevExpress.CodeParser;
using FinacPOS.Masters;
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
    public partial class MDIFinacPOS : Form
    {
        private int childFormNumber = 0;
        #region PUBLIC VARIABLES
        public static MDIFinacPOS MDIObj;
        public static string DBConnectiontype = "Single-User";   // for single userP 
        public static bool demoProject = true;
        public static bool isEstimateDB = false; // to indicate whether actual company or estimate company
        public static string strEstimateCompanyPath = "E:\\Varis Finac"; // to store path name of estimate company
        private string strcmbLastFontValue;
        #endregion

        public MDIFinacPOS()
        {
            InitializeComponent();
        }
        frmUserLogin frmuserloginobj = new frmUserLogin();
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void userCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPOSUser frmPOSuser = new frmPOSUser();
            frmPOSUser _isOpen = Application.OpenForms["frmPOSUser"] as frmPOSUser;
            if (_isOpen == null)
            {
                frmPOSuser.WindowState = FormWindowState.Normal;
                frmPOSuser.MdiParent = MDIObj;
                frmPOSuser.Show();
            }
            else
            {
                _isOpen.MdiParent = MDIObj;
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
        public void LoadEasyAccess()
        {
            try
            {
                FrmeasyAccess frm = new FrmeasyAccess();
                FrmeasyAccess open = Application.OpenForms["FrmeasyAccess"] as FrmeasyAccess;
                if (open == null)
                {
                    frm.MdiParent = this;
                    frm.Show();
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("MDI3:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void MDIFinacPOS_Load(object sender, EventArgs e)
        {
            //masterToolStripMenuItem.Enabled = false;
            //transactionsToolStripMenuItem.Enabled = false;
            //settingsToolStripMenuItem.Enabled = false; 
            MDIObj = this;

            ////// Check whether any default company exist
            ////PrimaryDBSP SpPrimary = new PrimaryDBSP();
            ////DataTable dtblCompanises = new DataTable();
            ////dtblCompanises = SpPrimary.CompanyPathViewAll();
            ////DataRow[] dr;
            ////if (dtblCompanises.Rows.Count > 0)
            ////{
              
            ////    if (dtblCompanises.Rows.Count == 1)
            ////    {
            ////        // Only one company exist
            ////        dr = dtblCompanises.Select();
            ////    }
            ////    else
            ////    {
            ////        dr = dtblCompanises.Select("defaultt" + " = '" + "True" + "'");
            ////    }
            ////    if (dr.Length > 0)
            ////    {
            ////        // Default company exist so need to load default company
            ////        PublicVariables._companyId = dr[0]["companyId"].ToString();

            ////        // Checkign whether the default company is branch enabled company
            ////        if (bool.Parse(dr[0]["branchEnabled"].ToString()) == true)
            ////        {
            ////            // Default company is branch enabled company so need to load select branch
            ////            BranchSP SpBranch = new BranchSP();
            ////            DataTable dtbl = new DataTable();
            ////            dtbl = SpBranch.BranchViewAll("");
            ////            if (dtbl.Rows.Count > 1)
            ////            {
            ////                //frmSelectBranch frmobj = new frmSelectBranch();
            ////                //frmSelectBranch _isOpen = Application.OpenForms["frmSelectBranch"] as frmSelectBranch;
            ////                //frmobj.WindowState = FormWindowState.Normal;
            ////                //frmobj.MdiParent = MDIObj;
            ////                //frmobj.Show();
            ////                //EnableSelectBranch();
            ////            }
            ////            else
            ////            {
            ////                // Default company is branch enabled but only one bracnh exist company so need to show login form 
            ////                PublicVariables._branchId = PublicVariables._companyId;
            ////                frmUserLogin frmobj = new frmUserLogin();
            ////                frmUserLogin _isOpen = Application.OpenForms["frmUserLogin"] as frmUserLogin;
            ////                frmobj.WindowState = FormWindowState.Normal;
            ////                frmobj.MdiParent = MDIObj;
            ////                frmobj.Show();
            ////            }

            ////        }
            ////        else
            ////        {
            ////            // Default company is branch disabled company so need to show login form 
            ////            PublicVariables._branchId = PublicVariables._companyId;
            ////            frmUserLogin frmobj = new frmUserLogin();
            ////            frmUserLogin _isOpen = Application.OpenForms["frmUserLogin"] as frmUserLogin;
            ////            frmobj.WindowState = FormWindowState.Normal;
            ////            frmobj.MdiParent = MDIObj;
            ////            frmobj.Show();
                      
            ////        }
            ////    }
            ////    else
            ////    {
            ////        //// Default company does not exist so need to select company
            ////        //frmSelectCompany frmobj = new frmSelectCompany();
            ////        //frmSelectCompany _isOpen = Application.OpenForms["frmSelectCompany"] as frmSelectCompany;
            ////        //frmobj.WindowState = FormWindowState.Normal;
            ////        //frmobj.MdiParent = MDIObj;
            ////        //frmobj.Show();

            ////    }
            ////}

            // get default branch
            BranchSP SpBranch = new BranchSP();
            DataTable dtblBranch = new DataTable();
            dtblBranch = SpBranch.BranchViewAll("");
            DataRow[] dr;
            if (dtblBranch.Rows.Count > 0)
            {
                if (dtblBranch.Rows.Count == 1)
                    {
                        // Only one company exist
                        dr = dtblBranch.Select();
                    }
                    else
                    {
                        dr = dtblBranch.Select("mainBranch" + " = '" + "True" + "'");
                    }
                    if (dr.Length > 0)
                    {
                        // Default company exist so need to load default company
                        PublicVariables._companyId = dr[0]["branchId"].ToString();


                        PublicVariables._branchId = PublicVariables._companyId;
                        frmUserLogin frmobj = new frmUserLogin();
                        frmUserLogin _isOpen = Application.OpenForms["frmUserLogin"] as frmUserLogin;
                        frmobj.WindowState = FormWindowState.Normal;
                        frmobj.MdiParent = MDIObj;
                        frmobj.Show();
                    }

                    
            }

           
        }
        public void EnableMenuItems()
        {
            //if (PublicVariables._userGroup == "Admin")
            //{
            //    menuStrip.Visible = true;
            //    masterToolStripMenuItem.Enabled = true;
            //    transactionsToolStripMenuItem.Enabled = true;
            //    settingsToolStripMenuItem.Enabled = true;
            //}
            //else
            //{
                //create menu from database
                CreateMenus();
             

            //POS Settings
            POSSettingsInfo InfoPOSSettings = new POSSettingsInfo();
            POSSettingsSP SpPOSSettings = new POSSettingsSP();
            InfoPOSSettings = SpPOSSettings.POSSettingsViewByBranchId(PublicVariables._branchId);


            //Finac Settings
            SettingsInfo InfoSettings = new SettingsInfo();
            SettingsSP SpSettings = new SettingsSP();
            InventorySettingsSP InVentorySp = new InventorySettingsSP();
            FinanceSettingsInfo InVentoryinfo = new FinanceSettingsInfo();
            InfoSettings = SpSettings.SettingsViewByBranchId(PublicVariables._branchId);
            InVentoryinfo = InVentorySp.FinanceSettingsViewAll(PublicVariables._branchId);

        }
        private void CreateMenus()
        {
            try
            {
               
                MenuStrip MainMenu1 = new MenuStrip();
                if (this.MainMenuStrip != null)
                {
                    MainMenu1 = this.MainMenuStrip;
                    MainMenu1.Items.Clear();
                }
                else
                {
                    MainMenu1 = new MenuStrip();
                    this.MainMenuStrip = MainMenu1;
                    Controls.Add(MainMenu1);
                }


                ToolStripMenuItem item = new ToolStripMenuItem();// 'Master
                ToolStripMenuItem innerItem = new ToolStripMenuItem();// 'User Creation
                ToolStripMenuItem innerItem1 = new ToolStripMenuItem();

                DataTable DtDisplay = new DataTable();
                POSUserSettingsSP spUserSettings = new POSUserSettingsSP();

                DtDisplay = spUserSettings.POSUserSettingsGetMenuByUSerGroup(PublicVariables._userGroup);
                if (DtDisplay.Rows.Count > 0)
                {
                    for (int iDtDisplay = 0; iDtDisplay < DtDisplay.Rows.Count; iDtDisplay++)
                    {
                        // item = new ToolStripMenuItem(String.Format(DtDisplay.Rows[iDtDisplay]["Menu"].ToString(), DtDisplay.Rows[iDtDisplay]["Menu"].ToString()));
                        string menuText = (PublicVariables._ModuleLanguage == "ARB")
                        ? DtDisplay.Rows[iDtDisplay]["MenuArabic"].ToString()
                        : DtDisplay.Rows[iDtDisplay]["menu"].ToString();

                        item = new ToolStripMenuItem(menuText);
                        item.Tag = DtDisplay.Rows[iDtDisplay]["menu"].ToString();
                        DataTable dtSubMenu = new DataTable();
                        dtSubMenu = spUserSettings.POSUserSettingsGetByMenuAndUserGroup(DtDisplay.Rows[iDtDisplay]["Menu"].ToString(), PublicVariables._userGroup);
                        for (int isubMenu = 0; isubMenu < dtSubMenu.Rows.Count; isubMenu++)
                        {
                            // Arabic or English submenus
                            string subMenuText = (PublicVariables._ModuleLanguage == "ARB")
                                ? dtSubMenu.Rows[isubMenu]["menuStripNameArabic"].ToString()
                                : dtSubMenu.Rows[isubMenu]["menuStripName"].ToString();


                            // innerItem = new ToolStripMenuItem(String.Format(dtSubMenu.Rows[isubMenu]["menuStripName"].ToString(), dtSubMenu.Rows[isubMenu]["menuStripName"].ToString()));
                            innerItem = new ToolStripMenuItem(subMenuText);
                            innerItem.Tag = dtSubMenu.Rows[isubMenu]["menuStripName"].ToString();
                            innerItem.Click += new EventHandler(MenuClick);
                            innerItem.BackColor = Color.FromArgb(0, 92, 187);
                            innerItem.ForeColor = Color.White;
                            item.DropDownItems.Add(innerItem);
                            //  innerItem = null;
                        }
                        item.BackColor = Color.FromArgb(0, 92, 187);
                        item.ForeColor = Color.White;
                        MainMenu1.Items.Add(item);
                        // item = null;
                    }
                }

                MainMenu1.Font = new Font("Century Gothic", 10, FontStyle.Bold);
                MainMenu1.BackColor = Color.FromArgb(0, 92, 187);
                MainMenu1.ForeColor = Color.White;
                this.MainMenuStrip = MainMenu1;
                Controls.Add(MainMenu1);

                if (PublicVariables._ModuleLanguage == "ARB") // added by Nishana 1-10-2025
                {
                    MainMenu1.RightToLeft = RightToLeft.Yes;

                    foreach (ToolStripMenuItem menuItem in MainMenu1.Items)
                    {
                        menuItem.RightToLeft = RightToLeft.Yes;

                        foreach (ToolStripMenuItem subItem in menuItem.DropDownItems)
                        {
                            subItem.RightToLeft = RightToLeft.Yes;
                        }
                    }


                }
                else
                {
                    MainMenu1.RightToLeft = RightToLeft.No;

                    foreach (ToolStripMenuItem menuItem in MainMenu1.Items)
                    {
                        menuItem.RightToLeft = RightToLeft.No;

                        foreach (ToolStripMenuItem subItem in menuItem.DropDownItems)
                        {
                            subItem.RightToLeft = RightToLeft.No;
                        }
                    }
                }

                //----------------------------------------------------------------------
                ComboBox comboBox = new ComboBox
                {
                    Width = 80,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };

                //comboBox.Items.AddRange(new string[] { "English", "Arabic" });


                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[]
                {
           new DataColumn("Id", typeof(string)),
           new DataColumn("Font", typeof(string))
                });
                dt.Rows.Add("ENG", "ENG");
                dt.Rows.Add("ARB", "ARB");

                comboBox.DataSource = dt;
                comboBox.DisplayMember = "Font";
                comboBox.ValueMember = "Id";

                ToolStripControlHost comboBoxHost = new ToolStripControlHost(comboBox)
                {
                    Alignment = ToolStripItemAlignment.Right
                };
                MainMenuStrip.Items.Add(comboBoxHost);
                if (!string.IsNullOrEmpty(PublicVariables._ModuleLanguage))
                {
                    comboBox.SelectedValue = PublicVariables._ModuleLanguage;
                }
                else
                {
                    comboBox.SelectedValue = "ENG";
                }

                strcmbLastFontValue = Convert.ToString(comboBox.SelectedValue);



                comboBox.SelectionChangeCommitted += (sender, e) =>
                {
                    if (PublicVariables.IsFormUserLoginOpen)
                        return;
                    bool isOpenForms = false;
                    if (Application.OpenForms.Count > 2)
                    {
                        // Two forms are already opened ,MDI, this form so need to ask message box only if any other forms opened
                        isOpenForms = true;
                    }
                    if (isOpenForms == true)
                    {
                        if (PublicVariables._ModuleLanguage == "ENG")
                        {
                            MessageBox.Show("Please close all opened windows before changing Language!");
                        }
                        else if (PublicVariables._ModuleLanguage == "ARB")
                        {
                            MessageBox.Show("يرجى إغلاق جميع النوافذ المفتوحة قبل تغيير اللغة!");
                        }

                        comboBox.SelectedValue = strcmbLastFontValue;
                    }
                    else
                    {
                        strcmbLastFontValue = Convert.ToString(comboBox.SelectedValue);
                        PublicVariables._ModuleLanguage = Convert.ToString(comboBox.SelectedValue);

                        CreateMenus();


                        foreach (Form frm in Application.OpenForms)
                        {
                            if (frm is FrmeasyAccess easyAccessForm)
                            {
                                easyAccessForm.AppyiconBasedOnLanaguage();
                            }
                        }
                    }

                };


                //comboBox.SelectionChangeCommitted += (sender, e) =>
                //    {
                //        string currentValue = "";

                //        if (Convert.ToString(comboBox.SelectedValue) == "")
                //        {
                //            //comboBox.SelectedValue = "ENG";
                //            currentValue = "ENG";
                //        }


                //        if (strcmbLastFontValue == null || strcmbLastFontValue != currentValue)
                //        {
                //            DialogResult result = MessageBox.Show("Changing the language will close all opened forms, Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo);

                //            if (result == DialogResult.No)
                //            {
                //                if (Convert.ToString(strcmbLastFontValue) == "")
                //                {
                //                    strcmbLastFontValue = "ENG";
                //                }
                //                comboBox.SelectedValue = strcmbLastFontValue;
                //            }
                //            else
                //            {
                //                strcmbLastFontValue = currentValue;
                //            }
                //        }
                //    };


            }
            catch { }

        }

        private void CloseAllFormsExceptMainForm()
        {
            // Close all forms except this one (MainForm)
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != this)
                {
                    form.Close();
                }
            }
        }

        private void counterCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPOSCounter frmPOSCounter = new frmPOSCounter();
            frmPOSCounter _isOpen = Application.OpenForms["frmPOSCounter"] as frmPOSCounter;
            if (_isOpen == null)
            {
                frmPOSCounter.WindowState = FormWindowState.Normal;
                frmPOSCounter.MdiParent = MDIObj;
                frmPOSCounter.Show();
            }
            else
            {
                _isOpen.MdiParent = MDIObj;
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

        public void openSalesReturn()
        {
            if (PublicVariables._counterId == "")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    MessageBox.Show("This System is not configured as Counter!");
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    MessageBox.Show("هذا النظام غير مهيأ ليعمل كـ كاونتر!");
                }

            }
            else
            {
                 SessionManagementSP sessionSp = new SessionManagementSP();
                DataTable dtbl = new DataTable();
                dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                if (dtbl.Rows.Count > 0)
                {
                    frmPOSSalesReturn frm = new frmPOSSalesReturn();
                    frmPOSSalesReturn _isOpen = Application.OpenForms["frmPOSSalesReturn"] as frmPOSSalesReturn;
                    if (_isOpen == null)
                    {
                        frm.WindowState = FormWindowState.Normal;
                        //frm.MdiParent = MDIObj;
                        frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                        frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                        frm.Show();
                    }
                    else
                    {
                        //_isOpen.MdiParent = MDIObj;
                        _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                        _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
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
                else
                {
                    if (PublicVariables._ModuleLanguage == "ENG")
                    {
                        MessageBox.Show("Session not opened! Please open session.");
                    }
                    else if (PublicVariables._ModuleLanguage == "ARB")
                    {
                        MessageBox.Show("الجلسة غير مفتوحة! الرجاء فتح الجلسة.");
                    }

                }
            }
        }
        public void openPOSReceipt()
        {
            if (PublicVariables._counterId == "")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    MessageBox.Show("This system is not configured as a Counter!");
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    MessageBox.Show("هذا النظام غير مهيأ ككاشير!");
                }

            }
            else
            {
                SessionManagementSP sessionSp = new SessionManagementSP();
                DataTable dtbl = new DataTable();
                dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                if (dtbl.Rows.Count > 0)
                {
                    frmPOSReceiptVoucher frm = new frmPOSReceiptVoucher();
                    frmPOSReceiptVoucher _isOpen = Application.OpenForms["frmPOSReceiptVoucher"] as frmPOSReceiptVoucher;
                    if (_isOpen == null)
                    {
                        frm.WindowState = FormWindowState.Normal;
                        frm.MdiParent = MDIObj;
                        frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                        frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                        frm.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
                        _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                        _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
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
                else
                {
                    MessageBox.Show("Session not Opened! Please Open session");
                }
            }
        }
        public void openPOSPayment()
        {
            if (PublicVariables._counterId == "")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    MessageBox.Show("This system is not configured as Counter!");
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    MessageBox.Show("هذا النظام غير مُهيأ كـ كاونتر!");
                }

            }
            else
            {
                SessionManagementSP sessionSp = new SessionManagementSP();
                DataTable dtbl = new DataTable();
                dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                if (dtbl.Rows.Count > 0)
                {
                    frmPOSPaymentVoucher frm = new frmPOSPaymentVoucher();
                    frmPOSPaymentVoucher _isOpen = Application.OpenForms["frmPOSPaymentVoucher"] as frmPOSPaymentVoucher;
                    if (_isOpen == null)
                    {
                        frm.WindowState = FormWindowState.Normal;
                        frm.MdiParent = MDIObj;
                        frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                        frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                        frm.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
                        _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                        _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
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
                else
                {
                    if (PublicVariables._ModuleLanguage == "ENG")
                    {
                        MessageBox.Show("Session not opened! Please open session.");
                    }
                    else if (PublicVariables._ModuleLanguage == "ARB")
                    {
                        MessageBox.Show("الجلسة غير مفتوحة! الرجاء فتح الجلسة.");
                    }

                }
            }
        }
        public void openSalesInvoice()
        {
            if (PublicVariables._counterId == "")
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    MessageBox.Show("This system is not configured as Counter!");
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    MessageBox.Show("هذا النظام غير مُهيأ كـ كاونتر!");
                }

            }
            else
            {
                SessionManagementSP sessionSp = new SessionManagementSP();
                DataTable dtbl = new DataTable();
                dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

                if (dtbl.Rows.Count > 0)
                {
                    if (PublicVariables._SalesScreenType == "Type1")
                    {
                        frmPOSSales frm = new frmPOSSales();
                        frmPOSSales _isOpen = Application.OpenForms["frmPOSSales"] as frmPOSSales;
                        if (_isOpen == null)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            //frm.MdiParent = MDIObj;
                            frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                            frm.Show();
                        }
                        else
                        {
                            //_isOpen.MdiParent = MDIObj;
                            _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
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
                    else if (PublicVariables._SalesScreenType == "Type2") //Added on 01/Apr/2025 Varis
                    {
                        frmPOSSales2 frm = new frmPOSSales2();
                        frmPOSSales2 _isOpen = Application.OpenForms["frmPOSSales2"] as frmPOSSales2;
                        if (_isOpen == null)
                        {
                            frm.WindowState = FormWindowState.Normal;
                            //frm.MdiParent = MDIObj;
                            frm.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            frm.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");

                            frm.Show();
                        }
                        else
                        {
                            //_isOpen.MdiParent = MDIObj;
                            _isOpen.strSessionNo = dtbl.Rows[0]["sessionNo"].ToString();
                            _isOpen.strSessionDate = Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()).ToString("dd-MMM-yyyy");
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
                    
                    
                }
                else
                {
                    string strOpenedUserId = sessionSp.CheckAnySessionIsOpenedInCounter(PublicVariables._counterId);
                    if (strOpenedUserId != "")
                    {
                        if (PublicVariables._ModuleLanguage == "ENG")
                        {
                            MessageBox.Show(strOpenedUserId + " session is kept open, please close it first.");
                        }
                        else if (PublicVariables._ModuleLanguage == "ARB")
                        {
                            MessageBox.Show("جلسة " + strOpenedUserId + " ما زالت مفتوحة، يرجى إغلاقها أولاً.");
                        }

                        Application.Exit(); 
                    }

                    frmSessionManagement frmsessionManagement = new frmSessionManagement();
                    frmSessionManagement _isOpen = Application.OpenForms["frmSessionManagement"] as frmSessionManagement;
                    if (_isOpen == null)
                    {
                        frmsessionManagement.WindowState = FormWindowState.Normal;
                        frmsessionManagement.MdiParent = MDIObj;
                        frmsessionManagement.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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


            }
        }
      
       
        public void sessionClose()
        {
            //Check Sales and Return screen is opened
            frmPOSSales _isOpen = Application.OpenForms["frmPOSSales"] as frmPOSSales;
            if (_isOpen != null)
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    MessageBox.Show("Please close Sales Screen!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    MessageBox.Show("يرجى إغلاق شاشة المبيعات!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;

            }
            frmPOSSalesReturn _isOpen1 = Application.OpenForms["frmPOSSalesReturn"] as frmPOSSalesReturn;
            if (_isOpen1 != null)
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    MessageBox.Show("Please close Sales Return Screen!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (PublicVariables._ModuleLanguage == "ARB")
                {
                    MessageBox.Show("يرجى إغلاق شاشة مرتجع المبيعات!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;

            }
            //

            SessionManagementSP sessionSp = new SessionManagementSP();
            DataTable dtbl = new DataTable();
            dtbl = sessionSp.GetActiveSession(PublicVariables._currentUserId, PublicVariables._counterId);

            if (dtbl.Rows.Count > 0)
            {
                if (MessageBox.Show("This is SESSION CLOSING Option. Do you want to continue?", "Session Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    frmSessionClosing frmObj = new frmSessionClosing();
                    frmObj.DoWhenComingFromPOSMDI(dtbl.Rows[0]["sessionNo"].ToString(), Convert.ToDateTime(dtbl.Rows[0]["sessionDate"].ToString()));

                    //SessionManagementSP SPSessionManagement = new SessionManagementSP();
                    //SPSessionManagement.UpdateSessionClose(PublicVariables._counterId);
                    //MessageBox.Show("Session Closed", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                if (PublicVariables._ModuleLanguage == "ENG")
                {
                    MessageBox.Show("Session is Already Closed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // ARB
                {
                    MessageBox.Show("تم إغلاق الجلسة مسبقاً!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        private void salesInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSalesInvoice();
            
        }

        private void sessionCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sessionClose();
        }

        private void salesReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSalesReturn();
        }

        private void pOSSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPOSSettings frmObj = new frmPOSSettings();
            frmPOSSettings _isOpen = Application.OpenForms["frmPOSSettings"] as frmPOSSettings;
            if (_isOpen == null)
            {
                frmObj.WindowState = FormWindowState.Normal;
                frmObj.MdiParent = MDIObj;
                frmObj.Show();
            }
            else
            {
                _isOpen.MdiParent = MDIObj;
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

        private void pOSUserSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPOSUserSettings frmObj = new frmPOSUserSettings();
            frmPOSUserSettings _isOpen = Application.OpenForms["frmPOSUserSettings"] as frmPOSUserSettings;
            if (_isOpen == null)
            {
                frmObj.WindowState = FormWindowState.Normal;
                frmObj.MdiParent = MDIObj;
                frmObj.Show();
            }
            else
            {
                _isOpen.MdiParent = MDIObj;
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
        private void MenuClick(object sender , EventArgs e)
        {
            // var menuItem = sender as MenuItem;
            ////var menuText = menuItem.Text;
            // MessageBox.Show("You selected the command - "+sender+" - of the File menu");
            var menuItem = sender as ToolStripMenuItem;
            string strowner = "";
            //menuItem.ForeColor = Color.Black;   
            try
            {
                var varownerItem = menuItem.OwnerItem.OwnerItem.OwnerItem;//get main owner of level3
                if (varownerItem == null)
                {
                    varownerItem = menuItem.OwnerItem.OwnerItem;//get main owner of level2
                }
                strowner = varownerItem.Tag.ToString();
            }
            catch
            {
                try
                {
                    strowner = menuItem.OwnerItem.Tag.ToString();//get main owner of level1
                }
                catch
                {
                    strowner = "";//main menu
                }
            }
            //MessageBox.Show("You selected the command - "+sender+" - of the File menu");
            
            menuItem.Tag.ToString();
            LoadingFormByLabel(menuItem.Tag.ToString());
        }
        public void LoadingFormByLabel(string NameFrm)
        {
            try
            {
               
                if (NameFrm == "User Creation") {
                    frmPOSUser frmPOSuser = new frmPOSUser();
                    frmPOSUser _isOpen = Application.OpenForms["frmPOSUser"] as frmPOSUser;
                    if (_isOpen == null)
                    {
                        frmPOSuser.WindowState = FormWindowState.Normal;
                        frmPOSuser.MdiParent = MDIObj;
                        frmPOSuser.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "Counter creation")
                {
                    frmPOSCounter frmPOSCounter = new frmPOSCounter();
                    frmPOSCounter _isOpen = Application.OpenForms["frmPOSCounter"] as frmPOSCounter;
                    if (_isOpen == null)
                    {
                        frmPOSCounter.WindowState = FormWindowState.Normal;
                        frmPOSCounter.MdiParent = MDIObj;
                        frmPOSCounter.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "POS Table")
                {
                    FrmPOSTable frmObj = new FrmPOSTable();
                    FrmPOSTable _isOpen = Application.OpenForms["FrmPOSTable"] as FrmPOSTable;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Normal;
                        frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "Session Control")
                {
                    frmSessionControl frmObj = new frmSessionControl();
                    frmSessionControl _isOpen = Application.OpenForms["frmSessionControl"] as frmSessionControl;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Normal;
                        frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "Session Close Reprint")
                {
                    frmSessionCloseReprint frmObj = new frmSessionCloseReprint();
                    frmSessionCloseReprint _isOpen = Application.OpenForms["frmSessionCloseReprint"] as frmSessionCloseReprint;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Normal;
                        frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "Session Close")
                {
                    sessionClose();
                }
                else if (NameFrm == "Sales Invoice")
                {
                    openSalesInvoice();
                }
                else if (NameFrm == "Sales Return")
                {
                    openSalesReturn();
                }
               
               
                else if (NameFrm == "POS Receipt")
                {
                    openPOSReceipt();
                }
                else if (NameFrm == "POS Payment")
                {
                    openPOSPayment();
                }
                else if (NameFrm == "POS Settings")
                {
                    frmPOSSettings frmObj = new frmPOSSettings();
                    frmPOSSettings _isOpen = Application.OpenForms["frmPOSSettings"] as frmPOSSettings;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Normal;
                        frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "POS User Settings")
                {
                    frmPOSUserSettings frmObj = new frmPOSUserSettings();
                    frmPOSUserSettings _isOpen = Application.OpenForms["frmPOSUserSettings"] as frmPOSUserSettings;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Normal;
                        frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "POS Control Settings")
                {
                    frmPOSControlSettings frmObj = new frmPOSControlSettings();
                    frmPOSControlSettings _isOpen = Application.OpenForms["frmPOSControlSettings"] as frmPOSControlSettings;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Normal;
                        frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
                else if (NameFrm == "Print Designer")
                {
                    frmPrintDesigner frmObj = new frmPrintDesigner();
                    frmPrintDesigner _isOpen = Application.OpenForms["frmPrintDesigner"] as frmPrintDesigner;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Maximized;
                        //frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        //_isOpen.MdiParent = MDIObj;
                        if (_isOpen.WindowState == FormWindowState.Minimized)
                        {
                            _isOpen.WindowState = FormWindowState.Maximized;
                        }
                        if (_isOpen.Enabled)
                        {
                            _isOpen.Activate();
                            _isOpen.BringToFront();
                        }

                    }
                }
                else if (NameFrm == "Bill Duplicate")
                {
                    frmBillPrint frmObj = new frmBillPrint();
                    frmBillPrint _isOpen = Application.OpenForms["frmBillPrint"] as frmBillPrint;
                    if (_isOpen == null)
                    {
                        frmObj.WindowState = FormWindowState.Normal;
                        frmObj.MdiParent = MDIObj;
                        frmObj.Show();
                    }
                    else
                    {
                        _isOpen.MdiParent = MDIObj;
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
              
            }
            catch { }
        }

        
    }
}
