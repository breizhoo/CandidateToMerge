using System;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace WinFormsCandidateToMerge
{
    public class ChangesetInterne
    {

        /// <summary>
        /// Gets or sets the ID of this changeset.
        /// </summary>
        /// 
        /// <returns>
        /// The ID of this changeset.
        /// </returns>
        public int ChangesetId { get; set; }

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
}