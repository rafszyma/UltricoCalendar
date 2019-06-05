using System;
using System.IO;
using System.Reflection;
using Akka.Actor;
using Akka.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace UltricoApiCommon
{
    public abstract class UltricoApi
    {
        protected readonly UltricoApiSettings ApiSettings;

        protected readonly IHostingEnvironment Env;

        protected UltricoApi(IConfiguration args, IHostingEnvironment env, UltricoApiSettings apiSettings)
        {
            Console.Title = ApiName;
            Env = env;

            var configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();
            configuration.Bind(apiSettings);
            ApiSettings = apiSettings;

            // Logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information($"Running {ApiName}");
        }

        protected abstract string ApiName { get; }

        protected abstract void GetActor(ActorSystem system);

        protected void RunActorSystem()
        {
            var config = ConfigurationFactory.ParseString(File.ReadAllText(ApiSettings.HoconPath));
            // Akka

            GetActor(ActorSystem.Create(ApiSettings.AkkaSystemName, config));
        }

        protected void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Info {Title = ApiName, Version = "v1"});
                    var basePath = AppContext.BaseDirectory;
                    var xmlPath = Path.Combine(basePath, $"{Assembly.GetEntryAssembly().GetName().Name.ToLower()}.xml");
                    c.IncludeXmlComments(xmlPath);
                    c.DescribeAllEnumsAsStrings();
                });

            services.ConfigureSwaggerGen(options =>
            {
                // UseFullTypeNameInSchemaIds replacement for .NET Core
                options.CustomSchemaIds(x => x.FullName);
            });
        }

        protected void ConfigureControllers(IServiceCollection services)
        {
            services.AddMvcCore().AddJsonFormatters().AddApiExplorer()
                .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "dd-MM-yyyy HH:mm:ss"; });
        }

        protected void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Solomio {ApiName}"));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}