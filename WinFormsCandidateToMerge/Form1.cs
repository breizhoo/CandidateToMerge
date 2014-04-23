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
            _dataSetManipulator = new DataSetManipulator(dsCandidateToMerge1);
            _uiSerializer = new UiSerializer(this);
            _dataSerializer = new DataSerializer(dsCandidateToMerge1);
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



        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var response = new DataExchangeBackgroundWorker();

            var reponseOfMerge = new GetMergeCandidateProcess(txtTfsUrl.Text,
                _dataSetManipulator.GetMergeCandidateRequest())
                .GetMergeResult();

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

    }
}
