using System.Collections.Generic;

namespace WinFormsCandidateToMerge
{
    public interface IDataSetManipulator
    {
        DsCandidateToMerge DsCandidateToMerge { get; }
        IEnumerable<GetMergeCandidateRequest> GetMergeCandidateRequest();
        void AddUserIfNotExist(IEnumerable<string> owners);
        void ClearMergeCandidate();
        void Ignore(DsCandidateToMerge.MergeResultRow mergeCandidates);
        void AddMergeCandidate(IEnumerable<GetMergeCandidateResponse> mergeCandidates);
        void AddMergeCandidate(IEnumerable<WorkItemTyped> workItems);
        void AddMergeCandidate(IEnumerable<WorkItemLiaison> workItems);
        string GetTfsUrl();
        void SetTfsUrl(string url);
    }
}