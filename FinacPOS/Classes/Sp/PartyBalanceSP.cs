using System;    
using System.Collections.Generic;    
using System.Text;    
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
    
//<summary>    
//Summary description for PartyBalanceSP    
//</summary>    
namespace FinacPOS    
{    
class PartyBalanceSP:DBConnection    
{    
    public void PartyBalanceAdd(PartyBalanceInfo partybalanceinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceAdd", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
            sprmparam.Value = partybalanceinfo.Date;
            sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.LedgerId;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.VoucherType;
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.VoucherNo;
            sprmparam = sccmd.Parameters.Add("@againstVoucherType", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.AgainstVoucherType;
            sprmparam = sccmd.Parameters.Add("@againstvoucherNo", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.AgainstvoucherNo;
            sprmparam = sccmd.Parameters.Add("@referenceType", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.ReferenceType;
            sprmparam = sccmd.Parameters.Add("@debit", SqlDbType.Decimal);
            sprmparam.Value = partybalanceinfo.Debit;
            sprmparam = sccmd.Parameters.Add("@credit", SqlDbType.Decimal);
            sprmparam.Value = partybalanceinfo.Credit;
            sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = partybalanceinfo.Optional;
            sprmparam = sccmd.Parameters.Add("@creditPeriod", SqlDbType.Int);
            sprmparam.Value = partybalanceinfo.CreditPeriod;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@currecyConversionId", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.CurrencyConversionId;
            sprmparam = sccmd.Parameters.Add("@invoiceNo", SqlDbType.VarChar);
           sprmparam.Value = partybalanceinfo.invoiceNo;
                sprmparam = sccmd.Parameters.Add("@referenceNo", SqlDbType.VarChar);
                sprmparam.Value = partybalanceinfo.referenceNo;
                sprmparam = sccmd.Parameters.Add("@BillAmount ", SqlDbType.Decimal);
                sprmparam.Value = partybalanceinfo.BillAmount;
                sprmparam = sccmd.Parameters.Add("@invoiceDate ", SqlDbType.DateTime);
                sprmparam.Value = partybalanceinfo.invoiceDate;
                sprmparam = sccmd.Parameters.Add("@costCentreId", SqlDbType.VarChar);
                sprmparam.Value = partybalanceinfo.costCentreId;
                sprmparam = sccmd.Parameters.Add("@exchangeRate", SqlDbType.Decimal);
                sprmparam.Value = partybalanceinfo.exchangeRate;
                sprmparam = sccmd.Parameters.Add("@exchangeDate ", SqlDbType.DateTime);
                sprmparam.Value = partybalanceinfo.exchangeDate;
                sccmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
    }
    public void PartyBalanceEdit(PartyBalanceInfo partybalanceinfo)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceEdit", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@balanceId", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.BalanceId;
            sprmparam = sccmd.Parameters.Add("@date", SqlDbType.DateTime);
            sprmparam.Value = partybalanceinfo.Date;
            sprmparam = sccmd.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.LedgerId;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.VoucherType;
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.VoucherNo;
            sprmparam = sccmd.Parameters.Add("@againstVoucherType", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.AgainstVoucherType;
            sprmparam = sccmd.Parameters.Add("@againstvoucherNo", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.AgainstvoucherNo;
            sprmparam = sccmd.Parameters.Add("@referenceType", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.ReferenceType;
            sprmparam = sccmd.Parameters.Add("@debit", SqlDbType.Decimal);
            sprmparam.Value = partybalanceinfo.Debit;
            sprmparam = sccmd.Parameters.Add("@credit", SqlDbType.Decimal);
            sprmparam.Value = partybalanceinfo.Credit;
            sprmparam = sccmd.Parameters.Add("@optional", SqlDbType.Bit);
            sprmparam.Value = partybalanceinfo.Optional;
            sprmparam = sccmd.Parameters.Add("@creditPeriod", SqlDbType.Int);
            sprmparam.Value = partybalanceinfo.CreditPeriod;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.BranchId;
            sprmparam = sccmd.Parameters.Add("@extra1", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.Extra1;
            sprmparam = sccmd.Parameters.Add("@extra2", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.Extra2;
            sprmparam = sccmd.Parameters.Add("@currencyConversionId", SqlDbType.VarChar);
            sprmparam.Value = partybalanceinfo.CurrencyConversionId;
            sccmd.ExecuteNonQuery();        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }

    }
    public DataTable PartyBalanceViewAll()
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("PartyBalanceViewAll", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            sdaadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public PartyBalanceInfo PartyBalanceView(string balanceId )
    {
        PartyBalanceInfo partybalanceinfo =new PartyBalanceInfo();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceView", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@balanceId", SqlDbType.VarChar);
            sprmparam.Value = balanceId;
            SqlDataReader sdrreader = sccmd.ExecuteReader();
            while (sdrreader.Read())
            {
                partybalanceinfo.BalanceId= sdrreader[0].ToString();
                partybalanceinfo.Date=DateTime.Parse(sdrreader[1].ToString());
                partybalanceinfo.LedgerId= sdrreader[2].ToString();
                partybalanceinfo.VoucherType= sdrreader[3].ToString();
                partybalanceinfo.VoucherNo= sdrreader[4].ToString();
                partybalanceinfo.AgainstVoucherType= sdrreader[5].ToString();
                partybalanceinfo.AgainstvoucherNo= sdrreader[6].ToString();
                partybalanceinfo.ReferenceType= sdrreader[7].ToString();
                partybalanceinfo.Debit=decimal.Parse(sdrreader[8].ToString());
                partybalanceinfo.Credit=decimal.Parse(sdrreader[9].ToString());
                partybalanceinfo.Optional=bool.Parse(sdrreader[10].ToString());
                partybalanceinfo.CreditPeriod=int.Parse(sdrreader[11].ToString());
                partybalanceinfo.BranchId= sdrreader[12].ToString();
                partybalanceinfo.ExtraDate=DateTime.Parse(sdrreader[13].ToString());
                partybalanceinfo.Extra1= sdrreader[14].ToString();
                partybalanceinfo.Extra2= sdrreader[15].ToString();
                partybalanceinfo.CurrencyConversionId = sdrreader[16].ToString();
            }
            sdrreader.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return partybalanceinfo;
    }
    public void PartyBalanceDelete(string BalanceId)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceDelete", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@balanceId", SqlDbType.VarChar);
            sprmparam.Value = BalanceId;
            sccmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
    }
    public int PartyBalanceGetMax()
    {
        int max = 0;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceMax", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            max = int.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
        return max;
    }
    public DataTable PartyBalanceViewByLedgerId(string strLedgerId, string strCrOrDr)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("PartyBalanceViewByLedgerId", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            prm.Value = strLedgerId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@crOrDr", SqlDbType.VarChar);
            prm.Value = strCrOrDr;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value =  PublicVariables._branchId;
            sdaadapter.Fill(dtbl);

        }
        catch (Exception ex)
        {
            MessageBox.Show( ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public void PartyBalanceDeleteByVoucherTypeVoucherNoAndReferenceType(string vocuherNumber, string voucherType)//, string referenceType)
    {
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceDeleteByVoucherTypeVoucherNoAndReferenceType", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = vocuherNumber;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = voucherType;
            sccmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        finally
        {
            sqlcon.Close();
        }
    }
    public DataTable PartyBalanceByVoucherTypeAndVoucherNo(string strVoucherType, string strVoucherNo)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("PartyBalanceByVoucherTypeAndVoucherNo", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
            prm.Value = strVoucherType;
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            prm.Value = strVoucherNo;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            sdaadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public bool PartyBalanceReferenceCheck(string strPaymentMasterId, string strVoucherType)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceReferenceCheck", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@paymentMasterId", SqlDbType.VarChar);
            sprmparam.Value = strPaymentMasterId;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = strVoucherType;
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
    public bool PartyBalanceAgainstReferenceCheck(string strVoucherNo, string strVoucherType)
    {
        bool isExist = false;
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceAgainstReferenceCheck", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            sprmparam.Value = strVoucherNo;
            sprmparam = sccmd.Parameters.Add("@voucherType", SqlDbType.VarChar);
            sprmparam.Value = strVoucherType;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;
            isExist = bool.Parse(sccmd.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return isExist;
    }
    public DataTable ForexGainAndLossCalculation(string strVoucherType, string strVoucherNo, decimal dcAmount)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("ForexGainAndLossCalculation", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
            prm.Value = strVoucherType;
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            prm.Value = strVoucherNo;
            prm = sdaadapter.SelectCommand.Parameters.Add("@amount", SqlDbType.Decimal);
            prm.Value = dcAmount;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            sdaadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public string PartyBalanceVaoucherTypeViewByBalanceId(string strBalanceId)
    {
        string strType = "";
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlCommand sccmd = new SqlCommand("PartyBalanceVaoucherTypeViewByBalanceId", sqlcon);
            sccmd.CommandType = CommandType.StoredProcedure;
            SqlParameter sprmparam = new SqlParameter();
            sprmparam = sccmd.Parameters.Add("@balanceId", SqlDbType.VarChar);
            sprmparam.Value = strBalanceId;
            sprmparam = sccmd.Parameters.Add("@branchId", SqlDbType.VarChar);
            sprmparam.Value = PublicVariables._branchId;
            strType = sccmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return strType;
    }
    public DataTable PartyBalanceGetByAgainst(string strAgainstVoucherType, string strAgainstvoucherNo, string strLedgerId, string strCrDr)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("PartyBalanceGetByAgainst", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@againstVoucherType", SqlDbType.VarChar);
            prm.Value = strAgainstVoucherType;
            prm = sdaadapter.SelectCommand.Parameters.Add("@againstvoucherNo", SqlDbType.VarChar);
            prm.Value = strAgainstvoucherNo;
            prm = sdaadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            prm.Value = strLedgerId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@crOrDr", SqlDbType.VarChar);
            prm.Value = strCrDr;
            sdaadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public DataTable BillsPayableReportFill(DateTime fromDate, DateTime toDate, bool optional, string branchId, string status, string ledgerId, string currencyId, string ledgerBy, string fillby, string salesmanId,string ledgerName)
    {
        DataTable dtbl = new DataTable();
        try
        {
           

            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sqldataadapter = new SqlDataAdapter();
            if (fillby == "Vendor")
            {
                 sqldataadapter = new SqlDataAdapter("BillsPayableReportFill", sqlcon);
                 
            }
            else if (fillby == "Customer")
            {
                sqldataadapter = new SqlDataAdapter("BillsReceivableReportFill", sqlcon);
                //sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            else if (fillby == "Total")
            {
                sqldataadapter = new SqlDataAdapter("PartyBalance", sqlcon);
                //sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlparameter = new SqlParameter();
            if (fillby != "Total")
            {
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
                sqlparameter.Value = fromDate;
            }
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
            sqlparameter.Value = toDate;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            sqlparameter.Value = optional;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sqlparameter.Value = branchId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar);
            sqlparameter.Value = status;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            sqlparameter.Value = ledgerId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sqlparameter.Value = currencyId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerBy", SqlDbType.VarChar);
            sqlparameter.Value = ledgerBy;
            if (fillby == "Customer")
            {
                sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@salesmanId", SqlDbType.VarChar);
                sqlparameter.Value = salesmanId;
            }
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerName", SqlDbType.VarChar);
            sqlparameter.Value = ledgerName;
            sqldataadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show("MRSP1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public DataTable PartyBalanceGetByAgainstNew(string strAgainstVoucherType, string strAgainstvoucherNo, string strLedgerId, string strCrDr)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("PartyBalanceGetByAgainstNew", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@againstVoucherType", SqlDbType.VarChar);
            prm.Value = strAgainstVoucherType;
            prm = sdaadapter.SelectCommand.Parameters.Add("@againstvoucherNo", SqlDbType.VarChar);
            prm.Value = strAgainstvoucherNo;
            prm = sdaadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            prm.Value = strLedgerId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@crOrDr", SqlDbType.VarChar);
            prm.Value = strCrDr;
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
    public DataTable SalesManwiseBillsReceivableReportFill(DateTime fromDate, DateTime toDate, bool optional, string branchId, string status, string ledgerId, string currencyId, string ledgerBy, string strEmployeeId) //, string fillby)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sqldataadapter = new SqlDataAdapter();
            sqldataadapter = new SqlDataAdapter("SalesManwiseBillsReceivableReportFill", sqlcon);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlparameter = new SqlParameter();
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
            sqlparameter.Value = fromDate;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
            sqlparameter.Value = toDate;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            sqlparameter.Value = optional;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sqlparameter.Value = branchId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar);
            sqlparameter.Value = status;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            sqlparameter.Value = ledgerId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sqlparameter.Value = currencyId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerBy", SqlDbType.VarChar);
            sqlparameter.Value = ledgerBy;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@employeeId", SqlDbType.VarChar);
            sqlparameter.Value = strEmployeeId;
            sqldataadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show("MRSP1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public DataTable BillsReceivableReportFill(DateTime fromDate, DateTime toDate, bool optional, string branchId, string status, string ledgerId, string currencyId, string ledgerBy)
    {
        DataTable dtbl = new DataTable();
        try
        {
           


            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sqldataadapter = new SqlDataAdapter("BillsReceivableReportFill", sqlcon);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlparameter = new SqlParameter();
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@fromDate", SqlDbType.DateTime);
            sqlparameter.Value = fromDate;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@toDate", SqlDbType.DateTime);
            sqlparameter.Value = toDate;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            sqlparameter.Value = optional;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sqlparameter.Value = branchId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar);
            sqlparameter.Value = status;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            sqlparameter.Value = ledgerId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sqlparameter.Value = currencyId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerBy", SqlDbType.VarChar);
            sqlparameter.Value = ledgerBy;

            sqldataadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show("MRSP1:" + ex.Message,"", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }

    public DataTable AgeingReportFillByReceivable(DateTime ageingDate, bool optional, string branchId, string status, string ledgerId, string currencyId, string ledgerBy, string salesmanId, string ledgerName)
    {

        DataTable dtbl = new DataTable();
        try
        {
           
            
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sqldataadapter = new SqlDataAdapter();
                sqldataadapter = new SqlDataAdapter("AgeingReportFillByReceivable", sqlcon);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlparameter = new SqlParameter();
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ageingDate", SqlDbType.DateTime);
            sqlparameter.Value = ageingDate;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            sqlparameter.Value = optional;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sqlparameter.Value = branchId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar);
            sqlparameter.Value = status;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            sqlparameter.Value = ledgerId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sqlparameter.Value = currencyId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerBy", SqlDbType.VarChar);
            sqlparameter.Value = ledgerBy;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@salesmanId", SqlDbType.VarChar);
            sqlparameter.Value = salesmanId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerName", SqlDbType.VarChar);
            sqlparameter.Value = ledgerName;
            sqldataadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show("MRSP1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public DataTable AgeingReportFillByPayable(DateTime ageingDate, bool optional, string branchId, string status, string ledgerId, string currencyId, string ledgerBy, string ledgerName)
    {

        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sqldataadapter = new SqlDataAdapter();
            sqldataadapter = new SqlDataAdapter("AgeingReportFillByPayable", sqlcon);
            sqldataadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter sqlparameter = new SqlParameter();
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ageingDate", SqlDbType.DateTime);
            sqlparameter.Value = ageingDate;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@optional", SqlDbType.Bit);
            sqlparameter.Value = optional;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            sqlparameter.Value = branchId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@status", SqlDbType.VarChar);
            sqlparameter.Value = status;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            sqlparameter.Value = ledgerId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@currencyId", SqlDbType.VarChar);
            sqlparameter.Value = currencyId;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerBy", SqlDbType.VarChar);
            sqlparameter.Value = ledgerBy;
            sqlparameter = sqldataadapter.SelectCommand.Parameters.Add("@ledgerName", SqlDbType.VarChar);
            sqlparameter.Value = ledgerName;
            sqldataadapter.Fill(dtbl);
        }
        catch (Exception ex)
        {
            MessageBox.Show("MRSP1:" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            sqlcon.Close();
        }
        return dtbl;
    }
    public DataTable PartyBalanceViewByLedgerIdNew(string strLedgerId, string strCrOrDr)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("PartyBalanceViewByLedgerIdNew", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@ledgerId", SqlDbType.VarChar);
            prm.Value = strLedgerId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@crOrDr", SqlDbType.VarChar);
            prm.Value = strCrOrDr;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
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
    public DataTable PartyBalanceByVoucherTypeAndVoucherNoNew(string strVoucherType, string strVoucherNo, string strCrorDr)
    {
        DataTable dtbl = new DataTable();
        try
        {
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            SqlDataAdapter sdaadapter = new SqlDataAdapter("PartyBalanceByVoucherTypeAndVoucherNoNew", sqlcon);
            sdaadapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter prm = new SqlParameter();
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherType", SqlDbType.VarChar);
            prm.Value = strVoucherType;
            prm = sdaadapter.SelectCommand.Parameters.Add("@voucherNo", SqlDbType.VarChar);
            prm.Value = strVoucherNo;
            prm = sdaadapter.SelectCommand.Parameters.Add("@branchId", SqlDbType.VarChar);
            prm.Value = PublicVariables._branchId;
            prm = sdaadapter.SelectCommand.Parameters.Add("@crOrDr", SqlDbType.VarChar);
            prm.Value = strCrorDr;
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
}
}
