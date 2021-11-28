using Antwiwaa.ArchBit.Domain.Common;
using Antwiwaa.ArchBit.Domain.Entities.General;

namespace Antwiwaa.ArchBit.Domain.Events
{
    public class NotificationCreatedEvent : DomainEvent
    {
        public NotificationCreatedEvent(Notification item)
        {
            Item = item;
        }

        public Notification Item { get; }
    }
}