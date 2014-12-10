using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.TeamFoundation;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Common.Internal;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WinFormsCandidateToMerge
{
    public class ChangesetInterne
    {
        public WorkItem[] UserStory { get; set; }

        /// <summary>
        /// Returns a limited set of information about the associated work items with this changeset. This property does not spin up a WorkItemStore. Therefore, it is very quick.
        /// </summary>
        public AssociatedWorkItemInfo[] AssociatedWorkItems { get; set; }
        /// <summary>
        /// Gets or sets the ID of this changeset.
        /// </summary>
        /// 
        /// <returns>
        /// The ID of this changeset.
        /// </returns>
        public int ChangesetId { get; set; }
        /// <summary>
        /// Gets or sets the check-in note of the changeset.
        /// </summary>
        /// 
        /// <returns>
        /// The check-in note of the changeset.
        /// </returns>
        public CheckinNote CheckinNote { get; set; }
        /// <summary>
        /// Gets or sets the comment of the changeset.
        /// </summary>
        /// 
        /// <returns>
        /// The comment of the changeset.
        /// </returns>
        public string Comment { get; set; }
        /// <summary>
        /// Gets or sets the user who committed this changeset.
        /// </summary>
        /// 
        /// <returns>
        /// The user who committed this changeset.
        /// </returns>
        public string Committer { get; set; }
        /// <summary>
        /// Display name of user who committed this change
        /// </summary>
        public string CommitterDisplayName { get; set; }
        /// <summary>
        /// Gets or sets the creation date of this changeset.
        /// </summary>
        /// 
        /// <returns>
        /// The creation date of this changeset.
        /// </returns>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Gets or sets the owner of this changeset.
        /// </summary>
        /// 
        /// <returns>
        /// The owner of this changeset.
        /// </returns>
        public string Owner { get; set; }
        /// <summary>
        /// Display name of user who owns this change. May differ from Committer if the change was imported or committed on behalf of another user.
        /// </summary>
        public string OwnerDisplayName { get; set; }

    }

    public class LambdaComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;
        private readonly Func<T, int> _hash;
        public LambdaComparer(Func<T, T, bool> comparer) :
            this(comparer, o => 0) { }
        public LambdaComparer(Func<T, T, bool> comparer, Func<T, int> hash)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");
            if (hash == null) throw new ArgumentNullException("hash");
            _comparer = comparer;
            _hash = hash;
        }
        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }
        public int GetHashCode(T obj)
        {
            return _hash(obj);
        }
    }

    public class GetMergeResultResponse : List<GetMergeCandidateResponse>
    {
        public IEnumerable<WorkItem> WorkItems;

        public IEnumerable<WorkItemLinkInfo> WorkItemLinkInfos;

    }

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

            Parallel.ForEach(_resquestOfMerge,
                             option,
                             resquestOfMerge =>
                             //foreach (var resquestOfMerge in _resquestOfMerge)
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
                                                 CheckinNote = x.Changeset.CheckinNote,
                                                 Comment = x.Changeset.Comment,
                                                 Committer = x.Changeset.Committer,
                                                 CommitterDisplayName = x.Changeset.CommitterDisplayName,
                                                 CreationDate = x.Changeset.CreationDate,
                                                 Owner = x.Changeset.Owner,
                                                 OwnerDisplayName = x.Changeset.Owner,
                                                 AssociatedWorkItems = x.Changeset.AssociatedWorkItems
                                             }).ToArray(),
                                             Project = project
                                         });
                                     }
                                 }
                                 catch (Exception ex)
                                 {
                                     Trace.WriteLine(ex.Message);
                                 }
                             });

            var changeSetId = listResponseProject.SelectMany(x => x.MergeCandidates)
                               .SelectMany(x => x.AssociatedWorkItems.Select(y => new WorkItemLinkInfo
                    {
                        TargetId = y.Id,
                        SourceId = x.ChangesetId
                    }))
                               .ToList();

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

                //workItemLinkInfos2.AddRange(changeSetId);
                //foreach (var l in look)
                //{
                //    foreach (var workItemLinkInfo in changeSetId)
                //    {
                //        if (!l.Contains(workItemLinkInfo.TargetId))
                //            continue;

                //        workItemLinkInfos2.AddRange(
                //        l[workItemLinkInfo.TargetId].Select(x => new WorkItemLinkInfo
                //        {
                //            SourceId = workItemLinkInfo.SourceId,
                //            TargetId = x.TargetId
                //        }));
                //    }
                //}

                var allLiaison = DistinctWorkItemLinkInfo(workItemLinkInfos2);

                var allWorkItems = allLiaison.Select(x => x.SourceId)
                    .Union(allLiaison.Select(x => x.TargetId))
                    .Distinct();

                //Ca !
                var workItems = GetWorkItems(workItemStore, allWorkItems);


                listResponseProject.WorkItemLinkInfos = allLiaison;
                listResponseProject.WorkItems = workItems;

                //var wiTrees1 = GetParentIds(ids, workItemStore);
                //var idsss1 = wiTrees1.Select(x => x.SourceId).Distinct();
                //var wiTrees2 = GetParentIds(idsss1, workItemStore);
                //var idsss2 = wiTrees2.Select(x => x.SourceId).Distinct();
                //var wiTrees3 = GetParentIds(idsss2, workItemStore);
                ////var idsss3 = wiTrees3.Select(x => x.SourceId).Distinct();


                ////var dico0 = changeSetId.ToLookup(x => x.SourceId);
                //var dico1 = wiTrees1.ToLookup(x => x.TargetId);
                //var dico2 = wiTrees2.ToLookup(x => x.TargetId);
                //var dico3 = wiTrees3.ToLookup(x => x.TargetId);

                //var wiTreesParent = changeSetId.ToList().Union(wiTrees1);

                //var wiTreesParent1 = wiTreesParent.SelectMany(x =>
                //    dico1[x.SourceId].Select(y => new WorkItemLinkInfo
                //    {
                //        TargetId = x.TargetId,
                //        SourceId = y.SourceId
                //    })).Union(wiTrees2);
                //var wiTreesParent2 = wiTreesParent1.SelectMany(x =>
                //    dico2[x.TargetId].Select(y => new WorkItemLinkInfo
                //    {
                //        TargetId = x.TargetId,
                //        SourceId = y.SourceId
                //    })).Union(wiTrees3);
                //var wiTreesParent3 = wiTreesParent2.SelectMany(x =>
                //    dico3[x.TargetId].Select(y => new WorkItemLinkInfo
                //    {
                //        TargetId = x.TargetId,
                //        SourceId = y.SourceId
                //    }));

                ////Ca !
                //wiTreesParent = DistinctWorkItemLinkInfo(wiTreesParent.Union(wiTreesParent1).Union(wiTreesParent2).Union(wiTreesParent3))
                //    .OrderBy(x => x.TargetId)
                //    .ToList();

                //var allLiaison = DistinctWorkItemLinkInfo(wiTrees1.Union(wiTrees2).Union(wiTrees3));

                //var allWorkItems = allLiaison.Select(x => x.SourceId)
                //    .Union(allLiaison.Select(x => x.TargetId))
                //    .Union(ids) // except changSet already downloaded.
                //    .Distinct();

                ////Ca !
                //var workItems = GetWorkItems(workItemStore, allWorkItems);


                //listResponseProject.WorkItemLinkInfos = wiTreesParent;
                //listResponseProject.WorkItems = workItems;


                //foreach (var getMergeCandidateResponse in listResponseProject)
                //{
                //    foreach (var changesetInterne in getMergeCandidateResponse.MergeCandidates)
                //    {
                //        changesetInterne.UserStory = 
                //        wiTrees2
                //            .SelectMany(x => changesetInterne
                //                                 .AssociatedWorkItems.Where(y => y.Id == x.TargetId)
                //                                 .Select(y => x.SourceId)
                //            ).Select(j =>
                //                     userStories.FirstOrDefault(t => t.Id == j)
                //            ).ToArray();
                //    }
                //}
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