using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NuReady.Forms
{
    public partial class frmHRInfo : Form
    {
        public frmHRInfo()
        {
            InitializeComponent();
        }

        void FillQuery()
        {
            DataTable dt = Query.HRIndex();
            dgvQuery.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvQuery.Rows.Add(dt.Rows[i]["ID"].ToString(),
                    dt.Rows[i]["Name"].ToString(),
                    dt.Rows[i]["Designation"].ToString(),
                    dt.Rows[i]["JoiningDate"].ToString(),
                    dt.Rows[i]["SalaryType"].ToString(),
                    dt.Rows[i]["Salary"].ToString()
                    );
            }
        }
        void FillDetail(string ID)
        {
            DataTable dt = Query.HRIndex(ID );
            Validation.Clear(tbDetail);
            if (dt.Rows.Count > 0)
            {
                txtHRID.Text = dt.Rows[0]["ID"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                cmbGender.Text = dt.Rows[0]["Gender"].ToString();  
                dtpDOB.Value = (DateTime)dt.Rows[0]["DOB"];
                cmdMaritialStatus.Text = dt.Rows[0]["MaritialStatus"].ToString();
                txtCNIC.Text = dt.Rows[0]["CNIC"].ToString();
                dtpAppointmentDate.Value = (DateTime)dt.Rows[0]["AppointmentDate"];
                dtpJoiningDate.Value = (DateTime)dt.Rows[0]["JoiningDate"];
                cmbDesignation.Text = dt.Rows[0]["Designation"].ToString();
                cmbSalaryType.Text = dt.Rows[0]["SalaryType"].ToString();
                ntxtSalary.Value  = (decimal)dt.Rows[0]["Salary"];
                ntxtLeaveCharges.Value = (decimal)dt.Rows[0]["LeaveCharges"];
                ntxtOvertime.Value = (decimal)dt.Rows[0]["Overtime"];
                cmbExpenseAccount.SelectedValue   = dt.Rows[0]["ExpenseAccount"];
                cmbPayable.SelectedValue = dt.Rows[0]["PayableAccount"];  
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHRInfo_Load(object sender, EventArgs e)
        {
            FillQuery();
            FillControls.FillcmbExpense(cmbExpenseAccount);
            FillControls.FillcmbPayable(cmbPayable); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to save this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (UserInfo.UserId != "Admin")
                {
                    frmAuthentication frm = new frmAuthentication();
                    if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }
                }
                DML.HRInfo_Add_Edit(txtHRID.Text, txtName.Text, txtFatherName.Text, cmbGender.Text, dtpDOB.Value, cmdMaritialStatus.Text,
                    txtCNIC.Text, dtpAppointmentDate.Value, dtpJoiningDate.Value, cmbDesignation.Text, cmbSalaryType.Text, ntxtSalary.Value,
                    ntxtLeaveCharges.Value, ntxtOvertime.Value, (string)cmbExpenseAccount.SelectedValue, (string)cmbPayable.SelectedValue, 0);                
                MessageBox.Show("Record Successfully Saved...!");
                FillQuery();
            }
        }

        private void dgvQuery_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FillDetail(dgvQuery[clnHRID.Index,e.RowIndex].Value.ToString());
            tabControl1.SelectedTab = tbDetail;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Validation.Clear(tbDetail );  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtHRID.Text != "")
            {
                if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (UserInfo.UserId != "Admin")
                    {
                        frmAuthentication frm = new frmAuthentication();
                        if (frm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        {
                            return;
                        }
                    }
                    DML.Execute("update HRinfo set status = 1 where id = '" + txtHRID.Text + "'");
                    Validation.Clear(tbDetail); 
                    MessageBox.Show("Record Successfully Deleted...!");
                }
            }
        }

        private void frmHRInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tabControl1 .SelectedTab == tbDetail )
            {
                SendKeys.Send("{tab}"); 
            }
        }

       
    }
}
