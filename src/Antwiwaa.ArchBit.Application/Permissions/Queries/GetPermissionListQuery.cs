using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using MediatR;

namespace Antwiwaa.ArchBit.Application.Permissions.Queries
{
    public class GetPermissionListQuery : DataTableListRequestModel, IRequest<DataTableVm<PermissionDto>>
    {
    }

    public class GetPermissionListQueryHandler : IRequestHandler<GetPermissionListQuery, DataTableVm<PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionListQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<DataTableVm<PermissionDto>> Handle(GetPermissionListQuery request,
            CancellationToken cancellationToken)
        {
            return await _permissionRepository.GetPermissionListAsync(request, cancellationToken, request.DataReadMode);
        }
    }
}