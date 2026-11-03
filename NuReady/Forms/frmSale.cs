using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Net.Http;
using QRCoder;
using FBRInvoice;
using System.IO;

namespace NuReady
{
    public partial class frmSale : Form
    {
        DataTable dtItems = Query.ItemDetail();
        DataTable dtUnits = Query.UnitIndex();

        public frmSale()
        {
            InitializeComponent();
            FillControls.FillclnCatagory(clnCatagory);

            dgvSale.Rows.Add();
        }
        private int currentRow;
        private bool resetRow = false;
        public string PostFBRInvoice(string Vno, string CNIC, string ntn, string name, string cell, string Add)
        {
            try
            {

                Invoice objinvoice = new NuReady.Invoice();
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
                    inv.SaleValue = (Convert.ToDouble(dtItems.Rows[i]["GrossAmount"]) - Convert.ToDouble(dtItems.Rows[i]["Discount"]));
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

                objinvoice.BuyerNTN = txtNTN.Text;
                objinvoice.BuyerCNIC = txtCNIC.Text;
                objinvoice.BuyerName = txtName.Text;
                objinvoice.BuyerPhoneNumber = txtCell.Text;

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

                DML.Execute("Update salemaster  set FBRInvoiceNo = '" + retval.InvoiceNumber + "',buyer_cnic='" + CNIC + "',buyer_ntn='" + ntn + "',buyer_name='" + name + "',buyer_cell='" + cell + "',buyer_add='" + Add + "' WHERE Vno = '" + Vno + "'");
                return retval.InvoiceNumber;
            }
            catch (Exception ex)
            {
                try
                {
                    string logPath = Application.StartupPath + "\\ErrorLog.txt";
                    string errorMessage =
                        "Date : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine +
                        "Vno : " + Vno + Environment.NewLine +
                        "Message : " + ex.Message + Environment.NewLine +
                        "StackTrace : " + ex.StackTrace + Environment.NewLine +
                        "-----------------------------------------------------" + Environment.NewLine;

                    File.AppendAllText(logPath, errorMessage);
                }
                catch
                {
                }
                MessageBox.Show(ex.Message);
                return "";
            }
        }
        void CalcTotAmount()
        {
            decimal TotAmount = 0, TotItems = 0, TotQty = 0, TotDisc = 0, TotSST = 0;
            try
            {
                TotAmount = (from DataGridViewRow row in dgvSale.Rows
                             where row.Cells[clnAmount.Index].Value != null
                             select decimal.Parse(row.Cells[clnAmount.Index].Value.ToString())).Sum();

                TotItems = dgvSale.Rows.Cast<DataGridViewRow>()
                            .Count(m => m.Cells[clnAmount.Index].Value != null);

                TotQty = dgvSale.Rows.Cast<DataGridViewRow>()
                            .Sum(m => m.Cells[clnAmount.Index].Value != null
                                      ? decimal.Parse(m.Cells[clnQty.Index].Value.ToString())
                                      : 0);

                TotDisc = dgvSale.Rows.Cast<DataGridViewRow>()
                            .Sum(m => m.Cells[clnDiscPercent.Index].Value != null
                                      ? decimal.Parse(m.Cells[clnDiscPercent.Index].Value.ToString())
                                      : 0);

                TotSST = dgvSale.Rows.Cast<DataGridViewRow>()
                            .Sum(m => m.Cells[clnAmount.Index].Value != null
                                      ? decimal.Parse(m.Cells[clnSST.Index].Value.ToString())
                                      : 0);
            }
            catch
            {
            }

            txtTotAmount.Text = (TotAmount).ToString("N2");
            txtTotQty.Text = (TotQty).ToString("N0");
            txtTotItems.Text = (TotItems).ToString("N0");
            txtTotDisc.Text = (TotDisc).ToString("N2");
            txtTotSST.Text = (TotSST).ToString("N2");
        }
        void Save()
        {
            if (true)
            {
                string voucher = txtVoucherNo.Text != "" ? txtVoucherNo.Text.Substring(3) : null;
                if (dgvSale.Rows.Count > 0)
                {
                    string PaymentMode = "Cash";
                    foreach (RadioButton rdb in grpPaymentMode.Controls)
                    {
                        if (rdb.Checked)
                        {
                            PaymentMode = rdb.Tag.ToString();
                            break;
                        }
                    }
                    DML.SaleMaster_AddEdit(dtpDate.Value, voucher, "SL", (string)cmbAccounts.SelectedValue, txtDescription.Text, PaymentMode,
                        0, 0, "001", ref voucher);
                }
                foreach (DataGridViewRow row in dgvSale.Rows)
                {
                    if ((string)row.Cells[clnItemNo.Index].Value != null)
                        DML.Sale_AddEdit(voucher, "SL", (string)row.Cells[clnSeq.Index].Value,
                           (string)row.Cells[clnItemNo.Index].Value,
                           (string)row.Cells[clnQty.Index].Value,
                            (string)row.Cells[clnRate.Index].Value,
                            (string)row.Cells[clnDiscPercent.Index].Value,
                            (string)row.Cells[clnSST.Index].Value,
                            (string)row.Cells[clnStatus.Index].Value,
                            (string)row.Cells[clnPCTCode.Index].Value);
                }

                MessageBox.Show("Record Successfully Saved...!");
                //FBRInvoice.Class1 obj = new FBRInvoice.Class1();
                //obj.PostFBRInvoice(voucher, txtCNIC.Text, txtNTN.Text, txtName.Text, txtCell.Text, txtAddress.Text);
                PostFBRInvoice(voucher, txtCNIC.Text, txtNTN.Text, txtName.Text, txtCell.Text, txtAddress.Text);
                FillSale(voucher);
                btnNew.Focus();
            }
            else
            {
                MessageBox.Show("Please Select Account...!");
            }
        }
        void FillQuery(string[] filter)
        {
            dgvQuery.Rows.Clear();
            DataTable dt = Query.SaleInvoices(filter);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvQuery.Rows.Add(((DateTime)dt.Rows[i]["Vdate"]).ToString("dd-MMM-yyyy"),
                    dt.Rows[i]["vtype"].ToString() + "-" + dt.Rows[i]["Vno"].ToString(),
                    dt.Rows[i]["fkaccountid"].ToString(),
                    dt.Rows[i]["CreatedBy"].ToString() + " | " + ((DateTime)dt.Rows[i]["CreatedTime"]).ToString("dd-MMM-yyyy hh:mm:ss tt"),
                    dt.Rows[i]["EditBy"].ToString() != "" ? dt.Rows[i]["EditBy"].ToString() + " | " + ((DateTime)dt.Rows[i]["EditTime"]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null);
            }
        }
        private string VoucherNum = null;
        internal void FillSale(string vno)
        {
            DataSet ds = Query.SaleDetail("SL", vno);
            txtVoucherNo.Text = "SL-" + vno;
            VoucherNum = vno;
            txtFBRInvoice.Text = ds.Tables[1].Rows[0]["FBRInvoiceNo"].ToString();
            dtpDate.Value = (DateTime)ds.Tables[1].Rows[0]["vdate"];
            txtDescription.Text = (string)Validation.DBNullToNull(ds.Tables[1].Rows[0]["Descr"]);
            txtCreatedBy.Text = ds.Tables[1].Rows[0]["CreatedBy"].ToString() + " | " + ((DateTime)ds.Tables[1].Rows[0]["CreatedTime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
            txtEditBy.Text = ds.Tables[1].Rows[0]["EditBy"].ToString() != "" ? ds.Tables[1].Rows[0]["EditBy"].ToString() + " | " + ((DateTime)ds.Tables[1].Rows[0]["EditTime"]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null;
            txtCNIC.Text = ds.Tables[1].Rows[0]["buyer_cnic"].ToString();
            txtNTN.Text = ds.Tables[1].Rows[0]["buyer_ntn"].ToString();
            txtName.Text = ds.Tables[1].Rows[0]["buyer_name"].ToString();
            txtCell.Text = ds.Tables[1].Rows[0]["buyer_cell"].ToString();
            txtAddress.Text = ds.Tables[1].Rows[0]["buyer_add"].ToString();
            foreach (RadioButton rdb in grpPaymentMode.Controls)
            {
                if (rdb.Tag.ToString() == ds.Tables[1].Rows[0]["PaymentMode"].ToString())
                {
                    rdb.Checked = true;
                    break;
                }
            }
            dgvSale.Rows.Clear();
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int ind = dgvSale.Rows.Add(
                        dt.Rows[i]["Seq"].ToString(),
                        null,
                        dtItems.Select("id = '" + dt.Rows[i]["fkItem"].ToString() + "'")
                               .FirstOrDefault()?["fkitemcatagory"].ToString(),
                        null,
                        dt.Rows[i]["Qty"].ToString(),
                        dt.Rows[i]["GrossRate"].ToString(),
                        (((decimal)dt.Rows[i]["Discount"])),
                        dt.Rows[i]["Discount"].ToString(),
                        dt.Rows[i]["taxcharged"].ToString(),
                        ((decimal)dt.Rows[i]["amount"]).ToString("N2"),
                        "0",
                        dt.Rows[i]["pctcode"].ToString()
                    );
                DataRow dr = dtItems.Select("id = '" + dt.Rows[i]["fkItem"].ToString() + "'").FirstOrDefault();
                DataRowView drv = dtItems.DefaultView[dtItems.Rows.IndexOf(dr)];
                SetcmbItemSource(dr["fkItemcatagory"].ToString(), ind);
                dgvSale[clnItemNo.Index, ind].Value = dr["Id"].ToString();
                dgvSale[clnRate.Index, ind].Value = dt.Rows[i]["GrossRate"].ToString();
            }

            dgvSale.Rows.Add();
            CalcTotAmount();
        }
        static bool FLogIn = true;
        void AllowNewRow()
        {
            if (dgvSale.CurrentRow.Index == dgvSale.Rows[dgvSale.Rows.Count - 1].Index)
            {
                if (dgvSale.CurrentRow.Cells[clnItemNo.Index].Value != null)
                {
                    int a = dgvSale.Rows.Add();

                }
            }
        }
        private Bitmap RenderQrCode(string Code)
        {
            Bitmap img;
            string level = "L";
            QRCodeGenerator.ECCLevel eccLevel = (QRCodeGenerator.ECCLevel)(level == "L" ? 0 : level == "M" ? 1 : level == "Q" ? 2 : 3);
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(Code, eccLevel))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {

                        img = qrCode.GetGraphic(20, Color.Black, Color.White, null);
                    }
                }
            }
            return img;
        }
        private Image GenBarCode(string code)
        {
            code = code == "" ? "12345" : code;
            Image img;
            Zen.Barcode.Code128BarcodeDraw brcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            img = brcode.Draw(code, 60);
            return img;
        }
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        private void Print(string vno, string Prv)
        {
            frmReportView frm = new frmReportView();
            DataSet dsdb = Query.SaleReceiptDetail("SL", vno);
            DataTable dt, dtHead;
            dt = dsdb.Tables[0].Copy();
            dtHead = dsdb.Tables[1];
            Reports.SaleReceipt rpt = new Reports.SaleReceipt();
            DataSet ds = new DataSet();
            ds.DataSetName = "rptdataset";

            DataTable dtQR = new DataTable();
            DataTable dtBar = new DataTable();
            dt.TableName = "SaleBill";
            dtQR.TableName = "QRCode";
            dtBar.TableName = "BarCode";
            //
            dtQR.Columns.Add("QR", typeof(byte[]));
            dtBar.Columns.Add("BR", typeof(byte[]));
            //
            string FBRNo = dtHead.Rows[0]["FBRInvoiceNo"].ToString();
            dtQR.Rows.Add(ImageToByte(RenderQrCode(FBRNo)));
            dtBar.Rows.Add(ImageToByte(GenBarCode(FBRNo)));
            //
            ds.Tables.Add(dt);
            ds.Tables.Add(dtQR);
            ds.Tables.Add(dtBar);



            rpt.Database.Tables[0].SetDataSource(ds.Tables[0]);
            rpt.Database.Tables[1].SetDataSource(ds.Tables[1]);
            rpt.Database.Tables[2].SetDataSource(ds.Tables[2]);
            rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);

