using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Core.Extensions
{
    public static class PagingExtension
    {
        public class PagedResult<T>
        {
            public IEnumerable<T> Items { get; set; }
            public int Total { get; set; }
            public int PageSize { get; set; }
            public int Skipped { get; set; }
        }
        public static Task<PagedResult<T>> ToPagedResult<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var totalItems = query.Count();

            var items = query.Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            return Task.FromResult(new PagedResult<T>
            {
                Items = items,
                Total = totalItems,
                PageSize = pageSize,
                Skipped = (pageIndex - 1) * pageSize,
            });
        }
    }
}
