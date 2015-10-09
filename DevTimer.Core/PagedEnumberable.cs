using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DevTimer.Core
{
    internal class PagedEnumerable<T> : IPagedEnumerable<T>
    {
        private readonly IEnumerable<T> _items;

        public int PageSize { get; private set; }

        public int PageNumber { get; private set; }

        public int PageCount
        {
            get
            {
                int pageCount = TotalCount / PageSize;
                if (TotalCount % PageSize > 0)
                    pageCount++;
                return pageCount;
            }
        }

        public IEnumerable<T> Items { get { return _items; } }

        public int ItemCount { get { return _items.Count(); } }

        public int TotalCount { get; private set; }

        internal PagedEnumerable(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize");

            _items = items;

            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (_items as IEnumerable).GetEnumerator();
        }
    }

    public static class PageEnumerableExtensions
    {
        public static IQueryable<T> ForPage<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException("pageNumber");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("pageSize");
            }

            return source.Skip((pageNumber - 1)*pageSize).Take(pageSize);
        }

        public static IPagedEnumerable<T> AsPagedEnumerable<T>(this IEnumerable<T> source, int pageNumber, int pageSize, int totalRowCount) where T : class
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException("pageNumber");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize");

            return new PagedEnumerable<T>(source, pageNumber, pageSize, totalRowCount);
        }
    }
}
