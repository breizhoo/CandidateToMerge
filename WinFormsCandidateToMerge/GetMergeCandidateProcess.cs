using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;

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

            //var teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(_tfsUrl, true, true);
            var teamProjectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri(_tfsUrl));
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
                    var versionControl = teamProjectCollection.GetService<VersionControlServer>();
                    var mergeCandidates =
                        versionControl.GetMergeCandidates(project + branchSource,
                            project + branchDestination, RecursionType.Full);


                    listResponseProject.Add(new GetMergeCandidateResponse()
                    {
                        BranchName = branchName,
                        MergeCandidates = mergeCandidates,
                        Project = project
                    });
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
            return listResponseProject;
        }
    }
}