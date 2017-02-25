using System.Collections.Generic;
using System.Linq;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services
{
    public class SnapshotStepBuilder : IBuildSnapshotSteps
    {
        private static readonly Dictionary<string, int> PriorityByProjectGroupName = new Dictionary<string, int>
        {
            {"AzureResourceGroups", 0},
            {"Databases", 1},
            {"ServiceCookbooks", 2},
            {"Services", 3},
            {"WorkflowCookbooks", 4},
            {"Workflows", 5},
            {"ApplicationCookbooks", 6},
            {"Applications", 7}
        };

        private readonly IBuildSnapshotComponentVersions _versions;

        public SnapshotStepBuilder(IBuildSnapshotComponentVersions versions)
        {
            _versions = versions;
        }

        public IEnumerable<SnapshotStep> Build()
        {
            var versions = _versions.Build();
            var versionsInOrder = versions.OrderBy(GetProjectPriority);
            var steps = versionsInOrder.Select(CreateStep);
            return steps.ToArray();
        }

        private static string GetProjectPriority(SnapshotComponentVersion version)
        {
            var priority = GetProjectGroupPriority(version.Component.ProjectGroupName);
            return $"{priority}{version.Component.ProjectName}";
        }

        private static SnapshotStep CreateStep(SnapshotComponentVersion version, int index)
        {
            return new SnapshotStep(version, index + 1);
        }

        private static int GetProjectGroupPriority(string projectGroupName)
        {
            int priority;
            return PriorityByProjectGroupName.TryGetValue(projectGroupName, out priority) ? priority : int.MaxValue;
        }
    }
}