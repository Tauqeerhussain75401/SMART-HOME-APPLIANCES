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
    public partial class frmSupplierInfo : Form
    {
        bool Flogin = true;
        public frmSupplierInfo()
        {
            InitializeComponent();
        }
        void FillCombo()
        {
            cmbCustomerCode.DataSource = Query.SupplierIndex();
            cmbCustomerCode.DisplayMember = "Title";
            cmbCustomerCode.ValueMember  = "Code";
            cmbCustomerCode.SelectedIndex = -1; 
        }
        void FillForm(string  Code)
        {
            if (!Flogin && Code != "")
            {
                DataTable dt = Query.SupplierDetail(Code);
                if (dt.Rows.Count > 0)
                {
                    txtEmail.Text = dt.Rows[0]["email"].ToString();
                    txtFax.Text = dt.Rows[0]["fax"].ToString();
                    txtNic.Text = dt.Rows[0]["CNIC"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();
                    txtQualification.Text = dt.Rows[0]["Qualification"].ToString();
                    txtPhone1.Text = dt.Rows[0]["phone1"].ToString();
                    txtPhone2.Text = dt.Rows[0]["phone2"].ToString();
                    txtSMSNumber.Text = dt.Rows[0]["SMSNumber"].ToString();
                    txtIBAN.Text = dt.Rows[0]["IBAN"].ToString();
                    lblCreatedBy.Text = dt.Rows[0]["Createdby"].ToString() + (dt.Rows[0]["Createdtime"].ToString() == "" ? "" : " | " + ((DateTime)dt.Rows[0]["Createdtime"]).ToString("dd-MMM-yyyy hh:mm:ss tt"));
                    lblEditBy.Text = dt.Rows[0]["EditBy"].ToString() + (dt.Rows[0]["Edittime"].ToString() == "" ? "" : " | " + ((DateTime)dt.Rows[0]["Edittime"]).ToString("dd-MMM-yyyy hh:mm:ss tt"));
                    chkSMSAlert.Checked = dt.Rows[0]["SMSAlert"].ToString() == "1" ? true : false;
                    chkEmailAlert.Checked = dt.Rows[0]["EmailAlert"].ToString() == "1" ? true : false;
                    chkActive.Checked = dt.Rows[0]["Active"].ToString() == "1" ? true : false;
                    chkShowInSales.Checked = dt.Rows[0]["ShowInSales"].ToString() == "1" ? true : false;
                }
                else
                    Validation.Clear(this, new Label  []{ lblCreatedBy,lblEditBy  });  
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            Validation.Clear(this, new Label[] { lblCreatedBy, lblEditBy });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void frmCustomerInfo_KeyDown(object sender, KeyEventArgs e)
        {
          
             if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{tab}");  
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Save this...!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
              
                    if (cmbCustomerCode.SelectedValue != null)
                    {
                        string Code = DML.SupplierInfo_Add_Edit((string)cmbCustomerCode.SelectedValue, txtEmail.Text, txtFax.Text, txtNic.Text,
                            txtAddress.Text, txtQualification.Text, txtPhone1.Text, txtPhone2.Text, txtSMSNumber.Text, txtIBAN.Text,
                            (chkSMSAlert.Checked ? "1" : "0"), (chkEmailAlert.Checked ? "1" : "0"), null, (chkActive.Checked ? "1" : "0"), (chkShowInSales.Checked ? "1" : "0"), "0");
                        MessageBox.Show("Record Successfully Saved..!");
                        cmbCustomerCode.SelectedValue = Code;
                        FillForm(Code);
                    }
               
            }
        }

        private void frmCustomerInfo_Load(object sender, EventArgs e)
        {
            FillCombo();
            Flogin = false;
        }

        private void cmbCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Flogin)
            {
                    FillForm((string )cmbCustomerCode.SelectedValue );
            }
        }

    

       
    }
}
