using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Application.Common.Security;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Permission = Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate.Permission;

namespace Antwiwaa.ArchBit.Application.Permissions.Commands
{
    [AuthorizePolicy(Policy = Policies.CanAddPermission)]
    public class CreatePermissionCmd : IRequest<Result<int>>
    {
        public PermissionDto PermissionDto { get; set; }
    }

    public class CreatePermissionCmdHandler : IRequestHandler<CreatePermissionCmd, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;

        public CreatePermissionCmdHandler(IMapper mapper, IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<int>> Handle(CreatePermissionCmd request, CancellationToken cancellationToken)
        {
            var obj = Permission.New(request.PermissionDto.PermissionName, request.PermissionDto.PermissionDescription,
                request.PermissionDto.LocalizationKey, request.PermissionDto.RequireAdminRole,
                request.PermissionDto.ParentPermissionId);

            var result = await _permissionRepository.AddNewPermissionAsync(obj, cancellationToken);

            return result.HasNoValue
                ? Result.Failure<int>("Permission could not be added")
                : Result.Success(result.Value);
        }
    }
}