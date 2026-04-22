namespace EasyShare.Application.Features.Items.Queries.GetCatalogItems;

public enum CatalogSortOption
{
    /// <summary>
    /// Найновіші (за замовчуванням) пропозиції
    /// </summary>
    Newest = 1,

    /// <summary>
    /// Найближчі до вас пропозиції
    /// </summary>
    Nearest = 2,

    /// <summary>
    /// Ціна (за зростанням) пропозиції
    /// </summary>
    PriceAscending = 3,

    /// <summary>
    /// Ціна (за спаданням) пропозиції
    /// </summary>
    PriceDescending = 4,

    /// <summary>
    /// Рейтинг (від найвищого) пропозиції 
    /// </summary>
    HighestRating = 5
}
