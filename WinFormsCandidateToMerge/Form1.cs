using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace WinFormsCandidateToMerge
{
    public partial class CandidateToMerge : Form
    {
        private readonly DataSetManipulator _dataSetManipulator;
        private WaitForms _waitForms;
        private readonly UiSerializer _uiSerializer;
        private readonly DataSerializer _dataSerializer;
        private readonly ChangesetVisualizer _changesetVisualizer;

        public CandidateToMerge()
        {
            InitializeComponent();

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
            _dataSetManipulator = new DataSetManipulator(dsCandidateToMerge1);
            _uiSerializer = new UiSerializer(this);
            _dataSerializer = new DataSerializer(dsCandidateToMerge1);
            _changesetVisualizer = new ChangesetVisualizer();
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _waitForms.progressBar1.Value = e.ProgressPercentage;
            _waitForms.txtCurrent.Text = currentProject;
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            _dataSetManipulator.ClearMergeCandidate();
            backgroundWorker1.RunWorkerAsync();

            _waitForms = new WaitForms();
            _waitForms.ShowDialog(this);

        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var r = e.Result as DataExchangeBackgroundWorker;

            _dataSetManipulator.AddUserIfNotExist(r.Owners);
            _dataSetManipulator.AddMergeCandidate(r.ResponseProjects);

            _waitForms.Close();
        }


        private string currentProject;
        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var response = new DataExchangeBackgroundWorker();
            var list = _dataSetManipulator.GetMergeCandidateRequest().ToList();
            if (list.Count > 0)
            {
                currentProject = list.ElementAt(0).Project;
                backgroundWorker1.ReportProgress(0);
            }
            var reponseOfMerge = new GetMergeCandidateProcess(txtTfsUrl.Text,
                list)
                .GetMergeResult(x =>
                {
                    var index = list.IndexOf(x);
                    var percent = (int)((decimal)100 / list.Count * index);
                    currentProject = list.ElementAt(index).Project;
                    if (percent > 0)
                        backgroundWorker1.ReportProgress(percent);

                });

            response.Owners = (from items in reponseOfMerge
                               from mergeItem in items.MergeCandidates
                               select mergeItem.Changeset.Owner)
                .Distinct()
                .ToList();

            response.ResponseProjects = reponseOfMerge;

            e.Result = response;
        }



        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            _dataSetManipulator.SetTfsUrl(txtTfsUrl.Text);
            _uiSerializer.Save();
            _dataSerializer.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _dataSerializer.Restore();
            _uiSerializer.Restore();
            txtTfsUrl.Text = _dataSetManipulator.GetTfsUrl();

            dgvResult.DataSource = new DataView(dsCandidateToMerge1.MergeResult, "IsToDisplay = true", "", DataViewRowState.CurrentRows);
            //objectListView1.SetObjects(new DataView(dsCandidateToMerge1.MergeResult, "IsToDisplay = true", "", DataViewRowState.CurrentRows));
            dataListView1.DataSource = new DataView(dsCandidateToMerge1.MergeResult, "IsToDisplay = true", "", DataViewRowState.CurrentRows);
        }

        private void dgvResult_DataSourceChanged(object sender, EventArgs e)
        {
            ChangeColorRowByOwner(userName);
        }

        private string userName;
        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            //userName = string.Empty;
            //if (dgvUsers.SelectedRows.Count > 0)
            //    userName = dgvUsers.SelectedRows[0]
            //        .Cells[colName.Name].Value.ToString();

            //ChangeColorRow(userName);
        }

        private void ChangeColorRowByProject(string projectName)
        {
            foreach (var row in dgvResult.Rows.Cast<DataGridViewRow>())
            {
                row.DefaultCellStyle.BackColor = row.Cells[projectDataGridViewTextBoxColumn1.Name].Value.ToString() == projectName
                    ? Color.Khaki
                    : Color.White;
            }
        }

        private void ChangeColorRowByOwner(string name)
        {
            foreach (var row in dgvResult.Rows.Cast<DataGridViewRow>())
            {
                row.DefaultCellStyle.BackColor = row.Cells[colOwner.Name].Value.ToString() == name
                    ? Color.Khaki
                    : Color.White;
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            userName = dgvUsers.Rows[e.RowIndex]
                .Cells[colName.Name].Value.ToString();

            ChangeColorRowByOwner(userName);
        }

        private void dgvUsers_CurrentCellChanged(object sender, EventArgs e)
        {
            userName = string.Empty;
            if (dgvUsers.CurrentCell != null &&
                dgvUsers.CurrentCell.RowIndex >= 0)
                userName = dgvUsers.Rows[dgvUsers.CurrentCell.RowIndex]
                    .Cells[colName.Name].Value.ToString();

            ChangeColorRowByOwner(userName);
        }

        private string project;
        private void dgvProjects_CurrentCellChanged(object sender, EventArgs e)
        {
            project = string.Empty;
            if (dgvProjects.CurrentCell != null &&
                dgvProjects.CurrentCell.RowIndex >= 0)
                project = dgvProjects.Rows[dgvProjects.CurrentCell.RowIndex]
                    .Cells[projectDataGridViewTextBoxColumn.Name].Value.ToString();

            ChangeColorRowByProject(project);
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Ignore.DisplayIndex == e.ColumnIndex)
                _dataSetManipulator.Ignore((DsCandidateToMerge.MergeResultRow)
                    ((DataRowView)(dgvResult.CurrentRow.DataBoundItem)).Row);
        }

        private bool CanShowChangesetDetails()
        {
            var isOnlyOneRowSelected = dgvResult.SelectedRows.Count == 1;
            var allCellsAreFromTheSameRow = dgvResult.SelectedCells.Cast<DataGridViewCell>().GroupBy(c => c.RowIndex).Count() == 1;

            return isOnlyOneRowSelected || allCellsAreFromTheSameRow;
        }


        private bool TryGetSelectedChangetSetId(out int outChangesetId)
        {
            if (!CanShowChangesetDetails())
            {
                outChangesetId = 0;
                return false;
            }

            int csId;
            var tryGetSelectedChangetSetId = int.TryParse(dgvResult.CurrentRow.Cells[changesetIdDataGridViewTextBoxColumn.Name].Value.ToString(), out csId);
            outChangesetId = csId;

            return tryGetSelectedChangetSetId;
        }

        private bool IsHoveringSelectedRow(MouseEventArgs e)
        {
            return dgvResult.HitTest(e.X, e.Y).RowIndex == dgvResult.CurrentRow.Index;
        }

        private void dgvResult_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right &&
                CanShowChangesetDetails() &&
                IsHoveringSelectedRow(e) &&
                _changesetVisualizer.IsVisualizerAvailable)
            {
                var menuItemCSDetails = new MenuItem("Changeset details");
                menuItemCSDetails.Click += dgvResult_MenuItemCsDetailsOnClick;
                var menu = new ContextMenu();
                menu.MenuItems.Add(menuItemCSDetails);

                menu.Show(dgvResult, new Point(e.X, e.Y));
            }
        }

        private void dgvResult_MenuItemCsDetailsOnClick(object sender, EventArgs eventArgs)
        {
            int csId;
            if (TryGetSelectedChangetSetId(out csId))
            {
                try
                {
                    _changesetVisualizer.Execute(csId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while starting tf.exe\r\n\r\n" + ex.ToString(), "Arf !");
                }
            }
        }

        private void dataListView1_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            e.MenuStrip = this.contextMenuStrip1;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripMenuItem1)
            {

                foreach (var obj in dataListView1.SelectedObjects)
                {
                    if (!(obj is DataRowView))
                        continue;
                    
                    _dataSetManipulator.Ignore((DsCandidateToMerge.MergeResultRow)
                         ((DataRowView)(obj)).Row);
                }
            }
        }
    }
}
