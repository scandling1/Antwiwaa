using System;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        protected Name()
        {
        }

        private Name(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }

        public override string ToString()
        {
            return $"{FirstName} {MiddleName} {LastName.ToUpper()}".Trim();
        }

        public static Result<Name> Create(string firstname, string middlename, string lastname)
        {
            if (string.IsNullOrEmpty(firstname)) return Result.Failure<Name>("first name is required");

            if (string.IsNullOrEmpty(lastname)) return Result.Failure<Name>("last name is required");

            var newStudentName = new Name(firstname, middlename, lastname);

            return Result.Success(newStudentName);
        }

        protected override bool EqualsCore(Name other)
        {
            return other.FirstName.Equals(FirstName, StringComparison.OrdinalIgnoreCase) &&
                   other.MiddleName.Equals(MiddleName, StringComparison.OrdinalIgnoreCase) &&
                   other.LastName.Equals(LastName, StringComparison.OrdinalIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return FirstName.GetHashCode() ^ MiddleName.GetHashCode() ^ LastName.GetHashCode();
        }
    }
}