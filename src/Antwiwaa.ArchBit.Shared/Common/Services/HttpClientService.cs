using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Antwiwaa.ArchBit.Shared.Common.Services
{
    public abstract class HttpClientService
    {
        private readonly ILogger<HttpClientService> _logger;
        protected HttpClient HttpClient;

        protected HttpClientService(ILogger<HttpClientService> logger)
        {
            _logger = logger;
        }

        public async Task<HttpResponse> DeleteAsync(string url, CancellationToken cancellationToken)
        {
            var response = await HttpClient.DeleteAsync(url, cancellationToken);
            return await GetDetailsAsync(response, cancellationToken);
        }

        public async Task<HttpResponse<T>> GetAsync<T>(string getUrl, CancellationToken cancellationToken)
            where T : IPayLoadObject
        {
            var result = await HttpClient.GetAsync(getUrl, cancellationToken);

            return await GetDetailsAsync<T>(result, cancellationToken);
        }

        private async Task<HttpResponse<T>> GetDetailsAsync<T>(HttpResponseMessage responseMessage,
            CancellationToken cancellationToken) where T : IPayLoadObject
        {
            var content = await responseMessage.Content.ReadAsStringAsync(cancellationToken);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var obj = JsonConvert.DeserializeObject<ExceptionDetails>(content);

                return new HttpResponse<T>
                {
                    IsFailure = true,
                    IsSuccess = false,
                    ExceptionDetails = obj
                };
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<T>(content);
                return new HttpResponse<T>
                {
                    IsFailure = false,
                    IsSuccess = true,
                    Data = obj
                };
            }
        }

        private async Task<HttpResponse> GetDetailsAsync(HttpResponseMessage responseMessage,
            CancellationToken cancellationToken)
        {
            var content = await responseMessage.Content.ReadAsStringAsync(cancellationToken);

            if (responseMessage.IsSuccessStatusCode)
                return new HttpResponse
                {
                    IsFailure = false,
                    IsSuccess = true
                };

            _logger.LogError(content);

            var obj = JsonConvert.DeserializeObject<ExceptionDetails>(content);
            return new HttpResponse
            {
                IsFailure = true,
                IsSuccess = false,
                ExceptionDetails = obj
            };
        }

        public async Task<HttpResponse<T>> PostAsync<T>(IPayLoadObject obj, string postUrl,
            CancellationToken cancellationToken) where T : IPayLoadObject
        {
            var result = await HttpClient.PostAsync(postUrl, obj.GetHttpContent(), cancellationToken);
            return await GetDetailsAsync<T>(result, cancellationToken);
        }

        public async Task<HttpResponse> PostAsync(IPayLoadObject obj, string postUrl,
            CancellationToken cancellationToken)
        {
            var result = await HttpClient.PostAsync(postUrl, obj.GetHttpContent(), cancellationToken);
            return await GetDetailsAsync(result, cancellationToken);
        }

        public async Task<HttpResponse<T>> PutAsync<T>(IPayLoadObject obj, string postUrl,
            CancellationToken cancellationToken) where T : IPayLoadObject
        {
            var result = await HttpClient.PutAsync(postUrl, obj.GetHttpContent(), cancellationToken);
            return await GetDetailsAsync<T>(result, cancellationToken);
        }

        public async Task<HttpResponse> PutAsync(IPayLoadObject obj, string postUrl,
            CancellationToken cancellationToken)
        {
            var result = await HttpClient.PutAsync(postUrl, obj.GetHttpContent(), cancellationToken);

            return await GetDetailsAsync(result, cancellationToken);
        }
    }
}