using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NSpec;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Specs.UnitTests
{
    internal class describe_snapshotStepBuilder : nspec
    {
        private SnapshotStepBuilder _builder;
        private IBuildSnapshotComponentVersions _versions;
        private IEnumerable<SnapshotComponentVersion> _snapshotComponentVersions;
        private string _releaseVersion;
        private string _releaseId;
        private SnapshotComponent _component1;
        private SnapshotComponent _component2;
        private IEnumerable<SnapshotStep> _actualSteps;

        private void before_each()
        {
            _versions = A.Fake<IBuildSnapshotComponentVersions>(x => x.Strict());
            _builder = new SnapshotStepBuilder(_versions);
            _component1 = new SnapshotComponent("Databases", "ProjectId", "ProjectGroupName");
            _component2 = new SnapshotComponent("WorkflowCookbooks", "ProjectId", "ProjectGroupName");
            _releaseId = "Release01";
            _releaseVersion = "ReleaseVersion01";
        }

        private void when_snapshot_steps_built()
        {
            act = () => { _actualSteps = _builder.Build(); };

            before = () =>
            {
                _snapshotComponentVersions = new List<SnapshotComponentVersion>
                {
                    new SnapshotComponentVersion(_component2, _releaseId, _releaseVersion),
                    new SnapshotComponentVersion(_component1, _releaseId, _releaseVersion)
                };
                A.CallTo(() => _versions.Build()).Returns(_snapshotComponentVersions);
            };

            it["should be in correct order"] = () =>
            {
                _actualSteps.Select(x => x.Index).should_be(Enumerable.Range(1, 2));
            };
        }
    }
}