using MediatR;

namespace Antwiwaa.ArchBit.Application.Common.Notifications
{
    public class DomainEventNotification<TDomainEvent> : INotification
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}