using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Update;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderValidator(ILocalizationService ls)
    {
        RuleFor(order => order.Status)
            .IsInEnum()
            .WithMessage(_ => ls.GetLocalizedString("InvalidOrderStatus"));

        RuleFor(order => order.Items)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("OrderItemsRequired"));

        RuleFor(order => order.DeliveryInfo)
            .NotNull()
            .WithMessage(_ => ls.GetLocalizedString("DeliveryInfoRequired"));
    }
}