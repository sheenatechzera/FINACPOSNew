using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace FinacPOS
{
    public class DBConnection
    {
        protected SqlConnection sqlcon = new SqlConnection();
        public DBConnection()
        {
            string strServer = ".\\sqlExpress";
            string strPrimaryDbName = "DB";
            string strSettings = "user id ='sa';password='a';";

            if (File.Exists(Application.StartupPath + "\\sys.txt"))
            {
                string DbData = File.ReadAllText(Application.StartupPath + "\\sys.txt"); ;
                string[] values = DbData.Split(',');

                strServer = values[0].Trim();
                strPrimaryDbName = values[1].Trim();

                //strServer = File.ReadAllText(Application.StartupPath + "\\sys.txt"); // getting ip of server
            }
            if (File.Exists(Application.StartupPath + "\\settings.txt"))
            {
                strSettings = File.ReadAllText(Application.StartupPath + "\\settings.txt"); // getting ip of server
            }

            sqlcon = new SqlConnection(@"Data Source=" + strServer + ";Initial Catalog=" + strPrimaryDbName + ";" + strSettings);
            
         
        }
          
    }
}
