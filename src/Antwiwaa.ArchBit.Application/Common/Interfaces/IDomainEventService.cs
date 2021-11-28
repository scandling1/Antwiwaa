using System.Threading.Tasks;
using Antwiwaa.ArchBit.Domain.Common;

namespace Antwiwaa.ArchBit.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}