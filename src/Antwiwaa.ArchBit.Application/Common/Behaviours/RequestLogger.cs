using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Antwiwaa.ArchBit.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public RequestLogger(ILogger<TRequest> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var user = _userService.GetUserId();
            var userId = user.HasValue ? user.Value : string.Empty;

            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var nameUser = _userService.GetUserName();

                userName = nameUser.HasValue ? nameUser.Value : string.Empty;
            }

            _logger.LogInformation("GeeCode.ArchBit Request: {Name} {@UserId} {@UserName} {@Request}", requestName,
                userId, userName, request);

            return Task.FromResult(Unit.Value);
        }
    }
}