using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Update;

public class UpdatePermissionValidator : AbstractValidator<UpdatePermissionDto>
{
    public UpdatePermissionValidator(ILocalizationService ls)
    {
        RuleFor(permission => permission.Name)
            .Must(IsValidPermissionName)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPermissionName"));

        RuleFor(permission => permission.Description)
            .Must(IsValidDescription)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPermissionDescription"));
    }
}