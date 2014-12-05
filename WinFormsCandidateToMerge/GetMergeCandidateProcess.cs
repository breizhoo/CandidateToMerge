using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WinFormsCandidateToMerge
{
    public class GetMergeCandidateProcess
    {
        private readonly IEnumerable<GetMergeCandidateRequest> _resquestOfMerge;
        private readonly string _tfsUrl;

        public GetMergeCandidateProcess(string tfsUrl, IEnumerable<GetMergeCandidateRequest> resquestOfMerge)
        {
            _tfsUrl = tfsUrl;
            _resquestOfMerge = resquestOfMerge;
        }

        public List<GetMergeCandidateResponse> GetMergeResult(Action<GetMergeCandidateRequest> processing = null)
        {
            var listResponseProject = new List<GetMergeCandidateResponse>();

            var teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(_tfsUrl));
            var workItemStore = teamProjectCollection.GetService<WorkItemStore>();
            var versionControl = teamProjectCollection.GetService<VersionControlServer>();
            var linking = teamProjectCollection.GetService<ILinking>();

            foreach (var resquestOfMerge in _resquestOfMerge)
            {
                if (processing != null)
                    processing(resquestOfMerge);

                var project = resquestOfMerge.Project;
                var branchDestination = resquestOfMerge.BranchDestination;
                var branchSource = resquestOfMerge.BranchSource;
                var branchName = resquestOfMerge.BranchName;

                try
                {
                    if (versionControl.ServerItemExists(project + branchDestination, VersionSpec.Latest, DeletedState.NonDeleted, ItemType.Any))
                    {
                        var mergeCandidates = versionControl.GetMergeCandidates(project + branchSource, project + branchDestination, RecursionType.Full);


                        listResponseProject.Add(new GetMergeCandidateResponse()
                        {
                            BranchName = branchName,
                            MergeCandidates = mergeCandidates,
                            Project = project
                        });
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }

            var changeSetId = listResponseProject.SelectMany(x => x.MergeCandidates)
                               .SelectMany(x => x.Changeset.AssociatedWorkItems)
                               .ToList();


            try
            {
                var ids = changeSetId
                    .Select(x => x.Id.ToString())
                    .Distinct()
                    .ToList();

                var workItems = workItemStore.Query(string.Format(@"
                    SELECT *
                    FROM WorkItems 
                    WHERE [System.Id] in ({0})
                ", string.Join(",", ids))).Cast<WorkItem>().Distinct().ToList();

                var queryy = string.Format(@"
                    SELECT *
                    FROM WorkItemLinks  
                    WHERE ([Target].[System.Id] in ({0}))
                    AND [System.Links.LinkType] = 'System.LinkTypes.Hierarchy-Forward'
                    order by [System.Id] mode(mustcontain)
                ", string.Join(",", ids));

                Query wiQuery = new Query(workItemStore, queryy);
                WorkItemLinkInfo[] wiTrees = wiQuery.RunLinkQuery();
                WorkItemLinkInfo[] wiTrees2 = wiTrees.Where(x => x.SourceId > 0).ToArray();
                var idsss = wiTrees2.Select(x => x.SourceId).Distinct().ToArray();

                var userStories = workItemStore.Query(string.Format(@"
                    SELECT *
                    FROM WorkItems 
                    WHERE [System.WorkItemType] = 'User Story'
                      AND [System.Id] in ({0})
                ", string.Join(",", idsss))).Cast<WorkItem>().Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw;
            }


            return listResponseProject;
        }


        //public WorkItemLink GetParentLink(WorkItem myWorkItem)
        //{
        //    WorkItemLinkTypeEnd parentLinkTypeEnd = GetWorkItemStore.WorkItemLinkTypes.LinkTypeEnds["Parent"];
        //    List<WorkItemLink> parentLinks = new List<WorkItemLink>();
        //    WorkItemLink parentLink = null;
        //    foreach (WorkItemLink link in myWorkItem.WorkItemLinks)
        //    {
        //        if (link.LinkTypeEnd.Id == parentLinkTypeEnd.Id) parentLink = link;
        //    }
        //    return parentLink;
        //}
    }
}