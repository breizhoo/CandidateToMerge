namespace WinFormsCandidateToMerge
{
    partial class UsersForms
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
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isFavoriteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsCandidateToMerge2 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).BeginInit();
            this.SuspendLayout();
            // 
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.dsCandidateToMerge2;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToOrderColumns = true;
            this.dgvUsers.AutoGenerateColumns = false;
            this.dgvUsers.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isToScanDataGridViewCheckBoxColumn,
            this.isFavoriteDataGridViewCheckBoxColumn,
            this.colName});
            this.dgvUsers.DataSource = this.usersBindingSource;
            this.dgvUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsers.Location = new System.Drawing.Point(0, 0);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(284, 261);
            this.dgvUsers.TabIndex = 1;
            // 
            // isToScanDataGridViewCheckBoxColumn
            // 
            this.isToScanDataGridViewCheckBoxColumn.DataPropertyName = "IsToScan";
            this.isToScanDataGridViewCheckBoxColumn.HeaderText = "V";
            this.isToScanDataGridViewCheckBoxColumn.Name = "isToScanDataGridViewCheckBoxColumn";
            this.isToScanDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isToScanDataGridViewCheckBoxColumn.Width = 25;
            // 
            // isFavoriteDataGridViewCheckBoxColumn
            // 
            this.isFavoriteDataGridViewCheckBoxColumn.DataPropertyName = "IsFavorite";
            this.isFavoriteDataGridViewCheckBoxColumn.HeaderText = "F";
            this.isFavoriteDataGridViewCheckBoxColumn.Name = "isFavoriteDataGridViewCheckBoxColumn";
            this.isFavoriteDataGridViewCheckBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.isFavoriteDataGridViewCheckBoxColumn.Width = 25;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            // 
            // dsCandidateToMerge2
            // 
            this.dsCandidateToMerge2.DataSource = typeof(WinFormsCandidateToMerge.DsCandidateToMerge);
            this.dsCandidateToMerge2.Position = 0;
            this.dsCandidateToMerge2.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            // 
            // UsersForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dgvUsers);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UsersForms";
            this.Text = "Users";
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.BindingSource usersBindingSource;
        internal System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isToScanDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isFavoriteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.BindingSource dsCandidateToMerge2;

    }
}