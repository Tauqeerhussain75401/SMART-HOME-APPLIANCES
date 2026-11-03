using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace NuReady
{
    class DML
    {
        internal static void Execute(string sql)
        {
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                con.Open(); // must explicitly open since we're executing directly
                com.ExecuteNonQuery();
            }
        }
        public static bool ChartOfAccAdd_Edit(string account, string title, string parentid, string AccType, string AccLevel)
{
    bool Saved = false;

    using (SqlConnection con = clsConnection.GetConnection())
    using (SqlCommand com = new SqlCommand("ChartOfAccAdd_Edit", con))
    {
        com.CommandType = CommandType.StoredProcedure;

        com.Parameters.Add("@account", SqlDbType.VarChar).Value = account;
        com.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
        com.Parameters.Add("@parentid", SqlDbType.VarChar).Value = parentid;
        com.Parameters.Add("@AccType", SqlDbType.VarChar).Value = AccType;
        com.Parameters.Add("@AccLevel", SqlDbType.VarChar).Value = AccLevel;
        com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

        con.Open();
        com.ExecuteNonQuery();
        Saved = true;
    }

    return Saved;
}

        public static bool OpenningBal_Add_Edit(string Account, string DrAmount, string CrAmount)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("OpenningBal_Add_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Account", SqlDbType.VarChar).Value = Account;
                com.Parameters.Add("@DrAmount", SqlDbType.VarChar).Value = Validation.NullToDBNull(DrAmount);
                com.Parameters.Add("@CrAmount", SqlDbType.VarChar).Value = Validation.NullToDBNull(CrAmount);
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool CustomerSupplyDetail_Add_Edit(string Account, string ItemCode, string Rate, string Qty,
            string Active, string status)
        {
            bool IsSave = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("CustomerSupplyDetail_Add_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Account", SqlDbType.VarChar).Value = Account;
                com.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = ItemCode;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Active", SqlDbType.VarChar).Value = Active;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                IsSave = true;
            }

            return IsSave;
        }

        public static string CustomerInfo_Add_Edit(string Code, string Email, string Fax, string CNIC, string Address,
            string Qualification, string Phone1, string Phone2, string SMSNumber, string IBAN, string SMSAlert, string EmailAlert,
            string image, string Active, string status)
        {
            string IVCode = Code;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("CustomerInfo_Add_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Code", SqlDbType.VarChar).Value = Code;
                com.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;
                com.Parameters.Add("@Fax", SqlDbType.VarChar).Value = Fax;
                com.Parameters.Add("@CNIC", SqlDbType.VarChar).Value = CNIC;
                com.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;
                com.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = Qualification;
                com.Parameters.Add("@Phone1", SqlDbType.VarChar).Value = Phone1;
                com.Parameters.Add("@Phone2", SqlDbType.VarChar).Value = Phone2;
                com.Parameters.Add("@SMSNumber", SqlDbType.VarChar).Value = SMSNumber;
                com.Parameters.Add("@IBAN", SqlDbType.VarChar).Value = IBAN;
                com.Parameters.Add("@SMSAlert", SqlDbType.VarChar).Value = SMSAlert;
                com.Parameters.Add("@EmailAlert", SqlDbType.VarChar).Value = EmailAlert;
                com.Parameters.Add("@image", SqlDbType.VarBinary).Value = DBNull.Value; // TODO: handle actual image bytes
                com.Parameters.Add("@Active", SqlDbType.VarChar).Value = Active;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                com.Parameters.Add("@Retval", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();

                IVCode = com.Parameters["@Retval"].Value.ToString();
            }

            return IVCode;
        }


        public static string SupplierInfo_Add_Edit(string Code, string Email, string Fax, string CNIC, string Address,
    string Qualification, string Phone1, string Phone2, string SMSNumber, string IBAN, string SMSAlert, string EmailAlert,
    string image, string Active, string ShowInSales, string status)
        {
            string IVCode = Code;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("SupplierInfo_Add_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Code", SqlDbType.VarChar).Value = Code;
                com.Parameters.Add("@Email", SqlDbType.VarChar).Value = Email;
                com.Parameters.Add("@Fax", SqlDbType.VarChar).Value = Fax;
                com.Parameters.Add("@CNIC", SqlDbType.VarChar).Value = CNIC;
                com.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;
                com.Parameters.Add("@Qualification", SqlDbType.VarChar).Value = Qualification;
                com.Parameters.Add("@Phone1", SqlDbType.VarChar).Value = Phone1;
                com.Parameters.Add("@Phone2", SqlDbType.VarChar).Value = Phone2;
                com.Parameters.Add("@SMSNumber", SqlDbType.VarChar).Value = SMSNumber;
                com.Parameters.Add("@IBAN", SqlDbType.VarChar).Value = IBAN;
                com.Parameters.Add("@SMSAlert", SqlDbType.VarChar).Value = SMSAlert;
                com.Parameters.Add("@EmailAlert", SqlDbType.VarChar).Value = EmailAlert;
                com.Parameters.Add("@image", SqlDbType.VarBinary).Value = DBNull.Value;
                com.Parameters.Add("@Active", SqlDbType.VarChar).Value = Active;
                com.Parameters.Add("@ShowInSales", SqlDbType.VarChar).Value = ShowInSales;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                com.Parameters.Add("@Retval", SqlDbType.VarChar, 5).Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();

                IVCode = com.Parameters["@Retval"].Value.ToString();
            }

            return IVCode;
        }

        public static bool Change_Password(string UserId, string Password)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("UPDATE users SET password = @Password WHERE userid = @UserId", con))
            {
                com.CommandType = CommandType.Text;

                com.Parameters.Add("@Password", SqlDbType.VarChar).Value = Password;
                com.Parameters.Add("@UserId", SqlDbType.VarChar).Value = UserId;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool NarrationAdd_Edit(string Code, string title, string Status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("NarrationAdd_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Code", SqlDbType.VarChar, 3).Value = Code;
                com.Parameters.Add("@title", SqlDbType.NVarChar, 150).Value = title;
                com.Parameters.Add("@status", SqlDbType.VarChar, 150).Value = Status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool TransporterAdd_Edit(string Code, string title, string Status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("TransporterAdd_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Code", SqlDbType.VarChar, 3).Value = Code;
                com.Parameters.Add("@title", SqlDbType.NVarChar, 150).Value = title;
                com.Parameters.Add("@status", SqlDbType.VarChar, 150).Value = Status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool UnitsAdd_Edit(string Code, string title, string Status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("UnitsAdd_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Code", SqlDbType.VarChar, 3).Value = Code;
                com.Parameters.Add("@title", SqlDbType.VarChar, 150).Value = title;
                com.Parameters.Add("@status", SqlDbType.VarChar, 150).Value = Status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }


        public static bool ItemCatagoryAdd_Edit(string Code, string title, string Active, string Status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("ItemCatagoryAdd_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Code", SqlDbType.VarChar, 3).Value = Code;
                com.Parameters.Add("@title", SqlDbType.VarChar, 150).Value = title;
                com.Parameters.Add("@Active", SqlDbType.VarChar, 1).Value = Active;
                com.Parameters.Add("@status", SqlDbType.VarChar, 1).Value = Status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }


        public static bool ItemAdd_Edit(string fkItemCatagory, string BarCode, string Id, string Code, string title,
     string ItemKey, string PriRate, string SecRate, string PrimaryUnit,
     string SecondaryUnit, string DefaultUnit, string QtyInPack, string Alert,
     string LowStockAlert, string OpnStock, string OpnRate, string Status, string PCTCode)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("ItemAdd_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@fkItemCatagory", SqlDbType.VarChar, 3).Value = fkItemCatagory;
                com.Parameters.Add("@Id", SqlDbType.VarChar, 10).Value = Id;
                com.Parameters.Add("@Code", SqlDbType.VarChar, 5).Value = Code;
                com.Parameters.Add("@BarCode", SqlDbType.VarChar, 150).Value = Validation.NullToDBNull(BarCode);
                com.Parameters.Add("@title", SqlDbType.VarChar, 150).Value = title;
                com.Parameters.Add("@ItemKey", SqlDbType.VarChar, 50).Value = Validation.NullToDBNull(ItemKey);
                com.Parameters.Add("@PriRate", SqlDbType.Decimal).Value = PriRate;
                com.Parameters.Add("@SecRate", SqlDbType.Decimal).Value = SecRate;
                com.Parameters.Add("@PrimaryUnit", SqlDbType.VarChar, 10).Value = Validation.NullToDBNull(PrimaryUnit);
                com.Parameters.Add("@SecondaryUnit", SqlDbType.VarChar, 10).Value = Validation.NullToDBNull(SecondaryUnit);
                com.Parameters.Add("@DefaultUnit", SqlDbType.VarChar, 10).Value = Validation.NullToDBNull(DefaultUnit);
                com.Parameters.Add("@QtyInPack", SqlDbType.Decimal).Value = Validation.NullToDBNull(QtyInPack);
                com.Parameters.Add("@Alert", SqlDbType.VarChar, 1).Value = Alert;
                com.Parameters.Add("@LowStockAlert", SqlDbType.Decimal).Value = Validation.NullToDBNull(LowStockAlert);
                com.Parameters.Add("@OpnStock", SqlDbType.Decimal).Value = Validation.NullToDBNull(OpnStock);
                com.Parameters.Add("@OpnRate", SqlDbType.Decimal).Value = Validation.NullToDBNull(OpnRate);
                com.Parameters.Add("@status", SqlDbType.VarChar, 1).Value = Status;
                com.Parameters.Add("@PCTCode", SqlDbType.VarChar).Value = PCTCode;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool ChartOfAcc_delete(string account)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("ChartOFAccount_delete", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@account", SqlDbType.VarChar).Value = account;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }


        public static bool GL_Add_Edit(DateTime VDate, string VoucherNo, string Vtype, string Vseq,
      string DrAccount, string Amount, string CrAccount, string Narration, string Remarks,
      string CheckNum, object CheckDate, string CheckStatus, string Clear, string status,
      ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("GLAdd_Edit1", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@VDate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@VoucherNo", SqlDbType.VarChar).Value = (object)VoucherNo ?? DBNull.Value;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Vseq;
                com.Parameters.Add("@DRAccount", SqlDbType.VarChar).Value = DrAccount;
                com.Parameters.Add("@Amount", SqlDbType.VarChar).Value = Amount;
                com.Parameters.Add("@CRAccount", SqlDbType.VarChar).Value = CrAccount;
                com.Parameters.Add("@Narration", SqlDbType.VarChar).Value = (object)Narration ?? DBNull.Value;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = (object)Remarks ?? DBNull.Value;
                com.Parameters.Add("@CheckNum", SqlDbType.VarChar).Value = (object)CheckNum ?? DBNull.Value;
                com.Parameters.Add("@CheckDate", SqlDbType.VarChar).Value = CheckDate ?? DBNull.Value;
                com.Parameters.Add("@CheckStatus", SqlDbType.VarChar).Value = (object)CheckStatus ?? DBNull.Value;
                com.Parameters.Add("@Clear", SqlDbType.VarChar).Value = Clear;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetVoucherNo", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();

                voucher = Retparam.Value.ToString();
                Saved = true;
            }

            return Saved;
        }




        public static bool SaleMaster_AddEdit(DateTime VDate, string Vno, string Vtype, string fkaccount, string Descr,
                string PaymentMode, decimal CashReceipt, decimal CashBack, string Counter, ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("SaleMaster_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@VDate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno == null ? DBNull.Value : (object)Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@fkaccount", SqlDbType.VarChar).Value = Validation.NullToDBNull(fkaccount);
                com.Parameters.Add("@Descr", SqlDbType.VarChar).Value = Validation.NullToDBNull(Descr);
                com.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                com.Parameters.Add("@CashReceipt", SqlDbType.Decimal).Value = CashReceipt;
                com.Parameters.Add("@CashBack", SqlDbType.Decimal).Value = CashBack;
                com.Parameters.Add("@Counter", SqlDbType.VarChar).Value = Counter;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = 0;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetInvoiceNo", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();
                voucher = com.Parameters["@RetInvoiceNo"].Value.ToString();
                Saved = true;
            }

            return Saved;
        }

        public static bool Sale_AddEdit(string Vno, string Vtype, string seq, string fkAccount, string Qty,
            string Rate, string Discount, string SST, string status, string PCTCode)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Sale_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkItem", SqlDbType.VarChar).Value = fkAccount;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Discount", SqlDbType.VarChar).Value = Discount;
                com.Parameters.Add("@SST", SqlDbType.VarChar).Value = SST;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                com.Parameters.Add("@pctcode", SqlDbType.VarChar).Value = PCTCode;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }


        public static bool SaleRetMaster_AddEdit(DateTime VDate, string Vno, string Vtype, string fkaccount, string Descr,
      string PaymentMode, decimal CashReceipt, decimal CashBack, string Counter, ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("SaleRetMaster_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@VDate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno == null ? DBNull.Value : (object)Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@fkaccount", SqlDbType.VarChar).Value = Validation.NullToDBNull(fkaccount);
                com.Parameters.Add("@Descr", SqlDbType.VarChar).Value = Validation.NullToDBNull(Descr);
                com.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                com.Parameters.Add("@CashReceipt", SqlDbType.Decimal).Value = CashReceipt;
                com.Parameters.Add("@CashBack", SqlDbType.Decimal).Value = CashBack;
                com.Parameters.Add("@Counter", SqlDbType.VarChar).Value = Counter;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = 0;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetInvoiceNo", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();
                voucher = com.Parameters["@RetInvoiceNo"].Value.ToString();
                Saved = true;
            }

            return Saved;
        }

        public static bool SaleRet_AddEdit(string Vno, string Vtype, string seq, string fkAccount,
            string Qty, string Rate, string Discount, string SST, string status, string PCTCode)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("SaleRetDetail_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkItem", SqlDbType.VarChar).Value = fkAccount;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Discount", SqlDbType.VarChar).Value = Discount;
                com.Parameters.Add("@SST", SqlDbType.VarChar).Value = SST;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                com.Parameters.Add("@pctcode", SqlDbType.VarChar).Value = PCTCode;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool WorkStationMaster_AddEdit(DateTime VDate, string Vno, string Vtype, string Descr,
            string Narration, decimal Qty, decimal CostRate, string Counter, ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("WorkStationMaster_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@VDate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno == null ? DBNull.Value : (object)Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Descr", SqlDbType.VarChar).Value = Validation.NullToDBNull(Descr);
                com.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Validation.NullToDBNull(Narration);
                com.Parameters.Add("@Qty", SqlDbType.Decimal).Value = Qty;
                com.Parameters.Add("@CostRate", SqlDbType.Decimal).Value = CostRate;
                com.Parameters.Add("@Counter", SqlDbType.VarChar).Value = Counter;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = 0;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetInvoiceNo", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();
                voucher = com.Parameters["@RetInvoiceNo"].Value.ToString();
                Saved = true;
            }

            return Saved;
        }


        public static bool WorkStationItem_AddEdit(string Vno, string Vtype, string seq, DateTime Vdate,
        string fkItem, string Unit, string Qty, string Rate, string Remarks, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("WorkStationItem_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vdate", SqlDbType.Date).Value = Vdate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkItem", SqlDbType.VarChar).Value = fkItem;
                com.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Validation.NullToDBNull(Unit);
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Validation.NullToDBNull(Remarks);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool WorkStationFinishItemDetail_AddEdit(string Vno, string Vtype, string seq, DateTime Vdate,
            string fkItem, string Unit, string Qty, string Rate, string Remarks, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("WorkStationFinishItemDetail_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vdate", SqlDbType.Date).Value = Vdate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkItem", SqlDbType.VarChar).Value = fkItem;
                com.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Validation.NullToDBNull(Remarks);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool WorkStationAcc_AddEdit(string Vno, string Vtype, string seq, DateTime Vdate,
            string fkAccount, string fkExpAccount, string Unit, string Qty, string Rate, string Remarks, string SerialNo, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("WorkStationAcc_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vdate", SqlDbType.Date).Value = Vdate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkAccount", SqlDbType.VarChar).Value = fkAccount;
                com.Parameters.Add("@fkExpAccount", SqlDbType.VarChar).Value = fkExpAccount;
                com.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Validation.NullToDBNull(Remarks);
                com.Parameters.Add("@SerialNo", SqlDbType.VarChar).Value = Validation.NullOrEmptyToDBNull(SerialNo);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool WorkStationAcc_AddEdit(string Vno, string Vtype, string MVno, string MVtype, string seq, DateTime Vdate,
            string fkAccount, string fkExpAccount, string Unit, string Qty, string Rate, string Remarks, string status, out string RetVno)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("WorkStationLabour_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vdate", SqlDbType.Date).Value = Vdate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vno);
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@MVno", SqlDbType.VarChar).Value = MVno;
                com.Parameters.Add("@MVtype", SqlDbType.VarChar).Value = MVtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = Validation.NullToDBNull(seq);
                com.Parameters.Add("@fkAccount", SqlDbType.VarChar).Value = fkAccount;
                com.Parameters.Add("@fkExpAccount", SqlDbType.VarChar).Value = fkExpAccount;
                com.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Validation.NullToDBNull(Remarks);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                com.Parameters.Add("@User", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetVno", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();
                RetVno = com.Parameters["@RetVno"].Value.ToString();
                Saved = true;
            }
            return Saved;
        }




        public static bool Change_GL_Status(string Vno, string Vtype, string Vseq, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Change_GL_Status", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vseq);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Change_SL_Status(string Vno, string Vtype, string Vseq, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Change_SL_Status", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vseq);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Change_SR_Status(string Vno, string Vtype, string Vseq, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Change_SR_Status", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vseq);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Change_MN_Status(string Vno, string Vtype, string Vseq, string Type, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Change_MN_Status", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@type", SqlDbType.VarChar).Value = Type;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vseq);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Change_LW_Status(string Vno, string Vtype, string Vseq, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Change_LW_Status", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vseq);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Change_PU_Status(string Vno, string Vtype, string Vseq, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Change_PU_Status", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vseq);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Change_PR_Status(string Vno, string Vtype, string Vseq, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Change_PR_Status", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Vseq", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vseq);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Change_ItemDetail_Status(string Id, string status)
        {
            bool Saved = false;
            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("update ItemDetail set status = @status where status = 0 and Id = @ID", con))
            {
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = Id;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }



        public static bool PurchaseMaster_AddEdit(DateTime VDate, string Vno, string Vtype, string fkaccount, string Descr,
       string Narration, string Counter, ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("puchaseMaster_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@VDate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno == null ? DBNull.Value : (object)Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@fkaccount", SqlDbType.VarChar).Value = fkaccount;
                com.Parameters.Add("@Descr", SqlDbType.VarChar).Value = Validation.NullToDBNull(Descr);
                com.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Validation.NullToDBNull(Narration);
                com.Parameters.Add("@Counter", SqlDbType.VarChar).Value = Counter;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetInvoiceNo", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();

                voucher = com.Parameters["@RetInvoiceNo"].Value.ToString();
                Saved = true;
            }

            return Saved;
        }

        public static bool PurchaseDetail_AddEdit(string Vno, string Vtype, string seq,
           string fkAccount, string Unit, string Qty, string Rate, string AddLess, string status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("PurchaseDetail_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkItem", SqlDbType.VarChar).Value = fkAccount;
                com.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@AddLess", SqlDbType.VarChar).Value = AddLess;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool PurchaseRetMaster_AddEdit(DateTime VDate, string Vno, string Vtype, string fkaccount, string Descr,
            string Narration, string Counter, ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("puchaseRetMaster_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@VDate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno == null ? DBNull.Value : (object)Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@fkaccount", SqlDbType.VarChar).Value = fkaccount;
                com.Parameters.Add("@Descr", SqlDbType.VarChar).Value = Validation.NullToDBNull(Descr);
                com.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Validation.NullToDBNull(Narration);
                com.Parameters.Add("@Counter", SqlDbType.VarChar).Value = Counter;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = "0";
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetInvoiceNo", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();

                voucher = com.Parameters["@RetInvoiceNo"].Value.ToString();
                Saved = true;
            }

            return Saved;
        }

        public static bool PurchaseRetDetail_AddEdit(string Vno, string Vtype, string seq,
           string fkAccount, string Unit, string Qty, string Rate, string status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("PurchaseRetDetail_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkItem", SqlDbType.VarChar).Value = fkAccount;
                com.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Unit;
                com.Parameters.Add("@Qty", SqlDbType.VarChar).Value = Qty;
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool UpdateBankReconcile(string Vno, string Clear, object ClearDate)
        {
            bool Saved = false;

            string sql = "UPDATE Gl1 SET clear = @clear, cleardate = @cleardate " +
                         "WHERE voucherno = @vno AND vtype = @vtype AND status = 0";

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand(sql, con))
            {
                com.CommandType = CommandType.Text;

                com.Parameters.Add("@clear", SqlDbType.VarChar).Value = Clear;
                com.Parameters.Add("@ClearDate", SqlDbType.Date).Value = Validation.NullToDBNull(ClearDate);
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vno.Substring(0, 2);
                com.Parameters.Add("@vno", SqlDbType.VarChar).Value = Vno.Substring(3);

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }

            return Saved;
        }

        public static bool StockAdjMaster_AddEdit(DateTime VDate, string Vno, string Vtype, string Descr,
            string Narration, string status, ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("StockAdjMaster_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@VDate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Validation.NullToDBNull(Vno);
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@Descr", SqlDbType.VarChar).Value = Validation.NullToDBNull(Descr);
                com.Parameters.Add("@Narration", SqlDbType.VarChar).Value = Validation.NullToDBNull(Narration);
                com.Parameters.Add("@Terminal", SqlDbType.VarChar).Value = Validation.NullToDBNull(SoftwareInfo.Terminal);
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                com.Parameters.Add("@user", SqlDbType.VarChar).Value = UserInfo.UserId;

                SqlParameter Retparam = com.Parameters.Add("@RetInvoiceNo", SqlDbType.VarChar, 8);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();
                voucher = com.Parameters["@RetInvoiceNo"].Value.ToString();
                Saved = true;
            }
            return Saved;
        }

        public static bool StockAdjDetail_AddEdit(string Vno, string Vtype, string seq, string fkItemCatagory,
            string fkItem, string QtyIn, string QtyOut, string Rate, string Desc, string status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("StockAdjDetail_AddEdit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@Vno", SqlDbType.VarChar).Value = Vno;
                com.Parameters.Add("@Vtype", SqlDbType.VarChar).Value = Vtype;
                com.Parameters.Add("@seq", SqlDbType.VarChar).Value = seq;
                com.Parameters.Add("@fkItemCatagory", SqlDbType.VarChar).Value = fkItemCatagory;
                com.Parameters.Add("@fkItem", SqlDbType.VarChar).Value = fkItem;
                com.Parameters.Add("@QtyIn", SqlDbType.VarChar).Value = Validation.NullToZero(QtyIn);
                com.Parameters.Add("@QtyOut", SqlDbType.VarChar).Value = Validation.NullToZero(QtyOut);
                com.Parameters.Add("@Rate", SqlDbType.VarChar).Value = Rate;
                com.Parameters.Add("@Desc", SqlDbType.VarChar).Value = Desc;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool HRInfo_Add_Edit(string ID, string Name, string FatherName, string Gender, DateTime DOB, string MaritialStatus,
            string CNIC, DateTime AppointmentDate, DateTime JoiningDate, string Designation, string SalaryType, decimal Salary, decimal LeaveCharges,
            decimal Overtime, string ExpenseAccount, string PayableAccount, int Status)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("HRInfo_Add_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = ID;
                com.Parameters.Add("@Name", SqlDbType.VarChar).Value = Name;
                com.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = FatherName;
                com.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
                com.Parameters.Add("@DOB", SqlDbType.Date).Value = DOB;
                com.Parameters.Add("@MaritialStatus", SqlDbType.VarChar).Value = MaritialStatus;
                com.Parameters.Add("@CNIC", SqlDbType.VarChar).Value = CNIC;
                com.Parameters.Add("@AppointmentDate", SqlDbType.Date).Value = AppointmentDate;
                com.Parameters.Add("@JoiningDate", SqlDbType.Date).Value = JoiningDate;
                com.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designation;
                com.Parameters.Add("@SalaryType", SqlDbType.VarChar).Value = SalaryType;
                com.Parameters.Add("@Salary", SqlDbType.Decimal).Value = Validation.NullToZero(Salary);
                com.Parameters.Add("@LeaveCharges", SqlDbType.Decimal).Value = Validation.NullToZero(LeaveCharges);
                com.Parameters.Add("@Overtime", SqlDbType.Decimal).Value = Validation.NullToZero(Overtime);
                com.Parameters.Add("@ExpenseAccount", SqlDbType.VarChar).Value = Validation.NullOrEmptyToDBNull(ExpenseAccount);
                com.Parameters.Add("@PayableAccount", SqlDbType.VarChar).Value = Validation.NullOrEmptyToDBNull(PayableAccount);
                com.Parameters.Add("@Status", SqlDbType.Int).Value = Status;

                con.Open();
                com.ExecuteNonQuery();
                Saved = true;
            }
            return Saved;
        }

        public static bool Payroll_Add_Edit(string VoucherNo, DateTime VDate, string SalaryType, string Description, string status, string User, DataTable Payroll, ref string voucher)
        {
            bool Saved = false;

            using (SqlConnection con = clsConnection.GetConnection())
            using (SqlCommand com = new SqlCommand("Payroll_Add_Edit", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@VoucherNo", SqlDbType.VarChar).Value = Validation.NullOrEmptyToDBNull(VoucherNo);
                com.Parameters.Add("@Vdate", SqlDbType.Date).Value = VDate;
                com.Parameters.Add("@SalaryType", SqlDbType.VarChar).Value = SalaryType;
                com.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description;
                com.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
                com.Parameters.Add("@User", SqlDbType.VarChar).Value = User;

                SqlParameter param = com.Parameters.Add("@payroll", SqlDbType.Structured);
                param.TypeName = "dbo.Payroll";
                param.Value = Payroll;

                SqlParameter Retparam = com.Parameters.Add("@RetVoucherNo", SqlDbType.VarChar, 15);
                Retparam.Direction = ParameterDirection.Output;

                con.Open();
                com.ExecuteNonQuery();
                voucher = com.Parameters["@RetVoucherNo"].Value.ToString();
                Saved = true;
            }
            return Saved;
        }


    }
}
