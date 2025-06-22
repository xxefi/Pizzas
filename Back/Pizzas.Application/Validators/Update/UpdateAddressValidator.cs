using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Core.Constants.ValidationConstants;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Update;

public class UpdateAddressValidator : AbstractValidator<UpdateAddressDto>
{
    public UpdateAddressValidator(ILocalizationService ls)
    {
        RuleFor(a => a.Street)
                .NotEmpty()
                .When(a => !string.IsNullOrEmpty(a.Street))
                .WithMessage(_ => ls.GetLocalizedString("StreetRequired"))
                .Must(IsValidStreet)
                .WithMessage(_ => ls.GetLocalizedString("StreetInvalid"))
                .MaximumLength(MaxStreetLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("StreetMaxLength"), MaxStreetLength));

            RuleFor(a => a.City)
                .NotEmpty()
                .When(a => !string.IsNullOrEmpty(a.City))
                .WithMessage(_ => ls.GetLocalizedString("CityRequired"))
                .Must(IsValidCity)
                .WithMessage(_ => ls.GetLocalizedString("CityInvalid"))
                .MaximumLength(MaxCityLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("CityMaxLength"), MaxCityLength));

            RuleFor(a => a.State)
                .NotEmpty()
                .When(a => !string.IsNullOrEmpty(a.State))
                .WithMessage(_ => ls.GetLocalizedString("StateRequired"))
                .Must(IsValidState)
                .WithMessage(_ => ls.GetLocalizedString("StateInvalid"))
                .MaximumLength(MaxStateLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("StateMaxLength"), MaxStateLength));

            RuleFor(a => a.Country)
                .NotEmpty()
                .When(a => !string.IsNullOrEmpty(a.Country))
                .WithMessage(_ => ls.GetLocalizedString("CountryRequired"))
                .MaximumLength(MaxCountryLength)
                .WithMessage(_ => string.Format(ls.GetLocalizedString("CountryMaxLength"), MaxCountryLength));

        
    }
} 