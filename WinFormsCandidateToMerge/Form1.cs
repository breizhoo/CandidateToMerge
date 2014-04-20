using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using WinFormsCandidateToMerge.Properties;

namespace WinFormsCandidateToMerge
{
    public partial class CandidateToMerge : Form
    {
        public CandidateToMerge()
        {
            InitializeComponent();

            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var r = e.Result as Response;
            foreach (var owner in r.Owners.Distinct())
            {

                if (dsCandidateToMerge1.Users.FindByName(owner) != null)
                    continue;

                var row = dsCandidateToMerge1.Users.NewUsersRow();
                row.Name = owner;
            }

            foreach (var resProj in r.ResponseProjects)
            {
                foreach (var mergeCandidate in resProj.MergeCandidates)
                {
                    var row = dsCandidateToMerge1.MergeResult.NewMergeResultRow();

                    row.ChangesetId = mergeCandidate.Changeset.ChangesetId;
                    row.Owner = mergeCandidate.Changeset.Owner;
                    row.CreationDate = mergeCandidate.Changeset.CreationDate;
                    row.Comment = mergeCandidate.Changeset.Comment;
                    row.BranchName = resProj.BranchName;
                    row.Project = resProj.Project;

                    dsCandidateToMerge1.MergeResult.AddMergeResultRow(row);
                }
                
            }
            dsCandidateToMerge1.AcceptChanges();


            _waitForms.Close();
        }

        public class ResponseProject
        {
            public string BranchName { get; set; }

            public string Project { get; set; }

            public MergeCandidate[] MergeCandidates { get; set; }
        }

        public class Response
        {
            public List<string> Owners = new List<string>();


            public List<ResponseProject> ResponseProjects = new List<ResponseProject>();

        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var res = new Response();

            dgvResult.SuspendLayout();
            dataGridView2.SuspendLayout();

            var teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(txtTfsUrl.Text));

            var project = dsCandidateToMerge1.Projects.Rows.Cast<DsCandidateToMerge.ProjectsRow>()
                .Where(x => x.IsToScan)
                .Select(x => x.Project);

            var branch = dsCandidateToMerge1.Branchs.Rows.Cast<DsCandidateToMerge.BranchsRow>()
                .Where(x => x.IsToScan)
                .Select(x => new { x.BranchSource, x.BranchDestination, x.Name });

            foreach (var p in project)
            {
                foreach (var b in branch)
                {
                    try
                    {
                        var versionControl = teamProjectCollection.GetService<VersionControlServer>();
                        var mergeCandidates =
                            versionControl.GetMergeCandidates(p + b.BranchSource,
                                                              p + b.BranchDestination, RecursionType.Full);


                        res.Owners.AddRange(mergeCandidates.Select(x => x.Changeset.Owner).Distinct());
                        res.ResponseProjects.Add(new ResponseProject()
                        {
                            BranchName = b.Name,
                            MergeCandidates = mergeCandidates,
                            Project = p
                        });
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                }
            }

            e.Result = res;
        }

        private ColumnState[] GetColumnState(DataGridViewColumnCollection c)
        {
            return c.Cast<DataGridViewColumn>()
                .Select(x => new ColumnState
                {
                    DisplayIndex = x.DisplayIndex,
                    Identifier = x.Name,
                    Width = x.Width
                }).ToArray();
        }

        private void SetColumnState(DataGridViewColumnCollection c, ColumnState[] cs)
        {
            foreach (var csc in cs)
            {
                c[csc.Identifier].DisplayIndex = csc.DisplayIndex;
                c[csc.Identifier].Width = csc.Width;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            var s = new SaveState
            {
                SplitterDistance1 = splitContainer1.SplitterDistance,
                SplitterDistance2 = splitContainer2.SplitterDistance,
                SplitterDistance3 = splitContainer3.SplitterDistance,

                DataGridView2 = GetColumnState(dataGridView2.Columns),
                DataGridView3 = GetColumnState(dataGridView3.Columns),
                DataGridView4 = GetColumnState(dataGridView4.Columns),
                DataGridView1 = GetColumnState(dgvResult.Columns)
            };

            var xs = new XmlSerializer(typeof(SaveState));
            using (var wr = new StreamWriter("SaveState.xml"))
            {
                xs.Serialize(wr, s);
            }

            var tfsUrl = dsCandidateToMerge1.Configuration.FindByName("TfsUrl");
            var isNewRow = (tfsUrl == null);
            if (isNewRow)
            {
                tfsUrl = dsCandidateToMerge1.Configuration.NewConfigurationRow();
                tfsUrl.Name = "TfsUrl";
            }

            tfsUrl.Value = txtTfsUrl.Text;
            if (isNewRow)
                dsCandidateToMerge1.Configuration.AddConfigurationRow(tfsUrl);

            dsCandidateToMerge1.AcceptChanges();


            dsCandidateToMerge1.WriteXml(@"Data.xml");

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var f = new FileInfo(@"Data.xml");
            if (f.Exists)
                dsCandidateToMerge1.ReadXml(@"Data.xml");
            var tfsUrl = dsCandidateToMerge1.Configuration.FindByName("TfsUrl");
            if (tfsUrl != null)
                txtTfsUrl.Text = tfsUrl.Value;

            dgvResult.DataSource = new DataView(dsCandidateToMerge1.MergeResult, "IsToDisplay = true", "", DataViewRowState.CurrentRows);

            if (new FileInfo("SaveState.xml").Exists)
            {
                var xs = new XmlSerializer(typeof(SaveState));
                using (var rd = new StreamReader("SaveState.xml"))
                {
                    var p = xs.Deserialize(rd) as SaveState;

                    splitContainer1.SplitterDistance = p.SplitterDistance1;
                    splitContainer2.SplitterDistance = p.SplitterDistance2;
                    splitContainer3.SplitterDistance = p.SplitterDistance3;

                    SetColumnState(dgvResult.Columns, p.DataGridView1);
                    SetColumnState(dataGridView2.Columns, p.DataGridView2);
                    SetColumnState(dataGridView3.Columns, p.DataGridView3);
                    SetColumnState(dataGridView4.Columns, p.DataGridView4);
                }

            }
        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private WaitForms _waitForms;

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            _waitForms = new WaitForms();
            dsCandidateToMerge1.MergeResult.Clear();
            backgroundWorker1.RunWorkerAsync();
            _waitForms.ShowDialog(this);



            //var versionControl = teamProjectCollection.GetService<VersionControlServer>();
            //var mergeCandidates =
            //    versionControl.GetMergeCandidates(@"$/isvalid/trunk",
            //                                      @"$/isvalid/tags2", RecursionType.Full);
            //Console.WriteLine("--- Start ---");
            //foreach (var mergeCandidate in mergeCandidates)
            //{
            //    Console.WriteLine(string.Format("{0} {1} {2} {3}",
            //                                    mergeCandidate.Changeset.ChangesetId,
            //                                    mergeCandidate.Changeset.Owner,
            //                                    mergeCandidate.Changeset.CreationDate,
            //                                    mergeCandidate.Changeset.Comment));
            //}
            //Console.WriteLine("--- Stop ---");
            //Console.ReadLine();
        }
    }
}
