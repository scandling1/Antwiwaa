using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Antwiwaa.ArchBit.Application.Common.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch _stopwatch;
        private readonly IUserService _userService;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, IUserService userService)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
            _userService = userService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _stopwatch.Start();

            var response = await next();

            _stopwatch.Stop();

            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds <= 1000) return response;

            var requestName = typeof(TRequest).Name;

            var user = _userService.GetUserId();

            var userId = user.HasValue ? user.Value : string.Empty;

            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var nameUser = _userService.GetUserName();

                userName = nameUser.HasValue ? nameUser.Value : string.Empty;
            }

            _logger.LogWarning(
                "GeeCode.ArchBit Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);

            return response;
        }
    }
}