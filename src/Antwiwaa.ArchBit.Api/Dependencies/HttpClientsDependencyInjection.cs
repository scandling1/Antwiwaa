using System;
using System.Net.Http.Headers;
using System.Text;
using Antwiwaa.ArchBit.Application.Common.Configurations;
using Antwiwaa.ArchBit.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antwiwaa.ArchBit.Api.Dependencies
{
    public static class HttpClientsDependencyInjection
    {
        public static void AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            var adminConfig = configuration.GetSection(nameof(AdminConfig)).Get<AdminConfig>();

            services.AddHttpClient<HubtelHttpClient>(client =>
            {
                client.BaseAddress = new Uri(adminConfig.HubtelConfig.BaseUrl);
                var byteArray =
                    Encoding.ASCII.GetBytes(
                        $"{adminConfig.HubtelConfig.ClientId}:{adminConfig.HubtelConfig.ClientSecret}");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            });
        }
    }
}