using System.IO;
using Newtonsoft.Json;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Configuration;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Models;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take.Services
{
    public class SnapshotWriter : IWriteSnapshots
    {
        private static readonly JsonSerializer Serializer;
        private readonly ISnapshotWriterConfiguration _configuration;

        static SnapshotWriter()
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            Serializer = JsonSerializer.Create(settings);
        }

        public SnapshotWriter(ISnapshotWriterConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Write(Snapshot snapshot)
        {
            using (var writer = File.CreateText(_configuration.FileName))
            {
                Serializer.Serialize(writer, snapshot);
            }
        }
    }
}