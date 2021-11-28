using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Application.Common.Security;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;

namespace Antwiwaa.ArchBit.Application.Permissions.Queries
{
    [AuthorizePolicy(Policy = Policies.CanListPermission)]
    public class GetPermissionQuery : IRequest<Maybe<PermissionDto>>
    {
        public int PermissionId { get; set; }
    }

    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, Maybe<PermissionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;

        public GetPermissionQueryHandler(IMapper mapper, IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }

        public async Task<Maybe<PermissionDto>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            var result = await _permissionRepository.GetById(request.PermissionId, cancellationToken);

            if (result.HasNoValue) return Maybe<PermissionDto>.None;

            var obj = _mapper.Map<PermissionDto>(result.Value);

            return Maybe<PermissionDto>.From(obj);
        }
    }
}