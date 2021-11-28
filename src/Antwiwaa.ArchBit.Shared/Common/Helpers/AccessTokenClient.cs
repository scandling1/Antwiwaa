using System.Net.Http;

namespace Antwiwaa.ArchBit.Shared.Common.Helpers
{
    public class AccessTokenClient
    {
        public AccessTokenClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public HttpClient Client { get; }
    }
}