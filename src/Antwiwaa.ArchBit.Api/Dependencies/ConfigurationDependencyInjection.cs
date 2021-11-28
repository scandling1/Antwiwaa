using Antwiwaa.ArchBit.Application.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antwiwaa.ArchBit.Api.Dependencies
{
    public static class ConfigurationDependencyInjection
    {
        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var adminConfiguration = configuration.GetSection(nameof(AdminConfig)).Get<AdminConfig>();
            services.AddSingleton(adminConfiguration);
        }
    }
}