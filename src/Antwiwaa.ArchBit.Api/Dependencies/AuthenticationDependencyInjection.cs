using System;
using System.Text;
using Antwiwaa.ArchBit.Application.Common.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Antwiwaa.ArchBit.Api.Dependencies
{
    public static class AuthenticationDependencyInjection
    {
        public static void AddOidcAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var adminConfig = configuration.GetSection(nameof(AdminConfig)).Get<AdminConfig>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = adminConfig.IdentityConfig.RequireHttpsMetadata;
                options.Authority = adminConfig.IdentityConfig.Issuer;
                options.Audience = adminConfig.IdentityConfig.OidcApiName;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidIssuer = adminConfig.IdentityConfig.Issuer,
                    ValidAudience = adminConfig.IdentityConfig.Audience,
                    ValidateIssuer = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(adminConfig.IdentityConfig.Secret)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });
        }
    }
}