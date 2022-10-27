using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Pagination
{
    public class PagedList<T>
    {
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages { get; init; }
        public IList<T> Items { get; set; }
        public bool HasPreviousPage => PageIndex > 0;
        public bool HasNextPage => PageIndex + 1 < TotalPages;
        
    }
}