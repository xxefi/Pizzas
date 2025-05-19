using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Core.Constants.ValidationConstants;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreateAddressValidator : AbstractValidator<CreateAddressDto>
{
    public CreateAddressValidator(ILocalizationService ls)
    {
        RuleFor(a => a.Street)
                .NotEmpty()
                .WithMessage(_ => ls.GetLocalizedString("StreetRequired"))
                .Must(IsValidStreet)
                .WithMessage(_ => ls.GetLocalizedString("StreetInvalid"))
                .MaximumLength(MaxStreetLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("StreetMaxLength"), MaxStreetLength));

            RuleFor(a => a.City)
                .NotEmpty()
                .WithMessage(_ => ls.GetLocalizedString("CityRequired"))
                .Must(IsValidCity)
                .WithMessage(_ => ls.GetLocalizedString("CityInvalid"))
                .MaximumLength(MaxCityLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("CityMaxLength"), MaxCityLength));

            RuleFor(a => a.State)
                .NotEmpty()
                .WithMessage(_ => ls.GetLocalizedString("StateRequired"))
                .Must(IsValidState)
                .WithMessage(_ => ls.GetLocalizedString("StateInvalid"))
                .MaximumLength(MaxStateLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("StateMaxLength"), MaxStateLength));

            RuleFor(a => a.Country)
                .NotEmpty()
                .WithMessage(_ => ls.GetLocalizedString("CountryRequired"))  
                .MaximumLength(MaxCountryLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("CountryMaxLength"), MaxCountryLength));
        
    }
} 