            rpt.SetParameterValue("@VoucherNo", Convert.ToDateTime(dtHead.Rows[0]["Vdate1"]).ToString("yyMMdd") + vno.PadLeft(7, '0'));
            rpt.SetParameterValue("@Date", (DateTime)dtHead.Rows[0]["Vdate1"]);
            rpt.SetParameterValue("@User", dtHead.Rows[0]["CreatedBy"].ToString());
            rpt.SetParameterValue("@ServerDate", (DateTime)dtHead.Rows[0]["Serverdate"]);
            rpt.SetParameterValue("@FBRInvoice", FBRNo);
            rpt.SetParameterValue("@TotalAmount", dtHead.Rows[0]["Amount"]);
            rpt.SetParameterValue("@PaymentType", dtHead.Rows[0]["paymentMode"].ToString());
            rpt.SetParameterValue("@Paid", dtHead.Rows[0]["NetAmount"]);
            rpt.SetParameterValue("@Balance", 0);
            rpt.SetParameterValue("@Head", CompanyInfo.Address);
            rpt.SetParameterValue("@ntn", CompanyInfo.ntn);
            rpt.SetParameterValue("@ComSTRN", CompanyInfo.strn);

            rpt.SetParameterValue("@PosServiceFees", dtHead.Rows[0]["posservicefees1"]);

