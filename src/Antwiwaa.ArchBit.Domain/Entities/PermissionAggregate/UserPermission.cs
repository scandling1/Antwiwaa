using Antwiwaa.ArchBit.Domain.Common;

namespace Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate
{
    public class UserPermission : Entity<int>
    {
        public UserPermission(string userId, int permissionId, string username)
        {
            UserId = userId;
            UserName = username;
            PermissionId = permissionId;
        }

        protected UserPermission()
        {
        }

        public Permission Permission { get; protected set; }
        public int PermissionId { get; protected set; }
        public string UserId { get; protected set; }
        public string UserName { get; protected set; }

        public static UserPermission CreateUserPermission(string userId, int permissionId, string username)
        {
            return new UserPermission(userId, permissionId, username);
        }
    }
}