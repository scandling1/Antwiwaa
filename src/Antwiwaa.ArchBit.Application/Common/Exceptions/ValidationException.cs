using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Results;

namespace Antwiwaa.ArchBit.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Errors.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Errors { get; }

        public override string ToString()
        {
            var errorString = new StringBuilder();

            foreach (var (key, value) in Errors)
            foreach (var s in value)
                errorString.AppendLine($"{s}");

            return errorString.ToString();
        }
    }
}