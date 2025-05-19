using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Update;
using static Pizzas.Core.Constants.ValidationConstants;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Update;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator(ILocalizationService ls)
    {

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(_ => ls.GetLocalizedString("CategoryNameRequired"))
            .Must(IsValidCategoryName)
            .WithMessage(_ => ls.GetLocalizedString("CategoryNameInvalid"))
            .MaximumLength(MaxCategoryNameLength)
            .WithMessage(_ => string.Format(ls.GetLocalizedString("CategoryNameMaxLength"), MaxCategoryNameLength));
    }
} 