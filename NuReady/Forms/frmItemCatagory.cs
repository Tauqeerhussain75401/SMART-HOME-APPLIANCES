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
    public partial class frmItemCatagory : Form
    {
        bool FLogin = true;
        public frmItemCatagory()
        {
            InitializeComponent();
         
        }
        void FillForm()
        {
            DataTable dt = Query.ItemCatagoryIndex();
            dgvItemCatagory.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvItemCatagory.Rows.Add(dt.Rows[i]["Code"].ToString(), 
                    dt.Rows[i]["Title"].ToString(),                    
                    dt.Rows[i]["Active"].ToString() == "1" ? true : false, 
                    dt.Rows[i]["status"].ToString(), 
                    "0");
            }
        
        }
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int code = 0;
            try
            {                      
            code = (int)(from DataGridViewRow row in dgvItemCatagory.Rows
                         where row.Cells[clnCode.Index].Value != null && row.Cells[clnCode.Index].Value.ToString()  != ""
                    select int.Parse (row.Cells[clnCode.Index].Value.ToString()  )).Max();
            dgvItemCatagory.Rows[dgvItemCatagory.Rows.Count - 2].Cells[clnCode.Index].Value = (code + 1).ToString("D3");
            dgvItemCatagory.Rows[dgvItemCatagory.Rows.Count - 2].Cells[clnStatus .Index].Value = "0";
            dgvItemCatagory.Rows[dgvItemCatagory.Rows.Count - 2].Cells[clnIsEdit.Index].Value = "1"; 

            }
            catch (Exception)
            {
                dgvItemCatagory.Rows[dgvItemCatagory.Rows.Count - 2].Cells[clnCode.Index].Value = (code + 1).ToString("D3");
                dgvItemCatagory.Rows[dgvItemCatagory.Rows.Count - 2].Cells[clnStatus.Index].Value = "0";
                dgvItemCatagory.Rows[dgvItemCatagory.Rows.Count - 2].Cells[clnIsEdit.Index].Value = "1";  
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
                dgvItemCatagory.CurrentRow .Cells[clnIsEdit.Index].Value = "1";  
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
                foreach (DataGridViewRow row in dgvItemCatagory.Rows)
                {
                    if (row.Cells[clnCode.Index].Value != null && row.Cells[clnItemCatagory.Index].Value != null && (string)row.Cells[clnIsEdit.Index].Value == "1")
                        DML.ItemCatagoryAdd_Edit(row.Cells[clnCode.Index].Value.ToString(),
                            row.Cells[clnItemCatagory.Index].Value.ToString(),
                            Validation.BoolToint(Validation.ToBool  (row.Cells[clnActive.Index].Value )),
                            row.Cells[clnStatus.Index].Value.ToString());
                }
                MessageBox.Show("Record Successfully Saved..!");
                FillForm();
            }
        }

        private void dgvItemCatagory_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

  

      
    }
}
