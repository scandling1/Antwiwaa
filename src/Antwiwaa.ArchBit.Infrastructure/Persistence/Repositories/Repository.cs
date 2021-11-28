using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IsRootAggregate<TId>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _rootAggregates;

        public Repository(AppDbContext context)
        {
            _context = context;
            _rootAggregates = _context.Set<TEntity>();
        }

        public async Task<TId> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _rootAggregates.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }


        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await _rootAggregates.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _rootAggregates.AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await _rootAggregates.CountAsync(predicate, cancellationToken);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _rootAggregates.Where(predicate);
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _rootAggregates.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _rootAggregates.AsQueryable();
        }


        public async Task<TEntity> GetById(TId id)
        {
            return await _rootAggregates.FindAsync(id);
        }

        public async Task<TEntity> GetByIds(KeyValuePair<TId, TId>[] values)
        {
            return await _rootAggregates.FindAsync(values);
        }

        public async Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _rootAggregates.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            _rootAggregates.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities) _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}