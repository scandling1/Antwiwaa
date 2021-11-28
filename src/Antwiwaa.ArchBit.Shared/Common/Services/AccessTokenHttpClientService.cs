using Antwiwaa.ArchBit.Shared.Common.Helpers;
using Microsoft.Extensions.Logging;

namespace Antwiwaa.ArchBit.Shared.Common.Services
{
    public class AccessTokenHttpClientService : HttpClientService
    {
        public AccessTokenHttpClientService(ILogger<AccessTokenHttpClientService> logger,
            AccessTokenClient accessTokenClient) : base(logger)
        {
            HttpClient = accessTokenClient.Client;
        }
    }
}