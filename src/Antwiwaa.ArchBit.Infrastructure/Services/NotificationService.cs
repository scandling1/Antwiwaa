using System;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Domain.Entities.General;
using Antwiwaa.ArchBit.Domain.Enums;

namespace Antwiwaa.ArchBit.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ISmsService _smsService;

        public NotificationService(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public Task<bool> SendNotification(Notification notification, CancellationToken cancellationToken)
        {
            switch (notification.NotificationType)
            {
                case NotificationType.Sms:
                    var result = SendSms(notification, cancellationToken);
                    return result;
                case NotificationType.Whatsapp:
                    return Task.FromResult(false);
                case NotificationType.Email:
                    return Task.FromResult(false);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Task<bool> SendSms(Notification notification, CancellationToken cancellationToken)
        {
            var response = _smsService.SendSmsAsync(notification, cancellationToken);

            return response;
        }
    }
}