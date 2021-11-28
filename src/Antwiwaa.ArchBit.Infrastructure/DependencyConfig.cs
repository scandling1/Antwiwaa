using System;
using System.Linq;
using System.Reflection;
using Antwiwaa.ArchBit.Application.Common.Configurations;
using Antwiwaa.ArchBit.Application.Common.Interfaces;
using Antwiwaa.ArchBit.Infrastructure.Persistence;
using Antwiwaa.ArchBit.Infrastructure.Persistence.Repositories;
using Antwiwaa.ArchBit.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Antwiwaa.ArchBit.Infrastructure
{
    public static class DependencyConfig
    {
        public static void AddCacheRepositoryMappings(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetInterfaces().Any(i => i.Name == nameof(IRepositoryCache))).ToList();

            foreach (var instance in types)
            {
                var contract = instance.GetInterfaces().First(x => x.Name != nameof(IRepositoryCache));
                services.Decorate(contract ?? throw new InvalidOperationException(), instance);
            }
        }

        public static void AddConfigFromInfraLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var adminConfig = configuration.GetSection(nameof(AdminConfig)).Get<AdminConfig>();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddRepositoryMappings();
            services.AddScoped<IDomainEventService, DomainEventService>();
            //services.AddScoped<INotificationService, NotificationService>();
            //services.AddScoped<ISmsService, HubtelSmsService>();
            services.AddRedisDependencies(configuration);
            if (adminConfig.EnableCache) services.AddCacheRepositoryMappings();
        }

        public static void AddRedisDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Redis Dependencies
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                options.InstanceName = configuration["AdminConfig:RedisConfig:Instance"];
            });
        }

        public static void AddRepositoryMappings(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType?.Name == "Repository`2")
                .ToList();

            foreach (var instance in types)
            {
                var contract = instance.GetInterface($"I{instance.Name}");
                services.AddScoped(contract ?? throw new InvalidOperationException(), instance);
            }
        }
    }
}