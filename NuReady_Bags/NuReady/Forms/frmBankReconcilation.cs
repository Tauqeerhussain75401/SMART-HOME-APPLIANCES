using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NuReady
{
    public partial class frmBankReconcilation : Form
    {
        public frmBankReconcilation()
        {
            InitializeComponent();
        }
        void Fill()
        {
            DataTable dt = Query.BankReconcilation((string )cmbBanks.SelectedValue ,dtpFdate.Value,dtpTdate.Value);
            dgvBankRec.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvBankRec.Rows.Add(dt.Rows[i]["clear"] .ToString () == "0" ? false : true,
                    dt.Rows[i]["Vno"].ToString(),
                    (DateTime )dt.Rows[i]["Vdate"],
                    (DateTime )dt.Rows[i]["CheckDate"],
                    dt.Rows[i]["CheckNum"].ToString(),
                    Validation.DBNullToNull(dt.Rows[i]["ClearDate"]),
                    dt.Rows[i]["title"].ToString(),
                    ((decimal )dt.Rows[i]["Dr"]).ToString("N2"),
                    ((decimal)dt.Rows[i]["Cr"]).ToString("N2")
                    );  
            }
            decimal ReconcileBal =  Query.Balance(dtpTdate.Value, (string)cmbBanks.SelectedValue,"1");
            decimal StatementBal = Query.Balance(dtpTdate.Value, (string)cmbBanks.SelectedValue);
            txtReconcileBal.Text = ReconcileBal.ToString("N2");
            txtStatementBal.Text = StatementBal.ToString("N2");  

            decimal TotDr = 0, TotCr = 0;
            TotDr = (Decimal)(from DataGridViewRow rr in dgvBankRec.Rows
                              select Convert.ToDecimal  (rr.Cells[clnDr.Index].Value )).Sum();
            TotCr = (Decimal)(from DataGridViewRow rr in dgvBankRec.Rows
                              select Convert.ToDecimal(rr.Cells[clnCredit.Index].Value)).Sum();
            lblTotDr.Text = TotDr.ToString("N2");
            lblTotCr.Text = TotCr.ToString("N2");
            CalcDiff();
        }
        bool AllowValidate = false;
        private void frmBankReconcilation_Load(object sender, EventArgs e)
        {
            FillControls.FillcmbCashAndBankAcc(cmbBanks);
            AllowValidate = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void dgvBankRec_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvBankRec.CurrentCell.ColumnIndex == clnClear.Index)
            {
                CheckBox chkClear = e.Control as CheckBox;
                if (chkClear != null)
                {
                    chkClear.CheckedChanged += new EventHandler(chkClear_CheckedChanged);
                }
            }
        }

        void chkClear_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void CalcDiff()
        {
            decimal Totclear = 0, TotBal = 0;
            Totclear = (Decimal)(from DataGridViewRow rr in dgvBankRec.Rows
                                   where Convert.ToBoolean(rr.Cells[clnClear.Index].Value.ToString()) == true 
                                   select Convert.ToDecimal(rr.Cells[clnDr.Index].Value) - Convert.ToDecimal(rr.Cells[clnCredit.Index].Value)).Sum();

            TotBal = (Decimal)(from DataGridViewRow rr in dgvBankRec.Rows
                               select Convert.ToDecimal(rr.Cells[clnDr.Index].Value) - Convert.ToDecimal(rr.Cells[clnCredit.Index].Value)).Sum();
            txtClear.Text = Totclear.ToString("N2");
            txtRealBal.Text = TotBal.ToString("N2");
            txtDiff.Text = (TotBal - Totclear).ToString("N2");
        }

        private void dgvBankRec_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ( AllowValidate  && dgvBankRec.CurrentCell.ColumnIndex == clnClear.Index )
            {
                CalcDiff();
                if ((bool)dgvBankRec[clnClear.Index  , e.RowIndex].Value == true)
                {
                    dgvBankRec[clnRecDate.Index, e.RowIndex].Value = dtpTdate.Value;
                }
                else
                {
                    dgvBankRec[clnRecDate.Index, e.RowIndex].Value = null;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Save this...!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool saved = false;
                foreach (DataGridViewRow item in dgvBankRec.Rows)
                {                   
                        saved = DML.UpdateBankReconcile(item.Cells[clnVNo.Index].Value.ToString(),
                            (bool)item.Cells[clnClear.Index].Value == true ? "1" : "0",
                            item.Cells[clnRecDate.Index].Value);
                }
                if (saved)
                {
                    MessageBox.Show("Record Successfully Saved..!");
                    Fill();                    
                }
            }
        }

       

        

        
    }
}
