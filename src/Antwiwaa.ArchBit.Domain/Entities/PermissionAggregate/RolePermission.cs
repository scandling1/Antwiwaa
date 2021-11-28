using Antwiwaa.ArchBit.Domain.Common;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate
{
    public class RolePermission : IsRootAggregate<int>
    {
        private RolePermission(int roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        protected RolePermission()
        {
        }

        public int RoleId { get; protected set; }
        public Role Role { get; protected set; }
        public int PermissionId { get; protected set; }
        public Permission Permission { get; protected set; }

        public static Result<RolePermission> Create(int roleId, int permissionId)
        {
            return Result.Success(new RolePermission(roleId, permissionId));
        }

        public void Update(int roleId, int permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }

        public void Delete(int id)
        {
            Id = id;
        }
    }
}