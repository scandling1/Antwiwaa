using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using FluentValidation;

namespace Antwiwaa.ArchBit.Application.Permissions.Commands
{
    public class CreatePermissionCmdValidator : AbstractValidator<CreatePermissionCmd>
    {
        private readonly IPermissionRepository _permissionRepository;

        public CreatePermissionCmdValidator(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;

            RuleFor(x => x.PermissionDto).Cascade(CascadeMode.Stop).NotNull()
                .WithMessage("Permission details cannot be null").DependentRules(() =>
                {
                    RuleFor(x => x.PermissionDto.PermissionName).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(50)
                        .DependentRules(() =>
                        {
                            RuleFor(x => x.PermissionDto.PermissionName).MustAsync(BeUniqueName)
                                .WithMessage("Permission Name already exist");
                        });
                    RuleFor(x => x.PermissionDto.PermissionDescription).MaximumLength(120).NotEmpty();
                    RuleFor(x => x.PermissionDto.LocalizationKey).NotEmpty().MaximumLength(50);
                    RuleFor(x => x.PermissionDto.RequireAdminRole).NotNull();
                    RuleFor(x => x.PermissionDto).MustAsync(BeValidPermission)
                        .When(x => !string.IsNullOrEmpty(x.PermissionDto.ParentPermissionName))
                        .WithMessage("Dependent permission details does not exist");
                });
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            var result = await _permissionRepository.GetByName(name, cancellationToken);

            return result.HasNoValue;
        }

        private async Task<bool> BeValidPermission(PermissionDto permissionDto, CancellationToken cancellationToken)
        {
            var result = await _permissionRepository.GetByName(permissionDto.PermissionName, cancellationToken);

            if (result.HasNoValue) return false;

            permissionDto.ParentPermissionId = result.Value.Id;

            return true;
        }
    }
}