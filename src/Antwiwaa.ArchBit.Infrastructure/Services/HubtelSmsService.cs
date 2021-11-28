using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Domain.Entities.General;
using Antwiwaa.ArchBit.Shared.Common.Models;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Infrastructure.Services
{
    public class HubtelSmsService : HttpClientService, ISmsService
    {
        public HubtelSmsService(HubtelHttpClient hubtelHttpClient)
        {
            HttpClient = hubtelHttpClient.Client;
        }

        public async Task<bool> SendSmsAsync(Notification notification, CancellationToken cancellationToken)
        {
            var (_, isFailure, value) = await PostAsync<SmsResponse>(new SmsRequest
            {
                Type = "0",
                From = "KTU",
                To = notification.RecipientAddress,
                Content = notification.Message,
                RegisteredDelivery = true
            }, "", cancellationToken);
            if (isFailure) return false;
            return value.Status == 0;
        }
    }

    public class SmsRequest : PayLoadObject
    {
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public bool RegisteredDelivery { get; set; }
    }

    public class SmsResponse : PayLoadObject
    {
        public int Status { get; set; }
    }
}