            rpt.SetParameterValue("@cnic", txtCNIC.Text);
            rpt.SetParameterValue("@name", txtName.Text);
            rpt.SetParameterValue("@Cntn", txtNTN.Text);
            rpt.SetParameterValue("@cell", txtCell.Text);
            rpt.SetParameterValue("@Address", txtAddress.Text);
            rpt.SetParameterValue("@Discount", Convert.ToInt32(dt.Rows.Cast<DataRow>().Sum(s => (decimal)s["qty"] * (decimal)s["discount"])));
            rpt.SetParameterValue("@returnText", "");
            rpt.ReportHeaderSection2.SectionFormat.EnableSuppress = true;
            frm.rptViewer.ReportSource = rpt;
            frm.Show();
            if (Prv == "print")
            {
                rpt.PrintToPrinter(1, true, 1, 1);
            }
        }
        private void frmPurchase_Load(object sender, EventArgs e)
        {
            FillQuery(new string[] { });

            FLogIn = false;
        }
        private void dgvPurchase_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            int seq = 0;
            try
            {
                seq = (int)(from DataGridViewRow row in dgvSale.Rows
                            where row.Cells[clnSeq.Index].Value != null && row.Cells[clnSeq.Index].Value.ToString() != ""
                            select int.Parse(row.Cells[clnSeq.Index].Value.ToString())).Max();
            }
            catch
            {
            }
            if (dgvSale.Rows[e.RowIndex].Cells[clnSeq.Index].Value == null)
            {
                dgvSale.Rows[e.RowIndex].Cells[clnSeq.Index].Value = (seq + 1).ToString();
                dgvSale.Rows[e.RowIndex].Cells[clnStatus.Index].Value = "0";
            }
        }
        private void frmPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !dgvSale.Focused)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{tab}");
            }
            else if (e.KeyCode == Keys.F1)
            {
                Print(txtVoucherNo.Text.Substring(3), "print");
            }
            else if (e.KeyCode == Keys.F5)
            {
                Save();
            }

        }
        private void dgvPurchase_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {


            if (dgvSale.CurrentCellAddress.X == clnItemNo.DisplayIndex)
            {
                ComboBox cmbItem = e.Control as ComboBox;
                if (cmbItem != null)
                {
                    cmbItem.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbItem.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbItem.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbItem.SelectedIndexChanged += new EventHandler(cmbItem_SelectedIndexChanged);
                }
            }
            else if (dgvSale.CurrentCellAddress.X == clnCatagory.DisplayIndex)
            {
                ComboBox cmbCatagory = e.Control as ComboBox;
                if (cmbCatagory != null)
                {
                    cmbCatagory.DropDownStyle = ComboBoxStyle.DropDown;
                    cmbCatagory.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbCatagory.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbCatagory.SelectedIndexChanged += new EventHandler(cmbCatagory_SelectedIndexChanged);
                }
            }

            else if (dgvSale.CurrentCell.ColumnIndex == clnRate.Index ||
                dgvSale.CurrentCell.ColumnIndex == clnQty.Index ||
                dgvSale.CurrentCell.ColumnIndex == clnDiscount.Index ||
                dgvSale.CurrentCell.ColumnIndex == clnDiscPercent.Index)
            {
                TextBox tbRate = e.Control as TextBox;
                if (tbRate != null && e.Control.Text != null)
                {
                    tbRate.KeyPress += new KeyPressEventHandler(tbRate_KeyPress);
                }
            }

        }
        private void CmbItem_Validated(object sender, EventArgs e)
        {
            dgvSale.CurrentRow.Cells[dgvSale.CurrentCell.ColumnIndex].Value = (sender as ComboBox).SelectedValue.ToString();
        }
        void cmbCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvSale.CurrentCellAddress.X == clnCatagory.DisplayIndex)
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    DataRowView dr = (DataRowView)(sender as ComboBox).SelectedItem;
                    dgvSale[dgvSale.CurrentCellAddress.X, dgvSale.CurrentCellAddress.Y].Value = dr[0].ToString();
                    SetcmbItemSource(dr[0].ToString(), dgvSale.CurrentCellAddress.Y);
                }

            }
        }
        private void SetcmbItemSource(string ItemCatagory, int Rowind)
        {
            DataTable dt = dtItems.Copy();
            dt.Rows.Clear();
            dt = dtItems.Select("fkItemcatagory = " + ItemCatagory).Count() > 0 ? dtItems.Select("fkItemcatagory = " + ItemCatagory).CopyToDataTable() : dt;
            DataGridViewComboBoxCell cmb = dgvSale[clnItemNo.Index, Rowind] as DataGridViewComboBoxCell;
            cmb.DataSource = dt;
            cmb.DisplayMember = "Title";
            cmb.ValueMember = "Id";
        }
        void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        void tbRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvSale.CurrentCell.ColumnIndex == clnRate.Index ||
                dgvSale.CurrentCell.ColumnIndex == clnQty.Index ||
                dgvSale.CurrentCell.ColumnIndex == clnDiscount.Index ||
                 dgvSale.CurrentCell.ColumnIndex == clnDiscPercent.Index)
            {
                if ((sender as TextBox).SelectedText.Length > 0)
                {
                    int selind = (sender as TextBox).SelectionStart;
                    (sender as TextBox).Text = (sender as TextBox).Text.Replace((sender as TextBox).SelectedText, "");
                    (sender as TextBox).SelectionStart = selind;
                    (sender as TextBox).SelectionLength = 0;
                }
                if (!char.IsControl(e.KeyChar)
           && !char.IsDigit(e.KeyChar)
           && !((sender as TextBox).Text.Count(a => a == '.') == 0 && e.KeyChar == '.'))
                {
                    e.Handled = true;
                }
            }
        }
        void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvSale.CurrentCellAddress.X == clnItemNo.DisplayIndex)
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    DataRowView dr = (DataRowView)(sender as ComboBox).SelectedItem;
                    if ((sender as ComboBox).SelectedValue == null)
                        (sender as ComboBox).SelectedValue = dr[0].ToString();
                    if (dr != null)
                    {
                        if (txtVoucherNo.Text == "")
                        {
                            dgvSale.Rows[dgvSale.CurrentCellAddress.Y].Cells[clnDiscPercent.Index].Value = "0";
                            dgvSale.Rows[dgvSale.CurrentCellAddress.Y].Cells[clnRate.Index].Value = dr["PriRate"].ToString();
                            dgvSale.Rows[dgvSale.CurrentCellAddress.Y].Cells[clnPCTCode.Index].Value = dr["pctcode"].ToString();
                        }
                    }
                }
            }
        }
        private void dgvPurchase_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
        private void dgvPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{tab}");
            }
        }
        private void dgvPurchase_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == clnItemKey.Index)
            {
                DataRow dr = dtItems.Select("ItemKey = '" + ((string)dgvSale[e.ColumnIndex, e.RowIndex].Value).nullToEmpty().ToUpper() + "'").FirstOrDefault();
                if (dr != null)
                {
                    DataRowView drv = dtItems.DefaultView[dtItems.Rows.IndexOf(dr)];
                    dgvSale[clnCatagory.Index, e.RowIndex].Value = dr["fkItemcatagory"].ToString();
                    SetcmbItemSource(dr["fkItemcatagory"].ToString(), dgvSale.CurrentCellAddress.Y);
                    dgvSale[clnItemNo.Index, e.RowIndex].Value = dr["Id"].ToString();

                }
            }
            AllowNewRow();
            resetRow = true;
            currentRow = e.RowIndex;
            decimal Qty = 0, Rate = 0, Discount = 0, Amount = 0;
            if (!decimal.TryParse(Convert.ToString(dgvSale[clnQty.Index, e.RowIndex].Value), out Qty))
                dgvSale[clnQty.Index, e.RowIndex].Value = Qty.ToString();

            if (!decimal.TryParse(Convert.ToString(dgvSale[clnRate.Index, e.RowIndex].Value), out Rate))
                dgvSale[clnRate.Index, e.RowIndex].Value = Rate.ToString();

            if (!decimal.TryParse(Convert.ToString(dgvSale[clnDiscPercent.Index, e.RowIndex].Value), out Discount))
                dgvSale[clnDiscPercent.Index, e.RowIndex].Value = Discount.ToString();

            decimal netRate = (Rate * Qty) - Discount;
            if (netRate < 0) netRate = 0;
            Amount = decimal.Round(Qty * netRate, 2);
            decimal sst = decimal.Round(netRate * 18 / 100, 2);
            dgvSale[clnSST.Index, e.RowIndex].Value = sst.ToString();
            dgvSale[clnAmount.Index, e.RowIndex].Value = (netRate + sst).ToString();
            CalcTotAmount();
        }
        private void dgvPurchase_SelectionChanged(object sender, EventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                dgvSale.CurrentCell = dgvSale.Rows[currentRow].Cells[dgvSale.CurrentCell.ColumnIndex];
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvSale.Rows.Count == 1)
            {
                MessageBox.Show("Missing Sales", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to save this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Save();
                }
            }

        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            string FDate = txtFdate.Text, TDate = txtTdate.Text, Account = "", Voucher = txtFilterVoucher.Text;
            FillQuery(new string[] { FDate != "" ? " and Vdate >= '" + FDate + "'" : "",
                TDate != "" ? " and Vdate <= '" + TDate + "'" : "",
            Account != "" ? " and fkaccountid = '" + Account  + "'" : "",
            Voucher  != "" ? " and Vno = '" + Voucher.Substring(4)    + "'" : "",
            });
        }
        private void txtFdate_Validated(object sender, EventArgs e)
        {
            try
            {
                string text = (sender as TextBox).Text.Replace(" ", "-").Replace("/", "-");
                DateTime date = DateTime.ParseExact(text, Validation.dateformats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                (sender as TextBox).Text = date.ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {
                (sender as TextBox).Text = "";
            }
        }
        private void txtFilterVoucher_Validated(object sender, EventArgs e)
        {
            txtFilterVoucher.Text = txtFilterVoucher.Text.ToUpper();
            if (!Regex.IsMatch(txtFilterVoucher.Text, @"SL-\d{5}"))
                txtFilterVoucher.Text = "";


        }
        private void dgvQuery_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!FLogIn)
            {
                string voucherNo = dgvQuery.Rows[e.RowIndex].Cells[clnVoucherNum.Index].Value.ToString();
                tbSaleQuery.SelectedTab = tbDetail;
                FillSale(voucherNo.Substring(3));
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Validation.Clear(this);
            txtCell.Clear();
            txtCNIC.Clear();
            txtNTN.Clear();

            dtpDate.Value = DateTime.Now;

            txtCNIC.Focus();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print(txtVoucherNo.Text.Substring(3), "print");

        }

        enum Navigators { Up, Down, Home, End };
        void Navigate(Navigators Nav)
        {
            if (dgvQuery.CurrentRow != null && tbSaleQuery.SelectedTab == tbDetail)
            {
                int rowIndex = dgvQuery.CurrentRow.Index;
                if (Nav == Navigators.Down && dgvQuery.Rows.Count - 1 > rowIndex)
                {
                    dgvQuery.CurrentCell = dgvQuery.Rows[rowIndex + 1].Cells[clnVoucherNum.Index];
                    string voucherNo = dgvQuery.CurrentCell.Value.ToString();
                    FillSale(voucherNo.Substring(3));
                }
                else if (Nav == Navigators.Up && 0 < rowIndex)
                {
                    dgvQuery.CurrentCell = dgvQuery.Rows[rowIndex - 1].Cells[clnVoucherNum.Index];
                    string voucherNo = dgvQuery.CurrentCell.Value.ToString();
                    FillSale(voucherNo.Substring(3));
                }
                else if (Nav == Navigators.Home)
                {
                    dgvQuery.CurrentCell = dgvQuery.Rows[0].Cells[clnVoucherNum.Index];
                    string voucherNo = dgvQuery.CurrentCell.Value.ToString();
                    FillSale(voucherNo.Substring(3));
                }
                else if (Nav == Navigators.End)
                {
                    dgvQuery.CurrentCell = dgvQuery.Rows[dgvQuery.Rows.Count - 1].Cells[clnVoucherNum.Index];
                    string voucherNo = dgvQuery.CurrentCell.Value.ToString();
                    FillSale(voucherNo.Substring(3));
                }
            }
        }
        #region Navigation
        private void btnHome_Click(object sender, EventArgs e)
        {
            Navigate(Navigators.Home);
        }

        private void btnPri_Click(object sender, EventArgs e)
        {
            Navigate(Navigators.Up);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Navigate(Navigators.Down);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Navigate(Navigators.End);
        }
        #endregion
        private void tbSaleQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageDown)
            {
                Navigate(Navigators.Down);
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                Navigate(Navigators.Up);
            }
            else if (e.KeyCode == Keys.Home)
            {
                Navigate(Navigators.Home);
            }
            else if (e.KeyCode == Keys.End)
            {
                Navigate(Navigators.End);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtVoucherNo.Text != "")
            {
                if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DML.Change_SL_Status(txtVoucherNo.Text.Substring(3), txtVoucherNo.Text.Substring(0, 2), null, "1");

                    DataGridViewRow row = dgvQuery.Rows.Cast<DataGridViewRow>()
           .Where(r => r.Cells[clnVoucherNum.Index].Value.ToString().Equals(txtVoucherNo.Text))
           .FirstOrDefault();
                    btnNew_Click(null, null);
                    if (row != null)
                    {
                        dgvQuery.Rows.Remove(row);
                    }

                    CalcTotAmount();
                    MessageBox.Show("Record Successfully Deleted..!");
                }
            }
        }

        private void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dgvSale[clnSeq.Index, dgvSale.CurrentRow.Index].Value != null)
                {
                    if (txtVoucherNo.Text != "")
                    {
                        DML.Change_SL_Status(txtVoucherNo.Text.Substring(3), txtVoucherNo.Text.Substring(0, 2), dgvSale[clnSeq.Index, dgvSale.CurrentRow.Index].Value.ToString(), "1");
                    }
                    dgvSale.Rows.RemoveAt(dgvSale.CurrentRow.Index);
                    CalcTotAmount();
                    MessageBox.Show("Record Successfully Deleted..!");
                }
            }
        }

        private void dgvSale_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvSale.Rows[e.RowIndex].Selected = true;
                dgvSale.CurrentCell = this.dgvSale.Rows[e.RowIndex].Cells[e.ColumnIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbDetail_Click(object sender, EventArgs e)
        {

        }

        private void dgvSale_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            Print(txtVoucherNo.Text.Substring(3), "p");
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void grpInvoiceDetail_Enter(object sender, EventArgs e)
        {

        }
    }
}
