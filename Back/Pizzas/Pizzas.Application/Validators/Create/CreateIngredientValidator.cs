using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreateIngredientValidator : AbstractValidator<CreateIngredientDto>
{
    public CreateIngredientValidator(ILocalizationService ls)
    {
        RuleFor(ingredient => ingredient.Name)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("IngredientNameRequired"))
            .Must(IsValidName)
            .WithMessage(_ => ls.GetLocalizedString("InvalidIngredientName"));
    }
}