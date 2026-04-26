namespace EasyShare.Application.Features.Admin.Queries.GetItemTypeAttributes;

public record AdminAttributeDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
