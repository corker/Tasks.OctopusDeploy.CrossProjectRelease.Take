using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NSpec;
using Octopus.Client;
using Octopus.Client.Model;
using Octopus.Client.Repositories;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Specs.UnitTests
{
    internal class describe_SnapshotComponentVersionBuilder : nspec
    {
        private SnapshotComponentVersionBuilder _builder;
        private SnapshotComponentVersion _actualVersion;
        private DashboardItemResource _itemResource;
        private SnapshotComponent _expectedComponent;
        private string _releaseId;
        private string _releaseVersion;

        private void before_each()
        {
            var environment = A.Fake<ISnapshotEnvironment>(x => x.Strict());
            var repository = A.Fake<IOctopusRepository>(x => x.Strict());
            var componentBuilder = A.Fake<IBuildSnapshotComponents>(x => x.Strict());
            _builder = new SnapshotComponentVersionBuilder(environment, repository, componentBuilder);

            var environmentId = "Env01";
            var projectId = "Proj01";
            _releaseId = "Release01";
            _releaseVersion = "ReleaseVersion01";

            A.CallTo(() => environment.Id).Returns(environmentId);

            _expectedComponent = new SnapshotComponent(projectId, "project name", "project group name");
            var components = new List<SnapshotComponent> {_expectedComponent};
            A.CallTo(() => componentBuilder.Build()).Returns(components);

            _itemResource = new DashboardItemResource
            {
                EnvironmentId = environmentId,
                ProjectId = projectId,
                ReleaseId = _releaseId,
                ReleaseVersion = _releaseVersion
            };
            var dashboardResource = new DashboardResource
            {
                Items = new List<DashboardItemResource>{_itemResource}
            };
            var dashboardRepository = A.Fake<IDashboardRepository>(x => x.Strict());
            A.CallTo(() => dashboardRepository.GetDashboard()).Returns(dashboardResource);
            A.CallTo(() => repository.Dashboards).Returns(dashboardRepository);
        }

        private void when_build_a_snapshot_component_version()
        {
            act = () => { _actualVersion = _builder.Build().FirstOrDefault(); };

            context["and component is not deployed successfully"] = () =>
            {
                before = () => { _itemResource.State = TaskState.Failed; };

                it["should throw invalid operation exception"] = expect<InvalidOperationException>();
            };

            context["and component is deployed sucessfully"] = () =>
            {
                before = () => { _itemResource.State = TaskState.Success; };

                it["should build a version"] = () =>
                {
                    _actualVersion.should_not_be_null();
                };

                it["should build a version with expected component"] = () =>
                {
                    _actualVersion.Component.should_be(_expectedComponent);
                };

                it["should build a version with expected release id"] = () =>
                {
                    _actualVersion.ReleaseId.should_be(_releaseId);
                };

                it["should build a version with expected release version"] = () =>
                {
                    _actualVersion.ReleaseVersion.should_be(_releaseVersion);
                };
            };
        }
    }
}