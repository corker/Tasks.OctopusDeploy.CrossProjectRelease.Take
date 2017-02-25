using System.Collections.Generic;
using FakeItEasy;
using NSpec;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Specs.UnitTests
{
    internal class describe_SnapshotBuilder : nspec
    {
        private IBuildSnapshots _builder;
        private IBuildSnapshotSteps _steps;
        private ISnapshotEnvironment _environment;
        private Snapshot _actualSnapshot;
        private List<SnapshotStep> _expectedSteps;
        private string _expectedEnvironmentId;
        private string _expectedEnvironmentName;

        private void before_each()
        {
            _steps = A.Fake<IBuildSnapshotSteps>(x => x.Strict());
            _environment = A.Fake<ISnapshotEnvironment>(x => x.Strict());
            _builder = new SnapshotBuilder(_environment, _steps);
        }

        private void when_build_a_snapshot()
        {
            before = () =>
            {
                _expectedEnvironmentId = "EnvId01";
                _expectedEnvironmentName = "EnvName01";
                _expectedSteps = new List<SnapshotStep>();
                A.CallTo(() => _steps.Build()).Returns(_expectedSteps);
                A.CallTo(() => _environment.Id).Returns(_expectedEnvironmentId);
                A.CallTo(() => _environment.Name).Returns(_expectedEnvironmentName);
            };

            act = () => { _actualSnapshot = _builder.Build(); };

            it["should return a snapshot"] = () => { _actualSnapshot.should_not_be_null(); };

            it["should return expected environment id"] =
                () => { _actualSnapshot.EnvironmentId.should_be(_expectedEnvironmentId); };

            it["should return expected environment name"] =
                () => { _actualSnapshot.EnvironmentName.should_be(_expectedEnvironmentName); };

            it["should return expected steps"] = () => { _actualSnapshot.Steps.should_be(_expectedSteps); };
        }
    }
}