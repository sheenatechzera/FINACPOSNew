using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FinacPOS
{
    partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }
        public void ShowFromSendMail()
        {
          
            ShowDialog();
        }
        public void ShowFromReport()
        {
           
            ShowDialog();
        }
    }
}