using FluentValidation;

namespace EasyShare.Application.Features.Items.Queries.GetCatalogItems
{
    public class GetCatalogItemsQueryValidator : AbstractValidator<GetCatalogItemsQuery>
    {
        public GetCatalogItemsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Номер сторінки не може бути меншим за 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Розмір сторінки має бути від 1 до 100 товарів.");

            When(x => x.SortBy == CatalogSortOption.Nearest, () =>
            {
                RuleFor(x => x.UserCity)
                    .NotEmpty()
                    .WithMessage("Для сортування 'Найближчі' необхідно обов'язково вказати ваше місто.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.SearchTerm), () =>
            {
                RuleFor(x => x.SearchTerm)
                    .MaximumLength(100)
                    .WithMessage("Пошуковий запит не може бути довшим за 100 символів.");
            });

            When(x => !string.IsNullOrWhiteSpace(x.City), () =>
            {
                RuleFor(x => x.City)
                    .MaximumLength(50)
                    .WithMessage("Назва міста не може бути довшою за 50 символів.");
            });

            When(x => x.AvailableFrom.HasValue && x.AvailableTo.HasValue, () =>
            {
                RuleFor(x => x.AvailableTo)
                    .GreaterThanOrEqualTo(x => x.AvailableFrom)
                    .WithMessage("Дата завершення оренди не може бути раніше за дату початку.");
            });

            When(x => x.MinQuantity.HasValue, () =>
            {
                RuleFor(x => x.MinQuantity)
                    .GreaterThanOrEqualTo(1)
                    .WithMessage("Мінімальна кількість має бути хоча б 1.");
            });

            When(x => x.CategoryIds != null, () =>
            {
                RuleFor(x => x.CategoryIds!.Count)
                    .LessThanOrEqualTo(50)
                    .WithMessage("Не можна обрати більше 50 категорій одночасно.");
            });

            When(x => x.TypeIds != null, () =>
            {
                RuleFor(x => x.TypeIds!.Count)
                    .LessThanOrEqualTo(50)
                    .WithMessage("Не можна обрати більше 50 типів одночасно.");
            });
        }
    }
}
