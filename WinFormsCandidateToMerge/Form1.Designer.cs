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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTfsUrl = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isFavoriteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCandidateToMerge1 = new WinFormsCandidateToMerge.DsCandidateToMerge();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvProjects = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.projectDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvBranches = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchSourceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchDestinationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.projectDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.changesetIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOwner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creationDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.branchNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isToDisplayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ignore = new System.Windows.Forms.DataGridViewButtonColumn();
            this.mergeResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnLaunch = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BranchsBindingSource)).BeginInit();
            this.groupBox5.SuspendLayout();
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
            this.panel2.Location = new System.Drawing.Point(0, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(823, 461);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(3, 2);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer3.Size = new System.Drawing.Size(817, 458);
            this.splitContainer3.SplitterDistance = 369;
            this.splitContainer3.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtTfsUrl);
            this.groupBox1.Location = new System.Drawing.Point(0, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 45);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Url of the TFS server :";
            // 
            // txtTfsUrl
            // 
            this.txtTfsUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTfsUrl.Location = new System.Drawing.Point(6, 19);
            this.txtTfsUrl.Name = "txtTfsUrl";
            this.txtTfsUrl.Size = new System.Drawing.Size(354, 20);
            this.txtTfsUrl.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(366, 352);
            this.splitContainer1.SplitterDistance = 110;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvUsers);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(366, 110);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Users :";
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
            this.dgvUsers.Location = new System.Drawing.Point(3, 16);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.Size = new System.Drawing.Size(360, 91);
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.CurrentCellChanged += new System.EventHandler(this.dgvUsers_CurrentCellChanged);
            this.dgvUsers.SelectionChanged += new System.EventHandler(this.dgvUsers_SelectionChanged);
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
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer2.Size = new System.Drawing.Size(366, 238);
            this.splitContainer2.SplitterDistance = 92;
            this.splitContainer2.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvProjects);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(366, 92);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Projects :";
            // 
            // dgvProjects
            // 
            this.dgvProjects.AllowUserToOrderColumns = true;
            this.dgvProjects.AutoGenerateColumns = false;
            this.dgvProjects.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isToScanDataGridViewCheckBoxColumn1,
            this.projectDataGridViewTextBoxColumn});
            this.dgvProjects.DataSource = this.ProjectsBindingSource;
            this.dgvProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProjects.Location = new System.Drawing.Point(3, 16);
            this.dgvProjects.Name = "dgvProjects";
            this.dgvProjects.Size = new System.Drawing.Size(360, 73);
            this.dgvProjects.TabIndex = 0;
            this.dgvProjects.CurrentCellChanged += new System.EventHandler(this.dgvProjects_CurrentCellChanged);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvBranches);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(366, 142);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Branchs :";
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
            this.dgvBranches.Location = new System.Drawing.Point(3, 16);
            this.dgvBranches.Name = "dgvBranches";
            this.dgvBranches.Size = new System.Drawing.Size(360, 123);
            this.dgvBranches.TabIndex = 1;
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvResult);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(444, 458);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ChangeSet cadidate to merge :";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.AutoGenerateColumns = false;
            this.dgvResult.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.projectDataGridViewTextBoxColumn1,
            this.changesetIdDataGridViewTextBoxColumn,
            this.colOwner,
            this.creationDateDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn,
            this.branchNameDataGridViewTextBoxColumn,
            this.isToDisplayDataGridViewTextBoxColumn,
            this.Ignore});
            this.dgvResult.DataSource = this.mergeResultBindingSource;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 16);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.Size = new System.Drawing.Size(438, 439);
            this.dgvResult.TabIndex = 1;
            this.dgvResult.DataSourceChanged += new System.EventHandler(this.dgvResult_DataSourceChanged);
            this.dgvResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellContentClick);
            this.dgvResult.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvResult_MouseDown);
            // 
            // projectDataGridViewTextBoxColumn1
            // 
            this.projectDataGridViewTextBoxColumn1.DataPropertyName = "Project";
            this.projectDataGridViewTextBoxColumn1.HeaderText = "Project";
            this.projectDataGridViewTextBoxColumn1.Name = "projectDataGridViewTextBoxColumn1";
            this.projectDataGridViewTextBoxColumn1.ReadOnly = true;
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
            // Ignore
            // 
            this.Ignore.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Ignore.HeaderText = "Ignore";
            this.Ignore.Name = "Ignore";
            this.Ignore.ReadOnly = true;
            // 
            // mergeResultBindingSource
            // 
            this.mergeResultBindingSource.DataMember = "MergeResult";
            this.mergeResultBindingSource.DataSource = this.dsCandidateToMerge1;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunch.Location = new System.Drawing.Point(694, 466);
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
            this.ClientSize = new System.Drawing.Size(824, 496);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnLaunch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CandidateToMerge";
            this.Text = "Candidate to merge";
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BranchsBindingSource)).EndInit();
            this.groupBox5.ResumeLayout(false);
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
        internal System.Windows.Forms.DataGridView dgvUsers;
        internal System.Windows.Forms.DataGridView dgvProjects;
        internal System.Windows.Forms.DataGridView dgvBranches;
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
        private System.Windows.Forms.DataGridViewButtonColumn Ignore;
        private System.Windows.Forms.DataGridViewTextBoxColumn projectDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn changesetIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOwner;
        private System.Windows.Forms.DataGridViewTextBoxColumn creationDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn branchNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isToDisplayDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}

