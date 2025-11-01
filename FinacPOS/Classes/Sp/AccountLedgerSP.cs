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
                    accountledgerinfo.routeId = sdrreader["routeId"].ToString();
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
        public DataTable AccountLedgerGetByLedgerCodeForZatcaSave(string ledgerCode)//
        {
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetByLedgerCodeForZatcaSave", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@ledgerCode", SqlDbType.VarChar);
                prm.Value = ledgerCode;
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
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
        public DataTable AccountLedgerGetVendorForTransaction()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetVendorForTransaction", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;

                sdaadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AccountLedgerGetAllWithBalance()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetAllWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sdaadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AccountLedgerViewllForPdc(string strBranchId)
        {
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerViewllForPdc", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = strBranchId;
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
        public DataTable AccountLedgerGetUnderExpenseWithBalance(string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetUnderExpenseWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sdaadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AccountLedgerGetByIncomeWithBalance(string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetByIncomeWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sdaadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AccountLedgerGetByExpenseWithBalance(string branchId)
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetByExpenseWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = branchId;
                sdaadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AccountGroupGetAllWithBalance()
        {
            DataTable dtbl = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountGroupGetAllWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = PublicVariables._branchId;
                sdaadapter.Fill(dtbl);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public DataTable AccountLedgerGetByDebtorAndCreditorWithBalance(string strBranchId)
        {
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetByDebtorAndCreditorWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = strBranchId;
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
        public DataTable AccountLedgerGetByIncomeAndExpenseWithBalance(string strBranchId)
        {
            DataTable dtblAccountLedger = new DataTable();
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgerGetByIncomeAndExpenseWithBalance", sqlcon);
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = strBranchId;
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
        public bool AccountLedgerCheckExistanceOfName(string strAccountLedgerName)
        {

            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountLedgerCheckExistanceOfName", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter prm = new SqlParameter();
                prm = sccmd.Parameters.Add("@ledgerName", SqlDbType.NVarChar);
                prm.Value = strAccountLedgerName;
                prm = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                prm.Value = PublicVariables._branchId;
                object obj = sccmd.ExecuteScalar();
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
        public string AccountLedgerAdd(AccountLedgerInfo accountledgerinfo)
        {
            string strLedgerId = "";
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountLedgerAdd", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.LedgerName;
                sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.GroupId;
                sprmparam = sccmd.Parameters.Add("@affectInventory", SqlDbType.Bit);
                sprmparam.Value = accountledgerinfo.BillByBill;
                sprmparam = sccmd.Parameters.Add("@openingBalance", SqlDbType.Decimal);
                sprmparam.Value = accountledgerinfo.OpeningBalance;
                sprmparam = sccmd.Parameters.Add("@crOrDr", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.CrOrDr;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Narration;
                sprmparam = sccmd.Parameters.Add("@name", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Name;
                sprmparam = sccmd.Parameters.Add("@accountNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AccountNo;
                sprmparam = sccmd.Parameters.Add("@address", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Address;
                sprmparam = sccmd.Parameters.Add("@phoneNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PhoneNo;
                sprmparam = sccmd.Parameters.Add("@faxNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.FaxNo;
                sprmparam = sccmd.Parameters.Add("@email", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Email;
                sprmparam = sccmd.Parameters.Add("@creditPeriod", SqlDbType.Int);
                sprmparam.Value = accountledgerinfo.CreditPeriod;
                sprmparam = sccmd.Parameters.Add("@creditLimit", SqlDbType.Decimal);
                sprmparam.Value = accountledgerinfo.CreditLimit;
                sprmparam = sccmd.Parameters.Add("@pricingLevelId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.PricingLevelId;
                sprmparam = sccmd.Parameters.Add("@currencyId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.CurrencyId;
                sprmparam = sccmd.Parameters.Add("@interestOrNot", SqlDbType.Bit);
                sprmparam.Value = accountledgerinfo.InterestOrNot;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@defaultt", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Default;
                sprmparam = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.MarketId;
                sprmparam = sccmd.Parameters.Add("@tinNumber", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.TinNumber;
                sprmparam = sccmd.Parameters.Add("@cstNumber", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CstNumber;
                sprmparam = sccmd.Parameters.Add("@panNumber", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PanNumber;
                sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.AreaId;
                sprmparam = sccmd.Parameters.Add("@ledgerCode", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.LedgerCode;

                sprmparam = sccmd.Parameters.Add("@BuildingNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BuildingNo;
                sprmparam = sccmd.Parameters.Add("@AdditionalNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AdditionalNo;
                sprmparam = sccmd.Parameters.Add("@StreetName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.StreetName;
                sprmparam = sccmd.Parameters.Add("@District", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.District;
                sprmparam = sccmd.Parameters.Add("@PostboxNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PostboxNo;
                sprmparam = sccmd.Parameters.Add("@CityName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CityName;
                sprmparam = sccmd.Parameters.Add("@Country", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Country;

                sprmparam = sccmd.Parameters.Add("@BuildingNoArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BuildingNoArb;
                sprmparam = sccmd.Parameters.Add("@AdditionalNoArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AdditionalNoArb;
                sprmparam = sccmd.Parameters.Add("@StreetNameArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.StreetNameArb;
                sprmparam = sccmd.Parameters.Add("@DistrictArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.DistrictArb;
                sprmparam = sccmd.Parameters.Add("@PostboxNoArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PostboxNoArb;
                sprmparam = sccmd.Parameters.Add("@CityNameArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CityNameArb;
                sprmparam = sccmd.Parameters.Add("@CountryArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CountryArb;


                sprmparam = sccmd.Parameters.Add("@creditLimitStatus", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CreditLimitStatus;

                sprmparam = sccmd.Parameters.Add("@bankname", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.bankname;
                sprmparam = sccmd.Parameters.Add("@ibanno", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.ibanno;
                sprmparam = sccmd.Parameters.Add("@bankaccname", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.bankaccname;

                sprmparam = sccmd.Parameters.Add("@bankBranchName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BankBranchName;
                sprmparam = sccmd.Parameters.Add("@bankSwiftCode", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BankSwiftCode;
                sprmparam = sccmd.Parameters.Add("@AddressArabic", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AddressArabic;
                sprmparam = sccmd.Parameters.Add("@ledgerType", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.LedgerType;
                sprmparam = sccmd.Parameters.Add("@routeId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.routeId;
                sprmparam = sccmd.Parameters.Add("@Userdef1", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef1;
                sprmparam = sccmd.Parameters.Add("@Userdef2", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef2;
                sprmparam = sccmd.Parameters.Add("@Userdef3", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef3;
                sprmparam = sccmd.Parameters.Add("@Userdef4", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef4;
                strLedgerId = sccmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return strLedgerId;
        }
        public void AccountLedgerEdit(AccountLedgerInfo accountledgerinfo)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountLedgerEdit", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.LedgerId;
                sprmparam = sccmd.Parameters.Add("@ledgerName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.LedgerName;
                sprmparam = sccmd.Parameters.Add("@groupId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.GroupId;
                sprmparam = sccmd.Parameters.Add("@affectInventory", SqlDbType.Bit);
                sprmparam.Value = accountledgerinfo.BillByBill;
                sprmparam = sccmd.Parameters.Add("@openingBalance", SqlDbType.Decimal);
                sprmparam.Value = accountledgerinfo.OpeningBalance;
                sprmparam = sccmd.Parameters.Add("@crOrDr", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.CrOrDr;
                sprmparam = sccmd.Parameters.Add("@narration", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Narration;
                sprmparam = sccmd.Parameters.Add("@name", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Name;
                sprmparam = sccmd.Parameters.Add("@accountNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AccountNo;
                sprmparam = sccmd.Parameters.Add("@address", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Address;
                sprmparam = sccmd.Parameters.Add("@phoneNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PhoneNo;
                sprmparam = sccmd.Parameters.Add("@faxNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.FaxNo;
                sprmparam = sccmd.Parameters.Add("@email", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Email;
                sprmparam = sccmd.Parameters.Add("@creditPeriod", SqlDbType.Int);
                sprmparam.Value = accountledgerinfo.CreditPeriod;
                sprmparam = sccmd.Parameters.Add("@creditLimit", SqlDbType.Decimal);
                sprmparam.Value = accountledgerinfo.CreditLimit;
                sprmparam = sccmd.Parameters.Add("@pricingLevelId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.PricingLevelId;
                sprmparam = sccmd.Parameters.Add("@currencyId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.CurrencyId;
                sprmparam = sccmd.Parameters.Add("@interestOrNot", SqlDbType.Bit);
                sprmparam.Value = accountledgerinfo.InterestOrNot;
                sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.BranchId;
                sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Extra1;
                sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Extra2;
                sprmparam = sccmd.Parameters.Add("@marketId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.MarketId;
                sprmparam = sccmd.Parameters.Add("@tinNumber", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.TinNumber;
                sprmparam = sccmd.Parameters.Add("@cstNumber", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CstNumber;
                sprmparam = sccmd.Parameters.Add("@panNumber", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PanNumber;
                sprmparam = sccmd.Parameters.Add("@areaId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.AreaId;
                sprmparam = sccmd.Parameters.Add("@ledgerCode", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.LedgerCode;

                sprmparam = sccmd.Parameters.Add("@BuildingNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BuildingNo;
                sprmparam = sccmd.Parameters.Add("@AdditionalNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AdditionalNo;
                sprmparam = sccmd.Parameters.Add("@StreetName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.StreetName;
                sprmparam = sccmd.Parameters.Add("@District", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.District;
                sprmparam = sccmd.Parameters.Add("@PostboxNo", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PostboxNo;
                sprmparam = sccmd.Parameters.Add("@CityName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CityName;
                sprmparam = sccmd.Parameters.Add("@Country", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Country;

                sprmparam = sccmd.Parameters.Add("@BuildingNoArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BuildingNoArb;
                sprmparam = sccmd.Parameters.Add("@AdditionalNoArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AdditionalNoArb;
                sprmparam = sccmd.Parameters.Add("@StreetNameArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.StreetNameArb;
                sprmparam = sccmd.Parameters.Add("@DistrictArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.DistrictArb;
                sprmparam = sccmd.Parameters.Add("@PostboxNoArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.PostboxNoArb;
                sprmparam = sccmd.Parameters.Add("@CityNameArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CityNameArb;
                sprmparam = sccmd.Parameters.Add("@CountryArb", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CountryArb;

                sprmparam = sccmd.Parameters.Add("@creditLimitStatus", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.CreditLimitStatus;

                sprmparam = sccmd.Parameters.Add("@bankname", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.bankname;
                sprmparam = sccmd.Parameters.Add("@ibanno", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.ibanno;
                sprmparam = sccmd.Parameters.Add("@bankaccname", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.bankaccname;

                sprmparam = sccmd.Parameters.Add("@bankBranchName", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BankBranchName;
                sprmparam = sccmd.Parameters.Add("@bankSwiftCode", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.BankSwiftCode;
                sprmparam = sccmd.Parameters.Add("@AddressArabic", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.AddressArabic;
                sprmparam = sccmd.Parameters.Add("@ledgerType", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.LedgerType;
                sprmparam = sccmd.Parameters.Add("@routeId", SqlDbType.VarChar);
                sprmparam.Value = accountledgerinfo.routeId;
                sprmparam = sccmd.Parameters.Add("@Userdef1", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef1;
                sprmparam = sccmd.Parameters.Add("@Userdef2", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef2;
                sprmparam = sccmd.Parameters.Add("@Userdef3", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef3;
                sprmparam = sccmd.Parameters.Add("@Userdef4", SqlDbType.NVarChar);
                sprmparam.Value = accountledgerinfo.Userdef4;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }

        }
        public void AccountLedgerDelete(string LedgerId)
        {
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountLedgerDelete", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = LedgerId;
                sccmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            finally
            {
                sqlcon.Close();
            }
        }
        public DataTable AccountLedgrGetGroupIdForLedgerCodeChecking()
        {
            DataTable dtbl = new DataTable();
            SqlDataAdapter sdaadapter = new SqlDataAdapter("AccountLedgrGetGroupIdForLedgerCodeChecking", sqlcon);
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                sdaadapter.Fill(dtbl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sqlcon.Close();
            }
            return dtbl;
        }
        public bool AccountLedgerExistenceOfLedgerCode(string ledgerCode, string ledgerId)
        {
            bool isExist = false;
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }
                SqlCommand sccmd = new SqlCommand("AccountLedgerExistenceOfLedgerCode", sqlcon);
                sccmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sprmparam = new SqlParameter();
                sprmparam = sccmd.Parameters.Add("@ledgerCode", SqlDbType.VarChar);
                sprmparam.Value = ledgerCode;
                sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
                sprmparam.Value = ledgerId;
                isExist = bool.Parse(sccmd.ExecuteScalar().ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                sqlcon.Close();
            }
            return isExist;
        }
    }
}
