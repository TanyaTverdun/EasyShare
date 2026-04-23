using FluentValidation;

namespace EasyShare.Application.Features.Attributes.Queries.GetAttributesByType;

public class GetAttributesByTypeQueryValidator : AbstractValidator<GetAttributesByTypeQuery>
{
    public GetAttributesByTypeQueryValidator()
    {
        RuleFor(x => x.TypeIds)
            .NotEmpty()
            .WithMessage("Список типів не може бути порожнім.")
            .Must(list => list.Count <= 50)
            .WithMessage("Не можна запитувати більше 50 типів за один раз.");

        RuleForEach(x => x.TypeIds)
            .GreaterThan(0)
            .WithMessage("ID типу має бути більшим за 0.");
    }
}
