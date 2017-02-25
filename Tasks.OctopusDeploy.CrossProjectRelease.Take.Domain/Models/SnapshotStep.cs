using System;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models
{
    public class SnapshotStep
    {
        public SnapshotStep(SnapshotComponentVersion version, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (version == null) throw new ArgumentNullException(nameof(version));

            Index = index;
            ProjectId = version.Component.ProjectId;
            ProjectName = version.Component.ProjectName;
            ReleaseVersion = version.ReleaseVersion;
            ReleaseId = version.ReleaseId;
        }

        public int Index { get; }
        public string ProjectId { get; }
        public string ProjectName { get; }
        public string ReleaseVersion { get; }
        public string ReleaseId { get; }
    }
}