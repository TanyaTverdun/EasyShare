namespace EasyShare.Application.Features.Items.Commands.CreateItem;

public record CreateItemAttributeDto
{
    public int AttributeId { get; init; }
    public required string Value { get; init; }
}
