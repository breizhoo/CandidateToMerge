using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BootStrapper;
using BrightIdeasSoftware;
using Core.Interface;
using WeifenLuo.WinFormsUI.Docking;

namespace WinFormsCandidateToMerge
{
    public partial class CandidateToMerge : Form
    {
        private readonly IDataSetManipulator _dataSetManipulator;
        private WaitForms _waitForms;
        private readonly UiSerializer _uiSerializer;
        private readonly DataSerializer _dataSerializer;
        private readonly IChangesetVisualizer _changesetVisualizer;

        public CandidateToMerge(IServiceLocator serviceLocator,
            IDataSetManipulator dataSetManipulator,
            IChangesetVisualizer changesetVisualizer)
        {
            _serviceLocator = serviceLocator;


            InitializeComponent();
            dsCandidateToMerge1 = dataSetManipulator.DsCandidateToMerge;
            _dataSerializer = new DataSerializer(dsCandidateToMerge1);
            
            IsMdiContainer = true;

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.WorkerReportsProgress = true;
            _dataSetManipulator = dataSetManipulator;
            _uiSerializer = new UiSerializer(this);

            _changesetVisualizer = changesetVisualizer;

            UsersFroms = serviceLocator.GetInstance<UsersForms>();
            ParametersForms = serviceLocator.GetInstance<ParametersForms>();
            ProjectForms = serviceLocator.GetInstance<ProjectsForms>();
            BranchsForms = serviceLocator.GetInstance<BranchsForms>();
            ChangesetForms = serviceLocator.GetInstance<ChangeSetFormsOld>();
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
            _dataSetManipulator.AddMergeCandidate(r.ResponseProjects.WorkItems);
            _dataSetManipulator.AddMergeCandidate(r.ResponseProjects.WorkItemLinkInfos);

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
            var listProcessed = new List<GetMergeCandidateRequest>();
            var reponseOfMerge = new GetMergeCandidateProcess(_dataSetManipulator.GetTfsUrl(),
                list)
                .GetMergeResult(processing: x =>
                {
                    listProcessed.Add(x);
                    var index = list.IndexOf(x);
                    var percent = (int)((decimal)100 / list.Count * listProcessed.Count());
                    currentProject = list.ElementAt(index).Project;
                    if (percent > 0)
                        backgroundWorker1.ReportProgress(percent);

                });

            response.Owners = (from items in reponseOfMerge
                               from mergeItem in items.MergeCandidates
                               select mergeItem.Owner)
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
        private readonly IServiceLocator _serviceLocator;
    }

}
