using System;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.ValueObjects
{
    public class TelephoneNumber : ValueObject<TelephoneNumber>
    {
        protected TelephoneNumber()
        {
        }

        private TelephoneNumber(string countryCode, string number)
        {
            CountryCode = countryCode;
            Number = number;
        }

        public string CountryCode { get; }
        public string Number { get; }

        public override string ToString()
        {
            return $"{CountryCode}{Number}";
        }

        public static Result<TelephoneNumber> Create(string countryCode, string number)
        {
            if (string.IsNullOrEmpty(countryCode)) return Result.Failure<TelephoneNumber>("country code is required");

            return string.IsNullOrEmpty(number)
                ? Result.Failure<TelephoneNumber>("Telephone number is required")
                : Result.Success(new TelephoneNumber(countryCode, number));
        }

        protected override bool EqualsCore(TelephoneNumber other)
        {
            return other.CountryCode.Equals(CountryCode, StringComparison.OrdinalIgnoreCase) &&
                   other.Number.Equals(Number, StringComparison.OrdinalIgnoreCase);
        }

        protected override int GetHashCodeCore()
        {
            return CountryCode.GetHashCode() ^ Number.GetHashCode();
        }
    }
}