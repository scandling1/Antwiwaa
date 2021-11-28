using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Domain.Entities.General;

namespace Antwiwaa.ArchBit.Application.Common.Interfaces
{
    public interface INotificationRepository
    {
        Task<int> AddNewAsync(Notification obj, CancellationToken cancellationToken);
        Task<Notification> GetForUpdateAsync(int id);
        Task UpdateNotificationAsync(Notification obj, CancellationToken cancellationToken);
    }
}