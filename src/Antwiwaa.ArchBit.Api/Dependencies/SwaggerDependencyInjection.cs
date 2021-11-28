using System;
using System.Collections.Generic;
using Antwiwaa.ArchBit.Api.Filter;
using Antwiwaa.ArchBit.Application.Common.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Antwiwaa.ArchBit.Api.Dependencies
{
    public static class SwaggerDependencyInjection
    {
        public static void AddSwaggerConfig(this IApplicationBuilder app, IConfiguration configuration)
        {
            var adminConfig = configuration.GetSection(nameof(AdminConfig)).Get<AdminConfig>();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{adminConfig.ApiBaseUrl}/swagger/v1/swagger.json", adminConfig.ApiName);
                c.OAuthClientId(adminConfig.IdentityConfig.OidcSwaggerUiClientId);
                c.OAuthAppName(adminConfig.ApiName);
                c.OAuthUsePkce();
            });
        }

        public static void AddSwaggerConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var adminConfig = configuration.GetSection(nameof(AdminConfig)).Get<AdminConfig>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(adminConfig.ApiVersion,
                    new OpenApiInfo
                    {
                        Title = adminConfig.ApiName,
                        Version = adminConfig.ApiVersion,
                        Description = adminConfig.ApiDescription,
                        Contact = new OpenApiContact
                        {
                            Name = adminConfig.ApiContact.Name,
                            Email = adminConfig.ApiContact.Email,
                            Url = new Uri(adminConfig.ApiContact.Url)
                        },
                        License = new OpenApiLicense
                        {
                            Name = adminConfig.ApiLicense.Name,
                            Url = new Uri(adminConfig.ApiLicense.Url)
                        }
                    });

                /*options.AddSecurityDefinition("oauth2",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{adminConfig.IdentityConfig.Issuer}/connect/authorize"),
                                TokenUrl = new Uri($"{adminConfig.IdentityConfig.Issuer}/connect/token"),
                                Scopes = new Dictionary<string, string>
                                    { { adminConfig.IdentityConfig.OidcApiName, adminConfig.ApiName } }
                            }
                        }
                    });*/

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }
    }
}