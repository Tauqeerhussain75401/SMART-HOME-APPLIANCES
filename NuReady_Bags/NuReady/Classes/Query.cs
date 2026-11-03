using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace NuReady
{
    class Query
    {
        internal static DataTable CompanyDetail()
        {
            string sql = "select * from CompanyDetail ";
            DataTable dt = getData(sql);
            return dt;
        }
        
        internal static DataTable getData(string sql)
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();            
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            //SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable getData(SqlCommand Comm)
        {
            DataTable dt = new DataTable();         
            SqlDataAdapter adap = new SqlDataAdapter(Comm);
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable get_CompanyInfo()
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select * from companydetail";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataSet   getDataSet(string sql)
        {
            DataSet ds = new DataSet();
            // SqlConnection con = conOpen();            
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(ds);
            adap.Dispose();
            return ds;
        }
        internal static DataTable get_ChartOfAcc()
        {
            DataTable dt = new DataTable();
           // SqlConnection con = conOpen();
            string sql = "select * from ChartOfAccount where status = 0";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose(); 
            return dt;
        }
        internal static DataTable AccountsLvl4Head()
        {
            string sql = "select Account, Title  from ChartofAccount where AccLevel = 4 group by Account, Title ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable CustomerIndex()
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select lvl5 Code, lvl5title Title from vu_ChartofAccount where lvl3 in (select customerhead from brands)";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable CustomerIndex(string SalesHead)
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select lvl5 Code, lvl5title Title from vu_ChartofAccount where (lvl3 in (select MapAccount  from DefaultAccount where Title = 'SalesToRec' and Account = '" + SalesHead + "') or  lvl5  in (select code from SupplierDetail where ShowInSales = 1))";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable CustomerAndVenderIndex()
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select lvl5 Code, lvl5title Title from vu_ChartofAccount where lvl3 in ( dbo.Get_Def_Acc ('Customers'),dbo.Get_Def_Acc ('Suppliers'))";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable PayableIndex()
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select lvl5 Code, lvl5title Title from vu_ChartofAccount where lvl1 = '002' order by Title ";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable ExpenseIndex()
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select lvl5 Code, lvl5title Title from vu_ChartofAccount where lvl1 = '004' order by title";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable SupplierIndex()
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select lvl5 Code, lvl5title Title from vu_ChartofAccount where lvl3 = dbo.Get_Def_Acc ('Suppliers')";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable CustomerDetail(string Code)
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select * from CustomerDetail where status = 0 and Code = '"+Code+"'";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }
        internal static DataTable SupplierDetail(string Code)
        {
            DataTable dt = new DataTable();
            // SqlConnection con = conOpen();
            string sql = "select * from supplierdetail where status = 0 and Code = '" + Code + "'";
            SqlDataAdapter adap = new SqlDataAdapter(sql, clsConnection.con);
            SqlCommandBuilder comBuild = new SqlCommandBuilder();
            adap.Fill(dt);
            adap.Dispose();
            return dt;
        }

        
        internal static DataTable NarrationIndex()
        {
            string sql = "select * from Narration  where status = 0";
            DataTable dt = getData(sql); 
            return dt;
        }
        internal static DataTable UnitIndex()
        {
            string sql = "select * from Units  where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable WaiterIndex()
        {
            string sql = "select * from Waiter  where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable TableIndex()
        {
            string sql = "select * from TableIndex  where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable ItemCatagoryIndex()
        {
            string sql = "select * from ItemCatagory  where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable ItemCatagoryIndexActive()
        {
            string sql = "select Code ,Title from ItemCatagory  where status = 0 and active = 1";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable ItemTypeIndexActive()
        {
            string sql = "select Code ,Title from ItemType where status = 0 and active = 1";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable ItemIndex()
        {
            string sql = "select * from ItemDetail where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable CustomerSupplyGenerate(string itemCode)
        {
            string sql = "select sp.account,sp.qty,sp.rate,sp.qty * sp.rate amount from CustomerSupplyDetail sp left join ItemDetail it on it.Code = sp.Item   where active = 1 and item = '" + itemCode + "' and it.status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable ItemDetail()
        {
            string sql = "select * from ItemDetail where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable ItemDetail2()
        {
            string sql = "select * from ItemDetail ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable Accounts()
        {
            string sql = "select Account,Title   from ChartOfAccount  where AccType = 'Detail' and status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable OpenningAccounts()
        {
            string sql = "select lvl4 as ParentCode,lvl5 as Code,lvl5title as Title ,isnull(dbo.OpenningBal(lvl5),0) Bal from vu_ChartofAccount  ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable OpenningAccounts(string ParentAcc)
        {
            string sql = "select lvl4 as ParentCode,lvl5 as Code,lvl5title as Title ,isnull(dbo.OpenningBal(lvl5),0) Bal from vu_ChartofAccount  where lvl4  like '" + ParentAcc + "'";
            DataTable dt = getData(sql);
            return dt;
        }
        
        internal static DataTable DetailAccounts()
        {
            string sql = "select lvl5 as account,lvl5title as title from vu_ChartofAccount ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable PayrollQuery( string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select  Vdate,'PL-'+voucherno voucherno ,sum(netsalary) Amount,salaryType,min(createdby ) createdby,min(createdtime) createdtime,max(editby) editby,max(edittime ) edittime from payroll where status = 0  " + filterCondition + " group by VoucherNo,vdate,salarytype order by VoucherNo desc";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable VoucherQuery(string vtype, string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select max(Vdate) Vdate,vtype,voucherno ,sum(case when  Amount  > 0 then  amount else -amount end) amount,dbo.Get_NarrationTitle(narration) narration ,min(createdby ) createdby,min(createdtime) createdtime,max(editby) editby,max(edittime ) edittime from gl1 where status = 0 and Vtype = '" + vtype + "' " + filterCondition + " group by vtype,VoucherNo ,narration  order by vtype,VoucherNo desc";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable VoucherQuery(string vtype, string Vno)
        {
            string sql = "select max(Vdate) Vdate,vtype,voucherno ,sum(case when  Amount  > 0 then  amount else -amount end) amount,dbo.Get_NarrationTitle(narration) narration ,min(createdby ) createdby,min(createdtime) createdtime,max(editby) editby,max(edittime ) edittime from gl1 where status = 0 and Vtype = '" + vtype + "' and VoucherNo  = '" + Vno + "'  group by vtype,VoucherNo ,narration  order by vtype,VoucherNo desc";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable VoucherDetail(string vtype, string Vno)
        {
            string sql = "select * from GL1 where status = 0 and Vtype = '" + vtype + "' and voucherno = '" + Vno + "'";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable VoucherDetail(string vtype, string Vno, string Account)
        {
            string sql = "select * from GL1 where status = 0 and Vtype = '" + vtype + "' and voucherno = '" + Vno + "' and '" + Account + "' in (CrAccount,drAccount)";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataSet SaleDetail(string vtype, string Vno)
        {
            string sql = "select *,dbo.ItemTitle(fkItem) ItemTitle,(GrossRate * qty) GrossAmount,(rate * qty)+taxcharged as amount   from Sales where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select * from SaleMaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet ds = getDataSet(sql);
            return ds;
        }
        internal static DataSet Return_PostFBRINV(string vtype, string Vno)
        {
            string sql = "select *,dbo.ItemTitle(fkItem) ItemTitle,(GrossRate * qty) GrossAmount,(rate * qty)+taxcharged as amount   from SaleRetDetail where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select * from SaleRetMaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet ds = getDataSet(sql);
            return ds;
        }
        internal static DataSet SaleReceiptDetail(string vtype, string Vno)
        {
            string sql = "select *,fkItem Code,dbo.ItemTitle(fkItem) Item,(GrossRate * qty) GrossAmount,(rate * qty) as amount,taxcharged tax   from Sales where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select *,CONVERT(DATETIME, CONVERT(CHAR(8), Vdate, 112) + ' ' + CONVERT(CHAR(8), Vtime, 108)) as vdate1,getdate() Serverdate,isnull(posservicefees,0) as posservicefees1 from SaleMaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet ds = getDataSet(sql);
            return ds;
        }
        internal static DataSet Return_receipt(string vtype, string Vno)
        {
            string sql = "select *,fkItem Code,dbo.ItemTitle(fkItem) Item,(GrossRate * qty) GrossAmount,(rate * qty) as amount,taxcharged tax   from SaleRetDetail where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select *,CONVERT(DATETIME, CONVERT(CHAR(8), Vdate, 112) + ' ' + CONVERT(CHAR(8), Vtime, 108)) as vdate1,getdate() Serverdate from SaleRetMaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet ds = getDataSet(sql);
            return ds;
        }


        internal static DataSet SaleRetDetail(string vtype, string Vno)
        {
            string sql = "select *,(rate * qty) as amount   from SaleretDetail where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select * from SaleRetMaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet ds = getDataSet(sql);
            return ds;
        }
        internal static DataSet PurchaseDetail(string vtype, string Vno)
        {
            string sql = "select *,(rate * qty) + addless as amount  from purchasedetail where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';" +
                "select * from purchasemaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet  ds = getDataSet(sql);
            return ds;
        }
        internal static DataSet PurchaseRetDetail(string vtype, string Vno)
        {
            string sql = "select *,(rate * qty) as amount  from purchaseRetdetail where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';" +
                "select * from purchaseRetmaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet ds = getDataSet(sql);
            return ds;
        }
        internal static DataSet WorkStationDetail(string vtype, string Vno)
        {
            string sql = "select *   from WorkStationMaster where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select *,qty*rate amount from WorkStationFinishItemDetail ii where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select *,qty*rate amount from WorkStationItemDetail i where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "'" +
                "select *,qty*rate amount from WorkStationAccDetail where status = 0 and Vtype = '" + vtype + "' and vno = '" + Vno + "';";
            DataSet ds = getDataSet(sql);
            return ds;
        }
        internal static DataTable  LabourWorkDetail(string vtype, string Vno)
        {
            string sql = "select *,qty*rate amount from WorkStationAccDetail where status = 0 and serialtype = '" + vtype + "' and serialno = '" + Vno + "';";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable  BankReconcilation(string Account,DateTime Fdate,DateTime Tdate)
        {
            string sql = "select clear,vdate,vtype +'-'+ VoucherNo Vno,vseq,CheckNum,CheckDate  ,dbo.Get_Title(particular) title,dr,cr   from GL_Detail  where status = 0 and checknum is not null and account = '" + Account + "' and vdate between '" + Fdate.ToString("MM/dd/yyyy") + "' and '" + Tdate.ToString("MM/dd/yyyy") + "'";
            DataTable  dt = getData(sql);
            return dt;
        }
 
        internal static DataTable CashBanks()
        {
            string sql = "select   lvl5 Account,lvl5Title Title  from vu_ChartofAccount where lvl3 = 001002002";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable SaleInvoices()
        {
            string sql = "select vdate ,vtime,vtype + '-' + vno vno,DeliveryConfirm   from SaleMaster  where status = 0  order by vdate desc,vtime ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable SaleReceipt(string vno)
        {
            string sql = "select CashReceipt,CashBack,Balance ,(select sum(qty * rate) from Sales sd where sd.status = 0 and  sm.vno = sd.Vno )  amount    from salemaster sm  where status = 0  and  vtype = 'SO' and vno = '" + vno + "' ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable SaleInvoices(string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select vdate,vtype,vno,dbo.Get_Title(fkaccountid) fkaccountid,CreatedBy,CreatedTime,EditBy,EditTime   from SaleMaster  where status = 0 " + filterCondition + " order by vdate ,vtime ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable SaleRetInvoices(string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select vdate,vtype,vno,dbo.Get_Title(fkaccountid) fkaccountid,CreatedBy,CreatedTime,EditBy,EditTime   from SaleRetMaster  where status = 0 " + filterCondition + " order by vdate ,vtime ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable WorkStationInvoices(string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select vdate,vtype,vno,dbo.ItemTitle(fkItemid) fkItem,CreatedBy,CreatedTime,EditBy,EditTime   from WorkStationMaster w where status = 0 " + filterCondition + " order by vdate ,vtime ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable LabourInvoices(string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select vdate,serialtype,serialno,dbo.Get_Title(fkAccount) fkAccount,sum(qty * rate) Amount,min(CreatedBy) CreatedBy,min(CreatedTime) CreatedTime,max(EditBy) EditBy,max(EditTime  ) EditTime   from WorkStationAccDetail w where status = 0 and serialno is not null " + filterCondition + " group by vdate,serialtype,serialno,CreatedBy,fkAccount order by vdate ";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable PurchaseInvoices(string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select vdate,vtype,vno,dbo.Get_Title(fkaccountid) fkaccountid,CreatedBy,CreatedTime,EditBy,EditTime   from PurchaseMaster  where status = 0  order by vdate ,vtime " + filterCondition;
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable PurchaseRetInvoices(string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select vdate,vtype,vno,dbo.Get_Title(fkaccountid) fkaccountid,CreatedBy,CreatedTime,EditBy,EditTime   from PurchaseRetMaster  where status = 0  order by vdate ,vtime " + filterCondition;
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable StockAdjInvoices(string[] filter)
        {
            string filterCondition = "";
            for (int i = 0; i < filter.Count(); i++)
            {
                filterCondition += filter[i] + " ";
            }
            string sql = "select vdate,vtype,vno,dbo.Get_NarrationTitle (Narration)  Narration ,CreatedBy,CreatedTime,EditBy,EditTime   from StockAdjMaster  where status = 0  order by vdate ,vtime " + filterCondition;
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable UserDetail(string UserId)
        {            
            string sql = "select *,getdate() date from users where userid = '" +UserId  + "'";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static decimal Balance(string Account)
        {
            decimal bal = 0;
            string sql = "select isnull(sum(dr - CR),0) from GL_Detail where status = 0  and account = '"+Account +"'";
            DataTable dt = getData(sql);
            if (dt.Rows.Count > 0) bal = decimal.Parse(dt.Rows[0][0].ToString());  
            return bal;
        }
        internal static decimal Balance(DateTime VDate, string Account)
        {
            decimal bal = 0;
            string sql = "select isnull(sum(dr - CR),0) from GL_Detail where status = 0 and vdate <= '" + VDate.ToString("MM/dd/yyyy") + "'  and account = '" + Account + "'";
            DataTable dt = getData(sql);
            if (dt.Rows.Count > 0) bal = decimal.Parse(dt.Rows[0][0].ToString());
            return bal;
        }
        internal static decimal Balance(DateTime  VDate,string Account,string  clear)
        {
            decimal bal = 0;
            string sql = "select isnull(sum(dr - CR),0) from GL_Detail where status = 0 and vdate <= '"+VDate.ToString("MM/dd/yyyy")+ "'  and clear = "+clear +" and account = '" + Account + "'";
            DataTable dt = getData(sql);
            if (dt.Rows.Count > 0) bal = decimal.Parse(dt.Rows[0][0].ToString());
            return bal;
        }
        internal static DataTable HRIndex()
        {
            string sql = "select   * from HRinfo  where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable HRIndex1()
        {
            string sql = "select   id,name Title from HRinfo  where status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable HRIndex(string ID)
        {
            string sql = "select   * from HRinfo where id = '" + ID  + "' and status = 0";
            DataTable dt = getData(sql);
            return dt;
        }
        internal static DataTable TransporterIndex()
        {
            string sql = "select   * from transporters";
            DataTable dt = getData(sql);
            return dt;
        }
    }
}
