using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinacPOS
{
    class FinacMessage
    {
        public static bool SaveUpdateMessage(string strType)
        {
            bool blReturnVal = false;
            if (PublicVariables._ModuleLanguage == "ARB")
            {
                if (strType == "Update")
                {
                    if (MessageBox.Show("هل تريد التحديث؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        blReturnVal = true;
                    }
                }
                else if (strType == "Save")
                {
                    if (MessageBox.Show("هل تريد حفظ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        blReturnVal = true;
                    }
                }
            }
            else
            { 
            
            }
            return blReturnVal;
        }

        public static bool SaveButtonText(Button btnControl,string strType)
        {
            bool blReturnVal = true;
            if (PublicVariables._ModuleLanguage == "ARB")
            {
                if (strType == "New")
                {
                    btnControl.Text = "يحفظ";
                }
                else if (strType == "Edit")
                {
                    btnControl.Text = "تحديث";
                }
            }
            else
            {
                if (strType == "New")
                {
                    btnControl.Text = "Save";
                }
                else if (strType == "Edit")
                {
                    btnControl.Text = "Update";
                }
            }
            return blReturnVal;
        }
        public static bool ClearButtonText(Button btnControl, string strType)
        {
            bool blReturnVal = true;
            if (PublicVariables._ModuleLanguage == "ARB")
            {
                if (strType == "New")
                {
                    btnControl.Text = "واضح";
                }
                else if (strType == "Edit")
                {
                    btnControl.Text = "جديد";
                }
            }
            else
            {
                if (strType == "New")
                {
                    btnControl.Text = "Clear";
                }
                else if (strType == "Edit")
                {
                    btnControl.Text = "New";
                }
            }
            return blReturnVal;
        }
        public static string ShowInputBox(string prompt, string title)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();

            form.Text = title;
            label.Text = prompt;

            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(309, 72, 75, 23);

            textBox.PasswordChar = Convert.ToChar("*"); 

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new System.Drawing.Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk });
            form.ClientSize = new System.Drawing.Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            DialogResult dialogResult = form.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                return textBox.Text;
            }
            return "";
        }
    }
}
