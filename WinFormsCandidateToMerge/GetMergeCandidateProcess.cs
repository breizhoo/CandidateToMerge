using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
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
            }

            var changeSetId = listResponseProject.SelectMany(x => x.MergeCandidates)
                               .SelectMany(x => x.AssociatedWorkItems)
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
                    order by [System.Id] mode(mustcontain)
                ", string.Join(",", ids));

                Query wiQuery = new Query(workItemStore, queryy);
                WorkItemLinkInfo[] wiTrees = wiQuery.RunLinkQuery();
                WorkItemLinkInfo[] wiTrees2 = wiTrees.Where(x => x.SourceId > 0).ToArray();
                var idsss = wiTrees2.Select(x => x.SourceId).Distinct().ToArray();

                var userStories = workItemStore.Query(string.Format(@"
                    SELECT *
                    FROM WorkItems 
                    WHERE [System.Id] in ({0})
                ", string.Join(",", idsss))).Cast<WorkItem>().Distinct().ToList();


                foreach (var getMergeCandidateResponse in listResponseProject)
                {
                    foreach (var changesetInterne in getMergeCandidateResponse.MergeCandidates)
                    {
                        changesetInterne.UserStory = 
                        wiTrees2
                            .SelectMany(x => changesetInterne
                                                 .AssociatedWorkItems.Where(y => y.Id == x.TargetId)
                                                 .Select(y => x.SourceId)
                            ).Select(j =>
                                     userStories.FirstOrDefault(t => t.Id == j)
                            ).ToArray();
                    }
                }
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