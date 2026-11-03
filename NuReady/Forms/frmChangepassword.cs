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
    public partial class frmChangepassword : Form
    {
        public frmChangepassword()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void frmChangepassword_Load(object sender, EventArgs e)
        {
            txtUserId.Text = UserInfo.UserId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to change your password...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtNewPassword.Text == txtConfirmPassword.Text)
                {
                    DataTable dtUsers = Query.UserDetail(UserInfo.UserId);
                    if (dtUsers.Rows.Count > 0)
                    {
                        if (dtUsers.Rows[0]["Password"].ToString() == txtOldPassword.Text)
                        {
                            DML.Change_Password(UserInfo.UserId ,txtNewPassword .Text  );
                            MessageBox.Show("Password Successfully changed..!");
                            this.Close();
                        }
                        else
                            MessageBox.Show("Invalid Password..!");
                    }                   
                }
                else
                    MessageBox.Show("Password not match");
            }
        }

        private void frmChangepassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");  
            }
        }
    }
}
