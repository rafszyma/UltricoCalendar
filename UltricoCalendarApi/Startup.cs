using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UltricoCalendarCommon;
using UltricoCalendarContracts;

namespace UltricoCalendarApi
{
    public class Startup : UltricoApi
    {
        protected override string ApiName { get; } = "UltricoApi";

        private const int ResolveOneTimeout = 1; 
        protected override void GetActor(ActorSystem system)
        {
            ServiceActorRefs.CalendarServiceActor = system.ActorSelection("akka.tcp://ultrico-calendar@localhost:8081/user/calendar-actor").ResolveOne(TimeSpan.FromSeconds(ResolveOneTimeout)).Result;
            ServiceActorRefs.CalendarQueryActor = system.ActorSelection("akka.tcp://ultrico-calendar@localhost:8081/user/query-actor").ResolveOne(TimeSpan.FromSeconds(ResolveOneTimeout)).Result;
        }

        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
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
