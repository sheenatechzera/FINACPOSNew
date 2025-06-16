using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace FinacPOS
{
    class DBConnectionForPDB
    {
        
        
        protected static SqlConnection sqlcon = new SqlConnection();
    
        public DBConnectionForPDB()
        {
            string strServer = ".\\sqlExpress";
            if (File.Exists(Application.StartupPath + "\\sys.txt"))
            {
                strServer = File.ReadAllText(Application.StartupPath + "\\sys.txt"); // getting ip of server
            }
            string strSettings = "user id ='sa';password='cool';";
            if (File.Exists(Application.StartupPath + "\\settings.txt"))
            {
                strSettings = File.ReadAllText(Application.StartupPath + "\\settings.txt"); // getting ip of server
            }
            if (MDIFinacPOS.DBConnectiontype == "Server")
            {
                sqlcon = new SqlConnection(@"Data Source=192.168.2.103;database=DBFinacAccount;user id='account';password='suit';Connect Timeout=12000000;");
            }
            else if (!MDIFinacPOS.demoProject)
            {
                #region ACTUAL

                string path = "";
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
                if (MDIFinacPOS.DBConnectiontype == "Single-User")
                {
                    if (MDIFinacPOS.isEstimateDB && MDIFinacPOS.strEstimateCompanyPath != "")
                    {
                        // checking whether working in estimate company
                        path = MDIFinacPOS.strEstimateCompanyPath + "\\Data\\DBFinacAccount.mdf";
                    }
                    else
                    {
                        // working in actual company
                        path = Application.StartupPath + "\\Data\\DBFinacAccount.mdf";

                    }
                    //sqlcon = new SqlConnection(@"Data Source=" + strServer + ";AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=120;User Instance=True");
                    sqlcon = new SqlConnection(@"Data Source=" + strServer + ";AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=12000000");
                }
                else
                {
                    if (MDIFinacPOS.isEstimateDB && MDIFinacPOS.strEstimateCompanyPath != "")
                    {
                        // checking whether working in estimate company
                        path = MDIFinacPOS.strEstimateCompanyPath + "\\Data\\DBFinacAccount.mdf";
                    }
                    else
                    {
                        // working in actual company
                        path = Application.StartupPath + "\\Data\\DBFinacAccount.mdf";
                    }
                    sqlcon = new SqlConnection(@"Data Source=" + strServer + ";AttachDbFilename=" + path + ";" + strSettings);
                }
                #endregion
            }
            else
            {
                string path = Application.StartupPath + "\\Data\\DBFinacAccount.mdf";
                if (MDIFinacPOS.DBConnectiontype == "Single-User")
                {
                   // sqlcon = new SqlConnection(@"Data Source=" + strServer + ";AttachDbFilename=" + path + ";Integrated Security=True;Connect Timeout=120;User Instance=True");
                    sqlcon = new SqlConnection(@"Data Source=" + Environment.MachineName + "\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=DBFINACACCOUNT");
                }
                else
                {
                    sqlcon = new SqlConnection(@"Data Source=" + strServer + ";AttachDbFilename=" + path + ";" + strSettings);
                }
            }
            ////---------------------------------------------------------------------------------------------------------------------------------

        }
    }
}
