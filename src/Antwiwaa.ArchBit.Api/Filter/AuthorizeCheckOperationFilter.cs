﻿using System.Collections.Generic;
using System.Linq;
using Antwiwaa.ArchBit.Application.Common.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Antwiwaa.ArchBit.Api.Filter
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly AdminConfig _adminApiConfiguration;

        public AuthorizeCheckOperationFilter(AdminConfig adminApiConfiguration)
        {
            _adminApiConfiguration = adminApiConfiguration;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType != null &&
                               (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                                   .Any() || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>()
                                   .Any());

            if (!hasAuthorize) return;

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    }] = new[] { _adminApiConfiguration.IdentityConfig.OidcApiName }
                }
            };
        }
    }
}