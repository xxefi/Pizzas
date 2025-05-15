using FluentValidation;
using Pizzas.Core.Abstractions.Services.Main;
using Pizzas.Core.Dtos.Create;
using static Pizzas.Core.Constants.ValidationConstants;
using static Pizzas.Common.Utilities.ValidationUtilities;

namespace Pizzas.Application.Validators.Create;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator(ILocalizationService ls)
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