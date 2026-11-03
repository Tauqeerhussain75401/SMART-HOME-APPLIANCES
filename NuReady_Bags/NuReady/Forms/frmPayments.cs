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
    public partial class frmPayments : Form
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
        public frmPayments()
        {
            InitializeComponent();
            FillAccount();
            FillCashBanks();
            FillNarration();
            FillFilterAccount();
            FillFilterCashBanks();
            FillFilterNarration();
            dgvExpenses.Rows.Add();
        }
        private int currentRow;
        private bool resetRow = false  ;
        bool FLogIn = true;
        private string VoucherNum = null;
        void ShowBal(string Title,string account)
        {
            Decimal bal = Query.Balance(account);
            lblBalance.Text = Title + " = " + (bal >= 0 ? bal.ToString("N2") + " Dr" : (-bal).ToString("N2") + " Cr");  
        }
        DataTable dtAccount = Query.Accounts();
        DataTable dtNarration = Query.NarrationIndex();
        DataTable dtCashBanks = Query.CashBanks();        
        #region Fill Controls
        DataTable dtQuery;
        void FillQuery(string[] filter )
        {
            dgvQuery.Rows.Clear();
             dtQuery = Query.VoucherQuery("PV", filter);
             for (int i = 0; i < dtQuery.Rows.Count; i++)
            {
                dgvQuery.Rows.Add(((DateTime)dtQuery.Rows[i][GL.VDate.ToString()]),
                    dtQuery.Rows[i][GL.Vtype.ToString()].ToString() + "-" + dtQuery.Rows[i][GL.VoucherNo.ToString()].ToString(),
                    dtQuery.Rows[i][GL.Amount.ToString()].ToString(),
                    dtQuery.Rows[i][GL.Narration.ToString()].ToString(),
                    dtQuery.Rows[i][GL.CreatedBy.ToString()].ToString() + " | " + ((DateTime)dtQuery.Rows[i][GL.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt"),
                    dtQuery.Rows[i][GL.EditBy.ToString()].ToString() != "" ? dtQuery.Rows[i][GL.EditBy.ToString()].ToString() + " | " + ((DateTime)dtQuery.Rows[i][GL.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null);      
            }
            

        
        }
        internal  void FillPayment(string vno)
        {
            DataTable dt;
            if (cmbFilterAccounts.SelectedValue != null)
                dt = Query.VoucherDetail("PV", vno, cmbFilterAccounts.SelectedValue.ToString());
            else
                dt = Query.VoucherDetail("PV", vno);
            /////////////////////
            txtVoucherNo.Text = "PV-" + vno;
            VoucherNum = vno;
              dgvExpenses.Rows.Clear();
              if (dt.Rows.Count > 0)
              {
                  dtpDate.Value = (DateTime)dt.Rows[0][GL.VDate.ToString()];
                  cmbCashBanks.SelectedValue = dt.Rows[0][GL.CRAccount.ToString()];
                  cmbNarration.SelectedValue = dt.Rows[0][GL.Narration.ToString()];
                  txtCreatedBy.Text = dt.Rows[0][GL.CreatedBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[0][GL.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                  txtEditBy.Text = dt.Rows[0][GL.EditBy.ToString()].ToString() != "" ? dt.Rows[0][GL.EditBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[0][GL.EditTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null;
                  for (int i = 0; i < dt.Rows.Count; i++)
                  {
                      dgvExpenses.Rows.Add(dt.Rows[i][GL.Vseq.ToString()].ToString(),
                          dt.Rows[i][GL.DRAccount.ToString()].ToString(),
                          Validation.NullIfEmpty(dt.Rows[i][GL.CheckNum.ToString()].ToString()),
                          dt.Rows[i][GL.CheckDate.ToString()].ToString() == "" ? null : dt.Rows[i][GL.CheckDate.ToString()],
                          ((decimal)(dt.Rows[i][GL.Amount.ToString()])).ToString("N0"),
                          dt.Rows[i][GL.Remarks.ToString()].ToString(),
                          dt.Rows[i][GL.CreatedBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[i][GL.CreatedTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt"),
                          dt.Rows[i][GL.EditBy.ToString()].ToString() != "" ? dt.Rows[i][GL.EditBy.ToString()].ToString() + " | " + ((DateTime)dt.Rows[i][GL.EditTime.ToString()]).ToString("dd-MMM-yyyy hh:mm:ss tt") : null);
                  }
                  dgvExpenses.Rows.Add();
                  CalcTotAmount();
              }
        }
        void FillAccount()
        {
            clnAccount.DataSource = dtAccount.Copy();  
            clnAccount.DisplayMember = "Title";
            clnAccount.ValueMember = "Account";
        }
        void FillCashBanks()
        {
            cmbCashBanks.DataSource = dtCashBanks.Copy();  
            cmbCashBanks.DisplayMember = "Title";
            cmbCashBanks.ValueMember = "Account";
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
        void FillFilterCashBanks()
        {
            cmbFilterCashBanks.DataSource = dtCashBanks.Copy();
            cmbFilterCashBanks.DisplayMember = "Title";
            cmbFilterCashBanks.ValueMember = "Account";
            cmbFilterCashBanks.SelectedIndex = -1;
        }
        void FillFilterNarration()
        {
            cmbFilterNarration.DataSource = dtNarration.Copy();
            cmbFilterNarration.DisplayMember = "Title";
            cmbFilterNarration.ValueMember = "Code";
            cmbFilterNarration.SelectedIndex = -1;
        }
        #endregion
        void checkValidation(int rowindex, int colIndex)
        {

            if (colIndex == clnCheque.Index)
            {
                if (dgvExpenses[colIndex, rowindex].Value != null &&
                    dgvExpenses[colIndex, rowindex].Value.ToString().Length > 0)
                {
                    dgvExpenses[clnChequeDate.Index, rowindex].ReadOnly = false;
                    dgvExpenses[clnChequeDate.Index, rowindex].Style.BackColor = Color.Empty;
                    dgvExpenses[clnChequeDate.Index, rowindex].Style.SelectionBackColor = Color.Empty;
                    if (dgvExpenses[clnChequeDate.Index, rowindex].Value == null)
                        dgvExpenses[clnChequeDate.Index, rowindex].Value = DateTime.Now;

                }
                else
                {
                    dgvExpenses[clnChequeDate.Index, rowindex].ReadOnly = true;
                    dgvExpenses[clnChequeDate.Index, rowindex].Style.BackColor = Color.LightGray;
                    dgvExpenses[clnChequeDate.Index, rowindex].Style.SelectionBackColor = Color.Gray;
                    dgvExpenses[clnChequeDate.Index, rowindex].Value = null;
                }
            }

        }
        void AllowNewRow()
        {
            if (dgvExpenses.CurrentRow.Index == dgvExpenses.Rows[dgvExpenses.Rows.Count - 1].Index)
            {
                if (dgvExpenses.CurrentRow.Cells[clnAccount.Index].Value != null &&
                    dgvExpenses.CurrentRow.Cells[clnAmount.Index].Value != null )
                    dgvExpenses.Rows.Add();   
            }
        }
        private void frmReceipt_Load(object sender, EventArgs e)
        {            
            
            FillQuery(new string[]{} );
            if (dtQuery.Rows.Count > 0 && txtVoucherNo.Text == "")
            {
                string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                FillPayment(voucherNo);
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
                dgvExpenses.CurrentCell = dgvExpenses.Rows[currentRow].Cells[dgvExpenses.CurrentCell.ColumnIndex];
            }           
        }
        void CalcTotAmount()
        {
            decimal Tot = 0;
            try
            {
                Tot = (from DataGridViewRow row in dgvExpenses.Rows
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
            if (dgvExpenses.CurrentCellAddress.X == clnAccount.DisplayIndex)
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
                }
            }
            if (dgvExpenses.CurrentCell.ColumnIndex == clnAmount.Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null && e.Control.Text  != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                }
            }
        }
        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvExpenses.CurrentCell.ColumnIndex == clnAccount.Index)
            {
                if ((sender as ComboBox).SelectedIndex != -1)
                {
                    DataRowView dr = (DataRowView)(sender as ComboBox).SelectedItem;
                    ShowBal(dr[1].ToString(), dr[0].ToString());
                    if ((sender as ComboBox).SelectedValue == null)
                        (sender as ComboBox).SelectedValue = dr[0].ToString();
                }
            }
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvExpenses.CurrentCell.ColumnIndex == clnAmount.Index)
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
            if (!FLogIn)
                checkValidation(e.RowIndex, e.ColumnIndex);            
            CalcTotAmount(); 
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            checkValidation(e.RowIndex ,clnCheque.Index  ); 
            int seq = 0;
            try
            {                      
            seq = (int)(from DataGridViewRow row in dgvExpenses.Rows
                         where row.Cells[clnSeq.Index].Value != null && row.Cells[clnSeq.Index].Value.ToString()  != ""
                    select int.Parse (row.Cells[clnSeq.Index].Value.ToString()  )).Max();            
            }
            catch 
            {
            }
            if (dgvExpenses.Rows[e.RowIndex].Cells[clnSeq.Index].Value == null)
            dgvExpenses.Rows[e.RowIndex].Cells[clnSeq.Index].Value = (seq + 1).ToString();            
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to save this...!","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question  ) == DialogResult .Yes)
            {
                if (UserInfo.UserId != "Admin" && txtVoucherNo.Text != "")
                {
                    frmAuthentication frm = new frmAuthentication();
                    if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                }
                string voucher = null;
                voucher = txtVoucherNo.Text == "" ? null : txtVoucherNo.Text.Split('-')[1]; 
                if (cmbCashBanks.SelectedValue != null)
                {
                    foreach (DataGridViewRow row in dgvExpenses.Rows)
                    {
                        if (row.Cells[clnAccount.Index].Value != null &&
                            row.Cells[clnAccount.Index].Value != null)
                        {
                            DML.GL_Add_Edit(dtpDate.Value, voucher, "PV", (string)row.Cells[clnSeq.Index].Value,
                                row.Cells[clnAccount.Index].Value.ToString(),decimal .Parse (row.Cells[clnAmount.Index].Value.ToString()).ToString() ,
                                cmbCashBanks.SelectedValue.ToString(), cmbNarration.SelectedValue.ToString(),
                                (string)row.Cells[clnRemarks.Index].Value, (string)row.Cells[clnCheque.Index].Value,
                                (row.Cells[clnChequeDate.Index].Value != null ? ((DateTime)row.Cells[clnChequeDate.Index].Value).ToString("MM/dd/yyyy").Replace("-","/")  : null),
                                "Pending", ((string)row.Cells[clnCheque.Index].Value != null ? "1" : "0"), "0", ref voucher); 
                        }
                    }
                    FillPayment(voucher);
                    if (dtQuery.Select("voucherno = '" + voucher + "'").Count() == 0)
                    {
                        DataRow dr = dtQuery.NewRow();
                        dr.ItemArray = Query.VoucherQuery("PV", voucher).Rows[0].ItemArray.Clone() as object[];
                        dtQuery.Rows.InsertAt(dr, 0);
                    }                    
                    MessageBox.Show("Record Successfully Saved..!");
                    btnNew.Focus();  
                }
            }
        }
        private void dgvQuery_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!FLogIn && e.RowIndex != -1)
            {
                string voucherNo = dgvQuery.Rows[e.RowIndex].Cells[clnVoucherNum.Index].Value.ToString();
                tabDetailQuery.SelectedTab = tabpgDetail;
                FillPayment(voucherNo.Substring(3));
            }
        }     
        private void btnFind_Click(object sender, EventArgs e)
        {
            string FDate = txtFdate.Text  , TDate = txtTdate.Text  , CashBank = "", Account = "", Narration = "";
            CashBank = cmbFilterCashBanks.SelectedIndex != -1 ? (string)cmbFilterCashBanks.SelectedValue : "";
            Account = cmbFilterAccounts.SelectedIndex != -1 ? (string)cmbFilterAccounts.SelectedValue : "";
            Narration = cmbFilterNarration.SelectedIndex != -1 ? (string)cmbFilterNarration.SelectedValue : "";
            
            
            FillQuery(new string[] { FDate != "" ? " and Vdate >= '" + FDate + "'" : "",
                TDate != "" ? " and Vdate <= '" + TDate + "'" : "",
            CashBank  != "" ? " and CRAccount = '" + CashBank  + "'" : "",
            Account != "" ? " and DRaccount = '" + Account  + "'" : "",
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
                    FillPayment(voucherNo.Substring(3));
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
                    FillPayment(voucherNo);
                }
                else if (Nav == Navigators.Down && dtQuery.Rows.Count - 1 > VoucherIndex)
                {
                    VoucherIndex++;
                    string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                    FillPayment(voucherNo);
                }
                else if (Nav == Navigators.End)
                {
                    VoucherIndex = 0;
                    string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                    FillPayment(voucherNo);
                }
                else if (Nav == Navigators.Home)
                {
                    VoucherIndex = dtQuery.Rows.Count - 1;
                    string voucherNo = dtQuery.Rows[VoucherIndex]["voucherno"].ToString();
                    FillPayment(voucherNo);
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
            dgvExpenses.Rows.Clear();
            Validation.Clear(grpInvoiceDetail);
            dgvExpenses.Rows.Add();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtVoucherNo.Text != "")
            {
                if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DML.Change_GL_Status(txtVoucherNo.Text.Substring(3), txtVoucherNo.Text.Substring(0, 2), null, "1");

                    DataGridViewRow row = dgvQuery.Rows.Cast<DataGridViewRow>()
           .Where(r => r.Cells[clnVoucherNum.Index ].Value.ToString().Equals(txtVoucherNo.Text))
           .FirstOrDefault();
                    btnNew_Click(null, null); 
                     if(row != null)dgvQuery.Rows.Remove(row);
                    CalcTotAmount();
                    MessageBox.Show("Record Successfully Deleted..!");  
                }
            }
        }

        private void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dgvExpenses[clnSeq.Index, dgvExpenses.CurrentRow.Index].Value != null)
                {                    
                    if (txtVoucherNo.Text != "")
                    {
                        DML.Change_GL_Status(txtVoucherNo.Text.Substring(3), txtVoucherNo.Text.Substring(0, 2), dgvExpenses[clnSeq.Index, dgvExpenses.CurrentRow.Index].Value.ToString(), "1");
                    }
                    dgvExpenses.Rows.RemoveAt(dgvExpenses.CurrentRow.Index);
                    CalcTotAmount();
                    MessageBox.Show("Record Successfully Deleted..!");
                }
            }
        }



        private void dgvExpenses_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right  && e.RowIndex >= 0 && e.ColumnIndex >= 0 )
            {
                dgvExpenses.Rows[e.RowIndex].Selected = true;
                dgvExpenses.CurrentCell = this.dgvExpenses.Rows[e.RowIndex].Cells[e.ColumnIndex];            
                contextMenuStrip1.Show(Cursor.Position);
            }

        }

        private void frmPayments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !dgvExpenses.Focused)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void dgvExpenses_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dgvExpenses[clnAccount.Index, e.RowIndex].Value != null)
            {
                ShowBal((dgvExpenses.Rows[e.RowIndex].Cells[clnAccount.Index] as DataGridViewComboBoxCell).FormattedValue.ToString(),
                    (dgvExpenses.Rows[e.RowIndex].Cells[clnAccount.Index] as DataGridViewComboBoxCell).Value.ToString());
            }
        }

        private void addNewAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChartOfAccLvl5 frm = new frmChartOfAccLvl5();
            frm.AccountHead = AccountHead.Farmer;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                FillAccount();
                FillFilterAccount();
            }
        }



      

     

     

     



    }
}
