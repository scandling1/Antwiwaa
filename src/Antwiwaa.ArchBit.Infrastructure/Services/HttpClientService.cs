using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Shared.Interfaces;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;

namespace Antwiwaa.ArchBit.Infrastructure.Services
{
    public abstract class HttpClientService
    {
        protected HttpClient HttpClient;

        public async Task<Result<T>> GetAsync<T>(string getUrl, CancellationToken cancellationToken)
            where T : IPayLoadObject
        {
            var result = await HttpClient.GetAsync(getUrl, cancellationToken);

            return await GetDetailsAsync<T>(result, cancellationToken);
        }

        public async Task<Result<T>> PostAsync<T>(IPayLoadObject obj, string postUrl,
            CancellationToken cancellationToken) where T : IPayLoadObject
        {
            var result = await HttpClient.PostAsync(postUrl, obj.GetHttpContent(), cancellationToken);

            return await GetDetailsAsync<T>(result, cancellationToken);
        }

        private async Task<Result<T>> GetDetailsAsync<T>(HttpResponseMessage responseMessage,
            CancellationToken cancellationToken) where T : IPayLoadObject
        {
            var content = await responseMessage.Content.ReadAsStringAsync(cancellationToken);

            if (!responseMessage.IsSuccessStatusCode)
            {
                return Result.Failure<T>(responseMessage.ReasonPhrase);
            }

            var obj = JsonConvert.DeserializeObject<T>(content);

            return Result.Success(obj);
        }
    }
}