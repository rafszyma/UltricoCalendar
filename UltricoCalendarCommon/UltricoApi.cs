using System;
using System.IO;
using System.Reflection;
using Akka.Actor;
using Akka.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using UltricoCalendarContracts;

namespace UltricoCalendarCommon
{
    public abstract class UltricoApi
    {
        protected abstract string ApiName { get; }
        
        protected readonly IHostingEnvironment Env;
        
        protected readonly UltricoApiSettings ApiSettings = new UltricoApiSettings();

        protected UltricoApi(IConfiguration args, IHostingEnvironment env)
        {
            Console.Title = ApiName;
            Env = env;
            
            var configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();
            configuration.Bind(ApiSettings);

            // Logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .Enrich.WithProperty("Diag.Environment", ApiSettings.LogEnvironment)
                .Enrich.WithProperty("Diag.Application", ApiName)
                .CreateLogger();

            Log.Information($"Running {ApiName}");
        }
        
        protected ActorSystem RunActorSystem()
        {
            // Akka
            var configurationStr = File.ReadAllText(Path.Combine(Env.ContentRootPath, "api.hocon"));

            Log.Information(configurationStr);

            return ActorSystem.Create("solomio", ConfigurationFactory.ParseString(configurationStr));
        }
        
        protected void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Info{Title = ApiName, Version = "v1"});
                    var basePath = AppContext.BaseDirectory;
                    var xmlPath = Path.Combine(basePath, $"{Assembly.GetEntryAssembly().GetName().Name.ToLower()}.xml");
                    c.IncludeXmlComments(xmlPath);
                });

            services.ConfigureSwaggerGen(options =>
            {
                // UseFullTypeNameInSchemaIds replacement for .NET Core
                options.CustomSchemaIds(x => x.FullName);
            });
        }
        
        protected void ConfigureControllers(IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer()
                .AddJsonOptions(options => { options.SerializerSettings.DateFormatString = "dd-MM-yyyy HH:mm:ss"; });
        }
        
        protected void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"Solomio {ApiName}"));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}