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
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
           
        }
        void INISetting()
        {
            //if (!Directory.Exists("Settings.ini"))
            //File.Create("Settings.ini");
            //if (!Directory.Exists(@"C:\\Con.ini"))
            // File.Create("Prii.ini");
            INIFile MyINI = new INIFile("Settings.ini");
         //   ConfigInfo.ThermalPrinterName = INIFile.ReadValue("PrinterSetting", "ThermalPrinter");
            // bool a =  INIFile.WriteValue("PrinterSetting", "ThermalPrinter", "ABC");


        }
        private void frmLogIn_Load(object sender, EventArgs e)
        {
            INISetting();
            UserInfo.UserId = "Log Out";

            //clsConnection.Con();
            ////temporary////
                DataTable dt = Query.get_CompanyInfo();
                CompanyInfo.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                CompanyInfo.UrCompanyName = dt.Rows[0]["UrCompanyName"].ToString(); 
                CompanyInfo.Address = dt.Rows[0]["Address"].ToString();          
                CompanyInfo.Cell = dt.Rows[0]["Cell"].ToString();          
                CompanyInfo.Cell2 = dt.Rows[0]["Cell2"].ToString();


            DataRow row = dt.Rows[0];
            CompanyInfo.ntn = dt.Columns.Contains("ntn") && row["ntn"] != DBNull.Value
                ? row["ntn"].ToString()
                : "";

            CompanyInfo.strn = dt.Columns.Contains("strn") && row["strn"] != DBNull.Value
                ? row["strn"].ToString()
                : "";


            //  txtUserId.Text = "Admin";
            //txtPassword.Text = "1";
            //  btnLogIn_Click(null, null); 
        }
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            Cursor.Current = Cursors.WaitCursor;  
            DataTable dtUsers = Query.UserDetail(txtUserId.Text);
            if (dtUsers.Rows.Count > 0)
            {
                if (dtUsers.Rows[0]["Password"].ToString() == txtPassword.Text)
                {
                   // frmMain frm = new frmMain();
                   frmMainMenu frm = new frmMainMenu();
                    frm.Owner = this;
                    UserInfo.UserId = (string )dtUsers.Rows[0]["userid"];
                    UserInfo.UserName  = (string)dtUsers.Rows[0]["username"];
                    UserInfo.LogInDateTime  = (DateTime )dtUsers.Rows[0]["date"];                       
                    //clsConnection.Con();                       
                    frm.ShowDialog();                    
                }
                else
                    MessageBox.Show("Invalid Password..!");  
            }
            else
                MessageBox.Show("Invalid User ID..!");
            progressBar1.Visible = false ;
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
            Application.Exit();
               
        }

        

       
    }
}
