using System.Collections.Generic;
using Antwiwaa.ArchBit.Domain.Common;
using Antwiwaa.ArchBit.Domain.Enums;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate
{
    public class Role : IsRootAggregate<int>
    {
        private Role(string name, RoleStatus roleStatus)
        {
            Name = name;
            RoleStatus = roleStatus;
        }

        protected Role()
        {
        }

        public string Name { get; protected set; }
        public RoleStatus RoleStatus { get; protected set; }

        public IList<RolePermission> RolePermissions { get; set; }

        public static Result<Role> Create(string name, RoleStatus roleStatus = RoleStatus.Active)
        {
            return Result.Success(new Role(name, roleStatus));
        }

        public void Update(string name, RoleStatus roleStatus)
        {
            Name = name;
            RoleStatus = roleStatus;
        }

        public void Delete(int id)
        {
            Id = id;
        }
    }
}