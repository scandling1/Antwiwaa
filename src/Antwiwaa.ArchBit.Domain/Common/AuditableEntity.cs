using System;
using System.ComponentModel.DataAnnotations;

namespace Antwiwaa.ArchBit.Domain.Common
{
    public class AuditableEntity
    {
        public DateTime Created { get; set; }

        [Required] [MaxLength(50)] public string CreatedBy { get; set; }

        public DateTime LastModified { get; set; }

        [Required] [MaxLength(50)] public string LastModifiedBy { get; set; }
    }
}