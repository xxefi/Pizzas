using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;

namespace Pizzas.Application.Validators.Create;

public class CreateBasketValidator : AbstractValidator<CreateBasketDto>
{
    public CreateBasketValidator(ILocalizationService ls)
    {
        RuleFor(b => b.Items)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("ItemsRequired"));

        RuleForEach(b => b.Items)
            .SetValidator(new CreateBasketItemValidator(ls));
    }
}