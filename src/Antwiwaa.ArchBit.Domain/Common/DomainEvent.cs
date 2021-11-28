using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Antwiwaa.ArchBit.Domain.Common
{
    public interface IHasDomainEvent
    {
        IReadOnlyList<DomainEvent> DomainEvents { get; set; }
        void ClearDomainEvents();
        void RaiseEvent(DomainEvent domainEvent);
    }

    public abstract class HasDomainEvent<TId> : IsRootAggregate<TId>, IHasDomainEvent
    {
        private IList<DomainEvent> _domainEvents;

        protected HasDomainEvent()
        {
            _domainEvents = new List<DomainEvent>();
        }

        [NotMapped]
        public IReadOnlyList<DomainEvent> DomainEvents
        {
            get => (IReadOnlyList<DomainEvent>)_domainEvents;
            set => _domainEvents = value.ToList();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void RaiseEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }

    public class DomainEvent
    {
        protected DomainEvent()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset DateOccurred { get; protected set; }
    }
}