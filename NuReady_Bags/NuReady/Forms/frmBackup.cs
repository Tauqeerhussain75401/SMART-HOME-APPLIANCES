using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO.Compression;
using System.IO;
namespace NuReady
{
   
    public partial class frmBackup : Form
    {
        OpenFileDialog dialog = new OpenFileDialog();
        public frmBackup()
        {
            InitializeComponent();
        }
        private void btnLocation_Click(object sender, EventArgs e)
        {            

            string ext = "bak";
            dialog.Filter = "BAK files(*.BAK)|*.bak";
            dialog.CheckFileExists = false;
            dialog.DefaultExt = ext;
            dialog.AddExtension = true;
            dialog.ShowDialog();
            txtLocation.Text = dialog.FileName;    
        }
        private void btnBackup_Click(object sender, EventArgs e)
        {
            prgbar.PerformStep(); 
            string connection = @"Data Source=.;Initial Catalog=master;User ID=mutahhar;Password=YOUR_PASSWORD";
            string com = "BACKUP DATABASE POS_SP TO  DISK = '" + txtLocation.Text + "'  with mediapassword = 'YOUR_MEDIA_PASSWORD'";
            SqlConnection con = new SqlConnection(connection);
            prgbar.PerformStep();
            con.Open();
            SqlCommand comm = new SqlCommand(com, con);
            comm.ExecuteNonQuery();
            prgbar.PerformStep();
            con.Close();
            
            prgbar.Value = 100; 
            MessageBox.Show("Backup successfully saved......!");
        }
        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            if (txtLocation.Text != "")
                btnBackup.Enabled = true;
            else
                btnBackup.Enabled = false;
        }
        private void frmBackup_Load(object sender, EventArgs e)
        {
            txtLocation_TextChanged(null, null);  
        }
    }
}
