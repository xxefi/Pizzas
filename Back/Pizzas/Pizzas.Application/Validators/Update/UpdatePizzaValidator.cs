using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Update;

public class UpdatePizzaValidator : AbstractValidator<UpdatePizzaDto>
{
    public UpdatePizzaValidator(ILocalizationService ls)
    {
        RuleFor(pizza => pizza.Name)
            .Must(IsValidPizzaName)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaName"));

        RuleFor(pizza => pizza.Description)
            .Must(IsValidPizzaDescription)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaDescription"));
        

        RuleFor(pizza => pizza.Ingredients)
            .Must(IsValidUpdateIngredients)
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaIngredients"));

        RuleFor(pizza => pizza.Size)
            .Must(size => IsValidPizzaSize(size)) 
            .WithMessage(_ => ls.GetLocalizedString("InvalidPizzaSize"));
    }
}