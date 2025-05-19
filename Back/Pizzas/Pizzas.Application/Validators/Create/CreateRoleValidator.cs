using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;

namespace Pizzas.Application.Validators.Create;

public class CreateRoleValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleValidator(ILocalizationService ls)
    {
        RuleFor(role => role.Name)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("RoleNameRequired"));
        
    }
}