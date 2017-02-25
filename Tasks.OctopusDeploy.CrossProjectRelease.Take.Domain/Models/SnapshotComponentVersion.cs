using System;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models
{
    public class SnapshotComponentVersion
    {
        public SnapshotComponentVersion(SnapshotComponent component, string releaseId, string releaseVersion)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }
            if (string.IsNullOrWhiteSpace(releaseVersion))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(releaseVersion));
            }
            if (string.IsNullOrWhiteSpace(releaseId))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(releaseId));
            }

            Component = component;
            ReleaseVersion = releaseVersion;
            ReleaseId = releaseId;
        }

        public SnapshotComponent Component { get; }
        public string ReleaseVersion { get; }
        public string ReleaseId { get; }
    }
}