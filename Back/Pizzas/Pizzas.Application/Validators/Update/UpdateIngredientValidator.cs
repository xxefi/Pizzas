using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Update;

public class UpdateIngredientValidator : AbstractValidator<UpdateIngredientDto>
{
    public UpdateIngredientValidator(ILocalizationService ls)
    {
        RuleFor(ingredient => ingredient.Name)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("IngredientNameRequired"))
            .Must(IsValidName)
            .WithMessage(_ => ls.GetLocalizedString("InvalidIngredientName"));
    }
}