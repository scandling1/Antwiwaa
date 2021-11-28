using System.Collections.Generic;
using System.Linq;
using Antwiwaa.ArchBit.Domain.Common;

namespace Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate
{
    public class Permission : IsRootAggregate<int>
    {
        private IList<Permission> _childPermissions;
        private IList<UserPermission> _users;

        protected Permission()
        {
            _users = new List<UserPermission>();
            _childPermissions = new List<Permission>();
        }

        protected Permission(string permissionName, string permissionDescription, string localizationKey,
            bool requireAdminRole, int? parentPermissionId) : this()
        {
            PermissionName = permissionName;
            PermissionDescription = permissionDescription;
            LocalizationKey = localizationKey;
            IsActive = true;
            RequireAdminRole = requireAdminRole;
            ParentPermissionId = parentPermissionId;
        }

        public IReadOnlyList<Permission> ChildPermissions
        {
            get => (IReadOnlyList<Permission>)_childPermissions;
            protected set => _childPermissions = value.ToList();
        }

        public bool IsActive { get; protected set; }
        public string LocalizationKey { get; protected set; }
        public Permission ParentPermission { get; protected set; }
        public int? ParentPermissionId { get; protected set; }
        public string PermissionDescription { get; protected set; }
        public string PermissionName { get; protected set; }
        public bool RequireAdminRole { get; protected set; }

        public IReadOnlyList<UserPermission> Users
        {
            get => (IReadOnlyList<UserPermission>)_users;
            protected set => _users = value.ToList();
        }

        public void AddChildPermission(Permission permission)
        {
            _childPermissions.Add(permission);
        }

        public void AddUser(UserPermission userPermission)
        {
            _users.Add(userPermission);
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public static Permission New(string permissionName, string permissionDescription, string localizationKey,
            bool requiredAdminRole, int? parentPermissionId = null)
        {
            return new Permission(permissionName, permissionDescription, localizationKey, requiredAdminRole,
                parentPermissionId);
        }

        public void UpdateDetails(string permissionName, string permissionDescription, string localizationKey,
            bool requireAdminRole, int? dependentPermissionId = null)
        {
            PermissionName = permissionName;
            PermissionDescription = permissionDescription;
            LocalizationKey = localizationKey;
            RequireAdminRole = requireAdminRole;
            ParentPermissionId = dependentPermissionId == 0 ? null : dependentPermissionId;
        }
    }
}