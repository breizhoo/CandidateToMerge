namespace WinFormsCandidateToMerge
{
    partial class ChangeSetFormsOld
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorDetail = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCandidateToMerge2 = new System.Windows.Forms.BindingSource(this.components);
            this.dataListView1 = new BrightIdeasSoftware.DataListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mergeResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripIgnore,
            this.toolStripSeparatorDetail,
            this.toolStripDetail});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 54);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip2_ItemClicked);
            // 
            // toolStripIgnore
            // 
            this.toolStripIgnore.Name = "toolStripIgnore";
            this.toolStripIgnore.Size = new System.Drawing.Size(167, 22);
            this.toolStripIgnore.Text = "Ignorer";
            // 
            // toolStripSeparatorDetail
            // 
            this.toolStripSeparatorDetail.Name = "toolStripSeparatorDetail";
            this.toolStripSeparatorDetail.Size = new System.Drawing.Size(164, 6);
            // 
            // toolStripDetail
            // 
            this.toolStripDetail.Name = "toolStripDetail";
            this.toolStripDetail.Size = new System.Drawing.Size(167, 22);
            this.toolStripDetail.Text = "Changeset details";
            // 
            // mergeResultBindingSource
            // 
            this.mergeResultBindingSource.DataMember = "MergeResult";
            this.mergeResultBindingSource.DataSource = this.dsCandidateToMerge2;
            // 
            // dsCandidateToMerge2
            // 
            this.dsCandidateToMerge2.DataSource = typeof(WinFormsCandidateToMerge.DsCandidateToMerge);
            this.dsCandidateToMerge2.Position = 0;
            // 
            // dataListView1
            // 
            this.dataListView1.AllColumns.Add(this.olvColumn1);
            this.dataListView1.AllColumns.Add(this.olvColumn6);
            this.dataListView1.AllColumns.Add(this.olvColumn2);
            this.dataListView1.AllColumns.Add(this.olvColumn3);
            this.dataListView1.AllColumns.Add(this.olvColumn4);
            this.dataListView1.AllColumns.Add(this.olvColumn5);
            this.dataListView1.AllColumns.Add(this.olvColumn7);
            this.dataListView1.AllColumns.Add(this.olvColumn8);
            this.dataListView1.AllowColumnReorder = true;
            this.dataListView1.AutoGenerateColumns = false;
            this.dataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn6,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn7,
            this.olvColumn8});
            this.dataListView1.DataSource = this.mergeResultBindingSource;
            this.dataListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListView1.FullRowSelect = true;
            this.dataListView1.Location = new System.Drawing.Point(0, 0);
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.ShowCommandMenuOnRightClick = true;
            this.dataListView1.ShowItemCountOnGroups = true;
            this.dataListView1.Size = new System.Drawing.Size(1276, 523);
            this.dataListView1.TabIndex = 3;
            this.dataListView1.UseCompatibleStateImageBehavior = false;
            this.dataListView1.UseFiltering = true;
            this.dataListView1.View = System.Windows.Forms.View.Details;
            this.dataListView1.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.dataListView1_CellRightClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Project";
            this.olvColumn1.Text = "Project";
            this.olvColumn1.Width = 200;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "BranchName";
            this.olvColumn6.Text = "BranchName";
            this.olvColumn6.Width = 90;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Owner";
            this.olvColumn2.Text = "Owner";
            this.olvColumn2.Width = 120;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Comment";
            this.olvColumn3.Groupable = false;
            this.olvColumn3.Text = "Comment";
            this.olvColumn3.Width = 300;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "ChangesetId";
            this.olvColumn4.Groupable = false;
            this.olvColumn4.Text = "ChangesetId";
            this.olvColumn4.Width = 75;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "CreationDate";
            this.olvColumn5.Groupable = false;
            this.olvColumn5.Text = "Creation date";
            this.olvColumn5.Width = 120;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "FeatureName";
            this.olvColumn7.Text = "Feature";
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "StoryName";
            this.olvColumn8.Text = "User Story";
            // 
            // ChangeSetFormsOld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 523);
            this.Controls.Add(this.dataListView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChangeSetFormsOld";
            this.Text = "ChangeSet Results";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mergeResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripIgnore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDetail;
        private System.Windows.Forms.ToolStripMenuItem toolStripDetail;
        internal System.Windows.Forms.BindingSource mergeResultBindingSource;
        private System.Windows.Forms.BindingSource dsCandidateToMerge2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        public BrightIdeasSoftware.DataListView dataListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
    }
}