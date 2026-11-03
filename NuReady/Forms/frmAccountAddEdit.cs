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
    public partial class frmAccountAddEdit : Form
    {
        public frmAccountAddEdit()
        {
            InitializeComponent();
        }
       public string parentId = "";
       public string Account = "";
       public string Title = "";
       public string level = ""; 
       public string Type = "";
       public string CreatedBy = "";
       public string EditBy = "";
        private void frmAccountAddEdit_Load(object sender, EventArgs e)
        {
            lblParent.Text = parentId;
            lblAccount.Text = Account;
            txtTitle.Text = Title;
            lblLevel.Text = level; 
            lblType.Text  = Type;
            lblCreated.Text = CreatedBy;
            lblEdit.Text = EditBy ;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Save this...!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool saved = DML.ChartOfAccAdd_Edit(lblAccount.Text, txtTitle.Text, lblParent.Text,
                     lblType.Text ,lblLevel.Text);
                if (saved)
                {
                    MessageBox.Show("Record Successfully Saved..!");
                    this.DialogResult = DialogResult.Yes;  
                    this.Close();
                }                
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            if (txtTitle.Text.Length > 0) btnSave.Enabled = true;
            else btnSave.Enabled = false;  
        }

        private void frmAccountAddEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();  
        }

      

    }
}
