namespace FinacPOS
{
    partial class frmProductImageListing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.PanelProductGroup = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // PanelProducts
            // 
            this.PanelProducts.AutoScroll = true;
            this.PanelProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelProducts.Location = new System.Drawing.Point(2, 134);
            this.PanelProducts.Name = "PanelProducts";
            this.PanelProducts.Size = new System.Drawing.Size(1019, 578);
            this.PanelProducts.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(2, 105);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1019, 32);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // PanelProductGroup
            // 
            this.PanelProductGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelProductGroup.Location = new System.Drawing.Point(2, 1);
            this.PanelProductGroup.Name = "PanelProductGroup";
            this.PanelProductGroup.Size = new System.Drawing.Size(1019, 104);
            this.PanelProductGroup.TabIndex = 2;
            // 
            // frmProductImageListing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 712);
            this.Controls.Add(this.PanelProductGroup);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.PanelProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProductImageListing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProductImageListing_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel PanelProducts;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel PanelProductGroup;
    }
}