using MediatR;

namespace EasyShare.Application.Features.Admin.Queries.GetCategories;

public record GetAdminCategoriesQuery : IRequest<List<AdminCategoryDto>>
{
    public string? SearchTerm { get; init; }
}
