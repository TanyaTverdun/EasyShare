namespace EasyShare.Application.Features.Items.Commands.UpdateItem;

public record UpdateItemAttributeDto
{
    public int AttributeId { get; init; }
    public required string Value { get; init; }
}
