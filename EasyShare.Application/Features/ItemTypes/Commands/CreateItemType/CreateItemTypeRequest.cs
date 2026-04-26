namespace EasyShare.Application.Features.ItemTypes.Commands.CreateItemType;

public record CreateItemTypeRequest
{
    public required string Name { get; init; }
    public int CategoryId { get; init; }

    public CreateItemTypeCommand ToCommand() => new CreateItemTypeCommand
    {
        Name = this.Name,
        CategoryId = this.CategoryId
    };
}
