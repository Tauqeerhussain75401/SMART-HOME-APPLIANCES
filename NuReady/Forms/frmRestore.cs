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
            if (MessageBox.Show(
                "After restoring, data will be replaced with the backup file."
                + Environment.NewLine
                + "Do you want to restore your backup?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    prgbar.Style = ProgressBarStyle.Marquee; // better UX
                    prgbar.MarqueeAnimationSpeed = 30;

                    string connectionString = @"Data Source=.;Initial Catalog=master;User ID=mutahhar;Password=YOUR_PASSWORD";

                    // SQL script (parameterized for file path)
                    string sql = @"
                ALTER DATABASE POS_SP SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE POS_SP 
                FROM DISK = @BackupFile 
                WITH REPLACE, MEDIAPASSWORD = 'YOUR_MEDIA_PASSWORD';
                ALTER DATABASE POS_SP SET MULTI_USER;";

                    using (SqlConnection con = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 5000;

                        // parameter for file path
                        cmd.Parameters.Add("@BackupFile", SqlDbType.NVarChar).Value = txtLocation.Text.Trim();

                        con.Open();

                        // clear existing pool for this connection
                        SqlConnection.ClearPool(con);

                        cmd.ExecuteNonQuery();
                    }

                    prgbar.Style = ProgressBarStyle.Blocks;
                    prgbar.Value = 100;

                    MessageBox.Show(
                        "Backup restored successfully!"
                        + Environment.NewLine
                        + "Application will restart now.",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Application.Restart();
                }
                catch (Exception ex)
                {
                    prgbar.Style = ProgressBarStyle.Blocks;
                    prgbar.Value = 0;

                    MessageBox.Show(
                        "Restore failed!"
                        + Environment.NewLine
                        + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        //private void btnRestore_Click(object sender, EventArgs e)
        //{
        //    if ((MessageBox.Show("After restoring.Data will replace with backup file" + Environment.NewLine + "Do you want to restore your backup?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
        //    {
        //        prgbar.PerformStep();
        //        string connection = @"Data Source=.;Initial Catalog=master;User ID=mutahhar;Password=YOUR_PASSWORD";
        //        prgbar.PerformStep();
        //        try
        //        {
        //            string com = "ALTER DATABASE POS_SP SET SINGLE_USER WITH ROLLBACK IMMEDIATE; ALTER DATABASE POS_SP SET MULTI_USER; restore DATABASE POS_SP from DISK = '" + txtLocation.Text + "' WITH replace   ,mediapassword = 'YOUR_MEDIA_PASSWORD'";

        //           // prgbar.PerformStep();
        //            SqlConnection con = new SqlConnection(connection);
        //            con.Open();
        //           // prgbar.PerformStep();
        //            SqlCommand comm = new SqlCommand(com, con);
        //            comm.CommandTimeout = 5000;
        //            SqlConnection.ClearPool(clsConnection .con);
        //            comm.ExecuteNonQuery();
        //           // prgbar.Value = 100;
        //            con.Close();
        //            MessageBox.Show("Backup restore successfully......!" + Environment.NewLine + "Application will restart now....!");
        //            Application.Restart();  
        //        }
        //        catch(Exception ex)
        //        {
        //            prgbar.Value = 0;
        //            MessageBox.Show("Restore failes...!  " + ex.Message , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}
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
