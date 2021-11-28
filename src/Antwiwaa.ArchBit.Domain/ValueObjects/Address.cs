using System;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public Address()
        {
        }

        private Address(string addressLine1, string addressLine2, string country, string state, string city)
        {
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            Country = country;
            State = state;
            AddressCity = city;
        }

        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string Country { get; }
        public string State { get; }
        public string AddressCity { get; }

        public override string ToString()
        {
            return $"{AddressLine1} {AddressLine2} {Country} {State} {AddressCity}";
        }

        public static Result<Address> Create(string addressLine1, string addressLine2, string country, string state,
            string city)
        {
            if (string.IsNullOrEmpty(addressLine1)) return Result.Failure<Address>("Address Line 1 is required");

            if (string.IsNullOrEmpty(country)) return Result.Failure<Address>("Country is required");

            if (string.IsNullOrEmpty(city)) return Result.Failure<Address>("City is required");

            var newAddress = new Address(addressLine1, addressLine2, country, state, city);

            return Result.Success(newAddress);
        }

        protected override bool EqualsCore(Address other)
        {
            return other.AddressLine1.Equals(AddressLine1, StringComparison.OrdinalIgnoreCase) &&
                   other.AddressLine2.Equals(AddressLine2, StringComparison.OrdinalIgnoreCase) &&
                   other.Country.Equals(Country, StringComparison.OrdinalIgnoreCase) &&
                   other.State.Equals(State, StringComparison.OrdinalIgnoreCase) &&
                   other.AddressCity.Equals(AddressCity, StringComparison.OrdinalIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return AddressLine1.GetHashCode() ^ AddressLine2.GetHashCode() ^ Country.GetHashCode() ^
                   State.GetHashCode() ^ AddressCity.GetHashCode();
        }
    }
}