using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Domain.Entities.General;

namespace Antwiwaa.ArchBit.Application.Common.Interfaces
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(Notification notification, CancellationToken cancellationToken);
    }
}