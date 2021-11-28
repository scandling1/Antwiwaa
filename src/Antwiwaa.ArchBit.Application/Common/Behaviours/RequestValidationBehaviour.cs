using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ValidationException = Antwiwaa.ArchBit.Application.Common.Exceptions.ValidationException;

namespace Antwiwaa.ArchBit.Application.Common.Behaviours
{
    public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any()) return next();

            var context = new ValidationContext<TRequest>(request);

            var failures = _validators.Select(v => v.Validate(context)).SelectMany(result => result.Errors)
                .Where(f => f != null).ToList();

            return failures.Count != 0 ? throw new ValidationException(failures) : next();
        }
    }
}