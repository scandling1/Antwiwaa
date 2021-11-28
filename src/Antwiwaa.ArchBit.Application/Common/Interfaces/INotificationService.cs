using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Domain.Entities.General;

namespace Antwiwaa.ArchBit.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotification(Notification notification, CancellationToken cancellationToken);
    }
}