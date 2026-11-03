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
    public partial class frmItemDetail : Form
    {
        DataTable dtUnits = Query.UnitIndex();
        DataTable dtCatagory = new DataTable();
        bool FLogin = true;
        public frmItemDetail()
        {
            InitializeComponent();
            FillControls.FillclnCatagory(clnCatagory);
        }
        void AllowNewRow()
        {
            if (dgvItemDetail.CurrentRow.Index == dgvItemDetail.Rows[dgvItemDetail.Rows.Count - 1].Index)
            {
                if (dgvItemDetail.CurrentRow.Cells[clnItem.Index].Value != null &&
                    dgvItemDetail.CurrentRow.Cells[clnPriRate.Index].Value != null &&
                    dgvItemDetail.CurrentRow.Cells[clnSecRate.Index].Value != null)
                    dgvItemDetail.Rows.Add();
            }
        }
        void fillItemCatagory()
        {
            dtCatagory = Query.ItemCatagoryIndex();
            cmbItemCatagory.DataSource = dtCatagory;
            cmbItemCatagory.DisplayMember = "Title";
            cmbItemCatagory.ValueMember = "Code"; 
        }
        void FillUnits()
        {            
            clnPriUnit.DataSource = dtUnits.Copy();
            clnPriUnit.DisplayMember = "Title";
            clnPriUnit.ValueMember = "Code";
            clnSecUnit.DataSource = dtUnits.Copy();
            clnSecUnit.DisplayMember = "Title";
            clnSecUnit.ValueMember = "Code";
        }
        DataTable dtItemDetail = Query.ItemDetail();
        void Fillform(string catagory)
        {
            DataTable dtfilter = dtItemDetail.Copy();
            dtfilter.Rows.Clear();
            dtfilter = dtItemDetail.Select("fkitemcatagory = '" + catagory + "'").Count() > 0 ? dtItemDetail.Select("fkitemcatagory = '" + catagory + "'").CopyToDataTable() : dtfilter;
            dgvItemDetail.Rows.Clear();
            for (int i = 0; i < dtfilter.Rows.Count; i++)
            {
                dgvItemDetail.Rows.Add(dtfilter.Rows[i]["Id"].ToString(),
                    dtfilter.Rows[i]["Code"].ToString(),
                    dtfilter.Rows[i]["Barcode"].ToString(),
                    dtfilter.Rows[i]["fkitemcatagory"].ToString(),
                    dtfilter.Rows[i]["Title"].ToString(),
                    dtfilter.Rows[i]["PCTCode"].ToString(),
                    dtfilter.Rows[i]["ItemKey"].ToString(),
                    dtfilter.Rows[i]["PriRate"].ToString(),
                    dtfilter.Rows[i]["SecRate"].ToString(),
                    dtfilter.Rows[i]["PrimaryUnit"].ToString().EmptyToNull(),
                    dtfilter.Rows[i]["SecondaryUnit"].ToString().EmptyToNull(),
                    dtfilter.Rows[i]["DefaultUnit"].ToString().EmptyToNull(),
                    dtfilter.Rows[i]["QtyInPack"].ToString().EmptyToNull(),
                    Validation.ToBool  (dtfilter.Rows[i]["Alert"].ToString()),
                    dtfilter.Rows[i]["LowStockAlert"].ToString().EmptyToNull(),
                    dtfilter.Rows[i]["OpnStock"].ToString().EmptyToNull(),
                    dtfilter.Rows[i]["OpnRate"].ToString().EmptyToNull(),
                    dtfilter.Rows[i]["status"].ToString(), 
                    "0"
                    );                
            }
            dgvItemDetail.Rows.Add();
            CalcTotals();
        }
        private void frmItemDetail_Load(object sender, EventArgs e)
        {
            fillItemCatagory();
            FillUnits(); 
            dgvItemDetail.Rows.Add();
            Fillform((string)cmbItemCatagory.SelectedValue); 
            FLogin = false;
            if (UserInfo.UserId != "Admin" )
            {
                clnOpnStock.ReadOnly = true;
            }
        }
      
        #region dgvItemDetail Events
        private int currentRow;
        private bool resetRow = false;
        private void dgvItemDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            AllowNewRow();
            resetRow = true;
            currentRow = e.RowIndex;
            
        }
        private void dgvItemDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;     
                dgvItemDetail.CurrentCell = dgvItemDetail.Rows[currentRow].Cells[dgvItemDetail.CurrentCell.ColumnIndex];
            }
        }
        private void dgvItemDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{tab}");
            }
        }
        private void dgvItemDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvItemDetail.CurrentCell.ColumnIndex == clnPriRate.Index || dgvItemDetail.CurrentCell.ColumnIndex == clnSecRate.Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null && e.Control.Text != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
                }
            }
        }
        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvItemDetail.CurrentCell.ColumnIndex == clnPriRate.Index || dgvItemDetail.CurrentCell.ColumnIndex == clnSecRate.Index)
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
        private void dgvItemDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (!FLogin && dgvItemDetail.CurrentRow != null)
                {
                    dgvItemDetail.CurrentRow.Cells[clnIsEdit.Index].Value = "1";
                }
                DataGridViewComboBoxCell cmb = (dgvItemDetail[clnDefUnif.Index, e.RowIndex] as DataGridViewComboBoxCell);
                string Filter = " Code in (";
                Filter += "'" + Validation.IsNull((string)dgvItemDetail[clnPriUnit.Index, e.RowIndex].Value, "") + "'";
                Filter += ",'" + Validation.IsNull((string)dgvItemDetail[clnSecUnit.Index, e.RowIndex].Value, "") + "')";
                DataTable dt = dtUnits.Copy();
                dt.Rows.Clear(); 
                dt = dtUnits.Select(Filter).Count() == 0 ? null : dtUnits.Select(Filter).CopyToDataTable();
                cmb.DataSource = dt;
                clnDefUnif.DisplayMember = "Title";
                clnDefUnif.ValueMember = "Code";
                if (dgvItemDetail[clnPriUnit.Index, e.RowIndex].Value == null)
                    dgvItemDetail[clnPriUnit.Index, e.RowIndex].Value = dgvItemDetail[clnSecUnit.Index, e.RowIndex].Value;
                if (dgvItemDetail[clnSecUnit.Index, e.RowIndex].Value == null)
                    dgvItemDetail[clnSecUnit.Index, e.RowIndex].Value = dgvItemDetail[clnPriUnit.Index, e.RowIndex].Value;              
                if(cmb.Value == null)
                cmb.Value = dgvItemDetail[clnSecUnit.Index, e.RowIndex].Value;

            }
        }
        private void dgvItemDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int seq = 0;
            try
            {
                seq = (int)(from DataGridViewRow row in dgvItemDetail.Rows
                            where row.Cells[clnCode.Index].Value != null && row.Cells[clnCode.Index].Value.ToString() != ""
                            select int.Parse(row.Cells[clnCode.Index].Value.ToString())).Max();
            }
            catch
            {
            }
            dgvItemDetail.Rows[e.RowIndex].Cells[clnCatagory.Index].Value = cmbItemCatagory.SelectedValue;
            if (dgvItemDetail.Rows[e.RowIndex].Cells[clnCode.Index].Value == null)
                dgvItemDetail.Rows[e.RowIndex].Cells[clnCode.Index].Value = (seq + 1).ToString();
            dgvItemDetail.Rows[e.RowIndex].Cells[clnIsEdit.Index].Value = "1";
            dgvItemDetail.Rows[e.RowIndex].Cells[clnstatus.Index].Value = "0";
            if (dgvItemDetail.Rows[e.RowIndex].Cells[clnId.Index].Value == null)
             dgvItemDetail.Rows[e.RowIndex].Cells[clnId.Index].Value = "0";
            
        }
        #endregion


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Save this...!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dgvItemDetail.Rows)
                {
                    if (row.Cells[clnCode.Index].Value != null &&
                        row.Cells[clnCatagory.Index].Value != null &&
                        row.Cells[clnItem.Index].Value != null &&
                        row.Cells[clnPriRate.Index].Value != null &&
                        row.Cells[clnSecRate.Index].Value != null &&
                        row.Cells[clnPriUnit.Index].Value != null && 
                        row.Cells[clnIsEdit.Index].Value.ToString()   == "1")
                        DML.ItemAdd_Edit(row.Cells[clnCatagory.Index].Value.ToString(),
                            (string)row.Cells[clnBarcode.Index].Value,
                            (string)row.Cells[clnId.Index].Value,
                            row.Cells[clnCode.Index].Value.ToString(),
                            row.Cells[clnItem.Index].Value.ToString(),
                            (string)row.Cells[clnKey.Index].Value,
                            row.Cells[clnPriRate.Index].Value.ToString(),
                            row.Cells[clnSecRate.Index].Value.ToString(),
                             (string)row.Cells[clnPriUnit.Index].Value,
                            (string)row.Cells[clnSecUnit.Index].Value,
                            (string)row.Cells[clnDefUnif.Index].Value,
                            (string)row.Cells[ClnQtyInPack.Index].Value,
                            Validation.BoolToint(Validation.ToBool(row.Cells[clnAlert.Index].Value)),
                            (string)row.Cells[clnLowStockAlert.Index].Value,
                            (string)row.Cells[clnOpnStock.Index].Value,
                            (string)row.Cells[clnOpnRate.Index].Value,
                            row.Cells[clnstatus.Index].Value.ToString(),
                            row.Cells[clnPCTCode.Index].Value.ToString());
                }
                MessageBox.Show("Record Successfully Saved..!");
                dtItemDetail = Query.ItemDetail();
                Fillform((string)cmbItemCatagory.SelectedValue);
            }
        }

        private void cmbItemCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( !FLogin && cmbItemCatagory.SelectedValue != null)
            Fillform((string)cmbItemCatagory.SelectedValue);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

      

        private void dgvItemDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (txtItemCode.Text != "" && txtItemCode.Text.Substring(txtItemCode.Text.Length - 1) == "\n")
            { 
                bool IsFind = false;
                for (int i = 0; i < dgvItemDetail.Rows .Count; i++)
                {
                    if ((string )dgvItemDetail[clnBarcode.Index,i].Value == txtItemCode.Text)
                    {
                        dgvItemDetail.CurrentCell = dgvItemDetail[clnBarcode.Index, i];
                        IsFind = true;   
                        break;                        
                    }
                }
                if (!IsFind)
                {
                    DataRow dr = dtItemDetail.Select("Barcode = '" + txtItemCode.Text + "'").FirstOrDefault(); 
                    if (dr != null)
                    {
                        dr = dtCatagory.Select("Code = '" + dr["fkitemcatagory"].ToString() + "'").FirstOrDefault();
                        MessageBox.Show("Barcode is exist in '" + dr["Title"].ToString() + "' catagory");
                        IsFind = true;
                    }
                }
                if (!IsFind)
                {
                    //dgvItemDetail.Rows.RemoveAt(dgvItemDetail.Rows.Count - 1);
                    int seq = 0;
                    try
                    {
                        seq = (int)(from DataGridViewRow row in dgvItemDetail.Rows
                                    where row.Cells[clnCode.Index].Value != null && row.Cells[clnCode.Index].Value.ToString() != ""
                                    select int.Parse(row.Cells[clnCode.Index].Value.ToString())).Max();
                    }
                    catch
                    {
                    }
                    object[] Param = new object[]{"0",(seq + 1).ToString(),txtItemCode.Text,cmbItemCatagory.SelectedValue,
                        "",null,"0","0","003","003","003","1",true,"0","0","0"};  
                    dgvItemDetail.Rows.Insert (0,Param);
                    //dgvItemDetail.Rows.Add();
                    dgvItemDetail.CurrentCell = dgvItemDetail[clnBarcode.Index, 0];
                }
                txtItemCode.Text = "";
                
                txtItemCode.Focus();
               
            }
            
        }

        private void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if ((string)dgvItemDetail[clnId.Index, dgvItemDetail.CurrentRow.Index].Value != "0")
                   {
                       DML.Change_ItemDetail_Status((string)dgvItemDetail[clnId.Index, dgvItemDetail.CurrentRow.Index].Value, "1");
                       dtItemDetail = Query.ItemDetail();
                   }
                    dgvItemDetail.Rows.RemoveAt(dgvItemDetail.CurrentRow.Index);                  
                    CalcTotals();
                    MessageBox.Show("Record Successfully Deleted..!");                
            }
        }
        void CalcTotals()
        {
            int TotItems = 0;
            try
            {
                TotItems = (from DataGridViewRow row in dgvItemDetail.Rows
                            where (string)row.Cells[clnId.Index].Value != "0"
                            select row).Count();

            }
            catch
            {

            }
            lblTotalItems.Text = "Total Items : " + (TotItems).ToString("N0");           
        }

        private void dgvItemDetail_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                dgvItemDetail.Rows[e.RowIndex].Selected = true;
                dgvItemDetail.CurrentCell = this.dgvItemDetail.Rows[e.RowIndex].Cells[e.ColumnIndex];
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

      

       
        
    }
}
