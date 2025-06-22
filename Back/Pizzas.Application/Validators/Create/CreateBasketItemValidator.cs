using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Core.Constants.ValidationConstants;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreateBasketItemValidator : AbstractValidator<CreateBasketItemDto>
{
    public CreateBasketItemValidator(ILocalizationService ls)
    {
        RuleFor(item => item.PizzaId)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PizzaIdRequired"))
            .Must(IsValidPizzaId)
            .WithMessage(_ => ls.GetLocalizedString("PizzaIdInvalid"));

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage(_ => ls.GetLocalizedString("ProductQuantityGreaterThanZero"))
            .LessThanOrEqualTo(MaxQuantity)
            .WithMessage(_ => string.Format(ls.GetLocalizedString("ProductQuantityMax"), MaxQuantity))
            .Must(IsValidQuantity)
            .WithMessage(_ => ls.GetLocalizedString("QuantityInvalid"));
    }
}