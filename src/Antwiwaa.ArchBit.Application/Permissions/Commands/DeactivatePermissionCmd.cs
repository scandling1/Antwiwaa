using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Application.Common.Security;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using CSharpFunctionalExtensions;
using MediatR;
using Permission = Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate.Permission;

namespace Antwiwaa.ArchBit.Application.Permissions.Commands
{
    [AuthorizePolicy(Policy = Policies.CanDeactivatePermission)]
    public class DeactivatePermissionCmd : IRequest<Result<bool>>
    {
        public int PermissionId { get; set; }
    }

    public class DeactivatePermissionCmdHandler : IRequestHandler<DeactivatePermissionCmd, Result<bool>>
    {
        private readonly IRepository<Permission, int> _permissionRepository;

        public DeactivatePermissionCmdHandler(IRepository<Permission, int> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<bool>> Handle(DeactivatePermissionCmd request, CancellationToken cancellationToken)
        {
            var obj = await _permissionRepository.GetById(request.PermissionId);
            obj.Deactivate();

            await _permissionRepository.UpdateAsync(obj, cancellationToken);

            return Result.Success(true);
        }
    }
}