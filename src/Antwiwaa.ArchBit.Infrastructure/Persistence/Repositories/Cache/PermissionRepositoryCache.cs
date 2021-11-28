using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Infrastructure.Services;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Models;
using Antwiwaa.ArchBit.Shared.Permissions.Dtos;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using Permission = Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate.Permission;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence.Repositories.Cache
{
    public class PermissionRepositoryCache : IPermissionRepository, IRepositoryCache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IPermissionRepository _permissionRepository;

        public PermissionRepositoryCache(IPermissionRepository permissionRepository, IDistributedCache distributedCache)
        {
            _permissionRepository = permissionRepository;
            _distributedCache = distributedCache;
        }

        public Task<Maybe<int>> AddNewPermissionAsync(Permission permission, CancellationToken cancellationToken)
        {
            return _permissionRepository.AddNewPermissionAsync(permission, cancellationToken);
        }

        public async Task<Maybe<PermissionDto>> GetById(int id, CancellationToken cancellationToken)
        {
            //Read From Cache First
            var permission = await _distributedCache.GetRecordAsync<PermissionDto>($"permission_{id}");

            if (permission != null) return Maybe<PermissionDto>.From(permission);

            var permFromDb = await _permissionRepository.GetById(id, cancellationToken);

            if (permFromDb.HasNoValue) return permFromDb;

            //Save to Cache
            await _distributedCache.SetRecordAsync(permFromDb.Value, $"permission_{id}", TimeSpan.FromSeconds(1800));

            return permFromDb;
        }

        public Task<Maybe<Permission>> GetByIdForUpdateOrDelete(int id, CancellationToken cancellationToken)
        {
            return _permissionRepository.GetByIdForUpdateOrDelete(id, cancellationToken);
        }

        public async Task<Maybe<PermissionDto>> GetByName(string name, CancellationToken cancellationToken)
        {
            //Read From Cache First
            var permission = await _distributedCache.GetRecordAsync<PermissionDto>($"permission_{name}");

            if (permission != null) return Maybe<PermissionDto>.From(permission);

            var permFromDb = await _permissionRepository.GetByName(name, cancellationToken);

            if (permFromDb.HasNoValue) return permFromDb;

            //Save to Cache
            await _distributedCache.SetRecordAsync(permFromDb.Value, $"permission_{name}", TimeSpan.FromSeconds(1800));

            return permFromDb;
        }

        public Task<Maybe<Permission>> GetByNameForUpdateOrDelete(string name, CancellationToken cancellationToken)
        {
            return _permissionRepository.GetByNameForUpdateOrDelete(name, cancellationToken);
        }

        public async Task<DataTableVm<PermissionDto>> GetPermissionListAsync(DataTableListRequestModel model,
            CancellationToken cancellationToken, DataReadMode readMode = DataReadMode.Default)
        {
            if (model.Parameters.Any(x =>
                !string.IsNullOrEmpty(x.SearchValue) || x.SortDirection != SortDirection.None))
                return await _permissionRepository.GetPermissionListAsync(model, cancellationToken);

            DataTableVm<PermissionDto> data;

            if (readMode == DataReadMode.Default)
            {
                //Read From Cache First
                data = await _distributedCache.GetRecordAsync<DataTableVm<PermissionDto>>(
                    $"permissionList_{model.Page}_{model.PageSize}");

                if (data != null) return data;
            }

            data = await _permissionRepository.GetPermissionListAsync(model, cancellationToken);
            //Save to Cache
            await _distributedCache.SetRecordAsync(data, $"permissionList_{model.Page}_{model.PageSize}",
                TimeSpan.FromSeconds(1800));

            return data;
        }

        public async Task<Maybe<string>> GetUserPermissionStringAsync(string userId,
            CancellationToken cancellationToken)
        {
            //Read From Cache First
            var permissions = await _distributedCache.GetRecordAsync<string>($"userPermission_{userId}");

            if (!string.IsNullOrEmpty(permissions)) return permissions;

            var permFromDb = await _permissionRepository.GetUserPermissionStringAsync(userId, cancellationToken);

            if (permFromDb.HasNoValue) return permFromDb;

            //Save to Cache
            await _distributedCache.SetRecordAsync(permFromDb.Value, $"userPermission_{userId}",
                TimeSpan.FromSeconds(1800));

            return permFromDb;
        }
    }
}