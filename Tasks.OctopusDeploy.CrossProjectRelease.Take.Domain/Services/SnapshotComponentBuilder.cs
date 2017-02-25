using System.Collections.Generic;
using System.Linq;
using Octopus.Client;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services
{
    public class SnapshotComponentBuilder : IBuildSnapshotComponents
    {
        private static readonly HashSet<string> ExcludedProjectGroupNames = new HashSet<string>
        {
            "Products"
        };

        private readonly IOctopusRepository _repository;

        public SnapshotComponentBuilder(IOctopusRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SnapshotComponent> Build()
        {
            var projectGroupResources = _repository.ProjectGroups.FindAll();
            var projectGroupResourceById = projectGroupResources.ToDictionary(x => x.Id);

            var allowedProjectGroupIds = projectGroupResources
                .Where(x => !ExcludedProjectGroupNames.Contains(x.Name))
                .ToLookup(x => x.Id);

            var projectResources = _repository.Projects.FindAll();

            var productComponents = projectResources
                .Where(x => allowedProjectGroupIds.Contains(x.ProjectGroupId))
                .Select(x =>
                {
                    var projectGroupName = projectGroupResourceById[x.ProjectGroupId].Name;
                    return new SnapshotComponent(x.Id, x.Name, projectGroupName);
                });

            return productComponents.ToArray();
        }
    }
}