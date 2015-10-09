using System.Collections.Generic;

namespace DevTimer.Core
{
	public interface IPagedEnumerable<out T> : IEnumerable<T>
	{
		IEnumerable<T> Items { get; }
		int PageSize { get; }
		int PageNumber { get; }
		int PageCount { get; }
		int ItemCount { get; }
		int TotalCount { get; }
	}
}
