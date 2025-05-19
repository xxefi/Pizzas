using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Application.Validators.Update;

public class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleValidator(ILocalizationService ls)
    {
        RuleFor(role => role.Name)
            .Must(name => string.IsNullOrWhiteSpace(name) || name.Length > 0)
            .WithMessage(_ => ls.GetLocalizedString("RoleNameRequired"));
    }
}