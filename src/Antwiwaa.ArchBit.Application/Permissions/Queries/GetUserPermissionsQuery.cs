using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using CSharpFunctionalExtensions;
using MediatR;

namespace Antwiwaa.ArchBit.Application.Permissions.Queries
{
    public class GetUserPermissionsQuery : IRequest<Result<string>>
    {
        public string UserId { get; set; }
    }

    public class GetUserPermissionsQueryHandler : IRequestHandler<GetUserPermissionsQuery, Result<string>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public GetUserPermissionsQueryHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<string>> Handle(GetUserPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions =
                await _permissionRepository.GetUserPermissionStringAsync(request.UserId, cancellationToken);

            return permissions.HasNoValue
                ? Result.Failure<string>("This user has no assigned permissions")
                : Result.Success(permissions.Value);
        }
    }
}