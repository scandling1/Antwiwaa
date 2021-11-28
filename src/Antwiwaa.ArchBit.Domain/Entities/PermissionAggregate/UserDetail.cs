using Antwiwaa.ArchBit.Domain.Common;

namespace Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate
{
    public class UserDetail : IsRootAggregate<int>
    {
        public UserDetail()
        {
        }


        public UserDetail(string userId, string email, string lastName, string otherNames, string phone,
            string department)
        {
            UserId = userId;
            Email = email;
            LastName = lastName;
            OtherNames = otherNames;
            Phone = phone;
            Department = department;
        }

        public string UserId { get; protected set; }
        public string Email { get; protected set; }
        public string LastName { get; protected set; }
        public string OtherNames { get; protected set; }
        public string Phone { get; protected set; }
        public string Department { get; protected set; }

        public static UserDetail Create(string userId, string email, string lastName, string otherNames, string phone,
            string department)
        {
            return new UserDetail(userId, email, lastName, otherNames, phone, department);
        }

        public void Update(string lastName, string otherNames, string phone, string department)
        {
            LastName = lastName;
            OtherNames = otherNames;
            Phone = phone;
            Department = department;
        }
    }
}