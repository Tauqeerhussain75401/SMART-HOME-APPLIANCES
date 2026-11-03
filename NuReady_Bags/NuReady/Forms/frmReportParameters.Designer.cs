namespace NuReady
{
    partial class frmReportParameters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportParameters));
			this.grpDateRange = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.dtpTDate = new System.Windows.Forms.DateTimePicker();
			this.dtpFDate = new System.Windows.Forms.DateTimePicker();
			this.grpAsOn = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.dtpAsOn = new System.Windows.Forms.DateTimePicker();
			this.grpAccounts = new System.Windows.Forms.GroupBox();
			this.cmbAccount = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.lblReport = new System.Windows.Forms.Label();
			this.grpItem = new System.Windows.Forms.GroupBox();
			this.cmbItem = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.grpFilter = new System.Windows.Forms.GroupBox();
			this.cmbFilter = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.pnlControl = new System.Windows.Forms.Panel();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.grpItemCatagory = new System.Windows.Forms.GroupBox();
			this.cmbItemCatagory = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.grpType = new System.Windows.Forms.GroupBox();
			this.cmbType = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.grpSelectedAccounts = new System.Windows.Forms.GroupBox();
			this.chkSelectAll = new System.Windows.Forms.CheckBox();
			this.chklstAccounts = new System.Windows.Forms.CheckedListBox();
			this.grpPrintOption = new System.Windows.Forms.GroupBox();
			this.rdoDirectPrint = new System.Windows.Forms.RadioButton();
			this.rdoViewReport = new System.Windows.Forms.RadioButton();
			this.txtQty = new NuReady.DecimalTextbox(this.components);
			this.grpDateRange.SuspendLayout();
			this.grpAsOn.SuspendLayout();
			this.grpAccounts.SuspendLayout();
			this.grpItem.SuspendLayout();
			this.grpFilter.SuspendLayout();
			this.pnlControl.SuspendLayout();
			this.grpItemCatagory.SuspendLayout();
			this.grpType.SuspendLayout();
			this.grpSelectedAccounts.SuspendLayout();
			this.grpPrintOption.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpDateRange
			// 
			this.grpDateRange.Controls.Add(this.label1);
			this.grpDateRange.Controls.Add(this.label7);
			this.grpDateRange.Controls.Add(this.dtpTDate);
			this.grpDateRange.Controls.Add(this.dtpFDate);
			this.grpDateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpDateRange.Location = new System.Drawing.Point(16, 74);
			this.grpDateRange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpDateRange.Name = "grpDateRange";
			this.grpDateRange.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpDateRange.Size = new System.Drawing.Size(521, 70);
			this.grpDateRange.TabIndex = 0;
			this.grpDateRange.TabStop = false;
			this.grpDateRange.Text = "Date Range ";
			this.grpDateRange.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(267, 32);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 20);
			this.label1.TabIndex = 196;
			this.label1.Text = "To :";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(8, 32);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(58, 20);
			this.label7.TabIndex = 195;
			this.label7.Text = "From :";
			// 
			// dtpTDate
			// 
			this.dtpTDate.CustomFormat = "dd-MMM-yyyy";
			this.dtpTDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpTDate.Location = new System.Drawing.Point(317, 28);
			this.dtpTDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dtpTDate.Name = "dtpTDate";
			this.dtpTDate.Size = new System.Drawing.Size(173, 26);
			this.dtpTDate.TabIndex = 194;
			// 
			// dtpFDate
			// 
			this.dtpFDate.CustomFormat = "dd-MMM-yyyy";
			this.dtpFDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpFDate.Location = new System.Drawing.Point(76, 28);
			this.dtpFDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dtpFDate.Name = "dtpFDate";
			this.dtpFDate.Size = new System.Drawing.Size(173, 26);
			this.dtpFDate.TabIndex = 193;
			// 
			// grpAsOn
			// 
			this.grpAsOn.Controls.Add(this.label2);
			this.grpAsOn.Controls.Add(this.dtpAsOn);
			this.grpAsOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpAsOn.Location = new System.Drawing.Point(16, 151);
			this.grpAsOn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpAsOn.Name = "grpAsOn";
			this.grpAsOn.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpAsOn.Size = new System.Drawing.Size(277, 70);
			this.grpAsOn.TabIndex = 1;
			this.grpAsOn.TabStop = false;
			this.grpAsOn.Text = "As On ";
			this.grpAsOn.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 30);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 20);
			this.label2.TabIndex = 198;
			this.label2.Text = "Date :";
			// 
			// dtpAsOn
			// 
			this.dtpAsOn.CustomFormat = "dd-MMM-yyyy";
			this.dtpAsOn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpAsOn.Location = new System.Drawing.Point(76, 26);
			this.dtpAsOn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.dtpAsOn.Name = "dtpAsOn";
			this.dtpAsOn.Size = new System.Drawing.Size(173, 26);
			this.dtpAsOn.TabIndex = 197;
			// 
			// grpAccounts
			// 
			this.grpAccounts.Controls.Add(this.cmbAccount);
			this.grpAccounts.Controls.Add(this.label10);
			this.grpAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpAccounts.Location = new System.Drawing.Point(16, 229);
			this.grpAccounts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpAccounts.Name = "grpAccounts";
			this.grpAccounts.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpAccounts.Size = new System.Drawing.Size(521, 70);
			this.grpAccounts.TabIndex = 1;
			this.grpAccounts.TabStop = false;
			this.grpAccounts.Text = "Account ";
			this.grpAccounts.Visible = false;
			// 
			// cmbAccount
			// 
			this.cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbAccount.FormattingEnabled = true;
			this.cmbAccount.Location = new System.Drawing.Point(105, 26);
			this.cmbAccount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cmbAccount.Name = "cmbAccount";
			this.cmbAccount.Size = new System.Drawing.Size(385, 28);
			this.cmbAccount.TabIndex = 198;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(15, 30);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(80, 20);
			this.label10.TabIndex = 199;
			this.label10.Text = "Account :";
			// 
			// lblReport
			// 
			this.lblReport.BackColor = System.Drawing.Color.Gainsboro;
			this.lblReport.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblReport.Location = new System.Drawing.Point(0, 0);
			this.lblReport.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblReport.Name = "lblReport";
			this.lblReport.Size = new System.Drawing.Size(841, 57);
			this.lblReport.TabIndex = 196;
			this.lblReport.Text = "Report";
			this.lblReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpItem
			// 
			this.grpItem.Controls.Add(this.cmbItem);
			this.grpItem.Controls.Add(this.label4);
			this.grpItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpItem.Location = new System.Drawing.Point(16, 306);
			this.grpItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpItem.Name = "grpItem";
			this.grpItem.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpItem.Size = new System.Drawing.Size(401, 68);
			this.grpItem.TabIndex = 200;
			this.grpItem.TabStop = false;
			this.grpItem.Text = "Item ";
			this.grpItem.Visible = false;
			// 
			// cmbItem
			// 
			this.cmbItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cmbItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbItem.FormattingEnabled = true;
			this.cmbItem.Location = new System.Drawing.Point(105, 26);
			this.cmbItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cmbItem.Name = "cmbItem";
			this.cmbItem.Size = new System.Drawing.Size(280, 28);
			this.cmbItem.TabIndex = 200;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(15, 30);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 20);
			this.label4.TabIndex = 201;
			this.label4.Text = "Item :";
			// 
			// grpFilter
			// 
			this.grpFilter.Controls.Add(this.cmbFilter);
			this.grpFilter.Controls.Add(this.txtQty);
			this.grpFilter.Controls.Add(this.label3);
			this.grpFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpFilter.Location = new System.Drawing.Point(16, 378);
			this.grpFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpFilter.Name = "grpFilter";
			this.grpFilter.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpFilter.Size = new System.Drawing.Size(351, 54);
			this.grpFilter.TabIndex = 201;
			this.grpFilter.TabStop = false;
			this.grpFilter.Text = "Filter";
			this.grpFilter.Visible = false;
			// 
			// cmbFilter
			// 
			this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFilter.FormattingEnabled = true;
			this.cmbFilter.Items.AddRange(new object[] {
            "All",
            "Equal",
            "Greater",
            "Less"});
			this.cmbFilter.Location = new System.Drawing.Point(72, 16);
			this.cmbFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cmbFilter.Name = "cmbFilter";
			this.cmbFilter.Size = new System.Drawing.Size(120, 28);
			this.cmbFilter.TabIndex = 203;
			this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(16, 22);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(50, 20);
			this.label3.TabIndex = 201;
			this.label3.Text = "Qty : ";
			// 
			// pnlControl
			// 
			this.pnlControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlControl.Controls.Add(this.btnClose);
			this.pnlControl.Controls.Add(this.btnPrint);
			this.pnlControl.Location = new System.Drawing.Point(545, 78);
			this.pnlControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pnlControl.Name = "pnlControl";
			this.pnlControl.Size = new System.Drawing.Size(219, 51);
			this.pnlControl.TabIndex = 202;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Location = new System.Drawing.Point(113, 7);
			this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(97, 36);
			this.btnClose.TabIndex = 206;
			this.btnClose.Tag = "";
			this.btnClose.Text = "&Close";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnPrint.Location = new System.Drawing.Point(8, 7);
			this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(97, 36);
			this.btnPrint.TabIndex = 0;
			this.btnPrint.Tag = "";
			this.btnPrint.Text = "&Print";
			this.btnPrint.UseVisualStyleBackColor = false;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// grpItemCatagory
			// 
			this.grpItemCatagory.Controls.Add(this.cmbItemCatagory);
			this.grpItemCatagory.Controls.Add(this.label5);
			this.grpItemCatagory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpItemCatagory.Location = new System.Drawing.Point(319, 154);
			this.grpItemCatagory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpItemCatagory.Name = "grpItemCatagory";
			this.grpItemCatagory.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpItemCatagory.Size = new System.Drawing.Size(401, 68);
			this.grpItemCatagory.TabIndex = 202;
			this.grpItemCatagory.TabStop = false;
			this.grpItemCatagory.Text = "Catagory";
			this.grpItemCatagory.Visible = false;
			// 
			// cmbItemCatagory
			// 
			this.cmbItemCatagory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cmbItemCatagory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbItemCatagory.FormattingEnabled = true;
			this.cmbItemCatagory.Location = new System.Drawing.Point(116, 26);
			this.cmbItemCatagory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cmbItemCatagory.Name = "cmbItemCatagory";
			this.cmbItemCatagory.Size = new System.Drawing.Size(276, 28);
			this.cmbItemCatagory.TabIndex = 200;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(15, 30);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 20);
			this.label5.TabIndex = 201;
			this.label5.Text = "Catagory :";
			// 
			// grpType
			// 
			this.grpType.Controls.Add(this.cmbType);
			this.grpType.Controls.Add(this.label6);
			this.grpType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpType.Location = new System.Drawing.Point(435, 306);
			this.grpType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpType.Name = "grpType";
			this.grpType.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpType.Size = new System.Drawing.Size(401, 68);
			this.grpType.TabIndex = 203;
			this.grpType.TabStop = false;
			this.grpType.Text = "Type";
			this.grpType.Visible = false;
			// 
			// cmbType
			// 
			this.cmbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cmbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cmbType.FormattingEnabled = true;
			this.cmbType.Location = new System.Drawing.Point(116, 26);
			this.cmbType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cmbType.Name = "cmbType";
			this.cmbType.Size = new System.Drawing.Size(276, 28);
			this.cmbType.TabIndex = 200;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(15, 30);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(55, 20);
			this.label6.TabIndex = 201;
			this.label6.Text = "Type :";
			// 
			// grpSelectedAccounts
			// 
			this.grpSelectedAccounts.BackColor = System.Drawing.SystemColors.Control;
			this.grpSelectedAccounts.Controls.Add(this.chkSelectAll);
			this.grpSelectedAccounts.Controls.Add(this.chklstAccounts);
			this.grpSelectedAccounts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpSelectedAccounts.Location = new System.Drawing.Point(16, 439);
			this.grpSelectedAccounts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpSelectedAccounts.Name = "grpSelectedAccounts";
			this.grpSelectedAccounts.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpSelectedAccounts.Size = new System.Drawing.Size(344, 161);
			this.grpSelectedAccounts.TabIndex = 204;
			this.grpSelectedAccounts.TabStop = false;
			this.grpSelectedAccounts.Text = "Select Accounts ";
			this.grpSelectedAccounts.Visible = false;
			// 
			// chkSelectAll
			// 
			this.chkSelectAll.AutoSize = true;
			this.chkSelectAll.Location = new System.Drawing.Point(7, 21);
			this.chkSelectAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chkSelectAll.Name = "chkSelectAll";
			this.chkSelectAll.Size = new System.Drawing.Size(102, 24);
			this.chkSelectAll.TabIndex = 204;
			this.chkSelectAll.Text = "Select All";
			this.chkSelectAll.UseVisualStyleBackColor = true;
			this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
			// 
			// chklstAccounts
			// 
			this.chklstAccounts.BackColor = System.Drawing.SystemColors.Control;
			this.chklstAccounts.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.chklstAccounts.FormattingEnabled = true;
			this.chklstAccounts.Location = new System.Drawing.Point(4, 48);
			this.chklstAccounts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chklstAccounts.Name = "chklstAccounts";
			this.chklstAccounts.Size = new System.Drawing.Size(336, 109);
			this.chklstAccounts.TabIndex = 203;
			// 
			// grpPrintOption
			// 
			this.grpPrintOption.Controls.Add(this.rdoDirectPrint);
			this.grpPrintOption.Controls.Add(this.rdoViewReport);
			this.grpPrintOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.grpPrintOption.Location = new System.Drawing.Point(376, 439);
			this.grpPrintOption.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpPrintOption.Name = "grpPrintOption";
			this.grpPrintOption.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.grpPrintOption.Size = new System.Drawing.Size(344, 68);
			this.grpPrintOption.TabIndex = 205;
			this.grpPrintOption.TabStop = false;
			this.grpPrintOption.Text = "Print Option";
			this.grpPrintOption.Visible = false;
			// 
			// rdoDirectPrint
			// 
			this.rdoDirectPrint.AutoSize = true;
			this.rdoDirectPrint.Location = new System.Drawing.Point(189, 27);
			this.rdoDirectPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoDirectPrint.Name = "rdoDirectPrint";
			this.rdoDirectPrint.Size = new System.Drawing.Size(116, 24);
			this.rdoDirectPrint.TabIndex = 1;
			this.rdoDirectPrint.Text = "Direct Print";
			this.rdoDirectPrint.UseVisualStyleBackColor = true;
			// 
			// rdoViewReport
			// 
			this.rdoViewReport.AutoSize = true;
			this.rdoViewReport.Checked = true;
			this.rdoViewReport.Location = new System.Drawing.Point(27, 27);
			this.rdoViewReport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rdoViewReport.Name = "rdoViewReport";
			this.rdoViewReport.Size = new System.Drawing.Size(121, 24);
			this.rdoViewReport.TabIndex = 0;
			this.rdoViewReport.TabStop = true;
			this.rdoViewReport.Text = "View Report";
			this.rdoViewReport.UseVisualStyleBackColor = true;
			this.rdoViewReport.CheckedChanged += new System.EventHandler(this.rdoViewReport_CheckedChanged);
			// 
			// txtQty
			// 
			this.txtQty.Location = new System.Drawing.Point(201, 17);
			this.txtQty.Margin = new System.Windows.Forms.Padding(4);
			this.txtQty.Name = "txtQty";
			this.txtQty.Size = new System.Drawing.Size(132, 26);
			this.txtQty.TabIndex = 202;
			this.txtQty.Text = "0";
			this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// frmReportParameters
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(841, 597);
			this.Controls.Add(this.grpPrintOption);
			this.Controls.Add(this.grpSelectedAccounts);
			this.Controls.Add(this.grpType);
			this.Controls.Add(this.grpItemCatagory);
			this.Controls.Add(this.pnlControl);
			this.Controls.Add(this.grpFilter);
			this.Controls.Add(this.grpItem);
			this.Controls.Add(this.lblReport);
			this.Controls.Add(this.grpAsOn);
			this.Controls.Add(this.grpAccounts);
			this.Controls.Add(this.grpDateRange);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "frmReportParameters";
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.Text = "Report Parameters";
			this.Load += new System.EventHandler(this.frmReportParameters_Load);
			this.grpDateRange.ResumeLayout(false);
			this.grpDateRange.PerformLayout();
			this.grpAsOn.ResumeLayout(false);
			this.grpAsOn.PerformLayout();
			this.grpAccounts.ResumeLayout(false);
			this.grpAccounts.PerformLayout();
			this.grpItem.ResumeLayout(false);
			this.grpItem.PerformLayout();
			this.grpFilter.ResumeLayout(false);
			this.grpFilter.PerformLayout();
			this.pnlControl.ResumeLayout(false);
			this.grpItemCatagory.ResumeLayout(false);
			this.grpItemCatagory.PerformLayout();
			this.grpType.ResumeLayout(false);
			this.grpType.PerformLayout();
			this.grpSelectedAccounts.ResumeLayout(false);
			this.grpSelectedAccounts.PerformLayout();
			this.grpPrintOption.ResumeLayout(false);
			this.grpPrintOption.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDateRange;
        private System.Windows.Forms.GroupBox grpAsOn;
        private System.Windows.Forms.GroupBox grpAccounts;
        private System.Windows.Forms.DateTimePicker dtpTDate;
        private System.Windows.Forms.DateTimePicker dtpFDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpAsOn;
        private System.Windows.Forms.Label lblReport;
        private System.Windows.Forms.ComboBox cmbAccount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grpItem;
        private System.Windows.Forms.ComboBox cmbItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpFilter;
        private DecimalTextbox txtQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.GroupBox grpItemCatagory;
        private System.Windows.Forms.ComboBox cmbItemCatagory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpSelectedAccounts;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.CheckedListBox chklstAccounts;
        private System.Windows.Forms.GroupBox grpPrintOption;
        private System.Windows.Forms.RadioButton rdoDirectPrint;
        private System.Windows.Forms.RadioButton rdoViewReport;
    }
}