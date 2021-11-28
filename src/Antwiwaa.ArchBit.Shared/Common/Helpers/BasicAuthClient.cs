using System.Net.Http;

namespace Antwiwaa.ArchBit.Shared.Common.Helpers
{
    public class BasicAuthClient
    {
        public BasicAuthClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public HttpClient Client { get; }
    }
}