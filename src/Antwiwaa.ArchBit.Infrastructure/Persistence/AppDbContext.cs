using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Domain.Common;
using Antwiwaa.ArchBit.Domain.Entities.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Antwiwaa.ArchBit.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IDomainEventService _domainEventService;
        private readonly IUserService _userService;
        private IDbContextTransaction _currentTransaction;

        public AppDbContext(DbContextOptions options, IDateTimeService dateTimeService, IUserService userService,
            IDomainEventService domainEventService) : base(options)
        {
            _dateTimeService = dateTimeService;
            _userService = userService;
            _domainEventService = domainEventService;
        }

        private AppDbContext(DbContextOptions<AppDbContext> optionsBuilderOptions) : base(optionsBuilderOptions)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }


        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return;

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted)
                .ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                if (_currentTransaction != null) await _currentTransaction?.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public static AppDbContext CreateDbContext(DbContextOptions<AppDbContext> options)
        {
            return new AppDbContext(options);
        }

        private async Task DispatchEvents()
        {
            var entities = ChangeTracker.Entries<IHasDomainEvent>().ToList();

            var domainEvents = entities.Select(x => x.Entity.DomainEvents).SelectMany(x => x).ToArray();

            foreach (var entityEntry in entities.Where(entityEntry => entityEntry.Entity.DomainEvents.Any()))
                entityEntry.Entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents) await _domainEventService.Publish(domainEvent);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var user = _userService.GetUserId();

            var userId = user.HasNoValue ? "system" : user.Value;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.Created = _dateTimeService.Now;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = userId;
                    entry.Entity.LastModified = _dateTimeService.Now;
                }
            }

            var persisted = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return persisted;
        }
    }
}