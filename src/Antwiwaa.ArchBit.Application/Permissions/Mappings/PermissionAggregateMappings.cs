using Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using AutoMapper;

namespace Antwiwaa.ArchBit.Application.Permissions.Mappings
{
    public class PermissionAggregateMappings : Profile
    {
        public PermissionAggregateMappings()
        {
            CreateMap<Permission, PermissionDto>()
                .ForMember(d => d.ParentPermissionName,
                    opt =>
                        opt.MapFrom(s => s.ParentPermission.PermissionName)).ReverseMap();
        }
    }
}