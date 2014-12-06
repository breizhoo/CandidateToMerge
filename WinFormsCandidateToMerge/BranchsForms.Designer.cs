namespace WinFormsCandidateToMerge
{
    partial class BranchsForms
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
            this.BranchsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCandidateToMerge2 = new System.Windows.Forms.BindingSource(this.components);
            this.dgvBranches = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchSourceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchDestinationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BranchsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).BeginInit();
            this.SuspendLayout();
            // 
            // BranchsBindingSource
            // 
            this.BranchsBindingSource.DataMember = "Branchs";
            this.BranchsBindingSource.DataSource = this.dsCandidateToMerge2;
            // 
            // dsCandidateToMerge2
            // 
            this.dsCandidateToMerge2.DataSource = typeof(WinFormsCandidateToMerge.DsCandidateToMerge);
            this.dsCandidateToMerge2.Position = 0;
            // 
            // dgvBranches
            // 
            this.dgvBranches.AllowUserToOrderColumns = true;
            this.dgvBranches.AutoGenerateColumns = false;
            this.dgvBranches.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvBranches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBranches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isToScanDataGridViewCheckBoxColumn2,
            this.nameDataGridViewTextBoxColumn1,
            this.branchSourceDataGridViewTextBoxColumn,
            this.branchDestinationDataGridViewTextBoxColumn});
            this.dgvBranches.DataSource = this.BranchsBindingSource;
            this.dgvBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBranches.Location = new System.Drawing.Point(0, 0);
            this.dgvBranches.Name = "dgvBranches";
            this.dgvBranches.Size = new System.Drawing.Size(284, 261);
            this.dgvBranches.TabIndex = 2;
            // 
            // isToScanDataGridViewCheckBoxColumn2
            // 
            this.isToScanDataGridViewCheckBoxColumn2.DataPropertyName = "IsToScan";
            this.isToScanDataGridViewCheckBoxColumn2.HeaderText = "A";
            this.isToScanDataGridViewCheckBoxColumn2.Name = "isToScanDataGridViewCheckBoxColumn2";
            this.isToScanDataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isToScanDataGridViewCheckBoxColumn2.Width = 25;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            // 
            // branchSourceDataGridViewTextBoxColumn
            // 
            this.branchSourceDataGridViewTextBoxColumn.DataPropertyName = "BranchSource";
            this.branchSourceDataGridViewTextBoxColumn.HeaderText = "BranchSource";
            this.branchSourceDataGridViewTextBoxColumn.Name = "branchSourceDataGridViewTextBoxColumn";
            // 
            // branchDestinationDataGridViewTextBoxColumn
            // 
            this.branchDestinationDataGridViewTextBoxColumn.DataPropertyName = "BranchDestination";
            this.branchDestinationDataGridViewTextBoxColumn.HeaderText = "BranchDestination";
            this.branchDestinationDataGridViewTextBoxColumn.Name = "branchDestinationDataGridViewTextBoxColumn";
            // 
            // BranchsForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dgvBranches);
            this.Name = "BranchsForms";
            this.Text = "Branchs";
            ((System.ComponentModel.ISupportInitialize)(this.BranchsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.BindingSource BranchsBindingSource;
        private System.Windows.Forms.BindingSource dsCandidateToMerge2;
        internal System.Windows.Forms.DataGridView dgvBranches;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn isToScanDataGridViewCheckBoxColumn2;
        internal System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn branchSourceDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn branchDestinationDataGridViewTextBoxColumn;
    }
}