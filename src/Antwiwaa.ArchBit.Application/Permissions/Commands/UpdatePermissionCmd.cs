using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using CSharpFunctionalExtensions;
using MediatR;

namespace Antwiwaa.ArchBit.Application.Permissions.Commands
{
    public class UpdatePermissionCmd : IRequest<Result<Unit>>
    {
        public PermissionDto PermissionDto { get; set; }
        public int PermissionId { get; set; }
    }

    public class UpdatePermissionCmdHandler : IRequestHandler<UpdatePermissionCmd, Result<Unit>>
    {
        private readonly IRepository<Permission, int> _permissionRepository;

        public UpdatePermissionCmdHandler(IRepository<Permission, int> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<Unit>> Handle(UpdatePermissionCmd request, CancellationToken cancellationToken)
        {
            var obj = await _permissionRepository.GetById(request.PermissionId);

            obj.UpdateDetails(request.PermissionDto.PermissionName, request.PermissionDto.PermissionDescription,
                request.PermissionDto.LocalizationKey, request.PermissionDto.RequireAdminRole,
                request.PermissionDto.ParentPermissionId);

            await _permissionRepository.UpdateAsync(obj, cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}