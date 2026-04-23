using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShare.Application.Features.ItemTypes.Queries.GetTypesByCategory;

public class GetTypesByCategoryQueryValidator : AbstractValidator<GetTypesByCategoryQuery>
{
    public GetTypesByCategoryQueryValidator()
    {
        RuleFor(x => x.CategoryIds)
            .NotEmpty()
            .WithMessage("Список категорій не може бути порожнім.")
            .Must(list => list.Count <= 50)
            .WithMessage("Не можна запитувати більше 50 категорій за один раз.");

        RuleForEach(x => x.CategoryIds)
            .GreaterThan(0)
            .WithMessage("ID категорії має бути більшим за 0.");
    }
}
