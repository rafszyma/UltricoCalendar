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

namespace UltricoCalendarApi
{
    public class Startup : UltricoApi
    {
        protected override string ApiName { get; } = "UltricoApi";
        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var actorSystem = RunActorSystem();

            ActorRefs.CalendarActor = actorSystem.ActorOf(Props.Empty, "calendar-actor");

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
