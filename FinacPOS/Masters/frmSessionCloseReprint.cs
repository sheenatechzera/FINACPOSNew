using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.DB;

namespace FinacPOS.Masters
{
    public partial class frmSessionCloseReprint : Form
    {
        public frmSessionCloseReprint()
        {
            InitializeComponent();
        }
        DateTime selectedDate;
        string selectedCounter = "";
        string SelectedSessionNo = "";
        public static bool IsSessionCloseReprint = false;
        SessionCloseReprintSP objSessioncloseReprintSp = new SessionCloseReprintSP();
        private void frmSessionCloseReprint_Load(object sender, EventArgs e)
        {
            LoadCounters();
        }
       
        public void LoadCounters()
        {
            DataTable dt = objSessioncloseReprintSp.GetPOSCounters();
            cmbPOSCounter.DataSource = dt;
            cmbPOSCounter.DisplayMember = "CounterName";
            cmbPOSCounter.ValueMember = "CounterId";
            cmbPOSCounter.SelectedIndex = -1;
        }
        public void LoadSessionId()
        {
            if (cmbPOSCounter.SelectedIndex == -1)
                return;

            selectedDate = dtpDate.Value.Date;

            selectedCounter = cmbPOSCounter.SelectedValue.ToString();
            DataTable dt = objSessioncloseReprintSp.GetSessionNoByDateAndCounter(selectedDate, selectedCounter);
            cmbSessionNo.DataSource = dt;
            cmbSessionNo.DisplayMember = "SessionNo";
            cmbSessionNo.ValueMember = "SessionNo";
            cmbSessionNo.SelectedIndex = -1;
        }
        private void cmbPOSCounter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadSessionId();
        }
       
        private void btnReprint_Click_1(object sender, EventArgs e)
        {
            IsSessionCloseReprint = true;
            frmSessionClosing objsessionClosing = new frmSessionClosing();
            SelectedSessionNo = cmbSessionNo.SelectedValue.ToString();
            objsessionClosing.loadSessionClosingData(SelectedSessionNo, selectedDate, selectedCounter, PublicVariables._currentUserId);
            objsessionClosing.ShowDialog();
            Clear();
        }
        public void Clear()
        {
            cmbSessionNo.SelectedIndex = -1;
            cmbPOSCounter.SelectedIndex = -1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
