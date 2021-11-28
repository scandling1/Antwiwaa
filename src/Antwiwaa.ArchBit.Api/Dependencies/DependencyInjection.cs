using Antwiwaa.ArchBit.Api.Services;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antwiwaa.ArchBit.Api.Dependencies
{
    public static class DependencyInjection
    {
        public static void AddApiLevelServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddAuthorization(options => { options.AddAuthorizationPolicies(LayerRole.Api); });

            //Setup CORS
            services.AddCors(configuration);

            //Setup Configurations
            services.AddConfigurations(configuration);

            //Setup Swagger
            services.AddSwaggerConfig(configuration);

            //Setup Authentication
            services.AddOidcAuthentication(configuration);

            services.AddHttpClientServices(configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IClaimsTransformation, PermissionToClaimsExtender>();
        }
    }
}