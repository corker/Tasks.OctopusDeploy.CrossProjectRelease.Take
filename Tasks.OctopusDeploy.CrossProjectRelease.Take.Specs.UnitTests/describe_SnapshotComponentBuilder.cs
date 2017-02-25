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
    internal class describe_SnapshotComponentBuilder : nspec
    {
        private SnapshotComponentBuilder _builder;
        private IEnumerable<SnapshotComponent> _actualComponents;
        private SnapshotComponent _actualComponent;
        private const string ExpectedProjectId = "Project01";
        private const string ExpectedProjectName = "ProjectName01";
        private ProjectResource _projectResource;
        private const string ProductsProjectGroupId = "ProductsGroupId";
        private const string ComponentsProjectGroupId = "ComponentsGroupId";
        private const string ProductsProjectGroupName = "Products";
        private const string ComponentsProjectGroupName = "Components";

        private void before_each()
        {
            var repository = A.Fake<IOctopusRepository>(x => x.Strict());
            _builder = new SnapshotComponentBuilder(repository);

            var groupRepository = A.Fake<IProjectGroupRepository>(x => x.Strict());
            A.CallTo(() => groupRepository.FindAll()).Returns(new List<ProjectGroupResource>
            {
                new ProjectGroupResource {Name = ProductsProjectGroupName, Id = ProductsProjectGroupId},
                new ProjectGroupResource {Name = ComponentsProjectGroupName, Id = ComponentsProjectGroupId}
            });

            var projectRepository = A.Fake<IProjectRepository>(x => x.Strict());
            _projectResource = new ProjectResource {Name = ExpectedProjectName, Id = ExpectedProjectId};
            A.CallTo(() => projectRepository.FindAll()).Returns(new List<ProjectResource> {_projectResource});

            A.CallTo(() => repository.ProjectGroups).Returns(groupRepository);
            A.CallTo(() => repository.Projects).Returns(projectRepository);
        }

        private void when_build_a_snapshot_component()
        {
            act = () => _actualComponents = _builder.Build();

            context["with a product project"] = () =>
            {
                before = () => { _projectResource.ProjectGroupId = ProductsProjectGroupId; };

                it["should not build a component"] = () => { _actualComponents.should_be_empty(); };
            };

            context["with a component project"] = () =>
            {
                before = () => { _projectResource.ProjectGroupId = ComponentsProjectGroupId; };

                act = () => _actualComponent = _actualComponents.Single();

                it["should build a component"] = () => { _actualComponents.should_not_be_empty(); };

                it["should build a component with expected project id"] =
                    () => { _actualComponent.ProjectId.should_be(ExpectedProjectId); };

                it["should build a component with expected project group name"] =
                    () => { _actualComponent.ProjectGroupName.should_be(ComponentsProjectGroupName); };

                it["should build a component with expected project name"] =
                    () => { _actualComponent.ProjectName.should_be(ExpectedProjectName); };
            };
        }
    }
}