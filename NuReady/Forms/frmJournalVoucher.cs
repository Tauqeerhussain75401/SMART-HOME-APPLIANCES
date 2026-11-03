using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace NuReady
{
    public partial class frmJournalVoucher : Form
    {
        enum GL
        {
            VDate,
            Vtime,
            VoucherNo,
            Vtype,
            Vseq,
            DRAccount,
            Amount,
            CRAccount,
            Narration,
            Remarks,
            CheckNum,
            CheckDate,
            CheckStatus,
            Clear,
            status,
            CreatedBy,
            CreatedTime,
            EditBy,
            EditTime
        };
        public frmJournalVoucher()
        {
            InitializeComponent();
            FillAccount();
            FillNarration();
            FillFilterAccount();
            FillFilterNarration();
            dgvJournal.Rows.Add();
        }
        private int currentRow;
        private bool resetRow = false  ;
        bool FLogIn = true;
        private string VoucherNum = null;
        void ShowDrBal(string Title,string account)
        {
            Decimal bal = Query.Balance(account);
            lblDrBalance.Text = Title + " = " + (bal >= 0 ? bal.ToString("N2") + " Dr" : (-bal).ToString("N2") + " Cr");              
        }
        void ShowCrBal(string Title, string account)
        {
            Decimal bal = Query.Balance(account);
            lblCrBalance.Text = Title + " = " + (bal >= 0 ? bal.ToString("N2") + " Dr" : (-bal).ToString("N2") + " Cr");                          
        }
        DataTable dtAccount = Query.Accounts();
        DataTable dtNarration = Query.NarrationIndex();
        DataTable dtCashBanks = Query.CashBanks();        
        #region Fill Controls
        DataTable dtQuery;
         void FillQuery(string[] filter )
        {
            dgvQuery.Rows.Clear();
            dtQuery = Query.VoucherQuery("JV", filter);
            for (int i = 0; i < dtQuery.Rows.Count; i++)
            {
                dgvQuery.Rows.Add(((DateTime)dtQuery.Rows[i][GL.VDate.ToString()]).ToString("dd-MMM-yyyy"),
                      dtQuery.Rows[i][GL.Vtype.ToString()].ToString() + "-" + dtQuery.Rows[i][GL.VoucherNo.ToString()].ToString(),
                      dtQuery.Rows[i][GL.Amount.ToString()].ToString(),
                      dtQuery.Rows[i][GL.Narration.ToString()].ToString(),
                      dtQuery.Rows[i][GL.CreatedBy.ToString()].ToString() + " | " + ((DateTime)dtQuery.Rows[i][GL.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt"),
                      dtQuery.Rows[i][GL.EditBy.ToString()].ToString() != "" ? dtQuery.Rows[i][GL.EditBy.ToString()].ToString() + " | " + ((DateTime)dtQuery.Rows[i][GL.EditTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null);      
            }
            

        
        }
        internal  void FillJournal(string vno)
        {
            DataTable dt ;
            if (cmbFilterAccounts.SelectedValue != null)
                dt = Query.VoucherDetail("JV", vno, cmbFilterAccounts.SelectedValue.ToString());
            else
                dt = Query.VoucherDetail("JV", vno);

            txtVoucherNo.Text = "JV-" + vno;
            VoucherNum = vno;
            dgvJournal.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                dtpDate.Value = (DateTime)dt.Rows[0][GL.VDate.ToString()];
                cmbNarration.SelectedValue = dt.Rows[0][GL.Narration.ToString()];
                txtCreatedBy.Text = dt.Rows[0][GL.CreatedBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[0][GL.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                txtEditBy.Text = dt.Rows[0][GL.EditBy.ToString()].ToString() != "" ? dt.Rows[0][GL.EditBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[0][GL.EditTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null;               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvJournal.Rows.Add(dt.Rows[i][GL.Vseq.ToString()].ToString(),
                        dt.Rows[i][GL.DRAccount.ToString()].ToString(),
                        ((decimal)(dt.Rows[i][GL.Amount.ToString()])).ToString("N0"),
                        dt.Rows[i][GL.CRAccount.ToString()].ToString(),
                        dt.Rows[i][GL.Remarks.ToString()].ToString(),
                        dt.Rows[i][GL.CreatedBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[i][GL.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt"),
                        dt.Rows[i][GL.EditBy.ToString()].ToString() != "" ? dt.Rows[i][GL.EditBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[i][GL.EditTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null);
                }
                dgvJournal.Rows.Add();
                CalcTotAmount();
            }

        }
        void FillAccount()
        {
            clnDrAccount.DataSource = dtAccount.Copy();
            clnDrAccount.DisplayMember = "Title";
            clnDrAccount.ValueMember = "Account";
            clnCrAccount.DataSource = dtAccount.Copy();
            clnCrAccount.DisplayMember = "Title";
            clnCrAccount.ValueMember = "Account";
        }
        void FillNarration()
        {
            cmbNarration.DataSource = dtNarration.Copy();  
            cmbNarration.DisplayMember = "Title";
            cmbNarration.ValueMember = "Code"; 
        }
        void FillFilterAccount()
        {
            cmbFilterAccounts.DataSource = dtAccount.Copy();
            cmbFilterAccounts.DisplayMember = "Title";
            cmbFilterAccounts.ValueMember = "Account";
            cmbFilterAccounts.SelectedIndex = -1;
        }
   
        void FillFilterNarration()
        {
            cmbFilterNarration.DataSource = dtNarration.Copy();
            cmbFilterNarration.DisplayMember = "Title";
            cmbFilterNarration.ValueMember = "Code";
            cmbFilterNarration.SelectedIndex = -1;
        }
        #endregion
       
        void AllowNewRow()
        {
            if (dgvJournal.CurrentRow.Index == dgvJournal.Rows[dgvJournal.Rows.Count - 1].Index)
            {
                if (dgvJournal.CurrentRow.Cells[clnDrAccount.Index].Value != null &&
                    dgvJournal.CurrentRow.Cells[clnCrAccount.Index].Value != null &&
                    dgvJournal.CurrentRow.Cells[clnAmount.Index].Value != null )
                    dgvJournal.Rows.Add();   
            }
        }
        private void frmReceipt_Load(object sender, EventArgs e)
        {                       
            FillQuery(new string[]{} );
            if (dtQuery.Rows.Count > 0 && txtVoucherNo.Text == "")
            {
                string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                FillJournal(voucherNo);
            }    
            FLogIn = false ;
        }
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{tab}");
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            AllowNewRow(); 
            resetRow = true;
            currentRow = e.RowIndex;

        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {        
            if (resetRow)
            {
                resetRow = false;
                //dataGridView1.CurrentCell = dataGridView1.Rows[currentRow].Cells[0];
                dgvJournal.CurrentCell = dgvJournal.Rows[currentRow].Cells[dgvJournal.CurrentCell.ColumnIndex];
            }

           
        }
        void CalcTotAmount()
        {
            decimal Tot = 0;
            try
            {
                Tot = (from DataGridViewRow row in dgvJournal.Rows
                       where row.Cells[clnAmount.Index].Value != null
                       select decimal.Parse(row.Cells[clnAmount.Index].Value.ToString())).Sum();
   
            }
            catch 
            {
                
            }
            lblTotAmount.Text = Tot.ToString();  
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvJournal.CurrentCellAddress.X == clnDrAccount.DisplayIndex)
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.SelectedIndexChanged += new EventHandler(cbDr_SelectedIndexChanged);
                }
            }
            if (dgvJournal.CurrentCellAddress.X == clnCrAccount.DisplayIndex)
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {

                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.SelectedIndexChanged += new EventHandler(cbCr_SelectedIndexChanged);
                }
            }
            if (dgvJournal.CurrentCell.ColumnIndex == clnAmount.Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null && e.Control.Text  != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                }
            }
        }
        void cbDr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvJournal.CurrentCell.ColumnIndex == clnDrAccount.Index)
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    DataRowView dr = (DataRowView)(sender as ComboBox).SelectedItem;
                    ShowDrBal(dr[1].ToString(), dr[0].ToString());
                    if ((sender as ComboBox).SelectedValue == null)
                        (sender as ComboBox).SelectedValue = dr[0].ToString();
                }
            }
        }
        void cbCr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvJournal.CurrentCell.ColumnIndex == clnCrAccount.Index)
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    DataRowView dr = (DataRowView)(sender as ComboBox).SelectedItem;
                    ShowCrBal(dr[1].ToString(), dr[0].ToString());
                    if ((sender as ComboBox).SelectedValue == null)
                        (sender as ComboBox).SelectedValue = dr[0].ToString();
                }
            }
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvJournal.CurrentCell.ColumnIndex == clnAmount.Index)
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
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {          
            CalcTotAmount(); 
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int seq = 0;
            try
            {                      
            seq = (int)(from DataGridViewRow row in dgvJournal.Rows
                         where row.Cells[clnSeq.Index].Value != null && row.Cells[clnSeq.Index].Value.ToString()  != ""
                    select int.Parse (row.Cells[clnSeq.Index].Value.ToString()  )).Max();            
            }
            catch 
            {
            }
            if (dgvJournal.Rows[e.RowIndex].Cells[clnSeq.Index].Value == null)
            dgvJournal.Rows[e.RowIndex].Cells[clnSeq.Index].Value = (seq + 1).ToString();            
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to save this...!","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question  ) == DialogResult .Yes)
            {
                string voucher = null;
                voucher = txtVoucherNo.Text == "" ? null : txtVoucherNo.Text.Split('-')[1];
                    foreach (DataGridViewRow row in dgvJournal.Rows)
                    {
                        if (row.Cells[clnDrAccount.Index].Value != null &&
                            row.Cells[clnCrAccount.Index].Value != null &&
                            row.Cells[clnAmount.Index].Value != null)
                        {
                            DML.GL_Add_Edit(dtpDate.Value, voucher, "JV", (string)row.Cells[clnSeq.Index].Value,
                                row.Cells[clnDrAccount.Index].Value.ToString(), decimal.Parse(row.Cells[clnAmount.Index].Value.ToString()).ToString(),
                                row.Cells[clnCrAccount.Index].Value.ToString(), cmbNarration.SelectedValue.ToString(),
                                (string)row.Cells[clnRemarks.Index].Value,null,
                                null,
                                null, "0", "0", ref voucher);
                        }
                    }
                    FillJournal(voucher);
                    if (dtQuery.Select("voucherno = '" + voucher + "'").Count() == 0)
                    {
                        DataRow dr = dtQuery.NewRow();
                        dr.ItemArray = Query.VoucherQuery("JV", voucher).Rows[0].ItemArray.Clone() as object[];
                        dtQuery.Rows.InsertAt(dr, 0);
                    }
                    MessageBox.Show("Record Successfully Saved..!");
                    btnNew.Focus();  
                
            }
        }
        private void dgvQuery_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!FLogIn && e.RowIndex != -1)
            {
                string voucherNo = dgvQuery.Rows[e.RowIndex].Cells[clnVoucherNum.Index].Value.ToString();
                tabDetailQuery.SelectedTab = tabpgDetail;
                FillJournal(voucherNo.Substring(3));
            }
        }     
        private void btnFind_Click(object sender, EventArgs e)
        {
            string FDate = txtFdate.Text  , TDate = txtTdate.Text  , CrAccount = "", Narration = "";
            CrAccount = cmbFilterAccounts.SelectedValue != null ? cmbFilterAccounts.SelectedValue.ToString() : "";
            Narration = cmbFilterNarration.SelectedValue != null ? cmbFilterNarration.SelectedValue.ToString() : "";
            
            
            FillQuery(new string[] { FDate != "" ? " and Vdate >= '" + FDate + "'" : "",
                TDate != "" ? " and Vdate <= '" + TDate + "'" : "",
            CrAccount != "" ? " and CRaccount = '" + CrAccount  + "'" : "",
            Narration != "" ? " and narration  = '" + Narration  + "'" : "",
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
                (sender as TextBox).Text  = ""; 
            }
        }
        private void dgvQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvQuery.CurrentRow != null)
                {
                    string voucherNo = dgvQuery.CurrentRow.Cells[clnVoucherNum.Index].Value.ToString();
                    tabDetailQuery.SelectedTab = tabpgDetail;
                    FillJournal(voucherNo.Substring(3));
                    e.Handled = true;
                }
            }
        }
        enum Navigators { Up, Down, Home, End };
        int VoucherIndex = 0;
        void Navigate(Navigators Nav)
        {
            if (dgvQuery.CurrentRow != null && tabDetailQuery.SelectedTab == tabpgDetail)
            {
                if (Nav == Navigators.Up && 0 < VoucherIndex)
                {
                    VoucherIndex--;
                    string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                    FillJournal(voucherNo);
                }
                else if (Nav == Navigators.Down && dtQuery.Rows.Count - 1 > VoucherIndex)
                {
                    VoucherIndex++;
                    string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                    FillJournal(voucherNo);
                }
                else if (Nav == Navigators.End)
                {
                    VoucherIndex = 0;
                    string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                    FillJournal(voucherNo);
                }
                else if (Nav == Navigators.Home)
                {
                    VoucherIndex = dtQuery.Rows.Count - 1;
                    string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                    FillJournal(voucherNo);
                }
            }
        }
        private void tabDetailQuery_KeyDown(object sender, KeyEventArgs e)
        {            
                 if (e.KeyCode == Keys.PageDown )
                {
                    Navigate(Navigators.Down);
                }
                else if (e.KeyCode == Keys.PageUp)
                {
                    Navigate(Navigators.Up );
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            dgvJournal.Rows.Clear();
            Validation.Clear(grpInvoiceDetail);
            dgvJournal.Rows.Add();
            dtpDate.Focus();  
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

        private void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dgvJournal[clnSeq.Index, dgvJournal.CurrentRow.Index].Value != null)
                {                    
                    if (txtVoucherNo.Text != "")
                    {
                        DML.Change_GL_Status(txtVoucherNo.Text.Substring(3), txtVoucherNo.Text.Substring(0, 2), dgvJournal[clnSeq.Index, dgvJournal.CurrentRow.Index].Value.ToString(), "1");
                    }
                    dgvJournal.Rows.RemoveAt(dgvJournal.CurrentRow.Index);
                    CalcTotAmount();
                    MessageBox.Show("Record Successfully Deleted..!");
                }
            }
        }

        private void dgvJournal_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvJournal.Rows[e.RowIndex].Selected = true;
                dgvJournal.CurrentCell = this.dgvJournal.Rows[e.RowIndex].Cells[e.ColumnIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtVoucherNo.Text != "")
            {
                if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DML.Change_GL_Status(txtVoucherNo.Text.Substring(3), txtVoucherNo.Text.Substring(0, 2), null, "1");

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

        private void frmJournalVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !dgvJournal.Focused)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void dgvJournal_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgvJournal[clnDrAccount.Index, e.RowIndex].Value != null)
            {
                ShowDrBal((dgvJournal.Rows[e.RowIndex].Cells[clnDrAccount.Index] as DataGridViewComboBoxCell).FormattedValue.ToString(),
                    (dgvJournal.Rows[e.RowIndex].Cells[clnDrAccount.Index] as DataGridViewComboBoxCell).Value.ToString());
            }
            if (e.RowIndex > -1 && dgvJournal[clnCrAccount.Index, e.RowIndex].Value != null)
            {
                ShowCrBal((dgvJournal.Rows[e.RowIndex].Cells[clnCrAccount.Index] as DataGridViewComboBoxCell).FormattedValue.ToString(),
                    (dgvJournal.Rows[e.RowIndex].Cells[clnCrAccount.Index] as DataGridViewComboBoxCell).Value.ToString());
            }
        }



    }
}
