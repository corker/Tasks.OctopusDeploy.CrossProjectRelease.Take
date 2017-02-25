using System;
using System.Collections.Generic;
using System.Linq;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models
{
    public class Snapshot
    {
        public Snapshot(ISnapshotEnvironment environment, IEnumerable<SnapshotStep> steps)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }
            if (steps == null)
            {
                throw new ArgumentNullException(nameof(steps));
            }

            EnvironmentId = environment.Id;
            EnvironmentName = environment.Name;
            Steps = steps.ToArray();
        }

        public string EnvironmentId { get; }
        public string EnvironmentName { get; }
        public SnapshotStep[] Steps { get; }
    }
}