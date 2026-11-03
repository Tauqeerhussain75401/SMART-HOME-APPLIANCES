using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace NuReady
{
    public partial class frmRestore : Form
    {
        public frmRestore()
        {
            InitializeComponent();
        }
        private void btnLocation_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog(); 
            string ext = "bak";
            dialog.Filter = "BAK files(*.BAK)|*.bak";
            dialog.CheckFileExists = true;
            dialog.DefaultExt = ext;
            dialog.AddExtension = true;
            dialog.ShowDialog();
            txtLocation.Text = dialog.FileName;
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("After restoring.Data will replace with backup file" + Environment.NewLine + "Do you want to restore your backup?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
            {
                prgbar.PerformStep();
                string connection = @"Data Source=.;Initial Catalog=master;User ID=mutahhar;Password=YOUR_PASSWORD";
                prgbar.PerformStep();
                try
                {
                    string com = "ALTER DATABASE POS_SP SET SINGLE_USER WITH ROLLBACK IMMEDIATE; ALTER DATABASE POS_SP SET MULTI_USER; restore DATABASE POS_SP from DISK = '" + txtLocation.Text + "' WITH replace   ,mediapassword = 'YOUR_MEDIA_PASSWORD'";
                    
                   // prgbar.PerformStep();
                    SqlConnection con = new SqlConnection(connection);
                    con.Open();
                   // prgbar.PerformStep();
                    SqlCommand comm = new SqlCommand(com, con);
                    comm.CommandTimeout = 5000;
                    SqlConnection.ClearPool(clsConnection .con);
                    comm.ExecuteNonQuery();
                   // prgbar.Value = 100;
                    con.Close();
                    MessageBox.Show("Backup restore successfully......!" + Environment.NewLine + "Application will restart now....!");
                    Application.Restart();  
                }
                catch(Exception ex)
                {
                    prgbar.Value = 0;
                    MessageBox.Show("Restore failes...!  " + ex.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (txtLocation.Text != "")
                btnRestore.Enabled = true;
            else
                btnRestore.Enabled = false;
        }
        private void frmRestore_Load(object sender, EventArgs e)
        {
            txtLocation_TextChanged(null, null); 
        }
    }
}
