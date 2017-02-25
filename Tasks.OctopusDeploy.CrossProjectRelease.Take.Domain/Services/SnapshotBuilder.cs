using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services
{
    public class SnapshotBuilder : IBuildSnapshots
    {
        private readonly ISnapshotEnvironment _environment;
        private readonly IBuildSnapshotSteps _steps;

        public SnapshotBuilder(ISnapshotEnvironment environment, IBuildSnapshotSteps steps)
        {
            _steps = steps;
            _environment = environment;
        }

        public Snapshot Build()
        {
            var steps = _steps.Build();
            return new Snapshot(_environment, steps);
        }
    }
}