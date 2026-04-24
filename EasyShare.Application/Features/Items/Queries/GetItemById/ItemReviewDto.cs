namespace EasyShare.Application.Features.Items.Queries.GetItemById;

public record ItemReviewDto
{
    public required int ReviewId { get; init; }
    public required int Rating { get; init; }
    public string? Comment { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required string ReviewerName { get; init; }
}
