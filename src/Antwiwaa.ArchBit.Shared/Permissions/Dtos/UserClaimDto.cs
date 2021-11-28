using System.Collections.Generic;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Interfaces;

namespace Antwiwaa.ArchBit.Shared.Permissions.Dtos
{
    public class UserClaimDto : IHasRowNumber
    {
        public int claimId { get; set; }
        public string userId { get; set; }
        public string claimType { get; set; }
        public string claimValue { get; set; }
        public int RowNumber { get; set; }
    }


    public class ClaimList : PayLoadObject
    {
        public List<UserClaimDto> claims { get; set; }
        public int totalCount { get; set; }
        public int pageSize { get; set; }
    }
}