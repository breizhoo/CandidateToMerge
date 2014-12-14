using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WinFormsCandidateToMerge
{
    public class DataSetManipulator : IDataSetManipulator
    {
        private readonly DsCandidateToMerge _dsCandidateToMerge;

        public DataSetManipulator(DsCandidateToMerge dsCandidateToMerge)
        {
            _dsCandidateToMerge = dsCandidateToMerge;
        }

        public DataSetManipulator()
            : this(new DsCandidateToMerge())
        {
        }

        public DsCandidateToMerge DsCandidateToMerge
        {
            get { return _dsCandidateToMerge; }
        }

        public IEnumerable<GetMergeCandidateRequest> GetMergeCandidateRequest()
        {
            return (from project in DsCandidateToMerge.Projects.Rows.Cast<DsCandidateToMerge.ProjectsRow>()
                    from branch in DsCandidateToMerge.Branchs.Rows.Cast<DsCandidateToMerge.BranchsRow>()
                    where project.IsToScan && branch.IsToScan
                    select new GetMergeCandidateRequest
                    {
                        BranchDestination = branch.BranchDestination,
                        BranchName = branch.Name,
                        BranchSource = branch.BranchSource,
                        Project = project.Project
                    }).ToList();
        }

        public void AddUserIfNotExist(IEnumerable<string> owners)
        {
            foreach (var owner in owners)
            {

                if (DsCandidateToMerge.Users.FindByName(owner) != null)
                    continue;

                var row = DsCandidateToMerge.Users.NewUsersRow();
                row.Name = owner;
                row.IsToScan = true;
                DsCandidateToMerge.Users.AddUsersRow(row);
            }
            DsCandidateToMerge.AcceptChanges();
        }

        public void ClearMergeCandidate()
        {
            DsCandidateToMerge.MergeResult.Clear();
            DsCandidateToMerge.WorkItems.Clear();
            DsCandidateToMerge.WorkItemLinkInfos.Clear();
            DsCandidateToMerge.WorkItemLinkInfosUserStory.Clear();
            DsCandidateToMerge.WorkItemLinkInfosFeature.Clear();
            
            DsCandidateToMerge.MergeResult.AcceptChanges();
            DsCandidateToMerge.WorkItems.AcceptChanges();
            DsCandidateToMerge.WorkItemLinkInfos.AcceptChanges();
            DsCandidateToMerge.WorkItemLinkInfosUserStory.AcceptChanges();
            DsCandidateToMerge.WorkItemLinkInfosFeature.AcceptChanges();
        }

        public void Ignore(DsCandidateToMerge.MergeResultRow mergeCandidates)
        {
            DsCandidateToMerge.MergeIgnore.AddMergeIgnoreRow(
                mergeCandidates.ChangesetId, mergeCandidates.Project, mergeCandidates.BranchName, true);
        }

        public void AddMergeCandidate(IEnumerable<GetMergeCandidateResponse> mergeCandidates)
        {
            foreach (var resProj in mergeCandidates)
            {
                foreach (var mergeCandidate in resProj.MergeCandidates)
                {
                    var row = DsCandidateToMerge.MergeResult.NewMergeResultRow();

                    row.ChangesetId = mergeCandidate.ChangesetId;
                    row.Owner = mergeCandidate.Owner;
                    row.CreationDate = mergeCandidate.CreationDate;
                    row.Comment = mergeCandidate.Comment;
                    row.BranchName = resProj.BranchName;
                    row.Project = resProj.Project;

                    DsCandidateToMerge.MergeResult.AddMergeResultRow(row);
                }

            }
            DsCandidateToMerge.AcceptChanges();
        }

        public void AddMergeCandidate(IEnumerable<WorkItemTyped> workItems)
        {
            foreach (var workItem in workItems)
            {
                var row = DsCandidateToMerge.WorkItems.NewWorkItemsRow();
                row.Id = workItem.Id;
                row.Name = workItem.Title;
                row.Type = workItem.TypeName;
                DsCandidateToMerge.WorkItems.AddWorkItemsRow(row);
            }
            DsCandidateToMerge.AcceptChanges();
        }



        public void AddMergeCandidate(IEnumerable<WorkItemLiaison> workItems)
        {
            foreach (var workItem in workItems)
            {
                var row = DsCandidateToMerge.WorkItemLinkInfos.NewWorkItemLinkInfosRow();
                row.SourceId = workItem.SourceId;
                row.TargetId = workItem.TargetId;
                DsCandidateToMerge.WorkItemLinkInfos.AddWorkItemLinkInfosRow(row);
            }
            DsCandidateToMerge.AcceptChanges();

            foreach (var workItem in DsCandidateToMerge.WorkItemLinkInfos.Where(x => x.TargetType == "User Story"))
            {
                var row = DsCandidateToMerge.WorkItemLinkInfosUserStory.NewWorkItemLinkInfosUserStoryRow();
                row.Id = workItem.TargetId;
                row.ChangesetId = workItem.SourceId;
                DsCandidateToMerge.WorkItemLinkInfosUserStory.AddWorkItemLinkInfosUserStoryRow(row);
            }
            DsCandidateToMerge.AcceptChanges();

            foreach (var workItem in DsCandidateToMerge.WorkItemLinkInfos.Where(x => x.TargetType == "Feature"))
            {
                var row = DsCandidateToMerge.WorkItemLinkInfosFeature.NewWorkItemLinkInfosFeatureRow();
                row.Id = workItem.TargetId;
                row.ChangesetId = workItem.SourceId;
                DsCandidateToMerge.WorkItemLinkInfosFeature.AddWorkItemLinkInfosFeatureRow(row);
            }

            DsCandidateToMerge.AcceptChanges();
        }

        public string GetTfsUrl()
        {
            var tfsUrl = DsCandidateToMerge.Configuration.FindByName("TfsUrl");
            if (tfsUrl != null)
                return tfsUrl.Value;
            return string.Empty;
        }

        public void SetTfsUrl(string url)
        {
            var tfsUrl = DsCandidateToMerge.Configuration.FindByName("TfsUrl");
            var isNewRow = (tfsUrl == null);
            if (isNewRow)
            {
                tfsUrl = DsCandidateToMerge.Configuration.NewConfigurationRow();
                tfsUrl.Name = "TfsUrl";
            }
            if (isNewRow || tfsUrl.Value != url)
                tfsUrl.Value = url;
            if (isNewRow)
                DsCandidateToMerge.Configuration.AddConfigurationRow(tfsUrl);
            tfsUrl.AcceptChanges();
        }

    }
}
