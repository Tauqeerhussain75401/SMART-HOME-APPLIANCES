using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBRInvoice
{
    public class Invoice
    {        
        public string InvoiceNumber = string.Empty;
        public string POSID = "";
        public string USIN = "";
        public DateTime DateTime = DateTime.Now;
        public string BuyerNTN = "";
        public string BuyerCNIC = "";
        public string BuyerName = "";
        public string BuyerPhoneNumber = "";
        public int PaymentMode = 1;
        public double TotalSaleValue = 3180;
        public double TotalQuantity = 3;
        public double TotalBillAmount = 3000;
        public double TotalTaxCharged = 180;
        public double Discount = 1000;
        //public decimal FurtherTax = 100;
        public int InvoiceType = 1;
        public string SaleType { get; set; }
        public List<InvoiceItems> items = new List<InvoiceItems>();
    }
    public class InvoiceItems
    {
        public string ItemCode = "0000";
        public string ItemName = "Item Name";
        public string PCTCode = "11001010";
        public double Quantity = 3;
        public float TaxRate = 17;
        public double SaleValue = 3180;
        public decimal Discount = 500;
        //public decimal FurtherTax = 20;
        public double TaxCharged = 180;
        public double TotalAmount = 3000;
        public int InvoiceType = 1;
        public string RefUSIN = "";
    }
    public class FBRRetInvoice
    {
        
        public string InvoiceNumber;
        public string Code;
        public string Response;
        public string Errors;
    }
}
