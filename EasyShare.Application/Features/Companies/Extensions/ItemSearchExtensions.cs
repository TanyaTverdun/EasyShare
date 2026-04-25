using EasyShare.Domain.Entities;

namespace EasyShare.Application.Features.Companies.Extensions;

public static class ItemSearchExtensions
{
    public static IQueryable<Item> Search(
        this IQueryable<Item> query, 
        string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return query;
        }

        return query.Where(i => i.Name.Contains(searchTerm));
    }
}
