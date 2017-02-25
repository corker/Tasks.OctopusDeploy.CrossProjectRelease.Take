using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Services
{
    public interface IWriteSnapshots
    {
        void Write(Snapshot snapshot);
    }
}