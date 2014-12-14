namespace WinFormsCandidateToMerge
{
    public class GetMergeCandidateRequest
    {
        public string Project { get; set; }

        public string BranchName { get; set; }

        public string BranchSource { get; set; }
        public string BranchDestination { get; set; }

    }
}