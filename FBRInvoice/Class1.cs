using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data;

namespace FBRInvoice
{
    public class Class1
    {

        public string PostFBRInvoice(string Vno,string CNIC,string ntn,string name,string cell,string Add)
        {
            Invoice objinvoice = new FBRInvoice.Invoice();
            DataSet ds = Query.SaleDetail("SL", Vno);
            DataTable dtHead = ds.Tables[1];
            DataTable dtItems = ds.Tables[0];
            if (dtHead.Rows[0]["FBRInvoiceNo"].ToString() != "")
            {
                return dtHead.Rows[0]["FBRInvoiceNo"].ToString();
            }
            List<InvoiceItems> it = new List<InvoiceItems>();
            for (int i = 0; i < dtItems.Rows.Count; i++)
            {
                InvoiceItems inv = new InvoiceItems();
                inv.ItemCode = dtItems.Rows[i]["fkItem"].ToString();
                inv.ItemName = dtItems.Rows[i]["ItemTitle"].ToString();
                inv.PCTCode = dtItems.Rows[i]["pctcode"].ToString(); 
                   
                inv.Quantity = Convert.ToDouble(dtItems.Rows[i]["Qty"]);
                inv.TaxRate = float.Parse(dtItems.Rows[i]["TaxRate"].ToString());
                inv.SaleValue = Convert.ToDouble(dtItems.Rows[i]["GrossAmount"]);
                inv.Discount = (decimal)dtItems.Rows[i]["Discount"];
                inv.TaxCharged = Convert.ToDouble(dtItems.Rows[i]["TaxCharged"]);
                inv.TotalAmount = Convert.ToDouble(dtItems.Rows[i]["amount"]);
                inv.InvoiceType = 1;
                inv.RefUSIN = "";
                it.Add(inv);
            }
            objinvoice.POSID = SoftwareInfo.POSID;
            objinvoice.USIN = dtHead.Rows[0]["Vno"].ToString();
            objinvoice.DateTime = (DateTime)dtHead.Rows[0]["Vdate"];
            objinvoice.BuyerNTN =       ntn;
            objinvoice.BuyerCNIC =      CNIC;
            objinvoice.BuyerName =      name;             
            objinvoice.BuyerPhoneNumber=cell ;

            objinvoice.TotalSaleValue = Convert.ToDouble(dtHead.Rows[0]["Amount"]);
            objinvoice.TotalQuantity = it.Sum(m => m.Quantity);
            objinvoice.TotalTaxCharged = it.Sum(m => m.TaxCharged);
            objinvoice.Discount = Convert.ToDouble(dtHead.Rows[0]["Discount"]);
            objinvoice.TotalBillAmount = Convert.ToDouble(dtHead.Rows[0]["NetAmount"]);
            objinvoice.PaymentMode = dtHead.Rows[0]["PaymentMode"].ToString() == "Cash" ? 1 :
                dtHead.Rows[0]["PaymentMode"].ToString() == "Credit" ? 5 :
                dtHead.Rows[0]["PaymentMode"].ToString() == "Credit Card" ? 2 :
                dtHead.Rows[0]["PaymentMode"].ToString() == "Voucher" ? 3 : 0;
        
            objinvoice.InvoiceType = 1;
            objinvoice.SaleType = "T1000023";
            objinvoice.items = it;
            HttpClient Client = new HttpClient();
            string jsonstr = JsonConvert.SerializeObject(objinvoice);
            var content = new StringContent(jsonstr, Encoding.UTF8, "application/json");
        
            HttpResponseMessage response = Client.PostAsync("http://localhost:8524/api/IMSFiscal/GetInvoiceNumberByModel", content).Result;
            FBRRetInvoice retval = JsonConvert.DeserializeObject<FBRRetInvoice>(response.Content.ReadAsStringAsync().Result);
        
            DML.Execute("Update salemaster  set FBRInvoiceNo = '" + retval.InvoiceNumber + "',buyer_cnic='"+CNIC+"',buyer_ntn='"+ntn+"',buyer_name='"+name+"',buyer_cell='"+cell+"',buyer_add='"+Add+"' WHERE Vno = '" + Vno + "'");
            return retval.InvoiceNumber;
        }
    }
}
