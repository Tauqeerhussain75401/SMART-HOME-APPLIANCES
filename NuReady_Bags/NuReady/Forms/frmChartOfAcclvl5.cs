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
    public partial class frmChartOfAccLvl5 : Form
    {
        DataTable dtChart;
        DataTable dtFilter;
        public frmChartOfAccLvl5()
        {
            InitializeComponent();
            FillControls.FillcmbAccLvl4Heads(cmbAccountHead);
            dtChart = Query.get_ChartOfAcc();
            dtFilter = dtChart.Copy();
        }
        bool FlogIn = true;

        internal string AccountHead = "";
        private void frmChartOfAccount_Load(object sender, EventArgs e)
        {                      
            FlogIn = false;
            if (AccountHead != "")
            {
                cmbAccountHead.SelectedValue = AccountHead;
                Filldgv();
                dgvAccount.Select();  
            }
            else
                Filldgv();
        }
        
        void Filldgv()
        {
            DataRowView dr = (DataRowView)cmbAccountHead.SelectedItem;
            if (!FlogIn)
            {
                lblParentHeader.Text = "";//   tvChartOfAcc.SelectedNode.Text;
                dtFilter.Rows.Clear();
                if (dr != null)
                {
                    lblParentHeader.Text = dr["Title"].ToString();
                    dtFilter = dtChart.Select("parentid = '" + dr["account"].ToString() + "'").Count() > 0 ? dtChart.Select("parentid = '" + dr["account"].ToString() + "'").CopyToDataTable() : dtFilter;
                }
                dgvAccount.Rows.Clear();
                for (int i = 0; i < dtFilter.Rows.Count; i++)
                {
                    dgvAccount.Rows.Add(dtFilter.Rows[i]["Account"].ToString(), dtFilter.Rows[i]["Title"].ToString(),
                        dtFilter.Rows[i]["parentid"].ToString(), dtFilter.Rows[i]["Acctype"].ToString(),
                        dtFilter.Rows[i]["AccLevel"].ToString(),
                        dtFilter.Rows[i]["CreatedBy"].ToString() + " " + (dtFilter.Rows[i]["CreatedTime"].ToString() == "" ? "" : ((DateTime)dtFilter.Rows[i]["CreatedTime"]).ToString("| dd-MMM-yyyy hh:mm:ss tt")),
                        dtFilter.Rows[i]["EditBy"].ToString() + " " + (dtFilter.Rows[i]["EditTime"].ToString() == "" ? "" : ((DateTime)dtFilter.Rows[i]["EditTime"]).ToString("| dd-MMM-yyyy hh:mm:ss tt")),
                        "0");
                }
                if (dr != null)
                    AllowRowAdd();                

                if (dtFilter.Rows.Count == 0)
                {
                    lbldgvCreated.Text = "-";
                    lbldgvEdit.Text = "-";
                }
            }
        }
      
     
        
        private void btnOpenning_Click(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)cmbAccountHead.SelectedItem;
            frmOpeningBal frm = new frmOpeningBal();
            if(dr != null)
            frm.ParentAcc = dr["account"].ToString();
            frm.ShowDialog(); 
        }

    

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Save this...!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool saved = false;
                foreach (DataGridViewRow item in dgvAccount.Rows)
                {
                    if (item.Cells[clnIsEdit.Index].Value.ToString() == "1" && item.Cells[clnTitle.Index].Value != null)
                        saved = DML.ChartOfAccAdd_Edit(item.Cells[clnAccount.Index].Value.ToString(),
                            item.Cells[clnTitle.Index].Value.ToString(),
                            item.Cells[clnParent.Index].Value.ToString(),
                            item.Cells[clnType.Index].Value.ToString(),
                             item.Cells[clnLevel.Index].Value.ToString());
                }             
                if (saved)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("Record Successfully Saved..!");
                    dtChart = Query.get_ChartOfAcc();
                    Filldgv();                     
                }
                
            }
        }

        private void dgvAccount_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAccount.CurrentRow != null)
            {
                lbldgvCreated.Text = dgvAccount.CurrentRow.Cells[clnCreated.Index].Value.ToString();
                lbldgvEdit.Text = dgvAccount.CurrentRow.Cells[clnEdit.Index].Value.ToString();
            }
            else
            {
                lbldgvCreated.Text = "-";
                lbldgvEdit.Text = "-";
            }
        }

      

        private void dgvAccount_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!FlogIn)
            {             
            dgvAccount.Rows[e.RowIndex].Cells[clnIsEdit.Index].Value = "1";
            }
        }

        private void dgvAccount_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == dgvAccount.Rows.Count - 1)
            {
                if (dgvAccount[clnTitle.Index, e.RowIndex].Value != null)
                {
                    AllowRowAdd();
                }
            }
        }

        private void AllowRowAdd()
        {
            DataRowView dr = (DataRowView)cmbAccountHead.SelectedItem;
            if (dr != null)
            {
                //     string Acc = dgvAccount[clnParent.Index, rowindex].Value.ToString() + (dgvAccount.Rows.Count + 1).ToString("D2");
                int ind = dgvAccount.Rows.Add("", null, dr["account"].ToString(),
                     "Detail" ,
                        "5", "", "", "1");
                dgvAccount.CurrentCell = dgvAccount.Rows[ind].Cells[clnTitle.Index];
            }
        }

        private void cmbAccountHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filldgv();
        }

        private void frmChartOfAccLvl5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter  && !dgvAccount.Focused)
            {
                SendKeys.Send("{tab}");  
            }
        }

        
        
       

     
    }
}
