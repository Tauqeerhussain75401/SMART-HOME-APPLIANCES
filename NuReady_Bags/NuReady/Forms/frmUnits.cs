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
    public partial class frmUnits : Form
    {
        bool FLogin = true;
        public frmUnits()
        {
            InitializeComponent();
        }
        void FillForm()
        {
            DataTable dt = Query.UnitIndex();
            dgvUnits.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count  ; i++)
            {
                dgvUnits.Rows.Add(dt.Rows[i]["Code"].ToString(),dt.Rows[i]["Title"].ToString(),dt.Rows[i]["status"].ToString(),"0");  
            }
        
        }
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int code = 0;
            try
            {                      
            code = (int)(from DataGridViewRow row in dgvUnits.Rows
                         where row.Cells[clnCode.Index].Value != null && row.Cells[clnCode.Index].Value.ToString()  != ""
                    select int.Parse (row.Cells[clnCode.Index].Value.ToString()  )).Max();
            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells[clnCode.Index].Value = (code + 1).ToString("D3");
            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells[clnStatus .Index].Value = "0";
            dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells[clnIsEdit.Index].Value = "1"; 

            }
            catch (Exception)
            {
                dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells[clnCode.Index].Value = (code + 1).ToString("D3");
                dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells[clnStatus.Index].Value = "0";
                dgvUnits.Rows[dgvUnits.Rows.Count - 2].Cells[clnIsEdit.Index].Value = "1";  
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void dgvNarration_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!FLogin )
            {
                dgvUnits.CurrentRow .Cells[clnIsEdit.Index].Value = "1";  
            }
        }

        private void frmNarration_Load(object sender, EventArgs e)
        { 
            FillForm(); 
            FLogin = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Save this...!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvUnits.Rows)
                {
                    if (row.Cells[clnCode.Index].Value != null && row.Cells[clnUnits.Index].Value != null && row.Cells[clnIsEdit.Index].Value == "1")
                        DML.UnitsAdd_Edit(row.Cells[clnCode.Index].Value.ToString(),
                            row.Cells[clnUnits.Index].Value.ToString(),
                            row.Cells[clnStatus.Index].Value.ToString());
                }
                MessageBox.Show("Record Successfully Saved..!");
            }
        }

  

      
    }
}
