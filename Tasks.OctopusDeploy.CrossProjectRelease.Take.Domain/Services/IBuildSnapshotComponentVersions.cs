using System.Collections.Generic;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services
{
    public interface IBuildSnapshotComponentVersions
    {
        IEnumerable<SnapshotComponentVersion> Build();
    }
}