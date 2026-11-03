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
    public partial class frmReportParameters : Form
    {
        public frmReportParameters()
        {
            InitializeComponent();
        }
        internal string Reportname = "";
        internal string Paramname = "";
        bool FLogIn = true;
        private void ManageControls(Control[] ctrl)
        {
            int LocX = 12, LocY = 60;
            pnlControl.Location = new Point(235, LocY);
            LocY += 50;
            foreach (Control item in ctrl)
            {
                item.Visible = true;
                item.Location = new Point(LocX, LocY);
                LocY += item.Height + 3;
            }
            this.Size = new Size(430, LocY + 50);
			this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void ManageControlsLocation(Control[] ctrl)
        {
            int LocX = 12, LocY = 60;
            pnlControl.Location = new Point(235, LocY);
            LocY += 50;
            foreach (Control item in ctrl)
            {
                item.Location = new Point(LocX, LocY);
                LocY += item.Height + 3;
            }
            this.Size = new Size(430, LocY + 50);
        }
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReportView frm = new frmReportView();
            #region Account Statement
            if (Reportname == "Account Statement")
            {
                if (rdoViewReport.Checked)
                {
                    Reports.AccountStatement rpt = new Reports.AccountStatement();
                    DataTable dt = ReportQuery.AccountStatement((string)cmbAccount.SelectedValue, dtpFDate.Value, dtpTDate.Value);
                    rpt.SetDataSource(dt);
                    rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                    rpt.SetParameterValue("@Account", cmbAccount.Text);
                    rpt.SetParameterValue("@Fdate", dtpFDate.Value);
                    rpt.SetParameterValue("@Tdate", dtpTDate.Value);
                    frm.rptViewer.ReportSource = rpt;
                }
                else
                {
                    foreach (DataRowView item in chklstAccounts.CheckedItems)
                    { 
                        
                        Reports.AccountStatement rpt = new Reports.AccountStatement();
                        DataTable dt = ReportQuery.AccountStatement(item["Account"].ToString() , dtpFDate.Value, dtpTDate.Value);
                        rpt.SetDataSource(dt);
                        rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                        rpt.SetParameterValue("@Account", item["Title"].ToString());
                        rpt.SetParameterValue("@Fdate", dtpFDate.Value);
                        rpt.SetParameterValue("@Tdate", dtpTDate.Value);
                        rpt.PrintToPrinter(1, true, 0, 0);
                    }
                    return;
                }
            }
            #endregion

            #region Account Statement With Due Days
            if (Reportname == "Account Statement With Due Days")
            {
               
                if (rdoViewReport.Checked)
                {
                    Reports.AccountStatementWithdue rpt = new Reports.AccountStatementWithdue();
                    DataTable dt = ReportQuery.AccountStatementWithDue((string)cmbAccount.SelectedValue, dtpFDate.Value, dtpTDate.Value);
                    rpt.SetDataSource(dt);
                    rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                    rpt.SetParameterValue("@Account", cmbAccount.Text);
                    rpt.SetParameterValue("@Fdate", dtpFDate.Value);
                    rpt.SetParameterValue("@Tdate", dtpTDate.Value);
                    frm.rptViewer.ReportSource = rpt;
                }
                else
                {
                    foreach (DataRowView item in chklstAccounts.CheckedItems)
                    {
                        Reports.AccountStatementWithdue rpt = new Reports.AccountStatementWithdue();
                        DataTable dt = ReportQuery.AccountStatementWithDue(item["Account"].ToString(), dtpFDate.Value, dtpTDate.Value);
                        rpt.SetDataSource(dt);
                        rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                        rpt.SetParameterValue("@Account", item["Title"].ToString());
                        rpt.SetParameterValue("@Fdate", dtpFDate.Value);
                        rpt.SetParameterValue("@Tdate", dtpTDate.Value);
                        rpt.PrintToPrinter(1, true, 0, 0);
                    }
                    return;
                }
            }
            #endregion
            #region Stock Balance
            if (Reportname == "Stock Balance")
            {
                Reports.StockBalace rpt = new Reports.StockBalace();
                DataTable dt = ReportQuery.StockBalance(dtpFDate.Value, dtpTDate.Value, cmbFilter.Text, decimal.Parse(txtQty.Text), (string)cmbItemCatagory.SelectedValue, (string)cmbType.SelectedValue);                
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                rpt.SetParameterValue("@Fdate", dtpFDate.Value);
                rpt.SetParameterValue("@Tdate", dtpTDate.Value);
                frm.rptViewer.ReportSource = rpt;
            }
            #endregion
            #region Stock Ledger
            if (Reportname == "Stock Ledger")
            {
                Reports.StockLedger rpt = new Reports.StockLedger();
                DataTable dt = ReportQuery.StockLedger( (string)cmbItem.SelectedValue, dtpFDate.Value, dtpTDate.Value);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                rpt.SetParameterValue("@item", cmbItem.Text);
                rpt.SetParameterValue("@Fdate", dtpFDate.Value);
                rpt.SetParameterValue("@Tdate", dtpTDate.Value);
                frm.rptViewer.ReportSource = rpt;

            }
            #endregion
            #region Trial Balance
            if (Reportname == "Trial Balance")
            {
                Reports.CrTrialBalance rpt = new Reports.CrTrialBalance();
                DataTable dt = ReportQuery.TrialBalance (dtpFDate.Value, dtpTDate.Value);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                rpt.SetParameterValue("@Fdate", dtpFDate.Value);
                rpt.SetParameterValue("@Tdate", dtpTDate.Value);
                frm.rptViewer.ReportSource = rpt;

            }
            #endregion
            #region Balance Sheet
            if (Reportname == "Balance Sheet")
            {
                Reports.BalanceSheet rpt = new Reports.BalanceSheet();
                DataTable dt = ReportQuery.BalanceSheet(dtpAsOn.Value);
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
                rpt.SetParameterValue("@Tdate", dtpAsOn.Value);
                frm.rptViewer.ReportSource = rpt;

            }
			#endregion

			#region Income Summery
			if (Reportname == "Income Summery")
			{
				Reports.IncomeSummery rpt = new Reports.IncomeSummery();
				DataTable dt = ReportQuery.IncomeSummery(dtpFDate.Value, dtpTDate.Value);
				rpt.SetDataSource(dt);
				rpt.SetParameterValue("@companyname", CompanyInfo.CompanyName);
				rpt.SetParameterValue("@Fdate", dtpFDate.Value);
				rpt.SetParameterValue("@Tdate", dtpTDate.Value);
				frm.rptViewer.ReportSource = rpt;

			}
			#endregion
			#region Balance Detail
			else if (Reportname == "Balance Detail")
			{
				Reports.CrBalance rpt = new Reports.CrBalance();
				DataTable dt = ReportQuery.Balance((string)cmbAccount.SelectedValue, dtpAsOn.Value);
				rpt.SetDataSource(dt);
				rpt.SetParameterValue("@tdate", dtpAsOn.Value);
				rpt.SetParameterValue("@Account", ((DataRowView)cmbAccount.SelectedItem)["Title"].ToString());
				rpt.SetParameterValue("@CompanyName", CompanyInfo.CompanyName);
				frm.rptViewer.ReportSource = rpt;

			}
			#endregion

			#region Sales Summary
			else if (Reportname == "Sales Summary")
			{
				Reports.SalesSummay rpt = new Reports.SalesSummay();
				DataTable dt = ReportQuery.SaleSummary(dtpFDate.Value, dtpTDate.Value);
				rpt.SetDataSource(dt);
				rpt.SetParameterValue("@fromDate", dtpFDate.Value);
				rpt.SetParameterValue("@CompanyName", CompanyInfo.CompanyName);
				rpt.SetParameterValue("@toDate", dtpTDate.Value);
				frm.rptViewer.ReportSource = rpt;
			}
			#endregion

			frm.Show();
        }
        void FillAccouont()
        {
            cmbAccount.DataSource = Query.Accounts();
            cmbAccount.DisplayMember = "Title";
            cmbAccount.ValueMember = "Account";

        }
		private void frmReportParameters_Load(object sender, EventArgs e)
		{
			lblReport.Text = Reportname;
			Paramname = Paramname == "" ? Reportname : Paramname;
			#region Account Statement
			if (Paramname == "Account Statement")
			{
				FillAccouont();
				ManageControls(new Control[] { grpDateRange, grpPrintOption, grpAccounts });
				((ListBox)chklstAccounts).DataSource = Query.Accounts();
				((ListBox)chklstAccounts).DisplayMember = "Title";
				((ListBox)chklstAccounts).ValueMember = "Account";
			}
			#endregion
			#region Account Statement With Due Days
			if (Paramname == "Account Statement With Due Days")
			{
				FillAccouont();
				ManageControls(new Control[] { grpDateRange, grpPrintOption, grpAccounts });
				((ListBox)chklstAccounts).DataSource = Query.Accounts();
				((ListBox)chklstAccounts).DisplayMember = "Title";
				((ListBox)chklstAccounts).ValueMember = "Account";
			}
			#endregion
			#region Stock Balance
			if (Paramname == "Stock Balance")
			{
				FillControls.FillcmbType(cmbType);
				FillControls.FillcmbCatagory(cmbItemCatagory);
				ManageControls(new Control[] { grpDateRange, grpItemCatagory, grpType, grpFilter });
				cmbFilter.SelectedIndex = 0;
			}
			#endregion
			#region Stock Ledger
			if (Paramname == "Stock Ledger")
			{
				FillControls.FillcmbItem(cmbItem);
				ManageControls(new Control[] { grpDateRange, grpItem });
			}
			#endregion
			#region Trial Balance
			if (Paramname == "Trial Balance")
			{
				ManageControls(new Control[] { grpDateRange });
			}
			#endregion
			#region Balance Sheet
			if (Paramname == "Balance Sheet")
			{
				ManageControls(new Control[] { grpAsOn });
			}
			#endregion
			#region Income Summery
			if (Paramname == "Income Summery")
			{
				ManageControls(new Control[] { grpDateRange });
			}
			#endregion
			#region Customer Bill
			if (Paramname == "Customer Bill")
			{
				FillControls.FillcmbCustomers(cmbAccount);
				grpDateRange.Visible = true;
				grpDateRange.Location = new Point(12, 60);
				grpAccounts.Visible = true;
				grpAccounts.Location = new Point(12, 120);
				btnPrint.Location = new Point(btnPrint.Location.X, 180);
				this.Size = new Size(this.Size.Width, 250);
			}
			#endregion
			#region Balance Detail
			else if (Reportname == "Balance Detail")
			{
				FillControls.FillcmbAccLvl4Heads(cmbAccount);
				grpAccounts.Text = "Accounts Head";
				ManageControls(new Control[] { grpAsOn, grpAccounts });
			}
			#endregion

			#region Envelope
			else if (Reportname == "Envelope")
			{
				((ListBox)chklstAccounts).DataSource = Query.CustomerIndex();
				((ListBox)chklstAccounts).DisplayMember = "Title";
				((ListBox)chklstAccounts).ValueMember = "Code";
				ManageControls(new Control[] { grpSelectedAccounts });
			}
			#endregion

			#region Sales Summary
			else if (Reportname == "Sales Summary")
			{
				ManageControls(new Control[] { grpDateRange });
				this.Location = new Point(600,300 );

			}
			#endregion
			FLogIn = false;
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilter .SelectedValue == "All")
            {
                txtQty.Enabled = false;
                txtQty.Text = "";
            }
            else 
                txtQty.Enabled = true;            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rdoViewReport_CheckedChanged(object sender, EventArgs e)
        {
            if (Reportname == "Account Statement" )
            {
                if (!rdoViewReport.Checked)
                {
                    grpSelectedAccounts.Visible = true;
                    grpAccounts.Visible = false;                 
                    ManageControls(new Control[] { grpDateRange, grpPrintOption , grpSelectedAccounts });
                }
                else
                {
                    grpSelectedAccounts.Visible = false;
                    grpAccounts.Visible = true;                 
                    ManageControls(new Control[] { grpDateRange, grpPrintOption, grpAccounts });
                }

            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chklstAccounts.Items.Count; i++)
            {
                chklstAccounts.SetItemChecked(i, chkSelectAll.Checked);

            }
        }

    
        

        
    }
}
