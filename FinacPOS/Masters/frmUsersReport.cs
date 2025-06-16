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
    public partial class frmUsersReport : Form
    {
        public frmUsersReport()
        {
            InitializeComponent();
            MdiParent = MDIFinacPOS.MDIObj;
        }

        frmPOSUser ObjfrmPOSUser;

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

        public void CallFromPOSUser(frmPOSUser frmPOSUser)
        {
            try
            {
                base.Show();
                this.ObjfrmPOSUser = frmPOSUser;
                FillUSers();
            }
            catch (Exception ex)
            {

            }
        }
        public void FillUSers()
        {
            try
            {

                DataTable dtblUser = new DataTable();
                POSUserSP userSp = new POSUserSP();
                dtblUser = userSp.POSUserViewAll();
                dgvUsers.DataSource = dtblUser;

                dgvUsers.Columns[1].Visible = false;
                dgvUsers.Columns[4].Visible = false;
                dgvUsers.Columns[6].Visible = false;
                dgvUsers.Columns[7].Visible = false;
                dgvUsers.Columns[8].Visible = false;
                dgvUsers.Columns[9].Visible = false;
                dgvUsers.Columns[10].Visible = false;
                dgvUsers.Columns[11].Visible = false;
                dgvUsers.Columns[12].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("AG2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ObjfrmPOSUser != null)
            {
                ObjfrmPOSUser.CallFromPOSUserReport(this, dgvUsers.CurrentRow.Cells["userId"].Value.ToString());
                ObjfrmPOSUser = null;
            }

        }

        private void frmUsers_KeyDown(object sender, KeyEventArgs e)
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
        private void frmUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ObjfrmPOSUser != null)
            {
                ObjfrmPOSUser.Enabled = true;
            }
        }




    }
}
