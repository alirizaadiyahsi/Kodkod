using System.Collections.Generic;

namespace Kodkod.Utilities.PagedList
{
    public interface IPagedList<T>
    {
        int TotalCount { get; set; }

        IList<T> Items { get; set; }
    }
}
