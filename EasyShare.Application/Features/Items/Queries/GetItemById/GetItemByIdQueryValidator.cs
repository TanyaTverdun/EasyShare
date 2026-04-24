using FluentValidation;

namespace EasyShare.Application.Features.Items.Queries.GetItemById;

public class GetItemByIdQueryValidator : AbstractValidator<GetItemByIdQuery>
{
    public GetItemByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Некоректне посилання на товар. Перевірте правильність адреси.");
    }
}
