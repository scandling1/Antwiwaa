using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Domain.Common;

namespace Antwiwaa.ArchBit.Application.Common.Interfaces
{
    public interface IRepository<TEntity, TId> where TEntity : IsRootAggregate<TId>
    {
        Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(TId id);

        Task<TEntity> GetByIds(KeyValuePair<TId, TId>[] values);

        Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken);

        Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    }
}