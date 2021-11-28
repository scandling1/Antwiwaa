using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Helpers;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Antwiwaa.ArchBit.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IAuthorizationService authorizationService, IHttpContextAccessor httpContext)
        {
            _authorizationService = authorizationService;
            _httpContext = httpContext;
        }

        public async Task<bool> AuthorizeAsync(string policyName)
        {
            var authorize =
                await _authorizationService.AuthorizeAsync(
                    _httpContext.HttpContext?.User ?? throw new InvalidOperationException(), policyName);
            return authorize.Succeeded;
        }

        public IList<string> GetCurrentUserRoles()
        {
            var roles = _httpContext.HttpContext?.User?.FindAll(ClaimTypes.Role);

            return (roles ?? Array.Empty<Claim>()).Select(x => x.Value).ToList();
        }

        public Maybe<string> GetUserId()
        {
            var userId = _httpContext.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return string.IsNullOrEmpty(userId) ? Maybe<string>.None : Maybe<string>.From(userId);
        }

        public Maybe<string> GetUserName()
        {
            var username = _httpContext.HttpContext?.User?.FindFirstValue("name");

            return string.IsNullOrEmpty(username) ? Maybe<string>.None : Maybe<string>.From(username);
        }

        public bool UserHasRole(Roles role)
        {
            var roles = GetCurrentUserRoles();

            return roles.Contains(role.GetAttributeStringValue());
        }

        public bool UserIsInRole(string role)
        {
            return GetCurrentUserRoles().Contains(role);
        }
    }
}