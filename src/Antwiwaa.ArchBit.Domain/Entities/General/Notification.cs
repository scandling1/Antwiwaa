using System;
using Antwiwaa.ArchBit.Domain.Common;
using Antwiwaa.ArchBit.Domain.Enums;
using Antwiwaa.ArchBit.Domain.Events;
using CSharpFunctionalExtensions;

namespace Antwiwaa.ArchBit.Domain.Entities.General
{
    public class Notification : HasDomainEvent<int>
    {
        protected Notification()
        {
        }

        private Notification(string recipientAddress, string recipientName, string subject, string message,
            NotificationType notificationType, NotificationStatus status = NotificationStatus.Pending,
            bool instantNotify = true)
        {
            RecipientAddress = recipientAddress ?? throw new ArgumentNullException(nameof(recipientAddress));
            RecipientName = recipientName ?? throw new ArgumentNullException(nameof(recipientName));
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            NotificationType = notificationType;
            Status = status;
            Sender = "Swapgh"; 

            if (instantNotify) RaiseEvent(new NotificationCreatedEvent(this));
        }

        public string RecipientAddress { get; protected set; }
        public string RecipientName { get; protected set; }
        public string Subject { get; protected set; }
        public string Message { get; protected set; }
        public NotificationType NotificationType { get; protected set; }
        public NotificationStatus Status { get; set; }
        public string Sender { get; protected set; }

        public int FailureCount { get; set; }

        public static Result<Notification> CreateNew(string recipientAddress, string recipientName, string subject,
            string message, NotificationType notificationType, NotificationStatus status = NotificationStatus.Pending,
            bool instantNotify = true)
        {
            try
            {
                var newNotification = new Notification(recipientAddress, recipientName, subject, message,
                    notificationType, status, instantNotify);
                return Result.Success(newNotification);
            }
            catch (Exception ex)
            {
                return Result.Failure<Notification>(ex.Message);
            }
        }

        public Result<bool> UpdateStatus(NotificationStatus status)
        {
            Status = status;
            return Result.Success(true);
        }

        public void IncreaseFailureCount()
        {
            FailureCount = FailureCount + 1;
        }
    }
}