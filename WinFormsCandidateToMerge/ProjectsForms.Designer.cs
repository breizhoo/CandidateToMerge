namespace WinFormsCandidateToMerge
{
    partial class ProjectsForms
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
            this.ProjectsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsCandidateToMerge2 = new System.Windows.Forms.BindingSource(this.components);
            this.dgvProjects = new System.Windows.Forms.DataGridView();
            this.isToScanDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.projectDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // ProjectsBindingSource
            // 
            this.ProjectsBindingSource.DataMember = "Projects";
            this.ProjectsBindingSource.DataSource = this.dsCandidateToMerge2;
            // 
            // dsCandidateToMerge2
            // 
            this.dsCandidateToMerge2.DataSource = typeof(WinFormsCandidateToMerge.DsCandidateToMerge);
            this.dsCandidateToMerge2.Position = 0;
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
            this.dgvProjects.Location = new System.Drawing.Point(0, 0);
            this.dgvProjects.Name = "dgvProjects";
            this.dgvProjects.Size = new System.Drawing.Size(284, 261);
            this.dgvProjects.TabIndex = 1;
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
            // ProjectsForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.dgvProjects);
            this.Name = "ProjectsForms";
            this.Text = "Projects";
            ((System.ComponentModel.ISupportInitialize)(this.ProjectsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsCandidateToMerge2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.BindingSource ProjectsBindingSource;
        private System.Windows.Forms.BindingSource dsCandidateToMerge2;
        internal System.Windows.Forms.DataGridView dgvProjects;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn isToScanDataGridViewCheckBoxColumn1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn projectDataGridViewTextBoxColumn;
    }
}