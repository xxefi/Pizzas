using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Application.Validators.Update;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator(ILocalizationService ls)
    {
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("UsernameRequired"));

        RuleFor(user => user.FirstName)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("FirstNameRequired"));

        RuleFor(user => user.LastName)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("LastNameRequired"));

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("EmailRequired"))
            .EmailAddress()
            .WithMessage(_ => ls.GetLocalizedString("InvalidEmail"));
    }
}