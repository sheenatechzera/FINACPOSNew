using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
    

namespace FinacPOS
{
    class AccountLedgerSP : DBConnection    
    {
        public AccountLedgerInfo AccountLedgerView(string ledgerId)
        {
            AccountLedgerInfo accountledgerinfo = new AccountLedgerInfo();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountLedgerView", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = ledgerId;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                SqlDataReader sdrreader = sccmd.ExecuteReader();
                while (sdrreader.Read())
                {
                    accountledgerinfo.LedgerId = sdrreader[0].ToString();
                    accountledgerinfo.LedgerName = sdrreader[1].ToString();
                    accountledgerinfo.GroupId = sdrreader[2].ToString();
                    accountledgerinfo.BillByBill = bool.Parse(sdrreader[3].ToString());
                    accountledgerinfo.OpeningBalance = decimal.Parse(sdrreader[4].ToString());
                    accountledgerinfo.CrOrDr = sdrreader[5].ToString();
                    accountledgerinfo.Narration = sdrreader[6].ToString();
                    accountledgerinfo.Name = sdrreader[7].ToString();
                    accountledgerinfo.AccountNo = sdrreader[8].ToString();
                    accountledgerinfo.Address = sdrreader[9].ToString();
                    accountledgerinfo.PhoneNo = sdrreader[10].ToString();
                    accountledgerinfo.FaxNo = sdrreader[11].ToString();
                    accountledgerinfo.Email = sdrreader[12].ToString();
                    accountledgerinfo.CreditPeriod = int.Parse(sdrreader[13].ToString());
                    accountledgerinfo.CreditLimit = decimal.Parse(sdrreader[14].ToString());
                    accountledgerinfo.PricingLevelId = sdrreader[15].ToString();
                    accountledgerinfo.CurrencyId = sdrreader[16].ToString();
                    accountledgerinfo.InterestOrNot = bool.Parse(sdrreader[17].ToString());
                    accountledgerinfo.BranchId = sdrreader[18].ToString();
                    accountledgerinfo.ExtraDate = DateTime.Parse(sdrreader[19].ToString());
                    accountledgerinfo.Extra1 = sdrreader[20].ToString();
                    accountledgerinfo.Extra2 = sdrreader[21].ToString();
                    accountledgerinfo.Default = bool.Parse(sdrreader[22].ToString());
                    accountledgerinfo.MarketId = sdrreader[23].ToString();
                    accountledgerinfo.TinNumber = sdrreader[24].ToString();
                    accountledgerinfo.CstNumber = sdrreader[25].ToString();
                    accountledgerinfo.PanNumber = sdrreader[26].ToString();
                    accountledgerinfo.AreaId = sdrreader[27].ToString();
                    accountledgerinfo.LedgerCode = sdrreader[28].ToString();

                    accountledgerinfo.BuildingNo = sdrreader["BuildingNo"].ToString();
                    accountledgerinfo.AdditionalNo = sdrreader["AdditionalNo"].ToString();
                    accountledgerinfo.StreetName = sdrreader["StreetName"].ToString();
                    accountledgerinfo.District = sdrreader["district"].ToString();
                    accountledgerinfo.PostboxNo = sdrreader["PostboxNo"].ToString();
                    accountledgerinfo.CityName = sdrreader["CityName"].ToString();
                    accountledgerinfo.Country = sdrreader["Country"].ToString();

                    accountledgerinfo.BuildingNoArb = sdrreader["BuildingNoArb"].ToString();
                    accountledgerinfo.AdditionalNoArb = sdrreader["AdditionalNoArb"].ToString();
                    accountledgerinfo.StreetNameArb = sdrreader["StreetNameArb"].ToString();
                    accountledgerinfo.DistrictArb = sdrreader["districtArb"].ToString();
                    accountledgerinfo.PostboxNoArb = sdrreader["PostboxNoArb"].ToString();
                    accountledgerinfo.CityNameArb = sdrreader["CityNameArb"].ToString();
                    accountledgerinfo.CountryArb = sdrreader["CountryArb"].ToString();

                    accountledgerinfo.CreditLimitStatus = sdrreader["creditLimitStatus"].ToString();

                    accountledgerinfo.bankname = sdrreader["bankname"].ToString();
                    accountledgerinfo.ibanno = sdrreader["ibanno"].ToString();
                    accountledgerinfo.bankaccname = sdrreader["bankaccname"].ToString();

                    accountledgerinfo.BankSwiftCode = sdrreader["bankSwiftCode"].ToString();
                    accountledgerinfo.BankBranchName = sdrreader["bankBranchName"].ToString();
                    accountledgerinfo.AddressArabic = sdrreader["AddressArabic"].ToString();
                }
                sdrreader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return accountledgerinfo;
        }
        public DataTable AccountLedgerGetWithBalanceByGroupId(string groupId, string strBranchId)
        {
            // to get all account having parent group passed 
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetWithBalanceByGroupId", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = strBranchId;
                prm = sdaadapter.SelectCommand.Parameters.Add("@groupId", SqlDbType.VarChar);
                prm.Value = groupId;
                sdaadapter.Fill(dtblAccountLedger);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblAccountLedger;
        }
        public DataTable AccountLedgerGetCashBankAndODWithBalance(string branchId)
        {
            // to get all acocutnledgers whose parent group is under bank or cash
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetCashBankAndODWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = branchId;
                sdaadapter.Fill(dtblAccountLedger);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtblAccountLedger;
        }
    }
}
