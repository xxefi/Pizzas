using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreatePizzaValidator : AbstractValidator<CreatePizzaDto>
{
    public CreatePizzaValidator(ILocalizationService ls)
    {
        RuleFor(pizza => pizza.Name)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PizzaNameRequired"))
            .Must(IsValidPizzaName)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaName"));

        RuleFor(pizza => pizza.Description)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PizzaDescriptionRequired"))
            .Must(IsValidPizzaDescription)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaDescription"));
        

        RuleFor(pizza => pizza.Ingredients)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PizzaIngredientsRequired"))
            .Must(IsValidCreateIngredients)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaIngredients"));

        RuleFor(pizza => pizza.Size)
            .Must(size => IsValidPizzaSize(size)) 
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaSize"));
    }
}