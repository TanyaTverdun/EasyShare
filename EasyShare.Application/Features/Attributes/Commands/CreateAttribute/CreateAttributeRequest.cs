namespace EasyShare.Application.Features.Attributes.Commands.CreateAttribute;

public record CreateAttributeRequest
{
    public required string Name { get; init; }
    public int TypeId { get; init; }

    public CreateAttributeCommand ToCommand() => new CreateAttributeCommand
    {
        Name = this.Name,
        TypeId = this.TypeId
    };
}
