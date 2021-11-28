using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Models;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ClaimTypes = Antwiwaa.ArchBit.Shared.Common.Enums.ClaimTypes;

namespace Antwiwaa.ArchBit.Shared.Common.Helpers
{
    public static class UserClaimsHelper
    {
        public static Maybe<string> GetClaim(this ClaimsPrincipal claims, ClaimTypes claimTypes)
        {
            var claimValue = claims.FindFirst(claimTypes.GetAttributeStringValue())?.Value;

            return string.IsNullOrEmpty(claimValue) ? Maybe<string>.None : claimValue;
        }

        public static Maybe<IEnumerable<string>> GetCubePermissionClaims(this ClaimsPrincipal claims)
        {
            var permissions = claims.GetClaim(ClaimTypes.Permissions);

            return permissions.HasNoValue ? Maybe<IEnumerable<string>>.None : permissions.Value.Split(",").ToList();
        }

        public static IEnumerable<string> GetRoleClaims(this ClaimsPrincipal claims)
        {
            var roles = claims.FindAll(System.Security.Claims.ClaimTypes.Role); //claims.GetClaim(ClaimTypes.CubeRoles);

            return (roles ?? Array.Empty<Claim>()).Select(x => x.Value).ToList();
        }

        public static IList<string> GetRolesClientLayer(this ClaimsPrincipal claimsPrincipal)
        {
            var roles = claimsPrincipal.FindFirst(x => x.Type.Equals(ClaimTypes.Roles.GetAttributeStringValue()))
                ?.Value;

            try
            {
                var pareseRoles = JToken.Parse(roles ?? string.Empty);
            }
            catch
            {
                return new List<string> { roles };
            }

            var roleList = JsonConvert.DeserializeObject<List<string>>(roles ?? string.Empty);

            return roleList;
        }

        public static bool HasRequiredClaims(this ClaimsPrincipal claims, PolicyClaimValues claimValues,
            LayerRole layerRole)
        {
            var roles = layerRole == LayerRole.Api ? claims.GetRoleClaims() : claims.GetRolesClientLayer();

            var permissions = claims.GetCubePermissionClaims();

            if (permissions.HasNoValue) return false;

            var requiredRoles = claimValues.RequiredRoles.Select(x => x.GetAttributeStringValue());

            var requiredPermissions = claimValues.RequiredPermissions
                .Select(x => x.GetPermissionClaimValues().PermissioName).ToList();

            return roles.Any(x => requiredRoles.Contains(x)) &&
                   permissions.Value.Any(x => requiredPermissions.Contains(x));
        }
    }
}