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
    public partial class frmAuthentication : Form
    {
        public frmAuthentication()
        {
            InitializeComponent();
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
           txtUserId.Text = "Admin";
           txtUserId.ReadOnly = true;
        }        
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;  
            DataTable dtUsers = Query.UserDetail(txtUserId.Text);
            if (dtUsers.Rows.Count > 0)
            {
                if (dtUsers.Rows[0]["Password"].ToString() == txtPassword.Text)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;                
                }
                else
                    MessageBox.Show("Invalid Password..!");  
            }
            else
                MessageBox.Show("Invalid User ID..!");
            Cursor.Current = Cursors.Default;          
        }

        private void frmLogIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys .Enter )
            {
                SendKeys.Send("{tab}");  
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;                     
               
        }

        

       
    }
}
