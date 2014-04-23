namespace WinFormsCandidateToMerge
{
    partial class CandidateToMerge
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CandidateToMerge));
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isFavoriteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCandidateToMerge1 = new WinFormsCandidateToMerge.DsCandidateToMerge();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.projectDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchSourceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchDestinationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtTfsUrl = new System.Windows.Forms.TextBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.changesetIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creationDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.projectDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isToDisplayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mergeResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnLaunch = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BranchsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mergeResultBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.splitContainer3);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(799, 388);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            this.splitContainer3.Panel1.Controls.Add(this.txtTfsUrl);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.Panel2.Controls.Add(this.dgvResult);
            this.splitContainer3.Size = new System.Drawing.Size(793, 385);
            this.splitContainer3.SplitterDistance = 359;
            this.splitContainer3.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 33);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(357, 354);
            this.splitContainer1.SplitterDistance = 111;
            this.splitContainer1.TabIndex = 4;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isToScanDataGridViewCheckBoxColumn,
            this.isFavoriteDataGridViewCheckBoxColumn,
            this.colName});
            this.dataGridView2.DataSource = this.usersBindingSource;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(357, 111);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.CurrentCellChanged += new System.EventHandler(this.dataGridView2_CurrentCellChanged);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
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
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.dsCandidateToMerge1;
            // 
            // dsCandidateToMerge1
            // 
            this.dsCandidateToMerge1.DataSetName = "DsCandidateToMerge";
            this.dsCandidateToMerge1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(0, 1);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView4);
            this.splitContainer2.Size = new System.Drawing.Size(357, 236);
            this.splitContainer2.SplitterDistance = 92;
            this.splitContainer2.TabIndex = 4;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToOrderColumns = true;
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isToScanDataGridViewCheckBoxColumn1,
            this.projectDataGridViewTextBoxColumn});
            this.dataGridView3.DataSource = this.ProjectsBindingSource;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(357, 92);
            this.dataGridView3.TabIndex = 0;
            // 
            // isToScanDataGridViewCheckBoxColumn1
            // 
            this.isToScanDataGridViewCheckBoxColumn1.DataPropertyName = "IsToScan";
            this.isToScanDataGridViewCheckBoxColumn1.HeaderText = "A";
            this.isToScanDataGridViewCheckBoxColumn1.Name = "isToScanDataGridViewCheckBoxColumn1";
            this.isToScanDataGridViewCheckBoxColumn1.Width = 25;
            // 
            // projectDataGridViewTextBoxColumn
            // 
            this.projectDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.projectDataGridViewTextBoxColumn.DataPropertyName = "Project";
            this.projectDataGridViewTextBoxColumn.HeaderText = "Project";
            this.projectDataGridViewTextBoxColumn.Name = "projectDataGridViewTextBoxColumn";
            // 
            // ProjectsBindingSource
            // 
            this.ProjectsBindingSource.DataMember = "Projects";
            this.ProjectsBindingSource.DataSource = this.dsCandidateToMerge1;
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToOrderColumns = true;
            this.dataGridView4.AutoGenerateColumns = false;
            this.dataGridView4.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isToScanDataGridViewCheckBoxColumn2,
            this.nameDataGridViewTextBoxColumn1,
            this.branchSourceDataGridViewTextBoxColumn,
            this.branchDestinationDataGridViewTextBoxColumn});
            this.dataGridView4.DataSource = this.BranchsBindingSource;
            this.dataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView4.Location = new System.Drawing.Point(0, 0);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(357, 140);
            this.dataGridView4.TabIndex = 1;
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
            // BranchsBindingSource
            // 
            this.BranchsBindingSource.DataMember = "Branchs";
            this.BranchsBindingSource.DataSource = this.dsCandidateToMerge1;
            // 
            // txtTfsUrl
            // 
            this.txtTfsUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTfsUrl.Location = new System.Drawing.Point(3, 3);
            this.txtTfsUrl.Name = "txtTfsUrl";
            this.txtTfsUrl.Size = new System.Drawing.Size(354, 20);
            this.txtTfsUrl.TabIndex = 0;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AutoGenerateColumns = false;
            this.dgvResult.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.changesetIdDataGridViewTextBoxColumn,
            this.colOwner,
            this.creationDateDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn,
            this.projectDataGridViewTextBoxColumn1,
            this.branchNameDataGridViewTextBoxColumn,
            this.isToDisplayDataGridViewTextBoxColumn});
            this.dgvResult.DataSource = this.mergeResultBindingSource;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.Size = new System.Drawing.Size(430, 385);
            this.dgvResult.TabIndex = 1;
            this.dgvResult.DataSourceChanged += new System.EventHandler(this.dgvResult_DataSourceChanged);
            // 
            // changesetIdDataGridViewTextBoxColumn
            // 
            this.changesetIdDataGridViewTextBoxColumn.DataPropertyName = "ChangesetId";
            this.changesetIdDataGridViewTextBoxColumn.HeaderText = "ChangesetId";
            this.changesetIdDataGridViewTextBoxColumn.Name = "changesetIdDataGridViewTextBoxColumn";
            this.changesetIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // colOwner
            // 
            this.colOwner.DataPropertyName = "Owner";
            this.colOwner.HeaderText = "Owner";
            this.colOwner.Name = "colOwner";
            this.colOwner.ReadOnly = true;
            // 
            // creationDateDataGridViewTextBoxColumn
            // 
            this.creationDateDataGridViewTextBoxColumn.DataPropertyName = "CreationDate";
            this.creationDateDataGridViewTextBoxColumn.HeaderText = "CreationDate";
            this.creationDateDataGridViewTextBoxColumn.Name = "creationDateDataGridViewTextBoxColumn";
            this.creationDateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Comment";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // projectDataGridViewTextBoxColumn1
            // 
            this.projectDataGridViewTextBoxColumn1.DataPropertyName = "Project";
            this.projectDataGridViewTextBoxColumn1.HeaderText = "Project";
            this.projectDataGridViewTextBoxColumn1.Name = "projectDataGridViewTextBoxColumn1";
            this.projectDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // branchNameDataGridViewTextBoxColumn
            // 
            this.branchNameDataGridViewTextBoxColumn.DataPropertyName = "BranchName";
            this.branchNameDataGridViewTextBoxColumn.HeaderText = "BranchName";
            this.branchNameDataGridViewTextBoxColumn.Name = "branchNameDataGridViewTextBoxColumn";
            this.branchNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isToDisplayDataGridViewTextBoxColumn
            // 
            this.isToDisplayDataGridViewTextBoxColumn.DataPropertyName = "IsToDisplay";
            this.isToDisplayDataGridViewTextBoxColumn.HeaderText = "IsToDisplay";
            this.isToDisplayDataGridViewTextBoxColumn.Name = "isToDisplayDataGridViewTextBoxColumn";
            this.isToDisplayDataGridViewTextBoxColumn.ReadOnly = true;
            this.isToDisplayDataGridViewTextBoxColumn.Visible = false;
            // 
            // mergeResultBindingSource
            // 
            this.mergeResultBindingSource.DataMember = "MergeResult";
            this.mergeResultBindingSource.DataSource = this.dsCandidateToMerge1;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunch.Location = new System.Drawing.Point(688, 406);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(123, 23);
            this.btnLaunch.TabIndex = 0;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // CandidateToMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 441);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnLaunch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CandidateToMerge";
            this.Text = "Candidate to merge";
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BranchsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mergeResultBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.SplitContainer splitContainer3;
        internal System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.SplitContainer splitContainer2;
        internal System.Windows.Forms.Button btnLaunch;
        internal System.Windows.Forms.DataGridView dgvResult;
        internal System.Windows.Forms.DataGridView dataGridView2;
        internal System.Windows.Forms.DataGridView dataGridView3;
        internal System.Windows.Forms.DataGridView dataGridView4;
        internal System.Windows.Forms.BindingSource usersBindingSource;
        internal DsCandidateToMerge dsCandidateToMerge1;
        internal System.Windows.Forms.BindingSource ProjectsBindingSource;
        internal System.Windows.Forms.BindingSource BranchsBindingSource;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ProjectsDataGridViewTextBoxColumn1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn idBranchDataGridViewTextBoxColumn;
        internal System.Windows.Forms.BindingSource mergeResultBindingSource;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ProjectsDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn isToScanDataGridViewCheckBoxColumn1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn projectDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn isToScanDataGridViewCheckBoxColumn2;
        internal System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn branchSourceDataGridViewTextBoxColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn branchDestinationDataGridViewTextBoxColumn;
        internal System.ComponentModel.BackgroundWorker backgroundWorker1;
        internal System.Windows.Forms.TextBox txtTfsUrl;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isToScanDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isFavoriteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn changesetIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn creationDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isToDisplayDataGridViewTextBoxColumn;
    }
}

