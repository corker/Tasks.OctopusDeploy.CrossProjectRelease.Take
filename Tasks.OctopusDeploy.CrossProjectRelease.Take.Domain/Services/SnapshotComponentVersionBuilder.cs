using System;
using System.Collections.Generic;
using System.Linq;
using Octopus.Client;
using Octopus.Client.Model;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services
{
    public class SnapshotComponentVersionBuilder : IBuildSnapshotComponentVersions
    {
        private readonly IBuildSnapshotComponents _components;
        private readonly ISnapshotEnvironment _environment;
        private readonly IOctopusRepository _repository;

        public SnapshotComponentVersionBuilder(
            ISnapshotEnvironment environment,
            IOctopusRepository repository,
            IBuildSnapshotComponents components
            )
        {
            _environment = environment;
            _repository = repository;
            _components = components;
        }

        public IEnumerable<SnapshotComponentVersion> Build()
        {
            var dashboard = _repository.Dashboards.GetDashboard();
            var items = dashboard.Items.Where(x => x.EnvironmentId == _environment.Id);
            var componentByProjectId = _components.Build().ToDictionary(x => x.ProjectId);
            var versions = items.Select(item =>
            {
                var component = componentByProjectId[item.ProjectId];
                if (item.State != TaskState.Success)
                {
                    var projectName = component.ProjectName;
                    string message = $"Can't schedule an update for {projectName}. The project has {item.State} state.";
                    throw new InvalidOperationException(message);
                }
                return new SnapshotComponentVersion(component, item.ReleaseId, item.ReleaseVersion);
            });
            return versions.ToArray();
        }
    }
}