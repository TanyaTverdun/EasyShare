using MediatR;

namespace EasyShare.Application.Features.Admin.Commands.DeleteCategory;

public record DeleteCategoryCommand : IRequest<Unit>
{
    public int Id { get; init; }
}
