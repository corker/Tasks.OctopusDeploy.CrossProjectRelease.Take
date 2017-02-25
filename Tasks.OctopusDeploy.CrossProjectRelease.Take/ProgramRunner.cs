using Tasks.OctopusDeploy.CrossProjectRelease.Take.Domain.Services;
using Tasks.OctopusDeploy.CrossProjectRelease.Take.Services;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take
{
    public class ProgramRunner
    {
        private readonly IBuildSnapshots _builder;
        private readonly IWriteSnapshots _writer;

        public ProgramRunner(IBuildSnapshots builder, IWriteSnapshots writer)
        {
            _builder = builder;
            _writer = writer;
        }

        public void Run()
        {
            var release = _builder.Build();
            _writer.Write(release);
        }
    }
}