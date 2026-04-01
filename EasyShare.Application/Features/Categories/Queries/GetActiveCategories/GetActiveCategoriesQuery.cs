using MediatR;

namespace EasyShare.Application.Features.Categories.Queries.GetActiveCategories;

public record GetActiveCategoriesQuery : IRequest<List<CategoryDto>>;
