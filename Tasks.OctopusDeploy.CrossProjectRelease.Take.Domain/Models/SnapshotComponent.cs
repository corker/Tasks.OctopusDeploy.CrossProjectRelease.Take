using System;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models
{
    public class SnapshotComponent
    {
        public string ProjectId { get; }
        public string ProjectName { get; }
        public string ProjectGroupName { get; }

        public SnapshotComponent(string projectId, string projectName, string projectGroupName)
        {
            if (string.IsNullOrWhiteSpace(projectId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectId));
            }
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectName));
            }
            if (string.IsNullOrWhiteSpace(projectGroupName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(projectGroupName));
            }

            ProjectId = projectId;
            ProjectName = projectName;
            ProjectGroupName = projectGroupName;
        }
    }
}