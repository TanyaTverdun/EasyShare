namespace EasyShare.Application.Features.Admin.Queries.GetItemTypes;

public record AdminItemTypeDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int AttributesCount { get; init; }
}
