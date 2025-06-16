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
    public partial class frmCountersReport : Form
    {
        public frmCountersReport()
        {
            InitializeComponent();
            MdiParent = MDIFinacPOS.MDIObj;
        }

        frmPOSCounter ObjfrmPOScounter;

        public void CallFromPOSCounter(frmPOSCounter frmPOScounter)
        {
            try
            {
                base.Show();
                this.ObjfrmPOScounter = frmPOScounter;
                FillCounters();
            }
            catch (Exception ex)
            {

            }
        }
        public void FillCounters()
        {
            try
            {

                DataTable dtblCounter = new DataTable();
                POSCounterSP counterSp = new POSCounterSP();
                dtblCounter = counterSp.POSCounterViewAll();
                dgvCounter.DataSource = dtblCounter;

                dgvCounter.Columns[3].Visible = false;
                dgvCounter.Columns[4].Visible = false;
                dgvCounter.Columns[5].Visible = false;
                dgvCounter.Columns[6].Visible = false;
                dgvCounter.Columns[7].Visible = false;
                dgvCounter.Columns[8].Visible = false;
                dgvCounter.Columns[9].Visible = false;
                dgvCounter.Columns[10].Visible = false;
                dgvCounter.Columns[11].Visible = false;
                dgvCounter.Columns[12].Visible = false;
                dgvCounter.Columns[13].Visible = false;
                dgvCounter.Columns[14].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("AG2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("AG7:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvCounter_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ObjfrmPOScounter != null)
            {
                ObjfrmPOScounter.CallFromPOSCounterReport(this, dgvCounter.CurrentRow.Cells["counterId"].Value.ToString());
                ObjfrmPOScounter = null;
            }

        }

        private void frmCountersReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {

                    if (MessageBox.Show("Do you want to close?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PCR5:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmCountersReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ObjfrmPOScounter != null)
            {
                ObjfrmPOScounter.Enabled = true;
            }
        }
    }
}
