using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;

namespace Pizzas.Application.Validators.Update;

public class UpdateReviewValidator : AbstractValidator<UpdateReviewDto>
{
    public UpdateReviewValidator(ILocalizationService ls)
    {
        RuleFor(review => review.Content)
            .Must(content => string.IsNullOrWhiteSpace(content) || content.Length > 0)
            .WithMessage(_ => ls.GetLocalizedString("ContentRequired"));

        RuleFor(review => review.Rating)
            .Must(rating => !rating.HasValue || (rating >= 1 && rating <= 5))
            .WithMessage(_ => ls.GetLocalizedString("RatingOutOfRange"));
    }
}