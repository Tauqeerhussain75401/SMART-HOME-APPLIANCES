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

namespace NuReady
{
    public partial class frmStockAdjustment : Form
    {
        DataTable dtItems = Query.ItemDetail();
        DataTable dtUnits = Query.UnitIndex();
        public frmStockAdjustment()
        {            
            InitializeComponent();
            FillControls.FillcmbNarration(cmbNarration);
            FillControls.FillclnCatagory(clnCatagory);
            FillControls.FillcmbCustomers(cmbFilterAccounts);
            dgvSale.Rows.Add();
        }
        private int currentRow;
        private bool resetRow = false;

        #region enum
        enum SaleMaster
        {
            Vdate,
            VTime,
            Vtype,
            Vno,
            Account,
            Descr,
            Narration,
            CreatedBy,
            CreatedTime,
            EditBy,
            EditTime,
            status,
            Counter
        };
        enum SaleDetail
        {
            Vtype,
            Vno,
            seq,
            fkItem,
            Qty,
            Rate,
            status
        };
          #endregion
       
        void CalcTotAmount()
        {
            decimal TotAmount = 0;
            try
            {
                TotAmount = (from DataGridViewRow row in dgvSale.Rows
                       where row.Cells[clnAmount.Index].Value != null
                       select decimal.Parse(row.Cells[clnAmount.Index].Value.ToString())).Sum();

            }
            catch
            {

            }         
            txtTotAmount.Text = (TotAmount).ToString("N2");
            if (txtVoucherNo.Text == "")
            {
                txtCashReceipt.Text = (TotAmount).ToString("N2");
            }
            
                     
        }
        void Save()
        {
            //if (cmbAccounts.SelectedValue != null)
            //{
            //    string voucher = txtVoucherNo.Text != "" ? txtVoucherNo.Text.Substring(3) : null;
            //    if (dgvSale.Rows.Count > 0)
            //    {
            //        DML.SaleMaster_AddEdit(dtpDate.Value, voucher, "SL", (string)cmbAccounts.SelectedValue, txtDescription.Text,
            //            (string)cmbNarration.SelectedValue,txtCashReceipt.Value,txtCashBack.Value, "001", ref voucher);
            //    }
            //    foreach (DataGridViewRow row in dgvSale.Rows)
            //    {
            //        if((string)row.Cells[clnItemNo.Index].Value != null)
            //        DML.Sale_AddEdit (voucher, "SL", (string)row.Cells[clnSeq.Index].Value,
            //           (string)row.Cells[clnItemNo.Index].Value,
            //           (string)row.Cells[clnUnit.Index].Value,
            //           (string)row.Cells[clnQty.Index].Value,
            //            (string)row.Cells[clnRate.Index].Value,
            //            (string)row.Cells[clnDiscount.Index].Value,
            //            (string)row.Cells[clnStatus.Index].Value);
            //    }
            //    MessageBox.Show("Record Successfully Saved...!");  
            //    FillSale(voucher);
            //    btnNew.Focus();                 
            //}
            //else
            //    MessageBox.Show("Please Select Account...!");  
        }
        void FillQuery(string[] filter)
        {
            dgvQuery.Rows.Clear();
            DataTable dt = Query.SaleInvoices(filter);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvQuery.Rows.Add(((DateTime)dt.Rows[i][SaleMaster.Vdate.ToString()]).ToString("dd-MMM-yyyy"),
                    dt.Rows[i]["vtype"].ToString() + "-" + dt.Rows[i][SaleMaster.Vno.ToString()].ToString(),
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
                dtpDate.Value = (DateTime)ds.Tables[1].Rows[0]["vdate"];
                cmbNarration.SelectedValue = ds.Tables[1].Rows[0]["Narration"];                
                
                txtDescription.Text = (string)Validation.DBNullToNull(ds.Tables[1].Rows[0]["Descr"]);
                txtCreatedBy.Text = ds.Tables[1].Rows[0][SaleMaster.CreatedBy.ToString()].ToString() + " | " + ((DateTime)ds.Tables[1].Rows[0][SaleMaster.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                txtEditBy.Text = ds.Tables[1].Rows[0][SaleMaster.EditBy.ToString()].ToString() != "" ? ds.Tables[1].Rows[0][SaleMaster.EditBy.ToString()].ToString() + " | " + ((DateTime)ds.Tables[1].Rows[0][SaleMaster.EditTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null;
                dgvSale.Rows.Clear();
                DataTable dt = ds.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int ind = dgvSale.Rows.Add(dt.Rows[i]["Seq"].ToString(),null,
                    dtItems.Select("id = '" + dt.Rows[i]["fkItem"].ToString() + "'").FirstOrDefault()["fkitemcatagory"].ToString(),
                      null,
                      null,
                           dt.Rows[i]["Qty"].ToString(),
                           dt.Rows[i]["Rate"].ToString(),
                           dt.Rows[i]["Discount"].ToString(),
                            ((decimal)dt.Rows[i]["amount"]).ToString("N2"),
                           "0"
                           );  
                    DataRow dr = dtItems.Select("id = '" + dt.Rows[i]["fkItem"].ToString() + "'").FirstOrDefault(); 
                    DataRowView drv = dtItems.DefaultView[dtItems.Rows.IndexOf(dr)];
                    SetcmbItemSource(dr["fkItemcatagory"].ToString(), ind);
                    dgvSale[clnItemNo.Index, ind].Value = dr["Id"].ToString();
                    SetcmbUnitSource(drv, ind);
                    dgvSale[clnUnit.Index, ind].Value = dr["DefaultUnit"].ToString();        

                }
                txtCashReceipt.Text = ds.Tables[1].Rows[0]["CashReceipt"].ToString();
                txtCashBack.Text = ds.Tables[1].Rows[0]["CashBack"].ToString();
                dgvSale.Rows.Add();
                CalcTotAmount();
            

        }
        static  bool FLogIn = true; 
        void AllowNewRow()
        {            
            if (dgvSale.CurrentRow.Index ==   dgvSale.Rows[dgvSale.Rows.Count - 1].Index)
            {
                if (dgvSale.CurrentRow.Cells[clnItemNo.Index].Value != null )
                    dgvSale.Rows.Add();
            }
        }
        private void frmPurchase_Load(object sender, EventArgs e)
        {                                              
            FillQuery(new string[] { });
            btnNew_Click(null,null);
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
            if  ( e.KeyCode == Keys .Enter && !dgvSale.Focused  )
            {
                SendKeys.Send("{tab}");  
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
             else  if (dgvSale.CurrentCellAddress.X == clnCatagory.DisplayIndex)
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
             else if (dgvSale.CurrentCellAddress.X == clnUnit.DisplayIndex)
             {
                 ComboBox cmbunit = e.Control as ComboBox;
                 if (cmbunit != null)
                 {
                     //cmbunit.DropDownStyle = ComboBoxStyle.DropDown;
                     //cmbunit.AutoCompleteSource = AutoCompleteSource.ListItems;
                     //cmbunit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                     cmbunit.SelectedIndexChanged += new EventHandler(cmbunit_SelectedIndexChanged);
                 }
             }
             else if (dgvSale.CurrentCell.ColumnIndex == clnRate.Index ||
                 dgvSale.CurrentCell.ColumnIndex == clnQtyIn.Index ||
                 dgvSale.CurrentCell.ColumnIndex == clnQtyOut.Index)
           {
               TextBox tbRate = e.Control as TextBox;
               if (tbRate != null && e.Control.Text != null)
               {
                   tbRate.KeyPress += new KeyPressEventHandler(tbRate_KeyPress);       
               }
           }
           //else if (dgvSale.CurrentCell.ColumnIndex == clnQty.Index)
           //{
           //    TextBox tbQty = e.Control as TextBox;
           //    if (tbQty != null && e.Control.Text != null)
           //    {
           //        tbQty.KeyPress += new KeyPressEventHandler(tbQty_KeyPress);                   
           //    }
           //}
        }

        void cmbunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (dgvSale.CurrentCellAddress.X == clnUnit.DisplayIndex)
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {                   
                    string Filter = string.Format("Id = '{0}'", dgvSale[clnItemNo.Index, dgvSale.CurrentCellAddress.Y].Value.ToString());
                    DataRow dr = dtItems.Select(Filter).FirstOrDefault();
                    dgvSale[clnRate.Index, dgvSale.CurrentCellAddress.Y].Value = (sender as ComboBox).SelectedValue.ToString() == dr["SecondaryUnit"].ToString() ? dr["SecRate"].ToString() : dr["PriRate"].ToString();
                }

            }
        }

        void cmbCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvSale.CurrentCellAddress.X == clnCatagory.DisplayIndex)
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    DataRowView dr = (DataRowView)(sender as ComboBox).SelectedItem;                    
                    if ((sender as ComboBox).SelectedValue == null)
                        (sender as ComboBox).SelectedValue = dr[0].ToString();
                    SetcmbItemSource(dr[0].ToString(), dgvSale.CurrentCellAddress.Y);
                }

            }
        }

