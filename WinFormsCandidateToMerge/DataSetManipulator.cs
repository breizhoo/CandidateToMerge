using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsCandidateToMerge
{
    class DataSetManipulator
    {
        private readonly DsCandidateToMerge _dsCandidateToMerge;

        public DataSetManipulator(DsCandidateToMerge dsCandidateToMerge)
        {
            _dsCandidateToMerge = dsCandidateToMerge;
        }

        public IEnumerable<GetMergeCandidateRequest> GetMergeCandidateRequest()
        {
            return (from project in _dsCandidateToMerge.Projects.Rows.Cast<DsCandidateToMerge.ProjectsRow>()
                    from branch in _dsCandidateToMerge.Branchs.Rows.Cast<DsCandidateToMerge.BranchsRow>()
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

                if (_dsCandidateToMerge.Users.FindByName(owner) != null)
                    continue;

                var row = _dsCandidateToMerge.Users.NewUsersRow();
                row.Name = owner;
                row.IsToScan = true;
                _dsCandidateToMerge.Users.AddUsersRow(row);
            }
            _dsCandidateToMerge.AcceptChanges();
        }

        public void ClearMergeCandidate()
        {
            _dsCandidateToMerge.MergeResult.Clear();
            _dsCandidateToMerge.MergeResult.AcceptChanges();
        }

        public void AddMergeCandidate(IEnumerable<GetMergeCandidateResponse> mergeCandidates)
        {
            foreach (var resProj in mergeCandidates)
            {
                foreach (var mergeCandidate in resProj.MergeCandidates)
                {
                    var row = _dsCandidateToMerge.MergeResult.NewMergeResultRow();

                    row.ChangesetId = mergeCandidate.Changeset.ChangesetId;
                    row.Owner = mergeCandidate.Changeset.Owner;
                    row.CreationDate = mergeCandidate.Changeset.CreationDate;
                    row.Comment = mergeCandidate.Changeset.Comment;
                    row.BranchName = resProj.BranchName;
                    row.Project = resProj.Project;

                    _dsCandidateToMerge.MergeResult.AddMergeResultRow(row);
                }

            }
            _dsCandidateToMerge.AcceptChanges();
        }

        public string GetTfsUrl()
        {
            var tfsUrl = _dsCandidateToMerge.Configuration.FindByName("TfsUrl");
            if (tfsUrl != null)
                return tfsUrl.Value;
            return string.Empty;
        }

        public void SetTfsUrl(string url)
        {
            var tfsUrl = _dsCandidateToMerge.Configuration.FindByName("TfsUrl");
            var isNewRow = (tfsUrl == null);
            if (isNewRow)
            {
                tfsUrl = _dsCandidateToMerge.Configuration.NewConfigurationRow();
                tfsUrl.Name = "TfsUrl";
            }

            tfsUrl.Value = url;
            if (isNewRow)
                _dsCandidateToMerge.Configuration.AddConfigurationRow(tfsUrl);
        }

    }
}
