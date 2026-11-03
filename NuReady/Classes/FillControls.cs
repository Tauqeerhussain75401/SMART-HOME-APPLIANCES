using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NuReady
{
    class FillControls
    {
        internal static void FillcmbAccLvl4Heads(ComboBox cmb)
        {
            cmb.DataSource = Query.AccountsLvl4Head();
            cmb.DisplayMember = "title";
            cmb.ValueMember = "account";
            cmb.SelectedIndex = -1;
        }   
       internal static void FillcmbPartyAcc(ComboBox cmb)
        {
            cmb.DataSource = Query.DetailAccounts();
            cmb.DisplayMember = "title";
            cmb.ValueMember = "account";
            cmb.SelectedIndex = -1;
        }   
       internal static void  FillcmbNarration(ComboBox cmb)
        {
            cmb.DataSource = Query.NarrationIndex();
            cmb.DisplayMember = "Title";
            cmb.ValueMember = "Code";
            cmb.SelectedIndex = -1;
        }
       internal static void FillcmbCashAndBankAcc(ComboBox cmb)
       {
           cmb.DataSource = Query.CashBanks();
           cmb.DisplayMember = "title";
           cmb.ValueMember = "account";
           cmb.SelectedIndex = -1;
       }   
       internal static void FillcmbItem(ComboBox cmb)
       {
           cmb.DataSource = Query.ItemIndex();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "ID";
           cmb.SelectedIndex = -1;
       }
       internal static void FillcmbItem(DataGridViewComboBoxColumn cmb)
       {
           cmb.DataSource = Query.ItemIndex();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "ID";
       }
       internal static void FillclnCatagory(DataGridViewComboBoxColumn cmb)
       {
           cmb.DataSource = Query.ItemCatagoryIndexActive ();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
    
           
       }
       internal static void FillcmbCatagory(ComboBox  cmb)
       {
           cmb.DataSource = Query.ItemCatagoryIndexActive();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
           cmb.SelectedIndex = -1;
       }
       internal static void FillcmbType(ComboBox cmb)
       {
           cmb.DataSource = Query.ItemTypeIndexActive();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
           cmb.SelectedIndex = -1;
       }
       internal static void FillclnItemType(DataGridViewComboBoxColumn cmb)
       {
           cmb.DataSource = Query.ItemTypeIndexActive();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
       }
       internal static void FillclnCustomers(DataGridViewComboBoxColumn cln)
       {
           cln.DataSource = Query.CustomerIndex();
           cln.DisplayMember = "Title";
           cln.ValueMember = "Code";
       }
       internal static void FillclnPayable(DataGridViewComboBoxColumn cln)
       {
           cln.DataSource = Query.PayableIndex();
           cln.DisplayMember = "Title";
           cln.ValueMember = "Code";
       }
       internal static void FillcmbPayable(ComboBox  cmb)
       {
           cmb.DataSource = Query.PayableIndex();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
       }
       internal static void FillclnExpense(DataGridViewComboBoxColumn cln)
       {
           cln.DataSource = Query.ExpenseIndex();
           cln.DisplayMember = "Title";
           cln.ValueMember = "Code";
       }
       internal static void FillcmbExpense(ComboBox cmb)
       {
           cmb.DataSource = Query.ExpenseIndex();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
       }
       internal static void FillcmbCustomers(ComboBox cmb)
       {
           cmb.DataSource = Query.CustomerIndex();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
           cmb.SelectedIndex = -1; 
       }
       internal static void FillcmbCustomers(ComboBox cmb,string SalesHead)
       {
           cmb.DataSource = Query.CustomerIndex(SalesHead );
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
           cmb.SelectedIndex = -1;
       }
       internal static void FillcmbCustomersAndVenders(ComboBox cmb)
       {
           cmb.DataSource = Query.CustomerAndVenderIndex();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
           cmb.SelectedIndex = -1;
       }

       internal static void FillcmbSuppliers(ComboBox cmb)
       {
           cmb.DataSource = Query.SupplierIndex();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "Code";
           cmb.SelectedIndex = -1;
       }
       
        internal static void FillcmbTransporters(ComboBox cmb)
       {
           cmb.DataSource = Query.TransporterIndex ();
           cmb.DisplayMember = "Title";
           cmb.ValueMember = "ID";
           cmb.SelectedIndex = -1;
       }
        internal static void FillclnTransporters(DataGridViewComboBoxColumn cln)
        {
            cln.DataSource = Query.TransporterIndex();
            cln.DisplayMember = "Title";
            cln.ValueMember = "ID";         
        }

    }
}
