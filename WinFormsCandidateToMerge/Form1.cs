using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using WeifenLuo.WinFormsUI.Docking;

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
            _dataSerializer = new DataSerializer(dsCandidateToMerge1);
            
            IsMdiContainer = true;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
            _dataSetManipulator = new DataSetManipulator(dsCandidateToMerge1);
            _uiSerializer = new UiSerializer(this);

            _changesetVisualizer = new ChangesetVisualizer();

            ParametersForms = new ParametersForms(_dataSetManipulator);

            UsersFroms = new UsersForms(_dataSetManipulator);
            ProjectForms = new ProjectsForms(_dataSetManipulator);
            BranchsForms = new BranchsForms(_dataSetManipulator);
            ChangesetForms = new ChangeSetFormsOld(_dataSetManipulator);


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
            var reponseOfMerge = new GetMergeCandidateProcess(_dataSetManipulator.GetTfsUrl(),
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

            _uiSerializer.Save();
            _dataSerializer.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            _dataSerializer.Restore();
            _uiSerializer.Restore();

            base.OnLoad(e);



            if (UsersFroms.DockPanel == null)
                UsersFroms.Show(dockPanel1, DockState.DockLeftAutoHide);
            if (ParametersForms.DockPanel == null) 
                ParametersForms.Show(dockPanel1, DockState.DockLeftAutoHide);
            if (ProjectForms.DockPanel == null) 
                ProjectForms.Show(dockPanel1, DockState.DockLeftAutoHide);
            if (BranchsForms.DockPanel == null) 
                BranchsForms.Show(dockPanel1, DockState.DockLeftAutoHide);
            if (ChangesetForms.DockPanel == null) 
                ChangesetForms.Show(dockPanel1, DockState.Document);

            ParametersForms.Initialise();
            ChangesetForms.Initialise();
            UsersFroms.Initialise();
            BranchsForms.Initialise();
            ProjectForms.Initialise();

        }


        private string project;
        public readonly ParametersForms ParametersForms;
        public readonly UsersForms UsersFroms;
        public readonly ProjectsForms ProjectForms;
        public readonly BranchsForms BranchsForms;
        public readonly ChangeSetFormsOld ChangesetForms;
    }

}
