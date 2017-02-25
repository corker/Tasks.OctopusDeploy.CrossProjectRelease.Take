namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Configuration
{
    public interface IOctopusServerConfiguration
    {
        string Url { get; }
        string ApiKey { get; }
    }
}