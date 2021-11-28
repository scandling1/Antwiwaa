using System;
using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Shared.Permissions
{
    public class UserDetailDto : PayLoadObject, ICloneable
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class UserDetailListDto : PayLoadObject
    {
        public UserDetailListDto()
        {
            UserDetailDtos = new List<UserDetailDto>();
        }

        public IEnumerable<UserDetailDto> UserDetailDtos { get; set; }
    }
}