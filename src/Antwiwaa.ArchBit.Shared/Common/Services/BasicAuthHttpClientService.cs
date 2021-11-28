using Antwiwaa.ArchBit.Shared.Common.Helpers;
using Microsoft.Extensions.Logging;

namespace Antwiwaa.ArchBit.Shared.Common.Services
{
    public class BasicAuthHttpClientService : HttpClientService
    {
        public BasicAuthHttpClientService(ILogger<BasicAuthHttpClientService> logger, BasicAuthClient accessTokenClient)
            : base(logger)
        {
            HttpClient = accessTokenClient.Client;
        }
    }
}