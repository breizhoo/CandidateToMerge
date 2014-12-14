using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Build.Workflow.Tracking;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Common.Internal;
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

        public GetMergeResultResponse GetMergeResult(Action<GetMergeCandidateRequest> processing = null)
        {
            var listResponseProject = new GetMergeResultResponse();

            var teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(_tfsUrl));
            var workItemStore = teamProjectCollection.GetService<WorkItemStore>();
            var versionControl = teamProjectCollection.GetService<VersionControlServer>();
            var linking = teamProjectCollection.GetService<ILinking>();
            var option = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            var changeSetId = new List<WorkItemLinkInfo>();

            Parallel.ForEach(_resquestOfMerge,
                             option,
                             resquestOfMerge =>
                             {
                                 if (processing != null)
                                     processing(resquestOfMerge);

                                 var project = resquestOfMerge.Project;
                                 var branchDestination = resquestOfMerge.BranchDestination;
                                 var branchSource = resquestOfMerge.BranchSource;
                                 var branchName = resquestOfMerge.BranchName;

                                 try
                                 {
                                     if (versionControl.ServerItemExists(project + branchDestination,
                                                                         VersionSpec.Latest,
                                                                         DeletedState.NonDeleted,
                                                                         ItemType.Any))
                                     {
                                         var mergeCandidates = versionControl.GetMergeCandidates(
                                                                                                 project + branchSource,
                                                                                                 project +
                                                                                                 branchDestination,
                                                                                                 RecursionType.Full);


                                         listResponseProject.Add(new GetMergeCandidateResponse()
                                         {
                                             BranchName = branchName,
                                             MergeCandidates = mergeCandidates.Select(x => new ChangesetInterne
                                             {
                                                 ChangesetId = x.Changeset.ChangesetId,
                                                 Comment = x.Changeset.Comment,
                                                 Committer = x.Changeset.Committer,
                                                 CommitterDisplayName = x.Changeset.CommitterDisplayName,
                                                 CreationDate = x.Changeset.CreationDate,
                                                 Owner = x.Changeset.Owner,
                                                 OwnerDisplayName = x.Changeset.Owner,
                                             }).ToArray(),
                                             Project = project
                                         });

                                         changeSetId.AddRange(
                                         mergeCandidates.SelectMany(x => x.Changeset.AssociatedWorkItems.Select(y => new WorkItemLinkInfo
                                           {
                                               TargetId = y.Id,
                                               SourceId = x.Changeset.ChangesetId
                                           })));

                                     }
                                 }
                                 catch (Exception ex)
                                 {
                                     Trace.WriteLine(ex.Message);
                                 }
                             });

            try
            {
                // todo : http://msdn.microsoft.com/en-us/library/ee620634.aspx

                var ids = changeSetId
                    .Select(x => x.TargetId)
                    .Distinct()
                    .ToList();


                var workItemLinkInfos = new List<WorkItemLinkInfo>();
                var workItemLinkInfos2 = new List<WorkItemLinkInfo>();
                var look = new List<ILookup<int, WorkItemLinkInfo>>();
                while (ids.Count > 0)
                {
                    var wiTrees = GetParentIds(ids, workItemStore);
                    look.Add(wiTrees.ToLookup(x => x.SourceId));
                    ids = wiTrees.Select(x => x.TargetId).Distinct().ToList();
                    workItemLinkInfos.AddRange(wiTrees);
                }

                IEnumerable<WorkItemLinkInfo> itemtoAdd;
                workItemLinkInfos2.AddRange(workItemLinkInfos);
                workItemLinkInfos2.AddRange(changeSetId);
                do
                {

                    var result = DistinctWorkItemLinkInfo(from item in workItemLinkInfos2
                                                          join p in workItemLinkInfos2 on item.TargetId equals p.SourceId
                                                          select new WorkItemLinkInfo
                                                              {
                                                                  SourceId = item.SourceId,
                                                                  TargetId = p.TargetId
                                                              }).ToList();

                    itemtoAdd = result.Except(workItemLinkInfos2).ToList();
                    workItemLinkInfos2.AddRange(itemtoAdd);
                } while (itemtoAdd.Any());

                var allLiaison = DistinctWorkItemLinkInfo(workItemLinkInfos2);

                var allWorkItems = allLiaison.Select(x => x.SourceId)
                    .Union(allLiaison.Select(x => x.TargetId))
                    .Distinct();

                //Ca !
                var workItems = GetWorkItems(workItemStore, allWorkItems);


                listResponseProject.WorkItemLinkInfos = allLiaison.Select(x => new WorkItemLiaison()
                {
                    SourceId = x.SourceId,
                    TargetId = x.TargetId
                });
                listResponseProject.WorkItems = workItems.Select(x => new WorkItemTyped()
                {
                    Id = x.Id,
                    Title = x.Title,
                    TypeName = x.Type.Name
                });
            }
            catch (Exception ex)
            {
                throw;
            }


            return listResponseProject;
        }

        private IEnumerable<WorkItemLinkInfo> DistinctWorkItemLinkInfo(IEnumerable<WorkItemLinkInfo> workItemLinkInfos)
        {
            return workItemLinkInfos.Distinct(
                    new LambdaComparer<WorkItemLinkInfo>((x1, x2)
                        => x1.SourceId == x2.SourceId
                        && x1.TargetId == x2.TargetId));
        }

        private static IEnumerable<WorkItem> GetWorkItems(WorkItemStore workItemStore, IEnumerable<int> idsss)
        {
            var userStories = workItemStore.Query(string.Format(@"
                    SELECT *
                    FROM WorkItems 
                    WHERE [System.Id] in ({0})
                ", string.Join(",", idsss))).Cast<WorkItem>().Distinct().ToList();

            return userStories;
        }

        private static IEnumerable<WorkItemLinkInfo> GetParentIds(IEnumerable<int> ids, WorkItemStore workItemStore)
        {
            int parentLinkId = workItemStore.WorkItemLinkTypes.LinkTypeEnds["Parent"].Id;

            var queryy = string.Format(@"
                    SELECT *
                    FROM WorkItemLinks  
                    WHERE ([Source].[System.Id] in ({0}))
                    order by [System.Id] mode(mustcontain)
                ", string.Join(",", ids));

            var wiQuery = new Query(workItemStore, queryy);
            var wiTrees = wiQuery.RunLinkQuery();
            return wiTrees
                .Where(x => x.LinkTypeId == parentLinkId)
                .Where(x => x.TargetId > 0 && x.SourceId > 0).ToArray();
            //var wiTrees2 =

            //var idsss = wiTrees2.Select(x => x.SourceId).Distinct().ToArray();
            //return idsss;
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