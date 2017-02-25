using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services
{
    public interface IBuildSnapshots
    {
        Snapshot Build();
    }
}