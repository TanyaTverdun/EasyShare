using MediatR;

namespace EasyShare.Application.Features.Admin.Commands.UpdateCategory;

public record UpdateCategoryCommand: IRequest<Unit>
{
    public int Id { get; init; }
    public string Name { get; init; }
}
