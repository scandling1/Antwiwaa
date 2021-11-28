using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Permissions.Queries;
using Antwiwaa.ArchBit.Shared.Common.Helpers;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ClaimTypes = Antwiwaa.ArchBit.Shared.Common.Enums.ClaimTypes;

namespace Antwiwaa.ArchBit.Api.Services
{
    public class PermissionToClaimsExtender : IClaimsTransformation
    {
        private readonly IMediator _mediator;

        public PermissionToClaimsExtender(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is { IsAuthenticated: false }) return await Task.FromResult(principal);

            var userPermClaims = principal.GetClaim(ClaimTypes.Permissions);

            if (userPermClaims.HasValue) return principal;

            var userId = principal.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);

            var (_, isFailure, value) = await _mediator.Send(new GetUserPermissionsQuery { UserId = userId });

            if (isFailure) return principal;

            if (principal.HasClaim(x => x.Type == ClaimTypes.Permissions.GetAttributeStringValue()))
                return await Task.FromResult(principal);

            var claims = new List<Claim> { new(ClaimTypes.Permissions.GetAttributeStringValue(), value) };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            principal.AddIdentity(identity);

            return principal;
        }
    }
}