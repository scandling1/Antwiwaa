using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Interfaces;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class UserDto : PayLoadObject, IHasRowNumber
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockOutEnabled { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public int RowNumber { get; set; }
    }


    public class UserList : PayLoadObject
    {
        public int pageSize { get; set; }
        public int recordsFiltered { get; set; }
        public int totalCount { get; set; }
        public List<UserDto> users { get; set; }
    }
}