using System;
using Antwiwaa.ArchBit.Shared.Common.Enums;

namespace Antwiwaa.ArchBit.Application.Common.Security
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizePolicyAttribute : Attribute
    {
        /// <summary>
        ///     Gets or sets the policy name that determines access to the resource.
        /// </summary>
        public Policies Policy { get; set; }

        /// <summary>
        ///     Gets or sets a comma delimited list of roles that are allowed to access the resource.
        /// </summary>
        public Roles Roles { get; set; }
    }
}