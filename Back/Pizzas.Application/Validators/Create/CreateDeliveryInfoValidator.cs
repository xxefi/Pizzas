using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Core.Constants.ValidationConstants;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreateDeliveryInfoValidator : AbstractValidator<CreateDeliveryInfoDto>
{
    public CreateDeliveryInfoValidator(ILocalizationService ls)
    {
        RuleFor(delivery => delivery.OrderId)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("OrderIdRequired"));

        RuleFor(delivery => delivery.Address)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("AddressRequired"))
            .Must(IsValidAddress)
            .WithMessage(_ => ls.GetLocalizedString("InvalidAddress"));

        RuleFor(delivery => delivery.City)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("CityRequired"))
            .Must(IsValidCity)
            .WithMessage(_ => ls.GetLocalizedString("InvalidCity"));

        RuleFor(delivery => delivery.PostalCode)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PostalCodeRequired"))
            .Must(IsValidPostalCode)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPostalCode"));

        RuleFor(delivery => delivery.PhoneNumber)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PhoneNumberRequired"))
            .Must(IsValidPhoneNumber)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPhoneNumber"));
    }
}