using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator(ILocalizationService ls)
    {
        RuleFor(order => order.Items)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("OrderItemsRequired"))
            .Must(items => items.All(item => IsValidPizzaId(item.PizzaId) && IsValidQuantity(item.Quantity)))
            .WithMessage(_ => ls.GetLocalizedString("InvalidOrderItems"));

        RuleFor(order => order.Currency)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("CurrencyRequired"))
            .Must(currency => IsValidCurrency(currency))
            .WithMessage(_ => ls.GetLocalizedString("InvalidCurrency"));
    }
}