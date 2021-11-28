using System.Linq;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Antwiwaa.ArchBit.Shared.Common.Security
{
    public static class AuthorizationPolicies
    {
        public static AuthorizationOptions AddAuthorizationPolicies(this AuthorizationOptions policyOptions,
            LayerRole layerRole)
        {
            foreach (var pol in EnumHelper.GetValues<Policies>())
            {
                var policyClaims = pol.GetPolicyClaimValues();

                if (string.IsNullOrEmpty(policyClaims.PolicyName) || !policyClaims.RequiredRoles.Any() ||
                    !policyClaims.RequiredPermissions.Any()) continue;

                policyOptions.AddPolicy(policyClaims.PolicyName,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireAssertion(context => context.User.HasRequiredClaims(policyClaims, layerRole));
                    });
            }

            return policyOptions;
        }
    }
}