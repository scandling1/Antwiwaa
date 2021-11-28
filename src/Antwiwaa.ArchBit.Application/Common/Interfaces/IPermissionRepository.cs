using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using CSharpFunctionalExtensions;
using Permission = Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate.Permission;

namespace Antwiwaa.ArchBit.Application.Common.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Maybe<int>> AddNewPermissionAsync(Permission permission, CancellationToken cancellationToken);
        Task<Maybe<PermissionDto>> GetById(int id, CancellationToken cancellationToken);
        Task<Maybe<Permission>> GetByIdForUpdateOrDelete(int id, CancellationToken cancellationToken);
        Task<Maybe<PermissionDto>> GetByName(string name, CancellationToken cancellationToken);
        Task<Maybe<Permission>> GetByNameForUpdateOrDelete(string name, CancellationToken cancellationToken);

        Task<DataTableVm<PermissionDto>> GetPermissionListAsync(DataTableListRequestModel model,
            CancellationToken cancellationToken, DataReadMode dataReadMode = DataReadMode.Default);

        Task<Maybe<string>> GetUserPermissionStringAsync(string userId, CancellationToken cancellationToken);
    }
}