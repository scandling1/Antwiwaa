using Antwiwaa.ArchBit.Domain.Common;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate
{
    public class UserRole : IsRootAggregate<int>
    {
        private UserRole(string userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public UserRole()
        {
        }

        public string UserId { get; protected set; }
        public int RoleId { get; protected set; }
        public Role Role { get; protected set; }

        public static Result<UserRole> Create(string userId, int roleId)
        {
            return Result.Success(new UserRole(userId, roleId));
        }

        public void Update(string userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public void Delete(int id)
        {
            Id = id;
        }
    }
}