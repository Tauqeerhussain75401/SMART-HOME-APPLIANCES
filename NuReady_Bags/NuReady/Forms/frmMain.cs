using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
namespace NuReady
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void chartOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChartOfAcc chart = new frmChartOfAcc();
            chart.Show();  
        }
        Form frmLogin;
        Thread th;
        void INISetting()
        {
            //if (!Directory.Exists("Settings.ini"))
            //File.Create("Settings.ini");
            //if (!Directory.Exists(@"C:\\Con.ini"))
           // File.Create("Prii.ini");
            //INIFile MyINI = new INIFile("Settings.ini");
            ConfigInfo.ThermalPrinterName = INIFile.ReadValue("PrinterSetting", "ThermalPrinter");  
         // bool a =  INIFile.WriteValue("PrinterSetting", "ThermalPrinter", "ABC");


        }
        private void CompanyCetreLoc()
        {
            lblCompanyName.Location = new Point((this.Size.Width - lblCompanyName.Size.Width) / 2, lblCompanyName.Location.Y);
            lblUrCompanyName.Location = new Point((this.Size.Width - lblUrCompanyName.Size.Width) / 2, lblUrCompanyName.Location.Y);
            picLogo.Location = new Point((this.Size.Width - picLogo.Size.Width) / 2, picLogo.Location.Y);
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            INISetting();
            StlblUserId .Text =  UserInfo.UserName;
            this.Text = CompanyInfo.CompanyName;
            lblCompanyName.Text = CompanyInfo.CompanyName; 
            lblUrCompanyName.Text = CompanyInfo.UrCompanyName;
            CompanyCetreLoc();
            frmLogin = this.Owner;
            frmLogin.Hide();
            DataTable dtCompany = Query.CompanyDetail();
            CompanyInfo.Description = dtCompany.Rows[0]["Descr"].ToString();
            CompanyInfo.Address = dtCompany.Rows[0]["Address"].ToString();
            CompanyInfo.Cell  = dtCompany.Rows[0]["Cell"].ToString();
            CompanyInfo.Cell2  = dtCompany.Rows[0]["Cell2"].ToString();
            th = new Thread(Connection );
            th.Start();
            //string b = INIFile.ReadValue("PrinterSetting", "ThermalPrinter", @"C:\Settings.ini");
          // MessageBox.Show(b);  
        }

        
        void Connection()
        {
            while (true)
            {
                try
                {
                    Query.getData(" ");
                    //StConStatus.Text = clsConnection.con.State.ToString(); 
                }
                catch (Exception)
                {
                    try
                    {
                        //clsConnection.con = clsConnection.Con();
                        clsConnection.Con();
                    }
                    catch
                    {
                    }
                }

                StConStatus.Text = clsConnection.con.State.ToString();
            }
            
        }
        private void customerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomerInfo cus = new frmCustomerInfo();
            cus.Show(); 
        }

        private void paymentVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPayments frm = new frmPayments();
            frm.Show(); 

        }

        private void narrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNarration narr = new frmNarration();
            narr.Show(); 
        }

        private void receiptVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReceipt frm = new frmReceipt();
            frm.Show(); 
        }

        

        private void unitsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itemDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmItemDetail frm = new frmItemDetail();
            frm.Show(); 
        }

      

        private void saleOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSale frm = new frmSale();
            frm.Show(); 
        }
       

        

        private void unitIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUnits frm = new frmUnits();
            frm.Show(); 
        }

        private void accountStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Account Statement";
            frm.Show();
 
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchase frm = new frmPurchase();
            frm.Show(); 
        }

        private void stockBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Stock Balance";
            frm.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            th.Abort();
            Application.Exit();  
        }

      

        private void journalVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJournalVoucher frm = new frmJournalVoucher();
            frm.Show(); 
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X)
            {
                if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to exit this...!", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            else if (e.KeyCode == Keys.F12)
            {
                if ((Application.OpenForms["frmSale"]) != null)
                    Application.OpenForms["frmSale"].Focus();
                else
                {
                    frmSale frm = new frmSale();
                    frm.Show();
                }
            }
            else if (e.KeyCode == Keys.F11)
            {
                if ((Application.OpenForms["frmPurchase"]) != null)
                    Application.OpenForms["frmPurchase"].Focus();
                else
                {
                    frmPurchase frm = new frmPurchase();
                    frm.Show();
                }
            }
            else if (e.KeyCode == Keys.F8)
            {
                if ((Application.OpenForms["frmChartOfAcc"]) != null)
                    Application.OpenForms["frmChartOfAcc"].Focus();
                else
                {
                    frmChartOfAcc frm = new frmChartOfAcc();
                    frm.Show();
                }
            }
            //////////Calculator/////////////////////// 
            else if (e.KeyCode == Keys.F1)
            {
                System.Diagnostics.Process calcprc =  System.Diagnostics.Process.Start("calc.exe");
                calcprc.WaitForInputIdle(); 
                
            }   
        }

        private void itemLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Stock Ledger";
            frm.Show();
        }

        private void incomeSummeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Income Summery";
            frm.Show();
        }

        private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Trial Balance";
            frm.Show();
        }

        private void balanceSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Balance Sheet";
            frm.Show();
        }

        private void supplierInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSupplierInfo supp = new frmSupplierInfo();
            supp.Show(); 
        }

        private void monthlyInstallmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Monthly Installments";
            frm.Show();
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            CompanyCetreLoc();
        
        }

        private void bankReconcilationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBankReconcilation frm = new frmBankReconcilation();
            frm.Show(); 
        }

        private void accountStatementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Account Statement (Urdu)";
            frm.Paramname = "Account Statement";
            frm.Show();
        }

        private void balanceSheetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Balance Sheet (Urdu)";
            frm.Paramname = "Balance Sheet";
            frm.Show();
        }

        private void customerBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Customer Bill (Urdu)";
            frm.Paramname = "Customer Bill";
            frm.Show();
        }

        private void itemCatagoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmItemCatagory frm = new frmItemCatagory();
            frm.Show(); 
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackup frm = new frmBackup();
            frm.ShowDialog();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmConfiguration frm = new Forms.frmConfiguration();
            frm.Show(); 
        }

        

     

        private void detailAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChartOfAccLvl5 frm = new frmChartOfAccLvl5();
            frm.Show(); 
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frmChartOfAccLvl5 frm = new frmChartOfAccLvl5();
            frm.Show();  
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmSale frm = new frmSale();
            frm.Show();  
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmPurchase frm = new frmPurchase();
            frm.Show();  
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmReceipt frm = new frmReceipt();
            frm.Show(); 
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmPayments frm = new frmPayments();
            frm.Show(); 
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmJournalVoucher frm = new frmJournalVoucher();
            frm.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmBankReconcilation frm = new frmBankReconcilation();
            frm.Show(); 
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.bizgrip.somee.com");  
        }

        private void pictureBox7_Click_1(object sender, EventArgs e)
        {
            frmItemDetail frm = new frmItemDetail();
            frm.Show(); 
        }

        private void stockAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockAdjustment frm = new frmStockAdjustment();
            frm.Show();
        }


        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmChangepassword frm = new Forms.frmChangepassword();
            frm.Show(); 
        }

        private void accountBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Balance Detail";
            frm.Paramname = "Balance Detail";
            frm.Show();
        }

        private void itemLedgerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Stock Ledger";
            frm.Paramname = "Stock Ledger";
            frm.Show();
        }

        private void saleReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaleReturn frm = new frmSaleReturn();
            frm.Show();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRestore frm = new frmRestore();
            frm.Show();
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchaseReturn frm = new frmPurchaseReturn();
            frm.Show();
        }

        private void incomeSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Income Summery";
            frm.Paramname = "Income Summery";
            frm.Show();
            
        }

        private void balanceSheetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Balance Sheet";
            frm.Paramname = "Balance Sheet";
            frm.Show();
            
        }

        private void hRInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.frmHRInfo frm = new Forms.frmHRInfo();
            frm.Show();                                                                      
        }
        

        private void enToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Envelope";
            frm.Paramname = "Envelope";
            frm.Show();
        }

        private void accountStatementWithDueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportParameters frm = new frmReportParameters();
            frm.Reportname = "Account Statement With Due Days";
            frm.Show();
        }

        
        }

        

        

      
    }

