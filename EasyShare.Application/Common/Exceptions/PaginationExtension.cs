using EasyShare.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.Application.Common.Exceptions;

public static class PaginationExtension
{
    public static async Task<PagedResult<T>> PaginatedListAsync<T>(
        this IQueryable<T> queryable,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await queryable.CountAsync(cancellationToken);

        var items = await queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(
            items, 
            count, 
            pageNumber, 
            pageSize);
    }
}
