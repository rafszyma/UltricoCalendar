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
    public abstract class UltricoService
    {
        private ActorSystem _actorSystem;

        private IContainer _container;

        private readonly string _serviceName;

        private readonly UltricoModule _serviceModule;

        private readonly UltricoServiceSettings _serviceSettings;
        
        private string _akkaConfigurationStr;

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
        }

        private void SetupServiceConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();
            
            configuration.Bind(_serviceSettings);
            
            _akkaConfigurationStr = File.ReadAllText("service.hocon");
        }

        private void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .Enrich.WithProperty("Diag.Environment", _serviceSettings.LogEnvironment)
                .Enrich.WithProperty("Diag.Application", _serviceName)
                .CreateLogger();
            
            Log.Information($"Using Serilog logger for {_serviceName} using {_serviceSettings.LogEnvironment} property on system with LogEnvironment={Environment.GetEnvironmentVariable("LogEnvironment")} ");
        }

        private void SetupIoC()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(_serviceModule);
            containerBuilder.RegisterInstance(_serviceSettings); // Intended
            containerBuilder.RegisterInstance(_serviceSettings).AsSelf();
            _container = containerBuilder.Build();
        }

        private void MigrateDb()
        {
            _serviceModule.MigrateDatabase(_container);
        }

        private void CreateActorSystem()
        {
            try
            {
                _actorSystem = ActorSystem.Create("solomio", ConfigurationFactory.ParseString(_akkaConfigurationStr));
                if (_container != null)
                {
                    var autoFacDependencyResolver = new AutoFacDependencyResolver(_container, _actorSystem);
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
