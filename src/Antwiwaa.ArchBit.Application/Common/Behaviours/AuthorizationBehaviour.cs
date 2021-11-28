using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Exceptions;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Application.Common.Security;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Helpers;
using MediatR;

namespace Antwiwaa.ArchBit.Application.Common.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUserService _userService;

        public AuthorizationBehaviour(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizePolicyAttribute>().ToList();

            if (!authorizeAttributes.Any()) return await next();

            // Must be authenticated user
            if (_userService.GetUserId() == null) throw new UnauthorizedAccessException();

            // Role-based authorization
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => a.Roles != Roles.None).ToList();

            if (authorizeAttributesWithRoles.Any())
                if (authorizeAttributesWithRoles.Select(a => a.Roles)
                    .Select(role => _userService.UserIsInRole(role.GetAttributeStringValue().Trim()))
                    .Any(isInRole => isInRole))
                    return await next();

            // Policy-based authorization
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => a.Policy != Policies.None).ToList();

            if (!authorizeAttributesWithPolicies.Any()) return await next();
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _userService.AuthorizeAsync(policy.GetPolicyClaimValues().PolicyName);

                    if (!authorized) throw new ForbiddenAccessException();
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}