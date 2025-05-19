using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;

namespace Pizzas.Application.Validators.Create;

public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
{
    public CreateReviewValidator(ILocalizationService ls)
    {
        RuleFor(review => review.PizzaId)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("PizzaIdRequired"));

        RuleFor(review => review.Content)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("ContentRequired"));

        RuleFor(review => review.Rating)
            .InclusiveBetween(1, 5)
            .WithMessage(_ => ls.GetLocalizedString("RatingOutOfRange"));
    }
}