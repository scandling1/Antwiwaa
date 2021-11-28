using Antwiwaa.ArchBit.Application.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antwiwaa.ArchBit.Api.Dependencies
{
    public static class CorsConfigurationDependencyInjection
    {
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var adminConfig = configuration.GetSection(nameof(AdminConfig)).Get<AdminConfig>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    if (adminConfig.CorsAllowAnyOrigin)
                        builder.AllowAnyOrigin();
                    else
                        builder.WithOrigins(adminConfig.CorsAllowOrigins);

                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
        }
    }
}