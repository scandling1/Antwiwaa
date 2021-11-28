using System;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.ValueObjects
{
    public class AdmissionPeriod : ValueObject<AdmissionPeriod>
    {
        private AdmissionPeriod()
        {
        }


        private AdmissionPeriod(int startYear, int endYear)
        {
            StartYear = startYear;
            EndYear = endYear;
        }

        public int EndYear { get; }

        public int StartYear { get; }


        public static Result<AdmissionPeriod> Create(int startYear, int endYear, DateTime currentDate)
        {
            if (startYear < currentDate.Year)
                return Result.Failure<AdmissionPeriod>("Admission start year cannot be earlier than current year");
            if (endYear < currentDate.Year)
                return Result.Failure<AdmissionPeriod>("Admission end year cannot be earlier than current year");
            return endYear <= startYear
                ? Result.Failure<AdmissionPeriod>("Admission start year cannot be later or equal to admission end year")
                : Result.Success(new AdmissionPeriod(startYear, endYear));
        }


        protected override bool EqualsCore(AdmissionPeriod other)
        {
            return other.StartYear == StartYear && other.EndYear == EndYear;
        }


        protected override int GetHashCodeCore()
        {
            return StartYear.GetHashCode() ^ EndYear.GetHashCode();
        }


        public string GetName()
        {
            return $"{StartYear}/{EndYear} Academic Year";
        }
    }
}