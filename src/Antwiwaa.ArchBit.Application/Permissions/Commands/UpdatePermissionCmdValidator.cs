using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using FluentValidation;

namespace Antwiwaa.ArchBit.Application.Permissions.Commands
{
    public class UpdatePermissionCmdValidator : AbstractValidator<UpdatePermissionCmd>
    {
        private readonly IPermissionRepository _permissionRepository;

        public UpdatePermissionCmdValidator(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;

            RuleFor(x => x.PermissionDto).Cascade(CascadeMode.Stop).NotNull()
                .WithMessage("Permission details cannot be null").DependentRules(() =>
                {
                    RuleFor(x => x.PermissionDto.PermissionName).Cascade(CascadeMode.Stop).NotEmpty().MaximumLength(50)
                        .DependentRules(() =>
                        {
                            RuleFor(x => x.PermissionDto).MustAsync(BeUniqueName)
                                .WithMessage("Permission name already exist");
                        });
                    RuleFor(x => x.PermissionDto.PermissionDescription).MaximumLength(120).NotEmpty();
                    RuleFor(x => x.PermissionDto.LocalizationKey).NotEmpty().MaximumLength(50);
                    RuleFor(x => x.PermissionDto.RequireAdminRole).NotNull();
                    RuleFor(x => x.PermissionId).Cascade(CascadeMode.Stop).NotNull().DependentRules(() =>
                    {
                        RuleFor(x => x.PermissionId).MustAsync(BeValidPermission)
                            .WithMessage("Permission details does not exist");
                    });
                    RuleFor(x => x.PermissionDto).MustAsync(BeValidPermissionName)
                        .When(x => !string.IsNullOrEmpty(x.PermissionDto.ParentPermissionName))
                        .WithMessage("Dependent permission details does not exist");
                });
        }

        private async Task<bool> BeUniqueName(PermissionDto permissionDto, CancellationToken cancellationToken)
        {
            //Perm1-Kof-adweso==> adweso
            //Perm2-kof-Mile50

            var result =
                await _permissionRepository.GetByNameForUpdateOrDelete(permissionDto.PermissionName, cancellationToken);

            if (result.HasNoValue) return true;

            return result.Value.Id == permissionDto.Id;
        }

        private async Task<bool> BeValidPermission(int permissionId, CancellationToken cancellationToken)
        {
            var obj = await _permissionRepository.GetByIdForUpdateOrDelete(permissionId, cancellationToken);

            return obj != null;
        }

        private async Task<bool> BeValidPermissionName(PermissionDto permissionDto, CancellationToken cancellationToken)
        {
            var obj = await _permissionRepository.GetByNameForUpdateOrDelete(permissionDto.ParentPermissionName,
                cancellationToken);

            if (obj.HasNoValue) return false;
            permissionDto.ParentPermissionId = obj.Value.Id;
            return true;
        }
    }
}