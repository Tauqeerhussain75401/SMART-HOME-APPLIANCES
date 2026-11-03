using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace NuReady 
{
    class ReportQuery
    {
        internal  static DataTable AccountStatement(string account ,DateTime Fdate ,DateTime Tdate)
        {

            string sql = "exec Rpt_AccountStatement '" + Fdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + account + "'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable AccountStatementWithDue(string account, DateTime Fdate, DateTime Tdate)
        {

            string sql = "exec Rpt_AccountStatement_withDueDate '" + Fdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + account + "'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable StockBalance(DateTime Fdate, DateTime Tdate, string Filter, decimal Qty, string Catagory, string Type)
        {
            SqlCommand com = new SqlCommand("Rpt_StockBalance", clsConnection.con);            
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@FDate", SqlDbType.Date).Value = Fdate;
            com.Parameters.Add("@TDate", SqlDbType.Date).Value = Tdate;
            com.Parameters.Add("@Catagory", SqlDbType.VarChar, 50).Value = Validation.NullOrEmptyToDBNull  (Catagory);
            com.Parameters.Add("@Type", SqlDbType.VarChar, 50).Value = Validation.NullOrEmptyToDBNull  (Type);
            com.Parameters.Add("@Filter", SqlDbType.VarChar,15).Value = Filter;
            com.Parameters.Add("@qty", SqlDbType.Decimal,20).Value = Qty;
            DataTable dt = Query.getData(com);   
            return dt;
        }
        internal static DataTable StockLedger( string Item, DateTime Fdate, DateTime Tdate)
        {

            string sql = "exec rpt_stockLedger '" + Fdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','"+Item +"'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable IncomeSummery(DateTime Fdate, DateTime Tdate)
        {

            string sql = "exec Rep_IncomeSummery  '"+Fdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) +"' , '"+Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)+"'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable TrialBalance(DateTime Fdate, DateTime Tdate)
        {

            string sql = "exec Rpt_TrialBalance  '" + Fdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' , '" + Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable BalanceSheet( DateTime Tdate)
        {

            string sql = "exec Rpt_BalanceSheet  '" + Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable Balance(string account, DateTime Tdate)
        {

            string sql = "exec Rpt_Balance '" + Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + account + "'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataSet WorkStation(string Vno)
        {

            string sql = "select Vdate,dbo.ItemTitle (fkItem) Item ,dbo.Get_UnitTitle(unit) unit ,Qty ,Rate ,Qty * Rate amount from WorkStationFinishItemDetail  where status = 0 and Vtype = 'MN' and vno = '" + Vno + "'";
            sql += "select Vdate,dbo.ItemTitle (fkItem) Item ,dbo.Get_UnitTitle(unit) unit ,Qty ,Rate ,Qty * Rate amount from WorkStationItemDetail  where status = 0 and Vtype = 'MN' and vno = '" + Vno + "'";
            sql += " select Vdate,dbo.Get_Title (fkAccount ) Account ,dbo.Get_UnitTitle(unit) unit ,Qty ,Rate ,Qty * Rate amount from WorkStationAccDetail   where status = 0 and Vtype = 'MN' and vno = '" + Vno + "' ";
            sql += " select CostRate,Qty   from WorkStationMaster where status = 0 and Vtype = 'MN' and vno = '" + Vno + "' ";
            DataSet ds = Query.getDataSet(sql);
            return ds;
        }
        internal static DataSet  SaleBill(string Vno)
        {

            string sql = "select fkItem ItemCode,dbo.ItemTitle (fkItem ) ItemName,Qty,qtyinpack,dbo.Get_UnitTitle(unit) Unit ,Rate,GrossRate,qty * grossrate TAmount from sales where status = 0 and Vtype = 'SL' and vno = '" + Vno + "'";
            sql += " select  vdate, dbo.Get_Title ( fkAccountid)  Title,Amount,Discount,NetAmount,CashReceipt,CashBack,descr,sales from salemaster where status = 0 and Vtype = 'SL' and vno = '" + Vno + "'";
            sql += " select (select address from CustomerDetail where Code = fkAccountid) AccAddress from salemaster where status = 0 and Vtype = 'SL' and vno = '" + Vno + "'";
            DataSet ds = Query.getDataSet(sql);
            return ds;
        }
        internal static DataSet PurchaseBill(string Vno)
        {

            string sql = "select fkItem ItemCode,dbo.ItemTitle (fkItem ) ItemName,Qty,qtyinpack,dbo.Get_UnitTitle(unit) Unit ,Rate,qty * rate TAmount from PurchaseDetail where status = 0 and Vtype = 'PU' and vno = '" + Vno + "'";
            sql += " select  vdate, dbo.Get_Title ( fkAccountid)  Title, Amount,0 Discount,Amount NetAmount from PurchaseMaster where status = 0 and Vtype = 'PU' and vno = '" + Vno + "'";
            sql += " select (select address from SupplierDetail where Code = fkAccountid) AccAddress from PurchaseMaster where status = 0 and Vtype = 'PU' and vno = '" + Vno + "'";
            DataSet ds = Query.getDataSet(sql);
            return ds;
        }
        internal static DataSet PurchaseRetBill(string Vno)
        {

            string sql = "select fkItem ItemCode,dbo.ItemTitle (fkItem ) ItemName,Qty,qtyinpack,dbo.Get_UnitTitle(unit) Unit ,Rate,qty * rate TAmount from PurchaseRetDetail where status = 0 and Vtype = 'PR' and vno = '" + Vno + "'";
            sql += " select  vdate, dbo.Get_Title ( fkAccountid)  Title, Amount,0 Discount,Amount NetAmount from PurchaseRetMaster where status = 0 and Vtype = 'PR' and vno = '" + Vno + "'";
            sql += " select (select address from SupplierDetail where Code = fkAccountid) AccAddress from PurchaseRetMaster where status = 0 and Vtype = 'PR' and vno = '" + Vno + "'";
            DataSet ds = Query.getDataSet(sql);
            return ds;
        }
        internal static DataSet SaleRetBill(string Vno)
        {

            string sql = "select fkItem ItemCode,dbo.ItemTitle (fkItem ) ItemName,Qty,qtyinpack,dbo.Get_UnitTitle(unit) Unit ,Rate,GrossRate,qty * grossrate TAmount from SaleRetDetail where status = 0 and Vtype = 'SR' and vno = '" + Vno + "'";
            sql += " select  vdate, dbo.Get_Title ( fkAccountid)  Title,Amount,Discount,NetAmount,CashReceipt,CashBack,descr,sales from SaleRetMaster where status = 0 and Vtype = 'SR' and vno = '" + Vno + "'";
            sql += " select (select address from dbo.SupplierDetail where Code = fkAccountid) AccAddress from SaleRetMaster where status = 0 and Vtype = 'SR' and vno = '" + Vno + "'";
            DataSet ds = Query.getDataSet(sql);
            return ds;
        }
        internal static DataTable Rpt_MonthlyInstallments(DateTime date)
        {

            string sql = "exec Rpt_MonthlyInstallments " + date.Month + "," + date.Year;
            DataTable  dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable CustomerBill(string account, DateTime Fdate, DateTime Tdate)
        {

            string sql = "exec Rpt_CustomerBill '" + Fdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + Tdate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "','" + account + "'";
            DataTable dt = Query.getData(sql);
            return dt;
        }
        internal static DataTable EnvelopeDetail(string[] account)
        {

            string acc = "'"+ string.Join("','", account)+"'";
            string sql = @"select Title CustomerName,cd.Address ,cd.SMSNumber Cell,B.BrandName CompanyName from ChartOfAccount c
                           left join CustomerDetail  cd on cd.Code = c.Account
                           left join Brands  b on C.Account LIKE  b.CustomerHead  + '%'  where c.account in (" + acc + ") ";
            DataTable dt = Query.getData(sql);
            return dt;
        }

		internal static DataTable SaleSummary(DateTime fromDate, DateTime toDate)
		{
			DataTable dt = new DataTable();
			SqlCommand cmd = new SqlCommand("Rep_SalesSummary", clsConnection.con);
		
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@vfromdate", SqlDbType.Date).Value = fromDate;
			cmd.Parameters.Add("@vtodate", SqlDbType.Date).Value = toDate;
			SqlDataAdapter adp = new SqlDataAdapter();
			adp.SelectCommand = cmd;
			adp.Fill(dt);
			return dt;
		}
	}
}