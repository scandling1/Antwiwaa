using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Antwiwaa.ArchBit.Shared.Common.Enums;
using Antwiwaa.ArchBit.Shared.Common.Helpers;
using Antwiwaa.ArchBit.Shared.Common.Models;

namespace Antwiwaa.ArchBit.Application.Common.Helpers
{
    public static class LinqExtensions
    {
        public static IQueryable<TSource> FilterQueryable<TSource>(this IQueryable<TSource> source,
            IList<Parameter> parameters)
        {
            return parameters.Where(x => !string.IsNullOrEmpty(x.SearchValue)).Aggregate(source,
                (current, parameter) => current.Where($"{parameter.Field}.Contains(@0)", parameter.SearchValue));
        }

        public static List<TSource> FilterQueryable<TSource>(this IEnumerable<TSource> source,
            IList<Parameter> parameters)
        {
            return parameters.Where(x => !string.IsNullOrEmpty(x.SearchValue)).Aggregate(source,
                (current, parameter) =>
                    current.AsQueryable().Where($"{parameter.Field}.Contains(@0)", parameter.SearchValue)).ToList();
        }

        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        //used by LINQ
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<TSource> SortQueryable<TSource>(this IQueryable<TSource> source,
            IList<Parameter> parameters)
        {
            var ordering = string.Join(",",
                parameters.Where(x => x.SortDirection != SortDirection.None)
                    .Select(x => $"{x.Field} {x.SortDirection.GetAttributeStringValue()}"));

            return source.OrderBy(ordering);
        }
    }
}