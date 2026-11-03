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
    public partial class frmChartOfAccount : Form
    {
        DataTable dtChart;        
        public frmChartOfAccount()
        {
            InitializeComponent();
        }
        bool FlogIn = true;
        void Fill()
        {
            tvChartOfAcc.Nodes.Clear();
            dtChart = Query.get_ChartOfAcc();
            dtChart.PrimaryKey = new DataColumn[] { dtChart.Columns["Account"] };
            tvChartOfAcc.Nodes.Add("0", "Chart Of Account", null);
            for (int i = 0; i < dtChart.Rows.Count; i++)
            {

                //tvChartOfAcc.SelectedNode = tvChartOfAcc.Find(o => o.Name == dtChart.Rows[i]["parentid"].ToString());
                tvChartOfAcc.SelectedNode = tvChartOfAcc.Nodes.Find(dtChart.Rows[i]["parentid"].ToString(),true ).FirstOrDefault();
                tvChartOfAcc.SelectedNode.Nodes.Add(dtChart.Rows[i]["Account"].ToString(), dtChart.Rows[i]["Title"].ToString());
            }
            tvChartOfAcc.CollapseAll();
            tvChartOfAcc.Nodes [0].Expand ();
        }
        private void frmChartOfAccount_Load(object sender, EventArgs e)
        {
            Fill();
           
            
            FlogIn = false;
        }
        void FillDetail()
        {
            try
            {
                lblParent.Text = tvChartOfAcc.SelectedNode.Parent.Text;
                lblAccount.Text = tvChartOfAcc.SelectedNode.Name;
                lblTitle.Text = tvChartOfAcc.SelectedNode.Text;
                lblLevel.Text = (tvChartOfAcc.SelectedNode.Level).ToString();  
                lbltype .Text   = dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["Acctype"].ToString();
                lblCreated .Text  = dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["CreatedBy"].ToString() + " | " + ((DateTime)dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["CreatedTime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                lblEdit .Text  = dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["EditBy"].ToString() + " " + (dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["EditTime"].ToString() == "" ? "" : ((DateTime)dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["EditTime"]).ToString("| dd-MMM-yyyy hh:mm:ss tt"));   

            }
            catch (Exception)
            {
                lblParent.Text = "-";
                lblAccount.Text = "-";
                lblTitle.Text = "-";                
                lblLevel.Text = "-";
                lbltype.Text = "-";
                lblCreated.Text = "-";
                lblEdit.Text = "-";
            }
            

        
        }
        private void tvChartOfAcc_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tvChartOfAcc.SelectedNode.ContextMenuStrip = contextMenuStrip1;
            if (e.Node.Level == 5)
                tvChartOfAcc.SelectedNode.ContextMenuStrip.Items[0].Text = "&Add Into " + e.Node.Parent.Text;
            else
                tvChartOfAcc.SelectedNode.ContextMenuStrip.Items[0].Text = "&Add";
            if (!FlogIn)
            {
                FillDetail(); 
            
            }
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvChartOfAcc.SelectedNode != tvChartOfAcc.Nodes[0])
            {
                if (tvChartOfAcc.SelectedNode.Level == 5)
                    tvChartOfAcc.SelectedNode = tvChartOfAcc.SelectedNode.Parent;
                frmAccountAddEdit frm = new frmAccountAddEdit();
                frm.parentId = tvChartOfAcc.SelectedNode.Name;
                frm.Account = tvChartOfAcc.SelectedNode.Name + (tvChartOfAcc.SelectedNode.Nodes.Count + 1).ToString("D3");
                frm.Type = tvChartOfAcc.SelectedNode.Level == 4 ? "Detail" : "Parent";
                frm.level = (tvChartOfAcc.SelectedNode.Level + 1).ToString();
                frm.ShowDialog();
                tvChartOfAcc.CollapseAll();
                if (frm.DialogResult == DialogResult.Yes)
                {
                    Fill();
                    tvChartOfAcc.SelectedNode = tvChartOfAcc.Nodes.Find(frm.Account, true).FirstOrDefault();
                }
                else
                    tvChartOfAcc.SelectedNode = tvChartOfAcc.Nodes.Find(frm.parentId, true).FirstOrDefault();
                tvChartOfAcc.SelectedNode.Expand();
            }
        }
        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvChartOfAcc.SelectedNode != tvChartOfAcc.Nodes[0] && tvChartOfAcc.SelectedNode != tvChartOfAcc.Nodes[0].Nodes[tvChartOfAcc.SelectedNode.Index])
            {
                frmAccountAddEdit frm = new frmAccountAddEdit();
                frm.parentId = tvChartOfAcc.SelectedNode.Parent.Name;
                frm.Account = tvChartOfAcc.SelectedNode.Name;
                frm.Title = tvChartOfAcc.SelectedNode.Text;
                frm.Type = tvChartOfAcc.SelectedNode.Level == 5 ? "Detail" : "Parent";
                frm.level = tvChartOfAcc.SelectedNode.Level.ToString();
                frm.CreatedBy  = dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["CreatedBy"].ToString() + " | " + ((DateTime)dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["CreatedTime"]).ToString("dd-MMM-yyyy hh:mm:ss tt");
                frm.EditBy = dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["EditBy"].ToString() + " " + (dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["EditTime"].ToString() == "" ? "" : ((DateTime)dtChart.Rows.Find(tvChartOfAcc.SelectedNode.Name)["EditTime"]).ToString("| dd-MMM-yyyy hh:mm:ss tt"));   
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.Yes) Fill();
                tvChartOfAcc.CollapseAll();
                tvChartOfAcc.SelectedNode = tvChartOfAcc.Nodes.Find(frm.Account, true).FirstOrDefault();
                tvChartOfAcc.SelectedNode.Expand();
            }
        }
        private void tvChartOfAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
                addToolStripMenuItem_Click(null, null);
            else if (e.KeyCode == Keys.Enter)
                changeToolStripMenuItem_Click(null, null);
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvChartOfAcc.SelectedNode != tvChartOfAcc.Nodes[0] && tvChartOfAcc.SelectedNode != tvChartOfAcc.Nodes[0].Nodes[tvChartOfAcc.SelectedNode.Index]    )
            {
                if (MessageBox.Show("Are you sure?" + Environment.NewLine + "You want to Delete this...!", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    bool saved = DML.ChartOfAcc_delete(lblAccount.Text);
                    if (saved)
                    {
                        MessageBox.Show("Record Successfully Deleted..!");
                    }
                    tvChartOfAcc.SelectedNode.Remove();
                }
            }
        }
        private void btnOpenning_Click(object sender, EventArgs e)
        {
            frmOpeningBal frm = new frmOpeningBal();
            frm.ShowDialog(); 
        }

        private void tvChartOfAcc_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
             if (e.Button ==  MouseButtons .Right )  tvChartOfAcc.SelectedNode = e.Node;
        }

       

     
    }
}
