using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;

namespace Pizzas.Application.Validators.Create;

public class CreateUserValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserValidator(ILocalizationService ls)
    {
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("UsernameRequired"))
            .Must(username => username == username.ToLowerInvariant())
            .WithMessage(_ => ls.GetLocalizedString("UsernameMustBeLowercase"));

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

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PasswordRequired"))
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")
            .WithMessage(_ => ls.GetLocalizedString("InvalidPassword"));
    }
}