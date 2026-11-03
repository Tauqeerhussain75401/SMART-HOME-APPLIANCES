namespace NuReady.Forms
{
    partial class frmCustomerDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.clnAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnSMSNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clnCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnEdit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clnIsEdit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAccount
            // 
            this.dgvAccount.AllowUserToAddRows = false;
            this.dgvAccount.AllowUserToDeleteRows = false;
            this.dgvAccount.AllowUserToOrderColumns = true;
            this.dgvAccount.AllowUserToResizeColumns = false;
            this.dgvAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAccount.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clnAccount,
            this.clnTitle,
            this.clnSMSNumber,
            this.clnActive,
            this.clnCreated,
            this.clnEdit,
            this.clnIsEdit});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccount.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAccount.Location = new System.Drawing.Point(12, 64);
            this.dgvAccount.MultiSelect = false;
            this.dgvAccount.Name = "dgvAccount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccount.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAccount.RowHeadersWidth = 30;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAccount.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAccount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvAccount.Size = new System.Drawing.Size(900, 320);
            this.dgvAccount.TabIndex = 165;
            // 
            // clnAccount
            // 
            this.clnAccount.HeaderText = "Account";
            this.clnAccount.MinimumWidth = 120;
            this.clnAccount.Name = "clnAccount";
            this.clnAccount.ReadOnly = true;
            this.clnAccount.Width = 120;
            // 
            // clnTitle
            // 
            this.clnTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clnTitle.HeaderText = "Title";
            this.clnTitle.MinimumWidth = 120;
            this.clnTitle.Name = "clnTitle";
            // 
            // clnSMSNumber
            // 
            this.clnSMSNumber.HeaderText = "SMS Number";
            this.clnSMSNumber.Name = "clnSMSNumber";
            // 
            // clnActive
            // 
            this.clnActive.HeaderText = "Active";
            this.clnActive.Name = "clnActive";
            // 
            // clnCreated
            // 
            this.clnCreated.HeaderText = "Created";
            this.clnCreated.Name = "clnCreated";
            this.clnCreated.ReadOnly = true;
            this.clnCreated.Visible = false;
            // 
            // clnEdit
            // 
            this.clnEdit.HeaderText = "Edit";
            this.clnEdit.Name = "clnEdit";
            this.clnEdit.ReadOnly = true;
            this.clnEdit.Visible = false;
            // 
            // clnIsEdit
            // 
            this.clnIsEdit.HeaderText = "IsEdit";
            this.clnIsEdit.Name = "clnIsEdit";
            this.clnIsEdit.ReadOnly = true;
            this.clnIsEdit.Visible = false;
            // 
            // frmCustomerDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 426);
            this.Controls.Add(this.dgvAccount);
            this.Name = "frmCustomerDetail";
            this.Text = "frmCustomerDetail";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnSMSNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clnActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnCreated;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn clnIsEdit;
    }
}