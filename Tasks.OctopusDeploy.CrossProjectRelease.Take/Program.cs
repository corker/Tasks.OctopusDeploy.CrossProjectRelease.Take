using System;
using System.IO;
using Autofac;
using log4net;
using log4net.Config;

namespace Tasks.OctopusDeploy.CrossProjectRelease.Take
{
    internal class Program
    {
        private const string Log4NetConfigFileName = "log4net.config";

        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private static void Main(string[] args)
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Log4NetConfigFileName));
            Log.Info("Program started.");
            try
            {
                using (var container = ProgramContainerFactory.Create())
                {
                    container.Resolve<ProgramRunner>().Run();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception thrown.", ex);
                Environment.Exit(-1);
            }
            Log.Info("Program finished.");
        }
    }
}