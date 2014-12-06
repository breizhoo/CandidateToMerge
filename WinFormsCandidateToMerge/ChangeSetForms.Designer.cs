namespace WinFormsCandidateToMerge
{
    partial class ChangeSetForms
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorDetail = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCandidateToMerge2 = new System.Windows.Forms.BindingSource(this.components);
            this.dataListView1 = new BrightIdeasSoftware.DataListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mergeResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparatorDetail,
            this.toolStripDetail});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(168, 54);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.toolStripMenuItem1.Text = "Ignorer";
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
            this.dataListView1.AllColumns.Add(this.olvColumn2);
            this.dataListView1.AllColumns.Add(this.olvColumn3);
            this.dataListView1.AutoGenerateColumns = false;
            this.dataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.dataListView1.DataSource = null;
            this.dataListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListView1.FullRowSelect = true;
            this.dataListView1.Location = new System.Drawing.Point(0, 0);
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.ShowCommandMenuOnRightClick = true;
            this.dataListView1.ShowItemCountOnGroups = true;
            this.dataListView1.Size = new System.Drawing.Size(560, 523);
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
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Owner";
            this.olvColumn2.Text = "Owner";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Comment";
            this.olvColumn3.Text = "Comment";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripSeparator1,
            this.toolStripMenuItem3});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(168, 54);
            this.contextMenuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip2_ItemClicked);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(167, 22);
            this.toolStripMenuItem2.Text = "Ignorer";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(167, 22);
            this.toolStripMenuItem3.Text = "Changeset details";
            // 
            // ChangeSetForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 523);
            this.Controls.Add(this.dataListView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChangeSetForms";
            this.Text = "ChangeSet Results";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mergeResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorDetail;
        private System.Windows.Forms.ToolStripMenuItem toolStripDetail;
        internal System.Windows.Forms.BindingSource mergeResultBindingSource;
        private System.Windows.Forms.BindingSource dsCandidateToMerge2;
        private BrightIdeasSoftware.DataListView dataListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    }
}