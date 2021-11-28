using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Helpers;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Permission = Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate.Permission;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence.Repositories
{
    public class PermissionRepository : Repository<Permission, int>, IPermissionRepository
    {
        private readonly IMapper _mapper;

        public PermissionRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<Maybe<int>> AddNewPermissionAsync(Permission permission, CancellationToken cancellationToken)
        {
            var result = await AddAsync(permission, cancellationToken);
            return Maybe<int>.From(result);
        }

        public async Task<Maybe<PermissionDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var obj = await GetById(id);
            if (obj == null) return Maybe<PermissionDto>.None;
            var objDto = _mapper.Map<PermissionDto>(obj);
            return Maybe<PermissionDto>.From(objDto);
        }

        public async Task<Maybe<Permission>> GetByIdForUpdateOrDelete(int id, CancellationToken cancellationToken)
        {
            var obj = await GetById(id);
            return obj == null ? Maybe<Permission>.None : Maybe<Permission>.From(obj);
        }

        public async Task<Maybe<PermissionDto>> GetByName(string name, CancellationToken cancellationToken)
        {
            var obj = await Get(x => x.PermissionName.Equals(name), cancellationToken);
            if (obj == null) return Maybe<PermissionDto>.None;
            var objDto = _mapper.Map<PermissionDto>(obj);
            return Maybe<PermissionDto>.From(objDto);
        }

        public async Task<Maybe<Permission>> GetByNameForUpdateOrDelete(string name,
            CancellationToken cancellationToken)
        {
            var obj = await Get(x => x.PermissionName.Equals(name), cancellationToken);
            return obj == null ? Maybe<Permission>.None : Maybe<Permission>.From(obj);
        }

        public async Task<DataTableVm<PermissionDto>> GetPermissionListAsync(DataTableListRequestModel model,
            CancellationToken cancellationToken, DataReadMode readMode = DataReadMode.Default)
        {
            var data = GetAll();

            var totalRecords = await data.CountAsync(cancellationToken);

            data = model.Parameters.Any(x => !string.IsNullOrEmpty(x.SearchValue))
                ? data.FilterQueryable(model.Parameters)
                : data;

            data = model.Parameters.Any(x => x.SortDirection != SortDirection.None)
                ? data.SortQueryable(model.Parameters)
                : data;

            var filteredData = await data.Page(model.Page, model.PageSize)
                .ProjectTo<PermissionDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            var obj = DataTableVm<PermissionDto>.New(totalRecords, model.Page, model.PageSize, filteredData);

            return obj;
        }

        public async Task<Maybe<string>> GetUserPermissionStringAsync(string userId,
            CancellationToken cancellationToken)
        {
            var oList = await Find(x => x.Users.Any(y => y.UserId == userId)).ToListAsync(cancellationToken);

            return !oList.Any() ? Maybe<string>.None : string.Join(",", oList.Select(x => x.PermissionName));
        }
    }
}