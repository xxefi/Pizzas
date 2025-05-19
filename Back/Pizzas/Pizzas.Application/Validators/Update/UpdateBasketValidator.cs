using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Application.Validators.Update;

public class UpdateBasketValidator : AbstractValidator<UpdateBasketDto>
{
    public UpdateBasketValidator(ILocalizationService ls)
    {
        RuleFor(b => b.Items)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("ItemsRequired"));

        RuleForEach(b => b.Items)
            .SetValidator(new UpdateBasketItemValidator(ls));
    }
}