using System.Net.Http;

namespace Antwiwaa.ArchBit.Infrastructure.Services
{
    public class HubtelHttpClient
    {
        public HubtelHttpClient(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}