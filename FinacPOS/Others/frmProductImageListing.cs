using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinacPOS
{
    partial class frmProductImageListing : Form
    {
        public frmProductImageListing()
        {
            InitializeComponent();
        }
        frmPOSSales frmPOSSales;
        frmPOSSales2 frmPOSSales2;
        DataTable dtblProductFiltered = new DataTable();
        DataTable dtblProductAll = new DataTable();
        string strGroupId = "0";
        public void CallFromSalesInvoice(frmPOSSales frm,DataTable dtbl)
        {
            this.frmPOSSales = frm;
            dtblProductAll = dtbl;
            dtblProductFiltered = dtbl;
            LoadProductGroup();
            LoadProducts(dtbl);
            base.Show();
            txtSearch.Focus(); 
        }
        public void CallFromSalesInvoice2(frmPOSSales2 frm, DataTable dtbl)
        {
            this.frmPOSSales2 = frm;
            dtblProductAll = dtbl;
            dtblProductFiltered = dtbl;
            LoadProductGroup();
            LoadProducts(dtbl);
            base.Show();
            txtSearch.Focus();
        }
        private void LoadProductGroup()
        {
            ProductSP SPProduct = new ProductSP();
            DataTable dtbl = new DataTable();
            dtbl = SPProduct.ProductGroupViewAllByCategory("Category 4");
            strGroupId = "0";
            PanelProductGroup.Controls.Clear();
            foreach (DataRow row in dtbl.Rows)
            {
                AddProductGroupButton(row);
            }
        }
        private void AddProductGroupButton(DataRow row)
        {
            Button btn = new Button
            {
                Width = 100,  // Adjust as needed
                Height = 70, // Adjust as needed
                Text = row["groupName"].ToString(),
                //TextAlign = ContentAlignment.BottomCenter,
                TextAlign = ContentAlignment.MiddleCenter,  // Keep text at the bottom
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.Black,
                Tag = row["groupId"] // Store product name or ID for reference
            };

            btn.Click += ProductGroupButton_Click;
            PanelProductGroup.Controls.Add(btn);
        }
        private void LoadProducts(DataTable dt)
        {
            PanelProducts.Controls.Clear();

            foreach (DataRow row in dt.Rows)
            {
                AddProductButton(row);
            }
            
        }
        private void AddProductButton(DataRow row)
        {
            //Button btn = new Button
            //{
            //    Width = 150,  // Adjust as needed
            //    Height = 150, // Adjust as needed
            //    Text = row["productName"].ToString() + Environment.NewLine + "" + row["salesPrice"].ToString(),
            //    //TextAlign = ContentAlignment.BottomCenter,
            //    TextAlign = ContentAlignment.BottomCenter,  // Keep text at the bottom
            //    ImageAlign = ContentAlignment.TopCenter, 
            //    Font = new Font("Arial", 10, FontStyle.Bold),
            //    BackColor = Color.White,
            //    ForeColor = Color.Red, 
            //    Tag = row["productCode"] // Store product name or ID for reference
            //};

            //// Load image if available
            //if (row["pic"] != DBNull.Value)
            //{
            //    byte[] imageBytes = (byte[])row["pic"];
            //    using (MemoryStream ms = new MemoryStream(imageBytes))
            //    {
            //        btn.Image = Image.FromStream(ms);
            //        btn.ImageAlign = ContentAlignment.TopCenter;
            //    }
            //}

            //btn.Click += ProductButton_Click;
            //flowLayoutPanel1.Controls.Add(btn);

            Panel panel = new Panel
            {
                Width = 150,
                Height = 180,
                BackColor = Color.White,
                Tag =row["barcode"]
              
            };

            // Add Image
            byte[] imageBytes = row["pic"] != DBNull.Value ? (byte[])row["pic"] : null; // (byte[])row["pic"];
            
            PictureBox pictureBox = new PictureBox
            {
                Width = 140,
                Height = 100,
                Image = imageBytes != null ? ByteArrayToImage(imageBytes) : null,  // ByteArrayToImage(imageBytes), 
                SizeMode = PictureBoxSizeMode.StretchImage,
                Top = 5,
                Left = 5,
                Tag = row["barcode"]
            };

            // Add Product Name and Price and unitname
            Label lbl = new Label
            {
                Text = row["productName"].ToString() + Environment.NewLine + row["salesPrice"].ToString() + Environment.NewLine + row["unitName"].ToString(),
                Width = 150,
                Height = 70,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Top = 110,
                Tag = row["barcode"]
            };

            // Add controls to panel
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(lbl);

            // Add panel to FlowLayoutPanel
            panel.Click += ProductButton_Click;
            pictureBox.Click += ProductImage_Click;
            lbl.Click += ProductLabel_Click;

            PanelProducts.Controls.Add(panel);
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        private void ProductGroupButton_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            Button clickedButton = sender as Button;
            string groupid = clickedButton.Tag.ToString();
           //MessageBox.Show("Selected Group:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            strGroupId = groupid;
            //MessageBox.Show("a");
            if (strGroupId == "All")
            {
                LoadProducts(dtblProductAll);
            }
            else
            {
                string searchvalue = "groupid='" + strGroupId + "'";

                DataView dv = dtblProductAll.DefaultView;
                //dv.RowFilter = "LedgerName LIKE '%" + searchvalue + "%' OR ledgerCode LIKE '%" + searchvalue + "%'";
                dv.RowFilter = searchvalue;

                dtblProductFiltered = dv.ToTable();
                LoadProducts(dtblProductFiltered);
            }
            //MessageBox.Show("b");
            txtSearch.Focus(); 
        }
        private void ProductButton_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            Panel clickedButton = sender as Panel;
            String strBarcode = clickedButton.Tag.ToString();
            //MessageBox.Show("Selected Product:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
          SelectProduct(strBarcode);
        }
        private void ProductImage_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            PictureBox clickedButton = sender as PictureBox;
            String strBarcode  = clickedButton.Tag.ToString();
     
            //MessageBox.Show("Selected Product:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SelectProduct(strBarcode);
        }
        private void ProductLabel_Click(object sender, EventArgs e)
        {
            //Button clickedButton = sender as Button;
            Label clickedButton = sender as Label;
            String strBarcode = clickedButton.Tag.ToString();
           
            //MessageBox.Show("Selected Product:" + productName, "Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SelectProduct(strBarcode);
        }
        public void setGridAllFilter(DataTable dt, string strSearchValue)
        {
            if (dt.Rows.Count > 0)
            {
                string searchvalue = CreateFilterQry(strSearchValue, dt);

                DataView dv = dt.DefaultView;
                //dv.RowFilter = "LedgerName LIKE '%" + searchvalue + "%' OR ledgerCode LIKE '%" + searchvalue + "%'";
                dv.RowFilter = searchvalue;

                dtblProductFiltered = dv.ToTable();
                LoadProducts(dtblProductFiltered);
            }
        }
        //private string CreateFilterQry(string strVal,DataTable dt)
        //{
        //    string strqry = "";
        //    if (!string.IsNullOrEmpty(strVal))
        //    {
        //        strVal = strVal.Replace("'", "''"); // Escape single quotes to prevent syntax errors
        //    }
        //    for (int i = 0; i < dt.Columns.Count; ++i)
        //    {
        //         foreach (DataColumn column in dt.Columns)
        //{
        //    // Only include columns of type string
        //    if (column.DataType == typeof(string))
        //    {
        //        if (strqry.Length > 0)
        //            strqry += " OR ";

        //        strqry += $"[{column.ColumnName}] LIKE '%{strVal}%'";
        //    }
        //}
        //        if (dt.Columns[i].ColumnName != "salesPrice" && dt.Columns[i].ColumnName != "productImage" && dt.Columns[i].ColumnName != "pic")
        //        {
        //            if (strqry == "")
        //            {
        //                strqry = dt.Columns[i].ColumnName + " LIKE '%" + strVal + "%' ";
        //            }
        //            else
        //            {
        //                strqry = strqry + " OR " + dt.Columns[i].ColumnName + " LIKE '%" + strVal + "%' ";
        //            }
        //        }


        //    }
        //    return strqry;

        //}
        private string CreateFilterQry(string strVal, DataTable dt)
        {
            string strqry = "";

            if (!string.IsNullOrEmpty(strVal))
            {
                strVal = strVal.Replace("'", "''"); // Escape single quotes

                foreach (DataColumn column in dt.Columns)
                {
                    // Only include string columns, and skip known binary/image columns
                    if (column.DataType == typeof(string) &&
                        column.ColumnName != "productImage" &&
                        column.ColumnName != "pic" &&
                        column.ColumnName != "picImage")
                    {
                        if (strqry.Length > 0)
                            strqry += " OR ";

                        strqry += $"[{column.ColumnName}] LIKE '%{strVal}%'";
                    }
                }
            }

            return strqry;
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            setGridAllFilter(dtblProductAll, txtSearch.Text.Trim());
        }
        string productCodeToReturn = "";
        string UnitName = "";
        private void SelectProduct(string strBarcode)
        {
            if (strBarcode != "")
            {
                productCodeToReturn = strBarcode;
             
            }
            this.Close();
        }

        private void frmProductImageListing_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CheckWhenQuiting();
            }
            catch (Exception ex)
            {
                MessageBox.Show("PPG2:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void CheckWhenQuiting()
        {
            if (frmPOSSales != null)
            {
                frmPOSSales.Enabled = true;
                frmPOSSales.Activate();
                frmPOSSales.FillrowAfterPickingProduct(productCodeToReturn);
            }
            if (frmPOSSales2 != null)
            {
                frmPOSSales2.Enabled = true;
                frmPOSSales2.Activate();
                frmPOSSales2.FillrowAfterPickingProduct(productCodeToReturn);
            }
            

        }
    }
}
