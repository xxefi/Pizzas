using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreatePermissionValidator : AbstractValidator<CreatePermissionDto>
{
    public CreatePermissionValidator(ILocalizationService ls)
    {
        RuleFor(permission => permission.Name)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PermissionNameRequired"))
            .Must(IsValidPermissionName)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPermissionName"));
    }
}