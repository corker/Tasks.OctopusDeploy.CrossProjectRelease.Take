using System;
using System.Configuration;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Configuration;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take
{
    public class ProgramConfiguration :
        IOctopusServerConfiguration,
        ISnapshotEnvironmentConfiguration,
        ISnapshotWriterConfiguration
    {
        string IOctopusServerConfiguration.Url
            => GetValueFromAppSettings("OctopusDeploy.Url");

        string IOctopusServerConfiguration.ApiKey
            => GetValueFromAppSettings("OctopusDeploy.ApiKey");

        string ISnapshotEnvironmentConfiguration.Name
            => GetValueFromAppSettings("Tasks.OctopusDeploy.CrossProjectRelease.Take.Environment");

        public string FileName
            => GetValueFromAppSettings("Tasks.OctopusDeploy.CrossProjectRelease.FileName");

        private static string GetValueFromAppSettings(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                string message = $"{key} is not defined in AppSettings section of the application configuration file.";
                throw new InvalidOperationException(message);
            }
            return value;
        }
    }
}