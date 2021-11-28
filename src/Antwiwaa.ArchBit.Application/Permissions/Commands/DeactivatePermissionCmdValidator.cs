using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate;
using FluentValidation;

namespace Antwiwaa.ArchBit.Application.Permissions.Commands
{
    public class DeactivatePermissionCmdValidator : AbstractValidator<DeactivatePermissionCmd>
    {
        private readonly IRepository<Permission, int> _permissionRepository;

        public DeactivatePermissionCmdValidator(IRepository<Permission, int> permissionRepository)
        {
            _permissionRepository = permissionRepository;
            RuleFor(x => x.PermissionId).Cascade(CascadeMode.Stop).NotNull().GreaterThan(10).DependentRules(() =>
            {
                RuleFor(x => x.PermissionId).MustAsync(BeValidPermission)
                    .WithMessage("The permission details provided does not exist");
            });
        }

        public async Task<bool> BeValidPermission(int permissionId, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetById(permissionId);
            return permission != null;
        }
    }
}