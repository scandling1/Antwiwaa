using System;
using Antwiwaa.ArchBit.Api.Dependencies;
using Antwiwaa.ArchBit.Api.Filter;
using Antwiwaa.ArchBit.Application;
using Antwiwaa.ArchBit.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Antwiwaa.ArchBit.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            /*Log.Logger = new LoggerConfiguration().WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(configuration["Logging:Credentials:Uri"]))
                    {
                        ModifyConnectionSettings = x => x.BasicAuthentication(
                            configuration["Logging:Credentials:Username"],
                            configuration["Logging:Credentials:Password"]),
                        IndexFormat = "ArchBit-Logs-{0:yyyy.MM.dd}",
                        InlineFields = false
                    }).Enrich.WithProperty("Environment", environment).Enrich.FromLogContext().WriteTo.Console()
                .CreateLogger();*/
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //SetupSwagger
                app.AddSwaggerConfig(Configuration);
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Dependencies from API Level
            services.AddApiLevelServices(Configuration);

            //Dependencies from Application Layer
            services.AddApplication();

            //Dependencies from Infrastructure Layer
            services.AddConfigFromInfraLayer(Configuration);

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());
        }
    }
}