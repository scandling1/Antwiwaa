using System;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.ValueObjects
{
    public class FileAttachment : ValueObject<FileAttachment>
    {
        private FileAttachment(string productCode, string processCode, int year, int month, int day, string uniqueCode,
            string extension)
        {
            ProductCode = productCode;
            ProcessCode = processCode;
            Year = year;
            Month = month;
            Day = day;
            UniqueCode = uniqueCode;
            Extension = extension;
        }

        protected FileAttachment()
        {
        }

        public string ProductCode { get; }
        public string Extension { get; }
        public string ProcessCode { get; }
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public string UniqueCode { get; }

        public override string ToString()
        {
            return $"{ProductCode}{Year}{Month}{Day}{ProcessCode}{UniqueCode}";
        }

        public string GetPath()
        {
            return $"{ProductCode}\\{Year}\\{Month}\\{Day}\\{ProcessCode}\\{UniqueCode}";
        }

        public string GetFullPathWithNoExt()
        {
            return $"{ProductCode}\\{Year}\\{Month}\\{Day}\\{ProcessCode}\\{UniqueCode}\\{UniqueCode}";
        }

        public string GetPathWithExtension()
        {
            return $"{ProductCode}\\{Year}\\{Month}\\{Day}\\{ProcessCode}\\{UniqueCode}{Extension}";
        }

        public string GetFullTxtPath()
        {
            return $"{ProductCode}\\{Year}\\{Month}\\{Day}\\{ProcessCode}\\{UniqueCode}\\{UniqueCode}.txt";
        }

        public static Result<FileAttachment> Create(string productCode, string processCode, string extension)
        {
            if (string.IsNullOrEmpty(productCode))
                return Result.Failure<FileAttachment>("product code cannot be empty");

            if (string.IsNullOrEmpty(processCode))
                return Result.Failure<FileAttachment>("process code cannot be empty");

            var year = DateTime.UtcNow.Year;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Day;
            var uniqueCode = Guid.NewGuid().ToString();

            return Result.Success(new FileAttachment(productCode, processCode, year, month, day, uniqueCode,
                extension));
        }

        protected override bool EqualsCore(FileAttachment other)
        {
            return other.Year == Year && other.Month == Month && other.Day == Day && other.ProcessCode == ProcessCode &&
                   other.ProductCode == ProductCode && other.UniqueCode == UniqueCode;
        }

        protected override int GetHashCodeCore()
        {
            return ProductCode.GetHashCode() ^ ProcessCode.GetHashCode() ^ UniqueCode.GetHashCode();
        }
    }
}