        private void SetcmbItemSource(string ItemCatagory,int Rowind)
        {
            DataTable dt = dtItems.Copy();
            dt.Rows.Clear();
            dt = dtItems.Select("fkItemcatagory = " + ItemCatagory).Count() > 0 ? dtItems.Select("fkItemcatagory = " + ItemCatagory).CopyToDataTable() : dt;
            DataGridViewComboBoxCell cmb = dgvSale[clnItemNo.Index,Rowind] as DataGridViewComboBoxCell;
            cmb.DataSource = dt;
            cmb.DisplayMember = "Title";
            cmb.ValueMember = "Id";
        }
        void tbQty_KeyPress(object sender, KeyPressEventArgs e)
        {
           // if (dgvSale.CurrentCell.ColumnIndex == clnQty.Index)
           // {
           //     if ((sender as TextBox).SelectedText.Length > 0)
           //     {
           //         int selind = (sender as TextBox).SelectionStart;
           //         (sender as TextBox).Text = (sender as TextBox).Text.Replace((sender as TextBox).SelectedText, "");
           //         (sender as TextBox).SelectionStart = selind;
           //         (sender as TextBox).SelectionLength = 0;
           //     }
           //     if (!char.IsControl(e.KeyChar)
           //&& !char.IsDigit(e.KeyChar))
           //     {
           //         e.Handled = true;
           //     }
           // }
        }
        void tbRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvSale.CurrentCell.ColumnIndex == clnRate.Index ||
                dgvSale.CurrentCell.ColumnIndex == clnQtyIn.Index ||
                dgvSale.CurrentCell.ColumnIndex == clnQtyOut.Index)
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
                        SetcmbUnitSource(dr, dgvSale.CurrentCellAddress.Y);
                        
                    }
 
                }


            }
            

        }

        private void SetcmbUnitSource(DataRowView dr,int Rowind)
        {
            DataTable dt = dtUnits.Copy();
            dt.Rows.Clear();
            dt = dtUnits.Select("Code in ('" + dr["PrimaryUnit"].ToString() + "','" + dr["SecondaryUnit"].ToString() + "')").Count() == 0 ? dt :
                dtUnits.Select("Code in ('" + dr["PrimaryUnit"].ToString() + "','" + dr["SecondaryUnit"].ToString() + "')").CopyToDataTable();
            DataGridViewComboBoxCell cmb = dgvSale[clnUnit.Index, Rowind] as DataGridViewComboBoxCell;
            cmb.DataSource = dt;
            cmb.DisplayMember = "Title";
            cmb.ValueMember = "Code";
            cmb.Value = dr["DefaultUnit"];
            dgvSale[clnRate.Index, Rowind].Value = dr["DefaultUnit"].ToString() == dr["SecondaryUnit"].ToString() ? dr["SecRate"].ToString() : dr["PriRate"].ToString();
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
            if(e.ColumnIndex == clnItemKey .Index )
            {
                DataRow dr = dtItems.Select("ItemKey = '" + ((string)dgvSale[e.ColumnIndex, e.RowIndex].Value).nullToEmpty ().ToUpper()  + "'").FirstOrDefault();
                 if (dr != null)
                 {
                     DataRowView drv = dtItems.DefaultView[dtItems.Rows.IndexOf(dr)];
                     dgvSale[clnCatagory.Index, e.RowIndex].Value = dr["fkItemcatagory"].ToString();
                     SetcmbItemSource(dr["fkItemcatagory"].ToString(), dgvSale.CurrentCellAddress.Y);
                     dgvSale[clnItemNo.Index, e.RowIndex].Value = dr["Id"].ToString();
                     SetcmbUnitSource(drv, e.RowIndex);
                     dgvSale[clnUnit.Index, e.RowIndex].Value = dr["DefaultUnit"].ToString();                     
                 }
            }
            AllowNewRow();
            resetRow = true;
            currentRow = e.RowIndex;
            decimal QtyIn = 0, QtyOut = 0, Rate = 0;
            if(!decimal.TryParse((string )dgvSale[clnQtyIn.Index  ,e.RowIndex ].Value ,out QtyIn))
            dgvSale[clnQtyIn.Index  ,e.RowIndex ].Value = QtyIn .ToString();
            if (!decimal.TryParse((string)dgvSale[clnQtyOut .Index, e.RowIndex].Value, out QtyOut ))
                dgvSale[clnQtyOut.Index, e.RowIndex].Value = QtyOut.ToString();
            if (!decimal.TryParse((string)dgvSale[clnRate.Index, e.RowIndex].Value, out Rate ))
                dgvSale[clnRate.Index, e.RowIndex].Value = Rate.ToString();
                        
            dgvSale[clnAmount.Index, e.RowIndex].Value = decimal.Round((QtyIn - QtyOut)  * Rate, 2).ToString();
            CalcTotAmount(); 
        }

        private void dgvPurchase_SelectionChanged(object sender, EventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                //dataGridView1.CurrentCell = dataGridView1.Rows[currentRow].Cells[0];
                dgvSale.CurrentCell = dgvSale.Rows[currentRow].Cells[dgvSale.CurrentCell.ColumnIndex];
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to save this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Save();
            }
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            string FDate = txtFdate.Text, TDate = txtTdate.Text, Account = "", Voucher = txtFilterVoucher.Text  ;
            // CashBank = cmbCashBanks.SelectedValue != null ? cmbCashBanks.SelectedValue.ToString() : "";
            //Account = cmbFilterAccounts.SelectedValue != null ? cmbFilterAccounts.SelectedValue.ToString() : "";
            // Narration = cmbFilterNarration.SelectedValue != null ? cmbFilterNarration.SelectedValue.ToString() : "";


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
                //DateTime date = DateTime.ParseExact(dgvQuery.Rows[e.RowIndex].Cells[clnDate.Index].Value.ToString(), "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                FillSale(voucherNo.Substring(3));
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Validation.Clear(this);
            txtTotAmount.Text = "0";
            txtCashReceipt.Text = "0";
            txtCashBack.Text = "0";            
            cmbNarration.SelectedIndex = 0;
        }       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
            
        }

        private void Print()
        {
            if (txtVoucherNo.Text != "")
            {
                //DataSet ds = ReportQuery.SaleBill(txtVoucherNo.Text.Substring(3));
                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //   // frmReportView frm = new frmReportView();
                //    Reports.SaleReceipt rpt = new Reports.SaleReceipt();

                //    rpt.SetDataSource(ds.Tables[0]);
                //    rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                //    rpt.SetParameterValue("@Account", (string)ds.Tables[1].Rows[0]["Title"]);
                //    rpt.SetParameterValue("@VoucherNo", txtVoucherNo.Text);
                //    rpt.SetParameterValue("@Date", (DateTime)ds.Tables[1].Rows[0]["vdate"]);
                //    rpt.SetParameterValue("@User", UserInfo.UserName);
                //    rpt.SetParameterValue("@ServerDate", DateTime.Now);
                //    rpt.SetParameterValue("@CashPaid", (Decimal)ds.Tables[1].Rows[0]["CashReceipt"]);
                //    rpt.SetParameterValue("@CashBack", (Decimal)ds.Tables[1].Rows[0]["CashBack"]);
                //    rpt.PrintOptions.PrinterName = ConfigInfo.ThermalPrinterName; 
                //    rpt.PrintToPrinter(1, true, 0, 0);
                //    //frm.rptViewer.ReportSource = rpt;
                //   // frm.ShowDialog();
                //}
            }
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
           .First();
                    btnNew_Click(null, null);
                    dgvQuery.Rows.Remove(row);
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
            Cursor.Current = Cursors.WaitCursor; 
            if (txtVoucherNo.Text != "")
            {
                //DataSet ds = ReportQuery.SaleBill(txtVoucherNo.Text.Substring(3));
                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    frmReportView frm = new frmReportView();
                //    Reports.SaleBill2 rpt = new Reports.SaleBill2();

                //    rpt.SetDataSource(ds.Tables[0]);
                //    rpt.SetParameterValue("@HeaderLocation", Application .StartupPath + "\\rptHeader.jpg");
                //    //rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                //    rpt.SetParameterValue("@Account", (string)ds.Tables[1].Rows[0]["Title"]);
                //    rpt.SetParameterValue("@VoucherNo", txtVoucherNo.Text);
                //    rpt.SetParameterValue("@VDate", (DateTime)ds.Tables[1].Rows[0]["vdate"]);
                //    rpt.SetParameterValue("@Amount", ds.Tables[1].Rows[0]["Amount"]);
                //    rpt.SetParameterValue("@Discount", ds.Tables[1].Rows[0]["Discount"]);
                //    rpt.SetParameterValue("@NetAmount", ds.Tables[1].Rows[0]["NetAmount"]);
                //    //rpt.SetParameterValue("@User", UserInfo.UserName);
                //    //rpt.SetParameterValue("@ServerDate", DateTime.Now);
                //    //rpt.SetParameterValue("@CashPaid", (Decimal)ds.Tables[1].Rows[0]["CashReceipt"]);
                //    //rpt.SetParameterValue("@CashBack", (Decimal)ds.Tables[1].Rows[0]["CashBack"]);                    
                //    //Cursor.Current = Cursors.Default;
                //    //rpt.PrintToPrinter(); 
                //    frm.rptViewer.ReportSource = rpt;
                //    frm.ShowDialog();
                //}
            }
        }




   
        private void txtCashReceipt_TextChanged(object sender, EventArgs e)
        {

            txtCashBack.Text = (txtCashReceipt.Value - txtTotAmount.Value <= 0 ? 0 : txtCashReceipt.Value - txtTotAmount.Value).ToString("N2");      
            txtBalance.Text = (txtTotAmount.Value - (txtCashReceipt.Value - txtCashBack.Value)).ToString("N2");  
            
        }

        private void txtTotAmount_TextChanged(object sender, EventArgs e)
        {
            txtCashBack.Text = (txtCashReceipt.Value - txtTotAmount.Value <= 0 ? 0 : txtCashReceipt.Value - txtTotAmount.Value).ToString("N2");      
            txtBalance.Text = (txtTotAmount.Value - (txtCashReceipt.Value - txtCashBack.Value)).ToString("N2");  
        }

   

    }
}
