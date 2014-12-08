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
            this.dataListView1 = new BrightIdeasSoftware.DataListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.dsCandidateToMerge2 = new System.Windows.Forms.BindingSource(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripCheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripUncheckSelected = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // usersBindingSource
            // 
            this.usersBindingSource.DataMember = "Users";
            this.usersBindingSource.DataSource = this.dsCandidateToMerge2;
            // 
            // dataListView1
            // 
            this.dataListView1.AllColumns.Add(this.olvColumn2);
            this.dataListView1.AllColumns.Add(this.olvColumn3);
            this.dataListView1.AllColumns.Add(this.olvColumn1);
            this.dataListView1.AutoGenerateColumns = false;
            this.dataListView1.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.dataListView1.CheckBoxes = true;
            this.dataListView1.CheckedAspectName = "IsToScan";
            this.dataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn1});
            this.dataListView1.ContextMenuStrip = this.contextMenuStrip2;
            this.dataListView1.DataSource = this.usersBindingSource;
            this.dataListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataListView1.Location = new System.Drawing.Point(0, 0);
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.ShowCommandMenuOnRightClick = true;
            this.dataListView1.ShowItemCountOnGroups = true;
            this.dataListView1.Size = new System.Drawing.Size(284, 261);
            this.dataListView1.TabIndex = 2;
            this.dataListView1.UseCompatibleStateImageBehavior = false;
            this.dataListView1.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Name";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Groupable = false;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Name";
            this.olvColumn2.Width = 150;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "IsToScan";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Visible";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "TeamName";
            this.olvColumn3.Text = "Team name";
            this.olvColumn3.Width = 150;
            // 
            // dsCandidateToMerge2
            // 
            this.dsCandidateToMerge2.DataSource = typeof(WinFormsCandidateToMerge.DsCandidateToMerge);
            this.dsCandidateToMerge2.Position = 0;
            this.dsCandidateToMerge2.CurrentChanged += new System.EventHandler(this.bindingSource1_CurrentChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripCheckSelected,
            this.toolStripUncheckSelected});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(167, 48);
            // 
            // toolStripCheckSelected
            // 
            this.toolStripCheckSelected.Name = "toolStripCheckSelected";
            this.toolStripCheckSelected.Size = new System.Drawing.Size(166, 22);
            this.toolStripCheckSelected.Text = "Check selected";
            this.toolStripCheckSelected.Click += new System.EventHandler(this.toolStripCheckSelected_Click);
            // 
            // toolStripUncheckSelected
            // 
            this.toolStripUncheckSelected.Name = "toolStripUncheckSelected";
            this.toolStripUncheckSelected.Size = new System.Drawing.Size(166, 22);
            this.toolStripUncheckSelected.Text = "Uncheck selected";
            this.toolStripUncheckSelected.Click += new System.EventHandler(this.toolStripUncheckSelected_Click);
            // 
            // UsersForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dataListView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UsersForms";
            this.Text = "Users";
            ((System.ComponentModel.ISupportInitialize)(this.usersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.BindingSource usersBindingSource;
        private System.Windows.Forms.BindingSource dsCandidateToMerge2;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        public BrightIdeasSoftware.DataListView dataListView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripCheckSelected;
        private System.Windows.Forms.ToolStripMenuItem toolStripUncheckSelected;

    }
}