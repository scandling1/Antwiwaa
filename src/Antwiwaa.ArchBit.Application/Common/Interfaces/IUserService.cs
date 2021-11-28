using System.Collections.Generic;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<bool> AuthorizeAsync(string policyName);
        IList<string> GetCurrentUserRoles();
        Maybe<string> GetUserId();
        Maybe<string> GetUserName();
        bool UserHasRole(Roles role);
        bool UserIsInRole(string role);
    }
}