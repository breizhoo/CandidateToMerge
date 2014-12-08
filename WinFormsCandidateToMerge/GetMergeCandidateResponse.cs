using Microsoft.TeamFoundation.VersionControl.Client;

namespace WinFormsCandidateToMerge
{
    public class GetMergeCandidateResponse
    {
        public string BranchName { get; set; }

        public string Project { get; set; }

        public ChangesetInterne[] MergeCandidates { get; set; }
    }
}