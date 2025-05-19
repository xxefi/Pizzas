using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Core.Constants.ValidationConstants;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Update;

public class UpdateBasketItemValidator : AbstractValidator<UpdateBasketItemDto>
{
    public UpdateBasketItemValidator(ILocalizationService ls)
    {
        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage(_ => ls.GetLocalizedString("ProductQuantityGreaterThanZero"))
            .LessThanOrEqualTo(MaxQuantity)
            .WithMessage(_ => string.Format(ls.GetLocalizedString("ProductQuantityMax"), MaxQuantity))
            .Must(IsValidQuantity)
            .WithMessage(_ => ls.GetLocalizedString("QuantityInvalid"));
    }
}