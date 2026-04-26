namespace EasyShare.Application.Features.Admin.Queries.GetCategories;

public record AdminCategoryDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}
