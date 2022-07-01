Using System;
Using System.Collections.Generic;
Using System.Linq;
Using System.Threading.Tasks;
Using Microsoft.EntityFrameworkCore;

Namespace ContosoUniversity
{
    Public Class PaginatedList<T> : List<T>
    {
        Public int PageIndex { Get; Private Set; }
        Public int TotalPages { Get; Private Set; }

        Public PaginatedList(List < T > items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        Public bool HasPreviousPage => PageIndex > 1;

        Public bool HasNextPage => PageIndex < TotalPages;

        Public Static Async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            Return New PaginatedList < T > (items, count, pageIndex, pageSize);
        }
    }
}