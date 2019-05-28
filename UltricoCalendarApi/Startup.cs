using System;
using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UltricoCalendarCommon;

namespace UltricoCalendarApi
{
    public class Startup : UltricoApi
    {
        private const int ResolveOneTimeout = 1;

        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
        }

        protected override string ApiName { get; } = "UltricoApi";

        protected override void GetActor(ActorSystem system)
        {
            ServiceActorRefs.CalendarServiceActor = system
                .ActorSelection("akka.tcp://ultrico-calendar@localhost:8081/user/calendar-actor")
                .ResolveOne(TimeSpan.FromSeconds(ResolveOneTimeout)).Result;
            ServiceActorRefs.CalendarQueryActor = system
                .ActorSelection("akka.tcp://ultrico-calendar@localhost:8081/user/query-actor")
                .ResolveOne(TimeSpan.FromSeconds(ResolveOneTimeout)).Result;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RunActorSystem();
            services.AddTransient(provider => ApiSettings);

            ConfigureSwagger(services);
            ConfigureControllers(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Configure(app);
        }
    }
}