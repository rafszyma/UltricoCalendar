using System;
using System.IO;
using System.Runtime.CompilerServices;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using UltricoCalendarContracts;

namespace UltricoCalendarCommon
{
    public class UltricoService
    {
        private ActorSystem _actorSystem;

        public static IContainer Container;

        private readonly string _serviceName;

        private readonly UltricoModule _serviceModule;

        private readonly UltricoServiceSettings _serviceSettings;

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
                .Enrich.WithProperty("Diag.Environment", _serviceSettings.LogEnvironment)
                .Enrich.WithProperty("Diag.Application", _serviceName)
                .CreateLogger();
            
            Log.Information($"Using Serilog logger for {_serviceName} using {_serviceSettings.LogEnvironment} property");
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
                var config = ConfigurationFactory.ParseString(@"
akka {  
    actor {
        provider = remote
    }
    remote {
        dot-netty.tcp {
            port = 8081 #bound to a specific port
            hostname = localhost
        }
    }
}");
                _actorSystem = ActorSystem.Create("ultrico-calendar", config);
                if (Container != null)
                {
                    new AutoFacDependencyResolver(Container, _actorSystem);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to start actor system", ex);
                throw;
            }
        }
    }
}
