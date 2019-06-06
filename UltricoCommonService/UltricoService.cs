using System;
using System.IO;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using UltricoCalendarCommon.Settings;

namespace UltricoCalendarCommon
{
    public class UltricoService
    {
        public static IContainer Container;

        private readonly UltricoModule _serviceModule;

        private readonly string _serviceName;

        private readonly UltricoServiceSettings _serviceSettings;
        private ActorSystem _actorSystem;

        public UltricoService(string serviceName, UltricoModule serviceModule, UltricoServiceSettings serviceSettings)
        {
            _serviceName = serviceName;
            _serviceModule = serviceModule;
            _serviceSettings = serviceSettings;
        }

        public void RegisterService()
        {
            SetupServiceConfiguration();
            SetupLogger();
            SetupIoC();
            MigrateDb();
            CreateActorSystem();
            _serviceModule.CreateActors(_actorSystem);
            _serviceModule.SystemShutdown(_actorSystem);
        }

        private void SetupServiceConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            configuration.Bind(_serviceSettings);
        }

        private void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information($"Using Serilog logger for {_serviceName}");
        }

        private void SetupIoC()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(_serviceModule);
            containerBuilder.RegisterInstance(_serviceSettings);
            containerBuilder.RegisterInstance(_serviceSettings).AsSelf();
            Container = containerBuilder.Build();
            UltricoModule.IoCContainer = Container;
        }

        private void MigrateDb()
        {
            _serviceModule.MigrateDatabase(Container);
        }

        private void CreateActorSystem()
        {
            try
            {
                var config = ConfigurationFactory.ParseString(File.ReadAllText("service.hocon"));
                _actorSystem = ActorSystem.Create(_serviceSettings.AkkaSystemName, config);
                if (Container != null) new AutoFacDependencyResolver(Container, _actorSystem);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to start actor system", ex);
                throw;
            }
        }
    }
}