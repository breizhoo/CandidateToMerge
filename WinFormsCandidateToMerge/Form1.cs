using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsCandidateToMerge
{
    public partial class CandidateToMerge : Form
    {
        private readonly DataSetManipulator _dataSetManipulator;
        private WaitForms _waitForms;
        private readonly UiSerializer _uiSerializer;
        private readonly DataSerializer _dataSerializer;

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
        }

        private void dgvResult_DataSourceChanged(object sender, EventArgs e)
        {
            ChangeColorRow(userName);
        }

        private string userName;
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            //userName = string.Empty;
            //if (dataGridView2.SelectedRows.Count > 0)
            //    userName = dataGridView2.SelectedRows[0]
            //        .Cells[colName.Name].Value.ToString();

            //ChangeColorRow(userName);
        }

        private void ChangeColorRow(string name)
        {
            foreach (var row in dgvResult.Rows.Cast<DataGridViewRow>())
            {
                row.DefaultCellStyle.BackColor = row.Cells[colOwner.Name].Value.ToString() == name
                    ? Color.Khaki
                    : Color.White;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            userName = dataGridView2.Rows[e.RowIndex]
                .Cells[colName.Name].Value.ToString();

            ChangeColorRow(userName);
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            userName = string.Empty;
            if (dataGridView2.CurrentCell != null &&
                dataGridView2.CurrentCell.RowIndex >= 0)
                userName = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex]
                    .Cells[colName.Name].Value.ToString();

            ChangeColorRow(userName);
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Ignore.DisplayIndex == e.ColumnIndex)
                _dataSetManipulator.Ignore((DsCandidateToMerge.MergeResultRow)
                    ((DataRowView)(dgvResult.CurrentRow.DataBoundItem)).Row);
        }
    }
}
