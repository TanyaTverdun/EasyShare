using MediatR;

namespace EasyShare.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<int>
{
    public required string Name { get; init; }
}
