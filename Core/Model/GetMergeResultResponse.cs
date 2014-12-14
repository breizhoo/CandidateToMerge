using System.Collections.Generic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WinFormsCandidateToMerge
{
    public class GetMergeResultResponse : List<GetMergeCandidateResponse>
    {
        public IEnumerable<WorkItemTyped> WorkItems;

        public IEnumerable<WorkItemLiaison> WorkItemLinkInfos;

    }
}