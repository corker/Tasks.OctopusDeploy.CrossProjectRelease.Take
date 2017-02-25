namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services
{
    public interface ISnapshotEnvironment
    {
        string Id { get; }
        string Name { get; }
    }
}