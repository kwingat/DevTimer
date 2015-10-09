using System.Collections.Generic;
using AutoMapper;
using DevTimer.Core;
using Newtonsoft.Json;

namespace DevTimer.Models
{
    public class PageResult<T> where T : class
    {
        internal PageResult(IEnumerable<T> source, int totalCount)
        {
            rows = source;
            total = totalCount;
        }

        [JsonProperty("rows")]
        public IEnumerable<T> rows { get; private set; }

        [JsonProperty("total")]
        public int total { get; private set; }
    }

    public static class IPagedEnumerableExtensions
    {
        public static PageResult<TDestination> ToPageResult<TSource, TDestination>(this IPagedEnumerable<TSource> source)
            where TSource : class
            where TDestination : class
        {
            var items = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);

            return new PageResult<TDestination>(items, source.TotalCount);
        }
    }
}