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
    public partial class frmOpeningBal : Form
    {
        public frmOpeningBal()
        {
            InitializeComponent();
        }
        internal string ParentAcc = "%";
        void FillForm()
        {
            DataTable dt = Query.OpenningAccounts(ParentAcc);
            for (int i = 0; i < dt.Rows.Count  ; i++)
            {
                dgvOpenning.Rows.Add(dt.Rows[i]["ParentCode"].ToString(),
                    dt.Rows[i]["Code"].ToString(),
                    dt.Rows[i]["Title"].ToString(),
                    ((decimal)dt.Rows[i]["Bal"]) > 0 ? ((decimal)dt.Rows[i]["Bal"]).ToString("N2") : null,
                    ((decimal)dt.Rows[i]["Bal"]) < 0 ? (-(decimal)dt.Rows[i]["Bal"]).ToString("N2") : null);
                if (dt.Rows[i]["Code"].ToString().Substring(0, 3) == "002" || 
                    dt.Rows[i]["Code"].ToString().Substring(0, 3) == "003" || 
                    dt.Rows[i]["Code"].ToString().Substring(0, 3) == "005")
                {
                    //dgvOpenning[clnDr.Index, i].ReadOnly = true;
                    //dgvOpenning[clnDr.Index, i].Style.BackColor = Color.LightGray;
                }
                else
                {
                 //   dgvOpenning[clnCr.Index, i].ReadOnly = true;
                   // dgvOpenning[clnCr.Index, i].Style.BackColor = Color.LightGray;
                }
            }
        }
        void CalcTotals()
        {
            decimal TotDr = 0, TotCr = 0;
            try
            {
                TotDr = (from DataGridViewRow row in dgvOpenning.Rows
                       where row.Cells[clnDr.Index].Value != null
                       select decimal.Parse(row.Cells[clnDr.Index].Value.ToString())).Sum();

            }
            catch
            {

            }
            try
            {
                TotCr = (from DataGridViewRow row in dgvOpenning.Rows
                         where row.Cells[clnCr.Index].Value != null
                         select decimal.Parse(row.Cells[clnCr.Index].Value.ToString())).Sum();

            }
            catch
            {

            }
            lblTotDr.Text = TotDr.ToString();
            lblTotCr.Text = TotCr.ToString();
        }
        private void frmOpeningBal_Load(object sender, EventArgs e)
        {
            FillForm();
            CalcTotals(); 
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to save this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow item in dgvOpenning.Rows)
                {
                    DML.OpenningBal_Add_Edit(item.Cells[clnAccount.Index].Value.ToString(),
                        item.Cells[clnDr.Index].Value != null ? decimal.Parse(item.Cells[clnDr.Index].Value.ToString()).ToString() : null,
                        item.Cells[clnCr.Index].Value != null ? decimal.Parse(item.Cells[clnCr.Index].Value.ToString()).ToString() : null);
                }
                MessageBox.Show("Record Successfully Saved..!");  
            }
            
        }

        private void dgvOpenning_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CalcTotals(); 
        }
    }
}
