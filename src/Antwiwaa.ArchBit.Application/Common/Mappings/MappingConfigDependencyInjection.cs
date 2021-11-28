using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Antwiwaa.ArchBit.Application.Common.Mappings
{
    public static class MappingConfigDependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.BaseType == typeof(Profile))
                    .ToList();

                foreach (var instance in types.Select(Activator.CreateInstance)) mc.AddProfile((Profile)instance);
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}